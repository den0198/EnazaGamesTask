using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Moq;

namespace EnazaGamesTask.Tests.Infrastructure.Helpers
{
    public static class UserManagerHelper
    {
        public static Mock<UserManager<User>> GetMock(List<User> users)
        {
            var store = new Mock<IUserStore<User>>();
            
            var mock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);

            mock.Setup(x =>
                    x.Users)
                .Returns(users.AsQueryable());
            
            users.ForEach(item =>
            {
                mock.Setup(x =>
                        x.FindByNameAsync(item.Login))
                    .ReturnsAsync(users.FirstOrDefault());

                mock.Setup(x =>
                        x.CheckPasswordAsync(It.IsAny<User>(),item.Password))
                    .ReturnsAsync(true);
            
                mock.Setup(x =>
                        x.GetClaimsAsync(It.IsAny<User>()))
                    .ReturnsAsync(new List<Claim> { new Claim(ClaimTypes.Email, item.Login)});
                
            });

            
            mock.Setup(x =>
                    x.GenerateUserTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("Test token");

            mock.Setup(x =>
                    x.RemoveAuthenticationTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => IdentityResult.Success);
            
            mock.Setup(x =>
                    x.SetAuthenticationTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => IdentityResult.Success);
            
            return mock;
        }
    }
}