<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ConstantsProperties.aspx.cs" Inherits="Condesus.EMS.WebUI.AdministrationTools.PerformanceAssessment.ConstantsProperties" %>

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
                    <!-- Constant Classification -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstantClassification" runat="server" Text="Constant Classification:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstantClassification" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phConstantClassificationValidator" runat="server" />
                        </td>
                    </tr>
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
                    <!-- MeasurementUnit -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementUnit" runat="server" Text="Measurement Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upMeasurementUnit" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phMeasurementUnit" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phMeasurementUnitValidator" runat="server" />
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
                            <asp:CustomValidator Display="Dynamic" SkinID="EMS" ID="rfv2" runat="server" ErrorMessage="Invalid Special Characters"
                                ControlToValidate="txtName" ClientValidationFunction="CheckIndexesTags"></asp:CustomValidator>
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDescription" runat="server" Text="Description:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="6" MaxLength="8000" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
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
                    <!-- Symbol -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblSymbol" runat="server" Text="Symbol:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtSymbol" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator Display="Dynamic" SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ID="rfvSymbol" ControlToValidate="txtSymbol"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Value -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblValue" runat="server" Text="Value:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtValue"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="Validator" runat="server" Display="Dynamic" ErrorMessage="Required Field"
                                ID="rfvValue" ControlToValidate="txtValue"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator SkinID="Validator" runat="server" ValidationExpression="^[-+]?\d*\.?\d*$" ErrorMessage="Wrong Format" 
                                ID="revValue" Display="Dynamic" ControlToValidate="txtValue" Enabled="true"></asp:RegularExpressionValidator>
                        </td>
                    </tr>                    
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
