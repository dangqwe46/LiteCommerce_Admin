using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DomainModels
{
    public class UserAccount
    {
        /// <summary>
        /// ID cua User
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// Ten day du
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Duong dan anh
        /// </summary>
        public string Photo { get; set; }
        public string GroupName { get; set; }
        public string Password { get; set; }
    }
}
