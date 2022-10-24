namespace VMware.Horizon.PipeMessages
{
    public class ChannelCommand
    {
        public ChannelCommand(CommandType commandType, object parameters)
        {
            CommandType = commandType;
            CommandParameters = parameters;
        }

        public CommandType CommandType { get; set; }
        public object CommandParameters { get; set; }
    }
}