<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="ProyectBuyerSummaryContent.aspx.cs" Inherits="Condesus.EMS.WebUI.Dashboard.ProyectBuyerSummaryContent" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

    <script type="text/javascript">

        function OpenSeries(e, idMeasurement) {
            var _IEXPLORER = 'Microsoft Internet Explorer';
            var _FIREFOX = 'Netscape';
            var _BrowserName = navigator.appName;

//            StopEvent(e);
            if (_BrowserName == _IEXPLORER) {   //IE and Opera
                var newWindow = window.open('/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + _idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            else {   //FireFox
                var newWindow = window.parent.open('/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + _idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            //window.open('/ManagementTools/PerformanceAssessment/IndicatorSeries.aspx?idMeasurement=' + _idMeasurement, 'IndicatorSeries', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            StopEvent(e);     //window.event.returnValue = false;
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

    <table border="0" cellpadding="0" cellspacing="0" class="contentProyectBuyerSummaryTable">
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDTitle">
                <asp:Label ID="lblNameProject" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="contentProyectBuyerSummaryTDPicture">
                <div>
                    <asp:UpdatePanel ID="upCounter" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:Image ID="imgShowSlide" runat="server" />
                            <asp:HiddenField ID="hdn_ImagePosition" runat="server" Value="0" />
                            <asp:ImageButton ID="btnPrevPicture" CommandArgument="-1" runat="server" CssClass="Back" />
                            <asp:ImageButton ID="btnNextPicture" CommandArgument="1" runat="server" CssClass="Next" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
            <td class="contentProyectBuyerSummaryTDInfo">
                <table border="0" cellpadding="0" cellspacing="0">
                    <colgroup width="200px" valign="top">
                    </colgroup>
                    <colgroup valign="top">
                    </colgroup>
                    <tr>
                        <td>
                            <asp:Label ID="lblCategory" CssClass="Title" runat="server" Text="Category: " />
                        </td>
                        <td>
                            <asp:Label ID="lblCategoryValue" CssClass="Text" runat="server" Text="&nbsp;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblErType" CssClass="Title" runat="server" Text="ER Type: " />
                        </td>
                        <td>
                            <asp:Label ID="lblErTypeValue" CssClass="Text" runat="server" Text="&nbsp;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLocation" runat="server" CssClass="Title" Text="Location: " />
                        </td>
                        <td>
                            <asp:Label ID="lblLocationValue" runat="server" CssClass="Text" Text="&nbsp;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCountry" runat="server" CssClass="Title" Text="Country: " />
                        </td>
                        <td>
                            <asp:Label ID="lblCountryValue" runat="server" CssClass="Text" Text="&nbsp;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLastVisit" runat="server" CssClass="Title" Text="Last Visit: " />
                        </td>
                        <td>
                            <asp:Label ID="lblLastVisitValue" runat="server" CssClass="Text" Text="&nbsp;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNextVisit" runat="server" CssClass="Title" Text="Next Visit: " />
                        </td>
                        <td>
                            <asp:Label ID="lblNextVisitValue" runat="server" CssClass="Text" Text="&nbsp;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDescription" CssClass="Title" runat="server" Text="Description: " />
                        </td>
                        <td>
                            <asp:Label ID="lblDescriptionValue" CssClass="Text" runat="server" Text="&nbsp;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStatusDescription" CssClass="Title" runat="server" Text="Status Description: " />
                        </td>
                        <td>
                            <asp:Label ID="lblStatusDescriptionValue" CssClass="Text" runat="server" Text="&nbsp;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDTitle">
                <asp:Label ID="lblSubTitle" runat="server" Text="Emission Reduction Evolution" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDEmission">
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
                                            <DateInput Width="40px" ID="DateInput1" DateFormat="MM/dd/yyyy" runat="server">
                                            </DateInput>
                                            <Calendar ID="Calendar1" runat="server" Skin="EMS">
                                            </Calendar>
                                        </rad:RadDatePicker>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblEndDate" runat="server" Text="End Date" />
                                        <rad:RadDatePicker MinDate="1900-01-01" MaxDate="2099-01-01" ID="radcalEndDate" runat="server">
                                            <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                            <DateInput Width="40px" ID="DateInput2" DateFormat="MM/dd/yyyy" runat="server">
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
                            <asp:UpdatePanel ID="upchtIndicator" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <DCWC:Chart ID="chtIndicator" runat="server" BackColor="#ffffff" BackGradientEndColor="226, 239, 247"
                                        BorderLineColor="26, 59, 105" BorderLineStyle="Solid" BorderLineWidth="0" Palette="None"
                                        Width="800px" Height="450px" ImageType="Jpeg">
                                        <Legends>
                                            <DCWC:Legend AutoFitText="False" BackColor="White" BorderColor="26, 59, 105" Font="Verdana, 7pt"
                                                Name="Default">
                                            </DCWC:Legend>
                                        </Legends>
                                        <UI>
                                            <ContextMenu Enabled="True">
                                            </ContextMenu>
                                            <Toolbar Placement="InsideChart">
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
                                    </DCWC:Chart>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDTitle">
                <asp:Label ID="lblCurrent" runat="server" Text="Current ERs"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDGrid">
                <asp:UpdatePanel ID="upCurrent" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="pnlSummary" runat="server">
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDTitle">
                <asp:Label ID="lblTitleTotals" runat="server" Text="Project Performance" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDEmission">
                <table border="0" cellpadding="0" cellspacing="0" class="Filter">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="upChartTotals" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <DCWC:Chart ID="chtTotals" runat="server" BackColor="#ffffff" BackGradientEndColor="226, 239, 247"
                                        BorderLineColor="26, 59, 105" BorderLineStyle="Solid" BorderLineWidth="0" Palette="None"
                                        Width="800px" Height="450px" ImageType="Jpeg">
                                        <UI>
                                            <ContextMenu Enabled="True">
                                            </ContextMenu>
                                            <Toolbar BorderColor="26, 59, 105" Placement="InsideChart">
                                                <BorderSkin PageColor="60, 60, 60" SkinStyle="Emboss" />
                                            </Toolbar>
                                        </UI>
                                        <Titles>
                                            <DCWC:Title Color="60, 60, 60" Font="Verdana, 7" Name="Title1">
                                            </DCWC:Title>
                                        </Titles>
                                        <Series>
                                            <DCWC:Series Name="PDD" ChartArea="AreaData" MarkerStyle="Circle" MarkerSize="7"
                                                Color="Red" XValueType="String">
                                                <SmartLabels Enabled="True" />
                                            </DCWC:Series>
                                            <DCWC:Series Name="Calculated" ChartArea="AreaData" Color="Blue" MarkerStyle="Diamond"
                                                MarkerSize="7" XValueType="String">
                                            </DCWC:Series>
                                            <DCWC:Series Name="Issued" ChartArea="AreaData" Color="Green" MarkerStyle="Triangle"
                                                MarkerSize="7" XValueType="String">
                                            </DCWC:Series>
                                        </Series>
                                        <ChartAreas>
                                            <DCWC:ChartArea BackColor="White" BorderColor="26, 59, 105" BorderStyle="Solid" Name="AreaData"
                                                BackGradientEndColor="213, 241, 255" BackGradientType="DiagonalRight">
                                                <AxisY LabelsAutoFit="False" TitleFont="Verdana, 9.75pt, style=Bold">
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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDTitle">
                <asp:Label ID="lblVerification" runat="server" Text="Issued ERs"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDGrid">
                <asp:UpdatePanel ID="upVerifications" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="pnlVerifications" runat="server">
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDTitle">
                <asp:Label ID="lblKeyIndicator" runat="server" Text="Key Parameters"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDGrid">
                <asp:UpdatePanel ID="upKeyIndicator" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="pnlKeyIndicator" runat="server">
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDTitle">
                <asp:Label ID="lblDocuments" runat="server" Text="Documents"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="contentProyectBuyerSummaryTDGrid">
                <asp:UpdatePanel ID="upDocuments" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="pnlDocuments" runat="server">
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
