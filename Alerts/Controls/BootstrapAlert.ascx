<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BootstrapAlert.ascx.cs" Inherits="Alerts.Controls.BootstrapAlert" %>

<asp:PlaceHolder runat="server" ID="phAlert" Visible="false">
    <div runat="server" id="divAlert" class="alert alert-danger" role="alert">
        <asp:Literal runat="server" ID="litAlert"></asp:Literal>
    </div>
</asp:PlaceHolder>
