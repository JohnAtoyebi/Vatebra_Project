using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VatebraAcademy.Core.Enums;

namespace VatebraAcademy.Core
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? OtherNames { get; set; }
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
        public string Age { get; set; }
        public int Gender { get; set; } = (int)Enums.Gender.Male;
        public string Address { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
