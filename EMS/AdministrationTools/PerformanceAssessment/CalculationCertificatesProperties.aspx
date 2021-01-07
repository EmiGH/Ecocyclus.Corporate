<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="CalculationCertificatesProperties.aspx.cs" Inherits="Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment.CalculationCertificatesProperties"
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
                    <!-- Id. Certificated -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIdCertificated" runat="server" Text="Id. Certificated:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblIdCertificatedValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- From -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblCertificatedFromDate" runat="server" Text="From:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDatePicker ID="rdtCertificatedFromDate" Skin="EMS" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput CssClass="contentformInput" DateFormat="MM/dd/yyyy" />
                            </rad:RadDatePicker>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="Required Field"
                                ControlToValidate="rdtCertificatedFromDate" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cv2" runat="server" ErrorMessage="Data Error" ControlToValidate="rdtCertificatedFromDate"
                                Operator="DataTypeCheck" SkinID="EMS" Display="Dynamic" Type="Date"></asp:CompareValidator>
                        </td>
                    </tr>
                    <!-- To -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblCertificatedToDate" runat="server" Text="To:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDatePicker ID="rdtCertificatedToDate" Skin="EMS" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput CssClass="contentformInput" DateFormat="MM/dd/yyyy" />
                            </rad:RadDatePicker>
                        </td>
                        <td class="ColValidator">
                            <asp:CustomValidator ID="cvCertificatedEndDate" runat="server" ControlToValidate="rdtCertificatedToDate"
                                SkinID="EMS" ErrorMessage="The second date must be after the first one" Display="Dynamic"></asp:CustomValidator>
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="Required Field"
                                ControlToValidate="rdtCertificatedToDate" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cv1" runat="server" ErrorMessage="Data Error" ControlToValidate="rdtCertificatedToDate"
                                Operator="DataTypeCheck" SkinID="EMS" Display="Dynamic" Type="Date"></asp:CompareValidator>
                        </td>
                    </tr>
                    <!-- Value Certificated -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblValue" runat="server" Text="Value Certificated:" />
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
                    <!-- DOE -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganizationDOE" runat="server" Text="DOE:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phOrganization" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phOrganizationValidator" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
