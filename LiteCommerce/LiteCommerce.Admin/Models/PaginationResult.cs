using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    /// <summary>
    /// Chứa dữ liệu trả về dưới dạng phân trang
    /// </summary>
    public class PaginationResult
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public int PageCount
        {
            get
            {
                int pageCount=1;
                pageCount = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                {
                    pageCount += 1;
                }
                return pageCount;

            }
        }
        public string SearchValue { get; set; }
        public int categoryName { get; set; }
        public int companyName { get; set; }
    }
}