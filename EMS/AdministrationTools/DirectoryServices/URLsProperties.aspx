<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="URLsProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.URLsProperties" Title="EMS - Url Property" Culture="auto"
    UICulture="auto" %>

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
                    <!-- Contact Type -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblContactType" runat="server" Text="Contact Type:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phContactType" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phContactTypeValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Url -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblUrl" runat="server" Text="Url:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtUrl" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" SkinID="EMS" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtUrl" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblName" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" SkinID="EMS" ErrorMessage="Required Field"
                                ID="rfv2" ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDescription" Rows="6" MaxLength="8000" runat="server" TextMode="MultiLine"></asp:TextBox>
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
