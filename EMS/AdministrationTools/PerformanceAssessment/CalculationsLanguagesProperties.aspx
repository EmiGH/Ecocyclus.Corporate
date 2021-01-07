﻿<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="CalculationsLanguagesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment.CalculationsLanguagesProperties"
    Title="EMS - Calculation Language Option" Culture="auto" UICulture="auto" %>

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
                    <!-- Calculation -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblCalculation" runat="server" Text="Calculation:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtCalculation" MaxLength="150" runat="server" SkinID="EMS"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv1" SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ControlToValidate="txtCalculation" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtDescription"  TextMode="MultiLine" MaxLength="8000" Rows="6"></asp:TextBox>
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
