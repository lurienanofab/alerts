using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Alerts
{
    public partial class Index : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtAddAlertStartDateTime.Attributes.Add("placeholder", "YYYY-MM-DD hh:mm:ss");
            txtAddAlertEndDateTime.Attributes.Add("placeholder", "YYYY-MM-DD hh:mm:ss");

            if (!Page.IsPostBack)
            {
                txtAddAlertStartDateTime.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");
                txtAddAlertEndDateTime.Text = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd");

                if (!string.IsNullOrEmpty(Request.QueryString["ReturnTo"]))
                {
                    Session["ReturnTo"] = Request.QueryString["ReturnTo"];
                    Response.Redirect("~");
                }
            }

            LoadAlerts();
        }

        protected void LoadAlerts()
        {
            litNoDataActive.Text = string.Empty;
            litNoDataInactive.Text = string.Empty;

            var repo = new Alerts.Repository();

            var alerts = repo.GetAllAlerts();
            var active = alerts.Where(x => IsActive(x)).OrderByDescending(x => x.StartDate).ThenByDescending(x => x.EndDate).ToList();
            var inactive = alerts.Where(x => !IsActive(x)).OrderByDescending(x => x.StartDate).ThenByDescending(x => x.EndDate).ToList();

            if (active.Count == 0)
            {
                litNoDataActive.Text = "<em class=\"text-muted\">No active alerts were found</em>";
            }
            else
            {
                rptActiveAlerts.DataSource = active;
                rptActiveAlerts.DataBind();
            }

            if (inactive.Count == 0)
            {
                litNoDataInactive.Text = "<em class=\"text-muted\">No inactive alerts were found</em>";
            }
            else
            {
                rptInactiveAlerts.DataSource = inactive;
                rptInactiveAlerts.DataBind();
            }
        }

        protected void BtnAddAlert_Click(object sender, EventArgs e)
        {
            BootstrapAlert1.Hide();
            BootstrapAlert2.Hide();
            HideModal();

            try
            {
                var repo = new Alerts.Repository();

                var alertType = ddlAddAlertType.SelectedValue;
                var location = ddlAddAlertLocation.SelectedValue;
                var sd = GetAddAlertStartDateTime();
                var ed = GetAddAlertEndDateTime();

                if (sd >= ed)
                    throw new Exception("Start date/time must be before end date/time.");

                var text = txtAddAlertText.Text;

                if (string.IsNullOrEmpty(text))
                    throw new Exception("Alert text is required.");

                if (string.IsNullOrEmpty(hidEditId.Value))
                    repo.AddAlert(alertType, location, sd, ed, text);
                else
                    repo.ModifyAlert(hidEditId.Value, alertType, location, sd, ed, text);

                Response.Redirect("~");
            }
            catch (Exception ex)
            {
                BootstrapAlert2.Show(ex.Message, "danger");
                ShowModal();
            }
        }

        protected bool IsActive(AlertItem alert)
        {
            var now = DateTime.Now;
            var sd = alert.StartDate.ToLocalTime();
            var ed = alert.EndDate.ToLocalTime();

            var result = now >= sd && now < ed;

            return result;
        }

        protected void Row_Command(object sender, CommandEventArgs e)
        {
            BootstrapAlert1.Hide();
            BootstrapAlert2.Hide();
            HideModal();

            try
            {
                var repo = new Alerts.Repository();

                if (e.CommandName == "edit")
                {
                    var alert = repo.GetAlert(e.CommandArgument);

                    ddlAddAlertType.SelectedValue = alert.Type;
                    ddlAddAlertLocation.SelectedValue = alert.Location;
                    txtAddAlertStartDateTime.Text = alert.StartDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    txtAddAlertEndDateTime.Text = alert.EndDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    txtAddAlertText.Text = alert.Text;

                    hidEditId.Value = e.CommandArgument.ToString();
                    litAddModalTitle.Text = "Edit Alert";
                    btnAddAlert.Text = "Modify";

                    ShowModal();
                }

                if (e.CommandName == "delete")
                {
                    repo.DeleteAlert(e.CommandArgument);
                    Response.Redirect("~");
                }
            }
            catch (Exception ex)
            {
                BootstrapAlert1.Show(ex.Message, "danger");
            }
        }

        protected DateTime GetAddAlertStartDateTime()
        {
            if (DateTime.TryParse(txtAddAlertStartDateTime.Text, out DateTime result))
                return result;
            else
                throw new Exception("Invalid start date/time.");
        }

        protected DateTime GetAddAlertEndDateTime()
        {
            if (DateTime.TryParse(txtAddAlertEndDateTime.Text, out DateTime result))
                return result;
            else
                throw new Exception("Invalid end date/time.");
        }

        protected void ShowModal()
        {
            phShowModal.Visible = true;
        }

        protected void HideModal()
        {
            phShowModal.Visible = false;
        }

        protected string GetDateTimeValue(object obj, string format)
        {
            if (DateTime.TryParse(obj.ToString(), out DateTime d))
                return string.Format(format, d.ToLocalTime());
            else
                return "invalid datetime";
        }

        protected string ClipText(string text, int len)
        {
            if (text.Length > len)
                return text.Substring(0, len) + "...";
            else
                return text;
        }
    }
}