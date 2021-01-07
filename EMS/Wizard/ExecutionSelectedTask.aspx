<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ExecutionSelectedTask.aspx.cs" 
Inherits="Condesus.EMS.WebUI.Wizard.ExecutionSelectedTask" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <asp:Panel runat="server" ID="contentHide" Style="margin-top: 10px">
        <table id="tblContentForm" runat="server" class="ContentForm">
            <colgroup>
                <col class="ColTitle" />
                <col class="ColContent" />
                <col class="ColValidator" />
            </colgroup>
            <!-- Process -->
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblProcess" runat="server" Text="Process:" />
                </td>
                <td class="ColContent">
                    <asp:PlaceHolder ID="phProcess" runat="server" />
                </td>
                <td class="ColValidator">
                    &nbsp;
                </td>
            </tr>            
            <!-- Measurement Value -->
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblMeasurementValue" runat="server" Text="Measurement Value:" />
                </td>
                <td class="ColContent">
                    <asp:TextBox ID="txtMeasurementValue" SkinID="EMS" runat="server"></asp:TextBox>
                </td>
                <td class="ColValidator">
                    <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtMeasurementValue"
                        Display="Dynamic" ErrorMessage="Required Field" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator SkinID="Validator" runat="server" ValidationExpression="^[-+]?\d*\.?\d*$" ErrorMessage="Wrong Format" 
                        ID="revValue" Display="Dynamic" ControlToValidate="txtMeasurementValue" Enabled="true"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <!-- Measurement Date -->
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblMeasurementDate" runat="server" Text="Measurement Date:" />
                </td>
                <td class="ColContent">
                    <rad:RadDateTimePicker ID="rdtMeasurementDate" Skin="EMS" runat="server" Culture="English (United States)">
                        <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                        <DateInput ID="diMeasurementDate" DateFormat="MM/dd/yyyy HH:mm:ss" runat="server" />
                        <Calendar ID="Calendar1" runat="server">
                        </Calendar>
                        <TimePopupButton ToolTip="<%$ Resources:Common, dtTimePickerTooltip %>" />
                        <TimeView ID="tvMeasurementTime" runat="server" TimeFormat="HH:mm:ss">
                        </TimeView>
                    </rad:RadDateTimePicker>
                </td>
                <td class="ColValidator">
                    <asp:RequiredFieldValidator ID="rfv3" runat="server" ControlToValidate="rdtMeasurementDate"
                        Display="Dynamic" ErrorMessage="Required Field" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <!-- Tasks -->
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblTasks" runat="server" Text="Select task to Execute:"></asp:Label>
                </td>
                <td class="ColContent">
                    <asp:UpdatePanel ID="upHierarchicalListManageTask" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="Treeview" style="height: 350px; width:850px;">
                                <asp:PlaceHolder ID="phTask" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="ColValidator">
                    &nbsp;
                </td>
            </tr>            
        </table>
    </asp:Panel>
</asp:Content>
