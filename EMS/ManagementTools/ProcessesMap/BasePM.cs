using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;

using Telerik.Web.UI;
using EMS_PM = Condesus.EMS.Business.PF;

namespace Condesus.EMS.WebUI.ManagementTools.ProcessesMap
{
    public class BasePM : BasePage
    {
        //Base Process...

        protected EMS_PM.Entities.Process _Process;

        protected String _ActiveTab
        {
            get { return Convert.ToString(ViewState["ActiveTab"]); }
            set { ViewState["ActiveTab"] = value; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);


        }

        #region Methods
        //El Estado del TabStrip.
        //El Enable/Disable del TabStrip solo es funcional cuando es una carga inicial (se encuentra fuera del Async)
        //Cuando es un Async, el estado lo maneja x Js con la variable hidden del UP integrada.
        protected Boolean SetTabStripState(RadTabStrip tabStrip, Button btnMainTabSave, HiddenField hdnMainTabState)
        {
            if (_Process == null)
            {
                if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                    foreach (RadTab _tab in tabStrip.Tabs)
                        _tab.Enabled = false;

                btnMainTabSave.Style.Add("display", "none");
                hdnMainTabState.Value = "disabled";

                return false;
            }
            else
            {
                if (!ScriptManager.GetCurrent(this.Page).IsInAsyncPostBack)
                    foreach (RadTab _tab in tabStrip.Tabs)
                        _tab.Enabled = true;

                btnMainTabSave.Style.Add("display", "block");
                hdnMainTabState.Value = "enabled";

                if (_ActiveTab == "Executions")
                    btnMainTabSave.Style.Add("display", "none");

                return true;
            }
        }

        protected void BuildEmptyTab(Panel tabContainer)
        {
            Label lblEmptyTab = new Label();
            lblEmptyTab.ID = "lblControlsNotImplemented";
            lblEmptyTab.CssClass = "contentdivNoImplemented";
            lblEmptyTab.Text = Resources.ConstantMessage.lblControlsNotImplemented;

            tabContainer.Controls.Add(lblEmptyTab);
        }

        protected void BuildExtendedDataTabContent(Panel pnlTabContainer)
        {

            #region Menu de Seleccion
            //RadMenuItem ItemExtendedPropView = new RadMenuItem(Resources.Common.mnuView);
            //ItemExtendedPropView.ID = "RadMenuItem1";
            ////Common.Functions.DoRadItemSecurity(ItemExtendedPropView, true);
            //rmnSelectionPMExtendedProps.Items.Add(ItemExtendedPropView);

            //RadMenuItem ItemExtendedPropEdit = new RadMenuItem(Resources.Common.mnuEdit);
            //ItemExtendedPropEdit.ID = "RadMenuItem2";
            ////Common.Functions.DoRadItemSecurity(ItemExecutionEdit, true);
            //rmnSelectionPMExtendedProps.Items.Add(ItemExtendedPropEdit);
            #endregion

            #region DropDownExtendedPropsClassification

            pnlTabContainer.Controls.Add(new LiteralControl("<div class='contentlist'>"));
            pnlTabContainer.Controls.Add(new LiteralControl("<table><tr><td>"));

            Label lblExtProp = new Label();
            lblExtProp.Width = Unit.Pixel(190);
            lblExtProp.Text = "Extended Property Classification:";
            lblExtProp.ID = "lblExtendedPropertyClassification";
            pnlTabContainer.Controls.Add(lblExtProp);


            //RadComboBox ddlExtendedPropertyClassification = new RadComboBox();
            DropDownList ddlExtendedPropertyClassification = new DropDownList();
            ddlExtendedPropertyClassification.ID = "DropDownEPClasification";
            ddlExtendedPropertyClassification.AutoPostBack = false;
            ddlExtendedPropertyClassification.Attributes.Add("onchange", "CheckSaveStatus()");
            //ddlExtendedPropertyClassification.Skin = "EMS";

            //Cuando se inyecte el js...
            //JsDdlExtendedPropertyClassification(ddlExtendedPropertyClassification.ClientID);

            //ddlExtendedPropertyClassification.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlExtendedPropertyClassification_SelectedIndexChanged);
            //LoadExtendedPropertyClassifications(ref ddlExtendedPropertyClassification);
            //ddlExtendedPropertyClassification.SelectedValue = _IdExtendedPropertyClassification;
            ddlExtendedPropertyClassification.SelectedIndexChanged += new EventHandler(ddlExtendedPropertyClassification_SelectedIndexChanged);
            LoadComboExtendedPropertyClassification(ddlExtendedPropertyClassification);

            pnlTabContainer.Controls.Add(ddlExtendedPropertyClassification);

            pnlTabContainer.Controls.Add(new LiteralControl("</td></tr></table>"));
            pnlTabContainer.Controls.Add(new LiteralControl("</div>"));
            #endregion

            #region Grilla
            pnlTabContainer.Controls.Add(new LiteralControl("<div>"));

            _RgdMasterGridExtendedProps = new RadGrid();
            _RgdMasterGridExtendedProps.ID = "rgdMasterGridExtendedProps";
            _RgdMasterGridExtendedProps.AllowPaging = true;
            _RgdMasterGridExtendedProps.AllowSorting = true;
            _RgdMasterGridExtendedProps.Width = Unit.Percentage(100);
            _RgdMasterGridExtendedProps.AutoGenerateColumns = false;
            _RgdMasterGridExtendedProps.GridLines = System.Web.UI.WebControls.GridLines.None;
            _RgdMasterGridExtendedProps.ShowStatusBar = false;
            _RgdMasterGridExtendedProps.PageSize = 18;
            _RgdMasterGridExtendedProps.AllowMultiRowSelection = true;
            _RgdMasterGridExtendedProps.PagerStyle.AlwaysVisible = true;
            _RgdMasterGridExtendedProps.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
            _RgdMasterGridExtendedProps.MasterTableView.Width = Unit.Percentage(100);
            _RgdMasterGridExtendedProps.EnableViewState = false;

            //Crea los metodos de la grilla (Server)
            _RgdMasterGridExtendedProps.NeedDataSource += new GridNeedDataSourceEventHandler(_RgdMasterGridExtendedProps_NeedDataSource);
            _RgdMasterGridExtendedProps.ItemCreated += new GridItemEventHandler(_RgdMasterGridExtendedProps_ItemCreated);
            _RgdMasterGridExtendedProps.SortCommand += new GridSortCommandEventHandler(_RgdMasterGridExtendedProps_SortCommand);
            _RgdMasterGridExtendedProps.PageIndexChanged += new GridPageChangedEventHandler(_RgdMasterGridExtendedProps_PageIndexChanged);
            _RgdMasterGridExtendedProps.ItemDataBound += new GridItemEventHandler(_RgdMasterGridExtendedProps_ItemDataBound);
            _RgdMasterGridExtendedProps.ColumnCreated += new GridColumnCreatedEventHandler(_RgdMasterGridExtendedProps_ColumnCreated);

            //Crea los metodos de la grilla (Cliente)
            //_RgdMasterGridExtendedProps.ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenuPMExtendedProps";
            _RgdMasterGridExtendedProps.ClientSettings.AllowExpandCollapse = false;
            //_RgdMasterGridExtendedProps.ClientSettings.EnableClientKeyValues = true;
            _RgdMasterGridExtendedProps.AllowMultiRowSelection = true;
            _RgdMasterGridExtendedProps.ClientSettings.Selecting.AllowRowSelect = true;
            _RgdMasterGridExtendedProps.ClientSettings.Selecting.EnableDragToSelectRows = false;
            _RgdMasterGridExtendedProps.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;

            //Define los atributos de la MasterGrid
            _RgdMasterGridExtendedProps.MasterTableView.Name = "gridMaster";
            _RgdMasterGridExtendedProps.MasterTableView.DataKeyNames = new string[] { "IdExtendedProperty" };
            _RgdMasterGridExtendedProps.MasterTableView.EnableViewState = false;
            _RgdMasterGridExtendedProps.MasterTableView.CellPadding = 0;
            _RgdMasterGridExtendedProps.MasterTableView.CellSpacing = 0;
            _RgdMasterGridExtendedProps.MasterTableView.GridLines = System.Web.UI.WebControls.GridLines.None;
            _RgdMasterGridExtendedProps.MasterTableView.GroupsDefaultExpanded = false;
            _RgdMasterGridExtendedProps.MasterTableView.AllowMultiColumnSorting = false;
            _RgdMasterGridExtendedProps.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
            _RgdMasterGridExtendedProps.MasterTableView.ExpandCollapseColumn.Resizable = false;
            _RgdMasterGridExtendedProps.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = Unit.Pixel(20);
            _RgdMasterGridExtendedProps.MasterTableView.RowIndicatorColumn.Visible = false;
            _RgdMasterGridExtendedProps.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = Unit.Pixel(20);
            _RgdMasterGridExtendedProps.MasterTableView.EditMode = GridEditMode.InPlace;

            _RgdMasterGridExtendedProps.HeaderStyle.Font.Bold = false;
            _RgdMasterGridExtendedProps.HeaderStyle.Font.Italic = false;
            _RgdMasterGridExtendedProps.HeaderStyle.Font.Overline = false;
            _RgdMasterGridExtendedProps.HeaderStyle.Font.Strikeout = false;
            _RgdMasterGridExtendedProps.HeaderStyle.Font.Underline = false;
            _RgdMasterGridExtendedProps.HeaderStyle.Wrap = true;

            //**
            //Crea las columnas para la MasterGrid.
            DefineColumnsExtendedProps(_RgdMasterGridExtendedProps.MasterTableView);

            //Agrega toda la grilla dentro del panle que ya esta en el html.
            pnlTabContainer.Controls.Add(_RgdMasterGridExtendedProps);
            pnlTabContainer.Controls.Add(new LiteralControl("</div>"));
            #endregion
        }



        protected void BuildExecutionTabContenten(Panel pnlTabContainer)
        {
            //Menu de Seleccion
            //RadMenuItem ItemExecutionView = new RadMenuItem(Resources.Common.mnuView);
            //ItemExecutionView.ID = "RadMenuItem1";
            ////Common.Functions.DoRadItemSecurity(ItemExecutionView, true);
            //rmnSelectionPMExecution.Items.Add(ItemExecutionView);

            //RadMenuItem ItemExecutionEdit = new RadMenuItem(Resources.Common.mnuEdit);
            //ItemExecutionEdit.ID = "RadMenuItem2";
            ////Common.Functions.DoRadItemSecurity(ItemExecutionEdit, true);
            //rmnSelectionPMExecution.Items.Add(ItemExecutionEdit);

            //Cargar Js Dinamicamente...


            //Grilla
            pnlTabContainer.Controls.Add(new LiteralControl("<div class='contentform'>"));

            _RgdMasterGridExecution = new RadGrid();
            _RgdMasterGridExecution.ID = "rgdMasterGrid";
            _RgdMasterGridExecution.AllowPaging = true;
            _RgdMasterGridExecution.AllowSorting = true;
            _RgdMasterGridExecution.Width = Unit.Percentage(100);
            _RgdMasterGridExecution.AutoGenerateColumns = false;
            _RgdMasterGridExecution.GridLines = System.Web.UI.WebControls.GridLines.None;
            _RgdMasterGridExecution.ShowStatusBar = false;
            _RgdMasterGridExecution.PageSize = 18;
            _RgdMasterGridExecution.AllowMultiRowSelection = true;
            _RgdMasterGridExecution.PagerStyle.AlwaysVisible = true;
            _RgdMasterGridExecution.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
            _RgdMasterGridExecution.MasterTableView.Width = Unit.Percentage(100);
            _RgdMasterGridExecution.EnableViewState = false;

            //Crea los metodos de la grilla (Server)
            _RgdMasterGridExecution.NeedDataSource += new GridNeedDataSourceEventHandler(this.rgdMasterGridExecution_NeedDataSource);
            _RgdMasterGridExecution.ItemCreated += new GridItemEventHandler(this.rgdMasterGridExecution_ItemCreated);
            _RgdMasterGridExecution.SortCommand += new GridSortCommandEventHandler(this.rgdMasterGridExecution_SortCommand);
            _RgdMasterGridExecution.PageIndexChanged += new GridPageChangedEventHandler(this.rgdMasterGridExecution_PageIndexChanged);

            //Crea los metodos de la grilla (Cliente)
            //_RgdMasterGridExecution.ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenuPMExecution";
            _RgdMasterGridExecution.ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenu";
            //rgdMasterGrid.ClientSettings.ClientEvents.OnRowSelected = "RowSelected";
            _RgdMasterGridExecution.ClientSettings.AllowExpandCollapse = false;
            //_RgdMasterGridExecution.ClientSettings.EnableClientKeyValues = true;
            _RgdMasterGridExecution.AllowMultiRowSelection = false;
            _RgdMasterGridExecution.ClientSettings.Selecting.AllowRowSelect = true;
            _RgdMasterGridExecution.ClientSettings.Selecting.EnableDragToSelectRows = false;
            _RgdMasterGridExecution.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;


            //Define los atributos de la MasterGrid
            _RgdMasterGridExecution.MasterTableView.Name = "gridMaster";
            //_RgdMasterGridExecution.MasterTableView.DataKeyNames = new string[] { "IdExecution", "Post", "HasException" };
            _RgdMasterGridExecution.MasterTableView.DataKeyNames = new string[] { "IdExecution", "Post" };
            _RgdMasterGridExecution.MasterTableView.EnableViewState = false;
            _RgdMasterGridExecution.MasterTableView.CellPadding = 0;
            _RgdMasterGridExecution.MasterTableView.CellSpacing = 0;
            _RgdMasterGridExecution.MasterTableView.GridLines = System.Web.UI.WebControls.GridLines.None;
            _RgdMasterGridExecution.MasterTableView.GroupsDefaultExpanded = false;
            _RgdMasterGridExecution.MasterTableView.AllowMultiColumnSorting = false;
            _RgdMasterGridExecution.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
            _RgdMasterGridExecution.MasterTableView.ExpandCollapseColumn.Resizable = false;
            _RgdMasterGridExecution.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = Unit.Pixel(20);
            _RgdMasterGridExecution.MasterTableView.RowIndicatorColumn.Visible = false;
            _RgdMasterGridExecution.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = Unit.Pixel(20);

            _RgdMasterGridExecution.HeaderStyle.Font.Bold = false;
            _RgdMasterGridExecution.HeaderStyle.Font.Italic = false;
            _RgdMasterGridExecution.HeaderStyle.Font.Overline = false;
            _RgdMasterGridExecution.HeaderStyle.Font.Strikeout = false;
            _RgdMasterGridExecution.HeaderStyle.Font.Underline = false;
            _RgdMasterGridExecution.HeaderStyle.Wrap = true;

            //Crea las columnas para la MasterGrid.
            DefineColumnsExecutions(_RgdMasterGridExecution.MasterTableView);

            //Agrega toda la grilla dentro del panle que ya esta en el html.
            pnlTabContainer.Controls.Add(_RgdMasterGridExecution);
            pnlTabContainer.Controls.Add(new LiteralControl("</div>"));
        }

        protected Int16 SetProcessOrder(String order)
        {
            if (order != String.Empty)
                return Convert.ToInt16(order);
            else
                return 0;
        }
        #endregion

        #region DdlPropertyClassification
        //El Id Seleccionado del Combo de PropClassifications
        protected String _IdExtendedPropertyClassification
        {
            get
            {
                object _o = ViewState["IdExtendedPropertyClassification"];

                if (_o != null)
                    return Convert.ToString(ViewState["IdExtendedPropertyClassification"]);

                return "-1";
            }
            set { ViewState["IdExtendedPropertyClassification"] = value; }
        }
        #region Methods
        protected void LoadExtendedPropertyClassifications(ref RadComboBox rcbCombo)
        {
            rcbCombo.Items.Clear();

            Dictionary<Int64, Condesus.EMS.Business.EP.Entities.ExtendedPropertyClassification> _extendedPropertyClassifications = EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassifications();

            RadComboBoxItem _li = new RadComboBoxItem(Resources.CommonListManage.comboboxselectitemextendedpropertiesclassifications, "-1");
            rcbCombo.Items.Add(_li);

            foreach (Condesus.EMS.Business.EP.Entities.ExtendedPropertyClassification _extendedPropertyClassification in _extendedPropertyClassifications.Values)
            {
                _li = new RadComboBoxItem(_extendedPropertyClassification.LanguageOption.Name, _extendedPropertyClassification.IdExtendedPropertyClassification.ToString());
                rcbCombo.Items.Add(_li);
            }
            rcbCombo.SelectedValue = "-1";
        }

        protected void LoadComboExtendedPropertyClassification(DropDownList ddlExtendedPropertyClassification)
        {
            ddlExtendedPropertyClassification.Items.Clear();

            Dictionary<Int64, Condesus.EMS.Business.EP.Entities.ExtendedPropertyClassification> oExtendedPropertyClassifications = EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassifications();

            ListItem li = new ListItem(Resources.CommonListManage.comboboxselectitemextendedpropertiesclassifications, "-1");
            ddlExtendedPropertyClassification.Items.Add(li);
            foreach (Condesus.EMS.Business.EP.Entities.ExtendedPropertyClassification oExtendedPropertyClassification in oExtendedPropertyClassifications.Values)
            {
                li = new ListItem(oExtendedPropertyClassification.LanguageOption.Name, oExtendedPropertyClassification.IdExtendedPropertyClassification.ToString());
                ddlExtendedPropertyClassification.Items.Add(li);
            }

            ddlExtendedPropertyClassification.SelectedValue = _IdExtendedPropertyClassification;
        }

        protected void JsDdlExtendedPropertyClassification(String ddlClientId)
        {
            StringBuilder jsBuffer = new StringBuilder();

            jsBuffer.Append(OpenHtmlJavaScript());
            jsBuffer.Append("var valueHasChanged = false;");
            jsBuffer.Append("//Para persisitir el Indice del DropDown");
            jsBuffer.Append("var ddSelIndex;");
            //jsBuffer.Append("window.onload = InitDdSelIndex;");
            jsBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            jsBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            jsBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");
            jsBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            jsBuffer.Append("  {   //IE and Opera                                                                          \n");
            jsBuffer.Append("      window.attachEvent('onload', InitDdSelIndex);                                \n");
            jsBuffer.Append("  }                                                                                           \n");
            jsBuffer.Append("  else                                                                                        \n");
            jsBuffer.Append("  {   //FireFox                                                                               \n");
            jsBuffer.Append("      document.addEventListener('DOMContentLoaded', InitDdSelIndex, false);        \n");
            jsBuffer.Append("  }                                                                                           \n");

            jsBuffer.Append("function InitDdSelIndex() {");
            jsBuffer.Append("var ddExtProp = " + ddlClientId);
            jsBuffer.Append("if(ddExtProp != null)");
            jsBuffer.Append("ddSelIndex = ddExtProp.selectedIndex;}");
            jsBuffer.Append("function ResetDdSelIndexes() {");
            jsBuffer.Append("var ddExtProp = " + ddlClientId);
            jsBuffer.Append("if(ddExtProp != null)");
            jsBuffer.Append("ddExtProp.selectedIndex = ddSelIndex; }");
            jsBuffer.Append("function CheckExtendedProperty(chkBox, textBoxId) {");
            jsBuffer.Append("valueHasChanged = true;");
            jsBuffer.Append("var _chkBox = chkBox;");
            jsBuffer.Append("var _txtBox = document.getElementById(textBoxId);");
            jsBuffer.Append("if(chkBox.checked) {");
            jsBuffer.Append("_txtBox.disabled = false;");
            jsBuffer.Append("_txtBox.focus();");
            jsBuffer.Append("} else {");
            jsBuffer.Append("_txtBox.value = '';");
            jsBuffer.Append("_txtBox.disabled = true;");
            jsBuffer.Append("_txtBox.blur(); }");
            jsBuffer.Append("}");
            jsBuffer.Append("function ValidateTextBox(txtBox, chkBoxId) {");
            jsBuffer.Append("var _txtBox = txtBox;");
            jsBuffer.Append("var _chkBox = document.getElementById(chkBoxId);");
            jsBuffer.Append("valueHasChanged = true; }");
            jsBuffer.Append("function CheckSaveStatus() {");
            jsBuffer.Append("if(valueHasChanged) {");
            jsBuffer.Append("if(confirm('There has been changes made on the Extended Properties.\nDo you want to continue without saving? (changes will be lost)')) {");
            jsBuffer.Append("DoPostBackDropDown();");
            jsBuffer.Append("} else {");
            jsBuffer.Append("ResetDdSelIndexes(); }");
            jsBuffer.Append("} else {");
            jsBuffer.Append("DoPostBackDropDown(); }");
            jsBuffer.Append("}");
            jsBuffer.Append("function DoPostBackDropDown() {");
            jsBuffer.Append("setTimeout('__doPostBack(\'ctl00$ContentMain$PM$DropDownEPClasification\',\'\')', 0)");
            jsBuffer.Append("}");
            jsBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("DDL_EXTENDED_JS", jsBuffer.ToString());
        }
        #endregion
        #region Events
        // void ddlExtendedPropertyClassification_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    RadComboBox _ddlExtendedPropertyClassification = (RadComboBox)o;

        //     _IdExtendedPropertyClassification = _ddlExtendedPropertyClassification.SelectedValue;

        //     //2 opciones. Buscarlo o que el Hijo se Lo pase
        //     //Si se disparo este Evento, es x que el TabStrip esta habilitado (no hay necesidad de pasar por el SetTabState)
        //     Control tabPanel = Page.FindControl("ctl00$ContentMain$pnlTabContainer");
        //     if (tabPanel != null && tabPanel is Panel)
        //     {
        //         ((Panel)tabPanel).Controls.Clear();
        //         BuildExtendedDataTabContent(((Panel)tabPanel));
        //     }
        // }
        void ddlExtendedPropertyClassification_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList _ddlExtendedPropertyClassification = (DropDownList)sender;

            _IdExtendedPropertyClassification = _ddlExtendedPropertyClassification.SelectedValue;

            //2 opciones. Buscarlo o que el Hijo se Lo pase
            //Si se disparo este Evento, es x que el TabStrip esta habilitado (no hay necesidad de pasar por el SetTabState)
            Control tabPanel = Page.FindControl("ctl00$ContentMain$pnlTabContainer");
            if (tabPanel != null && tabPanel is Panel)
            {
                ((Panel)tabPanel).Controls.Clear();
                BuildExtendedDataTabContent(((Panel)tabPanel));
            }
        }
        #endregion
        #endregion

        protected void LoadMeasurementDeviceType(ref RadComboBox rcbCombo)
        {
            rcbCombo.Items.Clear();

            Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDeviceType> _measurementDeviceTypes = EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDeviceTypes();

            RadComboBoxItem _li = new RadComboBoxItem(Resources.CommonListManage.comboboxselectitemmeasurementdevicetypes, "-1");
            rcbCombo.Items.Add(_li);

            foreach (Condesus.EMS.Business.PA.Entities.MeasurementDeviceType _measurementDeviceType in _measurementDeviceTypes.Values)
            {
                _li = new RadComboBoxItem(_measurementDeviceType.LanguageOption.Name, _measurementDeviceType.IdMeasurementDeviceType.ToString());
                rcbCombo.Items.Add(_li);
            }
            rcbCombo.SelectedValue = "-1";
        }

        #region RadMasterGrid ExtendedProperties
        protected RadGrid _RgdMasterGridExtendedProps;
        #region Methods
        private DataTable ReturnDataGridExtendedProps()
        {
            //Crea el DataTable para luego devolverlo y asociarlo a la grilla.
            DataTable _dt = new DataTable();
            _dt.TableName = "Root";
            _dt.Columns.Add("IdExtendedProperty");
            _dt.Columns.Add("Name");
            _dt.Columns.Add("Description");

            //Se carga el valor del campo de filtro
            if (_IdExtendedPropertyClassification != "-1")
            {
                foreach (Condesus.EMS.Business.EP.Entities.ExtendedProperty oExtendedProperty in EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassification(Convert.ToInt64(_IdExtendedPropertyClassification)).ExtendedProperties.Values)
                {
                    Condesus.EMS.Business.EP.Entities.ExtendedProperty_LG _extendedProperty_LG = oExtendedProperty.LanguageOption;
                    String _name = Common.Functions.ReplaceIndexesTags(_extendedProperty_LG.Name);

                    _dt.Rows.Add(oExtendedProperty.IdExtendedProperty, _name, _extendedProperty_LG.Description);
                }
            }
            return _dt;
        }
        private void SetExtendedPropertyRowState(Boolean state, String val, CheckBox chkBox, TextBox txtBox)
        {
            //Setea los estados
            chkBox.Checked = txtBox.Enabled = state;

            //Setea los Valores
            chkBox.Attributes.Add("Existe", (state) ? "true" : "false");
            txtBox.Text = val;
        }
        protected void DefineColumnsExtendedProps(GridTableView gridTableViewDetails)
        {
            GridTemplateColumn template;

            template = new GridTemplateColumn();
            template.UniqueName = "TemplateColumn";

            //template.HeaderTemplate = new PMCheckTemplate("chkSelectHeader", "Check All", "javascript:Check(this);");
            template.ItemTemplate = new PMCheckTemplate("chkSelect", "Check", "");
            template.ItemStyle.Width = Unit.Pixel(21);

            TableItemStyle headerStyle = template.HeaderStyle;
            headerStyle.Width = Unit.Pixel(21);
            template.HeaderText = "";
            template.AllowFiltering = false;
            gridTableViewDetails.Columns.Add(template);

            //Add columns bound
            GridBoundColumn boundColumn;

            //Columnas que no se ven...
            //Crea y agrega las columnas de tipo Bound
            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "IdExtendedProperty";
            boundColumn.HeaderText = "IdExtendedProperty";
            boundColumn.Display = false;
            gridTableViewDetails.Columns.Add(boundColumn);

            //Crea y agrega las columnas de tipo Bound
            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Name";
            boundColumn.HeaderText = "Name";
            boundColumn.ItemStyle.Width = Unit.Pixel(200);
            boundColumn.Display = true;
            gridTableViewDetails.Columns.Add(boundColumn);

            //boundColumn = new GridBoundColumn();
            //boundColumn.DataField = "Description";
            //boundColumn.HeaderText = "Description";
            //boundColumn.Display = true;
            //gridTableViewDetails.Columns.Add(boundColumn);

            template = new GridTemplateColumn();
            template.UniqueName = "txtValueTemplateColumn";
            template.ItemTemplate = new PMTextBoxTemplate("txtValue");
            //template.ItemStyle.Width = Unit.Pixel(100);
            template.HeaderText = "Value";
            template.AllowFiltering = false;
            gridTableViewDetails.Columns.Add(template);
        }

        protected void SaveExecutionPropertyRels()
        {
            foreach (GridDataItem row in _RgdMasterGridExtendedProps.Items)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                TextBox _txtBox = (TextBox)row.FindControl("txtValue");

                if (chkSelect != null)
                {
                    Boolean _existe = Convert.ToBoolean(chkSelect.Attributes["Existe"]);
                    Int64 _idExtendedProperty = Convert.ToInt64(row.OwnerTableView.DataKeyValues[row.ItemIndex]["IdExtendedProperty"]);
                    Int64 _idExtendedPropertyClass = Convert.ToInt64(row.OwnerTableView.DataKeyValues[row.ItemIndex]["IdExtendedPropertyClassification"]);

                    //Elimino
                    if ((!chkSelect.Checked) && (_existe))
                    {
                        _Process.Remove(_Process.ExtendedPropertyValue(_idExtendedProperty));
                        //chkSelect.Attributes["Existe"] = "false";
                    }

                    //Agrego o Modifico
                    if ((chkSelect.Checked))
                    {
                        Condesus.EMS.Business.EP.Entities.ExtendedProperty _extendedProperty = EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassification(_idExtendedPropertyClass).ExtendedProperty(_idExtendedProperty);
                        if (!_existe)
                            _Process.ExtendedPropertyValueAdd(_extendedProperty, _txtBox.Text);
                        else
                            //Procedure or function 'PM_ProcessPosts_ReadById' expects parameter '@IdPerson', which was not supplied    
                            _Process.ExtendedPropertyValueModify(_Process.ExtendedPropertyValue(_idExtendedProperty), _txtBox.Text);

                        chkSelect.Attributes["Existe"] = "true";
                    }
                }
            }
        }

        #endregion
        #region Events
        protected void _RgdMasterGridExtendedProps_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            if (e.Column is GridTemplateColumn)
            {
                if ((e.Column as GridTemplateColumn).UniqueName == "TemplateColumn")
                {
                    e.Column.HeaderStyle.Width = Unit.Pixel(21);
                }
            }
        }
        protected void _RgdMasterGridExtendedProps_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //System.Diagnostics.Debugger.Break();
                CheckBox _chkBox = (CheckBox)e.Item.FindControl("chkSelect");
                TextBox _txtBox = (TextBox)e.Item.FindControl("txtValue");

                _chkBox.Attributes.Add("onclick", "CheckExtendedProperty(this, '" + _txtBox.ClientID + "')");
                _txtBox.Attributes.Add("onchange", "ValidateTextBox(this,  '" + _chkBox.ClientID + "')");


                if (_Process.ExtendedPropertyValues != null && _Process.ExtendedPropertyValues.Count > 0)
                {
                    Int64 _idExtendedProperty = Convert.ToInt64(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IdExtendedProperty"]);

                    var isChecked = from w in _Process.ExtendedPropertyValues
                                    where w.ExtendedProperty.IdExtendedProperty == _idExtendedProperty
                                    select w.Value;

                    if (isChecked.Count() > 0)
                    {
                        foreach (var _extPropVal in isChecked.Take(1))
                            SetExtendedPropertyRowState(true, _extPropVal, _chkBox, _txtBox);
                    }
                    else
                    {
                        SetExtendedPropertyRowState(false, String.Empty, _chkBox, _txtBox);
                    }
                }
                else
                {
                    SetExtendedPropertyRowState(false, String.Empty, _chkBox, _txtBox);
                }

                HtmlImage oimg = (HtmlImage)e.Item.FindControl("selButton");
                if (!(oimg == null))
                {
                    oimg.Attributes["onclick"] = string.Format("return ShowMenu(event, " + e.Item.RowIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
                }
            }
        }
        protected void _RgdMasterGridExtendedProps_ItemDataBound(object sender, GridItemEventArgs e)
        {
            CheckBox oCheckHeader = (CheckBox)e.Item.FindControl("chkSelectHeader");
            if (!(oCheckHeader == null))
            {
                oCheckHeader.InputAttributes.Add("InternalID", "Header");
            }
            CheckBox oCheck = (CheckBox)e.Item.FindControl("chkSelect");
            if (!(oCheck == null))
            {
                oCheck.InputAttributes.Add("InternalID", "Header|Item" + e.Item.UniqueID);
                //Solo para los registros, no cabeceras...
                oCheck.InputAttributes.Add("TableID", e.Item.OwnerTableView.Name);
            }
        }
        protected void _RgdMasterGridExtendedProps_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridExtendedProps();
            handler.MasterTableView.Rebind();
        }
        protected void _RgdMasterGridExtendedProps_SortCommand(object source, GridSortCommandEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridExtendedProps();
            handler.MasterTableView.Rebind();
        }
        protected void _RgdMasterGridExtendedProps_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            //Carga todos los ROOT en la grilla.
            handler.DataSource = ReturnDataGridExtendedProps();
        }
        #endregion
        #endregion

        #region RadMasterGrid Executions
        protected RadGrid _RgdMasterGridExecution;
        #region Methods
        private void DefineColumnsExecutions(GridTableView gridTableViewDetails)
        {
            //Add columns bound
            GridBoundColumn boundColumn;

            //Columnas que no se ven...
            //Crea y agrega las columnas de tipo Bound
            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "IdExecution";
            boundColumn.HeaderText = "IdExecution";
            boundColumn.Display = false;
            gridTableViewDetails.Columns.Add(boundColumn);

            //Crea y agrega las columnas de tipo Bound
            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Date";
            boundColumn.HeaderText = "Date";
            boundColumn.Display = true;
            gridTableViewDetails.Columns.Add(boundColumn);


            //Crea y agrega las columnas de tipo Bound
            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Post";
            boundColumn.HeaderText = "Post";
            boundColumn.Display = true;
            gridTableViewDetails.Columns.Add(boundColumn);

            //Crea y agrega las columnas de tipo Bound
            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Comment";
            boundColumn.HeaderText = "Comment";
            boundColumn.Display = true;
            gridTableViewDetails.Columns.Add(boundColumn);

            ////Crea y agrega las columnas de tipo Bound
            //boundColumn = new GridBoundColumn();
            //boundColumn.DataField = "HasException";
            //boundColumn.HeaderText = "HasException";
            //boundColumn.Display = false;
            //gridTableViewDetails.Columns.Add(boundColumn);
        }
        private DataTable ReturnDataGridExecutions()
        {
            //Crea el DataTable para luego devolverlo y asociarlo a la grilla.
            DataTable _dt = new DataTable();
            _dt.TableName = "Root";
            _dt.Columns.Add("IdExecution");
            _dt.Columns.Add("Date");
            _dt.Columns.Add("Post");
            _dt.Columns.Add("Comment");
            //_dt.Columns.Add("HasException");

            //Si es una tarea nueva, no puede cargar nada
            if (_Process != null)
            {
                //List<Condesus.EMS.Business.PF.Entities.ProcessTaskExecution> _processTaskExecutions = ((Condesus.EMS.Business.PF.Entities.ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_Process.IdProcess)).ProcessTaskExecutionsOnly;
                Dictionary<Int64, Condesus.EMS.Business.PF.Entities.ProcessTaskExecution> _processTaskExecutions = ((Condesus.EMS.Business.PF.Entities.ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_Process.IdProcess)).ProcessTaskExecutionsOnly;

                foreach (Condesus.EMS.Business.PF.Entities.ProcessTaskExecution _processTaskExecution in _processTaskExecutions.Values)
                {
                    //Agrega los datos al DataTable
                    if (_processTaskExecution.Post == null)
                    {
                        _dt.Rows.Add(_processTaskExecution.IdExecution, _processTaskExecution.Date, String.Empty, _processTaskExecution.Comment);//, "false");
                    }
                    else
                    {
                        _dt.Rows.Add(_processTaskExecution.IdExecution, _processTaskExecution.Date, EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).Person(_processTaskExecution.Post.Person.IdPerson).LastName + ", " + EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).Person(_processTaskExecution.Post.Person.IdPerson).FirstName + " - " + _processTaskExecution.Post.JobTitle.Name(), _processTaskExecution.Comment);//, HasExecution(_processTaskExecution));
                    }
                }
            }
            return _dt;
        }
        //private String HasExecution(Condesus.EMS.Business.PF.Entities.ProcessTaskExecution execution)
        //{
        //    if (execution.Exception != null)
        //        return "true";

        //    return "false";
        //}
        #endregion

        #region Events
        protected void rgdMasterGridExecution_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                HtmlImage oimg = (HtmlImage)e.Item.FindControl("selButton");
                if (!(oimg == null))
                {
                    oimg.Attributes["onclick"] = string.Format("return ShowMenu(event, " + e.Item.RowIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
                }
            }
        }
        protected void rgdMasterGridExecution_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridExecutions();
            handler.MasterTableView.Rebind();
        }
        protected void rgdMasterGridExecution_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridExecutions();
            handler.MasterTableView.Rebind();
        }
        protected void rgdMasterGridExecution_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            //Carga todos los ROOT en la grilla.
            handler.DataSource = ReturnDataGridExecutions();
        }
        #endregion

        #region Menu Seleccion
        protected void rmnSelection_ItemClick(String key)
        {
            Context.Items.Add(key, _RgdMasterGridExecution.SelectedValue);  //identificador de la ejecucion
        }
        #endregion

        #endregion

        #region JavaScript

        protected void InjectInitializeTaskValidators(RadioButtonList radioBtnList, String taskType)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            //TODO: Desarollar Factory (reflection) para permitir n tipos

            //Name Exapmle: "ctl00_ContentMain_rblOptionTypeExecution_0"
            String _radioListButtonClientId = radioBtnList.ClientID;
            radioBtnList.CausesValidation = false;
            switch (taskType)
            {
                case "Spontaneous":
                case "":
                    _radioListButtonClientId += "_0";
                    radioBtnList.Items[0].Selected = true;
                    break;
                case "Repeatability":
                    _radioListButtonClientId += "_1";
                    radioBtnList.Items[1].Selected = true;
                    break;
                case "Scheduler":
                    _radioListButtonClientId += "_2";
                    radioBtnList.Items[2].Selected = true;
                    break;
            }

            _sbBuffer.Append(OpenHtmlJavaScript());
            //Inicializacion de Variables
            _sbBuffer.Append("var radioListId = '" + _radioListButtonClientId + "';                                            \n");
            //_sbBuffer.Append("window.attachEvent('onload', InitializeTaskValidators);                                       \n");
            _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

            _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("      window.attachEvent('onload', InitializeTaskValidators);                                \n");
            _sbBuffer.Append("  }                                                                                           \n");
            _sbBuffer.Append("  else                                                                                        \n");
            _sbBuffer.Append("  {   //FireFox                                                                               \n");
            _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', InitializeTaskValidators, false);        \n");
            _sbBuffer.Append("  }                                                                                           \n");
            //Initialize
            _sbBuffer.Append("function InitializeTaskValidators()                                                           \n");
            _sbBuffer.Append("{                                                                                             \n");
            _sbBuffer.Append("  var oRadioList = $get(radioListId);                                                         \n");
            _sbBuffer.Append("  if(oRadioList != null)                                                                      \n");
            _sbBuffer.Append("    RadioButtonChange(oRadioList);                                                            \n");
            _sbBuffer.Append("}                                                                                             \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_InitializeTaskValidators", _sbBuffer.ToString());
        }

        #endregion
    }

    public class PMCheckTemplate : ITemplate
    {
        protected CheckBox _CheckBox;

        private String _Id;
        private String _ToolTip;
        private String _JsOnClick;

        public PMCheckTemplate(String id, String toolTip, String jsOnClick)
        {
            _Id = id;
            _ToolTip = toolTip;
            _JsOnClick = jsOnClick;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            _CheckBox = new CheckBox();

            _CheckBox.ID = _Id;
            _CheckBox.ToolTip = _ToolTip;

            if (_JsOnClick != String.Empty)
                _CheckBox.Attributes.Add("onclick", _JsOnClick);

            container.Controls.Add(_CheckBox);
        }


    }

    public class PMTextBoxTemplate : ITemplate
    {
        protected TextBox _TextBox;

        private String _Id;

        public PMTextBoxTemplate(String id)
        {
            _Id = id;
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            _TextBox = new TextBox();
            _TextBox.Attributes["class"] = "contentformGridTextBox";
            _TextBox.ID = _Id;
            _TextBox.Width = Unit.Percentage(100);
            //_CheckBox.Attributes.Add("onBlur", _Js);

            container.Controls.Add(_TextBox);
        }
    }
}
