<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="HeredarCoordenadas.aspx.cs" 
Inherits="Condesus.EMS.WebUI.Wizard.HeredarCoordenadas" %>

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
        </table>
    </asp:Panel>
</asp:Content>
