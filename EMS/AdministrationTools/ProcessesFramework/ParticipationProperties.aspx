<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ParticipationProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.ManagementTools.ProcessesMap.ParticipationProperties"
    Title="EMS - Participation Property" Culture="auto" UICulture="auto" %>

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
                    <!-- Participation Type -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblRelationshipType" runat="server" Text="Participation Type:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phParticipationType" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phParticipationTypeValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Organization -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganization" runat="server" Text="Organization:" Visible="false" />
                            <asp:Label ID="lblProject" runat="server" Text="Project:" Visible="false" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phOrganization" runat="server" />
                            <asp:PlaceHolder ID="phProject" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phOrganizationValidator" runat="server" />
                            <asp:PlaceHolder ID="phProjectValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Comment -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblComment" runat="server" Text="Comment:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtComment" Rows="6" MaxLength="8000" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
