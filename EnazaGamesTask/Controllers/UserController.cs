using Microsoft.AspNetCore.Mvc;

namespace EnazaGamesTask.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("GetAllUsers")]
        public string GetAllUsers() => "test";
        
    }
}