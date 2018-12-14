using LTMSWebApp.LTMSServiceRef;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace LTMSWebApp
{
    /// <summary>
    /// Summary description for hxDownloader
    /// </summary>
    public class hxDownloader : IHttpHandler
    {
        /*public void ProcessRequest(HttpContext context)
        {
            string user = context.Request.QueryString["user"];
            string file = context.Request.QueryString["key"];
            try
            {
                if (!string.IsNullOrEmpty(file) && !string.IsNullOrEmpty(user) && File.Exists(context.Server.MapPath(string.Format("~/Downloads/zip/{0}/{1}.zip", user, file))))
                {
                    try
                    {
                        string directoryPath = context.Server.MapPath(string.Format("~/Downloads/{0}/{1}/", user, file));
                        DirectoryInfo di = new DirectoryInfo(directoryPath);
                        foreach (FileInfo f in di.GetFiles())
                            f.Delete();
                    }
                    catch { }

                    context.Response.Clear();
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(string.Format("{0}.zip", file)));
                    //context.Response.WriteFile(context.Server.MapPath(string.Format("~/Downloads/zip/{0}/{1}.zip", user, file)));
                    //context.Response.End();
                    context.Response.TransmitFile(context.Server.MapPath(string.Format("~/Downloads/zip/{0}/{1}.zip", user, file)));
                    context.Response.Flush();
                }
                else
                    throw new Exception("File not be found!");
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.Write(JsonConvert.SerializeObject(new { Message = ex.Message }));
            }
            finally
            {
                if (File.Exists(context.Server.MapPath(string.Format("~/Downloads/zip/{0}/{1}.zip", user, file))))
                {
                    File.Delete(context.Server.MapPath(string.Format("~/Downloads/zip/{0}/{1}.zip", user, file)));
                }
            }
        }*/

        public void ProcessRequest(HttpContext context)
        {
            string user = context.Request.QueryString["user"];
            string file = context.Request.QueryString["key"];
            try
            {
                if (!string.IsNullOrEmpty(file) && !string.IsNullOrEmpty(user) && File.Exists(context.Server.MapPath(string.Format("~/Downloads/merged/{0}/{1}.txt", user, file))))
                {
                    try
                    {
                        string directoryPath = context.Server.MapPath(string.Format("~/Downloads/{0}/{1}/", user, file));
                        DirectoryInfo di = new DirectoryInfo(directoryPath);
                        foreach (FileInfo f in di.GetFiles())
                            f.Delete();
                    }
                    catch { }

                    context.Response.Clear();
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(string.Format("{0}.txt", file)));
                    //context.Response.WriteFile(context.Server.MapPath(string.Format("~/Downloads/zip/{0}/{1}.zip", user, file)));
                    //context.Response.End();
                    context.Response.TransmitFile(context.Server.MapPath(string.Format("~/Downloads/merged/{0}/{1}.txt", user, file)));
                    context.Response.Flush();
                }
                else
                    throw new Exception("File not be found!");
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.Write(JsonConvert.SerializeObject(new { Message = ex.Message }));
            }
            finally
            {
                if (File.Exists(context.Server.MapPath(string.Format("~/Downloads/merged/{0}/{1}.txt", user, file))))
                {
                    File.Delete(context.Server.MapPath(string.Format("~/Downloads/merged/{0}/{1}.txt", user, file)));
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}