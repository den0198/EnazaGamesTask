using System.Collections.Generic;
using Common.Enums;
using Models.Entities;

namespace EnazaGamesTask.Tests.Infrastructure.Moсks
{
    public class UserGroupMock
    {
        public static IEnumerable<UserGroup> GetMany()
        {
            yield return GetOne();
            yield return GetOne(2, GroupCodesEnum.User);
        }

        private static UserGroup GetOne
            (int id = 1, GroupCodesEnum code = GroupCodesEnum.Admin) =>
            new UserGroup
            {
                Id = id,
                Name = "TestUserGroup",
                Code = code,
                Description = "TestUserGroup"
            };
    }
}