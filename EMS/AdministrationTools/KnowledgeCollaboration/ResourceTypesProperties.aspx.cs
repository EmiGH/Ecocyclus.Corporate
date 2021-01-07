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
using Condesus.EMS.Business.KC.Entities;

namespace Condesus.EMS.WebUI.KC
{
    public partial class ResourceTypesProperties : BaseProperties
    {
        #region Internal Properties

        private Condesus.EMS.Business.KC.Entities.ResourceType _Entity = null;
        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdResourceType") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdResourceType")) : 0;
            }
        }
        private Condesus.EMS.Business.KC.Entities.ResourceType Entity
        {
            get
            {
                try
                {
                    if (_Entity == null)
                        _Entity = EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceType(_IdEntity);

                    return _Entity;
                }
                catch { return null; }
            }

            set { _Entity = value; }
        }

        private RadTreeView _RtvResourceTypeVar;
        private RadTreeView _RtvResourceType
        {
            get
            {
                Session["rtvResourceType"] = _RtvResourceTypeVar;
                return _RtvResourceTypeVar;
            }
            set
            {
                Session["rtvResourceType"] = value;
                _RtvResourceTypeVar = value;
            }
        }
        private RadComboBox _RdcResourceTypeVar;
        private RadComboBox _RdcResourceType
        {
            get
            {
                Session["rdcResourceType"] = _RdcResourceTypeVar;
                return _RdcResourceTypeVar;
            }
            set
            {
                Session["rdcResourceType"] = value;
                _RdcResourceTypeVar = value;
            }
        }
        private String _FilterExpression;

        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);

                base.InjectCheckIndexesTags();
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                AddCombos();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                    this.txtName.Focus();
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.ResourceType;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ResourceType;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                lblParentResourceType.Text = Resources.CommonListManage.Parent;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
                rfv2.ErrorMessage = Resources.ConstantMessage.ValidationInvalidSpecialCharacter;
            }
            private void ClearLocalSession()
            {
                _RdcResourceType = null;
                _RtvResourceType = null;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                lblLanguageValue.Text = Global.DefaultLanguage.Name;
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
                
                _RdcResourceType.SelectedValue = Common.Constants.ComboBoxNoDependencyValue;
            }
            private void LoadData()
            {
                base.PageTitle = Entity.LanguageOption.Name;

                txtName.Text = Entity.LanguageOption.Name;
                txtDescription.Text = Entity.LanguageOption.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;            

                //si es un root, no debe hacer nada de esto.
                if (_Entity.ParentResourceType != null)
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdResourceType=" + _Entity.ParentResourceType.IdResourceType.ToString();
                    RadTreeView _rtvParent = _RtvResourceType;
                    RadComboBox _rcbParent = _RdcResourceType;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _rtvParent, ref _rcbParent, "ResourceType", "ResourceTypesChildren");
                    _RdcResourceType = _rcbParent;
                    _RtvResourceType = _rtvParent;
                }
            }
            private void AddCombos()
            {
                AddComboResourceType();
            }
            private void AddComboResourceType()
            {
                if (Entity != null)
                {
                    _FilterExpression = "IdResourceType<>" + Entity.IdResourceType.ToString();
                }
                //Combo Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTree(phParentResourceType, ref _RdcResourceTypeVar, ref _RtvResourceTypeVar,
                    "ResourceTypes", _params, false, false, true, ref _FilterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);
            }
        #endregion

        #region Page Events
       
        //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
        //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
        protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            if (Entity != null)
            {
                _FilterExpression = "IdResourceType<>" + Entity.IdResourceType.ToString();
            }
            NodeExpand(sender, e, Common.ConstantsEntitiesName.KC.ResourceTypeChildren, _FilterExpression, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtiene el key necesario.
                Object _obj = GetKeyValue(_RtvResourceType.SelectedNode.Value, "IdResourceType");   //Si lo saco del tree, funciona!!!.
                //Si el key obtenido no llega a exister devuelve null.
                Int64 _parentValue = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.

                if (Entity == null)
                {
                    //Alta
                    Entity = EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceTypeAdd(_parentValue, txtName.Text, txtDescription.Text);
                }
                else
                {
                    //Modificacion
                    EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceType(_IdEntity).Modify(_parentValue, txtName.Text, txtDescription.Text);
                    Entity = EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceType(_IdEntity);
                }
                Int64 _idParent = (Entity.ParentResourceType == null) ? 0 : Entity.ParentResourceType.IdResourceType;
                base.NavigatorAddTransferVar("IdResourceType", Entity.IdResourceType);
                base.NavigatorAddTransferVar("IdParentResourceType", _idParent);

                String _pkValues = "IdResourceType=" + Entity.IdResourceType.ToString()
                    + "& IdParentResourceType=" + _idParent.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.KC.ResourceType);
                base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.ResourceType + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.KC.ResourceType, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                base.StatusBar.ShowMessage(Resources.Common.SaveOK);
            }
            catch (System.Exception ex)
            {
                base.StatusBar.ShowMessage(ex);
            }
        }
       
        #endregion
    }
}

