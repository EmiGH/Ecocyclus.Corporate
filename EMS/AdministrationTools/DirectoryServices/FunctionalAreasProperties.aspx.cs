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
    public partial class FunctionalAreasProperties : BaseProperties
    {
        #region Internal Properties
            //CompareValidator _CvOrganization;
            private FunctionalArea _Entity = null;
            //RadComboBox _RdcOrganization;
            //private RadTreeView _RtvOrganization;
            private Int64 _IdOrganization
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                //    Int64 _idOrg = base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : 0;
                //    if (_idOrg == 0)
                //    {
                //        //Quiere decir que no vino el IdOrg por parametros... seguramente es un ADD.
                //        //Entonces lo guarda localmente y trabaja con esto.
                //        if (Session["IdOrganization_local"] == null)
                //        {
                //            //Como aun nadie la seteo, devuelve por defecto la del usuario
                //            Session["IdOrganization_local"] = IdOrganizationDefaultUser;
                //        }
                //        //Finalmente retorna el ID.
                //        return (Int64)Session["IdOrganization_local"];
                //    }
                //    else
                //    {
                //        return _idOrg;
                //    }
                }
                //set { Session["IdOrganization_local"] = value; }
            }
            private Int64 _IdFunctionalArea
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdFunctionalArea") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdFunctionalArea")) : 0;
                }
            }
            private FunctionalArea Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).FunctionalArea(_IdFunctionalArea);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
            private RadTreeView _RtvFunctionalArea;
            private RadComboBox _RdcFunctionalArea;
            private String _FilterExpressionFunctionalArea;
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
                AddCombos();
                //AddValidators();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                lblOrganizationValue.Text = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).CorporateName;

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                        { Add(); }
                    else
                        { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);                   
                    this.txtFunctionalArea.Focus();
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.LanguageOption.Name : Resources.CommonListManage.FunctionalArea;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.FunctionalArea;
                lblFunctionalArea.Text = Resources.CommonListManage.FunctionalArea;
                lblIdParent.Text = Resources.CommonListManage.Parent;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblMnemo.Text = Resources.CommonListManage.Mnemo;
                lblOrganization.Text = Resources.CommonListManage.Organization;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void ClearLocalSession()
            {
                _RdcFunctionalArea = null;
                _RtvFunctionalArea = null;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                _FilterExpressionFunctionalArea = String.Empty;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;
                txtFunctionalArea.Text = String.Empty;
                txtMnemo.Text = String.Empty;
                txtFunctionalArea.ReadOnly = false;
                txtMnemo.ReadOnly = false;
                //_RdcOrganization.Enabled = true;
                
            }
            private void LoadData()
            {
                base.PageTitle = Entity.LanguageOption.Name;

                txtMnemo.Text = Entity.Mnemo;
                txtFunctionalArea.Text = Entity.LanguagesOptions.Item(Global.DefaultLanguage.IdLanguage).Name;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;

                txtFunctionalArea.ReadOnly = false;
                txtMnemo.ReadOnly = false;
                //_RdcOrganization.Enabled = false;

                _FilterExpressionFunctionalArea = "IdFunctionalArea<>" + _Entity.IdParentFunctionalArea.ToString();

                ////Seteamos la organizacion...
                ////Realiza el seteo del parent en el Combo-Tree.
                //Organization _oganization = EMSLibrary.User.DirectoryServices.Map.Organization(Entity.IdOrganization);
                //String _keyValuesElement = "IdOrganization=" + _oganization.IdOrganization.ToString();
                //if (_oganization.Classifications.Count > 0)
                //{
                //    String _keyValuesClassification = "IdOrganizationClassification=" + _oganization.Classifications.First().Value.IdOrganizationClassification;
                //    SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvOrganization, ref _RdcOrganization, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.Organizations);
                //}
                //else
                //{
                //    SelectItemTreeViewParent(_keyValuesElement, ref _RtvOrganization, ref _RdcOrganization, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Organizations);
                //}

                //si es un root, no debe hacer nada de esto.
                if (_Entity.IdParentFunctionalArea != 0)
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdFunctionalArea=" + _Entity.IdParentFunctionalArea.ToString() + "& IdOrganization=" + _IdOrganization.ToString();
                    RadTreeView _rtvFunArea = _RtvFunctionalArea;
                    RadComboBox _rcbFunArea = _RdcFunctionalArea;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _rtvFunArea, ref _rcbFunArea, Common.ConstantsEntitiesName.DS.FunctionalArea, Common.ConstantsEntitiesName.DS.FunctionalAreaChildren);
                    _RdcFunctionalArea= _rcbFunArea;
                    _RtvFunctionalArea= _rtvFunArea;
                }
            }
            private void AddCombos()
            {
                //AddComboOrganizations();
                AddComboFunctionalAreas();
            }
            //private void AddValidators()
            //{
            //    ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, phOrganizationValidator, ref _CvOrganization, _RdcOrganization, Resources.ConstantMessage.SelectAnOrganization);
            //}
            //private void AddComboOrganizations()
            //{
            //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
            //    String _selectedValue = "IdOrganization=" + IdOrganization.ToString();
            //    AddCombo(phOrganization, ref _RdcOrganization, Common.ConstantsEntitiesName.DS.Organizations, _selectedValue, _params, false, true, false);
            //    _RdcOrganization.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_RdcOrganization_SelectedIndexChanged);
            //    FwMasterPage.RegisterContentAsyncPostBackTrigger(_RdcOrganization, "SelectedIndexChanged");
            //}
            //private void AddComboOrganizations()
            //{
            //    String _filterExpression = String.Empty;
            //    //Combo de Organizaciones
            //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
            //    AddComboWithTreeElementMaps(ref phOrganization, ref _RdcOrganization, ref _RtvOrganization,
            //        Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationsRoots, _params, false, true, false, ref _filterExpression,
            //        new RadTreeViewEventHandler(rtvOrganizations_NodeExpand),
            //        new RadTreeViewEventHandler(rtvOrganizations_NodeClick),
            //        Resources.Common.ComboBoxNoDependency);

            //    ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, phOrganizationValidator, ref _CvOrganization, _RdcOrganization, Resources.ConstantMessage.SelectAnOrganization);
            //}

            private void AddComboFunctionalAreas()
            {
                //Combo Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);
                AddComboWithTree(phFunctionalArea, ref _RdcFunctionalArea, ref _RtvFunctionalArea,
                    Common.ConstantsEntitiesName.DS.FunctionalAreas, _params, false, false, true, ref _FilterExpressionFunctionalArea,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);
            }
        #endregion

        #region Page Events
            //protected void rtvOrganizations_NodeExpand(object sender, RadTreeNodeEventArgs e)
            //{
            //    //Limpio los hijos, para no duplicar al abrir y cerrar.
            //    e.Node.Nodes.Clear();
            //    Dictionary<String, Object> _params = new Dictionary<String, Object>();
            //    _params = GetKeyValues(e.Node.Value);

            //    //Primero lo hace sobre las Clasificaciones Hijas...
            //    //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
            //    BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, _params);
            //    if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren))
            //    {
            //        foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren].Rows)
            //        {
            //            RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
            //            e.Node.Nodes.Add(_node);
            //            //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
            //            SetExpandMode(_node, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, true);
            //        }
            //    }

            //    //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
            //    BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Organizations, _params);
            //    if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.Organizations))
            //    {
            //        foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.Organizations].Rows)
            //        {
            //            RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Organizations, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
            //            e.Node.Nodes.Add(_node);
            //        }
            //    }
            //}
            //protected void rtvOrganizations_NodeClick(object sender, RadTreeNodeEventArgs e)
            //{
            //    ClearLocalSession();
            //    IdOrganization = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdOrganization"));
            //    AddComboFunctionalAreas();
            //}

            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.FunctionalAreaChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }
            //void _RdcOrganization_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            //{
            //    ClearLocalSession();
            //    IdOrganization = Convert.ToInt64(GetKeyValue(e.Value, "IdOrganization"));
            //    AddComboFunctionalAreas();
            //}
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //Obtiene el key necesario.
                    Object _obj = GetKeyValue(_RtvFunctionalArea.SelectedNode.Value, "IdFunctionalArea");   //Si lo saco del tree, funciona!!!.
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _parentValue = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                    FunctionalArea _funAreaParent = _organization.FunctionalArea(_parentValue);

                    if (Entity == null)
                    {
                        //Alta
                        Entity = _organization.FunctionalAreasAdd(_funAreaParent, txtFunctionalArea.Text, txtMnemo.Text);
                        
                    }
                    else
                    {
                        //Modificacion
                        EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).FunctionalAreasModify(Entity, _funAreaParent, txtFunctionalArea.Text, txtMnemo.Text);
                        Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).FunctionalArea(_IdFunctionalArea);
                    }
                    base.NavigatorAddTransferVar("IdOrganization", Entity.IdOrganization);
                    base.NavigatorAddTransferVar("IdFunctionalArea", Entity.IdFunctionalArea);
                    base.NavigatorAddTransferVar("IdParentFunctionalArea", Entity.IdParentFunctionalArea);

                    String _pkValues = "IdOrganization=" + Entity.IdOrganization.ToString() +
                        "& IdFunctionalArea=" + Entity.IdFunctionalArea.ToString() +
                        "& IdParentFunctionalArea=" + Entity.IdParentFunctionalArea.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.LanguageOption.Name + " " + Resources.Common.Edit);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.FunctionalArea);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.FunctionalArea + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.FunctionalArea, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
