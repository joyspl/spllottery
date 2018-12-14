using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsPrizeWinner
    {
        #region Busness Objects
        public string DataUniqueId { get; set; }
        public Int64 LotteryId { get; set; }
        public string JudgesName1 { get; set; }
        public string JudgesName2 { get; set; }
        public string JudgesName3 { get; set; }
        public string PlayingAddress { get; set; }
        public string DrawTime { get; set; }
        public int SaveStatus { get; set; }
        public DateTime DrawDate { get; set; }      
        public string UserId { get; set; }
        public string IpAdd { get; set; }
        #endregion
    }
}
