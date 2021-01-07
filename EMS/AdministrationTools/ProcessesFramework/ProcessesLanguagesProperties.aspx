<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ProcessesLanguagesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PF.ProcessesLanguagesProperties" Title="EMS - Project Language Option"
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
                    <!-- Title -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblProcessTitle" runat="server" Text="Title:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ValidationGroup="ProcessProperties" MaxLength="150" ID="txtProcessTitle"
                                SkinID="EMS"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="Required Field"
                                Display="Dynamic" ControlToValidate="txtProcessTitle" SkinID="EMS" ValidationGroup="ProcessProperties"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Purpose -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblPurpose" runat="server" Text="Purpose:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtPurpose"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtDescription" MaxLength="8000" TextMode="MultiLine"
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
