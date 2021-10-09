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
        public static Mock<UserManager<TUser>> GetMock<TUser>(List<TUser> ls) where TUser : User
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x =>
                    x.FindByNameAsync(It.IsIn("TestUser")))
                .ReturnsAsync(ls.FirstOrDefault());

            mgr.Setup(x =>
                    x.CheckPasswordAsync(It.IsAny<TUser>(),It.IsIn("qwe123QWE!@#")))
                .ReturnsAsync(true);
            
            mgr.Setup(x =>
                    x.GetClaimsAsync(It.IsAny<TUser>()))
                .ReturnsAsync(new List<Claim> { new Claim(ClaimTypes.Email, ls.FirstOrDefault()?.Login)});

            mgr.Setup(x =>
                    x.GenerateUserTokenAsync(It.IsAny<TUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("akslhdjkasgbhdjkvbagwyueudajkbsdnkm");

            mgr.Setup(x =>
                    x.RemoveAuthenticationTokenAsync(It.IsAny<TUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => IdentityResult.Success);
            
            mgr.Setup(x =>
                    x.SetAuthenticationTokenAsync(It.IsAny<TUser>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => IdentityResult.Success);

            mgr.Setup(x => 
                x.DeleteAsync(It.IsAny<TUser>()))
                .ReturnsAsync(IdentityResult.Success);
            
            mgr.Setup(x => 
                x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback<TUser, string>((x, y) => ls.Add(x));
            
            mgr.Setup(x => 
                x.UpdateAsync(It.IsAny<TUser>()))
                .ReturnsAsync(IdentityResult.Success);

            return mgr;
        }
    }
}