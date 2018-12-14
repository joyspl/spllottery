using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class frmChangePassword : System.Web.UI.Page
    {
        ClsUser objUser = new ClsUser();
        //ClsLotteryClaimDetails objClsLotteryClaimDetails = new ClsLotteryClaimDetails();
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSendOtp_Click(object sender, EventArgs e)
        {
            string mobOtpSendStatus = "", emailOtpSendStatus = "";
            Session["LoginOtp"] = objValidateData.GenerateRandomOTP();
            string TextOtpMsg = Session["LoginOtp"].ToString() + " is your One Time Password for changeing Password";
            string MailSubject = "Your One-Time Password to Change your password";
            StringBuilder strMessage = new StringBuilder();
            strMessage.AppendLine("<table id='tblOTP' cellpadding='0' cellspacing='0' border='0' width='650px'>");
            strMessage.AppendLine("     <tr style='border:solid 1px;'><td><b>Your Login Key One-Time Password</b></td></tr>");
            strMessage.AppendLine("     <tr><td><b>" + Session["LoginOtp"].ToString() + " is your One-Time Password to change your password. </b></td></tr>");
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
            else
            {
                lblMsg.Text = "There are some issue while sending SMS to your mobile and email as provided above. Please contact administrator.";
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Session["PasswordChange"] = null;
            if (objValidateData.isEmail(txtEmailId.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid Email Id.');", true);
                txtEmailId.Focus();
                return;
            }

            if (txtMobileNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Mobile No.');", true);
                txtMobileNo.Focus();
                return;
            }
            DataTable dtUser = new DataTable();
            dtUser = objLtmsService.GetUserDtlByEmailOrMobile(txtEmailId.Text.Trim(), txtMobileNo.Text.Trim());
            if (dtUser.Rows.Count > 0)
            {
                objUser.DataUniqueId = dtUser.Rows[0]["DataUniqueId"].ToString();
                objUser.UserId=  dtUser.Rows[0]["UserId"].ToString();
                objUser.MobileNo = txtMobileNo.Text.Trim();
                objUser.EmailId = txtEmailId.Text.Trim();
                //objUser.OTP = txtOTP.Text.Trim();

                Session["PasswordChange"] = objUser;
                
            }
            else {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Not a valid user for the Mobile and Email provided');", true);
                return;
            }
            if (Session["LoginOtp"] == null) {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Enter valid OTP');", true);
                return;
            }
            if (txtOTP.Text.Trim() != Session["LoginOtp"].ToString())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Enter valid OTP sent to your Mobile and Email provided');", true);
                return;
            }
            Response.Redirect("frmChangePasswordConfirm.aspx");

          
        }
    }
}