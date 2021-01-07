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
    public partial class FunctionalPositionsProperties : BaseProperties
    {
        #region Internal Properties
            //CompareValidator _CvOrganization;
            CompareValidator _CvFunctionalArea;
            CompareValidator _CvPosition;
            private FunctionalPosition _Entity = null;
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
            private Int64 _IdPosition
            {
                get
                {
                    //Si no existe, entonces traba de buscarlo en el TransferVar, y si tampoco esta, pone 0
                    if (Session["IdPosition"] != null)
                        { Session["IdPosition"] = base.NavigatorContainsTransferVar("IdPosition") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdPosition")) : Session["IdPosition"]; }
                    else
                        { Session["IdPosition"] = 0; }

                    return Convert.ToInt64(Session["IdPosition"]);
                }
                set { Session["IdPosition"] = value; }
            }
            private FunctionalPosition Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                        {
                            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                            Position _position = _organization.Position(_IdPosition);
                            FunctionalArea _funArea = _organization.FunctionalArea(_IdFunctionalArea);
                            _Entity = _organization.FunctionalPosition(_position, _funArea);
                        }
                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }

            private RadComboBox _RdcFunctionalArea;
            private RadTreeView _RtvFunctionalArea;

            private RadComboBox _RdcFunctionalPosition;
            private RadTreeView _RtvFunctionalPosition;

            private RadComboBox _RdcPosition;
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
                lblOrganizationValue.Text = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).CorporateName;

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
                }
            }
            //Setea el Titulo de la Pagina
            //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.Name() : Resources.CommonListManage.FunctionalPosition;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.FunctionalPosition;
                lblFunctionalArea.Text = Resources.CommonListManage.FunctionalArea;
                lblFunctionalPosition.Text = Resources.CommonListManage.FunctionalPosition;
                lblOrganization.Text = Resources.CommonListManage.Organization;
                lblPosition.Text = Resources.CommonListManage.Position;
            }
            private void ClearLocalSession()
            {
                _RdcFunctionalArea = null;
                _RtvFunctionalArea = null;
                _RdcFunctionalPosition = null;
                _RtvFunctionalPosition = null;
                _RdcPosition = null;
                Session["IdPosition"] = null;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                _RdcPosition.Enabled = true;
                _RdcFunctionalArea.Enabled = true;
                //_RdcOrganization.Enabled = true;
            }
            private void SetParentFunctionalPosition()
            {
                if (_Entity.ParentFunctionalPosition != null)
                {
                    //si es un root, no debe hacer nada de esto.
                    if ((_Entity.ParentFunctionalPosition.Position.IdPosition != 0) || (_Entity.ParentFunctionalPosition.FunctionalArea.IdFunctionalArea != 0))
                    {
                        //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                        String _keyValues = "IdFunctionalArea=" + _Entity.ParentFunctionalPosition.FunctionalArea.IdFunctionalArea.ToString() + "& IdPosition=" + _Entity.ParentFunctionalPosition.Position.IdPosition.ToString() + "& IdOrganization=" + _IdOrganization.ToString();
                        RadTreeView _rtvFunPos = _RtvFunctionalPosition;
                        RadComboBox _rcbFunPos = _RdcFunctionalPosition;
                        //Realiza el seteo del parent en el Combo-Tree.
                        SelectItemTreeViewParent(_keyValues, ref _rtvFunPos, ref _rcbFunPos, "FunctionalPosition", "FunctionalPositionsChildren");
                        _RdcFunctionalPosition = _rcbFunPos;
                        _RtvFunctionalPosition = _rtvFunPos;
                    }
                }
            }
            private void SetParentFunctionalArea()
            {
                //si es un root, no debe hacer nada de esto.
                if (_Entity.FunctionalArea.IdFunctionalArea != 0)
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdFunctionalArea=" + _Entity.FunctionalArea.IdFunctionalArea.ToString() + "& IdOrganization=" + _IdOrganization.ToString();
                    RadTreeView _rtvFunArea = _RtvFunctionalArea;
                    RadComboBox _rcbFunArea = _RdcFunctionalArea;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _rtvFunArea, ref _rcbFunArea, "FunctionalArea", "FunctionalAreasChildren");
                    _RdcFunctionalArea = _rcbFunArea;
                    _RtvFunctionalArea = _rtvFunArea;
                }
            }
            private void SetPosition()
            {
                _RdcPosition.SelectedValue = "IdPosition=" + _Entity.Position.IdPosition.ToString() + "& IdOrganization=" + _Entity.Position.IdOrganization.ToString();
            }
            private void LoadData()
            {
                base.PageTitle = Entity.Name();

                _RdcPosition.Enabled = false;
                _RdcFunctionalArea.Enabled = false;
                //_RdcOrganization.Enabled = false;

                //Seteamos la organizacion...
                //Realiza el seteo del parent en el Combo-Tree.
                //Organization _oganization = EMSLibrary.User.DirectoryServices.Map.Organization(Entity.Position.IdOrganization);
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

                SetParentFunctionalPosition();
                SetParentFunctionalArea();
                SetPosition();
            }
            private void AddCombos()
            {
                //AddComboOrganizations();
                AddComboFunctionalPositions();
                AddComboPositions();
                AddComboFunctionalAreas();
            }
            private void AddValidators()
            {
                //ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, phOrganizationValidator, ref _CvOrganization, _RdcOrganization, Resources.ConstantMessage.SelectAnOrganization);
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.FunctionalAreas, phFunctionalAreaValidator, ref _CvFunctionalArea, _RdcFunctionalArea, Resources.ConstantMessage.SelectAFunctionalArea);
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.Positions, phPositionValidator, ref _CvPosition, _RdcPosition, Resources.ConstantMessage.SelectAPosition);
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

            private void AddComboPositions()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);
                String _selectedValue = "IdPosition=" + _IdPosition.ToString() + "& IdOrganization=" + _IdOrganization.ToString();
                AddCombo(phPosition, ref _RdcPosition, Common.ConstantsEntitiesName.DS.Positions, _selectedValue, _params, false, true, false, true, false);
                _RdcPosition.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_RdcPosition_SelectedIndexChanged);
                //FwMasterPage.RegisterContentAsyncPostBackTrigger(_RdcPosition, "SelectedIndexChanged");
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
            private void AddComboFunctionalPositions()
            {
                String _filterExpression = String.Empty;
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);
                AddComboWithTree(phFunctionalPosition, ref _RdcFunctionalPosition, ref _RtvFunctionalPosition,
                    Common.ConstantsEntitiesName.DS.FunctionalPositions, _params, false, false, true, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboFunPos_NodeExpand),
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
            //    AddComboFunctionalPositions();
            //    AddComboFunctionalAreas();
            //    AddComboPositions();
            //}

            //void _RdcOrganization_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            //{
            //    ClearLocalSession();
            //    IdOrganization = Convert.ToInt64(GetKeyValue(e.Value, "IdOrganization"));
            //    AddComboFunctionalPositions();
            //    AddComboFunctionalAreas();
            //    AddComboPositions();
            //}
            void _RdcPosition_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                _IdPosition = Convert.ToInt64(GetKeyValue(e.Value, "IdPosition"));
                AddComboFunctionalPositions();
                AddComboFunctionalAreas();
                //AddComboPositions();
            }
            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            protected void rtvHierarchicalTreeViewInComboFunArea_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.FunctionalAreaChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }

            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            protected void rtvHierarchicalTreeViewInComboFunPos_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.FunctionalPositionChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);              
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //Obtiene el key necesario. para armar el Parent
                    Object _obj = GetKeyValue(_RtvFunctionalPosition.SelectedNode.Value, "IdFunctionalArea");   //Si lo saco del tree, funciona!!!.
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _parentValueFunctionalArea = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    _obj = GetKeyValue(_RtvFunctionalPosition.SelectedNode.Value, "IdPosition");   //Si lo saco del tree, funciona!!!.
                    Int64 _parentValuePosition = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.

                    //con esto obtiene los ID para armar el FunctionalPosition
                    Int64 _idPosition = _IdPosition; //Convert.ToInt64(GetKeyValue(_RdcPosition.SelectedValue, "IdPosition"));
                    Int64 _idFunctionalArea = Convert.ToInt64(GetKeyValue(_RtvFunctionalArea.SelectedNode.Value, "IdFunctionalArea"));

                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                    FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                    Position _position = _organization.Position(_idPosition);

                    FunctionalArea _funAreaParent = _organization.FunctionalArea(_parentValueFunctionalArea);
                    Position _positionParent = _organization.Position(_parentValuePosition);

                    FunctionalPosition _functionalPositionParent = _organization.FunctionalPosition(_positionParent, _funAreaParent);


                    if (Entity == null)
                    {
                        //Alta
                        _organization.FunctionalPositionsAdd(_position, _funArea, _functionalPositionParent);
                        Entity = _organization.FunctionalPosition(_position, _funArea);
                    }
                    else
                    {
                        //Modificacion
                        _organization.FunctionalPositionsModify(Entity, _functionalPositionParent);
                        Entity = _organization.FunctionalPosition(_position, _funArea);
                        //base.NavigatorAddTransferVar("IdOrganization", Entity.Position.IdOrganization);
                        //base.NavigatorAddTransferVar("IdFunctionalArea", Entity.FunctionalArea.IdFunctionalArea);
                        //base.NavigatorAddTransferVar("IdPosition", Entity.Position.IdPosition);
                        //base.NavigatorAddTransferVar("IdParentFunctionalArea", Entity.IdParentFunctionalArea);
                        //base.NavigatorAddTransferVar("IdParentPosition", Entity.IdParentPosition);
                        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.Name() + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    }

                    base.NavigatorAddTransferVar("IdOrganization", Entity.Position.IdOrganization);
                    base.NavigatorAddTransferVar("IdFunctionalArea", Entity.FunctionalArea.IdFunctionalArea);
                    base.NavigatorAddTransferVar("IdPosition", Entity.Position.IdPosition);
                    base.NavigatorAddTransferVar("IdParentFunctionalArea", Entity.IdParentFunctionalArea);
                    base.NavigatorAddTransferVar("IdParentPosition", Entity.IdParentPosition);

                    String _pkValues = "IdOrganization=" + Entity.Position.IdOrganization.ToString() +
                        "& IdFunctionalArea=" + Entity.FunctionalArea.IdFunctionalArea.ToString()
                        + "& IdPosition=" + Entity.Position.IdPosition.ToString() +
                        "& IdParentFunctionalArea =" + Entity.IdParentFunctionalArea.ToString()
                        + "& IdParentPosition=" + Entity.IdParentPosition.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.FunctionalPosition);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.FunctionalPosition + " " + Entity.Name(), Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.Name());
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.FunctionalPosition, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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