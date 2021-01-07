﻿<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ScriptGasolineTransfo.aspx.cs" 
Inherits="Condesus.EMS.WebUI.Wizard.ScriptGasolineTransfo" %>

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
                    <!-- Process -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblProcess" runat="server" Text="Process:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phProcess" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
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
                    <!-- Transformation -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTransformation" runat="server" Text="Transformation:" />
                        </td>
                        <td class="ColContent">
                           <asp:PlaceHolder ID="phTransformation" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>

                    <!-- Seleccionar las 7 Constantes -->
                    <!-- Constante a- Densidad Gasoline -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstDesidadGasoline" runat="server" Text="Densidad del Gasoline:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstDesidadGasoline" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Constante b- PCG_CH4 -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstPCG_CH4" runat="server" Text="Potencial de Calentamiento Global para CH4:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstPCG_CH4" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Constante c- PCG_N2O-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstPCG_N2O" runat="server" Text="Potencial de Calentamiento Global para N2O:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstPCG_N2O" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Constante 18. Poder Calorifico del Gasoline -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstPCGasoline" runat="server" Text="Poder Calorifico del Gasoline:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstPCGasoline" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr> 
                    <!-- Constante 19. Factor de Emision del Gasoline para CO2 -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstFEGasolineCO2" runat="server" Text="Factor de Emision del Gasoline para CO2:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstFEGasolineCO2" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr> 
                    <!-- Constante 20. Factor de Emision del Gasoline para CH4 -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstFEGasolineCH4" runat="server" Text="Factor de Emision del Gasoline para CH4:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstFEGasolineCH4" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr> 
                    <!-- Constante 21. Factor de Emision del Gasoline para N2O -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstFEGasolineN2O" runat="server" Text="Factor de Emision del Gasoline para N2O:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstFEGasolineN2O" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr> 
                    
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
                            <asp:TextBox runat="server" ID="txtListEmails" MaxLength="8000" TextMode="MultiLine" Rows="6"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator ID="revEmails" ControlToValidate="txtListEmails" runat="server" 
                                ErrorMessage="You must enter an valid email address. eg.: lewis@moten.com | lewis@moten.com, me@lewismoten.com | lewis@moten.com;me@lewismoten.com" 
                                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*([,;]\s*\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)*"> 
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>                                       
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
