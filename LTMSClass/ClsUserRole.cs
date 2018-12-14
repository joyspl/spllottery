using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsUserRole
    {
        #region Busness Objects
        public string DataUniqueId { get; set; }
        public string UserRole { get; set; }
        public bool Locked { get; set; }
        public string UserId { get; set; }
        public string IpAdd { get; set; }
        #endregion
    }
}
