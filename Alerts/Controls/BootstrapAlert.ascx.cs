using System.Web.UI;

namespace Alerts.Controls
{
    public partial class BootstrapAlert : UserControl
    {
        public void Hide()
        {
            phAlert.Visible = false;
        }

        public void Show(string text, string alertType)
        {
            phAlert.Visible = !string.IsNullOrEmpty(text);

            if (phAlert.Visible)
            {
                divAlert.Attributes["class"] = string.Format("alert alert-{0}", alertType);
                litAlert.Text = text;
            }
        }
    }
}