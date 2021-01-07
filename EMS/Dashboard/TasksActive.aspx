<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="TasksActive.aspx.cs"
    Inherits="Condesus.EMS.WebUI.Dashboard.TasksActive" Title="Active Tasks" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <asp:Panel ID="pnlTasks" runat="server">
    </asp:Panel>
    <input type="hidden" id="radGridClickedRowIndex" name="radGridClickedRowIndex" />
    <input type="hidden" id="radGridClickedTableId" name="radGridClickedTableId" />
    
    <!--Abro RadMenu-->
    <rad:RadContextMenu ID="rmnSelection" runat="server"
        CollapseDelay="0" 
        OnItemClick="rmnSelection_ItemClick" Style="left: 0px; top: 0px">
        <CollapseAnimation Type="None" Duration="0"></CollapseAnimation>
        <ExpandAnimation Type="None" Duration="0"></ExpandAnimation>
    </rad:RadContextMenu>
    <!--Abro RadMenu-->   

</asp:Content>
