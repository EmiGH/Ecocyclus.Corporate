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
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.AdministrationTools.PerformanceAssessment
{
    public partial class MethodologiesProperties : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdEntity
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdMethodology") ? base.NavigatorGetTransferVar<Int64>("IdMethodology") : 0;
                }
            }
            private Methodology _Entity = null;
            private Methodology Entity
            {
                get
                {
                    if (_Entity == null)
                    {
                        _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.Methodology(_IdEntity);
                    }

                    return _Entity;
                }

                set { _Entity = value; }
            }
            private RadComboBox _RdcResource;
            private RadTreeView _RtvResource;
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
                AddComboResources();

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
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.MethodName : Resources.CommonListManage.Methodology;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Methodology;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblType.Text = Resources.CommonListManage.MethodType;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
                rfvType.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                cvType.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
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
                txtName.Text = Entity.LanguageOption.MethodName;
                txtDescription.Text = Entity.LanguageOption.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;
                txtType.Text = Entity.LanguageOption.MethodType;
                SetResources();
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
                if (Entity.Resource != null)
                {
                    //Realiza el seteo del parent en el Combo-Tree.
                    Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(Entity.Resource.IdResource);
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
        #endregion

        #region Page Events
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
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
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
                        Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource);

                        if (Entity == null)
                        {
                            //Alta
                            Entity = EMSLibrary.User.PerformanceAssessments.Configuration.MethodologyAdd(_resource, txtName.Text, txtType.Text, txtDescription.Text);
                        }
                        else
                        {
                            //Modificacion
                            Entity.Modify(_resource, txtName.Text, txtType.Text, txtDescription.Text);
                        }
                        base.NavigatorAddTransferVar("IdMethodology", Entity.IdMethodology);

                        String _pkValues = "IdMethodology=" + Entity.IdMethodology.ToString();
                        base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.Methodology);
                        base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                        String _entityPropertyName = String.Concat(Entity.LanguageOption.MethodName);
                        NavigatePropertyEntity(Common.ConstantsEntitiesName.PA.Methodology, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                        base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                    }
                    else
                    {
                        //Caso contrario, no hace nada y muestra un mensaje.
                        base.StatusBar.ShowMessage(Resources.ConstantMessage.msgIncorrectSelectionResource, Pnyx.WebControls.PnyxStatusBar.StatusState.Warning);
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
