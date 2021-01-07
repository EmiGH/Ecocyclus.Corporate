<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="MessageEditor.aspx.cs" Inherits="Condesus.EMS.WebUI.AdministrationTools.CollaborationTools.MessageEditor" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <telerik:RadEditor ID="redtOriginalMessage" Enabled="false" Visible="false" EnableResize="false"  Width="100%" runat="server"  >
    </telerik:RadEditor>
<br />
<br />
    <telerik:RadEditor ID="reMessage" EnableResize="false" runat="server" EditModes="Design" Width="100%" Skin="Outlook" EnableEmbeddedSkins="true">
    </telerik:RadEditor>
</asp:Content>



