<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="ChartTest.aspx.cs" Inherits="Condesus.EMS.WebUI.ChartTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentMain" runat="server">

<div id="MainPlaceHolder">
            <div id="ChartArea">
                <div id="chartOptionsPlaceholder">
                    <br />
                    <asp:Label ID="lblChartOrientation" runat="server" Text="Series orientation:" />
                    <asp:RadioButtonList AutoPostBack="true" ID="OrientationList" runat="server" OnSelectedIndexChanged="OrientationList_SelectedIndexChanged">
                        <asp:ListItem Text="Horizontal" Value="Horizontal" />
                        <asp:ListItem Text="Vertical" Value="Vertical" Selected="True" />
                    </asp:RadioButtonList>
                    <br />
                    <asp:Label ID="lblChartType" runat="server" Text="Additional chart types:" />
                    <asp:DropDownList AutoPostBack="true" ID="SubtypeDropdown" runat="server" OnSelectedIndexChanged="SubtypeDropdown_SelectedIndexChanged">
                        <asp:ListItem Text="Normal Line" Value="Line" Selected="True" />
                        <asp:ListItem Text="Bezier Line" Value="Bezier" />
                        <asp:ListItem Text="Spline Line" Value="Spline" />
                        <asp:ListItem Text="Stacked Line" Value="StackedLine" />
                        <asp:ListItem Text="Stacked Spline Line" Value="StackedSpline" />
                        <asp:ListItem Text="Point" Value="Point" />
                    </asp:DropDownList>
                </div>
                <div id="chartPlaceholder">
                    <asp:UpdatePanel ID="upChart" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <rad:RadChart ID="RadChart1" SkinsOverrideStyles="true" runat="server">
                            <PlotArea>
                                <XAxis MaxValue="5" MinValue="1" Step="1">
                                </XAxis>
                                <YAxis MaxValue="3" Step="0.5" AxisMode="Extended">
                                </YAxis>
                                <YAxis2 MaxValue="5" MinValue="1" Step="1">
                                </YAxis2>
                            </PlotArea>
                            <Series>
                                <rad:ChartSeries Name="Series 1" Type="Line">
                                    <Items>
                                        <rad:ChartSeriesItem YValue="3" Name="Item 20">
                                        </rad:ChartSeriesItem>
                                    </Items>
                                </rad:ChartSeries>
                                <rad:ChartSeries Name="Series 2" Type="Line">
                                    <Items>
                                        <rad:ChartSeriesItem YValue="1" Name="Item 10">
                                        </rad:ChartSeriesItem>
                                    </Items>
                                </rad:ChartSeries>
                            </Series>
                        </rad:RadChart>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
</asp:Content>
