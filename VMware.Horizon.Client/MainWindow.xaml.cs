using System.Windows;
using VMware.Horizon.Client.Helpers;

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
}