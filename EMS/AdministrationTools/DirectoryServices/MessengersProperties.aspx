<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="MessengersProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.MessengersProperties" Title="EMS - Messenger Property"
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
                    <!-- Provider -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblProvider" runat="server" Text="Provider:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phProvider" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phProviderValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Application -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblApplication" runat="server" Text="Application:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phApplication" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phApplicationValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Data -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblData" runat="server" Text="Data:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtData" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" SkinID="EMS" Display="Dynamic" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtData"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
