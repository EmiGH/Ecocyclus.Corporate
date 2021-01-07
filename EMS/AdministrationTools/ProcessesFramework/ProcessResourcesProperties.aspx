<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ProcessResourcesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.ManagementTools.ProcessesMap.ProcessResourcesProperties"
    Title="Process Resources Properties" Culture="auto" 
    UICulture="auto" %>

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
                    <!-- Resource -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblResources" runat="server" Text="Resource:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phResources" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phResourcesValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Comment -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblComment" runat="server" Text="Value:"/>
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtComment" MaxLength="8000" TextMode="MultiLine" Rows="6"></asp:TextBox>
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
