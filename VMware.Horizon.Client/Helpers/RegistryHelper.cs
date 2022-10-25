using System;
using Microsoft.Win32;

namespace VMware.Horizon.Client.Helpers;

public static class RegistryHelper
{
    private const string VmWareClientRegKeyPath = @"SOFTWARE\VMware, Inc.\VMware VDM\Client";

    public static bool IsHorizonClientInstalled()
    {
        try
        {
            using (var machineHive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            {
                using (var clientKey = machineHive.OpenSubKey(VmWareClientRegKeyPath))
                {
                    if (clientKey != null)
                    {
                        var clientVersion = clientKey.GetValue("Version", null);
                        if (clientVersion != null)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}