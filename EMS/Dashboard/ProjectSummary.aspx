<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ProjectSummary.aspx.cs"
    Inherits="Condesus.EMS.WebUI.Dashboard.ProjectSummary" Title="Project Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="content">
        <div id="contentdivPadBackground">
            <!--Abro SiteMap-->
            <asp:SiteMapPath ID="smpEMS" runat="server">
            </asp:SiteMapPath>
            <!--Cierro SiteMap-->
        </div>
        <div id="contentdivContainerTitle">
            <div id="contentdivContainerTitleIcon">
            </div>
            <div id="contentdivContainerTitleTitle">
                <asp:Label ID="lblTitle" CssClass="contentdivContainerTitleTitle" runat="server"
                    Text="Project Summary" />
                <asp:Label ID="lblSubTitle" CssClass="contentdivContainerSubTitle" runat="server"
                    Text="<%$ Resources:CommonProperties, lblSubtitle %>" Height="13px" />
            </div>
            <asp:Label CssClass="contentdivContainerDetail" ID="lblDetail" runat="server" Text="Proyect Summary detail page" />
        </div>
            <div class="contentmenu">
        <div id="contentmenudivBackground">
            <div id="contentmenudivPosition">
            </div>
        </div>
    </div>
    <asp:Panel ID="pnlSummary" runat="server">
    </asp:Panel>
        
        
    </div>

</asp:Content>
