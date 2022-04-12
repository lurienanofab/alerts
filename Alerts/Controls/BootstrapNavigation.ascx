<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BootstrapNavigation.ascx.cs" Inherits="Alerts.Controls.BootstrapNavigation" %>

<asp:PlaceHolder runat="server" ID="phReturnTo" Visible="false">
    <div class="mb-3">
        <asp:HyperLink runat="server" ID="hypReturnTo">&larr; Return to main menu</asp:HyperLink>
    </div>
</asp:PlaceHolder>

<asp:Repeater runat="server" ID="rptNav">
    <HeaderTemplate>
        <ul class="nav nav-pills mb-3">
    </HeaderTemplate>
    <ItemTemplate>
            <li class="nav-item">
                <asp:HyperLink runat="server" ID="hypNavLink" CssClass='<%#Eval("CssClass")%>' NavigateUrl='<%#Eval("Url")%>' Text='<%#Eval("Text")%>' Target='<%#Eval("Target")%>' />
            </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>

<hr />