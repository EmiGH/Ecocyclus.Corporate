<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="MeasurementUnitsProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PA.MeasurementUnitsProperties" Title="EMS - Measurement Unit Property"
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
                    <!-- Magnitud -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMagnitud" runat="server" Text="Magnitud:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phMagnitud" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phMagnitudValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblName" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtName" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator Display="Dynamic" SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                            <asp:CustomValidator Display="Dynamic" SkinID="EMS" ID="cv1" runat="server" ErrorMessage="Invalid Special Characters"
                                ControlToValidate="txtName" ClientValidationFunction="CheckIndexesTags"></asp:CustomValidator>
                        </td>
                    </tr>
                    <!-- Language -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLanguage" runat="server" Text="Language:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblLanguageValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDescription" MaxLength="8000" runat="server" TextMode="MultiLine"
                                Rows="6"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Numerator -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblNumerator" runat="server" Text="Numerator:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtNumerator" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtNumerator"
                                ErrorMessage="Required Field" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cv2" runat="server" Display="Dynamic" ControlToValidate="txtNumerator"
                                ErrorMessage="Invalid Data" Operator="DataTypeCheck" SkinID="EMS" Type="Integer"></asp:CompareValidator>
                        </td>
                    </tr>
                    <!-- Denominator -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDenominator" runat="server" Text="Denominator:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDenominator" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv5" runat="server" Display="Dynamic" ControlToValidate="txtDenominator"
                                ErrorMessage="Required Field" SkinID="EMS"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator SkinID="Validator" runat="server" ValidationExpression="^[-+]?\d*\.?\d*$" ErrorMessage="Wrong Format" 
                                ID="revDenominator" Display="Dynamic" ControlToValidate="txtDenominator" Enabled="true"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <!-- Exponent -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblExponent" runat="server" Text="Exponent:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtExponent" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ControlToValidate="txtExponent"
                                ErrorMessage="Required Field" SkinID="EMS"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cv4" runat="server" Display="Dynamic" ControlToValidate="txtExponent"
                                ErrorMessage="Invalid Data" Operator="DataTypeCheck" SkinID="EMS" Type="Integer"></asp:CompareValidator>
                        </td>
                    </tr>
                    <!-- Constant -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstant" runat="server" Text="Constant:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtConstant" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ControlToValidate="txtConstant"
                                ErrorMessage="Required Field" SkinID="EMS"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator SkinID="Validator" runat="server" ValidationExpression="^[-+]?\d*\.?\d*$" ErrorMessage="Wrong Format" 
                                ID="revConstant" Display="Dynamic" ControlToValidate="txtConstant" Enabled="true"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <!-- Is Pattern -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIsPattern" runat="server" Text="Is Pattern:" />
                        </td>
                        <td class="ColContent">
                            <div class="Check">
                                <asp:CheckBox SkinID="EMS" ID="chkIsPattern" runat="server"></asp:CheckBox>
                            </div>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
