using ExcelDataReader;
using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class TrxVariableClaim : System.Web.UI.Page
    {
        ClsMenuInfo objMenuOptions = new ClsMenuInfo();
        ClsLottery objClsLottery = new ClsLottery();
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnUpload);
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
                items.Add(new ListItem("<<--All-->>", "-150"));
                items.Add(new ListItem("Not Uploaded", "63"));
                items.Add(new ListItem("Claimed Ticket Uploaded", "81"));
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

                DataTable dtInfo = new DataTable();
                DataTable Exceldt = new DataTable();
                DataTable dtTicketNumber = new DataTable();
                dtTicketNumber.TableName = "dtTicketNumber";
                dtTicketNumber.Columns.Add(new DataColumn("TicketNumber", typeof(string)));
                dtTicketNumber.Columns.Add(new DataColumn("WinnerId", typeof(string)));
                if (((Button)sender).CommandName == "Upload")
                {
                    if (fileUpldFromExcel.HasFile)
                    {
                        string strFileType = Path.GetExtension(fileUpldFromExcel.FileName).ToLower();
                        string FileContentType = fileUpldFromExcel.PostedFile.ContentType;
                        if (!((FileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || (FileContentType == "application/vnd.ms-excel")))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Only xls,xlsx excel file type allowed.');", true);
                            return;
                        }

                        IExcelDataReader excelReader = null;
                        var excelreaderDS = new DataSet();
                        Stream stream = Request.Files[0].InputStream;
                        if (strFileType.Trim() == ".xls")
                        {
                            excelReader = ExcelReaderFactory.CreateReader(stream);
                        }
                        else if (strFileType.Trim() == ".xlsx")
                        {
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        excelreaderDS = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true

                            }
                        });
                        Exceldt = excelreaderDS.Tables[0];
                        if (IsValidEntry(Exceldt, Convert.ToInt64(hdUniqueId.Value), ref dtTicketNumber) == false) { return; }//Check for valid data entry

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Plese upload xls,xlsx excel file type.');", true);
                        return;
                    }

                }
                objClsLottery.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                objClsLottery.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                objClsLottery.IpAdd = Request.UserHostAddress;
                if (hdUniqueId.Value.ToString().Trim() != "")
                {

                    bool IsAdded = objLtmsService.InserInTicketInventoryClaimed(objClsLottery, dtTicketNumber);
                    if (IsAdded == true)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Claimed Ticked information Uploaded successfully.');", true);
                    }
                }
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
        //Checking for valid data entry 
        private bool IsValidEntry(DataTable dtExcelData, Int64 DatauNiqueId, ref DataTable dtTicketNumber)
        {
            string ErrorMsg = "";
            Int32 TotError = 0;
            Int64 WinnerId = 0;
            //Int32 NoOfRecordSToBeUpload = 60;
            bool ValidationForUnsold = false;
            int errorCntForTkt = 0;
            StringBuilder strMsg = new StringBuilder();
            ClsTicketInventory objGeneratedNo = new ClsTicketInventory();
            DataTable dtLotteryInfo = new DataTable();
            DataTable distinctTable = dtExcelData.DefaultView.ToTable(true);
            if (dtExcelData.Rows.Count != distinctTable.Rows.Count)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Upload can not be complited.Duplicate Ticket No exist in file.');", true);               
                return false;
            }
           

            distinctTable.Clear();
            distinctTable = null;
            DataRow dr = null;
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

                DataSet dtLotteryWiningSerialNoDtl = objLtmsService.GetLotteryWiningSerialNoDtlByLotteryNo(objGeneratedNo.DataUniqueId, "");          
                for (Int16 colCnt = 0; colCnt < dtExcelData.Columns.Count; colCnt++)
                {
                    if (dtExcelData.Columns[colCnt].ToString().Trim().Length > 0)
                    {
                        errorCntForTkt = 0;
                        WinnerId = 0;
                        bool isValidTicketNo = true, IsCommonToAllSeries = false, isPartialTicket = false;
                        ErrorMsg = objValidateData.ValidateTicketNo(objGeneratedNo, dtExcelData.Columns[colCnt].ToString());
                        if (ErrorMsg.Trim().Length > 0)
                        {
                            errorCntForTkt++;
                            TotError++;
                            strMsg.AppendLine("Column " + (colCnt + 1) + " and Row No 1:" + ErrorMsg);
                            isValidTicketNo = false;
                        }
                        if (isValidTicketNo == true)
                        {
                            for (int rwCnt = 0; rwCnt < dtLotteryWiningSerialNoDtl.Tables[1].Rows.Count; rwCnt++)
                            {
                                if (dtExcelData.Columns[colCnt].ToString().ToUpper() == dtLotteryWiningSerialNoDtl.Tables[1].Rows[rwCnt]["WiningSerialNo"].ToString().ToUpper())
                                {
                                    IsCommonToAllSeries = true;                                    
                                }
                                if (dtExcelData.Columns[colCnt].ToString().ToUpper().EndsWith(dtLotteryWiningSerialNoDtl.Tables[1].Rows[rwCnt]["WiningSerialNo"].ToString()) == true)
                                {
                                    isPartialTicket = true;
                                    WinnerId = Convert.ToInt64(dtLotteryWiningSerialNoDtl.Tables[1].Rows[rwCnt]["WinnerId"].ToString());

                                    ValidationForUnsold = (dtLotteryWiningSerialNoDtl.Tables[1].Rows[rwCnt]["ValidationForUnsold"].ToString().ToUpper()=="Y"?true:false);
                                    if (ValidationForUnsold == true)
                                    {
                                        int FnNo = 0;
                                        string Alphabet = "";
                                        Int64 TnNo = 0;
                                        string AlphabetSeries = objGeneratedNo.AlphabetSeries;
                                        string[] values = AlphabetSeries.Split(',');
                                        int AlphabetLen = Convert.ToInt16(values[values.Length - 1].ToString().Length);
                                        FnNo = Convert.ToInt16(((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? "0" : dtExcelData.Columns[colCnt].ToString().ToUpper().Trim().Substring(0, objGeneratedNo.FnEnd.ToString().Length)));
                                        Alphabet = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? dtExcelData.Columns[colCnt].ToString().ToUpper().Trim().Substring(0, AlphabetLen) : dtExcelData.Columns[colCnt].ToString().ToUpper().Trim().Substring(objGeneratedNo.FnEnd.ToString().Length, AlphabetLen));
                                        TnNo = Convert.ToInt64(dtExcelData.Columns[colCnt].ToString().ToUpper().Trim().Substring(dtExcelData.Columns[colCnt].ToString().ToUpper().Trim().Length - objGeneratedNo.TnEnd.ToString().Length, objGeneratedNo.TnEnd.ToString().Length));
                                        DataSet dtLotteryDtl = objLtmsService.GetLotteryDtlInClaimAndUnSold(objGeneratedNo.DataUniqueId, dtExcelData.Columns[colCnt].ToString().ToUpper().Trim().ToUpper(), FnNo, Alphabet, TnNo);
                                        if (dtLotteryDtl != null)
                                        {
                                            if (dtLotteryDtl.Tables[1].Rows.Count > 0)
                                            {
                                                errorCntForTkt++;
                                                TotError++;
                                                strMsg.AppendLine("Column " + (colCnt + 1) + " and Row No 1: The Lottery Ticket " + dtExcelData.Columns[colCnt].ToString().ToUpper() + " is available in unsold ticket");
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (IsCommonToAllSeries == true)
                        {
                            errorCntForTkt++;
                            TotError++;
                            strMsg.AppendLine("Column " + (colCnt + 1) + " and Row No 1: The Lottery Ticket " + dtExcelData.Columns[colCnt].ToString().ToUpper() + " is type PWT So it will not uploded with this portal");
                        }
                        if (isPartialTicket == false)
                        {
                            errorCntForTkt++;
                            TotError++;
                            strMsg.AppendLine("Column " + (colCnt + 1) + " and Row No 1: The Lottery Ticket " + dtExcelData.Columns[colCnt].ToString().ToUpper() + " is Not a Winning prize.");
                        }
                        if (errorCntForTkt == 0 && isPartialTicket==true)
                        {
                            dr = dtTicketNumber.NewRow();
                            dr["TicketNumber"] = dtExcelData.Columns[colCnt].ToString();
                            dr["WinnerId"] = WinnerId.ToString();
                            dtTicketNumber.Rows.Add(dr);                        
                        }
                        for (Int32 iCnt = 0; iCnt < dtExcelData.Rows.Count; iCnt++)
                        {
                            if (dtExcelData.Rows[iCnt][colCnt].ToString().Trim().Length > 0)
                            {   
                                isValidTicketNo = true;
                                IsCommonToAllSeries = false;
                                isPartialTicket = false;
                                errorCntForTkt = 0;
                                WinnerId = 0;
                                ErrorMsg = objValidateData.ValidateTicketNo(objGeneratedNo, dtExcelData.Rows[iCnt][colCnt].ToString());
                                if (ErrorMsg.Trim().Length > 0)
                                {
                                    errorCntForTkt++;
                                    TotError++;
                                    if (TotError <= 10)
                                    {
                                        isValidTicketNo = false;
                                        strMsg.AppendLine("Column " + (colCnt + 1) + " and Row No " + (iCnt + 1) + ": " + ErrorMsg);
                                    }
                                    if (TotError > 10) { break; };

                                }
                                if (isValidTicketNo == true)
                                {
                                    for (int rwCnt = 0; rwCnt < dtLotteryWiningSerialNoDtl.Tables[1].Rows.Count; rwCnt++)
                                    {
                                        if (dtExcelData.Rows[iCnt][colCnt].ToString().ToUpper() == dtLotteryWiningSerialNoDtl.Tables[1].Rows[rwCnt]["WiningSerialNo"].ToString().ToUpper())
                                        {
                                            IsCommonToAllSeries = true;
                                        }
                                        if (dtExcelData.Rows[iCnt][colCnt].ToString().ToUpper().EndsWith(dtLotteryWiningSerialNoDtl.Tables[1].Rows[rwCnt]["WiningSerialNo"].ToString()) == true)
                                        {
                                            WinnerId = Convert.ToInt64(dtLotteryWiningSerialNoDtl.Tables[1].Rows[rwCnt]["WinnerId"].ToString());
                                            isPartialTicket = true;
                                             ValidationForUnsold = (dtLotteryWiningSerialNoDtl.Tables[1].Rows[rwCnt]["ValidationForUnsold"].ToString().ToUpper()=="Y"?true:false);
                                             if (ValidationForUnsold == true)
                                             {
                                                 int FnNo = 0;
                                                 string Alphabet = "";
                                                 Int64 TnNo = 0;


                                                 string AlphabetSeries = objGeneratedNo.AlphabetSeries;
                                                 string[] values = AlphabetSeries.Split(',');
                                                 int AlphabetLen = Convert.ToInt16(values[values.Length - 1].ToString().Length);

                                                 FnNo = Convert.ToInt16(((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? "0" : dtExcelData.Rows[iCnt][colCnt].ToString().ToUpper().Trim().Substring(0, objGeneratedNo.FnEnd.ToString().Length)));
                                                 Alphabet = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? dtExcelData.Rows[iCnt][colCnt].ToString().ToUpper().Trim().Substring(0, AlphabetLen) : dtExcelData.Rows[iCnt][colCnt].ToString().ToUpper().Trim().Substring(objGeneratedNo.FnEnd.ToString().Length, AlphabetLen));
                                                 TnNo = Convert.ToInt64(dtExcelData.Rows[iCnt][colCnt].ToString().ToUpper().Trim().Substring(dtExcelData.Rows[iCnt][colCnt].ToString().ToUpper().Trim().Length - objGeneratedNo.TnEnd.ToString().Length, objGeneratedNo.TnEnd.ToString().Length));

                                                 DataSet dtLotteryDtl = objLtmsService.GetLotteryDtlInClaimAndUnSold(objGeneratedNo.DataUniqueId, dtExcelData.Rows[iCnt][colCnt].ToString().ToUpper().Trim(), FnNo, Alphabet, TnNo);
                                                 if (dtLotteryDtl != null)
                                                 {
                                                     if (dtLotteryDtl.Tables[1].Rows.Count > 0)
                                                     {
                                                         errorCntForTkt++;
                                                         TotError++;
                                                         strMsg.AppendLine("Column " + (colCnt + 1) + " and Row No 1: The Lottery Ticket " + dtExcelData.Rows[iCnt][colCnt].ToString().ToUpper() + " is available in unsold ticket");

                                                     }
                                                 }
                                             }
                                        }
                                    }
                                }
                                if (IsCommonToAllSeries == true)
                                {
                                    errorCntForTkt++;
                                    TotError++;
                                    strMsg.AppendLine("Column " + (colCnt + 1) + " and Row No  " + (iCnt + 1) + ":  The Lottery Ticket " + dtExcelData.Rows[iCnt][colCnt].ToString().ToUpper() + " is type PWT So it will not uploded with this portal");
                                }
                                if (isPartialTicket == false)
                                {
                                    errorCntForTkt++;
                                    TotError++;
                                    strMsg.AppendLine("Column " + (colCnt + 1) + " and Row No  " + (iCnt + 1) + ": The Lottery Ticket " + dtExcelData.Rows[iCnt][colCnt].ToString().ToUpper() + " is Not a Winning prize.");
                                }

                                if (errorCntForTkt == 0 && isPartialTicket == true)
                                {
                                    dr = dtTicketNumber.NewRow();
                                    dr["TicketNumber"] = dtExcelData.Rows[iCnt][colCnt].ToString().ToUpper();
                                    dr["WinnerId"] = WinnerId.ToString();
                                    dtTicketNumber.Rows.Add(dr);
                                }
                            }
                            if (TotError > 10) { break; };
                        }
                    }
                }
            }
            if (strMsg.ToString().Trim().Length > 0)
            {
                if (TotError > 10)
                {
                    ErrorMsg = "There are more than 10 Error in the file  first 10 error are as below " + strMsg.ToString();
                }
                else
                {
                    ErrorMsg = "There are " + TotError + " Error in the file as below " + strMsg.ToString();
                }
                var message = new JavaScriptSerializer().Serialize(ErrorMsg);
                var script = string.Format("alert({0});", message);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "", script, true);
                return false;
            }
           
            dtLotteryInfo = objLtmsService.GetVariableClaimPrizeById(DatauNiqueId);
            bool IsFound = false;
            int DuplicateTicketCount = 0;
            for (int iRwCnt = 0; iRwCnt < dtLotteryInfo.Rows.Count; iRwCnt++)
            {
                for (int jRwCnt = 0; jRwCnt <dtTicketNumber.Rows.Count; jRwCnt++)
                {
                    if (dtTicketNumber.Rows[jRwCnt]["TicketNumber"].ToString().Trim() == dtLotteryInfo.Rows[iRwCnt]["TicketNumber"].ToString().Trim())
                    {
                        DuplicateTicketCount += 1;
                        IsFound = true;
                    }
                }
            }
            if (IsFound==true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Upload can not be completed. Total " + DuplicateTicketCount + " duplicate ticket no exist in database.');", true);
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
                        btnUpload.Visible = true;
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
                    Label lblTicketClaimedUploadStatus = ((Label)e.Row.FindControl("lblTicketClaimedUploadStatus"));
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 63) { Status = "Not Uploaded"; }
                    else if (StatusVal == 81) { Status = "Claim Ticket Uploaded"; }
                    lblStatus.Text = Status;
                    if (StatusVal == 81)
                    {
                        //((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = false;
                    }
                    else if (StatusVal == 83 || StatusVal == 85 || StatusVal == 87)
                    {
                        //((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = true;
                    }
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

                    ImageButton imgDeleteButton = ((ImageButton)e.Row.FindControl("imgDeleteEntry"));
                    Label lblStatus = ((Label)e.Row.FindControl("lblStatus"));
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    imgDeleteButton.Visible = true;
                    Dictionary<String, String> objClaimType = objValidateData.ClaimUploadStatus();
                    lblStatus.Text = objClaimType[lblStatus.Text];
                    imgDeleteButton.Visible = ((StatusVal == 2 || StatusVal == 4 || StatusVal == 6) ? false : true);
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