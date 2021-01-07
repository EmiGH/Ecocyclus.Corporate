<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="FunctionalAreasLanguagesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.FunctionalAreasLanguagesProperties" Title="EMS - Functional Area Language Option"
    Culture="auto" UICulture="auto" %>

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
                    <!-- Functional Area -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFunctionalAreas" runat="server" Text="Functional Area Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtFunctionalAreas" MaxLength="50"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ErrorMessage="Required Field"
                                ControlToValidate="txtFunctionalAreas" SkinID="EMS"></asp:RequiredFieldValidator>
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
