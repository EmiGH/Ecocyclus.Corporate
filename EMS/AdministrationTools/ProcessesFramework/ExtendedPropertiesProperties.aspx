<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ExtendedPropertiesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PF.ExtendedPropertiesProperties" Title="EMS - Extended Properties Property"
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
                    <!-- Classification -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblExtendedPropertyClassification" runat="server" Text="Classification:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phExtendedPropertyClassification" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label CssClass="contentformspanTitle" ID="lblName" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtName" SkinID="EMS" runat="server" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="EMS" runat="server" Display="Dynamic" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="8000" TextMode="MultiLine"
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
