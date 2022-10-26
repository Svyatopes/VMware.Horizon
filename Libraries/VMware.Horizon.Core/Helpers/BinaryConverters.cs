using System.Text;

namespace VMware.Horizon.VirtualChannel.RDPVCBridgeInterop;

public class BinaryConverters
{
    public static byte[] StringToBinary(string data) =>
        Encoding.ASCII.GetBytes(data);

    public static string BinaryToString(byte[] data) =>
        Encoding.ASCII.GetString(data);
}