using System;
using System.Collections.Generic;
using Models.Entities;

namespace DAL.Initialization.Seeds
{
    public static class UserSeed
    {
        public static List<User> Get => new()
        {
            new User
            {
                Login = "Admin",
                Password = "qwe123QWE!@#",
                CreatedDate = DateTime.Now,
            }
        };
    }
}