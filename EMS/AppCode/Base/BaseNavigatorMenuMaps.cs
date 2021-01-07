using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.WebUI.Navigation.State;
using Condesus.WebUI.Navigation.Status;
using Condesus.WebUI.Navigation.Transference;
using Condesus.WebUI.Navigation;

namespace Condesus.EMS.WebUI
{
    public partial class BasePage : System.Web.UI.Page
    {
        //Handler del Click en algun Item del Menu
        void rtvMenuModule_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            //Limpio todas las variables del navigator
            NavigatorClearTransferVars();

            //Se limpia el valor del filtro.
            FilterExpressionGrid = String.Empty;
            String _entityName = String.Empty;
            String _titleDecorator = String.Empty;
            //Se limpian estas variables por las dudas.
            Session["xmlComboFilterSimple"] = String.Empty;
            Session["xmlFilterHierarchy"] = String.Empty;
            Session["comboFilterSimpleSelectedValue"] = String.Empty;

            _SelectedNodeTextGlobalMenu = e.Node.Text;
            _SelectedNodeValueGlobalMenu = e.Node.Value;
            _TreeViewGlobalMenuXML = ((RadTreeView)sender).GetXml();

            if ((Navigator.Current.Transference.Items.MenuContextVars["ModuleSection"].ToLower() == "map") && (Navigator.Current.Transference.Items.MenuContextVars["ModuleValue"] != "Dashboard"))
            {
                //Mapas
                //Obtiene y carga los parametros desde el value del nodo
                BuildNavigateParamsFromSelectedValue(e.Node.Value);
                NavigatorAddPkEntityIdTransferVar("PkCompost", BuildStringParamsFromValues(e.Node.Value));

                //Pasa el nombre de la entidad que debe mostrar
                _entityName = e.Node.Attributes["SingleEntityName"].ToString();
                String _entityNameContextInfo = e.Node.Attributes["EntityNameContextInfo"].ToString();
                String _entityNameContextElement = e.Node.Attributes["EntityNameContextElement"].ToString();
                
                if (_entityName == Common.ConstantsEntitiesName.PF.ProcessGroupProcess)
                {   //Cambio esto para que pueda navegar al view de los facilities...
                    _entityName = Common.ConstantsEntitiesName.DS.FacilitiesByProcess;
                    _entityNameContextInfo=Common.ConstantsEntitiesName.PF.Process;
                    _entityNameContextElement = Common.ConstantsEntitiesName.PF.ProcessGroupProcess;
                    NavigatorAddTransferVar("EntityNameGrid", Common.ConstantsEntitiesName.DS.FacilitiesByProcess);
                }

                NavigatorAddTransferVar("EntityName", _entityName);
                NavigatorAddTransferVar("EntityNameContextInfo", _entityNameContextInfo);
                NavigatorAddTransferVar("EntityNameContextElement", _entityNameContextElement);
                //Finalmente hace el Navigate al viewer.
                var argsColl = new Dictionary<String, String>();
                argsColl.Add("EntityName", _entityName);
                argsColl.Add("EntityNameContextInfo", _entityNameContextInfo);
                argsColl.Add("EntityNameContextElement", _entityNameContextElement);
                argsColl.Add("PkCompost", BuildStringParamsFromValues(e.Node.Value));

                NavigateMenuEventArgs _menuArgs = new NavigateMenuEventArgs(NavigateMenuType.GlobalMenu, argsColl);
                //Va al Viewer
                _titleDecorator = String.Concat(e.Node.Text, " [", GetValueFromGlobalResource("CommonListManage", _entityName), "]", " [", Resources.Common.mnuView, "]");
                Navigate(GetPageViewerByEntity(_entityName), _titleDecorator, _menuArgs);
                //Navigate("~/MainInfo/ListViewer.aspx", e.Node.Text, _menuArgs);
            }
            else
            {
                //Config
                _entityName = e.Node.Attributes["EntityName"];
                NavigatorAddTransferVar("EntityNameGrid", e.Node.Attributes["EntityNameGrid"]);
                NavigatorAddTransferVar("EntityName", _entityName);

                NavigatorAddTransferVar("EntityNameContextInfo", e.Node.Attributes["EntityNameContextInfo"]);
                NavigatorAddTransferVar("EntityNameContextElement", e.Node.Attributes["EntityNameContextElement"]);
                NavigatorAddTransferVar("EntityNameComboFilter", e.Node.Attributes["EntityNameComboFilter"]);
                NavigatorAddTransferVar("EntityNameHierarchical", e.Node.Attributes["EntityNameHierarchical"]);
                NavigatorAddTransferVar("EntityNameHierarchicalChildren", e.Node.Attributes["EntityNameHierarchicalChildren"]);
                NavigatorAddTransferVar("IsFilterHierarchy", Convert.ToBoolean(e.Node.Attributes["IsFilterHierarchy"]));
                NavigatorAddTransferVar("EntityNameChildrenComboFilter", e.Node.Attributes["EntityNameChildrenComboFilter"]);
                NavigatorAddTransferVar("EntityNameMapClassification", e.Node.Attributes["EntityNameMapClassification"]);
                NavigatorAddTransferVar("EntityNameMapClassificationChildren", e.Node.Attributes["EntityNameMapClassificationChildren"]);
                NavigatorAddTransferVar("EntityNameMapElement", e.Node.Attributes["EntityNameMapElement"]);
                NavigatorAddTransferVar("EntityNameMapElementChildren", e.Node.Attributes["EntityNameMapElementChildren"]);

                //Finalmente hace el Navigate al Manage Correspondiente.
                var argsColl = new Dictionary<String, String>();
                argsColl.Add("EntityName", _entityName);
                argsColl.Add("EntityNameGrid", e.Node.Attributes["EntityNameGrid"]);
                argsColl.Add("EntityNameHierarchical", e.Node.Attributes["EntityNameHierarchical"]);
                argsColl.Add("EntityNameHierarchicalChildren", e.Node.Attributes["EntityNameHierarchicalChildren"]);

                NavigateMenuEventArgs _menuArgs = new NavigateMenuEventArgs(NavigateMenuType.GlobalMenu, argsColl);
                //Va al Manager
                _titleDecorator = String.Concat(e.Node.Text, " [", GetValueFromGlobalResource("CommonListManage", _entityName), "]");
                Navigate(e.Node.Attributes["URL"], _titleDecorator, _menuArgs);
                //Navigate(e.Node.Attributes["URL"], e.Node.Text, new NavigateMenuEventArgs());//new NavigateMenuEventArgs(e.Node.Attributes["Menu"], String.Empty));
            }
        }
        /// <summary>
        /// Evento para el Click en un item de ContextMenu del TreeView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rtvMenuElementMaps_ContextMenuItemClick(object sender, RadTreeViewContextMenuEventArgs e)
        {
            String _singleEntityName = String.Empty;
            String _urlProperties = String.Empty;

            String _entityNameJT = String.Empty;
            String _entityNamePost = String.Empty;
            String _entityNameGridPost = String.Empty;
            String _entityNameGridJT = String.Empty;
            String _pkValues = String.Empty;
            String _pageTitleJT = String.Empty;
            String _pageTitlePost = String.Empty;
            String _pageTitleJTConfig = String.Empty;
            String _pageTitlePostConfig = String.Empty;

            String _pkValuesConfig = String.Empty;

            String _entityNameGridJTConfig = String.Empty;
            String _entityNameGridPostConfig = String.Empty;
            String _entityNameJTConfig = String.Empty;
            String _entityNamePostConfig = String.Empty;

            //Se limpian estas variables por las dudas.
            Session["xmlComboFilterSimple"] = String.Empty;
            Session["xmlFilterHierarchy"] = String.Empty;
            Session["comboFilterSimpleSelectedValue"] = String.Empty;

            //Limpio todas las variables del navigator
            NavigatorClearTransferVars();

            #region Set Variables por Modulo
            switch (Navigator.Current.Transference.Items.MenuContextVars["ModuleValue"])
            {
                case "DS":
                    _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.MapDS;
                    _pageTitleJT = Resources.CommonListManage.RightJobTitleMapDS;
                    _pageTitlePost = Resources.CommonListManage.RightPersonMapDS;
                    _entityNameGridJT = Common.ConstantsEntitiesName.SS.RightJobTitleMapsDS;
                    _entityNameJT = Common.ConstantsEntitiesName.SS.RightJobTitleMapDS;
                    _entityNameGridPost = Common.ConstantsEntitiesName.SS.RightPersonMapsDS;
                    _entityNamePost = Common.ConstantsEntitiesName.SS.RightPersonMapDS;
                    //Variables para Config
                    _pkValuesConfig = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationDS;
                    _pageTitleJTConfig = Resources.CommonListManage.RightJobTitleConfigurationDS;
                    _pageTitlePostConfig = Resources.CommonListManage.RightPersonConfigurationDS;
                    _entityNameJTConfig = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationDS;
                    _entityNameGridJTConfig = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsDS;
                    _entityNameGridPostConfig= Common.ConstantsEntitiesName.SS.RightPersonConfigurationsDS;
                    _entityNamePostConfig = Common.ConstantsEntitiesName.SS.RightPersonConfigurationDS;
                    if (e.MenuItem.Value == "rmiClassification")
                    { _singleEntityName = Common.ConstantsEntitiesName.DS.OrganizationClassification; }
                    else
                    { _singleEntityName = Common.ConstantsEntitiesName.DS.Organization; }
                    break;
                case "PM":
                    _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.MapPF;
                    _pageTitleJT = Resources.CommonListManage.RightJobTitleMapPF;
                    _pageTitlePost = Resources.CommonListManage.RightPersonMapPF;
                    _entityNameGridJT = Common.ConstantsEntitiesName.SS.RightJobTitleMapsPF;
                    _entityNameJT = Common.ConstantsEntitiesName.SS.RightJobTitleMapPF;
                    _entityNameGridPost = Common.ConstantsEntitiesName.SS.RightPersonMapsPF;
                    _entityNamePost = Common.ConstantsEntitiesName.SS.RightPersonMapPF;
                    //Variables para Config
                    _pkValuesConfig = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationPF;
                    _pageTitleJTConfig = Resources.CommonListManage.RightJobTitleConfigurationPF;
                    _pageTitlePostConfig = Resources.CommonListManage.RightPersonConfigurationPF;
                    _entityNameJTConfig = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationPF;
                    _entityNameGridJTConfig = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsPF;
                    _entityNamePostConfig = Common.ConstantsEntitiesName.SS.RightPersonConfigurationPF;
                    _entityNameGridPostConfig = Common.ConstantsEntitiesName.SS.RightPersonConfigurationsPF;
                    if (e.MenuItem.Value == "rmiClassification")
                    { _singleEntityName = Common.ConstantsEntitiesName.PF.ProcessClassification; }
                    else
                    { _singleEntityName = Common.ConstantsEntitiesName.PF.Process; }
                    break;
                case "PA":
                    _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.MapPA;
                    _pageTitleJT = Resources.CommonListManage.RightJobTitleMapPA;
                    _pageTitlePost = Resources.CommonListManage.RightPersonMapPA;
                    _entityNameGridJT = Common.ConstantsEntitiesName.SS.RightJobTitleMapsPA;
                    _entityNameJT = Common.ConstantsEntitiesName.SS.RightJobTitleMapPA;
                    _entityNameGridPost = Common.ConstantsEntitiesName.SS.RightPersonMapsPA;
                    _entityNamePost = Common.ConstantsEntitiesName.SS.RightPersonMapPA;
                    //Variables para Config
                    _pkValuesConfig = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationPA;
                    _pageTitleJTConfig = Resources.CommonListManage.RightJobTitleConfigurationPF;
                    _pageTitlePostConfig = Resources.CommonListManage.RightPersonConfigurationPF;
                    _entityNameJTConfig = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationPA;
                    _entityNameGridJTConfig = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsPA;
                    _entityNamePostConfig = Common.ConstantsEntitiesName.SS.RightPersonConfigurationPA;
                    _entityNameGridPostConfig = Common.ConstantsEntitiesName.SS.RightPersonConfigurationsPA;
                    if (e.MenuItem.Value == "rmiClassification")
                    { _singleEntityName = Common.ConstantsEntitiesName.PA.IndicatorClassification; }
                    else
                    { _singleEntityName = Common.ConstantsEntitiesName.PA.Indicator; }
                    break;
                case "KC":
                    _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.MapKC;
                    _pageTitleJT = Resources.CommonListManage.RightJobTitleMapKC;
                    _pageTitlePost = Resources.CommonListManage.RightPersonMapKC;
                    _entityNameGridJT = Common.ConstantsEntitiesName.SS.RightJobTitleMapsKC;
                    _entityNameJT = Common.ConstantsEntitiesName.SS.RightJobTitleMapKC;
                    _entityNameGridPost = Common.ConstantsEntitiesName.SS.RightPersonMapsKC;
                    _entityNamePost = Common.ConstantsEntitiesName.SS.RightPersonMapKC;
                    //Variables para Config
                    _pkValuesConfig = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationKC;
                    _pageTitleJTConfig = Resources.CommonListManage.RightJobTitleConfigurationKC;
                    _pageTitlePostConfig = Resources.CommonListManage.RightPersonConfigurationKC;
                    _entityNameJTConfig = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationKC;
                    _entityNameGridJTConfig = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsKC;
                    _entityNamePostConfig = Common.ConstantsEntitiesName.SS.RightPersonConfigurationKC;
                    _entityNameGridPostConfig = Common.ConstantsEntitiesName.SS.RightPersonConfigurationsKC;
                    if (e.MenuItem.Value == "rmiClassification")
                    { _singleEntityName = Common.ConstantsEntitiesName.KC.ResourceClassification; }
                    else
                    { _singleEntityName = Common.ConstantsEntitiesName.KC.Resource; }
                    break;
                case "RM":
                    _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.MapRM;
                    _pageTitleJT = Resources.CommonListManage.RightJobTitleMapRM;
                    _pageTitlePost = Resources.CommonListManage.RightPersonMapRM;
                    _entityNameGridJT = Common.ConstantsEntitiesName.SS.RightJobTitleMapsRM;
                    _entityNameJT = Common.ConstantsEntitiesName.SS.RightJobTitleMapRM;
                    _entityNameGridPost = Common.ConstantsEntitiesName.SS.RightPersonMapsRM;
                    _entityNamePost = Common.ConstantsEntitiesName.SS.RightPersonMapRM;
                    //Variables para Config
                    _pkValuesConfig = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationRM;
                    _pageTitleJTConfig = Resources.CommonListManage.RightJobTitleConfigurationRM;
                    _pageTitlePostConfig = Resources.CommonListManage.RightPersonConfigurationRM;
                    _entityNameJTConfig = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationRM;
                    _entityNameGridJTConfig = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsRM;
                    _entityNamePostConfig = Common.ConstantsEntitiesName.SS.RightPersonConfigurationRM;
                    _entityNameGridPostConfig = Common.ConstantsEntitiesName.SS.RightPersonConfigurationsRM;
                    if (e.MenuItem.Value == "rmiClassification")
                    { _singleEntityName = Common.ConstantsEntitiesName.RM.RiskClassification; }
                    break;
                case "IA":
                    _pkValues = "ParentEntity=" + Condesus.EMS.Business.Common.Security.MapIA;
                    _pageTitleJT = Resources.CommonListManage.RightJobTitleMapIA;
                    _pageTitlePost = Resources.CommonListManage.RightPersonMapIA;
                    _entityNameGridJT = Common.ConstantsEntitiesName.SS.RightJobTitleMapsIA;
                    _entityNameJT = Common.ConstantsEntitiesName.SS.RightJobTitleMapIA;
                    _entityNameGridPost = Common.ConstantsEntitiesName.SS.RightPersonMapsIA;
                    _entityNamePost = Common.ConstantsEntitiesName.SS.RightPersonMapIA;
                    //Variables para Config
                    _pkValuesConfig = "ParentEntity=" + Condesus.EMS.Business.Common.Security.ConfigurationIA;
                    _pageTitleJTConfig = Resources.CommonListManage.RightJobTitleConfigurationIA;
                    _pageTitlePostConfig = Resources.CommonListManage.RightPersonConfigurationIA;
                    _entityNameJTConfig = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationIA;
                    _entityNameGridJTConfig = Common.ConstantsEntitiesName.SS.RightJobTitleConfigurationsIA;
                    _entityNamePostConfig = Common.ConstantsEntitiesName.SS.RightPersonConfigurationIA;
                    _entityNameGridPostConfig = Common.ConstantsEntitiesName.SS.RightPersonConfigurationsIA;
                    if (e.MenuItem.Value == "rmiClassification")
                    { _singleEntityName = Common.ConstantsEntitiesName.IA.ProjectClassification; }
                    break;
            }
            #endregion

            //_pageTitleJT = _singleEntityName + " - " + Resources.Common.RightJobTitle;
            //_pageTitlePost = _singleEntityName + " - " + Resources.Common.RightPerson;

            String _pageTitle = String.Empty;
            String _pkValuesContext = String.Empty;
            Boolean _isTreeContextElement = false;
            //Se limpia el valor del filtro.
            FilterExpressionGrid = String.Empty;

            String _menuSection = "ContextElementMapsNavigation" + e.MenuItem.Value + "_" + e.Node.Text.Trim().Replace(" ", "_");

            NavigateMenuEventArgs _menuArgs =null;
            String _nameTreeView = ((RadTreeView)sender).ID;
            //Esto es para identificar si viene un click desde el menu de contexto o menu general.
            if ((_nameTreeView.Contains("MenuContextInformation")) || (_nameTreeView.Contains("ElementMaps")))
            {//Si viene de algun menu de contexto, pasa el ContextInfoMenu
                _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.ContextInfoMenu, _menuSection);
            }
            else
            {//Sino, pasa como GlobalMenu
                _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.GlobalMenu, _menuSection);
            }

            String _entityClassName = String.Empty;
            String _titleDecorator = String.Empty;
            var _argsColl = new Dictionary<String, String>();

            switch (e.MenuItem.Value)
            {
                case "rmiAdd":
                    //Debe navegar al property para add
                    //Pasa el nombre de la entidad que debe mostrar
                    //Primero saca las PK Entity que vienen como atributos generales
                    BuildNavigateParamsFromSelectedValue(e.Node.Value);
                    _pageTitle = ((RadTreeView)sender).Attributes["PageTitle"];
                    _pkValuesContext = _pkValuesContext = "PageTitle=" + _pageTitle + "&";
                    _pkValuesContext += GetPKEntityFromAtributes(((RadTreeView)sender).Attributes);
                    //Segundo saca las PK Entity que vienen como atributos en cada nodo.
                    _pkValuesContext += GetPKEntityFromAtributes(e.Node.Attributes);
                    //Agrego los key que vienen el el Value al PkCompost...
                    _pkValuesContext += BuildStringParamsFromValues(e.Node.Value) + "&";
                    //Le saco el ultimo &.
                    if (!String.IsNullOrEmpty(_pkValuesContext))
                    { _pkValuesContext = _pkValuesContext.Substring(0, _pkValuesContext.Length - 1); }
                    //Si hay PK entonces las agrego.
                    if (!String.IsNullOrEmpty(_pkValuesContext))
                    { NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValuesContext); }

                    //Si el Add viene del ContextElement, entonces debe mirar el EntityNameByCE y el EntityNameGridByCE
                    try { _isTreeContextElement = Convert.ToBoolean(((RadTreeView)sender).Attributes["IsTreeContextElement"]); }
                    catch { } //Si no existe este atributo en el TreeView, no hace nada... es un tree normal.

                    if (_isTreeContextElement)
                    {//Si el tree es del ContextElementMap, entonces lee otro atributo para el EntityName y para el EntityNameGrid.
                        _singleEntityName = e.Node.Attributes["EntityNameByCE"];
                        NavigatorAddTransferVar("EntityNameGrid", e.Node.Attributes["EntityNameGridByCE"]);
                    }
                    else
                    {
                        _singleEntityName = e.Node.Attributes["EntityName"];
                        NavigatorAddTransferVar("EntityNameGrid", e.Node.Attributes["EntityNameGrid"]);
                    }
                    NavigatorAddTransferVar("EntityName", _singleEntityName);
                    NavigatorAddTransferVar("EntityNameContextInfo", e.Node.Attributes["EntityNameContextInfo"]);
                    NavigatorAddTransferVar("EntityNameContextElement", e.Node.Attributes["EntityNameContextElement"]);
                    _urlProperties = GetGlobalResourceObject("URLEntityProperties", _singleEntityName).ToString();

                    _entityClassName = GetValueFromGlobalResource("CommonListManage", _singleEntityName);
                    _titleDecorator = String.Concat(_pageTitle, " [", _entityClassName, "] [", Resources.Common.mnuAdd, "]");
                    Navigate(_urlProperties, _titleDecorator, _menuArgs);

                    break;
                case "rmItemAddNode":
                    ////Debe navegar al property para add de Process Group Node
                    ////Pasa el nombre de la entidad que debe mostrar
                    ////Primero saca las PK Entity que vienen como atributos generales
                    NavigatorAddPkEntityIdTransferVar("PkCompost", BuildStringParamsFromValues(e.Node.Value));
                    NavigatorAddTransferVar("IsParent", true);

                    NavigatorAddTransferVar("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessGroupNodes);
                    NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessGroupNode);
                    NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessGroupNode);
                    NavigatorAddTransferVar("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                    _urlProperties = GetGlobalResourceObject("URLEntityProperties", Common.ConstantsEntitiesName.PF.ProcessGroupNode).ToString();

                    _entityClassName = GetValueFromGlobalResource("CommonListManage", Common.ConstantsEntitiesName.PF.ProcessGroupNode);
                    _titleDecorator = String.Concat(_entityClassName, " [", Resources.Common.mnuAdd, "]");
                    Navigate(_urlProperties, _titleDecorator, _menuArgs);
                    break;
                case "rmItemAddTaskCalibration":
                    //Debe navegar al property para add de Process Task Calibration
                    //Pasa el nombre de la entidad que debe mostrar
                    //Primero saca las PK Entity que vienen como atributos generales
                    BuildNavigateParamsFromSelectedValue(e.Node.Value);
                    _pageTitle = ((RadTreeView)sender).Attributes["PageTitle"];
                    _pkValuesContext = _pkValuesContext = "PageTitle=" + _pageTitle + "&";
                    _pkValuesContext += GetPKEntityFromAtributes(((RadTreeView)sender).Attributes);
                    //Segundo saca las PK Entity que vienen como atributos en cada nodo.
                    _pkValuesContext += GetPKEntityFromAtributes(e.Node.Attributes);
                    //Agrego los key que vienen el el Value al PkCompost...
                    _pkValuesContext += BuildStringParamsFromValues(e.Node.Value) + "&";
                    //Le saco el ultimo &.
                    if (!String.IsNullOrEmpty(_pkValuesContext))
                    { _pkValuesContext = _pkValuesContext.Substring(0, _pkValuesContext.Length - 1); }
                    //Si hay PK entonces las agrego.
                    if (!String.IsNullOrEmpty(_pkValuesContext))
                    { NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValuesContext); }

                    NavigatorAddTransferVar("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessTaskCalibrations);
                    NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskCalibration);
                    NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskCalibration);
                    NavigatorAddTransferVar("EntityNameContextElement", Common.ConstantsEntitiesName.PF.Process);
                    _urlProperties = GetGlobalResourceObject("URLEntityProperties", Common.ConstantsEntitiesName.PF.ProcessTaskCalibration).ToString();
                    _entityClassName = GetValueFromGlobalResource("CommonListManage", Common.ConstantsEntitiesName.PF.ProcessTaskCalibration);
                    _titleDecorator = String.Concat(_pageTitle, " [", _entityClassName, "] [", Resources.Common.mnuAdd, "]");
                    Navigate(_urlProperties, _titleDecorator, _menuArgs);
                    break;
                case "rmItemAddTaskMeasurement":
                    //Debe navegar al property para add de Process Task Measurement
                    //Pasa el nombre de la entidad que debe mostrar
                    //Primero saca las PK Entity que vienen como atributos generales
                    BuildNavigateParamsFromSelectedValue(e.Node.Value);
                    _pageTitle = ((RadTreeView)sender).Attributes["PageTitle"];
                    _pkValuesContext = _pkValuesContext = "PageTitle=" + _pageTitle + "&";
                    _pkValuesContext += GetPKEntityFromAtributes(((RadTreeView)sender).Attributes);
                    //Segundo saca las PK Entity que vienen como atributos en cada nodo.
                    _pkValuesContext += GetPKEntityFromAtributes(e.Node.Attributes);
                    //Agrego los key que vienen el el Value al PkCompost...
                    _pkValuesContext += BuildStringParamsFromValues(e.Node.Value) + "&";
                    //Le saco el ultimo &.
                    if (!String.IsNullOrEmpty(_pkValuesContext))
                    { _pkValuesContext = _pkValuesContext.Substring(0, _pkValuesContext.Length - 1); }
                    //Si hay PK entonces las agrego.
                    if (!String.IsNullOrEmpty(_pkValuesContext))
                    { NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValuesContext); }

                    NavigatorAddTransferVar("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessTaskMeasurements);
                    NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement);
                    NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement);
                    NavigatorAddTransferVar("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                    _urlProperties = GetGlobalResourceObject("URLEntityProperties", Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement).ToString();
                    _entityClassName = GetValueFromGlobalResource("CommonListManage", Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement);
                    //Si no hay pagetitle, entonces usa el texto del nodo root.
                    _pageTitle = String.IsNullOrEmpty(_pageTitle) ? ((RadTreeView)sender).Nodes[0].Text : _pageTitle;
                    _titleDecorator = String.Concat(_pageTitle," [", _entityClassName, "] [", Resources.Common.mnuAdd, "]");
                    Navigate(_urlProperties, _titleDecorator, _menuArgs);
                    break;
                case "rmItemAddTaskOperation":
                    //Debe navegar al property para add de Process Task Measurement
                    //Pasa el nombre de la entidad que debe mostrar
                    //Primero saca las PK Entity que vienen como atributos generales
                    BuildNavigateParamsFromSelectedValue(e.Node.Value);
                    _pageTitle = ((RadTreeView)sender).Attributes["PageTitle"];
                    _pkValuesContext = _pkValuesContext = "PageTitle=" + _pageTitle + "&";
                    _pkValuesContext += GetPKEntityFromAtributes(((RadTreeView)sender).Attributes);
                    //Segundo saca las PK Entity que vienen como atributos en cada nodo.
                    _pkValuesContext += GetPKEntityFromAtributes(e.Node.Attributes);
                    //Agrego los key que vienen el el Value al PkCompost...
                    _pkValuesContext += BuildStringParamsFromValues(e.Node.Value) + "&";
                    //Le saco el ultimo &.
                    if (!String.IsNullOrEmpty(_pkValuesContext))
                    { _pkValuesContext = _pkValuesContext.Substring(0, _pkValuesContext.Length - 1); }
                    //Si hay PK entonces las agrego.
                    if (!String.IsNullOrEmpty(_pkValuesContext))
                    { NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValuesContext); }

                    NavigatorAddTransferVar("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessTaskOperations);
                    NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskOperation);
                    NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskOperation);
                    NavigatorAddTransferVar("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                    _urlProperties = GetGlobalResourceObject("URLEntityProperties", Common.ConstantsEntitiesName.PF.ProcessTaskOperation).ToString();
                    _entityClassName = GetValueFromGlobalResource("CommonListManage", Common.ConstantsEntitiesName.PF.ProcessTaskOperation);
                    _titleDecorator = String.Concat(_pageTitle, " [", _entityClassName, "] [", Resources.Common.mnuAdd, "]");
                    Navigate(_urlProperties, _titleDecorator, _menuArgs);
                    break;
                case "rmiElement":
                case "rmiClassification":
                    //Debe navegar al property para add
                    //Pasa el nombre de la entidad que debe mostrar
                    //Debe navegar al property para edit
                    //Obtiene y carga los parametros desde el value del nodo
                    //BuildNavigateParamsFromSelectedValue(e.Node.Value);
                    NavigatorAddPkEntityIdTransferVar("PkCompost", BuildStringParamsFromValues(e.Node.Value));
                    NavigatorAddTransferVar("IsParent", true);
                    NavigatorAddTransferVar("EntityName", _singleEntityName);
                    NavigatorAddTransferVar("EntityNameContextInfo", e.Node.Attributes["EntityNameContextInfo"]);
                    NavigatorAddTransferVar("EntityNameContextElement", e.Node.Attributes["EntityNameContextElement"]);

                    _urlProperties = GetGlobalResourceObject("URLEntityProperties", _singleEntityName).ToString();
                    _entityClassName = GetValueFromGlobalResource("CommonListManage", _singleEntityName);
                    _titleDecorator = String.Concat(_entityClassName, " [", Resources.Common.mnuAdd, "]");
                    Navigate(_urlProperties, _titleDecorator, _menuArgs);
                    break;
                case "rmiEdit":
                    //Solo debemos persistir el treeview de los modulos...
                    if (((RadTreeView)sender).ID.Contains("rtvMenuModule"))
                    {
                        _TreeViewGlobalMenuXML = ((RadTreeView)sender).GetXml();
                    }
                    //Debe navegar al property para edit
                    //Obtiene y carga los parametros desde el value del nodo
                    BuildNavigateParamsFromSelectedValue(e.Node.Value);
                    NavigatorAddPkEntityIdTransferVar("PkCompost", BuildStringParamsFromValues(e.Node.Value));

                    //Pasa el nombre de la entidad que debe mostrar
                    _singleEntityName = e.Node.Attributes["SingleEntityName"].ToString();
                    //Si viene el nombre ProcessTask le meto el nombre especifico de la tarea.
                    if (_singleEntityName == Common.ConstantsEntitiesName.PF.ProcessTask)
                    {
                        _singleEntityName = e.Node.Attributes["EntityName"].ToString();
                    }
                    NavigatorAddTransferVar("EntityName", _singleEntityName);
                    NavigatorAddTransferVar("EntityNameContextInfo", e.Node.Attributes["EntityNameContextInfo"]);
                    NavigatorAddTransferVar("EntityNameContextElement", e.Node.Attributes["EntityNameContextElement"]);

                    _urlProperties = GetGlobalResourceObject("URLEntityProperties", _singleEntityName).ToString();
                    _entityClassName = GetValueFromGlobalResource("CommonListManage", _singleEntityName);
                    _titleDecorator = String.Concat(e.Node.Text, " [", _entityClassName, "]", " [", Resources.Common.mnuEdit, "]");
                    Navigate(_urlProperties, _titleDecorator, _menuArgs);
                    break;
                case "rmiEditSites":
                    //Debe navegar al property para edit
                    //Obtiene y carga los parametros desde el value del nodo
                    BuildNavigateParamsFromSelectedValue(e.Node.Value);
                    NavigatorAddPkEntityIdTransferVar("PkCompost", BuildStringParamsFromValues(e.Node.Value));

                    //Pasa el nombre de la entidad que debe mostrar
                    _singleEntityName = e.Node.Attributes["EntityName"].ToString();
                    
                    NavigatorAddTransferVar("EntityName", _singleEntityName);
                    NavigatorAddTransferVar("EntityNameContextInfo", e.Node.Attributes["EntityNameContextInfo"]);
                    NavigatorAddTransferVar("EntityNameContextElement", e.Node.Attributes["EntityNameContextElement"]);

                    _urlProperties = GetGlobalResourceObject("URLEntityProperties", _singleEntityName).ToString();
                    _entityClassName = GetValueFromGlobalResource("CommonListManage", _singleEntityName);
                    _titleDecorator = String.Concat(e.Node.Text, " [", _entityClassName, "]", " [", Resources.Common.mnuEdit, "]");
                    Navigate(_urlProperties, _titleDecorator, _menuArgs);
                    break;
                case "rmItemJobTitle":
                    if (e.Node.Value == "NodeRootTitle")
                    {//Esto es seguridad para Mapas
                        NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                        NavigatorAddTransferVar("PageTitle", _pageTitleJT);
                        NavigatorAddTransferVar("EntityNameGrid", _entityNameGridJT);
                        NavigatorAddTransferVar("EntityName", _entityNameJT);
                        Navigate("~/Managers/ListManageAndView.aspx", GetValueFromGlobalResource("CommonListManage", _entityNameJT), _menuArgs);
                    }
                    else
                    {//Seguridad para cada entidad..
                        //Obtiene y carga los parametros desde el value del nodo
                        _singleEntityName = e.Node.Attributes["SingleEntityName"].ToString();
                        BuildNavigateParamsFromSelectedValue(e.Node.Value);

                        _pkValues = BuildStringParamsFromValues(e.Node.Value);
                        _pkValues += "&ParentEntity=" + _singleEntityName;

                        NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                        SecuritySystemJobTitlesNavigate(_singleEntityName, e.Node.Text);
                    }
                    break;
                case "rmItemPerson":
                    if (e.Node.Value == "NodeRootTitle")
                    {//Esto es seguridad para Mapas
                        NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                        NavigatorAddTransferVar("PageTitle", _pageTitlePost);
                        NavigatorAddTransferVar("EntityNameGrid", _entityNameGridPost);
                        NavigatorAddTransferVar("EntityName", _entityNamePost);
                        //Navigate("~/Managers/ListManageAndView.aspx", GetValueFromGlobalResource("CommonListManage", _entityNameGridPost), _menuArgs);

                        _argsColl = new Dictionary<String, String>();
                        _argsColl.Add("EntityName", _entityNamePost);
                        _argsColl.Add("EntityNameGrid", _entityNameGridPost);

                        _menuArgs = new NavigateMenuEventArgs(NavigateMenuType.GlobalMenu, _argsColl);
                        Navigate("~/Managers/ListManageAndView.aspx", e.Node.Text + "[" + GetValueFromGlobalResource("CommonListManage", _entityNamePost) + "]", _menuArgs);
                    }
                    else
                    {//Seguridad para cada entidad..
                        //Obtiene y carga los parametros desde el value del nodo
                        _singleEntityName = e.Node.Attributes["SingleEntityName"].ToString();
                        BuildNavigateParamsFromSelectedValue(e.Node.Value);

                        _pkValues = BuildStringParamsFromValues(e.Node.Value);
                        _pkValues += "&ParentEntity=" + _singleEntityName;

                        NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                        SecuritySystemPersonNavigate(_singleEntityName, e.Node.Text);
                    }
                    break;


                case "rmItemJobTitleConfig":
                    //Seguridad Config.
                    NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValuesConfig);
                    NavigatorAddTransferVar("PageTitle", _pageTitleJTConfig);
                    NavigatorAddTransferVar("EntityNameGrid", _entityNameGridJTConfig);
                    NavigatorAddTransferVar("EntityName", _entityNameJTConfig);
                    //Navigate("~/Managers/ListManageAndView.aspx", GetValueFromGlobalResource("CommonListManage", _entityNameGridJTConfig), _menuArgs);

                    _argsColl = new Dictionary<String, String>();
                    _argsColl.Add("EntityName", _entityNameJTConfig);
                    _argsColl.Add("EntityNameGrid", _entityNameGridJTConfig);

                    _menuArgs = new NavigateMenuEventArgs(NavigateMenuType.GlobalMenu, _argsColl);
                    Navigate("~/Managers/ListManageAndView.aspx", GetValueFromGlobalResource("CommonListManage", _entityNameJTConfig), _menuArgs);

                    break;
                case "rmItemPersonConfig":
                    //Seguridad Config.
                    NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValuesConfig);
                    NavigatorAddTransferVar("PageTitle", _pageTitlePostConfig);
                    NavigatorAddTransferVar("EntityNameGrid", _entityNameGridPostConfig);
                    NavigatorAddTransferVar("EntityName", _entityNamePostConfig);
                    //Navigate("~/Managers/ListManageAndView.aspx", GetValueFromGlobalResource("CommonListManage", _entityNameGridPostConfig), _menuArgs);
                    _argsColl = new Dictionary<String, String>();
                    _argsColl.Add("EntityName", _entityNamePostConfig);
                    _argsColl.Add("EntityNameGrid", _entityNameGridPostConfig);

                    _menuArgs = new NavigateMenuEventArgs(NavigateMenuType.GlobalMenu, _argsColl);
                    Navigate("~/Managers/ListManageAndView.aspx", GetValueFromGlobalResource("CommonListManage", _entityNamePostConfig), _menuArgs);
                    break;
                    
                case "rmItemAddAuditPlan":
                    //Debe navegar al property para add de Process Task Measurement
                    //Pasa el nombre de la entidad que debe mostrar
                    //Primero saca las PK Entity que vienen como atributos generales
                    BuildNavigateParamsFromSelectedValue(e.Node.Value);
                    _pageTitle = ((RadTreeView)sender).Attributes["PageTitle"];
                    _pkValuesContext = _pkValuesContext = "PageTitle=" + _pageTitle + "&";
                    _pkValuesContext += GetPKEntityFromAtributes(((RadTreeView)sender).Attributes);
                    //Segundo saca las PK Entity que vienen como atributos en cada nodo.
                    _pkValuesContext += GetPKEntityFromAtributes(e.Node.Attributes);
                    //Agrego los key que vienen el el Value al PkCompost...
                    _pkValuesContext += BuildStringParamsFromValues(e.Node.Value) + "&";
                    //Le saco el ultimo &.
                    if (!String.IsNullOrEmpty(_pkValuesContext))
                    { _pkValuesContext = _pkValuesContext.Substring(0, _pkValuesContext.Length - 1); }
                    //Si hay PK entonces las agrego.
                    if (!String.IsNullOrEmpty(_pkValuesContext))
                    { NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValuesContext); }

                    NavigatorAddTransferVar("EntityNameGrid", String.Empty);
                    NavigatorAddTransferVar("EntityName", String.Empty);
                    NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);
                    NavigatorAddTransferVar("EntityNameContextElement", String.Empty);
                    _titleDecorator = String.Concat(_pageTitle, " [", Resources.Common.AuditPlan, "] [", Resources.Common.mnuAdd, "]");
                    Navigate("~/AdministrationTools/ProcessesFramework/ShowUploadAuditPlanProperties.aspx", _titleDecorator, _menuArgs);
                    break;
            }

        }
        private void SecuritySystemJobTitlesNavigate(String singleEntityName, String titleProperty)
        {
            String _entityNameGrid = String.Empty;
            String _entityName = String.Empty;
            String _pageTitle = singleEntityName + " - Right JobTitle";
            String _navigateEntity = String.Empty;

            switch (singleEntityName)
            {
                case Common.ConstantsEntitiesName.DS.Organization:
                    _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleOrganizations;
                    _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleOrganization;
                    _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleOrganizations;
                    break;
                case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    _entityNameGrid = Common.ConstantsEntitiesName.SS.RightJobTitleProcesses;
                    _entityName = Common.ConstantsEntitiesName.SS.RightJobTitleProcess;
                    _navigateEntity = Common.ConstantsEntitiesName.SS.RightJobTitleProcesses;
                    break;
            }

            NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
            NavigatorAddTransferVar("EntityName", _entityName);
            NavigatorAddTransferVar("PageTitle", _pageTitle);

            //NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.GlobalMenu, "ContextElementMenuMapNavigation");
            //Navigate("~/Managers/ListManageAndView.aspx", GetValueFromGlobalResource("CommonListManage", _entityNameGrid), _menuArgs);

            //Finalmente hace el Navigate al Manage Correspondiente.
            var argsColl = new Dictionary<String, String>();
            argsColl.Add("EntityName", _entityName);
            argsColl.Add("EntityNameGrid", _entityNameGrid);

            NavigateMenuEventArgs _menuArgs = new NavigateMenuEventArgs(NavigateMenuType.GlobalMenu, argsColl);
            Navigate("~/Managers/ListManageAndView.aspx", titleProperty + "[" + GetValueFromGlobalResource("CommonListManage", _entityNameGrid) + "]", _menuArgs);
        }
        private void SecuritySystemPersonNavigate(String singleEntityName, String titleProperty)
        {
            String _entityNameGrid = String.Empty;
            String _entityName = String.Empty;
            String _pageTitle = singleEntityName + " - Right Post";
            String _navigateEntity = String.Empty;

            switch (singleEntityName)
            {
                case Common.ConstantsEntitiesName.DS.Organization:
                    _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonOrganizations;
                    _entityName = Common.ConstantsEntitiesName.SS.RightPersonOrganization;
                    _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonOrganizations;
                    break;
                case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                    _entityNameGrid = Common.ConstantsEntitiesName.SS.RightPersonProcesses;
                    _entityName = Common.ConstantsEntitiesName.SS.RightPersonProcess;
                    _navigateEntity = Common.ConstantsEntitiesName.SS.RightPersonProcesses;
                    break;
            }

            NavigatorAddTransferVar("EntityNameGrid", _entityNameGrid);
            NavigatorAddTransferVar("EntityName", _entityName);
            NavigatorAddTransferVar("PageTitle", _pageTitle);


            //Finalmente hace el Navigate al Manage Correspondiente.
            var argsColl = new Dictionary<String, String>();
            argsColl.Add("EntityName", _entityName);
            argsColl.Add("EntityNameGrid", _entityNameGrid);

            NavigateMenuEventArgs _menuArgs = new NavigateMenuEventArgs(NavigateMenuType.GlobalMenu, argsColl);
            //NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.GlobalMenu, "ContextElementMenuMapNavigation");
            Navigate("~/Managers/ListManageAndView.aspx", titleProperty + "[" + GetValueFromGlobalResource("CommonListManage", _entityNameGrid) + "]", _menuArgs);
        }
    }
}
