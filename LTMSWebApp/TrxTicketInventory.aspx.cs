using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class TrxTicketInventory : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsTicketInventory objTicketInventory = new ClsTicketInventory();
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
                objValidateData.FillDropDownList(ddlLotteryType, "LotteryType", "");
                string drpItem = "";                
                for (int iCnt = 65; iCnt < 91; iCnt++)
                {
                    drpItem = ((char)iCnt).ToString();
                    chkLstAlphabetSeries.Items.Add(drpItem);
                }
                for (int iCnt = 65; iCnt < 91; iCnt++)
                {
                    drpItem = ((char)iCnt).ToString() + ((char)iCnt).ToString();
                    chkLstAlphabetSeries.Items.Add(drpItem);
                } 
                ddlStatus.Items.Clear();
                List<ListItem> items = new List<ListItem>();
                items.Add(new ListItem("<<--All-->>", "-30"));
                items.Add(new ListItem("Pending", "6"));
                items.Add(new ListItem("Draft", "11"));
                items.Add(new ListItem("Confirm", "16"));               
                items.Add(new ListItem("Ticket Printed", "17")); 
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
        protected void ddlLotteryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                objValidateData.FillDropDownList(ddlLotteryName, "LotteryByApprovedGovOrder", ddlLotteryType.SelectedValue);
                //objValidateData.FillDropDownList(ddlLotteryName, "LotteryNameByLotteryTypeID", ddlLotteryType.SelectedValue);

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
                string AlphabetSeries = "";
                DataTable dtInfo = new DataTable();
                if (IsValidEntry() == false) { return; }//Check for valid data entry
                objTicketInventory.LotteryId = Convert.ToInt16(ddlLotteryName.SelectedValue.ToString());
                objTicketInventory.FnStart = Convert.ToInt16(txtFnStart.Text);
                objTicketInventory.FnEnd = Convert.ToInt16(txtFnEnd.Text);
                for (int i = 0; i < chkLstAlphabetSeries.Items.Count; i++)
                {
                    if (chkLstAlphabetSeries.Items[i].Selected)
                    {
                        AlphabetSeries = AlphabetSeries + chkLstAlphabetSeries.Items[i].Text + ",";
                    }
                }
                objTicketInventory.AlphabetSeries = AlphabetSeries.TrimEnd(','); 
                objTicketInventory.TnStart = Convert.ToInt64(txtTnStart.Text);
                objTicketInventory.TnEnd = Convert.ToInt64(txtTnEnd.Text);
                objTicketInventory.DrawDate = Convert.ToDateTime(txtDrawDate.Text.Trim());
                objTicketInventory.DrawNo =Convert.ToInt16(txtDrawNo.Text.Trim());
                objTicketInventory.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId; 
                objTicketInventory.IpAdd = Request.UserHostAddress;
                if (((Button)sender).CommandName == "Save")
                {
                    objTicketInventory.SaveStatus = 11;
                }
                else if (((Button)sender).CommandName == "Confirm")
                {
                    objTicketInventory.SaveStatus = 16;
                }
                if (hdUniqueId.Value.ToString().Trim() != "")
                {
                    objTicketInventory.DataUniqueId =Convert.ToInt64(hdUniqueId.Value.ToString().Trim());
                    bool IsUpdated = objLtmsService.UpdateInTicketInventory(objTicketInventory);
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
        //Checking for valid data entry 
        private bool IsValidEntry()
        {
            string AlphabetSeries = "";
            int FnStart = 0, FnEnd = 0;
            Int64 TnStart = 0, TnEnd = 0, NoOfTicketToBeGen = 0;
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
            if (objValidateData.IsIntegerWithZero(txtFnStart.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('First Start number should be numeric.');", true);
                txtFnStart.Focus();
                return false;
            }
            if (objValidateData.IsIntegerWithZero(txtFnEnd.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('First End number should be numeric.');", true);
                txtFnEnd.Focus();
                return false;
            }
            if (Convert.ToInt16(txtFnStart.Text.Trim()) > Convert.ToInt16(txtFnEnd.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('First number should be less than the second no.');", true);
                txtFnStart.Focus();
                return false;
            }

            bool IsSelected = false;
            for (int i = 0; i < chkLstAlphabetSeries.Items.Count; i++)
            {
                if (chkLstAlphabetSeries.Items[i].Selected)
                {
                    IsSelected = true;
                    break;
                }
            }
            if (IsSelected == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please select alteast one Alphabet from the list of Alphabet Series.');", true);
                chkLstAlphabetSeries.Focus();
                return false;
            }


            for (int i = 0; i < chkLstAlphabetSeries.Items.Count; i++)
            {
                if (chkLstAlphabetSeries.Items[i].Selected)
                {
                    AlphabetSeries = AlphabetSeries + chkLstAlphabetSeries.Items[i].Text + ",";
                }
            }
            string[] values = AlphabetSeries.TrimEnd(',').Split(',');
            if (values.Length>0)
            {
                if (values[0].Trim().Length != values[values.Length-1].Trim().Length)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Alphabet should be of same length from the list of Alphabet Series.');", true);
                    return false;
                }
            }
            if (objValidateData.IsIntegerWithZero(txtTnStart.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Second End number should be numeric.');", true);
                txtTnStart.Focus();
                return false;
            }
            if (objValidateData.IsInteger(txtTnEnd.Text.Trim()) == false)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Second End number should be numeric.');", true);
                txtTnEnd.Focus();
                return false;
            }
            if (Convert.ToInt32(txtTnStart.Text.Trim()) > Convert.ToInt32(txtTnEnd.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('First End number should be less than the second End no.');", true);
                txtTnStart.Focus();
                return false;
            }
                       

            FnStart =Convert.ToInt16(txtFnStart.Text.Trim());
            FnEnd = Convert.ToInt16(txtFnEnd.Text.Trim());

            TnStart = Convert.ToInt64(txtTnStart.Text.Trim());
            TnEnd = Convert.ToInt64(txtTnEnd.Text.Trim());
            NoOfTicketToBeGen = ((FnEnd - FnStart) <= 0 ? 1 : ((FnEnd - FnStart) + 1)) * values.Length * ((TnEnd - TnStart) <= 0 ? 1 : ((TnEnd - TnStart) + 1));

            if (NoOfTicketToBeGen != Convert.ToInt64(txtQty.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('You are genrating the ticket " + NoOfTicketToBeGen + ". It should be "+ txtQty.Text +" .');", true);
                txtTnStart.Focus();
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
                        txtReqCode.Text = dtInfo.Rows[0]["ReqCode"].ToString();
                        txtReqDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["ReqDate"]).ToString("dd-MMM-yyyy");
                        SaveStatus = Convert.ToInt16(dtInfo.Rows[0]["SaveStatus"].ToString());                        
                        txtDrawNo.Text = dtInfo.Rows[0]["DrawNo"].ToString();
                        txtDrawDate.Text =Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"]).ToString("dd-MMM-yyyy");
                        ddlLotteryType.SelectedValue = dtInfo.Rows[0]["LotteryTypeId"].ToString();
                        ddlLotteryType_SelectedIndexChanged(ddlLotteryType, null);
                        ddlLotteryName.SelectedValue = dtInfo.Rows[0]["GovermentOrderId"].ToString();
                        txtGovermentOrder.Text = dtInfo.Rows[0]["GovermentOrder"].ToString();  
                        txtQty.Text = dtInfo.Rows[0]["Qty"].ToString();  
                        txtFnStart.Text = dtInfo.Rows[0]["FnStart"].ToString();
                        txtFnEnd.Text = dtInfo.Rows[0]["FnEnd"].ToString();
                        txtSlabLimit.Text = dtInfo.Rows[0]["SlabLimit"].ToString();
                        //AlphabetSeries
                        string AlphabetSeries = dtInfo.Rows[0]["AlphabetSeries"].ToString();
                        string[] values = AlphabetSeries.Split(',');
                        for (int iCnt = 0; iCnt < values.Length; iCnt++)
                        {
                            for (int jCnt = 0; jCnt < chkLstAlphabetSeries.Items.Count; jCnt++)
                            {
                                if (chkLstAlphabetSeries.Items[jCnt].Text == values[iCnt].Trim())
                                {
                                    chkLstAlphabetSeries.Items[jCnt].Selected = true;
                                }
                            }
                        }
                        txtTnStart.Text = dtInfo.Rows[0]["TnStart"].ToString();
                        txtTnEnd.Text = dtInfo.Rows[0]["TnEnd"].ToString();
                                               
                        btnSave.Visible = false;
                        btnConfirm.Visible = false;
                        btnPrinted.Visible = false;
                       
                        if (SaveStatus < 16)
                        {
                            btnSave.Visible = true;                            
                            foreach (System.Web.UI.Control ctrl in pnlDataEntry.Controls)
                            {
                                if (ctrl is TextBox)
                                {
                                    ((TextBox)ctrl).ReadOnly = false;
                                }
                                else if (ctrl is DropDownList)
                                {
                                    ((DropDownList)ctrl).Enabled = true;
                                }
                                else if (ctrl is CheckBoxList)
                                {
                                    ((CheckBoxList)ctrl).Enabled = true;
                                }
                            }
                        }
                        if (SaveStatus == 11) 
                        {
                            btnConfirm.Visible = true;
                        }
                        if (SaveStatus >= 16)
                        {
                            
                            foreach (System.Web.UI.Control ctrl in pnlDataEntry.Controls)
                            {
                                if (ctrl is TextBox)
                                {
                                    ((TextBox)ctrl).ReadOnly = true;
                                }
                                else if (ctrl is DropDownList)
                                {
                                    ((DropDownList)ctrl).Enabled = false;
                                }
                                else if (ctrl is CheckBoxList)
                                {
                                    ((CheckBoxList)ctrl).Enabled = false;
                                }
                            }
                            btnPrinted.Visible = true;
                        }                       
                                
                    }
                    txtReqCode.ReadOnly = true;
                    txtReqDate.ReadOnly = true;
                    txtDrawNo.ReadOnly = true;
                    txtDrawDate.ReadOnly = true;                                          
                    txtGovermentOrder.ReadOnly = true;
                    txtQty.ReadOnly = true;
                    ddlLotteryType.Enabled = false;
                    ddlLotteryName.Enabled = false;
                    dtInfo.Dispose();
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                    btnSave.Text = "Update";
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
                    if (StatusVal == 6) { Status = "Pending"; }
                    else if (StatusVal == 11) { Status = "Draft"; }
                    else if (StatusVal == 16) { Status = "Confirmed"; }
                    else if (StatusVal >= 17) { Status = "Ticket Printed"; }
                    lblStatus.Text = Status;
                    if (StatusVal > 12 && StatusVal<14)
                    {
                        ((ImageButton)e.Row.FindControl("imgEditEntry")).ImageUrl = "~/Content/Images/Search.png";
                    }
                    else if (StatusVal == 14)
                    {
                        ((ImageButton)e.Row.FindControl("imgEditEntry")).ImageUrl = "~/Content/Images/Loading.gif";
                    }
                    else if (StatusVal== 16)
                    {
                        ImageButton imgEditEntry = ((ImageButton)e.Row.FindControl("imgEditEntry"));
                        imgEditEntry.ImageUrl = "~/Content/Images/upload.png";
                        imgEditEntry.Height = 20;
                        imgEditEntry.Width = 20;
                    }
                    else if (StatusVal >=17)
                    {
                        ((ImageButton)e.Row.FindControl("imgEditEntry")).ImageUrl = "~/Content/Images/Apply.png";
                    }

                }
                catch { }
            }
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            int Status = 0;
            string  UserId = ((ClsUserInfo)Session["UserInfo"]).UserId; 
            string IpAdd = Request.UserHostAddress;
            try
            {                       
                if (hdUniqueId.Value.ToString().Trim() != "")
                {
                    if (((Button)sender).CommandName == "Printed")
                    {
                        Status = 17;                        
                    }
                  
                    bool isGenerated = objLtmsService.GenerateLotteryNumberDtlById(Convert.ToInt64(hdUniqueId.Value.ToString().Trim()), Status);
                    if (isGenerated == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Information Saved successfully.');", true);
                    }

                    if (((Button)sender).CommandName == "Printed" && isGenerated == true)
                    {
                        UserId = "";
                        string UserEmailId = ""; 
                        DataTable dtInfo = objLtmsService.GetRequisitionDtlById(Convert.ToInt64(hdUniqueId.Value));
                        if (dtInfo.Rows.Count > 0)
                        {
                            UserId = dtInfo.Rows[0]["CreatedBy"].ToString();
                            DataTable dtUser = objLtmsService.GetEmailToDtlByType("CHALLAN");
                            if (dtUser.Rows.Count > 0)
                            {
                                string emailOtpSendStatus = "";
                                string MailSubject = "Ticket Printed For Requisition " + dtInfo.Rows[0]["ReqCode"].ToString();
                                StringBuilder strMessage = new StringBuilder();
                                strMessage.AppendLine("<table id='tblOTP' cellpadding='0' cellspacing='0' border='0' width='650px'>");
                                strMessage.AppendLine("     <tr style='border:solid 1px;'><td><b>Ticket Printed</b></td></tr>");
                                strMessage.AppendLine("     <tr><td><b>Ticket Printed For Requisition " + dtInfo.Rows[0]["ReqCode"].ToString() + " and  for Qty. " + dtInfo.Rows[0]["Qty"].ToString() + "</b></td></tr>");
                                strMessage.AppendLine("</table>");

                                for (int iCnt = 0; iCnt < dtUser.Rows.Count; iCnt++)
                                {
                                    UserEmailId = dtUser.Rows[iCnt]["EmailId"].ToString();
                                    emailOtpSendStatus = objValidateData.SendEmail(UserEmailId, MailSubject, strMessage.ToString());
                                }

                                if (emailOtpSendStatus == "SUCCESSFULL")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Ticket Printed and Email successfully sent to distributor.');", true);
                                }
                                else if (emailOtpSendStatus == "UNSUCCESSFULL")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Ticket Printed successfully and Email not sent to distributor due to some error.');", true);
                                }
                            }
                        }

                    }

                    BindGvData();
                    btnCancel_Click(sender, e);
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
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDataEntry.Visible = false;
            pnlDataDisplay.Visible = true;
        }        
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            clsInputParameter objInputParameter = new clsInputParameter();
            objInputParameter.InFromDate =Convert.ToDateTime(txtFromDate.Text.Trim());
            objInputParameter.InToDate = Convert.ToDateTime(txtToDate.Text.Trim());
            objInputParameter.InStatus =Convert.ToInt16(ddlStatus.SelectedValue);
            objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
            Session["InputParameter"] = objInputParameter;
            Session["ReportName"] = "TicketInventoryViewDtl";            
            Response.Redirect("rptViewAppReport.aspx");
        }

       
    }
}