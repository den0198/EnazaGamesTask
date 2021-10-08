using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(typeof(BaseResponse<TokenResponse>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status500InternalServerError)]
        public async Task<TokenResponse> SignIn(SignInRequest request) => 
            await _authService.SignIn(request);
        
        [HttpPost]
        [Route("RefreshToken")]
        [ProducesResponseType(typeof(BaseResponse<TokenResponse>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest), StatusCodes.Status500InternalServerError)]
        public async Task<TokenResponse> RefreshToken(RefreshTokenRequest request) => 
            await _authService.RefreshToken(request);
    }
}