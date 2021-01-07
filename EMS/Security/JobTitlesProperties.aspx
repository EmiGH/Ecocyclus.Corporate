<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="JobTitlesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.Security.JobTitlesProperties" Title="EMS - Job Title Security Property"
    Culture="auto" UICulture="auto" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server" EnableViewState="true">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server" EnableViewState="true">
    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <table id="tblContentForm" runat="server" class="ContentForm">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                        <col class="ColValidator" />
                    </colgroup>
                    <!-- Organization -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganization2" runat="server" Text="Organization:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phOrganization" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phOrganizationValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Organizational Chart -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganizationalChart" runat="server" Text="Organizational Chart:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upOrganizationalChart" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phOrganizationalChart" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phOrganizationalChartValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Job Title -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblPost" runat="server" Text="Job Title:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phJobTitle" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phJobTitleValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Permission -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblPermission" runat="server" Text="Permission:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phPermission" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phPermissionValidator" runat="server" />
                        </td>
                    </tr>
                    <%--<!-- Role Type -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblRoleType" runat="server" Text="Role Type:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phRoleType" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phRoleTypeValidator" runat="server" />
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
