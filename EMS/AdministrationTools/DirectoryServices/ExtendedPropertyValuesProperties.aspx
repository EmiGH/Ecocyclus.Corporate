<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ExtendedPropertyValuesProperties.aspx.cs" 
Inherits="Condesus.EMS.WebUI.AdministrationTools.DirectoryServices.ExtendedPropertyValuesProperties" %>

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
                    <!-- ExtendedPropertyClassification -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblExtendedPropertyClassification" runat="server" Text="Extended Property Classification:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phExtendedPropertyClassification" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phExtendedPropertyClassificationValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- ExtendedProperty -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblExtendedProperty" runat="server" Text="Extended Property:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phExtendedProperty" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phExtendedPropertyValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Value -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblValue" runat="server" Text="Value:"/>
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtValue" TextMode="MultiLine" Rows="6" MaxLength="500" ></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfv1"
                                ControlToValidate="txtValue" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
