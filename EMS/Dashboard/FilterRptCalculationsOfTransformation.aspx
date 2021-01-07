<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="FilterRptCalculationsOfTransformation.aspx.cs" Inherits="Condesus.EMS.WebUI.Dashboard.FilterRptCalculationsOfTransformation" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <!-- Filtros -->
    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="upFilters" UpdateMode="Always">
                    <ContentTemplate>
                        <table id="tblContentForm" runat="server" class="ContentForm">
                            <colgroup>
                                <col class="ColTitle" />
                                <col class="ColContent" />
                                <col class="ColValidator" />
                            </colgroup>
                            <!-- Report -->
                            <tr runat="server" id="trReport" style="display:;">
                                <td class="ColTitle">
                                    <asp:Label ID="lblReporte" runat="server" Text="<%$ Resources:Common, Report %>" />
                                </td>
                                <td class="ColContentList">
                                    <asp:RadioButtonList ID="rblReport" runat="server" >
                                        <asp:ListItem Text="<%$ Resources:Common, GEI %>" Value="GEI" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="<%$ Resources:Common, CL %>" Value="CL"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="ColValidator">
                                    &nbsp;
                                </td>
                            </tr>
                            <!-- Report Type -->
                            <tr runat="server" id="trReportType" style="display:;">
                                <td class="ColTitle">
                                    <asp:Label ID="lblReportType" runat="server" Text="<%$ Resources:Common, ReportType %>" />
                                </td>
                                <td class="ColContentList">
                                    <asp:RadioButtonList ID="rblReportType" runat="server">
                                        <asp:ListItem Text="<%$ Resources:Common, GA_S_A_FT_F %>" Value="GA-S-A-FT-F" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="<%$ Resources:Common, GA_FT_F_S_A %>" Value="GA-FT-F-S-A"></asp:ListItem>
                                        <asp:ListItem Text="<%$ Resources:Common, S_GA_A_FT_F %>" Value="S-GA-A-FT-F"></asp:ListItem>
                                        <asp:ListItem Text="<%$ Resources:Common, S_A_FT_F %>" Value="S-A-FT-F"></asp:ListItem>
                                        <asp:ListItem Text="<%$ Resources:Common, FT_F_S_A %>" Value="FT-F-S-A"></asp:ListItem>
                                        <asp:ListItem Text="<%$ Resources:Common, O_S_A_FT_F %>" Value="O_S_A_FT_F"></asp:ListItem>
                                        <asp:ListItem Text="<%$ Resources:Common, Evolution %>" Value="Evolution"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td class="ColValidator">
                                    &nbsp;
                                </td>
                            </tr>
                            <!-- From -->
                            <tr>
                                <td class="ColTitle">
                                    <asp:Label ID="lblFrom" runat="server" Text="<%$ Resources:CommonListManage, From %>" />
                                </td>
                                <td class="ColContent">
                                    <rad:RadDatePicker ID="rdtFrom" Skin="EMS" runat="server" Culture="English (United States)">
                                        <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                        <DateInput ID="diFrom" DateFormat="MM/dd/yyyy" runat="server" />
                                        <Calendar ID="Calendar1" runat="server">
                                        </Calendar>
                                    </rad:RadDatePicker>
                                </td>
                                <td class="ColValidator">
                                    &nbsp;
                                </td>
                            </tr>
                            <!-- Through -->
                            <tr>
                                <td class="ColTitle">
                                    <asp:Label ID="lblThrough" runat="server" Text="<%$ Resources:CommonListManage, Through %>" />
                                </td>
                                <td class="ColContent">
                                    <rad:RadDatePicker ID="rdtThrough" Skin="EMS" runat="server" Culture="English (United States)">
                                        <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                        <DateInput ID="diThrough" DateFormat="MM/dd/yyyy" runat="server" />
                                        <Calendar ID="Calendar2" runat="server">
                                        </Calendar>
                                    </rad:RadDatePicker>
                                </td>
                                <td class="ColValidator">
                                    <asp:CustomValidator ID="customvEndDate" runat="server" ControlToValidate="rdtThrough"
                                        SkinID="EMS" ErrorMessage="The second date must be after the first one" Display="Dynamic"></asp:CustomValidator>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!-- Button List -->
                <asp:Button ID="btnList" runat="server" Text="List" CssClass="contentButton" OnClientClick="javascript:ShowReportCalculation(this);" />
            </td>
        </tr>
    </table>
</asp:Content>
