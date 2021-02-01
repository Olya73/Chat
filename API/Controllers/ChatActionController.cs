using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.Bot;
using Contract.Bot.Interface;
using Contract.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatActionController : ControllerBase
    {
        private readonly IChatActionService _chatActionService;

        public ChatActionController(IChatActionService chatActionService)
        {
            _chatActionService = chatActionService;
        }

        public async Task<IActionResult> Post(ChatEventDTO chatEventDTO)
        {
            string s = "NewMessage";
            int actionTypes = (int)Enum.Parse(typeof(ActionTypes), s);
            await _chatActionService.AddChatEvent(chatEventDTO);
            Task.Run(() => _chatActionService.NotifyAsync());
            return Ok();
        }
    }
}
