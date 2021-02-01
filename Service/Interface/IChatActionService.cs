using Contract.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IChatActionService
    {
        Task AddChatEvent(ChatEventDTO chatEventDTO);
        Task NotifyAsync();
    }
}
