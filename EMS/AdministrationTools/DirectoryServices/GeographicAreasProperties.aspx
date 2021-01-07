<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="GeographicAreasProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.GeographicAreasProperties" Title="EMS - Geographic Area Property"
    Culture="auto" UICulture="auto" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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
                    <!-- Parent -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIdParent" runat="server" Text="Parent:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phGeographicArea" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Selected Resource -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLayer" runat="server" Text="Layer:" />
                        </td>
                        <td class="ColContentList">
                            <asp:RadioButtonList ID="rbLayer" runat="server" AutoPostBack="false">
                                <asp:ListItem Text="Country" Value="rbCountry" Selected="True">
                                </asp:ListItem>
                                <asp:ListItem Text="Province" Value="rbProvince">
                                </asp:ListItem>
                                <asp:ListItem Text="Municipality" Value="rbMunicipality">
                                </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                    
                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblGeographicArea" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtGeographicArea" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfv1"
                                ControlToValidate="txtGeographicArea" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                    <!-- ISO Code -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblISOCode" runat="server" Text="Alpha:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtISOCode" runat="server" MaxLength="4"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
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
                    <!-- Organization -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganization" runat="server" Text="Organization:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phOrganization" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phOrganizationValidator" runat="server" />
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
