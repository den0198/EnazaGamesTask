using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Models.Entities;
using Moq;
using Moq.Protected;

namespace EnazaGamesTask.Tests.Infrastructure.Helpers
{
    public static class UserManagerHelper
    {
        public static Mock<UserManager<User>> GetMock(List<User> ls)
        {
            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<User>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<User>());
            
            ls.ForEach(item =>
            {
                mgr.Setup(x =>
                        x.FindByNameAsync(It.IsIn(item.Login)))
                    .ReturnsAsync(ls.FirstOrDefault());

                mgr.Setup(x =>
                        x.CheckPasswordAsync(It.IsAny<User>(),It.IsIn(item.Password)))
                    .ReturnsAsync(true);
            
                mgr.Setup(x =>
                        x.GetClaimsAsync(It.IsAny<User>()))
                    .ReturnsAsync(new List<Claim> { new Claim(ClaimTypes.Email, item.Login)});
                
                
            });

            
            mgr.Setup(x =>
                    x.GenerateUserTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("akslhdjkasgbhdjkvbagwyueudajkbsdnkm");

            mgr.Setup(x =>
                    x.RemoveAuthenticationTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => IdentityResult.Success);
            
            mgr.Setup(x =>
                    x.SetAuthenticationTokenAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => IdentityResult.Success);
            
            return mgr;
        }
    }
}