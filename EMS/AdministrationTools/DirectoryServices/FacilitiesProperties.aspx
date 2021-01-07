<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="FacilitiesProperties.aspx.cs" Inherits="Condesus.EMS.WebUI.AdministrationTools.DirectoryServices.FacilitiesProperties" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <%--Hay que agregar este manager para administrar la ventana de pickup del mapa.--%>
    <rad:RadWindowManager Modal="true" Behaviors="Close,Move" ID="rwmDialogPickUpCoords"
        VisibleTitlebar="false" ShowContentDuringLoad="true" Width="780px" Height="500px"
        VisibleStatusbar="false" ReloadOnShow="true" runat="server" Skin="EMS">
    </rad:RadWindowManager>
    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <table id="tblContentForm" runat="server" class="ContentForm">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                        <col class="ColValidator" />
                    </colgroup>
                    <!-- Facility Type -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFacilityType" runat="server" Text="Facility Type:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phFacilityType" runat="server"></asp:PlaceHolder>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phFacilityTypeValidator" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFacility" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtName" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfv1"
                                ControlToValidate="txtName" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="6" MaxLength="8000" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Language -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLanguage" runat="server" Text="Language:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblLanguageValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Active -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblActive" runat="server" Text="Active:" />
                        </td>
                        <td class="ColContentCheck">
                            <asp:CheckBox CssClass="CheckBox" ID="chkActive" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Resource Catalog -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblResourceCatalog" runat="server" Text="Picture:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phResourceCatalog" runat="server"></asp:PlaceHolder>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- GeographicArea -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblGeographicArea" runat="server" Text="GeographicArea" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phGeographicArea" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                    
                    <!-- GeoPoints (campo completo a agregar en otras paginas)-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblCoordenates" runat="server" Text="Coordenates" />
                        </td>
                        <td class="ColContentCoord">
                            <table class="tableCoord" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="tdEdit">
                                        <asp:Button ID="btnShowPopUp" runat="server" Text="" OnClientClick="openWin(); return false;"
                                            CausesValidation="False" />
                                    </td>
                                    <td>
                                        <div id="pnlCoords" runat="server">
                                            <!---->
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%--estas son variables ocultas para obtener los puntos geograficos.--%>
    <input id="inputPoints" name="inputPoints" type="hidden" runat="server" />
    <input id="drawModeType" name="drawModeType" type="hidden" runat="server" />
</asp:Content>
