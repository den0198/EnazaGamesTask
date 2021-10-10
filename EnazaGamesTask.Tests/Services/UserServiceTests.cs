using System;
using System.Threading.Tasks;
using BLL.Services;
using EnazaGamesTask.Tests.Infrastructure.Helpers;
using EnazaGamesTask.Tests.Infrastructure.Moсks;
using Models.DTOs.Requests;
using Xunit;

namespace EnazaGamesTask.Tests.Services
{
    public class UserServiceTests 
    {
        private readonly DbContextHelper _db; 

        public UserServiceTests()
        {
            _db = new DbContextHelper();
        }

        #region GetAllUsers

        [Fact]
        public async void GetAllUsersItShouldListUser()
        {
            //arrange
            await using var context = _db.Create();
            var sut = new UserService(_db.UserManager, _db.RoleManager, context);

            //act
            var actual = await sut.GetAllUsers();

            //assert
            Assert.NotNull(actual);
        }
        
        #endregion

        #region GetUserById

        [Fact]
        public async void GetUserByIdItShouldGetExceptionIfIdIncorrect()
        {
            //arrange
            await using var context = _db.Create();
            var sut = new UserService(_db.UserManager, _db.RoleManager, context);

            //act

            //assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await sut.GetUserById(1233412);
            });
        }
        
        [Fact]
        public async void GetUserByIdItShouldGetUserIfIdCorrect()
        {
            //arrange
            await using var context = _db.Create();
            var sut = new UserService(_db.UserManager, _db.RoleManager, context);

            //act
            var user = UserMock.GetOne();
            var actual = await sut.GetUserById(user.Id);

            //assert
            Assert.NotNull(actual);
        }

        #endregion

        #region AddUser

        [Fact]
        public async void AddUserItShouldAddUserIfDataCorrect()
        {
            await Task.Delay(5000);
            //arrange
            await using var context = _db.Create();
            var sut = new UserService(_db.UserManager, _db.RoleManager, context);

            //act
            var actual = true;
            try
            {
                await sut.AddUser(new AddUserRequest
                {
                    Login = "TestUser12312423",
                    Password = "qwe123QWE!@#"
                });
            }
            catch
            {
                actual = false;
            }
            
            //assert
            Assert.True(actual);
        }
        
        [Fact]
        public async void AddUserItShouldExceptionIfUserHave()
        {
            await Task.Delay(5000);
            //arrange
            await using var context = _db.Create();
            var sut = new UserService(_db.UserManager, _db.RoleManager, context);

            //act
            var user = UserMock.GetOne();
            
            //assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await sut.AddUser(new AddUserRequest
                {
                    Login = user.Login,
                    Password = user.Password
                });
            });
        }
        
        [Fact]
        public async void AddUserItShouldExceptionIfDoubleQueryStraightaway()
        { 
            //arrange
            
            await using var context = _db.Create();
            var sut = new UserService(_db.UserManager, _db.RoleManager, context);

            //act

            //assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await sut.AddUser(new AddUserRequest
                {
                    Login = "NewUser2131242",
                    Password = "qwe123QWE!@#"
                });
                
                await sut.AddUser(new AddUserRequest
                {
                    Login = "NewUser8695476",
                    Password = "qwe123QWE!@#"
                });
            });
        }

        #endregion

        #region DeleteUserById

        [Fact]
        public async void DeleteUserByIdItShouldGetExceptionIfIdIncorrect()
        {
            //arrange
            await using var context = _db.Create();
            var sut = new UserService(_db.UserManager, _db.RoleManager, context);

            //act

            //assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await sut.DeleteUserById(1233412);
            });
        }
        
        [Fact]
        public async void DeleteUserByIdItShouldDeleteUserIfIdCorrect()
        {
            //arrange
            await using var context = _db.Create();
            var sut = new UserService(_db.UserManager, _db.RoleManager, context);

            //act
            var user = UserMock.GetOne(2);
            var actual = true;
            
            try
            {
                await sut.DeleteUserById(user.Id);
            }
            catch
            {
                actual = false;
            }
            
            //assert
            Assert.True(actual);
        }

        #endregion
    }
}