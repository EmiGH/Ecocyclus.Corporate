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
using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class RelationshipsProperties : BaseProperties
    {
        #region Internal Properties
            private OrganizationRelationship _Entity = null;
            RadComboBox _RdcOrganization2;
            private RadTreeView _RtvOrganization;
            CompareValidator _CvOrganization2;
            RadComboBox _RdcRelationshipType;
            CompareValidator _CvRelationshipType;
            private Int64 _IdOrganizationRelationshipType
            {
                get
                {
                    object _o = ViewState["IdOrganizationRelationshipType"];
                    if (_o != null)
                        return (Int64)ViewState["IdOrganizationRelationshipType"];

                    return 0;
                }

                set
                {
                    ViewState["IdOrganizationRelationshipType"] = value;
                }
            }
            private Int64 _IdOrganization
            {
                get
                {
                    object _o = ViewState["IdOrganization"];
                    if (_o != null)
                        return (Int64)ViewState["IdOrganization"];

                    return 0;
                }

                set
                {
                    ViewState["IdOrganization"] = value;
                }
            }
            private Int64 _IdOrganization2
            {
                get
                {
                    object _o = ViewState["IdOrganization2"];
                    if (_o != null)
                        return (Int64)ViewState["IdOrganization2"];

                    return 0;
                }

                set
                {
                    ViewState["IdOrganization2"] = value;
                }
            }
            private OrganizationRelationship Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).OrganizationRelationship(_IdOrganization, _IdOrganization2, _IdOrganizationRelationshipType);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
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
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddCombos();

                if (!Page.IsPostBack)
                {
                    InitFkVars();

                    //Inicializo el Form
                    if (Entity == null)
                        Add();

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }
            }
            private void InitFkVars()
            {
                //Aca intenta obtener el/los Id desde el TransferVar, si no esta ahi, entonces lo busca en las PKEntity.
                _IdOrganization = base.NavigatorContainsTransferVar("IdOrganization") ? base.NavigatorGetTransferVar<Int64>("IdOrganization") : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                _IdOrganizationRelationshipType = base.NavigatorContainsTransferVar("IdOrganizationRelationshipType") ? base.NavigatorGetTransferVar<Int64>("IdOrganizationRelationshipType") : Convert.ToInt64(GetPKfromNavigator("IdOrganizationRelationshipType"));
                _IdOrganization2 = NavigatorContainsTransferVar("IdOrganization2") ? base.NavigatorGetTransferVar<Int64>("IdOrganization2") : Convert.ToInt64(GetPKfromNavigator("IdOrganization2"));
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

                    base.PageTitle = (Entity != null) ? Entity.OrganizationRelationshipType.LanguageOption.Name + " - " + Entity.OrganizationRelated.Name : Resources.CommonListManage.OrganizationRelationships;
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
                Page.Title = Resources.CommonListManage.OrganizationRelationships;
                lblOrganization2.Text = Resources.CommonListManage.Organization;
                lblRelationshipType.Text = Resources.CommonListManage.OrganizationRelationshipType;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                _RdcRelationshipType.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
            }
            private void AddCombos()
            {
                AddComboOrganizationRelationshipTypes();
                AddComboOrganizations();
            }
            private void AddComboOrganizations()
            {
                String _filterExpression = "IdOrganization<>" + _IdOrganization.ToString();
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phOrganization2, ref _RdcOrganization2, ref _RtvOrganization,
                    Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationsRoots, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvOrganizations_NodeExpand),
                    Resources.Common.ComboBoxNoDependency,false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, phOrganization2Validator, ref _CvOrganization2, _RdcOrganization2, Resources.ConstantMessage.SelectAnOrganization);
            }
            private void AddComboOrganizationRelationshipTypes()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phRelationshipType, ref _RdcRelationshipType, Common.ConstantsEntitiesName.DS.OrganizationRelationshipTypes, String.Empty, _params, false, true, false, false, false);
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationRelationshipTypes, phRelationshipTypeValidator, ref _CvRelationshipType, _RdcRelationshipType, Resources.ConstantMessage.SelectARelationshipType);
            }
        #endregion

        #region Page Events
            protected void rtvOrganizations_NodeExpand(object sender, RadTreeNodeEventArgs e)
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
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Organizations, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        if(GetKeyValue(_node.Value, "IdOrganization").ToString() != _IdOrganization.ToString())
                            e.Node.Nodes.Add(_node);
                    }
                }
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    Int64 _idOrganization2 = Convert.ToInt64(GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));
                    Int64 _idRelationshipType = Convert.ToInt64(GetKeyValue(_RdcRelationshipType.SelectedValue, "IdOrganizationRelationshipType"));

                    if (Entity == null)
                    {
                        //Alta
                        Organization _organizationRelated = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization2);
                        OrganizationRelationshipType _organizationRelationshipType = EMSLibrary.User.DirectoryServices.Configuration.OrganizationRelationshipType(_idRelationshipType);
                        EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).OrganizationRelationshipsAdd(_organizationRelated, _organizationRelationshipType);

                        Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).OrganizationRelationship(_IdOrganization, _idOrganization2, _idRelationshipType);

                        base.NavigatorAddTransferVar("IdOrganization2", _idOrganization2);
                        base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);
                        base.NavigatorAddTransferVar("IdOrganizationRelationshipType", _idRelationshipType);

                        String _pkValues = "IdOrganization2=" + _idOrganization2.ToString()
                            + "& IdOrganization=" + _IdOrganization.ToString()
                            + "& IdOrganizationRelationshipType=" + _idRelationshipType.ToString();
                        base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);


                        base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.OrganizationRelationship);
                        if (Convert.ToString(GetPKfromNavigator("ParentEntity"))!="0")
                        {
                            base.NavigatorAddTransferVar("ParentEntity", Convert.ToString(GetPKfromNavigator("ParentEntity")));
                        }
                        else
                        {
                            if (base.NavigatorContainsTransferVar("ParentEntity"))
                            {
                                base.NavigatorAddTransferVar("ParentEntity", base.NavigatorGetTransferVar<String>("ParentEntity"));
                            }
                        }

                        base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));
                        //Navigate("~/MainInfo/ListViewer.aspx", Convert.ToString(GetPKfromNavigator("PageTitle")), Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                        String _entityPropertyName = String.Concat(Entity.Organization.CorporateName, " ", Entity.OrganizationRelationshipType.LanguageOption.Name, " ", Entity.OrganizationRelated.CorporateName);
                        NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.OrganizationRelationship, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);
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