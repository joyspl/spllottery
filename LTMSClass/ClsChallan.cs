using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsChallan
    {
        #region Busness Objects
        public Int32 DataUniqueId { get; set; }
        public Int64 RequisitionId { get; set; }
        public string TransporterName { get; set; }
        public string ConsignmentNo { get; set; }
        public string CustomerOrderNo { get; set; }
        public DateTime CustomerOrdeDate { get; set; }
        public string VehicleNo { get; set; }
        public string Subject { get; set; }
        public string SACCode { get; set; }
        public string No { get; set; }
        public Int32 DeliveredQuantity { get; set; }
        public Int32 NoOfBoxBundle { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        public string IpAdd { get; set; }
        public Int16 SaveStatus { get; set; }
        public Int16 ChallanStatus { get; set; }
        #endregion
    }
}
