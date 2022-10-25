using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    [TypeLibType(4160)]
    [Guid("4BED49D6-FA24-424E-8ED4-90FA4429F91F")]
    [ComImport]
    public interface IVMwareHorizonClientVChanEvents
    {
        [DispId(1)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void InitEventProc([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken, [In] uint rc);

        [DispId(2)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ConnectEventProc([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] uint eventType,
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] [In]
            Array eventData);

        [DispId(3)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ReadEventProc(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] uint channelHandle,
            [In] uint eventType,
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UI1)] [In]
            Array eventData,
            [In] uint totalLength,
            [In] uint dataFlags);
    }
}