<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ProcessTaskExecutionCalibrationsProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PM.ProcessTaskExecutionCalibrationsProperties" Title="EMS - Process Task Execution Calibrations Properties"
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
                    <!-- Task -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTaskName" runat="server" Text="Task:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblTaskNameValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Id Execution -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIdExecution" runat="server" Text="Id Execution:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblIdExecutionValue" runat="server" />
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
                    <!-- Post -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblPost" runat="server" Text="Post:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblPostValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Date -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDate" runat="server" Text="Date:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblDateValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Comment -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblComment" runat="server" Text="Comment:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtComment" runat="server" MaxLength="8000" TextMode="MultiLine"
                                Rows="6"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Valid From -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblValidationStart" runat="server" Text="Valid From:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDateTimePicker ID="rdtValidationStart" Skin="EMS" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput ID="diValidationStartDate" DateFormat="MM/dd/yyyy HH:mm:ss" runat="server" />
                                <TimePopupButton ToolTip="<%$ Resources:Common, dtTimePickerTooltip %>" />
                                <TimeView ID="tvValidationStartTime" runat="server" TimeFormat="HH:mm:ss" >
                                </TimeView>
                            </rad:RadDateTimePicker>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Valid Throught -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblValidationEnd" runat="server" Text="Valid Throught:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDateTimePicker ID="rdtValidationEnd" Skin="EMS" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput ID="diValidationEndDate" DateFormat="MM/dd/yyyy HH:mm:ss" runat="server" />
                                <TimePopupButton ToolTip="<%$ Resources:Common, dtTimePickerTooltip %>" />
                                <TimeView ID="tvValidationEndTime" runat="server" TimeFormat="HH:mm:ss" >
                                </TimeView>
                            </rad:RadDateTimePicker>
                        </td>
                        <td class="ColValidator">
                            <asp:CustomValidator ID="customvEndDate" runat="server" ControlToValidate="rdtValidationEnd"
                                SkinID="EMS" Display="Dynamic" ErrorMessage="The second date must be after the first one"></asp:CustomValidator>
                        </td>
                    </tr>
                    <!-- Upload File Calibration -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFile" runat="server" Text="Upload File Calibration:" />
                        </td>
                        <td class="ColContent">
                            <asp:FileUpload ID="fileUploadCalibration" runat="server" />
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
