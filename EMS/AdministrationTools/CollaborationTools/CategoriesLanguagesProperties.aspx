<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="CategoriesLanguagesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.CT.CategoriesLanguagesProperties" Title="EMS - Category Language Option"
    Culture="auto" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                    <!-- Category -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblCategory" runat="server" Text="Category:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" MaxLength="150" ID="txtCategory"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Required Field"
                                Display="Dynamic" ControlToValidate="txtCategory" SkinID="EMS"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDescription" MaxLength="8000" runat="server" TextMode="MultiLine"
                                Rows="6"></asp:TextBox>
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
