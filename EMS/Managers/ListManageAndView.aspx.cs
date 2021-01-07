using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using Condesus.WebUI.Navigation;
using Condesus.EMS.Business.KC.Entities;

namespace Condesus.EMS.WebUI.Managers
{
    public partial class ListManageAndView : BaseProperties
    {
        #region Internal Properties
            private RadGrid _RgdMasterGridListManage;
            private RadTreeView _rtvComboHierarchicalFilter;
            private RadComboBox _rdcComboFilter;

            private String _EntityName = String.Empty;
            private String _EntityNameGRC = String.Empty;
            private String _EntityNameContextElement = String.Empty;
            private String _EntityNameMapClassification = String.Empty;
            private String _EntityNameMapClassificationChildren = String.Empty;
            private String _EntityNameMapElement = String.Empty;
            private String _EntityNameMapElementChildren = String.Empty;
            private Dictionary<Int64, CatalogDoc> _CatalogDoc
            {
                get
                {
                    return GetPicturesByObject(_EntityName + "Pictures", ManageEntityParams);
                }
            }
            private RadTabStrip _RtsTabStrip = new RadTabStrip();
            private RadMultiPage _RmpMultiPage = new RadMultiPage();
            private Dictionary<String, RadGrid> _RgdMasterGridListManageRelated = new Dictionary<String, RadGrid>();
            private String _EntityNameGridRelatedOn = String.Empty;
            private String _EntityNameRelatedOn = String.Empty;
            private RadGrid _rgdMasterGridListViewerMainData;
            private String _xmlFilterHierarchy
            {
                get
                {
                    object _o = Session["xmlFilterHierarchy"];
                    if (_o != null)
                        return (String)Session["xmlFilterHierarchy"];

                    return String.Empty;
                }

                set
                {
                    Session["xmlFilterHierarchy"] = value;
                }
            }
            private String _xmlComboFilterSimple
            {
                get
                {
                    object _o = Session["xmlComboFilterSimple"];
                    if (_o != null)
                        return (String)Session["xmlComboFilterSimple"];

                    return String.Empty;
                }

                set
                {
                    Session["xmlComboFilterSimple"] = value;
                }
            }
            private String _comboFilterSimpleSelectedValue
            {
                get
                {
                    object _o = Session["comboFilterSimpleSelectedValue"];
                    if (_o != null)
                        return (String)Session["comboFilterSimpleSelectedValue"];

                    return String.Empty;
                }

                set
                {
                    Session["comboFilterSimpleSelectedValue"] = value;
                }
            }


            #region Only for Execution Task
                private RadComboBox _RdcAccountingActivity;
                private RadTreeView _RtvAccountingActivity;
                private RadComboBox _RdcMethodology;
                private RadComboBox _RdcFacilityType;
                private RadComboBox _RdcSite;
                private RadTreeView _RtvSite;
                private String _xmlFilterActivity
                {
                    get
                    {
                        object _o = Session["xmlFilterActivity"];
                        if (_o != null)
                            return (String)Session["xmlFilterActivity"];

                        return String.Empty;
                    }

                    set
                    {
                        Session["xmlFilterActivity"] = value;
                    }
                }
                private String _xmlFilterMethodology
                {
                    get
                    {
                        object _o = Session["xmlFilterMethodology"];
                        if (_o != null)
                            return (String)Session["xmlFilterMethodology"];

                        return String.Empty;
                    }

                    set
                    {
                        Session["xmlFilterMethodology"] = value;
                    }
                }
                private String _xmlFilterFacilityType
                {
                    get
                    {
                        object _o = Session["xmlFilterFacilityType"];
                        if (_o != null)
                            return (String)Session["xmlFilterFacilityType"];

                        return String.Empty;
                    }

                    set
                    {
                        Session["xmlFilterFacilityType"] = value;
                    }
                }
                private String _xmlFilterFacility
                {
                    get
                    {
                        object _o = Session["xmlFilterFacility"];
                        if (_o != null)
                            return (String)Session["xmlFilterFacility"];

                        return String.Empty;
                    }

                    set
                    {
                        Session["xmlFilterFacility"] = value;
                    }
                }
            #endregion
        #endregion

        #region PageLoad & Init
            protected override void OnPreInit(EventArgs e)
            {
                base.OnPreInit(e);

                //Registro Mis Custom MenuPanels
                RegisterCustomMenuPanels();
                IsGridPageIndexChanged = false;
                IsReturnFromDelete = false;
            }
            protected override void OnLoadComplete(EventArgs e)
            {
                base.OnLoadComplete(e);
                
                //Si es de Lenguajes, no arma el view...no tiene sentido para eso.
                if (CheckBuildViewer()) // (!EntityNameGrid.Contains("_LG"))
                {
                    //Esto lo vuelvo a poner aca, porque cuando se navega desde el breadcrum, carga los estados de la pagina...
                    //Si viene de un delete, entonces no tiene que hacer esto!
                    if ((_RgdMasterGridListManage.SelectedItems.Count > 0) && (!IsReturnFromDelete))
                    {
                        //Init Image Viewer
                        LoadImages();
                        //Esto lo hago porque luego de un change page...no se tiene que volver a cargar, sino siempre queda en pagina 1.
                        if (!IsGridPageIndexChanged)
                        {
                            ShowListViewerRelated();
                        }
                    }
                }
            }
            private Boolean CheckBuildViewer()
            {
                //Si son LG no tiene view
                if (!EntityNameGrid.Contains("_LG"))
                {
                    return false;
                }
                //lo mismo para estas entidades (las tareas)
                switch (EntityNameGrid)
                {
                    case Common.ConstantsEntitiesName.DB.PlannedTasks:
                    case Common.ConstantsEntitiesName.DB.OpenedExceptions:
                    case Common.ConstantsEntitiesName.DB.WorkingExceptions:
                    case Common.ConstantsEntitiesName.DB.ActiveTasks:
                    case Common.ConstantsEntitiesName.DB.OverDueTasks:
                    case Common.ConstantsEntitiesName.DB.AllMyExecutions:
                    case Common.ConstantsEntitiesName.DB.ProcessTaskExecutionExecuted:
                        return false;
                        break;
                }
                //Cualquier otra cosa, si tiene!
                return true;
            }

            private void LoadParameters()
            {
                ManageEntityParams = new Dictionary<String, Object>();
                //Debe recorrer las PK para saber si es un Manage de Lenguajes.
                String _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost");
                //Ahora genera el Dictionary con los parametros y se lo pasa a la ManageEntityParams para que contruya la grilla como corresponde.
                ManageEntityParams = GetKeyValues(_pkValues);

                //Setea el nombre de la entidad que se va a mostrar 
                //y el resto de los parametros recibidos
                EntityNameGrid = base.NavigatorGetTransferVar<String>("EntityNameGrid");
                _EntityName = base.NavigatorGetTransferVar<String>("EntityName");
                _EntityNameGRC = base.NavigatorGetTransferVar<String>("EntityNameContextInfo");
                if (base.NavigatorContainsTransferVar("EntityNameContextElement"))
                {
                    _EntityNameContextElement = base.NavigatorGetTransferVar<String>("EntityNameContextElement");
                }
                //EntityNameToRemove = base.NavigatorGetTransferVar<String>("EntityNameToRemove");
                //Para el caso del Remove, debe ser el nombre de la entidad mas la palabra Remove!!!
                EntityNameToRemove = _EntityName + "Remove";

                IsFilterHierarchy = base.NavigatorGetTransferVar<Object>("IsFilterHierarchy") != null ? base.NavigatorGetTransferVar<Boolean>("IsFilterHierarchy") : false;
                EntityNameComboFilter = base.NavigatorGetTransferVar<String>("EntityNameComboFilter");
                EntityNameChildrenComboFilter = base.NavigatorGetTransferVar<String>("EntityNameChildrenComboFilter");
                _EntityNameMapClassification = base.NavigatorGetTransferVar<String>("EntityNameMapClassification");
                _EntityNameMapClassificationChildren = base.NavigatorGetTransferVar<String>("EntityNameMapClassificationChildren");
                _EntityNameMapElement = base.NavigatorGetTransferVar<String>("EntityNameMapElement");
                _EntityNameMapElementChildren = base.NavigatorGetTransferVar<String>("EntityNameMapElementChildren");
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                //Carga todos los paramtros que recibe la pagina...
                LoadParameters();

                ////Setea los datos en el DataTable de base, para que luego se carge la grilla.
                //BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                ////Arma la grilla completa
                //LoadListManage();
                ////Carga el GRC en caso de que lo hayan pasado por parametros.
                //LoadGRCByEntity();
                ////Carga el menu de opciones generales.
                //LoadGeneralOptionMenu();
                ////Inyecta los javascript que necesita la pagina
                //InyectJavaScript();
                ////Inserta todos los manejadores de eventos que necesita la apgina
                //InitializeHandlers();
                ////Arma el Menu de Seleccion.
                //LoadMenuSelection();

                //LoadMenuSearch();

                ////Construye los Tabs para la informacion Relacionada de la entidad.
                //LoadTabsForRelatedData();
            }
            private void RebuildGenericDatatable()
            {
                ////Si tenemos el filtro seleccionado, lo agregamos a los parametros.
                //if ((_rdcComboFilter != null) && (_rdcComboFilter.SelectedValue != Common.Constants.ComboBoxSelectItemValue))
                //{
                //    RebindFilter();
                //    //ManageEntityParams = GetKeyValues(_rdcComboFilter.SelectedValue);
                //}

                //Ahora lo pongo en el onload, ya que si es lo mismo que esta en el mapa entran en conflicto...
                switch (_EntityName)
                {
                    case Common.ConstantsEntitiesName.PF.ProcessClassification:
                    case Common.ConstantsEntitiesName.PF.Process:
                    case Common.ConstantsEntitiesName.DS.OrganizationClassification:
                    case Common.ConstantsEntitiesName.DS.Organization:
                    case Common.ConstantsEntitiesName.PA.IndicatorClassification:
                    case Common.ConstantsEntitiesName.PA.Indicator:
                    case Common.ConstantsEntitiesName.IA.ProjectClassification:
                    case Common.ConstantsEntitiesName.KC.ResourceClassification:
                    case Common.ConstantsEntitiesName.KC.Resource:
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                        break;
                }
                
                ////Si no se ejecuto nada del switch...pregunto si viene de otro lado...
                //if (IsGridPageIndexChanged)
                //{ 
                    //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                    //BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                //}

            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                //Esto es para limpiar la variable de session para las properties
                Session["IdOrganization_local"] = null;

                //Para el caso de que sea una entidad que tambien se esta mostrando en el mapa, se recarga el datatable, para que no entren en conflicto
                //RebuildGenericDatatable();


                //lblActivity.Text = Resources.CommonListManage.AccountingActivity;
                //lblFacility.Text = Resources.CommonListManage.Facility;
                //lblFacilityType.Text = Resources.CommonListManage.FacilityType;
                //lblMethodology.Text = Resources.CommonListManage.Methodology;
                lblFilterForExecutionTask.Text = Resources.Common.Filter;

                btnClearFilter.Text = Resources.Common.ClearFilter;
                btnFilter.Text = Resources.Common.Filter;
                
                

                LoadFilterForExecutionTask();

                //Si no hay entidad para el filtro, no lo inyecta.
                if (!String.IsNullOrEmpty(EntityNameComboFilter) || !String.IsNullOrEmpty(_EntityNameMapClassification))
                {
                    LoadFilter();
                }
                else
                {
                    if (_EntityName == Common.ConstantsEntitiesName.DB.PlannedTasks)
                    {
                        RadDatePicker _rdp = new RadDatePicker();
                        _rdp.AutoPostBack = true;
                        _rdp.SelectedDateChanged += new Telerik.Web.UI.Calendar.SelectedDateChangedEventHandler(_rdp_SelectedDateChanged);

                        pnlFilter.Controls.Add(BuildFilterTable(_rdp));

                        PersistControlState(_rdp);
                    }
                }

                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                ////Si no es un listado de  tareas vencidas..., lo hace de manera estandard y generica
                //if (_EntityName != Common.ConstantsEntitiesName.DB.OverDueTasks)
                //{
                    //Arma la grilla completa
                    LoadListManage();
                //}
                //else
                //{
                //    //Como es un listado de tareas overdue...
                //    LoadListManageGrouping();
                //}
                //Carga el GRC en caso de que lo hayan pasado por parametros.
                LoadGRCByEntity();
                //Carga el menu de opciones generales.
                LoadGeneralOptionMenu();
                //Inyecta los javascript que necesita la pagina
                InyectJavaScript();
                //Inserta todos los manejadores de eventos que necesita la apgina
                InitializeHandlers();
                //Arma el Menu de Seleccion.
                LoadMenuSelection();

                LoadMenuSearch();

                //Construye los Tabs para la informacion Relacionada de la entidad.
                LoadTabsForRelatedData();

                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

                if (Page.IsPostBack)
                {
                    //Si es de Lenguajes, no arma el view...no tiene sentido para eso.
                    if (CheckBuildViewer())     //(!EntityNameGrid.Contains("_LG"))
                    {
                        //Esto lo vuelvo a poner aca, porque cuando se navega desde el breadcrum, carga los estados de la pagina...
                        if (_RgdMasterGridListManage.SelectedItems.Count > 0)
                        {
                            ShowListViewerRelated();
                        }
                    }
                }
                else
                {

                    //Esto es para cuando se recarga la pagina luego de un delete...entonces guarda el estado del combo de filtro...
                    if ((_rtvComboHierarchicalFilter != null) && (!String.IsNullOrEmpty(_xmlFilterHierarchy)))  // (base.NavigatorContainsTransferVar("xmlFilterHierarchy"))
                    {
                        _rtvComboHierarchicalFilter.LoadXml(_xmlFilterHierarchy);
                        RebindFilter();
                        if (_rtvComboHierarchicalFilter.SelectedNode != null)
                        {
                            InjectOnLoadSetComboFilterText(EntityNameComboFilter, _rtvComboHierarchicalFilter.SelectedNode.Text);
                        }
                    }
                    else
                    {
                        if ((_rdcComboFilter != null) && (!String.IsNullOrEmpty(EntityNameComboFilter)) && (!String.IsNullOrEmpty(_xmlComboFilterSimple)))
                        {
                            //InjectOnLoadSetComboFilterText(_rdcComboFilter.ClientID, _rdcComboFilter.Text);
                            _rdcComboFilter.LoadXml(_xmlComboFilterSimple);
                            RebindFilter();
                        }
                    }

                    String _textActivity = String.Empty;
                    String _textMethodology = String.Empty;
                    String _textFacilityType = String.Empty;
                    String _textFacility = String.Empty;

                    //Esto es para cuando se recarga la pagina solo para los filtros de las tareas
                    if ((_RtvAccountingActivity != null) && (_RtvAccountingActivity.SelectedNode != null) && (!String.IsNullOrEmpty(_xmlFilterActivity)))
                    {
                        _RtvAccountingActivity.LoadXml(_xmlFilterActivity);
                        _textActivity = _RtvAccountingActivity.SelectedNode.Text;
                    }
                    if ((_RdcMethodology != null) && (!String.IsNullOrEmpty(_xmlFilterMethodology)))
                    {
                        _RdcMethodology.LoadXml(_xmlFilterMethodology);
                        _textMethodology = _RdcMethodology.SelectedItem.Text;
                    }
                    if ((_RdcFacilityType != null) && (!String.IsNullOrEmpty(_xmlFilterFacilityType)))
                    {
                        _RdcFacilityType.LoadXml(_xmlFilterFacilityType);
                        _textFacilityType = _RdcFacilityType.SelectedItem.Text;
                    }
                    if ((_RtvSite != null) && (_RtvSite.SelectedNode != null) && (!String.IsNullOrEmpty(_xmlFilterFacility)))
                    {
                        _RtvSite.LoadXml(_xmlFilterFacility);
                        _textFacility = _RtvSite.SelectedNode.Text;
                    }

                    if ((!String.IsNullOrEmpty(_xmlFilterActivity)) || (!String.IsNullOrEmpty(_xmlFilterMethodology)) || (!String.IsNullOrEmpty(_xmlFilterFacilityType)) || (!String.IsNullOrEmpty(_xmlFilterFacility)))
                    {
                        ApplyFilterForTaskExecution();
                        InjectOnLoadSetComboFilterTextForTaskExecution(_RdcAccountingActivity.ClientID, _textActivity, _RdcMethodology.ClientID, _textMethodology, _RdcFacilityType.ClientID, _textFacilityType, _RdcSite.ClientID, _textFacility);
                    }

                }

                //Si tenemos el filtro seleccionado, lo agregamos a los parametros.
                if ((_rdcComboFilter != null) && (_rdcComboFilter.SelectedValue != Common.Constants.ComboBoxSelectItemValue))
                {
                    //RebindFilter();
                    SetParameterOfFilter();
                }
            }
            protected override void SetPagetitle()
            {
                try
                {
                    //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                    String _pageTitle = base.NavigatorGetTransferVar<String>("PageTitle");
                    if (String.IsNullOrEmpty(_pageTitle))
                    {
                        base.PageTitle = GetGlobalResourceObject("CommonListManage", _EntityName).ToString();
                    }
                    else
                    {
                        base.PageTitle = _pageTitle;
                    }
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                String _pageSubTitle = base.NavigatorGetTransferVar<String>("PageSubTitle");
                if (String.IsNullOrEmpty(_pageSubTitle))
                {
                    base.PageTitleSubTitle = Resources.Common.PageSubtitleList;
                }
                else
                {
                    base.PageTitleSubTitle = _pageSubTitle;
                }
            }
        #endregion

        #region Page Event
            private void RebindFilter()
            {

                if (_rtvComboHierarchicalFilter != null)
                {
                    if (_rtvComboHierarchicalFilter.SelectedNode != null)
                    {
                        foreach (KeyValuePair<String, Object> _item in GetKeyValues(_rtvComboHierarchicalFilter.SelectedNode.Value))
                        {
                            if (!ManageEntityParams.ContainsKey(_item.Key))
                            {
                                ManageEntityParams.Add(_item.Key, _item.Value);
                            }
                        }
                        _rdcComboFilter.Text = _rtvComboHierarchicalFilter.SelectedNode.Text;
                        _rdcComboFilter.SelectedValue = _rtvComboHierarchicalFilter.SelectedNode.Value;

                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        BuildGenericDataTable(EntityNameGrid, ManageEntityParams);

                        //Rebind de la grilla.
                        _RgdMasterGridListManage.Rebind();
                    }
                }
                else
                {
                    //si no esta en el combotree, revisa el combo simple.
                    if (_rdcComboFilter != null)
                    {
                        foreach (KeyValuePair<String, Object> _item in GetKeyValues(_rdcComboFilter.SelectedValue))
                        {
                            if (!ManageEntityParams.ContainsKey(_item.Key))
                            {
                                ManageEntityParams.Add(_item.Key, _item.Value);
                            }
                        }
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        BuildGenericDataTable(EntityNameGrid, ManageEntityParams);

                        //Rebind de la grilla.
                        _RgdMasterGridListManage.Rebind();
                    }
                }
            }
            private void SetParameterOfFilter()
            {

                if (_rtvComboHierarchicalFilter != null)
                {
                    if (_rtvComboHierarchicalFilter.SelectedNode != null)
                    {
                        foreach (KeyValuePair<String, Object> _item in GetKeyValues(_rtvComboHierarchicalFilter.SelectedNode.Value))
                        {
                            if (!ManageEntityParams.ContainsKey(_item.Key))
                            {
                                ManageEntityParams.Add(_item.Key, _item.Value);
                            }
                        }
                        _rdcComboFilter.Text = _rtvComboHierarchicalFilter.SelectedNode.Text;
                        _rdcComboFilter.SelectedValue = _rtvComboHierarchicalFilter.SelectedNode.Value;

                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                    }
                }
                else
                {
                    //si no esta en el combotree, revisa el combo simple.
                    if (_rdcComboFilter != null)
                    {
                        foreach (KeyValuePair<String, Object> _item in GetKeyValues(_rdcComboFilter.SelectedValue))
                        {
                            if (!ManageEntityParams.ContainsKey(_item.Key))
                            {
                                ManageEntityParams.Add(_item.Key, _item.Value);
                            }
                        }
                        //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                        BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                    }
                }
            }
            protected void GridLinkButtonClick_Click(object sender, EventArgs e)
            {
                IButtonControl _lnkButton = (IButtonControl)sender;
                //Debo acceder a la grilla...
                //Como puede haber muchas grillas, entonces la busco como padre el linkButton.
                RadGrid _rgdViewers = (RadGrid)((LinkButton)sender).Parent.Parent.Parent.Parent.Parent;

                //Setea los parametros en el Navigate.
                BuildNavigateParamsFromListManageSelected(_rgdViewers);

                //Aca vuelve a enviar el PKCompost, y ademas el DataKeyName de la grilla lo mete ahi, para que los use el GRC.
                String _pkCompost = NavigatorGetPkEntityIdTransferVar<String>("PkCompost")
                    + "&" + BuildParamsFromListManageSelected(_rgdViewers);
                NavigatorAddPkEntityIdTransferVar("PkCompost", _pkCompost);

                String _paramsToNavigate = ((LinkButton)sender).CommandArgument; //_pkCompost;
                if (String.IsNullOrEmpty(_paramsToNavigate))
                {
                    //Si viene vacio, entonces uso lo que tiene cargado pkCompost...
                    _paramsToNavigate = _pkCompost;
                }
                String _entityName = Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName"));
                NavigatorAddTransferVar("EntityName", _entityName);
                NavigatorAddTransferVar("EntityNameGrid", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameGrid")));
                NavigatorAddTransferVar("EntityNameContextInfo", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameContextInfo")));
                NavigatorAddTransferVar("EntityNameContextElement", Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityNameContextElement")));

                //Navigate(GetPageViewerByEntity(Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName"))), Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName")));

                String _url = GetPageViewerByEntity(Convert.ToString(GetKeyValue(_paramsToNavigate, "EntityName")));
                String _entityPropertyName = _lnkButton.Text;
                NavigateEntity(_url, _entityName, _entityPropertyName, NavigateMenuType.ListManagerMenu);

                ClearLocalSession();
                if (IsFilterHierarchy)
                {
                    _xmlFilterHierarchy = _rtvComboHierarchicalFilter.GetXml();
                }
                else
                {
                    if (!String.IsNullOrEmpty(EntityNameComboFilter))
                    { _xmlComboFilterSimple = _rdcComboFilter.GetXml(); }
                }

                if (_RtvAccountingActivity != null)
                {
                    _xmlFilterActivity = _RtvAccountingActivity.GetXml();
                    _xmlFilterMethodology = _RdcMethodology.GetXml();
                    _xmlFilterFacilityType = _RdcFacilityType.GetXml();
                    _xmlFilterFacility = _RtvSite.GetXml();
                }
            }
            protected void rmnSelection_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
            {
                String _actionTitleDecorator = GetActionTitleDecorator(e.Item);
                String _entityPropertyName = GetContextInfoCaption(EntityNameGrid, _RgdMasterGridListManage);
                String _urlProperties = String.Empty;

                switch (e.Item.Value)
                {
                    case "rmiView":  //VIEW
                        FilterExpressionGrid = String.Empty;
                        //String _params = BuildQueryStringParamsFromListManageSelected(_RgdMasterGridListManage);
                        //Setea los parametros en el Navigate.
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManage);

                        //Aca vuelve a enviar el PKCompost, y ademas el DataKeyName de la grilla lo mete ahi, para que los use el GRC.
                        //NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        //NavigatorAddPkEntityIdTransferVar("PkCompost", BuildParamsFromListManageSelected(_RgdMasterGridListManage));
                        String _pkCompost = NavigatorGetPkEntityIdTransferVar<String>("PkCompost")
                            + "&" + BuildParamsFromListManageSelected(_RgdMasterGridListManage);
                        NavigatorAddPkEntityIdTransferVar("PkCompost", _pkCompost);

                        String _entityName = _EntityName.Replace("_LG", String.Empty);
                        base.NavigatorAddTransferVar("EntityName", _entityName);
                        base.NavigatorAddTransferVar("EntityNameGrid", EntityNameGrid.Replace("_LG", String.Empty));
                        //if (EntityNameGrid == Common.ConstantsEntitiesName.PA.AllKeyIndicators)
                        //{
                        //    _EntityNameGRC = Common.ConstantsEntitiesName.PA.Measurement;
                        //}
                        base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                        base.NavigatorAddTransferVar("EntityNameContextElement", _EntityNameContextElement);

                        //Navigate(GetPageViewerByEntity(_EntityName.Replace("_LG", String.Empty)), EntityNameGrid + " " + e.Item.Text);

                        //NavigateEntity(GetPageViewerByEntity(_entityName), , NavigateMenuAction.View);
                        NavigateEntity(GetPageViewerByEntity(_entityName), _entityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.View);

                        //Navigate("~/MainInfo/ListViewer.aspx", EntityNameGrid + " " + e.Item.Text);
                        break;

                    case "rmiEdit":  //EDIT
                        FilterExpressionGrid = String.Empty;
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        //Context.Items.Add("IdValue", _RgdMasterGridListManage.SelectedValue);
                        //Server.Transfer("~/TestBackEnd/CountriesProperties.aspx");
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManage);
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }
                        base.NavigatorAddTransferVar("EntityName", _EntityName);

                        switch (_EntityName)
                        {
                            case Common.ConstantsEntitiesName.DB.PlannedTasks:
                            case Common.ConstantsEntitiesName.DB.OpenedExceptions:
                            case Common.ConstantsEntitiesName.DB.WorkingExceptions:
                            case Common.ConstantsEntitiesName.DB.ActiveTasks:
                            case Common.ConstantsEntitiesName.DB.OverDueTasks:
                            case Common.ConstantsEntitiesName.DB.AllMyExecutions:
                            case Common.ConstantsEntitiesName.DB.ProcessTaskExecutionExecuted:
                                String _type = _RgdMasterGridListManage.MasterTableView.DataKeyValues[_RgdMasterGridListManage.SelectedItems[0].ItemIndex]["TypeExecution"].ToString();
                                //Si ya contiene la palabra execution...lo dejo, sino reemplazo el processtask por processtaskexecution...
                                if (_type.Contains("Execution"))
                                {
                                    _urlProperties = GetGlobalResourceObject("URLEntityProperties", _type).ToString();
                                }
                                else
                                {
                                    _urlProperties = GetGlobalResourceObject("URLEntityProperties", _type.Replace("ProcessTask", "ProcessTaskExecution")).ToString();
                                }
                                break;
                            default:
                                _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();
                                break;
                        }

                        NavigateEntity(_urlProperties, _EntityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);

                        //Navigate(_urlProperties, EntityNameGrid + " " + e.Item.Text);
                        break;

                    //Para el caso de estar viendo Calculos, se puede ejecutar!!!
                    case "rmiCompute":
                        try
                        {
                            Int64 _idCalculation = Convert.ToInt64(GetKeyValue(BuildParamsFromListManageSelected(_RgdMasterGridListManage), "IdCalculation"));
                            Condesus.EMS.Business.PA.Entities.Calculation _calculation = EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_idCalculation);
                            Decimal _yValueCalculated = _calculation.Calculate();

                            FilterExpressionGrid = String.Empty;
                            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                            BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                            //Recarga la grilla
                            _RgdMasterGridListManage.Rebind();
                        }
                        catch (Exception ex)
                        {
                            //Mostrar en el Status Bar....
                            base.StatusBar.ShowMessage(ex);
                        }
                        break;

                    case "rmiCreateException":
                        FilterExpressionGrid = String.Empty;

                        base.NavigatorAddTransferVar("ExceptionState", Common.Constants.ExceptionStateCreateName);
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManage);
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();

                        NavigateEntity(_urlProperties, _EntityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    case "rmiCloseException":
                        FilterExpressionGrid = String.Empty;

                        base.NavigatorAddTransferVar("ExceptionState", Common.Constants.ExceptionStateCloseName);
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManage);
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();

                        NavigateEntity(_urlProperties, _EntityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    case "rmiTreatException":
                        FilterExpressionGrid = String.Empty;

                        base.NavigatorAddTransferVar("ExceptionState", Common.Constants.ExceptionStateTreatName);
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                        base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManage);
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }

                        _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();

                        NavigateEntity(_urlProperties, _EntityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    default:  //DELETE se ejecuta en el btnOkDelete_Click()
                        break;
                }

                ClearLocalSession();
                if (IsFilterHierarchy)
                {
                    _xmlFilterHierarchy = _rtvComboHierarchicalFilter.GetXml();
                }
                else
                {
                    if (!String.IsNullOrEmpty(EntityNameComboFilter))
                    { _xmlComboFilterSimple = _rdcComboFilter.GetXml(); }
                }

                if (_RtvAccountingActivity != null)
                {
                    _xmlFilterActivity = _RtvAccountingActivity.GetXml();
                    _xmlFilterMethodology = _RdcMethodology.GetXml();
                    _xmlFilterFacilityType = _RdcFacilityType.GetXml();
                    _xmlFilterFacility = _RtvSite.GetXml();
                }
            }
            protected void rmnuGeneralOption_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
            {
                //Cuando estoy en un Listado de Lenguajes y alguien pide add, lo debo llevar al add de la entidad completa, no del Lenguaje. por eso todos los replace del "_LG".
                switch (e.Item.Value)
                {
                    case "rmiAdd": //ADD
                        FilterExpressionGrid = String.Empty;
                        //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo.
                        base.NavigatorClearTransferVars();
                        if (NavigatorContainsPkTransferVar("PkCompost"))
                        {
                            base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                        }
                        base.NavigatorAddTransferVar("EntityName", _EntityName.Replace("_LG", String.Empty));
                        base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                        base.NavigatorAddTransferVar("EntityNameContextElement", _EntityNameContextElement);

                        String _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName.Replace("_LG", String.Empty)).ToString();
                        String _actionTitleDecorator = GetActionTitleDecorator(e.Item);
                        NavigateEntity(_urlProperties, _EntityName.Replace("_LG", String.Empty), _actionTitleDecorator, NavigateMenuAction.Edit);
                        break;

                    default:
                        break;
                }
                ClearLocalSession();
                if (IsFilterHierarchy)
                {
                    if (_rtvComboHierarchicalFilter != null)
                    {
                        _xmlFilterHierarchy = _rtvComboHierarchicalFilter.GetXml();
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(EntityNameComboFilter))
                    { _xmlComboFilterSimple = _rdcComboFilter.GetXml(); }
                }

                if (_RtvAccountingActivity != null)
                {
                    _xmlFilterActivity = _RtvAccountingActivity.GetXml();
                    _xmlFilterMethodology = _RdcMethodology.GetXml();
                    _xmlFilterFacilityType = _RdcFacilityType.GetXml();
                    _xmlFilterFacility = _RtvSite.GetXml();
                }
            }
            protected void _rdcComboFilter_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                //Verifica si hay seleccionado los valores fijos de un combo (-2, -1 o 0)
                String _selectedValue = ((RadComboBox)sender).SelectedValue;
                if (_comboFilterSimpleSelectedValue != _selectedValue)
                {
                    ClearLocalSession();
                    foreach (KeyValuePair<String, Object> _item in GetKeyValues(_selectedValue))
                    {
                        //Obtiene las Key y vuelve a armar el DataTable.
                        if (ManageEntityParams.ContainsKey(_item.Key))
                        {   //Si existe la borra, para volver a agregar
                            ManageEntityParams.Remove(_item.Key);
                        }
                        //Agrega el key al dictionary
                        ManageEntityParams.Add(_item.Key, _item.Value);
                    }
                    //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                    BuildGenericDataTable(EntityNameGrid, ManageEntityParams);

                    //Rebind de la grilla.
                    _RgdMasterGridListManage.Rebind();

                    _comboFilterSimpleSelectedValue = _selectedValue;
                }
            }
            protected void btnOkDelete_Click(object sender, EventArgs e)
            {
                String _radMenuClickedId = Convert.ToString(Request.Form["radMenuClickedId"]);

                try
                {
                    Dictionary<String, Object> _paramForDelete = new Dictionary<String, Object>();
                    String _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost");
                    //Ahora genera el Dictionary con los parametros y se lo pasa a la ManageEntityParams para que contruya la grilla como corresponde.
                    _paramForDelete = GetKeyValues(_pkValues);
                    //Se guarda todos los parametros que recibe... si es que no vienen por PK
                    foreach (KeyValuePair<String, Object> _item in NavigatorTransferenceCollection)
                    {
                        if (!_paramForDelete.ContainsKey(_item.Key))
                        {
                            _paramForDelete.Add(_item.Key, _item.Value);
                        }
                        else
                        {
                            _paramForDelete.Remove(_item.Key);
                            _paramForDelete.Add(_item.Key, _item.Value);
                        }
                    }

                    #region Massive Delete
                    if (_radMenuClickedId == "Option")
                    {
                        //Cuando se ejecuta desde option, recorre toda la grilla quien esta chequeado
                        //De esta forma recorremos todas las filas de la grilla ( de la pagina en la que estamos )
                        foreach (Telerik.Web.UI.GridDataItem _row in _RgdMasterGridListManage.Items)
                        {
                            //Buscamos el Checkbox para ver el valor que tiene y saber si lo eliminamos o no
                            CheckBox _chkSelect = (CheckBox)(_row.FindControl("chkSelectItem"));
                            //Si esta clickeado entramos a la rutina para borrar la fila.
                            if (_chkSelect.Checked)
                            {
                                //Con esto Arma el string con los parametros en base a los DataKeyName de la grilla.
                                String _params = BuildParamsFromListManageChecked(_RgdMasterGridListManage, _row);
                                if (String.IsNullOrEmpty(_params))
                                {
                                    _params = _row.KeyValues.ToString().Replace("{", String.Empty).Replace("}", String.Empty).Replace(@"""", String.Empty).Replace(":", "=");
                                }
                                foreach (KeyValuePair<String, Object> _item in GetKeyValues(_params))
                                {
                                    if (!_paramForDelete.ContainsKey(_item.Key))
                                    {
                                        _paramForDelete.Add(_item.Key, _item.Value);
                                    }
                                    else
                                    {
                                        _paramForDelete.Remove(_item.Key);
                                        _paramForDelete.Add(_item.Key, _item.Value);
                                    }
                                }
                                //Cuando se ejecuta el menu Opciones generales, se borran sobre todos los que estan chequeados.
                                ExecuteGenericMethodEntity(EntityNameToRemove, _paramForDelete);
                            }
                        }
                    }
                    #endregion
                    #region Simple Delete
                    else
                    {
                        //Cuando se ejecuta el menu Selection, solo borra un registro.(el seleccionado)
                        String _params = BuildParamsFromListManageSelected(_RgdMasterGridListManage);
                        foreach (KeyValuePair<String, Object> _item in GetKeyValues(_params))
                        {
                            //si ya existe el dato, lo borra y lo vuelve a meter(actualiza, por las dudas)
                            if (_paramForDelete.ContainsKey(_item.Key))
                            {
                                _paramForDelete.Remove(_item.Key);
                                _paramForDelete.Add(_item.Key, _item.Value);
                            }
                            else
                            {
                                //Si no existe, solamanete lo inserta
                                _paramForDelete.Add(_item.Key, _item.Value);
                            }
                        }
                        //Se borra el elemento seleccionado.
                        ExecuteGenericMethodEntity(EntityNameToRemove, _paramForDelete);
                    }
                    #endregion

                    //Mostrar en el Status Bar
                    base.StatusBar.ShowMessage(Resources.Common.DeleteOK);

                    //Revisa si hay algo en el filtro jeraquico y lo agrega a los parametros para volver a cargar la grilla
                    if (_rtvComboHierarchicalFilter != null)
                    {
                        if (_rtvComboHierarchicalFilter.SelectedNode != null)
                        {
                            foreach (KeyValuePair<String, Object> _item in GetKeyValues(_rtvComboHierarchicalFilter.SelectedNode.Value))
                            {
                                if (!ManageEntityParams.ContainsKey(_item.Key))
                                {
                                    ManageEntityParams.Add(_item.Key, _item.Value);
                                }
                            }
                        }
                    }
                    else
                    {
                        //si no esta en el combotree, revisa el combo simple.
                        if (_rdcComboFilter != null)
                        {
                            foreach (KeyValuePair<String, Object> _item in GetKeyValues(_rdcComboFilter.SelectedValue))
                            {
                                if (!ManageEntityParams.ContainsKey(_item.Key))
                                {
                                    ManageEntityParams.Add(_item.Key, _item.Value);
                                }
                            }
                        }
                    }

                    //Setea que viene de un delete
                    IsReturnFromDelete = true;

                    //Pasa las variables otra vez...para poder navegar a si misma
                    base.NavigatorAddTransferVar("EntityName", _EntityName);
                    base.NavigatorAddTransferVar("EntityNameGrid", EntityNameGrid);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                    base.NavigatorAddTransferVar("EntityNameContextElement", _EntityNameContextElement);
                    base.NavigatorAddTransferVar("IsFilterHierarchy", IsFilterHierarchy);
                    base.NavigatorAddTransferVar("EntityNameComboFilter", EntityNameComboFilter);
                    base.NavigatorAddTransferVar("EntityNameChildrenComboFilter", EntityNameChildrenComboFilter);
                    base.NavigatorAddTransferVar("EntityNameMapClassification", _EntityNameMapClassification);
                    base.NavigatorAddTransferVar("EntityNameMapClassificationChildren", _EntityNameMapClassificationChildren);
                    base.NavigatorAddTransferVar("EntityNameMapElement", _EntityNameMapElement);
                    base.NavigatorAddTransferVar("EntityNameMapElementChildren", _EntityNameMapElementChildren);
                    base.NavigatorAddTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));

                    if (IsFilterHierarchy)
                    {
                        _xmlFilterHierarchy = _rtvComboHierarchicalFilter.GetXml();
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(EntityNameComboFilter))
                        { _xmlComboFilterSimple = _rdcComboFilter.GetXml(); }
                    }

                    if (_RtvAccountingActivity != null)
                    {
                        _xmlFilterActivity = _RtvAccountingActivity.GetXml();
                        _xmlFilterMethodology = _RdcMethodology.GetXml();
                        _xmlFilterFacilityType = _RdcFacilityType.GetXml();
                        _xmlFilterFacility = _RtvSite.GetXml();
                    }
                    NavigateEntity(HttpContext.Current.Request.Url.AbsolutePath, EntityNameGrid, Condesus.WebUI.Navigation.NavigateMenuAction.Delete);
                    IsGridPageIndexChanged = true;

                    //FilterExpressionGrid = String.Empty;
                    ////Setea los datos en el DataTable de base, para que luego se carge la grilla.
                    //BuildGenericDataTable(EntityNameGrid, ManageEntityParams);
                    ////Recarga la grilla sin el/los registros borrados
                    //_RgdMasterGridListManage.Rebind();
                }
                catch (Exception ex)
                {
                    //Mostrar en el Status Bar....
                    base.StatusBar.ShowMessage(ex);
                }
                //oculta el popup.
                this.mpelbDelete.Hide();
            }
            protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                e.Node.Nodes.Clear();
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(EntityNameChildrenComboFilter, _params);
                foreach (DataRow _drRecord in DataTableListManage[EntityNameChildrenComboFilter].Rows)
                {
                    RadTreeNode _node = SetGenericNodeTreeView(_drRecord, EntityNameChildrenComboFilter, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, false, false);
                    e.Node.Nodes.Add(_node);
                    SetExpandMode(_node, EntityNameChildrenComboFilter, false, false);
                }
            }
            //Evento para el Expand del Combo con Tree pero ElementMaps
            protected void rtvElementMaps_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                //Limpio los hijos, para no duplicar al abrir y cerrar.
                e.Node.Nodes.Clear();
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params = GetKeyValues(e.Node.Value);

                //Primero lo hace sobre las Clasificaciones Hijas...
                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(_EntityNameMapClassificationChildren, _params);
                if (DataTableListManage.ContainsKey(_EntityNameMapClassificationChildren))
                {
                    foreach (DataRow _drRecord in DataTableListManage[_EntityNameMapClassificationChildren].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, _EntityNameMapClassificationChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                        SetExpandMode(_node, _EntityNameMapClassificationChildren, true, false);
                    }
                }

                //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                BuildGenericDataTable(_EntityNameMapElementChildren, _params);
                if (DataTableListManage.ContainsKey(_EntityNameMapElementChildren))
                {
                    foreach (DataRow _drRecord in DataTableListManage[_EntityNameMapElementChildren].Rows)
                    {
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, _EntityNameMapElementChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                        e.Node.Nodes.Add(_node);
                        //Los elementos no tienen hijos
                        //SetExpandMode(_node, _EntityNameMapElementChildren);
                    }
                }
            }
            protected void rtvElementMaps_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                ClearLocalSession();
                _rdcComboFilter.Text = e.Node.Text;
                _rdcComboFilter.SelectedValue = e.Node.Value;

                //Verifica si hay seleccionado los valores fijos de un combo (-2, -1 o 0)
                String _selectedValue = ((RadTreeView)sender).SelectedValue;
                foreach (KeyValuePair<String, Object> _item in GetKeyValues(_selectedValue))
                {
                    //Obtiene las Key y vuelve a armar el DataTable.
                    if (ManageEntityParams.ContainsKey(_item.Key))
                    {   //Si existe la borra, para volver a agregar
                        ManageEntityParams.Remove(_item.Key);
                    }
                    //Agrega el key al dictionary
                    ManageEntityParams.Add(_item.Key, _item.Value);
                }
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(EntityNameGrid, ManageEntityParams);

                //Rebind de la grilla.
                _RgdMasterGridListManage.Rebind();
            }
            protected void rtvHierarchicalTreeViewInCombo_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                ClearLocalSession();
                _rdcComboFilter.Text = e.Node.Text;
                _rdcComboFilter.SelectedValue = e.Node.Value;

                //Verifica si hay seleccionado los valores fijos de un combo (-2, -1 o 0)
                String _selectedValue = ((RadTreeView)sender).SelectedValue;
                foreach (KeyValuePair<String, Object> _item in GetKeyValues(_selectedValue))
                {
                    //Obtiene las Key y vuelve a armar el DataTable.
                    if (ManageEntityParams.ContainsKey(_item.Key))
                    {   //Si existe la borra, para volver a agregar
                        ManageEntityParams.Remove(_item.Key);
                    }
                    //Agrega el key al dictionary
                    ManageEntityParams.Add(_item.Key, _item.Value);
                }
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(EntityNameGrid, ManageEntityParams);

                //Rebind de la grilla.
                _RgdMasterGridListManage.Rebind();

                _xmlFilterHierarchy = _rtvComboHierarchicalFilter.GetXml();
            }
            protected void _rdp_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
            {
                ClearLocalSession();
                Dictionary<String, Object> _param = new Dictionary<String, Object>();
                _param.Add("StartDate", e.NewDate);

                BuildGenericDataTable(EntityNameGrid, _param);

                _RgdMasterGridListManage.Rebind();
            }
            protected void RgdMasterGridListManage_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item is GridDataItem)
                {
                    #region rptButton
                    HtmlImage oimg2 = (HtmlImage)e.Item.FindControl("rptButton");
                    if (!(oimg2 == null))
                    {
                        try
                        {
                            if (_EntityName.Contains(Common.ConstantsEntitiesName.PF.ProcessTaskExecutionsAttachments))
                            {
                                Int64 _fileSize = Convert.ToInt64(((DataRowView)e.Item.DataItem).Row["FileSize"].ToString());

                                if (_fileSize > 0)
                                {
                                    Int64 _idExecution = Convert.ToInt64(((DataRowView)e.Item.DataItem).Row["IdExecution"]);
                                    Int64 _idProcess = Convert.ToInt64(((DataRowView)e.Item.DataItem).Row["IdProcess"]);
                                    Int64 _idTask = Convert.ToInt64(((DataRowView)e.Item.DataItem).Row["IdTask"]);

                                    oimg2.Attributes["onclick"] = string.Format("return ShowFileAttach(event, " + _idProcess + "," + _idTask + "," + _idExecution + ");");
                                    oimg2.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                                }
                                else
                                {
                                    //No quiero que salga el icono, pero que no pierda el estilo la celda.
                                    oimg2.Attributes["class"] = String.Empty;
                                }
                            }
                            else
                            {
                                String _resourceType = ((DataRowView)e.Item.DataItem).Row["ResourceType"].ToString();
                                String _urlName = ((DataRowView)e.Item.DataItem).Row["Name"].ToString();
                                Int64 _idResource = Convert.ToInt64(((DataRowView)e.Item.DataItem).Row["IdResource"]);
                                Int64 _idResourceFile = Convert.ToInt64(((DataRowView)e.Item.DataItem).Row["IdResourceFile"]);

                                if (_idResourceFile > 0)
                                {
                                    oimg2.Attributes["onclick"] = string.Format("return ShowFile(event, '" + _resourceType + "','" + _urlName + "'," + _idResource + ", " + _idResourceFile + ");");
                                    oimg2.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                                }
                                else
                                {
                                    //No quiero que salga el icono, pero que no pierda el estilo la celda.
                                    oimg2.Attributes["class"] = String.Empty;
                                    //oimg2.Visible = false;
                                }
                            }
                        }
                        catch
                        {
                            //Como sale por error, no quiero que salga el icono.
                            oimg2.Attributes["class"] = String.Empty;
                        }
                    }
                    #endregion

                    #region Image
                    Byte[] _fileStream = null;
                    RadBinaryImage _rbiImage = (RadBinaryImage)e.Item.FindControl("rbiImage");
                    if (_rbiImage != null)
                    {
                        String _pkValuesForRow = GetPKCompostFromItem((RadGrid)sender, e.Item.ItemIndex);
                        //Ahora genera el Dictionary con los parametros y se lo pasa a la ManageEntityParams para que contruya la grilla como corresponde.
                        ManageEntityParams = GetKeyValues(_pkValuesForRow);

                        if ((_CatalogDoc != null) && (_CatalogDoc.Count > 0))
                        {
                            _fileStream = _CatalogDoc.First().Value.FileAttach.FileStream;
                            _rbiImage.DataValue = _fileStream;
                        }
                        else
                        {
                            //_rbiImage.Visible = false;
                            _rbiImage.ImageUrl = "~/Skins/Images/NoImagesAvailable.gif";
                        }
                    }
                    #endregion

                    #region Exception Measurement Out Of Range (Color Row)
                        try
                        {
                            String _measurementStatus = ((DataRowView)e.Item.DataItem).Row["Status"].ToString();
                            if (_measurementStatus.ToLower() == "true")
                            {
                                e.Item.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        catch { }
                    #endregion
                }

            }
            protected void RgdMasterGridListManageKeyIndicator_ItemDataBound(object sender, GridItemEventArgs e)
            {
                if (e.Item is GridDataItem)
                {
                    String _idMeasurement = ((DataRowView)e.Item.DataItem).Row["IdMeasurement"].ToString();

                    ImageButton oimg = (ImageButton)e.Item.FindControl("btnChartLink");
                    if (!(oimg == null))
                    {
                        oimg.Attributes["onclick"] = string.Format("return ShowChart(event, " + _idMeasurement + ");");
                        oimg.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                    }

                    ImageButton oimg2 = (ImageButton)e.Item.FindControl("btnSeriesLink");
                    if (!(oimg2 == null))
                    {
                        //javascript:NavigateToContent(this, event);
                        //oimg2.Attributes["onclick"] = string.Format("return ShowSeries(event,  " + _idMeasurement + ");");

                        String _title = ((DataRowView)e.Item.DataItem).Row["Measurement"].ToString();
                        //Aca hacemos el replace para evitar errores por seguridad de los navegadores.
                        _title = _title.Replace("<sub>", "__").Replace("</sub>", "__").Replace("<sup>", "--").Replace("</sup>", "--");

                        String _keyValues = "Title=" + _title
                            + "&IdMeasurement=" + _idMeasurement.ToString()
                            + "&EntityName=" + Common.ConstantsEntitiesName.PA.Measurement;

                        //Solo para este caso, que esta dentro de un iFrame, se reemplaza el & por | (pipe)
                        oimg2.Attributes.Add("PkCompost", _keyValues.Replace("&", "|"));
                        oimg2.Attributes.Add("Text", _title);
                        oimg2.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Measurement);
                        oimg2.Attributes.Add("EntityNameGrid", String.Empty);
                        oimg2.Attributes.Add("EntityNameContextInfo", String.Empty);
                        oimg2.Attributes.Add("EntityNameContextElement", String.Empty);
                        oimg2.Attributes["onclick"] = string.Format("javascript:ShowSeries(this, event);");
                        oimg2.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                    }

                    #region Exception Measurement Out Of Range (Color Row)
                        try
                        {
                            String _measurementStatus = ((DataRowView)e.Item.DataItem).Row["Status"].ToString();
                            if (_measurementStatus.ToLower() == "true")
                            {
                                e.Item.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        catch { }
                    #endregion

                }
            }

            protected void lnkExportGridMeasurement_Click(object sender, EventArgs e)
            {   //Tengo que volver a construir la grilla plana.
                //LoadGridTransformationMeasurement();

                //ConfigureExport(_rgdMasterGridTransformationMeasurement);

                _RgdMasterGridListManage.Rebind();

                _RgdMasterGridListManage.ExportSettings.ExportOnlyData = true;
                _RgdMasterGridListManage.ExportSettings.IgnorePaging = true;
                _RgdMasterGridListManage.ExportSettings.OpenInNewWindow = true;

                _RgdMasterGridListManage.MasterTableView.ExportToExcel();


                //Response.Clear();
                //Response.ContentType = "application/ms-excel";
                //Response.AddHeader("content-disposition", "attachment;filename=excelimage.xls");
                //System.IO.StringWriter sw = new System.IO.StringWriter();
                //HtmlTextWriter hw = new HtmlTextWriter(sw);
                //_RgdMasterGridListManage.RenderControl(hw);
                //Response.Write(sw.ToString());
                //Response.End();
            }

            #region Viewer
                protected void RgdMasterGridListManage_SelectedIndexChanged(object sender, EventArgs e)
                {
                    ClearLocalSession();
                    //Por defecto el viewer de abajo no sale...cuando se selecciona un registro de la grilla ahi se muestra.
                    pnlViewer.Style.Add("display", "block");

                    String _pkValues = BuildParamsFromListManageSelected(_RgdMasterGridListManage);
                    //Se guarda todos los parametros que estan en la seleccion de la grilla
                    foreach (KeyValuePair<String, Object> _item in GetKeyValues(_pkValues))
                    {
                        if (!ManageEntityParams.ContainsKey(_item.Key))
                        {
                            ManageEntityParams.Add(_item.Key, _item.Value);
                        }
                    }
                    //Init Image Viewer
                    LoadImages();

                    //Carga la info en el tab de RelatedData seleccionado.(Por defecto la primera vez viene el 1° tab seleccionado)
                    if (_RtsTabStrip.SelectedTab != null)
                    {
                        _EntityNameGridRelatedOn = _RtsTabStrip.SelectedTab.Attributes["EntityNameGrid"];
                        _EntityNameRelatedOn = _RtsTabStrip.SelectedTab.Attributes["EntityName"];
                        //Si esta en el primer tab (main data)
                        //Carga el main data
                        if (_EntityNameRelatedOn == _EntityName)
                        {
                            LoadListViewerMainData();
                        }
                        else
                        {
                            //Caso contrario, carga las relaciones
                            LoadRelatedData(_EntityNameGridRelatedOn);
                        }
                    }
                }
                protected void PagerPicture_Click(object sender, ImageClickEventArgs e)
                {
                    IButtonControl _pagerButton = (IButtonControl)sender;

                    //Muevo la Posicion
                    Int32 _index = Int32.Parse(hdn_ImagePosition.Value);
                    _index += Int32.Parse(_pagerButton.CommandArgument);
                    hdn_ImagePosition.Value = _index.ToString();

                    //Con el Index Rearmo la Pantalla con la foto nueva
                    SetPagerStatus(_index);

                    SetImageViewerContent(_index);
                }
                protected void _RtsTabStrip_TabClick(object sender, RadTabStripEventArgs e)
                {
                    String _pkValues = BuildParamsFromListManageSelected(_RgdMasterGridListManage);
                    //Se guarda todos los parametros que estan en la seleccion de la grilla
                    foreach (KeyValuePair<String, Object> _item in GetKeyValues(_pkValues))
                    {
                        if (!ManageEntityParams.ContainsKey(_item.Key))
                        {
                            ManageEntityParams.Add(_item.Key, _item.Value);
                        }
                    }

                    //Construye la grilla en el tab seleccionado.
                    _EntityNameGridRelatedOn = e.Tab.Attributes["EntityNameGrid"];
                    _EntityNameRelatedOn = e.Tab.Attributes["EntityName"];
                    //Si esta en el primer tab (main data)
                    //Carga el main data
                    if (_EntityNameRelatedOn == _EntityName)
                    {
                        LoadListViewerMainData();
                    }
                    else
                    {
                        //Caso contrario, carga las relaciones
                        LoadRelatedData(_EntityNameGridRelatedOn);
                    }
                }
                protected void rgdMasterGridListViewer_ItemDataBound(object sender, GridItemEventArgs e)
                {
                    if (e.Item is GridDataItem)
                    {
                        foreach (GridColumn column in e.Item.OwnerTableView.RenderColumns)
                        {
                            if (column.UniqueName == "Value")
                            {
                                if (String.IsNullOrEmpty(((LinkButton)((GridDataItem)e.Item)["Value"].Controls[0]).Text))
                                {
                                    ((LinkButton)((GridDataItem)e.Item)["Value"].Controls[0]).Text = "&nbsp;";
                                }
                            }
                        }
                        #region Exception Measurement Out Of Range (Color Row)
                            try
                            {
                                String _measurementStatus = ((DataRowView)e.Item.DataItem).Row["Status"].ToString();
                                if (_measurementStatus.ToLower() == "true")
                                {
                                    e.Item.ForeColor = System.Drawing.Color.Red;
                                }
                            }
                            catch { }
                        #endregion
                    }
                }
                protected void rmnSelectionOnRelated_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
                {
                    String _actionTitleDecorator = GetActionTitleDecorator(e.Item);
                    String _entityPropertyName = GetContextInfoCaption(EntityNameGrid, _RgdMasterGridListManageRelated[_EntityNameGridRelatedOn]);
                    String _urlProperties = String.Empty;

                    switch (e.Item.Value)
                    {
                        case "rmiView":  //VIEW
                            FilterExpressionGrid = String.Empty;
                            //Setea los parametros en el Navigate.
                            base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn]);

                            //Aca vuelve a enviar el PKCompost, y ademas el DataKeyName de la grilla lo mete ahi, para que los use el GRC.
                            String _pkCompost = NavigatorGetPkEntityIdTransferVar<String>("PkCompost")
                                + "&" + BuildParamsFromListManageSelected(_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn]);
                            NavigatorAddPkEntityIdTransferVar("PkCompost", _pkCompost);

                            if (_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn].MasterTableView.Columns.FindByUniqueNameSafe("EntityName") != null)
                            {
                                base.NavigatorAddTransferVar("EntityName", _RgdMasterGridListManageRelated[_EntityNameGridRelatedOn].MasterTableView.Items[_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn].SelectedItems[0].ItemIndex].Cells[_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn].Columns.FindByUniqueNameSafe("EntityName").OrderIndex].Text);
                            }
                            else
                            {
                                base.NavigatorAddTransferVar("EntityName", _EntityNameRelatedOn.Replace("_LG", String.Empty));
                            }
                            base.NavigatorAddTransferVar("EntityNameGrid", _EntityNameGridRelatedOn.Replace("_LG", String.Empty));
                            base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                            base.NavigatorAddTransferVar("EntityNameContextElement", _EntityNameContextElement);

                            String _entityName = _EntityNameRelatedOn.Replace("_LG", String.Empty);
                            String _url = GetPageViewerByEntity(_entityName);
                            //NavigateEntity(_url, NavigateMenuAction.View);

                            NavigateEntity(_url, _entityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.View);

                            break;
                        case "rmiEdit":  //EDIT
                            FilterExpressionGrid = String.Empty;

                            base.BuildNavigateParamsFromListManageSelected(_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn]);
                            if (NavigatorContainsPkTransferVar("PkCompost"))
                            {
                                base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                            }

                            _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityNameRelatedOn).ToString();
                            _actionTitleDecorator = GetActionTitleDecorator(e.Item);

                            NavigateEntity(_urlProperties, _EntityNameRelatedOn, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);
                            break;
                    }
                    ClearLocalSession();
                    if (IsFilterHierarchy)
                    {
                        _xmlFilterHierarchy = _rtvComboHierarchicalFilter.GetXml();
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(EntityNameComboFilter))
                        { _xmlComboFilterSimple = _rdcComboFilter.GetXml(); }
                    }

                    if (_RtvAccountingActivity != null)
                    {
                        _xmlFilterActivity = _RtvAccountingActivity.GetXml();
                        _xmlFilterMethodology = _RdcMethodology.GetXml();
                        _xmlFilterFacilityType = _RdcFacilityType.GetXml();
                        _xmlFilterFacility = _RtvSite.GetXml();
                    }
                }
            #endregion
        #endregion

        #region Private Method
            #region List Manage
                private void InyectJavaScript()
                {
                    base.InjectRowContextMenu(rmnSelection.ClientID, String.Empty);
                    base.InjectShowMenu(rmnSelection.ClientID, _RgdMasterGridListManage.ClientID);
                    base.InjectClientSelectRow(_RgdMasterGridListManage.ClientID);
                    base.InjectRmnSelectionItemClickHandler(String.Empty, String.Empty, false);
                    base.InjectValidateItemChecked(_RgdMasterGridListManage.ClientID);
                }
                private void InitializeHandlers()
                {
                    btnOkDelete.Click += new EventHandler(btnOkDelete_Click);
                    GridLinkButtonClick = new EventHandler(GridLinkButtonClick_Click);
                    _RgdMasterGridListManage.ItemCreated += new GridItemEventHandler(rgdMasterGrid_ItemCreated);
                    //_RgdMasterGridListManage.SortCommand += new GridSortCommandEventHandler(rgdMasterGrid_SortCommand);
                    //_RgdMasterGridListManage.PageIndexChanged += new GridPageChangedEventHandler(rgdMasterGrid_PageIndexChanged);
                    _RgdMasterGridListManage.ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenu";
                }
                private void LoadListManage()
                {
                    Boolean _showImgSelect = true;
                    Boolean _showCheck = true;
                    Boolean _showOpenFile = false;
                    Boolean _showOpenChart = false;
                    Boolean _showOpenSeries = false;
                    Boolean _allowSearchableGrid = true; //En el manage si se muestra el search.
                    Boolean _showBinaryImage = false;

                    //POr ahora lo hago para la entidad Execution...despues vemos si es necesario meterlo en un Base por Reflection
                    _showCheck = ShowCheck(_EntityName);
                    //Si se tiene que mostrar un file, entonces agrega la columna de openfile.
                    if ((_EntityName.Contains(Common.ConstantsEntitiesName.KC.ResourceVersion)) 
                        || (_EntityName.Contains(Common.ConstantsEntitiesName.PF.ProcessResource)) 
                        || (_EntityName.Contains(Common.ConstantsEntitiesName.PA.Formula)) 
                        || (_EntityName.Contains(Common.ConstantsEntitiesName.KC.ResourceCatalog)))
                    { _showOpenFile = true; }

                    if (_EntityName.Contains(Common.ConstantsEntitiesName.PF.ProcessTaskExecutionsAttachments) )
                    {
                        _showOpenFile = true;
                        _showImgSelect = false;
                    }

                    //Si se esta visualizando un key indicator, entonces agrega las columnas showchart y showseries
                    if (_EntityName.Contains(Common.ConstantsEntitiesName.PA.KeyIndicator))
                    {
                        _showOpenChart = true;
                        _showOpenSeries = true;
                    }
                    //if (_EntityName.Contains(Common.ConstantsEntitiesName.PA.MeasurementDevice))
                    //{
                    //    _showBinaryImage = true;
                    //}
                    _RgdMasterGridListManage = base.BuildListManageContent(EntityNameGrid, _showImgSelect, _showCheck, _showOpenFile, _showOpenChart, _showOpenSeries, _allowSearchableGrid);
                    //Si es de Lenguajes, no arma el view...no tiene sentido para eso.
                    if (CheckBuildViewer())     //(!EntityNameGrid.Contains("_LG"))
                    {
                        _RgdMasterGridListManage.PageSize = 7;
                        _RgdMasterGridListManage.ClientSettings.EnablePostBackOnRowClick = true;
                        _RgdMasterGridListManage.ClientSettings.Selecting.AllowRowSelect = true;
                        _RgdMasterGridListManage.SelectedIndexChanged += new EventHandler(RgdMasterGridListManage_SelectedIndexChanged);
                    }
                    if (_EntityName == "EmissionByFacility")
                    {
                        _RgdMasterGridListManage.ExportSettings.ExportOnlyData = true;
                        _RgdMasterGridListManage.ExportSettings.IgnorePaging = true;
                        _RgdMasterGridListManage.ExportSettings.OpenInNewWindow = true;
                        _RgdMasterGridListManage.AllowPaging = false;
                        _RgdMasterGridListManage.AllowSorting = false;
                        lnkExport.Style.Add("display", "block");
                    }

                    if (_showBinaryImage)
                    {
                        _RgdMasterGridListManage.ItemDataBound += new GridItemEventHandler(RgdMasterGridListManage_ItemDataBound);
                    }
                    //Si tiene el open file, agrego el evento e inyecto el JS.
                    if (_showOpenFile)
                    {
                        _RgdMasterGridListManage.ItemDataBound += new GridItemEventHandler(RgdMasterGridListManage_ItemDataBound);
                        InjectShowFile(_RgdMasterGridListManage.ClientID);
                        InjectShowFileAttach(_RgdMasterGridListManage.ClientID);
                    }
                    //Si tiene el ShowChart o ShowSeries, agrego el evento e inyecto el JS.
                    if ((_showOpenChart) || (_showOpenSeries))
                    {
                        _RgdMasterGridListManage.ItemDataBound += new GridItemEventHandler(RgdMasterGridListManageKeyIndicator_ItemDataBound);
                        InjectShowChart();
                        InjectShowSeries();
                    }

                    //Si el listado debe ser con Grouping...este metodo debe devolver true!
                    if (IsGridWithGrouping(_EntityName))
                    {
                        _RgdMasterGridListManage.GroupingEnabled = true;
                        _RgdMasterGridListManage.ShowGroupPanel = false;
                        _RgdMasterGridListManage.MasterTableView.GroupLoadMode = GridGroupLoadMode.Client;
                        _RgdMasterGridListManage.MasterTableView.GroupsDefaultExpanded = false;
                        _RgdMasterGridListManage.ClientSettings.AllowDragToGroup = false;
                        _RgdMasterGridListManage.MasterTableView.UseAllDataFields = true;
                        _RgdMasterGridListManage.MasterTableView.RetrieveAllDataFields = true;

                        //_RgdMasterGridListManage.NeedDataSource += new GridNeedDataSourceEventHandler(_RgdMasterGridListManage_NeedDataSource);

                        String _expression = "Project [" + Resources.CommonListManage.Project + "], Title [" + Resources.CommonListManage.Task + "], count(Title) Items [" + Resources.CommonListManage.Count + "] Group By Project, Title";
                        GridGroupByExpression _gridGroupByExpression = GridGroupByExpression.Parse(_expression);

                        _RgdMasterGridListManage.MasterTableView.GroupByExpressions.Add(_gridGroupByExpression);
                    }

                    pnlListManage.Controls.Add(_RgdMasterGridListManage);
                    base.PersistControlState(_RgdMasterGridListManage);
                }
                private void LoadListManageGrouping()
                {
                    _RgdMasterGridListManage = new RadGrid();

                    _RgdMasterGridListManage.ID = "rgdMasterGridListManage" + _EntityName;
                    _RgdMasterGridListManage.AllowPaging = true;
                    _RgdMasterGridListManage.AllowSorting = true;
                    _RgdMasterGridListManage.Skin = "EMS";
                    _RgdMasterGridListManage.EnableEmbeddedSkins = false;
                    _RgdMasterGridListManage.Width = Unit.Percentage(100);
                    _RgdMasterGridListManage.AutoGenerateColumns = true;
                    _RgdMasterGridListManage.GridLines = System.Web.UI.WebControls.GridLines.None;
                    _RgdMasterGridListManage.ShowStatusBar = false;
                    _RgdMasterGridListManage.PageSize = 18;
                    _RgdMasterGridListManage.AllowMultiRowSelection = false;
                    _RgdMasterGridListManage.PagerStyle.AlwaysVisible = true;
                    _RgdMasterGridListManage.MasterTableView.Width = Unit.Percentage(100);
                    _RgdMasterGridListManage.EnableViewState = true;
                    _RgdMasterGridListManage.GroupingEnabled = true;
                    _RgdMasterGridListManage.ShowGroupPanel = true;
                    _RgdMasterGridListManage.MasterTableView.GroupLoadMode = GridGroupLoadMode.Client;
                    _RgdMasterGridListManage.MasterTableView.GroupsDefaultExpanded = false;
                    _RgdMasterGridListManage.ClientSettings.AllowDragToGroup = true;
                    _RgdMasterGridListManage.MasterTableView.UseAllDataFields = true;
                    _RgdMasterGridListManage.MasterTableView.RetrieveAllDataFields = true;

                    _RgdMasterGridListManage.NeedDataSource += new GridNeedDataSourceEventHandler(_RgdMasterGridListManage_NeedDataSource);
                    //_RgdMasterGridListManage.NeedDataSource += new GridNeedDataSourceEventHandler(rgdMasterGrid_NeedDataSource);
                    _RgdMasterGridListManage.PageIndexChanged += new GridPageChangedEventHandler(rgdMasterGrid_PageIndexChanged);
                    _RgdMasterGridListManage.SortCommand += new GridSortCommandEventHandler(rgdMasterGrid_SortCommand);

                    String _expression = "Project [" + Resources.CommonListManage.Project + "], Title [" + Resources.CommonListManage.Task + "], count(Title) Items [" + Resources.CommonListManage.Count + "] Group By Project, Title";
                    GridGroupByExpression expression1 = GridGroupByExpression.Parse(_expression);

                    _RgdMasterGridListManage.MasterTableView.GroupByExpressions.Add(expression1);


                    //Carga la grilla en el panel
                    pnlListManage.Controls.Add(_RgdMasterGridListManage);
                    base.PersistControlState(_RgdMasterGridListManage);

                }
                
                void _RgdMasterGridListManage_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
                {
                    //Este metodo se utiliza para el caso de usar una grilla con Grouping
                    if (String.IsNullOrEmpty(FilterExpressionGrid))
                    {
                        _RgdMasterGridListManage.DataSource = DataTableListManage[EntityNameGrid];
                    }
                    else
                    {
                        DataRow[] _dataRow = DataTableListManage[EntityNameGrid].Select(FilterExpressionGrid);
                        DataTable _dt = DataTableListManage[EntityNameGrid].Clone();
                        for (Int64 i = 0; i < _dataRow.Length; i++)
                        {
                            _dt.ImportRow(_dataRow[i]);
                        }

                        _RgdMasterGridListManage.DataSource = _dt;
                    }
                }
                private void CustomizeExpression(GridGroupByExpression expression)
                {
                    //avoid adding the total field twice
                    GridGroupByField existing = expression.SelectFields.FindByName("Title");
                    if (existing == null) //field is not present
                    {
                        //Construct and add a new aggregate field 
                        GridGroupByField field = new GridGroupByField();
                        field.FieldName = "Title";
                        field.FieldAlias = "Title";
                        field.Aggregate = GridAggregateFunction.Count;
                        field.FormatString = "{0:C}";

                        expression.SelectFields.Add(field);
                    }
                    else //field is present then set a format string
                    {
                        existing.FormatString = "{0:C}";
                    }

                    ////avoid adding the total field twice
                    //GridGroupByField existing = expression.SelectFields.FindByName("TotalAmount");
                    //if (existing == null) //field is not present
                    //{
                    //    //Construct and add a new aggregate field 
                    //    GridGroupByField field = new GridGroupByField();
                    //    field.FieldName = "TotalAmount";
                    //    field.FieldAlias = "SubTotal";
                    //    field.Aggregate = GridAggregateFunction.Sum;
                    //    field.FormatString = "{0:C}";

                    //    expression.SelectFields.Add(field);
                    //}
                    //else //field is present then set a format string
                    //{
                    //    existing.FormatString = "{0:C}";
                    //}
                }

                private Boolean ShowCheck(String entity)
                {
                    switch (entity)
                    {
                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecution:
                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionCalibration:
                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionOperation:
                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement:
                        case Common.ConstantsEntitiesName.DB.ActiveTasks:
                        case Common.ConstantsEntitiesName.DB.PlannedTasks:
                        case Common.ConstantsEntitiesName.DB.FinishedTasks:
                        case Common.ConstantsEntitiesName.DB.OverDueTasks:
                        case "EmissionByFacility":
                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionsAttachments:
                            return false;

                        default: return true;
                    }
                }
                private void LoadMenuSearch()
                {
                    pnlSearch.Controls.Add(base.BuildSearchContent(pnlSearch));

                    //Busca el control Button del Search y si lo encuentra, entonces lo regristra como async
                    Button _btnSearch = (Button)pnlSearch.FindControl("btnSearch");
                    if (_btnSearch != null)
                    {
                        //Registra como Async
                        FwMasterPage.RegisterContentAsyncPostBackTrigger(_btnSearch, "Click");
                    }
                    
                    SetSearchValue();
                }
                private void SetSearchValue()
                {
                    if (!String.IsNullOrEmpty(FilterExpressionGrid))
                    {
                        const int _indexColName = 0;
                        const int _indexColText = 1;

                        String _filter = FilterExpressionGrid.Replace(" OR ", ";"); //.Replace(" = ", String.Empty).Replace("like", String.Empty).Replace("'", String.Empty).Replace("%", String.Empty).Trim();
                        String[] _reg = _filter.Split(';');
                        for (int i = 0; i < _reg.Length; i++)
                        {
                            String[] _col;
                            if (_reg[i].Contains("="))
                            {
                                _col = _reg[i].Replace(" = ", ";").Replace("'", String.Empty).Replace("%", String.Empty).Trim().Split(';');
                            }
                            else
                            {
                                _col = _reg[i].Replace("like", ";").Replace("'", String.Empty).Replace("%", String.Empty).Trim().Split(';');
                            }

                            TextBox _txtSearch = (TextBox)pnlSearch.FindControl("txt" + _col[_indexColName].Trim());
                            _txtSearch.Text = _col[_indexColText].TrimStart().Trim();
                        }
                    }
                }
                private void LoadFilter()
                {
                    PlaceHolder _phComboWithTreeView = new PlaceHolder();
                    _phComboWithTreeView.ID = "phComboWithTreeView" + _EntityNameMapClassification;
                    //Arma el texto para mostrar en combo por defecto.
                    String _selectedValue = String.Empty;

                    //Si el filtro es jerarquico, entonces verifica si es de tipo Mapa o Tree Simple.
                    if (IsFilterHierarchy)
                    {
                        //Verifica si el filtro a cargar es uno de Mapa o un combo Simple.
                        if (_EntityNameMapClassification != String.Empty)
                        {
                            #region Armar un Combo con Tree de ElementMaps


                            //Como se carga un tree con los ElementMaps, se setea este parametro
                            if (!ManageEntityParams.ContainsKey("IsLoadElementMap"))
                            { ManageEntityParams.Add("IsLoadElementMap", true); }
                            //Esto es para hacer combo con tree de ElementMap....
                            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                            BuildGenericDataTable(_EntityNameMapClassification, ManageEntityParams);
                            BuildGenericDataTable(_EntityNameMapElement, ManageEntityParams);
                            //Construye el TreeView
                            _rtvComboHierarchicalFilter = BuildElementMapsContent(_EntityNameMapClassification);
                            //Asocia el Handler del Expand y click
                            _rtvComboHierarchicalFilter.NodeExpand += new RadTreeViewEventHandler(rtvElementMaps_NodeExpand);
                            _rtvComboHierarchicalFilter.NodeClick += new RadTreeViewEventHandler(rtvElementMaps_NodeClick); //+= new RadTreeViewEventHandler(_rtvComboHierarchicalFilter_NodeClick);
                            //Carga los registros en el Tree
                            base.LoadGenericTreeViewElementMap(ref _rtvComboHierarchicalFilter, _EntityNameMapClassification, _EntityNameMapElement, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                            //Busca y setea el valor por default que debe mostrar el combo, por ejemplo "Select a item..."
                            _selectedValue = GetInitialTextComboWithTreeView(_rdcComboFilter, _EntityNameMapClassification);
                            //Contruye el Combo de Filtro.
                            _rdcComboFilter = BuildComboBoxWithTreeView(_rtvComboHierarchicalFilter, _EntityNameMapClassification, false, true, false, _selectedValue, _phComboWithTreeView, false);

                            //Agrego el combo al panel con su tabla
                            pnlFilter.Controls.Add(BuildFilterTable(_rdcComboFilter));

                            PersistControlState(_rtvComboHierarchicalFilter);
                            PersistControlState(_rdcComboFilter);
                            //Ojo
                            //Evento Cliente para mostrar el texto en el combo
                            _rtvComboHierarchicalFilter.OnClientNodeClicking = "OnClientNodeClicking" + _rtvComboHierarchicalFilter.ClientID;

                            //Inyecta el JavaScript del evento cliente para mostrar el texto en el combo
                            base.InjectOnClientNodeClicking(_rdcComboFilter.ClientID, _rtvComboHierarchicalFilter.ClientID, _EntityNameMapClassification, _rtvComboHierarchicalFilter.ClientID);
                            #endregion
                        }
                        else
                        {
                            #region Armar un Combo con Tree (Generico)
                            //Aca cargo un combo con TreeView extra en la pagina(combotree generico)
                            _rtvComboHierarchicalFilter = BuildHierarchicalInComboContent(EntityNameComboFilter);

                            //#region esto para las pruebas....
                            //if (!ManageEntityParams.ContainsKey("IdOrganization"))
                            //{ ManageEntityParams.Add("IdOrganization", 2); }
                            //#endregion

                            //Arma el DataTable con la entidad indicada
                            BuildGenericDataTable(EntityNameComboFilter, ManageEntityParams);
                            //Asocia los Handlers del Tree.
                            _rtvComboHierarchicalFilter.NodeExpand += new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand);
                            _rtvComboHierarchicalFilter.NodeClick += new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeClick);
                            //Carga los datos en el Tree.
                            base.LoadGenericTreeView(ref _rtvComboHierarchicalFilter, EntityNameComboFilter, EntityNameComboFilter, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty, false, false);
                            //Busca y setea el valor por default que debe mostrar el combo, por ejemplo "Select a item..."
                            _selectedValue = GetInitialTextComboWithTreeView(_rdcComboFilter, EntityNameComboFilter);
                            //Contruye el combo con el treeView adentro
                            _rdcComboFilter = BuildComboBoxWithTreeView(_rtvComboHierarchicalFilter, EntityNameComboFilter, false, true, false, _selectedValue, _phComboWithTreeView, false);

                            //Agrego el combo al panel con su tabla
                            pnlFilter.Controls.Add(BuildFilterTable(_rdcComboFilter));

                            PersistControlState(_rtvComboHierarchicalFilter);
                            PersistControlState(_rdcComboFilter);

                            //Ojo
                            //Asocia el Evento de cliente, para mostrar el texto en el combo.
                            _rtvComboHierarchicalFilter.OnClientNodeClicking = "OnClientNodeClicking" + _rtvComboHierarchicalFilter.ClientID;
                            //Inyecta el javascript para mostrar el texto seleccionado en el combo.
                            base.InjectOnClientNodeClicking(_rdcComboFilter.ClientID, _rtvComboHierarchicalFilter.ClientID, EntityNameComboFilter, _rtvComboHierarchicalFilter.ClientID);
                            #endregion
                        }
                    }
                    else
                    {
                        //Carga el DataTable del combo
                        BuildGenericDataTable(EntityNameComboFilter, ManageEntityParams);
                        _rdcComboFilter = BuildComboBox(EntityNameComboFilter, false, true, false, SelectedValueDefaultComboBox, true, false);
                        _rdcComboFilter.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_rdcComboFilter_SelectedIndexChanged);
                        //pnlFilter.Controls.Add(base.BuildFilterContent(pnlFilter, _rdcComboFilter));
                        _rdcComboFilter.EnableLoadOnDemand = false;
 
                        //Agrego el combo al panel con su tabla
                        pnlFilter.Controls.Add(BuildFilterTable(_rdcComboFilter));

                        //PersistControlState(_rdcComboFilter);

                        //Registra como Async
                        //FwMasterPage.RegisterContentAsyncPostBackTrigger(_rdcComboFilter, "SelectedIndexChanged");

                        //Page.RegisterRequiresControlState(_rdcComboFilter);
                    }
                }
                private Table BuildFilterTable(Control filter)
                {

                    //************************ Armo la tabla del filtro ********************

                    String _imagePath = "~/App_Themes/" + this.Page.Theme + "/Images/";

                    Table _tblContentForm = new Table();
                    _tblContentForm.CssClass = "ContentFilter";
                    _tblContentForm.CellSpacing = 0;
                    _tblContentForm.CellPadding = 0;

                    TableRow _currentRow = new TableRow();
                    TableCell _tblCell = new TableCell();
                    _tblCell.CssClass = "Header";
                    //Arreglar problema al hacer click sobre el Search (linkbutton).
                    //_tblCell.Attributes.Add("onclick", "javascript:HideShowSearchContent(this);");
                    Label _lblCaption = new Label();
                    _lblCaption.ID = "lblFilterTitle";
                    _lblCaption.Text = Resources.Common.lblFilter;
                    _tblCell.Controls.Add(_lblCaption);
                    _currentRow.Cells.Add(_tblCell);

                    //_tblCell = new TableCell();
                    //ImageButton _imgControlFilter = new ImageButton();
                    //_imgControlFilter.ID = "imgFilterControl";
                    //_imgControlFilter.ImageUrl = _imagePath + "tras.gif";
                    //_imgControlFilter.CssClass = "ImagesControlFilter";
                    //_imgControlFilter.OnClientClick = "HideShowFilterContent(this)";

                    //_tblCell.Controls.Add(_imgControlFilter);
                    _currentRow.Cells.Add(_tblCell);

                    _tblContentForm.Controls.Add(_currentRow);

                    TableRow _FilterContent = new TableRow();
                    _FilterContent.ID = "filterContent";
                    _FilterContent.Style.Add("display", "block");


                    _currentRow = new TableRow();
                    _tblContentForm.Rows.Add(_currentRow);

                    _tblCell = new TableCell();

                    Panel _pnlContentFormFilter = new Panel();
                    _pnlContentFormFilter.CssClass = "Form";

                    //Agrego el combo a la celda
                    _pnlContentFormFilter.Controls.Add(filter);
                    _tblCell.Controls.Add(_pnlContentFormFilter);
                    _currentRow.Cells.Add(_tblCell);

                    return _tblContentForm;

                }
                private void LoadGeneralOptionMenu()
                {
                    //Para las ejecuciones, no es necesario tener meno de general options....
                    //POr ahora lo hago para la entidad Execution...despues vemos si es necesario meterlo en un Base por Reflection
                    if (!_EntityName.Contains(Common.ConstantsEntitiesName.PF.ProcessTaskExecution))
                    {
                        //RadMenu _rmnuGeneralOption = base.BuildGeneralOptionMenu(true, GetOptionMenuByEntity(_EntityName + "_MenuOption", ManageEntityParams, false));
                        RadMenu _rmnuGeneralOption = base.BuildGeneralOptionMenu(true, GetOptionMenuByEntity(_EntityName + "_MenuOption", ManageEntityParams, false));
                        _rmnuGeneralOption.ItemClick += new RadMenuEventHandler(rmnuGeneralOption_ItemClick);
                    }
                    else
                    {
                        //Si es una ejecucion, debo mirar si es de tipo espontanea, para meter el menu.
                        if (CheckTaskTypeExecutionSpontaneous())
                        {
                            //RadMenu _rmnuGeneralOption = base.BuildGeneralOptionMenu(true, GetOptionMenuByEntity("ProcessTaskExecution_MenuOption", ManageEntityParams, false));
                            RadMenu _rmnuGeneralOption = base.BuildGeneralOptionMenu(true, GetOptionMenuByEntity("ProcessTaskExecution_MenuOption", ManageEntityParams, false));
                            _rmnuGeneralOption.ItemClick += new RadMenuEventHandler(rmnuGeneralOption_ItemClick);
                        }
                    }
                }
                private Boolean CheckTaskTypeExecutionSpontaneous()
                {
                    try
                    {
                        String _typeExecution = String.Empty;
                        Int64 _idprocess = Convert.ToInt64(ManageEntityParams["IdProcess"]);
                        Int64 _idTask = Convert.ToInt64(ManageEntityParams["IdTask"]);

                        _typeExecution = ((Condesus.EMS.Business.PF.Entities.ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_idTask)).TypeExecution;
                        if (_typeExecution == "Spontaneous")
                        { return true; }

                        return false;
                    }
                    catch { return false; }
                }
                private void LoadMenuSelection()
                {
                    //Arma los items del menu.
                    Boolean _showDelete = true;
                    Boolean _showEdit = true;
                    Boolean _showView = true;
                    Boolean _showExecution = false;
                    Boolean _showCompute = false;
                    Boolean _showCloseException = false;
                    Boolean _showTreatException = false;
                    Boolean _showCreateException = false;

                    //POr ahora lo hago para la entidad Execution...despues vemos si es necesario meterlo en un Base por Reflection
                    switch (_EntityName)
                    {
                        case Common.ConstantsEntitiesName.IA.Exception:
                            _showCloseException = true;
                            _showTreatException = true;
                            _showEdit = false;
                            _showDelete = false;
                            break;

                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecution:
                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionCalibration:
                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionOperation:
                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement:
                            _showDelete = false;
                            _showEdit = false;
                            //Como son ejecuciones, puedo ejecutar o crear excepcion, pero para crear una excepcion debe ya estar ejecutada.
                            _showExecution = true;
                            _showCreateException = true;
                            break;

                        case Common.ConstantsEntitiesName.DB.FinishedTasks:
                            _showDelete = false;
                            _showView = false;
                            _showEdit = false;
                            _showCreateException = true;
                            break;
                        case Common.ConstantsEntitiesName.DB.ClosedExceptions:
                            _showDelete = false;
                            _showView = false;
                            _showEdit = false;
                            break;

                        case Common.ConstantsEntitiesName.DB.OpenedExceptions:
                        case Common.ConstantsEntitiesName.DB.WorkingExceptions:
                            _showDelete = false;
                            _showView = false;
                            _showEdit = false;
                            _showExecution = true;
                            _showCloseException = true;
                            _showTreatException = true;
                            break;

                        case Common.ConstantsEntitiesName.DB.PlannedTasks:
                        case Common.ConstantsEntitiesName.DB.ActiveTasks:
                        case Common.ConstantsEntitiesName.DB.OverDueTasks:
                        case Common.ConstantsEntitiesName.DB.AllMyExecutions:
                            _showDelete = true;     // false;
                            _showView = false;
                            _showEdit = false;
                            _showExecution = true;
                            break;
                        case Common.ConstantsEntitiesName.DB.ProcessTaskExecutionExecuted:
                            _showDelete = true;
                            _showView = false;
                            _showEdit = false;
                            _showExecution = false;
                            break;

                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionsAttachments:
                            _showDelete = false;
                            _showView = false;
                            _showEdit = false;
                            _showExecution = false;
                            break;

                        //Todas las entidades que no tienen EDIT
                        case Common.ConstantsEntitiesName.PA.KeyIndicator:
                        case Common.ConstantsEntitiesName.PA.Measurement:
                        case Common.ConstantsEntitiesName.DS.Applicability:
                        case Common.ConstantsEntitiesName.PF.TimeUnit:
                            _showEdit = false;
                            _showDelete = false;
                            break;

                        case Common.ConstantsEntitiesName.PA.Calculations:
                        case Common.ConstantsEntitiesName.PA.Calculation:
                            _showCompute = true;
                            break;


                        case Common.ConstantsEntitiesName.DB.BulkLoad:
                            _showDelete = false;
                            _showView = false;
                            _showEdit = false;
                            _showExecution = true;
                            break;
                    }
                    BuildManageContextMenu(ref rmnSelection, new RadMenuEventHandler(rmnSelection_ItemClick), _showDelete, _showEdit, _showView, _showExecution, _showCompute, _showCloseException, _showTreatException, _showCreateException);
                    InjectContextMenuSelectionOnClientShowing(_RgdMasterGridListManage.ClientID, true);
                }
                private void LoadGRCByEntity()
                {
                    //Cuando es un Add, no debe cargar el GRC!!!
                    if (!String.IsNullOrEmpty(_EntityNameGRC))
                    {
                        //Dictionary<String, Object> _param = new Dictionary<String, Object>();
                        //_param.Add("IdOrganization", _IdOrganization);
                        //_param.Add("PageTitle", txtCorporateName.Text);
                        if (BuildContextInfoModuleMenu(_EntityNameGRC, ManageEntityParams))
                        {
                            base.BuildContextInfoShowMenuButton();
                        }
                    }
                }
                private void RegisterCustomMenuPanels()
                {
                    List<String> _menuPanels = new List<String>();
                    _menuPanels.Add(Common.Constants.ContextInformationKey);
                    _menuPanels.Add(Common.Constants.ContextElementMapsKey);

                    FwMasterPage.ContentNavigatorCustomMenuPanels(_menuPanels);
                }


                private void AddComboAccountingActivities()
                {
                    String _filterExpression = String.Empty;
                    //Combo de AccountingActivity Parent
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboWithTree(phAccountingActivity, ref _RdcAccountingActivity, ref _RtvAccountingActivity,
                        Common.ConstantsEntitiesName.PA.AccountingActivities, _params, false, false, true, ref _filterExpression,
                        new RadTreeViewEventHandler(rtvActivity_NodeExpand), Resources.ConstantMessage.SelectAAccountingActivity, false);
                }
                private void AddComboMethodology()
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddCombo(phMethodology, ref _RdcMethodology, Common.ConstantsEntitiesName.PA.Methodologies, String.Empty, _params, false, true, false, false, false);
                }
                private void AddComboFacilityTypes()
                {
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddCombo(phFacilityType, ref _RdcFacilityType, Common.ConstantsEntitiesName.DS.FacilityTypes, String.Empty, _params, false, true, false, false, false);
                }
                private void AddComboSites()
                {
                    String _filterExpression = String.Empty;
                    //Combo de Organizaciones
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    AddComboTreeSites(ref phFacility, ref _RdcSite, ref _RtvSite, new RadTreeViewEventHandler(rtvSite_NodeExpand));
                }
                protected void rtvSite_NodeExpand(object sender, RadTreeNodeEventArgs e)
                {
                    NodeExpandSites(sender, e, true);
                }
                protected void rtvActivity_NodeExpand(object sender, RadTreeNodeEventArgs e)
                {
                    NodeExpand(sender, e, Common.ConstantsEntitiesName.PA.AccountingActivitiesChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
                }

                private void LoadFilterForExecutionTask()
                {
                    switch (_EntityName)
                    {
                        case Common.ConstantsEntitiesName.DB.PlannedTasks:
                        case Common.ConstantsEntitiesName.DB.ActiveTasks:
                        case Common.ConstantsEntitiesName.DB.OverDueTasks:
                        case Common.ConstantsEntitiesName.DB.AllMyExecutions:
                        case Common.ConstantsEntitiesName.DB.ProcessTaskExecutionExecuted:
                           // base.SetContentTableRowsCss(tblContentFilter);
                            pnlFilterForExecutionTask.Visible = true;
                            btnFilter.Visible = true;
                            btnFilter.Click += new EventHandler(btnFilter_Click);
                            btnClearFilter.Visible = true;
                            btnClearFilter.Click += new EventHandler(btnClearFilter_Click);
                            AddComboAccountingActivities();
                            AddComboMethodology();
                            AddComboFacilityTypes();
                            AddComboSites();
                            break;
                    }
                }

                void btnClearFilter_Click(object sender, EventArgs e)
                {
                    _xmlFilterActivity = String.Empty;
                    _xmlFilterMethodology = String.Empty;
                    _xmlFilterFacilityType = String.Empty;
                    _xmlFilterFacility = String.Empty;

                    AddComboAccountingActivities();
                    AddComboMethodology();
                    AddComboFacilityTypes();
                    AddComboSites();

                    ApplyFilterForTaskExecution();
                }

                private void ApplyFilterForTaskExecution()
                {
                    //Arma el filtro
                    String _search = String.Empty;
                    if ((_RtvAccountingActivity != null) && (_RtvAccountingActivity.SelectedNode != null) && (_RtvAccountingActivity.SelectedNode.Value != Common.Constants.ComboBoxNoDependencyValue))
                    {
                        _search = "IdActivity = " + Convert.ToInt64(GetKeyValue(_RtvAccountingActivity.SelectedNode.Value, "IdActivity"));
                    }

                    if ((_RdcMethodology != null) && (_RdcMethodology.SelectedValue != Common.Constants.ComboBoxSelectItemValue))
                    {
                        if (!String.IsNullOrEmpty(_search))
                        {
                            _search += " AND ";
                        }

                        _search += "IdMethodology = " + Convert.ToInt64(GetKeyValue(_RdcMethodology.SelectedValue, "IdMethodology"));
                    }
                    if ((_RdcFacilityType != null) && (_RdcFacilityType.SelectedValue != Common.Constants.ComboBoxSelectItemValue))
                    {
                        if (!String.IsNullOrEmpty(_search))
                        {
                            _search += " AND ";
                        }

                        _search += "IdFacilityType = " + Convert.ToInt64(GetKeyValue(_RdcFacilityType.SelectedValue, "IdFacilityType"));
                    }

                    //Este metodo se utiliza para el caso de usar una grilla con Grouping
                    if (String.IsNullOrEmpty(_search))
                    {
                        _RgdMasterGridListManage.DataSource = DataTableListManage[EntityNameGrid];
                    }
                    else
                    {
                        DataRow[] _dataRow = DataTableListManage[EntityNameGrid].Select(_search);
                        DataTable _dt = DataTableListManage[EntityNameGrid].Clone();
                        for (Int64 i = 0; i < _dataRow.Length; i++)
                        {
                            _dt.ImportRow(_dataRow[i]);
                        }

                        _RgdMasterGridListManage.DataSource = _dt;
                    }
                    //Rebind de la grilla.
                    _RgdMasterGridListManage.Rebind();
                }
                protected void btnFilter_Click(object sender, EventArgs e)
                {
                    ApplyFilterForTaskExecution();

                    _RdcAccountingActivity.Text = _RtvAccountingActivity.SelectedNode.Text;
                    _RdcAccountingActivity.SelectedItem.Text = _RtvAccountingActivity.SelectedNode.Text;
                }

                //private void ConfigureExport(RadGrid grid)
                //{
                //    grid.ExportSettings.ExportOnlyData = true;
                //    grid.ExportSettings.IgnorePaging = true;
                //    grid.ExportSettings.OpenInNewWindow = true;
                //}


            #endregion

            #region Viewer
                private void ShowListViewerRelated()
                {
                    //Por defecto el viewer de abajo no sale...cuando se selecciona un registro de la grilla ahi se muestra.
                    pnlViewer.Style.Add("display", "block");

                    String _pkValues = BuildParamsFromListManageSelected(_RgdMasterGridListManage);
                    //Se guarda todos los parametros que estan en la seleccion de la grilla
                    foreach (KeyValuePair<String, Object> _item in GetKeyValues(_pkValues))
                    {
                        if (!ManageEntityParams.ContainsKey(_item.Key))
                        {
                            ManageEntityParams.Add(_item.Key, _item.Value);
                        }
                    }
                    //Carga la info en el tab de RelatedData seleccionado.(Por defecto la primera vez viene el 1° tab seleccionado)
                    if (_RtsTabStrip.SelectedTab != null)
                    {
                        _EntityNameGridRelatedOn = _RtsTabStrip.SelectedTab.Attributes["EntityNameGrid"];
                        _EntityNameRelatedOn = _RtsTabStrip.SelectedTab.Attributes["EntityName"];
                        //Si esta en el primer tab (main data)
                        //Carga el main data
                        if (_EntityNameRelatedOn == _EntityName)
                        {
                            LoadListViewerMainData();
                        }
                        else
                        {
                            //Caso contrario, carga las relaciones
                            LoadRelatedData(_EntityNameGridRelatedOn);
                        }
                    }

                }
                private void LoadListViewerMainData()
                {
                    //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                    BuildGenericDataTable(_EntityName, ManageEntityParams);

                    _rgdMasterGridListViewerMainData = base.BuildListViewerContent(_EntityName);
                    pnlListViewerMainData.Controls.Add(_rgdMasterGridListViewerMainData);

                    //Y ahora construye un PageView con su contenido...
                    RadPageView _rpvPageView = BuildPageView("rpvPageView_" + _EntityName, true);
                    //Finalmente agrega la grilla al pageView...
                    _rpvPageView.Controls.Add(_rgdMasterGridListViewerMainData);

                    //La view que sale con el Main Data no puede tener cell Link.
                    GridLinkButtonClick = null;
                    //GridLinkButtonClick = new EventHandler(GridLinkButtonClick_Click);
                    _rgdMasterGridListViewerMainData.ItemDataBound += new GridItemEventHandler(rgdMasterGridListViewer_ItemDataBound);
                    //base.InjectClientSelectRow(_rgdMasterGridListViewerMainData.ClientID);

                    //Limpia los PageViewer del MultiPage...
                    _RmpMultiPage.PageViews.Clear();
                    //Agrega el PageView al MultiPage.
                    _RmpMultiPage.PageViews.Add(_rpvPageView);
                }
                private void LoadTabsForRelatedData()
                {
                    //Obtiene el Array con la entidad y titulo a mostrar...
                    String[,] _relatedDataEntityNames = GetEntityRelated(_EntityName, ManageEntityParams);
                    String _entityGrid = String.Empty;
                    String _entityName = String.Empty;
                    pnlListViewerRelatedData.Controls.Clear();

                    String _multiPageID = "rmpRelatedData";
                    //Arma el TabStrip
                    _RtsTabStrip = BuildTabStrip(_multiPageID);  // BuildTabStripExtendedInfo();
                    _RtsTabStrip.Align = TabStripAlign.Right;
                    //Mete el evento click en server para que carga en cada Tab la grilla y el evento cliente para que muestre el Loading...
                    _RtsTabStrip.TabClick += new RadTabStripEventHandler(_RtsTabStrip_TabClick);
                    _RtsTabStrip.OnClientTabSelecting = "ShowLoading";

                    //Construye el MultiPage
                    _RmpMultiPage = BuildMultiPage(_multiPageID);

                    //__________________________________________________________________________
                    //Meto a la fuerza un tab con el main info.
                    //Construye los tabs a mostrar
                    RadTab _radTabMainData = BuildTab(Resources.Common.MainData, "contentListViewMainData", "contentListViewMainDataOpen");
                    //Le agrega como atributo el EntityName para que en el evento click construya la grilla.
                    _radTabMainData.Attributes.Add("EntityNameGrid", String.Empty);
                    _radTabMainData.Attributes.Add("EntityName", _EntityName);

                    //Agrega el TAb al TabStrip.
                    _RtsTabStrip.Tabs.Add(_radTabMainData);

                    //Agrega el Tab y el MultiPage al panel a retornar que es lo que se inyectara en la pagina.
                    pnlTabStrip.Controls.Add(_RtsTabStrip);
                    pnlListViewerRelatedData.Controls.Add(_RmpMultiPage);
                    //_____________________________________________________

                    //Si hay al menos un dato para mostrar, construye el tab... y sigue
                    if (_relatedDataEntityNames.GetLength(0) > 0)
                    {
                        //Recorre las Entidades relacionadas para la entidad que se esta visualizando y arma cada Tab.
                        for (int i = 0; i < _relatedDataEntityNames.GetLength(0); i++)
                        {
                            _entityGrid = _relatedDataEntityNames[i, 0]; //Accede al nombre de entindad (con este contruye la grilla)
                            _entityName = _relatedDataEntityNames[i, 4];    //Accede al nombre de la entidad singular, para el view o edit.
                            String _cssClassName = _relatedDataEntityNames[i, 1]; //Accede al cssClass.
                            String _cssClassSelectedName = _relatedDataEntityNames[i, 2]; //Accede al cssClassSelected.
                            String _textTooltip = _relatedDataEntityNames[i, 3]; //Accede al titulo. para visualizar como tooltip en el tab

                            //Inyecta el JS para el manejo del menu de seleccion....
                            base.InjectRowContextMenu(rmnSelectionOnRelated.ClientID, _entityGrid);
                            base.InjectRmnSelectionItemClickHandler(String.Empty, String.Empty, false);

                            //Construye los tabs a mostrar
                            RadTab _radTab = BuildTab(_textTooltip, _cssClassName, _cssClassSelectedName);
                            //Le agrega como atributo el EntityName para que en el evento click construya la grilla.
                            _radTab.Attributes.Add("EntityNameGrid", _entityGrid);
                            _radTab.Attributes.Add("EntityName", _entityName);

                            //Construye una grilla vacia, para inyectar el JS 
                            //se usa esta constante y se hace aqui, ya que las grillas se construyen on demand; entonces no se puede inyectar un JS en un postback, solo en la carga inicial de la pagina...
                            base.InjectClientSelectRow("ctl00_ContentMain_rgdMasterGridListManage" + _entityGrid);
                            base.InjectShowMenu(rmnSelectionOnRelated.ClientID, "ctl00_ContentMain_rgdMasterGridListManage" + _entityGrid);

                            //Agrega el TAb al TabStrip.
                            _RtsTabStrip.Tabs.Add(_radTab);

                            //Agrega el Tab y el MultiPage al panel a retornar que es lo que se inyectara en la pagina.
                            pnlTabStrip.Controls.Add(_RtsTabStrip);
                            pnlListViewerRelatedData.Controls.Add(_RmpMultiPage);
                        }
                    }
                    else
                    {
                        //Agrega el Tab y el MultiPage al panel a retornar que es lo que se inyectara en la pagina.
                        pnlTabStrip.Controls.Add(_RtsTabStrip);
                        pnlListViewerRelatedData.Controls.Add(_RmpMultiPage);
                    }
                }
                private void LoadRelatedData(String entityGrid)
                {
                    //Y ahora construye un PageView con su contenido...
                    RadPageView _rpvPageView = BuildPageView("rpvPageView_" + entityGrid, true);
                    if (!String.IsNullOrEmpty(entityGrid))
                    {
                        //Cargo la grilla con el nombre de la entidad indicada
                        if (_RgdMasterGridListManageRelated.ContainsKey(entityGrid))
                        {
                            _RgdMasterGridListManageRelated.Remove(entityGrid);
                        }
                        _RgdMasterGridListManageRelated.Add(entityGrid, LoadGridRelatedData(entityGrid));
                        _RgdMasterGridListManageRelated[entityGrid].ClientSettings.ClientEvents.OnRowContextMenu = "RowContextMenu" + entityGrid;

                        //Finalmente agrega la grilla al pageView...
                        _rpvPageView.Controls.Add(_RgdMasterGridListManageRelated[entityGrid]);
                    }

                    //Limpia los PageViewer del MultiPage...
                    _RmpMultiPage.PageViews.Clear();
                    //Agrega el PageView al MultiPage.
                    _RmpMultiPage.PageViews.Add(_rpvPageView);
                }
                private RadGrid LoadGridRelatedData(String entityGrid)
                {
                    //Construye la grilla para la entidad indicada.
                    Boolean _showImgSelect = true;
                    Boolean _showCheck = false;
                    Boolean _allowSearchableGrid = false;
                    Boolean _showOpenFile = false;
                    Boolean _showOpenChart = false;
                    Boolean _showOpenSeries = false;
                    Boolean _showOpenChat = false;

                    BuildGenericDataTable(entityGrid, ManageEntityParams);
                    ////Si se tiene que mostrar un file, entonces agrega la columna de openfile.
                    if ((_EntityName.Contains("Attachment")) || (_EntityName.Contains("Specification")) || (_EntityName.Contains("Image")))
                    { _showOpenFile = true; }

                    if (entityGrid.Contains("ChatByContext"))
                    { _showOpenChat = true; }

                    RadGrid _RgdMasterGridRelated = base.BuildListManageContent(entityGrid, _showImgSelect, _showCheck, _showOpenFile, _showOpenChart, _showOpenSeries, _allowSearchableGrid);
                    //Cambio la cantidad de registros, para las grillas de relaciones
                    _RgdMasterGridRelated.PageSize = 3;

                    base.PersistControlState(_RgdMasterGridRelated);
                    _RgdMasterGridRelated.ItemCreated += new GridItemEventHandler(rgdMasterGrid_ItemCreated);

                    //Si tiene el open file, agrego el evento e inyecto el JS.
                    if (_showOpenFile)
                    {
                        _RgdMasterGridRelated.ItemDataBound += new GridItemEventHandler(RgdMasterGridListManage_ItemDataBound);
                        InjectShowFile(_RgdMasterGridRelated.ClientID);
                        //vuelve a la normalidad..
                        _showOpenFile = false;
                    }

                    return _RgdMasterGridRelated;
                }
                private void LoadMenuSelectionForGridRelated()
                {
                    //Arma los items del menu.
                    Boolean _showDelete = true;
                    Boolean _showEdit = true;
                    Boolean _showView = true;
                    Boolean _showExecution = false;
                    Boolean _showCompute = false;
                    Boolean _showCloseException = false;
                    Boolean _showTreatException = false;
                    Boolean _showCreateException = false;

                    //POr ahora lo hago para la entidad Execution...despues vemos si es necesario meterlo en un Base por Reflection
                    switch (_EntityName)
                    {
                        case Common.ConstantsEntitiesName.IA.Exception:
                            _showCloseException = true;
                            _showTreatException = true;
                            _showEdit = false;
                            _showDelete = false;
                            break;

                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecution:
                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionCalibration:
                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionOperation:
                        case Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement:
                            _showDelete = false;
                            _showEdit = false;
                            //Como son ejecuciones, puedo ejecutar o crear excepcion, pero para crear una excepcion debe ya estar ejecutada.
                            _showExecution = true;
                            _showCreateException = true;
                            break;

                        case Common.ConstantsEntitiesName.DB.FinishedTasks:
                            _showDelete = false;
                            _showView = false;
                            _showEdit = false;
                            _showCreateException = true;
                            break;
                        case Common.ConstantsEntitiesName.DB.ClosedExceptions:
                            _showDelete = false;
                            _showView = false;
                            _showEdit = false;
                            break;

                        case Common.ConstantsEntitiesName.DB.OpenedExceptions:
                        case Common.ConstantsEntitiesName.DB.WorkingExceptions:
                            _showDelete = false;
                            _showView = false;
                            _showEdit = false;
                            _showExecution = true;
                            _showCloseException = true;
                            _showTreatException = true;
                            break;

                        case Common.ConstantsEntitiesName.DB.PlannedTasks:
                        case Common.ConstantsEntitiesName.DB.ActiveTasks:
                        case Common.ConstantsEntitiesName.DB.OverDueTasks:
                        case Common.ConstantsEntitiesName.DB.AllMyExecutions:
                        case Common.ConstantsEntitiesName.DB.ProcessTaskExecutionExecuted:
                            _showDelete = false;
                            _showView = false;
                            _showEdit = false;
                            _showExecution = true;
                            break;

                        //Todas las entidades que no tienen EDIT
                        case Common.ConstantsEntitiesName.DS.Applicability:
                        case Common.ConstantsEntitiesName.PF.TimeUnit:
                            _showEdit = false;
                            _showDelete = false;
                            break;

                        case Common.ConstantsEntitiesName.PA.Calculations:
                        case Common.ConstantsEntitiesName.PA.Calculation:
                            _showCompute = true;
                            break;
                    }
                    BuildManageContextMenu(ref rmnSelectionOnRelated, new RadMenuEventHandler(rmnSelectionOnRelated_ItemClick), _showDelete, _showEdit, _showView, _showExecution, _showCompute, _showCloseException, _showTreatException, _showCreateException);
                    InjectContextMenuSelectionOnClientShowing(_RgdMasterGridListManageRelated[_EntityNameGridRelatedOn].ClientID, true);

                    //Si existe un Delete...hay que sacarlo...
                    if (rmnSelectionOnRelated.Items.FindItemByValue("rmiDelete") != null)
                    {
                        rmnSelectionOnRelated.Items.FindItemByValue("rmiDelete").Visible = false;
                    }

                }

                #region Image
                    private void LoadImages()
                    {
                        //Init Image Viewer
                        SetImageViewerContent(0);
                        SetPagerStatus(0);

                        btnPrevPicture.Click += new ImageClickEventHandler(PagerPicture_Click);
                        btnNextPicture.Click += new ImageClickEventHandler(PagerPicture_Click);
                    }
                    private void SetImageViewerContent(Int32 index)
                    {
                        Int64 _idResource = -1;
                        Int64 _idResourceFile = -1;
                        try
                        {
                            imgShowSlide.Style.Add("display", "inline-block");
                            btnPrevPicture.Style.Add("display", "inline-block");
                            btnNextPicture.Style.Add("display", "inline-block");
                            upCounter.Visible = true;
                            if (_CatalogDoc.Count == 0)
                            {
                                //Cuando no hay imagenes, se oculta todo.
                                upCounter.Visible = false;
                                imgShowSlide.Style.Add("display", "none");
                                btnPrevPicture.Style.Add("display", "none");
                                btnNextPicture.Style.Add("display", "none");
                                //imgShowSlide.ImageUrl = "~/Skins/Images/NoImagesAvailable.gif";
                                hdn_ImagePosition.Value = "0";
                                return;
                            }

                            _idResource = _CatalogDoc.ElementAt(index).Value.IdResource;
                            _idResourceFile = _CatalogDoc.ElementAt(index).Value.IdResourceFile;
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            _idResource = _CatalogDoc.ElementAt(_CatalogDoc.Count - 1).Value.IdResource;
                            _idResourceFile = _CatalogDoc.ElementAt(_CatalogDoc.Count - 1).Value.IdResourceFile;
                            hdn_ImagePosition.Value = (_CatalogDoc.Count - 1).ToString();
                        }

                        imgShowSlide.ImageUrl = "~/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx?IdResource=" + _idResource.ToString() + "&IdResourceFile=" + _idResourceFile.ToString();
                    }
                    private void SetPagerStatus(Int32 index)
                    {
                        btnPrevPicture.Enabled = true;
                        btnNextPicture.Enabled = true;

                        if (index == 0)
                            btnPrevPicture.Enabled = false;
                        if (index == _CatalogDoc.Count - 1 || index > _CatalogDoc.Count - 1)
                            btnNextPicture.Enabled = false;

                        //Si no hay ninguna foto en la coleccion
                        if (_CatalogDoc.Count == 0)
                            btnPrevPicture.Enabled = btnNextPicture.Enabled = false;

                        btnPrevPicture.ImageUrl = "~/Skins/Images/Trans.gif";
                        btnNextPicture.ImageUrl = "~/Skins/Images/Trans.gif";
                    }
                #endregion

            #endregion

            private void ClearLocalSession()
            {
                _xmlFilterHierarchy = String.Empty;
                _xmlComboFilterSimple = String.Empty;
                _comboFilterSimpleSelectedValue = String.Empty;
            }
            protected void InjectOnLoadSetComboFilterText(String entityComboName, String selectedText)
            {
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());
                _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
                _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
                _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

                _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
                _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
                _sbBuffer.Append("      window.attachEvent('onload', OnLoadSetComboFilterText);                                 \n");
                _sbBuffer.Append("  }                                                                                           \n");
                _sbBuffer.Append("  else                                                                                        \n");
                _sbBuffer.Append("  {   //FireFox                                                                               \n");
                _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', OnLoadSetComboFilterText, false);         \n");
                _sbBuffer.Append("  }                                                                                           \n");
                _sbBuffer.Append("var ComboBoxTreeview = null;                                                                  \n");
                _sbBuffer.Append("function OnLoadSetComboFilterText()                                                           \n");
                _sbBuffer.Append("{                                                                                                     \n");
                //Obtiene el combo
                _sbBuffer.Append("  var comboBox = document.getElementById('ctl00_ContentMain_rdc" + entityComboName + "_Input');                                                \n");
                //Setea el texto del combo con el nodo del tree
                _sbBuffer.Append("  comboBox.value = '" + selectedText + "';                                                                 \n");
                _sbBuffer.Append("}                                                                                                     \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_OnLoadSetComboFilterText", _sbBuffer.ToString());
            }
            protected void InjectOnLoadSetComboFilterTextForTaskExecution(String comboActivity, String selectedTextActivity, String comboMethodology, String selectedTextMethodology,
                String comboFacilityType, String selectedTextFacilityType, String comboFacility, String selectedTextFacility)
            {
                System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

                _sbBuffer.Append(OpenHtmlJavaScript());
                _sbBuffer.Append("  var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
                _sbBuffer.Append("  var _FIREFOX = 'Netscape';                                                                  \n");
                _sbBuffer.Append("  var _BrowserName = navigator.appName;                                                       \n");

                _sbBuffer.Append("  if (_BrowserName == _IEXPLORER)                                                             \n");
                _sbBuffer.Append("  {   //IE and Opera                                                                          \n");
                _sbBuffer.Append("      window.attachEvent('onload', OnLoadSetComboFilterTextForTaskExecution);                                 \n");
                _sbBuffer.Append("  }                                                                                           \n");
                _sbBuffer.Append("  else                                                                                        \n");
                _sbBuffer.Append("  {   //FireFox                                                                               \n");
                _sbBuffer.Append("      document.addEventListener('DOMContentLoaded', OnLoadSetComboFilterTextForTaskExecution, false);         \n");
                _sbBuffer.Append("  }                                                                                           \n");
                _sbBuffer.Append("var ComboBoxTreeview = null;                                                                  \n");
                _sbBuffer.Append("function OnLoadSetComboFilterTextForTaskExecution()                                                           \n");
                _sbBuffer.Append("{                                                                                                     \n");
                //Obtiene el combo 
                _sbBuffer.Append("  var comboBox = document.getElementById('" + comboActivity + "_Input');                                                \n");
                //Setea el texto del combo con el nodo del tree
                _sbBuffer.Append("  comboBox.value = '" + selectedTextActivity + "';                                                                 \n");

                //Obtiene el combo 
                _sbBuffer.Append("  var comboBox = document.getElementById('" + comboMethodology + "_Input');                                                \n");
                //Setea el texto del combo con el nodo del tree
                _sbBuffer.Append("  comboBox.value = '" + selectedTextMethodology + "';                                                                 \n");

                //Obtiene el combo 
                _sbBuffer.Append("  var comboBox = document.getElementById('" + comboFacilityType + "_Input');                                                \n");
                //Setea el texto del combo con el nodo del tree
                _sbBuffer.Append("  comboBox.value = '" + selectedTextFacilityType + "';                                                                 \n");

                //Obtiene el combo 
                _sbBuffer.Append("  var comboBox = document.getElementById('" + comboFacility + "_Input');                                                \n");
                //Setea el texto del combo con el nodo del tree
                _sbBuffer.Append("  comboBox.value = '" + selectedTextFacility + "';                                                                 \n");

                _sbBuffer.Append("}                                                                                                     \n");

                _sbBuffer.Append(CloseHtmlJavaScript());

                InjectJavascript("JS_OnLoadSetComboFilterTextForTaskExecution", _sbBuffer.ToString());
            }
        #endregion
    }
}
