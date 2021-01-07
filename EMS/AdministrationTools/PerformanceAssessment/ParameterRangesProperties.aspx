<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="ParameterRangesProperties.aspx.cs" Inherits="Condesus.EMS.WebUI.PA.ParameterRangesProperties"
    Culture="auto" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="rad" %>
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
                    <!-- Low Value -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLowValue" runat="server" Text="Low Value:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtLowValue"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="Validator" runat="server" Display="Dynamic" ErrorMessage="Required Field"
                                ID="rfvLV" ControlToValidate="txtLowValue"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvLV" runat="server" Display="Dynamic" ErrorMessage="Invalid Data"
                                ControlToValidate="txtLowValue" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                            <asp:CompareValidator ID="cvLVLessHV" runat="server" Display="Dynamic" ErrorMessage="Low Value must be lower than High Value"
                                ControlToValidate="txtLowValue" Operator="LessThan" Type="Double" ControlToCompare="txtHighValue"></asp:CompareValidator>
                        </td>
                    </tr>
                    <!-- High Value -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblHighValue" runat="server" Text="High Value:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtHighValue"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="Validator" runat="server" Display="Dynamic" ErrorMessage="Required Field"
                                ID="rfvHV" ControlToValidate="txtHighValue"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvHV" runat="server" Display="Dynamic" ErrorMessage="Invalid Data"
                                ControlToValidate="txtHighValue" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                            <asp:CompareValidator ID="cvHVHigherLV" runat="server" Display="Dynamic" ErrorMessage="High Value must be higher than Lower Value"
                                ControlToValidate="txtHighValue" Operator="GreaterThan" Type="Double" ControlToCompare="txtLowValue"></asp:CompareValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
