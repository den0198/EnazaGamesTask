using Microsoft.AspNetCore.Mvc;

namespace EnazaGamesTask.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        [Route("SignIn")]
        public string SignIn() => "test";
    }
}