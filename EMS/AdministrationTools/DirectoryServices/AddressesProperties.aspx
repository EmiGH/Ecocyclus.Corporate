<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="AddressesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.DS.AddressesProperties" Title="EMS - Address Property"
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
    <table id="tblContentForm" runat="server" class="ContentForm">
        <colgroup>
            <col class="ColTitle" />
            <col class="ColContent" />
            <col class="ColValidator" />
        </colgroup>
        <!-- Geographic Area -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblGeographicArea" runat="server" Text="Geographic Area:" />
            </td>
            <td class="ColContent">
                <asp:PlaceHolder ID="phGeographicArea" runat="server" />
            </td>
            <td class="ColValidator">
                <asp:PlaceHolder ID="phGeographicAreaValidator" runat="server" />
            </td>
        </tr>
        <!-- Street -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblStreet" runat="server" Text="Street:" />
            </td>
            <td class="ColContent">
                <asp:TextBox runat="server" ID="txtStreet" MaxLength="150"></asp:TextBox>
            </td>
            <td class="ColValidator">
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfv1"
                    ControlToValidate="txtStreet" Display="Dynamic" SkinID="EMS"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <!-- Number -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblNumber" runat="server" Text="Number:" />
            </td>
            <td class="ColContent">
                <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
            </td>
            <td class="ColValidator">
                &nbsp;
            </td>
        </tr>
        <!-- Floor -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblFloor" runat="server" Text="Floor:" />
            </td>
            <td class="ColContent">
                <asp:TextBox ID="txtFloor" runat="server"></asp:TextBox>
            </td>
            <td class="ColValidator">
                &nbsp;
            </td>
        </tr>
        <!-- Apartment -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblApartment" runat="server" Text="Apartment:" />
            </td>
            <td class="ColContent">
                <asp:TextBox ID="txtApartment" runat="server"></asp:TextBox>
            </td>
            <td class="ColValidator">
                &nbsp;
            </td>
        </tr>
        <!-- Zip Code -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblZipCode" runat="server" Text="Zip Code:" />
            </td>
            <td class="ColContent">
                <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>
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
    <%--estas son variables ocultas para obtener los puntos geograficos.--%>
    <input id="inputPoints" name="inputPoints" type="hidden" runat="server" />
    <input id="drawModeType" name="drawModeType" type="hidden" runat="server" />
</asp:Content>
