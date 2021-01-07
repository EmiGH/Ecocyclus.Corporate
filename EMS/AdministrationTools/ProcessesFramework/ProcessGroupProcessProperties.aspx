<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ProcessGroupProcessProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.ManagementTools.ProcessesMap.ProcessGroupProcessProperties"
    Title="EMS - Process Group Process Properties" Culture="auto" UICulture="auto" %>

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
                    <!-- Title -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTitle" runat="server" Text="Title:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtTitle" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="EMS" Display="Dynamic" runat="server" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Order -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrder" runat="server" Text="Order:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtOrder"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="EMS" runat="server" ValidationExpression="[-]?[\d]*"
                                ErrorMessage="Wrong Format" ID="rfvOrder" ControlToValidate="txtOrder" Display="Dynamic"
                                ValidationGroup="Task"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <!-- Purpose -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblPurpose" runat="server" Text="Purpose:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtPurpose" MaxLength="8000" Rows="6" runat="server" TextMode="MultiLine"></asp:TextBox>
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
                            <asp:TextBox ID="txtDescription" MaxLength="8000" Rows="6" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Weight -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblWeight" runat="server" Text="Weight:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtWeight" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="EMS" Display="Dynamic" runat="server" ErrorMessage="Required Field"
                                ID="rfv2" ControlToValidate="txtWeight"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="compareValidatorWeight" runat="server" ErrorMessage="Data Error"
                                ControlToValidate="txtWeight" SkinID="EMS" Display="Dynamic" Operator="DataTypeCheck"
                                Type="Integer"></asp:CompareValidator>
                            <asp:RangeValidator ID="rangeValidator0_100" runat="server" ErrorMessage="Range:0-100"
                                Display="Dynamic" Type="Integer" ControlToValidate="txtWeight" SkinID="EMS" MinimumValue="0"
                                MaximumValue="100"></asp:RangeValidator>
                        </td>
                    </tr>
                    <!-- Threshold -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblThreshold" runat="server" Text="Threshold:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="TxtThreshold" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="EMS" Display="Dynamic" runat="server" ErrorMessage="Required Field"
                                ID="rfv4" ControlToValidate="TxtThreshold"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cv1" runat="server" SkinID="EMS" ErrorMessage="Data Error"
                                ControlToValidate="TxtThreshold" Display="Dynamic" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                            <asp:RangeValidator ID="rangeValidator2" runat="server" ErrorMessage="Range:0-100"
                                Display="Dynamic" Type="Integer" ControlToValidate="TxtThreshold" SkinID="EMS"
                                MinimumValue="0" MaximumValue="100"></asp:RangeValidator>
                        </td>
                    </tr>
                    <!-- Identification -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIdentification" runat="server" Text="Identification:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtIdentification" runat="server" SkinID="EMS"></asp:TextBox>
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
                            <%--<asp:PlaceHolder ID="phResourceCatalogValidator" runat="server"></asp:PlaceHolder>--%>
                        </td>
                    </tr>
                    <!-- Campaign Start Date -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblCampaignStartDate" runat="server" Text="Campaign Start Date:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDatePicker ID="rdtCampaignStartDate" Skin="EMS" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput ID="DateInput1" runat="server" DateFormat="MM/dd/yyyy" />
                                <Calendar ID="Calendar1" runat="server">
                                </Calendar>
                            </rad:RadDatePicker>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="Required Field"
                                ControlToValidate="rdtCampaignStartDate" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cv2" runat="server" ErrorMessage="Data Error" ControlToValidate="rdtCampaignStartDate"
                                Operator="DataTypeCheck" SkinID="EMS" Display="Dynamic" Type="Date"></asp:CompareValidator>
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
                    <%--<!-- Facility -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblSite" runat="server" Text="Facility" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phSite" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>--%>
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
                    <!-- Classifications -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblClassifications" runat="server" Text="Process Classifications" />
                        </td>
                        <td class="ColContent">
                            <div class="Treeview">
                                <asp:UpdatePanel ID="upHierarchicalListManage" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:PlaceHolder ID="phProcessClassifications" runat="server"></asp:PlaceHolder>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- TwitterUser -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTwitterUser" runat="server" Text="TwitterUser:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtTwitterUser" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- FacebookUser -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFacebookUser" runat="server" Text="FacebookUser:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtFacebookUser" MaxLength="150"></asp:TextBox>
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
