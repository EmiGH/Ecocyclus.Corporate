<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="EmailsProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.EmailsProperties" Title="EMS - Email Property"
    Culture="auto" UICulture="auto" %>

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
                    <!-- E-Mail -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblEmail" runat="server" Text="E-Mail:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtEmail"
                                SkinID="EMS" ErrorMessage="Required Field" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmails" ControlToValidate="txtEmail" runat="server" 
                                ErrorMessage="You must enter an valid email address. eg.: he_llo@worl.d.com | hel.l-o@wor-ld.museum | h1ello@123.com" 
                                ValidationExpression="^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
