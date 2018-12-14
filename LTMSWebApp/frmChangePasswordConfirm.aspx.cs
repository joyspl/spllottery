using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class frmChangePasswordConfirm : System.Web.UI.Page
    {
        ClsUser objUser = new ClsUser();
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["PasswordChange"] == null)
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
        public void ShowApplicationIdDetails()
        {
            hdUniqueId.Value = ((ClsUser)Session["PasswordChange"]).DataUniqueId;
            txtUserId.Text = ((ClsUser)Session["PasswordChange"]).UserId.ToString();
            txtMobileNo.Text = ((ClsUser)Session["PasswordChange"]).MobileNo.ToString();
            txtEmailId.Text = ((ClsUser)Session["PasswordChange"]).EmailId.ToString();
        
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {


            if (txtNewPassword.Text.Trim() == "" && !(txtNewPassword.Text.Trim().Length>=8))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('New Password can not be left blank and it should be 8 character in length.');", true);
                txtNewPassword.Focus();
                return;
            }

            if (txtNewPassword.Text.Trim() != txtConformPassword.Text.Trim())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('New Password and confirm password is not matching it should be same.');", true);
                txtConformPassword.Focus();
                return;
            }

            if (hdUniqueId.Value.ToString().Trim() != "")
            {
                objUser.DataUniqueId = ((ClsUser)Session["PasswordChange"]).DataUniqueId;
                objUser.UserPassword = txtNewPassword.Text.Trim();
                bool IsAdded = objLtmsService.UpdateUserPassword(objUser);
                if (IsAdded == true)
                {
                    lblMsg.Text = "Password changed successfully.. Click <a class='d-block small' href='Default.aspx'>Here</a> to login again";
                   // ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Password changed Saved successfully.');", true);
                }
            }
            
        }
    }
}