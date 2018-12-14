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
    public partial class TrxSpecialTicketClaimedFinalLevelApproval : System.Web.UI.Page
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
                    BindGvData();
                }
            }
        }
        private void FillComboBox()
        {
            try
            {
                ddlStatus.Items.Clear();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-3"));
                items.Add(new ListItem("Pending", "3"));
                ddlStatus.Items.AddRange(items.ToArray());
                ddlStatus.SelectedIndex = 1;
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
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
                    objRequisition.SaveStatus = 5;
                }
                else if (((Button)sender).CommandName == "Reject")
                {
                    Status = "Rejected";
                    objRequisition.SaveStatus = 6;
                }                             
 
                objRequisition.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId; 
                objRequisition.IpAdd = Request.UserHostAddress;
                bool IsUpdated = objLtmsService.UpdateApprovalByStausInLotteryClaimEntry(objRequisition, ReqId,3);
                if (IsUpdated == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Selected information " + Status + " successfully.');", true);
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

                clsInputParameter objInputParameter = new clsInputParameter();
                objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00.000");
                objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59.999");
                objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);
                GvData.DataSource = objLtmsService.GetLotteryClaimEntryDtlByStatus(objInputParameter, 3);//1==PWT entry
                GvData.DataBind();
                gridDiv.Visible = objMenuOptions.AllowView;
                btnPrint.Visible = (GvData.Rows.Count > 0 ? true : false);

            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void GvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DataSet dtInfo = new DataSet();
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdUniqueId.Value = GvData.DataKeys[gvrow.RowIndex].Value.ToString();
                dtInfo = objLtmsService.GetLotteryClaimEntryDtlByReqId(Convert.ToInt64(hdUniqueId.Value));
                if (e.CommandName == "PrintEntry")
                {
                    if (dtInfo.Tables[0].Rows.Count > 0)
                    {
                        Session["ApplicationId"] = dtInfo.Tables[0].Rows[0]["ReqCode"].ToString();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('TrxLotteryClaimEntryPrint.aspx','_blank')", true);
                    }
                }
                dtInfo.Dispose();
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
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
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 3) { Status = "Pending"; }                    
                    else if (StatusVal == 5) { Status = "Final Leval Approved"; }
                    else if (StatusVal == 6) { Status = "Final Leval Reject"; }
                    lblStatus.Text = Status;
                    if (StatusVal == 5 || StatusVal >= 6)
                    {
                        ((CheckBox)e.Row.FindControl("chkSelect")).Visible = false;
                    }
                    Label lblClaimType = ((Label)e.Row.FindControl("lblClaimType"));
                    Dictionary<String, String> objClaimType = objValidateData.ClaimType();
                    lblClaimType.Text = objClaimType[lblClaimType.Text];

                }
                catch { }

                //((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = objMenuOptions.AllowEdit;
                //((ImageButton)e.Row.FindControl("imgDeleteEntry")).Visible = objMenuOptions.AllowDelete;
                
                
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