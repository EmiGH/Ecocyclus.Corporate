<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="FacilityTypesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.AdministrationTools.DirectoryServices.FacilityTypesProperties" %>

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
                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblName" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtName" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator Display="Dynamic" SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                            <asp:CustomValidator Display="Dynamic" SkinID="EMS" ID="rfv2" runat="server" ErrorMessage="Invalid Special Characters"
                                ControlToValidate="txtName" ClientValidationFunction="CheckIndexesTags"></asp:CustomValidator>
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
                    <!-- Icon Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIconName" runat="server" Text="Icon Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtIconName" TextMode="SingleLine" MaxLength="150" runat="server"></asp:TextBox>
                            <%--<rad:RadComboBox ID="rdcIconName" runat="server" Skin="EMS" Width="343px" DropDownWidth="343px" EnableEmbeddedSkins="false">
                                <items>
                                    <rad:RadComboBoxItem Value="PowerPlant" Text="PowerPlant" />
                                    <rad:RadComboBoxItem Value="ShopsandServices" Text="ShopsandServices" />
                                    <rad:RadComboBoxItem Value="Homes" Text="Homes" />
                                    <rad:RadComboBoxItem Value="Refineries" Text="Refineries" />
                                    <rad:RadComboBoxItem Value="ServiceStations" Text="ServiceStations" />
                                    <rad:RadComboBoxItem Value="WaterTreatmentPlants" Text="WaterTreatmentPlants" />
                                    <rad:RadComboBoxItem Value="Industries" Text="Industries" />
                                    <rad:RadComboBoxItem Value="Farms" Text="Farms" />
                                    <rad:RadComboBoxItem Value="WasteTreatmentPlants" Text="WasteTreatmentPlants" />
                                    <rad:RadComboBoxItem Value="Landfill" Text="Landfill" />
                                    <rad:RadComboBoxItem Value="Land" Text="Land" />
                                    <rad:RadComboBoxItem Value="Office" Text="Office" />
                                    <rad:RadComboBoxItem Value="OilPipeline" Text="OilPipeline" />
                                    <rad:RadComboBoxItem Value="GasPipeline" Text="GasPipeline" />
                                    <rad:RadComboBoxItem Value="BatteriesOil" Text="BatteriesOil" />
                                    <rad:RadComboBoxItem Value="MotorCompressorStation" Text="MotorCompressorStation" />
                                    <rad:RadComboBoxItem Value="OilTreatmentPlant" Text="OilTreatmentPlant" />
                                    <rad:RadComboBoxItem Value="ConditioningPlantDewpoint" Text="ConditioningPlantDewpoint" />
                                    <rad:RadComboBoxItem Value="SeparationPlantOfLiquefiedGases" Text="SeparationPlantOfLiquefiedGases" />
                                    <rad:RadComboBoxItem Value="SaltWaterInjectionPlant" Text="SaltWaterInjectionPlant" />
                                    <rad:RadComboBoxItem Value="FreshWaterInjectionPlant" Text="FreshWaterInjectionPlant" />
                                    <rad:RadComboBoxItem Value="FreshWaterTransferPlant" Text="FreshWaterTransferPlant" />
                                    <rad:RadComboBoxItem Value="ThermalPowerPlant" Text="ThermalPowerPlant" />
                                    <rad:RadComboBoxItem Value="OilWell" Text="OilWell" />
                                    <rad:RadComboBoxItem Value="FleetVehicles" Text="FleetVehicles" />
                                    <rad:RadComboBoxItem Value="Global" Text="Global" />
                                    <rad:RadComboBoxItem Value="Mining" Text="Mining" />
                                    <rad:RadComboBoxItem Value="Services" Text="Services" />
                                    <rad:RadComboBoxItem Value="Educational" Text="Educational" />
                                    <rad:RadComboBoxItem Value="Unspecified" Text="Unspecified" />
                                    <rad:RadComboBoxItem Value="BuildingStructures" Text="BuildingStructures" />
                                    <rad:RadComboBoxItem Value="BusFleet" Text="BusFleet" />
                                    <rad:RadComboBoxItem Value="CarFleet" Text="CarFleet" />
                                    <rad:RadComboBoxItem Value="Hospital" Text="Hospital" />
                                    <rad:RadComboBoxItem Value="IndustrialSector" Text="IndustrialSector" />
                                    <rad:RadComboBoxItem Value="PublicSpaces" Text="PublicSpaces" />
                                    <rad:RadComboBoxItem Value="Railway" Text="Railway" />
                                    <rad:RadComboBoxItem Value="ResidentialSector" Text="ResidentialSector" />
                                    <rad:RadComboBoxItem Value="Subway" Text="Subway" />
                                    <rad:RadComboBoxItem Value="Transport" Text="Transport" />
                                    <rad:RadComboBoxItem Value="CommercialSector" Text="CommercialSector" />
                                    <rad:RadComboBoxItem Value="Restaurant" Text="Restaurant" />
                                    <rad:RadComboBoxItem Value="Fastfood" Text="Fastfood" />

                                </items>
                            </rad:RadComboBox>--%>
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
