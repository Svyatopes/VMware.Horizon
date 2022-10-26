using System.Windows;
using VMware.Horizon.Client.Helpers;
using VMware.Horizon.PipeMessages;

namespace VMware.Horizon.Client;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        if (!CheckAvailableForRun())
        {
            MessageBox.Show("Horizon client not installed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }

        VmwareChannelClient.GetInstance();

        InitializeComponent();
    }

    private static bool CheckAvailableForRun() => RegistryHelper.IsHorizonClientInstalled();

    private void FirstButton_OnClick(object sender, RoutedEventArgs e)
    {
        VmwareChannelClient.GetInstance().SendToAllConnectedChannels(new ChannelCommand(CommandType.Message,
            new VmWareMessage { Text = "First button from client" }));
    }

    private void SecondButton_OnClick(object sender, RoutedEventArgs e)
    {
        VmwareChannelClient.GetInstance().SendToAllConnectedChannels(new ChannelCommand(CommandType.Message,
            new VmWareMessage { Text = "Second button from client" }));
    }
}