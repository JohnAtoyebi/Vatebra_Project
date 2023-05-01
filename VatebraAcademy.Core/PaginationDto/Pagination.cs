using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VatebraAcademy.Core.PaginationDto
{
    public static class Pagination
    {
        public class Pagnation<T>
        {
            public static PageMetaData CreatePageMetaData(int page, int perPage, int total)
            {
                var total_pages = total % perPage == 0 ? total / perPage : total / perPage + 1;

                return new PageMetaData
                {
                    Page = page,
                    PerPage = perPage,
                    Total = total,
                    TotalPages = total_pages
                };
            }

            public static PaginatedListDto<T> Paginate(List<T> source, int page, int perPage)
            {
                page = page < 1 ? 1 : page;

                var paginatedList = source.Skip((page - 1) * perPage).Take(perPage);

                var pageMeta = CreatePageMetaData(page, perPage, source.Count);

                return new PaginatedListDto<T>
                {
                    MetaData = pageMeta,
                    Data = paginatedList
                };
            }
        }
    }
}
