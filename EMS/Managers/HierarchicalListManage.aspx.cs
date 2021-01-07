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

namespace Condesus.EMS.WebUI.Managers
{
    public partial class HierarchicalListManage : BasePage
    {
        #region Internal Properties
        private RadTreeView _rtvMasterHierarchicalListManage;
        private RadComboBox _rdcComboFilter;
        private RadTreeView _rtvComboHierarchicalFilter;
        private RadComboBox _RdcOrganizationalChart;

        private String _EntityNameMapClassification = String.Empty; // "IndicatorClassifications";
        private String _EntityNameMapClassificationChildren = "IndicatorClassificationsChildren";
        private String _EntityNameMapElement = "Indicators";
        private String _EntityNameMapElementChildren = "Indicators";

        private String _EntityName = String.Empty;
        private String _EntityNameGRC = String.Empty;
        private String _EntityNameContextElement = String.Empty;
        private String _EntityNameHierarchical = "IndicatorClassifications";
        private String _EntityNameHierarchicalChildren = "IndicatorClassificationsChildren";
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
        #endregion

        #region PageLoad & Init
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            //Registro Mis Custom MenuPanels
            RegisterCustomMenuPanels();
        }
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //Esto es para limpiar la variable de session para las properties
            Session["IdOrganization_local"] = null;

            //Setea el nombre de la entidad que se va a mostrar
            EntityNameComboFilter = String.Empty;   // "Organizations";
            //EntityNameToRemove = "GeographicAreaRemove";

            SelectedValueDefaultComboBox = String.Empty;    // "IdOrganization=2";

            //Esto es para cuando se llama desde un ContextInfo...
            ManageEntityParams = new Dictionary<String, Object>();
            //Debe recorrer las PK para saber si es un Manage de Lenguajes.
            String _pkValues = NavigatorGetPkEntityIdTransferVar<String>("PkCompost");
            //Ahora genera el Dictionary con los parametros y se lo pasa a la ManageEntityParams para que contruya la grilla como corresponde.
            ManageEntityParams = GetKeyValues(_pkValues);

            _EntityName = base.NavigatorGetTransferVar<String>("EntityName");
            _EntityNameGRC = base.NavigatorGetTransferVar<String>("EntityNameContextInfo");
            if (base.NavigatorContainsTransferVar("EntityNameContextElement"))
            {

                _EntityNameContextElement = base.NavigatorGetTransferVar<String>("EntityNameContextElement");
            }
            _EntityNameHierarchical = base.NavigatorGetTransferVar<String>("EntityNameHierarchical");
            _EntityNameHierarchicalChildren = base.NavigatorGetTransferVar<String>("EntityNameHierarchicalChildren");
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


            //Si no hay entidad para el filtro, no lo inyecta.
            if (!String.IsNullOrEmpty(EntityNameComboFilter) || !String.IsNullOrEmpty(_EntityNameMapClassification))
            {
                LoadFilter();
            }

            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
            BuildGenericDataTable(_EntityNameHierarchical, ManageEntityParams);
            //Arma tree con todos los roots.
            LoadHirarchicalListManage();
            //FwMasterPage.RegisterContentAsyncPostBackTrigger(_rtvMasterHierarchicalListManage, "NodeExpand");

            ////Busca el control grid y lo mantiene en esta pagina
            //_rtvMasterHierarchicalListManage = (RadTreeView)pnlHirarchicalListManage.FindControl("rtvMasterHierarchicalListManage");
            //Carga el GRC en caso de que lo hayan pasado por parametros.
            LoadGRCByEntity();
            //Carga el menu de opciones generales.
            LoadGeneralOptionMenu();
            //Inyecta los javascript que necesita la pagina
            InyectJavaScript();
            //Inserta todos los manejadores de eventos que necesita la apgina
            InitializeHandlers();
            //Arma los items del menu.
            //base.CheckSecurity(ref rmnSelection, "DirectoryServices", "Countries", false, true, true, true, false);
            BuildManageContextMenu(ref rmnSelection, new RadMenuEventHandler(rmnSelection_ItemClick), true, true, true, false, false, false, false, false);
            if (_EntityName == Common.ConstantsEntitiesName.PA.TransformationByTransformation)
            {
                RadMenuItem _rmiShowChart = new RadMenuItem(Resources.CommonListManage.OpenChart);
                _rmiShowChart.Value = "rmiShowChart";
                Common.Functions.DoRadItemSecurity(_rmiShowChart, true);
                rmnSelection.Items.Add(_rmiShowChart);

                RadMenuItem _rmiShowSeries = new RadMenuItem(Resources.CommonListManage.OpenSeries);
                _rmiShowSeries.Value = "rmiShowSeries";
                Common.Functions.DoRadItemSecurity(_rmiShowSeries, true);
                rmnSelection.Items.Add(_rmiShowSeries);

                InjectShowTransformationChart();
                //InjectShowTransformationSeries();
                //InjectShowSeries();
            }

            InjectContextMenuSelectionOnClientShowing(_rtvMasterHierarchicalListManage.ClientID, false);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ////Borra los datos del tree y los vuelve a cargar
            //_rtvMasterHierarchicalListManage.Nodes.Clear();
            ////Vuelve a cargar los datos...
            ////Setea los datos en el DataTable de base, para que luego se carge la grilla.
            //BuildGenericDataTable(_EntityNameHierarchical, ManageEntityParams);
            //base.LoadGenericTreeView(ref _rtvMasterHierarchicalListManage, _EntityNameHierarchical, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage, true, String.Empty, false);
            if (!Page.IsPostBack)
            {
                base.LoadGenericTreeView(ref _rtvMasterHierarchicalListManage, _EntityNameHierarchical, _EntityName, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage, true, String.Empty, false, false);

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
            }
            //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
            String _pageTitle = base.NavigatorGetTransferVar<String>("PageTitle");
            String _pageSubTitle = base.NavigatorGetTransferVar<String>("PageSubTitle");
            if (String.IsNullOrEmpty(_pageTitle))
            {
                base.PageTitle = GetGlobalResourceObject("CommonListManage", _EntityName).ToString();
                base.PageTitleSubTitle = Resources.CommonListManage.lblSubtitle;
            }
            else
            {
                base.PageTitle = _pageTitle;
                base.PageTitleSubTitle = _pageSubTitle;
            }

            ////Si tenemos el filtro seleccionado, lo agregamos a los parametros.
            //if ((_rdcComboFilter != null)  && (_rdcComboFilter.SelectedValue != Common.Constants.ComboBoxSelectItemValue))
            //{
            //    //RebindFilter();
            //    SetParameterOfFilter();
            //}
        }
        //Setea el Titulo de la Pagina
        //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
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
        //Setea el Sub Titulo de la Pagina
        //(como no puedo pasar el Objeto como una 'Interface', lo tengo que hacer localmente)
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
        private String IsEntityMeasurement(String entityName)
        {
            String _entityName = entityName;    // String.Empty;

            //Si es la entidad TransformationByTransformation, puede ser 2 cosas, una transformacion o una medicion...por eso esto!
            if (entityName == Common.ConstantsEntitiesName.PA.TransformationByTransformation)
            {
                //Ahora verifico si el nodo seleccionado tiene el idTransformation quiere decir que es una transformacion, sino es medicion.
                if (_rtvMasterHierarchicalListManage.SelectedNode.Value.Contains("IdTransformation"))
                {   //Queda como esta!
                    _entityName = entityName;
                }
                else
                {   //Es una medicion
                    _entityName = Common.ConstantsEntitiesName.PA.Measurement;
                    _EntityNameGRC = Common.ConstantsEntitiesName.PA.Measurement;
                }
            }

            return _entityName;
        }
        protected void rmnSelection_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
        {
            String _actionTitleDecorator = GetActionTitleDecorator(e.Item);
            String _entityPropertyName = GetContextInfoCaption(_EntityNameHierarchical, _rtvMasterHierarchicalListManage); // GetContextInfoCaption(EntityNameGrid, _RgdMasterGridListManage);
            String _pkCompost = String.Empty;
            String _entityName = String.Empty;
            switch (e.Item.Value)
            {
                case "rmiView":  //VIEW
                    base.BuildNavigateParamsFromSelectedValue(_rtvMasterHierarchicalListManage.SelectedNode.Value);

                    //Aca vuelve a enviar el PKCompost, y ademas el DataKeyName de la grilla lo mete ahi, para que los use el GRC.
                    _pkCompost = NavigatorGetPkEntityIdTransferVar<String>("PkCompost")
                        + "&" + _rtvMasterHierarchicalListManage.SelectedNode.Value;
                    NavigatorAddPkEntityIdTransferVar("PkCompost", _pkCompost);
                    _entityName = _EntityName.Replace("_LG", String.Empty);
                    //Revisa si es una medicion o una transformacion.
                    _entityName = IsEntityMeasurement(_entityName);

                    base.NavigatorAddTransferVar("EntityName", _entityName);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                    base.NavigatorAddTransferVar("EntityNameContextElement", _EntityNameContextElement);
                    
                    NavigateEntity(GetPageViewerByEntity(_entityName), _entityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.View);
                    break;

                case "rmiEdit":  //EDIT
                    ////Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo
                    base.BuildNavigateParamsFromSelectedValue(_rtvMasterHierarchicalListManage.SelectedNode.Value);
                    if (NavigatorContainsPkTransferVar("PkCompost"))
                    {
                        base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                    }
                    String _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();

                    NavigateEntity(_urlProperties, _EntityName, _entityPropertyName, _actionTitleDecorator, NavigateMenuAction.Edit);
                    break;

                case "rmiShowSeries":
                    base.BuildNavigateParamsFromSelectedValue(_rtvMasterHierarchicalListManage.SelectedNode.Value);

                    //Aca vuelve a enviar el PKCompost, y ademas el DataKeyName de la grilla lo mete ahi, para que los use el GRC.
                    _pkCompost = NavigatorGetPkEntityIdTransferVar<String>("PkCompost")
                        + "&" + _rtvMasterHierarchicalListManage.SelectedNode.Value;
                    NavigatorAddPkEntityIdTransferVar("PkCompost", _pkCompost);

                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.Measurement);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                    base.NavigatorAddTransferVar("EntityNameContextElement", String.Empty);
                    NavigatorAddTransferVar("EntityNameGrid", String.Empty);

                    String _args = "ContextInfoNavigation_" + _pkCompost;
                    NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.ContextInfoMenu, _args);

                    String _entityClassName = String.Concat(Common.Functions.ReplaceIndexesTags(_entityPropertyName), " [", GetValueFromGlobalResource("CommonListManage", _entityName), "]", " [", Resources.Common.mnuView, "]");
                    Navigate("~/Managers/IndicatorSeries.aspx", _entityClassName, _menuArgs);
                    break;

                default:  //DELETE se ejecuta en el btnOkDelete_Click()
                    break;
            }

            ClearLocalSession();
            if ((IsFilterHierarchy) && (_rtvComboHierarchicalFilter != null))
            {
                _xmlFilterHierarchy = _rtvComboHierarchicalFilter.GetXml();
            }
            else
            {
                if (!String.IsNullOrEmpty(EntityNameComboFilter))
                { _xmlComboFilterSimple = _rdcComboFilter.GetXml(); }
            }
        }
        protected void rmnuGeneralOption_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "rmiAdd": //ADD
                    //Redireccionar a la pagina que se utiliza para agregar nuevos registros del mismo tipo.
                    base.NavigatorClearTransferVars();
                    if (NavigatorContainsPkTransferVar("PkCompost"))
                    {
                        base.NavigatorAddPkEntityIdTransferVar("PkCompost", NavigatorGetPkEntityIdTransferVar<String>("PkCompost"));
                    }
                    base.NavigatorAddTransferVar("EntityNameContextInfo", _EntityNameGRC);
                    String _urlProperties = GetGlobalResourceObject("URLEntityProperties", _EntityName).ToString();
                    //Navigate(_urlProperties, _EntityNameHierarchical + " " + e.Item.Text);

                    String _actionTitleDecorator = GetActionTitleDecorator(e.Item);
                    //NavigateEntity(_urlProperties, _actionTitleDecorator, NavigateMenuAction.Add);
                    NavigateEntity(_urlProperties, _EntityName, _actionTitleDecorator, NavigateMenuAction.Edit);
                    break;

                default:
                    break;
            }

            ClearLocalSession();
            if ((IsFilterHierarchy) && (_rtvComboHierarchicalFilter!=null))
            {
                _xmlFilterHierarchy = _rtvComboHierarchicalFilter.GetXml();
            }
            else
            {
                if (!String.IsNullOrEmpty(EntityNameComboFilter))
                { _xmlComboFilterSimple = _rdcComboFilter.GetXml(); }
            }
        }
        protected void _rdcComboFilter_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //Limpia el tree, para empezar de cero, con lo filtrado.
            _rtvMasterHierarchicalListManage.Nodes.Clear();
            //Verifica si hay seleccionado los valores fijos de un combo (-2, -1 o 0)
            String _selectedValue = ((RadComboBox)sender).SelectedValue;
            if (_comboFilterSimpleSelectedValue != _selectedValue)
            {
                ClearLocalSession();
                //Obtiene las Key y vuelve a armar el DataTable.
                //ManageEntityParams = GetKeyValues(_selectedValue);
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
                BuildGenericDataTable(_EntityNameHierarchical, ManageEntityParams);
                //Carga los nuevos datos filtrados.
                base.LoadGenericTreeView(ref _rtvMasterHierarchicalListManage, _EntityNameHierarchical, _EntityName, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage, true, String.Empty, false, false);

                _comboFilterSimpleSelectedValue = _selectedValue;
            }
        }
        protected void btnOkDelete_Click(object sender, EventArgs e)
        {
            //Declara una estructura de tipo PILA (con generics) para poder guardar los item chequeadon en la grilla y luego recorrerlos para borrar de hijos a padres...
            Stack<RadTreeNode> _stackRiskClass = new Stack<RadTreeNode>();
            //identifica si ejecuto el menu de Option o Selection
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
                }

                if (_radMenuClickedId == "Option")
                {
                    //Se recorren todos los nodos chequeados
                    foreach (RadTreeNode _node in _rtvMasterHierarchicalListManage.CheckedNodes)
                    {
                        //Se guardan en una pila para luego borrar en orden...
                        _stackRiskClass.Push(_node);
                    }

                    //Ahora recorre la pila y va borrando
                    while (_stackRiskClass.Count > 0)
                    {
                        RadTreeNode _radTreeNode = _stackRiskClass.Pop();

                        //Con esto Arma el string con los parametros en base a los DataKeyName de la grilla.
                        String _params = _radTreeNode.Value;
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
                        //EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).GeographicAreasRemove(Convert.ToInt64(_radTreeNode.Value));
                        _radTreeNode.Remove();
                    }

                }
                else  //Menu Seleccion
                {
                    String _params = _rtvMasterHierarchicalListManage.SelectedNode.Value;
                    //Se borra el elemento seleccionado.
                    foreach (KeyValuePair<String, Object> _item in GetKeyValues(_params))
                    {
                        if (!_paramForDelete.ContainsKey(_item.Key))
                        {
                            _paramForDelete.Add(_item.Key, _item.Value);
                        }
                    }
                    //Cuando se ejecuta el menu Opciones generales, se borran sobre todos los que estan chequeados.
                    ExecuteGenericMethodEntity(EntityNameToRemove, _paramForDelete);

                    //EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).GeographicAreasRemove(_intID);
                    _rtvMasterHierarchicalListManage.SelectedNode.Remove();
                }

                //Mostrar en el Status Bar
                base.StatusBar.ShowMessage(Resources.Common.DeleteOK);

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

                if ((IsFilterHierarchy) && (_rtvComboHierarchicalFilter != null))
                {
                    _xmlFilterHierarchy = _rtvComboHierarchicalFilter.GetXml();
                }
                else
                {
                    if (!String.IsNullOrEmpty(EntityNameComboFilter))
                    { _xmlComboFilterSimple = _rdcComboFilter.GetXml(); }
                }

                NavigateEntity(HttpContext.Current.Request.Url.AbsolutePath, EntityNameGrid, Condesus.WebUI.Navigation.NavigateMenuAction.Delete);
            }
            catch (Exception ex)
            {
                //Mostrar en el Status Bar....
                base.StatusBar.ShowMessage(ex);
            }
            //oculta el popup de confirmacion del delete.
            this.mpelbDelete.Hide();
        }
        //Como este ya es un Combo con TRee Generico, entonces debo manejar el expand en la pagina.
        //La idea de generico es porque puedo poner cuantos yo quiera en una pagina indicandole el nombre de la entidad a cargarle.
        protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
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
        protected void rtvMasterHierarchicalListManage_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            //Limpia todo lo que hay, por las dudas.
            e.Node.Nodes.Clear();

            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            _params = GetKeyValues(e.Node.Value);

            //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
            BuildGenericDataTable(_EntityNameHierarchicalChildren, _params);
            foreach (DataRow _drRecord in DataTableListManage[_EntityNameHierarchicalChildren].Rows)
            {
                RadTreeNode _node = SetNodeTreeViewManage(_drRecord, _EntityNameHierarchicalChildren);
                e.Node.Nodes.Add(_node);
                SetExpandMode(_node, _EntityNameHierarchicalChildren, false, false);
            }
        }
        protected void rtvElementMaps_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            _rdcComboFilter.Text = e.Node.Text;
            _rdcComboFilter.SelectedValue = e.Node.Value;

            //Verifica si hay seleccionado los valores fijos de un combo (-2, -1 o 0)
            String _selectedValue = ((RadTreeView)sender).SelectedValue;
            //Obtiene las Key y vuelve a armar el DataTable.
            //ManageEntityParams = GetKeyValues(_selectedValue);
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
            //Limpia todo lo que hay, por las dudas.
            _rtvMasterHierarchicalListManage.Nodes.Clear();
            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
            BuildGenericDataTable(_EntityNameHierarchical, ManageEntityParams);
            //Carga los nuevos datos filtrados.
            base.LoadGenericTreeView(ref _rtvMasterHierarchicalListManage, _EntityNameHierarchical, _EntityName, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage, true, String.Empty, false, false);
        }
        protected void rtvHierarchicalTreeViewInCombo_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            _rdcComboFilter.Text = e.Node.Text;
            _rdcComboFilter.SelectedValue = e.Node.Value;

            //Verifica si hay seleccionado los valores fijos de un combo (-2, -1 o 0)
            String _selectedValue = ((RadTreeView)sender).SelectedValue;
            //Obtiene las Key y vuelve a armar el DataTable.
            //ManageEntityParams = GetKeyValues(_selectedValue);
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
            //Limpia todo lo que hay, por las dudas.
            _rtvMasterHierarchicalListManage.Nodes.Clear();
            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
            BuildGenericDataTable(_EntityNameHierarchical, ManageEntityParams);
            //Carga los nuevos datos filtrados.
            base.LoadGenericTreeView(ref _rtvMasterHierarchicalListManage, _EntityNameHierarchical, _EntityName, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage, true, String.Empty, false, false);

            _xmlFilterHierarchy = _rtvComboHierarchicalFilter.GetXml();
        }
        void RdcOrganizationalChart_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //Verifica si hay seleccionado los valores fijos de un combo (-2, -1 o 0)
            String _selectedValue = ((RadComboBox)o).SelectedValue;
            //Obtiene las Key y vuelve a armar el DataTable.
            //ManageEntityParams = GetKeyValues(_selectedValue);
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
            //Limpia todo lo que hay, por las dudas.
            _rtvMasterHierarchicalListManage.Nodes.Clear();
            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
            BuildGenericDataTable(_EntityNameHierarchical, ManageEntityParams);
            //Carga los nuevos datos filtrados.
            base.LoadGenericTreeView(ref _rtvMasterHierarchicalListManage, _EntityNameHierarchical, _EntityName, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage, true, String.Empty, false, false);
        }
        void rtvComboHierarchicalFilter_NodeCreated(object sender, RadTreeNodeEventArgs e)
        {
            Dictionary<String, Object> _keyValues = GetKeyValues(e.Node.Value);
            if ((_keyValues.Count == 1) && (_keyValues.ContainsKey("IdOrganization")))
            {
                e.Node.Expanded = true;
                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationalCharts, _keyValues);
                e.Node.Nodes.Clear();
                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.OrganizationalCharts].Rows)
                {
                    RadTreeNode _node = SetGenericNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.OrganizationalCharts, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, false, false);
                    e.Node.Nodes.Add(_node);
                }
            }
        }
        protected void rtvMHListManageTransformation_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            //Limpia todo lo que hay, por las dudas.
            e.Node.Nodes.Clear();
            String _entityNameHierarchicalChildren = String.Empty;

            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            _params = GetKeyValues(e.Node.Value);

            //Si estoy expandiendo el primer nivel o sea measurement, busco las transformaciones
            if (e.Node.Level == 0)
            {
                _entityNameHierarchicalChildren = _EntityNameHierarchicalChildren;
            }
            else
            {
                //Si estoy en los niveles siguientes, entonces busco las transformaciones de transformaciones...
                _entityNameHierarchicalChildren = Common.ConstantsEntitiesName.PA.TransformationsByTransformation;
            }

            //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
            BuildGenericDataTable(_entityNameHierarchicalChildren, _params);
            foreach (DataRow _drRecord in DataTableListManage[_entityNameHierarchicalChildren].Rows)
            {
                RadTreeNode _node = SetNodeTreeViewManage(_drRecord, _entityNameHierarchicalChildren);
                e.Node.Nodes.Add(_node);
                SetExpandMode(_node, _entityNameHierarchicalChildren, false, false);
            }

        }
        protected void rtvMHListManageTransformation_NodeCreated(object sender, RadTreeNodeEventArgs e)
        {
            //Si el level es cero (0) quiere decir que es una medicion, entonces no debe tener checkbox.
            if (e.Node.Level == 0)
            {
                //Si es measurement, no pone check...
                e.Node.Checkable = false;
            }
        }
        #endregion

        #region Private Method
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
                    BuildGenericDataTable(_EntityNameHierarchical, ManageEntityParams);
                    //Carga los nuevos datos filtrados.
                    base.LoadGenericTreeView(ref _rtvMasterHierarchicalListManage, _EntityNameHierarchical, _EntityName, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage, true, String.Empty, false, false);
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
                    BuildGenericDataTable(_EntityNameHierarchical, ManageEntityParams);
                    //Carga los nuevos datos filtrados.
                    base.LoadGenericTreeView(ref _rtvMasterHierarchicalListManage, _EntityNameHierarchical, _EntityName, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayManage, true, String.Empty, false, false);
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
        private void InyectJavaScript()
        {
            base.InjectShowContextMenuTreeView(rmnSelection.ClientID);
            base.InjectRmnSelectionItemClickHandler(String.Empty, _rtvMasterHierarchicalListManage.ClientID, true);
            base.InjectValidateItemChecked(_rtvMasterHierarchicalListManage.ClientID);
        }
        private void InitializeHandlers()
        {
            btnOkDelete.Click += new EventHandler(btnOkDelete_Click);

            rmnSelection.OnClientItemClicking = "rmnSelection_OnClientItemClickedHandler";

            _rtvMasterHierarchicalListManage.OnClientContextMenuShowing = "ShowContextMenuTreeView";

            if (_EntityNameHierarchical == Common.ConstantsEntitiesName.PA.MeasurementsOfTransformation)
            {   //Crea los metodos del TreeView (Server).
                _rtvMasterHierarchicalListManage.NodeExpand += new RadTreeViewEventHandler(rtvMHListManageTransformation_NodeExpand);
                _rtvMasterHierarchicalListManage.NodeCreated += new RadTreeViewEventHandler(rtvMHListManageTransformation_NodeCreated);
            }
            else
            {   //Crea los metodos del TreeView (Server).
                _rtvMasterHierarchicalListManage.NodeExpand += new RadTreeViewEventHandler(rtvMasterHierarchicalListManage_NodeExpand);
            }
        }
        private void LoadHirarchicalListManage()
        {
            phHeaderTable.Controls.Clear();
            phHeaderTable.Controls.Add(BuildHeaderTable());
            pnlHirarchicalListManage.Controls.Clear();
            _rtvMasterHierarchicalListManage = base.BuildHierarchicalListManageContent(_EntityNameHierarchical, "Manage");
            //Por ahora lo saco...Revisar con el Tincho!!!
            //base.PersistControlState(_rtvMasterHierarchicalListManage);
            pnlHirarchicalListManage.Controls.Add(_rtvMasterHierarchicalListManage);
        }
        private void LoadFilter()
        {
            pnlFilter.Controls.Clear();

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
                    if (_EntityNameHierarchical == Common.ConstantsEntitiesName.DS.JobTitles)   //Solo para jobtitles (por ahora.)
                    {
                        //Primero construye el combo y despues lo inyecta junto con el combo de filtro original.
                        _rtvComboHierarchicalFilter.NodeCreated += new RadTreeViewEventHandler(rtvComboHierarchicalFilter_NodeCreated);
                    }
                    //Carga los registros en el Tree
                    base.LoadGenericTreeViewElementMap(ref _rtvComboHierarchicalFilter, _EntityNameMapClassification, _EntityNameMapElement, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
                    //Busca y setea el valor por default que debe mostrar el combo, por ejemplo "Select a item..."
                    _selectedValue = GetInitialTextComboWithTreeView(_rdcComboFilter, _EntityNameMapClassification);
                    //Contruye el Combo de Filtro.
                    _rdcComboFilter = BuildComboBoxWithTreeView(_rtvComboHierarchicalFilter, _EntityNameMapClassification, false, true, false, _selectedValue, _phComboWithTreeView, false);
                    //Finalmente inyecta el combo con el treeview en el panel del filtro que ya existe en la pagina.
                    //pnlFilter.Controls.Add(_rdcComboFilter);
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
                    //RadTreeView _rtvCombo = BuildHierarchicalInComboContent(EntityNameComboFilter);
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
                    _selectedValue = GetInitialTextComboWithTreeView(_rdcComboFilter, _EntityNameMapClassification);
                    //Contruye el combo con el treeView adentro
                    _rdcComboFilter = BuildComboBoxWithTreeView(_rtvComboHierarchicalFilter, EntityNameComboFilter, false, true, false, _selectedValue, _phComboWithTreeView, false);
                    //Inyecta el combo con tree en el panel de filtro que ya existe en la pagina.
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
                //Agrego el combo al panel con su tabla
                pnlFilter.Controls.Add(BuildFilterTable(_rdcComboFilter));

                PersistControlState(_rdcComboFilter);

                //Registra como Async
                FwMasterPage.RegisterContentAsyncPostBackTrigger(_rdcComboFilter, "SelectedIndexChanged");
            }
        }
        private Table BuildFilterTable(RadComboBox filter)
        {

            //************************ Armo la tabla del filtro ********************

            String _imagePath = "~/App_Themes/" + this.Page.Theme + "/Images/";

            Table _tblContentForm = new Table();
            _tblContentForm.CssClass = "ContentFilter";
            _tblContentForm.ID = "tblContentFormFilter";

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
        private Table BuildHeaderTable()
        {
            //************************ Armo la tabla para el header ********************

            Table _tblHeaderTable = new Table();
            _tblHeaderTable.ID = "tblHeaderTable";
            _tblHeaderTable.CssClass = "ContentTableHeader";
            _tblHeaderTable.CellSpacing = 0;
            _tblHeaderTable.CellPadding = 0;

            TableRow _tblHeaderTableRow = new TableRow();
            _tblHeaderTableRow.ID = "tblHeaderTableRow";

            TableCell _tblHeaderTableCell = new TableCell();
            _tblHeaderTableCell.ID = "tblHeaderTableCell";
            _tblHeaderTableCell.Text = GetGlobalResourceObject("CommonListManage", _EntityName).ToString();

            //TableCell _tblHeaderTableCell1 = new TableCell();
            //_tblHeaderTableCell1.ID = "tblHeaderTableCell1";
            //_tblHeaderTableCell1.Text = "Value";

            _tblHeaderTableRow.Controls.Add(_tblHeaderTableCell);
            //_tblHeaderTableRow.Controls.Add(_tblHeaderTableCell1);
            _tblHeaderTable.Controls.Add(_tblHeaderTableRow);

            return _tblHeaderTable;

        }
        private void LoadGeneralOptionMenu()
        {
            RadMenu _rmnuGeneralOption = base.BuildGeneralOptionMenu(true, GetOptionMenuByEntity(_EntityName + "_MenuOption", ManageEntityParams, false));
            _rmnuGeneralOption.ItemClick += new RadMenuEventHandler(rmnuGeneralOption_ItemClick);

            if (_EntityNameHierarchical == Common.ConstantsEntitiesName.PA.MeasurementsOfTransformation)
            {
                //Si existe un Add...hay que sacarlo...porque en este manage el add no va, solo se agregan desde una medicion o desde una transformacion
                if (_rmnuGeneralOption.Items.FindItemByValue("rmiAdd") != null)
                {
                    _rmnuGeneralOption.Items.FindItemByValue("rmiAdd").Visible = false;
                }
            }
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
        #endregion

    }
}
