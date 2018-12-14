using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsUser
    {
        #region Busness Objects
        public string DataUniqueId { get; set; }
        public string TrxUserId { get; set; }
		public string UserPassword { get; set; }
		public string DisplayName { get; set; }
		public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public bool Locked { get; set; }
        public bool IsFirstTime { get; set; }
        public Int64 UserRoleId { get; set; }        
        public string UserId { get; set; }
        public string IpAdd { get; set; }
        #endregion
    }
}
