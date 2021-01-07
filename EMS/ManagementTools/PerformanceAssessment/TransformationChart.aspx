<%@ Page Language="C#" MasterPageFile="~/EMSPopUp.Master" AutoEventWireup="true"
    CodeBehind="TransformationChart.aspx.cs" Inherits="Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment.TransformationChart"
    Title="Transformation Chart" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="ContentPopUp" runat="server">
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
                    <asp:Label ID="lblBase" runat="server" Text="Base:" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblBaseValue" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="ColTitle">
                    <asp:Label ID="lblTransformationLabel" runat="server" Text="Transformation:" />
                </td>
                <td class="ColContent">
                    <asp:Label ID="lblTransformationValue" runat="server" />
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
            <table border="0" cellpadding="0" cellspacing="0" class="Filter">
                <tr>
                    <td>
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
