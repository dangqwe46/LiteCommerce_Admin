using LiteCommerce.DataLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// 
    /// </summary>
    public static class CatalogBLL
    {
        private static ISupplierDAL SupplierDB { get; set; }
        private static IShipperDAL ShipperDB { get; set; }
        private static ICustomerDAL CustomerDB { get; set; }
        private static ICategoryDAL CategoryDB { get; set; }
        private static IProductDAL ProductDB { get; set; }
        private static IProductAttributeDAL ProductAttributeDB { get; set; }
        /// <summary>
        /// Hàm này phải được gọi để khởi tạo các chức năng tác nghiệp
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            SupplierDB = new DataLayers.SqlServer.SupplierDAL(connectionString);
            ShipperDB = new DataLayers.SqlServer.ShipperDAL(connectionString);
            CustomerDB = new DataLayers.SqlServer.CustomerDAL(connectionString);
            CategoryDB = new DataLayers.SqlServer.CategoryDAL(connectionString);
            ProductDB = new DataLayer.SqlServer.ProductDAL(connectionString);
            ProductAttributeDB = new DataLayer.SqlServer.ProductAttributeDAL(connectionString);

        }
        /// <summary>
        /// Hiển thị danh sách các nhà cung cấp theo dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pageSize">Số dòng mỗi trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm</param>
        /// <returns></returns>
        #region SUPPLIER
        public static List<Supplier> Supplier_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;

            return SupplierDB.List(page, pageSize, searchValue);
        }
        public static int Supplier_Count (string searchValue)
        {
            return SupplierDB.Count(searchValue);
        }
        public static int Supplier_Add(Supplier data)
        {
            return SupplierDB.Add(data);
        }
        public static bool Supplier_Delete(int[] supplierIDs)
        {
            return SupplierDB.Delete(supplierIDs);
        }
        
        
        public static Supplier Supplier_Get(int supplierID)
        {
            return SupplierDB.Get(supplierID);
        }
        public static bool Supplier_Update(Supplier data)
        {
            return SupplierDB.Update(data);
        }
        public static List<Supplier> Suppliers_CompanyName()
        {
            return SupplierDB.List();
        }
        public static List<Supplier> List_CompanyName_And_SupplierID()
        {
            return SupplierDB.List_CompanyName_And_SupplierID();
        }
        #endregion
        #region SHIPPER
        public static List<Shipper> Shipper_List(int page, int pageSize, string searchValue)
        {


            return ShipperDB.List(page, pageSize, searchValue);
        }
        public static List<Shipper> Shipper_List_Shipper_And_ShipperID()
        {
            return ShipperDB.List_Shipper_And_ShipperID();
        }

        public static int Shipper_Count(string searchValue)
        {
            return ShipperDB.Count(searchValue);
        }
        public static int Shipper_Add(Shipper data)
        {
            return ShipperDB.Add(data);
        }
        public static bool Shipper_Delete(int[] shipperIDs)
        {
            return ShipperDB.Delete(shipperIDs);
        }
        public static Shipper Shipper_Get(int shipperID)
        {
            return ShipperDB.Get(shipperID);
        }
        public static bool Shipper_Update(Shipper data)
        {
            return ShipperDB.Update(data);
        }
        #endregion
        #region CUSTOMER
        public static List<Customer> Customer_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;

            return CustomerDB.List(page, pageSize, searchValue);
        }
        public static string Customer_Add(Customer data)
        {
            return CustomerDB.Add(data);
        }
        public static bool Customer_Delete(string[] customerIDs)
        {
            return CustomerDB.Delete(customerIDs);
        }
        public static Customer Customer_Get(string customerID)
        {
            return CustomerDB.Get(customerID);
        }
        public static bool Customer_Update(Customer data)
        {
            return CustomerDB.Update(data);
        }

        public static int Customer_Count(string searchValue)
        {
            return CustomerDB.Count(searchValue);
        }
        #endregion
        #region CATEGORY
        public static List<Category> Category_List(int page, int pageSize, string searchValue)
        {


            return CategoryDB.List(page, pageSize, searchValue);
        }

        public static int Category_Count(string searchValue)
        {
            return CategoryDB.Count(searchValue);
        }
        public static int Category_Add(Category data)
        {
            return CategoryDB.Add(data);
        }
        public static bool Category_Delete(int[] categoryIDs)
        {
            return CategoryDB.Delete(categoryIDs);
        }
        public static Category Category_Get(int categoryID)
        {
            return CategoryDB.Get(categoryID);
        }
        public static bool Category_Update(Category data)
        {
            return CategoryDB.Update(data);
        }
        public static List<Category> Category_CategoryName()
        {
            return CategoryDB.List();
        }
        public static List<Category> List_CategoryName_And_CategoryID()
        {
            return CategoryDB.List_CategoryName_And_CategoryID();
        }
        #endregion
        #region PRODUCT
        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Product> Product_List(int page, int pageSize, string searchValue, int Categogy, int Supplier)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;
            return ProductDB.List(page, pageSize, searchValue,Categogy,Supplier);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Product_Count(string searchValue, int categoryName, int companyName)
        {
            return ProductDB.Count(searchValue, categoryName, companyName);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static Product GetProduct(int productId)
        {
            return ProductDB.Get(productId);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Product_Add(Product data)
        {
            return ProductDB.Add(data);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="productIDs"></param>
        /// <returns></returns>
        public static bool Product_Delete(int[] productIDs)
        {
            return ProductDB.Delete(productIDs);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Product_Update(Product data)
        {
            return ProductDB.Update(data);
        }
        #endregion
        #region ProductAttribute
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductAttributes> ProductAttribute_List(int productID)
        {
            return ProductAttributeDB.List(productID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int ProductAttribute_Add(ProductAttributes data)
        {
            return ProductAttributeDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productAttributeId"></param>
        /// <returns></returns>
        public static ProductAttributes GetProductAttributes(int productAttributeId)
        {
            return ProductAttributeDB.Get(productAttributeId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productAttributesIDs"></param>
        /// <returns></returns>
        public static bool ProductAttributes_Delete(int[] productAttributesIDs)
        {
            return ProductAttributeDB.Delete(productAttributesIDs);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool ProductAttributes_Update(ProductAttributes data)
        {
            return ProductAttributeDB.Update(data);
        }
        #endregion ProductAttribute
        public static bool Product_DeleteByProductID(int productID)
        {
            return ProductAttributeDB.DeleteByProductID(productID);
        }

    }

}
