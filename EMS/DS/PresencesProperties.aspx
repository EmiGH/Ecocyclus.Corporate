<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="PresencesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.PresencesProperties" Title="EMS - Presences List" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
     
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
                    <!-- Person -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblPerson" runat="server" 
                            Text="Person:" meta:resourcekey="lblPersonResource1" />
                        
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblPersonValue" runat="server" 
                            meta:resourcekey="lblPersonValueResource1" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Job Title -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblJobTitle" runat="server" 
                            Text="Job Title:" meta:resourcekey="lblJobTitleResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblJobTitleValue" runat="server" 
                            meta:resourcekey="lblJobTitleValueResource1" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Tree GeographicArea -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label runat="server" ID="lblArea" Text="Area"></asp:Label>                     
                        </td>
                        <td class="ColContent">
                            <div class="Treeview">
                                <asp:UpdatePanel ID="upHierarchicalListManage" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:PlaceHolder ID="phGeographicArea" runat="server">
                                        </asp:PlaceHolder>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                  
                </table>
             </td>
         </tr>
    </table> 
    
    <!--Variables escondidas-->
    <input type="hidden" id="radGridClickedRowIndex" name="radGridClickedRowIndex"/>
    <input type="hidden" id="radGridClickedTableId" name="radGridClickedTableId"/>
    <input type="hidden" id="radMenuClickedId" name="radMenuClickedId"/>
    <input type="hidden" id="radGridClickedIdGeographicFunctionalArea" name="radGridClickedIdGeographicFunctionalArea"/>
    <!--Variables escondidas-->

</asp:Content>
