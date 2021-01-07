<%@ Page Language="C#" MasterPageFile="~/EMSPopUpReport.Master" AutoEventWireup="true"
    CodeBehind="MeasurementChart.aspx.cs" Inherits="Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment.MeasurementChart"
    Title="Measurement Chart" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="ContentPopUp" runat="server">

    <script type="text/javascript">
        function ExportMeasurementSeries(e, idMeasurement) {
            var _IEXPLORER = 'Microsoft Internet Explorer';
            var _FIREFOX = 'Netscape';
            var _BrowserName = navigator.appName;
//            StopEvent(e);

            if (_BrowserName == _IEXPLORER) {   //IE and Opera
                var newWindow = window.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + "/Managers/ExportMeasurementSeries.aspx?IdMeasurement=" + idMeasurement, 'Export', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }
            else {   //FireFox
                var newWindow = window.parent.open("http://" + '<%=Request.ServerVariables["HTTP_HOST"]%>' + "/Managers/ExportMeasurementSeries.aspx?IdMeasurement=" + idMeasurement, 'Export', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');
            }

            newWindow.focus();
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            StopEvent(e);
        }
    </script>

    <div class="ConteinerReport">
        <asp:Label ID="lblTitle" CssClass="Title" runat="server" Text="Measurement Chart" />
        <asp:Label CssClass="SubTitle" ID="lblDetail" runat="server" Text="Chart information of selected indicator." />
        <table id="tblContentForm2" runat="server" class="ContentFormReport">
            <colgroup>
                <col class="ColTitle" />
                <col class="ColContent" />
            </colgroup>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblProject" runat="server" Text="Project:" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblProjectValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblMeasurementLabel" runat="server" Text="Measurement:" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblMeasurementValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblIndicatorLabel" runat="server" Text="Indicator:" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblIndicatorValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblDeviceLabel" runat="server" Text="Device:" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblDeviceValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblMeasurementUnit" runat="server" Text="Measurement Unit:" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblMeasurementUnitValue" runat="server" />
                </td>
            </tr>
        </table>
        <div class="Chart">
            <div style="margin: auto; width: 1000px;">
                <table border="0" cellpadding="0" cellspacing="0" class="Filter">
                    <tr>
                        <td>
                            <div style="margin: auto; width: 900px;">
                                <table border="0" cellpadding="0" cellspacing="0" class="FilterComand">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblStartDate" runat="server" Text="" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEndDate" runat="server" Text="" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblGrouping" runat="server" Text="" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAggregate" runat="server" Text="" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblParameterGroup" runat="server" Text="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <rad:RadDatePicker MinDate="1900-01-01" MaxDate="2099-01-01" ID="radcalStartDate"
                                                runat="server">
                                                <DatePopupButton runat="server" ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                                <DateInput Width="40px" ID="DateInput1" DateFormat="dd/MM/yyyy" runat="server">
                                                </DateInput>
                                                <Calendar ID="Calendar1" runat="server">
                                                </Calendar>
                                            </rad:RadDatePicker>
                                        </td>
                                        <td>
                                            <rad:RadDatePicker MinDate="1900-01-01" MaxDate="2099-01-01" ID="radcalEndDate" runat="server">
                                                <DatePopupButton runat="server" ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                                <DateInput Width="40px" ID="DateInput2" DateFormat="dd/MM/yyyy" runat="server">
                                                </DateInput>
                                                <Calendar ID="Calendar2" runat="server">
                                                </Calendar>
                                            </rad:RadDatePicker>
                                        </td>
                                        <td>
                                            <rad:RadComboBox Width="160px" ID="radddlGrouping" runat="server">
                                            </rad:RadComboBox>
                                        </td>
                                        <td>
                                            <rad:RadComboBox Width="160px" ID="radddlAggregate" runat="server">
                                            </rad:RadComboBox>
                                        </td>
                                        <td>
                                            <asp:PlaceHolder ID="phParameterGroup" runat="server"></asp:PlaceHolder>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgBtnRefreshChart" runat="server" Height="16px" Width="16px"
                                                ImageUrl="../../Skins/Images/Trans.gif" CssClass="ImgBtnRefreshChart" AlternateText="" />
                                            <asp:ImageButton ID="imgBtnResetChart" runat="server" Height="16px" Width="16px"
                                                ImageUrl="../../Skins/Images/Trans.gif" CssClass="ImgBtnResetChart" AlternateText="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div class="CheckBox">
                                                <asp:Label CssClass="contentformspanTitleCheckBox" ID="lblShowMaxMin" runat="server"
                                                    Text="" />
                                                <asp:CheckBox ID="chkShowMaxMin" runat="server" />
                                                <asp:Label ID="lblShowFirstLast" runat="server" Text="" />
                                                <asp:CheckBox ID="chkShowFirstLast" runat="server" />
                                                <asp:Label ID="lblShowBands" runat="server" Text="" />
                                                <asp:CheckBox ID="chkShowBands" runat="server" />
                                                <asp:Label ID="lblShowAverage" runat="server" Text="" />
                                                <asp:CheckBox ID="chkShowAverage" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <asp:UpdatePanel ID="upToolbar" runat="server">
                                <ContentTemplate>
                                    <DCWC:ChartToolbar ID="chtIndicatorToolbar" runat="server" Width="301px" CssClass="Test"
                                        Height="44px" Visible="false" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <DCWC:Chart ID="chtIndicator" runat="server" BackColor="#ffffff" BackGradientEndColor="226, 239, 247"
                                BorderLineColor="26, 59, 105" BorderLineStyle="Solid" BorderLineWidth="0" Palette="None"
                                Width="1000px" Height="500px" ImageType="Jpeg" OnCommandFired="chtIndicator_CommandFired">
                                <Legends>
                                    <DCWC:Legend Enabled="true" DockInsideChartArea="true" DockToChartArea="NotSet" LegendStyle="Table"
                                        BackColor="White" BorderColor="26, 59, 105" Font="Verdana, 7" Name="Default"
                                        ShadowOffset="0">
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
                                    <DCWC:Title Color="60, 60, 60" Font="Verdana, 7" Name="Title1">
                                    </DCWC:Title>
                                </Titles>
                                <Series>
                                    <DCWC:Series Name="Data" ChartArea="AreaData" MarkerStyle="Circle" MarkerSize="5">
                                        <SmartLabels Enabled="True" />
                                    </DCWC:Series>
                                    <DCWC:Series Name="Average" ChartArea="AreaData">
                                    </DCWC:Series>
                                </Series>
                                <ChartAreas>
                                    <DCWC:ChartArea BackColor="White" BorderColor="26, 59, 105" BorderStyle="Solid" Name="AreaData"
                                        BackGradientEndColor="213, 241, 255" BackGradientType="DiagonalRight">
                                        <AxisY LabelsAutoFit="False">
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
                            <!-- Statistics -->
                            <div style="width: 100%;">
                                <div style="text-align: center; margin-bottom: 20px; width: 500px; margin: auto;">
                                    <table border="0" cellpadding="0" cellspacing="0" class="infoChart" style="width:100%;">
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblPeriod" runat="server" Text="Period" />
                                                <asp:Label ID="lblFirstDate" runat="server" />
                                                <asp:Label ID="lblSeparator" Text=" - " runat="server" />
                                                <asp:Label ID="lblLastDate" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%">
                                                <asp:Label ID="lblFIRST" runat="server" Text="" />
                                                <asp:Label ID="lblFirstVal" runat="server" />
                                            </td>
                                            <td style="width: 33%">
                                                <asp:Label ID="lblAVG" runat="server" Text="" />
                                                <asp:Label ID="lblAvgVal" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSTDDEV" runat="server" Text="" />
                                                <asp:Label ID="lblStdDevVal" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%">
                                                <asp:Label ID="lblLAST" runat="server" Text="" />
                                                <asp:Label ID="lblLastVal" runat="server" />
                                            </td>
                                            <td style="width: 33%">
                                                <asp:Label ID="lblSUM" runat="server" Text="" />
                                                <asp:Label ID="lblSumVal" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSTDDEVP" runat="server" Text="" />
                                                <asp:Label ID="lblStdDevPVal" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%">
                                                <asp:Label ID="lblMAX" runat="server" Text="" />
                                                <asp:Label ID="lblMaxVal" runat="server" />
                                            </td>
                                            <td style="width: 33%">
                                                <asp:Label ID="lblCOUNT" runat="server" Text="" />
                                                <asp:Label ID="lblCountVal" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblVAR" runat="server" Text="" />
                                                <asp:Label ID="lblVarVal" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 33%">
                                                <asp:Label ID="lblMIN" runat="server" Text="" />
                                                <asp:Label ID="lblMinVal" runat="server" />
                                            </td>
                                            <td style="width: 33%">
                                                &nbsp;
                                            </td>
                                            <td>
                                                <div>
                                                    <asp:Label ID="lblVARP" runat="server" Text="" />
                                                    <asp:Label ID="lblVarPVal" runat="server" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:UpdatePanel ID="upGridSeries" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <br />
                <br />
                <asp:Label ID="lblEntitySeries" CssClass="Title" runat="server" Style="padding: 0px 10px;" />
                <asp:LinkButton ID="lnkExport" runat="server" CssClass="lnkExport" Style="float: right;
                    padding-right: 15px;" Text="Export"></asp:LinkButton>
                <div style="padding: 20px 10px;">
                    <asp:PlaceHolder runat="server" ID="pchControls"></asp:PlaceHolder>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
