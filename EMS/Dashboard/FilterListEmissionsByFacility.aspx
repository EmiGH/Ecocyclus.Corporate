<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="FilterListEmissionsByFacility.aspx.cs" Inherits="Condesus.EMS.WebUI.Dashboard.FilterListEmissionsByFacility" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <!-- Filtros -->
                <table id="tblContentForm" runat="server" class="ContentForm">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                        <col class="ColValidator" />
                    </colgroup>
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
                <!-- Button List -->
                <asp:Button ID="btnList" runat="server" Text="List" CssClass="contentButton" OnClientClick="javascript:ShowEmissionsByFacility(this);" />
            </td>
        </tr>
    </table>
</asp:Content>
