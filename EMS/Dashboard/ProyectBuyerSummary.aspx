<%@ Page Title="" Language="C#" MasterPageFile="~/EMSPopUp.Master" AutoEventWireup="true"
    CodeBehind="ProyectBuyerSummary.aspx.cs" Inherits="Condesus.EMS.WebUI.Dashboard.ProyectBuyerSummary" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPopup" runat="server">
    <div class="ConteinerReport">
        <div class="Button">
            <input type="button" value="Print" onclick="javaScript:window.print();" />
        </div>

        <script type="text/javascript">

        function OpenSeries(e, idMeasurement) {
            var _IEXPLORER = 'Microsoft Internet Explorer';
            var _FIREFOX = 'Netscape';
            var _BrowserName = navigator.appName;

//            StopEvent(e);
            if (_BrowserName == _IEXPLORER)
            {   //IE and Opera
                var newWindow = window.open('/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + _idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            else
            {   //FireFox
                var newWindow = window.parent.open('/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + _idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            //window.open('/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + _idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            StopEvent(e);
        }

        function OpenCalculationChart(e, idMeasurement) {
            var _IEXPLORER = 'Microsoft Internet Explorer';
            var _FIREFOX = 'Netscape';
            var _BrowserName = navigator.appName;

//            StopEvent(e);
            if (_BrowserName == _IEXPLORER) {   //IE and Opera
                var newWindow = window.open('/ManagementTools/PerformanceAssessment/CalculationChart.aspx?idCalculation=' + _idCalculation, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            else {   //FireFox
                var newWindow = window.parent.open('/ManagementTools/PerformanceAssessment/CalculationChart.aspx?idCalculation=' + _idCalculation, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            //window.open('/ManagementTools/PerformanceAssessment/CalculationChart.aspx?idCalculation=' + _idCalculation, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            StopEvent(e);     //window.event.returnValue = false;
        }


        //Esta funcion es la encargada de hacer el show de las series de datos
        //Parametros    <e> event
        //          <idGridRow> el indice del row en donde esta parado
        //          <idGridTable> el id de la tabla donde esta parado(por si es jerarquica)
        function ShowSeries(e, idMeasurement) {
            //Abre una nueva ventana con el reporte.
            var _IEXPLORER = 'Microsoft Internet Explorer';
            var _FIREFOX = 'Netscape';
            var _BrowserName = navigator.appName;

//            StopEvent(e);
            if (_BrowserName == _IEXPLORER) {   //IE and Opera
                var newWindow = window.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + '/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            else {   //FireFox
                var newWindow = window.parent.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + '/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            //var newWindow = window.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + '/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            newWindow.focus();
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            StopEvent(e);     //window.event.returnValue = false;
        }

        //Esta funcion es la encargada de hacer el show del chart
        //Parametros    <e> event
        //          <idGridRow> el indice del row en donde esta parado
        //          <idGridTable> el id de la tabla donde esta parado(por si es jerarquica)
        function ShowChart(e, idMeasurement) {
            //Abre una nueva ventana con el reporte.
            var _IEXPLORER = 'Microsoft Internet Explorer';
            var _FIREFOX = 'Netscape';
            var _BrowserName = navigator.appName;

//            StopEvent(e);
            if (_BrowserName == _IEXPLORER) {   //IE and Opera
                var newWindow = window.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + '/ManagementTools/PerformanceAssessment/MeasurementChart.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            else {   //FireFox
                var newWindow = window.parent.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + '/ManagementTools/PerformanceAssessment/MeasurementChart.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            //var newWindow = window.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + '/ManagementTools/PerformanceAssessment/MeasurementChart.aspx?idMeasurement=' + idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            newWindow.focus();
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            StopEvent(e);     //window.event.returnValue = false;
        }    
        </script>

        <asp:Label ID="lblNameProject" runat="server" CssClass="Title"></asp:Label>
        <table id="tblContentForm" runat="server" class="ContentFormReport">
            <colgroup class="ColTitle" />
            <colgroup class="ColContent" />
            <!-- Title -->
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblProjectName" runat="server" Text="Project Name" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblProjectNameValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblPDDName" runat="server" Text="PDD Name" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblPDDNameValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblPINumber" runat="server" Text="PI Number" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblPINumberValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblProjectType" runat="server" Text="Project Type" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblProjectTypeValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblCategory" runat="server" Text="Category" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblCategoryValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblUNFCCC" runat="server" Text="UNFCCC Reference Number" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblUNFCCCValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblVerificationDOE" runat="server" Text="Verification DOE" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblVerificationDOEValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblVerificationFrequency" runat="server" Text="Verification Frequency" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblVerificationFrequencyValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblNextVerificationDate" runat="server" Text="Next Verification Date" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblNextVerificationDateValue" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblProjectInformation" runat="server" Text="Project Information" CssClass="Title"></asp:Label>
        <asp:Label ID="lblLocation" runat="server" Text="Location" CssClass="SubTitle"></asp:Label>
        <table id="tblContentForm2" runat="server" class="ContentFormReport">
            <colgroup>
                <col class="ColTitle" />
                <col class="ColText" />
            </colgroup>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblCountryPB" runat="server" Text="Country" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblCountryPBValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblProvince" runat="server" Text="Province / State / Dept / City" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblProvinceValue" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblProjectSponsor" runat="server" Text="Project Sponsor(s)" CssClass="Title"></asp:Label>
        <table id="tblContentForm3" runat="server" class="ContentFormReport">
            <colgroup>
                <col class="ColTitle" />
                <col />
            </colgroup>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblCompanyDescription" runat="server" Text="Company Description" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblCompanyDescriptionValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblContactInformation" runat="server" Text="Contact Information" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblContactInformationValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblConsultantIntermediary" runat="server" Text="Consultant / Intermediary" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblConsultantIntermediaryValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblProjectInvestor" runat="server" Text="Project Investor or Lenders" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblProjectInvestorValue" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblBriefProjectDescription" runat="server" Text="Brief Project Description & Status"
            CssClass="Title"></asp:Label>
        <table id="tblContentForm4" runat="server" class="ContentFormReport">
            <colgroup>
                <col class="ColTitle" />
                <col class="ColText" />
            </colgroup>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblProjectDescription" runat="server" Text="Brief Description of Project" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblProjectDescriptionValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblBriefDescription" runat="server" Text="Brief Description of Project" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblBriefDescriptionValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblCDM" runat="server" Text="CDM / JI / Voluntar & Status" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblCDMValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblMethodologyNumber" runat="server" Text="Methodology Number" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblMethodologyNumberValue" runat="server" ForeColor="#333333" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblCreditingPeriod" runat="server" Text="Crediting Period Starting Date" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblCreditingPeriodValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblStatusDescription" runat="server" Text="Status Description" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblStatusDescriptionValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblBriefDescriptionER" runat="server" Text="Brief Description of ER Deviations From PDD Estimations" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblBriefDescriptionERValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblUnexpectedEvents" runat="server" Text="Unexpected Events Affecting ERS (Equipment Down, Plant Down, Software Alarms)" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblUnexpectedEventsValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblExpectedEvents" runat="server" Text="Expected Events Affecting ERS (Expected Variations, Seasonal Variations, Changes in Regulations, Sectorial Characteristics)" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblExpectedEventsValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblTotalTonnes" runat="server" Text="Total Tonnes of CO2 Equivalent up to 2012" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblTotalTonnesValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblLifeTime" runat="server" Text="Life Time of the ER Project" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblLifeTimeValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblTotalTonnesOfCO2" runat="server" Text="Total tonnes of CO2 after 2012" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblTotalTonnesOfCO2Value" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblVerification" runat="server" CssClass="Title" Text="Issued ERs"></asp:Label><br />
        <asp:UpdatePanel ID="upVerifications" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Panel ID="pnlVerifications" runat="server">
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table class="ContentFormReport">
            <colgroup class="ColTitle" />
            <colgroup class="ColContent" />
            <!-- Title -->
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblMGMMonitoringContact" runat="server" Text="EMS Monitoring Contact Person" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblMGMMonitoringContactValue" runat="server" />
                </td>
            </tr>
        </table>
        <asp:Label ID="lblSubTitle" CssClass="Title" runat="server" Text="Emission Reduction Evolution" />
        <div class="Chart">
            <table border="0" cellpadding="0" cellspacing="0" class="Filter">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" class="FilterComand">
                            <tr>
                                <td>
                                    <asp:Label ID="lblStartDate" runat="server" Text="Start Date" />
                                    <rad:RadDatePicker MinDate="1900-01-01" MaxDate="2099-01-01" ID="radcalStartDate"
                                        runat="server">
                                        <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                        <DateInput Width="40px" ID="DateInput1" DateFormat="MM/dd/yyyy" runat="server" >
                                        </DateInput>
                                        <Calendar ID="Calendar1" runat="server" Skin="EMS">
                                        </Calendar>
                                    </rad:RadDatePicker>
                                </td>
                                <td>
                                    <asp:Label ID="lblEndDate" runat="server" Text="End Date" />
                                    <rad:RadDatePicker MinDate="1900-01-01" MaxDate="2099-01-01" ID="radcalEndDate" runat="server">
                                        <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                        <DateInput Width="40px" ID="DateInput2" DateFormat="MM/dd/yyyy" runat="server" >
                                        </DateInput>
                                        <Calendar ID="Calendar2" runat="server" Skin="EMS">
                                        </Calendar>
                                    </rad:RadDatePicker>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgBtnRefreshChart" runat="server" Height="16px" Width="16px"
                                        ImageUrl="../../Skins/Images/Trans.gif" CssClass="ImgBtnRefreshChart" AlternateText="Apply" />
                                    <asp:ImageButton ID="imgBtnResetChart" runat="server" Height="16px" Width="16px"
                                        ImageUrl="../../Skins/Images/Trans.gif" CssClass="ImgBtnResetChart" AlternateText="Reset" />
                                </td>
                            </tr>
                        </table>
                        <DCWC:Chart ID="chtIndicator" runat="server" BackColor="#ffffff" BackGradientEndColor="226, 239, 247"
                            BorderLineColor="26, 59, 105" BorderLineStyle="Solid" BorderLineWidth="0" Palette="None"
                            Width="750px" Height="450px" ImageType="Jpeg">
                            <Legends>
                                <DCWC:Legend AutoFitText="False" BackColor="White" BorderColor="26, 59, 105" Font="Verdana, 7pt"
                                    Name="Default">
                                </DCWC:Legend>
                            </Legends>
                            <UI>
                                <ContextMenu Enabled="True">
                                </ContextMenu>
                                <Toolbar BorderColor="26, 59, 105" Placement="InsideChart">
                                    <BorderSkin PageColor="60, 60, 60" SkinStyle="Emboss" />
                                </Toolbar>
                            </UI>
                            <Titles>
                                <DCWC:Title Color="60, 60, 60" Font="Verdana, 7pt" Name="Title1">
                                </DCWC:Title>
                            </Titles>
                            <Series>
                                <DCWC:Series Name="PDD ERs" ChartArea="AreaData" MarkerStyle="Circle" MarkerSize="7"
                                    Color="Red" XValueType="DateTime">
                                    <SmartLabels Enabled="True" />
                                </DCWC:Series>
                                <DCWC:Series Name="Current ERs" ChartArea="AreaData" Color="Blue" MarkerStyle="Diamond"
                                    MarkerSize="7" XValueType="DateTime">
                                </DCWC:Series>
                                <DCWC:Series Name="Issued ERs" ChartArea="AreaData" Color="Green" MarkerStyle="Triangle"
                                    MarkerSize="7" XValueType="DateTime">
                                </DCWC:Series>
                            </Series>
                            <ChartAreas>
                                <DCWC:ChartArea BackColor="White" BorderColor="26, 59, 105" BorderStyle="Solid" Name="AreaData"
                                    BackGradientEndColor="213, 241, 255" BackGradientType="DiagonalRight">
                                    <AxisY LabelsAutoFit="False" Title="tCO2" TitleFont="Verdana, 9.75pt, style=Bold">
                                        <MajorGrid LineColor="Silver" />
                                        <MinorGrid LineColor="Silver" />
                                        <LabelStyle Font="Verdana, 6.75pt" />
                                    </AxisY>
                                    <AxisX LabelsAutoFit="False">
                                        <MajorGrid LineColor="Silver" />
                                        <MinorGrid LineColor="Silver" />
                                        <LabelStyle Font="Verdana, 7pt" />
                                    </AxisX>
                                    <AxisX2>
                                        <MajorGrid LineColor="Silver" />
                                        <MinorGrid LineColor="Silver" />
                                    </AxisX2>
                                    <AxisY2>
                                        <MajorGrid LineColor="Silver" />
                                        <MinorGrid LineColor="Silver" />
                                    </AxisY2>
                                    <Area3DStyle WallWidth="0" />
                                </DCWC:ChartArea>
                            </ChartAreas>
                            <BorderSkin FrameBackColor="51, 102, 153" FrameBackGradientEndColor="CornflowerBlue"
                                PageColor="AliceBlue" />
                        </DCWC:Chart>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
