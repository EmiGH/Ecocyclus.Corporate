<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="PostsProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.PostsProperties" Title="EMS - Post Property"
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
                            <asp:Label ID="lblOrganizationValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Person -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblPerson" runat="server" Text="Person:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upPerson" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phPerson" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phPersonValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Organizational Chart -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganizationalChart" runat="server" Text="Organizational Chart:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phOrganizationalChart" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phOrganizationalChartValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Job Title -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblJobTitle" runat="server" Text="Job Title:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phJobTitle" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phJobTitleValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Start Date -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblStartDateValue" runat="server"></asp:Label>
                            <rad:RadDatePicker ID="rdtStartDate" runat="server" Culture="English (United States)"
                                Skin="EMS">
                                <Calendar ID="Calendar1" runat="server">
                                </Calendar>
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput ID="DateInput1" runat="server">
                                </DateInput>
                            </rad:RadDatePicker>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="rdtStartDate"
                                Display="Dynamic" ErrorMessage="Required Field" SetFocusOnError="true">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- End Date -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblEndDate" runat="server" Text="End Date:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblEndDateValue" runat="server"></asp:Label>
                            <rad:RadDatePicker ID="rdtEndDate" runat="server" Culture="English (United States)" Skin="EMS">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <Calendar ID="Calendar2" runat="server">
                                </Calendar>
                                <DateInput ID="DateInput2" runat="server" CssClass="contentformInput">
                                </DateInput>
                            </rad:RadDatePicker>
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
