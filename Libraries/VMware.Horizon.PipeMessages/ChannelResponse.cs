namespace VMware.Horizon.PipeMessages
{
    public class ChannelResponse
    {
        public ChannelResponse(bool success, string details)
        {
            Successful = success;
            Details = details;
        }

        public ChannelResponse()
        {
            Successful = true;
        }

        public bool Successful { get; set; }
        public string Details { get; set; }
    }
}