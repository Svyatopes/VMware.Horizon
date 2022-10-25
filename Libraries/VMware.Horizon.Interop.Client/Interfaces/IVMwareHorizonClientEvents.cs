using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    [TypeLibType(4160)]
    [Guid("FFD18CC1-BEB8-4828-94F1-EF75613A83FB")]
    [ComImport]
    public interface IVMwareHorizonClientEvents
    {
        [DispId(1)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnStarted();

        [DispId(2)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnExit();

        [DispId(3)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnConnecting([MarshalAs(UnmanagedType.IUnknown)] [In] object serverInfo);

        [DispId(4)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnConnectFailed([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string errorMessage);

        [DispId(5)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnAuthenticationRequested([In] uint serverId, [In] VmwHorizonClientAuthType authType);

        [DispId(6)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnAuthenticating([In] uint serverId, [In] VmwHorizonClientAuthType authType,
            [MarshalAs(UnmanagedType.BStr)] [In] string user);

        [DispId(7)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnAuthenticationDeclined([In] uint serverId, [In] VmwHorizonClientAuthType authType);

        [DispId(8)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnAuthenticationFailed(
            [In] uint serverId,
            [In] VmwHorizonClientAuthType authType,
            [MarshalAs(UnmanagedType.BStr)] [In] string errorMessage,
            [In] int retryAllowed);

        [DispId(9)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnLoggedIn([In] uint serverId);

        [DispId(10)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnDisconnected([In] uint serverId);

        [DispId(11)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnReceivedLaunchItems([In] uint serverId,
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)] [In]
            Array launchItems);

        [DispId(12)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnLaunchingItem(
            [In] uint serverId,
            [In] VmwHorizonLaunchItemType type,
            [MarshalAs(UnmanagedType.BStr)] [In] string launchItemId,
            [In] VmwHorizonClientProtocol protocol);

        [DispId(13)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnItemLaunchSucceeded([In] uint serverId, [In] VmwHorizonLaunchItemType type,
            [MarshalAs(UnmanagedType.BStr)] [In] string launchItemId);

        [DispId(14)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnItemLaunchFailed(
            [In] uint serverId,
            [In] VmwHorizonLaunchItemType type,
            [MarshalAs(UnmanagedType.BStr)] [In] string launchItemId,
            [MarshalAs(UnmanagedType.BStr)] [In] string errorMessage);

        [DispId(15)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnNewProtocolSessionCreated(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] VmwHorizonClientProtocol protocol,
            [In] VmwHorizonClientSessionType type,
            [MarshalAs(UnmanagedType.BStr)] [In] string clientId);

        [DispId(16)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnProtocolSessionDisconnected(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] uint connectionFailed,
            [MarshalAs(UnmanagedType.BStr)] [In] string errorMessage);

        [DispId(17)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnSeamlessWindowsModeChanged([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] uint enabled);

        [DispId(18)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnSeamlessWindowAdded(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [MarshalAs(UnmanagedType.BStr)] [In] string windowPath,
            [MarshalAs(UnmanagedType.BStr)] [In] string entitlementId,
            [In] int windowId,
            [In] long windowHandle,
            [In] VmwHorizonClientSeamlessWindowType type);

        [DispId(19)]
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnSeamlessWindowRemoved([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] int windowId);
    }
}