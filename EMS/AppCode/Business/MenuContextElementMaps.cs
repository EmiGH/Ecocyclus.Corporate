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
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;
using System.IO;

namespace Condesus.EMS.WebUI.Business
{
    public partial class MenuGRC : Base
    {
        /// <summary>
        /// Este metodo arma el nodo para el tree para ElementMaps
        /// </summary>
        /// <param name="drRecord">Indica el registro para insertar en el nodo</param>
        /// <returns>Un<c>RadTreeNode</c></returns>
        private RadTreeNode SetElementMapsNodeTreeView(String nodeText, String keyValue, String cssClassName)
        {
            RadTreeNode _node = new RadTreeNode();

            _node.Text = Common.Functions.ReplaceIndexesTags(nodeText);
            _node.Value = keyValue;
            _node.Checkable = false;
            _node.PostBack = true;

            _node.CssClass = cssClassName;
            return _node;
        }
        /// <summary>
        /// Este metodo construye el Menu contextual de accesos directo para agregarselo a los TReeView.
        /// </summary>
        /// <returns>Un<c>RadTreeViewContextMenu</c></returns>
        private RadTreeViewContextMenu BuildContextMenuContextElementShortCut()
        {
            RadTreeViewContextMenu _rtvContextMenu = new RadTreeViewContextMenu();
            _rtvContextMenu.ID = "rtvContextMenuContextInfo";
            _rtvContextMenu.EnableEmbeddedSkins = false;
            _rtvContextMenu.Skin = "EMS";

            //Crea los items del menu contextual
            RadMenuItem _rmItemAdd = new RadMenuItem(Resources.Common.mnuAdd);
            _rmItemAdd.Value = "rmiAdd";
            RadMenuItem _rmItemEdit = new RadMenuItem(Resources.Common.mnuEdit);
            _rmItemEdit.Value = "rmiEdit";
            //Crea security y tiene hijos
            RadMenuItem _rmItemSecurity = new RadMenuItem(Resources.Common.mnuSecurity);
            _rmItemSecurity.Value = "rmiSecurity";
            _rmItemSecurity.PostBack = false;
            //Aca crea los 2 hijos de security
            RadMenuItem _rmItemJobTitle = new RadMenuItem(Resources.Common.mnuSSJobTitle);
            _rmItemJobTitle.Value = "rmItemJobTitleConfig";
            RadMenuItem _rmItemPerson = new RadMenuItem(Resources.Common.mnuSSPerson);
            _rmItemPerson.Value = "rmItemPersonConfig";
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
        /// Este metodo construye el Menu contextual de process de accesos directo para agregarselo a los TReeView.
        /// </summary>
        /// <returns>Un<c>RadTreeViewContextMenu</c></returns>
        private RadTreeViewContextMenu BuildContextMenuContextElementProcessShortCut()
        {
            RadTreeViewContextMenu _rtvContextMenu = new RadTreeViewContextMenu();
            _rtvContextMenu.ID = "rtvContextMenuContextInfo";
            _rtvContextMenu.EnableEmbeddedSkins = false;
            _rtvContextMenu.Skin = "EMS";

            //Crea los items del menu contextual
            RadMenuItem _rmItemAdd = new RadMenuItem(Resources.Common.mnuAdd);
            _rmItemAdd.Value = "rmiAdd";
            _rmItemAdd.PostBack = false;
            //Aca crea los hijos del ADD
            //RadMenuItem _rmItemNode = new RadMenuItem(Resources.Common.mnuAddNode);
            //_rmItemNode.Value = "rmItemAddNode";
            RadMenuItem _rmItemTask = new RadMenuItem(Resources.Common.mnuAddTask);
            _rmItemTask.Value = "rmItemAddTask";
            _rmItemTask.PostBack = false;
            //Los hijos del add Task
            RadMenuItem _rmItemTaskCalibration = new RadMenuItem(Resources.Common.mnuCalibration);
            _rmItemTaskCalibration.Value = "rmItemAddTaskCalibration";
            RadMenuItem _rmItemTaskMeasurement = new RadMenuItem(Resources.Common.mnuMeasurement);
            _rmItemTaskMeasurement.Value = "rmItemAddTaskMeasurement";
            RadMenuItem _rmItemTaskOperation = new RadMenuItem(Resources.Common.mnuOperation);
            _rmItemTaskOperation.Value = "rmItemAddTaskOperation";
            //Inserta el nodo dentro del add
            //_rmItemAdd.Items.Add(_rmItemNode);
            //Insertar cada task dentro del Task
            _rmItemTask.Items.Add(_rmItemTaskCalibration);
            _rmItemTask.Items.Add(_rmItemTaskMeasurement);
            _rmItemTask.Items.Add(_rmItemTaskOperation);
            //Finalmente inserta el task dentro del Add.
            _rmItemAdd.Items.Add(_rmItemTask);

            //Agregamos un item para Poder Agregar Archivos al Plan de Auditoria...
            RadMenuItem _rmItemAuditPlan = new RadMenuItem(Resources.Common.mnuAuditFile);
            _rmItemAuditPlan.Value = "rmItemAddAuditPlan";
            _rmItemAdd.Items.Add(_rmItemAuditPlan);

            RadMenuItem _rmItemEdit = new RadMenuItem(Resources.Common.mnuEdit);
            _rmItemEdit.Value = "rmiEdit";
            //Crea security y tiene hijos
            RadMenuItem _rmItemSecurity = new RadMenuItem(Resources.Common.mnuSecurity);
            _rmItemSecurity.Value = "rmiSecurity";
            _rmItemSecurity.PostBack = false;
            //Aca crea los 2 hijos de security
            RadMenuItem _rmItemJobTitle = new RadMenuItem(Resources.Common.mnuSSJobTitle);
            _rmItemJobTitle.Value = "rmItemJobTitleConfig";
            RadMenuItem _rmItemPerson = new RadMenuItem(Resources.Common.mnuSSPerson);
            _rmItemPerson.Value = "rmItemPersonConfig";
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
        /// Este metodo construye el Menu contextual de accesos directo al tree de los facilities
        /// </summary>
        /// <returns>Un<c>RadTreeViewContextMenu</c></returns>
        private RadTreeViewContextMenu BuildContextMenuContextElementFacilityTypeShortCut()
        {
            RadTreeViewContextMenu _rtvContextMenu = new RadTreeViewContextMenu();
            _rtvContextMenu.ID = "rtvContextMenuContextInfo";
            _rtvContextMenu.EnableEmbeddedSkins = false;
            _rtvContextMenu.Skin = "EMS";

            //Crea los items del menu contextual
            RadMenuItem _rmItemEdit = new RadMenuItem(Resources.Common.mnuEdit);
            _rmItemEdit.Value = "rmiEditSites";

            //Agrega los items root del menu
            _rtvContextMenu.Items.Add(_rmItemEdit);

            return _rtvContextMenu;
        }

        #region Public Methods (Build ContentCustomPanel ContextElementMaps)

            #region Process
                public RadTreeView BuildContextElementMapsMenuProcessGroupProcess(Dictionary<String, Object> param)
                {
                    //Arma un Tree Vacio para que no de error.
                    Int64 _idProcess;
                    if (param.ContainsKey("IdParentProcess"))
                    {
                        _idProcess = Convert.ToInt64(param["IdParentProcess"]);
                    }
                    else
                    {
                        _idProcess = Convert.ToInt64(param["IdProcess"]);
                    }
                    Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _process = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess);
                    String _pageTitle = String.Empty;
                    if (!param.ContainsKey("PageTitle"))
                    {
                        _pageTitle = _process.LanguageOption.Title;
                    }
                    else
                    {
                        _pageTitle = Convert.ToString(param["PageTitle"]);
                    }
                    String _permissionType = String.Empty;
                    //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                    if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _permissionType = Common.Constants.PermissionManageName; }
                    else
                    { _permissionType = Common.Constants.PermissionViewName; }

                    RadTreeView _rdtvContextElementMapsByProcess = BuildElementMapsContent(Common.ConstantsEntitiesName.PF.ProcessGroupProcesses);
                    //Agrega el ContextMenu al treeview.
                    _rdtvContextElementMapsByProcess.ContextMenus.Add(BuildContextMenuContextElementProcessShortCut());
                    _rdtvContextElementMapsByProcess.Skin = "ContextElementMapsByProcess";
                    _rdtvContextElementMapsByProcess.Attributes.Add("IsTreeContextElement", "true");
                    _rdtvContextElementMapsByProcess.Attributes.Add("IdProcess", _process.IdProcess.ToString());
                    _rdtvContextElementMapsByProcess.OnClientContextMenuShowing = "onClientContextMenuContextElementProcessShowing";

                    //1° se carga el Proceso en el que estoy parado
                    String _keyValue = "IdProcess=" + _process.IdProcess.ToString();
                    RadTreeNode _node = SetElementMapsNodeTreeView(_process.LanguageOption.Title, _keyValue, String.Empty);
                    _node.PostBack = false;
                    _node.CssClass = GetClassNameProcessState(Resources.IconsByEntity.ProcessGroupProcesses, _process.State, false, false);
                    _node.Attributes.Add("WithSecurity", "true");
                    _node.Attributes.Add("SingleEntityName", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                    _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                    _node.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.PF.ProcessGroupNode);
                    _node.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.PF.ProcessGroupNodes);
                    _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                    _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                    _node.Attributes.Add("PermissionType", _permissionType);
                    BuildCustomAttributesForContextElementProcess(_node, _process);

                    _rdtvContextElementMapsByProcess.Nodes.Add(_node);
                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                    SetExpandMode(_node, Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, true, false);

                    //2° ahora carga los nodos al proceso recien cargado...
                    //Carga los registros en el Tree
                    RadTreeNode _rtn = _rdtvContextElementMapsByProcess.Nodes[0];
                    _rdtvContextElementMapsByProcess.OnClientMouseOver = "ShowPopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientMouseOut = "HidePopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientNodeClicked = "HidePopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientNodeExpanded = "HidePopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientNodeCollapsed = "HidePopUpProcces";

                    //Dictionary<Int64, ProcessTask> _childrenTask = ((ProcessGroup)_process).ChildrenTask;

                    //Damos de Alta 3 Nodos Root para meter ahi dentro las tareas...
                    #region Nodos Root Planes
                        //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                        if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        RadTreeNode _nodeMonitoringPlan = SetElementMapsNodeTreeView(Resources.Common.MonitoringPlan, _keyValue, String.Empty);
                        _nodeMonitoringPlan.Attributes.Add("EntityName", String.Empty);
                        //Usa el type para configurar el ContextInfo.
                        _nodeMonitoringPlan.Attributes.Add("EntityNameContextInfo", String.Empty);
                        _nodeMonitoringPlan.Attributes.Add("EntityNameContextElement", String.Empty);
                        _nodeMonitoringPlan.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        _nodeMonitoringPlan.Attributes.Add("PkCompost", _keyValue);
                        _nodeMonitoringPlan.Attributes.Add("PK_Compost", _keyValue);
                        _nodeMonitoringPlan.Attributes.Add("PermissionType", _permissionType);
                        _nodeMonitoringPlan.CssClass = "Folder";
                        _nodeMonitoringPlan.PostBack = false;
                        _rtn.Nodes.Add(_nodeMonitoringPlan);

                        RadTreeNode _nodeCalibrationPlan = SetElementMapsNodeTreeView(Resources.Common.CalibrationPlan, _keyValue, String.Empty);
                        _nodeCalibrationPlan.Attributes.Add("EntityName", String.Empty);
                        //Usa el type para configurar el ContextInfo.
                        _nodeCalibrationPlan.Attributes.Add("EntityNameContextInfo", String.Empty);
                        _nodeCalibrationPlan.Attributes.Add("EntityNameContextElement", String.Empty);
                        _nodeCalibrationPlan.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        _nodeCalibrationPlan.Attributes.Add("PkCompost", _keyValue);
                        _nodeCalibrationPlan.Attributes.Add("PermissionType", _permissionType);
                        _nodeCalibrationPlan.CssClass = "Folder"; 
                        _nodeCalibrationPlan.PostBack = false;
                        _rtn.Nodes.Add(_nodeCalibrationPlan);

                        RadTreeNode _nodeOperativePlan = SetElementMapsNodeTreeView(Resources.Common.OperativePlan, _keyValue, String.Empty);
                        _nodeOperativePlan.Attributes.Add("EntityName", String.Empty);
                        //Usa el type para configurar el ContextInfo.
                        _nodeOperativePlan.Attributes.Add("EntityNameContextInfo", String.Empty);
                        _nodeOperativePlan.Attributes.Add("EntityNameContextElement", String.Empty);
                        _nodeOperativePlan.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        _nodeOperativePlan.Attributes.Add("PkCompost", _keyValue);
                        _nodeOperativePlan.Attributes.Add("PK_Compost", _keyValue);
                        _nodeOperativePlan.Attributes.Add("PermissionType", _permissionType);
                        _nodeOperativePlan.CssClass = "Folder"; 
                        _nodeOperativePlan.PostBack = false;
                        _rtn.Nodes.Add(_nodeOperativePlan);


                        RadTreeNode _nodeAuditPlan = SetElementMapsNodeTreeView(Resources.Common.AuditPlan, _keyValue, String.Empty);
                        _nodeAuditPlan.Attributes.Add("EntityName", String.Empty);
                        //Usa el type para configurar el ContextInfo.
                        _nodeAuditPlan.Attributes.Add("EntityNameContextInfo", String.Empty);
                        _nodeAuditPlan.Attributes.Add("EntityNameContextElement", String.Empty);
                        _nodeAuditPlan.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        _nodeAuditPlan.Attributes.Add("PkCompost", _keyValue);
                        _nodeAuditPlan.Attributes.Add("PK_Compost", _keyValue);
                        _nodeAuditPlan.Attributes.Add("PermissionType", _permissionType);
                        _nodeAuditPlan.CssClass = "Folder";
                        _nodeAuditPlan.PostBack = false;
                        _rtn.Nodes.Add(_nodeAuditPlan);

                        _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;
                    #endregion

                    #region Calibration Plan
                    Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDeviceType> _measurementDeviceTypes = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDeviceType>();
                    Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDevice> _measurementDevices = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDevice>();
                    Dictionary<Int64, ProcessTaskCalibration> _processTaskCalibrations = new Dictionary<Int64, ProcessTaskCalibration>();

                    var _taskCalibration = from t in _process.TaskCalibrations.Values
                                        select t;
                    foreach (ProcessTask _item in _taskCalibration)
                    {
                        ProcessTaskCalibration _processTaskCalibration = (ProcessTaskCalibration)_item;
                        _measurementDeviceTypes.Add(_processTaskCalibration.MeasurementDevice.DeviceType.IdMeasurementDeviceType, _processTaskCalibration.MeasurementDevice.DeviceType);
                        _measurementDevices.Add(_processTaskCalibration.MeasurementDevice.IdMeasurementDevice, _processTaskCalibration.MeasurementDevice);
                        _processTaskCalibrations.Add(_processTaskCalibration.IdProcess, _processTaskCalibration);
                    }

                    var _lnqMDeviceType = from mdt in _measurementDeviceTypes.Values
                                          orderby mdt.LanguageOption.Name
                                          select mdt;
                    foreach (Condesus.EMS.Business.PA.Entities.MeasurementDeviceType _measurementDeviceType in _lnqMDeviceType)
                    {
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        _keyValue = "IdMeasurementDeviceType=" + _measurementDeviceType.IdMeasurementDeviceType.ToString();
                        RadTreeNode _nodeMeasurementDeviceType = SetElementMapsNodeTreeView(_measurementDeviceType.LanguageOption.Name, _keyValue, Common.ConstantsEntitiesName.PA.MeasurementDeviceTypes, Common.ConstantsEntitiesName.PA.MeasurementDeviceType, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                        _nodeMeasurementDeviceType.PostBack = false;
                        _nodeMeasurementDeviceType.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.MeasurementDeviceType);
                        //Usa el type para configurar el ContextInfo.
                        _nodeMeasurementDeviceType.Attributes.Add("EntityNameContextInfo", String.Empty);
                        _nodeMeasurementDeviceType.Attributes.Add("EntityNameContextElement", String.Empty);
                        _nodeMeasurementDeviceType.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        _nodeMeasurementDeviceType.Attributes.Add("PkCompost", _nodeMeasurementDeviceType.Value);
                        _nodeMeasurementDeviceType.CssClass = "Folder";

                        _nodeCalibrationPlan.Nodes.Add(_nodeMeasurementDeviceType);

                        //Ahora cargamos los Device
                        var _lnqMDevice = from md in _measurementDevices.Values
                                          where md.DeviceType.IdMeasurementDeviceType == _measurementDeviceType.IdMeasurementDeviceType
                                          orderby md.FullName
                                          select md;
                        foreach (Condesus.EMS.Business.PA.Entities.MeasurementDevice _measurementDevice in _lnqMDevice)
                        {
                            //Obtiene el permiso que tiene el usuario para esa organizacion.
                            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            _keyValue = "IdMeasurementDevice=" + _measurementDevice.IdMeasurementDevice.ToString()
                                + "&IdMeasurementDevice=" + _measurementDevice.DeviceType.IdMeasurementDeviceType.ToString();
                            RadTreeNode _nodeMeasurementDevice = SetElementMapsNodeTreeView(_measurementDevice.FullName, _keyValue, Common.ConstantsEntitiesName.PA.MeasurementDevices, Common.ConstantsEntitiesName.PA.MeasurementDevice, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                            //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                            _nodeMeasurementDevice.PostBack = false;
                            _nodeMeasurementDevice.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.MeasurementDevice);
                            //Usa el type para configurar el ContextInfo.
                            _nodeMeasurementDevice.Attributes.Add("EntityNameContextInfo", String.Empty);
                            _nodeMeasurementDevice.Attributes.Add("EntityNameContextElement", String.Empty);
                            _nodeMeasurementDevice.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                            _nodeMeasurementDevice.Attributes.Add("PkCompost", _nodeMeasurementDeviceType.Value);
                            _nodeMeasurementDevice.CssClass = "Folder";

                            _nodeMeasurementDeviceType.Nodes.Add(_nodeMeasurementDevice);

                            //Ahora cargamos las Tareas
                            var _lnqTaskCalibrations = from ptc in _processTaskCalibrations.Values
                                                       where ptc.MeasurementDevice.IdMeasurementDevice == _measurementDevice.IdMeasurementDevice
                                                       orderby ptc.LanguageOption.Title
                                                       select ptc;
                            foreach (ProcessTaskCalibration _itemPTC in _lnqTaskCalibrations)
                            {
                                //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                                if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                _keyValue = "IdTask=" + _itemPTC.IdProcess.ToString()
                                    + "& IdProcess=" + _itemPTC.Parent.IdProcess.ToString()
                                    + "&IdMeasurementDevice=" + _itemPTC.MeasurementDevice.IdMeasurementDevice.ToString();

                                RadTreeNode _nodeTaskCalibration = SetElementMapsNodeTreeView(_itemPTC.LanguageOption.Title, _keyValue, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.ProcessTask, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.Process, String.Empty, _permissionType);
                                //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                _nodeTaskCalibration.PostBack = true;
                                _nodeTaskCalibration.Attributes.Add("EntityName", _itemPTC.GetType().Name);
                                //Usa el type para configurar el ContextInfo.
                                _nodeTaskCalibration.Attributes.Add("EntityNameContextInfo", _itemPTC.GetType().Name);
                                _nodeTaskCalibration.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                _nodeTaskCalibration.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                _nodeTaskCalibration.Attributes.Add("PkCompost", _nodeTaskCalibration.Value);
                                _nodeTaskCalibration.CssClass = GetClassNameProcessState(_itemPTC.GetType().Name, _itemPTC.State, _itemPTC.ExecutionStatus, false);
                                BuildCustomAttributesForContextElementProcess(_nodeTaskCalibration, Common.ConstantsEntitiesName.PF.ProcessTasks, _itemPTC);

                                _nodeMeasurementDevice.Nodes.Add(_nodeTaskCalibration);
                                _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;
                            }
                        }

                    }

                    #endregion

                    #region Operative Plan
                    ArrayList _posts = new ArrayList();
                    Dictionary<Int64, ProcessTaskOperation> _processTaskOperations = new Dictionary<Int64, ProcessTaskOperation>();

                    var _taskOperations = from t in _process.TaskOperations.Values
                                          select t;
                    foreach (ProcessTaskOperation _item in _taskOperations)
                    {
                        //ProcessTaskOperation _processTaskOperation = (ProcessTaskOperation)_item;
                        var _operators = from op in _item.ExecutionPermissions()
                                         orderby op.Person.FullName
                                         select op;
                        foreach (Post _post in _operators)
                        {
                            Int64 _idGeographicArea = _post.JobTitle.GeographicArea.IdGeographicArea;
                            Int64 _idPosition = _post.JobTitle.Position.IdPosition;
                            Int64 _idFunctionalArea = _post.JobTitle.FunctionalArea.IdFunctionalArea;
                            Int64 _idOrganization = _post.JobTitle.Organization.IdOrganization;
                            Int64 _idPerson = _post.Person.IdPerson;
                            
                            String _pk = "IdGeographicArea=" + _idGeographicArea.ToString()
                                + "&IdPosition=" + _idPosition.ToString()
                                + "&IdFunctionalArea=" + _idFunctionalArea.ToString()
                                + "&IdOrganization=" + _idOrganization.ToString()
                                + "&IdPerson=" + _idPerson.ToString();
                            _posts.Add(_pk);
                        }

                        _processTaskOperations.Add(_item.IdProcess, _item);
                    }

                    foreach (String _operator in _posts)
                    {
                        Int64 _idGeographicArea = Convert.ToInt64(GetKeyValue(_operator, "IdGeographicArea"));
                        Int64 _idPosition = Convert.ToInt64(GetKeyValue(_operator, "IdPosition"));
                        Int64 _idFunctionalArea = Convert.ToInt64(GetKeyValue(_operator, "IdFunctionalArea"));
                        Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_operator, "IdOrganization"));
                        Int64 _idPerson = Convert.ToInt64(GetKeyValue(_operator, "IdPerson"));
                        //Construye el Post y lo agrega al List, que necesita el AddTask
                        Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                        Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_idPosition);
                        Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                        Condesus.EMS.Business.DS.Entities.FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        Condesus.EMS.Business.DS.Entities.GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);
                        Condesus.EMS.Business.DS.Entities.Post _post = _organization.Post(_jobTitle, _organization.Person(_idPerson));

                        String _operatorName = _organization.Person(_idPerson).FullName;

                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        _keyValue = "IdGeographicArea=" + _idGeographicArea.ToString()
                            + "&IdPosition=" + _idPosition.ToString()
                            + "&IdFunctionalArea=" + _idFunctionalArea.ToString()
                            + "&IdOrganization=" + _idOrganization.ToString()
                            + "&IdPerson=" + _idPerson.ToString();
                        RadTreeNode _nodeOperator = SetElementMapsNodeTreeView(_operatorName, _keyValue, Common.ConstantsEntitiesName.DS.Posts, Common.ConstantsEntitiesName.DS.Post, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                        _nodeOperator.PostBack = false;
                        _nodeOperator.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.MeasurementDeviceType);
                        //Usa el type para configurar el ContextInfo.
                        _nodeOperator.Attributes.Add("EntityNameContextInfo", String.Empty);
                        _nodeOperator.Attributes.Add("EntityNameContextElement", String.Empty);
                        _nodeOperator.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        _nodeOperator.Attributes.Add("PkCompost", _nodeOperator.Value);
                        _nodeOperator.CssClass = "Folder";

                        _nodeOperativePlan.Nodes.Add(_nodeOperator);

                        //Ahora cargamos las Tareas
                        var _lnqTaskOperations = from pto in _post.ProcessTaskOperator
                                                   where pto.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessTaskOperation
                                                   orderby pto.LanguageOption.Title
                                                   select pto;
                        foreach (ProcessTaskOperation _itemPTO in _lnqTaskOperations)
                        {
                            //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                            if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            _keyValue = "IdTask=" + _itemPTO.IdProcess.ToString()
                                + "& IdProcess=" + _itemPTO.Parent.IdProcess.ToString();

                            RadTreeNode _nodeTaskOperative = SetElementMapsNodeTreeView(_itemPTO.LanguageOption.Title, _keyValue, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.ProcessTask, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.Process, String.Empty, _permissionType);
                            //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                            _nodeTaskOperative.PostBack = true;
                            _nodeTaskOperative.Attributes.Add("EntityName", _itemPTO.GetType().Name);
                            //Usa el type para configurar el ContextInfo.
                            _nodeTaskOperative.Attributes.Add("EntityNameContextInfo", _itemPTO.GetType().Name);
                            _nodeTaskOperative.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _nodeTaskOperative.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                            _nodeTaskOperative.Attributes.Add("PkCompost", _nodeTaskOperative.Value);
                            _nodeTaskOperative.CssClass = GetClassNameProcessState(_itemPTO.GetType().Name, _itemPTO.State, _itemPTO.ExecutionStatus, false);
                            BuildCustomAttributesForContextElementProcess(_nodeTaskOperative, Common.ConstantsEntitiesName.PF.ProcessTasks, _itemPTO);

                            _nodeOperator.Nodes.Add(_nodeTaskOperative);
                            _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;
                        }
                    }

                    #endregion

                    #region Monitorin Plan

                        #region Carga los Facility Types
                            //Ordena los facility Types con LinQ
                            var _facilityTypeSorted = from ft in _process.FacilityTypesWhitMeasurements.Values
                                                      orderby ft.LanguageOption.Name ascending
                                                      select ft;
                            foreach (FacilityType _facilityType in _facilityTypeSorted)
                            {
                                //Obtiene el permiso que tiene el usuario para esa organizacion.
                                if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                _keyValue = "IdFacilityType=" + _facilityType.IdFacilityType.ToString()
                                    + "& IdProcess=" + _process.IdProcess.ToString();
                                RadTreeNode _nodeFacilityType = SetElementMapsNodeTreeView(_facilityType.LanguageOption.Name, _keyValue, Common.ConstantsEntitiesName.DS.FacilityTypes, Common.ConstantsEntitiesName.DS.FacilityType, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                                //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                _nodeFacilityType.PostBack = false;
                                _nodeFacilityType.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.FacilityType);
                                //Usa el type para configurar el ContextInfo.
                                _nodeFacilityType.Attributes.Add("EntityNameContextInfo", String.Empty);
                                _nodeFacilityType.Attributes.Add("EntityNameContextElement", String.Empty);
                                _nodeFacilityType.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                _nodeFacilityType.Attributes.Add("PkCompost", _nodeFacilityType.Value);
                                _nodeFacilityType.CssClass = "Folder";
                                _nodeFacilityType.ExpandMode = TreeNodeExpandMode.ServerSide;

                                _nodeMonitoringPlan.Nodes.Add(_nodeFacilityType);
                                //Asocia el Handler del Expand y click
                                _rdtvContextElementMapsByProcess.NodeExpand += new RadTreeViewEventHandler(rdtvContextElementMapsByProcess_NodeExpand);

                                #region Carga los Facilities
                                ////Revisar si este join hace lo mismo que el if que esta dentro del foreach.
                                //var _facilitiesSorted = from f in _facilityType.FacilitiesInMeasurements(_process).Values
                                //                        orderby f.LanguageOption.Name ascending
                                //                        select f;
                                ////Por cada Facility Type, carga los Facilities
                                //foreach (Site _facility in _facilitiesSorted)
                                //{   //Si el facility esta dentro del dictionary filtrado anteriormente, lo cargo, sino lo saltea.
                                //    _keyValue = "IdOrganization=" + _facility.Organization.IdOrganization.ToString()
                                //        + "& IdFacility=" + _facility.IdFacility.ToString()
                                //        + "& IdProcess=" + _process.IdProcess.ToString();
                                //    RadTreeNode _nodeFacility = SetElementMapsNodeTreeView(_facility.LanguageOption.Name, _keyValue, Common.ConstantsEntitiesName.DS.Facilities, Common.ConstantsEntitiesName.DS.Facility, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                                //    //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                //    _nodeFacility.PostBack = false;
                                //    _nodeFacility.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Facility);
                                //    //Usa el type para configurar el ContextInfo.
                                //    _nodeFacility.Attributes.Add("EntityNameContextInfo", String.Empty);
                                //    _nodeFacility.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                //    _nodeFacility.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                //    _nodeFacility.Attributes.Add("PkCompost", _nodeFacility.Value);
                                //    _nodeFacility.CssClass = "Folder";

                                //    _nodeFacilityType.Nodes.Add(_nodeFacility);
                                //    _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;

                                    #region Carga las Tareas
                                    ////Ordena las tareas con LinQ
                                    //var _tasks = from ptbf in _facility.TasksByProcess(_process).Values
                                    //             orderby ptbf.LanguageOption.Title ascending
                                    //             select ptbf;
                                    //foreach (ProcessTask _processTask in _tasks)
                                    //{
                                    //    //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                                    //    if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                    //    { _permissionType = Common.Constants.PermissionManageName; }
                                    //    else
                                    //    { _permissionType = Common.Constants.PermissionViewName; }

                                    //    _keyValue = "IdTask=" + _processTask.IdProcess.ToString()
                                    //        + "& IdProcess=" + _processTask.Parent.IdProcess.ToString();

                                    //    if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                                    //    {
                                    //        _keyValue += "&IdMeasurement=" + ((ProcessTaskMeasurement)_processTask).Measurement.IdMeasurement.ToString();
                                    //    }
                                    //    RadTreeNode _nodeTask = SetElementMapsNodeTreeView(_processTask.LanguageOption.Title, _keyValue, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.ProcessTask, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.Process, String.Empty, _permissionType);
                                    //    //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                    //    _nodeTask.PostBack = true;
                                    //    _nodeTask.Attributes.Add("EntityName", _processTask.GetType().Name);
                                    //    //Usa el type para configurar el ContextInfo.
                                    //    _nodeTask.Attributes.Add("EntityNameContextInfo", _processTask.GetType().Name);
                                    //    _nodeTask.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                    //    if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                                    //    {
                                    //        _nodeTask.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                                    //    }
                                    //    else
                                    //    {
                                    //        _nodeTask.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                    //    }
                                    //    _nodeTask.Attributes.Add("PkCompost", _nodeTask.Value);
                                    //    _nodeTask.CssClass = GetClassNameProcessState(_processTask.GetType().Name, _processTask.State);
                                    //    BuildCustomAttributesForContextElementProcess(_nodeTask, Common.ConstantsEntitiesName.PF.ProcessTasks, _processTask);

                                    //    _nodeFacility.Nodes.Add(_nodeTask);
                                    //}
                                    #endregion
                                //}
                                #endregion
                            }
                            #endregion

                        #region Carga las Tareas que estan fuera de los Facilities
                            RadTreeNode _nodeTaskWithoutFacility = SetElementMapsNodeTreeView(Resources.CommonListManage.TaskWithoutFacility, String.Empty, String.Empty, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, Common.Constants.PermissionViewName);
                            //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                            _nodeTaskWithoutFacility.PostBack = false;
                            _nodeTaskWithoutFacility.Attributes.Add("EntityName", String.Empty);
                            //Usa el type para configurar el ContextInfo.
                            _nodeTaskWithoutFacility.Attributes.Add("EntityNameContextInfo", String.Empty);
                            _nodeTaskWithoutFacility.Attributes.Add("EntityNameContextElement", String.Empty);
                            _nodeTaskWithoutFacility.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                            //_nodeTaskWithoutFacility.Attributes.Add("PkCompost", _nodeTaskWithoutFacility.Value);
                            _nodeTaskWithoutFacility.CssClass = "Folder";

                            _nodeMonitoringPlan.Nodes.Add(_nodeTaskWithoutFacility);
                            _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;

                            //Tengo que obtener todos las tareas de un process que no tienen facility asociados
                            var _tasksWithoutFacility = from ptwf in _process.TaskMeasurementWhitOutSite.Values
                                                        orderby ptwf.LanguageOption.Title ascending
                                                        select ptwf;
                            foreach (ProcessTask _processTask in _tasksWithoutFacility)
                            {
                                //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                                if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                _keyValue = "IdTask=" + _processTask.IdProcess.ToString()
                                    + "& IdProcess=" + _processTask.Parent.IdProcess.ToString();
                                if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                                {
                                    _keyValue += "&IdMeasurement=" + ((ProcessTaskMeasurement)_processTask).Measurement.IdMeasurement.ToString();
                                }
                                RadTreeNode _nodeTask = SetElementMapsNodeTreeView(_processTask.LanguageOption.Title, _keyValue, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.ProcessTask, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.Process, String.Empty, _permissionType);
                                //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                _nodeTask.PostBack = true;
                                _nodeTask.Attributes.Add("EntityName", _processTask.GetType().Name);
                                //Usa el type para configurar el ContextInfo.
                                _nodeTask.Attributes.Add("EntityNameContextInfo", _processTask.GetType().Name);
                                _nodeTask.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                                {
                                    _nodeTask.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                                }
                                else
                                {
                                    _nodeTask.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                }
                                _nodeTask.Attributes.Add("PkCompost", _nodeTask.Value);
                                _nodeTask.CssClass = GetClassNameProcessState(_processTask.GetType().Name, _processTask.State, _processTask.ExecutionStatus, ((ProcessTaskMeasurement)_processTask).MeasurementStatus);
                                BuildCustomAttributesForContextElementProcess(_nodeTask, Common.ConstantsEntitiesName.PF.ProcessTasks, _processTask);

                                _nodeTaskWithoutFacility.Nodes.Add(_nodeTask);
                            }
                            #endregion

                    #endregion


                    #region Audit Plan (Nodo Excel)
                        //AuditPlan
                        String[] _files;
                        String _path = Server.MapPath("~/AuditPlan");
                        //'Leyendo los archivos de la carpeta ‘C:\Musica’
                        _files = System.IO.Directory.GetFiles(_path +"\\", _idProcess.ToString() + "_*.xls*");

                        foreach (String _file in _files)
                        {
                            //'Lee el nombre del fichero sin su extension
                            String _strFile = System.IO.Path.GetFileNameWithoutExtension(_file).Replace(_idProcess.ToString() + "_", "");
                            _keyValue += "&FileNameAudit=" + System.IO.Path.GetFileName(_file);
                            String _pkCompost = "IdProcess=" + _idProcess.ToString() + "&FileNameAudit=" + System.IO.Path.GetFileName(_file);

                            RadTreeNode _nodeExcel = SetElementMapsNodeTreeView(_strFile, _keyValue, "Document");
                            //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                            _nodeExcel.PostBack = true;
                            _nodeExcel.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.AuditPlan);
                            //Usa el type para configurar el ContextInfo.
                            _nodeExcel.Attributes.Add("EntityNameContextInfo", String.Empty);
                            _nodeExcel.Attributes.Add("EntityNameContextElement", String.Empty);
                            _nodeExcel.Attributes.Add("URL", "~/AdministrationTools/ProcessesFramework/ShowUploadAuditPlanProperties.aspx");
                            _nodeExcel.Attributes.Add("PkCompost", _pkCompost);
                            //_nodeExcel.CssClass = "Document";

                            _nodeAuditPlan.Nodes.Add(_nodeExcel);
                        }
                     #endregion


                    //Finalmente tengo el tree armado y lo retorna.
                    return _rdtvContextElementMapsByProcess;
                }
                public RadTreeView BuildContextElementMapsMenuProcessGroupProcess_AL_14_06_11(Dictionary<String, Object> param)
                {
                    //Arma un Tree Vacio para que no de error.
                    Int64 _idProcess;
                    if (param.ContainsKey("IdParentProcess"))
                    {
                        _idProcess = Convert.ToInt64(param["IdParentProcess"]);
                    }
                    else
                    {
                        _idProcess = Convert.ToInt64(param["IdProcess"]);
                    }
                    Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _process = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess);
                    String _pageTitle = String.Empty;
                    if (!param.ContainsKey("PageTitle"))
                    {
                        _pageTitle = _process.LanguageOption.Title;
                    }
                    else
                    {
                        _pageTitle = Convert.ToString(param["PageTitle"]);
                    }
                    String _permissionType = String.Empty;
                    //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                    if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _permissionType = Common.Constants.PermissionManageName; }
                    else
                    { _permissionType = Common.Constants.PermissionViewName; }

                    RadTreeView _rdtvContextElementMapsByProcess = BuildElementMapsContent(Common.ConstantsEntitiesName.PF.ProcessGroupProcesses);
                    //Agrega el ContextMenu al treeview.
                    _rdtvContextElementMapsByProcess.ContextMenus.Add(BuildContextMenuContextElementProcessShortCut());
                    _rdtvContextElementMapsByProcess.Skin = "ContextElementMapsByProcess";
                    _rdtvContextElementMapsByProcess.Attributes.Add("IsTreeContextElement", "true");
                    _rdtvContextElementMapsByProcess.Attributes.Add("IdProcess", _process.IdProcess.ToString());
                    _rdtvContextElementMapsByProcess.OnClientContextMenuShowing = "onClientContextMenuContextElementProcessShowing";

                    //1° se carga el Proceso en el que estoy parado
                    String _keyValue = "IdProcess=" + _process.IdProcess.ToString();
                    RadTreeNode _node = SetElementMapsNodeTreeView(_process.LanguageOption.Title, _keyValue, String.Empty);
                    _node.PostBack = false;
                    _node.CssClass = GetClassNameProcessState(Resources.IconsByEntity.ProcessGroupProcesses, _process.State, false, false);
                    _node.Attributes.Add("WithSecurity", "true");
                    _node.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.PF.ProcessGroupNode);
                    _node.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.PF.ProcessGroupNodes);
                    _node.Attributes.Add("PermissionType", _permissionType);
                    BuildCustomAttributesForContextElementProcess(_node, _process);

                    _rdtvContextElementMapsByProcess.Nodes.Add(_node);
                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                    SetExpandMode(_node, Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, true, false);

                    //2° ahora carga los nodos al proceso recien cargado...
                    //Carga los registros en el Tree
                    RadTreeNode _rtn = _rdtvContextElementMapsByProcess.Nodes[0];
                    _rdtvContextElementMapsByProcess.OnClientMouseOver = "ShowPopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientMouseOut = "HidePopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientNodeClicked = "HidePopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientNodeExpanded = "HidePopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientNodeCollapsed = "HidePopUpProcces";

                    //Damos de Alta 3 Nodos Root para meter ahi dentro las tareas...
                    #region Nodos Root Planes
                    //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                    if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _permissionType = Common.Constants.PermissionManageName; }
                    else
                    { _permissionType = Common.Constants.PermissionViewName; }

                    RadTreeNode _nodeMonitoringPlan = SetElementMapsNodeTreeView(Resources.Common.MonitoringPlan, _keyValue, String.Empty);
                    _nodeMonitoringPlan.Attributes.Add("EntityName", String.Empty);
                    //Usa el type para configurar el ContextInfo.
                    _nodeMonitoringPlan.Attributes.Add("EntityNameContextInfo", String.Empty);
                    _nodeMonitoringPlan.Attributes.Add("EntityNameContextElement", String.Empty);
                    _nodeMonitoringPlan.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                    _nodeMonitoringPlan.Attributes.Add("PkCompost", _keyValue);
                    _nodeMonitoringPlan.Attributes.Add("PK_Compost", _keyValue);
                    _nodeMonitoringPlan.Attributes.Add("PermissionType", _permissionType);
                    _nodeMonitoringPlan.CssClass = "Folder";
                    _nodeMonitoringPlan.PostBack = false;
                    _rtn.Nodes.Add(_nodeMonitoringPlan);

                    RadTreeNode _nodeCalibrationPlan = SetElementMapsNodeTreeView(Resources.Common.CalibrationPlan, _keyValue, String.Empty);
                    _nodeCalibrationPlan.Attributes.Add("EntityName", String.Empty);
                    //Usa el type para configurar el ContextInfo.
                    _nodeCalibrationPlan.Attributes.Add("EntityNameContextInfo", String.Empty);
                    _nodeCalibrationPlan.Attributes.Add("EntityNameContextElement", String.Empty);
                    _nodeCalibrationPlan.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                    _nodeCalibrationPlan.Attributes.Add("PkCompost", _keyValue);
                    _nodeCalibrationPlan.Attributes.Add("PermissionType", _permissionType);
                    _nodeCalibrationPlan.CssClass = "Folder";
                    _nodeCalibrationPlan.PostBack = false;
                    _rtn.Nodes.Add(_nodeCalibrationPlan);

                    RadTreeNode _nodeOperativePlan = SetElementMapsNodeTreeView(Resources.Common.OperativePlan, _keyValue, String.Empty);
                    _nodeOperativePlan.Attributes.Add("EntityName", String.Empty);
                    //Usa el type para configurar el ContextInfo.
                    _nodeOperativePlan.Attributes.Add("EntityNameContextInfo", String.Empty);
                    _nodeOperativePlan.Attributes.Add("EntityNameContextElement", String.Empty);
                    _nodeOperativePlan.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                    _nodeOperativePlan.Attributes.Add("PkCompost", _keyValue);
                    _nodeOperativePlan.Attributes.Add("PK_Compost", _keyValue);
                    _nodeOperativePlan.Attributes.Add("PermissionType", _permissionType);
                    _nodeOperativePlan.CssClass = "Folder";
                    _nodeOperativePlan.PostBack = false;
                    _rtn.Nodes.Add(_nodeOperativePlan);

                    _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;
                    #endregion


                    Dictionary<Int64, Facility> _facilities = new Dictionary<Int64, Facility>();
                    Dictionary<Int64, FacilityType> _facilityTypes = new Dictionary<Int64, FacilityType>();
                    Dictionary<Int64, ProcessTask> _processTasks = new Dictionary<Int64, ProcessTask>();
                    Dictionary<Int64, Dictionary<Int64, ProcessTask>> _processTasksByFacility = new Dictionary<Int64, Dictionary<Int64, ProcessTask>>();

                    Dictionary<Int64, ProcessTask> _childrenTask = ((ProcessGroup)_process).ChildrenTask;
                    Int64 _idLastFacility = 0;



                    #region Calibration Plan
                    Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDeviceType> _measurementDeviceTypes = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDeviceType>();
                    Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDevice> _measurementDevices = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDevice>();
                    Dictionary<Int64, ProcessTaskCalibration> _processTaskCalibrations = new Dictionary<Int64, ProcessTaskCalibration>();

                    var _taskCalibration = from t in _childrenTask.Values
                                           where t.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessTaskCalibration
                                           select t;
                    foreach (ProcessTask _item in _taskCalibration)
                    {
                        ProcessTaskCalibration _processTaskCalibration = (ProcessTaskCalibration)_item;
                        _measurementDeviceTypes.Add(_processTaskCalibration.MeasurementDevice.DeviceType.IdMeasurementDeviceType, _processTaskCalibration.MeasurementDevice.DeviceType);
                        _measurementDevices.Add(_processTaskCalibration.MeasurementDevice.IdMeasurementDevice, _processTaskCalibration.MeasurementDevice);
                        _processTaskCalibrations.Add(_processTaskCalibration.IdProcess, _processTaskCalibration);
                    }

                    var _lnqMDeviceType = from mdt in _measurementDeviceTypes.Values
                                          orderby mdt.LanguageOption.Name
                                          select mdt;
                    foreach (Condesus.EMS.Business.PA.Entities.MeasurementDeviceType _measurementDeviceType in _lnqMDeviceType)
                    {
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        _keyValue = "IdMeasurementDeviceType=" + _measurementDeviceType.IdMeasurementDeviceType.ToString();
                        RadTreeNode _nodeMeasurementDeviceType = SetElementMapsNodeTreeView(_measurementDeviceType.LanguageOption.Name, _keyValue, Common.ConstantsEntitiesName.PA.MeasurementDeviceTypes, Common.ConstantsEntitiesName.PA.MeasurementDeviceType, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                        _nodeMeasurementDeviceType.PostBack = false;
                        _nodeMeasurementDeviceType.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.MeasurementDeviceType);
                        //Usa el type para configurar el ContextInfo.
                        _nodeMeasurementDeviceType.Attributes.Add("EntityNameContextInfo", String.Empty);
                        _nodeMeasurementDeviceType.Attributes.Add("EntityNameContextElement", String.Empty);
                        _nodeMeasurementDeviceType.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        _nodeMeasurementDeviceType.Attributes.Add("PkCompost", _nodeMeasurementDeviceType.Value);
                        _nodeMeasurementDeviceType.CssClass = "Folder";

                        _nodeCalibrationPlan.Nodes.Add(_nodeMeasurementDeviceType);

                        //Ahora cargamos los Device
                        var _lnqMDevice = from md in _measurementDevices.Values
                                          where md.DeviceType.IdMeasurementDeviceType == _measurementDeviceType.IdMeasurementDeviceType
                                          orderby md.FullName
                                          select md;
                        foreach (Condesus.EMS.Business.PA.Entities.MeasurementDevice _measurementDevice in _lnqMDevice)
                        {
                            //Obtiene el permiso que tiene el usuario para esa organizacion.
                            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            _keyValue = "IdMeasurementDevice=" + _measurementDevice.IdMeasurementDevice.ToString()
                                + "&IdMeasurementDevice=" + _measurementDevice.DeviceType.IdMeasurementDeviceType.ToString();
                            RadTreeNode _nodeMeasurementDevice = SetElementMapsNodeTreeView(_measurementDevice.FullName, _keyValue, Common.ConstantsEntitiesName.PA.MeasurementDevices, Common.ConstantsEntitiesName.PA.MeasurementDevice, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                            //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                            _nodeMeasurementDevice.PostBack = false;
                            _nodeMeasurementDevice.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.MeasurementDevice);
                            //Usa el type para configurar el ContextInfo.
                            _nodeMeasurementDevice.Attributes.Add("EntityNameContextInfo", String.Empty);
                            _nodeMeasurementDevice.Attributes.Add("EntityNameContextElement", String.Empty);
                            _nodeMeasurementDevice.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                            _nodeMeasurementDevice.Attributes.Add("PkCompost", _nodeMeasurementDeviceType.Value);
                            _nodeMeasurementDevice.CssClass = "Folder";

                            _nodeMeasurementDeviceType.Nodes.Add(_nodeMeasurementDevice);

                            //Ahora cargamos las Tareas
                            var _lnqTaskCalibrations = from ptc in _processTaskCalibrations.Values
                                                       where ptc.MeasurementDevice.IdMeasurementDevice == _measurementDevice.IdMeasurementDevice
                                                       orderby ptc.LanguageOption.Title
                                                       select ptc;
                            foreach (ProcessTaskCalibration _itemPTC in _lnqTaskCalibrations)
                            {
                                //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                                if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                _keyValue = "IdTask=" + _itemPTC.IdProcess.ToString()
                                    + "& IdProcess=" + _itemPTC.Parent.IdProcess.ToString()
                                    + "&IdMeasurementDevice=" + _itemPTC.MeasurementDevice.IdMeasurementDevice.ToString();

                                RadTreeNode _nodeTaskCalibration = SetElementMapsNodeTreeView(_itemPTC.LanguageOption.Title, _keyValue, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.ProcessTask, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.Process, String.Empty, _permissionType);
                                //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                _nodeTaskCalibration.PostBack = true;
                                _nodeTaskCalibration.Attributes.Add("EntityName", _itemPTC.GetType().Name);
                                //Usa el type para configurar el ContextInfo.
                                _nodeTaskCalibration.Attributes.Add("EntityNameContextInfo", _itemPTC.GetType().Name);
                                _nodeTaskCalibration.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                _nodeTaskCalibration.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                _nodeTaskCalibration.Attributes.Add("PkCompost", _nodeTaskCalibration.Value);
                                _nodeTaskCalibration.CssClass = GetClassNameProcessState(_itemPTC.GetType().Name, _itemPTC.State, _itemPTC.ExecutionStatus, false);
                                BuildCustomAttributesForContextElementProcess(_nodeTaskCalibration, Common.ConstantsEntitiesName.PF.ProcessTasks, _itemPTC);

                                _nodeMeasurementDevice.Nodes.Add(_nodeTaskCalibration);
                                _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;
                            }
                        }

                    }

                    #endregion

                    #region Operative Plan
                    ArrayList _posts = new ArrayList();
                    Dictionary<Int64, ProcessTaskOperation> _processTaskOperations = new Dictionary<Int64, ProcessTaskOperation>();

                    var _taskOperations = from t in _childrenTask.Values
                                          where t.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessTaskOperation
                                          select t;
                    foreach (ProcessTask _item in _taskOperations)
                    {
                        ProcessTaskOperation _processTaskOperation = (ProcessTaskOperation)_item;
                        var _operators = from op in _processTaskOperation.ExecutionPermissions()
                                         orderby op.Person.FullName
                                         select op;
                        foreach (Post _post in _operators)
                        {
                            Int64 _idGeographicArea = _post.JobTitle.GeographicArea.IdGeographicArea;
                            Int64 _idPosition = _post.JobTitle.Position.IdPosition;
                            Int64 _idFunctionalArea = _post.JobTitle.FunctionalArea.IdFunctionalArea;
                            Int64 _idOrganization = _post.JobTitle.Organization.IdOrganization;
                            Int64 _idPerson = _post.Person.IdPerson;

                            String _pk = "IdGeographicArea=" + _idGeographicArea.ToString()
                                + "&IdPosition=" + _idPosition.ToString()
                                + "&IdFunctionalArea=" + _idFunctionalArea.ToString()
                                + "&IdOrganization=" + _idOrganization.ToString()
                                + "&IdPerson=" + _idPerson.ToString();
                            _posts.Add(_pk);
                        }

                        _processTaskOperations.Add(_processTaskOperation.IdProcess, _processTaskOperation);
                    }

                    foreach (String _operator in _posts)
                    {
                        Int64 _idGeographicArea = Convert.ToInt64(GetKeyValue(_operator, "IdGeographicArea"));
                        Int64 _idPosition = Convert.ToInt64(GetKeyValue(_operator, "IdPosition"));
                        Int64 _idFunctionalArea = Convert.ToInt64(GetKeyValue(_operator, "IdFunctionalArea"));
                        Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_operator, "IdOrganization"));
                        Int64 _idPerson = Convert.ToInt64(GetKeyValue(_operator, "IdPerson"));
                        //Construye el Post y lo agrega al List, que necesita el AddTask
                        Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                        Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_idPosition);
                        Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                        Condesus.EMS.Business.DS.Entities.FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        Condesus.EMS.Business.DS.Entities.GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);
                        Condesus.EMS.Business.DS.Entities.Post _post = _organization.Post(_jobTitle, _organization.Person(_idPerson));

                        String _operatorName = _organization.Person(_idPerson).FullName;

                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        _keyValue = "IdGeographicArea=" + _idGeographicArea.ToString()
                            + "&IdPosition=" + _idPosition.ToString()
                            + "&IdFunctionalArea=" + _idFunctionalArea.ToString()
                            + "&IdOrganization=" + _idOrganization.ToString()
                            + "&IdPerson=" + _idPerson.ToString();
                        RadTreeNode _nodeOperator = SetElementMapsNodeTreeView(_operatorName, _keyValue, Common.ConstantsEntitiesName.DS.Posts, Common.ConstantsEntitiesName.DS.Post, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                        _nodeOperator.PostBack = false;
                        _nodeOperator.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.MeasurementDeviceType);
                        //Usa el type para configurar el ContextInfo.
                        _nodeOperator.Attributes.Add("EntityNameContextInfo", String.Empty);
                        _nodeOperator.Attributes.Add("EntityNameContextElement", String.Empty);
                        _nodeOperator.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        _nodeOperator.Attributes.Add("PkCompost", _nodeOperator.Value);
                        _nodeOperator.CssClass = "Folder";

                        _nodeOperativePlan.Nodes.Add(_nodeOperator);

                        //Ahora cargamos las Tareas
                        var _lnqTaskOperations = from pto in _post.ProcessTaskOperator
                                                 where pto.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessTaskOperation
                                                 orderby pto.LanguageOption.Title
                                                 select pto;
                        foreach (ProcessTaskOperation _itemPTO in _lnqTaskOperations)
                        {
                            //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                            if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            _keyValue = "IdTask=" + _itemPTO.IdProcess.ToString()
                                + "& IdProcess=" + _itemPTO.Parent.IdProcess.ToString();

                            RadTreeNode _nodeTaskOperative = SetElementMapsNodeTreeView(_itemPTO.LanguageOption.Title, _keyValue, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.ProcessTask, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.Process, String.Empty, _permissionType);
                            //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                            _nodeTaskOperative.PostBack = true;
                            _nodeTaskOperative.Attributes.Add("EntityName", _itemPTO.GetType().Name);
                            //Usa el type para configurar el ContextInfo.
                            _nodeTaskOperative.Attributes.Add("EntityNameContextInfo", _itemPTO.GetType().Name);
                            _nodeTaskOperative.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _nodeTaskOperative.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                            _nodeTaskOperative.Attributes.Add("PkCompost", _nodeTaskOperative.Value);
                            _nodeTaskOperative.CssClass = GetClassNameProcessState(_itemPTO.GetType().Name, _itemPTO.State, _itemPTO.ExecutionStatus, false);
                            BuildCustomAttributesForContextElementProcess(_nodeTaskOperative, Common.ConstantsEntitiesName.PF.ProcessTasks, _itemPTO);

                            _nodeOperator.Nodes.Add(_nodeTaskOperative);
                            _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;
                        }
                    }

                    #endregion


                    #region Monitorin Plan
                    Dictionary<Int64, Condesus.EMS.Business.PA.Entities.AccountingActivity> _activities = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.AccountingActivity>();
                    Dictionary<Int64, Condesus.EMS.Business.PA.Entities.AccountingScope> _scopes = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.AccountingScope>();

                    //RUBEN Ver esto, se va por TIMEOUT y no carga las tareas que tienen Facility.
                    //Tengo que obtener todos las tareas de un process que tienen facility asociado
                    var _taskWithSite = from t in _childrenTask.Values
                                        where t.Site != null
                                        && t.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement
                                        orderby t.Site.IdFacility ascending
                                        select t;
                    foreach (ProcessTask _processTask in _taskWithSite)
                    {
                        Facility _facility;
                        //Si es un Sector...entonces hay que buscar el facility
                        if (_processTask.Site.GetType().Name == "Sector")
                        {
                            Sector _sector = (Sector)_processTask.Site;
                            while (_sector.Parent.GetType().Name != "Facility")
                            {
                                _sector = (Sector)_sector.Parent;
                            }
                            _facility = (Facility)_sector.Parent;
                        }
                        else
                        {   //Es facility, entonces va directo....
                            _facility = (Facility)_processTask.Site;
                        }

                        if (_facility.IdFacility != _idLastFacility)
                        {
                            _processTasks = new Dictionary<Int64, ProcessTask>();
                        }
                        _idLastFacility = _facility.IdFacility;
                        if (!_facilities.ContainsKey(_facility.IdFacility))
                        {
                            _facilities.Add(_facility.IdFacility, _facility);
                        }
                        if (!_facilityTypes.ContainsKey(_facility.FacilityType.IdFacilityType))
                        {
                            _facilityTypes.Add(_facility.FacilityType.IdFacilityType, _facility.FacilityType);
                        }

                        //Si ya existe, lo borro, para luego cargarlo.
                        if (_processTasksByFacility.ContainsKey(_facility.IdFacility))
                        {
                            //Como el facility ya existe, entonces busco la coleccion de Processos y le agrego estos nuevos...
                            _processTasks = _processTasksByFacility[_facility.IdFacility];

                            //Igualmente borra este key para luego agregarlo con el nuevo proceso...
                            _processTasksByFacility.Remove(_facility.IdFacility);
                        }

                        //Agrega el task con el id del facility, para poder identificarlo.
                        _processTasks.Add(_processTask.IdProcess, _processTask);

                        //Ahora lo carga
                        _processTasksByFacility.Add(_facility.IdFacility, _processTasks);

                        ////Tambien cargamos las Actividades y el Scope...
                        ////Primero revisamos que tenga esta entidad, sino forzamos cero...
                        //if (((ProcessTaskMeasurement)_processTask).AccountingActivity != null)
                        //{
                        //    if (!_activities.ContainsKey(((ProcessTaskMeasurement)_processTask).AccountingActivity.IdActivity))
                        //    {
                        //        _activities.Add(((ProcessTaskMeasurement)_processTask).AccountingActivity.IdActivity, ((ProcessTaskMeasurement)_processTask).AccountingActivity);
                        //    }
                        //}
                        //else
                        //{
                        //    if (!_activities.ContainsKey(121))
                        //    {
                        //        Condesus.EMS.Business.PA.Entities.AccountingActivity _aa = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingActivity(121);
                        //        _activities.Add(_aa.IdActivity, _aa);
                        //    }
                        //}

                        //if (((ProcessTaskMeasurement)_processTask).AccountingScope != null)
                        //{
                        //    if (!_scopes.ContainsKey(((ProcessTaskMeasurement)_processTask).AccountingScope.IdScope))
                        //    {
                        //        _scopes.Add(((ProcessTaskMeasurement)_processTask).AccountingScope.IdScope, ((ProcessTaskMeasurement)_processTask).AccountingScope);
                        //    }
                        //}
                        //else
                        //{
                        //    if (!_scopes.ContainsKey(1))
                        //    {
                        //        Condesus.EMS.Business.PA.Entities.AccountingScope _as = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(1);
                        //        _scopes.Add(_as.IdScope, _as);
                        //    }
                        //}

                    }

                    //#region Carga los Scopes
                    //var _lnqScopes = from s in _scopes.Values
                    //                 orderby s.LanguageOption.Name ascending
                    //                 select s;
                    //foreach (Condesus.EMS.Business.PA.Entities.AccountingScope _scope in _lnqScopes)
                    //{
                    //    //Obtiene el permiso que tiene el usuario para esa organizacion.
                    //    if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    //    { _permissionType = Common.Constants.PermissionManageName; }
                    //    else
                    //    { _permissionType = Common.Constants.PermissionViewName; }

                    //    _keyValue = "IdScope=" + _scope.IdScope.ToString();
                    //    RadTreeNode _nodeScope = SetElementMapsNodeTreeView(_scope.LanguageOption.Name, _keyValue, Common.ConstantsEntitiesName.PA.AccountingScopes, Common.ConstantsEntitiesName.PA.AccountingScope, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                    //    //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                    //    _nodeScope.PostBack = false;
                    //    _nodeScope.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.AccountingScope);
                    //    //Usa el type para configurar el ContextInfo.
                    //    _nodeScope.Attributes.Add("EntityNameContextInfo", String.Empty);
                    //    _nodeScope.Attributes.Add("EntityNameContextElement", String.Empty);
                    //    _nodeScope.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                    //    _nodeScope.Attributes.Add("PkCompost", _nodeScope.Value);
                    //    _nodeScope.CssClass = "Folder";

                    //    _nodeMonitoringPlan.Nodes.Add(_nodeScope);
                    //#endregion
                    //#region Carga las Actividades
                    //    var _lnqActivities = from a in _activities.Values
                    //                 orderby a.LanguageOption.Name ascending
                    //                 select a;
                    //    foreach (Condesus.EMS.Business.PA.Entities.AccountingActivity _activity in _lnqActivities)
                    //    {
                    //        //Obtiene el permiso que tiene el usuario para esa organizacion.
                    //        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    //        { _permissionType = Common.Constants.PermissionManageName; }
                    //        else
                    //        { _permissionType = Common.Constants.PermissionViewName; }

                    //        _keyValue = "IdActivity=" + _activity.IdActivity.ToString();
                    //        RadTreeNode _nodeActivity= SetElementMapsNodeTreeView(_activity.LanguageOption.Name, _keyValue, Common.ConstantsEntitiesName.PA.AccountingActivities, Common.ConstantsEntitiesName.PA.AccountingActivity, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                    //        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                    //        _nodeActivity.PostBack = false;
                    //        _nodeActivity.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.AccountingActivity);
                    //        //Usa el type para configurar el ContextInfo.
                    //        _nodeActivity.Attributes.Add("EntityNameContextInfo", String.Empty);
                    //        _nodeActivity.Attributes.Add("EntityNameContextElement", String.Empty);
                    //        _nodeActivity.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                    //        _nodeActivity.Attributes.Add("PkCompost", _nodeActivity.Value);
                    //        _nodeActivity.CssClass = "Folder";

                    //        _nodeScope.Nodes.Add(_nodeActivity);
                    //    #endregion

                    #region Carga los Facility Types
                    //Ordena los facility Types con LinQ
                    var _facilityTypeSorted = from ft in _facilityTypes.Values
                                              orderby ft.LanguageOption.Name ascending
                                              select ft;
                    foreach (FacilityType _facilityType in _facilityTypeSorted)
                    {
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        _keyValue = "IdFacilityType=" + _facilityType.IdFacilityType.ToString()
                            + "& IdProcess=" + _process.IdProcess.ToString();
                        RadTreeNode _nodeFacilityType = SetElementMapsNodeTreeView(_facilityType.LanguageOption.Name, _keyValue, Common.ConstantsEntitiesName.DS.FacilityTypes, Common.ConstantsEntitiesName.DS.FacilityType, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                        _nodeFacilityType.PostBack = false;
                        _nodeFacilityType.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.FacilityType);
                        //Usa el type para configurar el ContextInfo.
                        _nodeFacilityType.Attributes.Add("EntityNameContextInfo", String.Empty);
                        _nodeFacilityType.Attributes.Add("EntityNameContextElement", String.Empty);
                        _nodeFacilityType.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        _nodeFacilityType.Attributes.Add("PkCompost", _nodeFacilityType.Value);
                        _nodeFacilityType.CssClass = "Folder";

                        _nodeMonitoringPlan.Nodes.Add(_nodeFacilityType);
                        _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;

                        #region Carga los Facilities
                        //Ordena los facilities con LinQ
                        //var _facilitiesSorted = from f in _facilityType.Facilities.Values
                        //                        orderby f.LanguageOption.Name ascending
                        //                        select f;

                        //Revisar si este join hace lo mismo que el if que esta dentro del foreach.
                        var _facilitiesSorted = from f in _facilityType.Facilities.Values
                                                join fFiltered in _facilities.Values
                                                on f.IdFacility equals fFiltered.IdFacility
                                                orderby f.LanguageOption.Name ascending
                                                select f;
                        //Por cada Facility Type, carga los Facilities
                        foreach (Facility _facility in _facilitiesSorted)
                        {   //Si el facility esta dentro del dictionary filtrado anteriormente, lo cargo, sino lo saltea.
                            //if (_facilities.ContainsKey(_facility.IdFacility))
                            //{
                            _keyValue = "IdOrganization=" + _facility.Organization.IdOrganization.ToString()
                                + "& IdFacility=" + _facility.IdFacility.ToString()
                                + "& IdProcess=" + _process.IdProcess.ToString();
                            RadTreeNode _nodeFacility = SetElementMapsNodeTreeView(_facility.LanguageOption.Name, _keyValue, Common.ConstantsEntitiesName.DS.Facilities, Common.ConstantsEntitiesName.DS.Facility, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                            //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                            _nodeFacility.PostBack = false;
                            _nodeFacility.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Facility);
                            //Usa el type para configurar el ContextInfo.
                            _nodeFacility.Attributes.Add("EntityNameContextInfo", String.Empty);
                            _nodeFacility.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _nodeFacility.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                            _nodeFacility.Attributes.Add("PkCompost", _nodeFacility.Value);
                            _nodeFacility.CssClass = "Folder";

                            _nodeFacilityType.Nodes.Add(_nodeFacility);
                            _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;

                            #region Carga las Tareas
                            //Ordena las tareas con LinQ
                            var _tasks = from ptbf in _processTasksByFacility[_facility.IdFacility].Values
                                         where ptbf.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement
                                         orderby ptbf.LanguageOption.Title ascending
                                         select ptbf;
                            foreach (ProcessTask _processTask in _tasks)
                            {
                                //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                                if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                _keyValue = "IdTask=" + _processTask.IdProcess.ToString()
                                    + "& IdProcess=" + _processTask.Parent.IdProcess.ToString();

                                if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                                {
                                    _keyValue += "&IdMeasurement=" + ((ProcessTaskMeasurement)_processTask).Measurement.IdMeasurement.ToString();
                                }
                                RadTreeNode _nodeTask = SetElementMapsNodeTreeView(_processTask.LanguageOption.Title, _keyValue, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.ProcessTask, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.Process, String.Empty, _permissionType);
                                //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                _nodeTask.PostBack = true;
                                _nodeTask.Attributes.Add("EntityName", _processTask.GetType().Name);
                                //Usa el type para configurar el ContextInfo.
                                _nodeTask.Attributes.Add("EntityNameContextInfo", _processTask.GetType().Name);
                                _nodeTask.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                                {
                                    _nodeTask.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                                }
                                else
                                {
                                    _nodeTask.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                }
                                _nodeTask.Attributes.Add("PkCompost", _nodeTask.Value);
                                _nodeTask.CssClass = GetClassNameProcessState(_processTask.GetType().Name, _processTask.State, _processTask.ExecutionStatus, ((ProcessTaskMeasurement)_processTask).MeasurementStatus);
                                BuildCustomAttributesForContextElementProcess(_nodeTask, Common.ConstantsEntitiesName.PF.ProcessTasks, _processTask);

                                _nodeFacility.Nodes.Add(_nodeTask);
                            }
                            #endregion
                            //}
                        }
                        #endregion
                    }
                    #endregion

                    #region Carga las Tareas que estan fuera de los Facilities
                    RadTreeNode _nodeTaskWithoutFacility = SetElementMapsNodeTreeView(Resources.CommonListManage.TaskWithoutFacility, String.Empty, String.Empty, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, Common.Constants.PermissionViewName);
                    //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                    _nodeTaskWithoutFacility.PostBack = false;
                    _nodeTaskWithoutFacility.Attributes.Add("EntityName", String.Empty);
                    //Usa el type para configurar el ContextInfo.
                    _nodeTaskWithoutFacility.Attributes.Add("EntityNameContextInfo", String.Empty);
                    _nodeTaskWithoutFacility.Attributes.Add("EntityNameContextElement", String.Empty);
                    _nodeTaskWithoutFacility.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                    //_nodeTaskWithoutFacility.Attributes.Add("PkCompost", _nodeTaskWithoutFacility.Value);
                    _nodeTaskWithoutFacility.CssClass = "Folder";

                    _nodeMonitoringPlan.Nodes.Add(_nodeTaskWithoutFacility);
                    _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;

                    //Tengo que obtener todos las tareas de un process que no tienen facility asociados
                    var _tasksWithoutFacility = from ptwf in _childrenTask.Values
                                                where ptwf.Site == null
                                                && ptwf.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement
                                                orderby ptwf.LanguageOption.Title ascending
                                                select ptwf;
                    foreach (ProcessTask _processTask in _tasksWithoutFacility)
                    {
                        //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                        if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        _keyValue = "IdTask=" + _processTask.IdProcess.ToString()
                            + "& IdProcess=" + _processTask.Parent.IdProcess.ToString();
                        if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                        {
                            _keyValue += "&IdMeasurement=" + ((ProcessTaskMeasurement)_processTask).Measurement.IdMeasurement.ToString();
                        }
                        RadTreeNode _nodeTask = SetElementMapsNodeTreeView(_processTask.LanguageOption.Title, _keyValue, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.ProcessTask, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.Process, String.Empty, _permissionType);
                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                        _nodeTask.PostBack = true;
                        _nodeTask.Attributes.Add("EntityName", _processTask.GetType().Name);
                        //Usa el type para configurar el ContextInfo.
                        _nodeTask.Attributes.Add("EntityNameContextInfo", _processTask.GetType().Name);
                        _nodeTask.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                        if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                        {
                            _nodeTask.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                        }
                        else
                        {
                            _nodeTask.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        }
                        _nodeTask.Attributes.Add("PkCompost", _nodeTask.Value);
                        _nodeTask.CssClass = GetClassNameProcessState(_processTask.GetType().Name, _processTask.State, _processTask.ExecutionStatus, ((ProcessTaskMeasurement)_processTask).MeasurementStatus);
                        BuildCustomAttributesForContextElementProcess(_nodeTask, Common.ConstantsEntitiesName.PF.ProcessTasks, _processTask);

                        _nodeTaskWithoutFacility.Nodes.Add(_nodeTask);
                    }
                    #endregion
                    //    }
                    //}


                    #endregion

                    //Finalmente tengo el tree armado y lo retorna.
                    return _rdtvContextElementMapsByProcess;
                }
                public RadTreeView BuildContextElementMapsMenuProcessGroupProcess_AL_02_06_11(Dictionary<String, Object> param)
                {
                    //Arma un Tree Vacio para que no de error.
                    Int64 _idProcess;
                    if (param.ContainsKey("IdParentProcess"))
                    {
                        _idProcess = Convert.ToInt64(param["IdParentProcess"]);
                    }
                    else
                    {
                        _idProcess = Convert.ToInt64(param["IdProcess"]);
                    }
                    Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _process = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess);
                    String _pageTitle = String.Empty;
                    if (!param.ContainsKey("PageTitle"))
                    {
                        _pageTitle = _process.LanguageOption.Title;
                    }
                    else
                    {
                        _pageTitle = Convert.ToString(param["PageTitle"]);
                    }
                    String _permissionType = String.Empty;
                    //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                    if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _permissionType = Common.Constants.PermissionManageName; }
                    else
                    { _permissionType = Common.Constants.PermissionViewName; }

                    RadTreeView _rdtvContextElementMapsByProcess = BuildElementMapsContent(Common.ConstantsEntitiesName.PF.ProcessGroupProcesses);
                    //Agrega el ContextMenu al treeview.
                    _rdtvContextElementMapsByProcess.ContextMenus.Add(BuildContextMenuContextElementProcessShortCut());
                    _rdtvContextElementMapsByProcess.Skin = "ContextElementMapsByProcess";
                    _rdtvContextElementMapsByProcess.Attributes.Add("IsTreeContextElement", "true");
                    _rdtvContextElementMapsByProcess.Attributes.Add("IdProcess", _process.IdProcess.ToString());
                    _rdtvContextElementMapsByProcess.OnClientContextMenuShowing = "onClientContextMenuContextElementProcessShowing";

                    //1° se carga el Proceso en el que estoy parado
                    String _keyValue = "IdProcess=" + _process.IdProcess.ToString();
                    RadTreeNode _node = SetElementMapsNodeTreeView(_process.LanguageOption.Title, _keyValue, String.Empty);
                    _node.PostBack = false;
                    _node.CssClass = GetClassNameProcessState(Resources.IconsByEntity.ProcessGroupProcesses, _process.State, false, false);
                    _node.Attributes.Add("WithSecurity", "true");
                    _node.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.PF.ProcessGroupNode);
                    _node.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.PF.ProcessGroupNodes);
                    _node.Attributes.Add("PermissionType", _permissionType);
                    BuildCustomAttributesForContextElementProcess(_node, _process);

                    _rdtvContextElementMapsByProcess.Nodes.Add(_node);
                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                    SetExpandMode(_node, Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, true, false);

                    //2° ahora carga los nodos al proceso recien cargado...
                    //Carga los registros en el Tree
                    RadTreeNode _rtn = _rdtvContextElementMapsByProcess.Nodes[0];
                    _rdtvContextElementMapsByProcess.OnClientMouseOver = "ShowPopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientMouseOut = "HidePopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientNodeClicked = "HidePopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientNodeExpanded = "HidePopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientNodeCollapsed = "HidePopUpProcces";

                    Dictionary<Int64, Facility> _facilities = new Dictionary<Int64, Facility>();
                    Dictionary<Int64, FacilityType> _facilityTypes = new Dictionary<Int64, FacilityType>();
                    Dictionary<Int64, ProcessTask> _processTasks = new Dictionary<Int64, ProcessTask>();
                    Dictionary<Int64, Dictionary<Int64, ProcessTask>> _processTasksByFacility = new Dictionary<Int64, Dictionary<Int64, ProcessTask>>();

                    Dictionary<Int64, ProcessTask> _childrenTask = ((ProcessGroup)_process).ChildrenTask;
                    Int64 _idLastFacility = 0;
                    //RUBEN Ver esto, se va por TIMEOUT y no carga las tareas que tienen Facility.
                    //Tengo que obtener todos las tareas de un process que tienen facility asociado
                    var _taskWithSite = from t in _childrenTask.Values
                                        where t.Site != null
                                        orderby t.Site.IdFacility ascending
                                        select t;
                    foreach (ProcessTask _processTask in _taskWithSite)
                    //foreach (ProcessTask _processTask in _childrenTask.Values)
                    {
                        Facility _facility;
                        //Si es un Sector...entonces hay que buscar el facility
                        if (_processTask.Site.GetType().Name == "Sector")
                        {
                            Sector _sector = (Sector)_processTask.Site;
                            while (_sector.Parent.GetType().Name != "Facility")
                            {
                                _sector = (Sector)_sector.Parent;
                            }
                            _facility = (Facility)_sector.Parent;
                        }
                        else
                        {   //Es facility, entonces va directo....
                            _facility = (Facility)_processTask.Site;
                        }

                        if (_facility.IdFacility != _idLastFacility)
                        {
                            _processTasks = new Dictionary<Int64, ProcessTask>();
                        }
                        _idLastFacility = _facility.IdFacility;
                        if (!_facilities.ContainsKey(_facility.IdFacility))
                        {
                            _facilities.Add(_facility.IdFacility, _facility);
                        }
                        if (!_facilityTypes.ContainsKey(_facility.FacilityType.IdFacilityType))
                        {
                            _facilityTypes.Add(_facility.FacilityType.IdFacilityType, _facility.FacilityType);
                        }

                        //Si ya existe, lo borro, para luego cargarlo.
                        if (_processTasksByFacility.ContainsKey(_facility.IdFacility))
                        {
                            //Como el facility ya existe, entonces busco la coleccion de Processos y le agrego estos nuevos...
                            _processTasks = _processTasksByFacility[_facility.IdFacility];

                            //Igualmente borra este key para luego agregarlo con el nuevo proceso...
                            _processTasksByFacility.Remove(_facility.IdFacility);
                        }

                        //Agrega el task con el id del facility, para poder identificarlo.
                        _processTasks.Add(_processTask.IdProcess, _processTask);
                        
                        //Ahora lo carga
                        _processTasksByFacility.Add(_facility.IdFacility, _processTasks);
                    }
                    #region Carga los Facility Types
                    //Ordena los facility Types con LinQ
                    var _facilityTypeSorted = from ft in _facilityTypes.Values
                                            orderby ft.LanguageOption.Name ascending
                                            select ft;
                    foreach (FacilityType _facilityType in _facilityTypeSorted)
                    {
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        _keyValue = "IdFacilityType=" + _facilityType.IdFacilityType.ToString()
                            + "& IdProcess=" + _process.IdProcess.ToString();
                        RadTreeNode _nodeFacilityType = SetElementMapsNodeTreeView(_facilityType.LanguageOption.Name, _keyValue, Common.ConstantsEntitiesName.DS.FacilityTypes, Common.ConstantsEntitiesName.DS.FacilityType, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                        _nodeFacilityType.PostBack = false;
                        _nodeFacilityType.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.FacilityType);
                        //Usa el type para configurar el ContextInfo.
                        _nodeFacilityType.Attributes.Add("EntityNameContextInfo", String.Empty);
                        _nodeFacilityType.Attributes.Add("EntityNameContextElement", String.Empty);
                        _nodeFacilityType.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        _nodeFacilityType.Attributes.Add("PkCompost", _nodeFacilityType.Value);
                        _nodeFacilityType.CssClass = "Folder";

                        _rtn.Nodes.Add(_nodeFacilityType);
                        _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;

                        #region Carga los Facilities
                        //Ordena los facilities con LinQ
                        //var _facilitiesSorted = from f in _facilityType.Facilities.Values
                        //                        orderby f.LanguageOption.Name ascending
                        //                        select f;

                        //Revisar si este join hace lo mismo que el if que esta dentro del foreach.
                        var _facilitiesSorted = from f in _facilityType.Facilities.Values
                                                join fFiltered in _facilities.Values
                                                on f.IdFacility equals fFiltered.IdFacility
                                                orderby f.LanguageOption.Name ascending
                                                select f;
                        //Por cada Facility Type, carga los Facilities
                        foreach (Facility _facility in _facilitiesSorted)
                        {   //Si el facility esta dentro del dictionary filtrado anteriormente, lo cargo, sino lo saltea.
                            //if (_facilities.ContainsKey(_facility.IdFacility))
                            //{
                            _keyValue = "IdOrganization=" + _facility.Organization.IdOrganization.ToString()
                                + "& IdFacility=" + _facility.IdFacility.ToString()
                                + "& IdProcess=" + _process.IdProcess.ToString();
                            RadTreeNode _nodeFacility = SetElementMapsNodeTreeView(_facility.LanguageOption.Name, _keyValue, Common.ConstantsEntitiesName.DS.Facilities, Common.ConstantsEntitiesName.DS.Facility, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                            //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                            _nodeFacility.PostBack = false;
                            _nodeFacility.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Facility);
                            //Usa el type para configurar el ContextInfo.
                            _nodeFacility.Attributes.Add("EntityNameContextInfo", String.Empty);
                            _nodeFacility.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _nodeFacility.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                            _nodeFacility.Attributes.Add("PkCompost", _nodeFacility.Value);
                            _nodeFacility.CssClass = "Folder";

                            _nodeFacilityType.Nodes.Add(_nodeFacility);
                            _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;

                            #region Carga las Tareas
                            //Ordena las tareas con LinQ
                            var _tasks = from ptbf in _processTasksByFacility[_facility.IdFacility].Values
                                         orderby ptbf.LanguageOption.Title ascending
                                         select ptbf;
                            foreach (ProcessTask _processTask in _tasks)
                            {
                                //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                                if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                _keyValue = "IdTask=" + _processTask.IdProcess.ToString()
                                    + "& IdProcess=" + _processTask.Parent.IdProcess.ToString();

                                if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                                {
                                    _keyValue += "&IdMeasurement=" + ((ProcessTaskMeasurement)_processTask).Measurement.IdMeasurement.ToString();
                                }
                                RadTreeNode _nodeTask = SetElementMapsNodeTreeView(_processTask.LanguageOption.Title, _keyValue, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.ProcessTask, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.Process, String.Empty, _permissionType);
                                //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                _nodeTask.PostBack = true;
                                _nodeTask.Attributes.Add("EntityName", _processTask.GetType().Name);
                                //Usa el type para configurar el ContextInfo.
                                _nodeTask.Attributes.Add("EntityNameContextInfo", _processTask.GetType().Name);
                                _nodeTask.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                                {
                                    _nodeTask.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                                }
                                else
                                {
                                    _nodeTask.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                }
                                _nodeTask.Attributes.Add("PkCompost", _nodeTask.Value);
                                _nodeTask.CssClass = GetClassNameProcessState(_processTask.GetType().Name, _processTask.State, _processTask.ExecutionStatus, false);
                                BuildCustomAttributesForContextElementProcess(_nodeTask, Common.ConstantsEntitiesName.PF.ProcessTasks, _processTask);

                                _nodeFacility.Nodes.Add(_nodeTask);
                            }
                            #endregion
                            //}
                        }
                        #endregion
                    }
                    #endregion

                    #region Carga las Tareas que estan fuera de los Facilities
                    RadTreeNode _nodeTaskWithoutFacility = SetElementMapsNodeTreeView(Resources.CommonListManage.TaskWithoutFacility, String.Empty, String.Empty, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, Common.Constants.PermissionViewName);
                    //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                    _nodeTaskWithoutFacility.PostBack = false;
                    _nodeTaskWithoutFacility.Attributes.Add("EntityName", String.Empty);
                    //Usa el type para configurar el ContextInfo.
                    _nodeTaskWithoutFacility.Attributes.Add("EntityNameContextInfo", String.Empty);
                    _nodeTaskWithoutFacility.Attributes.Add("EntityNameContextElement", String.Empty);
                    _nodeTaskWithoutFacility.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                    //_nodeTaskWithoutFacility.Attributes.Add("PkCompost", _nodeTaskWithoutFacility.Value);
                    _nodeTaskWithoutFacility.CssClass = "Folder";

                    _rtn.Nodes.Add(_nodeTaskWithoutFacility);
                    _rtn.ExpandMode = TreeNodeExpandMode.ClientSide;

                    //Tengo que obtener todos las tareas de un process que no tienen facility asociados
                    var _tasksWithoutFacility = from ptwf in _childrenTask.Values
                                           where ptwf.Site == null
                                           orderby ptwf.LanguageOption.Title ascending
                                           select ptwf;
                    foreach (ProcessTask _processTask in _tasksWithoutFacility)
                    {
                        //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                        if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        _keyValue = "IdTask=" + _processTask.IdProcess.ToString()
                            + "& IdProcess=" + _processTask.Parent.IdProcess.ToString();
                        RadTreeNode _nodeTask = SetElementMapsNodeTreeView(_processTask.LanguageOption.Title, _keyValue, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.ProcessTask, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.Process, String.Empty, _permissionType);
                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                        _nodeTask.PostBack = true;
                        _nodeTask.Attributes.Add("EntityName", _processTask.GetType().Name);
                        //Usa el type para configurar el ContextInfo.
                        _nodeTask.Attributes.Add("EntityNameContextInfo", _processTask.GetType().Name);
                        _nodeTask.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                        _nodeTask.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        _nodeTask.Attributes.Add("PkCompost", _nodeTask.Value);
                        _nodeTask.CssClass = GetClassNameProcessState(_processTask.GetType().Name, _processTask.State, _processTask.ExecutionStatus, false);
                        BuildCustomAttributesForContextElementProcess(_nodeTask, Common.ConstantsEntitiesName.PF.ProcessTasks, _processTask);

                        _nodeTaskWithoutFacility.Nodes.Add(_nodeTask);
                    }
                    #endregion

                    //Finalmente tengo el tree armado y lo retorna.
                    return _rdtvContextElementMapsByProcess;
                }
                public RadTreeView BuildContextElementMapsMenuProcessGroupProcessOLD(Dictionary<String, Object> param)
                {
                    //Arma un Tree Vacio para que no de error.
                    Int64 _idProcess;
                    if (param.ContainsKey("IdParentProcess"))
                    {
                        _idProcess = Convert.ToInt64(param["IdParentProcess"]);
                    }
                    else
                    {
                        _idProcess = Convert.ToInt64(param["IdProcess"]);
                    }
                    Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _process = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                    String _pageTitle = String.Empty;
                    if (!param.ContainsKey("PageTitle"))
                    {
                        _pageTitle = _process.LanguageOption.Title;
                    }
                    else
                    {
                        _pageTitle = Convert.ToString(param["PageTitle"]);
                    }
                    String _permissionType = String.Empty;
                    //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                    if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _permissionType = Common.Constants.PermissionManageName; }
                    else
                    { _permissionType = Common.Constants.PermissionViewName; }

                    RadTreeView _rdtvContextElementMapsByProcess = BuildElementMapsContent(Common.ConstantsEntitiesName.PF.ProcessGroupProcesses);
                    //Agrega el ContextMenu al treeview.
                    _rdtvContextElementMapsByProcess.ContextMenus.Add(BuildContextMenuContextElementProcessShortCut());
                    _rdtvContextElementMapsByProcess.Skin = "ContextElementMapsByProcess";
                    _rdtvContextElementMapsByProcess.Attributes.Add("IsTreeContextElement", "true");
                    _rdtvContextElementMapsByProcess.OnClientContextMenuShowing = "onClientContextMenuContextElementProcessShowing";

                    //1° se carga el Proceso en el que estoy parado
                    String _keyValue = "IdProcess=" + _process.IdProcess.ToString();
                    RadTreeNode _node = SetElementMapsNodeTreeView(_process.LanguageOption.Title, _keyValue, String.Empty);
                    _node.PostBack = false;
                    _node.CssClass = GetClassNameProcessState(Resources.IconsByEntity.ProcessGroupProcesses, _process.State, false, false);
                    _node.Attributes.Add("WithSecurity", "true");
                    _node.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.PF.ProcessGroupNode);
                    _node.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.PF.ProcessGroupNodes);
                    _node.Attributes.Add("PermissionType", _permissionType); 
                    BuildCustomAttributesForContextElementProcess(_node, _process);

                    _rdtvContextElementMapsByProcess.Nodes.Add(_node);
                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                    SetExpandMode(_node, Common.ConstantsEntitiesName.PF.ProcessGroupProcesses, true, false);
                    
                    //2° ahora carga los nodos al proceso recien cargado...
                    //Carga los registros en el Tree
                    RadTreeNode _rtn = _rdtvContextElementMapsByProcess.Nodes[0];
                    //base.LoadGenericTreeViewElementMap(ref _rtn, Common.ConstantsEntitiesName.PF.ProcessGroupNodes, Common.ConstantsEntitiesName.PF.ProcessTasks,
                    //    Common.ConstantsEntitiesName.PF.ProcessGroupNode, Common.ConstantsEntitiesName.PF.ProcessTask, Common.ConstantsEntitiesName.PF.ProcessGroupNodes, Common.ConstantsEntitiesName.PF.ProcessTasks);
                    //Asocia el Handler del Expand y click
                    
                    _rdtvContextElementMapsByProcess.NodeExpand += new RadTreeViewEventHandler(rdtvContextElementMapsByProcess_NodeExpand);
                    //_rdtvContextElementMapsByProcess.OnClientMouseOver = "OnClientMouseOver";
                    _rdtvContextElementMapsByProcess.OnClientMouseOver = "ShowPopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientMouseOut = "HidePopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientNodeClicked = "HidePopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientNodeExpanded = "HidePopUpProcces";
                    _rdtvContextElementMapsByProcess.OnClientNodeCollapsed = "HidePopUpProcces";

                    return _rdtvContextElementMapsByProcess;
                }
                private String FormatStateString(String state)
                {
                    String retString = state;

                    if (state == "OverDue")
                        retString = state.Substring(0, 1) + state.Substring(1, state.Length - 1).ToLower();

                    return retString;
                }

                private String GetClassNameProcessState(String cssNameEntity, String processState, Boolean processExecutionStatus, Boolean measurementStatus)
                {
                    //Si el estado de ejecuciones esta en Falso, pongo rojo, como OverDue
                    //SI es una excepcion de la medicion (fuera de rango) tambien pongo rojo como overdue
                    if ((processExecutionStatus) || (measurementStatus))
                    {
                        switch (cssNameEntity)
                        {
                            case Common.ConstantsEntitiesName.PF.ProcessGroupProcesses:
                                return Common.ConstantsStyleClassName.cssNameProcessGroupProcessesOverdue;
                            case Common.ConstantsEntitiesName.PF.ProcessGroupNodes:
                                return Common.ConstantsStyleClassName.cssNameProcessNodeOverdue;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskCalibration:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskCalibrationOverdue;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskOperation:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskOperationOverdue;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskMeasurementOverdue;
                        }
                    }
                    
                    //Sigo con el resto en forma normal
                    //Hago con if, porque el switch me pide constante y los resources no lo son.
                    if (processState == Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Finished)
                    {
                        switch (cssNameEntity)
                        {
                            case Common.ConstantsEntitiesName.PF.ProcessGroupProcesses:
                                return Common.ConstantsStyleClassName.cssNameProcessGroupProcessesFinished;
                            case Common.ConstantsEntitiesName.PF.ProcessGroupNodes:
                                return Common.ConstantsStyleClassName.cssNameProcessNodeFinished;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskCalibration:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskCalibrationFinished;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskOperation:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskOperationFinished;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskMeasurementFinished;
                        }
                    }
                    if (processState == Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_OverDue)
                    {
                        switch (cssNameEntity)
                        {
                            case Common.ConstantsEntitiesName.PF.ProcessGroupProcesses:
                                return Common.ConstantsStyleClassName.cssNameProcessGroupProcessesOverdue;
                            case Common.ConstantsEntitiesName.PF.ProcessGroupNodes:
                                return Common.ConstantsStyleClassName.cssNameProcessNodeOverdue;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskCalibration:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskCalibrationOverdue;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskOperation:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskOperationOverdue;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskMeasurementOverdue;
                        }
                    }
                    if (processState == Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Planned)
                    {
                        switch (cssNameEntity)
                        {
                            case Common.ConstantsEntitiesName.PF.ProcessGroupProcesses:
                                return Common.ConstantsStyleClassName.cssNameProcessGroupProcessesPlanned;
                            case Common.ConstantsEntitiesName.PF.ProcessGroupNodes:
                                return Common.ConstantsStyleClassName.cssNameProcessNodePlanned;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskCalibration:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskCalibrationPlanned;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskOperation:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskOperationPlanned;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskMeasurementPlanned;
                        }
                    }
                    if (processState == Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Working)
                    {
                        switch (cssNameEntity)
                        {
                            case Common.ConstantsEntitiesName.PF.ProcessGroupProcesses:
                                return Common.ConstantsStyleClassName.cssNameProcessGroupProcessesWorking;
                            case Common.ConstantsEntitiesName.PF.ProcessGroupNodes:
                                return Common.ConstantsStyleClassName.cssNameProcessNodeWorking;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskCalibration:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskCalibrationWorking;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskOperation:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskOperationWorking;
                            case Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement:
                                return Common.ConstantsStyleClassName.cssNameProcessTaskMeasurementWorking;
                        }
                    }

                    return String.Empty;
                }
                private void BuildCustomAttributesForContextElementProcess(RadTreeNode node, Condesus.EMS.Business.PF.Entities.Process process)
                {
                    node.Attributes.Add("Identity", process.IdProcess.ToString());
                    node.Attributes.Add("ProcessType", process.GetType().Name);
                    
                    node.Attributes.Add("TitleNode", Common.Functions.ReplaceIndexesTags(process.LanguageOption.Title));
                    node.Attributes.Add("StartDate", process.StartDate.ToString());
                    node.Attributes.Add("EndDate", process.EndDate.ToString());
                    node.Attributes.Add("Duration", String.Empty);
                    node.Attributes.Add("State", FormatStateString(process.State.ToString()));    // FormatStateString(row["State"].ToString());
                    node.Attributes.Add("Completed", process.Completed.ToString());
                    node.Attributes.Add("Result", process.Result.ToString());

                    //node.Attributes["MeasurementDevice"] = row["MeasurementDevice"].ToString();
                    //node.Attributes["Indicator"] = row["Indicator"].ToString();
                }
                private void BuildCustomAttributesForContextElementProcess(RadTreeNode node, String entityID, DataRow rows)
                {
                    switch (entityID)
                    {
                        case Common.ConstantsEntitiesName.PF.ProcessGroupNodes:
                            node.Attributes.Add("ProcessType", entityID);
                            break;

                        case Common.ConstantsEntitiesName.PF.ProcessTasks:
                            node.Attributes.Add("ProcessType", rows["TaskType"].ToString());
                            if (rows["TaskType"].ToString() == Resources.IconsByEntity.ProcessTaskMeasurement)
                            {
                                node.Attributes.Add("Measurement", rows["Measurement"].ToString());
                                node.Attributes.Add("Indicator", rows["Indicator"].ToString());

                                Int64 _idProcess = Convert.ToInt64(rows["IdProcess"].ToString());
                                Condesus.EMS.Business.PA.Entities.Measurement _measurement = ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).Measurement;
                                node.Attributes.Add("MeasurementUnit", Common.Functions.ReplaceIndexesTags(_measurement.MeasurementUnit.LanguageOption.Name));

                                node.Attributes.Add("Frequency", Resources.Common.WordFrequency + " " + _measurement.Frequency.ToString() + " " + EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(_measurement.TimeUnitFrequency).LanguageOption.Name);

                                if (((ProcessTaskMeasurement)_measurement.ProcessTask).MeasurementStatus)
                                {
                                    node.Attributes.Add("OutOfRange", Resources.Common.True);
                                }
                                else
                                {
                                    node.Attributes.Add("OutOfRange", Resources.Common.False);
                                }

                            }
                            else
                            {
                                if (rows["TaskType"].ToString() == Resources.IconsByEntity.ProcessTaskCalibration)
                                {
                                    node.Attributes.Add("MeasurementDevice", rows["MeasurementDevice"].ToString());
                                }
                            }
                            break;
                    }


                    node.Attributes.Add("TitleNode", rows["Title"].ToString());
                    node.Attributes.Add("StartDate", rows["StartDate"].ToString());
                    node.Attributes.Add("EndDate", rows["EndDate"].ToString());
                    node.Attributes.Add("Duration", rows["Duration"].ToString());
                    node.Attributes.Add("State", FormatStateString(rows["State"].ToString()));
                    node.Attributes.Add("Completed", rows["Completed"].ToString());
                    node.Attributes.Add("Result", rows["Result"].ToString());

                }
                private void BuildCustomAttributesForContextElementProcess(RadTreeNode node, String entityID, ProcessTask _processTask)
                {
                    String _taskType = _processTask.GetType().Name;
                    switch (entityID)
                    {
                        case Common.ConstantsEntitiesName.PF.ProcessGroupNodes:
                            node.Attributes.Add("ProcessType", entityID);
                            break;

                        case Common.ConstantsEntitiesName.PF.ProcessTasks:
                            node.Attributes.Add("ProcessType", _taskType);
                            if (_taskType == Resources.IconsByEntity.ProcessTaskMeasurement)
                            {
                                Condesus.EMS.Business.PA.Entities.Measurement _measurement = ((ProcessTaskMeasurement)_processTask).Measurement;
                                node.Attributes.Add("Measurement", Common.Functions.ReplaceIndexesTags(_measurement.LanguageOption.Name));
                                node.Attributes.Add("Indicator", Common.Functions.ReplaceIndexesTags(_measurement.Indicator.LanguageOption.Name));
                                node.Attributes.Add("MeasurementUnit", Common.Functions.ReplaceIndexesTags(_measurement.MeasurementUnit.LanguageOption.Name));
                                node.Attributes.Add("Frequency", Resources.Common.WordFrequency + " " + _measurement.Frequency.ToString() + " " + EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(_measurement.TimeUnitFrequency).LanguageOption.Name);

                                if (((ProcessTaskMeasurement)_processTask).MeasurementStatus)
                                {
                                    node.Attributes.Add("OutOfRange", Resources.Common.True);
                                }
                                else
                                {
                                    node.Attributes.Add("OutOfRange", Resources.Common.False);
                                }
                            }
                            else
                            {
                                if (_taskType == Resources.IconsByEntity.ProcessTaskCalibration)
                                {
                                    node.Attributes.Add("MeasurementDevice", Common.Functions.ReplaceIndexesTags(((ProcessTaskCalibration)_processTask).MeasurementDevice.FullName));
                                }
                            }
                            if (_processTask.ExecutionStatus)
                            {
                                node.Attributes.Add("ExecutionStatus", Resources.CommonListManage.OverDueTasks);
                            }
                            else
                            {
                                node.Attributes.Add("ExecutionStatus", _processTask.State);
                            }


                            break;
                    }


                    node.Attributes.Add("TitleNode", _processTask.LanguageOption.Title);
                    node.Attributes.Add("StartDate", _processTask.StartDate.ToString());
                    node.Attributes.Add("EndDate", _processTask.EndDate.ToString());
                    node.Attributes.Add("Duration", _processTask.Duration.ToString() + " " + EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(_processTask.TimeUnitDuration).LanguageOption.Name);
                    node.Attributes.Add("State", FormatStateString(_processTask.State));
                    node.Attributes.Add("Completed", _processTask.Completed.ToString());
                    node.Attributes.Add("Result", _processTask.Result);

                }

                #region Events
                    /// <summary>
                    /// Evento para el Expand del Combo con Tree pero ElementMaps
                    /// </summary>
                    /// <param name="sender"></param>
                    /// <param name="e"></param>
                    protected void rdtvContextElementMapsByProcess_NodeExpand(object sender, RadTreeNodeEventArgs e)
                    {
                        //Limpio los hijos, para no duplicar al abrir y cerrar.
                        e.Node.Nodes.Clear();
                        Dictionary<String, Object> _params = new Dictionary<String, Object>();
                        _params = GetKeyValues(e.Node.Value);
                        String _keyValue = String.Empty;
                        String _permissionType = String.Empty;
                        Int64 _idProcess = 0;
                        ProcessGroupProcess _process = null;

                        switch (e.Node.Attributes["EntityName"])
                        {
                            case Common.ConstantsEntitiesName.DS.FacilityType:
                                //Obtiene el permiso que tiene el usuario para esa organizacion.
                                if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                _idProcess = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdProcess"));
                                _process = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess);
                                Int64 _idFacilityType = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdFacilityType"));
                                FacilityType _facilityType = EMSLibrary.User.GeographicInformationSystem.FacilityType(_idFacilityType);
                                //Revisar si este join hace lo mismo que el if que esta dentro del foreach.
                                var _facilitiesSorted = from f in _facilityType.FacilitiesInMeasurements(_process).Values
                                                        orderby f.LanguageOption.Name ascending
                                                        select f;
                                //Por cada Facility Type, carga los Facilities
                                foreach (Site _facility in _facilitiesSorted)
                                {   //Si el facility esta dentro del dictionary filtrado anteriormente, lo cargo, sino lo saltea.
                                    String _entityName = String.Empty;
                                    String _entityId = String.Empty;
                                    String _siteKeyValue = String.Empty;
                                    if (_facility.GetType().Name == Common.ConstantsEntitiesName.DS.Facility)
                                    {
                                        _entityName = Common.ConstantsEntitiesName.DS.Facility;
                                        _entityId = Common.ConstantsEntitiesName.DS.Facilities;
                                        _siteKeyValue = "& IdFacility=" + _facility.IdFacility.ToString();
                                    }
                                    else
                                    {
                                        _entityName = Common.ConstantsEntitiesName.DS.Sector;
                                        _entityId = Common.ConstantsEntitiesName.DS.Sectors;
                                        _siteKeyValue = "& IdFacility=" + ((Sector)_facility).Parent.IdFacility.ToString()
                                            + "&IdSector=" + _facility.IdFacility.ToString();
                                    }
                                    _keyValue = "IdOrganization=" + _facility.Organization.IdOrganization.ToString()
                                        + "& IdProcess=" + _process.IdProcess.ToString()
                                        + _siteKeyValue;
                                    RadTreeNode _nodeFacility = SetElementMapsNodeTreeView(_facility.LanguageOption.Name, _keyValue, _entityId, _entityName, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty, _permissionType);
                                    //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                    _nodeFacility.PostBack = false;
                                    _nodeFacility.Attributes.Add("EntityName", _entityName);
                                    //Usa el type para configurar el ContextInfo.
                                    _nodeFacility.Attributes.Add("EntityNameContextInfo", String.Empty);
                                    _nodeFacility.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                    _nodeFacility.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                    _nodeFacility.Attributes.Add("PkCompost", _nodeFacility.Value);
                                    _nodeFacility.CssClass = "Folder";
                                    _nodeFacility.ExpandMode = TreeNodeExpandMode.ServerSide;

                                    e.Node.Nodes.Add(_nodeFacility);
                                }

                                break;

                            case Common.ConstantsEntitiesName.DS.Facility:
                            case Common.ConstantsEntitiesName.DS.Sector:
                                //Obtiene el permiso que tiene el usuario para esa organizacion.
                                if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                _idProcess = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdProcess"));
                                Int64 _idSite = 0;
                                if (e.Node.Value.Contains("IdSector"))
                                {
                                    _idSite = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdSector"));
                                }
                                else
                                {
                                    _idSite = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdFacility"));
                                }
                                _process = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess);
                                Site _site = EMSLibrary.User.GeographicInformationSystem.Site(_idSite);

                                //if (_site.TasksByProcess(_process).Count == 0)
                                //{
                                //    _site = EMSLibrary.User.GeographicInformationSystem.Site(Convert.ToInt64(GetKeyValue(e.Node.Value, "IdSector")));
                                //}
                                //Ordena las tareas con LinQ
                                var _tasks = from ptbf in _site.TasksByProcess(_process).Values
                                             orderby ptbf.LanguageOption.Title ascending
                                             select ptbf;
                                foreach (ProcessTask _processTask in _tasks)
                                {
                                    //Obtiene el permiso que tiene el usuario para ese proceso, tengo que castearlo al ProcessGroupProcess.
                                    if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                    { _permissionType = Common.Constants.PermissionManageName; }
                                    else
                                    { _permissionType = Common.Constants.PermissionViewName; }

                                    _keyValue = "IdTask=" + _processTask.IdProcess.ToString()
                                        + "& IdProcess=" + _processTask.Parent.IdProcess.ToString();

                                    if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                                    {
                                        _keyValue += "&IdMeasurement=" + ((ProcessTaskMeasurement)_processTask).Measurement.IdMeasurement.ToString();
                                    }
                                    RadTreeNode _nodeTask = SetElementMapsNodeTreeView(_processTask.LanguageOption.Title, _keyValue, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.ProcessTask, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PF.ProcessTasks, Common.ConstantsEntitiesName.PF.Process, String.Empty, _permissionType);
                                    //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                    _nodeTask.PostBack = true;
                                    _nodeTask.Attributes.Add("EntityName", _processTask.GetType().Name);
                                    //Usa el type para configurar el ContextInfo.
                                    _nodeTask.Attributes.Add("EntityNameContextInfo", _processTask.GetType().Name);
                                    _nodeTask.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                    if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                                    {
                                        _nodeTask.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                                    }
                                    else
                                    {
                                        _nodeTask.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                    }
                                    _nodeTask.Attributes.Add("PkCompost", _nodeTask.Value);
                                    Boolean _measurementStatus = false;
                                    if (_processTask.GetType().Name == Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement)
                                    {
                                        _measurementStatus = ((ProcessTaskMeasurement)_processTask).MeasurementStatus;
                                    }
                                    _nodeTask.CssClass = GetClassNameProcessState(_processTask.GetType().Name, _processTask.State, _processTask.ExecutionStatus, _measurementStatus);
                                    BuildCustomAttributesForContextElementProcess(_nodeTask, Common.ConstantsEntitiesName.PF.ProcessTasks, _processTask);

                                    e.Node.Nodes.Add(_nodeTask);
                                }
                                break;
                        }

                    }
                #endregion

            #endregion

            #region Indicator
                public RadTreeView BuildContextElementMapsMenuIndicator(Dictionary<String, Object> param)
                {
                    //Arma un Tree Vacio para que no de error.
                    Int64 _idIndicator = 0;
                    if (param.ContainsKey("IdIndicator"))
                    {
                        _idIndicator = Convert.ToInt64(param["IdIndicator"]);
                    }
                    Condesus.EMS.Business.PA.Entities.Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator);
                    String _pageTitle = String.Empty;
                    if (!param.ContainsKey("PageTitle"))
                    {
                        _pageTitle = Common.Functions.ReplaceIndexesTags(_indicator.LanguageOption.Name);
                    }
                    else
                    {
                        _pageTitle = Convert.ToString(param["PageTitle"]);
                    }

                    RadTreeView _rdtvContextElementMapsByIndicator = BuildElementMapsContent(Common.ConstantsEntitiesName.PA.Indicators);
                    //Agrega el ContextMenu al treeview.
                    _rdtvContextElementMapsByIndicator.ContextMenus.Add(BuildContextMenuContextElementShortCut());
                    _rdtvContextElementMapsByIndicator.Skin = "EMS";    // "ContextElementMapsByIndicator";
                    _rdtvContextElementMapsByIndicator.Attributes.Add("IsTreeContextElement", "true");
                    _rdtvContextElementMapsByIndicator.OnClientContextMenuShowing = "onClientContextMenuContextElementShowing";

                    //1° se carga el Proceso en el que estoy parado
                    String _keyValue = "IdIndicator=" + _idIndicator.ToString();
                    RadTreeNode _node = SetElementMapsNodeTreeView(_indicator.LanguageOption.Name, _keyValue, String.Empty);
                    _node.PostBack = false;
                    _node.CssClass = "Indicator";
                    _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Indicator);
                    _node.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.PA.ParameterGroup);
                    _node.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.PA.ParameterGroups);
                    _node.Attributes.Add("WithSecurity", "true");
                    _rdtvContextElementMapsByIndicator.Nodes.Add(_node);
                    //Aca verifica si este indicator, tiene parameterGroup como hijos.
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params.Add("IdIndicator", _idIndicator);
                    if (HasChildren("IndicatorsHasParameterGroup", _params))
                        { _node.ExpandMode = TreeNodeExpandMode.ServerSide; }

                    //2° ahora carga los nodos al proceso recien cargado...
                    //Carga los registros en el Tree
                    RadTreeNode _rtn = _rdtvContextElementMapsByIndicator.Nodes[0];
                    //Asocia el Handler del Expand y click
                    _rdtvContextElementMapsByIndicator.NodeExpand += new RadTreeViewEventHandler(rdtvContextElementMapsByIndicator_NodeExpand);

                    return _rdtvContextElementMapsByIndicator;
                }

                #region Events
                    /// <summary>
                    /// Evento para el Expand del Combo con Tree pero ElementMaps
                    /// </summary>
                    /// <param name="sender"></param>
                    /// <param name="e"></param>
                    protected void rdtvContextElementMapsByIndicator_NodeExpand(object sender, RadTreeNodeEventArgs e)
                    {
                        //En este Expand, se debe primero expandir de un indicador al parameterGroup
                        //Del parameterGroup al Parameter
                        //Del parameter al Range.

                        //Limpio los hijos, para no duplicar al abrir y cerrar.
                        e.Node.Nodes.Clear();
                        Dictionary<String, Object> _params = new Dictionary<String, Object>();
                        _params = GetKeyValues(e.Node.Value);

                        String _entityExpand = e.Node.Attributes["EntityName"].ToString();
                        switch (_entityExpand)
                        {
                            case Common.ConstantsEntitiesName.PA.Indicator:
                                //Si el expand es sobre el Indicator, entonces, muestro sus parameterGroups.
                                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                                BuildGenericDataTable(Common.ConstantsEntitiesName.PA.ParameterGroups, _params);
                                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.PA.ParameterGroups))
                                {
                                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.PA.ParameterGroups].Rows)
                                    {
                                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.PA.ParameterGroups, Common.ConstantsEntitiesName.PA.ParameterGroup, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PA.ParameterGroups, Common.ConstantsEntitiesName.PA.Indicator, String.Empty);
                                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                        _node.PostBack = true;
                                        _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.ParameterGroup);
                                        _node.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.PA.Parameter);
                                        _node.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.PA.Parameters);
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.ParameterGroup);
                                        _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PA.Indicator);
                                        _node.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                        _node.Attributes.Add("PkCompost", _node.Value);
                                        _node.Attributes["id"] = _node.ClientID;
                                        _node.CssClass = "ParametersGroup";
                                        e.Node.Nodes.Add(_node);
                                        //Aca verifica si este indicator, tiene parameterGroup como hijos.
                                        //Dictionary<String, Object> _params = new Dictionary<String, Object>();
                                        if (_params.ContainsKey("IdParameterGroup"))
                                            { _params.Remove("IdParameterGroup"); }
                                        _params.Add("IdParameterGroup", GetKeyValue(_node.Value, "IdParameterGroup"));
                                        if (HasChildren("ParameterGroupsHasParameter", _params))
                                            { _node.ExpandMode = TreeNodeExpandMode.ServerSide; }

                                    }
                                }
                                break;

                            case Common.ConstantsEntitiesName.PA.ParameterGroup:
                                //Si el expand es sobre el Indicator, entonces, muestro sus parameterGroups.
                                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                                BuildGenericDataTable(Common.ConstantsEntitiesName.PA.Parameters, _params);
                                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.PA.Parameters))
                                {
                                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.PA.Parameters].Rows)
                                    {
                                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.PA.Parameters, Common.ConstantsEntitiesName.PA.Parameter, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PA.Parameters, Common.ConstantsEntitiesName.PA.Indicator, String.Empty);
                                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                        _node.PostBack = true;
                                        _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Parameter);
                                        _node.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.PA.ParameterRange);
                                        _node.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.PA.ParameterRanges);
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Parameter);
                                        _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PA.Indicator);
                                        _node.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                        _node.Attributes.Add("PkCompost", _node.Value);
                                        _node.Attributes["id"] = _node.ClientID;
                                        _node.CssClass = "Parameter";
                                        e.Node.Nodes.Add(_node);
                                        //Aca verifica si este indicator, tiene parameterGroup como hijos.
                                        if (_params.ContainsKey("IdParameter"))
                                            { _params.Remove("IdParameter"); }
                                        _params.Add("IdParameter", GetKeyValue(_node.Value, "IdParameter"));

                                        if (HasChildren("ParametersHasParameterRange", _params))
                                            { _node.ExpandMode = TreeNodeExpandMode.ServerSide; }
                                    }
                                }
                                break;

                            case Common.ConstantsEntitiesName.PA.Parameter:
                                //Si el expand es sobre el Indicator, entonces, muestro sus parameterGroups.
                                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                                BuildGenericDataTable(Common.ConstantsEntitiesName.PA.ParameterRanges, _params);
                                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.PA.ParameterRanges))
                                {
                                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.PA.ParameterRanges].Rows)
                                    {
                                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.PA.ParameterRanges, Common.ConstantsEntitiesName.PA.ParameterRange, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.PA.ParameterRanges, Common.ConstantsEntitiesName.PA.Indicator, String.Empty);
                                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                        _node.PostBack = true;
                                        _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.ParameterRange);
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Parameter);
                                        _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PA.Indicator);
                                        _node.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                        _node.Attributes.Add("PkCompost", _node.Value);
                                        _node.Attributes["id"] = _node.ClientID;
                                        _node.CssClass = "ParameterRange";
                                        e.Node.Nodes.Add(_node);
                                    }
                                }
                                break;
                        }
                    }
                #endregion

            #endregion

            #region Organization
                public RadTreeView BuildContextElementMapsMenuOrganization(Dictionary<String, Object> param)
                {
                    //Arma un Tree Vacio para que no de error.
                    Int64 _idOrganization= 0;
                    if (param.ContainsKey("IdOrganization"))
                    {
                        _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                    }
                    Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                    String _pageTitle = String.Empty;
                    if (!param.ContainsKey("PageTitle"))
                    {
                        _pageTitle = _organization.CorporateName;
                    }
                    else
                    {
                        _pageTitle = Convert.ToString(param["PageTitle"]);
                    }

                    RadTreeView _rdtvContextElementMapsByOrganization = BuildElementMapsContent(Common.ConstantsEntitiesName.DS.Organizations);
                    //Agrega el ContextMenu al treeview.
                    _rdtvContextElementMapsByOrganization.ContextMenus.Add(BuildContextMenuContextElementShortCut());
                    _rdtvContextElementMapsByOrganization.Skin = "EMS";    // "ContextElementMapsByIndicator";
                    _rdtvContextElementMapsByOrganization.Attributes.Add("IsTreeContextElement", "true");
                    _rdtvContextElementMapsByOrganization.OnClientContextMenuShowing = "onClientContextMenuContextElementShowing";

                    //1° se carga el Proceso en el que estoy parado
                    String _keyValue = "IdOrganization=" + _idOrganization.ToString();
                    String _permissionType = String.Empty;
                    if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                    else
                        { _permissionType = Common.Constants.PermissionViewName; }

                    RadTreeNode _node = SetElementMapsNodeTreeView(_organization.CorporateName, _keyValue, String.Empty);
                    _node.PostBack = false;
                    _node.CssClass = "Organization";
                    _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Organization);
                    _node.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.DS.OrganizationalChart);
                    _node.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.DS.OrganizationalCharts);
                    _node.Attributes.Add("WithSecurity", "true");
                    _node.Attributes.Add("PermissionType", _permissionType);
                    _rdtvContextElementMapsByOrganization.Nodes.Add(_node);
                    //Aca verifica si este indicator, tiene parameterGroup como hijos.
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params.Add("IdOrganization", _idOrganization);
                    if (HasChildren("OrganizationsHasOrganizationalChart", _params))
                        { _node.ExpandMode = TreeNodeExpandMode.ServerSide; }

                    //2° ahora carga los nodos al proceso recien cargado...
                    //Carga los registros en el Tree
                    RadTreeNode _rtn = _rdtvContextElementMapsByOrganization.Nodes[0];
                    //Asocia el Handler del Expand y click
                    _rdtvContextElementMapsByOrganization.NodeExpand += new RadTreeViewEventHandler(rdtvContextElementMapsByOrganization_NodeExpand);

                    return _rdtvContextElementMapsByOrganization;
                }

                #region Events
                    /// <summary>
                    /// Evento para el Expand del Combo con Tree pero ElementMaps
                    /// </summary>
                    /// <param name="sender"></param>
                    /// <param name="e"></param>
                    protected void rdtvContextElementMapsByOrganization_NodeExpand(object sender, RadTreeNodeEventArgs e)
                    {
                        //En este Expand, se debe primero expandir de una organizacion al organizationalchart
                        //Del organizationalchart al jobTitle
                        //Del jobTitle al Posts.

                        //Limpio los hijos, para no duplicar al abrir y cerrar.
                        e.Node.Nodes.Clear();
                        Dictionary<String, Object> _params = new Dictionary<String, Object>();
                        _params = GetKeyValues(e.Node.Value);

                        String _entityExpand = e.Node.Attributes["EntityName"].ToString();
                        switch (_entityExpand)
                        {
                            case Common.ConstantsEntitiesName.DS.Organization:
                                //Si el expand es sobre la Organization, entonces, muestro sus OrganizationalCharts.
                                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                                BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationalCharts, _params);
                                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.OrganizationalCharts))
                                {
                                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.OrganizationalCharts].Rows)
                                    {
                                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.OrganizationalCharts, Common.ConstantsEntitiesName.DS.OrganizationalChart, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.DS.OrganizationalCharts, Common.ConstantsEntitiesName.DS.Organization, String.Empty);
                                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                        _node.PostBack = true;
                                        _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.OrganizationalChart);
                                        _node.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.DS.JobTitle);
                                        _node.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.DS.JobTitles);
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                                        _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Organization);
                                        _node.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                        _node.Attributes.Add("PkCompost", _node.Value);
                                        _node.Attributes["id"] = _node.ClientID;
                                        _node.CssClass = "OrganizationalChart";
                                        e.Node.Nodes.Add(_node);
                                        //Aca verifica si esta Organization, tiene OrganizationalChart como hijos.
                                        //Dictionary<String, Object> _params = new Dictionary<String, Object>();
                                        if (_params.ContainsKey("IdOrganizationalChart"))
                                            { _params.Remove("IdOrganizationalChart"); }
                                        _params.Add("IdOrganizationalChart", GetKeyValue(_node.Value, "IdOrganizationalChart")); 
                                        
                                        if (HasChildren("OrganizationalChartsHasJobTitle", _params))
                                            { _node.ExpandMode = TreeNodeExpandMode.ServerSide; }

                                    }
                                }
                                break;

                            case Common.ConstantsEntitiesName.DS.OrganizationalChart:
                                //Si el expand es sobre el OrganizationalChart, entonces, muestro sus JobTitles.
                                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                                BuildGenericDataTable(Common.ConstantsEntitiesName.DS.JobTitles, _params);
                                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.JobTitles))
                                {
                                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.JobTitles].Rows)
                                    {
                                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.JobTitles, Common.ConstantsEntitiesName.DS.JobTitle, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.DS.JobTitles, Common.ConstantsEntitiesName.DS.Organization, String.Empty);
                                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                        _node.PostBack = true;
                                        _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.JobTitle);
                                        _node.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.DS.Person);
                                        _node.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.DS.People);
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                                        _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Organization);
                                        _node.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                        _node.Attributes.Add("PkCompost", _node.Value);
                                        _node.Attributes["id"] = _node.ClientID;
                                        _node.CssClass = "JobTitle";
                                        //Aca verifica si este JobTitles, tiene Post como hijos.
                                        if (_params.ContainsKey("IdGeographicArea"))
                                            { _params.Remove("IdGeographicArea"); }
                                        _params.Add("IdGeographicArea", GetKeyValue(_node.Value, "IdGeographicArea"));

                                        if (_params.ContainsKey("IdPosition"))
                                            { _params.Remove("IdPosition"); }
                                        _params.Add("IdPosition", GetKeyValue(_node.Value, "IdPosition"));

                                        if (_params.ContainsKey("IdFunctionalArea"))
                                            { _params.Remove("IdFunctionalArea"); }
                                        _params.Add("IdFunctionalArea", GetKeyValue(_node.Value, "IdFunctionalArea"));

                                        if ((HasChildren("JobTitlesHasPost", _params)) || (HasChildren("JobTitlesHasChildren", _params)))
                                            { _node.ExpandMode = TreeNodeExpandMode.ServerSide; }

                                        //Finalmente mete el nodo.
                                        e.Node.Nodes.Add(_node);
                                    }
                                }
                                break;

                            case Common.ConstantsEntitiesName.DS.JobTitle:
                                //1° Mira si tiene mas JT como hijos, y despues carga los post que tenga asociados.
                                //Si el expand es sobre el OrganizationalChart, entonces, muestro sus JobTitles.
                                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                                BuildGenericDataTable(Common.ConstantsEntitiesName.DS.JobTitleChildren, _params);
                                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.JobTitleChildren))
                                {
                                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.JobTitleChildren].Rows)
                                    {
                                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.JobTitles, Common.ConstantsEntitiesName.DS.JobTitle, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.DS.JobTitles, Common.ConstantsEntitiesName.DS.Organization, String.Empty);
                                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                        _node.PostBack = true;
                                        _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.JobTitle);
                                        _node.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.DS.Person);
                                        _node.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.DS.People);
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                                        _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Organization);
                                        _node.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                        _node.Attributes.Add("PkCompost", _node.Value);
                                        _node.Attributes["id"] = _node.ClientID;
                                        _node.CssClass = "JobTitle";
                                        //Aca verifica si este JobTitles, tiene Post como hijos.
                                        if (_params.ContainsKey("IdGeographicArea"))
                                        { _params.Remove("IdGeographicArea"); }
                                        _params.Add("IdGeographicArea", GetKeyValue(_node.Value, "IdGeographicArea"));

                                        if (_params.ContainsKey("IdPosition"))
                                        { _params.Remove("IdPosition"); }
                                        _params.Add("IdPosition", GetKeyValue(_node.Value, "IdPosition"));

                                        if (_params.ContainsKey("IdFunctionalArea"))
                                        { _params.Remove("IdFunctionalArea"); }
                                        _params.Add("IdFunctionalArea", GetKeyValue(_node.Value, "IdFunctionalArea"));

                                        if ((HasChildren("JobTitlesHasPost", _params)) || (HasChildren("JobTitlesHasChildren", _params)))
                                        { _node.ExpandMode = TreeNodeExpandMode.ServerSide; }

                                        //Finalmente mete el nodo.
                                        e.Node.Nodes.Add(_node);
                                    }
                                }


                                //Limpia los parametros y vuelve a cargar...
                                _params.Clear();
                                _params = GetKeyValues(e.Node.Value);
                                //Si el expand es sobre el JobTitle, entonces, muestro sus Posts.
                                //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                                BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Posts, _params);
                                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.Posts))
                                {
                                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.Posts].Rows)
                                    {
                                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Posts, Common.ConstantsEntitiesName.DS.Post, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, Common.ConstantsEntitiesName.DS.Posts, Common.ConstantsEntitiesName.DS.Organization, String.Empty);
                                        //Ahora a cada nodo, le pongo los atributos necesarios para que en el Click navege al Viewer.
                                        _node.PostBack = true;
                                        _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Post);
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                                        _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Organization);
                                        _node.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                        _node.Attributes.Add("PkCompost", _node.Value);
                                        _node.Attributes.Add("NoADD", "true"); 
                                        _node.Attributes["id"] = _node.ClientID;
                                        _node.CssClass = "Post";
                                        e.Node.Nodes.Add(_node);
                                        //Aca verifica si este JobTitles, tiene Post como hijos.
                                        //if (HasChildren("JobTitlesHasPost", _params))
                                        //    { _node.ExpandMode = TreeNodeExpandMode.ServerSide; }
                                    }
                                }
                                break;
                        }
                    }
                #endregion

            #endregion

            #region Facility Types
                public RadTreeView BuildContextElementMapsMenuFacilityType(Dictionary<String, Object> param)
                {
                    RadTreeView _rdtvContextElementMapsByFacility = BuildElementMapsContent(Common.ConstantsEntitiesName.DS.Facilities);
                    //Agrega el ContextMenu al treeview.
                    _rdtvContextElementMapsByFacility.ContextMenus.Add(BuildContextMenuContextElementFacilityTypeShortCut());
                    _rdtvContextElementMapsByFacility.Skin = "EMS";    // "ContextElementMapsByIndicator";
                    _rdtvContextElementMapsByFacility.Attributes.Add("IsTreeContextElement", "true");
                    //_rdtvContextElementMapsByFacility.OnClientContextMenuShowing = "onClientContextMenuContextElementShowing";

                    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);

                    //1° se carga el Proceso en el que estoy parado
                    String _permissionType = String.Empty;
                    if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _permissionType = Common.Constants.PermissionManageName; }
                    else
                    { _permissionType = Common.Constants.PermissionViewName; }

                    Dictionary<Int64, FacilityType> _facilityTypes = new Dictionary<Int64, FacilityType>();

                    //Se cargan los Types recorriendo todos los facilities de la organizacion, para que no quede mal!
                    foreach (Facility _item in EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Facilities.Values)
                    {
                        if (!_facilityTypes.ContainsKey(_item.FacilityType.IdFacilityType))
                        {
                            _facilityTypes.Add(_item.FacilityType.IdFacilityType, _item.FacilityType);
                        }
                    }

                    //var _lnqFacilityTypes = from facilityType in EMSLibrary.User.GeographicInformationSystem.FacilityTypes().Values
                    //                        where facilityType.Facilities.Count > 0 //Para que no queden Types sin hijos...
                    //                        orderby facilityType.LanguageOption.Name ascending
                    //                        select facilityType;

                    var _lnqFacilityTypes = from facilityType in _facilityTypes.Values
                                            where facilityType.Facilities.Count > 0 //Para que no queden Types sin hijos...
                                            orderby facilityType.LanguageOption.Name ascending
                                            select facilityType;
                    foreach (FacilityType _facilityType in _lnqFacilityTypes)
                    {
                        RadTreeNode _nodeFacilityType = SetElementMapsNodeTreeView(_facilityType.LanguageOption.Name, "IdFacilityType=" + _facilityType.IdFacilityType.ToString(), String.Empty);
                        _nodeFacilityType.PostBack = true;
                        _nodeFacilityType.CssClass = GetIconNameFacilityType(_facilityType.IdFacilityType);
                        _nodeFacilityType.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.FacilityType);
                        //_nodeFacilityType.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.DS.OrganizationalChart);
                        //_nodeFacilityType.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.DS.OrganizationalCharts);
                        _nodeFacilityType.Attributes.Add("EntityNameContextInfo", String.Empty);    // Common.ConstantsEntitiesName.DS.FacilityType);
                        _nodeFacilityType.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.FacilityType);
                        _nodeFacilityType.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                        //_nodeFacilityType.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                        _nodeFacilityType.Attributes.Add("PkCompost", _nodeFacilityType.Value);
                        _nodeFacilityType.Attributes.Add("WithSecurity", "false");
                        _nodeFacilityType.Attributes.Add("PermissionType", _permissionType);
                        _rdtvContextElementMapsByFacility.Nodes.Add(_nodeFacilityType);

                        _nodeFacilityType.ExpandMode = TreeNodeExpandMode.ClientSide;

                        var _lnqFacilities = from facilities in _facilityType.Facilities.Values
                                             where facilities.Organization.IdOrganization == _idOrganization    //Se filtran para que muestre solo los facilities de la organizacion.
                                             orderby facilities.LanguageOption.Name ascending
                                             select facilities;
                        foreach (Facility _facility in _lnqFacilities)
                        {
                            RadTreeNode _nodeFacility = SetElementMapsNodeTreeView(_facility.LanguageOption.Name, "IdOrganization=" + _facility.IdOrganization.ToString() + "&IdFacilityType=" + _facility.FacilityType.IdFacilityType.ToString() + "&IdFacility=" + _facility.IdFacility.ToString(), String.Empty);
                            _nodeFacility.PostBack = true;
                            _nodeFacility.CssClass = "Facility";
                            _nodeFacility.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Facility);
                            //_nodeFacility.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.DS.OrganizationalChart);
                            //_nodeFacility.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.DS.OrganizationalCharts);
                            _nodeFacility.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Facility);
                            _nodeFacility.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Facility);
                            //_nodeFacility.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                            _nodeFacility.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                            _nodeFacility.Attributes.Add("PkCompost", _nodeFacility.Value);
                            _nodeFacility.Attributes.Add("WithSecurity", "false");
                            _nodeFacility.Attributes.Add("PermissionType", _permissionType);
                            _nodeFacilityType.Nodes.Add(_nodeFacility);

                            _nodeFacility.ExpandMode = TreeNodeExpandMode.ClientSide;

                            var _lnqSectors = from sectors in _facility.Sectors.Values
                                              orderby sectors.LanguageOption.Name ascending
                                              select sectors;
                            foreach (Sector _sector in _lnqSectors)
                            {
                                RadTreeNode _nodeSector = SetElementMapsNodeTreeView(_sector.LanguageOption.Name, "IdOrganization=" + _facility.IdOrganization.ToString() + "&IdFacility=" + _facility.IdFacility.ToString() + "&IdSector=" + _sector.IdFacility.ToString(), String.Empty);
                                _nodeSector.PostBack = true;
                                _nodeSector.CssClass = "Sector";
                                _nodeSector.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Sector);
                                //_nodeSector.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.DS.OrganizationalChart);
                                //_nodeSector.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.DS.OrganizationalCharts);
                                _nodeSector.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Sector);
                                _nodeSector.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Sector);
                                //_nodeSector.Attributes.Add("URL", "~/MainInfo/ListViewer.aspx");
                                _nodeSector.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                                _nodeSector.Attributes.Add("PkCompost", _nodeSector.Value);
                                _nodeSector.Attributes.Add("WithSecurity", "false");
                                _nodeSector.Attributes.Add("PermissionType", _permissionType);
                                _nodeFacility.Nodes.Add(_nodeSector);

                                if (_sector.Sectors.Count > 0)
                                {
                                    _nodeSector.ExpandMode = TreeNodeExpandMode.ClientSide;

                                    var _lnqSectorsChild = from sectorsChild in _sector.Sectors.Values
                                                           orderby sectorsChild.LanguageOption.Name ascending
                                                           select sectorsChild;
                                    foreach (Sector _sectorChild in _lnqSectorsChild)
                                    {
                                        RadTreeNode _nodeSectorChild = SetElementMapsNodeTreeView(_sectorChild.LanguageOption.Name, "IdOrganization=" + _facility.IdOrganization.ToString() + "&IdFacility=" + _facility.IdFacility.ToString() + "&IdSector=" + _sectorChild.IdFacility.ToString(), String.Empty);
                                        _nodeSectorChild.PostBack = true;
                                        _nodeSectorChild.CssClass = "Sector";
                                        _nodeSectorChild.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Sector);
                                        //_nodeSectorChild.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.DS.OrganizationalChart);
                                        //_nodeSectorChild.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.DS.OrganizationalCharts);
                                        _nodeSectorChild.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Sector);
                                        _nodeSectorChild.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Sector);
                                        _nodeSectorChild.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                                        _nodeSectorChild.Attributes.Add("PkCompost", _nodeSectorChild.Value);
                                        _nodeSectorChild.Attributes.Add("WithSecurity", "false");
                                        _nodeSectorChild.Attributes.Add("PermissionType", _permissionType);
                                        _nodeSector.Nodes.Add(_nodeSectorChild);

                                        if (_sectorChild.Sectors.Count > 0)
                                        {
                                            _nodeSectorChild.ExpandMode = TreeNodeExpandMode.ClientSide;

                                            var _lnqSectorsChild2 = from sectorsChild2 in _sectorChild.Sectors.Values
                                                                    orderby sectorsChild2.LanguageOption.Name ascending
                                                                    select sectorsChild2;
                                            foreach (Sector _sectorChild2 in _lnqSectorsChild2)
                                            {
                                                RadTreeNode _nodeSectorChild2 = SetElementMapsNodeTreeView(_sectorChild2.LanguageOption.Name, "IdOrganization=" + _facility.IdOrganization.ToString() + "&IdFacility=" + _facility.IdFacility.ToString() + "&IdSector=" + _sectorChild2.IdFacility.ToString(), String.Empty);
                                                _nodeSectorChild2.PostBack = true;
                                                _nodeSectorChild2.CssClass = "Sector";
                                                _nodeSectorChild2.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Sector);
                                                //_nodeSectorChild2.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.DS.OrganizationalChart);
                                                //_nodeSectorChild2.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.DS.OrganizationalCharts);
                                                _nodeSectorChild2.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Sector);
                                                _nodeSectorChild2.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Sector);
                                                _nodeSectorChild2.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                                                _nodeSectorChild2.Attributes.Add("PkCompost", _nodeSectorChild2.Value);
                                                _nodeSectorChild2.Attributes.Add("WithSecurity", "false");
                                                _nodeSectorChild2.Attributes.Add("PermissionType", _permissionType);
                                                _nodeSectorChild.Nodes.Add(_nodeSectorChild2);

                                                if (_sectorChild2.Sectors.Count > 0)
                                                {
                                                    _nodeSectorChild2.ExpandMode = TreeNodeExpandMode.ClientSide;

                                                    var _lnqSectorsChild3 = from sectorsChild3 in _sectorChild2.Sectors.Values
                                                                            orderby sectorsChild3.LanguageOption.Name ascending
                                                                            select sectorsChild3;
                                                    foreach (Sector _sectorChild3 in _lnqSectorsChild3)
                                                    {
                                                        RadTreeNode _nodeSectorChild3 = SetElementMapsNodeTreeView(_sectorChild3.LanguageOption.Name, "IdOrganization=" + _facility.IdOrganization.ToString() + "&IdFacility=" + _facility.IdFacility.ToString() + "&IdSector=" + _sectorChild3.IdFacility.ToString(), String.Empty);
                                                        _nodeSectorChild3.PostBack = true;
                                                        _nodeSectorChild3.CssClass = "Sector";
                                                        _nodeSectorChild3.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Sector);
                                                        //_nodeSectorChild3.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.DS.OrganizationalChart);
                                                        //_nodeSectorChild3.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.DS.OrganizationalCharts);
                                                        _nodeSectorChild3.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Sector);
                                                        _nodeSectorChild3.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Sector);
                                                        _nodeSectorChild3.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                                                        _nodeSectorChild3.Attributes.Add("PkCompost", _nodeSectorChild3.Value);
                                                        _nodeSectorChild3.Attributes.Add("WithSecurity", "false");
                                                        _nodeSectorChild3.Attributes.Add("PermissionType", _permissionType);
                                                        _nodeSectorChild2.Nodes.Add(_nodeSectorChild3);

                                                        if (_sectorChild3.Sectors.Count > 0)
                                                        {
                                                            _nodeSectorChild3.ExpandMode = TreeNodeExpandMode.ClientSide;

                                                            var _lnqSectorsChild4 = from sectorsChild4 in _sectorChild3.Sectors.Values
                                                                                    orderby sectorsChild4.LanguageOption.Name ascending
                                                                                    select sectorsChild4;
                                                            foreach (Sector _sectorChild4 in _lnqSectorsChild4)
                                                            {
                                                                RadTreeNode _nodeSectorChild4 = SetElementMapsNodeTreeView(_sectorChild4.LanguageOption.Name, "IdOrganization=" + _facility.IdOrganization.ToString() + "&IdFacility=" + _facility.IdFacility.ToString() + "&IdSector=" + _sectorChild4.IdFacility.ToString(), String.Empty);
                                                                _nodeSectorChild4.PostBack = true;
                                                                _nodeSectorChild4.CssClass = "Sector";
                                                                _nodeSectorChild4.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Sector);
                                                                //_nodeSectorChild3.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.DS.OrganizationalChart);
                                                                //_nodeSectorChild3.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.DS.OrganizationalCharts);
                                                                _nodeSectorChild4.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Sector);
                                                                _nodeSectorChild4.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Sector);
                                                                _nodeSectorChild4.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                                                                _nodeSectorChild4.Attributes.Add("PkCompost", _nodeSectorChild3.Value);
                                                                _nodeSectorChild4.Attributes.Add("WithSecurity", "false");
                                                                _nodeSectorChild4.Attributes.Add("PermissionType", _permissionType);
                                                                _nodeSectorChild3.Nodes.Add(_nodeSectorChild3);

                                                                if (_sectorChild4.Sectors.Count > 0)
                                                                {
                                                                    _nodeSectorChild4.ExpandMode = TreeNodeExpandMode.ClientSide;

                                                                    var _lnqSectorsChild5 = from sectorsChild5 in _sectorChild4.Sectors.Values
                                                                                            orderby sectorsChild5.LanguageOption.Name ascending
                                                                                            select sectorsChild5;
                                                                    foreach (Sector _sectorChild5 in _lnqSectorsChild5)
                                                                    {
                                                                        RadTreeNode _nodeSectorChild5 = SetElementMapsNodeTreeView(_sectorChild5.LanguageOption.Name, "IdOrganization=" + _facility.IdOrganization.ToString() + "&IdFacility=" + _facility.IdFacility.ToString() + "&IdSector=" + _sectorChild5.IdFacility.ToString(), String.Empty);
                                                                        _nodeSectorChild5.PostBack = true;
                                                                        _nodeSectorChild5.CssClass = "Sector";
                                                                        _nodeSectorChild5.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Sector);
                                                                        //_nodeSectorChild3.Attributes.Add("EntityNameByCE", Common.ConstantsEntitiesName.DS.OrganizationalChart);
                                                                        //_nodeSectorChild3.Attributes.Add("EntityNameGridByCE", Common.ConstantsEntitiesName.DS.OrganizationalCharts);
                                                                        _nodeSectorChild5.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Sector);
                                                                        _nodeSectorChild5.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Sector);
                                                                        _nodeSectorChild5.Attributes.Add("URL", "~/MainInfo/ListReportViewer.aspx");
                                                                        _nodeSectorChild5.Attributes.Add("PkCompost", _nodeSectorChild3.Value);
                                                                        _nodeSectorChild5.Attributes.Add("WithSecurity", "false");
                                                                        _nodeSectorChild5.Attributes.Add("PermissionType", _permissionType);
                                                                        _nodeSectorChild4.Nodes.Add(_nodeSectorChild3);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                        }

                                    }
                                }

                            }
                        }
                    }


                    return _rdtvContextElementMapsByFacility;
                }

                private String GetIconNameFacilityType(Int64 idFacilityType)
                {
                    String _iconName = String.Empty;
                    FacilityType _facilityType = EMSLibrary.User.GeographicInformationSystem.FacilityType(idFacilityType);

                    if (String.IsNullOrEmpty(_facilityType.IconName))
                    {
                        _iconName = "Unspecified";
                    }
                    else
                    {
                        _iconName = _facilityType.IconName;
                    }

                    return _iconName;



                    //String _iconName = String.Empty;
                    //switch (idFacilityType)
                    //{
                    //    case Common.Constants.idPowerPlant: //1
                    //        _iconName = "PowerPlant";
                    //        break;
                    //    case Common.Constants.idShopsAndServices: //2
                    //        _iconName = "ShopsandServices";
                    //        break;
                    //    case Common.Constants.idHomes:  // 3
                    //        _iconName = "Homes";
                    //        break;
                    //    case Common.Constants.idRefineries: // 4
                    //        _iconName = "Refineries";
                    //        break;
                    //    case Common.Constants.idServiceStations:    // 5
                    //        _iconName = "ServiceStations";
                    //        break;
                    //    case Common.Constants.idWaterTreatmentPlants:  //7
                    //        _iconName = "WaterTreatmentPlants";
                    //        break;
                    //    case Common.Constants.idIndustries://8
                    //        _iconName = "Industries";
                    //        break;
                    //    case Common.Constants.idFarms:  //  9
                    //        _iconName = "Farms";
                    //        break;
                    //    case Common.Constants.idWasteTreatmentPlants:   // 10
                    //        _iconName = "WasteTreatmentPlants";
                    //        break;
                    //    case Common.Constants.idLandfill:   // 11
                    //        _iconName = "Landfill";
                    //        break;
                    //    case Common.Constants.idLand:   // 12
                    //        _iconName = "Land";
                    //        break;

                    //    case Common.Constants.idOffice: // = 13
                    //        _iconName = "Office";
                    //        break;
                    //    case Common.Constants.idUnspecified:  // = 14;
                    //        _iconName = "Unspecified";
                    //        break;
                    //    case Common.Constants.idOilPipeline:    // = 15;
                    //        _iconName = "OilPipeline";
                    //        break;
                    //    case Common.Constants.idGasPipeline:    // = 16;
                    //        _iconName = "GasPipeline";
                    //        break;
                    //    case Common.Constants.idBatteries:  // = 17;
                    //        _iconName = "Batteries";
                    //        break;
                    //    case Common.Constants.idMotorCompressorStation: // = 19;
                    //        _iconName = "MotorCompressorStation";
                    //        break;
                    //    case Common.Constants.idOilTreatmentPlant:  // = 20;
                    //        _iconName = "OilTreatmentPlant";
                    //        break;
                    //    case Common.Constants.idConditioningPlantDewpoint:  // = 21;
                    //        _iconName = "ConditioningPlantDewpoint";
                    //        break;
                    //    case Common.Constants.idSeparationPlantOfLiquefiedGases:    // = 22;
                    //        _iconName = "SeparationPlantOfLiquefiedGases";
                    //        break;
                    //    case Common.Constants.idSaltWaterInjectionPlant:    // = 23;
                    //        _iconName = "SaltWaterInjectionPlant";
                    //        break;
                    //    case Common.Constants.idFreshWaterInjectionPlant:   // = 24;
                    //        _iconName = "FreshWaterInjectionPlant";
                    //        break;
                    //    case Common.Constants.idFreshWaterTransferPlant: // = 25;
                    //        _iconName = "FreshWaterTransferPlant";
                    //        break;
                    //    case Common.Constants.idThermalPowerPlant:  // = 27;
                    //        _iconName = "ThermalPowerPlant";
                    //        break;
                    //    case Common.Constants.idOilWell:    // = 28;
                    //        _iconName = "OilWell";
                    //        break;
                    //    case Common.Constants.idFleetVehicles:  // = 29;
                    //        _iconName = "FleetVehicles";
                    //        break;
                    //    case Common.Constants.idGlobal:  // = 30;
                    //        _iconName = "Global";
                    //        break;
                    //}

                    //return _iconName;
                }

                #region Events
                #endregion

            #endregion

        #endregion
    }
}