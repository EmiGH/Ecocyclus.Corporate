<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/EMS.Master" CodeBehind="Default.aspx.cs"
    Inherits="Condesus.EMS.WebUI.IA.Default" culture="auto" uiculture="auto" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="content">
        <div id="contentdivPadBackground">
            <asp:SiteMapPath ID="smpEMS" runat="server">
            </asp:SiteMapPath>
        </div>
        <div id="contentdivBackgroundMenuOption">
            <asp:Label runat="server" ID="lblOrganization" Text=""></asp:Label>
        </div>
        <asp:Label runat="server" ID="lblTitleDescription" CssClass="contentdivDefaultTitleDescription"
            Text=""></asp:Label>
        <asp:Label runat="server" ID="lblDescription" CssClass="contentdivDefaultDescription" Text=""></asp:Label>
    </div>
</asp:Content>
