<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="LanguagesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.LanguagesProperties" Culture="auto" UICulture="auto"
    Title="EMS - Language Property" %>

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
                    <!-- Id -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblID" runat="server" Text="Id:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtIdValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ErrorMessage="Required Field"
                                ControlToValidate="txtIdValue" SkinID="EMS"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Language -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLanguage" runat="server" Text="Language:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtLanguage" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ErrorMessage="Required Field"
                                ControlToValidate="txtLanguage" SkinID="EMS"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Default -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDefault" runat="server" Text="Default Language:" />
                        </td>
                        <td class="ColContent">
                            <div class="Check">
                                <asp:CheckBox ID="chkDefault" runat="server" Enabled="False"></asp:CheckBox>
                            </div>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Enabled -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblEnabled" runat="server" Text="Enabled:" />
                        </td>
                        <td class="ColContent">
                            <div class="Check">
                                <asp:CheckBox ID="chkEnabled" runat="server"></asp:CheckBox>
                            </div>
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
