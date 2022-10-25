using System.Runtime.InteropServices;

namespace VMware.Horizon.Interop
{
    [Guid("1B3F2D66-8670-4787-BA9E-211C5817E479")]
    [CoClass(typeof(VMwareHorizonClientClass))]
    [ComImport]
    public interface VMwareHorizonClient : IVMwareHorizonClient
    {
    }
}