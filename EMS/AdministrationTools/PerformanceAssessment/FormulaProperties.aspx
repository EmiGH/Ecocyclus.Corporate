<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="FormulaProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.PA.FormulaProperties" Title="EMS - Formula Property"
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
                    <!-- Id Formula -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblIDFormula" runat="server" Text="Id Formula:" />
                        </td>
                        <td class="ColContent">
                            <asp:Label ID="lblIdFormulaValue" runat="server" />
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Formula Name -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="blName" runat="server" Text="Name:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfv1"
                                ControlToValidate="txtName" SkinID="Validator"></asp:RequiredFieldValidator>
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
                    <!-- Literal Formula -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblLiteralFormula" runat="server" Text="Literal Formula:" />
                        </td>
                        <td class="ColContent">
                            <asp:TextBox ID="txtLiteralFormula" runat="server"></asp:TextBox>
                        </td>
                        <td class="ColValidator">
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfv2"
                                ControlToValidate="txtLiteralFormula" SkinID="Validator"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <!-- SP Schema -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblSPSchema" runat="server" Text="SP Schema:" />
                        </td>
                        <td class="ColContent">
                            <rad:RadComboBox ID="ddlSPSchema" runat="server" AutoPostBack="True" Width="343px"
                                Height="343px" SkinsPath="~/RadControls/ComboBox/Skins">
                            </rad:RadComboBox>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Indicator -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblClassification" runat="server" Text="Indicator:"></asp:Label>
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
                    <!-- Resource Catalog -->
                    <tr>
                        <td class="ColTitle">
                            <asp:Label ID="lblResourceCatalog" runat="server" Text="Picture:" />
                        </td>
                        <td class="ColContent">
                            <asp:PlaceHolder ID="phResourceCatalog" runat="server"></asp:PlaceHolder>
                        </td>
                        <td class="ColValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <!-- Grilla Parametros -->
                </table>
                <asp:Label ID="lblTitleFormulaParams" CssClass="Title" runat="server" Text="Parameters"></asp:Label>
                <div class="ContentFormulaGrid">
                    <asp:UpdatePanel ID="up_paramsGrid" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <!-- Grilla de Paramteros -->
                            <!-- Contenedor de Grilla de Paramteros Dinamica -->
                            <asp:Panel ID="pnlFormulaParams" runat="server">
                            </asp:Panel>
                            <!-- END - Grilla de Paramteros -->
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlSPSchema" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <!-- Los PopUps de Indicator y Measurement de Parameter Grid -->
                    <div id="popUpParams" class="Popup">
                        <asp:UpdatePanel ID="up_popUpMeasurementUnit" runat="server" RenderMode="Inline"
                            UpdateMode="Conditional">
                            <ContentTemplate>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table class='Form' runat='server' id='tblContentFormPopup'>
                                                <colgroup>
                                                    <col class='ColTitle' />
                                                    <col class='ColContent' />
                                                </colgroup>
                                                <tr>
                                                    <td class='ColTitle'>
                                                        <asp:Label ID="lblPopUpIndicatorClassification" runat="server" Text="Classification:"></asp:Label>
                                                    </td>
                                                    <td class='ColContent'>
                                                        <asp:DropDownList ID="aspDdlPopUpIndicatorClassification" runat="server" AutoPostBack="True" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='ColTitle'>
                                                        <asp:Label ID="lblPopUpIndicator" runat="server" Text="Indicator:" />
                                                    </td>
                                                    <td class='ColContent'>
                                                        <asp:DropDownList ID="aspDdlPopUpIndicator" runat="server" AutoPostBack="True" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='ColTitle'>
                                                        <asp:Label ID="lblPopUpMeasurementUnit" runat="server" Text="Measurement Unit:" />
                                                    </td>
                                                    <td class='ColContent'>
                                                        <asp:DropDownList ID="aspDdlPopUpMeasurementUnit" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="ContentNavigator">
                                            <input id="cancelPopUpMU" type="button" class="Cancel" onclick="CancelPopUp('popUpParams', event);" />
                                            <input id="okPopUpMU" type="button" class="Ok" onclick="OkPopUp('popUpParams');" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <!-- Ajax Vars -->
    <asp:HiddenField ID="hdn_changeParamsGridState" runat="server" Value="false" />
    <input id="msgSelectIndicatorAndMeasurementUnit" type="hidden" value="<% =_msgSelectIndicatorAndMeasurementUnit%>" />
    <!-- End Ajax Vars -->

    <script type="text/javascript">
        //Ajax FW
        var prmLocalClient = Sys.WebForms.PageRequestManager.getInstance();

        prmLocalClient.add_endRequest(LocalEndRequest);


        //La Grilla de Paramtros, al ser ClientSide, no esta en un Bloque UP.
        //Esta Funcion deshabilita x JS la "editabilidad" de la grilla "extendiendo" el Ajax FW

        function LocalEndRequest(sender, args) {
            if (document.getElementById("ctl00_ContentMain_hdn_changeParamsGridState").value == "true")
                SetParamsGridEditMode();
        }

        function SetParamsGridEditMode() {
            var indicatorEditButons = document.getElementsByName("Indicator_EditButton");
            for (var i = 0; i < indicatorEditButons.length; i++)
                indicatorEditButons[i].style.display = 'none';

            var measurementUnitEditButons = document.getElementsByName("MeasurementUnit_EditButton");
            for (var i = 0; i < measurementUnitEditButons.length; i++)
                measurementUnitEditButons[i].style.display = 'none';
        }
    </script>

    <script type="text/javascript">
        var _labelIndicatorId;
        var _labelIndicatorValue;
        var _labelMeasurementUnitId;
        var _labelMeasurementUnitValue;

        function DoPopUp(btn, typeEntity, e) {
            HideAllPopUps();

            //Primero Cancelo el PostBack
            StopEvent(e);     //window.event.returnValue = false;

            //Tomo Posiciones del botonSeleccionado
            var x = GetMousePositionX(e);    //window.event.x;
            var y = GetMousePositionY(e);    //window.event.y;

            //Inicializo el DropDown que voy a abrir
            InitializeDropDown();

            //Inizializo y Muestro el PopUp
            var popup = document.getElementById('popUpParams');
            if (popup != null) {
                popup.style.display = "block";
                popup.style.left = String(x - popup.clientWidth) + 'px';
                popup.style.top = String(y - (popup.clientHeight / 2)) + 'px';
            }
            //Tengo que llegar a recuperar los labels para insertarle Ids y Text de los items seleccionados del PopUp
            if (_BrowserName == _IEXPLORER) {
                _labelIndicatorId = btn.parentElement.parentElement.childNodes[4].childNodes[0];
                _labelIndicatorValue = btn.parentElement.parentElement.childNodes[4].childNodes[1];
                _labelMeasurementUnitId = btn.parentElement.parentElement.childNodes[5].childNodes[0];
                _labelMeasurementUnitValue = btn.parentElement.parentElement.childNodes[5].childNodes[1];
            }
            else {

                _labelIndicatorId = btn.parentNode.parentNode.children[4].children[0];
                _labelIndicatorValue = btn.parentNode.parentNode.children[4].children[1];
                _labelMeasurementUnitId = btn.parentNode.parentNode.children[5].children[0];
                _labelMeasurementUnitValue = btn.parentNode.parentNode.children[5].children[1];

            }
            //Tengo que llegar a recuperar los labels para insertarle Ids y Text de los items seleccionados del PopUp
//            _labelIndicatorId = _element.childNodes[4].childNodes[0];
//            _labelIndicatorValue = _element.childNodes[4].childNodes[1];
//            _labelMeasurementUnitId = _element.childNodes[5].childNodes[0];
//            _labelMeasurementUnitValue = _element.childNodes[5].childNodes[1];
        }

        function CancelPopUp(idPopUp, e) {
            StopEvent(e);     //window.event.returnValue = false;

            //Escondo PopUp
            var popup = document.getElementById(idPopUp);
            HidePopUp(popup);

            //Limpio los SelectedValues
            ClearLabels();
        }

        function OkPopUp(idPopUp) {
            var popup = document.getElementById(idPopUp);
            var _msgSelectIndicatorAndMeasurementUnit = document.getElementById('msgSelectIndicatorAndMeasurementUnit').value;

            if (_labelIndicatorId != null && _labelIndicatorValue != null && _labelMeasurementUnitId != null && _labelMeasurementUnitValue != null) {
                var _ddlIndicator;
                var _idIndicator;
                var _ddlMeasurementUnit;
                var _idMeasurementUnit;

                if (idPopUp == 'popUpParams') {
                    _ddlIndicator = document.getElementById('ctl00_ContentMain_aspDdlPopUpIndicator');
                    _idIndicator = _ddlIndicator.options[_ddlIndicator.selectedIndex].value;

                    _ddlMeasurementUnit = document.getElementById('ctl00_ContentMain_aspDdlPopUpMeasurementUnit');
                    _idMeasurementUnit = _ddlMeasurementUnit.options[_ddlMeasurementUnit.selectedIndex].value;
                }

                if (_ddlIndicator.selectedIndex > 0 && _ddlMeasurementUnit.selectedIndex > 0) {
                    _labelIndicatorId.value = _idIndicator;
                    _labelIndicatorValue.innerHTML = _ddlIndicator.options[_ddlIndicator.selectedIndex].text;

                    _labelMeasurementUnitId.value = _idMeasurementUnit;
                    _labelMeasurementUnitValue.innerHTML = _ddlMeasurementUnit.options[_ddlMeasurementUnit.selectedIndex].text;
                }
                else {
                    alert(_msgSelectIndicatorAndMeasurementUnit);
                    //alert('You must select an Indicator and a Measurement Unit');
                    return;
                }
            }

            HidePopUp(popup);
            ClearLabels();
        }

        function HideAllPopUps() {
            HidePopUp(document.getElementById('popUpParams'));
        }

        function HidePopUp(popup) {
            if (popup != null) {
                popup.style.display = "none";
                popup.style.left = "0px";
                popup.style.top = "0px";
            }
        }

        function ClearLabels() {
            _labelIndicatorId = null;
            _labelIndicatorValue = null;
            _labelMeasurementUnitId = null;
            _labelMeasurementUnitValue = null;
        }

        function InitializeDropDown() {
            var ddlClass;
            var ddlIndicator;
            var ddlMeasurementUnit;

            ddlClass = document.getElementById('ctl00_ContentMain_aspDdlPopUpIndicatorClassification');
            ddlIndicator = document.getElementById('ctl00_ContentMain_aspDdlPopUpIndicator');
            ddlMeasurementUnit = document.getElementById('ctl00_ContentMain_aspDdlPopUpMeasurementUnit');

            if (ddlClass != null && ddlIndicator != null && ddlMeasurementUnit != null) {
                ddlClass.selectedIndex = 0;

                ddlIndicator.selectedIndex = 0;
                ddlIndicator.disabled = true

                ddlMeasurementUnit.selectedIndex = 0;
                ddlMeasurementUnit.disabled = true
            }
        }
    
    </script>

</asp:Content>
