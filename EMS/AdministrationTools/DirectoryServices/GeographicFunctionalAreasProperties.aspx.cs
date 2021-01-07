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
using Condesus.EMS.Business.GIS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class GeographicFunctionalAreasProperties : BaseProperties
    {
        #region Internal Properties
            //CompareValidator _CvOrganization;
            CompareValidator _CvFunctionalArea;
            CompareValidator _CvGeographicArea;
            private GeographicFunctionalArea _Entity = null;
            //RadComboBox _RdcOrganization;
            //private RadTreeView _RtvOrganization;
            private Int64 _IdOrganization
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                    //Int64 _idOrg = base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : 0;
                    //if (_idOrg == 0)
                    //{
                    //    //Quiere decir que no vino el IdOrg por parametros... seguramente es un ADD.
                    //    //Entonces lo guarda localmente y trabaja con esto.
                    //    if (Session["IdOrganization_local"] == null)
                    //    {
                    //        //Como aun nadie la seteo, devuelve por defecto la del usuario
                    //        Session["IdOrganization_local"] = IdOrganizationDefaultUser;
                    //    }
                    //    //Finalmente retorna el ID.
                    //    return (Int64)Session["IdOrganization_local"];
                    //}
                    //else
                    //{
                    //    return _idOrg;
                    //}
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
            private Int64 _IdGeographicArea
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdGeographicArea") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdGeographicArea")) : 0;
                }
                //get
                //{
                //    //Si no existe, entonces traba de buscarlo en el TransferVar, y si tampoco esta, pone 0
                //    if (Session["IdGeographicArea"] != null)
                //        { Session["IdGeographicArea"] = base.NavigatorContainsTransferVar("IdGeographicArea") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdGeographicArea")) : Session["IdGeographicArea"]; }
                //    else
                //        { Session["IdGeographicArea"] = 0; }

                //    return Convert.ToInt64(Session["IdGeographicArea"]);
                //}
                //set { Session["IdGeographicArea"] = value; }
            }
            private GeographicFunctionalArea Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                        {
                            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_IdGeographicArea);
                            FunctionalArea _funArea = _organization.FunctionalArea(_IdFunctionalArea);
                            _Entity = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        }
                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }

            private RadComboBox _RdcFunctionalArea;
            private RadTreeView _RtvFunctionalArea;
            private RadComboBox _RdcGeographicFunctionalArea;
            private RadTreeView _RtvGeographicFunctionalArea;
            private RadComboBox _RdcGeographicArea;
            private RadTreeView _RtvGeographicArea;
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
                
                //agrega el handler para navegar a los Add de otras entidades.
                lnkFunctionalArea.Click += new EventHandler(lnkFunctionalArea_Click);
                lnkGeographicArea.Click += new EventHandler(lnkGeographicArea_Click);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                AddCombos();
                AddValidators();
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
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.Name() : Resources.CommonListManage.GeographicFunctionalArea;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.GeographicFunctionalArea;
                lblFunctionalArea.Text = Resources.CommonListManage.FunctionalArea;
                lblGeographicArea.Text = Resources.CommonListManage.GeographicArea;
                lblGeographicFunctionalArea.Text = Resources.CommonListManage.GeographicFunctionalArea;
                lblOrganization.Text = Resources.CommonListManage.Organization;
            }
            private void ClearLocalSession()
            {
                _RdcFunctionalArea = null;
                _RtvFunctionalArea = null;
                _RdcGeographicFunctionalArea = null;
                _RtvGeographicFunctionalArea = null;
                _RdcGeographicArea = null;
                _RtvGeographicArea = null;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //_RdcOrganization.Enabled = true;
                _RdcFunctionalArea.Enabled = true;
                _RdcGeographicArea.Enabled = true;
            }
            private void LoadData()
            {
                base.PageTitle = Entity.Name();

                //_RdcOrganization.Enabled = false;
                _RdcFunctionalArea.Enabled = false;
                _RdcGeographicArea.Enabled = false;

                ////Seteamos la organizacion...
                ////Realiza el seteo del parent en el Combo-Tree.
                //Organization _oganization = EMSLibrary.User.DirectoryServices.Map.Organization(Entity.FunctionalArea.IdOrganization);
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


                SetParentGeographicFunctionalArea();
                SetParentFunctionalArea();
                SetGeographicArea();

            }
            private void SetParentGeographicFunctionalArea()
            {
                //si es un root, no debe hacer nada de esto.
                if (Entity.ParentGeographicFunctionalArea != null)
                {
                    if ((Entity.ParentGeographicFunctionalArea.FunctionalArea.IdFunctionalArea != 0) || (Entity.ParentGeographicFunctionalArea.GeographicArea.IdGeographicArea != 0))
                    {
                        //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                        String _keyValues = "IdGeographicArea=" + Entity.ParentGeographicFunctionalArea.GeographicArea.IdGeographicArea.ToString() + "& IdFunctionalArea=" + Entity.ParentGeographicFunctionalArea.FunctionalArea.IdFunctionalArea.ToString() + "& IdOrganization=" + _IdOrganization.ToString();
                        RadTreeView _rtvGeoFunArea = _RtvGeographicFunctionalArea;
                        RadComboBox _rcbGeoFunArea = _RdcGeographicFunctionalArea;
                        //Realiza el seteo del parent en el Combo-Tree.
                        SelectItemTreeViewParent(_keyValues, ref _rtvGeoFunArea, ref _rcbGeoFunArea, Common.ConstantsEntitiesName.DS.GeographicFunctionalArea, Common.ConstantsEntitiesName.DS.GeographicFunctionalAreaChildren);
                        _RdcGeographicFunctionalArea = _rcbGeoFunArea;
                        _RtvGeographicFunctionalArea = _rtvGeoFunArea;
                    }
                }
            }
            private void SetParentFunctionalArea()
            {
                //si es un root, no debe hacer nada de esto.
                if (Entity.FunctionalArea.IdFunctionalArea != 0)
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdFunctionalArea=" + Entity.FunctionalArea.IdFunctionalArea.ToString() + "& IdOrganization=" + _IdOrganization.ToString();
                    RadTreeView _rtvFunArea = _RtvFunctionalArea;
                    RadComboBox _rcbFunArea = _RdcFunctionalArea;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _rtvFunArea, ref _rcbFunArea, Common.ConstantsEntitiesName.DS.FunctionalArea, Common.ConstantsEntitiesName.DS.FunctionalAreaChildren);
                    _RdcFunctionalArea = _rcbFunArea;
                    _RtvFunctionalArea = _rtvFunArea;
                }
            }
            private void SetGeographicArea()
            {
                //si es un root, no debe hacer nada de esto.
                if (Entity.GeographicArea.IdGeographicArea != 0)
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdGeographicArea=" + Entity.GeographicArea.IdGeographicArea.ToString();
                    RadTreeView _rtvGeoArea = _RtvGeographicArea;
                    RadComboBox _rcbGeoArea = _RdcGeographicArea;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _rtvGeoArea, ref _rcbGeoArea, Common.ConstantsEntitiesName.DS.GeographicArea, Common.ConstantsEntitiesName.DS.GeographicAreaChildren);
                    _RdcGeographicArea = _rcbGeoArea;
                    _RtvGeographicArea = _rtvGeoArea;
                }
            }
            private void AddCombos()
            {
                //AddComboOrganizations();
                AddComboGeographicFunctionalAreas();
                AddComboFunctionalAreas();
                AddComboGeographicAreas();
            }
            private void AddValidators()
            {
                //ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, phOrganizationValidator, ref _CvOrganization, _RdcOrganization, Resources.ConstantMessage.SelectAnOrganization);
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.FunctionalAreas, phFunctionalAreaValidator, ref _CvFunctionalArea, _RdcFunctionalArea, Resources.ConstantMessage.SelectAFunctionalArea);
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.GeographicAreas, phGeographicAreaValidator, ref _CvGeographicArea, _RdcGeographicArea, Resources.ConstantMessage.SelectAGeographicArea);
            }
           
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

            private void AddComboGeographicAreas()
            {
                String _filterExpression = String.Empty;
                //Combo de GeographicArea Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);
                AddComboWithTree(phGeographicArea, ref _RdcGeographicArea, ref _RtvGeographicArea,
                    Common.ConstantsEntitiesName.DS.GeographicAreas, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboGeoArea_NodeExpand),
                    Common.Constants.ComboBoxSelectItemDefaultPrefix,false);
            }
            private void AddComboFunctionalAreas()
            {
                String _filterExpression = String.Empty;
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);
                AddComboWithTree(phFunctionalArea, ref _RdcFunctionalArea, ref _RtvFunctionalArea,
                    Common.ConstantsEntitiesName.DS.FunctionalAreas, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboFunArea_NodeExpand),
                    Common.Constants.ComboBoxSelectItemDefaultPrefix, false);
            }
            private void AddComboGeographicFunctionalAreas()
            {
                String _filterExpression = String.Empty;
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);
                AddComboWithTree(phGeographicFunctionalArea, ref _RdcGeographicFunctionalArea, ref _RtvGeographicFunctionalArea,
                    Common.ConstantsEntitiesName.DS.GeographicFunctionalAreas, _params, false, false, true, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboGeoFunArea_NodeExpand),
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
            //    AddComboGeographicFunctionalAreas();
            //    AddComboFunctionalAreas();
            //    AddComboGeographicAreas();
            //}

            //void _RdcOrganization_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            //{
            //    ClearLocalSession();
            //    IdOrganization = Convert.ToInt64(GetKeyValue(e.Value, "IdOrganization"));
            //    AddComboGeographicFunctionalAreas();
            //    AddComboFunctionalAreas();
            //    AddComboGeographicAreas();
            //}
            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            protected void rtvHierarchicalTreeViewInComboFunArea_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.FunctionalAreaChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);                             
            }
            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            protected void rtvHierarchicalTreeViewInComboGeoFunArea_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.GeographicFunctionalAreaChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);               
            }
            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            protected void rtvHierarchicalTreeViewInComboGeoArea_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.GeographicAreaChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }

            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //Obtiene el key necesario. para armar el Parent
                    Object _obj = GetKeyValue(_RtvGeographicFunctionalArea.SelectedNode.Value, "IdFunctionalArea");   //Si lo saco del tree, funciona!!!.
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _parentValueFunctionalArea = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    _obj = GetKeyValue(_RtvGeographicFunctionalArea.SelectedNode.Value, "IdGeographicArea");   //Si lo saco del tree, funciona!!!.
                    Int64 _parentValueGeographicArea = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.

                    //con esto obtiene los ID para armar el FunctionalPosition
                    Int64 _idGeographicArea = Convert.ToInt64(GetKeyValue(_RtvGeographicArea.SelectedValue, "IdGeographicArea"));
                    Int64 _idFunctionalArea = Convert.ToInt64(GetKeyValue(_RtvFunctionalArea.SelectedValue, "IdFunctionalArea"));

                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                    GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);

                    FunctionalArea _funAreaParent = _organization.FunctionalArea(_parentValueFunctionalArea);
                    GeographicArea _geoAreaParent = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_parentValueGeographicArea);

                    GeographicFunctionalArea _geoFunAreaParent = _organization.GeographicFunctionalArea(_funAreaParent, _geoAreaParent);

                    if (Entity == null)
                    {
                        //Alta
                        _organization.GeographicFunctionalAreasAdd(_funArea, _geoArea, _geoFunAreaParent);
                        Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).GeographicFunctionalArea(_funArea, _geoArea);
                    }
                    else
                    {
                        //Modificacion
                        _organization.GeographicFunctionalAreasModify(_Entity, _geoFunAreaParent);
                        Entity = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        
                        //base.NavigatorAddTransferVar("IdOrganization", Entity.FunctionalArea.IdOrganization);
                        //base.NavigatorAddTransferVar("IdFunctionalArea", Entity.FunctionalArea.IdFunctionalArea);
                        //base.NavigatorAddTransferVar("IdGeographicArea", Entity.GeographicArea.IdGeographicArea);
                        //base.NavigatorAddTransferVar("IdParentFunctionalArea", Entity.ParentGeographicFunctionalArea.FunctionalArea.IdFunctionalArea);
                        //base.NavigatorAddTransferVar("IdParentGeographicArea", Entity.ParentGeographicFunctionalArea.GeographicArea.IdGeographicArea);
                        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.Name() + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    }
                    base.NavigatorAddTransferVar("IdOrganization", Entity.FunctionalArea.IdOrganization);
                    base.NavigatorAddTransferVar("IdFunctionalArea", Entity.FunctionalArea.IdFunctionalArea);
                    base.NavigatorAddTransferVar("IdGeographicArea", Entity.GeographicArea.IdGeographicArea);
                    base.NavigatorAddTransferVar("IdParentFunctionalArea", Entity.IdParentFunctionalArea);
                    base.NavigatorAddTransferVar("IdParentGeographicArea", Entity.IdParentGeographicArea);

                    String _pkValues = "IdOrganization=" + Entity.FunctionalArea.IdOrganization.ToString()
                        + "& IdFunctionalArea=" + Entity.FunctionalArea.IdFunctionalArea.ToString()
                        + "& IdGeographicArea=" + Entity.GeographicArea.IdGeographicArea.ToString()
                        + "& IdParentFunctionalArea=" + Entity.IdParentFunctionalArea.ToString()
                        + "& IdParentGeographicArea=" + Entity.IdParentGeographicArea.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.GeographicFunctionalArea);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                    
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.GeographicFunctionalArea + " " + Entity.Name(), Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.Name());
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.GeographicFunctionalArea, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
            void lnkGeographicArea_Click(object sender, EventArgs e)
            {
                //Config
                String _urlProperties = GetGlobalResourceObject("URLEntityProperties", Common.ConstantsEntitiesName.DS.GeographicArea).ToString();
                Navigate(_urlProperties, Common.ConstantsEntitiesName.DS.GeographicArea);
            }
            void lnkFunctionalArea_Click(object sender, EventArgs e)
            {
                //Config
                String _urlProperties = GetGlobalResourceObject("URLEntityProperties", Common.ConstantsEntitiesName.DS.FunctionalArea).ToString();
                Navigate(_urlProperties, Common.ConstantsEntitiesName.DS.FunctionalArea);
            }
        #endregion
    }
}