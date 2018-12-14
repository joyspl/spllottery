using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
    public class ClsLottery
    {
        #region Busness Objects
        public string DataUniqueId { get; set; }
        public string LotteryName { get; set; }
        public int LotteryTypeId { get; set; }
        public int NoOfDigit { get; set; }
        public double SyndicateRate { get; set; }
        public double RateForSpl { get; set; }
        public double TotTicketRate { get; set; }       
        public double GstRate { get; set; }
        public int PrizeCategory { get; set; }
        public bool IncludeConsPrize { get; set; }
        public int ClaimDays { get; set; }
        public int ClaimDaysVariable { get; set; }
        public string SizeOfTicket { get; set; }
        public string PaperQuality { get; set; }        
        public string UserId { get; set; }
        public string IpAdd { get; set; }
        #endregion
    }
}
