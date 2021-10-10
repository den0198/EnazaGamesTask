using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Models.Entities;
using Moq;

namespace EnazaGamesTask.Tests.Infrastructure.Helpers
{
    public static class RoleManagerHelper
    {
        public static Mock<RoleManager<UserGroup>> GetMock(List<UserGroup> userGroups)
        {
            var store = new Mock<IRoleStore<UserGroup>>();
            var mock = new Mock<RoleManager<UserGroup>>(store.Object, null, null, null, null);

            userGroups.ForEach(item =>
            {
                mock.Setup(x =>
                        x.FindByNameAsync(item.Name))
                    .ReturnsAsync(item);
            });
            
            return mock;
        }
    }
}