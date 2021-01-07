<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ProcessTaskExecutionsProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PM.ProcessTaskExecutionsProperties" Title="EMS - Process Task Executions Properties"
    Culture="auto" UICulture="auto" %>

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
                <asp:TextBox ID="txtComment" SkinID="EMS" MaxLength="8000" runat="server" TextMode="MultiLine"
                    Rows="6"></asp:TextBox>
            </td>
            <td class="ColValidator">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
