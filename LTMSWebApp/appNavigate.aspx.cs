using LTMSClass;
using LTMSWebApp.LTMSServiceRef;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LTMSWebApp
{
    public partial class appNavigate : System.Web.UI.Page
    {
        LTMSServiceClient objLtmsService = new LTMSServiceClient();
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((bool)Session["Allow"] == false)            //Check whether the Session is Valid or not
            {
                Session["CustomError"] = "Your session has expired. Cannot Continue.";
                Response.Redirect("AppSessionExpired.htm");
                //Server.Transfer("appError.aspx?ID=" + Guid.NewGuid().ToString());
            }
            else
            {
                Session["FromNavigation"] = false;  //Variable to force that the the page has been called from the normal MENU operation
                Session["MenuInfo"] = null;
               // Session["MenuInfoAudit"] = null;
                //Session["FileUploadParams"] = null;
                //Session["DataUID"] = "";
                //Initialise the Session Variables used as Temporary Placeholders
                String PageToNavigate = String.Empty;
                PageToNavigate = Request.QueryString["ID"].Trim();
                //string isAudit = "";
                //try
                //{
                //    isAudit = Request.QueryString["ISAUDIT"].ToString().Trim().ToUpper();
                //}
                //catch (Exception ex)
                //{ }

                String ParamString = String.Empty;
                if (PageToNavigate.ToUpper() == "ERROR" || PageToNavigate.Trim().Length == 0 )
                {
                    Session["CustomError"] = "Invalid navigation option. Please try again";
                    Server.Transfer("appError.aspx?ID=" + Guid.NewGuid().ToString() + "&ID=" + DateTime.Now.ToString());
                }
                else
                {

                    string authToken = "";
                    try
                    {
                        authToken = Request.Cookies["appToken"].Value.Trim();
                    }
                    catch (Exception ex)
                    {
                        Session["CustomError"] = "Your session Token has expired. Cannot Continue.";
                        Server.Transfer("appError.aspx");
                        return;
                    }
                    if (authToken != Session["AuthToken"].ToString().Trim() || (authToken.Length == 0))
                    {
                        Session["Allow"] = false;
                        Session.Abandon();
                        Server.Transfer("appError.aspx?XX=Your session Token has expired. Cannot Continue.");
                        return;
                    }
                    if (PageToNavigate.Trim().ToUpper() == "LOGOUT")
                    {
                        //objBusnessObjects.UserIDInfo = ((UserInfo)Session["UserInfo"]).UserID;
                        //objBusnessObjects.UserDisplayNameInfo = ((UserInfo)Session["UserInfo"]).UserName;
                        //objBusnessObjects.ModuleInfo = ((UserInfo)Session["UserInfo"]).Module;
                        //objBusnessObjects.LocationInfo = ((UserInfo)Session["UserInfo"]).FactCode;
                        //objBusnessObjects.IpAddress = Request.UserHostAddress;
                        //objAppNavigate.InsertInSysAudit(objBusnessObjects, "System Access", "System Log out Information", "Log Out", "<font color = 'green'>Successful Log-Out from web application from IP Address : " + Request.UserHostAddress + "</font>", "SYS_AUDIT");

                        //// VISITOR_ENTRY objBal = new VISITOR_ENTRY();
                        //objBal.SaveLogInfo(Session["USER_ID"].ToString(), "<font color = 'green'>Successful Log-Out from web application from IP Address : " + Request.UserHostAddress + "</font>");
                        Session["AuthToken"] = "";
                        if (Response.Cookies["appToken"] != null)
                        {
                            Response.Cookies["appToken"].Value = "";
                            Response.Cookies["appToken"].Expires = DateTime.Now.AddMonths(-100);
                            Response.AppendCookie(Response.Cookies["appToken"]);
                        }
                        Session.Clear();
                        Session.Abandon();
                        Session.RemoveAll();
                        Response.Redirect("Default.aspx?ID=" + Guid.NewGuid().ToString() + "&DT=" + DateTime.Now);
                    }
                    else if (PageToNavigate.Trim().ToUpper() == "HOME")
                    {
                        Server.Transfer("Default.aspx?ID=" + Guid.NewGuid().ToString() + "&DT=" + DateTime.Now);
                    }
                    else
                    {
                        Session["CustomError"] = "";

                        //Check whether the access for the specified MENU is available to the User
                        //Also check that the requested page is available in the SYstem Schema or not.

                        ClsMenuInfo objMenuDetails = new ClsMenuInfo();
                        DataTable ObjDataTable = new DataTable();
                        ObjDataTable = objLtmsService.GetIsMenuAccessAvailable(((ClsUserInfo)Session["UserInfo"]).UserRoleId, PageToNavigate);
                        if (ObjDataTable.Rows.Count > 0)
                        {
                            objMenuDetails.MenuCode = ObjDataTable.Rows[0]["MENUCODE"].ToString();
                            objMenuDetails.MenuDesc = ObjDataTable.Rows[0]["MENUDESCRIPTION"].ToString();
                            objMenuDetails.PageToNavigate = ObjDataTable.Rows[0]["PageToNavigate"].ToString();
                            objMenuDetails.AllowEntry = ObjDataTable.Rows[0]["EntryAccessAllowed"].ToString() == "Y" ? true : false;
                            objMenuDetails.AllowEdit = ObjDataTable.Rows[0]["EditAccessAllowed"].ToString() == "Y" ? true : false;
                            objMenuDetails.AllowDelete = ObjDataTable.Rows[0]["DeleteAccessAllowed"].ToString() == "Y" ? true : false;
                            objMenuDetails.AllowView = ObjDataTable.Rows[0]["ViewAccessAllowed"].ToString() == "Y" ? true : false;
                            ObjDataTable.Dispose();

                            if (objMenuDetails != null)
                            {
                                Session["MenuInfo"] = objMenuDetails;
                                if (!(objMenuDetails.AllowEntry || objMenuDetails.AllowEdit || objMenuDetails.AllowDelete || objMenuDetails.AllowView))
                                {
                                    Session["CustomError"] = "You do not have proper privilege for the selected page.<br><br><br>Please contact the Administrator for further assistance.";
                                    Server.Transfer("appError.aspx?ID=" + Guid.NewGuid().ToString() + "&DT=" + DateTime.Now);
                                }
                                else
                                {
                                    ParamString = "?ID=" + Guid.NewGuid().ToString();
                                    Session["FromNavigation"] = true;

                                    if (System.IO.File.Exists(Server.MapPath(objMenuDetails.PageToNavigate)))
                                    {
                                       
                                        Server.Transfer(objMenuDetails.PageToNavigate);

                                    }
                                    else
                                    {
                                        Session["CustomError"] = "The selected page is either Under Construction or is Inaccessible.<br><br><br>Please contact the Administrator for further assistance.";
                                        Server.Transfer("appError.aspx?ID=" + Guid.NewGuid().ToString() + "&DT=" + DateTime.Now);
                                    }
                                }
                            }
                            else
                            {
                                Session["CustomError"] = "You need to have Viewer privilege for the requested page.";
                                Server.Transfer("appError.aspx?ID=" + Guid.NewGuid().ToString() + "&ID=" + DateTime.Now.ToString());
                            }
                        }
                        else
                        {
                            Session["CustomError"] = "You need to have Viewer privilege for the requested page.";
                            Server.Transfer("appError.aspx?ID=" + Guid.NewGuid().ToString() + "&ID=" + DateTime.Now.ToString());
                        }
                    }
                }
            }
        }
    }
}