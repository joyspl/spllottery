using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class ToTicketToExcel : System.Web.UI.Page
    {
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            string QRUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["QRUrl"].ToString() + "?JB="; 
            StringBuilder strReport = new StringBuilder();
            StringBuilder strTableReport = new StringBuilder();

            ClsTicketGenRequest objClsTicketGenRequest = new ClsTicketGenRequest();
            objClsTicketGenRequest = (ClsTicketGenRequest)Session["TicketGenRequest"];

            ClsTicketInventory objGeneratedNo = new ClsTicketInventory();
            DataTable dtInfo = objLtmsService.GetRequisitionDtlById(objClsTicketGenRequest.DataUniqueId);
            if (dtInfo.Rows.Count > 0)
            {
                objGeneratedNo.DataUniqueId = Convert.ToInt64(dtInfo.Rows[0]["DataUniqueId"].ToString());
                objGeneratedNo.ReqDate = Convert.ToDateTime(dtInfo.Rows[0]["ReqDate"].ToString());
                objGeneratedNo.DrawNo = Convert.ToInt16(dtInfo.Rows[0]["DrawNo"].ToString());
                objGeneratedNo.DrawDate = Convert.ToDateTime(dtInfo.Rows[0]["DrawDate"].ToString());

                /*objGeneratedNo.FnStart = Convert.ToInt16(dtInfo.Rows[0]["FnStart"].ToString());
                objGeneratedNo.FnEnd = Convert.ToInt16(dtInfo.Rows[0]["FnEnd"].ToString());
                objGeneratedNo.AlphabetSeries = dtInfo.Rows[0]["AlphabetSeries"].ToString();
                objGeneratedNo.TnStart = Convert.ToInt64(dtInfo.Rows[0]["TnStart"].ToString());
                objGeneratedNo.TnEnd = Convert.ToInt64(dtInfo.Rows[0]["TnEnd"].ToString());*/
                objGeneratedNo.ID = objClsTicketGenRequest.ID;
                objGeneratedNo.FnStart = objClsTicketGenRequest.FnStart;
                objGeneratedNo.FnEnd = objClsTicketGenRequest.FnEnd;
                objGeneratedNo.AlphabetSeries = objClsTicketGenRequest.AlphabetSeries;
                objGeneratedNo.TnStart = objClsTicketGenRequest.TnStart;
                objGeneratedNo.TnEnd = objClsTicketGenRequest.TnEnd;

                objGeneratedNo.ReqCode = dtInfo.Rows[0]["ReqCode"].ToString();
                objGeneratedNo.StrReqDate = objGeneratedNo.ReqDate.Minute.ToString();
             
                Int64 FnStartRange = objClsTicketGenRequest.TnStart;
                Int64 FnEndRange = objClsTicketGenRequest.TnEnd;               
                //string FileName = objClsTicketGenRequest.RowNo.ToString()+ "_DR" + objGeneratedNo.DrawNo + "_" + Convert.ToDateTime(objGeneratedNo.DrawDate).ToString("ddMMMyyyy").ToUpper() + "_" + objGeneratedNo.ReqCode.Replace("/", "") + "_" + FnStartRange.ToString() + "_" + FnEndRange.ToString() + ".xls";
                //string FileName = objClsTicketGenRequest.RowNo.ToString() + "_DR" + objGeneratedNo.DrawNo + "_" + Convert.ToDateTime(objGeneratedNo.DrawDate).ToString("ddMMMyyyy").ToUpper() + "_" + objGeneratedNo.ReqCode.Replace("/", "") + "_" + FnStartRange.ToString() + "_" + FnEndRange.ToString() + ".csv";
                string FileName = objClsTicketGenRequest.RowNo.ToString() + "_DR" + objGeneratedNo.DrawNo + "_" + Convert.ToDateTime(objGeneratedNo.DrawDate).ToString("ddMMMyyyy").ToUpper() + "_" + objGeneratedNo.ReqCode.Replace("/", "") + "_" + FnStartRange.ToString() + "_" + FnEndRange.ToString() + ".xlsx";
                int FnStart = objGeneratedNo.FnStart;
                int FnEnd = objGeneratedNo.FnEnd;
                string AlphabetSeries = objGeneratedNo.AlphabetSeries;
                string[] values = AlphabetSeries.Split(',');
                Int64 TnStart = objGeneratedNo.TnStart;
                bool IsStartNoRequire = false;
                Int64 TnEnd = objGeneratedNo.TnEnd;
                FnEnd = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? 1 : objGeneratedNo.FnEnd);
                FnStart = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? 1 : objGeneratedNo.FnStart);
                IsStartNoRequire = ((objGeneratedNo.FnStart == 0 && objGeneratedNo.FnEnd == 0) ? false : true);   

                string AlphabetChar = "", LotteryNumber = "";

                /*for (int sCnt = FnStart; sCnt <= FnEnd; sCnt++)
                {
                    for (Int64 eCnt = FnEndRange; eCnt >= FnStartRange; eCnt--)
                    {                        
                        for (int aCnt =values.Length-1; aCnt >= 0; aCnt--)                       
                        {
                            AlphabetChar = values[aCnt].ToString().Trim();

                            strReport.AppendLine("<tr>");
                            //LotteryNumber =(IsStartNoRequire==true? sCnt.ToString().PadLeft(FnEnd.ToString().Length,'0') :"") + AlphabetChar + eCnt.ToString().PadLeft(TnEnd.ToString().Length,'0');
                            LotteryNumber = (IsStartNoRequire == true ? sCnt.ToString().PadLeft(FnEnd.ToString().Length, '0') : "") + AlphabetChar + eCnt.ToString().PadLeft(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PadLeftCharCount"]), '0');
                            strReport.AppendLine("      <td>" + LotteryNumber + "</td>");
                            strReport.AppendLine("      <td>" + QRUrl + GenQRNo(objGeneratedNo, LotteryNumber) +"</td>");
                            strReport.AppendLine("</tr>");
                        }
                    }
                }*/

                DataTable dt = new DataTable();
                dt.TableName = "dt";
                dt.Columns.Add(new DataColumn("LotteryNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("QRUrl", typeof(string)));
                DataRow dtRw = null;

                //StringBuilder builder = new StringBuilder();
                //List<string> columnNames = new List<string>();
                //List<string> rows = new List<string>();
                //columnNames.Add("\"LotteryNumber\"");
                //columnNames.Add("\"QRUrl\"");
                //builder.Append(string.Join(",", columnNames.ToArray())).Append("\n");

                for (Int64 eCnt = FnEndRange; eCnt >= FnStartRange; eCnt--)
                {
                    //for (int sCnt = FnStart; sCnt <= FnEnd; sCnt++)
                    for (int sCnt = FnEnd; sCnt >= FnStart; sCnt--)
                    {
                        for (int aCnt = values.Length - 1; aCnt >= 0; aCnt--)
                        {
                            AlphabetChar = values[aCnt].ToString().Trim();

                            /*strReport.AppendLine("<tr>");
                            //LotteryNumber =(IsStartNoRequire==true? sCnt.ToString().PadLeft(FnEnd.ToString().Length,'0') :"") + AlphabetChar + eCnt.ToString().PadLeft(TnEnd.ToString().Length,'0');
                            LotteryNumber = (IsStartNoRequire == true ? sCnt.ToString().PadLeft(FnEnd.ToString().Length, '0') : "") + AlphabetChar + eCnt.ToString().PadLeft(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PadLeftCharCount"]), '0');
                            //strReport.AppendLine("      <td>&#39;" + LotteryNumber + "</td>");
                            strReport.AppendLine("      <td>" + LotteryNumber + "</td>");
                            strReport.AppendLine("      <td>" + QRUrl + GenQRNo(objGeneratedNo, LotteryNumber) + "</td>");
                            strReport.AppendLine("</tr>");*/

                            dtRw = dt.NewRow();
                            LotteryNumber = (IsStartNoRequire == true ? sCnt.ToString().PadLeft(FnEnd.ToString().Length, '0') : "") + AlphabetChar + eCnt.ToString().PadLeft(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PadLeftCharCount"]), '0');
                            dtRw["LotteryNumber"] = LotteryNumber;
                            dtRw["QRUrl"] = QRUrl + GenQRNo(objGeneratedNo, LotteryNumber);
                            dt.Rows.Add(dtRw);

                            //List<string> currentRow = new List<string>();
                            //LotteryNumber = (IsStartNoRequire == true ? sCnt.ToString().PadLeft(FnEnd.ToString().Length, '0') : "") + AlphabetChar + eCnt.ToString().PadLeft(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PadLeftCharCount"]), '0');
                            //currentRow.Add("\"" + LotteryNumber + "\"");
                            //currentRow.Add("\"" + QRUrl + GenQRNo(objGeneratedNo, LotteryNumber) + "\"");
                            //rows.Add(string.Join(",", currentRow.ToArray()));
                        }
                    }
                }
                dt.AcceptChanges();

                //builder.Append(string.Join("\n", rows.ToArray()));

                /*strTableReport.AppendLine("<table border='1' >");
                strTableReport.AppendLine("          " + strReport.ToString());
                strTableReport.AppendLine("</table>");*/

                Response.Clear();
                Response.Buffer = true;
                //Response.ContentType = "application/vnd.ms-excel";
                Response.ContentType = "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "inline;filename=" + FileName);
                //Response.ContentType = "text/csv";
                //Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                //Response.Write(builder.ToString());
                //String HTMLDataToExport = strTableReport.ToString();


                /*Response.Write("<html><head><head>" +
                HTMLDataToExport.Replace("<BR>", "<br style='mso-data-placement:same-cell;'>")
                                               .Replace("<br>", "<br style='mso-data-placement:same-cell;'>")
                                               .Replace("<BR >", "<br style='mso-data-placement:same-cell;'>")
                                               .Replace("<BR />", "<br style='mso-data-placement:same-cell;'>")
                                               .Replace("<br />", "<br style='mso-data-placement:same-cell;'>")
                                               .Replace("<Br />", "<br style='mso-data-placement:same-cell;'>")
                                               .Replace("<Br>", "<br style='mso-data-placement:same-cell;'>")
                                               .Replace("<br >", "<br style='mso-data-placement:same-cell;'>") + "</html>");*/
                Response.BinaryWrite(GenerateExcel2007(dt));
                Response.End();
            }                
        }
        public string GenQRNo(ClsTicketInventory objGeneratedNo, string LotteryNo)
        {
            string QRCode = "", RQId = "", RQDate = "", GenLotteryNo = "", GenCheckDigit = "";
            CheckDigitCalculation objCheckDigitCalculation = new CheckDigitCalculation();
            RQId = "R_" + objGeneratedNo.DataUniqueId.ToString() + "~";
            RQDate = "M_" + objGeneratedNo.StrReqDate + ":";
            GenLotteryNo = "X_" + LotteryNo + "-";
            GenCheckDigit = "S_" + objCheckDigitCalculation.GenCheckDigit(objGeneratedNo.DataUniqueId.ToString() + objGeneratedNo.StrReqDate + LotteryNo) + "!";


            Random rnd = new Random();
            int IdPosition = rnd.Next(1, 3);
            if (IdPosition == 1)
            {
                QRCode = GenLotteryNo + RQDate + GenCheckDigit + RQId + "X";
            }
            else if (IdPosition == 2)
            {
                QRCode = RQId + GenCheckDigit + GenLotteryNo + "X" + RQDate;
            }
            else if (IdPosition == 3)
            {
                QRCode = GenCheckDigit + RQDate + "X" + RQId + GenLotteryNo;
            }

            if (objGeneratedNo.ID > default(long))
                return string.Format("{0}_{1}", QRCode, objGeneratedNo.ID.ToString());
            else
                return QRCode;
        }
        public byte[] GenerateExcel2007(DataTable p_dsSrc)
        {
            using (ExcelPackage objExcelPackage = new ExcelPackage())
            {
                //Create the woorkbook
                ExcelWorkbook objWorkbook = objExcelPackage.Workbook;
                //Create the worksheet    
                ExcelWorksheet objWorksheet = objWorkbook.Worksheets.Add(p_dsSrc.TableName);
                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1    
                objWorksheet.Cells["A1"].LoadFromDataTable(p_dsSrc, true);
                objWorksheet.Cells.Style.Font.SetFromFont(new Font("Calibri", 11));

                //Add autoFilter to all columns
                objWorksheet.Cells[objWorksheet.Dimension.Address].AutoFilter = true;

                //AutoFit All Columns
                objWorksheet.Cells.AutoFitColumns();

                //Format the header
                //var headerCells = objWorksheet.Cells[1, 1, 1, objWorksheet.Dimension.End.Column];
                using (ExcelRange objRange = objWorksheet.Cells[1, 1, 1, objWorksheet.Dimension.End.Column])
                {
                    objRange.Style.Font.Bold = true;
                    //objRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;    
                    //objRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;    
                    objRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    objRange.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#FFCC66"));
                }

                return objExcelPackage.GetAsByteArray();
            }
        }
    }
}