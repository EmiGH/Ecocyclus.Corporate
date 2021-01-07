<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ProcessGroupNodeStandardProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PF.ProcessGroupNodeStandardProperties" Title="EMS - Process Group Node Standards Properties"
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
                    <!-- Parent Process -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIdParent" runat="server" Text="Parent Process:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblIdParentValue" runat="server" />
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
                    <!-- Title -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTitle" runat="server" Text="Title:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtTitle" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtTitle" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Order -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrder" runat="server" Text="Order:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtOrder"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="EMS" runat="server" ValidationExpression="[-]?[\d]*"
                                ErrorMessage="Wrong Format" ID="rfvOrder" ControlToValidate="txtOrder" Display="Dynamic"
                                ValidationGroup="Task"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <!-- Purpose -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblPurpose" runat="server" Text="Purpose:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtPurpose" runat="server" MaxLength="8000" TextMode="MultiLine"
                                Rows="6"></asp:TextBox>
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
                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="8000" TextMode="MultiLine"
                                Rows="6"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Weight -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblWeight" runat="server" Text="Weight:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Threshold -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblThreshold" runat="server" Text="Threshold:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtThreshold" runat="server"></asp:TextBox>
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
