<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true"
    CodeBehind="CalculationOfTransformationsProperties.aspx.cs" Inherits="Condesus.EMS.WebUI.AdministrationTools.PerformanceAssessment.CalculationOfTransformationsProperties" %>

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
                    <!-- Accounting Activity -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblAccountingActivity" runat="server" Text="Accounting Activity:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phAccountingActivity" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Indicator -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicator" runat="server" Text="Indicator:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phIndicator" runat="server"></asp:PlaceHolder>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phIndicatorValidator" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <!-- Magnitud -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMagnitud" runat="server" Text="Magnitud:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upMagnitud" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:Label ID="lblMagnitudValue" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Measurement Unit -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementUnit" runat="server" Text="Measurement Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upMeasurementUnit" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phMeasurementUnit" runat="server"></asp:PlaceHolder>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phMeasurementUnitValidator" runat="server"></asp:PlaceHolder>
                        </td>
                    </tr>
                    <!-- Trasnformation Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblName" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfv1"
                                ControlToValidate="txtName" SkinID="Validator"></asp:RequiredFieldValidator>
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
                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" MaxLength="8000"
                                Rows="6"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Selected Type Source Data (Medicion, Constante o Transformacion)-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblSourceType" runat="server" Text="Select Source Type:" />
                        </td>
                        <td class="ColContentList">
                            <asp:RadioButtonList ID="rbList" runat="server">
                                <asp:ListItem Text="Measurement" Value="rbMeasurement" Selected="True">
                                </asp:ListItem>
                                <asp:ListItem Text="Constant" Value="rbConstant">
                                </asp:ListItem>
                                <asp:ListItem Text="Transformation" Value="rbTransformation">
                                </asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <!--Start -->
                <table runat="server" id="tblContentSourceType" class="ContentForm" >
                    <tr>
                        <td>
                            <table style="margin: 8px;" class="tblContentDoubleColumn">
                                <tr>
                                    <td class="tdLeft">
                                        <%-- Measurement --%>
                                        <asp:Panel runat="server" ID="contentHideMeasurement" Style="display: block;">
                                            <table>
                                                <!-- Site -->
                                                <tr>
                                                    <td class="ColContentDoubleColumn">
                                                        <asp:Label ID="lblSite" runat="server" Text="Site:" CssClass="TitleDoubleColumn" />
                                                        <asp:PlaceHolder ID="phSite" runat="server"></asp:PlaceHolder>
                                                    </td>
                                                </tr>
                                                <!-- Measurement -->
                                                <tr>
                                                    <td class="ColContentDoubleColumn">
                                                        <asp:Label ID="lblMeasurement" runat="server" Text="Measurement:" CssClass="TitleDoubleColumn" />
                                                        <asp:UpdatePanel ID="upMeasurement" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <asp:PlaceHolder ID="phMeasurement" runat="server" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <%--Constantes--%>
                                        <asp:Panel runat="server" ID="contentHideConstant" Style="display: none;">
                                            <table>
                                                <!-- Constant Classification -->
                                                <tr>
                                                    <td class="ColContentDoubleColumn">
                                                        <asp:Label ID="lblConstantClassification" runat="server" Text="Constant Classification:"
                                                            CssClass="TitleDoubleColumn" />
                                                        <asp:PlaceHolder ID="phConstantClassification" runat="server" />
                                                    </td>
                                                </tr>
                                                <!-- Constant -->
                                                <tr>
                                                    <td class="ColContentDoubleColumn">
                                                        <asp:Label ID="lblConstant" runat="server" Text="Constant:" CssClass="TitleDoubleColumn" />
                                                        <asp:UpdatePanel ID="upConstant" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <asp:PlaceHolder ID="phConstant" runat="server" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <%--Transformation--%>
                                        <asp:Panel runat="server" ID="contentHideTransformation" Style="display: none;">
                                            <table>
                                                <!-- Transformation -->
                                                <tr>
                                                    <td class="ColContentDoubleColumn">
                                                        <asp:Label ID="lblTransformation" runat="server" Text="Transformation:" CssClass="TitleDoubleColumn" />
                                                        <asp:UpdatePanel ID="upTransformation" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <asp:PlaceHolder ID="phTransformation" runat="server" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <%-- Variable --%>
                                        <asp:Panel runat="server" ID="contentVariable" Style="display: block;">
                                            <table>
                                                <!-- Variable -->
                                                <tr>
                                                    <td class="ColContentDoubleColumn">
                                                        <asp:Label ID="lblVariable" runat="server" Text="Variable:" CssClass="TitleDoubleColumn" />
                                                        <asp:UpdatePanel ID="upVariable" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <asp:PlaceHolder ID="phVariable" runat="server" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td class="tdCenter">
                                        <asp:Button ID="btnAddOperand" runat="server" Text="" ToolTip="Add Operand" CssClass="contentCalculationOfTransformationsPropertiesbtnAdd"
                                            ValidationGroup="other" BorderWidth="0px" BorderColor="Transparent" />
                                        <br />
                                        <asp:Button ID="btnRemoveOperand" runat="server" Text="" ToolTip="Remove Operand"
                                            ValidationGroup="other" CssClass="contentCalculationOfTransformationsPropertiesbtnRemove"
                                            BorderWidth="0px" BorderColor="Transparent" />
                                    </td>
                                    <td class="tdRight">
                                        <%-- Seleccionados --%>
                                        <asp:Panel runat="server" ID="contentSelectedOperand" Style="display: block;">
                                            <table>
                                                <!-- Operadores -->
                                                <tr>
                                                    <td class="ColContentDoubleColumn">
                                                        <asp:Label ID="lblOperand" runat="server" Text="Operands:" CssClass="TitleDoubleColumn" />
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
                            <!--End-->
                        </td>
                    </tr>
                </table>
                <%-- Formula --%>
                <asp:Panel runat="server" ID="contentFormula" Style="display: block;">
                    <table id="tblContentFormFormula" runat="server" class="ContentForm">
                        <colgroup>
                            <col class="ColTitle" />
                            <col class="ColContent" />
                            <col class="ColValidator" />
                        </colgroup>
                        <!-- Base -->
                        <tr>
                            <td class="ColTitle">
                                <asp:Label ID="lblBase" runat="server" Text="Base:" />
                            </td>
                            <td class="ColContent">
                                <asp:Label ID="lblBaseValue" runat="server" />
                            </td>
                            <td class="ColValidator">
                                &nbsp;
                            </td>
                        </tr>
                        <!-- Formula Example -->
                        <tr>
                            <td class="ColTitle">
                                <asp:Label ID="lblFormulaExample" runat="server" Text="Formula Example:" />
                            </td>
                            <td class="ColContent">
                                <asp:Label ID="lblFormulaExampleValue" runat="server" Text="(Base * A + B) / 1000" />
                            </td>
                            <td class="ColValidator">
                                &nbsp;
                            </td>
                        </tr>
                        <!-- Formula -->
                        <tr>
                            <td class="ColTitle">
                                <asp:Label ID="lblFormula" runat="server" Text="Formula:" />
                            </td>
                            <td class="ColContent">
                                <asp:TextBox ID="txtFormula" runat="server" Width="270px"></asp:TextBox>
                                <asp:LinkButton ID="lnkTestFormula" runat="server" Text="Test Formula" ToolTip="Test Formula"
                                    ValidationGroup="other"></asp:LinkButton>
                            </td>
                            <td class="ColValidator">
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfvFormula"
                                    ControlToValidate="txtFormula" SkinID="Validator"></asp:RequiredFieldValidator>
                                <asp:CustomValidator Display="Dynamic" SkinID="EMS" ID="cvFormula" runat="server"
                                    ErrorMessage="The variable 'Base' must always be declared in the formula." ControlToValidate="txtFormula"
                                    ClientValidationFunction="CheckContainVariableBaseinFormula"></asp:CustomValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <%-- Notification Recipients --%>
                <asp:Panel runat="server" ID="contentNotificationRecipient" Style="display: block;">
                    <table id="tblContentFormNotificationRecipient" runat="server" class="ContentForm">
                        <colgroup>
                            <col class="ColTitle" />
                            <col class="ColContent" />
                            <col class="ColValidator" />
                        </colgroup>
                        <!-- Asignacion de email para Notificar errores en la Tarea -->
                        <!-- Emails -->
                        <tr>
                            <td class="ColTitle">
                                <asp:Label ID="lblEmails" runat="server" Text="Select the e-mails person to notify:"></asp:Label>
                            </td>
                            <td class="ColContent">
                                <asp:UpdatePanel ID="upHierarchicalListManageEmails" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <div class="Treeview">
                                            <asp:PlaceHolder ID="phEmails" runat="server" />
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="ColValidator">
                                &nbsp;
                            </td>
                        </tr>
                        <!-- List emails-->
                        <tr>
                            <td class="ColTitle">
                                <asp:Label ID="lblListEmails" runat="server" Text="Or undeclared Emails:" />
                            </td>
                            <td class="ColContent">
                                <asp:TextBox runat="server" ID="txtListEmails" MaxLength="8000" TextMode="MultiLine"
                                    Rows="6"></asp:TextBox>
                            </td>
                            <td class="ColValidator">
                                <asp:RegularExpressionValidator ID="revEmails" ControlToValidate="txtListEmails"
                                    runat="server" ErrorMessage="You must enter an valid email address. eg.: lewis@moten.com | lewis@moten.com, me@lewismoten.com | lewis@moten.com;me@lewismoten.com"
                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"> 
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>