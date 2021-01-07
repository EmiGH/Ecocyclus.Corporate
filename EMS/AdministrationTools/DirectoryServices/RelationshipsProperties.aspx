<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="RelationshipsProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.RelationshipsProperties" Title="EMS - Organization Relationship Property"
    EnableEventValidation="false" Culture="auto" UICulture="auto" %>

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
                    <!-- Relationship Type -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblRelationshipType" runat="server" Text="Relationship Type:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phRelationshipType" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phRelationshipTypeValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Organization -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganization2" runat="server" Text="Organization:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phOrganization2" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phOrganization2Validator" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
