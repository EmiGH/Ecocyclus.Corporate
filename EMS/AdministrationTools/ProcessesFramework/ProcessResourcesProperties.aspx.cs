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
using Condesus.EMS.Business.PF.Entities;

namespace Condesus.EMS.WebUI.ManagementTools.ProcessesMap
{
    public partial class ProcessResourcesProperties : BaseProperties
    {
        #region Internal Properties
            CompareValidator _CvResource;
            private Int64 _IdProcess
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdProcess") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdProcess")) : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
                }
            }
            private Int64 _IdResource
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdResource") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdResource")) : Convert.ToInt64(GetPKfromNavigator("IdResource"));
                }
            }
            private ProcessResource _Entity = null;
            private ProcessResource Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_IdProcess).ProcessResource(_IdResource);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private RadComboBox _RdcResource;
            private RadTreeView _RtvResource;
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
                    { Add(); }
                    else
                    { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }
            }
            protected override void SetPagetitle()
            {
                //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                String _pageTitle = Convert.ToString(GetPKfromNavigator("PageTitle"));
                if (_pageTitle != "0")
                {
                    base.PageTitle = _pageTitle;
                }
                else
                {
                    if (Entity != null)
                    {
                        String _title = Entity.Resource.LanguageOption.Title;
                        base.PageTitle = _title;
                    }
                    else
                    {
                        base.PageTitle = Resources.CommonListManage.ProcessResource;
                    }
                }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ProcessResource;
                lblComment.Text = Resources.CommonListManage.Value;
                lblResources.Text = Resources.CommonListManage.Resource;
            }
            private void Add()
            {
                base.StatusBar.Clear();
                txtComment.Text = String.Empty;
            }
            private void LoadData()
            {
                txtComment.Text = Entity.Comment;
                SetResources();
                _RdcResource.Enabled = false;
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

                ValidatorRequiredField(Common.ConstantsEntitiesName.KC.ResourceClassifications, phResourcesValidator, ref _CvResource, _RdcResource, Resources.ConstantMessage.SelectAResource);
            }
            private void SetResources()
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
                    String _pkValues = String.Empty;

                    //Obtiene el key necesario.
                    Int64 _idResource = Convert.ToInt64(GetKeyValue(_RtvResource.SelectedNode.Value, "IdResource"));
                    Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource);

                    if (Entity == null)
                    {
                        //Alta
                        EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess).ResourceAdd(_resource, txtComment.Text);
                        base.NavigatorAddTransferVar("IdResource", _idResource);
                        

                        _pkValues = "IdResource=" + _idResource.ToString()
                            + "& IdProcess=" + _IdProcess.ToString();
                        
                    }
                    else
                    {
                        //Modificacion
                        EMSLibrary.User.ProcessFramework.Map.Process(_IdProcess).ResourceModify(_IdResource, txtComment.Text);
                        base.NavigatorAddTransferVar("IdResource", _IdResource);

                        _pkValues = "IdResource=" + _IdResource.ToString()
                            + "& IdProcess=" + _IdProcess.ToString();

                    }
                    base.NavigatorAddTransferVar("IdProcess", _IdProcess);

                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessResource);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                    base.NavigatorAddTransferVar("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                    String _entityPropertyName = String.Concat(txtComment.Text);
                    
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.PF.ProcessResource, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
