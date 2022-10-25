using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using VMware.Horizon.Agent.Helpers;
using VMware.Horizon.Interop;
using VMware.Horizon.PipeMessages;

namespace VMware.Horizon.Agent;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private VirtualChannelAgent _vca;

    public MainWindow()
    {
        if (!CheckAvailableForRun())
        {
            MessageBox.Show("Horizon client not installed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
        }

        InitializeListeners();
        InitializeComponent();
    }

    private void InitializeListeners()
    {
        if (!RdpvcBridge.VDP_IsViewSession((uint)Process.GetCurrentProcess().SessionId))
        {
            MessageBox.Show("This is not a Horizon Session, closing", "Error", MessageBoxButton.OK,
                MessageBoxImage.Asterisk);
            Shutdown();
        }
        else
        {
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
            try
            {
                OpenAgent();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open virtual channel: {ex}", "Failed to start");
                Shutdown();
            }
        }
    }

    private async Task SendMessageToVmWare(string text)
    {
        await _vca.SendMessage(new ChannelCommand(CommandType.Message, new VmWareMessage { Text = text }),
            typeof(ChannelResponse));
    }

    private void OpenAgent()
    {
        _vca = new VirtualChannelAgent("VVCAM");
        _vca.LogMessage += AgentThread_ThreadMessage;
    }

    private static void AgentThread_ThreadMessage(int severity, string message)
    {
        LogMessage($"Sev: {severity} - Message: {message}");
    }

    private static void LogMessage(string message)
    {
        const string path = "C:\\Temp\\HorizonAgent.log";
        if (File.Exists(path))
        {
            File.AppendAllText(path, DateTime.Now + message + Environment.NewLine);
        }
        else
        {
            File.WriteAllText(path, DateTime.Now + message + Environment.NewLine);
        }
    }

    private async void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
    {
        switch (e.Reason)
        {
            case SessionSwitchReason.ConsoleDisconnect:
            case SessionSwitchReason.RemoteDisconnect:
                CloseAgent();
                break;
            case SessionSwitchReason.RemoteConnect:
            case SessionSwitchReason.ConsoleConnect:
                var isView = RdpvcBridge.VDP_IsViewSession((uint)Process.GetCurrentProcess().SessionId);
                if (isView)
                {
                    // giving the audio component a chance to catchup
                    await Task.Run(() => { Task.Delay(3000); });
                    OpenAgent();
                }

                break;
        }
    }

    private void CloseAgent()
    {
        if (_vca != null)
        {
            _vca.LogMessage -= AgentThread_ThreadMessage;
            _vca = null;
        }
    }

    private void Shutdown()
    {
        Application.Current.Shutdown();
    }

    private static bool CheckAvailableForRun() =>
        RegistryHelper.IsAgentInstalled();

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        await SendMessageToVmWare("First button clicked");
    }

    private async void ButtonBase2_OnClick(object sender, RoutedEventArgs e)
    {
        await SendMessageToVmWare("Second button clicked");
    }
}