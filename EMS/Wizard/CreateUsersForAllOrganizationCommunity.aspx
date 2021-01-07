<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="CreateUsersForAllOrganizationCommunity.aspx.cs" 
Inherits="Condesus.EMS.WebUI.Wizard.CreateUsersForAllOrganizationCommunity" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <asp:Label runat="server" ID="lblDescription" CssClass="contentConfigurationText"
            Text="Esta Página realiza la creación automatica de 3 Usuarios por cada organizacion existente en el sistema.
                Solo debe hacer click en el botón Guardar."></asp:Label>
    <asp:TextBox ID="txtIdOrgDesde" runat="server"></asp:TextBox>                
    <asp:TextBox ID="txtIdOrgHasta" runat="server"></asp:TextBox>                

</asp:Content>
