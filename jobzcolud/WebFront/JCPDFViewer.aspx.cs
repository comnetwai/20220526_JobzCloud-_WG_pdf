using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jobzcolud.pdf;
using Service;
using System.Data;
using Common;

namespace jobzcolud.WebFront
{
    public partial class JCPDFViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Page.Title = "見積書";
                if (Session["PDFMemoryStream"] != null)
                {
                    if(Session["UriagePDF"].ToString() == "true")
                    {
                        String id = Request.QueryString["id"];
                        DataTable dt_db = JC01Login_Class.Get_DB(id);
                        String db = dt_db.Rows[0]["db"].ToString();
                        String mail = dt_db.Rows[0]["mail"].ToString();
                        ConstantVal.DB_NAME = db;

                        JC27UriageTouroku_Class getinoviceseikyusho = new JC27UriageTouroku_Class();
                        DataTable dt = getinoviceseikyusho.GetInoviceSeikyusho();

                        String sINVOICE = dt.Rows[0]["sINVOICE"].ToString();
                        String sSEIKYUSHO= dt.Rows[0]["sSEIKYUUSHO"].ToString();
                        String cURIAGE = "";
                        if(Session["uriageCode"].ToString() == "true")
                        {
                            cURIAGE = JC34UriageList.uriagecode;
                        }
                        else
                        {
                            cURIAGE = JC27UriageTouroku.curiage;
                        }
                        var date = DateTime.Now;
                        var datenow = date.ToString("yyyyMMdd");
                       
                        String filename = "";
                        if (sINVOICE != "" && sSEIKYUSHO != "")
                        {
                            filename = sINVOICE + "+" + sSEIKYUSHO + cURIAGE + "_" + datenow;                           
                        }
                        else
                        {
                            filename = sINVOICE + sSEIKYUSHO + cURIAGE + "_" + datenow;
                        }
                       
                        MemoryStream ms = new MemoryStream();
                        ms = Session["PDFMemoryStream"] as MemoryStream;
                        HttpResponse response = HttpContext.Current.Response;
                       
                        response.Clear();
                        response.ClearHeaders();
                        response.ClearContent();
                        filename = HttpUtility.UrlPathEncode(filename + ".pdf");
                        response.ContentType = "application/pdf";
                        response.AddHeader("CONTENT-DISPOSITION", "inline;filename=" + filename);
                        response.AddHeader("Content-Length", ms.Length.ToString());
                        Response.AddHeader("Content-Type", "application/pdf");
                        response.BinaryWrite(ms.ToArray());
                        response.Flush();
                        response.End();
                    }
                    else
                    {
                        String filename = Session["PDFFileName"].ToString();
                        MemoryStream ms = new MemoryStream();
                        ms = Session["PDFMemoryStream"] as MemoryStream;
                        HttpResponse response = HttpContext.Current.Response;
                        response.Clear();
                        response.ClearHeaders();
                        response.ClearContent();
                        filename = HttpUtility.UrlPathEncode(filename + ".pdf").Replace('+', ' ');
                        response.ContentType = "application/pdf";
                        response.AddHeader("CONTENT-DISPOSITION", "inline;filename=" + filename);
                        response.AddHeader("Content-Length", ms.Length.ToString());
                        Response.AddHeader("Content-Type", "application/pdf");
                        response.BinaryWrite(ms.ToArray());
                        response.Flush();
                        response.End();
                    }
                    
                }
            }
            Session.Timeout = 480;
        }
    }
}