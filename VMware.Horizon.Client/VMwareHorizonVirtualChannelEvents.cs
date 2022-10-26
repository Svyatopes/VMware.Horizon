using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using VMware.Horizon.Helpers;
using VMware.Horizon.Interop;
using VMware.Horizon.PipeMessages;
using VMware.Horizon.VirtualChannel.RDPVCBridgeInterop;

namespace VMware.Horizon.Client;

public class VMwareHorizonVirtualChannelEvents : IVMwareHorizonClientVChanEvents
{
    private readonly Action<int, string> _callbackMessage;

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
        switch (currentEventType)
        {
            case VirtualChannelStructures.ChannelEvents.Connected:
                try
                {
                    HorizonClientVirtualChannel.VirtualChannelOpen(serverId, sessionToken, Constants.VirtualChannelName,
                        out var mChannelHandle);
                    _callbackMessage.Invoke(3, "!! VirtualChannelOpen() succeeded");
                    VmwareChannelClient.GetInstance()
                        .RegisterVirtualChannelData(serverId, sessionToken, mChannelHandle);
                }
                catch (Exception ex)
                {
                    _callbackMessage.Invoke(3,
                        $"VirtualChannelOpen() failed: {ex}");
                }

                break;

            case VirtualChannelStructures.ChannelEvents.Disconnected:
                VmwareChannelClient.GetInstance().VirtualChannelDisconnected(serverId);
                break;
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
            $"ReadEventProc(): {currentEventType} - channelHandle {channelHandle} - Flags: {dataFlags} - Length: {totalLength}");

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
                        SendData(serverId, sessionToken, channelHandle, new ChannelResponse());
                        break;
                    case CommandType.Ping:
                        SendData(serverId, sessionToken, channelHandle, new ChannelResponse());
                        break;
                }
            }
            catch (Exception ex)
            {
                _callbackMessage.Invoke(3,
                    $"VirtualChannelWrite failed: {ex}");
            }
        }
    }

    public void SendData(uint serverId,
        string sessionToken, uint channelHandle, object objToSend)
    {
        HorizonClientVirtualChannel.VirtualChannelWrite(serverId, sessionToken, channelHandle,
            BinaryConverters.StringToBinary(
                JsonConvert.SerializeObject(objToSend)));
    }
}