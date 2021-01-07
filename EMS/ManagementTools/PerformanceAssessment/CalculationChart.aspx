<%@ Page Language="C#" MasterPageFile="~/EMSPopUp.Master" AutoEventWireup="true"
    CodeBehind="CalculationChart.aspx.cs" Inherits="Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment.CalculationChart"
    Title="Calculation Chart" Culture="auto" UICulture="auto" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>
<%@ Register Assembly="DundasWebGauge" Namespace="Dundas.Gauges.WebControl" TagPrefix="DGWC" %>
<asp:Content ID="cntContent" ContentPlaceHolderID="ContentPopUp" runat="server">
    <div class="content">
        <div id="contentdivContainerTitle">
            <div id="contentdivContainerTitleIcon">
            </div>
            <div id="contentdivContainerTitleTitle">
                <asp:Label ID="lblTitle" CssClass="contentdivSubContainerTitleTitle" runat="server"
                    Text="Calculation" />
                <asp:Label ID="lblSubTitle" CssClass="contentdivContainerSubTitle" runat="server"
                    Text="[Chart]" Height="13px" />
            </div>
            <asp:Label CssClass="contentdivContainerDetail" ID="lblDetail" runat="server" Text="Calculation Chart Details" />
        </div>
        <div class="contentmenu">
            <div id="contentmenudivBackground">
                <div id="contentmenudivPosition">
                </div>
            </div>
        </div>
        <div class="contentform">
            <div class="contentformdivImpar">
                <asp:Label ID="lblCalculationNameLabel" CssClass="contentformspanTitle" runat="server"
                    Text="Calculation:" />
                <asp:Label ID="lblCalculationNameValue" runat="server" CssClass="contentformspanText" />
            </div>
            <div class="contentformdivImpar">
                <asp:Label ID="lblCalculationDescriptionLabel" CssClass="contentformspanTitle" runat="server"
                    Text="Description:" />
                <asp:Label ID="lblCalculationDescriptionValue" runat="server" CssClass="contentformspanText" />
            </div>
            <div class="contentformdivPar">
                <asp:Label ID="lblCalculationUnitLabel" CssClass="contentformspanTitle" runat="server"
                    Text="Unit:" />
                <asp:Label ID="lblCalculationUnitValue" runat="server" CssClass="contentformspanText" />
            </div>
            <div class="contentformdivImpar">
                <asp:Label ID="lblCalculationFrequencyLabel" CssClass="contentformspanTitle" runat="server"
                    Text="Frequency:" />
                <asp:Label ID="lblCalculationFrequencyValue" runat="server" CssClass="contentformspanText"
                    Text="Every 15 minutes" />
            </div>
            <div class="contentformdivImpar">
                <asp:Label ID="lblCalculationFormulaLabel" CssClass="contentformspanTitle" runat="server"
                    Text="Formula:" />
                <asp:Label ID="lblCalculationFormulaValue" runat="server" CssClass="contentformspanText" />
            </div>
            <div class="contentformdivImpar">
                <asp:Label ID="lblCalculationLastDate" CssClass="contentformspanTitle" runat="server"
                    Text="Last Date:" />
                <asp:Label ID="lblCalculationLastDateValue" runat="server" CssClass="contentformspanText" />
            </div>
            <div class="contentformdivImpar">
                <asp:Label ID="lblCalculationLastValue" CssClass="contentformspanTitle" runat="server"
                    Text="Last Value:" />
                <asp:Label ID="lblCalculationLastValueValue" runat="server" CssClass="contentformspanText" />
            </div>
        </div>
        <br />
        <div class="contentform" style="text-align: center; margin-top: 25px;">
            <div style="background-color: #eff6fc; width: 600px; height: 30px; padding: 10px 15px 10px 15px;
                border: solid 1px #93b7d0;">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                    <tr style="text-align: left; vertical-align: top;">
                        <td style="text-align: right; vertical-align: middle;">
                            <asp:Label CssClass="contentformspanTitleNoPadding" ID="lblStartDate" runat="server"
                                Text="Start Date" />
                        </td>
                        <td>
                            <rad:RadDatePicker CssClass="Calendar" MinDate="1900-01-01" MaxDate="2099-01-01"
                                ID="radcalStartDate" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput Width="40px" ID="DateInput1" DateFormat="dd/MM/yyyy" runat="server">
                                </DateInput>
                                <Calendar ID="Calendar1" runat="server">
                                </Calendar>
                            </rad:RadDatePicker>
                        </td>
                        <td style="text-align: right; vertical-align: middle;">
                            <asp:Label CssClass="contentformspanTitleNoPadding" ID="lblEndDate" runat="server"
                                Text="End Date" />
                        </td>
                        <td>
                            <rad:RadDatePicker CssClass="Calendar" MinDate="1900-01-01" MaxDate="2099-01-01"
                                ID="radcalEndDate" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput Width="40px" ID="DateInput2" DateFormat="dd/MM/yyyy" runat="server">
                                </DateInput>
                                <Calendar ID="Calendar2" runat="server">
                                </Calendar>
                            </rad:RadDatePicker>
                        </td>
                        <td style="padding: 10px 3px 0 3px;">
                            <asp:ImageButton ID="imgBtnRefreshChart" runat="server" Height="16px" Width="16px"
                                ImageUrl="../../Skins/Images/Trans.gif" CssClass="ImgBtnRefreshChart" AlternateText="Refresh Chart" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <br />
        <br />
        <div style="text-align: center;">
            <DGWC:GaugeContainer ID="dndGauge" runat="server" Height="200px" Width="500px">
                <LinearGauges>
                    <DGWC:LinearGauge Name="Calculation" Orientation="Horizontal">
                    </DGWC:LinearGauge>
                </LinearGauges>
            </DGWC:GaugeContainer>
        </div>
</asp:Content>
