using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace LTMSWebApp
{
    public partial class TrxLotteryClaimEntryPrint : System.Web.UI.Page
    {
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
           // Session["ApplicationId"] = "LD1819000006";
            if (Session["ApplicationId"] == null)
            {
                Session["CustomError"] = "Invalid Navigation option. Please use the Navigation panel to access this page.";
                Server.Transfer("appError.aspx");
                return;
            }
            if (IsPostBack == false)
            {
                ShowApplicationIdDetails();                
            }
        }      
        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx", false);
        }
        public void ShowApplicationIdDetails()
        {
            try
            {
                string PrizeAmountInWord = "";
                string PrizeAmount = "";
                 DataTable dtInfo = new DataTable();
                 dtInfo = objLtmsService.GetLotteryClaimEntryDtlByReqCode(Session["ApplicationId"].ToString());
                 if (dtInfo.Rows.Count > 0)
                 {
                    
                     if (dtInfo.Rows[0]["ClaimType"].ToString() == "1")
                     {
                         PrizeAmount = dtInfo.Rows[0]["PrizeAmount"].ToString();
                         PrizeAmountInWord = objValidateData.NumberToText(Convert.ToInt64(Math.Round(Convert.ToDouble(dtInfo.Rows[0]["PrizeAmount"].ToString())))) + " Only"; ;
                         dvPwtAnnexI.Visible = true;
                         dvPwtBondAnnexI.Visible = false;
                         dvPwtAnnexIII.Visible = false;
                         dvPageBreak.Visible = false;
                         trPwtTicket.Visible = true;

                         lblApplicationId.Text = "Application Id : " + dtInfo.Rows[0]["ReqCode"].ToString();
                         lblLotteryType.Text = dtInfo.Rows[0]["LotteryType"].ToString();
                         lblLotteryName.Text = dtInfo.Rows[0]["LotteryName"].ToString().ToUpper();
                         lblDrawNo.Text = dtInfo.Rows[0]["DrawNo"].ToString();
                         lblDrawDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"]).ToString("dd-MMM-yyyy").ToUpper();
                         lblLotteryNo.Text = dtInfo.Rows[0]["LotteryNo"].ToString();
                         lblMobileNo.Text = dtInfo.Rows[0]["MobileNo"].ToString();
                         lblEmailId.Text = dtInfo.Rows[0]["EmailId"].ToString().ToUpper();
                         lblName.Text = dtInfo.Rows[0]["Name"].ToString().ToUpper();
                         lblFatherOrGuardianName.Text = dtInfo.Rows[0]["FatherOrGuardianName"].ToString().ToUpper();
                         lblAddress.Text = dtInfo.Rows[0]["Address"].ToString().ToUpper();
                         lblAadharNo.Text = dtInfo.Rows[0]["AadharNo"].ToString().ToUpper();
                         lblPanCard.Text = dtInfo.Rows[0]["PanNo"].ToString().ToUpper();
                         lblBankName.Text = dtInfo.Rows[0]["BankName"].ToString().ToUpper();
                         lblBankAccountNo.Text = dtInfo.Rows[0]["BankAccountNo"].ToString().ToUpper();
                         lblIFSCCode.Text = dtInfo.Rows[0]["IFSCCode"].ToString().ToUpper();
                         lblNameOfPrize.Text = dtInfo.Rows[0]["NameOfPrize"].ToString().ToUpper();
                         lblPrizeAmount.Text = PrizeAmount;
                         
                         lblRupees.Text = PrizeAmountInWord;

                         #region Annexture II
                         lblNameOfPrizeAnxII.Text = dtInfo.Rows[0]["NameOfPrize"].ToString().ToUpper();
                         lblLotteryNameAndDrawNoAnxII.Text = dtInfo.Rows[0]["DrawNo"].ToString().ToUpper() + " (" + dtInfo.Rows[0]["LotteryName"].ToString() + ")";
                         lblNameAnxII.Text = dtInfo.Rows[0]["Name"].ToString().ToUpper();
                         lblAddressAnxII.Text = dtInfo.Rows[0]["Address"].ToString().ToUpper();
                         lblPrizeAmountAnxII.Text = PrizeAmount;
                         lblRupeesAnxII.Text = PrizeAmountInWord;
                         lblEquivalentNameOfPrizeAnxII.Text = dtInfo.Rows[0]["NameOfPrize"].ToString().ToUpper();
                         lblLotteryNoAnxII.Text = dtInfo.Rows[0]["LotteryNo"].ToString().ToUpper();
                         lblLotteryNameAndDrawNoAnxII_2.Text = dtInfo.Rows[0]["DrawNo"].ToString().ToUpper() + " (" + dtInfo.Rows[0]["LotteryName"].ToString() + ")";
                         lblLotteryNoAnxII_2.Text = dtInfo.Rows[0]["LotteryNo"].ToString().ToUpper();
                         lblNameOfPrizeAnxII_2.Text = dtInfo.Rows[0]["NameOfPrize"].ToString().ToUpper();
                         lblLotteryNameAndDrawNoAnxII_3.Text = dtInfo.Rows[0]["DrawNo"].ToString().ToUpper() + " (" + dtInfo.Rows[0]["LotteryName"].ToString() + ")";
                         lblPrizeAmountAnxII_3.Text = PrizeAmount;
                         lblLotteryNoAnxII_3.Text = dtInfo.Rows[0]["LotteryNo"].ToString().ToUpper();
                         lblNameOfPrizeAnxII_3.Text = dtInfo.Rows[0]["NameOfPrize"].ToString().ToUpper();
                         lblPrizeAmountAnxII_4.Text = PrizeAmount;
                         lblNameOfPrizeAnxII_4.Text = dtInfo.Rows[0]["NameOfPrize"].ToString().ToUpper();
                         lblPrizeAmountAnxII_6.Text = PrizeAmount;
                         lblRupeesAnxII_6.Text = PrizeAmountInWord;
                         lblPrizeAmountAnxII_7.Text = PrizeAmount;
                         lblRupeesAnxII_7.Text = PrizeAmountInWord;
                         lblSoDoWoAnxII.Text = dtInfo.Rows[0]["FatherOrGuardianName"].ToString().ToUpper();
                        // lblSoDoWoAnxII.Text = dtInfo.Rows[0]["SoDoWo"].ToString().ToUpper();
                         lblSuretyAnxII.Text = dtInfo.Rows[0]["Surety"].ToString().ToUpper();
                         lblSuretyAddressAnxII.Text = dtInfo.Rows[0]["Address"].ToString().ToUpper();
                         if (dtInfo.Rows[0]["Photo"].ToString().Length > 0)
                         {
                             byte[] Photo = ((byte[])dtInfo.Rows[0]["Photo"]);
                             imgPhoto.Visible = true;
                             imgPhoto.BorderWidth = 1;
                             imgPhoto.ImageUrl = "data:image;base64," + Convert.ToBase64String(Photo);                            
                         }

                         if (dtInfo.Rows[0]["PwtTicket"].ToString().Length > 0)
                         {
                             byte[] PwtTicket = ((byte[])dtInfo.Rows[0]["PwtTicket"]);
                             imgPwtTicket.Visible = true;
                             imgPwtTicket.BorderWidth = 1;
                             imgPwtTicket.ImageUrl = "data:image;base64," + Convert.ToBase64String(PwtTicket);
                         }
                         #endregion
                     }
                     else if (dtInfo.Rows[0]["ClaimType"].ToString() == "2" | dtInfo.Rows[0]["ClaimType"].ToString() == "3")
                     {
                         if (dtInfo.Rows[0]["ClaimType"].ToString() == "2")
                         {
                             PrizeAmount = dtInfo.Rows[0]["SuperTicketAmount"].ToString();
                             PrizeAmountInWord = objValidateData.NumberToText(Convert.ToInt64(Math.Round(Convert.ToDouble(dtInfo.Rows[0]["SuperTicketAmount"].ToString())))) + " Only"; ;
                         }
                         else if (dtInfo.Rows[0]["ClaimType"].ToString() == "3")
                         {
                             PrizeAmount = dtInfo.Rows[0]["SpecialTicketAmount"].ToString();
                             PrizeAmountInWord = objValidateData.NumberToText(Convert.ToInt64(Math.Round(Convert.ToDouble(dtInfo.Rows[0]["SpecialTicketAmount"].ToString())))) + " Only"; ;
                         }
                         dvPageBreak.Visible = false;
                         dvPwtAnnexI.Visible = false;
                         dvPwtBondAnnexI.Visible = false;
                         dvPwtAnnexIII.Visible = true;

                         #region Annexture III
                         lblApplicationIdAnxIII.Text = "Application Id : " + dtInfo.Rows[0]["ReqCode"].ToString();
                         lblNameOfPrizeAnxIII.Text = dtInfo.Rows[0]["NameOfPrize"].ToString().ToUpper();
                         lblLotteryNameAndDrawNoAnxIII.Text = dtInfo.Rows[0]["DrawNo"].ToString().ToUpper() + " (" + dtInfo.Rows[0]["LotteryName"].ToString() + ")";
                         lblNameAnxIII.Text = dtInfo.Rows[0]["Name"].ToString().ToUpper();
                         lblAddressAnxIII.Text = dtInfo.Rows[0]["Address"].ToString().ToUpper();
                         lblMobileNoAnxIII.Text = dtInfo.Rows[0]["MobileNo"].ToString();
                         lblPrizeAmountAnxIII.Text = PrizeAmount;
                         lblRupeesAnxIII.Text = PrizeAmountInWord;
                         lblLotteryNoAnxIII.Text = dtInfo.Rows[0]["LotteryNo"].ToString().ToUpper();
                         lblDrawDateAnxIII.Text = Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"]).ToString("dd-MMM-yyyy").ToUpper();
                         lblBankNameAnxIII.Text = dtInfo.Rows[0]["BankName"].ToString().ToUpper();
                         lblBankAccountNoAnxIII.Text = dtInfo.Rows[0]["BankAccountNo"].ToString().ToUpper();
                         lblIFSCCodeAnxIII.Text = dtInfo.Rows[0]["IFSCCode"].ToString().ToUpper();
                         lblPanCardAnxIII.Text = dtInfo.Rows[0]["PanNo"].ToString().ToUpper();
                         lblLotteryNoAnxIII_2.Text = dtInfo.Rows[0]["LotteryNo"].ToString();
                         lblLotteryNameAndDrawNoAnxIII_2.Text = dtInfo.Rows[0]["DrawNo"].ToString().ToUpper();
                         lblNameOfPrizeAnxIII_2.Text = dtInfo.Rows[0]["NameOfPrize"].ToString().ToUpper();
                         lblDrawDateAnxIII_2.Text = Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"]).ToString("dd-MMM-yyyy").ToUpper();
                         lblNameOfPrizeAnxIII_3.Text = dtInfo.Rows[0]["NameOfPrize"].ToString().ToUpper();
                         lblLotteryNameAndDrawNoAnxIII_3.Text = dtInfo.Rows[0]["DrawNo"].ToString().ToUpper();
                         lblNameAnxIII_2.Text = dtInfo.Rows[0]["Name"].ToString().ToUpper();
                         lblPrizeAmountAnxIII_2.Text = PrizeAmount;
                         lblProprietorName.Text = dtInfo.Rows[0]["ProprietorOf"].ToString();
                         lblRupeesAnxIII_2.Text = PrizeAmountInWord;

                         if (dtInfo.Rows[0]["Photo"].ToString().Length > 0)
                         {                            
                             byte[] Photo = ((byte[])dtInfo.Rows[0]["Photo"]);
                             imgAnnextureIII.Visible = true;
                             imgAnnextureIII.BorderWidth = 1;
                             imgAnnextureIII.ImageUrl = "data:image;base64," + Convert.ToBase64String(Photo);
                         }
                         #endregion
                     }
                     

                    

                     byte[] aadharPic =((byte[])dtInfo.Rows[0]["AadharFile"]);
                     imgAadharPic.Visible = true;
                     imgAadharPic.BorderWidth = 1;
                     imgAadharPic.ImageUrl = "data:image;base64," + Convert.ToBase64String(aadharPic);

                     byte[] PanPic = ((byte[])dtInfo.Rows[0]["PanFile"]);
                     imgPan.Visible = true;
                     imgPan.BorderWidth = 1;
                     imgPan.ImageUrl = "data:image;base64," + Convert.ToBase64String(PanPic);

                     byte[] BankDtlPic = ((byte[])dtInfo.Rows[0]["BankDtl"]);
                     imgBankDtl.Visible = true;
                     imgBankDtl.BorderWidth = 1;
                     imgBankDtl.ImageUrl = "data:image;base64," + Convert.ToBase64String(BankDtlPic);

                    // ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Lottery Claim information Saved successfully. Your Application id is :" + dtInfo.Rows[0]["ReqCode"].ToString() + " .Please Keep the Application Id for your futere reference');", true);
                    // Session["ApplicationId"] = null;
                 }
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }

        public Image byteArrayToImage(byte[] imgBytes)
        {
            using (MemoryStream imgStream = new MemoryStream(imgBytes))
            {
                return Image.FromStream(imgStream);
            }
        }

    }
}