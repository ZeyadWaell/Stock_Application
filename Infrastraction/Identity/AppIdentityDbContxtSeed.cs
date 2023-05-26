using Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Identity
{
    public class AppIdentityDbContxtSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> usermanger)
        {
            if(!usermanger.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "John",
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                    Address = new Address()
                    {
                        FirstName = "Ahmed",
                        LastName = "Mohammed",
                        State = "N/A",
                        City = "N/A",
                        Street = "N/A",
                        PostalCode = "N/A"
                    }
                };
                await usermanger.CreateAsync(user,"Password12313!@#");
            }
        }
    }
}
