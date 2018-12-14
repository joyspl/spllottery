using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsLotteryClaimApprovalDetails
    {
        public Int64 DataUniqueId { get; set; }
        public string Remarks { get; set; }
        public double PayableAmount { get; set; }   
        public string UserId { get; set; }
        public string IpAdd { get; set; }
        public int SaveStatus { get; set; }
       
    }
}
