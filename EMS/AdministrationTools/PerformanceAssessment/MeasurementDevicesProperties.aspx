<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="MeasurementDevicesProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PA.MeasurementDevicesProperties" Title="EMS - Measurement Device Property"
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
                    <!-- Measurement Device Type -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementDeviceType" runat="server" Text="Measurement Device Type:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phMeasurementDeviceType" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phMeasurementDeviceTypeValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Brand -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblBrand" runat="server" Text="Brand:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtBrand" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtBrand" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Model -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblModel" runat="server" Text="Model:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtModel" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ID="rfv2" ControlToValidate="txtModel" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Serial Number -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblSerialNumber" runat="server" Text="Serial Number:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtSerialNumber"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Reference -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblReference" runat="server" Text="Reference:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtReference"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Calibration Periodicity -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblCalibrationPeriodicity" runat="server" Text="Calibration Periodicity:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtCalibrationPeriodicity"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Maintenance -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMaintenance" runat="server" Text="Maintenance:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtMaintenance"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Installation Date-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblInstallationDate" runat="server" Text="Installation Date:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDatePicker ID="rdtInstallationDate" Skin="EMS" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput ID="DateInput1" runat="server" DateFormat="MM/dd/yyyy" />
                                <Calendar ID="Calendar1" runat="server">
                                </Calendar>
                            </rad:RadDatePicker>
                        </td>
                        <td class="ColValidator">
                            <asp:CompareValidator ID="cv2" runat="server" ErrorMessage="Data Error" ControlToValidate="rdtInstallationDate"
                                Operator="DataTypeCheck" SkinID="EMS" Display="Dynamic" Type="Date"></asp:CompareValidator>
                        </td>
                    </tr>                    
                    <!-- Magnitud + MeasurementUnit  -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementUnit" runat="server" Text="Measurement Unit" />
                        </td>
                        <td class="ColContent">
                            <div class="Treeview">
                                <asp:UpdatePanel ID="upHierarchicalListManage" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:PlaceHolder ID="phMeasurementUnit" runat="server"></asp:PlaceHolder>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Resource Catalog -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblResourceCatalog" runat="server" Text="Picture:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phResourceCatalog" runat="server"></asp:PlaceHolder>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
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
                    <!-- UpperLimit -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblUpperLimit" runat="server" Text="Upper Limit:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtUpperLimit"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="Validator" runat="server" ValidationExpression="\d*\.?\d*"
                                ErrorMessage="Wrong Format" ID="revUpperLimit" Display="Dynamic" ControlToValidate="txtUpperLimit"
                                Enabled="true"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <!-- LowerLimit -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLowerLimit" runat="server" Text="Lower Limit:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtLowerLimit"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="Validator" runat="server" ValidationExpression="\d*\.?\d*"
                                ErrorMessage="Wrong Format" ID="revLowerLimit" Display="Dynamic" ControlToValidate="txtLowerLimit"
                                Enabled="true"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <!-- Uncertainty -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblUncertainty" runat="server" Text="Uncertainty:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtUncertainty"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="Validator" runat="server" ValidationExpression="\d*\.?\d*"
                                ErrorMessage="Wrong Format" ID="revUncertainty" Display="Dynamic" ControlToValidate="txtUncertainty"
                                Enabled="true"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
