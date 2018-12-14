using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsRequisition
    { 
        #region Busness Objects
        public string DataUniqueId { get; set; }
        public string ReqCode { get; set; }
        public DateTime ReqDate { get; set; }
        public Int64 LotteryId { get; set; }
        public Int64 GovermentOrderId { get; set; }        
        public DateTime DrawDate { get; set; }
        public int DrawNo { get; set; }
        public Int64 Qty { get; set; }
        public string UserId { get; set; }
        public int ClaimType { get; set; }
        public string IpAdd { get; set; }
        public int SaveStatus { get; set; }
        public int UploadStatus { get; set; }
        public DateTime PressDeliveryDate { get; set; }
        public long SlabLimit { get; set; }
       #endregion
    }
}
