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
    public partial class ScriptEngine : BasePropertiesTask
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


            #region Constants
                private RadComboBox _RdcConstantA;
                private RadTreeView _RtvConstantA;
                private RadComboBox _RdcConstantB;
                private RadTreeView _RtvConstantB;
                private RadComboBox _RdcConstantC;
                private RadTreeView _RtvConstantC;
                private RadComboBox _RdcConstantD;
                private RadTreeView _RtvConstantD;
                private RadComboBox _RdcConstantE;
                private RadTreeView _RtvConstantE;
                private RadComboBox _RdcConstantF;
                private RadTreeView _RtvConstantF;
                private RadComboBox _RdcConstantG;
                private RadTreeView _RtvConstantG;
                private RadComboBox _RdcConstantH;
                private RadTreeView _RtvConstantH;
                private RadComboBox _RdcConstantI;
                private RadTreeView _RtvConstantI;
            #endregion

            #region Measurements
                private RadComboBox _RdcMeasurementJ;
                private RadComboBox _RdcMeasurementK;
                private RadComboBox _RdcMeasurementL;
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
                private CompareValidator _CvIndicator;
                private RadComboBox _RdcIndicator;
                private RadTreeView _RtvIndicator;
                private CompareValidator _CvParameterGroup;
                private RadComboBox _RdcParameterGroup;
                private CompareValidator _CvMeasurementUnit;
                private RadComboBox _RdcMeasurementUnit;

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

            #region Indicator for Transformation
                private CompareValidator _CvIndicatorCO2e;
                private RadComboBox _RdcIndicatorCO2e;
                private RadTreeView _RtvIndicatorCO2e;
                private CompareValidator _CvIndicatorCO2efromCO2;
                private RadComboBox _RdcIndicatorCO2efromCO2;
                private RadTreeView _RtvIndicatorCO2efromCO2;
                private CompareValidator _CvIndicatorCO2;
                private RadComboBox _RdcIndicatorCO2;
                private RadTreeView _RtvIndicatorCO2;
                private CompareValidator _CvIndicatorCH4;
                private RadComboBox _RdcIndicatorCH4;
                private RadTreeView _RtvIndicatorCH4;
                private CompareValidator _CvIndicatorCO2efromCH4;
                private RadComboBox _RdcIndicatorCO2efromCH4;
                private RadTreeView _RtvIndicatorCO2efromCH4;
                private CompareValidator _CvIndicatorCO2efromN2O;
                private RadComboBox _RdcIndicatorCO2efromN2O;
                private RadTreeView _RtvIndicatorCO2efromN2O;
                private CompareValidator _CvIndicatorN2O;
                private RadComboBox _RdcIndicatorN2O;
                private RadTreeView _RtvIndicatorN2O;
                private CompareValidator _CvIndicatorSO2;
                private RadComboBox _RdcIndicatorSO2;
                private RadTreeView _RtvIndicatorSO2;
                private CompareValidator _CvIndicatorCO;
                private RadComboBox _RdcIndicatorCO;
                private RadTreeView _RtvIndicatorCO;
                private CompareValidator _CvIndicatorNOx;
                private RadComboBox _RdcIndicatorNOx;
                private RadTreeView _RtvIndicatorNOx;
                private CompareValidator _CvIndicatorHCT;
                private RadComboBox _RdcIndicatorHCT;
                private RadTreeView _RtvIndicatorHCT;
                private CompareValidator _CvIndicatorHCNM;
                private RadComboBox _RdcIndicatorHCNM;
                private RadTreeView _RtvIndicatorHCNM;
                private CompareValidator _CvIndicatorMP10;
                private RadComboBox _RdcIndicatorMP10;
                private RadTreeView _RtvIndicatorMP10;
            #endregion

            #region Measurement Unit for Transformation
                private RadTreeView _RtvMeasurementUnitMg;
                private RadComboBox _RdcMeasurementUnitMg;
                private RadTreeView _RtvMeasurementUnitgGj;
                private RadComboBox _RdcMeasurementUnitgGj;
            #endregion

            #region Parameter Group For Accessory Task
                private RadComboBox _RdcParameterGroupCO;
                private RadComboBox _RdcParameterGroupMP10;
                private RadComboBox _RdcParameterGroupNOx;
                private RadComboBox _RdcParameterGroupN2O;
                private RadComboBox _RdcParameterGroupHCT;
                private RadComboBox _RdcParameterGroupCH4;
                private RadComboBox _RdcParameterGroupHCNM;
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
                AddComboSitesByFacilityType();
                AddComboConstantA();
                AddComboConstantB();
                AddComboConstantC();
                AddComboConstantD();
                AddComboConstantE();
                AddComboConstantF();
                AddComboConstantG();
                AddComboConstantH();
                AddComboConstantI();
                AddComboMeasurementJ();
                AddComboMeasurementK();
                AddComboMeasurementL();


                AddComboTimeUnitNotifications();
                AddComboTimeUnitDurations();
                AddComboTimeUnitIntervals();
                AddComboTimeUnitFrequency();
                AddComboAccountingActivities();
                AddComboAccountingScope();


                AddTreeViewEmails();
                AddComboIndicators();
                AddComboParameterGroups();
                AddComboMeasurementUnits();

                AddComboIndicatorsCH4();
                AddComboIndicatorsCO();
                AddComboIndicatorsCO2();
                AddComboIndicatorsCO2e();
                AddComboIndicatorsCO2efromCH4();
                AddComboIndicatorsCO2efromCO2();
                AddComboIndicatorsCO2efromN2O();
                AddComboIndicatorsHCNM();
                AddComboIndicatorsHCT();
                AddComboIndicatorsMP10();
                AddComboIndicatorsN2O();
                AddComboIndicatorsNOx();
                AddComboIndicatorsSO2();

                AddComboMeasurementUnitsMg();
                AddComboMeasurementUnitsgGj();

                AddComboParameterGroupsCH4();
                AddComboParameterGroupsCO();
                AddComboParameterGroupsHCNM();
                AddComboParameterGroupsHCT();
                AddComboParameterGroupsMP10();
                AddComboParameterGroupsN2O();
                AddComboParameterGroupsNOx();

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

                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonListManage.ScriptEngine;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Quality;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                lblDescription.Text = Resources.CommonListManage.Description;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;

            }
            private void AddComboProcess()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phProcess, ref _RdcProcess, Common.ConstantsEntitiesName.PF.ProcessGroupProcessesWithoutClassification, String.Empty, _params, false, true, false, false, false);
            }
            private void AddComboSitesByFacilityType()
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeSitesByType(ref phSite, ref _RdcSite, ref _RtvSite, new RadTreeViewEventHandler(rtvSite_NodeExpand),0);

                _RtvSite.NodeCheck += new RadTreeViewEventHandler(rtvSite_NodeCheck);
            }
            private void AddComboConstantA()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstA, ref _RdcConstantA, ref _RtvConstantA, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConstantB()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstB, ref _RdcConstantB, ref _RtvConstantB, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConstantC()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstC, ref _RdcConstantC, ref _RtvConstantC, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConstantD()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstD, ref _RdcConstantD, ref _RtvConstantD, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConstantE()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstE, ref _RdcConstantE, ref _RtvConstantE, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConstantF()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstF, ref _RdcConstantF, ref _RtvConstantF, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConstantG()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstG, ref _RdcConstantG, ref _RtvConstantG, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConstantH()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstH, ref _RdcConstantH, ref _RtvConstantH, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConstantI()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstI, ref _RdcConstantI, ref _RtvConstantI, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboMeasurementJ()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (_params.ContainsKey("IdProcess"))
                { _params.Remove("IdProcess"); }
                _params.Add("IdProcess", Convert.ToInt64(GetKeyValue(_RdcProcess.SelectedValue, "IdProcess")));

                AddCombo(phMeasurementJ, ref _RdcMeasurementJ, Common.ConstantsEntitiesName.PA.Measurements, String.Empty, _params, false, true, false, false, false);
                //Esto es porque en esta pagina se cargan 2 combos de timeunit...entonces a uno le cambio el nombre...
                _RdcMeasurementJ.ID = "rdcMeasurementJ";
            }
            private void AddComboMeasurementK()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (_params.ContainsKey("IdProcess"))
                { _params.Remove("IdProcess"); }
                _params.Add("IdProcess", Convert.ToInt64(GetKeyValue(_RdcProcess.SelectedValue, "IdProcess")));

                AddCombo(phMeasurementK, ref _RdcMeasurementK, Common.ConstantsEntitiesName.PA.Measurements, String.Empty, _params, false, true, false, false, false);
                //Esto es porque en esta pagina se cargan 2 combos de timeunit...entonces a uno le cambio el nombre...
                _RdcMeasurementK.ID = "rdcMeasurementK";
            }
            private void AddComboMeasurementL()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (_params.ContainsKey("IdProcess"))
                { _params.Remove("IdProcess"); }
                _params.Add("IdProcess", Convert.ToInt64(GetKeyValue(_RdcProcess.SelectedValue, "IdProcess")));

                AddCombo(phMeasurementL, ref _RdcMeasurementL, Common.ConstantsEntitiesName.PA.Measurements, String.Empty, _params, false, true, false, false, false);
                //Esto es porque en esta pagina se cargan 2 combos de timeunit...entonces a uno le cambio el nombre...
                _RdcMeasurementL.ID = "rdcMeasurementL";
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
            private void AddComboIndicators()
            {
                String _filterExpression = String.Empty;
                //Combo de Indicator
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phIndicator, ref _RdcIndicator, ref _RtvIndicator,
                    Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                    new RadTreeViewEventHandler(rtvIndicators_NodeClick),
                    Resources.Common.ComboBoxNoDependency, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorValidator, ref _CvIndicator, _RdcIndicator, Resources.ConstantMessage.SelectAIndicator);
            }
            private void AddComboParameterGroups()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (_RtvIndicator.SelectedNode != null)
                {
                    if (GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator") != null)
                    {
                        _params.Add("IdIndicator", Convert.ToInt64(GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator")));
                    }
                }
                AddCombo(phParameterGroup, ref _RdcParameterGroup, Common.ConstantsEntitiesName.PA.ParameterGroups, String.Empty, _params, false, true, false, true, false);
                _RdcParameterGroup.CausesValidation = true;

                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.ParameterGroups, phParameterGroupValidator, ref _CvParameterGroup, _RdcParameterGroup, Resources.ConstantMessage.SelectAParameterGroup);
            }
            private void AddComboMeasurementUnits()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (_RtvIndicator.SelectedNode != null)
                {
                    if (GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator") != null)
                    {
                        Int64 _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator"));
                        _params.Add("IdMagnitud", EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).Magnitud.IdMagnitud);
                    }
                }
                AddCombo(phMeasurementUnit, ref _RdcMeasurementUnit, Common.ConstantsEntitiesName.PA.MeasurementUnits, String.Empty, _params, false, true, false, true, false);
                _RdcMeasurementUnit.CausesValidation = true;

                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.MeasurementUnits, phMeasurementUnitValidator, ref _CvMeasurementUnit, _RdcMeasurementUnit, Resources.ConstantMessage.SelectAMeasurementUnit);
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

            #region Indicator for Transformation
                private void AddComboIndicatorsCO2e()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorCO2e, ref _RdcIndicatorCO2e, ref _RtvIndicatorCO2e,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand), Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorCO2eValidator, ref _CvIndicatorCO2e, _RdcIndicatorCO2e, Resources.ConstantMessage.SelectAIndicator);
                }
                private void AddComboIndicatorsCO2efromCO2()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorCO2efromCO2, ref _RdcIndicatorCO2efromCO2, ref _RtvIndicatorCO2efromCO2,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                        Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorCO2efromCO2Validator, ref _CvIndicatorCO2efromCO2, _RdcIndicatorCO2efromCO2, Resources.ConstantMessage.SelectAIndicator);
                }
                private void AddComboIndicatorsCO2()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorCO2, ref _RdcIndicatorCO2, ref _RtvIndicatorCO2,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                        Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorCO2Validator, ref _CvIndicatorCO2, _RdcIndicatorCO2, Resources.ConstantMessage.SelectAIndicator);
                }
                private void AddComboIndicatorsCH4()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorCH4, ref _RdcIndicatorCH4, ref _RtvIndicatorCH4,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                        new RadTreeViewEventHandler(_RtvIndicatorCH4_NodeClick),
                        Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorCH4Validator, ref _CvIndicatorCH4, _RdcIndicatorCH4, Resources.ConstantMessage.SelectAIndicator);
                }
                private void AddComboIndicatorsCO2efromCH4()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorCO2eFromCH4, ref _RdcIndicatorCO2efromCH4, ref _RtvIndicatorCO2efromCH4,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                        Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorCO2eFromCH4Validator, ref _CvIndicatorCO2efromCH4, _RdcIndicatorCO2efromCH4, Resources.ConstantMessage.SelectAIndicator);
                }
                private void AddComboIndicatorsCO2efromN2O()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorCO2efromN2O, ref _RdcIndicatorCO2efromN2O, ref _RtvIndicatorCO2efromN2O,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                        Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorCO2efromN2OValidator, ref _CvIndicatorCO2efromN2O, _RdcIndicatorCO2efromN2O, Resources.ConstantMessage.SelectAIndicator);
                }
                private void AddComboIndicatorsN2O()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorN2O, ref _RdcIndicatorN2O, ref _RtvIndicatorN2O,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                        new RadTreeViewEventHandler(_RtvIndicatorN2O_NodeClick),
                        Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorN2OValidator, ref _CvIndicatorN2O, _RdcIndicatorN2O, Resources.ConstantMessage.SelectAIndicator);
                }
                private void AddComboIndicatorsSO2()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorSO2, ref _RdcIndicatorSO2, ref _RtvIndicatorSO2,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                        Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorSO2Validator, ref _CvIndicatorSO2, _RdcIndicatorSO2, Resources.ConstantMessage.SelectAIndicator);
                }
                private void AddComboIndicatorsCO()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorCO, ref _RdcIndicatorCO, ref _RtvIndicatorCO,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                        new RadTreeViewEventHandler(_RtvIndicatorCO_NodeClick),
                        Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorCOValidator, ref _CvIndicatorCO, _RdcIndicatorCO, Resources.ConstantMessage.SelectAIndicator);
                }
                private void AddComboIndicatorsNOx()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorNOx, ref _RdcIndicatorNOx, ref _RtvIndicatorNOx,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                        new RadTreeViewEventHandler(_RtvIndicatorNOx_NodeClick),
                        Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorNOxValidator, ref _CvIndicatorNOx, _RdcIndicatorNOx, Resources.ConstantMessage.SelectAIndicator);
                }
                private void AddComboIndicatorsHCT()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorHCT, ref _RdcIndicatorHCT, ref _RtvIndicatorHCT,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                        new RadTreeViewEventHandler(_RtvIndicatorHCT_NodeClick),
                        Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorHCTValidator, ref _CvIndicatorHCT, _RdcIndicatorHCT, Resources.ConstantMessage.SelectAIndicator);
                }
                private void AddComboIndicatorsHCNM()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorHCNM, ref _RdcIndicatorHCNM, ref _RtvIndicatorHCNM,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                        new RadTreeViewEventHandler(_RtvIndicatorHCNM_NodeClick),
                        Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorHCNMValidator, ref _CvIndicatorHCNM, _RdcIndicatorHCNM, Resources.ConstantMessage.SelectAIndicator);
                }
                private void AddComboIndicatorsMP10()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phIndicatorMP10, ref _RdcIndicatorMP10, ref _RtvIndicatorMP10,
                        Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorsRoots, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvIndicators_NodeExpand),
                        new RadTreeViewEventHandler(_RtvIndicatorMP10_NodeClick),
                        Resources.Common.ComboBoxNoDependency, false);

                    ValidatorRequiredField(Common.ConstantsEntitiesName.PA.IndicatorClassifications, phIndicatorMP10Validator, ref _CvIndicatorMP10, _RdcIndicatorMP10, Resources.ConstantMessage.SelectAIndicator);
                }
            #endregion

            #region Measurement Unit result of Trasnformation
                private void AddComboMeasurementUnitsMg()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phMeasurementUnitMg, ref _RdcMeasurementUnitMg, ref _RtvMeasurementUnitMg,
                        Common.ConstantsEntitiesName.PA.Magnitudes, Common.ConstantsEntitiesName.PA.MeasurementUnits, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(_RtvMeasurementUnit_NodeExpand), Resources.Common.ComboBoxNoDependency, false);
                }
                private void AddComboMeasurementUnitsgGj()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Indicator
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTreeElementMaps(ref phMeasurementUnitgGj, ref _RdcMeasurementUnitgGj, ref _RtvMeasurementUnitgGj,
                        Common.ConstantsEntitiesName.PA.Magnitudes, Common.ConstantsEntitiesName.PA.MeasurementUnits, _params, false, true, false, ref _filterExpression,
                        new RadTreeViewEventHandler(_RtvMeasurementUnit_NodeExpand), Resources.Common.ComboBoxNoDependency, false);
                }
            #endregion

            #region Parameter Group For Accessory Task
                private void AddComboParameterGroupsCO()
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    if (_RtvIndicatorCO.SelectedNode != null)
                    {
                        if (GetKeyValue(_RtvIndicatorCO.SelectedNode.Value, "IdIndicator") != null)
                        {
                            _params.Add("IdIndicator", Convert.ToInt64(GetKeyValue(_RtvIndicatorCO.SelectedNode.Value, "IdIndicator")));
                        }
                    }
                    AddCombo(phParameterGroupCO, ref _RdcParameterGroupCO, Common.ConstantsEntitiesName.PA.ParameterGroups, String.Empty, _params, false, true, false, false, false);
                    _RdcParameterGroupCO.CausesValidation = true;
                }
                private void AddComboParameterGroupsMP10()
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    if (_RtvIndicatorMP10.SelectedNode != null)
                    {
                        if (GetKeyValue(_RtvIndicatorMP10.SelectedNode.Value, "IdIndicator") != null)
                        {
                            _params.Add("IdIndicator", Convert.ToInt64(GetKeyValue(_RtvIndicatorMP10.SelectedNode.Value, "IdIndicator")));
                        }
                    }
                    AddCombo(phParameterGroupMP10, ref _RdcParameterGroupMP10, Common.ConstantsEntitiesName.PA.ParameterGroups, String.Empty, _params, false, true, false, false, false);
                    _RdcParameterGroupMP10.CausesValidation = true;
                }
                private void AddComboParameterGroupsNOx()
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    if (_RtvIndicatorNOx.SelectedNode != null)
                    {
                        if (GetKeyValue(_RtvIndicatorNOx.SelectedNode.Value, "IdIndicator") != null)
                        {
                            _params.Add("IdIndicator", Convert.ToInt64(GetKeyValue(_RtvIndicatorNOx.SelectedNode.Value, "IdIndicator")));
                        }
                    }
                    AddCombo(phParameterGroupNOx, ref _RdcParameterGroupNOx, Common.ConstantsEntitiesName.PA.ParameterGroups, String.Empty, _params, false, true, false, false, false);
                    _RdcParameterGroupNOx.CausesValidation = true;
                }
                private void AddComboParameterGroupsN2O()
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    if (_RtvIndicatorN2O.SelectedNode != null)
                    {
                        if (GetKeyValue(_RtvIndicatorN2O.SelectedNode.Value, "IdIndicator") != null)
                        {
                            _params.Add("IdIndicator", Convert.ToInt64(GetKeyValue(_RtvIndicatorN2O.SelectedNode.Value, "IdIndicator")));
                        }
                    }
                    AddCombo(phParameterGroupN2O, ref _RdcParameterGroupN2O, Common.ConstantsEntitiesName.PA.ParameterGroups, String.Empty, _params, false, true, false, false, false);
                    _RdcParameterGroupN2O.CausesValidation = true;
                }
                private void AddComboParameterGroupsHCT()
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    if (_RtvIndicatorHCT.SelectedNode != null)
                    {
                        if (GetKeyValue(_RtvIndicatorHCT.SelectedNode.Value, "IdIndicator") != null)
                        {
                            _params.Add("IdIndicator", Convert.ToInt64(GetKeyValue(_RtvIndicatorHCT.SelectedNode.Value, "IdIndicator")));
                        }
                    }
                    AddCombo(phParameterGroupHCT, ref _RdcParameterGroupHCT, Common.ConstantsEntitiesName.PA.ParameterGroups, String.Empty, _params, false, true, false, false, false);
                    _RdcParameterGroupHCT.CausesValidation = true;
                }
                private void AddComboParameterGroupsCH4()
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    if (_RtvIndicatorCH4.SelectedNode != null)
                    {
                        if (GetKeyValue(_RtvIndicatorCH4.SelectedNode.Value, "IdIndicator") != null)
                        {
                            _params.Add("IdIndicator", Convert.ToInt64(GetKeyValue(_RtvIndicatorCH4.SelectedNode.Value, "IdIndicator")));
                        }
                    }
                    AddCombo(phParameterGroupCH4, ref _RdcParameterGroupCH4, Common.ConstantsEntitiesName.PA.ParameterGroups, String.Empty, _params, false, true, false, false, false);
                    _RdcParameterGroupCH4.CausesValidation = true;
                }
                private void AddComboParameterGroupsHCNM()
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    if (_RtvIndicatorHCNM.SelectedNode != null)
                    {
                        if (GetKeyValue(_RtvIndicatorHCNM.SelectedNode.Value, "IdIndicator") != null)
                        {
                            _params.Add("IdIndicator", Convert.ToInt64(GetKeyValue(_RtvIndicatorHCNM.SelectedNode.Value, "IdIndicator")));
                        }
                    }
                    AddCombo(phParameterGroupHCNM, ref _RdcParameterGroupHCNM, Common.ConstantsEntitiesName.PA.ParameterGroups, String.Empty, _params, false, true, false, false, false);
                    _RdcParameterGroupHCNM.CausesValidation = true;
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
                            IndicatorClassification _indicatorClassification = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(Convert.ToInt64(GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicatorClassification")));
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


                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_T()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idMeasurement_J = Convert.ToInt64(GetKeyValue(_RdcMeasurementJ.SelectedValue, "IdMeasurement"));
                    Int64 _idConstantClass_A = Convert.ToInt64(GetKeyValue(_RtvConstantA.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstantClass_C = Convert.ToInt64(GetKeyValue(_RtvConstantC.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstantClass_D = Convert.ToInt64(GetKeyValue(_RtvConstantD.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstant_A = Convert.ToInt64(GetKeyValue(_RtvConstantA.SelectedNode.Value, "IdConstant"));
                    Int64 _idConstant_C = Convert.ToInt64(GetKeyValue(_RtvConstantC.SelectedNode.Value, "IdConstant"));
                    Int64 _idConstant_D = Convert.ToInt64(GetKeyValue(_RtvConstantD.SelectedNode.Value, "IdConstant"));

                    _operands.Add("J", EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement_J));
                    _operands.Add("A", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_A).Constant(_idConstant_A));
                    _operands.Add("C", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_C).Constant(_idConstant_C));
                    _operands.Add("D", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_D).Constant(_idConstant_D));

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_U()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idConstantClass_G = Convert.ToInt64(GetKeyValue(_RtvConstantG.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstant_G = Convert.ToInt64(GetKeyValue(_RtvConstantG.SelectedNode.Value, "IdConstant"));

                    _operands.Add("G", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_G).Constant(_idConstant_G));

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_V()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    //No tiene Operadores!!!
                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_W(ProcessTaskMeasurement _taskMeasurement_R)
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idMeasurement_L = Convert.ToInt64(GetKeyValue(_RdcMeasurementL.SelectedValue, "IdMeasurement"));

                    _operands.Add("L", EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement_L));
                    _operands.Add("R", _taskMeasurement_R.Measurement);

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_X()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idConstantClass_H = Convert.ToInt64(GetKeyValue(_RtvConstantH.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstant_H = Convert.ToInt64(GetKeyValue(_RtvConstantH.SelectedNode.Value, "IdConstant"));

                    _operands.Add("H", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_H).Constant(_idConstant_H));

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_Y()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    //No tiene Operadores
                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_Z(ProcessTaskMeasurement _taskMeasurement_P)
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idMeasurement_L = Convert.ToInt64(GetKeyValue(_RdcMeasurementL.SelectedValue, "IdMeasurement"));

                    _operands.Add("L", EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement_L));
                    _operands.Add("P", _taskMeasurement_P.Measurement);

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_AA()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idConstantClass_I = Convert.ToInt64(GetKeyValue(_RdcConstantI.SelectedValue, "IdConstantClassification"));
                    Int64 _idConstant_I = Convert.ToInt64(GetKeyValue(_RdcConstantI.SelectedValue, "IdConstant"));

                    _operands.Add("I", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_I).Constant(_idConstant_I));

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_AB()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    //No Tiene Operadores
                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_AC()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idMeasurement_K = Convert.ToInt64(GetKeyValue(_RdcMeasurementK.SelectedValue, "IdMeasurement"));
                    Int64 _idConstantClass_B = Convert.ToInt64(GetKeyValue(_RdcConstantB.SelectedValue, "IdConstantClassification"));
                    Int64 _idConstant_B = Convert.ToInt64(GetKeyValue(_RdcConstantB.SelectedValue, "IdConstant"));
                    Int64 _idConstantClass_E = Convert.ToInt64(GetKeyValue(_RdcConstantE.SelectedValue, "IdConstantClassification"));
                    Int64 _idConstant_E = Convert.ToInt64(GetKeyValue(_RdcConstantE.SelectedValue, "IdConstant"));
                    Int64 _idConstantClass_F = Convert.ToInt64(GetKeyValue(_RdcConstantF.SelectedValue, "IdConstantClassification"));
                    Int64 _idConstant_F = Convert.ToInt64(GetKeyValue(_RdcConstantF.SelectedValue, "IdConstant"));

                    _operands.Add("K", EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement_K));
                    _operands.Add("B", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_B).Constant(_idConstant_B));
                    _operands.Add("E", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_E).Constant(_idConstant_E));
                    _operands.Add("F", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_F).Constant(_idConstant_F));

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_AD(ProcessTaskMeasurement _taskMeasurement_M)
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idMeasurement_L = Convert.ToInt64(GetKeyValue(_RdcMeasurementL.SelectedValue, "IdMeasurement"));

                    _operands.Add("L", EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement_L));
                    _operands.Add("M", _taskMeasurement_M.Measurement);

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_AE(ProcessTaskMeasurement _taskMeasurement_N)
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idMeasurement_L = Convert.ToInt64(GetKeyValue(_RdcMeasurementL.SelectedValue, "IdMeasurement"));

                    _operands.Add("L", EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement_L));
                    _operands.Add("N", _taskMeasurement_N.Measurement);

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_AF(ProcessTaskMeasurement _taskMeasurement_O)
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idMeasurement_L = Convert.ToInt64(GetKeyValue(_RdcMeasurementL.SelectedValue, "IdMeasurement"));

                    _operands.Add("L", EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement_L));
                    _operands.Add("O", _taskMeasurement_O.Measurement);

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_AG(ProcessTaskMeasurement _taskMeasurement_Q)
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idMeasurement_L = Convert.ToInt64(GetKeyValue(_RdcMeasurementL.SelectedValue, "IdMeasurement"));

                    _operands.Add("L", EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement_L));
                    _operands.Add("Q", _taskMeasurement_Q.Measurement);

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation_AH(ProcessTaskMeasurement _taskMeasurement_S)
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idMeasurement_L = Convert.ToInt64(GetKeyValue(_RdcMeasurementL.SelectedValue, "IdMeasurement"));

                    _operands.Add("L", EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement_L));
                    _operands.Add("S", _taskMeasurement_S.Measurement);

                    return _operands;
                }


                //private void BuildOperators()
                //{
                //    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();
                //    //Aca recorre el ArrayList con los id que fueron chequeado, y contruye un Dictionary, para pasar al ADD.
                //    foreach (KeyValuePair<String, String> _item in _Operands)
                //    {
                //        Condesus.EMS.Business.IOperand _operand = null;
                //        if (_item.Value.Contains("IdTransformation"))
                //        {
                //            //Esta es una transformacion
                //            _operand = _Measurement.Transformation(Convert.ToInt64(GetKeyValue(_item.Value, "IdTransformation")));
                //        }
                //        else
                //        {
                //            if (_item.Value.Contains("IdMeasurement"))
                //            {
                //                //Esta es medicion
                //                _operand = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(Convert.ToInt64(GetKeyValue(_item.Value, "IdMeasurement")));
                //            }
                //            else
                //            {
                //                if (_item.Value.Contains("IdConstant"))
                //                {
                //                    //Esta es una constante
                //                    _operand = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(Convert.ToInt64(GetKeyValue(_item.Value, "IdConstantClassification"))).Constant(Convert.ToInt64(GetKeyValue(_item.Value, "IdConstant")));
                //                }
                //            }
                //        }
                //        //construye el diccionario de operadores con el key = letra y el operador = medicion,constante o transformacion.
                //        _operands.Add(_item.Key, _operand);
                //    }
                //}
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
            protected void _RtvMeasurementUnit_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                base.NodeExpand(sender, e, Common.ConstantsEntitiesName.PA.MeasurementUnits, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }
            //protected void _RtvMeasurementUnit_NodeCreated(object sender, RadTreeNodeEventArgs e)
            //{
            //    //Si el level es cero (0) quiere decir que es una magnitud, entonces no debe tener checkbox.
            //    if (e.Node.Level == 0)
            //    {
            //        //Si es magnitud, no pone check...
            //        e.Node.Checkable = false;
            //    }
            //    else
            //    {
            //        if (_MeasurementUnitAux.Contains(e.Node.Value))
            //        {
            //            e.Node.Checked = true;
            //        }
            //        else
            //        {
            //            e.Node.Checked = false;
            //        }
            //    }
            //}
            //protected void _RtvMeasurementUnit_NodeCheck(object sender, RadTreeNodeEventArgs e)
            //{
            //    RadTreeNode _node = e.Node;

            //    //Obtiene el Id del nodo checkeado
            //    Int64 _idMeasurementUnit = Convert.ToInt64(GetKeyValue(_node.Value, "IdMeasurementUnit"));
            //    if (_MeasurementUnitAux.Contains(_node.Value))
            //    {
            //        if (!_node.Checked)
            //        {
            //            _MeasurementUnitAux.Remove(_node.Value);
            //        }
            //    }
            //    else
            //    {
            //        _MeasurementUnitAux.Add(_node.Value);
            //    }
            //}

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
            protected void rtvIndicators_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                //Limpio los hijos, para no duplicar al abrir y cerrar.
                e.Node.Nodes.Clear();
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                //Primero lo hace sobre las Clasificaciones Hijas...
                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                        SetExpandMode(_node, Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren, true, false);
                    }
                }

                //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                BuildGenericDataTable(Common.ConstantsEntitiesName.PA.Indicators, _params);
                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.PA.Indicators))
                {
                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.PA.Indicators].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.PA.Indicators, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                    }
                }
            }
            protected void rtvIndicators_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                //Carga...ParameterGroups..
                AddComboParameterGroups();
                AddComboMeasurementUnits();
            }
            protected void _RtvIndicatorCO_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                //Carga...ParameterGroups..
                AddComboParameterGroupsCO();
            }
            protected void _RtvIndicatorMP10_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                //Carga...ParameterGroups..
                AddComboParameterGroupsMP10();
            }
            protected void _RtvIndicatorNOx_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                //Carga...ParameterGroups..
                AddComboParameterGroupsNOx();
            }
            protected void _RtvIndicatorN2O_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                //Carga...ParameterGroups..
                AddComboParameterGroupsN2O();
            }
            protected void _RtvIndicatorHCT_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                //Carga...ParameterGroups..
                AddComboParameterGroupsHCT();
            }
            protected void _RtvIndicatorCH4_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                //Carga...ParameterGroups..
                AddComboParameterGroupsCH4();
            }
            protected void _RtvIndicatorHCNM_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                //Carga...ParameterGroups..
                AddComboParameterGroupsHCNM();
            }
            protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.PA.AccountingActivitiesChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }
            protected void rtvSite_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                Int64 _idProcess = Convert.ToInt64(GetKeyValue(_RdcProcess.SelectedValue, "IdProcess"));
                Int64 _idOrganization = ((ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).IdOrganization;

                NodeExpandSitesByType(sender, e, true, true, _idOrganization);
            }
            protected void rtvConst_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpandConstants(sender, e);
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
                const String _EMISSION_FACTOR_NAME = " - Factor de Emisión de ";
                try
                {
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
                                String _facilityRootName = GetFacilityRoot(_site.IdFacility).LanguageOption.Name;
                                String _measurementTaskBase = _facilityRootName + " - " + _site.LanguageOption.Name + " - " + txtName.Text;

                                Int64 _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator"));
                                Int64 _idParameterGroup = Convert.ToInt64(GetKeyValue(_RdcParameterGroup.SelectedValue, "IdParameterGroup"));
                                Int64 _idMeasurementUnit = Convert.ToInt64(GetKeyValue(_RdcMeasurementUnit.SelectedValue, "IdMeasurementUnit"));
                                //  a- Alta de la Tarea de medicion base
                                ProcessTaskMeasurement _taskMeasurementBase = MeasurementTaskAdd(true, _site, _idIndicator, _idParameterGroup, _idMeasurementUnit, _measurementTaskBase, txtDescription.Text);
                            #endregion

                            //Esta unidad de medida es la misma para todos los Factores de Emision
                            _idMeasurementUnit = Convert.ToInt64(GetKeyValue(_RdcMeasurementUnitgGj.SelectedValue, "IdMeasurementUnit"));

                            //  b- Alta de 7 Tareas de medicion (son mediciones accesorias) todo idem a base solo que con unidad de medida g/Gj
                            #region b1- Facility Root - Site – Factor de Emisión de CO (M)
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorCO.SelectedNode.Value, "IdIndicator"));
                                _idParameterGroup = Convert.ToInt64(GetKeyValue(_RdcParameterGroupCO.SelectedValue, "IdParameterGroup"));
                                String _measurementTaskAccessory = _facilityRootName + " - " + _site.LanguageOption.Name + _EMISSION_FACTOR_NAME + "CO";
                                ProcessTaskMeasurement _taskMeasurementEF_M = MeasurementTaskAdd(false, _site, _idIndicator, _idParameterGroup, _idMeasurementUnit, _measurementTaskAccessory, txtDescription.Text);
                            #endregion

                            #region b2- Facility Root - Site – Factor de Emisión de MP-10 (N)
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorMP10.SelectedNode.Value, "IdIndicator"));
                                _idParameterGroup = Convert.ToInt64(GetKeyValue(_RdcParameterGroupMP10.SelectedValue, "IdParameterGroup"));
                                _measurementTaskAccessory = _facilityRootName + " - " + _site.LanguageOption.Name + _EMISSION_FACTOR_NAME + "PM__10__";
                                ProcessTaskMeasurement _taskMeasurementEF_N = MeasurementTaskAdd(false, _site, _idIndicator, _idParameterGroup, _idMeasurementUnit, _measurementTaskAccessory, txtDescription.Text);
                            #endregion

                            #region b3- Facility Root - Site – Factor de Emisión de NOx (O)
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorNOx.SelectedNode.Value, "IdIndicator"));
                                _idParameterGroup = Convert.ToInt64(GetKeyValue(_RdcParameterGroupNOx.SelectedValue, "IdParameterGroup"));
                                _measurementTaskAccessory = _facilityRootName + " - " + _site.LanguageOption.Name + _EMISSION_FACTOR_NAME + "NO__x__";
                                ProcessTaskMeasurement _taskMeasurementEF_O = MeasurementTaskAdd(false, _site, _idIndicator, _idParameterGroup, _idMeasurementUnit, _measurementTaskAccessory, txtDescription.Text);
                            #endregion

                            #region b4- Facility Root - Site – Factor de Emisión de N2O (P)
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorN2O.SelectedNode.Value, "IdIndicator"));
                                _idParameterGroup = Convert.ToInt64(GetKeyValue(_RdcParameterGroupN2O.SelectedValue, "IdParameterGroup"));
                                _measurementTaskAccessory = _facilityRootName + " - " + _site.LanguageOption.Name + _EMISSION_FACTOR_NAME + "N__2__O";
                                ProcessTaskMeasurement _taskMeasurementEF_P = MeasurementTaskAdd(false, _site, _idIndicator, _idParameterGroup, _idMeasurementUnit, _measurementTaskAccessory, txtDescription.Text);
                            #endregion

                            #region b5- Facility Root - Site – Factor de Emisión de HCT (Q)
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorHCT.SelectedNode.Value, "IdIndicator"));
                                _idParameterGroup = Convert.ToInt64(GetKeyValue(_RdcParameterGroupHCT.SelectedValue, "IdParameterGroup"));
                                _measurementTaskAccessory = _facilityRootName + " - " + _site.LanguageOption.Name + _EMISSION_FACTOR_NAME + "HCT";
                                ProcessTaskMeasurement _taskMeasurementEF_Q = MeasurementTaskAdd(false, _site, _idIndicator, _idParameterGroup, _idMeasurementUnit, _measurementTaskAccessory, txtDescription.Text);
                            #endregion

                            #region b6- Facility Root - Site – Factor de Emisión de CH4 (R)
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorCH4.SelectedNode.Value, "IdIndicator"));
                                _idParameterGroup = Convert.ToInt64(GetKeyValue(_RdcParameterGroupCH4.SelectedValue, "IdParameterGroup"));
                                _measurementTaskAccessory = _facilityRootName + " - " + _site.LanguageOption.Name + _EMISSION_FACTOR_NAME + "CH__4__";
                                ProcessTaskMeasurement _taskMeasurementEF_R = MeasurementTaskAdd(false, _site, _idIndicator, _idParameterGroup, _idMeasurementUnit, _measurementTaskAccessory, txtDescription.Text);
                            #endregion

                            #region b7- Facility Root - Site – Factor de Emisión de HCNM (S)
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorHCNM.SelectedNode.Value, "IdIndicator"));
                                _idParameterGroup = Convert.ToInt64(GetKeyValue(_RdcParameterGroupHCNM.SelectedValue, "IdParameterGroup"));
                                _measurementTaskAccessory = _facilityRootName + " - " + _site.LanguageOption.Name + _EMISSION_FACTOR_NAME + "HCNM";
                                ProcessTaskMeasurement _taskMeasurementEF_S = MeasurementTaskAdd(false, _site, _idIndicator, _idParameterGroup, _idMeasurementUnit, _measurementTaskAccessory, txtDescription.Text);
                            #endregion

                            //--------------TRANSFORMACIONES---------------------------
                            Int64 _idMeasurementUnitMg = Convert.ToInt64(GetKeyValue(_RdcMeasurementUnitMg.SelectedValue, "IdMeasurementUnit"));

                            #region   c- Alta de Transformacion Hija de Medicion Base (T):
                                String _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Emisiones de Dióxido de Carbono";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorCO2.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_T = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    _taskMeasurementBase.Measurement, null, BuildOperatorForTransformation_T(), _transformationName, txtDescription.Text, "J/100*A*Base*C/D");
                            #endregion
                            
                            #region   d- Alta de Transformacion Hija de T (U):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Conversión de CO__2e__ desde el Dióxido de Carbono";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorCO2efromCO2.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_U = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    null, _calculateOfTransformation_T, BuildOperatorForTransformation_U(), _transformationName, txtDescription.Text, "Base*G");
                            #endregion
                            
                            #region   e- Alta de Transformacion Hija de U (V):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Conversión a CO__2e__";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorCO2e.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_V = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    null, _calculateOfTransformation_U, BuildOperatorForTransformation_V(), _transformationName, txtDescription.Text, "Base");
                            #endregion
                            
                            #region   f- Alta de Transformacion Hija de Medición Base (W):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Emisiones de Metano";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorCH4.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_W = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    _taskMeasurementBase.Measurement, null, BuildOperatorForTransformation_W(_taskMeasurementEF_R), _transformationName, txtDescription.Text, "Base*L*R/10^6");
                            #endregion
                            
                            #region   g- Alta de Transformacion Hija de W (X):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Conversión de CO__2e__ desde el Metano";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorCO2efromCH4.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_X = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    null, _calculateOfTransformation_W, BuildOperatorForTransformation_X(), _transformationName, txtDescription.Text, "Base*H");
                            #endregion
                            
                            #region   h- Alta de Transformacion Hija de X (Y):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Conversión a CO__2e__";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorCO2e.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_Y = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    null, _calculateOfTransformation_X, BuildOperatorForTransformation_Y(), _transformationName, txtDescription.Text, "Base");
                            #endregion
                            
                            #region   i- Alta de Transformacion Hija de Medicion Base (Z):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Emisiones de Óxido Nitroso";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorN2O.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_Z = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    _taskMeasurementBase.Measurement, null, BuildOperatorForTransformation_Z(_taskMeasurementEF_P), _transformationName, txtDescription.Text, "Base*L*P/10^6");
                            #endregion
                            
                            #region   j- Alta de Transformación Hija de Z (AA):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Conversión de CO__2e__ desde el Óxido Nitroso";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorCO2efromN2O.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_AA = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    null, _calculateOfTransformation_Z, BuildOperatorForTransformation_AA(), _transformationName, txtDescription.Text, "Base*I");
                            #endregion
                            
                            #region   k- Alta de Transformación Hija de AA (AB):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Conversión de CO__2e__";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorCO2e.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_AB = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    null, _calculateOfTransformation_AA, BuildOperatorForTransformation_AB(), _transformationName, txtDescription.Text, "Base");
                            #endregion
                            
                            #region   l- Alta de Transformacion Hija de Medicion Base (AC):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Emisiones de Dióxido de Azufre";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorSO2.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_AC = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    _taskMeasurementBase.Measurement, null, BuildOperatorForTransformation_AC(), _transformationName, txtDescription.Text, "K/100*B*Base*E/F");
                            #endregion
                            
                            #region   m- Alta de Transformacion Hija de Medición Base (AD):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Emisiones de Monóxido de Carbono";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorCO.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_AD = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    _taskMeasurementBase.Measurement, null, BuildOperatorForTransformation_AD(_taskMeasurementEF_M), _transformationName, txtDescription.Text, "Base*L*M/10^6");
                            #endregion
                            
                            #region   n- Alta de Transformacion Hija de Medición Base (AE):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Emisiones de Material Particulado 10";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorMP10.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_AE = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    _taskMeasurementBase.Measurement, null, BuildOperatorForTransformation_AE(_taskMeasurementEF_N), _transformationName, txtDescription.Text, "Base*L*N/10^6");
                            #endregion
                            
                            #region   o- Alta de Transformacion Hija de Medición Base (AF):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Emisiones de Óxido de Nitrógeno";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorNOx.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_AF = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    _taskMeasurementBase.Measurement, null, BuildOperatorForTransformation_AF(_taskMeasurementEF_O), _transformationName, txtDescription.Text, "Base*L*O/10^6");
                            #endregion
                            
                            #region   p- Alta de Transformacion Hija de Medición Base (AG):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Emisiones de Hidrocarburos Totales";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorHCT.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_AG = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    _taskMeasurementBase.Measurement, null, BuildOperatorForTransformation_AG(_taskMeasurementEF_Q), _transformationName, txtDescription.Text, "Base*L*Q/10^6");
                            #endregion
                            
                            #region   q- Alta de Transformacion Hija de Medición Base (AH):
                                _transformationName = _facilityRootName + " - " + _site.LanguageOption.Name + " - Emisiones de Hidrocarburos no Metánicos";
                                _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicatorHCNM.SelectedNode.Value, "IdIndicator"));
                                CalculateOfTransformation _calculateOfTransformation_AH = TransformationAdd(_idIndicator, _idMeasurementUnitMg,
                                    _taskMeasurementBase.Measurement, null, BuildOperatorForTransformation_AH(_taskMeasurementEF_S), _transformationName, txtDescription.Text, "Base*L*S/10^6");
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
