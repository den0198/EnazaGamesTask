using System;
using System.Collections.Generic;
using Common.Enums;
using Models.Entities;

namespace DAL.Initialization.Seeds
{
    public static class UserStateSeed
    {
        public static List<UserState> Get => new()
        {
            new UserState
            {
                Code = StateCodesEnum.Active,
                Description = "Active"
            },
            new UserState
            {
                Code = StateCodesEnum.Blocked,
                Description = "Blocked"
            }
        };
    }
}