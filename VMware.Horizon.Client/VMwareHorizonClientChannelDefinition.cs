using VMware.Horizon.Interop;

namespace VMware.Horizon.Client;

public class VMwareHorizonClientChannelDefinition : IVMwareHorizonClientChannelDef
{
    public VMwareHorizonClientChannelDefinition(string name, uint options)
    {
        this.name = name;
        this.options = options;
    }

    public string name { get; }

    public uint options { get; }
}