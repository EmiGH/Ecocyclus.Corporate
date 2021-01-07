<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="PeopleProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.PeopleProperties" Title="EMS - Person Property"
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
                    <!-- Organization -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganization" runat="server" Text="Organization:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblOrganizationValue" runat="server" Text="" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Salutation -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblAddressingType" runat="server" Text="Salutation:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phSalutationType" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phSalutationTypeValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Last Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtLastName" runat="server" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ControlToValidate="txtLastName" SkinID="EMS" runat="server"
                                ErrorMessage="Required Field" ID="rfv1" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- First Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFirstName" runat="server" Text="First Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtFirstName" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ControlToValidate="txtFirstName" SkinID="EMS" runat="server"
                                ErrorMessage="Required Field" ID="rfv2"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- PosName -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblPosName" runat="server" Text="PosName:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtPosName" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Nickname -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblNickName" runat="server" Text="Nickname:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtNickName" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Resource Catalog -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblResourceCatalog" runat="server" Text="Picture:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phResourceCatalog" runat="server"></asp:PlaceHolder>
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
