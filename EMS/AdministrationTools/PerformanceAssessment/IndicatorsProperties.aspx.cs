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

namespace Condesus.EMS.WebUI.PA
{
    public partial class IndicatorsProperties : BaseProperties
    {
        #region Internal Properties
            CompareValidator _CvMagnitud;
            private Indicator _Entity = null;
            private Int64 _IdIndicatorClassification
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdIndicatorClassification") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdIndicatorClassification")) : 0;
                }
            }
            private Int64 _IdIndicator
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdIndicator") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdIndicator")) : 0;
                }
            }
            private Indicator Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private RadComboBox _RdcMagnitud;
            private RadTreeView _RtvIndicatorClassification;
            private ArrayList _IndicatorClassificationAux //Estructura interna para guardar los id de clasificacion que son seleccionados.
            {
                get
                {
                    if (ViewState["IndicatorClassificationAux"] == null)
                    {
                        ViewState["IndicatorClassificationAux"] = new ArrayList();
                    }
                    return (ArrayList)ViewState["IndicatorClassificationAux"];
                }
                set { ViewState["IndicatorClassificationAux"] = value; }
            }
        #endregion

        #region PageLoad & Init
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

                AddTreeViewIndicatorClassifications();

                base.InjectCheckIndexesTags();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                
                AddCombos();
                AddValidators();

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);

                    //Realiza la carga de los datos de Clasificaciones, para que se puedan usar.
                    LoadDataIndicatorClassification();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.Indicator;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Indicator;
                lblClassifications.Text = Resources.CommonListManage.IndicatorClassification;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblMagnitud.Text = Resources.CommonListManage.Magnitud;
                lblName.Text = Resources.CommonListManage.Name;
                lblScope.Text = Resources.CommonListManage.Scope;
                lblDefinition.Text = Resources.CommonListManage.Definition;
                lblLimitation.Text = Resources.CommonListManage.Limitation;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
            }
            private void AddCombos()
            {
                AddComboMagnitudes();
            }
            private void AddValidators()
            {
                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.Magnitudes, phMagnitudValidator, ref _CvMagnitud, _RdcMagnitud, Resources.ConstantMessage.SelectAMagnitud);
            }
            private void AddComboMagnitudes()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phMagnitud, ref _RdcMagnitud, Common.ConstantsEntitiesName.PA.Magnitudes, String.Empty, _params, false, true, false, false, false);
            }
            private void AddTreeViewIndicatorClassifications()
            {
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                BuildGenericDataTable(Common.ConstantsEntitiesName.PA.IndicatorClassifications, _params);

                //Arma tree con todos los roots.
                phIndicatorClassifications.Controls.Clear();
                _RtvIndicatorClassification = base.BuildHierarchicalListManageContent(Common.ConstantsEntitiesName.PA.IndicatorClassifications, "Form");
                //Ya tengo el Tree le attacho los Handlers
                _RtvIndicatorClassification.NodeExpand += new RadTreeViewEventHandler(_RtvIndicatorClassification_NodeExpand);
                _RtvIndicatorClassification.NodeCreated += new RadTreeViewEventHandler(_RtvIndicatorClassification_NodeCreated);
                _RtvIndicatorClassification.NodeCheck += new RadTreeViewEventHandler(_RtvIndicatorClassification_NodeCheck);
                phIndicatorClassifications.Controls.Add(_RtvIndicatorClassification);
            }
            /// <summary>
            /// Este metodo se encarga solamente de cargar los datos dentro del TreeView que ya debe existir en la pagina (que se crea en el init)
            /// </summary>
            private void LoadDataIndicatorClassification()
            {
                _RtvIndicatorClassification.Nodes.Clear();
                //Con el tree ya armado, ahora hay que llenarlo con datos.
                base.LoadGenericTreeView(ref _RtvIndicatorClassification, Common.ConstantsEntitiesName.PA.IndicatorClassifications, Common.ConstantsEntitiesName.PA.IndicatorClassification, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty, false, false);
            }
            private void LoadStructIndicatorClassificationAux()
            {
                //Carga de forma inicial todos los id de clasificacion que ya estan cargados en este proyecto.
                _IndicatorClassificationAux = new ArrayList();
                Dictionary<Int64, Condesus.EMS.Business.PA.Entities.IndicatorClassification> _indicatorClassifications = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.IndicatorClassification>();
                if (_IdIndicator != 0)
                {
                    _indicatorClassifications = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_IdIndicator).Classifications;
                }
                //Ahora recorre todas las clasificaciones que ya tiene asiganadas el indicador, y los guarda en la estructura interna (ArrayList).
                foreach (Condesus.EMS.Business.PA.Entities.IndicatorClassification _item in _indicatorClassifications.Values)
                {
                    _IndicatorClassificationAux.Add(_item.IdIndicatorClassification);
                }
            }
            private void SetMagnitud()
            {
                _RdcMagnitud.SelectedValue = "IdMagnitud=" + Entity.Magnitud.IdMagnitud.ToString();
            }
            private void LoadData()
            {
                //Contruye el LG de indicators.
                Condesus.EMS.Business.PA.Entities.Indicator_LG _indicator_LG = Entity.LanguagesOptions.Item(Global.DefaultLanguage);
                //Setea el nombre en el PageTitle
                base.PageTitle = _indicator_LG.Name;

                //Recarga el check en el edit.
                if (_Entity.IsCumulative)
                {
                    chkIsCumulative.Checked = true;
                }
                else
                {
                    chkIsCumulative.Checked = false;
                }
                chkIsCumulative.Enabled = false;    //No se puede editar...
                txtName.Text = _indicator_LG.Name;
                txtDescription.Text = _indicator_LG.Description;
                lblLanguageValue.Text = _indicator_LG.Language.Name;
                txtDefinition.Text = _indicator_LG.Definition;
                txtLimitation.Text = _indicator_LG.Limitation;
                txtScope.Text = _indicator_LG.Scope;

                //Setea el valor de Magnitud en el combo.
                SetMagnitud();
                //Carga la estructura paralela con las clasificaciones que tiene el indicador.
                LoadStructIndicatorClassificationAux();
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //limpio los textbox por si hay datos
                txtDescription.Text = String.Empty;
                txtName.Text = String.Empty;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;
            }
        #endregion

        #region Page Events
            protected void _RtvIndicatorClassification_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                base.NodeExpand(sender, e, Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty);
            }
            protected void _RtvIndicatorClassification_NodeCreated(object sender, RadTreeNodeEventArgs e)
            {
                Int64 _idIndicatorClassification = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdIndicatorClassification"));
                if (_IndicatorClassificationAux.Contains(_idIndicatorClassification))
                {
                    e.Node.Checked = true;
                }
                else
                {
                    e.Node.Checked = false;
                }
                //Si el usuario no tiene permisos de manage sobre la Clasificacion que viene (que se crea), no puede seleccionarla para asociarla.
                String _permissionType = e.Node.Attributes["PermissionType"].ToString();
                if (_permissionType != Common.Constants.PermissionManageName)
                {
                    e.Node.Checkable = false;
                }
            }
            protected void _RtvIndicatorClassification_NodeCheck(object sender, RadTreeNodeEventArgs e)
            {
                RadTreeNode _node = e.Node;

                //Obtiene el Id del nodo checkeado
                Int64 _idIndicatorClass = Convert.ToInt64(GetKeyValue(_node.Value, "IdIndicatorClassification"));
                if (_IndicatorClassificationAux.Contains(_idIndicatorClass))
                {
                    if (!_node.Checked)
                    {
                        _IndicatorClassificationAux.Remove(_idIndicatorClass);
                    }
                }
                else
                {
                    _IndicatorClassificationAux.Add(_idIndicatorClass);
                }
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //Se deben insertar los indicadores a 1 o mas Clasificaciones
                    Dictionary<Int64, Condesus.EMS.Business.PA.Entities.IndicatorClassification> _indicatorClassifications = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.IndicatorClassification>();
                    Int64 _idMagnitud = Convert.ToInt64(GetKeyValue(_RdcMagnitud.SelectedValue, "IdMagnitud"));
                    //Aca recorre el ArrayList con los id que fueron chequeado, y contruye un Dictionary, para pasar al ADD.
                    foreach (Int64 _item in _IndicatorClassificationAux)
                    {
                        _indicatorClassifications.Add(_item, EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_item));
                    }
                    Magnitud _magnitud = EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud);

                    //verifica si es un ADD o un Modify
                    if (Entity == null)
                    {
                        Entity = EMSLibrary.User.PerformanceAssessments.Map.IndicatorAdd(_magnitud, chkIsCumulative.Checked, txtName.Text, txtDescription.Text, txtScope.Text, txtLimitation.Text, txtDefinition.Text, _indicatorClassifications);
                    }
                    else
                    {
                        Entity.Modify(_magnitud, txtName.Text, txtDescription.Text, txtScope.Text, txtLimitation.Text, txtDefinition.Text, _indicatorClassifications);
                    }

                    base.NavigatorAddTransferVar("IdIndicator", Entity.IdIndicator);
                    String _pkValues = "IdIndicator=" + Entity.IdIndicator.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.Indicator);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Indicator);
                    base.NavigatorAddTransferVar("EntityNameContextElement", Common.ConstantsEntitiesName.PA.Indicator);

                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.Indicator, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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

