using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    [Guid("D505DF9B-DC25-4741-A387-0DA2B83AC291")]
    [TypeLibType(4160)]
    [ComImport]
    public interface IVMwareHorizonClientVChan
    {
        [DispId(1)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void VirtualChannelOpen(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [MarshalAs(UnmanagedType.BStr)] [In] string channelName,
            out uint openHandle);

        [DispId(2)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void VirtualChannelClose([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] uint openHandle);

        [DispId(3)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void VirtualChannelWrite([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] uint openHandle,
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] [In] Array data);
    }
}