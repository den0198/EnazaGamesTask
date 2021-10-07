using Common.Enums;
using Microsoft.AspNetCore.Authorization;

namespace EnazaGamesTask.Attributes
{
    public class AuthorizeUserGroupAttribute : AuthorizeAttribute
    {
        public AuthorizeUserGroupAttribute(GroupCodesEnum allowedRoles)
        {
            Roles = allowedRoles.ToString();
        }
    }
}