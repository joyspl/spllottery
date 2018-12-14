using LTMSClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public class ValidationAndCommonFunction
    {
        LTMSServiceRef.LTMSServiceClient objLTMSServiceClient = new LTMSServiceRef.LTMSServiceClient();
        int indexOf;
        char[] SpecialChars = "<>:/'|".ToCharArray();

        public Dictionary<String, String> ClaimType()
        {
            Dictionary<String, String> objClaimType = new Dictionary<String, String>();
            objClaimType.Add("-1", "<<--Select-->>");
            objClaimType.Add("1", "Fixed Prize Winning Ticket");
            objClaimType.Add("2", "Super Ticket for Fixed Prize");
            objClaimType.Add("3", "Spl. Ticket for Fixed Prize");
            return objClaimType;
        }
       
        // Cryptography objCryptography = new Cryptography();
        public bool IsInteger(string strVal)
        {
            try
            {
                int IntVal = int.Parse(strVal.Trim());
                if (IntVal > 0) { return true; } else { return false; }
            }
            catch { return false; }
        }
        public bool IsIntegerWithZero(string strVal)
        {
            try
            {
                int IntVal = int.Parse(strVal.Trim());
                return true;
            }
            catch { return false; }
        }
        public bool IsLongInteger(string strVal)
        {
            try
            {
                Int64 IntVal = Convert.ToInt64(strVal.Trim());
                if (IntVal > 0) { return true; } else { return false; }
            }
            catch { return false; }
        }
        public bool IsLongIntegerWithZero(string strVal)
        {
            try
            {
                Int64 IntVal = Convert.ToInt64(strVal.Trim());
                return true; 
            }
            catch { return false; }
        }
        public bool IsDouble(string strVal)
        {
            try
            {

                double IntVal = double.Parse(strVal.Trim());
                if (IntVal > 0) { return true; } else { return false; }
            }
            catch { return false; }
        }
        public bool IsDoubleWithZero(string strVal)
        {
            try
            {

                double IntVal = double.Parse(strVal.Trim());
                return true;
            }
            catch { return false; }
        }
        public bool RestrictSpecialChars(string FieldName)
        {
            indexOf = FieldName.Trim().IndexOfAny(SpecialChars);
            if (indexOf != -1) { return false; } else { return true; }
        }
        public bool isEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        public bool istValidAlphabets(string strVal)
        {
            bool IsValid;
            Regex isAlphabets = new Regex("[^a-zA-Z]");
            IsValid = !isAlphabets.IsMatch(strVal);
            return IsValid;
        }
        public bool isValidDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return true;
            }
            catch { return false; }
        }
        public string GenerateRandomOTP()
        {
            string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string sOTP = String.Empty;
            string sTempChars = String.Empty;
            Random rand = new Random();
            for (int i = 0; i < 8; i++)
            {
                int p = rand.Next(0, saAllowedCharacters.Length);
                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];
                sOTP += sTempChars;
            }
            return sOTP;
        }  
        public bool ClearAllInputField(System.Web.UI.WebControls.Panel UiPanel)
        {
            try
            {
                foreach (System.Web.UI.Control ctrl in UiPanel.Controls)
                {
                    if (ctrl is TextBox)
                    {
                        ((TextBox)ctrl).Text = "";
                    }
                    else if (ctrl is DropDownList)
                    {
                        ((DropDownList)ctrl).SelectedIndex = 0;
                    }
                    else if (ctrl is CheckBox)
                    {
                        ((CheckBox)ctrl).Checked = false;
                    }
                    else if (ctrl is CheckBoxList)
                    {
                        for (int i = 0; i < ((CheckBoxList)ctrl).Items.Count; i++)
                        {
                            ((CheckBoxList)ctrl).Items[i].Selected = false;
                        }
                    }

                }
                return true;
            }
            catch (Exception Ex)
            { return false; }
        }
        public DropDownList FillSearchDropDown(DropDownList DrpList, string[] DropDownListDisplayMember, string[] DropDownListValueMember, int DropDownListIndex)
        {
            try
            {
                #region Fill the Datatable
                DataTable dtFillDropDownList = new DataTable();
                DataRow drFillDropDownList = null;


                dtFillDropDownList.Columns.Add(new DataColumn("FieldDescription", typeof(string)));
                dtFillDropDownList.Columns.Add(new DataColumn("FieldName", typeof(string)));

                for (int iCnt = 0; iCnt < DropDownListDisplayMember.Length; iCnt++)
                {
                    drFillDropDownList = dtFillDropDownList.NewRow();
                    drFillDropDownList["FieldDescription"] = DropDownListDisplayMember[iCnt].ToString();
                    drFillDropDownList["FieldName"] = DropDownListValueMember[iCnt].ToString();
                    dtFillDropDownList.Rows.Add(drFillDropDownList);
                }
                #endregion

                #region Clear DropDownList
                DrpList.DataSource = null;
                DrpList.Items.Clear();
                #endregion

                #region Fill DropDownList & return
                DrpList.DataSource = dtFillDropDownList;
                DrpList.DataTextField = "FieldDescription";
                DrpList.DataValueField = "FieldName";
                DrpList.DataBind();
                DrpList.SelectedIndex = DropDownListIndex;
                dtFillDropDownList.Dispose();
                return DrpList;
                #endregion
            }
            catch (Exception ex)
            { throw new Exception(ex.Message); }
            finally { }
        }
        public DropDownList FillDropDownList(DropDownList cmb, string cmboFieldName, string Parameter)
        {

            string DisplayField = "", ValueField = "";
            DataTable dtDropDownList = new DataTable();
            try
            {
                
#region Fill the dataset
                switch (cmboFieldName)
                {
                    case "DepositMethod":
                        dtDropDownList = objLTMSServiceClient.GetDepositMethodDetails();
                        DisplayField = "DepositMethod";
                        ValueField = "DataUniqueId";
                        break;
                    case "DepositTo":
                        dtDropDownList = objLTMSServiceClient.GetDepositToDtl();
                        DisplayField = "DepositTo";
                        ValueField = "DataUniqueId";
                        break;

                    case "LotteryType":
                        dtDropDownList = objLTMSServiceClient.GetLotteryTypeDtl();
                        DisplayField = "LotteryType";
                        ValueField = "DataUniqueId";
                        break;
                    case "UserRole":
                        dtDropDownList = objLTMSServiceClient.GetUserRoleDtl();
                        DisplayField = "UserRole";
                        ValueField = "DataUniqueId";
                        break;
                    case "LotteryNameByLotteryTypeID":
                        dtDropDownList = objLTMSServiceClient.GetLotteryDtlByLotteryTypeId(Convert.ToInt64(Parameter));
                        DisplayField = "LotteryName";
                        ValueField = "DataUniqueId";
                        break;
                    case "LotteryByApprovedGovOrder":
                        dtDropDownList = objLTMSServiceClient.GetLotteryByApprovedGovOrder(Convert.ToInt64(Parameter));
                        DisplayField = "NewModifiedLotteryName";
                        ValueField = "DataUniqueId";// Goverment Data Unique Id
                        break;
                    case "GovOrderDtlByLotteryId":
                        dtDropDownList = objLTMSServiceClient.GetGovOrderDtlByLotteryId(Convert.ToInt64(Parameter));
                        DisplayField = "GovermentOrder";
                        ValueField = "DataUniqueId";
                        break;  
                        
                    default:
                        dtDropDownList = null;
                        break;
                }
                #endregion
                #region Fill Combobox & return
                cmb.DataSource = null;
                cmb.Items.Clear();
                if (dtDropDownList.Rows.Count > 0)
                {
                    DataRow dtComboRow = dtDropDownList.NewRow();
                    dtComboRow[DisplayField] = "<<--Select-->>";
                    dtDropDownList.Rows.InsertAt(dtComboRow, 0);
                    cmb.DataSource = dtDropDownList;
                    cmb.DataTextField = DisplayField;
                    cmb.DataValueField = ValueField;
                    cmb.DataBind();
                    cmb.SelectedIndex = 0;
                }
                #endregion


            }
            catch (Exception ex)
            {
                throw;
            }
            return cmb;

        }
        public void SaveSystemErrorLog(Exception e, string IPAddress)
        {

            StringBuilder sbErrorText = new StringBuilder();
            string FileName = DateTime.Now.ToString("dd-MMM-yyyy");
            string ErrorLogPath = System.Web.Configuration.WebConfigurationManager.AppSettings["ErrorLogPath"].ToString();
            sbErrorText.Append(Environment.NewLine);
            sbErrorText.Append(Environment.NewLine);
            sbErrorText.Append("======================================================================================" + Environment.NewLine);
            sbErrorText.Append("@  Error Date Time  : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") + Environment.NewLine);
            sbErrorText.Append("@  Error At  : " + IPAddress + Environment.NewLine);
            sbErrorText.Append("--------------------------------------------------------------------------------------" + Environment.NewLine);
            sbErrorText.Append("@  Message            : " + e.Message + Environment.NewLine);
            sbErrorText.Append("@  Inner Exception    : " + e.InnerException + Environment.NewLine);
            sbErrorText.Append("@  Source             : " + e.Source + Environment.NewLine);
            sbErrorText.Append("@  Stack Trace        : " + e.StackTrace + Environment.NewLine);
            sbErrorText.Append("@  TargetSite         : " + e.Data + Environment.NewLine);
            sbErrorText.Append("--------------------------------------------------------------------------------------" + Environment.NewLine);


            string Path = ErrorLogPath + @"\" + FileName + ".txt";
            string TemplatePath = ErrorLogPath + @"\ErrorLog.txt";
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(ErrorLogPath);
                File.WriteAllText(TemplatePath, "");
            }
            if (!File.Exists(Path))
            {
                try
                {
                    File.Copy(TemplatePath, Path);
                }
                catch (Exception ex) { }
            }
            if (File.Exists(Path))
            {
                try
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(Path, true);
                    file.WriteLine(sbErrorText);
                    file.Close();
                }
                catch (Exception ex) { }
            }


        }
        public string RightStr(string str, int length)
        {
            return str.Substring(str.Length - length, length);
        }
        public string SMSSend(string MobileNo, string TextOtpMsg)
        {
            string Msg = "";
            string ErrorMsg = "";
            WebClient client = new WebClient();
            try
            {
                string SmsUserID = System.Web.Configuration.WebConfigurationManager.AppSettings["SmsUserID"].ToString();
                string SmsPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["SmsPassword"].ToString();
                string SmsSenderName = System.Web.Configuration.WebConfigurationManager.AppSettings["SmsSenderName"].ToString();

                string TextMsg = "";
                string baseurl = "";
                ErrorMsg = "";
                try
                {
                    TextMsg = TextOtpMsg;
                    baseurl = "http://bulksms.mysmsmantra.com:8080/WebSMS/SMSAPI.jsp?username=" + SmsUserID + "&password=" + SmsPassword + "&sendername=" + SmsSenderName + "&mobileno=" + MobileNo + "&message=" + TextMsg;
                    Stream data = client.OpenRead(baseurl);
                    StreamReader reader = new StreamReader(data);
                    Msg = reader.ReadToEnd();
                    data.Close();
                    reader.Close();
                }
                catch (Exception ex)
                {
                    SaveSystemErrorLog(ex, "[SMS Sent Status]");
                    ErrorMsg = "UNSUCCESSFULL";
                    // ErrorMsg += ErrorMsg + " " + ex;
                }
                if (ErrorMsg == "")
                {
                    ErrorMsg = "SUCCESSFULL";
                }
            }
            catch (Exception ex)
            {
                SaveSystemErrorLog(ex, "[SMS Sent Status]");
                ErrorMsg = "UNSUCCESSFULL";
                //ErrorMsg = "Some error occured while sending mail " + ex.Message;
            }
            return ErrorMsg;
        }
        public string SendEmail(string EmailIdTo, string Subject, string MailBody)
        {
            string ErrorMsg = "";
            string MailSubject = "";
            StringBuilder strMessage = new StringBuilder();
            try
            {
                string GmailUserNameKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GmailUserNameKey"].ToString();
                string GmailPasswordKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GmailPasswordKey"].ToString();
                string GmailHostKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GmailHostKey"].ToString();
                string GmailPortKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GmailPortKey"].ToString();
                string GmailSslKey = System.Web.Configuration.WebConfigurationManager.AppSettings["GmailSslKey"].ToString();

                SmtpClient smtp = new SmtpClient();
                smtp.Host = GmailHostKey;
                smtp.Port = Convert.ToInt16(GmailPortKey);
                smtp.EnableSsl = Convert.ToBoolean(GmailSslKey);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(GmailUserNameKey, GmailPasswordKey);
                try
                {                   
                    MailSubject = Subject;                   
                    using (var message = new MailMessage(GmailUserNameKey, EmailIdTo))
                    {
                        message.Subject = MailSubject;
                        message.Body = MailBody;
                        message.IsBodyHtml = true;
                        smtp.Send(message);
                    }
                }
                catch (Exception ex)
                {
                    SaveSystemErrorLog(ex, "[Email Sent Status]");
                    ErrorMsg = "UNSUCCESSFULL";
                    //ErrorMsg += ErrorMsg + " " + ex;
                }
                if (ErrorMsg == "")
                {
                    ErrorMsg = "SUCCESSFULL";
                }
            }
            catch (Exception ex)
            {
                SaveSystemErrorLog(ex, "[Email Sent Status]");
                ErrorMsg = "UNSUCCESSFULL";
                //ErrorMsg = "Some error occoured while sending email. " + ex.Message + ". " + ex.InnerException;
            }
            return ErrorMsg;
        }
        public string ToOrdinal(int number)
        {
            if (number < 0) return number.ToString();
            int rem = number % 100;
            if (rem >= 11 && rem <= 13) return number + "th";

            switch (number % 10)
            {
                case 1:
                    return number + "st";
                case 2:
                    return number + "nd";
                case 3:
                    return number + "rd";
                default:
                    return number + "th";
            }
        }
        public string ValidateTicketNo(ClsTicketInventory objGeneratedNo, string Ticketno, DataTable dt = null)
        {
            string ErrorMsg = "", AlphabetSeries = "";
            int TicketLength = 0, FnEndLength = 0, AlphabetSeriesLength = 0, TnEndLength = 0;
            string StartNo = "", Alphabet = "", EndNo = "";
            // Check for Ticket Length
            AlphabetSeries = objGeneratedNo.AlphabetSeries;
            string[] values = AlphabetSeries.Split(',');

            string finTktAlphabet = string.Empty;
            long finTktLastSeries = default(long);
            List<ClsSeriesGeneration> lst = new List<ClsSeriesGeneration>();
            List<ClsSeriesGeneration> fltr1 = new List<ClsSeriesGeneration>();

            FnEndLength = (objGeneratedNo.FnEnd == 0 ? 0 : objGeneratedNo.FnEnd.ToString().Length);//objGeneratedNo.FnEnd.ToString().Length;
            TnEndLength = objGeneratedNo.TnEnd.ToString().Length;
            AlphabetSeriesLength = values[values.Length - 1].ToString().Trim().Length;
            if (Ticketno.Trim().Length > 0)
            {
                try
                {
                    finTktAlphabet = Ticketno.Substring(FnEndLength, AlphabetSeriesLength);
                    long.TryParse(Ticketno.Substring((FnEndLength + AlphabetSeriesLength), (Ticketno.Trim().Length - (FnEndLength + AlphabetSeriesLength))), out finTktLastSeries);

                    /*var res = from row in dt.AsEnumerable()
                              where row.Field<long>("NumStart") <= finTktLastSeries && row.Field<long>("NumEnd") > finTktLastSeries
                              select row;*/

                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var obj = new ClsSeriesGeneration();
                            obj.ID = Convert.ToInt64(dt.Rows[i]["ID"]);
                            obj.ReqId = Convert.ToInt32(dt.Rows[i]["ReqId"]);
                            obj.Series1Start = Convert.ToInt64(dt.Rows[i]["Series1Start"]);
                            obj.Series1End = Convert.ToInt64(dt.Rows[i]["Series1End"]);
                            obj.Series2Start = Convert.ToString(dt.Rows[i]["Series2Start"]);
                            obj.Series2End = Convert.ToString(dt.Rows[i]["Series2End"]);
                            obj.NumStart = Convert.ToInt64(dt.Rows[i]["NumStart"]);
                            obj.NumEnd = Convert.ToInt64(dt.Rows[i]["NumEnd"]);
                            lst.Add(obj);
                        }
                        fltr1 = lst.Where(l => l.NumStart <= finTktLastSeries && l.NumEnd >= finTktLastSeries).ToList();
                        if (fltr1.Count() > default(int))
                        {
                            foreach(var item in fltr1)
                            {
                                var alpSries = values.Slice(Array.FindIndex(values, m => m == item.Series2Start), (Array.FindIndex(values, m => m == item.Series2End) + 1));
                                var results = Array.FindAll(alpSries, s => s.Equals(finTktAlphabet));
                                if (results != null && results.Length > default(int))
                                {
                                    objGeneratedNo.FnStart = Convert.ToInt32(item.Series1Start);
                                    objGeneratedNo.FnEnd = Convert.ToInt32(item.Series1End);
                                    objGeneratedNo.TnStart = item.NumStart;
                                    objGeneratedNo.TnEnd = item.NumEnd;
                                    FnEndLength = (objGeneratedNo.FnEnd == 0 ? 0 : objGeneratedNo.FnEnd.ToString().Length);
                                    TnEndLength = objGeneratedNo.TnEnd.ToString().Length;
                                    break;
                                }
                            }
                        }
                    }
                }
                catch { }


                TicketLength = (objGeneratedNo.FnEnd == 0 ? 0 : FnEndLength) + AlphabetSeriesLength + TnEndLength;
                if (Ticketno.Trim().Length != TicketLength)
                {
                    ErrorMsg = "Invalid Ticket No. No of characters in Ticket should be " + TicketLength + " for ticket no " + Ticketno;
                    return ErrorMsg;
                }
                if (objGeneratedNo.FnEnd > 0)
                {
                    StartNo = Ticketno.Substring(0, FnEndLength);

                    if (IsIntegerWithZero(StartNo) == false)
                    {
                        ErrorMsg = "Invalid Ticket No. First " + FnEndLength + " No  sholud be should numeric and less thar or equal to " + objGeneratedNo.FnEnd + " for ticket no " + Ticketno;
                        return ErrorMsg;
                    }

                    if ((Convert.ToInt16(StartNo) <= objGeneratedNo.FnEnd) == false)
                    {
                        ErrorMsg = "Invalid Ticket No. First " + FnEndLength + " No  sholud be should be less than or equal to " + objGeneratedNo.FnEnd + " for ticket no " + Ticketno;
                        return ErrorMsg;
                    }
                }

                bool IsFound = false;
                for (int aCnt = 0; aCnt < values.Length; aCnt++)
                {
                    Alphabet = Ticketno.Substring(FnEndLength, (TicketLength - (FnEndLength + TnEndLength)));
                    if (Alphabet == values[aCnt].ToString().Trim() == true)
                    {
                        IsFound = true;
                        break;
                    }
                }
                if (IsFound == false)
                {
                    ErrorMsg = "Invalid Ticket No. Alphabet charcter sholud be among " + AlphabetSeries + " for ticket no " + Ticketno;
                    return ErrorMsg;
                }

                EndNo = Ticketno.Substring(Ticketno.Length - TnEndLength, TnEndLength);

                if (IsLongIntegerWithZero(EndNo) == false)
                {
                    ErrorMsg = "Invalid Ticket No. Last " + TnEndLength + " No  sholud be should numeric and less thar or equal to " + objGeneratedNo.TnEnd + " for ticket no " + Ticketno;
                    return ErrorMsg;
                }

                if ((Convert.ToInt64(EndNo) <= objGeneratedNo.TnEnd) == false)
                {
                    ErrorMsg = "Invalid Ticket No. Last " + TnEndLength + " No  sholud be should be less than or equal to " + objGeneratedNo.TnEnd + " for ticket no " + Ticketno;
                    return ErrorMsg;
                }
            }
            return ErrorMsg;
        }

        public  string NumberToText(Int64 number)
        {
            if (number == 0) return "Zero";
            Int64[] num = new Int64[4];
            Int64 first = 0;
            Int64 u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (Int64 i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens

                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");

                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
        public string ValidatePartialTicketNo(ClsTicketInventory objGeneratedNo, string Ticketno)
        {
            string ErrorMsg = "", AlphabetSeries = "";
            int TicketLength, FnEndLength = 0, AlphabetSeriesLength = 0, TnEndLength = 0;
            string Alphabet = "";
            // Check for Ticket Length
            AlphabetSeries = objGeneratedNo.AlphabetSeries;
            string[] values = AlphabetSeries.Split(',');

            FnEndLength = (objGeneratedNo.FnEnd == 0 ? 0 : FnEndLength);//objGeneratedNo.FnEnd.ToString().Length;
            AlphabetSeriesLength = values[values.Length - 1].ToString().Trim().Length;
            TnEndLength = objGeneratedNo.TnEnd.ToString().Length;

            if (Ticketno.Trim().Length <= TnEndLength && IsIntegerWithZero(Ticketno))
            {
                if (Convert.ToInt32(Ticketno) > objGeneratedNo.TnEnd)
                {
                    ErrorMsg = "Invalid Ticket No. Ticket no should less than or equal to " + objGeneratedNo.TnEnd;
                    return ErrorMsg;
                }
            }
            bool IsFound = false;
            if (Ticketno.Trim().Length == (AlphabetSeriesLength + TnEndLength) && IsIntegerWithZero(Ticketno))
            {
                for (int aCnt = 0; aCnt < values.Length; aCnt++)
                {
                    Alphabet = Ticketno.Substring(FnEndLength, TnEndLength);
                    if (Alphabet == values[aCnt].ToString().Trim() == true)
                    {
                        IsFound = true;
                        break;
                    }
                }
                if (IsFound == false)
                {
                    ErrorMsg = "Invalid Ticket No. Alphabet charcter sholud be among " + AlphabetSeries + " for ticket no " + Ticketno;
                    return ErrorMsg;
                }
            }

            TicketLength = FnEndLength + AlphabetSeriesLength + TnEndLength;
            if (Ticketno.Trim().Length == TicketLength)
            {
                ErrorMsg = ValidateTicketNo(objGeneratedNo, Ticketno);
                return ErrorMsg;
            }


            return ErrorMsg;
        }
        public Dictionary<String, String> ClaimUploadStatus()
        {
            Dictionary<String, String> objClaimType = new Dictionary<String, String>();
            objClaimType.Add("1", "Pending");
            objClaimType.Add("2", "Confirm by Distributor");
            objClaimType.Add("3", "Rejected by Distributor");
            objClaimType.Add("4", "First Level Approved");
            objClaimType.Add("5", "First Level Rejected");
            objClaimType.Add("6", "Second Level Approved");
            objClaimType.Add("7", "Second Level Rejected");
            return objClaimType;
        }
        public Dictionary<String, String> DealerDepositStatus()
        {
            Dictionary<String, String> objClaimType = new Dictionary<String, String>();
            objClaimType.Add("0", "Draft");
            objClaimType.Add("1", "Confirm");
            objClaimType.Add("2", "Realized");
            objClaimType.Add("3", "Not-Realized");
            objClaimType.Add("4", "Approve");
            objClaimType.Add("5", "Reject");
            return objClaimType;
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Get the array slice between the two indexes.
        /// ... Inclusive for start index, exclusive for end index.
        /// </summary>
        public static T[] Slice<T>(this T[] source, int start, int end)
        {
            // Handles negative ends.
            if (end < 0)
            {
                end = source.Length + end;
            }
            int len = end - start;

            // Return new array.
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
                res[i] = source[i + start];
            }
            return res;
        }
    }
}