using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class appError : System.Web.UI.Page
    {
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String errMsg = "";
                StringBuilder strMsg = new StringBuilder();
                try
                {
                    errMsg = Request.QueryString["XX"].ToString();
                }
                catch (Exception ex)
                {
                }
                if (errMsg.Trim().Length > 0 && errMsg.Trim().ToUpper() != "TIMEOUT")
                {
                    ltrlError.Text = errMsg;
                }
                else
                {


                    if (ltrlError.Text == "You are not allowed to access the System. Please contact Administrator.")
                    {
                        ltrlError.Visible = false;
                    }
                    if (errMsg.Trim().ToUpper() == "TIMEOUT")
                    {
                        ltrlError.Text = "Your session has expired. Cannot Continue.";
                        Response.Redirect("HTMLPage1.htm");
                        Session.Abandon();
                        return;
                    }
                    try
                    {
                        if (((String)Session["CustomError"]).Trim().Length > 0)
                        {
                            ltrlError.Text = (String)Session["CustomError"];
                        }
                        else
                        {
                            ltrlError.Text = "Some unspecified Error(s) have occured.<br>Please try again.";
                        }
                    }
                    catch (Exception Ex)
                    {
                        ltrlError.Text = "Some unspecified Error(s) have occured.<br>Please try again.";
                    }


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
    }
}