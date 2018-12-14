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
    public partial class TrxTicketInventoryChallan : System.Web.UI.Page
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
                items.Add(new ListItem("<<--All-->>", "-60"));
                items.Add(new ListItem("Pending", "33"));
                items.Add(new ListItem("Draft", "41"));
                items.Add(new ListItem("Confirm", "42"));
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
                if (IsValidEntry() == false) { return; }//Check for valid data entry
                string TransactionNo = "";
                objChallan.RequisitionId =Convert.ToInt32(hdUniqueId.Value);
                objChallan.TransporterName = txtTransporterName.Text.Trim();
                objChallan.ConsignmentNo = txtConsignmentNo.Text.Trim();
                objChallan.CustomerOrderNo = txtCustomerOrderNo.Text.Trim();
                objChallan.CustomerOrdeDate =Convert.ToDateTime(txtCustomerOrdeDate.Text.Trim());
                objChallan.VehicleNo = txtVehicleNo.Text.Trim();
                objChallan.Subject = txtSubject.Text.Trim();
                objChallan.SACCode = txtSACCode.Text.Trim();
                objChallan.No = txtNo.Text.Trim();
                objChallan.DeliveredQuantity =Convert.ToInt32(txtDeliveredQuantity.Text.Trim());
                objChallan.NoOfBoxBundle = Convert.ToInt32(txtNoOfBoxBundle.Text.Trim());
                objChallan.Remarks = txtRemarks.Text.Trim();
                objChallan.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                objChallan.IpAdd = Request.UserHostAddress;
                if (((Button)sender).CommandName == "Save")
                {
                    objChallan.SaveStatus = 41;
                    objChallan.ChallanStatus = 0;
                    bool IsUpdated = objLtmsService.InserInRequisitionChallan(objChallan, out TransactionNo);
                    if (IsUpdated == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Challan Genarated successfully and channan No is "+ TransactionNo + ".');", true);
                    }
                }
                //else if (((Button)sender).CommandName == "Confirm" && hdUniqueId.Value.ToString().Trim() != "")
                //{
                //    objChallan.SaveStatus = 42;                              
                //    bool IsUpdated = objLtmsService.UpdateInRequisitionChallan(objChallan);                   
                //    if (((Button)sender).CommandName == "Confirm" && IsUpdated == true)
                //    {
                //        string UserId = "", UserEmailId = ""; ;
                //        DataTable dtInfo = objLtmsService.GetRequisitionDtlById(Convert.ToInt64(hdUniqueId.Value));
                //        if (dtInfo.Rows.Count > 0)
                //        {
                //            UserId = dtInfo.Rows[0]["CreatedBy"].ToString();
                //            DataTable dtUser = objLtmsService.GetEmailToDtlByType("CHALLAN");
                //            if (dtUser.Rows.Count > 0)
                //            {

                //                string emailOtpSendStatus = "";
                //                string MailSubject = "Challan Generated For Requisition " + dtInfo.Rows[0]["ReqCode"].ToString();
                //                StringBuilder strMessage = new StringBuilder();
                //                strMessage.AppendLine("<table id='tblOTP' cellpadding='0' cellspacing='0' border='0' width='650px'>");
                //                strMessage.AppendLine("     <tr><td><b>Challan No. " + dtInfo.Rows[0]["ChallanNo"].ToString() + " and Challan Dated " + Convert.ToDateTime(dtInfo.Rows[0]["ChallanDate"]).ToString("dd-MMM-yyyy") + " Generated</b></td></tr>");
                //                strMessage.AppendLine("     <tr><td><b>Against Requisition No. " + dtInfo.Rows[0]["ReqCode"].ToString() + " with " + dtInfo.Rows[0]["Qty"].ToString() + " Quantity</b></td></tr>");
                //                strMessage.AppendLine("</table>");

                //                for (int iCnt = 0; iCnt < dtUser.Rows.Count; iCnt++)
                //                {
                //                    UserEmailId = dtUser.Rows[iCnt]["EmailId"].ToString();
                //                    emailOtpSendStatus = objValidateData.SendEmail(UserEmailId, MailSubject, strMessage.ToString());
                //                }

                //                if (emailOtpSendStatus == "SUCCESSFULL")
                //                {
                //                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Challan Genarated and Email successfully sent to distributor.');", true);
                //                }
                //                else if (emailOtpSendStatus == "UNSUCCESSFULL")
                //                {
                //                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Challan Genarated successfully and Email not sent to distributor due to some error.');", true);
                //                }
                //            }
                //        }

                //    }
                //}
              

                BindGvData();
                btnCancel_Click(sender, e);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance. The error is as below " + Ex.Message + "");
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
                    objChallan.ChallanStatus = 1;
                    objChallan.SaveStatus = 42;    
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
        private bool IsValidEntry()
        {
                      
            if (objValidateData.IsIntegerWithZero(txtDrawNo.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Draw No number should be numeric.');", true);
                txtDrawNo.Focus();
                return false;
            }
            if (objValidateData.isValidDate(txtDrawDate.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid draw date');", true);
                txtDrawDate.Focus();
                return false;
            }
            if (txtTransporterName.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Transporter Name.');", true);
                txtTransporterName.Focus();
                return false;
            }
            if (txtConsignmentNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Consignment No.');", true);
                txtConsignmentNo.Focus();
                return false;
            }
            if (txtCustomerOrderNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Customer Order No.');", true);
                txtCustomerOrderNo.Focus();
                return false;
            }
            if (objValidateData.isValidDate(txtCustomerOrdeDate.Text.Trim())==false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid Customer Orde Date.');", true);
                txtCustomerOrdeDate.Focus();
                return false;
            }
            if (txtVehicleNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Customer Vehicle No.');", true);
                txtVehicleNo.Focus();
                return false;
            }
            if (txtSubject.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Customer Subject.');", true);
                txtSubject.Focus();
                return false;
            }
            if (txtSACCode.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter SAC Code.');", true);
                txtSACCode.Focus();
                return false;
            }
            if (txtNo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter No.');", true);
                txtNo.Focus();
                return false;
            }
            if (objValidateData.IsIntegerWithZero(txtDeliveredQuantity.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid Delivered Quantity.');", true);
                txtDeliveredQuantity.Focus();
                return false;
            }
            if (objValidateData.IsIntegerWithZero(txtNoOfBoxBundle.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid No Of Box Bundle.');", true);
                txtNoOfBoxBundle.Focus();
                return false;
            }
            if (txtRemarks.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Remarks.');", true);
                txtRemarks.Focus();
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
                    pnlDataEntry.Visible = false;
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

        protected void gvChallan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                DataTable dtInfo = new DataTable();
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                hdChallanId.Value = gvChallan.DataKeys[gvrow.RowIndex].Value.ToString();
                // fill the Location information for edit
                if (e.CommandName == "DeleteEntry")
                {

                    if (objMenuOptions.AllowDelete == false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('You do not have proper privilege for deleting a record.');", true);
                        return;
                    }
                    bool isDeleted = objLtmsService.DeleteInChallanById(Convert.ToInt64(hdChallanId.Value));
                    if (isDeleted == true)
                    {
                        BindChallanData(Convert.ToInt64(hdUniqueId.Value));  
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Challan information deleted.');", true);
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
        protected void gvChallan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 0) { Status = "Draft"; }
                    else if (StatusVal == 1) { Status = "Confirmed"; }
                    lblStatus.Text = Status;
                    if (StatusVal == 1)
                    {
                        ((CheckBox)e.Row.FindControl("chkSelect")).Visible = false;
                        ((ImageButton)e.Row.FindControl("imgDeleteEntry")).Visible = false;
                    }
                }
                catch { }
            }           
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            DataTable dtInfo = new DataTable();
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
            }
            dtInfo.Dispose();
            pnlDataEntry.Visible = true;
            dvChallanDtl.Visible = false;
            pnlDataDisplay.Visible = false;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
            dvChallanDtl.Visible = false;
            pnlDataDisplay.Visible = true;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
            dvChallanDtl.Visible = true;
            pnlDataDisplay.Visible = false;
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