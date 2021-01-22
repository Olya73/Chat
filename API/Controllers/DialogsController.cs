using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Implementation;
using Service.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogsController : ControllerBase
    {
        private readonly IDialogService _dialogService;

        public DialogsController(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DialogAddDTO dialogAddDTO)
        {
            ServiceResponse<DialogGetDTO> serviceResponse = await _dialogService.CreateDialogAsync(dialogAddDTO);
            if (serviceResponse.Success == false)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<bool> serviceResponse = await _dialogService.DeleteDialogAsync(id);
            if (serviceResponse.Success == false)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok();
        }

        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> Post(UserDialogAddDTO userDialogAddDTO)
        {
            ServiceResponse<int> serviceResponse = await _dialogService.AddUserToDialogAsync(userDialogAddDTO);
            if (serviceResponse.Success == false)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse);
        }

        [HttpDelete]
        [Route("{dialogId:int}/users/{userId:int}")]
        public async Task<IActionResult> Delete([FromRoute] UserDialogAddDTO userDialogAddDTO)
        {
            ServiceResponse<bool> serviceResponse = await _dialogService.DeleteUserFromDialogAsync(userDialogAddDTO);
            if (serviceResponse.Success == false)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse);
        }

        [HttpGet]
        [Route("{id:int}/users")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResponse<UserGetDTO[]> serviceResponse = await _dialogService.GetUsersByDialogId(id);
            if (serviceResponse.Success == false)
            {
                return BadRequest(serviceResponse.Message);
            }
            return Ok(serviceResponse);
        }
    }
}
