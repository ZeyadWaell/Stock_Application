using Core.Entites.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastraction.Identity
{
    public class AppDpContext : IdentityDbContext<AppUser>
    {
        public AppDpContext(DbContextOptions<AppDpContext> options) : base(options)
        {
        }
    }
}
