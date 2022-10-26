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

        Connected = true;
        StartListeningBackground();
    }

    private IntPtr Handle { get; set; }
    private object Lock { get; }

    private bool Connected { get; set; }
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

    private void StartListeningBackground()
    {
        Task.Run(() =>
        {
            while (Connected)
                lock (Lock)
                {
                    var buffer = new byte[10240];
                    var actualRead = 0;

                    var receiveResult =
                        RdpvcBridge.VDP_VirtualChannelRead(Handle, 5000, buffer, buffer.Length, ref actualRead);
                    LogMessage?.Invoke(3, string.Format("VDP_VirtualChannelRead result: {0} - ActualRead: {1}",
                        receiveResult,
                        actualRead));
                    if (!receiveResult)
                    {
                        LogMessage?.Invoke(3, "Did not receive a response in a timely fashion or we received an error");
                        continue;
                    }

                    var receivedContents = new byte[actualRead];
                    Buffer.BlockCopy(buffer, 0, receivedContents, 0, actualRead);
                    var serialisedResponse = BinaryConverters.BinaryToString(receivedContents);
                    LogMessage?.Invoke(3, $"Received: {serialisedResponse}");
                    var command = JsonConvert.DeserializeObject(serialisedResponse, typeof(ChannelCommand),
                        (JsonSerializerSettings)null);
                    if (command != null)
                    {
                        LogMessage?.Invoke(3, "Deserialized command");
                    }

                    var response = JsonConvert.DeserializeObject(serialisedResponse, typeof(ChannelResponse),
                        (JsonSerializerSettings)null);
                    if (response != null)
                    {
                        LogMessage?.Invoke(3, "Deserialized response");
                    }
                }
        });
    }

    public async Task SendMessage(ChannelCommand messageObject)
    {
        await Task.Run(() =>
        {
            LogMessage?.Invoke(3, "send requested, awaiting lock.");

            lock (Lock)
            {
                LogMessage?.Invoke(3, "send requested, lock received.");

                var written = 0;
                var serialisedMessage = JsonConvert.SerializeObject(messageObject);
                LogMessage?.Invoke(3, $"Sending Message : {serialisedMessage}");
                var msg = BinaryConverters.StringToBinary(serialisedMessage);
                var sendResult = RdpvcBridge.VDP_VirtualChannelWrite(Handle, msg, msg.Length, ref written);
                LogMessage?.Invoke(3,
                    $"Sending Message result: {sendResult} - Written: {written}");
                if (!sendResult)
                {
                    LogMessage?.Invoke(2, "Sending the command was not successful");
                    ChangeConnectivity(false);
                }
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