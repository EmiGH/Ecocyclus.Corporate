using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Globalization;
using Condesus.EMS.Business.NT.Entities;

namespace Condesus.EMS.WebUI.PM
{
    public partial class ProcessTaskDataRecoveriesProperties : BasePropertiesTask   //Condesus.EMS.WebUI.ManagementTools.ProcessesMap.BasePM
    {
//        #region Internal Properties
//        private Int64 IdValue
//        {
//            get { return Convert.ToInt64(ViewState["IdValue"]); }
//            set { ViewState["IdValue"] = value.ToString(); }
//        }
//        private String ItemOptionSelected
//        {
//            get { return Convert.ToString(ViewState["ItemOptionSelected"]); }
//            set { ViewState["ItemOptionSelected"] = value.ToString(); }
//        }
//        private Int64 IdParentProcess
//        {
//            get { return Convert.ToInt64(ViewState["IdParentProcess"]); }
//            set { ViewState["IdParentProcess"] = value.ToString(); }
//        }

//        private Int64 IdException
//        {
//            get { return Convert.ToInt64(ViewState["IdException"]); }
//            set { ViewState["IdException"] = value.ToString(); }
//        }
        
//        private String TypeExecution
//        {
//            get { return Convert.ToString(ViewState["typeExecution"]); }
//            set { ViewState["typeExecution"] = value.ToString(); }
//        }

//        private List<DateTime> _SelectedRows
//        {
//            get 
//            {
//                object _o = ViewState["SelectedRows"];
//                if(_o != null)
//                    return (List<DateTime>)_o;

//                throw new Exception("The collection is null");
//            }
//            set { ViewState["SelectedRows"] = value; }
//        }
//        private RadComboBox _RdcSite;
//        private RadTreeView _RtvSite;
//        #endregion

//        #region Load Information
//        protected void Page_Init(object sender, EventArgs e)
//        {
//            InjectJavascript();
//            InitializeHandlers();

//            //rgdMasterGrid.MasterTableView.DataKeyNames = new String[] {"IdRow"};
//            rgdMasterGrid.ClientSettings.AllowExpandCollapse = false;
//            rgdMasterGrid.ClientSettings.Selecting.AllowRowSelect = true;
//            rgdMasterGrid.AllowMultiRowSelection = false;
//            rgdMasterGrid.ClientSettings.Selecting.EnableDragToSelectRows = false;
//            rgdMasterGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;

//            CheckSecurity();
//            lblSite.Text = Resources.CommonListManage.Site;

//        }
//        private void InjectJavascript()
//        {
////            base.InjectValidateCombo(ddlTimeUnitDuration.ClientID, btnSave.ClientID, "0", "TimeUnitDuration");
//////            base.InjectValidateCombo(ddlTimeUnitInterval.ClientID, btnSave.ClientID, "0", "TimeUnitInterval");

////            base.InjectValidateCombo(ddlMeasurement.ClientID, btnSave.ClientID, "0", "Measurement");
//            //base.InjectValidateDateTimePicker(rdtStartDate.ClientID, rdtEndDate.ClientID, "TaskDuration");
//            base.InjectValidateDateTimePicker(rdtMeasurementsFromDate.ClientID, rdtMeasurementsToDate.ClientID, "MeasurementDuration");
//        }
//        private void InitializeHandlers()
//        {
//            cvComboTimeUnitDuration.ClientValidationFunction = "ValidateComboTimeUnitDuration";

//            customvMeasurement.ClientValidationFunction = "ValidateComboMeasurement";

//            //customvEndDate.EnableClientScript = true;
//            //customvEndDate.ClientValidationFunction = "ValidateDateTimeRangeTaskDuration";
//            custumvMeasurementEndDate.EnableClientScript = true;
//            custumvMeasurementEndDate.ClientValidationFunction = "ValidateDateTimeRangeMeasurementDuration";


//            ddlMeasurement.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlMeasurement_SelectedIndexChanged);
//            btnFilter.Click += new EventHandler(btnFilter_Click);

//            //rgdMasterGrid.ItemCreated += new GridItemEventHandler(rgdMasterGrid_ItemCreated);
//            rgdMasterGrid.ItemDataBound += new GridItemEventHandler(rgdMasterGrid_ItemDataBound);
//            rgdMasterGrid.PageIndexChanged += new GridPageChangedEventHandler(rgdMasterGrid_PageIndexChanged);
//        }

//        void rgdMasterGrid_ItemDataBound(object sender, GridItemEventArgs e)
//        {
//            //Ver los Seleccionados para persistir el estado
//            if (e.Item is GridDataItem)
//            {
//                CheckBox _chk = (CheckBox)e.Item.FindControl("chkSelect");
//                DateTime _value = DateTime.MinValue;
//                if (_chk != null)
//                {
//                    try
//                    {
//                        _value = Convert.ToDateTime(((GridDataItem)e.Item)["Date"].Text);
//                        _chk.Checked = (_SelectedRows.Contains(_value)) ? true : false;
//                    }
//                    catch { }
//                }
//            }
//        }

//        protected void rgdMasterGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
//        {
//            SetSelectedItems();

//            rgdMasterGrid.DataSource = LoadGrid();
//            rgdMasterGrid.MasterTableView.Rebind();
//        }

//        /// <summary> Persiste los estados de los CheckBoxes de la Grilla </summary>
//        private void SetSelectedItems()
//        {
//            for (int i = 0; i < rgdMasterGrid.MasterTableView.Items.Count; i++)
//            {
//                GridDataItem _item = (GridDataItem)rgdMasterGrid.MasterTableView.Items[i];

//                CheckBox _chk = (CheckBox)_item.FindControl("chkSelect");



//                //String strDate = _item["Date"].Text;
//                //DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
//                //dtfi.ShortDatePattern = "MM/dd/yyyy";
//                //dtfi.ShortTimePattern = "HH:mm:ss";
//                //dtfi.DateSeparator = "/";
//                //dtfi.TimeSeparator = ":";

//                //DateTime objDate = Convert.ToDateTime(strDate, dtfi);
//                ////MessageBox.Show(objDate.ToShortDateString());

                
//                DateTime _value = Convert.ToDateTime(_item["Date"].Text);

//                if (_chk != null)
//                {
//                    if (_chk.Checked)
//                    {
//                        if (!_SelectedRows.Contains(_value))
//                            _SelectedRows.Add(_value);
//                    }
//                    else
//                    {
//                        if (_SelectedRows.Contains(_value))
//                            _SelectedRows.Remove(_value);
//                    }
//                }
//            }
//        }


//        private DataTable LoadGrid()
//        {
//            DataTable _dt = new DataTable();
//            _dt.TableName = "Root";
//            _dt.Columns.Add("Date", System.Type.GetType("System.DateTime"));

//            Int64 _IdMeasurement = Convert.ToInt64(ddlMeasurement.SelectedValue);

//            Condesus.EMS.Business.PA.Entities.Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
//            List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> _measurementPoints;
//            if (_measurement != null)
//                _measurementPoints = _measurement.Series();
//            else
//                return _dt;

//            //Filtro Date -> From-To (Linq)
//            DateTime _dFrom = DateTime.MinValue;
//            DateTime _dTo = DateTime.MaxValue;
//            if (rdtMeasurementsFromDate.DateInput.SelectedDate.HasValue)
//                _dFrom = rdtMeasurementsFromDate.DateInput.SelectedDate.Value;
//            if (rdtMeasurementsToDate.DateInput.SelectedDate.HasValue)
//                _dTo = rdtMeasurementsToDate.DateInput.SelectedDate.Value.AddHours(23).AddMinutes(59).AddSeconds(59);

//            var _filteredPoints = (from fp in _measurementPoints
//                                   where fp.MeasureDate >= _dFrom
//                                   select fp).Where(fp => fp.MeasureDate <= _dTo);

//            foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementPoint in _filteredPoints)
//                _dt.Rows.Add(_measurementPoint.MeasureDate.ToLongDateString() + " " + _measurementPoint.MeasureDate.ToLongTimeString());
//                //_dt.Rows.Add(_measurementPoint.MeasureDate.ToString("MM/dd/yyyy HH:mm:ss"));
                
            
//            //_dt.Rows.Add(_measurementPoint.MeasureDate.ToString("dd/MM/yyyy HH:mm:ss"));
            
//            return _dt;
//        }


//        void ddlMeasurement_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
//        {
//            _SelectedRows.Clear();

//            rgdMasterGrid.DataSource = LoadGrid();
//            rgdMasterGrid.Rebind();
//        }

//        void btnFilter_Click(object sender, EventArgs e)
//        {
//            rgdMasterGrid.DataSource = LoadGrid();
//            rgdMasterGrid.Rebind();
//        }

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnTransferAdd);

//            //rblOptionTypeExecution.Items[0].Attributes.Add("onclick", "javascript:RadioButtonChange(this);");
//            //rblOptionTypeExecution.Items[1].Attributes.Add("onclick", "javascript:RadioButtonChange(this);");
//            //rblOptionTypeExecution.Items[2].Attributes.Add("onclick", "javascript:RadioButtonChange(this);");
//            txtMaxNumberExecutions.ReadOnly = true;
//            txtMaxNumberExecutions.Text = "1";
//            txtInterval.Text = "0";
//            txtInterval.ReadOnly = true;
//            ddlTimeUnitInterval.Enabled = false;
//            rdtEndDate.Enabled = false;
//            TypeExecution = "Scheduler";
            
//            if (!Page.IsPostBack)
//            {
//                base.SetNavigator();

//                //Creo el List donde va a persistir las fechas
//                _SelectedRows = new List<DateTime>();

//                this.Title = "EMS - " + Resources.CommonListManage.ProcessTask;
//                lblLanguageValue.Text = Global.DefaultLanguage.Name;
//                ItemOptionSelected = Convert.ToString(this.Context.Items["ItemOptionSelected"]);
//                IdParentProcess = Convert.ToInt64(this.Context.Items["IdParentProcess"]);
//                IdException = Convert.ToInt64(this.Context.Items["IdException"]);


//                if (this.Context.Items["IdValue"] != null)
//                {
//                    IdValue = Convert.ToInt64(this.Context.Items["IdValue"]);
//                }

//                lblIdParentValue.Text = EMSLibrary.User.ProcessFramework.Map.Process(IdParentProcess).LanguageOption.Title;
//                lblProjectTitleValue.Text = EMSLibrary.User.ProcessFramework.Map.Process(IdParentProcess).ProcessGroupProcess.LanguageOption.Title;

//                LoadComboTimeUnitDuration();
//                LoadComboTimeUnitInterval();
//                LoadComboMeasurements();
//                rgdMasterGrid.DataSource = LoadGrid();
//                AddComboSites();

//                if (IdValue == 0)
//                {
//                    //Esto es un add,
//                    Add();
//                }
//                else if (ItemOptionSelected == "VIEW")
//                    { View(); }
//                else
//                    { Edit(); }

//                this.txtTitle.Focus();
//            }
//            else
//            {
//                if (TypeExecution == "")
//                {
//                    TypeExecution = Convert.ToString(Request.Form["typeExecution"]);
//                }
//            }
//            InjectInitializeTaskValidators(rblOptionTypeExecution, TypeExecution);
//            //Un recovery solo puede ser de tipo Scheduler.
//            rblOptionTypeExecution.Items[2].Selected = true;
            
//        }
//        private void Navigator()
//        {
//            String[,] _param = new String[,] { { "IdValue", IdValue.ToString() }, { "ItemOptionSelected", ItemOptionSelected.ToString() }, { "IdParentProcess", IdParentProcess.ToString() }, { "typeExecution", TypeExecution.ToString() } };

//            base.SetNavigator(_param);
//        }
//        private void CheckSecurity()
//        {
//            rmnGeneralOptions.Items.Clear();

//            //if (!EMSLibrary.User.Security.Authorize("DirectoryServices", "GeographicAreas", "Add")) { throw new UnauthorizedAccessException(Resources.Common.PageNotAlowed); }

//            //Boolean _deleteItem = EMSLibrary.User.Security.Authorize("DirectoryServices", "FunctionalAreas", "Remove");
//            //Boolean _viewItem = EMSLibrary.User.Security.Authorize("DirectoryServices", "FunctionalAreas", "Item");
//            //Boolean _editItem = EMSLibrary.User.Security.Authorize("DirectoryServices", "FunctionalAreas", "Modify");
//            //Boolean _addItem = EMSLibrary.User.Security.Authorize("DirectoryServices", "FunctionalAreas", "Add");

//            //Carga los menu en el inicio con el chequeo de seguridad
//            //Menu de Opciones
//            RadMenuItem ItemAdd = new RadMenuItem(Resources.Common.mnuAdd);
//            ItemAdd.Value = "ItemAdd";
//            Common.Functions.DoRadItemSecurity(ItemAdd, true);
//            rmnGeneralOptions.Items.Add(ItemAdd);

//            RadMenuItem ItemLanguage = new RadMenuItem(Resources.Common.mnuLanguage);
//            ItemLanguage.Value = "ItemLanguage";
//            Common.Functions.DoRadItemSecurity(ItemLanguage, true);
//            rmnGeneralOptions.Items.Add(ItemLanguage);

//            RadMenuItem ItemDelete = new RadMenuItem(Resources.Common.mnuDelete);
//            ItemDelete.Value = "ItemDelete";
//            Common.Functions.DoRadItemSecurity(ItemDelete, true);
//            rmnGeneralOptions.Items.Add(ItemDelete);
//        }
//        private void LoadComboTimeUnitDuration()
//        {
//            ddlTimeUnitDuration.Items.Clear();

//            RadComboBoxItem _radComboItem = new RadComboBoxItem(Resources.CommonListManage.comboboxselectitemtimeunits, "0");
//            ddlTimeUnitDuration.Items.Add(_radComboItem);

//            foreach (Condesus.EMS.Business.PF.Entities.TimeUnit oTimeUnit in EMSLibrary.User.ProcessFramework.Configuration.TimeUnits().Values)
//            {
//                _radComboItem = new RadComboBoxItem(oTimeUnit.LanguageOption.Name, oTimeUnit.IdTimeUnit.ToString());
//                ddlTimeUnitDuration.Items.Add(_radComboItem);
//            }
//        }
//        private void LoadComboTimeUnitInterval()
//        {
//            ddlTimeUnitInterval.Items.Clear();

//            RadComboBoxItem _radComboItem = new RadComboBoxItem(Resources.CommonListManage.comboboxselectitemtimeunits, "0");
//            ddlTimeUnitInterval.Items.Add(_radComboItem);

//            foreach (Condesus.EMS.Business.PF.Entities.TimeUnit oTimeUnit in EMSLibrary.User.ProcessFramework.Configuration.TimeUnits().Values)
//            {
//                _radComboItem = new RadComboBoxItem(oTimeUnit.LanguageOption.Name, oTimeUnit.IdTimeUnit.ToString());
//                ddlTimeUnitInterval.Items.Add(_radComboItem);
//            }
//        }

//        private void LoadComboMeasurements()
//        {
//            ddlMeasurement.Items.Clear();

//            RadComboBoxItem _radComboItem = new RadComboBoxItem(Resources.CommonListManage.comboboxselectitemtimeunits, "0");
//            ddlMeasurement.Items.Add(_radComboItem);

//            //Me Traigo todas las Mediciones del Proyceto al cual corresponde el Procees (GroupNodeExcpetion en este caso)
//            Condesus.EMS.Business.PF.Entities.Process _proyecto = EMSLibrary.User.ProcessFramework.Map.Process(IdParentProcess);
//            Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Measurement> _measurementsByProject = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)_proyecto).Measurements;
            
//            foreach (Condesus.EMS.Business.PA.Entities.Measurement measurement in _measurementsByProject.Values)
//            {
//                _radComboItem = new RadComboBoxItem(measurement.LanguageOption.Name, measurement.IdMeasurement.ToString());
//                ddlMeasurement.Items.Add(_radComboItem);
//            }
//        }
//        private void AddComboSites()
//        {
//            String _filterExpression = String.Empty;
//            //Combo de Organizaciones
//            Dictionary<String, Object> _params = new Dictionary<String, Object>();
//            AddComboTreeSites(ref phSite, ref _RdcSite, ref _RtvSite, new RadTreeViewEventHandler(rtvSite_NodeExpand));
//        }
//        private void SetSite()
//        {

//            Condesus.EMS.Business.GIS.Entities.Site _site = ((Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery)EMSLibrary.User.ProcessFramework.Map.Process(IdValue)).Site;
//            if (_site != null)
//            {
//                if (_site.GetType().Name == Common.ConstantsEntitiesName.DS.Facility)
//                {
//                    //Si el sitio seleccionado es un facility...
//                    //Seteamos la organizacion...
//                    //Realiza el seteo del parent en el Combo-Tree.
//                    Condesus.EMS.Business.DS.Entities.Organization _oganization = _site.Organization;
//                    String _keyValuesElement = "IdOrganization=" + _oganization.IdOrganization.ToString() + "& IdFacility=" + _site.IdFacility.ToString();
//                    if (_oganization.Classifications.Count > 0)
//                    {
//                        String _keyValuesClassification = "IdOrganizationClassification=" + _oganization.Classifications.First().Value.IdOrganizationClassification;
//                        SelectItemTreeViewParentElementMapsForSite(_keyValuesClassification, _keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.Organizations, true);
//                    }
//                    //Ahora busco el facility....
//                    SelectItemTreeViewParentForSite(_keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.Facility, Common.ConstantsEntitiesName.DS.Facilities, true);
//                }
//                else
//                {
//                    //Si es un sector...se hace un poquito mas complejo, ya que puede estar muy anidado...
//                    if (_site.GetType().Name == Common.ConstantsEntitiesName.DS.Sector)
//                    {
//                        //Casteo al sector!!!
//                        Condesus.EMS.Business.GIS.Entities.Sector _sector = (Condesus.EMS.Business.GIS.Entities.Sector)_site;
//                        //Tengo que obtener el facility de este sector!!!
//                        while (_sector.Parent.GetType().Name == Common.ConstantsEntitiesName.DS.Sector)
//                        {
//                            _sector = (Condesus.EMS.Business.GIS.Entities.Sector)_sector.Parent;
//                        }
//                        //Al salir de este while, tengo el facility;
//                        Condesus.EMS.Business.GIS.Entities.Facility _facility = (Condesus.EMS.Business.GIS.Entities.Facility)_sector.Parent;
//                        //Ahora busco la organizacion y todo el arbol!
//                        Condesus.EMS.Business.DS.Entities.Organization _oganization = _site.Organization;
//                        String _keyValuesElement = "IdOrganization=" + _oganization.IdOrganization.ToString() + "& IdFacility=" + _facility.IdFacility.ToString() + "& IdSector=" + _site.IdFacility.ToString();
//                        if (_oganization.Classifications.Count > 0)
//                        {
//                            String _keyValuesClassification = "IdOrganizationClassification=" + _oganization.Classifications.First().Value.IdOrganizationClassification;
//                            SelectItemTreeViewParentElementMapsForSite(_keyValuesClassification, _keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.Organizations, true);
//                        }
//                        //Ahora busco el sector....
//                        SelectItemTreeViewParentForSite(_keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.Sector, Common.ConstantsEntitiesName.DS.Sectors, true);
//                    }
//                }
//            }
//        }
//        #endregion

//        #region Controls Action
//        protected void rtvSite_NodeExpand(object sender, RadTreeNodeEventArgs e)
//        {
//            NodeExpandSites(sender, e, true);
//        }
//        protected void btnSave_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                //Condesus.EMS.Business.PF.Entities.ProcessGroupNode _processGroupNode = (Condesus.EMS.Business.PF.Entities.ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(IdParentProcess);

//                Condesus.EMS.Business.PF.Entities.TimeUnit _timeUnitDuration = EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(Convert.ToInt64(ddlTimeUnitDuration.SelectedValue));
//                Condesus.EMS.Business.PF.Entities.TimeUnit _timeUnitInterval = EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(Convert.ToInt64(ddlTimeUnitInterval.SelectedValue));

//                //OJO ACA!!!! hay fijo un 197!!!!!
//                //Se deberia obtener cual es la Medicion y la tarea asociada para recuperar!!!

//                Condesus.EMS.Business.IA.Entities.Exception _exception = (Condesus.EMS.Business.IA.Entities.Exception)EMSLibrary.User.ImprovementAction.Configuration.Exception(IdException);
//                Int64 _asociatedIdProcess = 0;
//                if (_exception.GetType().Name == "ExceptionProcessTask")
//                {
//                    _asociatedIdProcess = ((Condesus.EMS.Business.IA.Entities.ExceptionProcessTask)_exception).AssociateTask.IdProcess;
//                }
//                else
//                {
//                    //Aun no esta publicada la medicion en la excepcion....
//                    //_asociatedIdProcess = ((Condesus.EMS.Business.IA.Entities.ExceptionMeasurement)_exception).AssociateTask.IdProcess;
//                }
                
//                Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement _processTaskMeasurement = (Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_asociatedIdProcess);

                
//                //Obtener los measurementDate para recuperar!!!!!
//                //List<DateTime> _measurementDateToRecovery = new List<DateTime>();
//                //_measurementDateToRecovery.Add(DateTime.Now);
//                SetSelectedItems();
//                List<DateTime> _measurementDateToRecovery = _SelectedRows;

//                if (_timeUnitInterval == null)
//                {
//                    _timeUnitInterval = _timeUnitDuration;
//                }
//                Boolean _result = false;
//                if (lblResultValue.Text == Resources.ConstantMessage.ProcessTaskResult_Waiting)
//                { _result = false; }
//                else if (lblResultValue.Text == Resources.ConstantMessage.ProcessTaskResult_Error)
//                { _result = false; }
//                else if (lblResultValue.Text == Resources.ConstantMessage.ProcessTaskResult_Successfully)
//                { _result = true; }

//                //Se deben insertar los Operadores de esta tarea...
//                List<Condesus.EMS.Business.DS.Entities.Post> _posts = new List<Condesus.EMS.Business.DS.Entities.Post>();
//                //Agregar la opcion de seleccion de un Site...
//                Condesus.EMS.Business.GIS.Entities.Site _site = null;
//                //Si no hay nada seleccionado, queda en null
//                if (_RtvSite.SelectedNode != null)
//                {
//                    String _entityNameSiteSelected = _RtvSite.SelectedNode.Attributes["SingleEntityName"];
//                    Int64 _idSite = 0;
//                    switch (_entityNameSiteSelected)
//                    {
//                        case Common.ConstantsEntitiesName.DS.Facility:
//                            _idSite = Convert.ToInt64((_RtvSite.SelectedNode == null ? 0 : GetKeyValue(_RtvSite.SelectedNode.Value, "IdFacility")));    //Si queda en cero 0, quiere decir que no asocia.
//                            break;
//                        case Common.ConstantsEntitiesName.DS.Sector:
//                            _idSite = Convert.ToInt64((_RtvSite.SelectedNode == null ? 0 : GetKeyValue(_RtvSite.SelectedNode.Value, "IdSector")));    //Si queda en cero 0, quiere decir que no asocia.
//                            break;
//                    }
//                    _site = (Condesus.EMS.Business.GIS.Entities.Site)EMSLibrary.User.GeographicInformationSystem.Site(_idSite);
//                }
//                //Agregar combo de seleccion de Resource en la pagina
//                Condesus.EMS.Business.KC.Entities.Resource _taskInstruction = null;
//                //Agregar seleccion de NotificationRecipient
//                List<NotificationRecipient> _notificationRecipients = new List<NotificationRecipient>();

//                if (IdValue == 0)
//                {
//                    Condesus.EMS.Business.PF.Entities.ProcessGroupNode _processNodeException = (Condesus.EMS.Business.PF.Entities.ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(IdParentProcess);

//                    //Es un ADD
//                    Condesus.EMS.Business.PF.Entities.ProcessTask oProcessTaskDataRecovery = _processNodeException.ProcessTasksAdd(Convert.ToInt16(txtWeight.Text), base.SetProcessOrder(txtOrder.Text),
//                        txtTitle.Text, txtPurpose.Text, txtDescription.Text, Convert.ToDateTime(rdtStartDate.SelectedDate), Convert.ToDateTime(rdtEndDate.SelectedDate), Convert.ToInt32(txtDuration.Text),
//                        Convert.ToInt32(txtInterval.Text), Convert.ToInt32(txtMaxNumberExecutions.Text), _result, Convert.ToInt32(txtCompleted.Text), _timeUnitDuration, _timeUnitInterval, TypeExecution, _processTaskMeasurement, _measurementDateToRecovery, _posts, _site, _taskInstruction, _notificationRecipients);


//                    IdValue = oProcessTaskDataRecovery.IdProcess;
//                    rdtEndDate.SelectedDate = oProcessTaskDataRecovery.EndDate;
//                }
//                else
//                {
//                    //Es Modify
//                    //((Condesus.EMS.Business.PF.Entities.ProcessGroupNode)_processGroupNode).ProcessTasksModify(IdValue, Convert.ToInt16(txtWeight.Text), base.SetProcessOrder(txtOrder.Text),
//                    //    txtTitle.Text, txtPurpose.Text, txtDescription.Text, _processGroupNode, Convert.ToDateTime(rdtStartDate.SelectedDate), Convert.ToDateTime(rdtEndDate.SelectedDate), Convert.ToInt32(txtDuration.Text),
//                    //    Convert.ToInt32(txtInterval.Text), Convert.ToInt32(txtMaxNumberExecutions.Text), _result, Convert.ToInt32(txtCompleted.Text), _timeUnitDuration, _timeUnitInterval, TypeExecution, _processTaskMeasurement, _measurementDateToRecovery);
//                    //((Condesus.EMS.Business.PF.Entities.ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(IdValue)).ProcessTasksModify(IdValue, Convert.ToInt16(txtWeight.Text), base.SetProcessOrder(txtOrder.Text),
//                    //    txtTitle.Text, txtPurpose.Text, txtDescription.Text, _processGroupNode, Convert.ToDateTime(rdtStartDate.SelectedDate), Convert.ToDateTime(rdtEndDate.SelectedDate), Convert.ToInt32(txtDuration.Text),
//                    //    Convert.ToInt32(txtInterval.Text), Convert.ToInt32(txtMaxNumberExecutions.Text), _result, Convert.ToInt32(txtCompleted.Text), _timeUnitDuration, _timeUnitInterval, TypeExecution, _processTaskMeasurement, _measurementDateToRecovery);
//                    ((Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery)EMSLibrary.User.ProcessFramework.Map.Process(IdValue)).Modify(Convert.ToInt16(txtWeight.Text), base.SetProcessOrder(txtOrder.Text),
//                        txtTitle.Text, txtPurpose.Text, txtDescription.Text, _processGroupNode, Convert.ToDateTime(rdtStartDate.SelectedDate), Convert.ToDateTime(rdtEndDate.SelectedDate), Convert.ToInt32(txtDuration.Text),
//                        Convert.ToInt32(txtInterval.Text), Convert.ToInt32(txtMaxNumberExecutions.Text), _result, Convert.ToInt32(txtCompleted.Text), _timeUnitDuration, _timeUnitInterval, TypeExecution, _processTaskMeasurement, _measurementDateToRecovery, _posts, _site, _taskInstruction, _notificationRecipients);
//                }

//                CheckSecurity();
//                base.StatusBar.ShowMessage(Resources.Common.SaveOK);
//            }
//            catch (Exception ex)
//            {
//                base.StatusBar.ShowMessage(ex);
//            }

//        }
            
//        protected void btnBack_Click(object sender, EventArgs e)
//        {
//            base.Back();
//        }
        
//        protected void btnOkDelete_Click(object sender, EventArgs e)
//            {
//                if (IdValue != 0)
//                {
//                    try
//                    {
//                        Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery _processTaskDataRecovery = (Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery)((Condesus.EMS.Business.PF.Entities.ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(IdParentProcess)).ProcessTask(IdValue);

//                        ((Condesus.EMS.Business.PF.Entities.ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(IdParentProcess)).ProcessTasksRemove(_processTaskDataRecovery);

//                        //limpia los campos
//                        IdValue = 0;

//                        Add();

//                        base.StatusBar.ShowMessage(Resources.Common.DeleteOK);

//                    }
//                    catch (Exception ex)
//                    {
//                        base.StatusBar.ShowMessage(ex);
//                    }
//                    //oculta el popup de confirmacion del delete.
//                    this.mpelbDelete.Hide();
//                }
//            }

//        protected void btnTransferAdd_Click(object sender, EventArgs e)
//        {
//            Navigator();

//            Context.Items.Add("IdOrganization", IdOrganization);
//            Context.Items.Add("Id", IdValue);
//            Server.Transfer("~/DS/FunctionalAreasLanguages.aspx");
//        }

//        private void DisableInsertData(Boolean bAction)
//        {
//            txtTitle.ReadOnly = bAction;
//            txtOrder.ReadOnly = bAction;
//            txtPurpose.ReadOnly = bAction;
//            txtDescription.ReadOnly = bAction;
//            txtWeight.ReadOnly = bAction;
//            rdtStartDate.Enabled = !bAction;
//            txtDuration.ReadOnly = bAction;
//            txtCompleted.ReadOnly = bAction;
//            ddlTimeUnitDuration.Enabled = !bAction;

//            if (bAction)
//            { btnSave.Style.Add("display", "none"); }    //Esta todo en read only no se puede grabar
//            else
//            { btnSave.Style.Add("display", "block"); }   //esta habilitado para grabar.
//        }
        
//        private void Add()
//        {

//            txtTitle.Text = String.Empty;
//            txtOrder.Text = "0";
//            txtPurpose.Text = String.Empty;
//            txtDescription.Text = String.Empty;
//            txtWeight.Text = String.Empty;
//            rdtStartDate.SelectedDate = DateTime.Now;
//            rdtEndDate.SelectedDate = DateTime.Now;
//            txtDuration.Text = String.Empty;
//            txtInterval.Text = String.Empty;
//            txtMaxNumberExecutions.Text = String.Empty;
//            lblResultValue.Text = String.Empty;

//            txtCompleted.Text = String.Empty;
//            ddlTimeUnitDuration.SelectedValue = "0";
//            ddlTimeUnitInterval.SelectedValue = "0";

//            rblOptionTypeExecution.Items[0].Selected = false;
//            rblOptionTypeExecution.Items[1].Selected = false;
//            rblOptionTypeExecution.Items[2].Selected = true;

//            DisableInsertData(false);

//            ((RadMenuItem)rmnGeneralOptions.FindControl("m0")).Enabled = false; //Add
//            ((RadMenuItem)rmnGeneralOptions.FindControl("m1")).Enabled = false; //Language
//            ((RadMenuItem)rmnGeneralOptions.FindControl("m2")).Enabled = false; //Delete
//        }
        
//        private void Edit()
//        {
//            //Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery _processTaskDataRecovery = (Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery)((Condesus.EMS.Business.PF.Entities.ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(IdParentProcess)).ProcessTask(IdValue);
//            Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery _processTaskDataRecovery = (Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery)((Condesus.EMS.Business.PF.Entities.ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(IdParentProcess)).ProcessTask(IdValue);

//            base.PageTitle = _processTaskDataRecovery.LanguageOption.Title;

//            txtTitle.Text = _processTaskDataRecovery.LanguageOption.Title;
//            txtOrder.Text = _processTaskDataRecovery.OrderNumber.ToString();
//            txtPurpose.Text = _processTaskDataRecovery.LanguageOption.Purpose;
//            txtDescription.Text = _processTaskDataRecovery.LanguageOption.Description;
//            txtWeight.Text = _processTaskDataRecovery.Weight.ToString();
//            rdtStartDate.SelectedDate = Convert.ToDateTime(_processTaskDataRecovery.StartDate);
//            rdtEndDate.SelectedDate = Convert.ToDateTime(_processTaskDataRecovery.EndDate);
//            txtDuration.Text = _processTaskDataRecovery.Duration.ToString();
//            txtInterval.Text = _processTaskDataRecovery.Interval.ToString();
//            txtMaxNumberExecutions.Text = _processTaskDataRecovery.MaxNumberExecution.ToString();
//            //Si ya esta en 100% ahi se puede mostrar un resultado, sino muestra un waiting...
//            if (_processTaskDataRecovery.Completed == 100)
//            {
//                lblResultValue.Text = _processTaskDataRecovery.Result;
//            }
//            else
//            {
//                //Como todavia no se terminaron de ejecutar todas las ejecuciones... el resultado es waiting...
//                lblResultValue.Text = Resources.ConstantMessage.ProcessTaskResult_Waiting;
//            }

//            txtCompleted.Text = _processTaskDataRecovery.Completed.ToString();
//            ddlTimeUnitDuration.SelectedValue = _processTaskDataRecovery.TimeUnitDuration.ToString();
//            ddlTimeUnitInterval.SelectedValue = _processTaskDataRecovery.TimeUnitInterval.ToString();

//            rblOptionTypeExecution.Items[0].Selected = false;
//            rblOptionTypeExecution.Items[1].Selected = false;
//            rblOptionTypeExecution.Items[2].Selected = true;

//            switch (_processTaskDataRecovery.TypeExecution)
//            {
//                case "Spontaneous":
//                    rblOptionTypeExecution.Items[0].Selected = true;
//                    break;
//                case "Repeatability":
//                    rblOptionTypeExecution.Items[1].Selected = true;
//                    break;
//                case "Scheduler":
//                    rblOptionTypeExecution.Items[2].Selected = true;
//                    break;
//            }

//            DisableInsertData(false);

//            ((RadMenuItem)rmnGeneralOptions.FindControl("m0")).Enabled = true; //Add
//            ((RadMenuItem)rmnGeneralOptions.FindControl("m1")).Enabled = true; //Language
//            ((RadMenuItem)rmnGeneralOptions.FindControl("m2")).Enabled = true; //Delete
//            SetSite();
//        }
        
//        private void View()
//        {
//            Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery _processTaskDataRecovery = (Condesus.EMS.Business.PF.Entities.ProcessTaskDataRecovery)((Condesus.EMS.Business.PF.Entities.ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(IdParentProcess)).ProcessTask(IdValue);

//            base.PageTitle = _processTaskDataRecovery.LanguageOption.Title;

//            txtTitle.Text = _processTaskDataRecovery.LanguageOption.Title;
//            txtOrder.Text = _processTaskDataRecovery.OrderNumber.ToString();
//            txtPurpose.Text = _processTaskDataRecovery.LanguageOption.Purpose;
//            txtDescription.Text = _processTaskDataRecovery.LanguageOption.Description;
//            txtWeight.Text = _processTaskDataRecovery.Weight.ToString();
//            rdtStartDate.SelectedDate = Convert.ToDateTime(_processTaskDataRecovery.StartDate);
//            rdtEndDate.SelectedDate = Convert.ToDateTime(_processTaskDataRecovery.EndDate);
//            txtDuration.Text = _processTaskDataRecovery.Duration.ToString();
//            txtInterval.Text = _processTaskDataRecovery.Interval.ToString();
//            txtMaxNumberExecutions.Text = _processTaskDataRecovery.MaxNumberExecution.ToString();
//            //Si ya esta en 100% ahi se puede mostrar un resultado, sino muestra un waiting...
//            if (_processTaskDataRecovery.Completed == 100)
//            {
//                lblResultValue.Text = _processTaskDataRecovery.Result;
//            }
//            else
//            {
//                //Como todavia no se terminaron de ejecutar todas las ejecuciones... el resultado es waiting...
//                lblResultValue.Text = Resources.ConstantMessage.ProcessTaskResult_Waiting;
//            }
//            txtCompleted.Text = _processTaskDataRecovery.Completed.ToString();
//            ddlTimeUnitDuration.SelectedValue = _processTaskDataRecovery.TimeUnitDuration.ToString();
//            ddlTimeUnitInterval.SelectedValue = _processTaskDataRecovery.TimeUnitInterval.ToString();

//            rblOptionTypeExecution.Items[0].Selected = false;
//            rblOptionTypeExecution.Items[1].Selected = false;
//            rblOptionTypeExecution.Items[2].Selected = true;

//            switch (_processTaskDataRecovery.TypeExecution)
//            {
//                case "Spontaneous":
//                    rblOptionTypeExecution.Items[0].Selected = true;
//                    break;
//                case "Repeatability":
//                    rblOptionTypeExecution.Items[1].Selected = true;
//                    break;
//                case "Scheduler":
//                    rblOptionTypeExecution.Items[2].Selected = true;
//                    break;
//            }

//            DisableInsertData(true);

//            ((RadMenuItem)rmnGeneralOptions.FindControl("m0")).Enabled = true; //Add
//            ((RadMenuItem)rmnGeneralOptions.FindControl("m1")).Enabled = true; //Language
//            ((RadMenuItem)rmnGeneralOptions.FindControl("m2")).Enabled = true; //Delete
//            SetSite();
//        }
//        #endregion

//        #region Rad Menu
//            protected void rmnOption_ItemClick(object sender, RadMenuEventArgs e)
//        {
//            switch (e.Item.ID)
//            {
//                case "m0": //ADD
//                    IdValue = 0;
//                    lblLanguageValue.Text = Global.DefaultLanguage.Name;
//                    ItemOptionSelected = "ADD";
//                    txtTitle.Text = String.Empty;
//                    txtOrder.Text = "0";
//                    txtPurpose.Text = String.Empty;
//                    Add();

//                    base.StatusBar.Clear();

//                    ((RadMenuItem)rmnGeneralOptions.FindControl("m0")).Enabled = false; //Add
//                    ((RadMenuItem)rmnGeneralOptions.FindControl("m1")).Enabled = false; //Language
//                    ((RadMenuItem)rmnGeneralOptions.FindControl("m2")).Enabled = false; //Delete
//                    break;

//                case "m1":  //LANGUAGE  llama a la pagina de lenguajes
//                    //Context.Items.Add("IdOrganization", IdOrganization);
//                    //Context.Items.Add("OrganizationName", OrganizationName);
//                    //Context.Items.Add("Id", IdValue);
//                    //Server.Transfer("~/DS/FunctionalAreasLanguages.aspx");
//                    break;

//                case "m2":  //DELETE    Esta opcion no debe hacer nada aca. Lo hace en el btnOKDelete_Click()
//                    break;

//            }
//        }
//        #endregion
    }
}
