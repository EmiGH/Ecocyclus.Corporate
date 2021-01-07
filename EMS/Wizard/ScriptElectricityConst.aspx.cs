using System;
using System.Data;
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
using System.Transactions;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.NT.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;

namespace Condesus.EMS.WebUI.Wizard
{
    public partial class ScriptElectricityConst : BasePropertiesTask
    {
        #region Internal Properties
            private RadComboBox _RdcSite;
            private RadTreeView _RtvSite;
            private ArrayList _SitesAux //Estructura interna para guardar los id de emails que son seleccionados.
            {
                get
                {
                    if (ViewState["SitesAux"] == null)
                    {
                        ViewState["SitesAux"] = new ArrayList();
                    }
                    return (ArrayList)ViewState["SitesAux"];
                }
                set { ViewState["SitesAux"] = value; }
            }

            private Int64 _IdOrganization
            {
                get
                {
                    if (ViewState["IdOrganization"] == null)
                    {
                        ViewState["IdOrganization"] = 0;
                    }
                    return Convert.ToInt64(ViewState["IdOrganization"]);
                }
                set { ViewState["IdOrganization"] = value; }
            }
            private RadComboBox _RdcOrganization;
            private RadTreeView _RtvOrganization;
            private CompareValidator _CvOrganization;

            #region Constant
                private RadComboBox _RdcConst_EFRed;
                private RadTreeView _RtvConstant_EFRed;
            #endregion

            #region Info Process
                private RadComboBox _RdcProcess;
                private RadComboBox _RdcTimeUnitNotification;
                private CompareValidator _CvTimeUnitDuration;
                private CompareValidator _CvTimeUnitInterval;
                private RadComboBox _RdcTimeUnitDuration;
                private RadComboBox _RdcTimeUnitInterval;
                private CompareValidator _CvTimeUnitFrequency;
                private RadComboBox _RdcTimeUnitFrequency;

                private RadComboBox _RdcAccountingActivity;
                private RadTreeView _RtvAccountingActivity;
                private RadComboBox _RdcAccountingScope;
                private CompareValidator _CvAccountingActivity;
                private CompareValidator _CvAccountingScope;

                private RadTreeView _RtvPosts;
                private RadComboBox _RdcPosts;
                private ArrayList _PostsAux //Estructura interna para guardar los id de posts que son seleccionados.
                {
                    get
                    {
                        if (ViewState["PostsAux"] == null)
                        {
                            ViewState["PostsAux"] = new ArrayList();
                        }
                        return (ArrayList)ViewState["PostsAux"];
                    }
                    set { ViewState["PostsAux"] = value; }
                }
                private RadTreeView _RtvEmails;
                private ArrayList _EmailsAux //Estructura interna para guardar los id de emails que son seleccionados.
                {
                    get
                    {
                        if (ViewState["EmailsAux"] == null)
                        {
                            ViewState["EmailsAux"] = new ArrayList();
                        }
                        return (ArrayList)ViewState["EmailsAux"];
                    }
                    set { ViewState["EmailsAux"] = value; }
                }
            #endregion
       
        #endregion

        #region PageLoad & Init
            protected override void InyectJavaScript()
            {
                base.InjectCheckIndexesTags();
            }
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                AddComboPosts();

            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddComboProcess();
                AddComboOrganizations();
                AddComboSitesByFacilityType(0);
                AddComboConst_FactorEmisionRed();

                AddComboTimeUnitNotifications();
                AddComboTimeUnitDurations();
                AddComboTimeUnitIntervals();
                AddComboTimeUnitFrequency();
                AddComboAccountingActivities();
                AddComboAccountingScope();

                AddTreeViewEmails();

                AddValidators();

                base.InjectCheckIndexesTags();
                base.InjectValidateDateTimePicker(rdtStartDate.ClientID, rdtEndDate.ClientID, "TaskDuration");


                if (!Page.IsPostBack)
                {
                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    this.txtName.Focus();

                    BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationClassifications, new Dictionary<String, Object>());
                    base.LoadTreeViewPosts(ref _RtvPosts);
                    LoadDataEmails();
                    LoadData();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonListManage.ScriptElectricity;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadData()
            {
                Int64 _idIndicatorElectricity = 28; //Para DEMO         //18;//Para GHREE                     //18->Electricity
                Int64 _idNoConditionElectricity = 10;//Para DEMO        //18; //Para GHREE                   //18->No Condition	S/D
                Int64 _idMagnitud = 7;                                  //7->Energy
                Int64 _idMeasurementUnitKWh = 103;                      //103->kwh	Kilovatio hora

                Indicator _indicator=EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicatorElectricity);

                lblIndicatorValue.Text = Common.Functions.ReplaceIndexesTags(_indicator.LanguageOption.Name);
                lblParameterGroupValue.Text = _indicator.ParameterGroup(_idNoConditionElectricity).LanguageOption.Name;
                lblMeasurementUnitValue.Text = Common.Functions.ReplaceIndexesTags(EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud).MeasurementUnit(_idMeasurementUnitKWh).LanguageOption.Name);
            }
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ScriptElectricity;
                lblDescription.Text = Resources.CommonListManage.Description;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
            }
            private void AddComboProcess()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phProcess, ref _RdcProcess, Common.ConstantsEntitiesName.PF.ProcessGroupProcessesWithoutClassification, String.Empty, _params, false, true, false, false, false);
            }
            private void AddComboSitesByFacilityType(Int64 idOrganization)
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);

                AddComboTreeSitesByType(ref phSite, ref _RdcSite, ref _RtvSite, new RadTreeViewEventHandler(rtvSite_NodeExpand), _IdOrganization);

                _RtvSite.NodeCheck += new RadTreeViewEventHandler(rtvSite_NodeCheck);
                if (_IdOrganization == 0)
                {
                    _RdcSite.Enabled = false;
                }
                else
                {
                    _RdcSite.Enabled = true;
                }
            }
            private void AddValidators()
            {
                ValidatorRequiredField(Common.ConstantsEntitiesName.PF.TimeUnits, phTimeUnitDurationValidator, ref _CvTimeUnitDuration, _RdcTimeUnitDuration, Resources.ConstantMessage.SelectATimeUnit);
                ValidatorRequiredField(Common.ConstantsEntitiesName.PF.TimeUnits, phTimeUnitIntervalValidator, ref _CvTimeUnitInterval, _RdcTimeUnitInterval, Resources.ConstantMessage.SelectATimeUnit);
            }
            private void AddComboTimeUnitNotifications()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phTimeUnitNotification, ref _RdcTimeUnitNotification, Common.ConstantsEntitiesName.PF.TimeUnits, String.Empty, _params, false, true, false, false, false);
                //Esto es porque en esta pagina se cargan 2 combos de timeunit...entonces a uno le cambio el nombre...
                _RdcTimeUnitNotification.ID = "rdcTimeUnitNotifications";
            }
            private void AddComboTimeUnitDurations()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phTimeUnitDuration, ref _RdcTimeUnitDuration, Common.ConstantsEntitiesName.PF.TimeUnits, String.Empty, _params, false, true, false, false, false);
                //Esto es porque en esta pagina se cargan 2 combos de timeunit...entonces a uno le cambio el nombre...
                _RdcTimeUnitDuration.ID = "rdcTimeUnitDurations";
            }
            private void AddComboTimeUnitIntervals()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phTimeUnitInterval, ref _RdcTimeUnitInterval, Common.ConstantsEntitiesName.PF.TimeUnits, String.Empty, _params, false, true, false, false, false);
            }
            private void AddComboTimeUnitFrequency()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phTimeUnitFrequency, ref _RdcTimeUnitFrequency, Common.ConstantsEntitiesName.PF.TimeUnits, String.Empty, _params, false, true, false, false, false);
                //Esto es porque en esta pagina se cargan 3 combos de timeunit...entonces a uno le cambio el nombre...
                _RdcTimeUnitFrequency.ID = "rdcTimeUnitFrequency";

                ValidatorRequiredField(Common.ConstantsEntitiesName.PF.TimeUnits, phTimeUnitFrequencyValidator, ref _CvTimeUnitFrequency, _RdcTimeUnitFrequency, Resources.ConstantMessage.SelectATimeUnit);
            }
            private void AddComboAccountingActivities()
            {
                String _filterExpression = String.Empty;
                //Combo de AccountingActivity Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTree(phAccountingActivity, ref _RdcAccountingActivity, ref _RtvAccountingActivity,
                    Common.ConstantsEntitiesName.PA.AccountingActivities, _params, false, false, true, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand), Resources.Common.ComboBoxNoDependency, false);

                //ValidatorRequiredField(Common.ConstantsEntitiesName.PA.AccountingActivities, phAccountingActivityValidator, ref _CvAccountingActivity, _RdcAccountingActivity, Resources.ConstantMessage.SelectAAccountingActivity);
            }
            private void AddComboAccountingScope()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();

                String _selectedValue = String.Empty;
                AddCombo(phAccountingScope, ref _RdcAccountingScope, Common.ConstantsEntitiesName.PA.AccountingScopes, _selectedValue, _params, false, true, false, false, false);

                //ValidatorRequiredField(Common.ConstantsEntitiesName.PA.AccountingScopes, phAccountingScopeValidator, ref _CvAccountingScope, _RdcAccountingScope, Resources.ConstantMessage.SelectAAccountingScope);
            }
            private void AddComboConst_FactorEmisionRed()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstEFRed, ref _RdcConst_EFRed, ref _RtvConstant_EFRed, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboOrganizations()
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phOrganization, ref _RdcOrganization, ref _RtvOrganization,
                    Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationsRoots, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvOrganizations_NodeExpand),
                    new RadTreeViewEventHandler(rtvOrganizations_NodeClick),
                    Resources.Common.ComboBoxNoDependency, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, phOrganizationValidator, ref _CvOrganization, _RdcOrganization, Resources.ConstantMessage.SelectAnOrganization);
            }

            #region Operador y Notificacion
                private void AddTreeViewEmails()
                {
                    //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    BuildGenericDataTable(Common.ConstantsEntitiesName.DS.AllSystemPersonEmails, _params);

                    //Arma tree con todos los roots.
                    phEmails.Controls.Clear();
                    //Uso un tree, porque es mas comodo y mas lindo visiblemente, pero esta entidad, no tiene jerarquia, ya que el adapter entrega todos los emails y personas plano.
                    _RtvEmails = base.BuildHierarchicalListManageContent(Common.ConstantsEntitiesName.DS.AllSystemPersonEmails, "Form");
                    //Ya tengo el Tree le attacho el Handlers
                    _RtvEmails.NodeCreated += new RadTreeViewEventHandler(_RtvEmails_NodeCreated);
                    _RtvEmails.NodeCheck += new RadTreeViewEventHandler(_RtvEmails_NodeCheck);
                    phEmails.Controls.Add(_RtvEmails);
                }
                /// <summary>
                /// Este metodo se encarga solamente de cargar los datos dentro del TreeView que ya debe existir en la pagina (que se crea en el init)
                /// </summary>
                private void LoadDataEmails()
                {
                    _RtvEmails.Nodes.Clear();
                    //Con el tree ya armado, ahora hay que llenarlo con datos.
                    base.LoadGenericTreeView(ref _RtvEmails, Common.ConstantsEntitiesName.DS.AllSystemPersonEmails, Common.ConstantsEntitiesName.DS.AllSystemPersonEmails, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty, false, false);
                }
                private void AddComboPosts()
                {
                    base.AddTreeViewPosts(ref phPosts, ref _RtvPosts, new RadTreeViewEventHandler(rtvPosts_NodeExpand), new RadTreeViewEventHandler(rtvPosts_NodeCreated), new RadTreeViewEventHandler(rtvPosts_NodeCheck));
                }
                private Boolean ValidatePostSelected()
                {
                    //Verifica si hay al menos un puesto seleccionado.
                    if (_PostsAux.Count > 0)
                    {
                        return true;
                    }
                    //Si NO hay puestos asociados, entonces retorna falso.
                    return false;
                }
            #endregion

            #region Task Add
                private List<NotificationRecipient> GetNotificationRecipients()
                {
                    //Agregar seleccion de NotificationRecipient
                    List<NotificationRecipient> _notificationRecipients = new List<NotificationRecipient>();
                    NotificationRecipientEmail _notificationRecipientEmail;
                    NotificationRecipientPerson _notificationRecipientPerson;
                    Person _person = null;
                    ContactEmail _contactEmail = null;


                    //Aca recorre el ArrayList con los id que fueron chequeado, y contruye un List, para pasar al ADD.
                    foreach (String _item in _EmailsAux)
                    {
                        Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_item, "IdOrganization"));
                        Int64 _idPerson = Convert.ToInt64(GetKeyValue(_item, "IdPerson"));
                        Int64 _idContactEmail = Convert.ToInt64(GetKeyValue(_item, "IdContactEmail"));

                        _person = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Person(_idPerson);
                        _contactEmail = _person.ContactEmail(_idContactEmail);
                        _notificationRecipientPerson = new NotificationRecipientPerson(_person, _contactEmail);

                        _notificationRecipients.Add(_notificationRecipientPerson);
                    }
                    if (!String.IsNullOrEmpty(txtListEmails.Text))
                    {
                        //Ahora recorre los emails sueltos que haya podido agregar el usuario-
                        String _listEmails = txtListEmails.Text.Replace(",", ";"); //la lista viene delimitada con ";" o ",". y pasamos todo a ;
                        for (int i = 0; i < _listEmails.Split(';').Length; i++)
                        {
                            String _email = _listEmails.Split(';')[i];  //Obtiene el email
                            //Construye el objeto para construir el list.
                            _notificationRecipientEmail = new NotificationRecipientEmail(_email);
                            //Finalmente agrega el email al List.
                            _notificationRecipients.Add(_notificationRecipientEmail);
                        }
                    }

                    return _notificationRecipients;
                }
                private ProcessTaskMeasurement MeasurementTaskAdd(Boolean isCumulative, Site site, Int64 idIndicator, Int64 idParameterGroup, Int64 idMeasurementUnit, String name, String description)
                {
                    ProcessTaskMeasurement _taskMeasurement = null;

                    if (ValidatePostSelected())
                    {
                        //Agregar seleccion de NotificationRecipient
                        //Aqui carga el List<> de los notificationRecipients seleccionados
                        List<NotificationRecipient> _notificationRecipients = GetNotificationRecipients();
                        //Si al menos hay un mail seleccionado, sigo adelante, sino muestra mensaje
                        if (_notificationRecipients.Count > 0)
                        {
                            Int64 _idTimeUnitDuration = Convert.ToInt64(GetKeyValue(_RdcTimeUnitDuration.SelectedValue, "IdTimeUnit"));
                            Int64 _idTimeUnitInterval = Convert.ToInt64(GetKeyValue(_RdcTimeUnitInterval.SelectedValue, "IdTimeUnit"));
                            TimeUnit _timeUnitDuration = EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(_idTimeUnitDuration);
                            TimeUnit _timeUnitInterval = EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(_idTimeUnitInterval);
                            TimeUnit _timeUnitNotification = EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(Convert.ToInt64(GetKeyValue(_RdcTimeUnitNotification.SelectedValue, "IdTimeUnit")));

                            //Para los Motores no hay equipo de medicion
                            MeasurementDevice _measurementDevice = null;
                            //IndicatorClassification _indicatorClassification = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(idIndicatorClassification);
                            Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(idIndicator);
                            ParameterGroup _parameterGroup = _indicator.ParameterGroup(idParameterGroup);
                            //Agregamos el unico parametro al List
                            List<ParameterGroup> _parameterGroups = new List<ParameterGroup>();
                            _parameterGroups.Add(_parameterGroup);

                            TimeUnit _timeUnitFrequency = EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(Convert.ToInt64(GetKeyValue(_RdcTimeUnitFrequency.SelectedValue, "IdTimeUnit")));
                            MeasurementUnit _measurementUnit = _indicator.Magnitud.MeasurementUnit(idMeasurementUnit);

                            if (_timeUnitInterval == null)
                            {
                                _timeUnitInterval = _timeUnitDuration;
                            }
                            Boolean _result = true;
                            Boolean _isRegressive = true;
                            Boolean _isRelevant = true;

                            //Se deben insertar los Operadores de esta tarea...
                            List<Condesus.EMS.Business.DS.Entities.Post> _posts = new List<Condesus.EMS.Business.DS.Entities.Post>();
                            //Aca recorre el ArrayList con los id que fueron chequeado, y contruye un Dictionary, para pasar al ADD.
                            foreach (String _item in _PostsAux)
                            {
                                Int64 _idGeographicArea = Convert.ToInt64(GetKeyValue(_item, "IdGeographicArea"));
                                Int64 _idPosition = Convert.ToInt64(GetKeyValue(_item, "IdPosition"));
                                Int64 _idFunctionalArea = Convert.ToInt64(GetKeyValue(_item, "IdFunctionalArea"));
                                Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_item, "IdOrganization"));
                                Int64 _idPerson = Convert.ToInt64(GetKeyValue(_item, "IdPerson"));
                                //Construye el Post y lo agrega al List, que necesita el AddTask
                                Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                                Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                                Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_idPosition);
                                Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                                Condesus.EMS.Business.DS.Entities.FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                                Condesus.EMS.Business.DS.Entities.GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                                Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);

                                _posts.Add(_organization.Post(_jobTitle, _organization.Person(_idPerson)));
                            }
                            //Para este wizzard no hay Resources!!!
                            Condesus.EMS.Business.KC.Entities.Resource _taskInstruction = null;

                            //Todo esto va por defecto vacio!!
                            String _reference = String.Empty;
                            String _measurementSource = String.Empty;
                            String _measurementFrequencyAtSource = String.Empty;
                            Decimal _measurementUncertainty = 0;
                            Quality _measurementQuality = null;
                            Methodology _measurementMethodology = null;

                            //Actividad y Scope salen de seleccion!!!
                            Int64 _idActivity = Convert.ToInt64(GetKeyValue(_RtvAccountingActivity.SelectedNode.Value, "IdActivity"));
                            Int64 _idScope = Convert.ToInt64(GetKeyValue(_RdcAccountingScope.SelectedValue, "IdScope"));
                            AccountingActivity _accountingActivity = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingActivity(_idActivity);
                            AccountingScope _accountingScope = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(_idScope);

                            ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(Convert.ToInt64(GetKeyValue(_RdcProcess.SelectedValue, "IdProcess")));

                            Int16 _reminder = 0;
                            if (!String.IsNullOrEmpty(txtNotification.Text))
                            {
                                _reminder = Convert.ToInt16(txtNotification.Text);
                            }
                            //Siempre es ADD!!!
                            //Al grabar la tarea, se graba la medicion, esta dentro de una transaccion y todo...
                            _taskMeasurement = (ProcessTaskMeasurement)_processGroupProcess.ProcessTasksAdd(_measurementDevice, _parameterGroups,
                                _indicator, name, description, _timeUnitFrequency, Convert.ToInt32(txtFrequency.Text),
                                _measurementUnit, _isRegressive, _isRelevant, 0, base.SetProcessOrder("0"), name,
                                description, description, Convert.ToDateTime(rdtStartDate.SelectedDate),
                                Convert.ToDateTime(rdtEndDate.SelectedDate), Convert.ToInt32(txtDuration.Text),
                                Convert.ToInt32(txtInterval.Text), 0, _result, 0, _timeUnitDuration, _timeUnitInterval, "Repeatability",
                                _posts, site, _taskInstruction, _measurementSource, _measurementFrequencyAtSource, _measurementUncertainty,
                                _measurementQuality, _measurementMethodology, _accountingScope, _accountingActivity, _reference,
                                _notificationRecipients, _timeUnitNotification, _reminder);

                            //Agregamos el idioma Español a la tarea y la medicion!
                            _taskMeasurement.LanguagesOptions.Add("es-AR", txtNameEsAR.Text, String.Empty, description);
                            _taskMeasurement.Measurement.LanguagesOptions.Add("es-AR", txtNameEsAR.Text, description);
                        }
                        else
                        {
                            //No selecciono email
                            base.StatusBar.ShowMessage(Resources.ConstantMessage.SelectAtLeastOneEmail, Pnyx.WebControls.PnyxStatusBar.StatusState.Warning);
                        }
                    }
                    else
                    {
                        //No selecciono un Post
                        base.StatusBar.ShowMessage(Resources.ConstantMessage.SelectAtLeastOnePost, Pnyx.WebControls.PnyxStatusBar.StatusState.Warning);
                    }

                    return _taskMeasurement;
                }
            #endregion

            #region Transformation Add
                private Boolean ValidationFormulaVariable(String formula, Dictionary<String, Condesus.EMS.Business.IOperand> operands)
                {
                    //Se verifica que las variables que se usan en la formula esten definidas como operadores.
                    if (!String.IsNullOrEmpty(formula))
                    {
                        //Ver de armar una lista de las funciones que soporta el formulador...
                        //Char[] _charArrayFormula = formula.ToUpper().Replace("BASE", String.Empty).Replace("LOG", String.Empty).Replace("(", String.Empty).Replace(")", String.Empty).ToCharArray();
                        Char[] _charArrayFormula = ReplaceIntrinsicFormula(formula);

                        for (int i = 0; i < _charArrayFormula.Length; i++)
                        {
                            if (Char.IsLetter(_charArrayFormula[i]))
                            {
                                if (!operands.ContainsKey(_charArrayFormula[i].ToString()))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    return true;
                }
                private Char[] ReplaceIntrinsicFormula(String formula)
                {
                    //Se debe reemplazar las palabras claves que son formulas predefinidas para el formulador...
                    //El primero lo saca de la formula y despues va sacando de la nueva variable.
                    String _formulaWithoutIntrinsic = formula.ToUpper().Replace("(", String.Empty).Replace(")", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("ABS", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("ACOS", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("ASIN", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("ATAN", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("ATAN2", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("BIGMULL", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("CEILING", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("COS", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("COSH", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("DIVREM", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("EXP", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("FLOOR", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("IEEEREMAINDER", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("LOG", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("LOG10", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("MAX", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("MIN", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("POW", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("ROUND", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("SIGN", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("SIN", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("SINH", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("SQRT", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("TAN", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("TANH", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("TRUNCATE", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("IF", String.Empty);
                    //CUSTOM FUNCTION
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("EF_DRILLINGCH4", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("EF_DRILLINGC2H6", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("EF_DRILLINGC3H8", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("EF_DRILLINGC4H10", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("EF_DRILLINGH2S", String.Empty);
                    _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("EF_DRILLINGCO2", String.Empty);

                    //Por ultimo saco la palabra clave BASE que es interna nuestra para la serie de datos base que se va a usar.
                    Char[] _charArrayFormula = _formulaWithoutIntrinsic.ToUpper().Replace("BASE", String.Empty).ToCharArray();

                    //Retorna el array de caracteres sin las palabras claves.
                    return _charArrayFormula;
                }


                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idConstantClass_EFRed= Convert.ToInt64(GetKeyValue(_RtvConstant_EFRed.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstant_EFRed = Convert.ToInt64(GetKeyValue(_RtvConstant_EFRed.SelectedNode.Value, "IdConstant"));

                    _operands.Add("A", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_EFRed).Constant(_idConstant_EFRed));

                    return _operands;
                }

                private CalculateOfTransformation TransformationAdd(Int64 idIndicator, Int64 idMeasurementUnit,
                    Measurement measurement, CalculateOfTransformation calculateOfTransformation, Dictionary<String,
                    Condesus.EMS.Business.IOperand> operands, String name, String description, String formula)
                {
                    CalculateOfTransformation _calculateOfTransformation = null;

                    List<NotificationRecipient> _notificationRecipients = GetNotificationRecipients();
                    //Si al menos hay un mail seleccionado, sigo adelante, sino muestra mensaje
                    if (_notificationRecipients.Count > 0)
                    {
                        //Obtiene Indicator
                        Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(idIndicator);
                        //Obtiene Measurement Unit
                        MeasurementUnit _measurementUnit = _indicator.Magnitud.MeasurementUnit(idMeasurementUnit);
                        Int64 _idMagnitud = _indicator.Magnitud.IdMagnitud;
                        Int64 _idActivity = Convert.ToInt64(GetKeyValue(_RtvAccountingActivity.SelectedNode.Value, "IdActivity"));

                        if (ValidationFormulaVariable(formula, operands))
                        {
                            AccountingActivity _accountingActivity = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingActivity(_idActivity);
                            //Si este objeto esta en null es una transformacion de una medicion, sino es de una transformacion de transformacion.
                            if (calculateOfTransformation != null)
                            {
                                //Es un ADD desde una Transformacion
                                _calculateOfTransformation = calculateOfTransformation.TransformationAdd(_indicator, _measurementUnit, formula, name, description, _accountingActivity, operands, _notificationRecipients);
                            }
                            else
                            {
                                //Es un ADD desde una medicion
                                _calculateOfTransformation = measurement.TransformationAdd(_indicator, _measurementUnit, formula, name, description, _accountingActivity, operands, _notificationRecipients);
                            }
                        }
                        else
                        {
                            //En la formula usan una variable no definida.
                            base.StatusBar.ShowMessage(Resources.ConstantMessage.VariableNotDefinedAsParameter, Pnyx.WebControls.PnyxStatusBar.StatusState.Warning);
                        }
                    }
                    else
                    {
                        //No selecciono email
                        base.StatusBar.ShowMessage(Resources.ConstantMessage.SelectAtLeastOneEmail, Pnyx.WebControls.PnyxStatusBar.StatusState.Warning);
                    }

                    return _calculateOfTransformation;
                }

            #endregion

            private Facility GetFacilityRoot(Int64 idSite)
            {
                Site _site = EMSLibrary.User.GeographicInformationSystem.Site(idSite);
                if (_site.GetType().Name == "Facility")
                {
                    return (Facility)_site;
                }
                else
                {
                    return GetFacilityRoot(((Sector)_site).Parent.IdFacility);
                }
            }
        #endregion

        #region Page Events
            protected void rtvConst_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpandConstants(sender, e);
            }
            protected void rtvOrganizations_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                //Limpio los hijos, para no duplicar al abrir y cerrar.
                e.Node.Nodes.Clear();
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                //Primero lo hace sobre las Clasificaciones Hijas...
                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                        SetExpandMode(_node, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, true, false);
                    }
                }

                //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Organizations, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.Organizations))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.Organizations].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Organizations, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                    }
                }
            }
            protected void rtvOrganizations_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                _IdOrganization = Convert.ToInt64(GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));

                AddComboSitesByFacilityType(_IdOrganization);
            }
            protected void rtvPosts_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                //Limpio los hijos, para no duplicar al abrir y cerrar.
                e.Node.Nodes.Clear();
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                //Primero lo hace sobre las Clasificaciones Hijas...
                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                        SetExpandMode(_node, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, true, false);
                    }
                }

                //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Organizations, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.Organizations))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.Organizations].Rows)
                    {
                        //RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Organizations, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        RadTreeNode _node = SetGenericNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Organizations, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, true, false);
                        e.Node.Nodes.Add(_node);
                        //Si ya existe la organizacion, la actualiza, sino la agrega.
                        if (_params.ContainsKey("IdOrganization"))
                        {
                            _params.Remove("IdOrganization");
                            _params.Add("IdOrganization", _drRecord["IdOrganization"].ToString());
                        }
                        else
                        {
                            _params.Add("IdOrganization", _drRecord["IdOrganization"].ToString());
                        }

                        BuildGenericDataTable(Common.ConstantsEntitiesName.DS.PostsByOrganization, _params);
                        if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.PostsByOrganization))
                        {
                            if (DataTableListManage[Common.ConstantsEntitiesName.DS.PostsByOrganization].Rows.Count > 0)
                            { _node.ExpandMode = TreeNodeExpandMode.ClientSide; }//como queremos mostrar los puestos, para cada organizacion...aca esta...
                            foreach (DataRow _drRecordPosts in DataTableListManage[Common.ConstantsEntitiesName.DS.PostsByOrganization].Rows)
                            {
                                RadTreeNode _nodePost = SetElementMapsNodeTreeView(_drRecordPosts, Common.ConstantsEntitiesName.DS.PostsByOrganization, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                                _nodePost.Checkable = true;
                                _node.Nodes.Add(_nodePost);

                            }
                        }
                    }
                }
            }
            protected void rtvPosts_NodeCreated(object sender, RadTreeNodeEventArgs e)
            {
                if (_PostsAux.Contains(e.Node.Value))
                {
                    e.Node.Checked = true;
                }
                else
                {
                    e.Node.Checked = false;
                }
            }
            protected void rtvPosts_NodeCheck(object sender, RadTreeNodeEventArgs e)
            {
                RadTreeNode _node = e.Node;
                if (_PostsAux.Contains(_node.Value))
                {
                    if (!_node.Checked)
                    {
                        _PostsAux.Remove(_node.Value);
                    }
                }
                else
                {
                    _PostsAux.Add(_node.Value);
                }
            }
            protected void cvTreeView_ServerValidate(object source, ServerValidateEventArgs args)
            {
                if (_PostsAux.Count > 0)
                {
                    args.IsValid = true;
                }
                else
                {
                    args.IsValid = false;
                    Response.End();
                }
            }
            protected void _RtvEmails_NodeCreated(object sender, RadTreeNodeEventArgs e)
            {
                if (_EmailsAux.Contains(e.Node.Value))
                {
                    e.Node.Checked = true;
                }
                else
                {
                    e.Node.Checked = false;
                }
            }
            protected void _RtvEmails_NodeCheck(object sender, RadTreeNodeEventArgs e)
            {
                RadTreeNode _node = e.Node;

                if (_EmailsAux.Contains(_node.Value))
                {
                    if (!_node.Checked)
                    {
                        _EmailsAux.Remove(_node.Value);
                    }
                }
                else
                {
                    _EmailsAux.Add(_node.Value);
                }
            }
            protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.PA.AccountingActivitiesChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }
            protected void rtvSite_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                //Int64 _idProcess = Convert.ToInt64(GetKeyValue(_RdcProcess.SelectedValue, "IdProcess"));
                //Int64 _idOrganization = ((ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).IdOrganization;

                NodeExpandSitesByType(sender, e, true, true, _IdOrganization);
            }
            protected void rtvSite_NodeCheck(object sender, RadTreeNodeEventArgs e)
            {
                RadTreeNode _node = e.Node;

                if (_SitesAux.Contains(_node.Value))
                {
                    if (!_node.Checked)
                    {
                        _SitesAux.Remove(_node.Value);
                    }
                }
                else
                {
                    _SitesAux.Add(_node.Value);
                }
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    Int64 _idIndicatorElectricity = 28; //Para DEMO         //18;//Para GHREE                     //18->Electricity
                    Int64 _idNoConditionElectricity = 10;//Para DEMO        //18;//Para GHREE                //18->No Condition	S/D
                    Int64 _idMeasurementUnitKWh = 103;                      //103->kwh	Kilovatio hora

                    //Construye el Scope de la transaccion (todo lo que este dentro va en transaccion)
                    using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                    {
                        //1-Por cada site Seleccionado:
                        //Aca recorre el ArrayList con los id que fueron chequeado
                        foreach (String _item in _SitesAux)
                        {
                            //Obtiene los Id del Site seleccionado
                            Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_item, "IdOrganization"));
                            Int64 _idFacility = Convert.ToInt64(GetKeyValue(_item, "IdFacility"));
                            Int64 _idSector = Convert.ToInt64(GetKeyValue(_item, "IdSector"));
                            Int64 _idSite = _idSector == 0 ? _idFacility : _idSector;

                            #region Tarea Medicion BASE
                                //Construye el Site seleccionado y los demas datos para la Tarea de Medición Base!
                                Site _site = EMSLibrary.User.GeographicInformationSystem.Site(_idSite);
                                //El Root del Facility no cambia, es el mismo para todo
                                //String _facilityRootName = GetFacilityRoot(_site.IdFacility).LanguageOption.Name;
                                //String _measurementTaskBaseName = _facilityRootName + " - " + _site.LanguageOption.Name + " - " + txtName.Text;
                                String _measurementTaskBaseName = _site.LanguageOption.Name + " - " + txtName.Text;

                                //  a- Alta de la Tarea de medicion base
                                ProcessTaskMeasurement _taskMeasurementBase = MeasurementTaskAdd(true, _site, _idIndicatorElectricity, _idNoConditionElectricity, _idMeasurementUnitKWh, _measurementTaskBaseName, txtDescription.Text);
                            #endregion
                            
                            //--------------TRANSFORMACIONES---------------------------
                            Int64 _idMeasurementUnitTon = 213;   // 213->t (EE.UU.)	Tone (EE.UU.)
                            Int64 _idIndicatorCO2e = 37;//Para DEMO     //22; Para Ghree     //1->CO__2e__	CO__2__e
                            //Int64 _idMeasurement_EFRed = Convert.ToInt64(GetKeyValue(_RdcMeasurement_EFRed.SelectedValue, "IdMeasurement"));

                            #region   b- Alta de Transformacion Hija de Medicion Base:
                                String _transformationName = _site.LanguageOption.Name + " - Conversión a CO__2e__";
                                CalculateOfTransformation _calculateOfTransformation_1 = TransformationAdd(_idIndicatorCO2e, _idMeasurementUnitTon,
                                    _taskMeasurementBase.Measurement, null, BuildOperatorForTransformation(), _transformationName, txtDescription.Text, "Base/1000*A");
                            #endregion

                        }

                        //Finaliza la transaccion
                        _transactionScope.Complete();
                    }
                    
                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
        #endregion
    }
}
