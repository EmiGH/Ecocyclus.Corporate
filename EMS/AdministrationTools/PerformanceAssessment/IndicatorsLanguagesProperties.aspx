<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="IndicatorsLanguagesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PA.IndicatorsLanguagesProperties" Title="EMS - Indicator Language Option"
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
                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblVerificationEntities" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtName" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator Display="Dynamic" ID="rfv1" runat="server" ErrorMessage="Required Field"
                                ControlToValidate="txtName" SkinID="EMS"></asp:RequiredFieldValidator>
                            <asp:CustomValidator Display="Dynamic" SkinID="EMS" ID="rfv2" runat="server" ErrorMessage="Invalid Special Characters"
                                ControlToValidate="txtName" ClientValidationFunction="CheckIndexesTags"></asp:CustomValidator>
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
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtDescription" MaxLength="8000" Rows="6" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Scope -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblScope" runat="server" Text="Scope:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtScope" TextMode="MultiLine" Rows="6" MaxLength="8000" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                    
                    <!-- Limitation -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLimitation" runat="server" Text="Limitation:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtLimitation" TextMode="MultiLine" Rows="6" MaxLength="8000" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                    
                    <!-- Definition -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDefinition" runat="server" Text="Definition:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDefinition" TextMode="MultiLine" Rows="6" MaxLength="8000" runat="server"></asp:TextBox>
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
