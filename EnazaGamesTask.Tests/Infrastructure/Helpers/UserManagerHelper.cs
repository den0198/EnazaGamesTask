using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Models.Entities;
using Moq;

namespace EnazaGamesTask.Tests.Infrastructure.Helpers
{
    public static class UserManagerHelper
    {
        private const string TEST_REFRESH_TOKEN = "Test token";
        
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
                        x.FindByIdAsync(item.Id.ToString()))
                    .ReturnsAsync(item);

                mock.Setup(x =>
                        x.FindByNameAsync(item.Login))
                    .ReturnsAsync(item);

                mock.Setup(x =>
                        x.CheckPasswordAsync(item, item.Password))
                    .ReturnsAsync(true);

                mock.Setup(x =>
                        x.CheckPasswordAsync(item, It.IsNotIn(item.Password)))
                    .ReturnsAsync(false);

                mock.Setup(x =>
                        x.GetClaimsAsync(item))
                    .ReturnsAsync(new List<Claim>
                        { new Claim(ClaimTypes.Email, item.Login) });

                mock.Setup(x =>
                        x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                    .Returns<User, string>(async (user, password) =>
                        item.UserName == user.UserName ? IdentityResult.Failed() : IdentityResult.Success
                    );
            });

            mock.Setup(x =>
                    x.GenerateUserTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(TEST_REFRESH_TOKEN);

            mock.Setup(x =>
                    x.VerifyUserTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(),
                        TEST_REFRESH_TOKEN))
                .ReturnsAsync(true);
            
            mock.Setup(x =>
                    x.VerifyUserTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(),
                        It.IsNotIn(TEST_REFRESH_TOKEN)))
                .ReturnsAsync(false);

            mock.Setup(x =>
                    x.RemoveAuthenticationTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            
            mock.Setup(x =>
                    x.SetAuthenticationTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            
            return mock;
        }
    }
}