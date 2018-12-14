using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class TrxDashboard : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsRequisition objRequisition = new ClsRequisition();
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MenuInfo"] == null)
            {
                Session["CustomError"] = "Invalid Navigation option. Please use the Navigation panel to access this page.";
                Server.Transfer("appError.aspx");
                return;
            }
            else
            {
                objMenuOptions = (ClsMenuInfo)Session["MenuInfo"];
            }
            if (IsPostBack == false)
            {
                if (((bool)Session["FromNavigation"]) == false)
                {
                    Session["CustomError"] = "Invalid Navigation option. Please use the Navigation panel to access this page.";
                    Server.Transfer("appError.aspx");
                }
                else
                {
                    Session["FromNavigation"] = false;
                    if (!(objMenuOptions.AllowEntry || objMenuOptions.AllowEdit || objMenuOptions.AllowDelete || objMenuOptions.AllowView))
                    {
                        Session["CustomError"] = "You do not have proper Privilege to access the selected module";
                        Server.Transfer("appError.aspx");
                        return;
                    }
                    GetDealerDepositInHand();
                }
            }
           
        }

        private void GetDealerDepositInHand()
        {
            txtDepositeAmountLD.Text = "0";
          //  txtDepositeAmountSPL.Text = "0";
            DataSet dsDepositeAmountInHand = new DataSet();
            dsDepositeAmountInHand = objLtmsService.GetDealerDepositInHandDtlById(Convert.ToInt64(hdUniqueId.Value));
            if (dsDepositeAmountInHand.Tables.Count > 0)
            {
                if (dsDepositeAmountInHand.Tables[0].Rows.Count > 0)
                {
                    txtDepositeAmountLD.Text = dsDepositeAmountInHand.Tables[0].Rows[0]["DepositInHandAmountLD"].ToString();
                }
                //if (dsDepositeAmountInHand.Tables[1].Rows.Count > 0)
                //{
                //    txtDepositeAmountSPL.Text = dsDepositeAmountInHand.Tables[1].Rows[0]["DepositInHandAmountSPL"].ToString();
                //}
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        //protected void PrintOrder_Click(object sender, EventArgs e)
        //{
          
        //    clsInputParameter objInputParameter = new clsInputParameter();
        //    objInputParameter.DataUniqueId = Convert.ToInt64(DataUniqueId);
        //    objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
        //    Session["InputParameter"] = objInputParameter;
        //    Session["ReportName"] = "PrintOrder";
        //    Response.Redirect("rptViewAppReport.aspx");
        
        //}
    }
}