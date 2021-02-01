using Contract.DTO;


namespace Contract.Bot.Interface
{
    public interface IEventBot : IBot
    {
        ActionTypes AllowedActions { get; }
        string OnEvent(MessageGetDTO messageGetDTO, ActionTypes action);
    }
}
