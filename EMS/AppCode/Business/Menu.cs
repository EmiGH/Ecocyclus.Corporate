using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Telerik.Web.UI;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Condesus.EMS.WebUI.Business
{
    public class Menu : Base
    {
        #region Internal Properties
            String _EntityNameMapClassification = "IndicatorClassifications";
            String _EntityNameMapClassificationChildren = "IndicatorClassificationsChildren";
            String _EntityNameMapElement = "IndicatorsRoots";
            String _EntityNameMapElementChildren = "Indicators";
            String _SingleEntityClassificationName = "IndicatorClassification";
            String _SingleEntityElementName = "Indicator";
            String _ContextInfoEntityClassificationName = String.Empty;
            String _ContextInfoEntityName = String.Empty;
            String _ContextElementMapEntityName = String.Empty;
        #endregion

        #region Generic Public Methods
            public Menu(String commandName)
            {
                _CommandName = commandName;
            }
            public Menu Create(String commandName)
            {
                return new Menu(commandName);
            }
        #endregion

        #region Private Node Building Methods
            private RadTreeNode AddNode(String title, String toolTip, String value, String url, Boolean autoPostBack)
            {
                if (url == "") { return AddNode(title, toolTip, value, autoPostBack); }

                RadTreeNode _Node = new RadTreeNode(title, value);
                _Node.Attributes.Add("URL", url);
                _Node.ToolTip = toolTip;
                _Node.PostBack = autoPostBack;
                return _Node;

            }
            private RadTreeNode AddNode(String title, String toolTip, String value, Boolean autoPostBack)
            {
                RadTreeNode _Node = new RadTreeNode(title, value);
                _Node.ToolTip = toolTip;
                _Node.PostBack = autoPostBack;
                return _Node;
            }
            private RadTreeNode AddNode(String title, String value, String url, Boolean autoPostBack,
                String entityNameGrid, String entityName, String entityNameContextInfo, String entityNameContextElement, String entityNameHierarchical, String entityNameHierarchicalChildren,
                String entityNameComboFilter, Boolean isFilterHierarchy, String entityNameChildrenComboFilter,
                String entityNameMapClassification, String entityNameMapClassificationChildren, String entityNameMapElement,
                String entityNameMapElementChildren, String selectedValueDefaultComboBox,
                String imageUrl, String expandedImageUrl, String selectedImageUrl)
            {
                RadTreeNode _Node;
                if (url != null)
                    _Node = AddNode(title, title, value, url, autoPostBack);
                else
                    _Node = AddNode(title, title, value, autoPostBack);

                //Setea los parametros para las manage
                _Node.Attributes.Add("EntityNameGrid", entityNameGrid);
                _Node.Attributes.Add("EntityName", entityName);
                _Node.Attributes.Add("EntityNameHierarchical", entityNameHierarchical);
                _Node.Attributes.Add("EntityNameHierarchicalChildren", entityNameHierarchicalChildren);

                _Node.Attributes.Add("EntityNameContextInfo", entityNameContextInfo);
                _Node.Attributes.Add("EntityNameContextElement", entityNameContextElement);
                
                _Node.Attributes.Add("EntityNameComboFilter", entityNameComboFilter);
                _Node.Attributes.Add("SelectedValueDefaultComboBox", selectedValueDefaultComboBox);

                _Node.Attributes.Add("IsFilterHierarchy", isFilterHierarchy.ToString());
                _Node.Attributes.Add("EntityNameChildrenComboFilter", entityNameChildrenComboFilter);
                _Node.Attributes.Add("EntityNameMapClassification", entityNameMapClassification);
                _Node.Attributes.Add("EntityNameMapClassificationChildren", entityNameMapClassificationChildren);
                _Node.Attributes.Add("EntityNameMapElement", entityNameMapElement);
                _Node.Attributes.Add("EntityNameMapElementChildren", entityNameMapElementChildren);
                _Node.Value = "nodeConfig";

                SetNodeStyle(_Node, imageUrl, expandedImageUrl, selectedImageUrl);

                return _Node;
            }
            private RadTreeNode AddNode(String title, String toolTip, String value, String url, Boolean autoPostBack, String imageUrl, String expandedImageUrl, String selectedImageUrl)
            {
                RadTreeNode _Node;
                if (url != null)
                    _Node = AddNode(title, value, url, autoPostBack);
                else
                    _Node = AddNode(title, toolTip, value, autoPostBack);

                SetNodeStyle(_Node, imageUrl, expandedImageUrl, selectedImageUrl);

                return _Node;
            }
            private void SetNodeStyle(RadTreeNode node, string CssClas, string HoveredCssClass, string SelectedCssClass)
            {
                node.CssClass = CssClas;
                node.HoveredCssClass = HoveredCssClass;
                node.SelectedCssClass = SelectedCssClass;
            }
            //private void SetNodeStyle(RadTreeNode node, String imageUrl, String expandedImageUrl, String selectedImageUrl)
            //{
            //    node.ImageUrl = (imageUrl != String.Empty) ? Common.Functions.GetAbsolutePathAppThemes + "/Images/Icons/Module/" + imageUrl : String.Empty;
            //    node.ExpandedImageUrl = (expandedImageUrl != String.Empty) ? Common.Functions.GetAbsolutePathAppThemes + "/Images/Icons/Module/" + expandedImageUrl : String.Empty;
            //    node.SelectedImageUrl = (selectedImageUrl != String.Empty) ? Common.Functions.GetAbsolutePathAppThemes + "/Images/Icons/Module/" + selectedImageUrl : String.Empty;
            //}
            private RadTreeView BuildTVMenuModule(String entityId)
            {
                RadTreeView _rtvMenuModule = new RadTreeView();
                _rtvMenuModule.ID = "rtvMenuModule" + entityId;
                _rtvMenuModule.CheckBoxes = false;
                _rtvMenuModule.EnableViewState = false;
                _rtvMenuModule.PersistLoadOnDemandNodes = false;
                _rtvMenuModule.AllowNodeEditing = false;
                _rtvMenuModule.ShowLineImages = true;
                _rtvMenuModule.Skin = "EMS";
                _rtvMenuModule.EnableEmbeddedSkins = false;
                _rtvMenuModule.CausesValidation = false;

                _rtvMenuModule.OnClientContextMenuShowing = "onClientContextMenuConfigShowing";
                //Agrega el ContextMenu al treeview.
                _rtvMenuModule.ContextMenus.Add(BuildContextMenuConfigShortCut());

                return _rtvMenuModule;
            }

            /// <summary>
            /// Contruye y carga el Tree para los ElementMaps.
            /// </summary>
            /// <returns>Un<c>RadTreeView</c> con el mapa de elementos armado.</returns>
            //private RadTreeView BuildMenuElementMap(Dictionary<String, Object> param, String permissionType)
            private RadTreeView BuildMenuElementMap(Dictionary<String, Object> param, String permissionType)
            {
                //Esto es para hacer combo con tree de ElementMap....
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                BuildGenericDataTable(_EntityNameMapClassification, param);
                BuildGenericDataTable(_EntityNameMapElement, param);

                //RadTreeView _rtvMenuElementMaps = BuildElementMapsContent(_EntityNameMapClassification);
                RadTreeView _rtvMenuElementMaps = BuildElementMapsContent(_EntityNameMapClassification);
                //Al usar el XML, esto no es necesario!!!...
                _rtvMenuElementMaps.PersistLoadOnDemandNodes = false;

                _rtvMenuElementMaps.OnClientContextMenuShowing = "onClientContextMenuShowingMenuElementMap";
                //Agrega el ContextMenu al treeview.
                _rtvMenuElementMaps.ContextMenus.Add(BuildContextMenuElementMapShortCut());
                _rtvMenuElementMaps.EnableDragAndDrop = true;
                _rtvMenuElementMaps.EnableDragAndDropBetweenNodes = true;
                _rtvMenuElementMaps.MultipleSelect = false;

                _rtvMenuElementMaps.NodeDrop += new RadTreeViewDragDropEventHandler(rtvMenuElementMaps_NodeDrop);
                _rtvMenuElementMaps.OnClientNodeDropping = "onNodeDroppingElementMap";  //Para que se cancele el Drag&Drop si se selecciona un Elemento.
                _rtvMenuElementMaps.OnClientNodeDragging = "onNodeDragging";  //Para que se cancele el Drag&Drop si se selecciona un Elemento.
                _rtvMenuElementMaps.OnClientNodeDragStart = "ClientNodeDragStart";
                //Estos 2 eventos son para limpiar el estilo del mouse.
                _rtvMenuElementMaps.OnClientNodeDropped = "dragDropChangeCursor";
                _rtvMenuElementMaps.OnClientMouseOver = "dragDropChangeCursor";
                
                //Carga los registros en el Tree
                //base.LoadGenericTreeViewElementMap(ref _rtvMenuElementMaps, _EntityNameMapClassification, _EntityNameMapElement, _SingleEntityClassificationName, _SingleEntityElementName, _ContextInfoEntityClassificationName, _ContextInfoEntityName, _ContextElementMapEntityName);
                String _nodeRootTitle = Convert.ToString(param["NodeRootTitle"]);
                RadTreeNode _nodeRoot = new RadTreeNode(_nodeRootTitle);
                _nodeRoot.Value = "NodeRootTitle";
                _nodeRoot.PostBack = false;
                _nodeRoot.Expanded = true;
                _nodeRoot.CssClass = "Folder";
                _nodeRoot.Attributes.Add("PermissionType", permissionType);
                //_nodeRoot.Attributes.Add("MapName", mapName);
                _rtvMenuElementMaps.Nodes.Add(_nodeRoot);

                base.LoadGenericTreeViewElementMap(ref _nodeRoot, _EntityNameMapClassification, _EntityNameMapElement, _SingleEntityClassificationName, _SingleEntityElementName, _ContextInfoEntityClassificationName, _ContextInfoEntityName, _ContextElementMapEntityName, false);
                //Asocia el Handler del Expand y click
                _rtvMenuElementMaps.NodeExpand += new RadTreeViewEventHandler(rtvElementMaps_NodeExpand);

                return _rtvMenuElementMaps;
            }

            /// <summary>
            /// Este metodo construye el Menu contextual de accesos directo para agregarselo a los TReeView.
            /// </summary>
            /// <returns>Un<c>RadTreeViewContextMenu</c></returns>
            private RadTreeViewContextMenu BuildContextMenuElementMapShortCut()
            {
                RadTreeViewContextMenu _rtvContextMenu = new RadTreeViewContextMenu();
                _rtvContextMenu.ID = "rtvContextMenu" + _EntityNameMapClassification;
                _rtvContextMenu.EnableEmbeddedSkins = false;
                _rtvContextMenu.Skin = "EMS";

                //Crea los items del menu contextual
                RadMenuItem _rmItemAdd = new RadMenuItem(Resources.Common.mnuAdd);
                _rmItemAdd.Value = "rmiAdd";
                _rmItemAdd.PostBack = false;
                //Estos estaran dentro del ADD
                RadMenuItem _rmItemAddClass = new RadMenuItem(Resources.Common.mnuClassification);
                _rmItemAddClass.Value = "rmiClassification";
                RadMenuItem _rmItemAddElement = new RadMenuItem(Resources.Common.mnuElement);
                _rmItemAddElement.Value = "rmiElement";
                //Aca los agrega adentro.
                _rmItemAdd.Items.Add(_rmItemAddClass);
                _rmItemAdd.Items.Add(_rmItemAddElement);

                //Crea el edit va solo en el root.
                RadMenuItem _rmItemEdit = new RadMenuItem(Resources.Common.mnuEdit);
                _rmItemEdit.Value = "rmiEdit";
                //Crea security y tiene hijos
                RadMenuItem _rmItemSecurity = new RadMenuItem(Resources.Common.mnuSecurity);
                _rmItemSecurity.Value = "rmiSecurity";
                _rmItemSecurity.PostBack = false;
                //Aca crea los 2 hijos de security
                RadMenuItem _rmItemJobTitle = new RadMenuItem(Resources.Common.mnuSSJobTitle);
                _rmItemJobTitle.Value = "rmItemJobTitle";
                RadMenuItem _rmItemPerson = new RadMenuItem(Resources.Common.mnuSSPerson);
                _rmItemPerson.Value = "rmItemPerson"; //"rmItemPost";
                //Aca se los agrega.
                _rmItemSecurity.Items.Add(_rmItemJobTitle);
                _rmItemSecurity.Items.Add(_rmItemPerson);

                //Agrega los items root del menu
                _rtvContextMenu.Items.Add(_rmItemAdd);
                _rtvContextMenu.Items.Add(_rmItemEdit);
                _rtvContextMenu.Items.Add(_rmItemSecurity);

                return _rtvContextMenu;
            }
            /// <summary>
            /// Este metodo construye el Menu contextual de accesos directo para agregarselo a los TReeView.
            /// </summary>
            /// <returns>Un<c>RadTreeViewContextMenu</c></returns>
            private RadTreeViewContextMenu BuildContextMenuConfigShortCut()
            {
                RadTreeViewContextMenu _rtvContextMenu = new RadTreeViewContextMenu();
                _rtvContextMenu.ID = "rtvContextMenu" + _EntityNameMapClassification;
                _rtvContextMenu.EnableEmbeddedSkins = false;
                _rtvContextMenu.Skin = "EMS";

                //Crea los items del menu contextual
                RadMenuItem _rmItemAdd = new RadMenuItem(Resources.Common.mnuAdd);
                _rmItemAdd.Value = "rmiAdd";
                //Crea security y tiene hijos
                RadMenuItem _rmItemSecurity = new RadMenuItem(Resources.Common.mnuSecurity);
                _rmItemSecurity.Value = "rmiSecurity";
                _rmItemSecurity.PostBack = false;
                //Aca crea los 2 hijos de security
                RadMenuItem _rmItemJobTitle = new RadMenuItem(Resources.Common.mnuSSJobTitle);
                _rmItemJobTitle.Value = "rmItemJobTitleConfig";
                RadMenuItem _rmItemPerson = new RadMenuItem(Resources.Common.mnuSSPerson);
                _rmItemPerson.Value = "rmItemPersonConfig";   //"rmItemPostConfig";
                //Aca se los agrega.
                _rmItemSecurity.Items.Add(_rmItemJobTitle);
                _rmItemSecurity.Items.Add(_rmItemPerson);

                //Agrega los items root del menu
                _rtvContextMenu.Items.Add(_rmItemAdd);
                _rtvContextMenu.Items.Add(_rmItemSecurity);

                return _rtvContextMenu;
            }
    
        #endregion

        #region Public Methods (Build Menus)
            #region Module Menu Configuration
                public RadTreeView BuildMenuModuleDSAdmin(Dictionary<String, Object> param)
                {
                    RadTreeView _rtvMenuDS = BuildTVMenuModule("DSAdmin");

                    Int32 _pkNode = 0;

                    //Si el usuario tiene permisos de config sobre el mapa, entonces le habilita la seguridad...
                    String _permissionType = String.Empty;
                    //Si el usuario no tiene ningun permiso sobre config, no se arma el TREE.
                    if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.Count > 0)
                    {
                        if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        RadTreeNode _rootAuxiliaryData = AddNode(Resources.CommonMenu.mnuAuxiliaryData_Title, Resources.CommonMenu.mnuAuxiliaryData_ToolTip, (_pkNode++).ToString(), null, false, "Folder", "FolderHover", "FolderSelected");
                        RadTreeNode _childApplicabilityManage = AddNode(Resources.CommonListManage.Applicabilities, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DS.Applicabilities, Common.ConstantsEntitiesName.DS.Applicability, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childSalutationTypesManage = AddNode(Resources.CommonListManage.SalutationTypes, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DS.SalutationTypes, Common.ConstantsEntitiesName.DS.SalutationType, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childContactTypesManage = AddNode(Resources.CommonListManage.ContactTypes, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DS.ContactTypes, Common.ConstantsEntitiesName.DS.ContactType, String.Empty, String.Empty, String.Empty, String.Empty, Common.ConstantsEntitiesName.DS.Applicabilities, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childContactMessengersProviders = AddNode(Resources.CommonListManage.ContactMessengerProviders, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DS.ContactMessengerProviders, Common.ConstantsEntitiesName.DS.ContactMessengerProvider, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childContactMessengersApplication = AddNode(Resources.CommonListManage.ContactMessengerApplications, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DS.ContactMessengerApplications, Common.ConstantsEntitiesName.DS.ContactMessengerApplication, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childFacilityType = AddNode(Resources.CommonListManage.FacilityTypes, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DS.FacilityTypes, Common.ConstantsEntitiesName.DS.FacilityType, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childGeographicAreasManage = AddNode(Resources.CommonListManage.GeographicAreas, (_pkNode++).ToString(), "~/Managers/HierarchicalListManage.aspx", true, String.Empty, Common.ConstantsEntitiesName.DS.GeographicArea, String.Empty, String.Empty, Common.ConstantsEntitiesName.DS.GeographicAreas, Common.ConstantsEntitiesName.DS.GeographicAreaChildren, String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childLanguagesManage = AddNode(Resources.CommonListManage.Languages, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DS.Languages, Common.ConstantsEntitiesName.DS.Language, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        //RadTreeNode _childRelationshipTypeManage = AddNode(Resources.CommonListManage.OrganizationRelationshipTypes, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DS.OrganizationRelationshipTypes, Common.ConstantsEntitiesName.DS.OrganizationRelationshipType, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        //RadTreeNode _childRoleTypeManage = AddNode(Resources.CommonListManage.RoleTypes, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.SS.RoleTypes, Common.ConstantsEntitiesName.SS.RoleType, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childTimeUnitManage = AddNode(Resources.CommonListManage.TimeUnits, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.PF.TimeUnits, Common.ConstantsEntitiesName.PF.TimeUnit, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");

                        //Agrega atributo de permiso sobre el nodo de config.
                        _rootAuxiliaryData.Attributes.Add("PermissionType", _permissionType);
                        _childApplicabilityManage.Attributes.Add("PermissionType", Common.Constants.PermissionViewName); //No puede tener ADD, ya que solo es para el Language
                        _childSalutationTypesManage.Attributes.Add("PermissionType", _permissionType);
                        _childContactTypesManage.Attributes.Add("PermissionType", _permissionType);
                        _childContactMessengersProviders.Attributes.Add("PermissionType", _permissionType);
                        _childContactMessengersApplication.Attributes.Add("PermissionType", _permissionType);
                        _childFacilityType.Attributes.Add("PermissionType", _permissionType);
                        _childGeographicAreasManage.Attributes.Add("PermissionType", _permissionType);
                        _childLanguagesManage.Attributes.Add("PermissionType", _permissionType);
                        //_childRelationshipTypeManage.Attributes.Add("PermissionType", _permissionType);
                        //_childRoleTypeManage.Attributes.Add("PermissionType", _permissionType);
                        _childTimeUnitManage.Attributes.Add("PermissionType", Common.Constants.PermissionViewName); //No puede tener ADD, ya que solo es para el Language

                        _rootAuxiliaryData.Nodes.Add(_childSalutationTypesManage);
                        _rootAuxiliaryData.Nodes.Add(_childApplicabilityManage);
                        _rootAuxiliaryData.Nodes.Add(_childContactTypesManage);
                        _rootAuxiliaryData.Nodes.Add(_childContactMessengersProviders);
                        _rootAuxiliaryData.Nodes.Add(_childContactMessengersApplication);
                        _rootAuxiliaryData.Nodes.Add(_childFacilityType);
                        _rootAuxiliaryData.Nodes.Add(_childGeographicAreasManage);
                        _rootAuxiliaryData.Nodes.Add(_childLanguagesManage);
                        //_rootAuxiliaryData.Nodes.Add(_childRelationshipTypeManage);
                        //_rootAuxiliaryData.Nodes.Add(_childRoleTypeManage);
                        _rootAuxiliaryData.Nodes.Add(_childTimeUnitManage);

                        _rtvMenuDS.Nodes.Add(_rootAuxiliaryData);

                        _rtvMenuDS.ExpandAllNodes();
                    }
                    return _rtvMenuDS;
                }
                public RadTreeView BuildMenuModulePMAdmin(Dictionary<String, Object> param)
                {
                    RadTreeView _rtvMenuPM = BuildTVMenuModule("PMAdmin");
                    Int32 _pkNode = 0;

                    //Si el usuario tiene permisos de config sobre el mapa, entonces le habilita la seguridad...
                    String _permissionType = String.Empty;
                    //Si el usuario no tiene ningun permiso sobre config, no se arma el TREE.
                    if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.Count > 0)
                    {
                        if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        RadTreeNode _rootAuxiliaryDataPM = AddNode(Resources.CommonMenu.mnuAuxiliaryData_Title, Resources.CommonMenu.mnuAuxiliaryData_ToolTip, (_pkNode++).ToString(), null, false, "Folder", "FolderHover", "FolderSelected");
                        RadTreeNode _childExtendedPropertyClassificationsManage = AddNode(Resources.CommonListManage.ExtendedPropertyClassifications, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PF.ExtendedPropertyClassifications, Common.ConstantsEntitiesName.PF.ExtendedPropertyClassification, String.Empty, String.Empty, String.Empty, String.Empty,
                            String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childExtendedPropertiesManage = AddNode(Resources.CommonListManage.ExtendedProperties, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PF.ExtendedProperties, Common.ConstantsEntitiesName.PF.ExtendedProperty, String.Empty, String.Empty, String.Empty, String.Empty,
                            Common.ConstantsEntitiesName.PF.ExtendedPropertyClassifications, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childParticipationTypeManage = AddNode(Resources.CommonListManage.ParticipationTypes, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PF.ParticipationTypes, Common.ConstantsEntitiesName.PF.ParticipationType, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty,
                            String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");

                        //Agrega atributo de permiso sobre el nodo de config.
                        _rootAuxiliaryDataPM.Attributes.Add("PermissionType", _permissionType);
                        _childExtendedPropertyClassificationsManage.Attributes.Add("PermissionType", _permissionType);
                        _childExtendedPropertiesManage.Attributes.Add("PermissionType", _permissionType);
                        _childParticipationTypeManage.Attributes.Add("PermissionType", _permissionType);

                        _rootAuxiliaryDataPM.Nodes.Add(_childExtendedPropertyClassificationsManage);
                        _rootAuxiliaryDataPM.Nodes.Add(_childExtendedPropertiesManage);
                        _rootAuxiliaryDataPM.Nodes.Add(_childParticipationTypeManage);
                        _rtvMenuPM.Nodes.Add(_rootAuxiliaryDataPM);

                        _rtvMenuPM.ExpandAllNodes();
                    }

                    return _rtvMenuPM;
                }
                public RadTreeView BuildMenuModulePAAdmin(Dictionary<String, Object> param)
                {
                    RadTreeView _rtvMenuPA = BuildTVMenuModule("PAAdmin");
                    Int32 _pkNode = 0;

                    //Si el usuario tiene permisos de config sobre el mapa, entonces le habilita la seguridad...
                    String _permissionType = String.Empty;
                    //Si el usuario no tiene ningun permiso sobre config, no se arma el TREE.
                    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.Count > 0)
                    {
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }


                        //RadTreeNode _rootPerformanceData = AddNode(Resources.CommonMenu.mnuPAPerformanceData_Title, Resources.CommonMenu.mnuPAPerformanceData_ToolTip, (_pkNode++).ToString(), null, false, "Folder", "FolderHover", "FolderSelected");
                        //RadTreeNode _childFormulaManage = AddNode(Resources.CommonListManage.Formulas, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                        //    Common.ConstantsEntitiesName.PA.Formulas, Common.ConstantsEntitiesName.PA.Formula, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty,
                        //    String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        //RadTreeNode _childMeasurementByProcess = AddNode(Resources.CommonListManage.Measurements, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                        //    Common.ConstantsEntitiesName.PA.Measurements, Common.ConstantsEntitiesName.PA.Measurement, Common.ConstantsEntitiesName.PA.Measurement, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty,
                        //    Common.ConstantsEntitiesName.PF.ProcessClassifications, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, Common.ConstantsEntitiesName.PF.ProcessGroupProcessesRoots, Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        //RadTreeNode _childTransformationByProcess = AddNode(Resources.CommonListManage.Transformations, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                        //    Common.ConstantsEntitiesName.PA.Transformations, Common.ConstantsEntitiesName.PA.TransformationByTransformation, Common.ConstantsEntitiesName.PA.Transformation, String.Empty, String.Empty, String.Empty, String.Empty, true, String.Empty,
                        //    Common.ConstantsEntitiesName.PF.ProcessClassifications, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, Common.ConstantsEntitiesName.PF.ProcessGroupProcessesRoots, Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, String.Empty, "Document", "DocumentHover", "DocumentSelected");

                        //Agrega atributo de permiso sobre el nodo de config.
                        //_rootPerformanceData.Attributes.Add("PermissionType", _permissionType);
                        //_childFormulaManage.Attributes.Add("PermissionType", _permissionType);
                        //_childMeasurementByProcess.Attributes.Add("PermissionType", _permissionType);
                        //_childTransformationByProcess.Attributes.Add("PermissionType", _permissionType);

                        //_rootPerformanceData.Nodes.Add(_childFormulaManage);
                        //_rootPerformanceData.Nodes.Add(_childMeasurementByProcess);
                        //_rootPerformanceData.Nodes.Add(_childTransformationByProcess);

                        //_rtvMenuPA.Nodes.Add(_rootPerformanceData);

                        RadTreeNode _rootAuxiliaryDataPA = AddNode(Resources.CommonMenu.mnuAuxiliaryData_Title, Resources.CommonMenu.mnuAuxiliaryData_ToolTip, (_pkNode++).ToString(), null, false, "Folder", "FolderHover", "FolderSelected");
                        RadTreeNode _childMagnitudesManage = AddNode(Resources.CommonListManage.Magnitudes, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PA.Magnitudes, Common.ConstantsEntitiesName.PA.Magnitud, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty,
                            String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childMeasurementUnitsManage = AddNode(Resources.CommonListManage.MeasurementUnits, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PA.MeasurementUnits, Common.ConstantsEntitiesName.PA.MeasurementUnit, String.Empty, String.Empty, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.Magnitudes, false, String.Empty,
                            String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childMeasurementDeviceTypesManage = AddNode(Resources.CommonListManage.MeasurementDeviceTypes, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PA.MeasurementDeviceTypes, Common.ConstantsEntitiesName.PA.MeasurementDeviceType, String.Empty, String.Empty, String.Empty, String.Empty,
                            String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childMeasurementDevicesManage = AddNode(Resources.CommonListManage.MeasurementDevices, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PA.MeasurementDevices, Common.ConstantsEntitiesName.PA.MeasurementDevice, String.Empty, String.Empty, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.MeasurementDeviceTypes, false,
                            String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        //RadTreeNode _childCalculationScenarioType = AddNode(Resources.CommonListManage.CalculationScenarioTypes, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                        //    Common.ConstantsEntitiesName.PA.CalculationScenarioTypes, Common.ConstantsEntitiesName.PA.CalculationScenarioType, String.Empty, String.Empty, String.Empty, String.Empty, Common.ConstantsEntitiesName.PF.ProcessClassifications, true,
                        //    Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childConstantClassification = AddNode(Resources.CommonListManage.ConstantClassifications, (_pkNode++).ToString(), "~/Managers/HierarchicalListManage.aspx", true, 
                            String.Empty, Common.ConstantsEntitiesName.PA.ConstantClassification, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.ConstantClassifications, Common.ConstantsEntitiesName.PA.ConstantClassificationChildren, 
                            String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childConstant = AddNode(Resources.CommonListManage.Constants, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PA.Constants, Common.ConstantsEntitiesName.PA.Constant, String.Empty, String.Empty, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.ConstantClassifications, true,
                            Common.ConstantsEntitiesName.PA.ConstantClassificationChildren, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childAccountingActivity = AddNode(Resources.CommonListManage.AccountingActivities, (_pkNode++).ToString(), "~/Managers/HierarchicalListManage.aspx", true,
                            String.Empty, Common.ConstantsEntitiesName.PA.AccountingActivity, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.AccountingActivities, Common.ConstantsEntitiesName.PA.AccountingActivitiesChildren,
                            String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        //RadTreeNode _childAccountingSector = AddNode(Resources.CommonListManage.AccountingSector, (_pkNode++).ToString(), "~/Managers/HierarchicalListManage.aspx", true,
                        //    String.Empty, Common.ConstantsEntitiesName.PA.AccountingSector, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.AccountingSectors, Common.ConstantsEntitiesName.PA.AccountingSectorsChildren,
                        //    String.Empty, true, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childAccountingScenario = AddNode(Resources.CommonListManage.AccountingScenarios, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PA.AccountingScenarios, Common.ConstantsEntitiesName.PA.AccountingScenario, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty,
                            String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childAccountingScope = AddNode(Resources.CommonListManage.AccountingScopes, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PA.AccountingScopes, Common.ConstantsEntitiesName.PA.AccountingScope, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty,
                            String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childMethodology = AddNode(Resources.CommonListManage.Methodologies, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PA.Methodologies, Common.ConstantsEntitiesName.PA.Methodology, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty,
                            String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childQuality = AddNode(Resources.CommonListManage.Qualities, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PA.Qualities, Common.ConstantsEntitiesName.PA.Quality, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty,
                            String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");

                        RadTreeNode _childConfigExcelFile = AddNode(Resources.CommonListManage.ConfigurationExcelFiles, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                            Common.ConstantsEntitiesName.PA.ConfigurationExcelFiles, Common.ConstantsEntitiesName.PA.ConfigurationExcelFile, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty,
                            String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");

                        //RadTreeNode _childMeasurementByProcess = AddNode(Resources.CommonListManage.Measurement, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                        //    Common.ConstantsEntitiesName.PA.Measurements, Common.ConstantsEntitiesName.PA.Measurement, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, true,
                        //    String.Empty, Common.ConstantsEntitiesName.PF.ProcessClassification, Common.ConstantsEntitiesName.PF.ProcessClassificationChildren, Common.ConstantsEntitiesName.PF.ProcessGroupProcessesRoots, Common.ConstantsEntitiesName.PF.ProcessGroupProcess, String.Empty, "Document", "DocumentHover", "DocumentSelected");

                        //Agrega atributo de permiso sobre el nodo de config.
                        _rootAuxiliaryDataPA.Attributes.Add("PermissionType", _permissionType);
                        _childMagnitudesManage.Attributes.Add("PermissionType", _permissionType);
                        _childMeasurementUnitsManage.Attributes.Add("PermissionType", _permissionType);
                        _childMeasurementDeviceTypesManage.Attributes.Add("PermissionType", _permissionType);
                        _childMeasurementDevicesManage.Attributes.Add("PermissionType", _permissionType);
                        //_childCalculationScenarioType.Attributes.Add("PermissionType", _permissionType);
                        _childConstantClassification.Attributes.Add("PermissionType", _permissionType);
                        _childConstant.Attributes.Add("PermissionType", _permissionType);
                        _childAccountingActivity.Attributes.Add("PermissionType", _permissionType);
                        //_childAccountingSector.Attributes.Add("PermissionType", _permissionType);
                        _childAccountingScenario.Attributes.Add("PermissionType", _permissionType);
                        _childAccountingScope.Attributes.Add("PermissionType", _permissionType);
                        _childMethodology.Attributes.Add("PermissionType", _permissionType);
                        _childQuality.Attributes.Add("PermissionType", _permissionType);
                        _childConfigExcelFile.Attributes.Add("PermissionType", _permissionType);

                        _rootAuxiliaryDataPA.Nodes.Add(_childMagnitudesManage);
                        _rootAuxiliaryDataPA.Nodes.Add(_childMeasurementUnitsManage);
                        _rootAuxiliaryDataPA.Nodes.Add(_childMeasurementDeviceTypesManage);
                        _rootAuxiliaryDataPA.Nodes.Add(_childMeasurementDevicesManage);
                        //_rootAuxiliaryDataPA.Nodes.Add(_childCalculationScenarioType);
                        _rootAuxiliaryDataPA.Nodes.Add(_childConstantClassification);
                        _rootAuxiliaryDataPA.Nodes.Add(_childConstant);
                        _rootAuxiliaryDataPA.Nodes.Add(_childAccountingActivity);
                        //_rootAuxiliaryDataPA.Nodes.Add(_childAccountingSector);
                        _rootAuxiliaryDataPA.Nodes.Add(_childAccountingScenario);
                        _rootAuxiliaryDataPA.Nodes.Add(_childAccountingScope);
                        _rootAuxiliaryDataPA.Nodes.Add(_childMethodology);
                        _rootAuxiliaryDataPA.Nodes.Add(_childQuality);
                        _rootAuxiliaryDataPA.Nodes.Add(_childConfigExcelFile);

                        _rtvMenuPA.Nodes.Add(_rootAuxiliaryDataPA);

                        _rtvMenuPA.ExpandAllNodes();
                    }
                    return _rtvMenuPA;
                }
                public RadTreeView BuildMenuModuleKCAdmin(Dictionary<String, Object> param)
                {
                    RadTreeView _rtvMenuKC = BuildTVMenuModule("KCAdmin");
                    Int32 _pkNode = 0;
                    
                    //Si el usuario tiene permisos de config sobre el mapa, entonces le habilita la seguridad...
                    String _permissionType = String.Empty;
                    //Si el usuario no tiene ningun permiso sobre config, no se arma el TREE.
                    if (EMSLibrary.User.KnowledgeCollaboration.Configuration.Permissions.Count > 0)
                    {
                        if (EMSLibrary.User.KnowledgeCollaboration.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }


                        RadTreeNode _rootResourcesData = AddNode(Resources.CommonMenu.mnuKCResourcesData_Title, Resources.CommonMenu.mnuKCResourcesData_ToolTip, (_pkNode++).ToString(), null, false, "Folder", "FolderHover", "FolderSelected");
                        RadTreeNode _childResourceTypesManage = AddNode(Resources.CommonListManage.ResourceTypes, (_pkNode++).ToString(), "~/Managers/HierarchicalListManage.aspx", true,
                            String.Empty, Common.ConstantsEntitiesName.KC.ResourceType, String.Empty, String.Empty, Common.ConstantsEntitiesName.KC.ResourceTypes, Common.ConstantsEntitiesName.KC.ResourceTypeChildren,
                            String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childResourceFileStatesManage = AddNode(Resources.CommonListManage.ResourceFileStates, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true,
                             Common.ConstantsEntitiesName.KC.ResourceFileStates, Common.ConstantsEntitiesName.KC.ResourceHistoryState, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty,
                             String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");

                        //Agrega atributo de permiso sobre el nodo de config.
                        _rootResourcesData.Attributes.Add("PermissionType", _permissionType);
                        _childResourceTypesManage.Attributes.Add("PermissionType", _permissionType);
                        _childResourceFileStatesManage.Attributes.Add("PermissionType", _permissionType);

                        _rootResourcesData.Nodes.Add(_childResourceTypesManage);
                        _rootResourcesData.Nodes.Add(_childResourceFileStatesManage);
                        _rtvMenuKC.Nodes.Add(_rootResourcesData);

                        _rtvMenuKC.ExpandAllNodes();
                    }
                    return _rtvMenuKC;
                }
                public RadTreeView BuildMenuModuleIAAdmin(Dictionary<String, Object> param)
                {
                    RadTreeView _rtvMenuIA = BuildTVMenuModule("IAAdmin");
                    Int32 _pkNode = 0;
                    //Si el usuario tiene permisos de config sobre el mapa, entonces le habilita la seguridad...
                    String _permissionType = String.Empty;
                    //Si el usuario no tiene ningun permiso sobre config, no se arma el TREE.
                    if (EMSLibrary.User.ImprovementAction.Configuration.Permissions.Count > 0)
                    {
                        if (EMSLibrary.User.ImprovementAction.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Por ahora IA no tiene Auxiliary Data.
                        //RadTreeNode _rootImprovementActionsData = AddNode(Resources.CommonSiteMap.IA_ImprovementActionsData_Title, (_pkNode++).ToString(), null, false, "Folder", "FolderHover", "FolderSelected");

                        //RadTreeNode _childProjectClassificationsManage = AddNode(Resources.CommonSiteMap.IA_ProjectClassificationsManage_Title, (_pkNode++).ToString(), "~/Managers/HierarchicalListManage.aspx", true,
                        //    String.Empty, Common.ConstantsEntitiesName.IA.ProjectClassification, String.Empty, String.Empty, Common.ConstantsEntitiesName.IA.ProjectClassifications, Common.ConstantsEntitiesName.IA.ProjectClassificationChildren,
                        //    String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");

                        ////Agrega atributo de permiso sobre el nodo de config.
                        //_rootImprovementActionsData.Attributes.Add("PermissionType", _permissionType);
                        //_childProjectClassificationsManage.Attributes.Add("PermissionType", _permissionType);

                        //_rootImprovementActionsData.Nodes.Add(_childProjectClassificationsManage);
                        //_rtvMenuIA.Nodes.Add(_rootImprovementActionsData);
                    }

                    return _rtvMenuIA;
                }
                public RadTreeView BuildMenuModuleRMAdmin(Dictionary<String, Object> param)
                {
                    RadTreeView _rtvMenuRM = BuildTVMenuModule("RMAdmin");
                    Int32 _pkNode = 0;
                    //Si el usuario tiene permisos de config sobre el mapa, entonces le habilita la seguridad...
                    String _permissionType = String.Empty;
                    //Si el usuario no tiene ningun permiso sobre config, no se arma el TREE.
                    if (EMSLibrary.User.RiskManagement.Configuration.Permissions.Count > 0)
                    {
                        if (EMSLibrary.User.RiskManagement.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Por ahora RM no tiene Auxiliary Data.

                        //RadTreeNode _rootRiskManagementsData = AddNode(Resources.CommonSiteMap.RM_RisksData_Title, (_pkNode++).ToString(), null, false, "Folder", "FolderHover", "FolderSelected");

                        //RadTreeNode _childRiskClassificationsManage = AddNode(Resources.CommonSiteMap.RM_RiskClassificationsManage_Title, (_pkNode++).ToString(), "~/Managers/HierarchicalListManage.aspx", true,
                        //    String.Empty, Common.ConstantsEntitiesName.RM.RiskClassification, String.Empty, String.Empty, Common.ConstantsEntitiesName.RM.RiskClassifications, Common.ConstantsEntitiesName.RM.RiskClassificationChildren,
                        //    String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");

                        //_rootRiskManagementsData.Nodes.Add(_childRiskClassificationsManage);
                        //_rtvMenuRM.Nodes.Add(_rootRiskManagementsData);
                    }

                    return _rtvMenuRM;
                }
                public RadTreeView BuildMenuModuleDashboardAdmin(Dictionary<String, Object> param)
                {
                    //Se usa el mismo menu...
                    return BuildMenuModuleDashboardMap(param);
                }
            #endregion

            #region Module Menu Maps
                /// <summary>
                /// Arma el tree con el Mapa de Directory Services. OrganizationClassification + Organization.
                /// </summary>
                /// <param name="param"></param>
                /// <returns></returns>
                public RadTreeView BuildMenuModuleDSMap(Dictionary<String, Object> param)
                {
                    //Setea estas variables con las entidades de Process Framework.
                    _EntityNameMapClassification = Common.ConstantsEntitiesName.DS.OrganizationClassifications;
                    _EntityNameMapClassificationChildren = Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren;
                    _EntityNameMapElement = Common.ConstantsEntitiesName.DS.OrganizationsRoots;
                    _EntityNameMapElementChildren = Common.ConstantsEntitiesName.DS.Organizations;
                    _SingleEntityClassificationName = Common.ConstantsEntitiesName.DS.OrganizationClassification;
                    _SingleEntityElementName = Common.ConstantsEntitiesName.DS.Organization;
                    _ContextInfoEntityName = Common.ConstantsEntitiesName.DS.Organization;
                    _ContextInfoEntityClassificationName = Common.ConstantsEntitiesName.DS.OrganizationClassification;
                    _ContextElementMapEntityName = Common.ConstantsEntitiesName.DS.Organization;

                    //Contruye y retorna el TreeView con los datos y configurado.
                    param.Add("NodeRootTitle", Resources.Common.MainMenuATDirectoryServicesTitle);

                    String _permissionType = String.Empty;
                    //Si el usuario tiene permisos de MAnage sobre el mapa, entonces le habilita la seguridad...
                    if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType= Common.Constants.PermissionManageName; }
                    else
                        { _permissionType=Common.Constants.PermissionViewName; }

                    //RadTreeView _radTreeView = BuildMenuElementMap(param, _permissionType);
                    RadTreeView _radTreeView = BuildMenuElementMap(param, _permissionType);
                    _radTreeView.ID = "rtvMenuModuleOrganizationClassifications";

                    return _radTreeView;
                }
                /// <summary>
                /// Arma el tree con el Mapa de Performance Assessment. IndicatorClassification + Indicator
                /// </summary>
                /// <param name="param"></param>
                /// <returns></returns>
                public RadTreeView BuildMenuModulePAMap(Dictionary<String, Object> param)
                {
                    //Setea estas variables con las entidades de Performance Assessment.
                    _EntityNameMapClassification = Common.ConstantsEntitiesName.PA.IndicatorClassifications;
                    _EntityNameMapClassificationChildren = Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren;
                    _EntityNameMapElement = Common.ConstantsEntitiesName.PA.IndicatorsRoots;
                    _EntityNameMapElementChildren = Common.ConstantsEntitiesName.PA.Indicators;
                    _SingleEntityClassificationName = Common.ConstantsEntitiesName.PA.IndicatorClassification;
                    _SingleEntityElementName = Common.ConstantsEntitiesName.PA.Indicator;
                    _ContextInfoEntityName = Common.ConstantsEntitiesName.PA.Indicator;
                    _ContextElementMapEntityName = Common.ConstantsEntitiesName.PA.Indicator;
                    _ContextInfoEntityClassificationName = Common.ConstantsEntitiesName.PA.IndicatorClassification;

                    //Contruye y retorna el TreeView con los datos y configurado.
                    param.Add("NodeRootTitle", Resources.Common.MainMenuATPerformanceAssessmentTitle);
                    String _permissionType = String.Empty;
                    //Si el usuario tiene permisos de MAnage sobre el mapa, entonces le habilita la seguridad...
                    if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                    else
                        { _permissionType = Common.Constants.PermissionViewName; }

                    RadTreeView _radTreeView = BuildMenuElementMap(param, _permissionType);
                    _radTreeView.ID = "rtvMenuModuleIndicatorClassifications";

                    return _radTreeView;
                }
                /// <summary>
                /// Arma el tree con el Mapa de Process Framework. ProcessClassification + Process.
                /// </summary>
                /// <param name="param"></param>
                /// <returns></returns>
                public RadTreeView BuildMenuModulePMMap(Dictionary<String, Object> param)
                {
                    //Setea estas variables con las entidades de Process Framework.
                    _EntityNameMapClassification = Common.ConstantsEntitiesName.PF.ProcessClassifications;
                    _EntityNameMapClassificationChildren = Common.ConstantsEntitiesName.PF.ProcessClassificationChildren;
                    _EntityNameMapElement = Common.ConstantsEntitiesName.PF.ProcessGroupProcessesRoots;
                    _EntityNameMapElementChildren = Common.ConstantsEntitiesName.PF.ProcessGroupProcesses;
                    _SingleEntityClassificationName = Common.ConstantsEntitiesName.PF.ProcessClassification;
                    _SingleEntityElementName = Common.ConstantsEntitiesName.PF.ProcessGroupProcess;
                    _ContextInfoEntityName = Common.ConstantsEntitiesName.PF.Process;
                    _ContextInfoEntityClassificationName = Common.ConstantsEntitiesName.PF.ProcessClassification;
                    _ContextElementMapEntityName = Common.ConstantsEntitiesName.PF.ProcessGroupProcess;

                    //Contruye y retorna el TreeView con los datos y configurado.
                    param.Add("NodeRootTitle", Resources.Common.MainMenuATProcessesMapTitle);
                    String _permissionType = String.Empty;
                    //Si el usuario tiene permisos de MAnage sobre el mapa, entonces le habilita la seguridad...
                    if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                    else
                        { _permissionType = Common.Constants.PermissionViewName; }

                    RadTreeView _radTreeView = BuildMenuElementMap(param, _permissionType);
                    _radTreeView.ID = "rtvMenuModuleProcessClassifications";
                    
                    return _radTreeView;
                }
                /// <summary>
                /// Arma el tree con el Mapa de Knowledge Collaboration. ResourceClassification + Resource.
                /// </summary>
                /// <param name="param"></param>
                /// <returns></returns>
                public RadTreeView BuildMenuModuleKCMap(Dictionary<String, Object> param)
                {
                    //Setea estas variables con las entidades de Process Framework.
                    _EntityNameMapClassification = Common.ConstantsEntitiesName.KC.ResourceClassifications;
                    _EntityNameMapClassificationChildren = Common.ConstantsEntitiesName.KC.ResourceClassificationChildren;
                    _EntityNameMapElement = Common.ConstantsEntitiesName.KC.ResourcesRoots;
                    _EntityNameMapElementChildren = Common.ConstantsEntitiesName.KC.Resources;
                    _SingleEntityClassificationName = Common.ConstantsEntitiesName.KC.ResourceClassification;
                    _SingleEntityElementName = Common.ConstantsEntitiesName.KC.Resource;
                    _ContextInfoEntityName = Common.ConstantsEntitiesName.KC.Resource;
                    _ContextInfoEntityClassificationName = Common.ConstantsEntitiesName.KC.ResourceClassification;
                    _ContextElementMapEntityName = String.Empty;

                    //Contruye y retorna el TreeView con los datos y configurado.
                    param.Add("NodeRootTitle", Resources.Common.MainMenuATKnowledgeCollaborationTitle);
                    String _permissionType = String.Empty;
                    //Si el usuario tiene permisos de MAnage sobre el mapa, entonces le habilita la seguridad...
                    if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                    else
                        { _permissionType = Common.Constants.PermissionViewName; }

                    RadTreeView _radTreeView = BuildMenuElementMap(param, _permissionType);
                    _radTreeView.ID = "rtvMenuModuleResourceClassifications";

                    return _radTreeView;
                }
                /// <summary>
                /// Arma el tree con el Mapa de Improvement Actions. ProjectClassification + Project.
                /// </summary>
                /// <param name="param"></param>
                /// <returns></returns>
                public RadTreeView BuildMenuModuleIAMap(Dictionary<String, Object> param)
                {
                    //Setea estas variables con las entidades de Process Framework.
                    _EntityNameMapClassification = "ProjectClassifications";
                    _EntityNameMapClassificationChildren = "ProjectClassificationsChildren";
                    _EntityNameMapElement = "ProjectsRoots";
                    _EntityNameMapElementChildren = "IAProjects";
                    _SingleEntityClassificationName = "ProjectClassification";
                    _SingleEntityElementName = "IAProject";
                    _ContextInfoEntityName = String.Empty;
                    _ContextInfoEntityClassificationName = String.Empty;
                    _ContextElementMapEntityName = String.Empty;

                    //Contruye y retorna el TreeView con los datos y configurado.
                    //RadTreeView _radTreeView = BuildMenuElementMap();
                    param.Add("NodeRootTitle", Resources.Common.MainMenuATImprovementActions);
                    String _permissionType = String.Empty;
                    //Si el usuario tiene permisos de MAnage sobre el mapa, entonces le habilita la seguridad...
                    if (EMSLibrary.User.ImprovementAction.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                    else
                        { _permissionType = Common.Constants.PermissionViewName; }

                    RadTreeView _radTreeView = BuildMenuElementMap(param, _permissionType);
                    _radTreeView.ID = "rtvMenuModuleProjectClassifications";

                    return _radTreeView;
                }
                /// <summary>
                /// Arma el tree con el Mapa de Risk And Potencial.
                /// </summary>
                /// <param name="param"></param>
                /// <returns></returns>
                public RadTreeView BuildMenuModuleRMMap(Dictionary<String, Object> param)
                {
                    //Setea estas variables con las entidades de Process Framework.
                    _EntityNameMapClassification = "RiskClassifications";
                    _EntityNameMapClassificationChildren = "RiskClassificationsChildren";
                    _EntityNameMapElement = "RisksRoots";
                    _EntityNameMapElementChildren = "Risks";
                    _SingleEntityClassificationName = "RiskClassification";
                    _SingleEntityElementName = "Risk";
                    _ContextInfoEntityName = String.Empty;
                    _ContextInfoEntityClassificationName = String.Empty;
                    _ContextElementMapEntityName = String.Empty;

                    //Contruye y retorna el TreeView con los datos y configurado.
                    //RadTreeView _radTreeView = BuildMenuElementMap();
                    param.Add("NodeRootTitle", Resources.Common.MainMenuATRiskManagementTitle);
                    String _permissionType = String.Empty;
                    //Si el usuario tiene permisos de MAnage sobre el mapa, entonces le habilita la seguridad...
                    if (EMSLibrary.User.RiskManagement.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                    else
                        { _permissionType = Common.Constants.PermissionViewName; }

                    RadTreeView _radTreeView = BuildMenuElementMap(param, _permissionType);
                    _radTreeView.ID = "rtvMenuModuleRiskAndPotencial";

                    return _radTreeView;
                }
                /// <summary>
                /// Arma el tree con el Mapa de Dashboard. muestra Mis tareas y Excepciones.
                /// </summary>
                /// <param name="param"></param>
                /// <returns></returns>
                public RadTreeView BuildMenuModuleDashboardMap(Dictionary<String, Object> param)
                {
                    RadTreeView _rtvMenuDashboard = BuildTVMenuModule("Dashboard");

                    Int32 _pkNode = 0;

                    //Si el usuario tiene permisos de config sobre el mapa, entonces le habilita la seguridad...
                    String _permissionType =Common.Constants.PermissionViewName;

                    RadTreeNode _rootDashboardMyTasks = AddNode(Resources.CommonMenu.mnuDashboardMyTasks, Resources.CommonMenu.mnuDashboardMyTask_ToolTip, (_pkNode++).ToString(), null, false, "Folder", "FolderHover", "FolderSelected");

                    RadTreeNode _childTaskPlanned = AddNode(Resources.CommonMenu.mnuMyTaskPlanned, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DB.PlannedTasks, Common.ConstantsEntitiesName.DB.PlannedTasks, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                    RadTreeNode _childTaskWorking = AddNode(Resources.CommonMenu.mnuMyTaskWorking, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DB.ActiveTasks, Common.ConstantsEntitiesName.DB.ActiveTasks, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                    RadTreeNode _childTaskOverDue = AddNode(Resources.CommonMenu.mnuMyTaskOverDue, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DB.OverDueTasks, Common.ConstantsEntitiesName.DB.OverDueTasks, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                    RadTreeNode _childTaskFinished = AddNode(Resources.CommonMenu.mnuMyTaskFinished, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DB.FinishedTasks, Common.ConstantsEntitiesName.DB.FinishedTasks, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                    RadTreeNode _childTaskExecutionExecuted = AddNode(Resources.CommonMenu.mnuMyTaskExecutionsExecuted, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DB.ProcessTaskExecutionExecuted, Common.ConstantsEntitiesName.DB.ProcessTaskExecutionExecuted, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                    RadTreeNode _childBulkLoad = AddNode(Resources.CommonMenu.mnuBulkLoad, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DB.BulkLoad, Common.ConstantsEntitiesName.DB.BulkLoad, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");

                    //if (IsOperator() && IsOperatorOnly())
                    if (IsOperator() && !EMSLibrary.User.ViewGlobalMenu)
                    {
                        //Agrega atributo de permiso sobre el nodo de config.
                        _rootDashboardMyTasks.Attributes.Add("PermissionType", _permissionType);
                        _childTaskPlanned.Attributes.Add("PermissionType", _permissionType);
                        _childTaskWorking.Attributes.Add("PermissionType", _permissionType);
                        _childTaskOverDue.Attributes.Add("PermissionType", _permissionType);
                        _childBulkLoad.Attributes.Add("PermissionType", _permissionType);

                        _rootDashboardMyTasks.Nodes.Add(_childTaskPlanned);
                        _rootDashboardMyTasks.Nodes.Add(_childTaskWorking);
                        _rootDashboardMyTasks.Nodes.Add(_childTaskOverDue);
                        _rootDashboardMyTasks.Nodes.Add(_childBulkLoad);

                        _rtvMenuDashboard.Nodes.Add(_rootDashboardMyTasks);

                    }
                    else
                    {
                        //Agrega atributo de permiso sobre el nodo de config.
                        _rootDashboardMyTasks.Attributes.Add("PermissionType", _permissionType);
                        _childTaskPlanned.Attributes.Add("PermissionType", _permissionType);
                        _childTaskWorking.Attributes.Add("PermissionType", _permissionType);
                        _childTaskOverDue.Attributes.Add("PermissionType", _permissionType);
                        _childTaskFinished.Attributes.Add("PermissionType", _permissionType);
                        _childTaskExecutionExecuted.Attributes.Add("PermissionType", _permissionType);
                        _childBulkLoad.Attributes.Add("PermissionType", _permissionType);

                        _rootDashboardMyTasks.Nodes.Add(_childTaskPlanned);
                        _rootDashboardMyTasks.Nodes.Add(_childTaskWorking);
                        _rootDashboardMyTasks.Nodes.Add(_childTaskOverDue);
                        _rootDashboardMyTasks.Nodes.Add(_childTaskFinished);
                        _rootDashboardMyTasks.Nodes.Add(_childTaskExecutionExecuted);
                        _rootDashboardMyTasks.Nodes.Add(_childBulkLoad);

                        if (IsOperator())
                        {
                            _rtvMenuDashboard.Nodes.Add(_rootDashboardMyTasks);
                        }

                        //Excepciones
                        RadTreeNode _rootDashboardExceptions = AddNode(Resources.CommonMenu.mnuDashboardExceptions, Resources.CommonMenu.mnuDashboardExceptions_ToolTip, (_pkNode++).ToString(), null, false, "Folder", "FolderHover", "FolderSelected");

                        RadTreeNode _childExceptionOpened = AddNode(Resources.CommonMenu.mnuExceptionOpened, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DB.OpenedExceptions, Common.ConstantsEntitiesName.DB.OpenedExceptions, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        //RadTreeNode _childExceptionWorking = AddNode(Resources.CommonMenu.mnuExceptionWorking, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DB.WorkingExceptions, Common.ConstantsEntitiesName.DB.WorkingExceptions, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");
                        RadTreeNode _childExceptionClosed = AddNode(Resources.CommonMenu.mnuExceptionClosed, (_pkNode++).ToString(), "~/Managers/ListManageAndView.aspx", true, Common.ConstantsEntitiesName.DB.ClosedExceptions, Common.ConstantsEntitiesName.DB.ClosedExceptions, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Document", "DocumentHover", "DocumentSelected");

                        //Agrega atributo de permiso sobre el nodo de config.
                        _rootDashboardExceptions.Attributes.Add("PermissionType", _permissionType);

                        _rootDashboardExceptions.Nodes.Add(_childExceptionOpened);
                        _rootDashboardExceptions.Nodes.Add(_childExceptionClosed);

                        _rtvMenuDashboard.Nodes.Add(_rootDashboardExceptions);
                    }

                    _rtvMenuDashboard.ExpandAllNodes();
                    
                    return _rtvMenuDashboard;
                }
            #endregion
        #endregion

        #region Events
            /// <summary>
            /// Evento para el Expand del TreeView pero ElementMaps
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void rtvElementMaps_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                _TreeViewGlobalMenuXML = String.Empty;
                e.Node.Selected = true;

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
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, _EntityNameMapClassificationChildren, _SingleEntityClassificationName, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, _ContextInfoEntityClassificationName, _ContextElementMapEntityName, String.Empty);
                        _node.Attributes.Add("EntityType", "Classification");
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
                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, _EntityNameMapElementChildren, _SingleEntityElementName, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, _ContextInfoEntityName, _ContextElementMapEntityName, String.Empty);
                        _node.Attributes.Add("EntityType", "Element");
                        e.Node.Nodes.Add(_node);
                        //Los elementos no tienen hijos
                        //SetExpandMode(_node, _EntityNameMapElementChildren);
                    }
                }
            }
            /// <summary>
            /// Evento para el NodeDrop del TreeView de ElementMaps.
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void rtvMenuElementMaps_NodeDrop(object sender, RadTreeNodeDragDropEventArgs e)
            {
                RadTreeNode sourceNode = e.SourceDragNode; //Origen
                RadTreeNode destNode = e.DestDragNode;  //Destino
                RadTreeViewDropPosition dropPosition = e.DropPosition;

                try
                {
                    Dictionary<String, Object> _paramSource = new Dictionary<String, Object>();
                    Dictionary<String, Object> _paramDestination = new Dictionary<String, Object>();
                    //Obtiene el nombre de la entidad origen que se esta cambiando de nivel y con eso arma el nombre del metodo a ejecutar por Reflection.
                    String _methodName = sourceNode.Attributes["SingleEntityName"].ToString() + "Move";
                    //Agrega los parametros para el origen.
                    _paramSource = GetKeyValues(sourceNode.Value);
                    //Si el destino no es el root, entonces agrego los parametros para el destino.
                    if (destNode.Value != "NodeRootTitle")
                    {
                        _paramDestination = GetKeyValues(destNode.Value);
                    }
                    //Ejecuta por reflection el cambio de padre.
                    new Condesus.EMS.WebUI.Business.MoveEntityNode(_methodName).Execute(_paramSource, _paramDestination);
                    //Cambia el nodo en el arbol.
                    PerformDragAndDrop(dropPosition, sourceNode, destNode);
                }
                catch { }
                
                    ////Obtener la clave del hijo
                    //String _pkValueSelected = sourceNode.Value;
                    //String _pkValueParent = destNode.Value;
                    //Int64 _idParentValue = 0; //Si es root, queda 0
                    ////Verifica cual es el nodo Destino
                    ////Si el destino es al ROOT, no pasa pasa por aca entonces el id queda en 0
                    //if (_pkValueParent != "NodeRootTitle")
                    //{
                    //    //El destino es otra classificacion, entonces busca el id.
                    //    _idParentValue = Convert.ToInt64(GetKeyValue(destNode.Value, "IdOrganizationClassification"));
                    //}
                    //Int64 _idOrganizationClassification = Convert.ToInt64(GetKeyValue(sourceNode.Value, "IdOrganizationClassification"));
                    ////Construye el objeto con el nodo que se esta intentando modificar (Origen)
                    //Condesus.EMS.Business.DS.Entities.OrganizationClassification _orgClassSource = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification);
                    ////Construye el objeto con el nodo destino o en Cero(0) en caso de ser el root.
                    //Condesus.EMS.Business.DS.Entities.OrganizationClassification _orgClassParent = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idParentValue);

                    ////Finalmente modifica el padre del elemento origen.
                    //_orgClassSource.Modify(_orgClassParent);

                    ////Cambia el nodo en el arbol.
                    //PerformDragAndDrop(dropPosition, sourceNode, destNode);
                

                //string result = "";

                //if (destNode != null)//drag&drop is performed between trees
                //{
                //    //if (this.ChbBetweenNodes.Checked)//dropped node will at the same level as a destination node
                //    //{
                //    //    if (sourceNode.TreeView.SelectedNodes.Count <= 1)
                //    //    {
                //    //        result += "<b>" + sourceNode.Text + "</b>" + ";";
                //    //        PerformDragAndDrop(dropPosition, sourceNode, destNode);
                //    //    }
                //    //    else if (sourceNode.TreeView.SelectedNodes.Count > 1)
                //    //    {
                //    //        foreach (RadTreeNode node in sourceNode.TreeView.SelectedNodes)
                //    //        {
                //    //            result += "<b>" + node.Text + "</b>" + ";";
                //    //            PerformDragAndDrop(dropPosition, node, destNode);
                //    //        }
                //    //    }
                //    //}
                //    //else//dropped node will be a sibling of the destination node
                //    //{
                //        if (sourceNode.TreeView.SelectedNodes.Count <= 1)
                //        {

                //            if (!sourceNode.IsAncestorOf(destNode))
                //            {
                //                result += "<b>" + sourceNode.Text + "</b>" + ";";
                //                sourceNode.Owner.Nodes.Remove(sourceNode);
                //                destNode.Nodes.Add(sourceNode);
                //            }
                //        }
                //        else if (sourceNode.TreeView.SelectedNodes.Count > 1)
                //        {
                //            foreach (RadTreeNode node in ((RadTreeView)sender).SelectedNodes)
                //            {
                //                if (!node.IsAncestorOf(destNode))
                //                {
                //                    result += "<b>" + node.Text + "</b>" + ";";
                //                    node.Owner.Nodes.Remove(node);
                //                    destNode.Nodes.Add(node);
                //                }
                //            }
                //        }
                //    //}

                //    destNode.Expanded = true;
                //    //DragMessage.Text = "You dragged node(s)" + result + " onto node <b>" + destNode.Text + "</b>";
                //    sourceNode.TreeView.ClearSelectedNodes();
                //}
            }
            private static void PerformDragAndDrop(RadTreeViewDropPosition dropPosition, RadTreeNode sourceNode, RadTreeNode destNode)
            {
                if (sourceNode.Equals(destNode) || sourceNode.IsAncestorOf(destNode))
                {
                    return;
                }
                sourceNode.Owner.Nodes.Remove(sourceNode);

                switch (dropPosition)
                {
                    case RadTreeViewDropPosition.Over:
                        // child
                        if (!sourceNode.IsAncestorOf(destNode))
                        {
                            destNode.Nodes.Add(sourceNode);
                        }
                        break;

                    case RadTreeViewDropPosition.Above:
                        // sibling - above                    
                        destNode.InsertBefore(sourceNode);
                        break;

                    case RadTreeViewDropPosition.Below:
                        // sibling - below
                        destNode.InsertAfter(sourceNode);
                        break;
                }
            }

        #endregion

    }
}
