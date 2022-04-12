<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Alerts.Index" %>

<%@ Register TagPrefix="uc" TagName="BootstrapAlert" Src="~/Controls/BootstrapAlert.ascx" %>
<%@ Register TagPrefix="uc" TagName="BootstrapNavigation" Src="~/Controls/BootstrapNavigation.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alerts</title>
    <link rel="stylesheet" href="//ssel-apps.eecs.umich.edu/static/lib/bootstrap/5.0.2/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Literal runat="server" ID="litDebug" />
        <div class="container-fluid mt-3">
            <uc:BootstrapAlert runat="server" ID="BootstrapAlert1" />

            <uc:BootstrapNavigation runat="server" ID="BootstrapNavigation1" CurrentPage="home" />

            <button type="button" class="btn btn-outline-primary" data-bs-toggle="modal" data-bs-target="#add_alert_modal">Add Alert</button>

            <hr />

            <input type="hidden" runat="server" id="hidEditId" class="edit-id" />

            <h5>Active Alerts</h5>

            <asp:Literal runat="server" ID="litNoDataActive"></asp:Literal>

            <asp:Repeater runat="server" ID="rptActiveAlerts">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th style="width: 100px;">&nbsp;</th>
                                <th style="width: 100px;">Type</th>
                                <th style="width: 140px;">Location</th>
                                <th style="width: 200px;">Start</th>
                                <th style="width: 200px;">End</th>
                                <th>Text</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lbtnEdit" OnCommand="Row_Command" CommandName="edit" CommandArgument='<%#Eval("Id")%>' Text="e" />
                            |
                            <asp:LinkButton runat="server" ID="lbtnDelete" OnCommand="Row_Command" CommandName="delete" CommandArgument='<%#Eval("Id")%>' Text="d" ForeColor="#ff0000" />
                        </td>
                        <td><%#Eval("Type")%></td>
                        <td><%#Eval("Location")%></td>
                        <td><%#GetDateTimeValue(Eval("StartDate"), "{0:yyyy-MM-dd HH:mm:ss}")%></td>
                        <td><%#GetDateTimeValue(Eval("EndDate"), "{0:yyyy-MM-dd HH:mm:ss}")%></td>
                        <td><%#ClipText(HttpUtility.HtmlEncode(Eval("Text").ToString()), 120)%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <hr />

            <h5>Inactive Alerts
                <span class="text-muted fs-6">(past or future alerts)</span>
            </h5>

            <asp:Literal runat="server" ID="litNoDataInactive"></asp:Literal>

            <asp:Repeater runat="server" ID="rptInactiveAlerts">
                <HeaderTemplate>
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th style="width: 100px;">&nbsp;</th>
                                <th style="width: 100px;">Type</th>
                                <th style="width: 140px;">Location</th>
                                <th style="width: 200px;">Start</th>
                                <th style="width: 200px;">End</th>
                                <th>Text</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:LinkButton runat="server" ID="lbtnEdit" OnCommand="Row_Command" CommandName="edit" CommandArgument='<%#Eval("Id")%>' Text="e" />
                            |
                            <asp:LinkButton runat="server" ID="lbtnDelete" OnCommand="Row_Command" CommandName="delete" CommandArgument='<%#Eval("Id")%>' Text="d" ForeColor="#ff0000" />
                        </td>
                        <td><%#Eval("Type")%></td>
                        <td><%#Eval("Location")%></td>
                        <td><%#GetDateTimeValue(Eval("StartDate"), "{0:yyyy-MM-dd HH:mm:ss}")%></td>
                        <td><%#GetDateTimeValue(Eval("EndDate"), "{0:yyyy-MM-dd HH:mm:ss}")%></td>
                        <td><%#ClipText(HttpUtility.HtmlEncode(Eval("Text").ToString()), 120)%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <!-- Modal -->
            <div class="modal fade" id="add_alert_modal" tabindex="-1" aria-labelledby="add_alert_modal_label" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="add_alert_modal_label">
                                <asp:Literal runat="server" ID="litAddModalTitle">Add Alert</asp:Literal>
                            </h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <uc:BootstrapAlert runat="server" ID="BootstrapAlert2" />
                            <div class="mb-3">
                                <label for="ddlAddAlertType" class="form-label">Alert Type</label>
                                <asp:DropDownList runat="server" ID="ddlAddAlertType" CssClass="form-select" ClientIDMode="Static">
                                    <asp:ListItem Value="info">Info</asp:ListItem>
                                    <asp:ListItem Value="alert">Alert</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="mb-3">
                                <label for="ddlAddAlertLocation" class="form-label">Location</label>
                                <asp:DropDownList runat="server" ID="ddlAddAlertLocation" CssClass="form-select" ClientIDMode="Static">
                                    <asp:ListItem Value="default">Default</asp:ListItem>
                                    <asp:ListItem Value="menu" Selected="True">Menu</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="mb-3">
                                <label for="txtAddAlertStartDateTime" class="form-label">Start Date/Time</label>
                                <asp:TextBox runat="server" ID="txtAddAlertStartDateTime" CssClass="form-control" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3">
                                <label for="txtAddAlertEndDateTime" class="form-label">Start Date/Time</label>
                                <asp:TextBox runat="server" ID="txtAddAlertEndDateTime" CssClass="form-control" ClientIDMode="Static" />
                            </div>
                            <div class="mb-3">
                                <label for="txtAddAlertText" class="form-label">Text</label>
                                <asp:TextBox runat="server" ID="txtAddAlertText" TextMode="MultiLine" CssClass="form-control" Rows="3" ClientIDMode="Static" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                            <asp:Button runat="server" ID="btnAddAlert" CssClass="btn btn-primary modal-save-button" Text="Save" OnClick="BtnAddAlert_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script src="//ssel-apps.eecs.umich.edu/static/lib/bootstrap/5.0.2/js/bootstrap.bundle.min.js"></script>

        <script>
            var modal = document.getElementById('add_alert_modal');

            function clearForm() {
                document.querySelector('.edit-id').value = "";
                modal.querySelector('.modal-title').innerHTML = "Add Alert";
                modal.querySelector('#ddlAddAlertType').selectedIndex = 0;
                modal.querySelector('#ddlAddAlertLocation').selectedIndex = 1;
                modal.querySelector('#txtAddAlertStartDateTime').value = "";
                modal.querySelector('#txtAddAlertEndDateTime').value = "";
                modal.querySelector('#txtAddAlertText').value = "";
                modal.querySelector('.modal-save-button').value = "Save";
            }

            modal.addEventListener('hidden.bs.modal', function (e) {
                clearForm();
            });

        </script>

        <asp:PlaceHolder runat="server" ID="phShowModal" Visible="false">
            <script>
                var m = new bootstrap.Modal(modal, {});
                m.show();
            </script>
        </asp:PlaceHolder>
    </form>
</body>
</html>
