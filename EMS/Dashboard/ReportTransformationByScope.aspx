<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="ReportTransformationByScope.aspx.cs" Inherits="Condesus.EMS.WebUI.Dashboard.ReportTransformationByScope" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <!-- Filtros -->
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
                <asp:PlaceHolder ID="phOrganization" runat="server" />
            </td>
            <td class="ColValidator">
                <asp:PlaceHolder ID="phOrganizationValidator" runat="server" />
            </td>
        </tr>
        <!-- Accounting Scope -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblAccountingScope" runat="server" Text="Accounting Scope:" />
            </td>
            <td class="ColContent">
                <asp:PlaceHolder ID="phAccountingScope" runat="server" />
            </td>
            <td class="ColValidator">
                <asp:PlaceHolder ID="phAccountingScopeValidator" runat="server" />
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
                &nbsp;
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
                <asp:CustomValidator ID="customvEndDate" runat="server" ControlToValidate="rdtThrough"
                    SkinID="EMS" ErrorMessage="The second date must be after the first one" Display="Dynamic"></asp:CustomValidator>
            </td>
        </tr>
    </table>
    <!-- Button List -->
    <asp:Button ID="btnList" runat="server" Text="List" CssClass="contentButton" />
    <!-- Report -->
    <table cellpadding="0" cellspacing="0" style="width:100%; margin: 0 0 10px 0;">
        <tr>
            <td>
                <asp:UpdatePanel ID="upGridHierarchy" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <rad:RadGrid ID="rgdHierarchy" runat="server" Skin="EMS" OnColumnCreated="rgdHierarchy_ColumnCreated"
                            OnItemCreated="rgdHierarchy_ItemCreated" OnNeedDataSource="rgdHierarchy_NeedDataSource"
                            OnPreRender="rgdHierarchy_PreRender">
                            <MasterTableView HierarchyDefaultExpanded="false" HierarchyLoadMode="Client" AllowSorting="false"
                                DataKeyNames="IdActivity, IdParentActivity" TableLayout="Fixed" HorizontalAlign="Center"
                                NoDetailRecordsText="" NoMasterRecordsText="" BorderStyle="None" BorderWidth="0px">
                                <ItemStyle BorderStyle="None" BorderWidth="0px" />
                                <SelfHierarchySettings ParentKeyName="IdParentActivity" KeyName="IdActivity" MaximumDepth="100" />
                            </MasterTableView>
                            <ClientSettings AllowExpandCollapse="true">
                            </ClientSettings>
                        </rad:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
