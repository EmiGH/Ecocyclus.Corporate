<%@ Page Language="C#" MasterPageFile="~/EMS.Master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeBehind="ProcessTaskMeasurementsProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PM.ProcessTaskMeasurementsProperties" Title="EMS - Process Task Measurements Properties"
    Culture="auto" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <rad:RadTabStrip ID="rtsWizzardTask" runat="server" MultiPageID="rmpWizzardTask"
        SelectedIndex="0" EnableEmbeddedSkins="false" Skin="EMS" CausesValidation="false">
        <Tabs>
            <rad:RadTab Text="Main Data">
            </rad:RadTab>
            <rad:RadTab Text="Scheduler">
            </rad:RadTab>
            <rad:RadTab Text="Measurement">
            </rad:RadTab>
            <rad:RadTab Text="Task Operators">
            </rad:RadTab>
            <rad:RadTab Text="Notification">
            </rad:RadTab>
        </Tabs>
    </rad:RadTabStrip>
    <rad:RadMultiPage ID="rmpWizzardTask" runat="server" SelectedIndex="0">
        <rad:RadPageView ID="rpvMainData" runat="server" Selected="true">
            <table id="tblContentForm" runat="server" class="ContentForm">
                <colgroup>
                    <col class="ColTitle" />
                    <col class="ColContent" />
                    <col class="ColValidator" />
                </colgroup>
                <!-- Project Title -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblProjectTitle" runat="server" Text="Project Title:" />
                    </td>
                    <td class="ColContent">
                        <asp:Label ID="lblProjectTitleValue" runat="server" />
                    </td>
                    <td class="ColValidator">
                        &nbsp;
                    </td>
                </tr>
                <%--<!-- Node -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblIdParent" runat="server" Text="Node" />
                    </td>
                    <td class="ColContent">
                        <asp:Label ID="lblIdParentValue" runat="server" />
                    </td>
                    <td class="ColValidator">
                        &nbsp;
                    </td>
                </tr>--%>
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
                <!-- Title -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblTask" runat="server" Text="Title:" />
                    </td>
                    <td class="ColContent">
                        <asp:TextBox runat="server" ID="txtTitle" MaxLength="150" SkinID="EMS"></asp:TextBox>
                    </td>
                    <td class="ColValidator">
                        <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="&larr; Required Field"
                            Display="Dynamic" ID="rfv1" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <!-- Order -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblOrder" runat="server" Text="Order:" />
                    </td>
                    <td class="ColContent">
                        <asp:TextBox runat="server" ID="txtOrder" SkinID="EMS"></asp:TextBox>
                    </td>
                    <td class="ColValidator">
                        <asp:RegularExpressionValidator SkinID="EMS" runat="server" ValidationExpression="[-]?[\d]*"
                            ErrorMessage="Wrong Format" ID="rfvOrder" ControlToValidate="txtOrder" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <!-- Purpose -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblPurpose" runat="server" Text="Purpose:" />
                    </td>
                    <td class="ColContent">
                        <asp:TextBox ID="txtPurpose" runat="server" MaxLength="8000" Rows="6" TextMode="MultiLine"></asp:TextBox>
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
                        <asp:TextBox ID="txtDescription" MaxLength="8000" runat="server" Rows="6" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td class="ColValidator">
                        &nbsp;
                    </td>
                </tr>
                <!-- Weight -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblWeight" runat="server" Text="Weight:" />
                    </td>
                    <td class="ColContent">
                        <asp:TextBox ID="txtWeight" runat="server" Text="100" SkinID="EMS"></asp:TextBox>
                    </td>
                    <td class="ColValidator">
                        <asp:RangeValidator ID="rvWeight" ControlToValidate="txtWeight" MinimumValue="0"
                            Display="Dynamic" MaximumValue="100" Type="Integer" runat="server" ErrorMessage="&larr; Wrong Format"
                            SkinID="EMS"></asp:RangeValidator>
                        <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="&larr; Required Field"
                            ID="rfvWeight" ControlToValidate="txtWeight" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <!-- Main data nuevamente??? -->
                <!-- Result -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblResult" runat="server" Text="Result:" />
                    </td>
                    <td class="ColContent">
                        <asp:Label ID="lblResultValue" runat="server" />
                    </td>
                    <td class="ColValidator">
                        &nbsp;
                    </td>
                </tr>
                <!-- Completed Percentage -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblCompleted" runat="server" Text="Completed Percentage:" />
                    </td>
                    <td class="ColContent">
                        <ajaxToolkit:SliderExtender ID="SliderExtender3" runat="server" BehaviorID="Slider3_BoundControl"
                            TargetControlID="Slider3_BoundControl" BoundControlID="txtCompleted" EnableHandleAnimation="True"
                            TooltipText="Slider: value {0}. Please slide to change value." Enabled="True" />
                        <asp:TextBox ID="txtCompleted" Width="30px" runat="server"></asp:TextBox>
                        <asp:TextBox ID="Slider3_BoundControl" Width="30px" runat="server" />
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
                <!-- Resource -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblResources" runat="server" Text="Resource:" />
                    </td>
                    <td class="ColContent">
                        <asp:PlaceHolder ID="phResources" runat="server" />
                    </td>
                    <td class="ColValidator">
                        <asp:PlaceHolder ID="phResourcesValidator" runat="server" />
                    </td>
                </tr>
                <!-- FIN Main data nuevamente??? -->
            </table>
        </rad:RadPageView>
        <rad:RadPageView ID="rpvScheduler" runat="server" Selected="false">
            <table id="tblContentForm2" runat="server" class="ContentForm">
                <colgroup>
                    <col class="ColTitle" />
                    <col class="ColContent" />
                    <col class="ColValidator" />
                </colgroup>
                <!-- Schedule??? -->
                <!-- RadioButtonList -->
                <tr>
                    <td class="ColTitle">
                    </td>
                    <td class="ColContentList">
                        <asp:RadioButtonList ID="rblOptionTypeExecution" runat="server">
                            <asp:ListItem Text="Spontaneous" Enabled="false"></asp:ListItem>
                            <asp:ListItem Text="Recurrent" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Scheduled" Enabled="false"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="ColValidator">
                        <asp:RequiredFieldValidator Display="Dynamic" SkinID="EMS" runat="server" ErrorMessage="You must make a choice"
                            ID="rfvOptionTypeExecution" ControlToValidate="rblOptionTypeExecution"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <!-- Start Date -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" />
                    </td>
                    <td class="ColContent">
                        <rad:RadDateTimePicker ID="rdtStartDate" runat="server" Culture="English (United States)">
                            <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                            <DateInput ID="DateInput1" runat="server" DateFormat="MM/dd/yyyy HH:mm:ss" />
                            <Calendar ID="calStartDate" runat="server">
                            </Calendar>
                            <TimePopupButton ToolTip="<%$ Resources:Common, dtTimePickerTooltip %>" />
                            <TimeView ID="tvStartDate" runat="server" TimeFormat="HH:mm:ss" Culture="English (United States)">
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
                            <DateInput runat="server" DateFormat="MM/dd/yyyy HH:mm:ss"/>
                            <Calendar ID="calEndDate" runat="server">
                            </Calendar>
                            <TimePopupButton ToolTip="<%$ Resources:Common, dtTimePickerTooltip %>" />
                            <TimeView ID="tvEndDate" runat="server" TimeFormat="HH:mm:ss" Culture="English (United States)">
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
                
                <!-- Max Number Executions -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblMaxNumberExecutions" runat="server" Text="Max Number Executions:" />
                    </td>
                    <td class="ColContent">
                        <asp:TextBox ID="txtMaxNumberExecutions" runat="server" ToolTip="Use 0 (zero) for Infinite"
                            SkinID="EMS"></asp:TextBox>
                    </td>
                    <td class="ColValidator">
                        <asp:RegularExpressionValidator SkinID="EMS" runat="server" ValidationExpression="[\d]*"
                            ErrorMessage="Wrong Format" ID="revNumberExec" Display="Dynamic" ControlToValidate="txtMaxNumberExecutions"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <!-- FIN Schedule??? ^^^^^^^^^^^^^^ -->
            </table>
        </rad:RadPageView>
        <rad:RadPageView ID="rpvMeasurement" runat="server" Selected="false">
            <table id="tblContentForm3" runat="server" class="ContentForm">
                <colgroup>
                    <col class="ColTitle" />
                    <col class="ColContent" />
                    <col class="ColValidator" />
                </colgroup>
                <!-- Measurement -->
                <!-- Is Relevant -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblIsRelevant" runat="server" Text="Is Relevant:" />
                    </td>
                    <td class="ColContent">
                        <div class="Check">
                            <asp:CheckBox ID="chkIsRelevant" SkinID="EMS" runat="server" Checked="True" />
                        </div>
                    </td>
                    <td class="ColValidator">
                        &nbsp;
                    </td>
                </tr>
                <!-- Measurement Regressive -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblIsRegressive" runat="server" Text="Measurement Regressive" />
                    </td>
                    <td class="ColContent">
                        <div class="Check">
                            <asp:CheckBox ID="chkIsRegressive" SkinID="EMS" runat="server" Checked="True" />
                        </div>
                    </td>
                    <td class="ColValidator">
                        &nbsp;
                    </td>
                </tr>
                <!-- Indicator y su class-->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblIndicatorClassification" runat="server" Text="Classification:" />
                    </td>
                    <td class="ColContent">
                        <asp:PlaceHolder ID="phIndicator" runat="server" />
                        <%--<asp:LinkButton runat="server" ID="lnkIndicator" SkinID="Form" CausesValidation="false"></asp:LinkButton>--%>
                    </td>
                    <td class="ColValidator">
                        <asp:PlaceHolder ID="phIndicatorValidator" runat="server" />
                    </td>
                </tr>
                <!-- Parameter Group -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblParameterGroup" runat="server" Text="Parameter Group:" />
                    </td>
                    <td class="ColContent">
                        <asp:UpdatePanel ID="upParameterGroup" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <div class="Treeview">
                                    <asp:PlaceHolder ID="phParameterGroup" runat="server" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--<asp:LinkButton runat="server" ID="lnkParameterGroup" SkinID="Form" CausesValidation="false"></asp:LinkButton>--%>
                    </td>
                    <td class="ColValidator">
                        <asp:PlaceHolder ID="phParameterGroupValidator" runat="server" />
                    </td>
                </tr>
                <!-- Measurement Device Type -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblMeasurementDeviceType" runat="server" Text="Measurement Device Type:"></asp:Label>
                    </td>
                    <td class="ColContent">
                        <asp:PlaceHolder ID="phMeasurementDeviceType" runat="server" />
                        <%--<asp:LinkButton runat="server" ID="lnkMeasurementDeviceType" SkinID="Form" CausesValidation="false"></asp:LinkButton>--%>
                    </td>
                    <td class="ColValidator">
                        &nbsp;
                    </td>
                </tr>
                <!-- Measurement Device -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblMeasurementDevice" runat="server" Text="Measurement Device:" />
                    </td>
                    <td class="ColContent">
                        <asp:UpdatePanel ID="upDevice" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:PlaceHolder ID="phMeasurementDevice" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--<asp:LinkButton runat="server" ID="lnkMeasurementDevice" SkinID="Form" CausesValidation="false"></asp:LinkButton>--%>
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
                        <asp:UpdatePanel ID="upMeasurementUnit" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <asp:PlaceHolder ID="phMeasurementUnit" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--<asp:LinkButton runat="server" ID="lnkMeasurementUnit" SkinID="Form" CausesValidation="false"></asp:LinkButton>--%>
                    </td>
                    <td class="ColValidator">
                        <asp:PlaceHolder ID="phMeasurementUnitValidator" runat="server" />
                    </td>
                </tr>
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
                <!-- Measurement Source -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblMeasurementSource" runat="server" Text="Measurement Source:" />
                    </td>
                    <td class="ColContent">
                        <asp:TextBox ID="txtMeasurementSource" MaxLength="8000" runat="server" TextMode="MultiLine"
                            Rows="6"></asp:TextBox>
                    </td>
                    <td class="ColValidator">
                        <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="&larr; Required Field"
                            Display="Dynamic" ID="rfvMeasurementSource" ControlToValidate="txtMeasurementSource"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <!-- Measurement Frequency At Source-->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblMeasurementFrequencyAtSource" runat="server" Text="Measurement Frequency At Source:" />
                    </td>
                    <td class="ColContent">
                        <asp:TextBox ID="txtMeasurementFrequencyAtSource" MaxLength="8000" runat="server" TextMode="MultiLine"
                            Rows="6"></asp:TextBox>
                    </td>
                    <td class="ColValidator">
                        <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="&larr; Required Field"
                            Display="Dynamic" ID="rfvMeasurementFrequencyAtSource" ControlToValidate="txtMeasurementFrequencyAtSource"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <!-- Measurement Uncertainty -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblMeasurementUncertainty" runat="server" Text="Measurement Uncertainty:"></asp:Label>
                    </td>
                    <td class="ColContent">
                        <asp:TextBox runat="server" ID="txtMeasurementUncertainty"></asp:TextBox>
                    </td>
                    <td class="ColValidator">
                        <asp:RegularExpressionValidator SkinID="Validator" runat="server" ValidationExpression="\d*\.?\d*" ErrorMessage="Wrong Format" 
                            ID="revValue" Display="Dynamic" ControlToValidate="txtMeasurementUncertainty" Enabled="true"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <!-- Quality -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblQuality" runat="server" Text="Quality:"></asp:Label>
                    </td>
                    <td class="ColContent">
                        <asp:PlaceHolder ID="phQuality" runat="server" />
                    </td>
                    <td class="ColValidator">
                        &nbsp;
                    </td>
                </tr>
                <!-- Methodology -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblMethodology" runat="server" Text="Methodology:"></asp:Label>
                    </td>
                    <td class="ColContent">
                        <asp:PlaceHolder ID="phMethodology" runat="server" />
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
                        <asp:PlaceHolder ID="phAccountingActivityValidator" runat="server" />
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
                        <asp:PlaceHolder ID="phAccountingScopeValidator" runat="server" />
                    </td>
                </tr>
                <!-- Reference -->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblReference" runat="server" Text="Reference:" />
                    </td>
                    <td class="ColContent">
                        <asp:TextBox ID="txtReference" MaxLength="8000" runat="server" TextMode="MultiLine"
                            Rows="6"></asp:TextBox>
                    </td>
                    <td class="ColValidator">
                        &nbsp;
                    </td>
                </tr>

                <!-- Name Measurement-->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblMeasurementName" runat="server" Text="Name:" />
                    </td>
                    <td class="ColContent">
                        <asp:TextBox runat="server" ID="txtMeasurementName" MaxLength="150"></asp:TextBox>
                    </td>
                    <td class="ColValidator">
                        <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="Required Field"
                            Enabled="false" ID="rfvMeasurementName" ControlToValidate="txtMeasurementName"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <!-- Description Measurement-->
                <tr>
                    <td class="ColTitle">
                        <asp:Label ID="lblMeasurementDescription" runat="server" Text="Description:" />
                    </td>
                    <td class="ColContent">
                        <asp:TextBox ID="txtMeasurementDescription" MaxLength="8000" runat="server" TextMode="MultiLine"
                            Rows="6"></asp:TextBox>
                    </td>
                    <td class="ColValidator">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </rad:RadPageView>
        <rad:RadPageView ID="rpvTaskOperators" runat="server" Selected="false">
            <table id="tblContentForm4" runat="server" class="ContentForm">
                <colgroup>
                    <col class="ColTitle" />
                    <col class="ColContent" />
                    <col class="ColValidator" />
                </colgroup>
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
            </table>
        </rad:RadPageView>
        <rad:RadPageView ID="rpvTaskNotification" runat="server" Selected="false">
            <table id="tblContentForm5" runat="server" class="ContentForm">
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
        </rad:RadPageView>
    </rad:RadMultiPage>
    <!--Variables escondidas-->
    <input type="hidden" id="typeExecution" name="typeExecution" />
    <!--Variables escondidas-->
</asp:Content>
