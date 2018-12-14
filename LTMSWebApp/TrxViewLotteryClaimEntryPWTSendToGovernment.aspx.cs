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
    public partial class TrxViewLotteryClaimEntryPWTSendToGovernment : System.Web.UI.Page
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
                    txtFromDate.Text = DateTime.Now.AddMonths(-2).ToString("dd-MMM-yyyy");
                    txtToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    hdUniqueId.Value = null;
                    FillComboBox();
                   // BindGvData();
                }
            }
        }
        private void FillComboBox()
        {
            try
            {
                objValidateData.FillDropDownList(ddlLotteryType, "LotteryType", "");
                ddlStatus.Items.Clear();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-1"));
                items.Add(new ListItem("Pending", "5"));
                ddlStatus.Items.AddRange(items.ToArray());
                ddlStatus.SelectedIndex = 1;

                ddlClaimType.Items.Clear();
                items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-1"));
                items.Add(new ListItem("PWT", "1"));
                items.Add(new ListItem("Super / Special", "2"));
                ddlClaimType.Items.AddRange(items.ToArray());
                ddlClaimType.SelectedIndex = 0;
                
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
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void ddlLotteryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objValidateData.FillDropDownList(ddlGovernmentOrder, "GovOrderDtlByLotteryId",ddlLotteryName.SelectedValue);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ReqId="";
                string Status = "";
                DataTable dtInfo = new DataTable();               
                if (IsValidEntry() == false) { return; }
                foreach (GridViewRow row in GvData.Rows)
                {
                    CheckBox check = row.FindControl("chkSelect") as CheckBox;
                    if (check.Checked)
                    {
                        ReqId = ReqId + GvData.DataKeys[row.RowIndex].Value.ToString() +",";
                    }
                }
                ReqId = ReqId.TrimEnd(',');

                if (((Button)sender).CommandName == "Approve")
                {
                    Status = "Approved";
                    objRequisition.SaveStatus = 7;
                }                                        
                string TransactionNo="";

               

                objRequisition.GovermentOrderId = Convert.ToInt16(ddlGovernmentOrder.SelectedValue);
                objRequisition.ClaimType = Convert.ToInt16(ddlClaimType.SelectedValue);
                objRequisition.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId; 
                objRequisition.IpAdd = Request.UserHostAddress;
                bool IsUpdated = objLtmsService.UpdateLotteryClaimEntrySendToGov(objRequisition, ReqId,out TransactionNo);
                if (IsUpdated == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Selected information sent successfully to goverment and the Sent Id is " + TransactionNo + ".');", true);
                }
                BindGvData();
                //btnCancel_Click(sender, e);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }

        //Checking for valid data entry 
        private bool IsValidEntry()
        {
            bool IsSelected = false;
            foreach (GridViewRow row in GvData.Rows)
            {
                CheckBox check = row.FindControl("chkSelect") as CheckBox;
                if (check.Checked)
                {
                    IsSelected = true;
                    break;
                }
            }
            if (IsSelected == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select atleast one Requisition from the list');", true);
                return false;
            }         
           
            
            return true;
        }
        private void BindGvData()
        {
            try
            {
                int GovIdId =Convert.ToInt16(ddlGovernmentOrder.SelectedValue);
                int ClaimType = Convert.ToInt16(ddlClaimType.SelectedValue);
                
                clsInputParameter objInputParameter = new clsInputParameter();
                objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00.000");
                objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59.999");
                objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);
                GvData.DataSource = objLtmsService.GetLotteryClaimSendToGovDtlByStatus(objInputParameter, ClaimType, GovIdId);
                GvData.DataBind();
                gridDiv.Visible = objMenuOptions.AllowView;
                btnPrint.Visible = (GvData.Rows.Count > 0 ? true : false);
                btnSave.Visible = (ddlClaimType.SelectedIndex > 0 ? true : false);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        
        protected void GvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                    Label lblTicketUnSoldUploadStatus = ((Label)e.Row.FindControl("lblTicketUnSoldUploadStatus"));
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 5) { Status = "Pending"; }
                    else if (StatusVal == 7) { Status = "Send To Goverment"; }
                    lblStatus.Text = Status;
                    if (StatusVal >=7)
                    {
                        ((CheckBox)e.Row.FindControl("chkSelect")).Visible = false;
                    }
                    Label lblClaimType = ((Label)e.Row.FindControl("lblClaimType"));
                    Dictionary<String, String> objClaimType = objValidateData.ClaimType();
                    lblClaimType.Text = objClaimType[lblClaimType.Text];
                    
                }
                catch { }
            }
        }

        
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                BindGvData();
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            clsInputParameter objInputParameter = new clsInputParameter();
            objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00.000");
            objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59.999");
            objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);
            objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
            Session["InputParameter"] = objInputParameter;
            Session["ReportName"] = "RequisitionApprovalDtl";
            Response.Redirect("rptViewAppReport.aspx");

        }
    }
}