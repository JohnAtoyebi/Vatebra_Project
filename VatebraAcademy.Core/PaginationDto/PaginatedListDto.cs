using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VatebraAcademy.Core.PaginationDto
{
    public class PaginatedListDto<T>
    {
        public PageMetaData MetaData { get; set; }
        public IEnumerable<T> Data { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public PaginatedListDto()
        {
            Data = new List<T>();
        }
    }
}
