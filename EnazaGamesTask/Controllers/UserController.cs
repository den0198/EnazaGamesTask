using Common.Enums;
using EnazaGamesTask.Attributes;
using Microsoft.AspNetCore.Mvc;


namespace EnazaGamesTask.Controllers
{
    [ApiController]
    [Route("api/user")]
    [AuthorizeUserGroup(GroupCodesEnum.Admin)]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("GetAllUsers")]
        
        public string GetAllUsers() => "test";
        
    }
}