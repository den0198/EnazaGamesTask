using System;
using System.Linq;
using System.Security.Claims;
using Common.Enums;
using DAL.EntityFramework;
using DAL.Initialization.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;

namespace DAL.Initialization
{
    public static class InitData
    {
        public static async void InitialData(IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder
                .ApplicationServices
                .GetService<IServiceScopeFactory>()
                ?.CreateScope();

            var context = scope?.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope?.ServiceProvider.GetRequiredService<RoleManager<UserGroup>>();
            var userManager = scope?.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (context == null)
                throw new Exception("context is null");

            if (roleManager == null)
                throw new Exception("context roles is null");

            if (userManager == null)
                throw new Exception("context users is null");

            
            #region InitialsDataBase 

            #region Roles

            var userGroupSeeds = UserGroupSeed.Get;

            foreach (var identityRole in userGroupSeeds)
            {
                identityRole.Name = identityRole.Code.ToString();
                
                if (await roleManager.FindByNameAsync(identityRole.Name) != null)
                    continue;

                await roleManager.CreateAsync(identityRole);
                await roleManager.AddClaimAsync(identityRole, new Claim(ClaimTypes.Role, identityRole.Name));
            }

            #endregion

            #region State

            var userStateSeed = UserStateSeed.Get;

            foreach (var userState in userStateSeed
                .Where(userState => 
                    context.UserStates.SingleOrDefault(item => item.Code == userState.Code) == null))
            {
                context.Add(userState);
            }
            
            await context.SaveChangesAsync();
            
            #endregion

            #region Accounts

            var userSeed = UserSeed.Get;

            foreach (var user in userSeed)
            {
                user.UserName = user.Login;
                
                if (await userManager.FindByNameAsync(user.Login) != null)
                    continue;

                user.UserState  =
                    await context.UserStates.SingleOrDefaultAsync(item => item.Code == StateCodesEnum.Active);

                user.UserGroup =
                    await roleManager.FindByNameAsync(GroupCodesEnum.Admin.ToString());
                
                await userManager.CreateAsync(user, user.Password);
                await userManager.AddClaimAsync(user,
                    new Claim(ClaimTypes.Email, user.Login));
                
            }

            #endregion

            #endregion

            

        }
    }
}