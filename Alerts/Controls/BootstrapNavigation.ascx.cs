using System;
using System.Web;
using System.Web.UI;

namespace Alerts.Controls
{
    public partial class BootstrapNavigation : UserControl
    {
        public string CurrentPage { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            phReturnTo.Visible = false;

            if (Session["ReturnTo"] != null)
            {
                string returnTo = Session["ReturnTo"].ToString();
                if (!string.IsNullOrEmpty(returnTo))
                {
                    phReturnTo.Visible = true;
                    hypReturnTo.NavigateUrl = returnTo;
                }
            }

            var items = new[]
            {
            new { Text = "home", Url = VirtualPathUtility.ToAbsolute("~"), CssClass = GetCssClass("home"), Target = string.Empty },
            new { Text = "import", Url = VirtualPathUtility.ToAbsolute("~/import"), CssClass = GetCssClass("import"), Target = string.Empty },
            new { Text = "json", Url = VirtualPathUtility.ToAbsolute("~/alerts.json"), CssClass = GetCssClass("json"), Target = "_blank" }
        };

            rptNav.DataSource = items;
            rptNav.DataBind();
        }

        protected string GetCssClass(string key)
        {
            return (key == CurrentPage) ? "nav-link active" : "nav-link";
        }
    }
}