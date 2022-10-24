using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    [Guid("7D94C92E-5EF1-49FE-A9EA-48688A756B37")]
    [TypeLibType(256)]
    [InterfaceType(1)]
    [ComImport]
    public interface IVMwareHorizonClientLaunchItemInfo2 : IVMwareHorizonClientLaunchItemInfo
    {
        [DispId(1)]
        new string name
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
        }

        [DispId(2)]
        new string id
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
        }

        [DispId(3)]
        new VmwHorizonLaunchItemType type
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(4)]
        new VmwHorizonClientProtocol supportedProtocols
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(5)]
        new VmwHorizonClientProtocol defaultProtocol
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(6)]
        uint hasRemotableAssets
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }
    }
}