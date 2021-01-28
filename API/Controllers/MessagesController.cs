using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contract.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MessageAddDTO messageAddDTO)
        {
            ServiceResponse<MessageGetDTO> serviceResponse = await _messageService.CreateMessageAsync(messageAddDTO);
            if (serviceResponse.Success == false)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse);
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> Delete(int id, MessageGetDTO messageGetDTO)
        {
            ServiceResponse<bool> serviceResponse = await _messageService.DeleteMessageAsync(messageGetDTO);
            if (serviceResponse.Success == false)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok();
        }

        [HttpGet]
        [Route("~/api/dialogs/{id:int}/messages")]
        public async Task<IActionResult> Get(int id, int limit = 50, int offset = 0)
        {
            ServiceResponse<MessageGetDTO[]> serviceResponse = await _messageService.GetMessagesByDialogId(id, limit, offset);
            if (serviceResponse.Success == false)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse);
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResponse<MessageGetDTO> serviceResponse = await _messageService.GetMessageAsync(id);
            if (serviceResponse.Success == false)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse);
        }
    }
}
