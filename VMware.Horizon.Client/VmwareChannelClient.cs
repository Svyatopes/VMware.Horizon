using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VMware.Horizon.Helpers;
using VMware.Horizon.Interop;

namespace VMware.Horizon.Client;

public class VmwareChannelClient
{
    private static VmwareChannelClient _vmwareChannelClient;
    private static List<VirtualChannel> _virtualChannels;
    private static VMwareHorizonVirtualChannelEvents _vmwareHorizonVirtualChannelEvents;


    private VmwareChannelClient()
    {
        _virtualChannels = new List<VirtualChannel>();

        // Open Horizon Client Listener
        var vmMwareHorizonClient = (IVMwareHorizonClient4)new VMwareHorizonClient();
        IVMwareHorizonClientEvents5
            horizonEvents = new VMwareHorizonClientEvents(CallbackFunc);
        vmMwareHorizonClient.Advise2(horizonEvents,
            VmwHorizonClientAdviseFlags.VmwHorizonClientAdvise_DispatchCallbacksOnUIThread);
        GC.SuppressFinalize(vmMwareHorizonClient);
        CallbackFunc(3, "Opened Horizon API");

        // Register Virtual Channel Callback
        _vmwareHorizonVirtualChannelEvents = new VMwareHorizonVirtualChannelEvents(CallbackFunc);
        var Channels = new VMwareHorizonClientChannelDefinition[1];
        Channels[0] = new VMwareHorizonClientChannelDefinition(Constants.VirtualChannelName, 0);
        vmMwareHorizonClient.RegisterVirtualChannelConsumer2(Channels, _vmwareHorizonVirtualChannelEvents,
            out var apiObject);
        _vmwareHorizonVirtualChannelEvents.HorizonClientVirtualChannel = (IVMwareHorizonClientVChan)apiObject;
        GC.SuppressFinalize(_vmwareHorizonVirtualChannelEvents);
        CallbackFunc(3, "Opened Virtual Channel Listener");
    }

    public static VmwareChannelClient GetInstance()
    {
        if (_vmwareChannelClient == null)
        {
            _vmwareChannelClient = new VmwareChannelClient();
        }

        return _vmwareChannelClient;
    }

    public void RegisterVirtualChannelData(uint serverId, string sessionToken, uint channelHandle)
    {
        var virtualChannel = _virtualChannels.FirstOrDefault(x => x.ServerId == serverId);
        if (virtualChannel != null)
        {
            virtualChannel.IsConnected = true;
            virtualChannel.ChannelHandle = channelHandle;
            virtualChannel.SessionToken = sessionToken;
        }
        else
        {
            virtualChannel = new VirtualChannel
            {
                ChannelHandle = channelHandle,
                IsConnected = true,
                ServerId = serverId,
                SessionToken = sessionToken
            };
            _virtualChannels.Add(virtualChannel);
        }
    }

    public void VirtualChannelDisconnected(uint serverId)
    {
        var virtualChannel = _virtualChannels.FirstOrDefault(x => x.ServerId == serverId);
        if (virtualChannel != null)
        {
            virtualChannel.IsConnected = false;
        }
    }

    public void SendToAllConnectedChannels(object obj)
    {
        var connectedChannels = _virtualChannels.Where(x => x.IsConnected);
        foreach (var connectedChannel in connectedChannels)
        {
            _vmwareHorizonVirtualChannelEvents.SendData(connectedChannel.ServerId, connectedChannel.SessionToken,
                connectedChannel.ChannelHandle, obj);
        }
    }

    private void CallbackFunc(int severity, string message)
    {
        WriteMessageToLog($"severity: {severity}, message: {message}");
    }

    private void WriteMessageToLog(string message)
    {
        const string path = "C:\\Temp\\HorizonClient.log";
        if (File.Exists(path))
        {
            File.AppendAllText(path, DateTime.Now + message + Environment.NewLine);
        }
        else
        {
            File.WriteAllText(path, DateTime.Now + message + Environment.NewLine);
        }
    }
}