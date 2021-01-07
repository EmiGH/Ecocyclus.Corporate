using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using EMS_PA = Condesus.EMS.Business.PA;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.NT.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.PF.Entities;

namespace Condesus.EMS.WebUI.AdministrationTools.PerformanceAssessment
{
    public partial class CalculationOfTransformationsProperties : BaseProperties
    {
        #region Internal Properties
        public String _msgSelectIndicatorAndMeasurementUnit = String.Empty;
        private CompareValidator _CvMeasurementUnit;
        private RadComboBox _RdcMeasurementUnit;
        private CompareValidator _CvIndicator;
        private RadComboBox _RdcIndicator;
        private RadTreeView _RtvIndicator;
        private Measurement _Measurement
        {
            get
            {
                try
                {
                    //Si viene de una tarea de medicion...accede desde la tarea.
                    if (Convert.ToInt64(GetPKfromNavigator("IdTask")) == 0)
                    {
                        return (Measurement)EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement);
                    }
                    else
                    {
                        Int64 _idTask = Convert.ToInt64(GetPKfromNavigator("IdTask"));
                        return ((ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_idTask)).Measurement;
                    }
                }
                catch { return null; }
            }
        }
        private CalculateOfTransformation _Entity = null;
        private CalculateOfTransformation Entity
        {
            get
            {
                try
                {
                    if ((_Entity == null) && (_Measurement != null))
                    { _Entity = _Measurement.Transformation(_IdTransformation); }

                    return _Entity;
                }
                catch { return null; }
            }

            set { _Entity = value; }
        }
        private Int64 _IdTransformation
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdTransformation") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdTransformation")) : 0;
            }
        }
        private Int64 _IdMeasurement
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdMeasurement") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdMeasurement")) : Convert.ToInt64(GetPKfromNavigator("IdMeasurement"));
            }
        }
        private Int64 _IdMagnitud
        {
            get
            {
                if (ViewState["IdMagnitud"] == null)
                {
                    ViewState["IdMagnitud"] = 0;
                }
                return Convert.ToInt64(ViewState["IdMagnitud"]);
            }
            set { ViewState["IdMagnitud"] = value; }
        }

        private RadComboBox _RdcAccountingActivity;
        private RadTreeView _RtvAccountingActivity;
        //private CompareValidator _CvAccountingActivity;
        private RadComboBox _RdcConstantClassification;
        private RadTreeView _RtvConstantClassification;
        private RadComboBox _RdcConstant;
        private RadComboBox _RdcMeasurement;
        private RadComboBox _RdcAlphabet;
        private RadComboBox _RdcTransformation;
        private CompareValidator _CvVariable;
        private Dictionary<String, String> _Operands
        {
            get
            {
                if (ViewState["Operands"] == null)
                {
                    ViewState["Operands"] = new Dictionary<String, String>();
                }
                return (Dictionary<String, String>)ViewState["Operands"];
            }
            set { ViewState["Operands"] = value; }
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
        private RadComboBox _RdcSite;
        private RadTreeView _RtvSite;
        #endregion

        #region PageLoad & Init
        protected override void InitializeHandlers()
        {
            base.InitializeHandlers();
            //Le paso el Save a la MasterContentToolbar
            EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
            FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);

            btnAddOperand.Click += new EventHandler(btnAddOperand_Click);
            btnRemoveOperand.Click += new EventHandler(btnRemoveOperand_Click);
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LoadTextLabels();
            base.InjectCheckIndexesTags();
            base.InjectCheckContainVariableBaseinFormula();

            //Handler client event al radiobutton
            rbList.Items[0].Attributes.Add("onclick", "javascript:ShowPanelSource(this);");
            rbList.Items[1].Attributes.Add("onclick", "javascript:ShowPanelSource(this);");
            rbList.Items[2].Attributes.Add("onclick", "javascript:ShowPanelSource(this);");

            lnkTestFormula.Click += new EventHandler(lnkTestFormula_Click);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            AddComboIndicators();
            AddComboMeasurementUnits();
            AddComboConstantClassification();
            AddComboConstants();

            AddComboSites();
            Int64 _idSite = 0;
            if (_RtvSite.SelectedNode != null)
            {
                if (_RtvSite.SelectedNode.Value.Contains("IdSector"))
                {
                    _idSite = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdSector"));
                }
                else
                {
                    _idSite = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdFacility"));
                }
            }
            AddComboMeasurement(_idSite);
            AddComboAlphabet();
            AddComboTransformation();
            AddTreeViewEmails();
            AddComboAccountingActivities();

            //Inyecta el js que oculta o muestra los paneles para la seleccion del source del dato para la formula a aplicar.
            InjectShowPanelSource(contentHideMeasurement.ClientID, contentHideConstant.ClientID, contentHideTransformation.ClientID);

            //Identifica el tipo de seleccion y habilita/deshabilita los validator y el panel.
            switch (rbList.SelectedValue)
            {
                case "rbMeasurement":
                    contentHideMeasurement.Style.Add("display", "block");
                    contentHideConstant.Style.Add("display", "none");
                    contentHideTransformation.Style.Add("display", "none");
                    break;
                case "rbConstant":
                    contentHideMeasurement.Style.Add("display", "none");
                    contentHideConstant.Style.Add("display", "block");
                    contentHideTransformation.Style.Add("display", "none");
                    break;
                case "rbTransformation":
                    contentHideMeasurement.Style.Add("display", "none");
                    contentHideConstant.Style.Add("display", "none");
                    contentHideTransformation.Style.Add("display", "block");
                    break;
            }
            if (!Page.IsPostBack)
            {
                txtFormula.Text = "Base";   //inserta la constante que necesita la dll para poder calcular.(la base siempre es la medicion origen o en su defecto la transformacion origen)

                if (Entity == null)
                { Add(); }
                else
                { LoadData(); }

                //Form
                base.SetContentTableRowsCss(tblContentForm);
                base.SetContentTableRowsCss(tblContentSourceType);
                base.SetContentTableRowsCss(tblContentFormFormula);
                base.SetContentTableRowsCss(tblContentFormNotificationRecipient);

                LoadDataEmails();
            }

            String _baseName = String.Empty;
            //si viene el idTransformation, y el Entity es nulo, quiere decir que es una transformacion de transformacion.
            //if (Convert.ToInt64(GetPKfromNavigator("IdTransformation")) != 0)
            if (_IdTransformation != 0)
            {
                //Transformacion de Transformacion
                //_baseName = Common.Functions.ReplaceIndexesTags(_Measurement.Transformation(Convert.ToInt64(GetPKfromNavigator("IdTransformation"))).LanguageOption.Name);
                _baseName = Common.Functions.ReplaceIndexesTags(_Measurement.Transformation(_IdTransformation).BaseTransformer.Name);
            }
            else
            {
                if (Convert.ToInt64(GetPKfromNavigator("IdTransformation"))!=0)
                {
                    _baseName = Common.Functions.ReplaceIndexesTags(_Measurement.Transformation(Convert.ToInt64(GetPKfromNavigator("IdTransformation"))).LanguageOption.Name);
                }
                else
                {
                    //o Transformacion de una medicion.
                    _baseName = Common.Functions.ReplaceIndexesTags(_Measurement.LanguageOption.Name);
                }
            }
            //Aca va la base verdadera....
            lblBaseValue.Text = _baseName;

        }
        protected override void SetPagetitle()
        {
            try
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.Transformation;
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
            Page.Title = Resources.CommonListManage.Formula;
            _msgSelectIndicatorAndMeasurementUnit = Resources.ConstantMessage.msgSelectIndicatorAndMeasurementUnit;
            lblAccountingActivity.Text = Resources.CommonListManage.AccountingActivity;
            lblIndicator.Text = Resources.CommonListManage.Indicator;
            lblName.Text = Resources.CommonListManage.Name;
            lblDescription.Text = Resources.CommonListManage.Description;
            lblMagnitud.Text = Resources.CommonListManage.Magnitud;
            lblMeasurementUnit.Text = Resources.CommonListManage.MeasurementUnit;
            lblSourceType.Text = Resources.CommonListManage.SelectSourceType;
            lblMeasurement.Text = Resources.CommonListManage.Measurement;
            lblConstantClassification.Text = Resources.CommonListManage.ConstantClassification;
            lblConstant.Text = Resources.CommonListManage.Constant;
            lblTransformation.Text = Resources.CommonListManage.Transformation;
            lblOperand.Text = Resources.CommonListManage.Operands;
            lblFormula.Text = Resources.CommonListManage.Formula;
            lblVariable.Text = Resources.CommonListManage.Variable;
            lblBase.Text = Resources.CommonListManage.Base;
            lblFormulaExample.Text = Resources.CommonListManage.FormulaExample;
            rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
            rfvFormula.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            cvFormula.ErrorMessage = Resources.ConstantMessage.ValidationContainBaseinFormula;
            
            lblEmails.Text = Resources.CommonListManage.SelectEmailsPersontoNotify;
            lblListEmails.Text = Resources.CommonListManage.UndeclaredEmail;
            revEmails.ErrorMessage = Resources.ConstantMessage.ValidationListEmails;

            rbList.Items[0].Text = Resources.CommonListManage.Measurement;
            rbList.Items[1].Text = Resources.CommonListManage.Constant;
            rbList.Items[2].Text = Resources.CommonListManage.Transformation;
        }
        private void SetIndicator()
        {
            //Seteamos la organizacion...
            //Realiza el seteo del parent en el Combo-Tree.
            Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(Entity.Indicator.IdIndicator);
            String _keyValuesElement = "IdIndicator=" + _indicator.IdIndicator.ToString();
            if (_indicator.Classifications.Count > 0)
            {
                String _keyValuesClassification = "IdIndicatorClassification=" + _indicator.Classifications.First().Value.IdIndicatorClassification;
                SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvIndicator, ref _RdcIndicator, Common.ConstantsEntitiesName.PA.IndicatorClassification, Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren, Common.ConstantsEntitiesName.PA.Indicators);
            }
            else
            {
                SelectItemTreeViewParent(_keyValuesElement, ref _RtvIndicator, ref _RdcIndicator, Common.ConstantsEntitiesName.PA.Indicator, Common.ConstantsEntitiesName.PA.Indicators);
            }
        }
        private void SetMeasurementUnit()
        {
            _RdcMeasurementUnit.SelectedValue = "IdMeasurementUnit=" + Entity.MeasurementUnit.IdMeasurementUnit.ToString() + "& IdMagnitud=" + Entity.MeasurementUnit.Magnitud.IdMagnitud.ToString();
        }
        private void SetMagnitud()
        {
            lblMagnitudValue.Text = Entity.Indicator.Magnitud.LanguageOption.Name;
        }
        private void Add()
        {
            //Activo los textbox
            txtName.ReadOnly = false;
            txtDescription.ReadOnly = false;

            //limpio los textbox por si hay datos
            txtDescription.Text = String.Empty;
            txtName.Text = String.Empty;
        }
        private void LoadData()
        {
            //Inputs
            txtDescription.Text = Entity.LanguageOption.Description;
            txtName.Text = Entity.LanguageOption.Name;

            SetIndicator();
            SetMagnitud();
            //Setea la magnitud que tiene el measurement unit.
            _IdMagnitud = Entity.MeasurementUnit.Magnitud.IdMagnitud;
            //Despues de setear el indicador, carga los 2 combos que dependen de indicator.
            AddComboMeasurementUnits();
            SetMeasurementUnit();

            //Deberia eliminar del combo de medicion, constate y tranformaciones, las que ya estan en uso en los parametros...

            //Carga el listBox con los parametros.
            LoadParameters();
            txtFormula.Text = Entity.Formula;
            LoadStructEmailsAux();

            SetAccountingActivity();
        }
        private void LoadParameters()
        {
            //Carga el listBox y tambien el dictionary interno
            foreach (KeyValuePair<String, CalculateOfTransformationParameter> _item in _Entity.Parameters)
            {
                String _idLetter = _item.Key;
                String _operandName = _item.Value.Operand.Name;
                String _operand = String.Empty;
                String _keyValue = String.Empty;
                RadComboBoxItem _rdcbItem;
                //Verifica el tipo de objeto que viene en el parametro
                switch (_item.Value.Operand.GetType().Name)
                {
                    case Common.ConstantsEntitiesName.PA.Measurement:
                    case Common.ConstantsEntitiesName.PA.MeasurementExtensive:
                    case Common.ConstantsEntitiesName.PA.MeasurementIntensive:
                        //Arma el string que muestra el listBox y el KeyValue, para el dictionary interno
                        _operand = _operandName;
                        _operand = _idLetter + " = " + _operand;
                        _keyValue = "IdMeasurement=" + _item.Value.Operand.IdObject.ToString();
                        _Operands.Add(_item.Key, _keyValue);
                        //Elimina el item del combo medicion, para que no se pueda volver a seleccionar.
                        _rdcbItem = _RdcMeasurement.FindItemByValue(_keyValue);
                        if (_rdcbItem != null)
                        {
                            //Lo encontro, entonces lo borra.
                            _rdcbItem.Remove();
                        }
                        break;

                    case Common.ConstantsEntitiesName.PA.Constant:
                        //Arma el string que muestra el listBox y el KeyValue, para el dictionary interno
                        _operand = _operandName;
                        _operand = _idLetter + " = " + _operand;
                        Constant _constant = ((Constant)_item.Value.Operand);
                        _keyValue = "IdConstantClassification=" + _constant.ConstantClassification.IdConstantClassification.ToString()
                            + "& IdConstant=" + _constant.IdConstant.ToString();

                        _Operands.Add(_item.Key, _keyValue);
                        break;

                    case Common.ConstantsEntitiesName.PA.Transformation:
                    case Common.ConstantsEntitiesName.PA.CalculateOfTransformation:
                        //Arma el string que muestra el listBox y el KeyValue, para el dictionary interno
                        _operand = _operandName;
                        _operand = _idLetter + " = " + _operand;
                        CalculateOfTransformation _calculateOfTransformation = ((CalculateOfTransformation)_item.Value.Operand);
                        _keyValue = "IdTransformation=" + _calculateOfTransformation.IdTransformation.ToString()
                            + "& IdMeasurement=" + GetMeasurementOfTransformation().IdMeasurement.ToString()
                            + "& IdProcess=" + _calculateOfTransformation.ProcessGroupProcess.IdProcess.ToString();

                        _Operands.Add(_item.Key, _keyValue);
                        //Elimina el item del combo Transformacion, para que no se pueda volver a seleccionar.
                        _rdcbItem = _RdcTransformation.FindItemByValue(_keyValue);
                        if (_rdcbItem != null)
                        {
                            //Lo encontro, entonces lo borra.
                            _rdcbItem.Remove();
                        }
                        break;
                }
                //Agrega el parametro al ListBox
                //lstbOperands.Items.Add(_idLetter + " = " + _operandName);
                RadListBoxItem _rlbitem = new RadListBoxItem(_idLetter + " = " + _operandName);
                rdlstbOperands.Items.Add(_rlbitem);

                //Elimina el item del combo Abecedario, para que no se pueda volver a seleccionar.
                String _keyValueAlphabet = "IdLetter=" + _idLetter;
                _rdcbItem = _RdcAlphabet.FindItemByValue(_keyValueAlphabet);
                if (_rdcbItem != null)
                {
                    //Lo encontro, entonces lo borra.
                    _rdcbItem.Remove();
                }
            }
        }
        private void SetAccountingActivity()
        {
            //Seteamos el Accounting Activity...
            //Realiza el seteo en el Combo-Tree.
            AccountingActivity _accountingActivity;

            //Si la entidad no tiene actividad, tomamos por defecto la de la tarea!!!
            if (Entity.Activity!=null)
            {
                _accountingActivity = Entity.Activity;
            }
            else
            {
                _accountingActivity = ((ProcessTaskMeasurement)_Measurement.ProcessTask).AccountingActivity;
            }
            if (_accountingActivity != null)
            {
                //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                String _keyValues = "IdActivity=" + _accountingActivity.IdActivity.ToString() + "& IdParentActivity=" + _accountingActivity.IdParentActivity.ToString();
                //Realiza el seteo del parent en el Combo-Tree.
                SelectItemTreeViewParent(_keyValues, ref _RtvAccountingActivity, ref _RdcAccountingActivity, Common.ConstantsEntitiesName.PA.AccountingActivity, Common.ConstantsEntitiesName.PA.AccountingActivitiesChildren);
            }
        }
        private void AddComboAccountingActivities()
        {
            String _filterExpression = String.Empty;
            //Combo de AccountingActivity Parent
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            AddComboWithTree(phAccountingActivity, ref _RdcAccountingActivity, ref _RtvAccountingActivity,
                Common.ConstantsEntitiesName.PA.AccountingActivities, _params, false, false, true, ref _filterExpression,
                new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand), Resources.ConstantMessage.SelectAAccountingActivity, false);

            //ValidatorRequiredField(Common.ConstantsEntitiesName.PA.AccountingActivities, phAccountingActivityValidator, ref _CvAccountingActivity, _RdcAccountingActivity, Resources.ConstantMessage.SelectAAccountingActivity);
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

            ValidatorRequiredField(Common.ConstantsEntitiesName.PA.Indicators, phIndicatorValidator, ref _CvIndicator, _RdcIndicator, Resources.ConstantMessage.SelectAIndicator);
        }
        private void AddComboMeasurementUnits()
        {
            Dictionary<String, Object> _params = new Dictionary<String, Object>();

            if (_params.ContainsKey("IdMagnitud"))
            {
                _params.Remove("IdMagnitud");
            }
            _params.Add("IdMagnitud", _IdMagnitud);

            AddCombo(phMeasurementUnit, ref _RdcMeasurementUnit, Common.ConstantsEntitiesName.PA.MeasurementUnits, String.Empty, _params, false, true, false, false, false);
        }
        private void AddComboConstantClassification()
        {
            String _filterExpression = String.Empty;
            //Combo de ConstantClassification Parent
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            AddComboWithTree(phConstantClassification, ref _RdcConstantClassification, ref _RtvConstantClassification,
                Common.ConstantsEntitiesName.PA.ConstantClassifications, _params, false, true, false, ref _filterExpression,
                new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboConstantClassification_NodeExpand),
                new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboConstantClassification_NodeClick),
                Common.Constants.ComboBoxSelectItemDefaultPrefix, false);

        }
        private void AddComboConstants()
        {
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            if (_RtvConstantClassification.SelectedNode != null)
            {
                if (GetKeyValue(_RtvConstantClassification.SelectedNode.Value, "IdConstantClassification") != null)
                {
                    Int64 _idConstantClassification = Convert.ToInt64(GetKeyValue(_RtvConstantClassification.SelectedNode.Value, "IdConstantClassification"));
                    _params.Add("IdConstantClassification", _idConstantClassification);
                }
            }

            AddCombo(phConstant, ref _RdcConstant, Common.ConstantsEntitiesName.PA.Constants, String.Empty, _params, false, true, false, false, false);
        }
        private void AddComboMeasurement(Int64 idSite)
        {
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            if (_params.ContainsKey("IdSite"))
            {
                _params.Remove("IdSite");
            }
            _params.Add("IdSite", idSite);

            Int64 _idProcess = 0;
            //Agregar el caso de transformacion de transformacion...
            //switch (Entity.BaseTransformer.GetType().Name)
            //{
            //    case Common.ConstantsEntitiesName.PA.Measurement:
            //        _idProcess = ((Measurement)Entity.BaseTransformer).ProcessTask().ProcessGroupProcess.IdProcess;
            //        break;
            //}

            _idProcess = _Measurement.ProcessTask.Parent.IdProcess;
            _params.Add("IdProcess", _idProcess);

            AddCombo(phMeasurement, ref _RdcMeasurement, Common.ConstantsEntitiesName.PA.Measurements, String.Empty, _params, false, true, false, false, false);

            //Debo sacar del combo la medición Base (de donde deriva todo esto).
            String _keyValueMeasurement = "IdMeasurement=" + _Measurement.IdMeasurement.ToString();
            RadComboBoxItem _rdcbItem = _RdcMeasurement.FindItemByValue(_keyValueMeasurement);
            if (_rdcbItem != null)
            {
                //Lo encontro, entonces lo borra.
                _rdcbItem.Remove();
            }
        }
        private void AddComboSites()
        {
            String _filterExpression = String.Empty;
            //Combo de Organizaciones
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            AddComboTreeSites(ref phSite, ref _RdcSite, ref _RtvSite, new RadTreeViewEventHandler(rtvSite_NodeExpand));
            _RtvSite.NodeClick += new RadTreeViewEventHandler(_RtvSite_NodeClick);
        }

        private void AddComboAlphabet()
        {
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            AddCombo(phVariable, ref _RdcAlphabet, Common.ConstantsEntitiesName.PA.Alphabet, String.Empty, _params, false, true, false, false, false);
        }
        private void AddComboTransformation()
        {
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            Int64 _idProcess = 0;

            _idProcess = _Measurement.ProcessTask.Parent.IdProcess;
            _params.Add("IdProcess", _idProcess);

            AddCombo(phTransformation, ref _RdcTransformation, Common.ConstantsEntitiesName.PA.Transformations, String.Empty, _params, false, true, false, false, false);

            //Esto queda pendiente, hasta ver como es la transformacion de transformacion...
            //Debo sacar del combo la transformacion Base, si es que viene de una transformacion (de donde deriva todo esto).
            //String _keyValueMeasurement = "IdMeasurement=" + _Measurement.IdMeasurement.ToString();
            //RadComboBoxItem _rdcbItem = _RdcMeasurement.FindItemByValue(_keyValueMeasurement);
            //if (_rdcbItem != null)
            //{
            //    //Lo encontro, entonces lo borra.
            //    _rdcbItem.Remove();
            //}
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

        private Measurement GetMeasurementOfTransformation()
        {
            if (_Entity != null)
            {
                if (_Entity.BaseTransformer.GetType().Name.Contains(Common.ConstantsEntitiesName.PA.Measurement))
                {
                    return ((Measurement)_Entity.BaseTransformer);
                }
                else
                {
                    Condesus.EMS.Business.ITransformer _calcOfTransf = _Entity.BaseTransformer;
                    //Mientras no sea una medicion....
                    while (!_calcOfTransf.GetType().Name.Contains(Common.ConstantsEntitiesName.PA.Measurement))
                    {
                        _calcOfTransf = ((CalculateOfTransformation)_calcOfTransf).BaseTransformer;
                    }
                    return ((Measurement)_calcOfTransf);
                }
            }
            else
            {
                return null;
            }
        }
        protected void InjectShowPanelSource(String pnlContentMeasurement, String pnlContentConstant, String pnlContentTransformation)
        {   //Esta funcion se ejecuta al hacer click sobre un item del menuRad
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            _sbBuffer.Append(OpenHtmlJavaScript());

            _sbBuffer.Append("function ShowPanelSource(rbListType) {                                                       \n");
            _sbBuffer.Append("  var _panelContentMeasurement = document.getElementById('" + pnlContentMeasurement + "');                \n");
            _sbBuffer.Append("  var _panelContentConstant = document.getElementById('" + pnlContentConstant + "');                      \n");
            _sbBuffer.Append("  var _panelContentTransformation = document.getElementById('" + pnlContentTransformation + "');          \n");

            _sbBuffer.Append("  switch (rbListType.id) {                                                                                \n");
            _sbBuffer.Append("      case 'ctl00_ContentMain_rbList_0':      //Measurement                                               \n");
            _sbBuffer.Append("          _panelContentMeasurement.style.display = 'block';                                               \n");
            _sbBuffer.Append("          _panelContentConstant.style.display = 'none';                                                   \n");
            _sbBuffer.Append("          _panelContentTransformation.style.display = 'none';                                             \n");
            _sbBuffer.Append("          break;                                                                                          \n");
            _sbBuffer.Append("      case 'ctl00_ContentMain_rbList_1':      //Constant                                                  \n");
            _sbBuffer.Append("          _panelContentMeasurement.style.display = 'none';                                                \n");
            _sbBuffer.Append("          _panelContentConstant.style.display = 'block';                                                  \n");
            _sbBuffer.Append("          _panelContentTransformation.style.display = 'none';                                             \n");
            _sbBuffer.Append("          break;                                                                                          \n");
            _sbBuffer.Append("      case 'ctl00_ContentMain_rbList_2':      //Transformation                                            \n");
            _sbBuffer.Append("          _panelContentMeasurement.style.display = 'none';                                                \n");
            _sbBuffer.Append("          _panelContentConstant.style.display = 'none';                                                   \n");
            _sbBuffer.Append("          _panelContentTransformation.style.display = 'block';                                            \n");
            _sbBuffer.Append("          break;                                                                                          \n");
            _sbBuffer.Append("      }                                                                                                   \n");
            _sbBuffer.Append("}                                                                                                         \n");

            _sbBuffer.Append(CloseHtmlJavaScript());

            InjectJavascript("JS_ShowPanelSource", _sbBuffer.ToString());
        }
        private Boolean ValidateActivitySelected()
        {
            //Verifica si hay una actividad seleccionada
            if (Convert.ToInt64(GetKeyValue(_RtvAccountingActivity.SelectedNode.Value, "IdActivity")) > 0)
            {
                return true;
            }
            //Si NO hay actividad seleccionada, entonces retorna falso.
            return false;
        }
        #endregion

        #region Page Events
        protected void rtvSite_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            NodeExpandSites(sender, e, true);
        }
        protected void _RtvSite_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            Int64 _idSite = 0;
            if (_RtvSite.SelectedNode != null)
            {
                if (e.Node.Value.Contains("IdSector"))
                {
                    _idSite = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdSector"));
                }
                else
                {
                    _idSite = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdFacility"));
                }
            }

            AddComboMeasurement(_idSite);
        }
        protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            NodeExpand(sender, e, Common.ConstantsEntitiesName.PA.AccountingActivitiesChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
        }
        protected void lnkTestFormula_Click(object sender, EventArgs e)
        {
            try
            {
                //Llamar al formula validate.
                _Measurement.EvaluateFormula(txtFormula.Text);

                base.StatusBar.ShowMessage(Resources.ConstantMessage.ValidationFormulaCorrect, Pnyx.WebControls.PnyxStatusBar.StatusState.Success);
            }
            catch (Exception ex)
            {
                base.StatusBar.ShowMessage(Resources.ConstantMessage.ValidationFormulaError + " {" + ex.InnerException.Message + "}", Pnyx.WebControls.PnyxStatusBar.StatusState.Warning);
            }
        }
        protected void btnAddOperand_Click(object sender, EventArgs e)
        {
            String _operand = String.Empty;

            //Identifica que cual es el source de datos para los operand
            String _idLetter = Convert.ToString(GetKeyValue(_RdcAlphabet.SelectedValue, "IdLetter"));
            switch (rbList.SelectedValue)
            {
                case "rbMeasurement":
                    Int64 _idMeasurement = Convert.ToInt64(GetKeyValue(_RdcMeasurement.SelectedValue, "IdMeasurement"));
                    _operand = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement).LanguageOption.Name;
                    _operand = _idLetter + " = " + _operand;
                    _Operands.Add(_idLetter, _RdcMeasurement.SelectedValue);
                    //Elimina el item del combo, para que no se pueda volver a seleccionar.
                    _RdcMeasurement.SelectedItem.Remove();
                    _RdcMeasurement.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
                    break;

                case "rbConstant":
                    Int64 _idConstantClassification = Convert.ToInt64(GetKeyValue(_RdcConstant.SelectedValue, "IdConstantClassification"));
                    Int64 _idConstant = Convert.ToInt64(GetKeyValue(_RdcConstant.SelectedValue, "IdConstant"));
                    _operand = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClassification).Constant(_idConstant).LanguageOption.Name;
                    _operand = _idLetter + " = " + _operand;
                    _Operands.Add(_idLetter, _RdcConstant.SelectedValue);
                    //Elimina el item del combo, para que no se pueda volver a seleccionar.
                    _RdcConstant.SelectedItem.Remove();
                    _RdcConstant.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
                    break;

                case "rbTransformation":
                    Int64 _idTransformation = Convert.ToInt64(GetKeyValue(_RdcTransformation.SelectedValue, "IdTransformation"));
                    _operand = _Measurement.Transformation(_idTransformation).LanguageOption.Name;
                    _operand = _idLetter + " = " + _operand;
                    _Operands.Add(_idLetter, _RdcTransformation.SelectedValue);
                    //Elimina el item del combo, para que no se pueda volver a seleccionar.
                    _RdcTransformation.SelectedItem.Remove();
                    _RdcTransformation.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
                    break;
            }
            //Agrega la informacion al listBox de la pagina.
            //lstbOperands.Items.Add(_operand);
            RadListBoxItem _rlbitem = new RadListBoxItem(_operand);
            rdlstbOperands.Items.Add(_rlbitem);

            //Elimina el item del combo, para que no se pueda volver a seleccionar.
            _RdcAlphabet.SelectedItem.Remove();
            _RdcAlphabet.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
        }
        protected void btnRemoveOperand_Click(object sender, EventArgs e)
        {
            
            //if (lstbOperands.SelectedValue != String.Empty)
            if (rdlstbOperands.SelectedValue != String.Empty)
            {
                String _idLetter = rdlstbOperands.SelectedValue.Split('=')[0].Trim();   // lstbOperands.SelectedValue.Split('=')[0].Trim();

                //Agrega la letra al combo, para que pueda volver a ser utilizada.
                RadComboBoxItem _rcbItem = new RadComboBoxItem(_idLetter, "IdLetter=" + _idLetter);
                _RdcAlphabet.Items.Add(_rcbItem);

                //Agrega el parametro al combo original
                String _item = _Operands[_idLetter];
                Condesus.EMS.Business.IOperand _operand = null;
                if (_item.Contains("IdTransformation"))
                {
                    //Esta es una transformacion
                    _operand = _Measurement.Transformation(Convert.ToInt64(GetKeyValue(_item, "IdTransformation")));
                    //Agrega el item al combo de transformacion
                    _rcbItem = new RadComboBoxItem(_operand.Name, _item);
                    _RdcTransformation.Items.Add(_rcbItem);
                }
                else
                {
                    if (_item.Contains("IdMeasurement"))
                    {
                        //Esta es medicion
                        _operand = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(Convert.ToInt64(GetKeyValue(_item, "IdMeasurement")));
                        //Agrega el item al combo de mediciones
                        _rcbItem = new RadComboBoxItem(_operand.Name, _item);
                        _RdcMeasurement.Items.Add(_rcbItem);
                    }
                    else
                    {
                        if (_item.Contains("IdConstant"))
                        {
                            //Esta es una constante
                            _operand = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(Convert.ToInt64(GetKeyValue(_item, "IdConstantClassification"))).Constant(Convert.ToInt64(GetKeyValue(_item, "IdConstant")));
                            //Agrega el item al combo de constantes
                            _rcbItem = new RadComboBoxItem(_operand.Name, _item);
                            _RdcConstant.Items.Add(_rcbItem);
                        }
                    }
                }

                //Elimina el item del dictionary Interno.
                _Operands.Remove(_idLetter);
                //Elimina el item del ListBox.
                //lstbOperands.Items.Remove(lstbOperands.SelectedItem);
                rdlstbOperands.Items.Remove(rdlstbOperands.SelectedItem);
            }
        }
        protected void rtvHierarchicalTreeViewInComboConstantClassification_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            NodeExpand(sender, e, Common.ConstantsEntitiesName.PA.ConstantClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
        }
        protected void rtvHierarchicalTreeViewInComboConstantClassification_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            if (_RtvConstantClassification.SelectedNode != null)
            {
                AddComboConstants();
            }
        }
        //Evento para el Expand del Combo con Tree pero ElementMaps
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
            if (_RtvIndicator.SelectedNode != null)
            {
                if (GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator") != null)
                {
                    //Busco el indicador seleccionado
                    Int64 _idIndicator = Convert.ToInt64(GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator"));
                    //Construyo el indicator y obtengo la magnitud.
                    Magnitud _magnitud = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).Magnitud;
                    _IdMagnitud = _magnitud.IdMagnitud;
                    //Muestra el nombre de la magnitud!
                    lblMagnitudValue.Text = Common.Functions.ReplaceIndexesTags(_magnitud.LanguageOption.Name);
                }
            }

            AddComboMeasurementUnits();
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
                if (ValidateActivitySelected())
                {
                    //Agregar seleccion de NotificationRecipient
                    //Aqui carga el List<> de los notificationRecipients seleccionados
                    List<NotificationRecipient> _notificationRecipients = GetNotificationRecipients();
                    //Si al menos hay un mail seleccionado, sigo adelante, sino muestra mensaje
                    if (_notificationRecipients.Count > 0)
                    {
                        //Obtiene Indicator
                        Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(Convert.ToInt64(GetKeyValue(_RtvIndicator.SelectedNode.Value, "IdIndicator")));
                        //Obtiene Measurement Unit
                        MeasurementUnit _measurementUnit = _indicator.Magnitud.MeasurementUnit(Convert.ToInt64(GetKeyValue(_RdcMeasurementUnit.SelectedValue, "IdMeasurementUnit")));
                        Int64 _idMagnitud = _indicator.Magnitud.IdMagnitud;

                        Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();
                        //Aca recorre el ArrayList con los id que fueron chequeado, y contruye un Dictionary, para pasar al ADD.
                        foreach (KeyValuePair<String, String> _item in _Operands)
                        {
                            Condesus.EMS.Business.IOperand _operand = null;
                            if (_item.Value.Contains("IdTransformation"))
                            {
                                //Esta es una transformacion
                                _operand = _Measurement.Transformation(Convert.ToInt64(GetKeyValue(_item.Value, "IdTransformation")));
                            }
                            else
                            {
                                if (_item.Value.Contains("IdMeasurement"))
                                {
                                    //Esta es medicion
                                    _operand = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(Convert.ToInt64(GetKeyValue(_item.Value, "IdMeasurement")));
                                }
                                else
                                {
                                    if (_item.Value.Contains("IdConstant"))
                                    {
                                        //Esta es una constante
                                        _operand = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(Convert.ToInt64(GetKeyValue(_item.Value, "IdConstantClassification"))).Constant(Convert.ToInt64(GetKeyValue(_item.Value, "IdConstant")));
                                    }
                                }
                            }
                            //construye el diccionario de operadores con el key = letra y el operador = medicion,constante o transformacion.
                            _operands.Add(_item.Key, _operand);
                        }

                        if (ValidationFormulaVariable(txtFormula.Text, _operands))
                        {
                            Int64 _idActivity = Convert.ToInt64(GetKeyValue(_RtvAccountingActivity.SelectedNode.Value, "IdActivity"));
                            AccountingActivity _accountingActivity = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingActivity(_idActivity);

                            //Guardo y me traigo la Formula generada para guardar el Id y agregarle Parametros
                            if (Entity == null)
                            {
                                //si viene el idTransformation, y el Entity es nulo, quiere decir que es una transformacion de transformacion.
                                //if (Convert.ToInt64(GetPKfromNavigator("IdTransformation")) != 0)
                                if (_IdTransformation != 0)
                                {
                                    //Es un ADD desde una Transformacion
                                    //Entity = _Measurement.Transformation(Convert.ToInt64(GetPKfromNavigator("IdTransformation"))).TransformationAdd(_indicator, _measurementUnit, txtFormula.Text, txtName.Text, txtDescription.Text, _accountingActivity, _operands, _notificationRecipients);
                                    Entity = _Measurement.Transformation(_IdTransformation).TransformationAdd(_indicator, _measurementUnit, txtFormula.Text, txtName.Text, txtDescription.Text, _accountingActivity, _operands, _notificationRecipients);
                                }
                                else
                                {
                                    if (Convert.ToInt64(GetPKfromNavigator("IdTransformation")) != 0)
                                    {
                                        Entity = _Measurement.Transformation(Convert.ToInt64(GetPKfromNavigator("IdTransformation"))).TransformationAdd(_indicator, _measurementUnit, txtFormula.Text, txtName.Text, txtDescription.Text, _accountingActivity, _operands, _notificationRecipients);
                                    }
                                    else
                                    {
                                        //Es un ADD desde una medicion
                                        Entity = _Measurement.TransformationAdd(_indicator, _measurementUnit, txtFormula.Text, txtName.Text, txtDescription.Text, _accountingActivity, _operands, _notificationRecipients);
                                    }
                                }
                            }
                            else
                            {
                                //Guardo lo editado y tambien los cambios a sus Parametros
                                Entity.Modify(_indicator, _measurementUnit, txtFormula.Text, txtName.Text, txtDescription.Text, _accountingActivity, _operands, _notificationRecipients);
                            }
                            base.NavigatorAddTransferVar("IdTransformation", Entity.IdTransformation);
                            base.NavigatorAddTransferVar("IdMeasurement", _Measurement.IdMeasurement);

                            String _pkValues = "IdTransformation=" + Entity.IdTransformation.ToString()
                                + "& IdMeasurement=" + _Measurement.IdMeasurement.ToString();
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                            base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.Transformation);
                            base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                            base.NavigatorAddTransferVar("EntityNameContextElement", String.Empty);
                            String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                            NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.Transformation, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                            base.StatusBar.ShowMessage(Resources.Common.SaveOK);
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
                }
                else
                {
                    //No selecciono una Actividad
                    base.StatusBar.ShowMessage(Resources.ConstantMessage.SelectAAccountingActivity, Pnyx.WebControls.PnyxStatusBar.StatusState.Warning);
                }
            }
            catch (Exception ex)
            {
                base.StatusBar.ShowMessage(ex);
            }
        }
        private Boolean ValidationFormulaVariable(String formula, Dictionary<String, Condesus.EMS.Business.IOperand> operands)
        {
            //Se verifica que las variables que se usan en la formula esten definidas como operadores.
            if (!String.IsNullOrEmpty( formula))
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
            _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("WASTEMONTH", String.Empty);
            _formulaWithoutIntrinsic = _formulaWithoutIntrinsic.Replace("WASTE", String.Empty);

            //Por ultimo saco la palabra clave BASE que es interna nuestra para la serie de datos base que se va a usar.
            Char[] _charArrayFormula = _formulaWithoutIntrinsic.ToUpper().Replace("BASE", String.Empty).ToCharArray();

            //Retorna el array de caracteres sin las palabras claves.
            return _charArrayFormula;
        }
        #endregion
    }
}
