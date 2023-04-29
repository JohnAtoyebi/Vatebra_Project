using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VatebraAcademy.Core.Dtos
{
    public class AppUserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? OtherNames { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; } 
        public string Address { get; set; }
    }
}
