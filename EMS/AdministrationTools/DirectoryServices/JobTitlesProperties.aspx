<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="JobTitlesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.JobTitlesProperties" Title="EMS - Job Title Property"
    Culture="auto" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

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
                              <asp:Label ID="lblOrganization" runat="server" 
                            Text="Organization:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblOrganizationValue" runat="server" Text="" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Organizational Chart -->
                    <tr>
                        <td class="ColTitle">
                              <asp:Label ID="lblOrganizationalChart" runat="server" 
                            Text="Organizational Chart:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upOrganizationalChart" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phOrganizationalChart" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phOrganizationalChartValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Job Title Parent -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblJobTitleParent" runat="server" Text="Job Title Parent:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phJobTitle" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Job Title -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblJobTitle" runat="server" Text="Job Title:"
                            />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblJobTitleValue" runat="server" Text=""
                             />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Functional Position -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblFunctionalPosition" runat="server"
                            Text="Functional Position:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phFunctionalPosition" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phFunctionalPositionValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Geographic Functional Area -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblGeographicFunctionalArea" runat="server"
                                Text="Geographic Functional Area:"  />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phGeographicFunctionalArea" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phGeographicFunctionalAreaValidator" runat="server" />
                        </td>
                    </tr>
                </table>
             </td>
         </tr>
    </table> 
</asp:Content>
