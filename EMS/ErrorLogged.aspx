<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ErrorLogged.aspx.cs"
    Inherits="Condesus.EMS.WebUI.ErrorLogged" Title="Page Error" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="contentMain" ContentPlaceHolderID="contentMain" runat="server">
    <div class="divError">
        <asp:Label ID="lblErrorMessage" runat="server" Text="" CssClass="lblErrorMessage"></asp:Label>
    </div>
    <asp:LinkButton Text="" ID="lnkExtendedInformation" runat="server"></asp:LinkButton>
    <cc1:CollapsiblePanelExtender ID="colpnlExtendedInformation" runat="server" TargetControlID="pnlExtendedInformation"
        ExpandControlID="lblExtendedInformation" CollapseControlID="lblExtendedInformation"
        Collapsed="True" SuppressPostBack="True" EnableViewState="False" Enabled="True" />
    <asp:Panel ID="pnlExtendedInformation" runat="server" Height="0px" Style="overflow: hidden;">
        <asp:Label ID="lblErrorInformation" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblExtendedInformation" runat="server"></asp:Label>
    </asp:Panel>
</asp:Content>
