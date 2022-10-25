using System;
using Microsoft.Win32;

namespace VMware.Horizon.Agent.Helpers;

public static class RegistryHelper
{
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
        catch (Exception ex)
        {
            return false;
        }
    }
}