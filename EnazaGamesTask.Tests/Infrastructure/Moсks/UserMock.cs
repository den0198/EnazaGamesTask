using System;
using System.Collections.Generic;
using Models.Entities;

namespace EnazaGamesTask.Tests.Infrastructure.Moсks
{
    public static class UserMock
    {
        public static IEnumerable<User> GetMany()
        {
            yield return GetOne();
        }

        private static User GetOne
            (int id = 1, int userGroupId = 1, int userStateId = 1) =>
            new User
            {
                Id = id,
                UserName = "TestUser",
                Login = "TestUser",
                Password = "qwe123QWE!@#",
                CreatedDate = DateTime.Now,
                UserGroupId = userGroupId,
                UserStateId = userStateId
            };
    }
}