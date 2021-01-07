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
using System.Linq;
using Condesus.EMS.WebUI.ManagementTools.ProcessesMap;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.NT.Entities;
using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.PM
{
    public partial class ProcessTaskOperationsProperties : BasePropertiesTask
    {
        #region Internal Properties
            private RadComboBox _RdcTimeUnitNotification;
            CompareValidator _CvTimeUnitDuration;
            CompareValidator _CvTimeUnitInterval;
            private Int64 _IdParentProcess
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
                }
            }
            private String _TypeExecution
            {
                get { return Convert.ToString(ViewState["typeExecution"]); }
                set { ViewState["typeExecution"] = value.ToString(); }
            }
            private ProcessTaskOperation _Entity = null;
            private ProcessTaskOperation Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = (ProcessTaskOperation)EMSLibrary.User.ProcessFramework.Map.Process(_IdProcessTaskOperation);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private Int64 _IdProcessTaskOperation
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdTask") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdTask")) : 0;
                }
            }
            private RadComboBox _RdcTimeUnitDuration;
            private RadComboBox _RdcTimeUnitInterval;

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
            private RadComboBox _RdcSite;
            private RadTreeView _RtvSite;
            private RadComboBox _RdcResource;
            private RadTreeView _RtvResource;

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

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
                //Le paso el Siguiente a la MasterContentToolbar
                FwMasterPage.ContentNavigatorToolbarFileActionAdd("return onClientClickNextTabProcessTask();", MasterFwContentToolbarAction.Next);
                
                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
                //Si estoy en un alta, no se debe mostrar el boton. (lo pone el javascript, al momento de hacer click en el ultimo tab...)
                ImageButton _ibtnSave = (ImageButton)FwMasterPage.FindControl("MasterFWContentToolbarActionSave");
                if (Entity == null)
                {
                    _ibtnSave.Style.Add("display", "none");
                }

                customvEndDate.ClientValidationFunction = "ValidateDateTimeRangeTaskDuration";
                rtsWizzardTask.OnClientTabSelecting = "onTabWizzardTaskOperationSelecting";
                rtsWizzardTask.OnClientLoad = "onTabWizzardTaskOperationLoad";
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                rblOptionTypeExecution.Items[0].Attributes.Add("onclick", "javascript:RadioButtonChange(this);");
                rblOptionTypeExecution.Items[1].Attributes.Add("onclick", "javascript:RadioButtonChange(this);");
                rblOptionTypeExecution.Items[2].Attributes.Add("onclick", "javascript:RadioButtonChange(this);");

                base.AddTreeViewPosts(ref phPosts, ref _RtvPosts, new RadTreeViewEventHandler(rtvPosts_NodeExpand), new RadTreeViewEventHandler(rtvPosts_NodeCreated), new RadTreeViewEventHandler(rtvPosts_NodeCheck));
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddCombos();
                AddValidators();

                //base.InjectInitializeValidatorsOnTaskOperation(_CvTimeUnitDuration.ClientID, _CvTimeUnitInterval.ClientID, customvEndDate.ClientID, rtsWizzardTask.ClientID);
                base.InjectCheckIndexesTags();
                base.InjectValidateDateTimePicker(rdtStartDate.ClientID, rdtEndDate.ClientID, "TaskDuration");
                base.InjectonTabWizzardTaskOperationSelecting(rfvOptionTypeExecution.ClientID, customvEndDate.ClientID, revDuration.ClientID, rfvDuration.ClientID, rfvInterval.ClientID, revInterval.ClientID, revNumberExec.ClientID, _CvTimeUnitDuration.ClientID, _CvTimeUnitInterval.ClientID, cvTreeView.ClientID);
                base.InjectRadioButtonChangeScheduleTask(_RdcTimeUnitInterval.ClientID, _CvTimeUnitInterval.ClientID, rfvInterval.ClientID, revInterval.ClientID, customvEndDate.ClientID, txtMaxNumberExecutions.ClientID, txtInterval.ClientID, rdtEndDate.ClientID);

                base.InjectonTabWizzardTaskOperationLoad(rfvOptionTypeExecution.ClientID, customvEndDate.ClientID, revDuration.ClientID, rfvDuration.ClientID, rfvInterval.ClientID, revInterval.ClientID, revNumberExec.ClientID, _CvTimeUnitDuration.ClientID, _CvTimeUnitInterval.ClientID, cvTreeView.ClientID);
                base.InjectEnableValidatorsOperation();
                base.InjectOnClientClickNextTabProcessTask(rmpWizzardTask.ClientID, rtsWizzardTask.ClientID);

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                        { Add(); }
                    else
                        { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    base.SetContentTableRowsCss(tblContentForm2);
                    base.SetContentTableRowsCss(tblContentForm3);
                    base.SetContentTableRowsCss(tblContentForm4);
                    base.SetContentTableRowsCss(tblContentForm5);
                    lblLanguageValue.Text = Global.DefaultLanguage.Name;

                    lblProjectTitleValue.Text = EMSLibrary.User.ProcessFramework.Map.Process(_IdParentProcess).Parent.LanguageOption.Title;
                    //lblIdParentValue.Text = EMSLibrary.User.ProcessFramework.Map.Process(_IdParentProcess).LanguageOption.Title;

                    base.LoadTreeViewPosts(ref _RtvPosts);
                    LoadDataEmails();
                }
                else
                {
                    //Si lo que puede obtener de la pagina esta vacio, entonces se queda con lo que ya habia en el viewstate.
                    if (!String.IsNullOrEmpty(Convert.ToString(Request.Form["typeExecution"])))
                    {
                        _TypeExecution = Convert.ToString(Request.Form["typeExecution"]);
                    }
                }

                //Lo hago aca abajo, porque primero se debe setear el TypeExecution!!!
                base.InjectInitializeTaskValidators(rblOptionTypeExecution, _TypeExecution);

            }
            protected override void SetPagetitle()
            {
                try
                {
                    base.PageTitle = (Entity != null) ? Entity.LanguageOption.Title : Resources.CommonListManage.ProcessTaskOperation;
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ProcessTaskOperation;
                rtsWizzardTask.Tabs[0].Text = Resources.CommonListManage.MainData;
                rtsWizzardTask.Tabs[1].Text = Resources.CommonListManage.Scheduler;
                rtsWizzardTask.Tabs[2].Text = Resources.CommonListManage.Operative;
                rtsWizzardTask.Tabs[3].Text = Resources.CommonListManage.TaskOperators;
                rtsWizzardTask.Tabs[4].Text = Resources.CommonListManage.NotificationRecipients;

                rblOptionTypeExecution.Items[0].Text = Resources.CommonListManage.Spontaneous;
                rblOptionTypeExecution.Items[1].Text = Resources.CommonListManage.Recurrent;
                rblOptionTypeExecution.Items[2].Text = Resources.CommonListManage.Scheduled;
                lblCompleted.Text = Resources.CommonListManage.Completed;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblDuration.Text = Resources.CommonListManage.Duration;
                lblEndDate.Text = Resources.CommonListManage.EndDate;
                //lblIdParent.Text = Resources.CommonListManage.ProcessGroupNode;
                lblInterval.Text = Resources.CommonListManage.Interval;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblMaxNumberExecutions.Text = Resources.CommonListManage.MaxNumberExecution;
                lblOrder.Text = Resources.CommonListManage.OrderNumber;
                lblPosts.Text = Resources.CommonListManage.Post;
                lblProjectTitle.Text = Resources.CommonListManage.Process;
                lblPurpose.Text = Resources.CommonListManage.Purpose;
                lblResult.Text = Resources.CommonListManage.Result;
                lblStartDate.Text = Resources.CommonListManage.StartDate;
                lblTask.Text = Resources.CommonListManage.Title;
                lblTimeUnitDuration.Text = Resources.CommonListManage.TimeUnitDuration;
                lblTimeUnitInterval.Text = Resources.CommonListManage.TimeUnitInterval;
                lblWeight.Text = Resources.CommonListManage.Weight;
                lblComment.Text = Resources.CommonListManage.Comment;
                revDuration.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                revInterval.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                revNumberExec.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfvDuration.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfvInterval.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfvOptionTypeExecution.ErrorMessage = Resources.ConstantMessage.ValidationSelectChoice;
                rfvOrder.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                rfvWeight.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rvWeight.ErrorMessage = Resources.ConstantMessage.ValidationWrongFormat;
                customvEndDate.ErrorMessage = Resources.ConstantMessage.ValidationDateFromTo;
                cvTreeView.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                lblSite.Text = Resources.CommonListManage.Site;
                lblResources.Text = Resources.CommonListManage.Resource;

                lblEmails.Text = Resources.CommonListManage.SelectEmailsPersontoNotify;
                lblListEmails.Text = Resources.CommonListManage.UndeclaredEmail;
                revEmails.ErrorMessage = Resources.ConstantMessage.ValidationListEmails;

                lblTimeUnitNotificacion.Text = Resources.CommonListManage.TimeUnitAdvanceNotice;
                lblNotification.Text = Resources.CommonListManage.AdvanceNotice;
            }
            private void AddCombos()
            {
                AddComboTimeUnitNotifications();
                AddComboTimeUnitDurations();
                AddComboTimeUnitIntervals();
                AddComboSites();
                AddComboResources();
                AddTreeViewEmails();
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
            private void SetTimeUnitNotification()
            {
                _RdcTimeUnitNotification.SelectedValue = "IdTimeUnit=" + Entity.TimeUnitAdvanceNotice.ToString();
            }
            private void SetTimeUnitDuration()
            {
                _RdcTimeUnitDuration.SelectedValue = "IdTimeUnit=" + Entity.TimeUnitDuration.ToString();
            }
            private void SetTimeUnitInterval()
            {
                _RdcTimeUnitInterval.SelectedValue = "IdTimeUnit=" + Entity.TimeUnitInterval.ToString();
            }
            private void LoadData()
            {
                //Contruye el LG de TaskCalibration.
                Condesus.EMS.Business.PF.Entities.Process_LG _process_LG = Entity.LanguagesOptions.Item(Global.DefaultLanguage.IdLanguage);

                //Setea el nombre en el PageTitle
                base.PageTitle = _process_LG.Title;

                txtTitle.Text = _process_LG.Title;
                txtOrder.Text = Entity.OrderNumber.ToString();
                txtPurpose.Text = _process_LG.Purpose;
                txtDescription.Text = _process_LG.Description;
                txtWeight.Text = Entity.Weight.ToString();
                rdtStartDate.SelectedDate = Convert.ToDateTime(Entity.StartDate);
                rdtEndDate.SelectedDate = Convert.ToDateTime(Entity.EndDate);
                txtDuration.Text = Entity.Duration.ToString();
                txtInterval.Text = Entity.Interval.ToString();
                txtNotification.Text = Entity.AdvanceNotice.ToString();

                txtMaxNumberExecutions.Text = Entity.MaxNumberExecution.ToString();
                txtCompleted.Text = Entity.Completed.ToString();
                Slider3_BoundControl.Text = Entity.Completed.ToString();
                //Si ya esta en 100% ahi se puede mostrar un resultado, sino muestra un waiting...
                lblResultValue.Text = Entity.Result;

                ArrayList _postAuxiliar = _PostsAux;
                LoadStructPostsAux(ref _postAuxiliar, Entity.ExecutionPermissions());
                _PostsAux = _postAuxiliar;
                SetSite();
                SetResources();

                SetTimeUnitNotification(); 
                SetTimeUnitDuration();
                SetTimeUnitInterval();

                rblOptionTypeExecution.Items[0].Selected = false;
                rblOptionTypeExecution.Items[1].Selected = false;
                rblOptionTypeExecution.Items[2].Selected = false;

                _TypeExecution = Entity.TypeExecution;
                switch (Entity.TypeExecution)
                {
                    case "Spontaneous":
                        rblOptionTypeExecution.Items[0].Selected = true;
                        break;
                    case "Repeatability":
                        rblOptionTypeExecution.Items[1].Selected = true;
                        break;
                    case "Scheduler":
                        rblOptionTypeExecution.Items[2].Selected = true;
                        break;
                }

                //Operation
                txtComment.Text = Entity.Comment;
                LoadStructEmailsAux();
            }
            private void Add()
            {
                base.StatusBar.Clear();

                txtTitle.Text = String.Empty;
                txtOrder.Text = "0";
                txtPurpose.Text = String.Empty;
                txtDescription.Text = String.Empty;
                txtWeight.Text = String.Empty;
                rdtStartDate.SelectedDate = DateTime.Now;
                rdtEndDate.SelectedDate = DateTime.Now;
                txtDuration.Text = String.Empty;
                txtInterval.Text = String.Empty;
                txtMaxNumberExecutions.Text = String.Empty;
                lblResultValue.Text = String.Empty;
                txtCompleted.Text = String.Empty;

                rblOptionTypeExecution.Items[0].Selected = false;
                rblOptionTypeExecution.Items[1].Selected = false;
                rblOptionTypeExecution.Items[2].Selected = false;
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
            private void AddComboSites()
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeSites(ref phSite, ref _RdcSite, ref _RtvSite, new RadTreeViewEventHandler(rtvSite_NodeExpand));
            }
            private void SetSite()
            {
                Condesus.EMS.Business.GIS.Entities.Site _site = Entity.Site;
                if (_site != null)
                {
                    if (_site.GetType().Name == Common.ConstantsEntitiesName.DS.Facility)
                    {
                        //Si el sitio seleccionado es un facility...
                        //Seteamos la organizacion...
                        //Realiza el seteo del parent en el Combo-Tree.
                        Condesus.EMS.Business.DS.Entities.Organization _oganization = Entity.Site.Organization;
                        String _keyValuesElement = "IdOrganization=" + _oganization.IdOrganization.ToString() + "& IdFacility=" + _site.IdFacility.ToString();
                        if (_oganization.Classifications.Count > 0)
                        {
                            String _keyValuesClassification = "IdOrganizationClassification=" + _oganization.Classifications.First().Value.IdOrganizationClassification;
                            SelectItemTreeViewParentElementMapsForSite(_keyValuesClassification, _keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.Organizations, true);
                        }
                        //Ahora busco el facility....
                        SelectItemTreeViewParentForSite(_keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.Facility, Common.ConstantsEntitiesName.DS.Facilities, true);
                    }
                    else
                    {
                        //Si es un sector...se hace un poquito mas complejo, ya que puede estar muy anidado...
                        if (_site.GetType().Name == Common.ConstantsEntitiesName.DS.Sector)
                        {
                            //Casteo al sector!!!
                            Condesus.EMS.Business.GIS.Entities.Sector _sector = (Condesus.EMS.Business.GIS.Entities.Sector)_site;
                            //Tengo que obtener el facility de este sector!!!
                            while (_sector.Parent.GetType().Name == Common.ConstantsEntitiesName.DS.Sector)
                            {
                                _sector = (Condesus.EMS.Business.GIS.Entities.Sector)_sector.Parent;
                            }
                            //Al salir de este while, tengo el facility;
                            Condesus.EMS.Business.GIS.Entities.Facility _facility = (Condesus.EMS.Business.GIS.Entities.Facility)_sector.Parent;
                            //Ahora busco la organizacion y todo el arbol!
                            Condesus.EMS.Business.DS.Entities.Organization _oganization = Entity.Site.Organization;
                            String _keyValuesElement = "IdOrganization=" + _oganization.IdOrganization.ToString() + "& IdFacility=" + _facility.IdFacility.ToString() + "& IdSector=" + _site.IdFacility.ToString();
                            if (_oganization.Classifications.Count > 0)
                            {
                                String _keyValuesClassification = "IdOrganizationClassification=" + _oganization.Classifications.First().Value.IdOrganizationClassification;
                                SelectItemTreeViewParentElementMapsForSite(_keyValuesClassification, _keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.Organizations, true);
                            }
                            //Ahora busco el sector....
                            SelectItemTreeViewParentForSite(_keyValuesElement, ref _RtvSite, ref _RdcSite, Common.ConstantsEntitiesName.DS.Sector, Common.ConstantsEntitiesName.DS.Sectors, true);
                        }
                    }
                }
            }
            private void AddComboResources()
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phResources, ref _RdcResource, ref _RtvResource,
                    Common.ConstantsEntitiesName.KC.ResourceClassifications, Common.ConstantsEntitiesName.KC.ResourcesRoots, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvResources_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);
            }
            private void SetResources()
            {
                if (Entity.TaskInstruction != null)
                {
                    //Realiza el seteo del parent en el Combo-Tree.
                    Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(Entity.TaskInstruction.IdResource);
                    String _keyValuesElement = "IdResource=" + _resource.IdResource.ToString();
                    if (_resource.Classifications.Count > 0)
                    {
                        String _keyValuesClassification = "IdResourceClassification=" + _resource.Classifications.First().Value.IdResourceClassification;
                        SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvResource, ref _RdcResource, Common.ConstantsEntitiesName.KC.ResourceClassification, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, Common.ConstantsEntitiesName.KC.Resources);
                    }
                    else
                    {
                        SelectItemTreeViewParent(_keyValuesElement, ref _RtvResource, ref _RdcResource, Common.ConstantsEntitiesName.KC.Resource, Common.ConstantsEntitiesName.KC.Resources);
                    }
                }
            }
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
            private void LoadStructEmailsAux()
            {
                //Carga de forma inicial todos los id de clasificacion que ya estan cargados en este proyecto.
                _EmailsAux = new ArrayList();
                List<NotificationRecipient> _notificationRecipient = new List<NotificationRecipient>();
                _notificationRecipient = Entity.NotificationRecipient;

                //Ahora recorre todas las clasificaciones que ya tiene asiganadas el indicador, y los guarda en la estructura interna (ArrayList).
                foreach (NotificationRecipient _item in _notificationRecipient)
                {
                    if (_item.GetType().Name == "NotificationRecipientPerson")
                    {
                        NotificationRecipientPerson _notificationRecipientPerson = (NotificationRecipientPerson)_item;
                        _EmailsAux.Add("IdOrganization=" + _notificationRecipientPerson.Person.Organization.IdOrganization.ToString()
                            + "& IdPerson=" + _notificationRecipientPerson.Person.IdPerson.ToString()
                            + "& IdContactEmail=" + _notificationRecipientPerson.ConctactEmail.IdContactEmail.ToString());
                    }
                    else
                    {
                        NotificationRecipientEmail _notificationRecipientEmail = (NotificationRecipientEmail)_item;
                        txtListEmails.Text += _notificationRecipientEmail.Email + ";";
                    }
                }

                if (!String.IsNullOrEmpty(txtListEmails.Text))
                {
                    txtListEmails.Text = txtListEmails.Text.Substring(0, txtListEmails.Text.Length - 1);
                }
            }
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
        #endregion

        #region Page Events
            protected void rtvSite_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpandSites(sender, e, true);
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
            protected void rtvPosts_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                //Limpio los hijos, para no duplicar al abrir y cerrar.
                e.Node.Nodes.Clear();
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                if (e.Node.Attributes["SingleEntityName"] != Common.ConstantsEntitiesName.DS.Organization)
                {
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
                else
                {   //Ya esta en una Organizacion, entonces carga el post!!!
                    BuildGenericDataTable(Common.ConstantsEntitiesName.DS.PostsByOrganization, _params);
                    if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.PostsByOrganization))
                    {
                        if (DataTableListManage[Common.ConstantsEntitiesName.DS.PostsByOrganization].Rows.Count > 0)
                        { e.Node.ExpandMode = TreeNodeExpandMode.ClientSide; }//como queremos mostrar los puestos, para cada organizacion...aca esta...
                        foreach (DataRow _drRecordPosts in DataTableListManage[Common.ConstantsEntitiesName.DS.PostsByOrganization].Rows)
                        {
                            RadTreeNode _nodePost = SetElementMapsNodeTreeView(_drRecordPosts, Common.ConstantsEntitiesName.DS.PostsByOrganization, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                            _nodePost.Checkable = true;
                            e.Node.Nodes.Add(_nodePost);
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
            protected void rtvResources_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                //Limpio los hijos, para no duplicar al abrir y cerrar.
                e.Node.Nodes.Clear();
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                //Primero lo hace sobre las Clasificaciones Hijas...
                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.KC.ResourceClassificationChildren))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.KC.ResourceClassificationChildren].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                        SetExpandMode(_node, Common.ConstantsEntitiesName.KC.ResourceClassificationChildren, true, false);
                    }
                }

                //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                BuildGenericDataTable(Common.ConstantsEntitiesName.KC.Resources, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.KC.Resources))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.KC.Resources].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.KC.Resources, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                    }
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

            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    if (ValidatePostSelected())
                    {
                        //Agregar seleccion de NotificationRecipient
                        //Aqui carga el List<> de los notificationRecipients seleccionados
                        List<NotificationRecipient> _notificationRecipients = GetNotificationRecipients();
                        //Si al menos hay un mail seleccionado, sigo adelante, sino muestra mensaje
                        if (_notificationRecipients.Count > 0)
                        {
                            //Condesus.EMS.Business.PF.Entities.ProcessGroupNode _processGroupNode = (Condesus.EMS.Business.PF.Entities.ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(_IdParentProcess);

                            Int64 _idTimeUnitDuration = Convert.ToInt64(GetKeyValue(_RdcTimeUnitDuration.SelectedValue, "IdTimeUnit"));
                            Int64 _idTimeUnitInterval = Convert.ToInt64(GetKeyValue(_RdcTimeUnitInterval.SelectedValue, "IdTimeUnit"));
                            Condesus.EMS.Business.PF.Entities.TimeUnit _timeUnitDuration = EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(_idTimeUnitDuration);
                            Condesus.EMS.Business.PF.Entities.TimeUnit _timeUnitInterval = EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(_idTimeUnitInterval);
                            TimeUnit _timeUnitNotification = EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(Convert.ToInt64(GetKeyValue(_RdcTimeUnitNotification.SelectedValue, "IdTimeUnit")));

                            if (_timeUnitInterval == null)
                            {
                                _timeUnitInterval = _timeUnitDuration;
                            }
                            Boolean _result = false;
                            if (lblResultValue.Text == Resources.ConstantMessage.ProcessTaskResult_Waiting)
                            { _result = false; }
                            else if (lblResultValue.Text == Resources.ConstantMessage.ProcessTaskResult_Error)
                            { _result = false; }
                            else if (lblResultValue.Text == Resources.ConstantMessage.ProcessTaskResult_Successfully)
                            { _result = true; }

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

                                Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                                Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                                Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_idPosition);
                                Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                                Condesus.EMS.Business.DS.Entities.FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                                Condesus.EMS.Business.DS.Entities.GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                                Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);
                                Condesus.EMS.Business.DS.Entities.Post _post = _organization.Post(_jobTitle, _organization.Person(_idPerson));
                                //Construye el Post y lo agrega al List, que necesita el AddTask
                                _posts.Add(_post);  // (EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Post(_idGeographicArea, _idPosition, _idFunctionalArea, _idPerson));
                            }

                            //Agregar la opcion de seleccion de un Site...
                            Condesus.EMS.Business.GIS.Entities.Site _site = null;
                            //Si no hay nada seleccionado, queda en null
                            if (_RtvSite.SelectedNode != null)
                            {
                                String _entityNameSiteSelected = _RtvSite.SelectedNode.Attributes["SingleEntityName"];
                                Int64 _idSite = 0;
                                switch (_entityNameSiteSelected)
                                {
                                    case Common.ConstantsEntitiesName.DS.Facility:
                                        _idSite = Convert.ToInt64((_RtvSite.SelectedNode == null ? 0 : GetKeyValue(_RtvSite.SelectedNode.Value, "IdFacility")));    //Si queda en cero 0, quiere decir que no asocia.
                                        break;
                                    case Common.ConstantsEntitiesName.DS.Sector:
                                        _idSite = Convert.ToInt64((_RtvSite.SelectedNode == null ? 0 : GetKeyValue(_RtvSite.SelectedNode.Value, "IdSector")));    //Si queda en cero 0, quiere decir que no asocia.
                                        break;
                                }
                                _site = (Condesus.EMS.Business.GIS.Entities.Site)EMSLibrary.User.GeographicInformationSystem.Site(_idSite);
                            }
                            //seleccion de Resource en la pagina
                            //Obtiene el key necesario.
                            Int64 _idResource = 0;
                            Boolean _wrongComboSelected = false;
                            if ((_RtvResource.SelectedNode != null) && (_RtvResource.SelectedNode.Value != Common.Constants.ComboBoxSelectItemValue))
                            {
                                //Con esto me aseguro que se haya seleccionado un resource y no una classificacion.
                                if (_RtvResource.SelectedNode.Value.Contains("IdResource="))
                                {
                                    _idResource = Convert.ToInt64(GetKeyValue(_RtvResource.SelectedNode.Value, "IdResource"));
                                }
                                else
                                {
                                    _wrongComboSelected = true;
                                }
                            }
                            //Si esta en false, quiere decir que seleccionaron correctamente
                            if (!_wrongComboSelected)
                            {
                                Condesus.EMS.Business.KC.Entities.Resource _taskInstruction = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource);
                                ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_IdParentProcess);
                                Int16 _reminder = 0;
                                if (!String.IsNullOrEmpty(txtNotification.Text))
                                {
                                    _reminder = Convert.ToInt16(txtNotification.Text);
                                } 
                                if (Entity == null)
                                {
                                    //Es un ADD
                                    Entity = (ProcessTaskOperation)_processGroupProcess.ProcessTasksAdd(Convert.ToInt16(txtWeight.Text), base.SetProcessOrder(txtOrder.Text),
                                        txtTitle.Text, txtPurpose.Text, txtDescription.Text, Convert.ToDateTime(rdtStartDate.SelectedDate), Convert.ToDateTime(rdtEndDate.SelectedDate), Convert.ToInt32(txtDuration.Text),
                                        Convert.ToInt32(txtInterval.Text), Convert.ToInt32(txtMaxNumberExecutions.Text), _result, Convert.ToInt32(txtCompleted.Text), _timeUnitDuration, _timeUnitInterval, txtComment.Text, _TypeExecution,
                                        _posts, _site, _taskInstruction, _notificationRecipients, _timeUnitNotification, _reminder);
                                }
                                else
                                {
                                    //Es Modify
                                    Entity.Modify(Convert.ToInt16(txtWeight.Text), base.SetProcessOrder(txtOrder.Text),
                                        txtTitle.Text, txtPurpose.Text, txtDescription.Text, _processGroupProcess, Convert.ToDateTime(rdtStartDate.SelectedDate), Convert.ToDateTime(rdtEndDate.SelectedDate), Convert.ToInt32(txtDuration.Text),
                                        Convert.ToInt32(txtInterval.Text), Convert.ToInt32(txtMaxNumberExecutions.Text), _result, Convert.ToInt32(txtCompleted.Text), _timeUnitDuration, _timeUnitInterval, txtComment.Text, _TypeExecution,
                                        _posts, _site, _taskInstruction, _notificationRecipients, _timeUnitNotification, _reminder);
                                }

                                base.NavigatorAddTransferVar("IdProcess", _IdParentProcess);
                                base.NavigatorAddTransferVar("IdTask", Entity.IdProcess);

                                String _pkValues = "IdProcess=" + _IdParentProcess.ToString()
                                    + "& IdTask=" + Entity.IdProcess.ToString();
                                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskOperation);
                                base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskOperation);
                                base.NavigatorAddTransferVar("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ProcessTaskOperation + " " + Entity.LanguageOption.Title, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                                String _entityPropertyName = String.Concat(Entity.LanguageOption.Title);
                                NavigatePropertyEntity(Common.ConstantsEntitiesName.PF.ProcessTaskOperation, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                                base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                            }
                            else
                            {
                                //Caso contrario, no hace nada y muestra un mensaje.
                                base.StatusBar.ShowMessage(Resources.ConstantMessage.msgIncorrectSelectionResource, Pnyx.WebControls.PnyxStatusBar.StatusState.Warning);
                            }
                        }
                        else
                        {
                            base.StatusBar.ShowMessage(Resources.ConstantMessage.SelectAtLeastOnePost, Pnyx.WebControls.PnyxStatusBar.StatusState.Warning);
                        }
                    }
                    else
                    {
                        //No selecciono email
                        base.StatusBar.ShowMessage(Resources.ConstantMessage.SelectAtLeastOneEmail, Pnyx.WebControls.PnyxStatusBar.StatusState.Warning);
                    }
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
            protected void btnNextPageView_Click(object sender, EventArgs e)
            {

                //Si esta en el 1°, pasa al 2°
                if (rpvMainData.Selected)
                {
                    rtsWizzardTask.Tabs[1].Enabled = true;
                    rtsWizzardTask.Tabs[1].Selected = true;

                    rpvScheduler.Selected = true;
                }
                else
                {
                    //Si esta en el 2° pasa al 3°
                    if (rpvScheduler.Selected)
                    {
                        rtsWizzardTask.Tabs[2].Enabled = true;
                        rtsWizzardTask.Tabs[2].Selected = true;

                        rpvOperation.Selected = true;
                    }
                    else
                    {
                        //Si esta en el 3° se queda ahi...
                        if (rpvOperation.Selected)
                        {
                            rtsWizzardTask.Tabs[2].Enabled = true;
                            rtsWizzardTask.Tabs[2].Selected = true;

                            rpvOperation.Selected = true;
                        }
                    }
                }
            }
        #endregion
    }
}
