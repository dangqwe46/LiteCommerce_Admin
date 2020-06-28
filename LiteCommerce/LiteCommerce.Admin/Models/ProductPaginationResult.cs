using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class ProductPaginationResult:PaginationResult
    {
        public List<Product> Data;
        public Product DataProducts;
        public List<ProductAttributes> DataProductAttributes;
        public string Category { get; set; }
        public string Supplier { get; set; }
    }
}