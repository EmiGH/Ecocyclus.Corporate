<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="SecurityPersonProperties.aspx.cs" 
Inherits="Condesus.EMS.WebUI.Security.SecurityPersonProperties" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
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
                    <!-- Person -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblPerson" runat="server" Text="Person:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upPerson" runat="server">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phPerson" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phPersonValidator" runat="server" />
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
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
