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
    public partial class TrxRequisition : System.Web.UI.Page
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
                    FillComboBox();
                    BindGvData();
                    btnAddNew.Visible = objMenuOptions.AllowEntry;
                }
            }
           
        }
        //Populate the dropdownlist for Brand, category and company from the database 
        private void FillComboBox()
        {
            try
            {
                objValidateData.FillDropDownList(ddlLotteryType, "LotteryType","");
                ddlStatus.Items.Clear();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-10"));
                items.Add(new ListItem("Draft", "0"));
                items.Add(new ListItem("Confirm", "1"));
                items.Add(new ListItem("Approve", "2"));
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
            long slabLimit = default(long);
            try
            {
                try
                {
                    long.TryParse(txtSlabLimit.Text.Trim(), out slabLimit);
                }
                catch { }
                string ReqCode="";
                DataTable dtInfo = new DataTable();
               
                if (IsValidEntry() == false) { return; }//Check for valid data entry
                objRequisition.ReqCode = txtReqCode.Text;
                objRequisition.DrawDate = Convert.ToDateTime(txtDrawDate.Text.Trim());
                objRequisition.PressDeliveryDate = Convert.ToDateTime(txtDelivaryDate.Text.Trim());                
                objRequisition.GovermentOrderId = Convert.ToInt64(ddlLotteryName.SelectedValue.ToString());
                objRequisition.SlabLimit = slabLimit;
                objRequisition.Qty =Convert.ToInt64(txtQty.Text.Trim());
                objRequisition.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                objRequisition.DrawNo = Convert.ToInt16(txtDrawNo.Text.Trim());
                objRequisition.IpAdd = Request.UserHostAddress;
                if (((Button)sender).CommandName == "Save")
                {
                    objRequisition.SaveStatus = 0;
                }
                else if (((Button)sender).CommandName == "Confirm")
                {
                    objRequisition.SaveStatus = 1;
                }
                if (hdUniqueId.Value.ToString().Trim() == "")
                {                   
                    bool IsAdded = objLtmsService.InsertInRequisition(objRequisition, out ReqCode);
                    if (IsAdded == true)
                    {
                        txtReqCode.Text = ReqCode;
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Requisition information Saved successfully.');", true);
                    }
                }
                else
                {
                    objRequisition.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                    bool IsUpdated = objLtmsService.UpdateInRequisition(objRequisition);
                    if (IsUpdated == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Requisition closed updated successfully.');", true);
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
            if (objValidateData.isValidDate(txtDrawDate.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid date for draw date.');", true);
                txtDrawDate.Focus();
                return false;
            }
            if (objValidateData.isValidDate(txtDelivaryDate.Text) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid date for Press Delivery Date.');", true);
                txtDelivaryDate.Focus();
                return false;
            }
           
            if (objValidateData.isValidDate(txtDrawDate.Text) == true)
            {
                if (Convert.ToDateTime(txtDrawDate.Text.Trim()).Date < DateTime.Now.Date)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Draw date can not be past date.');", true);
                    txtDrawDate.Focus();
                    return false;
                }
            }
            if (objValidateData.isValidDate(txtDelivaryDate.Text) == true)
            {
                if (Convert.ToDateTime(txtDelivaryDate.Text.Trim()).Date < DateTime.Now.Date)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Press Delivary Date can not be past date.');", true);
                    txtDelivaryDate.Focus();
                    return false;
                }
            }
            if (objValidateData.isValidDate(hdLastDrawDate.Value))
            {
                if (Convert.ToDateTime(txtDrawDate.Text) <= Convert.ToDateTime(hdLastDrawDate.Value))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('draw date should be more than the last draw date " + Convert.ToDateTime(hdLastDrawDate.Value).ToString("dd-MMM-yyyy") + ".');", true);
                    txtDrawDate.Focus();
                    return false;
                }
            }
            if (ddlLotteryType.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Lottery Type.');", true);
                ddlLotteryType.Focus();
                return false;
            }
            if (ddlLotteryName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please Select Lottery Name.');", true);
                ddlLotteryName.Focus();
                return false;
            }

            if (objValidateData.IsLongInteger(txtQty.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Quantity should be numeric.');", true);
                txtQty.Focus();
                return false;
            }

            if (objValidateData.IsLongInteger(txtSlabLimit.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Slab Limit should be numeric.');", true);
                txtSlabLimit.Focus();
                return false;
            }
            //if (txtQty.Text.Trim().Length != txtQty.MaxLength)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('No of digit in Quantity should be " + txtQty.MaxLength + "');", true);
            //    txtQty.Focus();
            //    return false;
            //}
            
            
            return true;
        }
        private void BindGvData()
        {
            try
            {
                clsInputParameter objInputParameter = new clsInputParameter();
                objInputParameter.InFromDate = Convert.ToDateTime(txtFromDate.Text.Trim()+" 00:00:00.000");
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
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
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
        protected void GvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int SaveStatus=0;
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
                        SaveStatus = Convert.ToInt16(dtInfo.Rows[0]["SaveStatus"].ToString().ToUpper());
                        txtReqCode.Text = dtInfo.Rows[0]["ReqCode"].ToString();
                        txtReqDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["ReqDate"]).ToString("dd-MMM-yyyy");
                        txtDrawDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"]).ToString("dd-MMM-yyyy");
                        txtDelivaryDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["PressDeliveryDate"]).ToString("dd-MMM-yyyy");                        
                       
                        ddlLotteryType.SelectedValue = dtInfo.Rows[0]["LotteryTypeId"].ToString();
                        txtQty.MaxLength = Convert.ToInt16(dtInfo.Rows[0]["NoOfDigit"].ToString());

                        ddlLotteryType_SelectedIndexChanged(ddlLotteryType, null);
                        if (ddlLotteryName.Items.Count > 0)
                        {
                            txtSizeOfTicket.Text = dtInfo.Rows[0]["SizeOfTicket"].ToString();
                            txtPaperQuality.Text = dtInfo.Rows[0]["PaperQuality"].ToString();

                            ddlLotteryName.SelectedValue = dtInfo.Rows[0]["GovermentOrderId"].ToString();
                            if (SaveStatus == 0)
                            {
                                ddlLotteryName_SelectedIndexChanged(ddlLotteryName, null);
                            }
                        }
                        ddlLotteryType.Enabled = false;
                        ddlLotteryName.Enabled = false;
                        txtDrawNo.ReadOnly = true;
                        txtDrawNo.Text = dtInfo.Rows[0]["DrawNo"].ToString();
                        txtQty.Text = dtInfo.Rows[0]["Qty"].ToString();
                        txtGovermentOrder.Text = dtInfo.Rows[0]["GovermentOrder"].ToString();
                        txtSlabLimit.Text = dtInfo.Rows[0]["SlabLimit"].ToString();
                        btnSave.Visible = true;
                        btnSubmit.Visible = true;
                        if (SaveStatus == 1 || SaveStatus == 2 || SaveStatus == 4 || SaveStatus == 6 || SaveStatus > 7)
                        {
                            btnSave.Visible = false;
                            btnSubmit.Visible = false;
                        }
                    }
                    dtInfo.Dispose();
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                    btnSave.Text = "Update";
                   // lblSubHead.Text = "Update Location Information";
                    ddlLotteryType.Focus();
                }
                //Delete the location information
                if (e.CommandName == "DeleteEntry")
                {
                    if (objMenuOptions.AllowDelete == false)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('You do not have proper privilege for deleting a record.');", true);
                        return;
                    }
                   
                    bool isDeleted = objLtmsService.DeleteInRequisition(Convert.ToInt64(hdUniqueId.Value));
                    if (isDeleted == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Requisition information deleted.');", true);
                        BindGvData();
                      
                    }
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
                   
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 0) { Status = "Draft"; }
                    else if (StatusVal == 1 || StatusVal == 2 || StatusVal == 4 || StatusVal == 6 || StatusVal > 7) { Status = "Confirm"; }
                    else if (StatusVal == 3 || StatusVal == 5 || StatusVal == 7) { Status = "Rejected"; }                                     

                    lblStatus.Text = Status;
                    if (StatusVal == 1 || StatusVal == 2 || StatusVal == 4 || StatusVal == 6 || StatusVal > 7)
                    {
                        ((ImageButton)e.Row.FindControl("imgDeleteEntry")).Visible = false;
                        ((ImageButton)e.Row.FindControl("imgEditEntry")).ImageUrl = "~/Content/Images/Search.png";
                    }
                   
                }
                catch { } 
                
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            objValidateData.ClearAllInputField(pnlDataEntry);
            pnlDataEntry.Visible = true;
            pnlDataDisplay.Visible = false;
            btnSave.Text = "Save";
            btnSave.Visible = true;
            btnSubmit.Visible = false;
            txtReqCode.Text = "REQ/" + (DateTime.Now.Month >= 4 ? DateTime.Now.Year.ToString().Substring(2) + "-" + (Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2)) + 1) + "/XXXXXXX" : (Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2)) - 1) + "-" + DateTime.Now.Year.ToString().Substring(2) + "/XXXXXXX").ToString();
            txtReqDate.Text = Convert.ToDateTime(DateTime.Now).ToString("dd-MMM-yyyy");
            ddlLotteryType.Enabled = true;
            ddlLotteryName.Enabled = true;
            txtDrawNo.ReadOnly = false;
            hdLastDrawDate.Value = "";
            hdUniqueId.Value = null;
           
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
            pnlDataDisplay.Visible = true;         
           
        }
        protected void ddlLotteryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objValidateData.FillDropDownList(ddlLotteryName, "LotteryByApprovedGovOrder", ddlLotteryType.SelectedValue);
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex,  Request.UserHostAddress);
                var message = new JavaScriptSerializer().Serialize("Some Error has occurred while performing your activity. Please contact the System Administrator for further assistance.");
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
            }
        }
        protected void ddlLotteryName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtSizeOfTicket.Text = "";
                txtPaperQuality.Text = "";
                txtDrawNo.Text = "";
                txtDrawNo.ReadOnly = false;
                DataTable dtReqInfo = new DataTable();
                dtReqInfo = objLtmsService.GetLastDrawDateAndNoDtlByLotteryId(Convert.ToInt16(ddlLotteryName.SelectedValue));
                if(dtReqInfo.Rows.Count>0)
                {
                    txtDrawNo.Text = (Convert.ToInt16(dtReqInfo.Rows[0]["DrawNo"].ToString()) + 1).ToString();
                    hdLastDrawDate.Value = Convert.ToDateTime(dtReqInfo.Rows[0]["DrawDate"].ToString()).ToString();
                    txtDrawNo.ReadOnly = true;
                }
                dtReqInfo.Dispose();
                dtReqInfo = objLtmsService.GetGovermentOrderDtlById(Convert.ToInt64(Convert.ToInt16(ddlLotteryName.SelectedValue)));
                if (dtReqInfo.Rows.Count > 0)
                {
                    txtGovermentOrder.Text = dtReqInfo.Rows[0]["GovermentOrder"].ToString();
                    txtSizeOfTicket.Text = dtReqInfo.Rows[0]["SizeOfTicket"].ToString();
                    txtPaperQuality.Text = dtReqInfo.Rows[0]["PaperQuality"].ToString();
                }
                dtReqInfo.Dispose();
            }
            catch (Exception Ex)
            {
                objValidateData.SaveSystemErrorLog(Ex, Request.UserHostAddress);
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
            Session["ReportName"] = "RequisitionViewDtl";
            Response.Redirect("rptViewAppReport.aspx");
        }
    }
}