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
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PA.Entities;
using System.Globalization;
using System.Threading;

namespace Condesus.EMS.WebUI.AdministrationTools.PerformanceAssessment
{
    public partial class ConstantsProperties : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdConstant") ? base.NavigatorGetTransferVar<Int64>("IdConstant") : 0;
                }
            }
            private Int64 _IdConstantClassification
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdConstantClassification") ? base.NavigatorGetTransferVar<Int64>("IdConstantClassification") : 0;
                }
            }            
            private Constant _Entity = null;
            private Constant Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                        {
                            _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_IdConstantClassification).Constant(_IdEntity);
                        }

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private CompareValidator _CvMeasurementUnit;
            private RadComboBox _RdcMeasurementUnit;
            CompareValidator _CvMagnitud;
            private RadComboBox _RdcMagnitud;
            CompareValidator _CvConstantClassification;
            private RadComboBox _RdcConstantClassification;
            private RadTreeView _RtvConstantClassification;
        #endregion

        #region PageLoad & Init
            protected override void InyectJavaScript()
            {
                base.InyectJavaScript();

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
                AddCombos();

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                        Add();
                    else
                        LoadData(); //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    this.txtName.Focus();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.Constant;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Constant;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblConstantClassification.Text=Resources.CommonListManage.ConstantClassification;
                lblMagnitud.Text = Resources.CommonListManage.Magnitud;
                lblMeasurementUnit.Text = Resources.CommonListManage.MeasurementUnit;
                lblSymbol.Text = Resources.CommonListManage.Symbol;
                lblValue.Text = Resources.CommonListManage.Value;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
                rfvSymbol.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtName.Text = Entity.LanguageOption.Name;
                txtDescription.Text = Entity.LanguageOption.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
                txtSymbol.Text = Entity.Symbol;
                txtValue.Text = Entity.Value.ToString().Replace(',', '.');
                
                SetConstantClassification();
                SetMagnitud();
                AddComboMeasurementUnits();
                SetMeasurementUnit();
            }
            private void AddCombos()
            {
                AddComboConstantClassification();
                AddComboMagnitudes();
                AddComboMeasurementUnits();
            }

            private void AddComboMeasurementUnits()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (GetKeyValue(_RdcMagnitud.SelectedValue, "IdMagnitud") != null)
                {
                    Int64 _idMagnitud = Convert.ToInt64(GetKeyValue(_RdcMagnitud.SelectedValue, "IdMagnitud"));
                    _params.Add("IdMagnitud", _idMagnitud);
                }

                AddCombo(phMeasurementUnit, ref _RdcMeasurementUnit, Common.ConstantsEntitiesName.PA.MeasurementUnits, String.Empty, _params, false, true, false, false, false);
                
                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.MeasurementUnits, phMeasurementUnitValidator, ref _CvMeasurementUnit, _RdcMeasurementUnit, Resources.ConstantMessage.SelectAMeasurementUnit);
            }
            private void SetMeasurementUnit()
            {
                _RdcMeasurementUnit.SelectedValue = "IdMeasurementUnit=" + Entity.MeasurementUnit.IdMeasurementUnit.ToString() + "& IdMagnitud=" + Entity.MeasurementUnit.Magnitud.IdMagnitud.ToString();
            }
            private void AddComboMagnitudes()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phMagnitud, ref _RdcMagnitud, Common.ConstantsEntitiesName.PA.Magnitudes, String.Empty, _params, false, true, false, true, false);
                _RdcMagnitud.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_RdcMagnitud_SelectedIndexChanged);
                
                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.Magnitudes, phMagnitudValidator, ref _CvMagnitud, _RdcMagnitud, Resources.ConstantMessage.SelectAMagnitud);
            }
            private void SetMagnitud()
            {
                _RdcMagnitud.SelectedValue = "IdMagnitud=" + Entity.MeasurementUnit.Magnitud.IdMagnitud.ToString();
            }
            private void SetConstantClassification()
            {
                //si es un root, no debe hacer nada de esto.
                if (Entity.ConstantClassification.IdConstantClassification != 0)
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdConstantClassification=" + Entity.ConstantClassification.IdConstantClassification.ToString();
                    RadTreeView _rtvConstantClass = _RtvConstantClassification;
                    RadComboBox _rcbConstantClass = _RdcConstantClassification;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _rtvConstantClass, ref _rcbConstantClass, Common.ConstantsEntitiesName.PA.ConstantClassification, Common.ConstantsEntitiesName.PA.ConstantClassificationChildren);
                    _RdcConstantClassification = _rcbConstantClass;
                    _RtvConstantClassification = _rtvConstantClass;
                }
            }
            private void AddComboConstantClassification()
            {
                String _filterExpression = String.Empty;
                //Combo de ConstantClassification Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTree(phConstantClassification, ref _RdcConstantClassification, ref _RtvConstantClassification,
                    Common.ConstantsEntitiesName.PA.ConstantClassifications, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboConstantClassification_NodeExpand),
                    Common.Constants.ComboBoxSelectItemDefaultPrefix, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.ConstantClassifications, phConstantClassificationValidator, ref _CvConstantClassification, _RdcConstantClassification, Resources.ConstantMessage.SelectAConstantClassification);
            }
        #endregion

        #region Page Events
            protected void rtvHierarchicalTreeViewInComboConstantClassification_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.PA.ConstantClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }
            protected void _RdcMagnitud_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                AddComboMeasurementUnits();
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //Obtiene el key necesario.
                    Object _obj = GetKeyValue(_RtvConstantClassification.SelectedNode.Value, "IdConstantClassification");   //Si lo saco del tree, funciona!!!.
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _parentValue = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    ConstantClassification _constantClassification = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_parentValue);
                    Int64 _idMagnitud = Convert.ToInt64(GetKeyValue(_RdcMeasurementUnit.SelectedValue, "IdMagnitud"));
                    Int64 _idMeasurementUnit = Convert.ToInt64(GetKeyValue(_RdcMeasurementUnit.SelectedValue, "IdMeasurementUnit"));
                    MeasurementUnit _measurementUnit = EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud).MeasurementUnit(_idMeasurementUnit);

                    //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                    //Y en este caso para que el separador decimal sea punto "."
                    CultureInfo _cultureUSA = new CultureInfo("en-US");
                    //Me guarda la actual, para luego volver a esta...
                    CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                    //Seta la cultura estandard
                    Thread.CurrentThread.CurrentCulture = _cultureUSA;

                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_parentValue).ConstantAdd(txtSymbol.Text, Convert.ToDouble(txtValue.Text), _measurementUnit, txtName.Text, txtDescription.Text, _constantClassification);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(txtSymbol.Text, Convert.ToDouble(txtValue.Text), _measurementUnit, txtName.Text, txtDescription.Text, _constantClassification);
                    }
                    //Vuelve a la cultura original...
                    Thread.CurrentThread.CurrentCulture = _currentCulture;

                    base.NavigatorAddTransferVar("IdConstantClassification", Entity.ConstantClassification.IdConstantClassification);
                    base.NavigatorAddTransferVar("IdConstant", Entity.IdConstant);

                    String _pkValues = "IdConstantClassification=" + Entity.ConstantClassification.IdConstantClassification.ToString()
                        + "&IdConstant=" + Entity.IdConstant.ToString();

                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.Constant);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.Constant, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
