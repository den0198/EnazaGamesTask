using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Enums;
using Common.TimeLockAddUser;
using DAL.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.DTOs.Requests;
using Models.DTOs.Responses;
using Models.Entities;

namespace BLL.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserGroup> _roleManager;
        private readonly AppDbContext _context;

        public UserService(UserManager<User> userManager, RoleManager<UserGroup> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<List<UserResponse>> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            
            //сдесь надо было испоьзовать Mappseter, но я решил показать как собираю модельку
            var userResponseList = new List<UserResponse>();
            
            users.ForEach(item => userResponseList.Add(
                new UserResponse
                {
                    UserId = item.Id,
                    Login = item.Login,
                    CreatedDate = item.CreatedDate,
                    UserGroupCode = item.UserGroup.Code.ToString(),
                    UserGroupDescription = item.UserGroup.Description,
                    UserStateCode = item.UserState.Code.ToString(),
                    UserStateDescription = item.UserState.Description
                }));

            return userResponseList;
        }

        public async Task<UserResponse> GetUserById(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                throw new Exception("User with this ID was not found");

            //сдесь надо было испоьзовать Mappseter, но я решил показать как собираю модельку
            var userResponse = new UserResponse
            {
                UserId = user.Id,
                Login = user.Login,
                CreatedDate = user.CreatedDate,
                UserGroupCode = user.UserGroup.Code.ToString(),
                UserGroupDescription = user.UserGroup.Description,
                UserStateCode = user.UserState.Code.ToString(),
                UserStateDescription = user.UserState.Description
            };

            return userResponse;
        }

        public async Task AddUser(AddUserRequest request)
        {
            if (AddUserLock.Locker)
                throw new Exception("Timeout add user, request later");

            AddUserLock.Locker = true;
            
            var userGroup = await _roleManager.FindByNameAsync(GroupCodesEnum.User.ToString());
            var userState = await _context.UserStates
                .FirstOrDefaultAsync(item => item.Code == StateCodesEnum.Active);

            var newUser = new User
            {
                UserName = request.Login,
                Login = request.Login,
                Password = request.Password,
                CreatedDate = DateTime.Now,
                UserGroup = userGroup,
                UserState = userState
            };
            
            var resultCreateUser = await _userManager.CreateAsync(newUser, newUser.Password);
            
            if (!resultCreateUser.Succeeded)
            {
                var error = "";
                
                resultCreateUser.Errors.ToList().ForEach(item => 
                    error += item.Description + "\n");
                
                throw new Exception(error.Trim('\n'));
            }
        }

        public async Task DeleteUserById(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                throw new Exception("User with this ID was not found");

            if (user.UserGroup.Code == GroupCodesEnum.Admin)
                throw new Exception("You cannot remove the System Admin");

            user.UserState = await _context.UserStates
                .FirstOrDefaultAsync(item => item.Code == StateCodesEnum.Blocked);
            
            user.LockoutEnabled = true;
            
            await _context.SaveChangesAsync();
        }
    }
}