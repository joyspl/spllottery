using LTMSClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class WebPageMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");           
            if (IsPostBack == false)
            {
                if (((bool)Session["Allow"]) == true)
                {
                    if (Session["UserInfo"] != null)
                    {

                       ltrMenu.Text = ((ClsUserInfo)Session["UserInfo"]).MenuList.Replace("&", "&amp;").ToString();
                        ltrUserName.Text = ((ClsUserInfo)Session["UserInfo"]).DisplayName;
                        lblDateTime.Text = "Date : " + Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
                    }
                }
            }

        }
    }
}