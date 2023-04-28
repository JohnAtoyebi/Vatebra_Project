using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VatebraAcademy.Core;

namespace VatebraAcademy.Data
{
    public class VatebraAcademyDbContext : IdentityDbContext<AppUser>
    {
        public VatebraAcademyDbContext(DbContextOptions<VatebraAcademyDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}
