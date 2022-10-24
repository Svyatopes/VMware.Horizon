using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    [Guid("14F01CAB-3833-4E7C-BD99-61E38498EFA2")]
    [TypeLibType(256)]
    [InterfaceType(1)]
    [ComImport]
    public interface IVMwareHorizonClientChannelDef
    {
        [DispId(1)]
        string name
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            [return: MarshalAs(UnmanagedType.BStr)]
            get;
        }

        [DispId(2)]
        uint options
        {
            [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
            get;
        }
    }
}