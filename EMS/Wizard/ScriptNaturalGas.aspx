<%@ Page Title="" Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="ScriptNaturalGas.aspx.cs" 
Inherits="Condesus.EMS.WebUI.Wizard.ScriptNaturalGas" %>

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
                    <!-- Organization -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblOrganization" runat="server" Text="Organization:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phOrganization" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phOrganizationValidator" runat="server" />
                        </td>
                    </tr>                           
                    <!-- Site -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblSite" runat="server" Text="Site:" />
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upSites" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:PlaceHolder ID="phSite" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>

                    <!-- Seleccionar las 7 Constantes -->
                    <!-- Constante a- Densidad Natural Gas -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstDesidadNaturalGas" runat="server" Text="Densidad del Gas Natural:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstDesidadNaturalGas" runat="server" />
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
                    <!-- Constante 18. Poder Calorifico del Natural Gas -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstPCNaturalGas" runat="server" Text="Poder Calorifico del Natural Gas:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstPCNaturalGas" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr> 
                    <!-- Constante 19. Factor de Emision del NaturalGas para CO2 -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstFENaturalGasCO2" runat="server" Text="Factor de Emision del Natural Gas para CO2:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstFENaturalGasCO2" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr> 
                    <!-- Constante 20. Factor de Emision del NaturalGas para CH4 -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstFENaturalGasCH4" runat="server" Text="Factor de Emision del Natural Gas para CH4:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstFENaturalGasCH4" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr> 
                    <!-- Constante 21. Factor de Emision del NaturalGas para N2O -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblConstFENaturalGasN2O" runat="server" Text="Factor de Emision del Natural Gas para N2O:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phConstFENaturalGasN2O" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr> 


                    <!-- Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblName" runat="server" Text="Name (en-US):" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtName" MaxLength="150" Text="Caudal Másico del Combustible Utilizado"></asp:TextBox>
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
                    <!-- Name es-AR-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblNameEsAR" runat="server" Text="Name (es-AR):" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtNameEsAR" MaxLength="150" Text="Caudal Másico del Combustible Utilizado"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator Display="Dynamic" SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                ID="RequiredFieldValidator1" ControlToValidate="txtNameEsAR"></asp:RequiredFieldValidator>
                            <asp:CustomValidator Display="Dynamic" SkinID="EMS" ID="CustomValidator1" runat="server" ErrorMessage="Invalid Special Characters"
                                ControlToValidate="txtNameEsAR" ClientValidationFunction="CheckIndexesTags"></asp:CustomValidator>
                        </td>
                    </tr>
                    <!-- AGENDAR LA TAREA -->
                    <!-- Start Date -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDateTimePicker ID="rdtStartDate" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput ID="DateInput1" runat="server" DateFormat="MM/dd/yyyy HH:mm:ss" />
                                <Calendar runat="server">
                                </Calendar>
                                <TimePopupButton ToolTip="<%$ Resources:Common, dtTimePickerTooltip %>" />
                                <TimeView ID="TimeView1" runat="server" TimeFormat="HH:mm:ss" Culture="English (United States)">
                                </TimeView>
                            </rad:RadDateTimePicker>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- End Date -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblEndDate" runat="server" Text="End Date:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDateTimePicker ID="rdtEndDate" runat="server" Culture="English (United States)">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput runat="server" DateFormat="MM/dd/yyyy HH:mm:ss" />
                                <Calendar runat="server">
                                </Calendar>
                                <TimePopupButton ToolTip="<%$ Resources:Common, dtTimePickerTooltip %>" />
                                <TimeView runat="server" TimeFormat="HH:mm:ss" Culture="English (United States)">
                                </TimeView>
                            </rad:RadDateTimePicker>
                        </td>
                        <td class="ColValidator">
                            <asp:CustomValidator ID="customvEndDate" runat="server" ControlToValidate="rdtEndDate"
                                SkinID="EMS" Display="Dynamic" ErrorMessage="The second date must be after the first one"></asp:CustomValidator>
                        </td>
                    </tr>
                    <!-- Duration -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblDuration" runat="server" Text="Duration:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtDuration" runat="server" SkinID="EMS"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="EMS" Display="Dynamic" runat="server" ValidationExpression="[1-9][\d]*"
                                ErrorMessage="Wrong Format" ID="revDuration" ControlToValidate="txtDuration"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator SkinID="EMS" Display="Dynamic" runat="server" ErrorMessage="Required Field"
                                ID="rfvDuration" ControlToValidate="txtDuration"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Time Unit -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTimeUnitDuration" runat="server" Text="Time Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phTimeUnitDuration" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phTimeUnitDurationValidator" runat="server" />
                        </td>
                    </tr>
                    <!-- Interval -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblInterval" runat="server" Text="Interval:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox SkinID="EMS" ID="txtInterval" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="EMS" Display="Dynamic" runat="server" ValidationExpression="[1-9][\d]*"
                                ErrorMessage="Wrong Format" ID="revInterval" ControlToValidate="txtInterval"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator SkinID="EMS" Display="Dynamic" runat="server" ErrorMessage="Required Field"
                                ID="rfvInterval" ControlToValidate="txtInterval"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Time Unit -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTimeUnitInterval" runat="server" Text="Time Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phTimeUnitInterval" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phTimeUnitIntervalValidator" runat="server" />
                        </td>
                    </tr>                    
                    <!-- Aviso -->
                    <!-- Time Unit Notification -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTimeUnitNotificacion" runat="server" Text="Reminder Time Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phTimeUnitNotification" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Notificacion -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblNotification" runat="server" Text="Reminder:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox SkinID="EMS" ID="txtNotification" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="EMS" Display="Dynamic" runat="server" ValidationExpression="[1-9][\d]*"
                                ErrorMessage="Wrong Format" ID="revNotification" ControlToValidate="txtNotification"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    
                    <!-- MEDICION -->
                    <!-- Frequency -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblFrequency" runat="server" Text="Frequency:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtFrequency" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="EMS" runat="server" ValidationExpression="[1-9][\d]*"
                                ErrorMessage="Wrong Format" ID="revFrequency" Display="Dynamic" ControlToValidate="txtFrequency"
                                Enabled="false"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="Required Field"
                                Enabled="false" ID="rfvFrequency" ControlToValidate="txtFrequency" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Time Unit -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblTimeUnitFrequency" runat="server" Text="Time Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phTimeUnitFrequency" runat="server" />
                        </td>
                        <td class="ColValidator">
                            <asp:PlaceHolder ID="phTimeUnitFrequencyValidator" runat="server" />
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
                    <!-- Accounting Scope -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblAccountingScope" runat="server" Text="Accounting Scope:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phAccountingScope" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Indicator y su class-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIndicatorClassification" runat="server" Text="Indicador de la Base:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblIndicatorValue" runat="server" Text="" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Condition -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblParameterGroup" runat="server" Text="Condition:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblParameterGroupValue" runat="server" Text="" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Measurement Unit (la magnitud la pone el Indicador seleccionado)-->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblMeasurementUnit" runat="server" Text="Measurement Unit:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblMeasurementUnitValue" runat="server" Text="" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    
                    <!-- Lo dejamos al Final -->
                    <!-- Asignacion de Operadores a la Tarea -->
                    <!-- Posts -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblPosts" runat="server" Text="Select the operators of the task:"></asp:Label>
                        </td>
                        <td class="ColContent">
                            <asp:UpdatePanel ID="upHierarchicalListManage" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <div class="Treeview">
                                        <asp:PlaceHolder ID="phPosts" runat="server" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="ColValidator">
                            <asp:CustomValidator ID="cvTreeView" Display="Dynamic" runat="server" SkinID="Validator"
                                Enabled="false" ErrorMessage="Required Field"></asp:CustomValidator>
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
