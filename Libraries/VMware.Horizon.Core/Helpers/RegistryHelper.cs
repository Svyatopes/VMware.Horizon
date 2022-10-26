using System;
using Microsoft.Win32;

namespace VMware.Horizon.Client.Helpers;

public static class RegistryHelper
{
    private const string VmWareClientRegKeyPath = @"SOFTWARE\VMware, Inc.\VMware VDM\Client";
    private const string VmWareAgentRegKeyPath = @"SOFTWARE\VMware, Inc.\VMware VDM";

    public static bool IsAgentInstalled()
    {
        try
        {
            using (var machineHive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (var agentKey = machineHive.OpenSubKey(VmWareAgentRegKeyPath))
                {
                    if (agentKey != null)
                    {
                        var agentVersion = agentKey.GetValue("ProductVersion", null);
                        if (agentVersion != null)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

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
        catch (Exception)
        {
            return false;
        }
    }
}