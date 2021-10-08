using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Services;
using Common.Enums;
using EnazaGamesTask.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Requests;
using Models.DTOs.Responses;


namespace EnazaGamesTask.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<List<UserResponse>> GetAllUsers() => 
            await _userService.GetAllUsers();

        [HttpGet]
        [Route("GetUserById")]
        public async Task<UserResponse> GetUserById(int id) =>
            await _userService.GetUserById(id);

        [HttpPost]
        [Route("AddUser")]
        [AuthorizeUserGroup(GroupCodesEnum.Admin)]
        [ProducesResponseType(typeof(BaseResponse),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status500InternalServerError)]
        public async Task AddUser(AddUserRequest request) =>
            await _userService.AddUser(request);
        
        [HttpDelete]
        [Route("DeleteUserById")]
        [AuthorizeUserGroup(GroupCodesEnum.Admin)]
        public async Task DeleteUserById(int id) =>
            await _userService.DeleteUserById(id);
    }
}