using System;
using Microsoft.Win32;

namespace VMware.Horizon.Helpers
{
    public static class ClientHelper
    {
        private const string ClientPath = @"SOFTWARE\VMware, Inc.\VMware VDM\Client";

        public static bool IsHorizonClientInstalled()
        {
            try
            {
                using (var machineHive = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
                {
                    using (var clientKey = machineHive.OpenSubKey(ClientPath))
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
}