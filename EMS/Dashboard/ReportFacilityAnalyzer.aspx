<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="ReportFacilityAnalyzer.aspx.cs" Inherits="Condesus.EMS.WebUI.ReportFacilityAnalyzer" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
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
                    <!-- Sites (Facility o Sector) -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblSite" runat="server" Text="Sites (Facility or Sector)" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phSite" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:UpdatePanel runat="server" ID="upGridIndicators" UpdateMode="Always">
        <ContentTemplate>
            <div style="padding: 10px 10px 0 10px;">
                <asp:PlaceHolder ID="phGridIndicators" runat="server"></asp:PlaceHolder>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    

    <br />
    <br />
    <asp:UpdatePanel runat="server" ID="upEvolutionGrid" UpdateMode="Always">
        <ContentTemplate>
            <div style="margin: 0px auto; width: 1000px;">
                <div class="divContentReportChartBarStacked1">
                    <rad:RadChart Width="1000px" Height="650px" ID="chartFacilityAnalyzer" runat="server"
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
                            <asp:UpdatePanel ID="upds" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phGridDataSeries" runat="server" ></asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
