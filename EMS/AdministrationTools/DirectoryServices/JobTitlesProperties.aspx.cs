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
using Condesus.EMS.WebUI.Controls;
using System.Linq;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class JobTitlesProperties : BaseProperties
    {
        #region Internal Properties
            CompareValidator _CvOrganizationalChart;
            //CompareValidator _CvOrganization;
            CompareValidator _CvFunctionalPosition;
            CompareValidator _CvGeographicFunctionalArea;
            private RadComboBox _RdcOrganizationalChart;
            private JobtitleWithChart _Entity = null;
            //RadComboBox _RdcOrganization;
            //private RadTreeView _RtvOrganization;
            private Int64 _IdOrganization
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                }
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
            }
            private Int64 _IdPosition
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdPosition") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdPosition")) : 0;
                }
            }
            private Int64 _IdOrganizationalChart
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganizationalChart") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganizationalChart")) : 0;
                }
            }
            private JobtitleWithChart Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                        {
                            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_IdGeographicArea);
                            Position _position = _organization.Position(_IdPosition);
                            FunctionalArea _funArea = _organization.FunctionalArea(_IdFunctionalArea);
                            FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                            GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);

                            _Entity = (JobtitleWithChart)_organization.OrganizationalChart(_IdOrganizationalChart).JobTitle(_geoFunArea, _funPos);
                        }
                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }

            private RadComboBox _RdcGeographicFunctionalArea;
            private RadTreeView _RtvGeographicFunctionalArea;

            private RadComboBox _RdcFunctionalPosition;
            private RadTreeView _RtvFunctionalPosition;

            private RadComboBox _RdcJobTitle;
            private RadTreeView _RtvJobTitle;
  
            private String _FilterExpressionFunArea;

        //private Int64 _IdOrganization
        //{
        //    get { return Convert.ToInt64(ViewState["_IdOrganization"]); }
        //    set { ViewState["_IdOrganization"] = value.ToString(); }
        //}
        //private String _ActiveTab
        //{
        //    get
        //    {
        //        object o = ViewState["ActiveTab"];
        //        if (o != null)
        //            return (String)o;
        //        return "";
        //    }

        //    set { ViewState["ActiveTab"] = value; }
        //}
        //private Int64 _IdFunctionalArea
        //{
        //    get { return Convert.ToInt64(ViewState["IdFunctionalArea"]); }
        //    set { ViewState["IdFunctionalArea"] = value.ToString(); }
        //}
        //private Int64 _IdGeographicArea
        //{
        //    get { return Convert.ToInt64(ViewState["IdGeographicArea"]); }
        //    set { ViewState["IdGeographicArea"] = value.ToString(); }
        //}
        //private Int64 _IdPosition
        //{
        //    get { return Convert.ToInt64(ViewState["IdPosition"]); }
        //    set { ViewState["IdPosition"] = value.ToString(); }
        //}
        //private String _EntityName;
        //private String _IdJobTitle
        //{
        //    get { return ViewState["IdJobTitle"].ToString(); }
        //    set { ViewState["IdJobTitle"] = value.ToString(); }
        //}
        //private String _ChildControl
        //{
        //    get
        //    {
        //        object _o = ViewState["ChildControl"];

        //        if (_o != null)
        //            return (String)_o;

        //        return String.Empty;
        //    }
        //    set { ViewState["ChildControl"] = value; }
        //}
        //private String _ChildControlEntityId
        //{
        //    get
        //    {
        //        object _o = ViewState["ChildControlEntityId"];

        //        if (_o != null)
        //            return (String)_o;

        //        return "0";
        //    }
        //    set { ViewState["ChildControlEntityId"] = value; }
        //}
        //private Boolean? _ChildControlMode
        //{
        //    get
        //    {
        //        object _o = ViewState["ChildControlMode"];

        //        if (_o != null)
        //            return (Boolean)_o;

        //        return null;
        //    }
        //    set { ViewState["ChildControlMode"] = value; }
        //}
        //private RadTreeView _RtvGeographicFunctionalArea;
        //private RadTreeView _RtvFunctionalPosition;

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
                try
                {
                    base.PageTitle = (Entity!= null) ? Entity.Name() : Resources.CommonListManage.JobTitle;
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.JobTitle;
                lblFunctionalPosition.Text = Resources.CommonListManage.FunctionalPosition;
                lblGeographicFunctionalArea.Text = Resources.CommonListManage.GeographicFunctionalArea;
                lblJobTitle.Text = Resources.CommonListManage.JobTitle;
                lblJobTitleParent.Text = Resources.CommonListManage.Parent;
                lblOrganization.Text = Resources.CommonListManage.Organization;
                lblOrganizationalChart.Text = Resources.CommonListManage.OrganizationalChart;
            }
            private void ClearLocalSession()
            {
                _RdcOrganizationalChart = null;
                _RdcJobTitle = null;
                _RtvJobTitle = null;
                _RdcFunctionalPosition = null;
                _RtvFunctionalPosition = null;
                _RdcGeographicFunctionalArea = null;
                _RtvGeographicFunctionalArea = null;
                
            }
            private void AddCombos()
            {
                //AddComboOrganizations();
                AddComboOrganizationalCharts();
                AddComboJobTitles(); 
                AddComboFunctionalPositions();
                AddComboGeographicFunctionalAreas(_RtvFunctionalPosition.SelectedValue);
            }
            private void AddValidators()
            {
                //AddValidatorOrganizations();
                AddValidatorFunctionalPositions();
                AddValidatorGeographicFunctionalAreas();
            }
            private void AddValidatorFunctionalPositions()
            {
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.FunctionalPositions, phFunctionalPositionValidator, ref _CvFunctionalPosition, _RdcFunctionalPosition, Resources.ConstantMessage.SelectAFunctionalPosition);
            }
            private void AddValidatorGeographicFunctionalAreas()
            {
                //Inyecta Validator para Geographic Functional Area
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.GeographicFunctionalAreas, phGeographicFunctionalAreaValidator, ref _CvGeographicFunctionalArea, _RdcGeographicFunctionalArea, Resources.ConstantMessage.SelectAGeographicFunctionalArea);
            }
            private void AddComboOrganizationalCharts()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);

                String _selectedValue = String.Empty;   // = "IdOrganizationalChart=" + IdOrganization.ToString();
                AddCombo(phOrganizationalChart, ref _RdcOrganizationalChart, Common.ConstantsEntitiesName.DS.OrganizationalCharts, _selectedValue, _params, false, true, false, true, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationalCharts, phOrganizationalChartValidator, ref _CvOrganizationalChart, _RdcOrganizationalChart, Resources.ConstantMessage.SelectAnOrganizationalChart);
                _RdcOrganizationalChart.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(RdcOrganizationalChart_SelectedIndexChanged);
            }
            private void AddComboJobTitles()
            {
                String _filterExpression = String.Empty;
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                
                //if ((GetKeyValue(_RdcOrganizationalChart.SelectedValue, "IdOrganizationalChart") != null)&&(GetKeyValue(_RdcOrganization.SelectedValue, "IdOrganization") != null))
                if (GetKeyValue(_RdcOrganizationalChart.SelectedValue, "IdOrganizationalChart") != null)
                {
                    //_params.Add("IdOrganization", GetKeyValue(_RdcOrganization.SelectedValue, "IdOrganization"));
                    _params.Add("IdOrganization", _IdOrganization);
                    _params.Add("IdOrganizationalChart", GetKeyValue(_RdcOrganizationalChart.SelectedValue, "IdOrganizationalChart"));
                }
                AddComboWithTree(phJobTitle, ref _RdcJobTitle, ref _RtvJobTitle,
                    Common.ConstantsEntitiesName.DS.JobTitles, _params, false, false, true, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboJobTitle_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);
            }
            private void AddComboFunctionalPositions()
            {
                String _filterExpression = String.Empty;
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);
                AddComboWithTree(phFunctionalPosition, ref _RdcFunctionalPosition, ref _RtvFunctionalPosition,
                    Common.ConstantsEntitiesName.DS.FunctionalPositions, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboFunPos_NodeExpand),
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboFunPos_NodeClick),
                    Common.Constants.ComboBoxSelectItemDefaultPrefix, false);

                //FwMasterPage.RegisterContentAsyncPostBackTrigger(_RtvFunctionalPosition, "NodeClick");
            }
            private void AddComboGeographicFunctionalAreas(String functionalPosition)
            {
                String _filterExpression = String.Empty;
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if ((GetKeyValue(functionalPosition, "IdFunctionalArea") != null) && (GetKeyValue(functionalPosition, "IdOrganization") != null))
                {
                    _filterExpression = "IdFunctionalArea = " + GetKeyValue(functionalPosition, "IdFunctionalArea").ToString();
                    _params.Add("IdOrganization", _IdOrganization);
                    //_params.Add("IdOrganization", GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));
                }

                AddComboWithTree(phGeographicFunctionalArea, ref _RdcGeographicFunctionalArea, ref _RtvGeographicFunctionalArea,
                    Common.ConstantsEntitiesName.DS.GeographicFunctionalAreas, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInComboGeoFunArea_NodeExpand),
                    Common.Constants.ComboBoxSelectItemDefaultPrefix, false);
            }

            private void SetOrganizationalChart()
            {
                _RdcOrganizationalChart.SelectedValue = "IdOrganizationalChart=" + Entity.OrganizationalChart.IdOrganizationalChart.ToString() + "& IdOrganization=" + _Entity.Organization.IdOrganization.ToString();
            }
            private void SetParentFunctionalPosition()
            {
                //si es un root, no debe hacer nada de esto.
                if ((Entity.FunctionalPositions.Position.IdPosition != 0) || (Entity.FunctionalPositions.FunctionalArea.IdFunctionalArea != 0))
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdFunctionalArea=" + Entity.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea.ToString() + "& IdPosition=" + Entity.FunctionalPositions.Position.IdPosition.ToString() + "& IdOrganization=" + _IdOrganization.ToString();
                    RadTreeView _rtvFunPos = _RtvFunctionalPosition;
                    RadComboBox _rcbFunPos = _RdcFunctionalPosition;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _rtvFunPos, ref _rcbFunPos, "FunctionalPosition", "FunctionalPositionsChildren");
                    _RdcFunctionalPosition = _rcbFunPos;
                    _RtvFunctionalPosition = _rtvFunPos;

                    _FilterExpressionFunArea = _keyValues;
                }
            }
            private void SetParentGeographicFunctionalArea()
            {
                //si es un root, no debe hacer nada de esto.
                if ((Entity.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea != 0) || (Entity.GeographicFunctionalAreas.GeographicArea.IdGeographicArea != 0))
                {
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdGeographicArea=" + Entity.GeographicFunctionalAreas.GeographicArea.IdGeographicArea.ToString() + "& IdFunctionalArea=" + Entity.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea.ToString() + "& IdOrganization=" + _IdOrganization.ToString();
                    RadTreeView _rtvGeoFunArea = _RtvGeographicFunctionalArea;
                    RadComboBox _rcbGeoFunArea = _RdcGeographicFunctionalArea;
                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _rtvGeoFunArea, ref _rcbGeoFunArea, "GeographicFunctionalArea", "GeographicFunctionalAreasChildren");
                    _RdcGeographicFunctionalArea = _rcbGeoFunArea;
                    _RtvGeographicFunctionalArea = _rtvGeoFunArea;
                }
            }
            
            private void LoadData()
            {
                base.PageTitle = Entity.Name();

                lblJobTitleValue.Text = Entity.Name();

                //_RdcOrganization.Enabled = false;
                _RdcFunctionalPosition.Enabled = false;
                _RdcGeographicFunctionalArea.Enabled = false;

                AddComboOrganizationalCharts();
                SetOrganizationalChart();

                AddComboJobTitles();
                //si es un root, no debe hacer nada de esto.
                //if (_Entity.Parent.GeographicFunctionalAreas.GeographicArea.IdGeographicArea != 0)
                if (Entity.Parent != null)
                {
                    Int64 _idParentGeographicArea = 0;
                    Int64 _idParentPosition = 0;
                    Int64 _idParentFunctionalArea = 0;
                    if (Entity.Parent.Parent != null)
                    {
                        _idParentGeographicArea = _Entity.Parent.Parent.GeographicFunctionalAreas.GeographicArea.IdGeographicArea;
                        _idParentPosition = _Entity.Parent.Parent.FunctionalPositions.Position.IdPosition;
                        _idParentFunctionalArea = _Entity.Parent.Parent.FunctionalPositions.FunctionalArea.IdFunctionalArea;
                    }
                    //El orden de este KeyValue, debe ser el mismo al que se hace al momento de crear el tree, y el mismo del metodo publico del factory!!!
                    String _keyValues = "IdGeographicArea=" + _Entity.Parent.GeographicFunctionalAreas.GeographicArea.IdGeographicArea.ToString()
                            + "& IdPosition=" + _Entity.Parent.FunctionalPositions.Position.IdPosition.ToString()
                            + "& IdFunctionalArea=" + _Entity.Parent.FunctionalPositions.FunctionalArea.IdFunctionalArea.ToString()
                            + "& IdOrganizationalChart=" + _Entity.Parent.OrganizationalChart.IdOrganizationalChart.ToString()
                            + "& IdOrganization=" + _Entity.Parent.Organization.IdOrganization.ToString()
                            + "& IdParentGeographicArea=" + _idParentGeographicArea.ToString()
                            + "& IdParentPosition=" + _idParentPosition.ToString()
                            + "& IdParentFunctionalArea=" + _idParentFunctionalArea.ToString();

                    //Realiza el seteo del parent en el Combo-Tree.
                    SelectItemTreeViewParent(_keyValues, ref _RtvJobTitle, ref _RdcJobTitle, Common.ConstantsEntitiesName.DS.JobTitle, Common.ConstantsEntitiesName.DS.JobTitleChildren);
                }

                AddComboFunctionalPositions();
                SetParentFunctionalPosition();
                AddComboGeographicFunctionalAreas(_RtvFunctionalPosition.SelectedValue);
                SetParentGeographicFunctionalArea();

                //_RdcOrganization.Enabled = false; 
                _RdcFunctionalPosition.Enabled = false;
                _RdcGeographicFunctionalArea.Enabled = false;
                _RdcOrganizationalChart.Enabled = false;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                lblJobTitleValue.Text = String.Empty;
                //_RdcOrganization.Enabled = true;
                _RdcFunctionalPosition.Enabled = true;
                _RdcGeographicFunctionalArea.Enabled = true;
                _RdcOrganizationalChart.Enabled = true;
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
            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            protected void rtvHierarchicalTreeViewInComboJobTitle_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.JobTitleChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }
            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            protected void rtvHierarchicalTreeViewInComboFunPos_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.FunctionalPositionChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }
            //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
            //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
            protected void rtvHierarchicalTreeViewInComboGeoFunArea_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.GeographicFunctionalAreaChildren, _FilterExpressionFunArea, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
            }

            //protected void rtvOrganizations_NodeClick(object sender, RadTreeNodeEventArgs e)
            //{

            //    ClearLocalSession();
            //    IdOrganization = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdOrganization"));
            //    AddComboOrganizationalCharts();
            //    AddComboFunctionalPositions();
            //    AddComboGeographicFunctionalAreas(_RtvFunctionalPosition.SelectedValue);

            //    _RdcOrganization.Text = e.Node.Text;
            //}
            protected void RdcOrganizationalChart_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                AddComboJobTitles();
            }
            //este evento se utiliza para poder filtrar al momento de abrir el proximo combo.
            protected void rtvHierarchicalTreeViewInComboFunPos_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                //String _id = Convert.ToString(GetKeyValue(e.Node.Value, "IdFunctionalArea"));
                //if (!String.IsNullOrEmpty(_id))
                //    { _FilterExpressionFunArea = "IdFunctionalArea = " + _id; }
                //else
                //    { _FilterExpressionFunArea = String.Empty; }
                
                _RdcGeographicFunctionalArea = null;
                _RtvGeographicFunctionalArea = null;
                AddComboGeographicFunctionalAreas(e.Node.Value);
            }
            
            //protected void rtvHierarchicalTreeViewInComboJobTitle_NodeClick(object sender, RadTreeNodeEventArgs e)
            //{
            //    AddComboJobTitles();
            //}

            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    //con esto obtiene los ID para armar el JobTitle
                    Int64 _idGeographicArea = Convert.ToInt64(GetKeyValue(_RtvGeographicFunctionalArea.SelectedNode.Value, "IdGeographicArea"));
                    Int64 _idFunctionalArea = Convert.ToInt64(GetKeyValue(_RtvGeographicFunctionalArea.SelectedNode.Value, "IdFunctionalArea"));
                    //Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_RtvGeographicFunctionalArea.SelectedNode.Value, "IdOrganization"));
                    Int64 _idPosition = Convert.ToInt64(GetKeyValue(_RtvFunctionalPosition.SelectedNode.Value, "IdPosition"));

                    Int64 _idOrganizationalChart = Convert.ToInt64(GetKeyValue(_RdcOrganizationalChart.SelectedValue, "IdOrganizationalChart"));

                    //Obtiene el key necesario.
                    Object _obj = GetKeyValue(_RtvJobTitle.SelectedNode.Value, "IdGeographicArea");   //Si lo saco del tree, funciona!!!.
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _idParentGeographicArea = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    _obj = GetKeyValue(_RtvJobTitle.SelectedNode.Value, "IdFunctionalArea");   //Si lo saco del tree, funciona!!!.
                    //Si el key obtenido no llega a exister devuelve null.    
                    Int64 _idParentFunctionalArea = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    _obj = GetKeyValue(_RtvJobTitle.SelectedNode.Value, "IdPosition");   //Si lo saco del tree, funciona!!!.
                    //Si el key obtenido no llega a exister devuelve null.    
                    Int64 _idParentPosition = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.

                    if (Entity == null)
                    {
                        //Alta
                        Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                        Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                        Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_idPosition);
                        Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                        FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);

                        _organization.OrganizationalChart(_idOrganizationalChart).JobTitlesAdd(_idGeographicArea, _idPosition, _idFunctionalArea, _idParentGeographicArea, _idParentPosition, _idParentFunctionalArea);
                        Entity = (JobtitleWithChart)_organization.OrganizationalChart(_idOrganizationalChart).JobTitle(_geoFunArea, _funPos);
                    }
                    else
                    {
                        //Modificacion
                        _Entity.Modify(_idGeographicArea, _idPosition, _idFunctionalArea, _idParentGeographicArea, _idParentPosition, _idParentFunctionalArea);

                        //base.NavigatorAddTransferVar("IdOrganization", Entity.FunctionalPositions.FunctionalArea.IdOrganization);
                        //base.NavigatorAddTransferVar("IdFunctionalArea", Entity.FunctionalPositions.FunctionalArea.IdFunctionalArea);
                        //base.NavigatorAddTransferVar("IdGeographicArea", Entity.GeographicFunctionalAreas.GeographicArea.IdGeographicArea);
                        //base.NavigatorAddTransferVar("IdPosition", Entity.FunctionalPositions.Position.IdPosition);
                        //base.NavigatorAddTransferVar("IdOrganizationalChart", _idOrganizationalChart);
                        //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.Name() + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    }

                    base.NavigatorAddTransferVar("IdOrganization", Entity.FunctionalPositions.FunctionalArea.IdOrganization);
                    base.NavigatorAddTransferVar("IdFunctionalArea", Entity.FunctionalPositions.FunctionalArea.IdFunctionalArea);
                    base.NavigatorAddTransferVar("IdGeographicArea", Entity.GeographicFunctionalAreas.GeographicArea.IdGeographicArea);
                    base.NavigatorAddTransferVar("IdPosition", Entity.FunctionalPositions.Position.IdPosition);
                    base.NavigatorAddTransferVar("IdOrganizationalChart", _idOrganizationalChart);
                    String _pkValues = "IdOrganization=" + Entity.FunctionalPositions.FunctionalArea.IdOrganization.ToString()
                            + "& IdFunctionalArea=" + Entity.FunctionalPositions.FunctionalArea.IdFunctionalArea.ToString()
                            + "& IdGeographicArea=" + Entity.GeographicFunctionalAreas.GeographicArea.IdGeographicArea.ToString()
                            + "& IdPosition=" + Entity.FunctionalPositions.Position.IdPosition.ToString()
                            + "& IdOrganizationalChart=" + _idOrganizationalChart;
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.JobTitle);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.JobTitle);
                    
                    //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.JobTitle + " " + Entity.Name(), Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                    String _entityPropertyName = String.Concat(Entity.Name());
                    NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.JobTitle, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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