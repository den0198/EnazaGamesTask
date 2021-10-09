using System.Collections.Generic;
using Common.Enums;
using Models.Entities;

namespace EnazaGamesTask.Tests.Infrastructure.Moсks
{
    public static class UserStateMock
    {
        public static IEnumerable<UserState> GetMany()
        {
            yield return GetOne();
            yield return GetOne(2, StateCodesEnum.Blocked);
        }

        private static UserState GetOne
            (int id = 1, StateCodesEnum code = StateCodesEnum.Active) =>
            new UserState
            {
                Id = id,
                Code = code,
                Description = "TestUserState"
            };
    }
}