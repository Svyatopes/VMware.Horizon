using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    [InterfaceType(1)]
    [Guid("22439E11-E726-40A7-8476-43C530CA49C0")]
    [TypeLibType(256)]
    [ComImport]
    public interface IVMwareHorizonClientUSBDeviceInfo
    {
        [DispId(1)]
        long deviceId
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }

        [DispId(2)]
        string deviceName
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
        }

        [DispId(3)]
        uint isAvailable
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }
    }
}