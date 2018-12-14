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
    public partial class TrxTicketInventoryUnSold : System.Web.UI.Page
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
                items.Add(new ListItem("<<--All-->>", "-70"));
                items.Add(new ListItem("Not Uploaded", "42"));
                items.Add(new ListItem("Un-Sold Ticket Uploaded", "51"));
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
                DataRow dr = null;
                dtTicketNumber.Columns.Add(new DataColumn("TicketNumber", typeof(string)));
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
                            UseHeaderRow = false
                        }
                    });
                    Exceldt = excelreaderDS.Tables[0];

                   
                    DataTable dtTicket = new DataTable();
                    if (IsValidEntry(Exceldt, Convert.ToInt64(hdUniqueId.Value), out dtTicket) == false) { return; }//Check for valid data entry
                    Exceldt.Clear();
                    Exceldt = null;

                    bool IsAdded = false;
                    objClsLottery.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                    objClsLottery.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                    objClsLottery.IpAdd = Request.UserHostAddress;
                    if (hdUniqueId.Value.ToString().Trim() != "")
                    {
                        IsAdded = objLtmsService.InserInTicketInventoryUnSold(objClsLottery, dtTicket, 3);
                        if (IsAdded == true)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Un-Sold Ticked information Uploaded successfully.');", true);
                        }
                        dtTicket.Clear();
                        dtTicket = null;
                    }                   
                   
                    BindGvData();
                    btnCancel_Click(sender, e);
                }
                else if (chkNoFile.Checked == true)
                {
                    DataTable dtTicket = new DataTable();
                    dtTicket.TableName = "dtTicket";                   
                    dtTicket.Columns.Add(new DataColumn("FnNo", typeof(int)));
                    dtTicket.Columns.Add(new DataColumn("Alphabet", typeof(string)));
                    dtTicket.Columns.Add(new DataColumn("StartNo", typeof(Int64)));
                    dtTicket.Columns.Add(new DataColumn("EndNo", typeof(Int64)));

                    objClsLottery.DataUniqueId = hdUniqueId.Value.ToString().Trim();
                    objClsLottery.UserId = ((ClsUserInfo)Session["UserInfo"]).UserId;
                    objClsLottery.IpAdd = Request.UserHostAddress;
                    if (hdUniqueId.Value.ToString().Trim() != "")
                    {
                        bool IsAdded = objLtmsService.InserInTicketInventoryUnSold(objClsLottery, dtTicket, 4);
                        if (IsAdded == true)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Claimed Ticked information Uploaded successfully.');", true);
                        }
                    }
                    BindGvData();
                    btnCancel_Click(sender, e);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Plese upload xls,xlsx excel file type.');", true);
                    return;
                }
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
        private bool IsValidEntry(DataTable dtExcelData, Int64 DatauNiqueId,out DataTable dtTicketRange)
        {
            string ErrorMsg = "";
            Int32 TotError = 0;
            StringBuilder strMsg = new StringBuilder();
            ClsTicketInventory objGeneratedNo = new ClsTicketInventory();
            dtTicketRange = null;

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

             Int32 ColCnt = 0;
             for (Int32 colCnt = 0; colCnt < dtExcelData.Columns.Count; colCnt++)
             {
                if (dtExcelData.Columns[colCnt].ToString().Trim() != "")
                {
                    ColCnt++;
                }
             }

             if (ColCnt != 4)
             {
                 ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('No of column in excel should be 4 in a sequence of Initial No, Alphabates, Start no , end no.');", true);
                 return false;
             }

            DataTable dtTicketRow = new DataTable();
            dtTicketRow.TableName = "dtTicket";
            DataRow dataRw = null;
            dtTicketRow.Columns.Add(new DataColumn("FnNo", typeof(int)));
            dtTicketRow.Columns.Add(new DataColumn("Alphabet", typeof(string)));
            dtTicketRow.Columns.Add(new DataColumn("StartNo", typeof(Int64)));
            dtTicketRow.Columns.Add(new DataColumn("EndNo", typeof(Int64)));

                      
            DataTable dtLotteryInfo =  objLtmsService.GetRequisitionDtlById(DatauNiqueId);
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

                bool IsError = false;
                for (Int32 iCnt = 0; iCnt < dtExcelData.Rows.Count; iCnt++)
                {
                    string FirstInitialNo = dtExcelData.Rows[iCnt][0].ToString();
                    string exclAlphabetSeries = dtExcelData.Rows[iCnt][1].ToString();
                    string TnStart = dtExcelData.Rows[iCnt][2].ToString();
                    string TnEnd = dtExcelData.Rows[iCnt][3].ToString();
                    IsError = false;
                    if (isValidStartNo(objGeneratedNo.FnStart, objGeneratedNo.FnEnd, FirstInitialNo)==false)
                    {
                        TotError++;
                        IsError = true;
                        strMsg.AppendLine("Column " + 1 + " and Row No " + (iCnt + 2) + ": Invalid First Initial No");
                    }
                    if (isValidAlphabetSeries(objGeneratedNo.AlphabetSeries, exclAlphabetSeries)==false)
                    {
                        TotError++;
                        IsError = true;
                        strMsg.AppendLine("Column " + 2 + " and Row No " + (iCnt + 2) + ": Invalid Alphabets series");
                    }
                    if (isValidEndSeries(objGeneratedNo.TnStart, objGeneratedNo.TnEnd, TnStart, TnEnd)==false)
                    {
                        TotError++;
                        IsError = true;
                        strMsg.AppendLine("Column " + 3 + " and Row No " + (iCnt + 2) + ": Invalid End Range");
                    } 
                    if (TotError > 10) { break;};
                    if (IsError == false)
                    {
                        string[] excelValues = exclAlphabetSeries.TrimEnd(',').Split(',');
                        for (int jCnt = 0; jCnt < excelValues.Length; jCnt++)
                        {
                            dataRw = dtTicketRow.NewRow();
                            dataRw["FnNo"] = FirstInitialNo;
                            dataRw["Alphabet"] = excelValues[jCnt].ToString().Trim();
                            dataRw["StartNo"] = TnStart;
                            dataRw["EndNo"] = TnEnd;
                            dtTicketRow.Rows.Add(dataRw);
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

                #region Duplicate Ticket Check
                dtTicketRow.DefaultView.Sort = "FnNo ASC,Alphabet ASC,StartNo ASC,EndNo ASC";
                dtTicketRow = dtTicketRow.DefaultView.ToTable();

                DataTable distinctTable = dtTicketRow.DefaultView.ToTable(true);
                if (dtTicketRow.Rows.Count != distinctTable.Rows.Count)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Upload can not be completed.Duplicate Ticket No exist in file.');", true);
                    return false;
                }
                Int32 StratRowCnt = 0;
                Int32 EndRowCnt = 0;
                Int32 TicketCnt=0;
                for (int iCnt = 0; iCnt < dtTicketRow.Rows.Count; iCnt++)
                {
                    TicketCnt = TicketCnt+(Convert.ToInt32(dtTicketRow.Rows[iCnt][3].ToString()) - Convert.ToInt32(dtTicketRow.Rows[iCnt][2].ToString())) + 1;
                    for (int jCnt = iCnt+1; jCnt < dtTicketRow.Rows.Count; jCnt++)
                    {
                        if ((dtTicketRow.Rows[iCnt][0].ToString() == dtTicketRow.Rows[jCnt][0].ToString()) &&
                            (dtTicketRow.Rows[iCnt][1].ToString() == dtTicketRow.Rows[jCnt][1].ToString()) &&
                            (
                                (
                                    (   Convert.ToInt32(dtTicketRow.Rows[iCnt][2].ToString()) >=Convert.ToInt32(dtTicketRow.Rows[jCnt][2].ToString()) &&
                                        Convert.ToInt32(dtTicketRow.Rows[iCnt][2].ToString()) <=Convert.ToInt32(dtTicketRow.Rows[jCnt][3].ToString())
                                    )
                                    ||
                                    (Convert.ToInt32(dtTicketRow.Rows[iCnt][3].ToString()) >= Convert.ToInt32(dtTicketRow.Rows[jCnt][2].ToString()) &&
                                        Convert.ToInt32(dtTicketRow.Rows[iCnt][3].ToString()) <= Convert.ToInt32(dtTicketRow.Rows[jCnt][3].ToString())
                                    )
                                )||
                                (
                                    (Convert.ToInt32(dtTicketRow.Rows[jCnt][2].ToString()) >= Convert.ToInt32(dtTicketRow.Rows[iCnt][2].ToString()) &&
                                        Convert.ToInt32(dtTicketRow.Rows[jCnt][2].ToString()) <= Convert.ToInt32(dtTicketRow.Rows[iCnt][3].ToString())
                                    )
                                    ||
                                    (Convert.ToInt32(dtTicketRow.Rows[jCnt][3].ToString()) >= Convert.ToInt32(dtTicketRow.Rows[iCnt][2].ToString()) &&
                                        Convert.ToInt32(dtTicketRow.Rows[jCnt][3].ToString()) <= Convert.ToInt32(dtTicketRow.Rows[iCnt][3].ToString())
                                    )                                
                                )
                            )
                          )
                        {
                            StratRowCnt = iCnt;
                            EndRowCnt = jCnt;
                            break;
                        }
                    }
                    if (StratRowCnt > 0 && EndRowCnt > 0) 
                    {
                        break;
                    }
                }
                if (StratRowCnt > 0 && EndRowCnt > 0) 
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Upload can not be completed.Duplicate Ticket No exist within the range in file.');", true);
                    return false;
                }
                
                #endregion

                #region Unsold Percentage count
                Int64 Qty = Convert.ToInt64(txtQty.Text.Trim());
                Int64 SoldCount = Convert.ToInt64(TicketCnt);
                double Percentage = 0;
                if (SoldCount > 0)
                {
                    try
                    {
                        Percentage = Convert.ToDouble((Convert.ToDouble(SoldCount) / Convert.ToDouble(Qty)) * 100);
                        if (Convert.ToDouble(txtUnSoldPercentage.Text.Trim()) > 0 && Percentage > Convert.ToDouble(txtUnSoldPercentage.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('No of ticket percentage cannot be more than the " + txtUnSoldPercentage.Text.Trim() + "%.');", true);
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        objValidateData.SaveSystemErrorLog(ex, Request.UserHostAddress);
                        ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Some Error occured while calculating un-Sold persantage. Please contact system administrator');", true);
                        return false;
                    }
                }
                #endregion

                dtTicketRange = dtTicketRow.Copy();
                #region Validate ticker
                //for (Int16 colCnt = 0; colCnt < dtExcelData.Columns.Count; colCnt++)
                //{
                //    if (dtExcelData.Columns[colCnt].ToString().Trim() != "")
                //    {
                //        ErrorMsg = objValidateData.ValidateTicketNo(objGeneratedNo, dtExcelData.Columns[colCnt].ToString());
                //        if (ErrorMsg.Trim().Length > 0)
                //        {
                //            TotError++;
                //            strMsg.AppendLine("Column " + (colCnt + 1) + " and Row No 1:" + ErrorMsg);
                //        }
                //        else {
                //            TicketNo = dtExcelData.Columns[colCnt].ToString().Trim();
                //            dr = dtTicketNumber.NewRow();
                //            dr["FnNo"] = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? "0" : TicketNo.Substring(0, objGeneratedNo.FnEnd.ToString().Length));
                //            dr["Alphabet"] = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? TicketNo.Substring(0, AlphabetLen) : TicketNo.Substring(objGeneratedNo.FnEnd.ToString().Length, AlphabetLen));
                //            dr["TnNo"] = TicketNo.Substring(TicketNo.Length - objGeneratedNo.TnEnd.ToString().Length, objGeneratedNo.TnEnd.ToString().Length);
                //            dtTicketNumber.Rows.Add(dr);
                //        }
                //        for (Int32 iCnt = 0; iCnt < dtExcelData.Rows.Count; iCnt++)
                //        {
                //            if (dtExcelData.Rows[iCnt][colCnt].ToString() != "")
                //            {
                //                ErrorMsg = objValidateData.ValidateTicketNo(objGeneratedNo, dtExcelData.Rows[iCnt][colCnt].ToString());
                //                if (ErrorMsg.Trim().Length > 0)
                //                {
                //                    TotError++;
                //                    if (TotError <= 10)
                //                    {
                //                        strMsg.AppendLine("Column " + (colCnt + 1) + " and Row No " + (iCnt + 1) + ": " + ErrorMsg);
                //                    }
                //                    if (TotError > 10) { break; };
                //                }
                //                else 
                //                {
                //                    TicketNo = dtExcelData.Rows[iCnt][colCnt].ToString();
                //                    dr = dtTicketNumber.NewRow();
                //                    dr["FnNo"] = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? "0" : TicketNo.Substring(0, objGeneratedNo.FnEnd.ToString().Length));
                //                    dr["Alphabet"] = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? TicketNo.Substring(0, AlphabetLen) : TicketNo.Substring(objGeneratedNo.FnEnd.ToString().Length, AlphabetLen));
                //                    dr["TnNo"] = TicketNo.Substring(TicketNo.Length - objGeneratedNo.TnEnd.ToString().Length, objGeneratedNo.TnEnd.ToString().Length);
                //                    dtTicketNumber.Rows.Add(dr);
                //                }
                //            }
                //        }
                //    }
                //}

               
                #endregion
                #region Bind ticket in range
               // dtTicketNumber.DefaultView.Sort = "FnNo ASC,Alphabet ASC,TnNo ASC";
               // dtTicketNumber = dtTicketNumber.DefaultView.ToTable();
               // DataTable dtTicket = new DataTable();
               // dtTicket.TableName = "dtTicket";
               // DataRow dtRw = null;
               // dtTicket.Columns.Add(new DataColumn("FnNo", typeof(int)));
               // dtTicket.Columns.Add(new DataColumn("Alphabet", typeof(string)));
               //// dtTicket.Columns.Add(new DataColumn("TnNo", typeof(string)));
               // dtTicket.Columns.Add(new DataColumn("StartNo", typeof(Int64)));
               // dtTicket.Columns.Add(new DataColumn("EndNo", typeof(Int64)));
               // Int32 tempEndNo=0,rowCounter=0;                
               // int FnNo = 0;
               // string Alphabet = "";
               // Int32 TnNo = 0;
               
               // bool isRowExist = true;
               // for (Int32 iCnt = 0; iCnt < dtTicketNumber.Rows.Count; iCnt++)
               // {
               //     FnNo = Convert.ToInt16(dtTicketNumber.Rows[iCnt]["FnNo"].ToString());
               //     Alphabet = dtTicketNumber.Rows[iCnt]["Alphabet"].ToString();
               //     TnNo = Convert.ToInt32(dtTicketNumber.Rows[iCnt]["TnNo"].ToString());
               //     isRowExist = false;
               //     if (dtTicket.Rows.Count > 0)
               //     {
               //         if ((FnNo == Convert.ToInt16(dtTicket.Rows[rowCounter]["FnNo"].ToString())) &&
               //             (Alphabet == dtTicket.Rows[rowCounter]["Alphabet"].ToString())
               //            )
               //         {
               //             isRowExist = true;
               //         }
               //     }
               //     if (isRowExist == false)
               //     {
               //         dtRw = dtTicket.NewRow();
               //         dtRw["FnNo"] = FnNo;
               //         dtRw["Alphabet"] = Alphabet;
               //        // dtRw["TnNo"] = TnNo;
               //         dtRw["StartNo"] = TnNo;
               //         dtRw["EndNo"] = TnNo;
               //         dtTicket.Rows.Add(dtRw);
               //         tempEndNo = TnNo + 1;
               //         rowCounter = dtTicket.Rows.Count-1;
               //     }
                    
               //     if (isRowExist == true && TnNo == tempEndNo)
               //     {
               //         dtTicket.Rows[rowCounter]["EndNo"] = tempEndNo;
               //         tempEndNo = TnNo+1;
               //     }
               //     else if (isRowExist == true && TnNo > tempEndNo)
               //     {
               //         FnNo = Convert.ToInt16(dtTicketNumber.Rows[iCnt]["FnNo"].ToString());
               //         Alphabet = dtTicketNumber.Rows[iCnt]["Alphabet"].ToString();
               //         TnNo = Convert.ToInt32(dtTicketNumber.Rows[iCnt]["TnNo"].ToString());
                        
               //         dtRw = dtTicket.NewRow();
               //         dtRw["FnNo"] = FnNo;
               //         dtRw["Alphabet"] = Alphabet;
               //        // dtRw["TnNo"] = TnNo;
               //         dtRw["StartNo"] = TnNo;
               //         dtRw["EndNo"] = TnNo;
               //         dtTicket.Rows.Add(dtRw);
               //         tempEndNo = TnNo + 1;
               //         rowCounter = dtTicket.Rows.Count-1;
               //     }
               // }
               // dtTicketRange = dtTicket.Copy();
               //// dtTicketRange = dtTicket;

               // dtExcelData.Clear();
               // dtExcelData = null;

               // dtTicket.Clear();
               // dtTicket = null;
                #endregion
            }
            return true;
        }

        public bool isValidStartNo(int FnStart,int FnEnd , string FirstInitialNo)
        {
            bool isValid = false;
            try
            {
                if (Convert.ToInt32(FirstInitialNo) >= FnStart && Convert.ToInt32(FirstInitialNo) <= FnEnd)
                {
                    isValid= true;
                }
            }
            catch {
                isValid = false;
            }
            return isValid;
        }
        public bool isValidAlphabetSeries(string AlphabetSeries, string exclAlphabetSeries)
        {
            bool isValid = false;
            try
            {               
                string[] lotteryValues = AlphabetSeries.TrimEnd(',').Split(',');
                string[] excelValues = exclAlphabetSeries.TrimEnd(',').Split(',');
                int errorCnt = 0;
                for (int jCnt = 0; jCnt < excelValues.Length; jCnt++)
                {                    
                    for (int aCnt = 0; aCnt < lotteryValues.Length; aCnt++)
                    {                           
                        if (excelValues[jCnt].ToString().Trim() == lotteryValues[aCnt].ToString().Trim())
                        {
                            errorCnt++;
                            break;
                        }                            
                    }
                    if (errorCnt == 0){break;}
                }
                isValid = (errorCnt == 0 ? false : true);
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }
        public bool isValidEndSeries(Int64 TnStart, Int64 TnEnd, string excelTnStart, string excelTnEnd)
        {
            bool isValid = false;
            try
            {
                if ((Convert.ToInt32(excelTnStart) >= TnStart && Convert.ToInt32(excelTnStart) <= TnEnd) && (Convert.ToInt32(excelTnEnd) >= TnStart && Convert.ToInt32(excelTnEnd) <= TnEnd))
                {
                    isValid = true;
                }
            }
            catch
            {
                isValid = false;
            }
            return isValid;
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
                        txtUnSoldPercentage.Text = dtInfo.Rows[0]["UnSoldPercentage"].ToString();
                        txtQty.Text = dtInfo.Rows[0]["Qty"].ToString();
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
                    Session["ReportName"] = "UnsoldPrizeDistributor";
                    Response.Redirect("rptViewAppReport.aspx");

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
                    Label lblTicketUnSoldUploadStatus = ((Label)e.Row.FindControl("lblTicketUnSoldUploadStatus"));                    
                    string Status = "";
                    int StatusVal = Convert.ToInt16(lblStatus.Text);
                    if (StatusVal == 42) { Status = "Not Uploaded"; }
                    else if (StatusVal == 51 && lblTicketUnSoldUploadStatus.Text=="3") { Status = "Un-Sold Ticket Uploaded"; }
                    else if (StatusVal == 51 && lblTicketUnSoldUploadStatus.Text == "4") { Status = "Save As No Un-Sold Ticket"; }
                    else if (StatusVal == 53 || StatusVal == 55 || StatusVal == 57) { Status = "Rejected"; }
                    lblStatus.Text = Status;
                    if (StatusVal == 52 || StatusVal == 54 || StatusVal == 56 || StatusVal > 57)
                    {
                        ((ImageButton)e.Row.FindControl("imgEditEntry")).Visible = false;
                    }                   
                    
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