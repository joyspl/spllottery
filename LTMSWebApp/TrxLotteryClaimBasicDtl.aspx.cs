using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class TrxLotteryClaimBasicDtl : System.Web.UI.Page
    {
        ClsLotteryClaimDetails objClsLotteryClaimDetails = new ClsLotteryClaimDetails();
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Session["captcha"] = null;
                Session["LoginOtp"] = null;
                Session["ApplicationId"] = null;
                FillComboBox();
                FillCapctha();

            }
        }
        //Populate the dropdownlist for Brand, category and company from the database 
        private void FillComboBox()
        {
            try
            {
                objValidateData.FillDropDownList(ddlLotteryType, "LotteryType", "");
                ddlClaimType.Items.Clear();

                Dictionary<String, String> objClaimType = objValidateData.ClaimType();
                ddlClaimType.DataSource = objClaimType;
                ddlClaimType.DataTextField = "Value";
                ddlClaimType.DataValueField = "Key";
                ddlClaimType.DataBind();


            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void ddlLotteryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objValidateData.FillDropDownList(ddlLotteryName, "LotteryNameByLotteryTypeID", ddlLotteryType.SelectedValue);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }

        }
        public void FillCapctha()
        {
            try
            {
                Random random = new Random();
                string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StringBuilder captcha = new StringBuilder();
                for (int i = 0; i < 6; i++)
                {
                    captcha.Append(combination[random.Next(combination.Length)]);
                    Session["captcha"] = captcha.ToString();
                    imgCaptcha.ImageUrl = "GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();
                } 
            }
            catch { }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            FillCapctha();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Session["LotteryClaimDetails"] = null;
                // string AlphabetSeries = "";
                DataTable dtInfo = new DataTable();
                string DataUniqueId = "";
                if (IsValidEntry(out DataUniqueId) == false) { return; }//Check for valid data entry
                objClsLotteryClaimDetails.ClaimType = ddlClaimType.SelectedIndex;
                objClsLotteryClaimDetails.LotteryId = Convert.ToInt64(ddlLotteryName.SelectedValue);
                objClsLotteryClaimDetails.LotteryName = ddlLotteryName.Text;
                objClsLotteryClaimDetails.LotteryTypeId = Convert.ToInt64(ddlLotteryType.SelectedValue);
                objClsLotteryClaimDetails.LotteryType = ddlLotteryType.Text;
                objClsLotteryClaimDetails.DrawNo = Convert.ToInt16(txtDrawNo.Text.Trim());
                objClsLotteryClaimDetails.DrawDate = null;
                if (objValidateData.isValidDate(txtDrawDate.Text.Trim()) == true)
                {
                    objClsLotteryClaimDetails.DrawDate = Convert.ToDateTime(txtDrawDate.Text.Trim());
                }
                objClsLotteryClaimDetails.LotteryNo = txtLotteryNo.Text.Trim();

                objClsLotteryClaimDetails.Captcha = txtCaptcha.Text.Trim();
                objClsLotteryClaimDetails.DataUniqueId = DataUniqueId;
                Session["LotteryClaimDetails"] = objClsLotteryClaimDetails;
                 
                pnlDataEntry.Visible = false;
                pnlValidTicket.Visible = true;
                pnlLogin.Visible = false;
                //pnlClaimEntry.Visible = false;               
              
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void btnNo_Click(object sender, EventArgs e)
        { 
            Session["captcha"]="";
            Response.Redirect("Home.aspx");
        }
        protected void btnYes_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
            pnlValidTicket.Visible = false;
            pnlLogin.Visible = true;
            //pnlClaimEntry.Visible = false;
            lblMsg.Text = "";
           
        }

        protected void btnSendOtp_Click(object sender, EventArgs e)
        {
            string mobOtpSendStatus = "", emailOtpSendStatus = "";
            Session["LoginOtp"] = objValidateData.GenerateRandomOTP();
            //string TextOtpMsg= Session["LoginOtp"].ToString()+ " is your One Time Password for Claiming Prize";
            string TextOtpMsg = "One Time Password for Prize Claim Login is " + Session["LoginOtp"].ToString() + " Pls use OTP to complete the Login. Do not share with anyone. EMail ID " + txtEmailId.Text;
            string MailSubject = "Your One-Time Password to Change your password";
            StringBuilder strMessage = new StringBuilder();
            strMessage.AppendLine(" <table id='tblOTP' cellpadding='0' cellspacing='0' border='0' width='650px'>");
            strMessage.AppendLine("<tr style='border:solid 1px;'><td><b>Your Login Key One-Time Password</b></td></tr>");
            strMessage.AppendLine("<tr><td><b>One Time Password for Prize Claim Login is " + Session["LoginOtp"].ToString() + " Pls use OTP to complete the Login. Do not share with anyone. EMail ID " + txtEmailId.Text + "</b></td></tr>");

            strMessage.AppendLine("</table>");


            mobOtpSendStatus = objValidateData.SMSSend(txtMobileNo.Text, TextOtpMsg);
            emailOtpSendStatus = objValidateData.SendEmail(txtEmailId.Text, MailSubject, strMessage.ToString());
            //return;
            if (mobOtpSendStatus == "SUCCESSFULL" && emailOtpSendStatus == "SUCCESSFULL")
            {
                lblMsg.Text = "OTP successfully sent to your mobile and Email as provided above.";
            }
            else if (mobOtpSendStatus == "SUCCESSFULL" && emailOtpSendStatus == "UNSUCCESSFULL")
            {
                lblMsg.Text = "OTP successfully sent to your mobile and there was some issue while sending Email as provided above. please continue with OTP provided in mobile.";
            }
            else if (mobOtpSendStatus == "UNSUCCESSFULL" && emailOtpSendStatus == "SUCCESSFULL")
            {
                lblMsg.Text = "OTP successfully sent to your email and there was some issue while sending SMS to your mobile as provided above. please continue with OTP provided in email.";
            }
            else {
                lblMsg.Text = "There are some issue while sending SMS to your mobile and email as provided above. Please contact administrator.";
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (objValidateData.isEmail(txtEmailId.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid Email Id.');", true);
                txtEmailId.Focus();
                return ;
            }

            if (txtMobileNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Mobile No.');", true);
                txtMobileNo.Focus();
                return ;
            }
            if (Session["LoginOtp"] == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Enter valid OTP. Click on Send button to resend the OTP on your mobile and email id');", true);
                return;
            
            }
            //OPT
            if (txtOTP.Text.Trim() != Session["LoginOtp"].ToString())
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Enter valid OTP sent to your Mobile and Email provided');", true);
                return;
            }
            ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).MobileNo = txtMobileNo.Text.Trim();
            ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).EmailId = txtEmailId.Text.Trim();
            ((ClsLotteryClaimDetails)Session["LotteryClaimDetails"]).OTP = txtOTP.Text.Trim();
            Response.Redirect("TrxLotteryClaimEntry.aspx");
         
        }
       

        //Checking for valid data entry 
        private bool IsValidEntry(out string DataUniqueId)
        {
            DataUniqueId = "";
            if (ddlClaimType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Claim Type.');", true);
                ddlClaimType.Focus();
                return false;
            }
            if (ddlLotteryType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Lottery Type.');", true);
                ddlLotteryType.Focus();
                return false;
            }
            if (ddlLotteryName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Lottery Name.');", true);
                ddlLotteryName.Focus();
                return false;
            }
            
            if (objValidateData.isValidDate(txtDrawDate.Text.Trim()) == false && txtDrawNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid draw date or Draw no.');", true);
                txtDrawNo.Focus();
                return false;
            }
            if (txtDrawDate.Text.Trim() != "")
            {
                if (objValidateData.isValidDate(txtDrawDate.Text.Trim()) == false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid draw date.');", true);
                    txtDrawDate.Focus();
                    return false;
                }
            }
            if (txtDrawNo.Text.Trim() != "")
            {
                if (objValidateData.IsIntegerWithZero(txtDrawNo.Text.Trim())==false)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid draw No.');", true);
                    txtDrawNo.Focus();
                    return false;
                }
            }

            if (txtLotteryNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Lottery No.');", true);
                txtLotteryNo.Focus();
                return false;
            }
            if (txtCaptcha.Text.Trim() != Session["captcha"].ToString())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Enter valid captcha');", true);
                FillCapctha();
                txtCaptcha.Text = "";
                txtCaptcha.Focus();
                return false;
            }
            ClsTicketInventory objGeneratedNo = new ClsTicketInventory();
            DataTable dtLotteryInfo = new DataTable();

            Int64 InLotteryId=Convert.ToInt64(ddlLotteryName.SelectedValue);
            int? InDrawNo = string.IsNullOrEmpty(txtDrawNo.Text.Trim()) ? (int?)null : int.Parse(txtDrawNo.Text.Trim());  
            DateTime? InDrawDate = string.IsNullOrEmpty(txtDrawDate.Text.Trim()) ? (DateTime?)null : DateTime.Parse(txtDrawDate.Text.Trim());
            dtLotteryInfo = objLtmsService.GetLotteryDtlFromRequisitionDtl(InLotteryId, InDrawNo,  InDrawDate);
            if (dtLotteryInfo.Rows.Count > 0)
            {
                objGeneratedNo.DataUniqueId = Convert.ToInt64(dtLotteryInfo.Rows[0]["DataUniqueId"].ToString());
                DataUniqueId=objGeneratedNo.DataUniqueId.ToString();
                objGeneratedNo.DrawNo = Convert.ToInt16(dtLotteryInfo.Rows[0]["DrawNo"].ToString());
                objGeneratedNo.DrawDate = Convert.ToDateTime(dtLotteryInfo.Rows[0]["DrawDate"].ToString());
                objGeneratedNo.FnStart = Convert.ToInt16(dtLotteryInfo.Rows[0]["FnStart"].ToString());
                objGeneratedNo.FnEnd = Convert.ToInt16(dtLotteryInfo.Rows[0]["FnEnd"].ToString());
                objGeneratedNo.AlphabetSeries = dtLotteryInfo.Rows[0]["AlphabetSeries"].ToString();
                objGeneratedNo.TnStart = Convert.ToInt64(dtLotteryInfo.Rows[0]["TnStart"].ToString());
                objGeneratedNo.TnEnd = Convert.ToInt64(dtLotteryInfo.Rows[0]["TnEnd"].ToString());
                if (txtDrawNo.Text.Trim() == "")
                {
                    txtDrawNo.Text =Convert.ToDateTime(objGeneratedNo.DrawDate).ToString("dd-MMM-yyyy");
                }
                if (txtDrawNo.Text.Trim() != "")
                {
                    txtDrawNo.Text = objGeneratedNo.DrawNo.ToString();
                }
                string ErrorMsg = objValidateData.ValidateTicketNo(objGeneratedNo, txtLotteryNo.Text);
                if (ErrorMsg.Trim().Length > 0)
                {
                    var message = new JavaScriptSerializer().Serialize(ErrorMsg);
                    var script = string.Format("alert({0});", message);
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                    txtLotteryNo.Focus();
                    return false;
                }
                DataSet dtLotteryWiningSerialNoDtl = objLtmsService.GetLotteryWiningSerialNoDtlByLotteryNo(objGeneratedNo.DataUniqueId, txtLotteryNo.Text.Trim());
                if (dtLotteryWiningSerialNoDtl.Tables[0].Rows.Count > 0)
                {
                    if (ddlClaimType.SelectedIndex == 1)
                    {
                        lblPrize.Text = "<font color='Yellow'><b>The Lottery Ticket " + txtLotteryNo.Text.Trim() + " is available for " + dtLotteryWiningSerialNoDtl.Tables[0].Rows[0]["NameOfPrize"].ToString() + " and The Prize Amount is " + dtLotteryWiningSerialNoDtl.Tables[0].Rows[0]["PrizeAmount"].ToString() + "</b></font>";
                    }
                    else if (ddlClaimType.SelectedIndex == 2)
                    {
                        lblPrize.Text = "<font color='Yellow'><b>The Lottery Ticket " + txtLotteryNo.Text.Trim() + " is available for Super " + dtLotteryWiningSerialNoDtl.Tables[0].Rows[0]["NameOfPrize"].ToString() + " and The Prize Amount is " + dtLotteryWiningSerialNoDtl.Tables[0].Rows[0]["SuperTicketAmount"].ToString() + "</b></font>";
                    }
                    else if (ddlClaimType.SelectedIndex == 3)
                    {
                        lblPrize.Text = "<font color='Yellow'><b>The Lottery Ticket " + txtLotteryNo.Text.Trim() + " is available for Special " + dtLotteryWiningSerialNoDtl.Tables[0].Rows[0]["NameOfPrize"].ToString() + " and The Prize Amount is " + dtLotteryWiningSerialNoDtl.Tables[0].Rows[0]["SpecialTicketAmount"].ToString() + "</b></font>";
                    }
                }
                else {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Enter valid ticket no. The ticket no you entered is not in prize list for the date and draw no you specified');", true);
                    return false;
                }

                dtLotteryWiningSerialNoDtl.Dispose();
            }
            else {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Enter valid ticket no. The ticket no you entered is not correct for the date and draw no you specified');", true);
                return false;
            }
           // GetLotteryDtlFromRequisitionDtl(Int64 InLotteryId, int InDrawNo, DateTime InDrawDate)

            return true;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx?ID=" + Guid.NewGuid().ToString() + "&DT=" + DateTime.Now);       
        }
    }
}