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
    public partial class ScriptFuelOilTransfo : BasePropertiesTask
    {
        #region Internal Properties

            #region Constants
                private RadComboBox _RdcConst_DesidadFuelOil;
                private RadTreeView _RtvConstant_DesidadFuelOil;
                private RadComboBox _RdcConst_PCG_CH4;
                private RadTreeView _RtvConstant_PCG_CH4;
                private RadComboBox _RdcConst_PCG_N2O;
                private RadTreeView _RtvConstant_PCG_N2O;
                private RadComboBox _RdcConst_PCFuelOil;
                private RadTreeView _RtvConstant_PCFuelOil;
                private RadComboBox _RdcConst_EFFuelOil_CO2;
                private RadTreeView _RtvConstant_EFFuelOil_CO2;
                private RadComboBox _RdcConst_EFFuelOil_CH4;
                private RadTreeView _RtvConstant_EFFuelOil_CH4;
                private RadComboBox _RdcConst_EFFuelOil_N2O;
                private RadTreeView _RtvConstant_EFFuelOil_N2O;
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
                AddComboConst_DesidadFuelOil();
                AddComboConst_PCG_CH4();
                AddComboConst_PCG_N2O();

                AddComboConst_PCFuelOil();
                AddComboConstFEFuelOil_CO2();
                AddComboConstFEFuelOil_CH4();
                AddComboConstFEFuelOil_N2O();

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
                base.PageTitle = Resources.CommonListManage.ScriptFuelOil;
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
                Page.Title = Resources.CommonListManage.ScriptFuelOil;
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

            private void AddComboConst_DesidadFuelOil()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstDesidadFuelOil, ref _RdcConst_DesidadFuelOil, ref _RtvConstant_DesidadFuelOil, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConst_PCG_CH4()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstPCG_CH4, ref _RdcConst_PCG_CH4, ref _RtvConstant_PCG_CH4, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConst_PCG_N2O()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstPCG_N2O, ref _RdcConst_PCG_N2O, ref _RtvConstant_PCG_N2O, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConst_PCFuelOil()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstPCFuelOil, ref _RdcConst_PCFuelOil, ref _RtvConstant_PCFuelOil, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConstFEFuelOil_CO2()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstFEFuelOilCO2, ref _RdcConst_EFFuelOil_CO2, ref _RtvConstant_EFFuelOil_CO2, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConstFEFuelOil_CH4()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstFEFuelOilCH4, ref _RdcConst_EFFuelOil_CH4, ref _RtvConstant_EFFuelOil_CH4, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }
            private void AddComboConstFEFuelOil_N2O()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeConstant(ref phConstFEFuelOilN2O, ref _RdcConst_EFFuelOil_N2O, ref _RtvConstant_EFFuelOil_N2O, new RadTreeViewEventHandler(rtvConst_NodeExpand));
            }

            private Boolean ValidateComboConstantSelected()
            {

                if (Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CH4.SelectedNode.Value, "IdConstantClassification")) == 0)
                {
                    return false;
                }
                if (Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_N2O.SelectedNode.Value, "IdConstantClassification")) == 0)
                {
                    return false;
                }
                if (Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CO2.SelectedNode.Value, "IdConstantClassification")) == 0)
                {
                    return false;
                }
                if (Convert.ToInt64(GetKeyValue(_RtvConstant_PCFuelOil.SelectedNode.Value, "IdConstantClassification")) == 0)
                {
                    return false;
                }
                if (Convert.ToInt64(GetKeyValue(_RtvConstant_PCG_N2O.SelectedNode.Value, "IdConstantClassification")) == 0)
                {
                    return false;
                }
                if (Convert.ToInt64(GetKeyValue(_RtvConstant_PCG_CH4.SelectedNode.Value, "IdConstantClassification")) == 0)
                {
                    return false;
                }
                if (Convert.ToInt64(GetKeyValue(_RtvConstant_DesidadFuelOil.SelectedNode.Value, "IdConstantClassification")) == 0)
                {
                    return false;
                }

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

                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation1()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idConstantClass_EFFuelOil_CH4 = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CH4.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstantClass_EFFuelOil_N2O = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_N2O.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstantClass_EFFuelOil_CO2 = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CO2.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstantClass_PCFuelOil = Convert.ToInt64(GetKeyValue(_RtvConstant_PCFuelOil.SelectedNode.Value, "IdConstantClassification"));

                    Int64 _idConstant_EFFuelOil_CH4 = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CH4.SelectedNode.Value, "IdConstant"));
                    Int64 _idConstant_EFFuelOil_N2O = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_N2O.SelectedNode.Value, "IdConstant"));
                    Int64 _idConstant_EFFuelOil_CO2 = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CO2.SelectedNode.Value, "IdConstant"));
                    Int64 _idConstant_PCFuelOil = Convert.ToInt64(GetKeyValue(_RtvConstant_PCFuelOil.SelectedNode.Value, "IdConstant"));

                    Int64 _idConstantClass_DensidadFuelOil = Convert.ToInt64(GetKeyValue(_RtvConstant_DesidadFuelOil.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstant_DensidadFuelOil = Convert.ToInt64(GetKeyValue(_RtvConstant_DesidadFuelOil.SelectedNode.Value, "IdConstant")); ;

                    _operands.Add("A", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_DensidadFuelOil).Constant(_idConstant_DensidadFuelOil));
                    _operands.Add("B", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_PCFuelOil).Constant(_idConstant_PCFuelOil));
                    _operands.Add("C", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_EFFuelOil_CO2).Constant(_idConstant_EFFuelOil_CO2));

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation2()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    //No tiene parametros!
                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation3()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    //No tiene parametros!
                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation4()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idConstantClass_EFFuelOil_CH4 = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CH4.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstantClass_EFFuelOil_N2O = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_N2O.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstantClass_EFFuelOil_CO2 = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CO2.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstantClass_PCFuelOil = Convert.ToInt64(GetKeyValue(_RtvConstant_PCFuelOil.SelectedNode.Value, "IdConstantClassification"));

                    Int64 _idConstant_EFFuelOil_CH4 = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CH4.SelectedNode.Value, "IdConstant"));
                    Int64 _idConstant_EFFuelOil_N2O = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_N2O.SelectedNode.Value, "IdConstant"));
                    Int64 _idConstant_EFFuelOil_CO2 = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CO2.SelectedNode.Value, "IdConstant"));
                    Int64 _idConstant_PCFuelOil = Convert.ToInt64(GetKeyValue(_RtvConstant_PCFuelOil.SelectedNode.Value, "IdConstant"));

                    Int64 _idConstantClass_DensidadFuelOil = Convert.ToInt64(GetKeyValue(_RtvConstant_DesidadFuelOil.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstant_DensidadFuelOil = Convert.ToInt64(GetKeyValue(_RtvConstant_DesidadFuelOil.SelectedNode.Value, "IdConstant")); ;

                    _operands.Add("A", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_DensidadFuelOil).Constant(_idConstant_DensidadFuelOil));
                    _operands.Add("B", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_PCFuelOil).Constant(_idConstant_PCFuelOil));
                    _operands.Add("C", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_EFFuelOil_CH4).Constant(_idConstant_EFFuelOil_CH4));

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation5()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idConstantClass_PCG_CH4 = Convert.ToInt64(GetKeyValue(_RtvConstant_PCG_CH4.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstant_PCG_CH4 = Convert.ToInt64(GetKeyValue(_RtvConstant_PCG_CH4.SelectedNode.Value, "IdConstant"));

                    _operands.Add("A", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_PCG_CH4).Constant(_idConstant_PCG_CH4));

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation6()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    //No tiene parametros!
                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation7()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idConstantClass_EFFuelOil_CH4 = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CH4.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstantClass_EFFuelOil_N2O = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_N2O.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstantClass_EFFuelOil_CO2 = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CO2.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstantClass_PCFuelOil = Convert.ToInt64(GetKeyValue(_RtvConstant_PCFuelOil.SelectedNode.Value, "IdConstantClassification"));

                    Int64 _idConstant_EFFuelOil_CH4 = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CH4.SelectedNode.Value, "IdConstant"));
                    Int64 _idConstant_EFFuelOil_N2O = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_N2O.SelectedNode.Value, "IdConstant"));
                    Int64 _idConstant_EFFuelOil_CO2 = Convert.ToInt64(GetKeyValue(_RtvConstant_EFFuelOil_CO2.SelectedNode.Value, "IdConstant"));
                    Int64 _idConstant_PCFuelOil = Convert.ToInt64(GetKeyValue(_RtvConstant_PCFuelOil.SelectedNode.Value, "IdConstant"));

                    Int64 _idConstantClass_DensidadFuelOil = Convert.ToInt64(GetKeyValue(_RtvConstant_DesidadFuelOil.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstant_DensidadFuelOil = Convert.ToInt64(GetKeyValue(_RtvConstant_DesidadFuelOil.SelectedNode.Value, "IdConstant")); ;
                    
                    _operands.Add("A", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_DensidadFuelOil).Constant(_idConstant_DensidadFuelOil));
                    _operands.Add("B", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_PCFuelOil).Constant(_idConstant_PCFuelOil));
                    _operands.Add("C", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_EFFuelOil_N2O).Constant(_idConstant_EFFuelOil_N2O));

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation8()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    Int64 _idConstantClass_PCG_N2O = Convert.ToInt64(GetKeyValue(_RtvConstant_PCG_N2O.SelectedNode.Value, "IdConstantClassification"));
                    Int64 _idConstant_PCG_N2O = Convert.ToInt64(GetKeyValue(_RtvConstant_PCG_N2O.SelectedNode.Value, "IdConstant"));

                    _operands.Add("A", EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClass_PCG_N2O).Constant(_idConstant_PCG_N2O));

                    return _operands;
                }
                private Dictionary<String, Condesus.EMS.Business.IOperand> BuildOperatorForTransformation9()
                {
                    Dictionary<String, Condesus.EMS.Business.IOperand> _operands = new Dictionary<String, Condesus.EMS.Business.IOperand>();

                    //No tiene parametros!
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
                        Int64 _idIndicatorCH4 = 2;
                        Int64 _idIndicatorN2O = 3;
                        Int64 _idIndicatorCO2 = 1;
                        Int64 _idIndicatorCO2eFromCH4 = 7;
                        Int64 _idIndicatorCO2eFromN2O = 9;
                        Int64 _idIndicatorCO2eFromCO2 = 8;
                        Int64 _idIndicatorCO2e = 22;
                        Int64 _idMeasurementUnit_ton = 213;                     //213->ton	Tonelada EE.UU.

                        //Construye el Scope de la transaccion (todo lo que este dentro va en transaccion)
                        using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                        {
                            //1-Por cada site Seleccionado:
                            //Aca recorre el ArrayList con los id que fueron chequeado
                            //foreach (String _item in _SitesAux)
                            //{
                            //    //Obtiene los Id del Site seleccionado
                            //    Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_item, "IdOrganization"));
                            //    Int64 _idFacility = Convert.ToInt64(GetKeyValue(_item, "IdFacility"));
                            //    Int64 _idSector = Convert.ToInt64(GetKeyValue(_item, "IdSector"));
                            //    Int64 _idSite = _idSector == 0 ? _idFacility : _idSector;

                            //    #region Tarea Medicion BASE
                            //        //Construye el Site seleccionado y los demas datos para la Tarea de Medición Base!
                            //        Site _site = EMSLibrary.User.GeographicInformationSystem.Site(_idSite);
                            //        //El Root del Facility no cambia, es el mismo para todo
                            //        String _facilityRootName = GetFacilityRoot(_site.IdFacility).LanguageOption.Name;
                            //    #endregion

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

                            ////Obtenemos todas las mediciones que son requeridas...
                            //Int64 _idMeasurement_PCFuelOil = Convert.ToInt64(GetKeyValue(_RdcMeasurement_PCFuelOil.SelectedValue, "IdMeasurement"));
                            //Int64 _idMeasurement_EFFuelOil_CO2 = Convert.ToInt64(GetKeyValue(_RdcMeasurement_EFFuelOil_CO2.SelectedValue, "IdMeasurement"));
                            //Int64 _idMeasurement_EFFuelOil_CH4 = Convert.ToInt64(GetKeyValue(_RdcMeasurement_EFFuelOil_CH4.SelectedValue, "IdMeasurement"));
                            //Int64 _idMeasurement_EFFuelOil_N2O = Convert.ToInt64(GetKeyValue(_RdcMeasurement_EFFuelOil_N2O.SelectedValue, "IdMeasurement"));

                            #region   1- Alta de Transformacion Hija de Medicion Base:
                            String _transformationName = _facilityRootAndSiteName + " - Conversión a CO__2__";
                            CalculateOfTransformation _calculateOfTransformation_1 = TransformationAdd(_idIndicatorCO2, _idMeasurementUnit_ton,
                                null, _calcultaeOfTransformationBase, BuildOperatorForTransformation1(), _transformationName, String.Empty, "Base*A/1000000*B*C");
                            #endregion

                            #region   2- Transformacion 2 hija de 1:
                            _transformationName = _facilityRootAndSiteName + " - Conversión a CO__2e__ desde CO__2__";
                            CalculateOfTransformation _calculateOfTransformation_2 = TransformationAdd(_idIndicatorCO2eFromCO2, _idMeasurementUnit_ton,
                                null, _calculateOfTransformation_1, BuildOperatorForTransformation2(), _transformationName, String.Empty, "Base");
                            #endregion

                            #region   3- Transformacion 3 hija de 2:
                            _transformationName = _facilityRootAndSiteName + " - Conversión a CO__2e__";
                            CalculateOfTransformation _calculateOfTransformation_3 = TransformationAdd(_idIndicatorCO2e, _idMeasurementUnit_ton,
                                null, _calculateOfTransformation_2, BuildOperatorForTransformation3(), _transformationName, String.Empty, "Base");
                            #endregion

                            #region   4- Transformacion 4 hija de Medicion Base:
                            _transformationName = _facilityRootAndSiteName + " - Conversión a CH__4__";
                            CalculateOfTransformation _calculateOfTransformation_4 = TransformationAdd(_idIndicatorCH4, _idMeasurementUnit_ton,
                                null, _calcultaeOfTransformationBase, BuildOperatorForTransformation4(), _transformationName, String.Empty, "Base*A/1000000*B*C");
                            #endregion

                            #region   5- Transformacion 5 hija de 4:
                            _transformationName = _facilityRootAndSiteName + " - Conversión a CO__2e__ desde CH__4__";
                            CalculateOfTransformation _calculateOfTransformation_5 = TransformationAdd(_idIndicatorCO2eFromCH4, _idMeasurementUnit_ton,
                                null, _calculateOfTransformation_4, BuildOperatorForTransformation5(), _transformationName, String.Empty, "Base*A");
                            #endregion

                            #region   6- Transformacion 6 hija de 5:
                            _transformationName = _facilityRootAndSiteName + " - Conversión a CO__2e__";
                            CalculateOfTransformation _calculateOfTransformation_6 = TransformationAdd(_idIndicatorCO2e, _idMeasurementUnit_ton,
                                null, _calculateOfTransformation_5, BuildOperatorForTransformation6(), _transformationName, String.Empty, "Base");
                            #endregion

                            #region   7- Transformacion 7 hija de Medición Base:
                            _transformationName = _facilityRootAndSiteName + " - Conversión a N__2__O";
                            CalculateOfTransformation _calculateOfTransformation_7 = TransformationAdd(_idIndicatorN2O, _idMeasurementUnit_ton,
                                null, _calcultaeOfTransformationBase, BuildOperatorForTransformation7(), _transformationName, String.Empty, "Base*A*B*C");
                            #endregion

                            #region   8- Transformacion 8 hija de 7:
                            _transformationName = _facilityRootAndSiteName + " - Conversión a CO__2e__ desde N__2__O";
                            CalculateOfTransformation _calculateOfTransformation_8 = TransformationAdd(_idIndicatorCO2eFromN2O, _idMeasurementUnit_ton,
                                null, _calculateOfTransformation_7, BuildOperatorForTransformation8(), _transformationName, String.Empty, "Base*A");
                            #endregion

                            #region   9- Transformacion 9 hija de 8:
                            _transformationName = _facilityRootAndSiteName + " - Conversión a CO__2e__";
                            CalculateOfTransformation _calculateOfTransformation_9 = TransformationAdd(_idIndicatorCO2e, _idMeasurementUnit_ton,
                                null, _calculateOfTransformation_8, BuildOperatorForTransformation9(), _transformationName, String.Empty, "Base");
                            #endregion

                            //}

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
