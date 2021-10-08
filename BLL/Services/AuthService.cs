using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Common.HelpersClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs.Requests;
using Models.DTOs.Responses;
using Models.Entities;
using Models.Options;

namespace BLL.Services
{
    public class AuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthOptions _authOptions;

        public AuthService(UserManager<User> userManager, IOptions<AuthOptions> authOptions)
        {
            _userManager = userManager;
            _authOptions = authOptions.Value;
        }

        public async Task<TokenResponse> SignIn(SignInRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Login);
            
            if (user == null)
                throw new Exception("User not in system");
            
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                throw new Exception("Login or Password is not correct");    
            
            return await getTokenBase(user);
        }

        public async Task<TokenResponse> RefreshToken(RefreshTokenRequest request)
        {
            var userInfo = getAccountInfoByOldToken(request.AccessToken);
            
            if (userInfo == null)
                throw new Exception("Access token is invalid");
            
            var userLogin = userInfo.Claims
                .Where(_ => _.Type.Contains("email"))
                .Select(_ => _.Value)
                .FirstOrDefault();
            
            var user = await _userManager.FindByNameAsync(userLogin);
            
            if (user == null)
                throw new Exception("UserDetails is not system");
            
            var isTokenValid = await _userManager.VerifyUserTokenAsync(user,
                _authOptions.Audience, "RefreshToken", request.RefreshToken);
            
            if (!isTokenValid)
                throw new Exception("Refresh token is invalid");
                    
            return await getTokenBase(user);
        }
        
        private async Task<TokenResponse> getTokenBase(User user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var role = user.UserGroup;
            claims.Add(new Claim(ClaimTypes.Role, role.Code.ToString()));
            

            var accessToken = buildAndGetJwt(claims);
            var refreshToken = await _userManager.GenerateUserTokenAsync(user, _authOptions.Audience,
                "RefreshToken");

            await _userManager.RemoveAuthenticationTokenAsync(user, 
                _authOptions.Audience, "RefreshToken");
            await _userManager.SetAuthenticationTokenAsync(user, 
                _authOptions.Audience, "RefreshToken", refreshToken);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken =  refreshToken
            };
        }
        
        private string buildAndGetJwt(IEnumerable<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                _authOptions.Issuer,
                _authOptions.Audience,
                expires: DateTime.Now.AddMinutes(_authOptions.Lifetime),
                signingCredentials: AuthHelper.GetSigningCredentials(_authOptions.Key),
                claims: claims
            );
            
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsPrincipal getAccountInfoByOldToken(string oldAccessToken)
        {
            var tokenValidationParameters = getTokenValidationParameters();
            var jwtHandler = new JwtSecurityTokenHandler();
            var userInfo = jwtHandler.ValidateToken(oldAccessToken,
                tokenValidationParameters, out var securityToken);

            return securityToken is JwtSecurityToken ? userInfo : null;
        }
        
        private TokenValidationParameters getTokenValidationParameters () =>
            new TokenValidationParameters
            {
                ValidIssuer = _authOptions.Issuer,
                ValidateIssuer = true,
                ValidAudience = _authOptions.Audience,
                ValidateAudience = true,
                IssuerSigningKey = AuthHelper.GetIssuerSigningKey(_authOptions.Key),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false
            };
        
        
    }
}