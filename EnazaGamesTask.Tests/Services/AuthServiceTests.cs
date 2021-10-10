using System;
using BLL.Services;
using EnazaGamesTask.Tests.Infrastructure.Helpers;
using EnazaGamesTask.Tests.Infrastructure.Moсks;
using Models.DTOs.Requests;
using Xunit;

namespace EnazaGamesTask.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly DbContextHelper _db;

        public AuthServiceTests()
        {
            _db = new DbContextHelper();
        }

        #region SignIn

        [Fact]
        public async void SignInItShouldGetTokenIfDataCorrect()
        {
            //arrange
            var authOptions = AuthOptionHelper.GetMock().Object;
            var sut = new AuthService(_db.UserManager,authOptions); 

            //act
            var user = UserMock.GetOne();
            var actual = await sut.SignIn(new AddUserRequest
            {
                Login = user!.Login,
                Password = user!.Password
            });

            //assert
            Assert.NotNull(actual);
        }
        
        [Fact]
        public async void SignInItShouldGetExceptionIfLoginIncorrect()
        {
            //arrange
            var authOptions = AuthOptionHelper.GetMock().Object;
            var sut = new AuthService(_db.UserManager,authOptions); 

            //act
            var user = UserMock.GetOne();

            //assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await sut.SignIn(new AddUserRequest
                {
                    Login = "LoginIncorrect",
                    Password = user!.Password
                });
            });
        }

        [Fact]
        public async void SignInItShouldGetExceptionIfPasswordIncorrect()
        {
            //arrange
            var authOptions = AuthOptionHelper.GetMock().Object;
            var sut = new AuthService(_db.UserManager,authOptions); 

            //act
            var user = UserMock.GetOne();

            //assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await sut.SignIn(new AddUserRequest
                {
                    Login = user!.Login,
                    Password = "IncorrectPassword"
                });
            });
        }

        #endregion
        
        #region RefreshToken
        
        [Fact]
        public async void RefreshTokenItShouldGetTokenIfDataCorrect()
        {
            //arrange
            var authOptions = AuthOptionHelper.GetMock().Object;
            var sut = new AuthService(_db.UserManager,authOptions); 

            //act
            var user = UserMock.GetOne();
            var token = await sut.SignIn(new AddUserRequest
            {
                Login = user!.Login,
                Password = user!.Password
            });
            
            var actual = await sut.RefreshToken(new RefreshTokenRequest
            {
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken
            });

            //assert
            Assert.NotNull(actual);
        }
        
        [Fact]
        public async void RefreshTokenItShouldExceptionIfAccessTokenIncorrect()
        {
            //arrange
            var authOptions = AuthOptionHelper.GetMock().Object;
            var sut = new AuthService(_db.UserManager,authOptions); 

            //act
            var user = UserMock.GetOne();
            var token = await sut.SignIn(new AddUserRequest
            {
                Login = user!.Login,
                Password = user!.Password
            });
            
            //assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await sut.RefreshToken(new RefreshTokenRequest
                {
                    AccessToken = "AccessTokenIncorrect",
                    RefreshToken = token.RefreshToken
                });
            });
        }
        
        [Fact]
        public async void RefreshTokenItShouldExceptionIfRefreshTokenIncorrect()
        {
            //arrange
            var authOptions = AuthOptionHelper.GetMock().Object;
            var sut = new AuthService(_db.UserManager,authOptions); 

            //act
            var user = UserMock.GetOne();
            var token = await sut.SignIn(new AddUserRequest
            {
                Login = user!.Login,
                Password = user!.Password
            });
            
            //assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await sut.RefreshToken(new RefreshTokenRequest
                {
                    AccessToken = token.AccessToken,
                    RefreshToken = "RefreshTokenIncorrect"
                });
            });
        }
        
        #endregion
    }
}