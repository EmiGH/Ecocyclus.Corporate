<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
    Inherits="Condesus.EMS.WebUI.Default" %>


<asp:Content ID="cntContent" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="content">
        <div id="contentdivPadBackground">
            <!--Abro SiteMap-->
            <asp:SiteMapPath ID="smpEMS" runat="server">
            </asp:SiteMapPath>
            <!--Cierro SiteMap-->
        </div>
        <div id="contentdivWelcome">
        </div>
        <div id="contentdivBackgroundMenuOption">
            <asp:Label runat="server" ID="lblDescription" Text="<%$ Resources:lblOrganization.Text %>"></asp:Label>
        </div>
    </div>
</asp:Content>
