<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ExceptionProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.ManagementTools.ProcessesMap.ExceptionProperties"
    Title="Exception Properties" Culture="auto" UICulture="auto" %>

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
                    <!-- Project -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblProject" runat="server" Text="Project:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblProjectValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
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
                            <asp:Label ID="lblExecutedBy" runat="server" Text="Performanced By:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblExecutedByValue" runat="server" />
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
                    <!-- Exception Type -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblExceptionType" runat="server" Text="Exception Type:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblExceptionTypeValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Exception States -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblExceptionStates" runat="server" Text="Exception States:" />
                            <td class="ColContent">
                                <asp:Label ID="lblExceptionStatesValue" runat="server" />
                            </td>
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
                            <asp:TextBox ID="txtComment" Rows="6" runat="server" MaxLength="8000" TextMode="MultiLine"></asp:TextBox>
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
