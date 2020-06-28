namespace LiteCommerce.Admin
{
    /// <summary>
    /// Định nghĩa danh sách các Role của user
    /// </summary>
    public class WebUserRoles
    {
        /// <summary>
        /// Không xác định
        /// </summary>
        public const string ANONYMOUS = "anonymous";

        /// <summary>
        /// Nhân viên
        /// </summary>
        public const string STAFF = "staff";

        /// <summary>
        /// Quản trị hệ thống
        /// </summary>
        public const string ADMINISTRATOR = "Administrator";

        /// <summary>
        /// Nhân viên bán hàng
        /// </summary>
        public const string SALEMAN = "Saleman";

        /// <summary>
        /// Quản lý dữ liệu
        /// </summary>
        public const string ACCOUNTANT = "Accountant";
        public const string DATA_MANAGER_STAFF = "dataManagementStaff";
        public const string DATA_AD = "dataManagementStaff,Administrator";
        public const string FULL = "SaleMan,dataManagementStaff,Administrator";
    }
}