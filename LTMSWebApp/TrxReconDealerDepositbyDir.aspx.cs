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
    public partial class TrxReconDealerDepositbyDir : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsDealerDeposit objDealerDeposit = new ClsDealerDeposit();
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
                    // btnAddNew.Visible = objMenuOptions.AllowEntry;
                }
            }
        }
        //Populate the dropdownlist for Brand, category and company from the database 
        private void FillComboBox()
        {
            try
            {
                objValidateData.FillDropDownList(ddlDepositMethod, "DepositMethod","");
                ddlStatus.Items.Clear();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-1"));
                items.Add(new ListItem("Pending", "1"));
                items.Add(new ListItem("Realized", "2"));
                items.Add(new ListItem("Not-Realized", "3"));
                items.Add(new ListItem("Approve", "4"));
                items.Add(new ListItem("Reject", "5"));
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
                DataTable dtInfo = new DataTable();
                if (IsValidEntry() == false) { return; }//Check for valid data entry
                objDealerDeposit.BGValidityDay = (rdoReconStatus1.Checked == true ? 2 : 3);
                objDealerDeposit.ReconRemarks =txtReconRemarks.Text.Trim();                
                objDealerDeposit.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId; 
                objDealerDeposit.IpAdd = Request.UserHostAddress;
                if (hdUniqueId.Value.ToString().Trim() != "")
                {                    
                    objDealerDeposit.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                    bool IsUpdated = objLtmsService.UpdateReconInDealerDeposit(objDealerDeposit);
                    if (IsUpdated == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Information updated successfully.');", true);
                    }
                }
                BindGvData();
                btnCancel_Click(sender, e);
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
            if (objValidateData.isValidDate(txtDepositDate.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid date for Deposit date.');", true);
                txtDepositDate.Focus();
                return false;
            }

            if (objValidateData.IsDouble(txtDepositAmount.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Deposit Amount should be numeric.');", true);
                txtDepositAmount.Focus();
                return false;
            }
            if (txtDepositId.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Deposit Id.');", true);
                txtDepositId.Focus();
                return false;
            }

            if (ddlDepositMethod.SelectedIndex==0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Deposit Method.');", true);
                ddlDepositMethod.Focus();
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

                GvData.DataSource = objLtmsService.GetDealerDepositViewForApprovalDtl(objInputParameter, 1);
                GvData.DataBind();
                GvData.Columns[0].Visible = objMenuOptions.AllowEdit;
                //GvData.Columns[1].Visible = objMenuOptions.AllowDelete;
                //gridDiv.Visible = objMenuOptions.AllowView;
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
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
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
                
                DataTable dtInfo = new DataTable();
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdUniqueId.Value = GvData.DataKeys[gvrow.RowIndex].Value.ToString();
                // fill the Location information for edit
                if (e.CommandName == "EditEntry")
                {
                    objValidateData.ClearAllInputField(pnlDataEntry);
                    rdoReconStatus1.Checked = true;
                    dtInfo = objLtmsService.GetDealerDepositDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Rows.Count > 0)
                    {
                        txtDepositDate.Text =Convert.ToDateTime(dtInfo.Rows[0]["DepositDate"]).ToString("dd-MMM-yyyy");
                        txtDepositAmount.Text = dtInfo.Rows[0]["DepositAmount"].ToString();
                        txtDepositId.Text = dtInfo.Rows[0]["DepositId"].ToString().ToUpper();
                        ddlDepositMethod.SelectedValue = dtInfo.Rows[0]["DepositMethodId"].ToString().ToUpper();
                        txtRemarks.Text = dtInfo.Rows[0]["Remarks"].ToString().ToUpper();
                        txtReconRemarks.Text = dtInfo.Rows[0]["ReconRemarks"].ToString().ToUpper();
                    }
                    dtInfo.Dispose();
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                    btnSave.Text = "Update";
                   // lblSubHead.Text = "Update Location Information";
                    txtReconRemarks.Focus();
                }
                
                dtInfo.Dispose();
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
                    ((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = objMenuOptions.AllowEdit;
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 1) { Status = "Pending"; }
                    else if (StatusVal == 2) { Status = "Realized"; }
                    else if (StatusVal == 3) { Status = "Not-Realized"; }
                    else if (StatusVal == 4) { Status = "Approved"; }
                    else if (StatusVal == 5) { Status = "Reject"; }
                    lblStatus.Text = Status;
                    if (StatusVal > 1)
                    {
                        ((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = false;
                    }
                }
                catch { }
               
                //((ImageButton)e.Row.FindControl("imgDeleteEntry")).Visible = objMenuOptions.AllowDelete;
                
                
            }
        }

        //protected void btnAddNew_Click(object sender, EventArgs e)
        //{
        //    objValidateData.ClearAllInputField(pnlDataEntry);
        //    pnlDataEntry.Visible = true;
        //    pnlDataDisplay.Visible = false;
        //    btnSave.Text = "Save";
        // 
        // 
        //    hdUniqueId.Value = null;
        //    txtDepositDate.Text = "";
        //}
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
            pnlDataDisplay.Visible = true;
           
           
        }
        
        protected void btnPrint_Click(object sender, EventArgs e)
        {
           clsInputParameter objInputParameter = new clsInputParameter();
                objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim()+" 00:00:00.000");
                objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim() + " 23:59:59.999");
                objInputParameter.InStatus = Convert.ToInt16(ddlStatus.SelectedValue);
            objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
            Session["InputParameter"] = objInputParameter;
            Session["ReportName"] = "DealerDepositReconByDirViewDtl";
            Response.Redirect("rptViewAppReport.aspx");
        }
    }
}