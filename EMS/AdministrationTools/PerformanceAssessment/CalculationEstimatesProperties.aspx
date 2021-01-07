<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="CalculationEstimatesProperties.aspx.cs" Inherits="Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment.CalculationEstimatesProperties"
    Culture="auto" UICulture="auto" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <table id="tblContentForm" runat="server" class="ContentForm">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                        <col class="ColValidator" />
                    </colgroup>
                    <!-- Calculation -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblCalculation" runat="server" Text="Calculation:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblCalculationValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Id. Estimated -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIdEstimated" runat="server" Text="Id. Estimated:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblIdEstimatedValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Classification -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblClassifications" runat="server" Text="Process Classifications" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phProcessClassification" runat="server"></asp:PlaceHolder>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phProcessClassificationValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Calculation Scenario Type -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblScenario" runat="server" Text="Scenario Type:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upCalculationScenarioType" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phCalculationScenarioType" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phCalculationScenarioTypeValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- From -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblEstimatedFromDate" runat="server" Text="From:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDatePicker ID="rdtEstimatedFromDate" Skin="EMS" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput ID="DateInput1" runat="server" DateFormat="MM/dd/yyyy" />
                                <Calendar ID="Calendar1" runat="server">
                                </Calendar>
                            </rad:RadDatePicker>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="Required Field"
                                ControlToValidate="rdtEstimatedFromDate" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cv2" runat="server" ErrorMessage="Data Error" ControlToValidate="rdtEstimatedFromDate"
                                Operator="DataTypeCheck" SkinID="EMS" Display="Dynamic" Type="Date"></asp:CompareValidator>
                        </td>
                    </tr>
                    <!-- To -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblEstimatedToDate" runat="server" Text="To:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDatePicker ID="rdtEstimatedToDate" Skin="EMS" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput CssClass="contentformInput" DateFormat="MM/dd/yyyy" />
                            </rad:RadDatePicker>
                        </td>
                        <td class="ColValidator">
                            <asp:CustomValidator ID="cvEstimatedEndDate" runat="server" ControlToValidate="rdtEstimatedToDate"
                                SkinID="EMS" Display="Dynamic" ErrorMessage="The second date must be after the first one"></asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Required Field"
                                ControlToValidate="rdtEstimatedToDate" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cv1" runat="server" ErrorMessage="Data Error" ControlToValidate="rdtEstimatedToDate"
                                Operator="DataTypeCheck" SkinID="EMS" Display="Dynamic" Type="Date"></asp:CompareValidator>
                        </td>
                    </tr>
                    <!-- Value Estimated -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblValue" runat="server" Text="Value Estimated:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtValue" SkinID="EMS" runat="server" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator Display="Dynamic" SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtValue"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="rvValue" SkinID="EMS" runat="server" Type="Double" Display="Dynamic"
                                ControlToValidate="txtValue" MinimumValue="-999999999999999999.9999" MaximumValue="999999999999999999.9999"
                                ErrorMessage="Wrong Format"></asp:RangeValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
