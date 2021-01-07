<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ScriptEngine.aspx.cs" 
Inherits="Condesus.EMS.WebUI.Wizard.ScriptEngine" %>

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
                    <!-- Process -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblProcess" runat="server" Text="Process:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phProcess" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Site -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblSite" runat="server" Text="Site:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upSites" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phSite" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Seleccionar las 9 Constantes -->
                    <!-- Constante a-	Factor de Eficiencia del Carbono (A) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstA" runat="server" Text="Factor de Eficiencia del Carbono (A):" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstA" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Constante b-	Factor de  Eficiencia del Azufre (B) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstB" runat="server" Text="Factor de  Eficiencia del Azufre (B):" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstB" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Constante c-	Masa Molecular de CO2 (C) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstC" runat="server" Text="Masa Molecular de CO2 (C):" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstC" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Constante d-	Masa Atómica de C (D) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstD" runat="server" Text="Masa Atómica de C (D):" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstD" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Constante e-	Masa Molecular de SO2 (E) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstE" runat="server" Text="Masa Molecular de SO2 (E):" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstE" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Constante f-	Masa Atómica de S (F) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstF" runat="server" Text="Masa Atómica de S (F):" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstF" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Constante g-	GWP del CO2 (G) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstG" runat="server" Text="GWP del CO2 (G):" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstG" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Constante h-	GWP del CH4 (H) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstH" runat="server" Text="GWP del CH4 (H):" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstH" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Constante i-	GWP del N2O (I) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstI" runat="server" Text="GWP del N2O (I):" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstI" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>

                    <!-- SELECCION DE 3 MEDICIONES!!! -->
                    <!-- Medición a-	Fracción Másica de Carbono (J) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementJ" runat="server" Text="Fracción Másica de Carbono (J):" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upMeasurementJ" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phMeasurementJ" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Medición b-	Fracción Másica de Azufre (K) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementK" runat="server" Text="Fracción Másica de Azufre (K):" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upMeasurementK" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phMeasurementK" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Medición c-	Poder Calorífico Superior del Gas  (L) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementL" runat="server" Text="Poder Calorífico Superior del Gas (L):" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upMeasurementL" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phMeasurementL" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                                                            
                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblName" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtName" MaxLength="150" Text="Caudal Másico del Combustible Utilizado"></asp:TextBox>
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
                    <!-- AGENDAR LA TAREA -->
                    <!-- Start Date -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDateTimePicker ID="rdtStartDate" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput ID="DateInput1" runat="server" DateFormat="MM/dd/yyyy HH:mm:ss" />
                                <Calendar runat="server">
                                </Calendar>
                                <TimePopupButton ToolTip="<%$ Resources:Common, dtTimePickerTooltip %>" />
                                <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm:ss" Culture="English (United States)">
                                </TimeView>
                            </rad:RadDateTimePicker>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- End Date -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblEndDate" runat="server" Text="End Date:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDateTimePicker ID="rdtEndDate" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput runat="server" DateFormat="MM/dd/yyyy HH:mm:ss" />
                                <Calendar runat="server">
                                </Calendar>
                                <TimePopupButton ToolTip="<%$ Resources:Common, dtTimePickerTooltip %>" />
                                <TimeView runat="server" TimeFormat="HH:mm:ss" Culture="English (United States)">
                                </TimeView>
                            </rad:RadDateTimePicker>
                        </td>
                        <td class="ColValidator">
                            <asp:CustomValidator ID="customvEndDate" runat="server" ControlToValidate="rdtEndDate"
                                SkinID="EMS" Display="Dynamic" ErrorMessage="The second date must be after the first one"></asp:CustomValidator>
                        </td>
                    </tr>
                    <!-- Duration -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDuration" runat="server" Text="Duration:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDuration" runat="server" SkinID="EMS"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="EMS" Display="Dynamic" runat="server" ValidationExpression="[1-9][\d]*"
                                ErrorMessage="Wrong Format" ID="revDuration" ControlToValidate="txtDuration"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator SkinID="EMS" Display="Dynamic" runat="server" ErrorMessage="Required Field"
                                ID="rfvDuration" ControlToValidate="txtDuration"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Time Unit -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTimeUnitDuration" runat="server" Text="Time Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phTimeUnitDuration" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phTimeUnitDurationValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Interval -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblInterval" runat="server" Text="Interval:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox SkinID="EMS" ID="txtInterval" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="EMS" Display="Dynamic" runat="server" ValidationExpression="[1-9][\d]*"
                                ErrorMessage="Wrong Format" ID="revInterval" ControlToValidate="txtInterval"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator SkinID="EMS" Display="Dynamic" runat="server" ErrorMessage="Required Field"
                                ID="rfvInterval" ControlToValidate="txtInterval"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Time Unit -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTimeUnitInterval" runat="server" Text="Time Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phTimeUnitInterval" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phTimeUnitIntervalValidator" runat="server" />
                        </td>
                    </tr>                    
                    <!-- Aviso -->
                    <!-- Time Unit Notification -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTimeUnitNotificacion" runat="server" Text="Reminder Time Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phTimeUnitNotification" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Notificacion -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblNotification" runat="server" Text="Reminder:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox SkinID="EMS" ID="txtNotification" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="EMS" Display="Dynamic" runat="server" ValidationExpression="[1-9][\d]*"
                                ErrorMessage="Wrong Format" ID="revNotification" ControlToValidate="txtNotification"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    
                    <!-- MEDICION -->
                    <!-- Frequency -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFrequency" runat="server" Text="Frequency:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtFrequency" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="EMS" runat="server" ValidationExpression="[1-9][\d]*"
                                ErrorMessage="Wrong Format" ID="revFrequency" Display="Dynamic" ControlToValidate="txtFrequency"
                                Enabled="false"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                Enabled="false" ID="rfvFrequency" ControlToValidate="txtFrequency" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Time Unit -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTimeUnitFrequency" runat="server" Text="Time Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phTimeUnitFrequency" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phTimeUnitFrequencyValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Accounting Activity -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblAccountingActivity" runat="server" Text="Accounting Activity:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phAccountingActivity" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Accounting Scope -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblAccountingScope" runat="server" Text="Accounting Scope:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phAccountingScope" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Indicator y su class-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorClassification" runat="server" Text="Indicador de la Base:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicator" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Condition -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblParameterGroup" runat="server" Text="Condition:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upParameterGroup" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phParameterGroup" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phParameterGroupValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Measurement Unit (la magnitud la pone el Indicador seleccionado)-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementUnit" runat="server" Text="Measurement Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upMeasurementUnit" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phMeasurementUnit" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phMeasurementUnitValidator" runat="server" />
                        </td>
                    </tr>
                    
                    <!-- Apuntamos 15 Indicadores, que se usaran para los Resultados de las Transformaciones !!! -->
                    <!-- Indicator CO2e-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorCO2e" runat="server" Text="Indicator CO2e:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorCO2e" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorCO2eValidator" runat="server" />
                        </td>
                    </tr>
		            <!-- Indicator CO2efromCO2-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorCO2efromCO2" runat="server" Text="Indicator CO2e from CO2:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorCO2efromCO2" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorCO2efromCO2Validator" runat="server" />
                        </td>
                    </tr>
		            <!-- Indicator CO2-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorCO2" runat="server" Text="Indicator CO2:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorCO2" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorCO2Validator" runat="server" />
                        </td>
                    </tr>
		            <!-- Indicator CH4-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorCH4" runat="server" Text="Indicator CH4:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorCH4" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorCH4Validator" runat="server" />
                        </td>
                    </tr>
		            <!-- Indicator CO2eFromCH4-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorCO2eFromCH4" runat="server" Text="Indicator CO2e From CH4:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorCO2eFromCH4" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorCO2eFromCH4Validator" runat="server" />
                        </td>
                    </tr>
		            <!-- Indicator CO2efromN2O-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorCO2efromN2O" runat="server" Text="Indicator CO2e from N2O:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorCO2efromN2O" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorCO2efromN2OValidator" runat="server" />
                        </td>
                    </tr>
		            <!-- Indicator N2O-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorN2O" runat="server" Text="Indicator N2O:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorN2O" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorN2OValidator" runat="server" />
                        </td>
                    </tr>
		            <!-- Indicator SO2-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorSO2" runat="server" Text="Indicator SO2:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorSO2" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorSO2Validator" runat="server" />
                        </td>
                    </tr>
		            <!-- Indicator CO-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorCO" runat="server" Text="Indicator CO:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorCO" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorCOValidator" runat="server" />
                        </td>
                    </tr>
        		    <!-- Indicator NOx-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorNOx" runat="server" Text="Indicator NOx:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorNOx" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorNOxValidator" runat="server" />
                        </td>
                    </tr>
		            <!-- Indicator HCT-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorHCT" runat="server" Text="Indicator HCT:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorHCT" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorHCTValidator" runat="server" />
                        </td>
                    </tr>
		            <!-- Indicator HCNM-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorHCNM" runat="server" Text="Indicator HCNM:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorHCNM" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorHCNMValidator" runat="server" />
                        </td>
                    </tr>
		            <!-- Indicator MP10-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorMP10" runat="server" Text="Indicator MP10:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicatorMP10" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorMP10Validator" runat="server" />
                        </td>
                    </tr>


                    <!-- Apuntar 2 Unidades de medidas para luego usar internamente en las transformaciones Magnitud + MeasurementUnit  -->
                    <!-- Measurement Unit [Mg] -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementUnitMg" runat="server" Text="Measurement Unit [Mg]" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phMeasurementUnitMg" runat="server"></asp:PlaceHolder>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Measurement Unit [g/Gj] -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementUnitgGj" runat="server" Text="Measurement Unit [g/Gj]" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phMeasurementUnitgGj" runat="server"></asp:PlaceHolder>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Condition UNO por Cada Tarea Accesoria!! Filtrados por el Indicador de cada tarea -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblParameterGroupCO" runat="server" Text="Condition CO:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upParameterGroupCO" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phParameterGroupCO" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                    
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblParameterGroupMP10" runat="server" Text="Condition MP10:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upParameterGroupMP10" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phParameterGroupMP10" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                    
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblParameterGroupNOx" runat="server" Text="Condition NOx:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upParameterGroupNOx" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phParameterGroupNOx" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                    
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblParameterGroupN2O" runat="server" Text="Condition N2O:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upParameterGroupN2O" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phParameterGroupN2O" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                    
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblParameterGroupHCT" runat="server" Text="Condition HCT:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upParameterGroupHCT" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phParameterGroupHCT" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                    
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblParameterGroupCH4" runat="server" Text="Condition CH4:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upParameterGroupCH4" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phParameterGroupCH4" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                    
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblParameterGroupHCNM" runat="server" Text="Condition HCNM:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upParameterGroupHCNM" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phParameterGroupHCNM" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                    

                    <!-- Lo dejamos al Final -->
                    <!-- Asignacion de Operadores a la Tarea -->
                    <!-- Posts -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblPosts" runat="server" Text="Select the operators of the task:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upHierarchicalListManage" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="Treeview">
                                        <asp:PlaceHolder ID="phPosts" runat="server" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            <asp:CustomValidator ID="cvTreeView" Display="Dynamic" runat="server" SkinID="Validator"
                                Enabled="false" ErrorMessage="Required Field"></asp:CustomValidator>
                        </td>
                    </tr> 
                    <!-- Asignacion de email para Notificar errores en la Tarea -->
                    <!-- Emails -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblEmails" runat="server" Text="Select the e-mails person to notify:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upHierarchicalListManageEmails" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="Treeview">
                                        <asp:PlaceHolder ID="phEmails" runat="server" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- List emails-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblListEmails" runat="server" Text="Or undeclared Emails:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtListEmails" MaxLength="8000" TextMode="MultiLine" Rows="6"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator ID="revEmails" ControlToValidate="txtListEmails" runat="server" 
                                ErrorMessage="You must enter an valid email address. eg.: lewis@moten.com | lewis@moten.com, me@lewismoten.com | lewis@moten.com;me@lewismoten.com" 
                                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"> 
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>                                       
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
