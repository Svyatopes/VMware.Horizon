using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    [Guid("8987B676-630A-4711-A9C7-025064F7B2C6")]
    [TypeLibType(2)]
    [ClassInterface((short)0)]
    [ComImport]
    public class VMwareHorizonClientClass : VMwareHorizonClient
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void Advise([MarshalAs(UnmanagedType.IDispatch)] [In] object events,
            [In] VmwHorizonClientAdviseFlags flags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void Unadvise();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void GetServerInfo(
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
            out Array serverInfo);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void ConnectToServer(
            [MarshalAs(UnmanagedType.BStr)] [In] string serverAddress,
            [MarshalAs(UnmanagedType.IUnknown)] [In]
            object authInfo,
            [In] VmwHorizonClientLaunchFlags launchFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void ConnectToLaunchItem(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string id,
            [In] VmwHorizonClientProtocol protocol,
            [In] VmwHorizonClientDisplayType displayType);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void RegisterVirtualChannelConsumer(
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)] [In]
            Array channels,
            [MarshalAs(UnmanagedType.IDispatch)] [In]
            object eventsObject,
            [MarshalAs(UnmanagedType.IDispatch)] out object api);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        public virtual extern void ShutdownClient([In] uint serverId, [In] uint suppressPrompts);
    }
}