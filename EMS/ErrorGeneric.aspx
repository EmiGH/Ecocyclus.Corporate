<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="ErrorGeneric.aspx.cs" Inherits="Condesus.EMS.WebUI.ErrorGeneric" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <div class="divError">
        <asp:Label ID="lblErrorMessage" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
