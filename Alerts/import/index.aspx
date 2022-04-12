<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Alerts.Import.Index" %>

<%@ Register TagPrefix="uc" TagName="BootstrapAlert" Src="~/Controls/BootstrapAlert.ascx" %>
<%@ Register TagPrefix="uc" TagName="BootstrapNavigation" Src="~/Controls/BootstrapNavigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Import</title>
    <link rel="stylesheet" href="//ssel-apps.eecs.umich.edu/static/lib/bootstrap/5.0.2/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid mt-3">
            <uc:BootstrapAlert runat="server" ID="BootstrapAlert1" />

            <uc:BootstrapNavigation runat="server" ID="BootstrapNavigation1" CurrentPage="import" />

            <div class="mb-3">
                <label for="txtUrl" class="form-label">File URL</label>
                <asp:TextBox runat="server" ID="txtUrl" CssClass="form-control" ClientIDMode="Static" />
            </div>

            <div class="mb-3 form-check">
                <input type="checkbox" runat="server" id="chkAppend" class="form-check-input" name="append" />
                <label class="form-check-label" for="chkAppend">Append</label>
            </div>

            <asp:Button runat="server" ID="btnImport" OnClick="BtnImport_Click" Text="Import" CssClass="btn btn-primary" />
        </div>
    </form>

    <script src="//ssel-apps.eecs.umich.edu/static/lib/bootstrap/5.0.2/js/bootstrap.bundle.min.js"></script>
</body>
</html>
