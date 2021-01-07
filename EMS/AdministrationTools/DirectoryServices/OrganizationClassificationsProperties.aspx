<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="OrganizationClassificationsProperties.aspx.cs" Inherits="Condesus.EMS.WebUI.DS.OrganizationClassificationsProperties" %>

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
                    <!-- Parent -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIdParent" runat="server" Text="Parent:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblParentValue" runat="server" Text="" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganizationClassification" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtOrganizationClassification" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfv1"
                                ControlToValidate="txtOrganizationClassification" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="6" MaxLength="8000" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Language -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLanguage" runat="server" Text="Language:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblLanguageValue" runat="server" />
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
