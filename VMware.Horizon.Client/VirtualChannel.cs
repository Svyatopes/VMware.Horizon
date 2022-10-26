namespace VMware.Horizon.Client;

public class VirtualChannel
{
    public uint ServerId { get; set; }

    public string SessionToken { get; set; }

    public uint ChannelHandle { get; set; }

    public bool IsConnected { get; set; }
}