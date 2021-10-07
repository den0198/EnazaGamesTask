using System.Collections.Generic;
using Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace Models.Entities
{
    public class UserGroup : IdentityRole<int> 
    {
        public GroupCodesEnum Code { get; set; }
        public string Description { get; set; }
        public virtual List<User> Users { get; set; }
    }
}