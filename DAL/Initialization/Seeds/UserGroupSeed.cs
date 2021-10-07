using System.Collections.Generic;
using Common.Enums;
using Models.Entities;

namespace DAL.Initialization.Seeds
{
    public static class UserGroupSeed
    {
        public static List<UserGroup> Get => new()
        {
            new UserGroup
            {
                Code = GroupCodesEnum.Admin,
                Description = "Admin"
            },
            new UserGroup
            {
                Code = GroupCodesEnum.User,
                Description = "User"
            }
        };
    }
}