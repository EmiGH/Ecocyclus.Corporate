<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="GeographicAreasLanguageProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.GeographicAreasLanguageProperties" Title="EMS - Geographic Area Language Option"
    Culture="auto" UICulture="auto" %>

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
                    <!-- Geographic Area -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblGeographicAreas" runat="server" Text="Geographic Area Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtGeographicAreas" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ErrorMessage="Required Field"
                                ControlToValidate="txtGeographicAreas" SkinID="EMS"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblGeographicAreasDescription" runat="server" Text="Geographic Area Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtGeoAreaDescription" TextMode="MultiLine" Rows="6"
                                MaxLength="8000"></asp:TextBox>
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
