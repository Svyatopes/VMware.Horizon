using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VMware.Horizon.Interop;
using VMware.Horizon.PipeMessages;
using VMware.Horizon.VirtualChannel.RDPVCBridgeInterop;

namespace VMware.Horizon.Client;

public class VMwareHorizonVirtualChannelEvents : IVMwareHorizonClientVChanEvents
{
    private readonly Action<int, string> _callbackMessage;
    private uint _mChannelHandle;

    private int _mPingTestCurLen;
    private byte[] _mPingTestMsg;
    public IVMwareHorizonClientVChan HorizonClientVirtualChannel;

    public VMwareHorizonVirtualChannelEvents(Action<int, string> callbackMessage)
    {
        _callbackMessage = callbackMessage;
    }

    public void ConnectEventProc(uint serverId, string sessionToken, uint eventType, Array eventData)
    {
        var currentEventType =
            (VirtualChannelStructures.ChannelEvents)eventType;
        _callbackMessage.Invoke(3, "ConnectEventProc() called: " + currentEventType);
        //  SharedObjects.hvm.ThreadMessage?.Invoke(3, "ConnectEventProc() called ");

        if (eventType == (uint)VirtualChannelStructures.ChannelEvents.Connected)

        {
            try
            {
                HorizonClientVirtualChannel.VirtualChannelOpen(serverId, sessionToken, "VVCAM",
                    out _mChannelHandle);
                _callbackMessage.Invoke(3, "!! VirtualChannelOpen() succeeded");
            }
            catch (Exception ex)
            {
                _callbackMessage.Invoke(3,
                    string.Format("VirtualChannelOpen() failed: {0}", ex));
                _mChannelHandle = 0;
            }
        }
    }

    public void InitEventProc(uint serverId, string sessionToken, uint rc)
    {
        _callbackMessage.Invoke(3, "InitEventProc()");
    }

    public void ReadEventProc(uint serverId, string sessionToken, uint channelHandle, uint eventType,
        Array eventData, uint totalLength, uint dataFlags)
    {
        var currentEventType =
            (VirtualChannelStructures.ChannelEvents)eventType;
        var cf =
            (VirtualChannelStructures.ChannelFlags)dataFlags;
        _callbackMessage.Invoke(3,
            "ReadEventProc(): " + currentEventType + " - Flags: " + cf + " - Length: " +
            totalLength);

        var isFirst = (dataFlags & (uint)VirtualChannelStructures.ChannelFlags.First) != 0;
        var isLast = (dataFlags & (uint)VirtualChannelStructures.ChannelFlags.Last) != 0;

        if (isFirst)
        {
            _mPingTestMsg = new byte[totalLength];
            _mPingTestCurLen = 0;
        }

        eventData.CopyTo(_mPingTestMsg, _mPingTestCurLen);
        _mPingTestCurLen += eventData.Length;

        if (isLast)
        {
            if (totalLength != _mPingTestMsg.Length)
            {
                _callbackMessage.Invoke(3,
                    "Received {mPingTestMsg.Length} bytes but expected {totalLength} bytes!");
            }

            var message = BinaryConverters.BinaryToString(_mPingTestMsg);
            var channelCommand = JsonConvert.DeserializeObject<ChannelCommand>(message);
            _callbackMessage.Invoke(3,
                "Received: " + channelCommand.CommandType + " = " +
                BinaryConverters.BinaryToString(_mPingTestMsg));

            try
            {
                switch (channelCommand.CommandType)
                {
                    case CommandType.Message:
                        var jo = (JObject)channelCommand.CommandParameters;
                        var sv = jo.ToObject<VmWareMessage>();
                        _callbackMessage.Invoke(1, sv.Text);
                        HorizonClientVirtualChannel.VirtualChannelWrite(serverId, sessionToken, channelHandle,
                            BinaryConverters.StringToBinary(
                                JsonConvert.SerializeObject(new ChannelResponse())));
                        break;
                    case CommandType.Ping:
                        HorizonClientVirtualChannel.VirtualChannelWrite(serverId, sessionToken, channelHandle,
                            BinaryConverters.StringToBinary(
                                JsonConvert.SerializeObject(new ChannelResponse())));
                        break;
                }
            }
            catch (Exception ex)
            {
                _callbackMessage.Invoke(3,
                    string.Format("VirtualChannelWrite failed: {0}", ex));
            }
        }
    }
}