using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTMSClass
{
   public  class ClsLotteryClaimDetails
    {
        public string DataUniqueId { get; set; }
        public int ClaimType { get; set; }
        public string ReqCode { get; set; }
        public DateTime ReqDate { get; set; }
        public Int64 LotteryTypeId { get; set; }
        public string LotteryType { get; set; }
        public Int64 LotteryId { get; set; }
        public string LotteryName { get; set; }
        public int DrawNo { get; set; }
        public DateTime? DrawDate { get; set; }
        public string LotteryNo { get; set; }
        public string Captcha { get; set; }
        public string EmailId { get; set; }       
        public string MobileNo { get; set; }
        public string UserId { get; set; }
        public string OTP { get; set; }       
        public string Name { get; set; }
        public string SoDoWo { get; set; }
        public string Surety { get; set; }
        public string ProprietorOf { get; set; }
        public string FatherOrGuardianName { get; set; }
        public string Address { get; set; }
        public string AadharNo { get; set; }
        public string PanNo { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public string IFSCCode { get; set; }

        public byte[] PwtTicket { get; set; }
        public byte[] Photo { get; set; }
        public byte[] Pan { get; set; }
        public byte[] Aadhar { get; set; }
        public byte[] BankDtl { get; set; }    
           
        public string IpAdd { get; set; }
        public int SaveStatus { get; set; }
       
    }
}
