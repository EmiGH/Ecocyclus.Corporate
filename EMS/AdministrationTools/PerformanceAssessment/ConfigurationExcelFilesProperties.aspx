<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ConfigurationExcelFilesProperties.aspx.cs" 
Inherits="Condesus.EMS.WebUI.AdministrationTools.PerformanceAssessment.ConfigurationExcelFilesProperties" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <style type="text/css">
        /* Seteo el alto del listbox*/
        div.RadListBox .rlbGroup
        {
            height: 110px;
        }
    </style>

    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <table id="tblContentForm" runat="server" class="ContentForm">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                        <col class="ColValidator" />
                    </colgroup>
                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblName" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtName" runat="server" MaxLength="150"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator Display="Dynamic" SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ID="rfv1" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- StartIndexOfDataRows -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblStartIndexOfDataRows" runat="server" Text="Fila Inicial:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtStartIndexOfDataRows" runat="server" MaxLength="6"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- StartIndexOfDataCols -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblStartIndexOfDataCols" runat="server" Text="Columna Inicial:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtStartIndexOfDataCols" runat="server" MaxLength="6"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>       
                    <!-- ValueInRow -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIsDataRows" runat="server" Text="Valores en Fila:" />
                        </td>
                        <td class="ColContentCheck">
                            <asp:CheckBox CssClass="CheckBox" ID="chkIsDataRows" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>  
                    <!-- DatesIndex StartDate -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDatesIndexStart" runat="server" Text="Donde esta la Fecha Inicial (columna o fila):" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDatesIndexStart" runat="server" MaxLength="6"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                                                         
                    <!-- DatesIndex -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDatesIndexEnd" runat="server" Text="Donde esta la Fecha Final (columna o fila):" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDatesIndexEnd" runat="server" MaxLength="6"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>                                                         
                </table>
            </td>
        </tr>
    </table>

    <table runat="server" id="tblContentSourceType" class="ContentForm">
        <tr>
            <td>
                <table style="margin: 8px;">
                    <tr>
                        <td style="border: solid 1px #ccc; vertical-align: top; padding: 4px 0 8px 0;">
                            <%-- Measurement --%>
                            <asp:Panel runat="server" ID="contentHideMeasurement" Style="display: block;">
                                <table>
                                    <!-- Site -->
                                    <tr>
                                        <td class="ColContentDoubleColumn">
                                            <asp:Label ID="lblSite" runat="server" Text="Site:" CssClass="Title" />
                                            <asp:PlaceHolder ID="phSite" runat="server"></asp:PlaceHolder>
                                        </td>
                                    </tr>                                
                                    <!-- Measurement -->
                                    <tr>
                                        <td class="ColContentDoubleColumn">
                                            <asp:Label ID="lblMeasurement" runat="server" Text="Measurement:" CssClass="Title" />
                                            <asp:UpdatePanel ID="upMeasurement" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:PlaceHolder ID="phMeasurement" runat="server" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <!-- IndexMesurement -->
                                    <tr>
                                        <td class="ColContentDoubleColumn">
                                            <asp:Label ID="lblIndexMesurement" runat="server" Text="Donde esta la Medición:" CssClass="Title" />
                                            <asp:TextBox ID="txtIndexMesurement" runat="server" MaxLength="6"></asp:TextBox>
                                        </td>
                                    </tr>     
                                    <!-- IndexMesurement Date -->
                                    <tr>
                                        <td class="ColContentDoubleColumn">
                                            <asp:Label ID="lblIndexMesurementDate" runat="server" Text="Donde esta la Fecha de la Medición:" CssClass="Title" />
                                            <asp:TextBox ID="txtIndexMesurementDate" runat="server" MaxLength="6"></asp:TextBox>
                                        </td>
                                    </tr>     
                                </table>
                            </asp:Panel>
                        </td>
                        <td>
                            <asp:Button ID="btnAddOperand" runat="server" Text="" ToolTip="Add Operand" CssClass="contentCalculationOfTransformationsPropertiesbtnAdd"
                                ValidationGroup="other" BorderWidth="0px" BorderColor="Transparent" />
                            <br />
                            <asp:Button ID="btnRemoveOperand" runat="server" Text="" ToolTip="Remove Operand"
                                ValidationGroup="other" CssClass="contentCalculationOfTransformationsPropertiesbtnRemove"
                                BorderWidth="0px" BorderColor="Transparent" />
                        </td>
                        <td style="border: solid 1px #ccc; padding: 4px 0 8px 0;">
                            <%-- Seleccionados --%>
                            <asp:Panel runat="server" ID="contentSelectedOperand" Style="display: block;">
                                <table>
                                    <!-- Operadores -->
                                    <tr>
                                        <td class="ColContentDoubleColumn">
                                            <asp:Label ID="lblOperand" runat="server" Text="Operands:" CssClass="Title" />
                                            <asp:UpdatePanel ID="upOperands" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <%-- Recordar Setear el Width --%>
                                                    <rad:RadListBox ID="rdlstbOperands" Skin="EMS" runat="server" Width="400px" SelectionMode="Single"
                                                        AllowDelete="true">
                                                    </rad:RadListBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>    
</asp:Content>
