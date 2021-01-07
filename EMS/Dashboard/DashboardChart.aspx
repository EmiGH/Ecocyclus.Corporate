<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="DashboardChart.aspx.cs"
    Inherits="Condesus.EMS.WebUI.Dashboard.DashboardChart" Title="Dashboard" %>

<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentMain">
    <style type="text/css">
        .table
        {
            width: 100%;
            height: 300px;
            margin-bottom: 20px;
        }
        .table .header
        {
            background-color: #66789b;
            font-weight: 700;
            color: #fff;
            text-align: center;
        }
        .table .header .title
        {
            text-align: left;
            padding-left: 20px;
        }
        .table .subheader
        {
            background-color: #98a6c0;
            font-weight: 700;
            color: #fff;
            text-align: center;
        }
        .table .trTitle td, .table .trInfo td
        {
            border-bottom: 1px solid #ccc;
        }
        .table .trTitle
        {
            background-color: #eee;
            color: #333;
            text-align: center;
        }
        .table .trTitle .title
        {
            text-align: left;
            font-weight: 700;
            padding-left: 5px;
        }
        .table .trInfo
        {
            background-color: #fff;
            color: #666;
            text-align: center;
        }
        .table .trInfo .title
        {
            text-align: left;
            font-weight: 700;
            padding-left: 30px;
        }
        .table .trFooter
        {
            background-color: #b4c3df;
            color: #000;
            text-align: center;
        }
    </style>
    <table class="table" cellpadding="0" cellspacing="0">
        <tr class="header">
            <td colspan="2" class="title">
                Process
            </td>
            <td>
                Current Period
            </td>
            <td colspan="5">
                Performance
            </td>
            <td colspan="2">
                Exceptions
            </td>
        </tr>
        <tr class="subheader">
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                Calculated
            </td>
            <td>
                %
            </td>
            <td>
                Estimated
            </td>
            <td>
                Certified
            </td>
            <td>
                %
            </td>
            <td>
                Open
            </td>
            <td>
                Total
            </td>
        </tr>
        <tr class="trTitle">
            <td class="title">
                Renewable
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                46.379,00
            </td>
            <td>
                25,90%
            </td>
            <td>
                210.600,00
            </td>
            <td>
                40.000,00
            </td>
            <td>
                23,38%
            </td>
            <td>
                17
            </td>
            <td>
                30
            </td>
        </tr>
        <tr class="trInfo">
            <td class="title">
                Mini Hydro A
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                01/01/2009
            </td>
            <td>
                15879
            </td>
            <td>
                11,82%
            </td>
            <td>
                134.300,00
            </td>
            <td>
                10.000,00
            </td>
            <td>
                7,45%
            </td>
            <td>
                12
            </td>
            <td>
                20
            </td>
        </tr>
        <tr class="trInfo">
            <td class="title">
                Mini Hydro
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                01/01/2009
            </td>
            <td>
                30.500,00
            </td>
            <td>
                39,97%
            </td>
            <td>
                76.300,00
            </td>
            <td>
                30.000,00
            </td>
            <td>
                39,32%
            </td>
            <td>
                5
            </td>
            <td>
                10
            </td>
        </tr>
        <tr class="trTitle">
            <td class="title">
                Chemical
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                124.000,00
            </td>
            <td>
                27,19%
            </td>
            <td>
                456.000,00
            </td>
            <td>
                75.000,00
            </td>
            <td>
                16,45%
            </td>
            <td>
                8
            </td>
            <td>
                5
            </td>
        </tr>
        <tr class="trInfo">
            <td class="title">
                Nitric Acid
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                03/03/2009
            </td>
            <td>
                124.000,00
            </td>
            <td>
                27,19%
            </td>
            <td>
                456.000,00
            </td>
            <td>
                75.000,00
            </td>
            <td>
                16,45%
            </td>
            <td>
                8
            </td>
            <td>
                5
            </td>
        </tr>
        <tr class="trTitle">
            <td class="title">
                Efficiency
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                51.316,00
            </td>
            <td>
                34,35%
            </td>
            <td>
                153.400,00
            </td>
            <td>
                32.840,00
            </td>
            <td>
                21,15%
            </td>
            <td>
                73
            </td>
            <td>
                153
            </td>
        </tr>
        <tr class="trInfo">
            <td class="title">
                Fuel
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                01/01/2010
            </td>
            <td>
                21.768,00
            </td>
            <td>
                39,29%
            </td>
            <td>
                55.400,00
            </td>
            <td>
                14.000,00
            </td>
            <td>
                25,27%
            </td>
            <td>
                22
            </td>
            <td>
                50
            </td>
        </tr>
        <tr class="trInfo">
            <td class="title">
                Fuel
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                01/01/2009
            </td>
            <td>
                16.780,00
            </td>
            <td>
                26,22%
            </td>
            <td>
                64.000,00
            </td>
            <td>
                12.500,00
            </td>
            <td>
                19,53%
            </td>
            <td>
                25
            </td>
            <td>
                60
            </td>
        </tr>
        <tr class="trInfo">
            <td class="title">
                Fuel
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                01/01/2009
            </td>
            <td>
                16.780,00
            </td>
            <td>
                37,55%
            </td>
            <td>
                34.000,00
            </td>
            <td>
                6.340,00
            </td>
            <td>
                18,65%
            </td>
            <td>
                26
            </td>
            <td>
                43
            </td>
        </tr>
        <tr class="trFooter">
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                221.695,00
            </td>
            <td>
                29,15%
            </td>
            <td>
                820.000,00
            </td>
            <td>
                147.840,00
            </td>
            <td>
                20,33%
            </td>
            <td>
                98
            </td>
            <td>
                188
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" style="width: 100%; text-align: center; margin-bottom: 20px;">
        <tr>
            <td style="width: 50%;">
                <DCWC:Chart ID="Chart1" runat="server" ImageType="Png" BackColor="#D3DFF0" Width="500px"
                    Height="400px" BorderLineColor="26, 59, 105" Palette="Dundas" BorderLineStyle="Solid"
                    BackGradientType="TopBottom" BorderLineWidth="2">
                    <Legends>
                        <DCWC:Legend LegendStyle="Row" AutoFitText="False" Docking="Bottom" Name="Default"
                            BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Far">
                        </DCWC:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                    <ChartAreas>
                        <DCWC:ChartArea Name="Chart Area 1" BorderColor="64, 64, 64, 64" BorderStyle="Solid"
                            BackGradientEndColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                            BackGradientType="TopBottom">
                            <AxisY2 Enabled="False">
                            </AxisY2>
                            <AxisY2 Enabled="False">
                            </AxisY2>
                            <Area3DStyle YAngle="2" Perspective="10" XAngle="20" RightAngleAxes="False" WallWidth="0"
                                Clustered="True"></Area3DStyle>
                            <AxisY LineColor="64, 64, 64, 64" LabelsAutoFit="False">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                            </AxisY>
                            <AxisX LineColor="64, 64, 64, 64" LabelsAutoFit="False">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                            </AxisX>
                        </DCWC:ChartArea>
                    </ChartAreas>
                </DCWC:Chart>
            </td>
            <td style="width: 50%;">
                <DCWC:Chart ID="Chart2" runat="server" ImageType="Png" BackColor="#D3DFF0" Width="500px"
                    Height="400px" BorderLineColor="26, 59, 105" Palette="Dundas" BorderLineStyle="Solid"
                    BackGradientType="TopBottom" BorderLineWidth="2">
                    <Legends>
                        <DCWC:Legend LegendStyle="Row" AutoFitText="False" Docking="Bottom" Name="Default"
                            BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Far">
                        </DCWC:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                    <ChartAreas>
                        <DCWC:ChartArea Name="Chart Area 1" BorderColor="64, 64, 64, 64" BorderStyle="Solid"
                            BackGradientEndColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                            BackGradientType="TopBottom">
                            <AxisY2 Enabled="False">
                            </AxisY2>
                            <AxisY2 Enabled="False">
                            </AxisY2>
                            <Area3DStyle YAngle="2" Perspective="10" XAngle="20" RightAngleAxes="False" WallWidth="0"
                                Clustered="True"></Area3DStyle>
                            <AxisY LineColor="64, 64, 64, 64" LabelsAutoFit="False">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                            </AxisY>
                            <AxisX LineColor="64, 64, 64, 64" LabelsAutoFit="False">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                            </AxisX>
                        </DCWC:ChartArea>
                    </ChartAreas>
                </DCWC:Chart>
            </td>
        </tr>
        <tr>
            <td style="width: 50%;">
                <DCWC:Chart ID="Chart3" runat="server" ImageType="Png" BackColor="#D3DFF0" Width="500px"
                    Height="400px" BorderLineColor="26, 59, 105" Palette="Dundas" BorderLineStyle="Solid"
                    BackGradientType="TopBottom" BorderLineWidth="2">
                    <Titles>
                        <DCWC:Title Text="Calculation By Project Types (tns. CO2)">
                        </DCWC:Title>
                    </Titles>
                    <Legends>
                        <DCWC:Legend LegendStyle="Row" AutoFitText="False" Docking="Bottom" Name="Default"
                            BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Far">
                        </DCWC:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                    <ChartAreas>
                        <DCWC:ChartArea Name="Chart Area 1" BorderColor="64, 64, 64, 64" BorderStyle="Solid"
                            BackGradientEndColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                            BackGradientType="TopBottom" Area3DStyle-Enable3D="true">
                            <AxisY2 Enabled="False">
                            </AxisY2>
                            <AxisY2 Enabled="False">
                            </AxisY2>
                            <Area3DStyle YAngle="2" Perspective="10" XAngle="20" RightAngleAxes="False" WallWidth="0"
                                Clustered="True"></Area3DStyle>
                            <AxisY LineColor="64, 64, 64, 64" LabelsAutoFit="False">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                            </AxisY>
                            <AxisX LineColor="64, 64, 64, 64" LabelsAutoFit="False">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                            </AxisX>
                        </DCWC:ChartArea>
                    </ChartAreas>
                </DCWC:Chart>
            </td>
            <td style="width: 50%;">
                <DCWC:Chart ID="Chart4" runat="server" ImageType="Png" BackColor="#D3DFF0" Width="500px"
                    Height="400px" BorderLineColor="26, 59, 105" Palette="Dundas" BorderLineStyle="Solid"
                    BackGradientType="TopBottom" BorderLineWidth="2">
                    <Legends>
                        <DCWC:Legend LegendStyle="Row" AutoFitText="False" Docking="Bottom" Name="Default"
                            BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Far">
                        </DCWC:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss"></BorderSkin>
                    <ChartAreas>
                        <DCWC:ChartArea Name="Chart Area 1" BorderColor="64, 64, 64, 64" BorderStyle="Solid"
                            BackGradientEndColor="Transparent" BackColor="64, 165, 191, 228" ShadowColor="Transparent"
                            BackGradientType="TopBottom" Area3DStyle-Enable3D="true">
                            <AxisY2 Enabled="False">
                            </AxisY2>
                            <AxisY2 Enabled="False">
                            </AxisY2>
                            <Area3DStyle YAngle="2" Perspective="10" XAngle="20" RightAngleAxes="False" WallWidth="0"
                                Clustered="True"></Area3DStyle>
                            <AxisY LineColor="64, 64, 64, 64" LabelsAutoFit="False">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                            </AxisY>
                            <AxisX LineColor="64, 64, 64, 64" LabelsAutoFit="False">
                                <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                            </AxisX>
                        </DCWC:ChartArea>
                    </ChartAreas>
                </DCWC:Chart>
            </td>
        </tr>
    </table>
</asp:Content>
