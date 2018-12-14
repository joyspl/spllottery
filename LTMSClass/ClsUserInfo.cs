using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsUserInfo
    {
        public string UserId { get; set; }
        public string UserPassword { get; set; }
        public string DisplayName { get; set; }
        public string EmailId { get; set; }
        public bool IsAdministrator { get; set; }
        public bool AccessAllowed { get; set; }
        public bool IsFirstTime { get; set; }
        public Int64 UserRoleId { get; set; }
        public string MenuList { get; set; }
        
    }
}
