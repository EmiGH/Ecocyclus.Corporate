<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="ReportMultiObservatory.aspx.cs" Inherits="Condesus.EMS.WebUI.ReportMultiObservatory" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <!-- Filtros -->
    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
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
            </td>
        </tr>
        <tr>
            <td align="right">
                <!-- Button List -->
                <asp:Button ID="btnList" runat="server" Text="List" CssClass="contentButton" />
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />

    <asp:UpdatePanel runat="server" ID="upGridMultiObservatory" UpdateMode="Always">
        <ContentTemplate>
            <div style="padding: 10px 10px 0 10px;">
                <asp:PlaceHolder ID="phGridMultiObservatory" runat="server"></asp:PlaceHolder>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
