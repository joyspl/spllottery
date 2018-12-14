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
    public partial class TrxTicketTransaction : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsLottery objClsLottery = new ClsLottery();
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnConfirm);
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
                items.Add(new ListItem("<<--All-->>", "-24"));
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

                return;
                DataTable dtInfo = new DataTable();
                DataTable Exceldt = new DataTable();
               
               
               
                objClsLottery.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                objClsLottery.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                objClsLottery.IpAdd = Request.UserHostAddress;
                if (hdUniqueId.Value.ToString().Trim() != "")
                {

                    //bool IsAdded = objLtmsService.InserInTicketInventoryClaimed(objClsLottery, dtTicketNumber);
                    //if (IsAdded == true)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Claimed Ticked information Uploaded successfully.');", true);
                    //}
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
        private bool IsValidEntry(DataTable dtExcelData, Int64 DatauNiqueId)
        {
            string ErrorMsg = "";
            Int32 TotError = 0;
            StringBuilder strMsg=new StringBuilder();
            ClsTicketInventory objGeneratedNo = new ClsTicketInventory();
            DataTable dtLotteryInfo = new DataTable();
            dtLotteryInfo = objLtmsService.GetRequisitionDtlById(DatauNiqueId);
            if (dtLotteryInfo.Rows.Count > 0)
            {
                objGeneratedNo.DataUniqueId = Convert.ToInt64(dtLotteryInfo.Rows[0]["DataUniqueId"].ToString());
                objGeneratedNo.DrawNo = Convert.ToInt16(dtLotteryInfo.Rows[0]["DrawNo"].ToString());
                objGeneratedNo.DrawDate = Convert.ToDateTime(dtLotteryInfo.Rows[0]["DrawDate"].ToString());
                objGeneratedNo.FnStart = Convert.ToInt16(dtLotteryInfo.Rows[0]["FnStart"].ToString());
                objGeneratedNo.FnEnd = Convert.ToInt16(dtLotteryInfo.Rows[0]["FnEnd"].ToString());
                objGeneratedNo.AlphabetSeries = dtLotteryInfo.Rows[0]["AlphabetSeries"].ToString();
                objGeneratedNo.TnStart = Convert.ToInt64(dtLotteryInfo.Rows[0]["TnStart"].ToString());
                objGeneratedNo.TnEnd = Convert.ToInt64(dtLotteryInfo.Rows[0]["TnEnd"].ToString());
                for (Int16 colCnt = 0; colCnt < dtExcelData.Columns.Count; colCnt++)
                {
                    ErrorMsg = objValidateData.ValidateTicketNo(objGeneratedNo, dtExcelData.Columns[colCnt].ToString());
                    if (ErrorMsg.Trim().Length > 0)
                    {
                        TotError++;                        
                        strMsg.AppendLine("Column " + (colCnt + 1) + " and Row No 1:" + ErrorMsg);
                    }
                    
                    for (Int32 iCnt = 0; iCnt < dtExcelData.Rows.Count; iCnt++)
                    {
                        ErrorMsg = objValidateData.ValidateTicketNo(objGeneratedNo, dtExcelData.Rows[iCnt][colCnt].ToString());
                        if (ErrorMsg.Trim().Length > 0)
                        {
                            TotError++;
                            if (TotError <= 10)
                            {
                                strMsg.AppendLine("Column " + (colCnt + 1) + " and Row No " + (iCnt + 1) + ": " + ErrorMsg);
                            }
                            if (TotError > 10) { break; };

                        }
                    }
                }
            }
            if (strMsg.ToString().Trim().Length > 0) {
                if (TotError > 10)
                {
                    ErrorMsg = "There are more than 10 Error in the file  first 10 error are as below " + strMsg.ToString();                   
                }
                else {
                    ErrorMsg="There are " + TotError + " Error in the file as below " + strMsg.ToString();
                }
                var message = new JavaScriptSerializer().Serialize(ErrorMsg);
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                return false;
            }
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
                //GvData.Columns[1].Visible = objMenuOptions.AllowDelete;
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
                    dtInfo = objLtmsService.GetRequisitionTransactionDtlById(Convert.ToInt64(hdUniqueId.Value));
                    if (dtInfo.Rows.Count > 0)
                    {
                        txtDrawNo.Text = dtInfo.Rows[0]["DrawNo"].ToString();
                        txtDrawDate.Text = Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"]).ToString("dd-MMM-yyyy");
                        txtLotteryType.Text = dtInfo.Rows[0]["LotteryType"].ToString();
                        txtLotteryName.Text = dtInfo.Rows[0]["LotteryName"].ToString();
                        txtDrawNo.Text = dtInfo.Rows[0]["DrawNo"].ToString();


                        txtQuantity.Text = dtInfo.Rows[0]["Qty"].ToString();
                        txtAmount.Text = dtInfo.Rows[0]["TotAmount"].ToString();
                        txtClaimQuantity.Text = dtInfo.Rows[0]["ClaimedCount"].ToString();
                        txtClaimAmount.Text = (Convert.ToDouble(dtInfo.Rows[0]["ClaimedCountAmount"])).ToString();
                        txtUnSoldQty.Text = dtInfo.Rows[0]["CountOfUnSoldTicket"].ToString();
                        txtUnSoldAmount.Text = (Convert.ToDouble(dtInfo.Rows[0]["AmountOfUnSoldTicket"])).ToString();
                        txtDealerAdjustAmount.Text = (Convert.ToDouble(txtClaimAmount.Text) + Convert.ToDouble(txtUnSoldAmount.Text)).ToString("0.00");

                        btnConfirm.Visible = true;
                    }
                    
                    
                    dtInfo.Dispose();
                    pnlDataEntry.Visible = true;
                    pnlDataDisplay.Visible = false;
                }
                if (e.CommandName == "PrintEntry")
                {
                    clsInputParameter objInputParameter = new clsInputParameter();
                    objInputParameter.DataUniqueId = Convert.ToInt64(hdUniqueId.Value);
                    objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
                    Session["InputParameter"] = objInputParameter;
                    Session["ReportName"] = "UnsoldPrize";
                    Response.Redirect("rptViewAppReport.aspx");

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
                    //((ImageButton)e.Row.FindControl("imgDeleteEntry")).Visible = objMenuOptions.AllowDelete;
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 10) { Status = "Approved"; }
                    //else if (StatusVal == 1) { Status = "Ticket Genrated"; }
                    //else if (StatusVal == 2) { Status = "Un- Claimed Ticket Uploaded"; }
                    lblStatus.Text = Status;
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