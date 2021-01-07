<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="ReportIndicatorTracker.aspx.cs" Inherits="Condesus.EMS.WebUI.ReportIndicatorTracker" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <script type="text/javascript">
        function ExportMeasurementSeries(e, idMeasurement, idTransformation, startDate, endDate) {
            debugger;
            var _IEXPLORER = 'Microsoft Internet Explorer';
            var _FIREFOX = 'Netscape';
            var _BrowserName = navigator.appName;

            if (_BrowserName == _IEXPLORER) {   //IE and Opera
                var newWindow = window.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + "/Managers/ExportMeasurementSeries.aspx?IdMeasurement=" + idMeasurement + "&IdTransformation=" + idTransformation + "&StartDate=" + startDate + "&EndDate=" + endDate, 'Export', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            else {   //FireFox
                var newWindow = window.parent.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + "/Managers/ExportMeasurementSeries.aspx?IdMeasurement=" + idMeasurement + "&IdTransformation=" + idTransformation + "&StartDate=" + startDate + "&EndDate=" + endDate, 'Export', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }

            newWindow.focus();
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            StopEvent(e);
        }
    </script>
    
    <!-- Filtros -->
    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <table id="tblContentForm" runat="server" class="ContentForm">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                        <col class="ColValidator" />
                    </colgroup>
                    <!-- From -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFrom" runat="server" Text="<%$ Resources:CommonListManage, From %>" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDatePicker ID="rdtFrom" Skin="EMS" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput ID="diFrom" DateFormat="MM/dd/yyyy" runat="server" />
                                <Calendar ID="Calendar1" runat="server">
                                </Calendar>
                            </rad:RadDatePicker>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Through -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblThrough" runat="server" Text="<%$ Resources:CommonListManage, Through %>" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDatePicker ID="rdtThrough" Skin="EMS" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput ID="diThrough" DateFormat="MM/dd/yyyy" runat="server" />
                                <Calendar ID="Calendar2" runat="server">
                                </Calendar>
                            </rad:RadDatePicker>
                        </td>
                        <td class="ColValidator">
                            <asp:CustomValidator ID="customvEndDate" runat="server" ControlToValidate="rdtThrough"
                                SkinID="EMS" ErrorMessage="The second date must be after the first one" Display="Dynamic"></asp:CustomValidator>
                        </td>
                    </tr>
                    <!-- Indicator y su class-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorClassification" runat="server" Text="Indicator:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicator" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorValidator" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:UpdatePanel runat="server" ID="upGridFacilities" UpdateMode="Always">
        <ContentTemplate>
            <div style="padding: 10px 10px 0 10px;">
                <asp:PlaceHolder ID="phGridFacilities" runat="server"></asp:PlaceHolder>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    

    <br />
    <br />
    <asp:UpdatePanel runat="server" ID="upEvolutionGrid" UpdateMode="Always">
        <ContentTemplate>
            <div style="margin: 0px auto; width: 1000px;">
                <div class="divContentReportChartBarStacked1">
                    <rad:RadChart Width="1000px" Height="650px" ID="chartIndicatorTracker" runat="server"
                        AutoLayout="true" Skin="Office2007" SkinsOverrideStyles="false" AutoTextWrap="false">
                        <Appearance FillStyle-FillType="Solid" FillStyle-MainColor="#ffffff">
                            <Border Color="Transparent" />
                            <FillStyle MainColor="Transparent" SecondColor="Transparent" GammaCorrection="false"
                                FillType="Solid">
                            </FillStyle>
                        </Appearance>
                        <ChartTitle>
                            <Appearance Border-Color="Transparent" FillStyle-MainColor="Transparent">
                                <FillStyle MainColor="Transparent">
                                </FillStyle>
                                <Border Color="Transparent" />
                            </Appearance>
                            <TextBlock Appearance-TextProperties-Color="#4a5678">
                                <Appearance TextProperties-Color="74, 86, 120" TextProperties-Font="Arial, 12px">
                                </Appearance>
                            </TextBlock>
                        </ChartTitle>
                        <Legend>
                            <Appearance>
                                <ItemTextAppearance TextProperties-Color="Black">
                                </ItemTextAppearance>
                                <ItemMarkerAppearance Figure="Square">
                                </ItemMarkerAppearance>
                                <Border Color="#ffffff" />
                            </Appearance>
                        </Legend>
                        <PlotArea>
                            <XAxis DataLabelsColumn="Activity">
                                <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134">
                                    <MajorGridLines Color="134, 134, 134" Width="0" />
                                    <LabelAppearance RotationAngle="-90">
                                    </LabelAppearance>
                                    <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                    </TextAppearance>
                                </Appearance>
                                <AxisLabel>
                                    <TextBlock>
                                        <Appearance TextProperties-Color="Black">
                                        </Appearance>
                                    </TextBlock>
                                </AxisLabel>
                                <Items>
                                    <rad:ChartAxisItem>
                                    </rad:ChartAxisItem>
                                    <rad:ChartAxisItem Value="1">
                                    </rad:ChartAxisItem>
                                    <rad:ChartAxisItem Value="2">
                                    </rad:ChartAxisItem>
                                    <rad:ChartAxisItem Value="3">
                                    </rad:ChartAxisItem>
                                    <rad:ChartAxisItem Value="4">
                                    </rad:ChartAxisItem>
                                    <rad:ChartAxisItem Value="5">
                                    </rad:ChartAxisItem>
                                    <rad:ChartAxisItem Value="6">
                                    </rad:ChartAxisItem>
                                    <rad:ChartAxisItem Value="7">
                                    </rad:ChartAxisItem>
                                </Items>
                            </XAxis>
                            <YAxis>
                                <Appearance Color="134, 134, 134" MajorTick-Color="134, 134, 134" MinorTick-Color="134, 134, 134">
                                    <MajorGridLines Color="134, 134, 134" />
                                    <MinorGridLines Color="134, 134, 134" />
                                    <TextAppearance TextProperties-Color="Black">
                                    </TextAppearance>
                                </Appearance>
                                <AxisLabel>
                                    <TextBlock>
                                        <Appearance TextProperties-Color="Black">
                                        </Appearance>
                                    </TextBlock>
                                </AxisLabel>
                            </YAxis>
                            <Appearance>
                                <FillStyle FillType="Solid" MainColor="">
                                </FillStyle>
                            </Appearance>
                        </PlotArea>
                    </rad:RadChart>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <br />
    <br />
    <br />
    
    <asp:UpdatePanel ID="upTracker" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="padding: 10px 10px 0 10px;">
                <div class="divContentTabStrip">
                    <asp:Label ID="lblSubTitle" runat="server" Text="Related Data" CssClass="lblSubTitle" />
                    <asp:Panel ID="pnlTabStrip" runat="server" CssClass="pnlTabStrip">
                        <rad:RadTabStrip ID="rtsIndicatorTracker" runat="server" MultiPageID="rmpIndicatorTracker"
                            EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false" AutoPostBack="false"
                            CausesValidation="false" SelectedIndex="0" Align="Right">
                            <Tabs>
                                <rad:RadTab CssClass="contentListViewDataSeries" SelectedCssClass="contentListViewDataSeriesOpen" ToolTip="<%$ Resources:Common, DataSeries %>">
                                </rad:RadTab>
                                <rad:RadTab CssClass="contentListViewStats" SelectedCssClass="contentListViewStatsOpen" ToolTip="<%$ Resources:CommonListManage, Statistics %>">
                                </rad:RadTab>
                            </Tabs>
                        </rad:RadTabStrip>
                    </asp:Panel>
                </div>
                <asp:Panel ID="pnlListIndicatorTracker" runat="server">
                    <rad:RadMultiPage ID="rmpIndicatorTracker" runat="server" SelectedIndex="0">
                        <rad:RadPageView ID="rpvDataSeries" runat="server" Selected="true">
                            <asp:PlaceHolder ID="phGridDataSeries" runat="server" ></asp:PlaceHolder>
                        </rad:RadPageView>
                        <rad:RadPageView ID="rpvStats" runat="server" Selected="false">
                            <asp:PlaceHolder ID="phGridStats" runat="server" ></asp:PlaceHolder>
                        </rad:RadPageView>
                    </rad:RadMultiPage>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
                        
            
</asp:Content>
