using System;
using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    public static class RdpvcBridge
    {
        [DllImport("vdp_rdpvcbridge.dll", SetLastError = true)]
        public static extern IntPtr VDP_VirtualChannelOpen(IntPtr server, int sessionId,
            [MarshalAs(UnmanagedType.LPStr)] string virtualName);

        [DllImport("vdp_rdpvcbridge.dll", SetLastError = true)]
        public static extern bool VDP_VirtualChannelWrite(IntPtr channelHandle, byte[] data, int length,
            ref int bytesWritten);

        [DllImport("vdp_rdpvcbridge.dll", SetLastError = true)]
        public static extern bool VDP_VirtualChannelRead(IntPtr channelHandle, int timeOut, byte[] data, int length,
            ref int bytesReaded);

        [DllImport("vdp_rdpvcbridge.dll", SetLastError = true)]
        public static extern bool VDP_VirtualChannelClose(IntPtr channelHandle);


        [DllImport("vdp_rdpvcbridge", SetLastError = true)]
        public static extern bool VDP_IsViewSession(uint sessionId);
    }
}