using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{
    public class SelectListHelper
    {
        public static List<SelectListItem> ListRoles()
        {
            List<SelectListItem> listRoles = new List<SelectListItem>();
            listRoles.Add(new SelectListItem() { Value = "dataManagementStaff", Text = "Data Management Staff" });
            listRoles.Add(new SelectListItem() { Value = "SaleMan", Text = "SaleMan" });
            listRoles.Add(new SelectListItem() { Value = "Administrator", Text = "Administrator" });
            listRoles.Add(new SelectListItem() { Value = "dataManagementStaff,Administrator", Text = "Data Management Staff, Administrator" });
            listRoles.Add(new SelectListItem() { Value = "SaleMan,dataManagementStaff,Administrator", Text = "Data Management Staff, Administrator, SaleMan" });
            return listRoles;
        }

        public static List<SelectListItem> ListOfCountries(bool allowSelectAll = true)
        {
            //int CountryID;
            string CountryName;
            // string countryCode;

            List<Country> data = CountryBLL.GetAllCountry();
            List<SelectListItem> ListCountry = new List<SelectListItem>();
            if (allowSelectAll)
            {
                ListCountry.Add(new SelectListItem() { Value = "", Text = "-- All Countries --" });
            }
            foreach (var item in data)
            {
                CountryName = Convert.ToString(item.CountryName);
                // countryCode = Convert.ToString(item.CountryCode);
                ListCountry.Add(new SelectListItem() { Value = CountryName, Text = CountryName });
            }

            return ListCountry;
        }

        public static List<SelectListItem> Category()
        {
            string categoryID;
            string categoryName;
            List<Category> data = CatalogBLL.List_CategoryName_And_CategoryID();
            List<SelectListItem> ListCategory = new List<SelectListItem>();
            ListCategory.Add(new SelectListItem() { Value = "", Text = "---Choose Category---" });
            foreach (var item in data)
            {
                categoryID = Convert.ToString(item.CategoryID);
                categoryName = Convert.ToString(item.CategoryName);
                ListCategory.Add(new SelectListItem() { Value = categoryID, Text = categoryName });
            }

            return ListCategory;
        }

        public static List<SelectListItem> Supplier()
        {
            string supplierID;
            string companyName;
            List<Supplier> data = CatalogBLL.List_CompanyName_And_SupplierID();
            List<SelectListItem> ListSupplier = new List<SelectListItem>();
            ListSupplier.Add(new SelectListItem() { Value = "", Text = "---Choose Supplier---" });
            foreach (var item in data)
            {
                supplierID = Convert.ToString(item.SupplierID);
                companyName = Convert.ToString(item.ContactName);
                ListSupplier.Add(new SelectListItem() { Value = supplierID, Text = companyName });
            }

            return ListSupplier;
        }
        public static List<SelectListItem> ListCustomers()
        {
            string customerID;
            string contactName;
            List<Customer> data = SaleManagementBLL.Order_List_CustomerName_And_CustomerID();
            List<SelectListItem> listCustomers = new List<SelectListItem>();
            listCustomers.Add(new SelectListItem() { Value = "", Text = "--- Choose Customer ---" });
            foreach (var item in data)
            {
                customerID = Convert.ToString(item.CustomerID);
                contactName = Convert.ToString(item.ContactName);
                listCustomers.Add(new SelectListItem() { Value = customerID, Text = contactName });
            }

            return listCustomers;
        }

        public static List<SelectListItem> ListShippers()
        {
            string shipperID;
            string companyName;
            List<Shipper> data = CatalogBLL.Shipper_List_Shipper_And_ShipperID();
            List<SelectListItem> listShippers = new List<SelectListItem>();
            listShippers.Add(new SelectListItem() { Value = "", Text = "--- Choose shippers ---" });
            foreach (var item in data)
            {
                shipperID = Convert.ToString(item.ShipperID);
                companyName = Convert.ToString(item.CompanyName);
                listShippers.Add(new SelectListItem() { Value = shipperID, Text = companyName });
            }

            return listShippers;
        }
        public static List<SelectListItem> ListEmployees()
        {
            string employeeID;
            string firstName;
            string lastName;
            List<Employee> data = HumanResourceBLL.Employee_List_FullName_And_EmployeeID();
            List<SelectListItem> listEmloyees = new List<SelectListItem>();
            listEmloyees.Add(new SelectListItem() { Value = "", Text = "--- Choose Employees ---" });
            foreach (var item in data)
            {
                employeeID = Convert.ToString(item.EmployeeID);
                firstName = Convert.ToString(item.FirstName);
                lastName = Convert.ToString(item.LastName);
                listEmloyees.Add(new SelectListItem() { Value = employeeID, Text = firstName + lastName });
            }

            return listEmloyees;
        }
    }
    
}