using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsPrize
    {
        #region Busness Objects
        public Int64 DataUniqueId { get; set; }
        public Int64 LotteryId { get; set; }
        public Int64 LotteryTypeId { get; set; }
        public int ClaimDays { get; set; }      
        public string UserId { get; set; }
        public string IpAdd { get; set; }
        public int SaveStatus { get; set; }
        #endregion
    }
}
