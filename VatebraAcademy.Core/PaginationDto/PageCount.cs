using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace VatebraAcademy.Core.PaginationDto
{
    public class PageCount
    {
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 1;
    }
}
