using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class ToExcel : System.Web.UI.Page
    {
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            StringBuilder strTableReport = new StringBuilder();
            StringBuilder strReport = new StringBuilder();
            DataTable dtTicketInfo = new DataTable();
            if (Session["InputParameter"].ToString().Trim() != "")
            {
                dtTicketInfo = objLtmsService.GetRequisitionDtlById(Convert.ToInt64(Session["InputParameter"]));
                if (dtTicketInfo.Rows.Count > 0)
                {
                    strTableReport.AppendLine("<table border='1' >");
                    for (Int32 tktCnt = 0; tktCnt < dtTicketInfo.Rows.Count; tktCnt++)
                    {

                        strReport.AppendLine("    <tr>");
                        strReport.AppendLine("        <td>" + dtTicketInfo.Rows[tktCnt]["TicketNumber"].ToString() + "</td>");
                        strReport.AppendLine("    </tr>");
                    }
                    strTableReport.AppendLine(strReport.ToString());
                    strTableReport.AppendLine("</table>");
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/vnd.ms-excel";
                    String FileName = "LotteryTicket" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".xls";
                    Response.AddHeader("Content-Disposition", "inline;filename=" + FileName);
                    String HTMLDataToExport = strTableReport.ToString();


                    Response.Write("<html><head><head>" +
                    HTMLDataToExport.Replace("<BR>", "<br style='mso-data-placement:same-cell;'>")
                                                   .Replace("<br>", "<br style='mso-data-placement:same-cell;'>")
                                                   .Replace("<BR >", "<br style='mso-data-placement:same-cell;'>")
                                                   .Replace("<BR />", "<br style='mso-data-placement:same-cell;'>")
                                                   .Replace("<br />", "<br style='mso-data-placement:same-cell;'>")
                                                   .Replace("<Br />", "<br style='mso-data-placement:same-cell;'>")
                                                   .Replace("<Br>", "<br style='mso-data-placement:same-cell;'>")
                                                   .Replace("<br >", "<br style='mso-data-placement:same-cell;'>") + "</html>");
                    Response.End();
                }
            }
        }
    }
}