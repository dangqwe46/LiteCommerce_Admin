using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class EmployeePaginatonResult : PaginationResult
    {
        public List<Employee> Data;
        public Employee DataEmployee;
    }
}