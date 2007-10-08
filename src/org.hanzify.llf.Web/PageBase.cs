
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using Lephone.Web.Common;
using Lephone.Data;

namespace Lephone.Web
{
    public class PageBase : Page
    {
        protected internal Dictionary<string, object> bag = new Dictionary<string, object>();
        protected internal string ControllerName;

        protected FlashBox flash = new FlashBox();

        public PageBase()
        {
        }

        protected internal void Print(object o)
        {
            Response.Write(o);
        }

        protected internal void Print(string s)
        {
            Response.Write(s);
        }

        protected internal string LinkTo(string Title, string Controller, string Action, object Paramter)
        {
            string ParamterStr = (Paramter == null) ? null : Paramter.ToString();
            if (string.IsNullOrEmpty(Title))
            {
                throw new DataException("Title can not be null or empty.");
            }
            string ret = string.Format("<a href=\"{0}\">{1}</a>",
                UrlTo(Request.ApplicationPath,
                string.IsNullOrEmpty(Controller) ? ControllerName : Controller,
                Action, ParamterStr), Title);
            return ret;
        }

        internal static string UrlTo(string AppPath, string Controller, string Action, string Paramter)
        {
            StringBuilder url = new StringBuilder();
            url.Append(AppPath).Append("/");
            url.Append(Controller).Append("/");
            if (!string.IsNullOrEmpty(Action))
            {
                url.Append(Action).Append("/");
            }
            if (!string.IsNullOrEmpty(Paramter))
            {
                url.Append(Paramter).Append("/");
            }
            url.Length--;
            if (WebSettings.UsingAspxPostfix)
            {
                url.Append(".aspx");
            }
            return url.ToString();
        }
    }
}