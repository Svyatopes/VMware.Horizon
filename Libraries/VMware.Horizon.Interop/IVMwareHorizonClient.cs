using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    [InterfaceType(1)]
    [TypeLibType(256)]
    [Guid("1B3F2D66-8670-4787-BA9E-211C5817E479")]
    [ComImport]
    public interface IVMwareHorizonClient
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Advise([MarshalAs(UnmanagedType.IDispatch)] [In] object events, [In] VmwHorizonClientAdviseFlags flags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void Unadvise();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void GetServerInfo(
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)] out Array serverInfo);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ConnectToServer(
            [MarshalAs(UnmanagedType.BStr)] [In] string serverAddress,
            [MarshalAs(UnmanagedType.IUnknown)] [In]
            object authInfo,
            [In] VmwHorizonClientLaunchFlags launchFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ConnectToLaunchItem(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string id,
            [In] VmwHorizonClientProtocol protocol,
            [In] VmwHorizonClientDisplayType displayType);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void RegisterVirtualChannelConsumer(
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)] [In] Array channels,
            [MarshalAs(UnmanagedType.IDispatch)] [In] object eventsObject,
            [MarshalAs(UnmanagedType.IDispatch)] out object api);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ShutdownClient([In] uint serverId, [In] uint suppressPrompts);
    }
}