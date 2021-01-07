<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ParametersProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PA.ParametersProperties" Title="EMS - Parameter Property"
    Culture="auto" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>
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
                            <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtDescription" Display="Dynamic"></asp:RequiredFieldValidator>
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
                    <!-- Sign -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblSign" runat="server" Text="Sign:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <rad:RadComboBox ID="ddlSign" runat="server" EnableEmbeddedSkins="false" Skin="EMS">
                                <Items>
                                    <rad:RadComboBoxItem Value="+" Text="Positive" runat="server" />
                                    <rad:RadComboBoxItem Value="=" Text="Neutral" runat="server" />
                                    <rad:RadComboBoxItem Value="-" Text="Negative" runat="server" />
                                </Items>
                                <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                            </rad:RadComboBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Raise Exception -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblException" runat="server" Text="Raise Exception:" />
                        </td>
                        <td class="ColContent">
                            <asp:CheckBox ID="chkRaiseException" SkinID="EMS" runat="server" />
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
