using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsTicketInventory
    {
        #region Busness Objects
        public Int64 ID { get; set; }
        public Int64 DataUniqueId { get; set; }
        public Int64 LotteryId { get; set; }
        public DateTime ReqDate { get; set; }
        public string StrReqDate { get; set; }
        public DateTime DrawDate { get; set; }
        public int DrawNo { get; set; }
        public int FnStart { get; set; }
        public int FnEnd { get; set; }
        public string AlphabetSeries { get; set; }
        public Int64 TnStart { get; set; }
        public Int64 TnEnd { get; set; }
        public int SaveStatus { get; set; }       
        public string UserId { get; set; }
        public string IpAdd { get; set; }
        public string ReqCode { get; set; }
        #endregion
    }
}
