using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    [Guid("3DE45105-131E-48EE-9E46-8ED7265C303D")]
    [TypeLibType(256)]
    [InterfaceType(1)]
    [ComImport]
    public interface IVMwareHorizonClientLaunchItemInfo
    {
        [DispId(1)]
        string name
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
        }

        [DispId(2)]
        string id
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
        }

        [DispId(3)]
        VmwHorizonLaunchItemType type
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(4)]
        VmwHorizonClientProtocol supportedProtocols
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(5)]
        VmwHorizonClientProtocol defaultProtocol
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }
    }
}