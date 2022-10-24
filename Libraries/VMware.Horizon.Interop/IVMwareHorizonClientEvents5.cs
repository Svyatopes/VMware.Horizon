using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    [TypeLibType(4352)]
    [Guid("D2E97B67-CDB7-4DE5-94BC-B4C727523B2A")]
    [ComImport]
    public interface IVMwareHorizonClientEvents5 : IVMwareHorizonClientEvents4
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnStarted();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnExit();

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnConnecting([MarshalAs(UnmanagedType.IUnknown)] [In] object serverInfo);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnConnectFailed([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string errorMessage);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnAuthenticationRequested([In] uint serverId, [In] VmwHorizonClientAuthType authType);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnAuthenticating([In] uint serverId, [In] VmwHorizonClientAuthType authType,
            [MarshalAs(UnmanagedType.BStr)] [In] string user);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnAuthenticationDeclined([In] uint serverId, [In] VmwHorizonClientAuthType authType);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnAuthenticationFailed(
            [In] uint serverId,
            [In] VmwHorizonClientAuthType authType,
            [MarshalAs(UnmanagedType.BStr)] [In] string errorMessage,
            [In] int retryAllowed);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnLoggedIn([In] uint serverId);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnDisconnected([In] uint serverId);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnReceivedLaunchItems([In] uint serverId,
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)] [In] Array launchItems);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnLaunchingItem(
            [In] uint serverId,
            [In] VmwHorizonLaunchItemType type,
            [MarshalAs(UnmanagedType.BStr)] [In] string launchItemId,
            [In] VmwHorizonClientProtocol protocol);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnItemLaunchSucceeded(
            [In] uint serverId,
            [In] VmwHorizonLaunchItemType type,
            [MarshalAs(UnmanagedType.BStr)] [In] string launchItemId);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnItemLaunchFailed(
            [In] uint serverId,
            [In] VmwHorizonLaunchItemType type,
            [MarshalAs(UnmanagedType.BStr)] [In] string launchItemId,
            [MarshalAs(UnmanagedType.BStr)] [In] string errorMessage);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnNewProtocolSessionCreated(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] VmwHorizonClientProtocol protocol,
            [In] VmwHorizonClientSessionType type,
            [MarshalAs(UnmanagedType.BStr)] [In] string clientId);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnProtocolSessionDisconnected(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] uint connectionFailed,
            [MarshalAs(UnmanagedType.BStr)] [In] string errorMessage);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnSeamlessWindowsModeChanged([In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken, [In] uint enabled);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnSeamlessWindowAdded(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [MarshalAs(UnmanagedType.BStr)] [In] string windowPath,
            [MarshalAs(UnmanagedType.BStr)] [In] string entitlementId,
            [In] int windowId,
            [In] long windowHandle,
            [In] VmwHorizonClientSeamlessWindowType type);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnSeamlessWindowRemoved([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] int windowId);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnUSBInitializeComplete([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnConnectUSBDeviceComplete([In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken, [In] uint isConnected);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnUSBDeviceError([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [MarshalAs(UnmanagedType.BStr)] [In] string errorMessage);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnAddSharedFolderComplete(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string fullPath,
            [In] uint succeeded,
            [MarshalAs(UnmanagedType.BStr)] [In] string errorMessage);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnRemoveSharedFolderComplete(
            [In] uint serverId,
            [MarshalAs(UnmanagedType.BStr)] [In] string fullPath,
            [In] uint succeeded,
            [MarshalAs(UnmanagedType.BStr)] [In] string errorMessage);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnFolderCanBeShared([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] uint canShare);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnCDRForcedByAgent([In] uint serverId, [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken,
            [In] uint forcedByAgent);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        new void OnItemLaunchSucceeded2(
            [In] uint serverId,
            [In] VmwHorizonLaunchItemType type,
            [MarshalAs(UnmanagedType.BStr)] [In] string launchItemId,
            [MarshalAs(UnmanagedType.BStr)] [In] string sessionToken);

        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
        void OnReceivedLaunchItems2([In] uint serverId,
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_UNKNOWN)] [In] Array launchItems);
    }
}