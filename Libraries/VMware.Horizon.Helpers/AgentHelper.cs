using System;
using Microsoft.Win32;

namespace VMware.Horizon.Helpers
{
    public static class AgentHelper
    {
        private const string AgentPath = @"SOFTWARE\VMware, Inc.\VMware VDM";

        public static bool IsAgentInstalled()
        {
            try
            {
                using (var machineHive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                {
                    using (var agentKey = machineHive.OpenSubKey(AgentPath))
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
}