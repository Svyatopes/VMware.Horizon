using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VMware.Horizon.Interop;
using VMware.Horizon.PipeMessages;
using VMware.Horizon.VirtualChannel.RDPVCBridgeInterop;

namespace VMware.Horizon.Agent;

public class VirtualChannelAgent
{
    public delegate void ThreadMessageCallback(int severity, string message);


    public VirtualChannelAgent(string channelName)
    {
        Lock = new object();
        var sid = Process.GetCurrentProcess().SessionId;
        Handle = RdpvcBridge.VDP_VirtualChannelOpen(VirtualChannelStructures.WTS_CURRENT_SERVER_HANDLE, sid,
            channelName);
        if (Handle == IntPtr.Zero)
        {
            var er = Marshal.GetLastWin32Error();
            throw new Exception("Could not Open the virtual Channel: " + er);
        }
    }

    public IntPtr Handle { get; set; }
    public object Lock { get; set; }

    public bool Connected { get; private set; }
    public event ThreadMessageCallback LogMessage;

    public void Destroy()
    {
        RdpvcBridge.VDP_VirtualChannelClose(Handle);
        Handle = IntPtr.Zero;
        Connected = false;
    }

    private void ChangeConnectivity(bool connected)
    {
        if (Connected != connected)
        {
            Connected = connected;
        }
    }

    public async Task<object> SendMessage(ChannelCommand messageObject, Type returnType)
    {
        return await Task.Run(() =>
        {
            LogMessage?.Invoke(3, "send requested, awaiting lock.");

            lock (Lock)
            {
                LogMessage?.Invoke(3, "send requested, lock received.");

                var written = 0;
                var serialisedMessage = JsonConvert.SerializeObject(messageObject);
                LogMessage?.Invoke(3, $"Sending Message : {serialisedMessage}");
                var msg = BinaryConverters.StringToBinary(serialisedMessage);
                var SendResult = RdpvcBridge.VDP_VirtualChannelWrite(Handle, msg, msg.Length, ref written);
                LogMessage?.Invoke(3,
                    string.Format("Sending Message result: {0} - Written: {1}", SendResult, written));
                if (!SendResult)
                {
                    LogMessage?.Invoke(2, "Sending the command was not succesful");
                    ChangeConnectivity(false);
                    return null;
                }

                var buffer = new byte[10240];
                var actualRead = 0;

                var ReceiveResult =
                    RdpvcBridge.VDP_VirtualChannelRead(Handle, 5000, buffer, buffer.Length, ref actualRead);
                LogMessage?.Invoke(3,
                    string.Format("VDP_VirtualChannelRead result: {0} - ActualRead: {1}", ReceiveResult,
                        actualRead));
                if (!ReceiveResult)
                {
                    ChangeConnectivity(false);
                    LogMessage?.Invoke(3, "Did not receive a response in a timely fashion or we received an error");
                    return null;
                }

                var receivedContents = new byte[actualRead];
                Buffer.BlockCopy(buffer, 0, receivedContents, 0, actualRead);
                var serialisedResponse = BinaryConverters.BinaryToString(receivedContents);
                LogMessage?.Invoke(3, string.Format("Received: {0}", serialisedResponse));
                return JsonConvert.DeserializeObject(serialisedResponse, returnType, (JsonSerializerSettings)null);
            }
        });
    }


    ~VirtualChannelAgent()
    {
        if (Handle != IntPtr.Zero)
        {
            try
            {
                RdpvcBridge.VDP_VirtualChannelClose(Handle);
            }
            catch
            {
                // ignored
            }
        }
    }
}