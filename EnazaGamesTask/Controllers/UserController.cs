using Common.Enums;
using EnazaGamesTask.Attributes;
using Microsoft.AspNetCore.Mvc;


namespace EnazaGamesTask.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("GetAllUsers")]
        [AuthorizeUserGroup(GroupCodesEnum.Admin)]
        public string GetAllUsers() => "test";
        
    }
}