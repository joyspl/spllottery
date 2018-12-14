using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsGovermentOrder
    {
        #region Busness Objects
        public string DataUniqueId { get; set; }
        public string GovermentOrder { get; set; }
        public double UnSoldPercentage { get; set; }
        public string AlphabetName { get; set; }
        public Int64 NoOfAlphabet { get; set; }
        public string TicketSeriallNoFrom { get; set; }
        public string TicketSerialNoTo { get; set; }
        public Int64 TotalTickets { get; set; }
        public Int64  LotteryId { get; set; }   
        public string UserId { get; set; }
        public string IpAdd { get; set; }

        public string ModifiedLotteryName { get; set; }
        #endregion
    }
}
