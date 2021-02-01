using Contract.DTO;


namespace Contract.Bot.Interface
{
    public interface IMessageBot : IBot
    {
        string OnMessage(MessageGetDTO messageGetDTO);
    }
}
