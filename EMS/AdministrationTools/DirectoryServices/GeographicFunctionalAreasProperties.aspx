<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="GeographicFunctionalAreasProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.GeographicFunctionalAreasProperties" Title="EMS - Geographic Functional Area Property"
    EnableEventValidation="false" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
    
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
                    <!-- Organization -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblOrganization" runat="server" Text="Organization:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblOrganizationValue" runat="server" Text="" />
                        </td>
                        <td class="ColValidator">
                             &nbsp;
                        </td>
                    </tr>
                    <!-- Geographic Functional Area -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblGeographicFunctionalArea" runat="server"
                            Text="Geographic Functional Area:" meta:resourcekey="lblGeographicFunctionalAreaResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phGeographicFunctionalArea" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Functional Area -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFunctionalArea" runat="server"
                            Text="Functional Area:" meta:resourcekey="lblFunctionalAreaResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phFunctionalArea" runat="server" />
                            <asp:LinkButton runat="server" ID="lnkFunctionalArea" SkinID="Form" CausesValidation="false"></asp:LinkButton>                        
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phFunctionalAreaValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Geographic Area -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblGeographicArea" runat="server"
                            Text="Geographic Area:" meta:resourcekey="lblGeographicAreaResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phGeographicArea" runat="server" />
                            <asp:LinkButton runat="server" ID="lnkGeographicArea" SkinID="Form" CausesValidation="false"></asp:LinkButton>                        
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phGeographicAreaValidator" runat="server" />
                        </td>
                    </tr>
                </table>
             </td>
         </tr>
    </table> 
        
</asp:Content>
