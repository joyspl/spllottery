using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsDealerDeposit
    {
        #region Busness Objects
        public string DataUniqueId { get; set; }
        public Int64 LotteryId { get; set; }
        public string ReqCode { get; set; }
        public DateTime DepositDate { get; set; }
        public double DepositAmount { get; set; }
        public string DepositId { get; set; }
        public Int64 DepositMethodId { get; set; }
        public string BankName { get; set; }
        public int BGValidityDay { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        public int DepositToId { get; set; }
        public string IpAdd { get; set; }
        public int SaveStatus { get; set; }
        public string ReconRemarks { get; set; }        
        public DateTime ReconDate { get; set; }

        #endregion
    }
}
