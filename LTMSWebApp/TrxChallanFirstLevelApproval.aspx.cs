using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class TrxChallanFirstLevelApproval : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsChallan objChallan = new ClsChallan();
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
        //Populate the dropdownlist for Brand, category and company from the database 
        private void FillComboBox()
        {
            try
            {
                ddlStatus.Items.Clear();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-62"));
                items.Add(new ListItem("Pending", "42"));
                items.Add(new ListItem("Approved", "43"));
                items.Add(new ListItem("Rejected", "44"));
                ddlStatus.Items.AddRange(items.ToArray());
                ddlStatus.SelectedIndex = 1;
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }

        
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string ReqId = "";
                string Status = "";
                DataTable dtInfo = new DataTable();
                if (IsValidChallanSelection() == false) { return; }
                foreach (GridViewRow row in gvChallan.Rows)
                {
                    CheckBox check = row.FindControl("chkSelect") as CheckBox;
                    if (check.Checked)
                    {
                        ReqId = ReqId + gvChallan.DataKeys[row.RowIndex].Value.ToString() + ",";
                    }
                }
                ReqId = ReqId.TrimEnd(',');

                if (((Button)sender).CommandName == "Approve")
                {
                    Status = "Approved";
                    objChallan.ChallanStatus = 2;
                    objChallan.SaveStatus = 43;    
                }
                objChallan.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                objChallan.IpAdd = Request.UserHostAddress;
                objChallan.RequisitionId = Convert.ToInt64(hdUniqueId.Value);
                bool IsUpdated = objLtmsService.UpdateApprovalInRequisitionChallan(objChallan, ReqId);
                if (IsUpdated == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Selected information " + Status + " successfully.');", true);
                }
                BindChallanData(objChallan.RequisitionId);
                //btnCancel_Click(sender, e);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        private bool IsValidChallanSelection()
        {
            bool IsSelected = false;
            foreach (GridViewRow row in gvChallan.Rows)
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
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select atleast one challan from the list');", true);
                return false;
            }


            return true;
        }
        //Checking for valid data entry 
       
        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (objValidateData.isValidDate(txtFromDate.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid date for Deposit date.');", true);
                txtFromDate.Focus();
                return;
            }
            if (objValidateData.isValidDate(txtToDate.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid date for Deposit date.');", true);
                txtToDate.Focus();
                return;
            }


            BindGvData();
        }
        private void BindGvData()
        {
            try
            {
                clsInputParameter objInputParameter = new clsInputParameter();
                objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim() + " 00:00:00.000");
                objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59.999");
                objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);
                GvData.DataSource = objLtmsService.GetRequisitionDtlByStatus(objInputParameter);
                GvData.DataBind();
                GvData.Columns[0].Visible = objMenuOptions.AllowEdit;
                gridDiv.Visible = objMenuOptions.AllowView;
                btnPrint.Visible = (GvData.Rows.Count > 0 ? true : false);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        private void BindChallanData(Int64 RequisitionId)
        {
            try
            {
                gvChallan.DataSource = null;
                DataTable dtInfo = new DataTable();
                dtInfo = objLtmsService.GetRequisitionChallanDtlById(Convert.ToInt64(RequisitionId));
                if (dtInfo.Rows.Count > 0)
                {
                    gvChallan.DataSource = dtInfo;
                }
                gvChallan.DataBind();
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
        protected void GvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {                
               
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdUniqueId.Value = GvData.DataKeys[gvrow.RowIndex].Value.ToString();
                // fill the Location information for edit
                if (e.CommandName == "EditEntry")
                {
                    BindChallanData(Convert.ToInt64(hdUniqueId.Value));                   
                    dvChallanDtl.Visible = true;
                    pnlDataDisplay.Visible = false;
                }
                
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
                    ((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = objMenuOptions.AllowEdit;
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                   // Label lblTicketUnSoldUploadStatus = ((Label)e.Row.FindControl("lblTicketUnSoldUploadStatus"));                    
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 33) { Status = "Pending"; }
                    else if (StatusVal == 41 ) { Status = "Draft"; }
                    else if (StatusVal >= 42 ) { Status = "Confirm"; }
                    lblStatus.Text = Status;

                    if (StatusVal >= 42)
                    {
                        ((ImageButton)e.Row.FindControl("imgEditEntry")).ImageUrl = "~/Content/Images/Search.png";
                    }
                }
                catch { }
            }
        }
        protected void gvChallan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 1) { Status = "Pending"; }
                    else if (StatusVal == 2) { Status = "Approved"; }
                    else if (StatusVal == 3) { Status = "Rejected"; }
                    lblStatus.Text = Status;
                    if (StatusVal == 0 || StatusVal >=2)
                    {
                        ((CheckBox)e.Row.FindControl("chkSelect")).Visible = false;
                    }
                }
                catch { }
            }           
        }
        
        
        protected void btnBack_Click(object sender, EventArgs e)
        {
            dvChallanDtl.Visible = false;
            pnlDataDisplay.Visible = true;
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            clsInputParameter objInputParameter = new clsInputParameter();
            objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim());
            objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim());
            objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);
            objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
            Session["InputParameter"] = objInputParameter;
            Session["ReportName"] = "TicketInventoryViewDtl";
            Response.Redirect("rptViewAppReport.aspx");
        }
    }
}