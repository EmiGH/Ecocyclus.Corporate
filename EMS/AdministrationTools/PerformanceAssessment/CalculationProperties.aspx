<%@ Page Language="C#" MasterPageFile="~/EMS.Master" AutoEventWireup="true" CodeBehind="CalculationProperties.aspx.cs"
    Inherits="Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment.CalculationProperties"
    Title="Calculation Properties" Culture="auto" UICulture="auto" %>

<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMain" runat="server">
    <table id="tblContentForm" runat="server" class="ContentForm">
        <colgroup>
            <col class="ColTitle" />
            <col class="ColContent" />
            <col class="ColValidator" />
        </colgroup>
        <!-- Formula -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblFormula" runat="server" Text="Formula:" />
            </td>
            <td class="ColContent">
                <asp:PlaceHolder ID="phFormula" runat="server"></asp:PlaceHolder>
            </td>
            <td class="ColValidator">
                <asp:PlaceHolder ID="phFormulaValidator" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdatePanel ID="upFormulasProperties" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="pnlFormulaProperties" runat="server" Visible="False">
                            <table id="tblContentForm2" runat="server" class="ContentForm">
                                <colgroup>
                                    <col class="ColTitle" />
                                    <col class="ColContent" />
                                    <col class="ColValidator" />
                                </colgroup>
                                <!-- Literal Formula -->
                                <tr>
                                    <td class="ColTitle">
                                        <asp:Label ID="lblFormulaLiteral" runat="server" Text="Literal Formula:" />
                                    </td>
                                    <td class="ColContent">
                                        <asp:Label ID="lblFormulaLiteralValue" runat="server" />
                                    </td>
                                    <td class="ColValidator">
                                        &nbsp;
                                    </td>
                                </tr>
                                <!-- Measurement Unit -->
                                <tr>
                                    <td class="ColTitle">
                                        <asp:Label ID="lblFormulaMU" runat="server" Text="Measurement Unit:" />
                                    </td>
                                    <td class="ColContent">
                                        <asp:Label ID="lblFormulaMUValue" runat="server" />
                                    </td>
                                    <td class="ColValidator">
                                        &nbsp;
                                    </td>
                                </tr>
                                <!-- Indicator -->
                                <tr>
                                    <td class="ColTitle">
                                        <asp:Label ID="lblFormulaIndicator" runat="server" Text="Indicator:" />
                                    </td>
                                    <td class="ColContent">
                                        <asp:Label ID="lblFormulaIndicatorValue" runat="server" />
                                    </td>
                                    <td class="ColValidator">
                                        &nbsp;
                                    </td>
                                </tr>
                                <!-- SP Schema -->
                                <tr>
                                    <td class="ColTitle">
                                        <asp:Label ID="lblFormulaSp" runat="server" Text="SP Schema:" />
                                    </td>
                                    <td class="ColContent">
                                        <asp:Label ID="lblFormulaSpValue" runat="server" />
                                    </td>
                                    <td class="ColValidator">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <!-- Name Calculation -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="blName" runat="server" Text="Name:" />
            </td>
            <td class="ColContent">
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </td>
            <td class="ColValidator">
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfv1"
                    ControlToValidate="txtName" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <!-- Description -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblDescription" runat="server" Text="Description:" />
            </td>
            <td class="ColContent">
                <asp:TextBox ID="txtDescription" MaxLength="8000" Rows="6" TextMode="MultiLine" runat="server"></asp:TextBox>
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
        <!-- Frequency -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblFrequency" runat="server" Text="Frequency:" />
            </td>
            <td class="ColContent">
                <asp:TextBox ID="txtFrequency" runat="server"></asp:TextBox>
            </td>
            <td class="ColValidator">
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Required Field" ID="rfv3"
                    ControlToValidate="txtFrequency" SkinID="EMS" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cv1" runat="server" ErrorMessage="Data Error" ControlToValidate="txtFrequency"
                    Display="Dynamic" SkinID="EMS" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
            </td>
        </tr>
        <!-- Time Unit -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblTimeUnitFrequency" runat="server" Text="Time Unit:" />
            </td>
            <td class="ColContent">
                <asp:PlaceHolder ID="phTimeUnitFrequency" runat="server"></asp:PlaceHolder>
            </td>
            <td class="ColValidator">
                <asp:PlaceHolder ID="phTimeUnitFrequencyValidator" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <!-- Is Relevant -->
        <tr>
            <td class="ColTitle">
                <asp:Label ID="lblIsRelevant" runat="server" Text="Is Relevant:" />
            </td>
            <td class="ColContent">
                <asp:CheckBox ID="chkIsRelevant" runat="server" CssClass="Check" Checked="True" />
            </td>
            <td class="ColValidator">
                &nbsp;
            </td>
        </tr>
        <!-- Grilla Seleccion Proyecto -->
        <tr>
            <td class="ColContentGrid" colspan="3">
                <asp:Label ID="lblTitleProcess" CssClass="Title" runat="server" Text="Processes"></asp:Label>
                <asp:UpdatePanel ID="up_MainData" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <rad:RadGrid ID="rgdMasterGrid" runat="server" Skin="EMS" EnableEmbeddedSkins="false"
                            AllowPaging="True" Width="100%" AutoGenerateColumns="False" GridLines="None"
                            EnableAJAXLoadingTemplate="True" LoadingTemplateTransparency="25">
                            <PagerStyle AlwaysVisible="True" Mode="NextPrevAndNumeric" />
                            <MasterTableView Width="100%" Name="gridMaster2" CellPadding="0" GridLines="None">
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
                                    <%--columna IdProcess--%>
                                    <rad:GridBoundColumn HeaderText="IdProcess" DataField="IdProcess" Visible="false"
                                        UniqueName="IdProcess">
                                    </rad:GridBoundColumn>
                                    <%--columna Name--%>
                                    <rad:GridBoundColumn HeaderText="Project" DataField="Name" UniqueName="Name">
                                    </rad:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="True" />
                        </rad:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <!-- Grilla Seleccion Measurement -->
        <tr>
            <td class="ColContentGrid" colspan="3">
                <asp:Label ID="lblTitleParams" CssClass="Title" runat="server" Text="Parameters"></asp:Label>
                <!-- Grilla de Paramteros -->
                <div class="ContentFormulaGrid">
                    <asp:UpdatePanel ID="up_ParamsGrid" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlFormulaParams" runat="server">
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div id="popUpMeasurement" class="Popup">
                        <asp:UpdatePanel ID="up_popUpMeasurement" runat="server" UpdateMode="Conditional">
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
                                                        <asp:Label ID="lblPopUpProcessClassification" runat="server" Text="Process Classification:"></asp:Label>
                                                    </td>
                                                    <td class='ColContent'>
                                                        <asp:DropDownList ID="aspDdlPopUpProcessClassification" runat="server" AutoPostBack="True" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='ColTitle'>
                                                        <asp:Label ID="lblPopUpProject" runat="server" Text="Project:"></asp:Label>
                                                    </td>
                                                    <td class='ColContent'>
                                                        <asp:DropDownList ID="aspDdlPopUpProject" runat="server" AutoPostBack="True" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class='ColTitle'>
                                                        <asp:Label ID="lblPopUpMeasurement" runat="server" Text="Measurement:" />
                                                    </td>
                                                    <td class='ColContent'>
                                                        <asp:DropDownList ID="aspDdlPopUpMeasurement" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td class="ContentNavigator">
                                            <input id="cancelPopUpMU" class="Cancel" type="button" onclick="CancelPopUp('popUpMeasurement', event);" />
                                            <input id="okPopUpMU" class="Ok" type="button" onclick="OkPopUp('popUpMeasurement');" />
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
    <asp:HiddenField ID="hdn_ParamIndicatorId" runat="server" Value="-1" />
    <asp:HiddenField ID="hdn_changeParamsGridState" runat="server" Value="false" />
    <!-- End Ajax Vars -->

    <script type="text/javascript">
        //Ajax FW
        var prmLocalClient = Sys.WebForms.PageRequestManager.getInstance();
        prmLocalClient.add_endRequest(LocalEndRequest);

        //La Grilla de Paramtros, al ser ClientSide, no esta en un Bloque UP.
        //Esta Funcion deshabilita x JS la "editabilidad" de la grilla "extendiendo" el Ajax FW

        //TODO: Mover este comment de Seguridad a la documentacion Interna
        //Seguridad: Como el Metodo "Modify" del Objeto Calculation no recibe como parametro los Params, por mas que haga inyection JS
        //           es imposible que toque los datos.

        function LocalEndRequest(sender, args) {
            if (document.getElementById("ctl00_ContentMain_hdn_changeParamsGridState").value == "true")
                SetParamsGridEditMode();
        }

        function SetParamsGridEditMode() {
            var editButons = document.getElementsByName("Measurement_EditButton");
            for (var i = 0; i < editButons.length; i++)
                editButons[i].style.display = 'none';
        }
    </script>

    <script type="text/javascript">
        var _labelId;
        var _labelValue;
        function DoPopUp(btn, typeEntity, e) {
            HideAllPopUps();

            //Primero Cancelo el PostBack
            StopEvent(e);     //window.event.returnValue = false;

            //Tomo Posiciones del botonSeleccionado
            var x = GetMousePositionX(e);    //window.event.x;  //- 300;
            var y = GetMousePositionY(e);    //window.event.y;

            //Inicializo el DropDown que voy a abrir
            InitializeDropDown();

            //Inizializo y Muestro el PopUp
            var popup = document.getElementById('popUp' + typeEntity);
            if (popup != null) {
                popup.style.display = "block";
                popup.style.left = String(x - popup.clientWidth) + 'px';
                popup.style.top = String(y - (popup.clientHeight / 2)) + 'px';
            }

            //Para cargar el Combo de la Entidad COncreta, necesito el valor del Id Del Indicador de ese parametro
            //Lo guardo en el campo oculto hdn_ParamIndicatorId, y lo uso x Ajax cuando traigo los Measurements
            var _paramId = document.getElementById('ctl00_ContentMain_hdn_ParamIndicatorId');
            
            var _gridParamId;
            if (_BrowserName == _IEXPLORER) {
                _gridParamId = btn.parentElement.parentElement.childNodes[2];
                _labelId = btn.parentElement.childNodes[0];
                _labelValue = btn.parentElement.childNodes[1];
            }
            else {
                var _gridParamId = btn.parentNode.parentNode.children[2];
                _labelId = btn.parentNode.children[0];
                _labelValue = btn.parentNode.children[1];
            }
            //var _gridParamId = btn.parentElement.parentElement.childNodes[2];
            _paramId.value = _gridParamId.innerHTML;

            //Tengo que llegar a recuperar los labels para insertarle Ids y Text de los items seleccionados del PopUp
//            _labelId = btn.parentElement.childNodes[0];
//            _labelValue = btn.parentElement.childNodes[1];
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

            if (_labelId != null && _labelValue != null) {
                var ddl;
                var _id;
                if (idPopUp == 'popUpMeasurement') {
                    ddl = document.getElementById('ctl00_ContentMain_aspDdlPopUpMeasurement');
                    _id = ddl.options[ddl.selectedIndex].value;
                }

                if (ddl.selectedIndex > 0) {
                    _labelId.value = _id;
                    _labelValue.innerHTML = ddl.options[ddl.selectedIndex].text;
                }
                else {
                    alert('You must select a value');
                    return;
                }
            }

            HidePopUp(popup);
            ClearLabels();
        }

        function HideAllPopUps() {
            HidePopUp(document.getElementById('popUpMeasurement'));
        }

        function HidePopUp(popup) {
            if (popup != null) {
                popup.style.display = "none";
                popup.style.left = "0px";
                popup.style.top = "0px";
            }
        }

        function ClearLabels() {
            _labelId = null;
            _labelValue = null;
        }

        function InitializeDropDown() {
            var ddl;
            var ddlEntity;

            ddl = document.getElementById('ctl00_ContentMain_aspDdlPopUpProcessClassification');
            ddlEntity = document.getElementById('ctl00_ContentMain_aspDdlPopUpProject');
            ddlMainEntity = document.getElementById('ctl00_ContentMain_aspDdlPopUpMeasurement');


            if (ddl != null && ddlEntity != null && ddlMainEntity != null) {
                ddl.selectedIndex = 0;
                ddlEntity.selectedIndex = 0;
                ddlEntity.disabled = true;
                ddlMainEntity.selectedIndex = 0;
                ddlMainEntity.disabled = true
            }
        }
    </script>

</asp:Content>
