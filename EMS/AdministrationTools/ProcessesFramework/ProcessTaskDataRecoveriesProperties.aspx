<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeBehind="ProcessTaskDataRecoveriesProperties.aspx.cs" Inherits="Condesus.EMS.WebUI.PM.ProcessTaskDataRecoveriesProperties"
    Title="EMS - Process Task Data Recoveries Properties" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>
    
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">

<div class="contentmenu">
            <div class="contentmenudivBackground">
                <div class="contentmenudivPosition">
                    <!--Opciones Generales -->
                    <asp:UpdatePanel ID="upMenuOption" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <rad:RadMenu ID="rmnOption" CausesValidation="False" runat="server" OnClientItemClicked="rmnOption_OnClientItemClickedHandler"
                                OnItemClick="rmnOption_ItemClick"  
                                meta:resourcekey="rmnOptionResource1">
                                <Items>
                                    <rad:RadMenuItem ID="rmnGeneralOptions" CssClass="GeneralOptions" 
                                        Text="General Options" runat="server" 
                                        meta:resourcekey="rmnGeneralOptionsResource1">
                                    </rad:RadMenuItem>
                                </Items>
                                <ExpandAnimation Type="None"></ExpandAnimation>
                                <CollapseAnimation Type="None"></CollapseAnimation>
                            </rad:RadMenu>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rmnOption" EventName="ItemClick" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnOk" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <!-- Fin Opciones Generales -->
                </div>
            </div>
        </div>
        
    <table cellpadding="0" cellspacing="0" id="content">
        <tr>
            <td>
                <table id="tblContentForm" runat="server" class="ContentForm">
                    <colgroup>
                        <col class="ColTitle" />
                        <col class="ColContent" />
                        <col class="ColValidator" />
                    </colgroup>
                    <!-- Project Title -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblProjectTitle" runat="server" Text="Project Title:" 
                                meta:resourcekey="lblProjectTitleResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblProjectTitleValue" runat="server" 
                                meta:resourcekey="lblProjectTitleValueResource1" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Parent -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblIdParent" runat="server" Text="Parent" 
                                meta:resourcekey="lblIdParentResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblIdParentValue" runat="server" 
                                meta:resourcekey="lblIdParentValueResource1" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Language -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblLanguage" runat="server" Text="Language:" 
                                meta:resourcekey="lblLanguageResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblLanguageValue" runat="server" 
                                meta:resourcekey="lblLanguageValueResource1" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Title -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblTask" runat="server" Text="Title:" 
                                meta:resourcekey="lblTaskResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtTitle" MaxLength="150" SkinID="EMS"
                                meta:resourcekey="txtTitleResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator SkinID="EMS" runat="server" 
                            ErrorMessage="Required Field" Display="Dynamic"
                            ID="rfv1" ControlToValidate="txtTitle" meta:resourcekey="rfv1Resource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Order -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblOrder" runat="server" Text="Order:" 
                                meta:resourcekey="lblOrderResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox runat="server" ID="txtOrder" SkinID="EMS"
                            meta:resourcekey="txtOrderResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="EMS" runat="server" ValidationExpression="[-]?[\d]*"
                                ErrorMessage="Wrong Format" ID="rfvOrder" ControlToValidate="txtOrder" Display="Dynamic"
                                meta:resourcekey="rfvOrderResource1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <!-- Purpose -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblPurpose" runat="server" Text="Purpose:" 
                                meta:resourcekey="lblPurposeResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtPurpose" runat="server" MaxLength="8000"  Rows="6"  
                                TextMode="MultiLine" meta:resourcekey="txtPurposeResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Description -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblDescription" runat="server" Text="Description:" 
                                meta:resourcekey="lblDescriptionResource1" />
                        </td>
                        <td class="ColContent">
                             <asp:TextBox ID="txtDescription" runat="server" MaxLength="8000"  Rows="6" 
                                TextMode="MultiLine" meta:resourcekey="txtDescriptionResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Weight -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblWeight" runat="server" Text="Weight:" 
                                meta:resourcekey="lblWeightResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtWeight" runat="server" SkinID="EMS"
                                meta:resourcekey="txtWeightResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                             <asp:RangeValidator ID="rvWeight" ControlToValidate="txtWeight" 
                             MinimumValue="0" Display="Dynamic"
                             MaximumValue="100" Type="Integer" runat="server" ErrorMessage="Wrong Format"
                             SkinID="EMS" meta:resourcekey="rvWeightResource1"></asp:RangeValidator>
                             <asp:RequiredFieldValidator SkinID="EMS" runat="server" ErrorMessage="Required Field"
                             ID="rfvWeight" ControlToValidate="txtWeight" Display="Dynamic"
                             meta:resourcekey="rfvWeightResource1"></asp:RequiredFieldValidator>
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
                    <!-- rblOptionTypeExecution -->
                    <tr>
                        <td class="ColTitle">
                             
                        </td>
                        <td class="ColContent">
                            <asp:RadioButtonList ID="rblOptionTypeExecution" runat="server" SkinID="EMS"
                                meta:resourcekey="rblOptionTypeExecutionResource1">
                                <asp:ListItem Text="Spontaneous" Enabled="False" 
                                    meta:resourcekey="ListItemResource1"></asp:ListItem>
                                <asp:ListItem Text="Recurrent" Enabled="False" 
                                    meta:resourcekey="ListItemResource2"></asp:ListItem>
                                <asp:ListItem Text="Scheduled" Selected="True" 
                                    meta:resourcekey="ListItemResource3"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Start Date -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblStartDate" runat="server" Text="Start Date:" 
                                meta:resourcekey="lblStartDateResource1" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDateTimePicker ID="rdtStartDate" Skin="EMS" runat="server" 
                                Culture="English (United States)" meta:resourcekey="rdtStartDateResource1">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput DateFormat="MM/dd/yyyy HH:mm:ss"  />
                                <Calendar >
                                </Calendar>
                                <TimePopupButton ToolTip="<%$ Resources:Common, dtTimePickerTooltip %>" />
                                <TimeView TimeFormat="HH:mm:ss" Culture="English (United States)" >
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
                            <asp:Label ID="lblEndDate" runat="server" Text="End Date:" 
                                meta:resourcekey="lblEndDateResource1" /> 
                        </td>
                        <td class="ColContent">
                            <rad:RadDateTimePicker ID="rdtEndDate" Skin="EMS" runat="server" 
                                Culture="English (United States)" meta:resourcekey="rdtEndDateResource1">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput DateFormat="MM/dd/yyyy HH:mm:ss" />
                                <Calendar >
                                </Calendar>
                                <TimePopupButton ToolTip="<%$ Resources:Common, dtTimePickerTooltip %>" />
                                <TimeView TimeFormat="HH:mm:ss" Culture="English (United States)" >
                                </TimeView>
                            </rad:RadDateTimePicker>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Duration -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblDuration" runat="server" Text="Duration:" 
                              meta:resourcekey="lblDurationResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox SkinID="EMS" ID="txtDuration" runat="server" 
                            meta:resourcekey="txtDurationResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RegularExpressionValidator SkinID="EMS" runat="server" Display="Dynamic"
                            ValidationExpression="[1-9][\d]*" ErrorMessage="Wrong Format" ID="revDuration"
                            ControlToValidate="txtDuration" meta:resourcekey="revDurationResource1"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator SkinID="EMS" runat="server" Display="Dynamic" ErrorMessage="Required Field"
                            ID="rfvDuration" ControlToValidate="txtDuration" 
                            meta:resourcekey="rfvDurationResource1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- Time Unit -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblTimeUnitDuration" runat="server" Text="Time Unit:" 
                             meta:resourcekey="lblTimeUnitDurationResource1" />
                        </td>
                        <td class="ColContent">
                            <rad:RadComboBox ID="ddlTimeUnitDuration" runat="server" Skin="EMS"
                            meta:resourcekey="ddlTimeUnitDurationResource1">
                            </rad:RadComboBox>
                        </td>
                        <td class="ColValidator">
                            <asp:CustomValidator ID="cvComboTimeUnitDuration" runat="server" ControlToValidate="ddlTimeUnitDuration"
                            SkinID="EMS" ErrorMessage="Selection Required" Display="Dynamic"
                            meta:resourcekey="cvComboTimeUnitDurationResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                    <!-- Interval -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblInterval" runat="server" Text="Interval:" 
                             meta:resourcekey="lblIntervalResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtInterval" runat="server" SkinID="EMS"
                            meta:resourcekey="txtIntervalResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Time Unit -->
                    <tr>
                        <td class="ColTitle">
                              <asp:Label ID="lblTimeUnitInterval" runat="server" Text="Time Unit:" 
                              meta:resourcekey="lblTimeUnitIntervalResource1" />
                        </td>
                        <td class="ColContent">
                            <rad:RadComboBox ID="ddlTimeUnitInterval" runat="server" 
                             Skin="EMS" meta:resourcekey="ddlTimeUnitIntervalResource1" >
                             </rad:RadComboBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Max Number Executions -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblMaxNumberExecutions" runat="server" 
                             Text="Max Number Executions:" 
                             meta:resourcekey="lblMaxNumberExecutionsResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtMaxNumberExecutions" runat="server" 
                                ToolTip="Use 0 (zero) for Infinite" SkinID="EMS"
                                meta:resourcekey="txtMaxNumberExecutionsResource1"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Result -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblResult" runat="server" Text="Result:" 
                                meta:resourcekey="lblResultResource1" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblResultValue" runat="server" 
                                meta:resourcekey="lblResultValueResource1" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Completed Percentage -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblCompleted" runat="server" Text="Completed Percentage:" 
                                meta:resourcekey="lblCompletedResource1" />
                        </td>
                        <td class="ColContent">
                             <div style="float: left; margin: 5px 6px 0px 6px;">
                                <ajaxToolkit:SliderExtender ID="SliderExtender3" runat="server" BehaviorID="Slider3_BoundControl"
                                    TargetControlID="Slider3_BoundControl" BoundControlID="txtCompleted"
                                    EnableHandleAnimation="True" 
                                    TooltipText="Slider: value {0}. Please slide to change value." Enabled="True" />
                                <div style="float: left">
                                    <asp:TextBox ID="txtCompleted" CssClass="inputSlider" Width="30px" 
                                        runat="server" meta:resourcekey="txtCompletedResource1"></asp:TextBox>
                                </div>
                                <div style="float: left">
                                    <asp:TextBox ID="Slider3_BoundControl" CssClass="inputSlider" Width="30px" 
                                        runat="server" meta:resourcekey="Slider3_BoundControlResource1" />
                                </div>
                            </div>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Measurement -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblMeasurement" runat="server" Text="Measurement:" 
                                meta:resourcekey="lblMeasurementResource1" />
                        </td>
                        <td class="ColContent">
                            <rad:RadComboBox AutoPostBack="True" ID="ddlMeasurement" runat="server" 
                             Skin="EMS" meta:resourcekey="ddlMeasurementResource1" >
                            </rad:RadComboBox>
                        </td>
                        <td class="ColValidator">
                            <asp:CustomValidator ID="customvMeasurement" runat="server" ControlToValidate="ddlMeasurement"
                                SkinID="EMS" ErrorMessage="Selection Required" Display="Dynamic"
                                meta:resourcekey="customvMeasurementResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                    <!-- From -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblMeasurementsFromDate" runat="server" Text="From:" 
                                meta:resourcekey="lblMeasurementsFromDateResource1" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDatePicker ID="rdtMeasurementsFromDate" Skin="EMS" 
                                runat="server" Culture="English (United States)" 
                                meta:resourcekey="rdtMeasurementsFromDateResource1">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput DateFormat="MM/dd/yyyy" />
                                <Calendar >
                                </Calendar>
                            </rad:RadDatePicker>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- To -->
                    <tr>
                        <td class="ColTitle">
                             <asp:Label ID="lblMeasurementsToDate" runat="server" Text="To:" 
                                meta:resourcekey="lblMeasurementsToDateResource1" />
                        </td>
                        <td class="ColContent">
                            <rad:RadDatePicker ID="rdtMeasurementsToDate" Skin="EMS" 
                                runat="server" Culture="English (United States)" 
                                meta:resourcekey="rdtMeasurementsToDateResource1">
                                <DatePopupButton ToolTip="<%$ Resources:Common, dtDatePickerTooltip %>" />
                                <DateInput DateFormat="MM/dd/yyyy" />
                                <Calendar >
                                </Calendar>
                            </rad:RadDatePicker>
                        </td>
                        <td class="ColValidator">
                            <asp:CustomValidator ID="custumvMeasurementEndDate" runat="server" ControlToValidate="rdtMeasurementsToDate"
                                SkinID="EMS" Display="Dynamic"
                                ErrorMessage="The second date must be after the first one" 
                                meta:resourcekey="custumvMeasurementEndDateResource1"></asp:CustomValidator>
                        </td>
                    </tr>
                </table>
             </td>
         </tr>
    </table> 
        
        <!--Abro ContentTabla-->
        <asp:UpdatePanel ID="upProperties" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="formdivContainerBotton">
                    <asp:Button ID="btnFilter" runat="server" Text="Filter" 
                        meta:resourcekey="btnFilterResource1" />
                </div>
                <br />
                <br />
                <br />
                <br />
                <rad:RadGrid ID="rgdMasterGrid" runat="server" AllowPaging="True"
                     Width="100%" AutoGenerateColumns="False" GridLines="None" 
                    EnableAJAXLoadingTemplate="True" LoadingTemplateTransparency="25" 
                    meta:resourcekey="rgdMasterGridResource1">
                    <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
                    <MasterTableView Width="100%" Name="gridMaster" CellPadding="0" 
                        GridLines="None">
                        <RowIndicatorColumn Visible="False">
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Resizable="False" Visible="False">
                            <HeaderStyle Width="20px" />
                        </ExpandCollapseColumn>
                        <Columns>
                            <rad:GridBoundColumn HeaderText="IdRow" Visible="false" DataField="IdRow" UniqueName="IdRow">
                            </rad:GridBoundColumn>
                            <%--columna chekbox--%>
                            <rad:GridTemplateColumn UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <a onmouseover='this.style.cursor = "hand"'>
                                        <asp:CheckBox ToolTip="Check" ID="chkSelect" runat="server" CausesValidation="false">
                                        </asp:CheckBox>
                                    </a>
                                </ItemTemplate>
                                <HeaderStyle Width="21px" />
                            </rad:GridTemplateColumn>
                            <%--columna Date--%>
                            <rad:GridBoundColumn HeaderText="Date" DataField="Date" UniqueName="Date" DataType="System.DateTime">
                            </rad:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="True" />
                </rad:RadGrid>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnOk" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="rmnOption" EventName="ItemClick" />
            </Triggers>
        </asp:UpdatePanel>
        <!--Cierro ContentTabla-->
        <asp:UpdatePanel ID="upButtonSave" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="formdivContainerBotton">
                    <asp:Button runat="server" Text="<%$ Resources:Common, btnSave %>" ID="btnSave" 
                        OnClick="btnSave_Click" meta:resourcekey="btnSaveResource1" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnOk" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="rmnOption" EventName="ItemClick" />
            </Triggers>
        </asp:UpdatePanel>
       
    <!--Variables escondidas-->
    <asp:Button ID="btnTransferAdd" runat="server" OnClick="btnTransferAdd_Click" 
        Style="display: none" meta:resourcekey="btnTransferAddResource1" />
    <input type="hidden" id="radMenuClickedId" name="radMenuClickedId" />
    <asp:Button ID="btnHidden" runat="server" Visible="False" 
        CausesValidation="False" meta:resourcekey="btnHiddenResource1" />
    <input type="hidden" id="radMenuItemClicked" name="radMenuItemClicked" />
    <input type="hidden" id="typeExecution" name="typeExecution" />
    <!--Variables escondidas-->
    
    <%--Cierra menu opciones generales--%>
    <asp:Panel ID="pnlDelete" runat="server" meta:resourcekey="pnlDeleteResource1">
        <ajaxToolkit:ConfirmButtonExtender ID="cbelbDelete" runat="server" TargetControlID="btnHidden"
            OnClientCancel="cancelClick" DisplayModalPopupID="mpelbDelete" 
            Enabled="False" ConfirmText="" />
        <ajaxToolkit:ModalPopupExtender ID="mpelbDelete" runat="server" TargetControlID="btnHidden"
            BehaviorID="programmaticModalPopupBehavior" PopupControlID="pnlConfirmDelete"
            CancelControlID="btnCancel" BackgroundCssClass="ModalPopUp" 
            DynamicServicePath="" Enabled="True" />
        <div class="contentpopup">
            <asp:Panel ID="pnlConfirmDelete" runat="server" Style="display: none" 
                meta:resourcekey="pnlConfirmDeleteResource1">
                <span>
                    <asp:Literal ID="liMsgConfirmDelete" runat="server" 
                    Text="<%$ Resources:Common, msgConfirmDelete %>" /></span>
                <asp:Button ID="btnOk" CssClass="contentformBotton" runat="server" Text="<%$ Resources:Common, btnOk %>"
                    CausesValidation="False" OnClick="btnOkDelete_Click" BorderStyle="None" 
                    meta:resourcekey="btnOkResource1" />
                <asp:Button ID="btnCancel" CssClass="contentformBotton" runat="server" Text="<%$ Resources:Common, btnCancel %>"
                    BorderStyle="None" meta:resourcekey="btnCancelResource1" />
            </asp:Panel>
        </div>
    </asp:Panel>

    <script type="text/javascript">
    //Inicializacion de Variables PageRequestManager
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    var postBackElement;

    prm.add_initializeRequest(InitializeRequest);

    //Handlers          
    function InitializeRequest(sender, args)
    { 
        postBackElement = args.get_postBackElement();
        //Si es un LinkButton Attacheado a la Grilla (sino puede ser cualquier evento de la Grilla (paginador etc)
        if (document.getElementById("radMenuItemClicked").value== "Language")
        {
            args.set_cancel(true);
            //Dispara el OnClick del btnHidden emulando un post back normal por sobre el async que dispara el Boton de la Grilla
            DoNormalPostBack(document.getElementById("<%= btnTransferAdd.ClientID %>"));
        }
    }

    //El Handler que Cancela el PostBack "Asyncronic" del UpdatePanel, termina haciendo un PostBack Normal
    function DoNormalPostBack(btnPostBack)
    {
        btnPostBack.click();
    }

    //******************************************************
    function RadioButtonChange(rbListTypeExec) 
    {
        var _maxNumberExecutions = document.getElementById("<%= txtMaxNumberExecutions.ClientID %>");
        var _interval = document.getElementById("<%= txtInterval.ClientID %>");
        var _timeUnitInterval = document.getElementById("<%= ddlTimeUnitInterval.ClientID %>");
        var _endDate = document.getElementById("<%= rdtEndDate.ClientID %>");

//Cuando es scheduler, hay que bloquear el calendario de fecha fin...

        switch (rbListTypeExec.id)
        {
            case "ctl00_ContentMain_rblOptionTypeExecution_0": //Spontaneous
                _interval.readOnly = true;
                _interval.value = "0";
                _timeUnitInterval.disabled = true;
                _endDate.disabled = false;
//                _inputEndTime.readOnly = false;
//                _inputEndDate.readOnly = false;
                _maxNumberExecutions.readOnly = false;
                _maxNumberExecutions.value = "0";
                document.getElementById("typeExecution").value = "Spontaneous";
                break;

            case "ctl00_ContentMain_rblOptionTypeExecution_1": //Repeatability
                _maxNumberExecutions.readOnly = true;
                _interval.readOnly = false;
                _timeUnitInterval.disabled = false;
                _endDate.disabled = false;
//                _inputEndTime.readOnly = false;
//                _inputEndDate.readOnly = false;
                _maxNumberExecutions.value = "0";
                document.getElementById("typeExecution").value = "Repeatability";
                break;

            case "ctl00_ContentMain_rblOptionTypeExecution_2": //Scheduler
                _maxNumberExecutions.readOnly = true;
                _maxNumberExecutions.value = "1";
                _interval.value = "0";
                _interval.readOnly = true;
                _timeUnitInterval.disabled = true;
                _endDate.disabled = true;
//                _inputEndTime.readOnly = true;
//                _inputEndDate.readOnly = true;
                document.getElementById("typeExecution").value = "Scheduler";
                break;
        } 
    }
         
    /////////////////////////////////////////////////////////////////////
    ///
    ///     Funciones para mostrar el popup
    ///
    /////////////////////////////////////////////////////////////////////

    //Esta funcion se ejecuta al hacer click sobre un item del menuRad
    function rmnOption_OnClientItemClickedHandler(sender, eventArgs)
    {
        //cargo las constantes
        var mioptAdd = "m0"; //menu item Add
        var mioptLanguage = "m1"; //menu item Language
        var mioptDelete = "m2";  //menu item Delete

       if (eventArgs.Item.ID=="ctl00_ContentMain_rmnOption_m0")
        {
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            StopEvent(eventArgs);     //window.event.returnValue = false;
        }
        else
        {
            //Se guarda el id del item que fue seleccionado del menu
            var strMenuItemId = eventArgs.Item.ID;

            //Aca se guarda si se ejecuto el menu Option o el Selection
            document.getElementById("radMenuClickedId").value = "Option"

            //solo muestra el popup cuando piden borrar
            if (strMenuItemId.substring(strMenuItemId.lastIndexOf("_") + 1) == mioptDelete)
            {
               //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
                StopEvent(eventArgs);     //window.event.returnValue = false;
               //Busca el popup
               var modalPopupBehavior = $find('programmaticModalPopupBehavior');
               //muestra el popup
               modalPopupBehavior.show();
            }
            else
            {
                //al item que hacer server.transfer, lo identifico.
                if (strMenuItemId.substring(strMenuItemId.lastIndexOf("_") + 1) == mioptLanguage)
                {
                    $get("<%= rmnOption.ClientID %>").style.display = "none";
                    $get("<%= uProgProperties.ClientID %>").style.display = "block";
                    document.getElementById("radMenuItemClicked").value = "Language";
                }
            }
        }
    }
    </script>

</asp:Content>
