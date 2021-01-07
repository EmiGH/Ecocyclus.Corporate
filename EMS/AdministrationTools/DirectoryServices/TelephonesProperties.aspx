<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="TelephonesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.TelephonesProperties" Title="EMS - Telephone Property"
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
                    <!-- International Dialing Code -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblInternationalCode" runat="server" Text="International Dialing Code:"
                                />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtInternationalCode"  runat="server"
                                MaxLength="4" ></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator ID="rev" runat="server" ControlToValidate="txtInternationalCode"
                                Display="Dynamic" ErrorMessage="&larr; Invalid Data" SetFocusOnError="True" SkinID="Validator"
                                ValidationExpression="^[0-9]{4}$" ></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <!-- Area Code -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblAreaCode" runat="server" Text="Area Code:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtAreaCode"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Number -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblNumber" runat="server" Text="Number:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="EMS" runat="server" Display="Dynamic" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtNumber"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Extension -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblExtension" runat="server" Text="Extension:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtExtension" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Reason -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblReason" runat="server" Text="Reason:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtReason" TextMode="MultiLine" Rows="6" MaxLength="8000" ></asp:TextBox>
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
