using System;
using System.Web;
using System.Web.UI;

namespace Alerts.Import
{
    public partial class Index : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["url"]))
            {
                bool append = !string.IsNullOrEmpty(Request.QueryString["append"]) && Request.QueryString["append"] == "on";
                txtUrl.Text = Request.QueryString["url"];
                chkAppend.Checked = append;
                ImportFile(Request.QueryString["url"], append);
            }
        }

        protected void ImportFile(string url, bool append)
        {
            BootstrapAlert1.Hide();

            try
            {
                var repo = new Alerts.Repository();
                var count = repo.ImportAlerts(url, append);
                BootstrapAlert1.Show(string.Format("Imported items: {0}", count), "success");
            }
            catch (Exception ex)
            {
                BootstrapAlert1.Show(ex.Message, "danger");
            }
        }

        protected void BtnImport_Click(object sender, EventArgs e)
        {
            string redirectUrl;

            if (string.IsNullOrEmpty(txtUrl.Text))
                redirectUrl = "~/import";
            else
                redirectUrl = string.Format("~/import/?url={0}&append={1}", HttpUtility.UrlEncode(txtUrl.Text), chkAppend.Checked ? "1" : "0");

            Response.Redirect(redirectUrl);
        }
    }
}