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
    public partial class Default : System.Web.UI.Page
    {
        int iMenuCtr = 0;
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        ValidationAndCommonFunction objValidateData = new ValidationAndCommonFunction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["JB"]))
            {
                Response.Redirect("TrxQrValidation.aspx?JB=" + Request.QueryString["JB"]);
            }
            if (IsPostBack == false)
            {
                BindGvData();
            }
        }
        protected void btnClaim_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrxLotteryClaimBasicDtl.aspx?ID=" + Guid.NewGuid().ToString());
        }
        protected void btnValidateTicket_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrxValidateTicketAnonymous.aspx?ID=" + Guid.NewGuid().ToString());
        }       

        private void BindGvData()
        {
            try
            {
                clsInputParameter objInputParameter = new clsInputParameter();
                objInputParameter.InFromDate = Convert.ToDateTime(DateTime.Now.AddMonths(-2).ToString("dd-MMM-yyyy") + " 00:00:00.000");
                objInputParameter.InToDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy") + " 23:59:59.999");
                objInputParameter.InStatus = Convert.ToInt16(-150);
                GvData.DataSource = objLtmsService.GetRequisitionDtlByStatus(objInputParameter);
                GvData.DataBind();
               
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
                DataTable dtInfo = new DataTable();
                GridViewRow gvrow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                hdUniqueId.Value = GvData.DataKeys[gvrow.RowIndex].Value.ToString();
                if (e.CommandName == "PrintEntry")
                {
                    Session["Allow"] = false;
                    Session["UserInfo"] = null;
                    clsInputParameter objInputParameter = new clsInputParameter();
                    objInputParameter.DataUniqueId = Convert.ToInt64(hdUniqueId.Value);
                   // objInputParameter.RequestUrl = Request.QueryString["ID"].Trim();
                    Session["InputParameter"] = objInputParameter;
                    Session["ReportName"] = "WinneListDtlById";
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
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ClsUserInfo objUserInfo = new ClsUserInfo();
            DataTable dtUserInfo = new DataTable();
            if (IsValidEntry() == false) { return; }//Check for valid data entry
            Session["UserInfo"] = null;
            string MenuList = "";
            dtUserInfo = objLtmsService.GetIsValidUser(txtUserId.Text.Trim(), txtUserPassword.Text.Trim());
            if (dtUserInfo.Rows.Count > 0)
            {
                objUserInfo.UserId = dtUserInfo.Rows[0]["USERID"].ToString();
                objUserInfo.DisplayName = dtUserInfo.Rows[0]["DISPLAYNAME"].ToString();
                objUserInfo.EmailId = dtUserInfo.Rows[0]["EMAILID"].ToString().ToLower().Replace("&nbsp;", "");
                objUserInfo.AccessAllowed = dtUserInfo.Rows[0]["LOCKED"].ToString().ToUpper() == "FALSE" ? true : false;
                objUserInfo.IsFirstTime = dtUserInfo.Rows[0]["IsFirstTime"].ToString().ToUpper() == "TRUE" ? true : false;
                if (objUserInfo.IsFirstTime == true)
                {
                    Response.Redirect("frmChangePassword.aspx?ID=" + Guid.NewGuid().ToString());
                }

                objUserInfo.UserRoleId = Convert.ToInt64(dtUserInfo.Rows[0]["USERROLEID"].ToString());
                bool IsMenuAvailable = BuildMenuList(objUserInfo.UserRoleId, out MenuList);
                objUserInfo.MenuList = MenuList;
                if (objUserInfo.AccessAllowed == false)
                {
                    // lblError.Text = "You have been blocked from using the System.<br>Please contact the Administrator for allowing you the access to the System.";
                }
                else
                {
                    Session["UserInfo"] = objUserInfo;

                    HttpCookie authCookie = new HttpCookie("appToken", Guid.NewGuid().ToString());
                    authCookie.HttpOnly = true;
                    authCookie.Name = "appToken";
                    authCookie.Value = Guid.NewGuid().ToString();
                    Response.AppendCookie(authCookie);
                    Session["AuthToken"] = authCookie.Value;
                    Session["Allow"] = true;
                    // Save Log for Login
                    //        objAppStartup.InsertInSysAudit(objBrandBusnessObjects, "System Access", "System Login Information", "Login", "<font color = 'green'>Successful Log-In in Web Application in " + Module + " Module from IP Address : " + Request.UserHostAddress + "</font>", "SYS_AUDIT");
                    ((ClsUserInfo)Session["UserInfo"]).MenuList = MenuList;
                    try
                    {
                        Response.Redirect("appHome.aspx?ID=" + Guid.NewGuid().ToString(),false);
                       // Server.Transfer("appHome.aspx?ID=" + Guid.NewGuid().ToString(),false);
                    }
                    catch (Exception ex) { 
                    
                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter valid User Id and password.');", true);
                txtUserId.Focus();
                return;
            }
        }
        private bool IsValidEntry()
        {
            if (txtUserId.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter User Id.');", true);
                txtUserId.Focus();
                return false;
            }
            if (txtUserPassword.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "MyMsg", "alert('Please enter Password.');", true);
                txtUserPassword.Focus();
                return false;
            }

            return true;
        }
        public bool BuildMenuList(Int64 UserRoleId, out string MenuList)
        {
            DataTable dtMenuList = new DataTable();
            dtMenuList = objLtmsService.GetNavMenuListForUserRoleId(UserRoleId);
            if (dtMenuList == null)
            {
                MenuList = "";
                return false;
            }
            else
            {
                iMenuCtr = 0;
                System.Text.StringBuilder sbMenuList = new System.Text.StringBuilder();
                sbMenuList.AppendLine(" <ul class='navbar-nav navbar-sidenav' id='exampleAccordion'>");
                //sbMenuList.AppendLine("<ul id='nav'>");
                for (iMenuCtr = 1; iMenuCtr < dtMenuList.Rows.Count; iMenuCtr++)
                {
                    // sbMenuList.AppendLine("  <li><a  href='appNavigate.aspx?ID=" + dtMenuList.Rows[iMenuCtr]["MENUCODE"].ToString().Trim() + "&UID=" + Guid.NewGuid().ToString() + "'>" + dtMenuList.Rows[iMenuCtr]["MENUDESCRIPTION"].ToString().Trim() + "</a></li>");
                    if (dtMenuList.Rows[iMenuCtr]["HASCHILD"].ToString().Trim().ToUpper() == "YES")
                    {                        
                        sbMenuList.AppendLine("<li class='nav-item' data-toggle='tooltip' data-placement='right' title='" + dtMenuList.Rows[iMenuCtr]["MENUDESCRIPTION"].ToString().Trim() + "'>");
                        sbMenuList.AppendLine("        <a class='nav-link nav-link-collapse collapsed' data-toggle='collapse' href='#" + dtMenuList.Rows[iMenuCtr]["MENUCODE"].ToString().Trim() + "' data-parent='#exampleAccordion'>");
                        sbMenuList.AppendLine("            <i class='fa fa-fw " + dtMenuList.Rows[iMenuCtr]["ImageFileName"].ToString().Trim() + "'></i>");
                        sbMenuList.AppendLine("            <span class='nav-link-text'>" + dtMenuList.Rows[iMenuCtr]["MENUDESCRIPTION"].ToString().Trim() + "</span>");
                        sbMenuList.AppendLine("        </a>");
                        sbMenuList.AppendLine("        <ul class='sidenav-second-level collapse' id='" + dtMenuList.Rows[iMenuCtr]["MENUCODE"].ToString().Trim() + "'>");
                        appendChildMenus(dtMenuList, ref sbMenuList);
                        sbMenuList.AppendLine("        </ul>");
                       sbMenuList.AppendLine("</li>");
                    }
                    else
                    {
                        sbMenuList.AppendLine("  <li class='nav-item' data-toggle='tooltip' data-placement='right' title='" + dtMenuList.Rows[iMenuCtr]["MENUDESCRIPTION"].ToString().Trim() + "'>");
                        sbMenuList.AppendLine("       <a class='nav-link' href='appNavigate.aspx?ID=" + dtMenuList.Rows[iMenuCtr]["MENUCODE"].ToString().Trim() + "&UID=" + Guid.NewGuid().ToString() + "'>");
                        sbMenuList.AppendLine("            <i class='fa fa-fw " + dtMenuList.Rows[iMenuCtr]["ImageFileName"].ToString().Trim() + "'></i>");
                        sbMenuList.AppendLine("            <span class='nav-link-text'>" + dtMenuList.Rows[iMenuCtr]["MENUDESCRIPTION"].ToString().Trim() + "</span>");
                        sbMenuList.AppendLine("       </a>");
                        sbMenuList.AppendLine("  </li>");

                        //sbMenuList.AppendLine("  <li><a href='appNavigate.aspx?ID=" + dtMenuList.Rows[iMenuCtr]["MENUCODE"].ToString().Trim() + "&UID=" + Guid.NewGuid().ToString() + "'>" + dtMenuList.Rows[iMenuCtr]["MENUDESCRIPTION"].ToString().Trim() + "</a></li>");
                    }
                   
                }
                
                sbMenuList.AppendLine("</ul>");
                MenuList = sbMenuList.ToString();
                dtMenuList.Dispose();
                return true;
            }
        }
        private void appendChildMenus(DataTable ObjDataTable, ref System.Text.StringBuilder sbMenuList)
        {
            String currID = ObjDataTable.Rows[iMenuCtr]["MENUCODE"].ToString().Trim();
            for (iMenuCtr = iMenuCtr + 1; iMenuCtr < ObjDataTable.Rows.Count; iMenuCtr++)
            {
                if (currID == ObjDataTable.Rows[iMenuCtr]["MENUCODE"].ToString().Trim().Substring(0, currID.Length) && ObjDataTable.Rows[iMenuCtr]["MENUCODE"].ToString().Trim().Length == (currID.Length + 3))
                {
                    if (ObjDataTable.Rows[iMenuCtr]["HASCHILD"].ToString().Trim().ToUpper() == "YES")
                    {
                        sbMenuList.AppendLine("<li class='nav-item' data-toggle='tooltip' data-placement='right' title='" + ObjDataTable.Rows[iMenuCtr]["MENUDESCRIPTION"].ToString().Trim() + "'>");
                        sbMenuList.AppendLine("        <a class='nav-link nav-link-collapse collapsed' data-toggle='collapse' href='#" + ObjDataTable.Rows[iMenuCtr]["MENUCODE"].ToString().Trim() + "' data-parent='#exampleAccordion'>");
                        sbMenuList.AppendLine("            <i class='fa fa-fw " + ObjDataTable.Rows[iMenuCtr]["ImageFileName"].ToString().Trim() + "'></i>");
                        sbMenuList.AppendLine("            <span class='nav-link-text'>" + ObjDataTable.Rows[iMenuCtr]["MENUDESCRIPTION"].ToString().Trim() + "</span>");
                        sbMenuList.AppendLine("        </a>");
                        sbMenuList.AppendLine("        <ul class='sidenav-second-level collapse' id='" + ObjDataTable.Rows[iMenuCtr]["MENUCODE"].ToString().Trim() + "'>");
                        appendChildMenus(ObjDataTable, ref sbMenuList);
                        sbMenuList.AppendLine("        </ul>");
                        sbMenuList.AppendLine("</li>");
                        
                    }
                    else
                    {
                       sbMenuList.AppendLine("  <li><a href='appNavigate.aspx?ID=" + ObjDataTable.Rows[iMenuCtr]["MENUCODE"].ToString().Trim() + "&UID=" + Guid.NewGuid().ToString() + "'>" + ObjDataTable.Rows[iMenuCtr]["MENUDESCRIPTION"].ToString().Trim() + "</a></li>	");
                    }
                }
                else
                {
                    break;
                }
            }
            iMenuCtr--;
        }
    }
}