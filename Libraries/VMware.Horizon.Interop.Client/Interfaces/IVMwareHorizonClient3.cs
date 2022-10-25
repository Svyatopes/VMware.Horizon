using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    [Guid("8594D1D3-5E5E-43D0-BB1A-74234DAFF905")]
    [TypeLibType(256)]
    [InterfaceType(1)]
    [ComImport]
    public interface IVMwareHorizonClient3 : IVMwareHorizonClient2
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void Advise([MarshalAs(UnmanagedType.IDispatch)] [In] object events,
            [In] VmwHorizonClientAdviseFlags flags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void Unadvise();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void GetServerInfo(
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)]
            out Array serverInfo);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void ConnectToServer(
            [MarshalAs(UnmanagedType.BStr)] [In] string serverAddress,
            [MarshalAs(UnmanagedType.IUnknown)] [In]
            object authInfo,
            [In] VmwHorizonClientLaunchFlags launchFlags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void ConnectToLaunchItem(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string id,
            [In] VmwHorizonClientProtocol protocol,
            [In] VmwHorizonClientDisplayType displayType);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void RegisterVirtualChannelConsumer(
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)] [In]
            Array channels,
            [MarshalAs(UnmanagedType.IDispatch)] [In]
            object eventsObject,
            [MarshalAs(UnmanagedType.IDispatch)] out object api);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void ShutdownClient([In] uint serverId, [In] uint suppressPrompts);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void Advise2([MarshalAs(UnmanagedType.IUnknown)] [In] object events,
            [In] VmwHorizonClientAdviseFlags flags);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void RegisterVirtualChannelConsumer2(
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)] [In]
            Array channels,
            [MarshalAs(UnmanagedType.IUnknown)] [In]
            object eventsObject,
            [MarshalAs(UnmanagedType.IUnknown)] out object api);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void ConnectToLaunchItem2(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string id,
            [In] VmwHorizonClientProtocol protocol,
            [In] VmwHorizonClientDisplayType displayType,
            [MarshalAs(UnmanagedType.BStr)] [In] string samlArtifact);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void DisconnectProtocolSession([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void LogoffProtocolSession([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken);
    }
}