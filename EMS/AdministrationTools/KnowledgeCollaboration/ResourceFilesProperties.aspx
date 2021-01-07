<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ResourceFilesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.ManagementTools.KnowledgeCollaboration.ResourceFilesProperties"
    Title="EMS - Resource File Property" Culture="auto" UICulture="auto" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <table id="tblContentForm" runat="server" class="ContentForm">
        <colgroup>
            <col class="ColTitle" />
            <col class="ColContent" />
            <col class="ColValidator" />
        </colgroup>
        <!-- Selected Resource -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblSelectedResouce" runat="server" Text="Selected Resource:" />
            </td>
            <td class="ColContentList">
                <asp:RadioButtonList ID="rbList" runat="server" AutoPostBack="True">
                    <asp:ListItem Text="File" Value="rbFile" Selected="True">
                    </asp:ListItem>
                    <asp:ListItem Text="Url" Value="rbUrl">
                    </asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="ColValidator">
                &nbsp;
            </td>
        </tr>
        <!-- File Name -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblResourceFile" runat="server" Text="File Name:" />
            </td>
            <td class="ColContent">
                <asp:TextBox runat="server" ID="txtResourceFile" MaxLength="150" SkinID="EMS"></asp:TextBox>
            </td>
            <td class="ColValidator">
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfvTitle"
                    ControlToValidate="txtResourceFile" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <!-- Version -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblVersion" runat="server" Text="Version:" />
            </td>
            <td class="ColContent">
                <asp:TextBox runat="server" SkinID="EMS" ID="txtVersion" Text="0.0.1"></asp:TextBox>
            </td>
            <td class="ColValidator">
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfvVersion"
                    ControlToValidate="txtVersion" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator SkinID="EMS" runat="server" ValidationExpression="^((\d)?(\d{1})(\.{1})(\d)?(\d{1})(\.{1})(\d)?(\d{1})){1}$"
                    ErrorMessage="Wrong Format" ID="rfvOrder" ControlToValidate="txtVersion" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <!-- From -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblFrom" runat="server" Text="From:" />
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
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfvDateFrom"
                    Display="Dynamic" ControlToValidate="rdtFrom" SkinID="EMS"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <!-- Through -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblThrough" runat="server" Text="Through:" />
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
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfvDateThrough"
                    ControlToValidate="rdtThrough" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="customvEndDate" runat="server" ControlToValidate="rdtThrough"
                    SkinID="EMS" ErrorMessage="The second date must be after the first one" Display="Dynamic"></asp:CustomValidator>
            </td>
        </tr>
        <!-- Current -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblCurrent" runat="server" Text="Current:" />
            </td>
            <td class="ColContentCheck">
                <asp:CheckBox ID="chkCurrent" runat="server" SkinID="EMS" />
            </td>
            <td class="ColValidator">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Panel runat="server" ID="contentHide" style="margin-top:10px">
        <table id="tblContentFormUpload" runat="server" class="ContentForm">
            <colgroup>
                <col class="ColTitle" />
                <col class="ColContent" />
                <col class="ColValidator" />
            </colgroup>
            <!-- File Name -->
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblFileName" runat="server" Text="File Name:" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblFileNameValue" runat="server" />
                </td>
                <td class="ColValidator">
                    &nbsp;
                </td>
            </tr>
            <!-- Extension -->
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblExtension" runat="server" Text="Extension:" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblExtensionValue" runat="server" />
                </td>
                <td class="ColValidator">
                    &nbsp;
                </td>
            </tr>
            <!-- Lenght -->
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblLenght" runat="server" Text="Lenght:" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblLenghtValue" runat="server" />
                </td>
                <td class="ColValidator">
                    &nbsp;
                </td>
            </tr>
            <!-- Type -->
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblType" runat="server" Text="Type:" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblTypeValue" runat="server" />
                </td>
                <td class="ColValidator">
                    &nbsp;
                </td>
            </tr>
            <!-- File Upload -->
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblUpload" runat="server" Text="File Upload" />
                </td>
                <td class="ColContent">
                    <asp:FileUpload ID="fileUploadVersionable" runat="server" />
                </td>
                <td class="ColValidator">
                    <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfvFile"
                        Display="Dynamic" ControlToValidate="fileUploadVersionable" SkinID="EMS"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
