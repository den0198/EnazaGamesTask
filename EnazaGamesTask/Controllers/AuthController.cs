using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Requests;
using Models.DTOs.Responses;

namespace EnazaGamesTask.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost]
        [Route("SignIn")]
        public async Task<TokenResponse> SignIn(SignInRequest request) => 
            await _authService.SignIn(request);
    }
}