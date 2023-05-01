﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VatebraAcademy.Core.PaginationDto
{
    public class PageMetaData
    {
        public int Page { get; set; } 
        public int PerPage { get; set; } 
        public int Total { get; set; }
        public int TotalPages { get; set; }
    }
}
