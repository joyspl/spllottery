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
    public partial class TrxVariableClaimFinalLevelApproval : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsLottery objClsLottery = new ClsLottery();
        ClsRequisition objRequisition = new ClsRequisition();
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            
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
                items.Add(new ListItem("<<--All-->>", "-180"));
                items.Add(new ListItem("Pending", "84"));
                items.Add(new ListItem("Final Approval", "86"));
                items.Add(new ListItem("Rejected", "87"));
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string ReqId = "";
                string Status = "";
                DataTable dtInfo = new DataTable();
                if (IsValidEntry() == false) { return; }
                foreach (GridViewRow row in gvClaimUploadDtl.Rows)
                {
                    CheckBox check = row.FindControl("chkSelect") as CheckBox;
                    if (check.Checked)
                    {
                        ReqId = ReqId + gvClaimUploadDtl.DataKeys[row.RowIndex].Value.ToString() + ",";
                    }
                }
                ReqId = ReqId.TrimEnd(',');

                if (((Button)sender).CommandName == "Approve")
                {
                    Status = "Approved";
                    objRequisition.SaveStatus = 86;
                    objRequisition.UploadStatus = 6;
                }
                else if (((Button)sender).CommandName == "Reject")
                {
                    Status = "Rejected";
                    objRequisition.SaveStatus = 87;
                    objRequisition.UploadStatus = 7;
                }
                objRequisition.DataUniqueId = hdUniqueId.Value;
                objRequisition.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                objRequisition.IpAdd = Request.UserHostAddress;
                bool IsUpdated = objLtmsService.UpdateApprovalInRequisitionUnClaimTicket(objRequisition, ReqId);
                if (IsUpdated == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Selected information " + Status + " successfully.');", true);
                }
                BindGvData();
                btnCancel_Click(sender, e);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }

        //Checking for valid data entry 
        private bool IsValidEntry()
        {
            bool IsSelected = false;
            foreach (GridViewRow row in gvClaimUploadDtl.Rows)
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
                GvData.Columns[1].Visible = objMenuOptions.AllowDelete;
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
        private void BindClaimGvData()
        {
            try
            {
                gvClaimUploadDtl.DataSource = objLtmsService.GetVariableClaimVoucherDtlById(Convert.ToInt64(hdUniqueId.Value));
                gvClaimUploadDtl.DataBind();
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
                int SaveStatus = 0;
                DataTable dtInfo = new DataTable();
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdUniqueId.Value = GvData.DataKeys[gvrow.RowIndex].Value.ToString();
                // fill the Location information for edit
                if (e.CommandName == "EditEntry")
                {
                    objValidateData.ClearAllInputField(pnlDataEntry);
                    dtInfo = objLtmsService.GetRequisitionDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Rows.Count > 0)
                    {
                        txtReqCode.Text = dtInfo.Rows[0]["ReqCode"].ToString();
                        txtReqDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["ReqDate"]).ToString("dd-MMM-yyyy");
                        txtDrawNo.Text = dtInfo.Rows[0]["DrawNo"].ToString();
                        txtDrawDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"]).ToString("dd-MMM-yyyy");
                        txtLotteryType.Text = dtInfo.Rows[0]["LotteryType"].ToString();
                        txtLotteryName.Text = dtInfo.Rows[0]["LotteryName"].ToString();
                        txtDrawNo.Text = dtInfo.Rows[0]["DrawNo"].ToString();
                        BindClaimGvData();
                        
                    }                   
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
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

                    ((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = objMenuOptions.AllowEdit;
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));                  
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal > 1) { Status = "Pending"; }
                    else if (StatusVal == 0) { Status = "Aproved"; }
                    lblStatus.Text = Status;
                    
                }
                catch { }
            }
        }
        protected void gvClaimUploadDtl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string DataUniqueId = gvClaimUploadDtl.DataKeys[gvrow.RowIndex].Value.ToString();
                if (e.CommandName == "PrintEntry")
                {
                    clsInputParameter objInputParameter = new clsInputParameter();
                    objInputParameter.DataUniqueId = Convert.ToInt64(DataUniqueId);
                    objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
                    Session["InputParameter"] = objInputParameter;
                    Session["ReportName"] = "VariableClaimPrize";
                    Response.Redirect("rptViewAppReport.aspx");

                }
                else if (e.CommandName == "DeleteEntry")
                {
                    bool isDeleted = objLtmsService.DeleteInVariableClaimByVoucherId(Convert.ToInt64(DataUniqueId));
                    if (isDeleted == true)
                    {
                        BindClaimGvData();
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Claim information deleted.');", true);                       
                    }

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
        protected void gvClaimUploadDtl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {

                   
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                    int StatusVal = Convert.ToInt16(lblStatus.Text);                   
                    Dictionary<String, String> objClaimType = objValidateData.ClaimUploadStatus();
                    lblStatus.Text = objClaimType[lblStatus.Text];
                    ((CheckBox)e.Row.FindControl("chkSelect")).Visible = (StatusVal == 4? true : false);    
                }
                catch { }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
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