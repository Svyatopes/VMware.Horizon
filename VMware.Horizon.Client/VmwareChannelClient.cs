using System;
using System.IO;
using VMware.Horizon.Interop;

namespace VMware.Horizon.Client;

public class VmwareChannelClient
{
    private static VmwareChannelClient _channelClient;
    private readonly VMwareHorizonVirtualChannelEvents _channelEvents;
    private readonly IVMwareHorizonClient4 _vmhc;

    private VmwareChannelClient()
    {
        // Open Horizon Client Listener
        _vmhc = (IVMwareHorizonClient4)new VMwareHorizonClient();
        IVMwareHorizonClientEvents5
            HorizonEvents = new VMwareHorizonClientEvents(CallbackFunc);
        _vmhc.Advise2(HorizonEvents, VmwHorizonClientAdviseFlags.VmwHorizonClientAdvise_DispatchCallbacksOnUIThread);
        GC.SuppressFinalize(_vmhc);
        CallbackFunc(3, "Opened Horizon API");

        // Register Virtual Channel Callback
        _channelEvents = new VMwareHorizonVirtualChannelEvents(CallbackFunc);
        var Channels = new VMwareHorizonClientChannelDefinition[1];
        Channels[0] = new VMwareHorizonClientChannelDefinition("VVCAM", 0);
        _vmhc.RegisterVirtualChannelConsumer2(Channels, _channelEvents, out var apiObject);
        _channelEvents.HorizonClientVirtualChannel = (IVMwareHorizonClientVChan)apiObject;
        GC.SuppressFinalize(_channelEvents);
        CallbackFunc(3, "Opened Virtual Channel Listener");
    }

    public static VmwareChannelClient GetInstance()
    {
        if (_channelClient == null)
        {
            _channelClient = new VmwareChannelClient();
        }

        return _channelClient;
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