namespace VMware.Horizon.Interop
{
    public enum VmwHorizonClientLaunchFlags
    {
        VmwHorizonClientLaunch_None = 0,
        VmwHorizonClientLaunch_NonInteractive = 1,
        VmwHorizonClientLaunch_Unattended = 2,
        VmwHorizonClientLaunch_ConnectUSBOnStartup = 4,
        VmwHorizonClientLaunch_ConnectUSBOnInsert = 8,
        VmwHorizonClientLaunch_NoVMwareAddins = 16, // 0x00000010
        VmwHorizonClientLaunch_HideClientAfterLaunchSession = 32, // 0x00000020
        VmwHorizonClientLaunch_SingleAutoConnect = 64, // 0x00000040
        VmwHorizonClientLaunch_AlwaysResumeAppSessions = 128, // 0x00000080
        VmwHorizonClientLaunch_NeverResumeAppSessions = 256, // 0x00000100
        VmwHorizonClientLaunch_LaunchMinimized = 512 // 0x00000200
    }
}