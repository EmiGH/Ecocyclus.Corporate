<%@ Page Language="C#" MasterPageFile="~/EMSPopUpReport.Master" AutoEventWireup="true"
    CodeBehind="ReportCalculationsOfTransformation.aspx.cs" Inherits="Condesus.EMS.WebUI.Dashboard.ReportCalculationsOfTransformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPopUp" runat="server">

    <script src="../AppCode/JavaScriptCore/jquery-1.5.2.js" type="text/javascript"></script>

    <script src="../AppCode/JavaScriptCore/jquery.treeTable.js" type="text/javascript"></script>

    <script type="text/javascript">
        function ShowLoading() {
            $get('FWMasterGlobalUpdateProgress').style.display = 'block';
        }
        function HideLoading() {
            $get('FWMasterGlobalUpdateProgress').style.display = 'none';
        }  
    </script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#ctl00_ContentPopUp_tblTreeTableReport").treeTable();
        });
    </script>

    <div class="divContentReport">
        <div style="float: left;">
            <asp:Label ID="lblProcessValue" runat="server" CssClass="lblOrganizationValue"></asp:Label>
            <br />
            <asp:Label ID="lblReportFilter" runat="server" CssClass="ReportFilterSubTitle"></asp:Label>
            <br />
        </div>
        
        <asp:LinkButton ID="lnkPrint" runat="server" CssClass="lnkPrint" Text="Print" OnClientClick="javaScript:window.print();return false;"></asp:LinkButton>
        <asp:LinkButton ID="lnkExport" runat="server" CssClass="lnkExport" Text="Export" OnClick="lnkExportGridMeasurement_Click"></asp:LinkButton>
        <asp:LinkButton ID="lnkExpand" runat="server" CssClass="lnkExpand" Text="Expand"
            OnClientClick="$('#ctl00_ContentPopUp_tblTreeTableReport').expandAll(); return false;"></asp:LinkButton>
                    
        <asp:Panel ID="pnlReports" runat="server">
            <rad:RadTabStrip ID="rtsReportCalculationsOfTransformation" runat="server" MultiPageID="rmpReportCalculationsOfTransformation"
                SelectedIndex="0" EnableEmbeddedSkins="false" Skin="Report">
                <Tabs>
                    <rad:RadTab Text="Report" Value="Grid">
                    </rad:RadTab>
                    <rad:RadTab Text="Charts" Value="Charts">
                    </rad:RadTab>
                </Tabs>
            </rad:RadTabStrip>
            <rad:RadMultiPage ID="rmpReportCalculationsOfTransformation" runat="server" SelectedIndex="0">
                <rad:RadPageView ID="rpvGrid" runat="server" Selected="true">
                    <asp:UpdatePanel ID="upReportGrid" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <!-- Report -->
                            <table class="tblTreeTableReport" id="tblTreeTableReport" runat="server" cellpadding="0"
                                cellspacing="0">
                                <thead>
                                    <tr>
                                        <th runat="server" id="title" class="rptColumnHeaderTitle">
                                            Title
                                        </th>
                                        <th runat="server" id="tCO2e" class="rptColumnHeaderResult">
                                            CO<sub>2e</sub> [tn]
                                        </th>
                                        <th runat="server" id="CO2" class="rptColumnHeaderResult">
                                            CO<sub>2</sub> [tCO<sub>2e</sub>]
                                        </th>
                                        <th runat="server" id="CH4" class="rptColumnHeaderResult">
                                            CH<sub>4</sub> [tCO<sub>2e</sub>]
                                        </th>
                                        <th runat="server" id="N20" class="rptColumnHeaderResult">
                                            N<sub>2</sub>O [tCO<sub>2e</sub>]
                                        </th>
                                        <th runat="server" id="PFC" class="rptColumnHeaderResult">
                                            PFC [tCO<sub>2e</sub>]
                                        </th>
                                        <th runat="server" id="HFC" class="rptColumnHeaderResult">
                                            HFC [tCO<sub>2e</sub>]
                                        </th>
                                        <th runat="server" id="SF6" class="rptColumnHeaderResult">
                                            SF<sub>6</sub> [tCO<sub>2e</sub>]
                                        </th>
                                        <th runat="server" id="HCT" class="rptColumnHeaderResult">
                                            HCT [Mg]
                                        </th>
                                        <th runat="server" id="HCNM" class="rptColumnHeaderResult">
                                            HCNM [Mg]
                                        </th>
                                        <th runat="server" id="C2H6" class="rptColumnHeaderResult">
                                            C<sub>2</sub>H<sub>6</sub> [Mg]
                                        </th>
                                        <th runat="server" id="C3H8" class="rptColumnHeaderResult">
                                            C<sub>3</sub>H<sub>8</sub> [Mg]
                                        </th>
                                        <th runat="server" id="C4H10" class="rptColumnHeaderResult">
                                            C<sub>4</sub>H<sub>10</sub> [Mg]
                                        </th>
                                        <th runat="server" id="CO" class="rptColumnHeaderResult">
                                            CO [Mg]
                                        </th>
                                        <th runat="server" id="NOx" class="rptColumnHeaderResult">
                                            NO<sub>x</sub> [Mg]
                                        </th>
                                        <th runat="server" id="SOx" class="rptColumnHeaderResult">
                                            SO<sub>x</sub> [Mg]
                                        </th>
                                        <th runat="server" id="SO2" class="rptColumnHeaderResult">
                                            SO<sub>2</sub> [Mg]
                                        </th>
                                        <th runat="server" id="H2S" class="rptColumnHeaderResult">
                                            H<sub>2</sub>S [Mg]
                                        </th>
                                        <th runat="server" id="PM" class="rptColumnHeaderResult">
                                            PM [Mg]
                                        </th>
                                        <th runat="server" id="PM10" class="rptColumnHeaderResult">
                                            PM<sub>10</sub> [Mg]
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tbody">
                                </tbody>
                            </table>
                            <!-- FIN del Report -->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </rad:RadPageView>
                <rad:RadPageView ID="rpvTotals" runat="server" Selected="false">
                    <rad:RadTabStrip ID="rtsCharts" runat="server" MultiPageID="rmpCharts" SelectedIndex="0"
                        EnableEmbeddedSkins="false" Skin="Report" BorderWidth="0px" Align="Right">
                        <Tabs>
                            <rad:RadTab Text="Scopes" Value="Scopes">
                            </rad:RadTab>
                            <rad:RadTab Text="Activities" Value="Activities">
                            </rad:RadTab>
                            <rad:RadTab Text="Geographical Areas" Value="GeographicalAreas">
                            </rad:RadTab>
                            <rad:RadTab Text="Facility Types" Value="FacilityTypes">
                            </rad:RadTab>
                            <rad:RadTab Text="Facilities" Value="Facilities">
                            </rad:RadTab>
                        </Tabs>
                    </rad:RadTabStrip>
                    <asp:UpdatePanel ID="upOptionChartType" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:Panel ID="pnlOptionChartType" runat="server">
                                <!-- Opciones para los chart -->
                                <table id="tblContentFilter" runat="server" class="ContentFormReport">
                                    <colgroup>
                                        <col class="ColTitle" />
                                        <col class="ColContent" />
                                    </colgroup>
                                    <tr class="trPar" runat="server" id="trOptionChartType">
                                        <td class="ColTitle">
                                            <asp:Label ID="lblModeBar" runat="server" Text="Mode:" />
                                        </td>
                                        <td class="ColContent">
                                            <asp:RadioButtonList ID="rblOptionChartType" runat="server" AutoPostBack="true" CssClass="rblOptionChartType"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Stacked Bar" Selected="True" Value="Stacked"></asp:ListItem>
                                                <asp:ListItem Text="Stacked Bar 100" Selected="False" Value="Stacked100"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <!-- Accounting Scope -->
                                    <tr class="TRImpar" runat="server" id="trOptionScopes">
                                        <td class="ColTitle">
                                            <asp:Label ID="lblScopeGeneral" runat="server" Text="Accounting Scope:"></asp:Label>
                                        </td>
                                        <td class="ColContent">
                                            <asp:PlaceHolder ID="phScopeGeneral" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <rad:RadMultiPage ID="rmpCharts" runat="server" SelectedIndex="0">
                        <rad:RadPageView ID="rpvScopes" runat="server" Selected="true">
                            <asp:UpdatePanel ID="upScopes" runat="server" UpdateMode="Always">
                                <ContentTemplate>                    
                                    <div style="margin: 0px auto; width: 1000px;">
                                        <!-- Chart PIE Scopes By Indicator -->
                                        <div class="divContentReportChartBarStacked">
                                            <rad:RadChart ID="chartTotalScopeByIndicator" runat="server" Height="300px" Width="980px"
                                                OnItemDataBound="chartTotalScopeByIndicator_ItemDataBound" OnDataBound="charts_DataBound"
                                                AutoLayout="false" AutoTextWrap="false" Skin="Office2007" SkinsOverrideStyles="false">
                                                <Appearance FillStyle-FillType="Solid" FillStyle-MainColor="#ffffff">
                                                    <Border Color="Transparent" />
                                                    <FillStyle MainColor="Transparent" SecondColor="Transparent" GammaCorrection="false"
                                                        FillType="Solid">
                                                    </FillStyle>
                                                </Appearance>
                                                <ChartTitle>
                                                    <Appearance Border-Color="Transparent" FillStyle-MainColor="Transparent">
                                                    </Appearance>
                                                    <TextBlock Appearance-TextProperties-Color="#4a5678" Appearance-TextProperties-Font="Arial, 12px">
                                                    </TextBlock>
                                                </ChartTitle>
                                                <Legend>
                                                    <Appearance>
                                                        <ItemTextAppearance Dimensions-Width="300px" Dimensions-Height="300px">
                                                        </ItemTextAppearance>
                                                        <Border Color="#ffffff" />
                                                    </Appearance>
                                                </Legend>
                                                <PlotArea  Appearance-Dimensions-Width="460px" Appearance-Dimensions-Height="250px"
                                                    Appearance-Dimensions-AutoSize="false">
                                                    <Appearance>
                                                        <Border Color="Transparent" />
                                                    </Appearance>
                                                </PlotArea>
                                                <Series>
                                                    <rad:ChartSeries Name="Series 1" Type="Pie" DataYColumn="Percentage">
                                                        <Appearance LegendDisplayMode="ItemLabels">
                                                        </Appearance>
                                                    </rad:ChartSeries>
                                                </Series>
                                            </rad:RadChart>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </rad:RadPageView>
                        <rad:RadPageView ID="rpvActivities" runat="server" Selected="false">
                            <div style="margin: 0px auto; width: 1000px;">
                                <asp:UpdatePanel ID="upActivites" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <!-- Chart BAR Activity by Scope -->
                                        <div class="divContentReportChartBarStacked1">
                                            <rad:RadChart Width="490px" Height="450px" ID="chartBarActivityByScope1" runat="server"
                                                AutoLayout="true" Skin="Office2007" SkinsOverrideStyles="false" AutoTextWrap="false"
                                                OnItemDataBound="chartBarActivityByScope1_ItemDataBound" OnDataBound="charts_DataBound">
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
                                                        <Appearance TextProperties-Color="74, 86, 120" TextProperties-Font="Arial, 12px"
                                                            Position-AlignedPosition="TopLeft" AutoTextWrap="Auto">
                                                        </Appearance>
                                                    </TextBlock>
                                                </ChartTitle>
                                                <Legend>
                                                    <Appearance Visible="true">
                                                        <ItemTextAppearance TextProperties-Color="Black">
                                                        </ItemTextAppearance>
                                                        <ItemMarkerAppearance Figure="Square">
                                                        </ItemMarkerAppearance>
                                                        <Border Color="#cccccc" />
                                                    </Appearance>
                                                </Legend>
                                                <Series>
                                                    <rad:ChartSeries Name="Series 1">
                                                        <Appearance LegendDisplayMode="ItemLabels" LabelAppearance-RotationAngle="180" TextAppearance-TextProperties-Font="Verdana, 3pt">
                                                            <FillStyle FillType="Solid" MainColor="69, 115, 167">
                                                            </FillStyle>
                                                            <LabelAppearance RotationAngle="-90">
                                                            </LabelAppearance>
                                                            <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                                            </TextAppearance>
                                                            <Border Color="69, 115, 167" />
                                                        </Appearance>
                                                    </rad:ChartSeries>
                                                </Series>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </rad:RadPageView>
                        <rad:RadPageView ID="rpvGeographicalAreas" runat="server" Selected="false">
                            <asp:UpdatePanel ID="upGeographicalAreas" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div style="margin: 0px auto; width: 1000px;">
                                        <!-- Chart BAR Provincias by Scope-->
                                        <div class="divContentReportChartBarStacked1">
                                            <rad:RadChart Width="490px" Height="450px" ID="chartBarStateByScope1" runat="server"
                                                AutoLayout="true" Skin="Office2007" SkinsOverrideStyles="false" AutoTextWrap="false"
                                                OnItemDataBound="chartBarStateByScope1_ItemDataBound" OnDataBound="charts_DataBound">
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
                                                <Legend Visible="true">
                                                    <Appearance>
                                                        <ItemTextAppearance TextProperties-Color="Black">
                                                        </ItemTextAppearance>
                                                        <ItemMarkerAppearance Figure="Square">
                                                        </ItemMarkerAppearance>
                                                        <Border Color="#cccccc" />
                                                    </Appearance>
                                                </Legend>
                                                <Series>
                                                    <rad:ChartSeries Name="Series 1">
                                                        <Appearance LegendDisplayMode="ItemLabels" LabelAppearance-RotationAngle="180" TextAppearance-TextProperties-Font="Verdana, 3pt">
                                                            <FillStyle FillType="Solid" MainColor="69, 115, 167">
                                                            </FillStyle>
                                                            <LabelAppearance RotationAngle="-90">
                                                            </LabelAppearance>
                                                            <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                                            </TextAppearance>
                                                            <Border Color="69, 115, 167" />
                                                        </Appearance>
                                                    </rad:ChartSeries>
                                                </Series>
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
                        </rad:RadPageView>
                        <rad:RadPageView ID="rpvFacilityTypes" runat="server" Selected="false">
                            <asp:UpdatePanel ID="upFacilityTypes" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div style="margin: 0px auto; width: 1000px;">
                                        <!-- Chart BAR FacilityTypes by Scope -->
                                        <div class="divContentReportChartBarStacked1">
                                            <rad:RadChart Width="490px" Height="450px" ID="chartBarFacilityTypeByScope1" runat="server"
                                                AutoLayout="true" Skin="Office2007" SkinsOverrideStyles="false" AutoTextWrap="false"
                                                OnItemDataBound="chartBarFacilityTypeByScope1_ItemDataBound" OnDataBound="charts_DataBound">
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
                                                <Legend Visible="true">
                                                    <Appearance>
                                                        <ItemTextAppearance TextProperties-Color="Black">
                                                        </ItemTextAppearance>
                                                        <ItemMarkerAppearance Figure="Square">
                                                        </ItemMarkerAppearance>
                                                        <Border Color="#cccccc" />
                                                    </Appearance>
                                                </Legend>
                                                <Series>
                                                    <rad:ChartSeries Name="Series 1">
                                                        <Appearance LegendDisplayMode="ItemLabels" LabelAppearance-RotationAngle="180" TextAppearance-TextProperties-Font="Verdana, 3pt">
                                                            <FillStyle FillType="Solid" MainColor="69, 115, 167">
                                                            </FillStyle>
                                                            <LabelAppearance RotationAngle="-90">
                                                            </LabelAppearance>
                                                            <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                                            </TextAppearance>
                                                            <Border Color="69, 115, 167" />
                                                        </Appearance>
                                                    </rad:ChartSeries>
                                                </Series>
                                                <PlotArea>
                                                    <XAxis DataLabelsColumn="FacilityType">
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
                        </rad:RadPageView>
                        <rad:RadPageView ID="rpvFacilities" runat="server" Selected="false">
                            <asp:Panel ID="pnlFacility" runat="server">
                                <!-- Opciones para los chart -->
                                <table id="tblFilterFacility" runat="server" class="ContentFormReport" style="margin-top: 0px;
                                    border-top: solid 0px;">
                                    <colgroup>
                                        <col class="ColTitle" />
                                        <col class="ColContent" />
                                    </colgroup>
                                    <tr class="TRPar" runat="server" id="trOptionFacility">
                                        <td class="ColTitle">
                                            <asp:Label ID="lblFacility" runat="server" Text="Facility:" />
                                        </td>
                                        <td class="ColContent">
                                            <asp:PlaceHolder ID="phFacility" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <div style="margin: 0px auto; width: 1000px;">
                                <div class="divContentReportChartBarStacked">
                                    <!-- Chart PIE Scopes By Facility -->
                                    <asp:UpdatePanel runat="server" ID="upChartPieScopeByFacility" UpdateMode="Always">
                                        <ContentTemplate>
                                            <rad:RadChart ID="chartTotalScopeByFacility" runat="server" Height="300px" Width="980px"
                                                OnItemDataBound="chartTotalScopeByFacility_ItemDataBound" OnDataBound="charts_DataBound"
                                                AutoLayout="false" AutoTextWrap="false" Skin="Office2007" SkinsOverrideStyles="false">
                                                <Appearance FillStyle-FillType="Solid" FillStyle-MainColor="#ffffff">
                                                    <Border Color="Transparent" />
                                                    <FillStyle MainColor="Transparent" SecondColor="Transparent" GammaCorrection="false"
                                                        FillType="Solid">
                                                    </FillStyle>
                                                </Appearance>
                                                <ChartTitle>
                                                    <Appearance Border-Color="Transparent" FillStyle-MainColor="Transparent">
                                                    </Appearance>
                                                    <TextBlock Appearance-TextProperties-Color="#4a5678" Appearance-TextProperties-Font="Arial, 12px">
                                                    </TextBlock>
                                                </ChartTitle>
                                                <Legend>
                                                    <Appearance>
                                                        <ItemTextAppearance Dimensions-Width="300px" Dimensions-Height="300px">
                                                        </ItemTextAppearance>
                                                        <Border Color="#ffffff" />
                                                    </Appearance>
                                                </Legend>
                                                <PlotArea Appearance-Dimensions-Width="460px" Appearance-Dimensions-Height="250px"
                                                    Appearance-Dimensions-AutoSize="false">
                                                    <Appearance>
                                                        <Border Color="Transparent" />
                                                    </Appearance>
                                                </PlotArea>
                                                <Series>
                                                    <rad:ChartSeries Name="Series 1" Type="Pie" DataYColumn="Percentage">
                                                        <Appearance LegendDisplayMode="ItemLabels">
                                                        </Appearance>
                                                    </rad:ChartSeries>
                                                </Series>
                                            </rad:RadChart>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- Chart BAR Activity by Scope And Facility -->
                                <div class="divContentReportChartBarStacked1">
                                    <asp:UpdatePanel runat="server" ID="upChartBarActivityByScope1AndFacility" UpdateMode="Always">
                                        <ContentTemplate>
                                            <rad:RadChart Width="490px" Height="450px" ID="chartBarActivityByScope1AndFacility"
                                                runat="server" AutoLayout="true" Skin="Office2007" SkinsOverrideStyles="false"
                                                AutoTextWrap="false" OnItemDataBound="chartBarActivityByScope1AndFacility_ItemDataBound"
                                                OnDataBound="charts_DataBound">
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
                                                <Legend Visible="true">
                                                    <Appearance>
                                                        <ItemTextAppearance TextProperties-Color="Black">
                                                        </ItemTextAppearance>
                                                        <ItemMarkerAppearance Figure="Square">
                                                        </ItemMarkerAppearance>
                                                        <Border Color="#cccccc" />
                                                    </Appearance>
                                                </Legend>
                                                <Series>
                                                    <rad:ChartSeries Name="Series 1">
                                                        <Appearance LegendDisplayMode="ItemLabels" LabelAppearance-RotationAngle="180" TextAppearance-TextProperties-Font="Verdana, 3pt">
                                                            <FillStyle FillType="Solid" MainColor="69, 115, 167">
                                                            </FillStyle>
                                                            <LabelAppearance RotationAngle="-90">
                                                            </LabelAppearance>
                                                            <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                                            </TextAppearance>
                                                            <Border Color="69, 115, 167" />
                                                        </Appearance>
                                                    </rad:ChartSeries>
                                                </Series>
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </rad:RadPageView>
                    </rad:RadMultiPage>
                </rad:RadPageView>
            </rad:RadMultiPage>
        </asp:Panel>
        
        <div style="clear:both;"></div>
        
        <!--Annual Evolution-->
        <asp:Panel ID="pnlReportEvolution" runat="server">
            <asp:Panel ID="pnlOrganization" runat="server">
             <!-- Opciones para los chart -->
            <table id="tblFilterOrganization" runat="server" class="ContentFormReport">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                    </colgroup>
                    <!-- Accounting Scope -->
                    <tr class="TRPar" runat="server" id="tr2">
                        <td class="ColTitle">
                            <asp:Label ID="lblAccountingScope" runat="server" Text="Accounting Scope:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phAccountingScope" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phAccountingScopeValidator" runat="server" />
                        </td>
                    </tr>
                    <tr class="trImpar" runat="server" id="tr1">
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganization" runat="server" Text="Organization:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phOrganization" runat="server" />
                        </td>
                    </tr>
                </table>
                <br /><br />
            </asp:Panel>
           <!--Bar-->
            <asp:UpdatePanel runat="server" ID="upEvolutionGrid" UpdateMode="Always">
                <ContentTemplate>
                    <asp:PlaceHolder ID="phEvolutionGrid" runat="server">
                    </asp:PlaceHolder>
                     <div style="margin: 0px auto; width: 1000px;">
                        <div class="divContentReportChartBarStacked1">                        
                            <rad:RadChart Width="490px" Height="450px" ID="chartLineEvolution"
                                runat="server" AutoLayout="true" Skin="Office2007" SkinsOverrideStyles="false"
                                AutoTextWrap="false" OnItemDataBound="chartLineEvolution_ItemDataBound"
                                OnDataBound="charts_DataBound">
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
                                <Series>
                                    <rad:ChartSeries Name="Series 1">
                                        <Appearance LegendDisplayMode="ItemLabels" LabelAppearance-RotationAngle="180" TextAppearance-TextProperties-Font="Verdana, 3pt">
                                            <FillStyle FillType="Solid" MainColor="69, 115, 167">
                                            </FillStyle>
                                            <LabelAppearance RotationAngle="-90">
                                            </LabelAppearance>
                                            <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                            </TextAppearance>
                                            <Border Color="69, 115, 167" />
                                        </Appearance>
                                    </rad:ChartSeries>
                                </Series>
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
                        <div class="divContentReportChartBarStacked1">                        
                            <rad:RadChart Width="490px" Height="450px" ID="chartLineEvolutionCO2"
                                runat="server" AutoLayout="true" Skin="Office2007" SkinsOverrideStyles="false"
                                AutoTextWrap="false" OnItemDataBound="chartLineEvolution_ItemDataBound"
                                OnDataBound="charts_DataBound">
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
                                <Series>
                                    <rad:ChartSeries Name="Series 1">
                                        <Appearance LegendDisplayMode="ItemLabels" LabelAppearance-RotationAngle="180" TextAppearance-TextProperties-Font="Verdana, 3pt">
                                            <FillStyle FillType="Solid" MainColor="69, 115, 167">
                                            </FillStyle>
                                            <LabelAppearance RotationAngle="-90">
                                            </LabelAppearance>
                                            <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                            </TextAppearance>
                                            <Border Color="69, 115, 167" />
                                        </Appearance>
                                    </rad:ChartSeries>
                                </Series>
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
                        <div class="divContentReportChartBarStacked1">                        
                            <rad:RadChart Width="490px" Height="450px" ID="chartLineEvolutionCH4"
                                runat="server" AutoLayout="true" Skin="Office2007" SkinsOverrideStyles="false"
                                AutoTextWrap="false" OnItemDataBound="chartLineEvolution_ItemDataBound"
                                OnDataBound="charts_DataBound">
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
                                <Series>
                                    <rad:ChartSeries Name="Series 1">
                                        <Appearance LegendDisplayMode="ItemLabels" LabelAppearance-RotationAngle="180" TextAppearance-TextProperties-Font="Verdana, 3pt">
                                            <FillStyle FillType="Solid" MainColor="69, 115, 167">
                                            </FillStyle>
                                            <LabelAppearance RotationAngle="-90">
                                            </LabelAppearance>
                                            <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                            </TextAppearance>
                                            <Border Color="69, 115, 167" />
                                        </Appearance>
                                    </rad:ChartSeries>
                                </Series>
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
                        <div class="divContentReportChartBarStacked1">                        
                            <rad:RadChart Width="490px" Height="450px" ID="chartLineEvolutionN2O"
                                runat="server" AutoLayout="true" Skin="Office2007" SkinsOverrideStyles="false"
                                AutoTextWrap="false" OnItemDataBound="chartLineEvolution_ItemDataBound"
                                OnDataBound="charts_DataBound">
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
                                <Series>
                                    <rad:ChartSeries Name="Series 1">
                                        <Appearance LegendDisplayMode="ItemLabels" LabelAppearance-RotationAngle="180" TextAppearance-TextProperties-Font="Verdana, 3pt">
                                            <FillStyle FillType="Solid" MainColor="69, 115, 167">
                                            </FillStyle>
                                            <LabelAppearance RotationAngle="-90">
                                            </LabelAppearance>
                                            <TextAppearance TextProperties-Color="Black" TextProperties-Font="Verdana, 10px">
                                            </TextAppearance>
                                            <Border Color="69, 115, 167" />
                                        </Appearance>
                                    </rad:ChartSeries>
                                </Series>
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
        
        </asp:Panel>
    </div>
</asp:Content>
