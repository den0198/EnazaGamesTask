using System;
using Microsoft.AspNetCore.Identity;

namespace Models.Entities
{
    public class User : IdentityUser<int> 
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserStateId { get; set; }
        public int UserGroupId { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}