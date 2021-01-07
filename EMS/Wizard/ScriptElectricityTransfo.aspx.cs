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
    public partial class ScriptElectricityTransfo : BasePropertiesTask
    {
        #region Internal Properties

        #region Constants
        #endregion

            #region Info Process
                private RadComboBox _RdcProcess;
                private RadComboBox _RdcAccountingActivity;
                private RadTreeView _RtvAccountingActivity;
                private CompareValidator _CvAccountingActivity;
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
                private RadComboBox _RdcMeasurementOfTransformation;
                private RadTreeView _RtvMeasurementOfTransformation;
                private Int64 _IdProcess
                {
                    get
                    {
                        if (ViewState["IdProcess"] == null)
                        {
                            ViewState["IdProcess"] = new Int64();
                        }
                        return (Int64)ViewState["IdProcess"];
                    }
                    set { ViewState["IdProcess"] = value; }
                }
                private Int64 _IdActivity
                {
                    get
                    {
                        if (ViewState["IdActivity"] == null)
                        {
                            ViewState["IdActivity"] = new Int64();
                        }
                        return (Int64)ViewState["IdActivity"];
                    }
                    set { ViewState["IdActivity"] = value; }
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
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddComboProcess();

                AddComboAccountingActivities();
                AddComboMeasurementOfTransformation(_IdProcess, _IdActivity);

                AddTreeViewEmails();

                base.InjectCheckIndexesTags();

                if (!Page.IsPostBack)
                {
                    //Form
                    base.SetContentTableRowsCss(tblContentForm);

                    LoadDataEmails();
                    LoadData();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonListManage.ScriptDiesel;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadData()
            {
            }
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ScriptDiesel;
            }
            private void AddComboProcess()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phProcess, ref _RdcProcess, Common.ConstantsEntitiesName.PF.ProcessGroupProcessesWithoutClassification, String.Empty, _params, false, true, false, true, false);
            }
            private void AddComboAccountingActivities()
            {
                String _filterExpression = String.Empty;
                //Combo de AccountingActivity Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTree(phAccountingActivity, ref _RdcAccountingActivity, ref _RtvAccountingActivity,
                    Common.ConstantsEntitiesName.PA.AccountingActivities, _params, false, false, true, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand), new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeClick), Resources.Common.ComboBoxNoDependency, false);

                //ValidatorRequiredField(Common.ConstantsEntitiesName.PA.AccountingActivities, phAccountingActivityValidator, ref _CvAccountingActivity, _RdcAccountingActivity, Resources.ConstantMessage.SelectAAccountingActivity);
            }
            private void AddComboMeasurementOfTransformation(Int64 idProcess, Int64 idActivity)
            {
                String _filterExpression = String.Empty;
                //Combo de AccountingActivity Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if ((idProcess > 0) && (idActivity > 0))
                {
                    _params.Add("IdProcess", idProcess);
                    _params.Add("IdActivity", idActivity);
                }
                AddComboWithTree(phTransformation, ref _RdcMeasurementOfTransformation, ref _RtvMeasurementOfTransformation,
                    Common.ConstantsEntitiesName.PA.MeasurementsOfTransformation, _params, false, false, true, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvMeasurementOfTransformation_NodeExpand), Resources.Common.ComboBoxNoDependency, false);

                //ValidatorRequiredField(Common.ConstantsEntitiesName.PA.AccountingActivities, phAccountingActivityValidator, ref _CvAccountingActivity, _RdcAccountingActivity, Resources.ConstantMessage.SelectAAccountingActivity);
            }
       
            private Boolean ValidateComboConstantSelected()
            {

                //Si llega aca, es que todo esta seleccionado!
                return true;
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


                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation(CalculateOfTransformation calcultaeOfTransformationBase)
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    _operands.Add("A", calcultaeOfTransformationBase);

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
            protected void rtvHierarchicalTreeViewInCombo_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                //Verifica si hay seleccionado los valores fijos de un combo (-2, -1 o 0)
                String _selectedValue = ((RadTreeView)sender).SelectedValue;

                _IdProcess = Convert.ToInt64(GetKeyValue(_RdcProcess.SelectedValue, "IdProcess"));
                _IdActivity = Convert.ToInt64(GetKeyValue(_selectedValue, "IdActivity"));

                AddComboMeasurementOfTransformation(_IdProcess, _IdActivity);
            }
            protected void rtvMeasurementOfTransformation_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                //Limpia todo lo que hay, por las dudas.
                e.Node.Nodes.Clear();
                String _entityNameHierarchicalChildren = String.Empty;

                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                //Si estoy expandiendo el primer nivel o sea measurement, busco las transformaciones
                if (e.Node.Level == 0)
                {
                    _entityNameHierarchicalChildren = Common.ConstantsEntitiesName.PA.BasedMeasurementsOfTheTransformations;
                }
                else
                {
                    //Si estoy en los niveles siguientes, entonces busco las transformaciones de transformaciones...
                    _entityNameHierarchicalChildren = Common.ConstantsEntitiesName.PA.TransformationsByTransformation;
                }

                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(_entityNameHierarchicalChildren, _params);
                foreach (DataRow _drRecord in DataTableListManage[_entityNameHierarchicalChildren].Rows)
                {
                    RadTreeNode _node = SetNodeTreeViewManage(_drRecord, _entityNameHierarchicalChildren);
                    e.Node.Nodes.Add(_node);
                    SetExpandMode(_node, _entityNameHierarchicalChildren, false, false);
                }
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    if (ValidateComboConstantSelected())
                    {
                        Int64 _idIndicatorCO2e = 22;
                        Int64 _idMeasurementUnit_ton = 213;                     //213->ton	Tonelada EE.UU.

                        //Construye el Scope de la transaccion (todo lo que este dentro va en transaccion)
                        using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                        {
                            //--------------TRANSFORMACIONES---------------------------
                            //Obtenemos la Transformacion Seleccionada que actua como BASE!
                            Int64 _idMeasurementOfTransformationBase = Convert.ToInt64(GetKeyValue(_RtvMeasurementOfTransformation.SelectedNode.Value, "IdMeasurement"));
                            Int64 _idTransformationBase = Convert.ToInt64(GetKeyValue(_RtvMeasurementOfTransformation.SelectedNode.Value, "IdTransformation"));
                            CalculateOfTransformation _calcultaeOfTransformationBase = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurementOfTransformationBase).Transformation(_idTransformationBase);

                            //Obtenemos el Nombre de la transformacion para sacar el facility y el site.
                            String _transformationBaseName = _calcultaeOfTransformationBase.LanguageOption.Name;
                            int _index = _transformationBaseName.LastIndexOf(" - ");
                            String _facilityRootAndSiteName = String.Empty;
                            if (_index > 0)
                            {
                                _facilityRootAndSiteName = _transformationBaseName.Substring(0, _index);
                            }
                            else
                            {   //Como no hay guion, me quedo con el texto completo...
                                _facilityRootAndSiteName = _transformationBaseName;
                            }

                            #region   1- Alta de Transformacion Hija de Medicion Base:

                            String _transformationName = _facilityRootAndSiteName + " - Conversión a CO__2e__";
                            CalculateOfTransformation _calculateOfTransformation_1 = TransformationAdd(_idIndicatorCO2e, _idMeasurementUnit_ton,
                                null, _calcultaeOfTransformationBase, BuildOperatorForTransformation(_calcultaeOfTransformationBase), _transformationName, String.Empty, "Base/1000*A");

                            #endregion

                            //Finaliza la transaccion
                            _transactionScope.Complete();
                        }

                        base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                    }
                    else
                    {

                        base.StatusBar.ShowMessage("Debe seleccionar todas las constantes", Pnyx.WebControls.PnyxStatusBar.StatusState.Warning);
                    }
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
        #endregion
    }
}
