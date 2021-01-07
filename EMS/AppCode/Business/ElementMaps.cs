using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.IA.Entities;
using Condesus.EMS.Business.RM.Entities;

namespace Condesus.EMS.WebUI.Business
{
    public partial class Collections : Base
    {
        //Elementos ROOTs
        //Aca estan los metodos que retornan DataTables de cada Entidad. cuando son elementos root
        #region Public Methods (Esta clase contiene todos los metodos publicos para los Element Map de Entidades elemento como root)

            #region Organization
                /// <summary>
                /// Construye el DataTable a modo List con los datos de la Coleccion Organization que son Roots.
                /// </summary>
                /// <param name="param">Parametros opcionales para filtrar</param>
                /// <returns>Un <c>DataTable</c></returns>
                public DataTable OrganizationsRoots(Dictionary<String, Object> param)
                {
                    //Construye el datatable
                    DataTable _dt = BuildDataTable("Organization");

                    //Contruye las columnas y sus atributos.
                    ColumnOptions _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.IdOrganization;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                    _columnOptions.IsPrimaryKey = true;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = false;
                    _columnOptions.IsSearchable = false;
                    _columnOptions.AllowNull = false;
                    BuildColumnsDataTable(ref _dt, "IdOrganization", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.CorporateName;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = true;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = false;
                    _columnOptions.IsContextMenuCaption = true;
                    BuildColumnsDataTable(ref _dt, "CorporateName", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Name;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = false;
                    BuildColumnsDataTable(ref _dt, "Name", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.FiscalIdentification;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = true;
                    BuildColumnsDataTable(ref _dt, "FiscalIdentification", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = false;
                    _columnOptions.IsSearchable = false;
                    _columnOptions.AllowNull = false;
                    BuildColumnsDataTable(ref _dt, "PermissionType", _columnOptions);

                    //Ya esta armado el DataTable, ahora lo carga
                    foreach (Organization _organization in EMSLibrary.User.DirectoryServices.Map.OrganizationRoots().Values)
                    {
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                        else
                            { _permissionType = Common.Constants.PermissionViewName; }

                        _dt.Rows.Add(_organization.IdOrganization, _organization.CorporateName, _organization.Name, _organization.FiscalIdentification, _permissionType);
                    }

                    //Retorna el DataTable
                    return _dt;
                }
                /// <summary>
                /// Indica si esa Organizacion tiene hijos o no.
                /// </summary>
                /// <param name="param">Parametros opcionales para filtrar</param>
                /// <returns>Un<c>Boolean</c></returns>
                public Boolean OrganizationsRootsHasChildren(Dictionary<String, Object> param)
                {
                    if (param.ContainsKey("HasPost"))
                    {
                        if (Convert.ToBoolean(param["HasPost"]))
                        {
                            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                            if (EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Posts().Count > 0)
                            {
                                return true;
                            }
                        }
                    }
                    //Siempre debe devolver false
                    return false;
                }

                /// <summary>
                /// Construye el DataTable a modo List con los datos de la Coleccion Organization que son Roots y que tienen facilities asociados.
                /// </summary>
                /// <param name="param">Parametros opcionales para filtrar</param>
                /// <returns>Un <c>DataTable</c></returns>
                public DataTable OrganizationsRootsWithFacility(Dictionary<String, Object> param)
                {
                    //Construye el datatable
                    DataTable _dt = BuildDataTable("Organization");

                    //Contruye las columnas y sus atributos.
                    ColumnOptions _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.IdOrganization;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                    _columnOptions.IsPrimaryKey = true;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = false;
                    _columnOptions.IsSearchable = false;
                    _columnOptions.AllowNull = false;
                    BuildColumnsDataTable(ref _dt, "IdOrganization", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.CorporateName;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = true;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = false;
                    _columnOptions.IsContextMenuCaption = true;
                    BuildColumnsDataTable(ref _dt, "CorporateName", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Name;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = false;
                    BuildColumnsDataTable(ref _dt, "Name", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.FiscalIdentification;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = true;
                    BuildColumnsDataTable(ref _dt, "FiscalIdentification", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = false;
                    _columnOptions.IsSearchable = false;
                    _columnOptions.AllowNull = false;
                    BuildColumnsDataTable(ref _dt, "PermissionType", _columnOptions);

                    //Ya esta armado el DataTable, ahora lo carga
                    foreach (Organization _organization in EMSLibrary.User.DirectoryServices.Map.OrganizationRoots().Values)
                    {
                        //Si tiene facilities asociados, lo hace.
                        if (_organization.Facilities.Count > 0)
                        {
                            String _permissionType = String.Empty;
                            //Obtiene el permiso que tiene el usuario para esa organizacion.
                            if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            _dt.Rows.Add(_organization.IdOrganization, _organization.CorporateName, _organization.Name, _organization.FiscalIdentification, _permissionType);
                        }
                    }

                    //Retorna el DataTable
                    return _dt;
                }
                /// <summary>
                /// Indica si esa Organizacion tiene hijos o no.
                /// </summary>
                /// <param name="param">Parametros opcionales para filtrar</param>
                /// <returns>Un<c>Boolean</c></returns>
                public Boolean OrganizationsRootsWithFacilityHasChildren(Dictionary<String, Object> param)
                {
                    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                    //Si tiene facility, retorna true!
                    if (EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Facilities.Count > 0)
                    { return true; }

                    return false;
                }
            #endregion

            #region Indicator
                /// <summary>
                /// Construye el DataTable a modo List con los datos de la Coleccion Indicators que son Roots.
                /// </summary>
                /// <param name="param">Parametros opcionales para filtrar</param>
                /// <returns>Un <c>DataTable</c></returns>
                public DataTable IndicatorsRoots(Dictionary<String, Object> param)
                {
                    //Construye el datatable
                    DataTable _dt = BuildDataTable("Indicator");

                    //Contruye las columnas y sus atributos.
                    ColumnOptions _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.IdIndicator;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                    _columnOptions.IsPrimaryKey = true;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = false;
                    _columnOptions.IsSearchable = false;
                    _columnOptions.AllowNull = false;
                    BuildColumnsDataTable(ref _dt, "IdIndicator", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Name;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = true;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = false;
                    _columnOptions.IsContextMenuCaption = true;
                    BuildColumnsDataTable(ref _dt, "Name", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = true;
                    BuildColumnsDataTable(ref _dt, "Description", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Scope;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = true;
                    BuildColumnsDataTable(ref _dt, "Scope", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Limitation;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = true;
                    BuildColumnsDataTable(ref _dt, "Limitation", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Definition;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = true;
                    BuildColumnsDataTable(ref _dt, "Definition", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = false;
                    _columnOptions.IsSearchable = false;
                    _columnOptions.AllowNull = false;
                    BuildColumnsDataTable(ref _dt, "PermissionType", _columnOptions);

                    //Ya esta armado el DataTable, ahora lo carga
                    String _permissionType = String.Empty;
                    //Obtiene el permiso que tiene el usuario para esa organizacion.
                    if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _permissionType = Common.Constants.PermissionManageName; }
                    else
                    { _permissionType = Common.Constants.PermissionViewName; }

                    foreach (Indicator _indicator in EMSLibrary.User.PerformanceAssessments.Map.IndicatorRoots().Values)
                    {
                        _dt.Rows.Add(_indicator.IdIndicator, Common.Functions.ReplaceIndexesTags(_indicator.LanguageOption.Name), _indicator.LanguageOption.Description, _indicator.LanguageOption.Scope, _indicator.LanguageOption.Limitation, _indicator.LanguageOption.Definition, _permissionType);
                    }
                    //}
                    //Retorna el DataTable
                    return _dt;
                }
            #endregion

            #region Process
                /// <summary>
                /// Construye el DataTable a modo List con los datos de la Coleccion Indicators que son Roots.
                /// </summary>
                /// <param name="param">Parametros opcionales para filtrar</param>
                /// <returns>Un <c>DataTable</c></returns>
                public DataTable ProcessGroupProcessesRoots(Dictionary<String, Object> param)
                {
                    //Construye el datatable
                    DataTable _dt = BuildDataTable("ProcessGroupProcess");

                    //Contruye las columnas y sus atributos.
                    ColumnOptions _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.IdProcess;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                    _columnOptions.IsPrimaryKey = true;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = false;
                    _columnOptions.IsSearchable = false;
                    _columnOptions.AllowNull = false;
                    BuildColumnsDataTable(ref _dt, "IdProcess", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Name;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = true;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = false;
                    _columnOptions.IsContextMenuCaption = true;
                    BuildColumnsDataTable(ref _dt, "Name", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = true;
                    BuildColumnsDataTable(ref _dt, "Description", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = false;
                    _columnOptions.IsSearchable = false;
                    _columnOptions.AllowNull = false;
                    BuildColumnsDataTable(ref _dt, "PermissionType", _columnOptions);
                    
                    //Cuando implementemos la opcion de traer todo en un filtro.
                    Boolean _showAll = false;
                    if (ValidateSelectedItemComboBox(param, ref _showAll))
                    {
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (ProcessGroupProcess _processGroupProcess in EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcessRoots().Values)
                        {
                            String _permissionType = String.Empty;
                            //Obtiene el permiso que tiene el usuario para esa organizacion.
                            if (_processGroupProcess.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            _dt.Rows.Add(_processGroupProcess.IdProcess, Common.Functions.ReplaceIndexesTags(_processGroupProcess.LanguageOption.Title), Common.Functions.ReplaceIndexesTags(_processGroupProcess.LanguageOption.Description), _permissionType);
                        }
                    }
                    //Retorna el DataTable
                    return _dt;
                }
            #endregion

            #region Resource
                /// <summary>
                /// Construye el DataTable a modo List con los datos de la Coleccion Indicators que son Roots.
                /// </summary>
                /// <param name="param">Parametros opcionales para filtrar</param>
                /// <returns>Un <c>DataTable</c></returns>
                public DataTable ResourcesRoots(Dictionary<String, Object> param)
                {
                    //Construye el datatable
                    DataTable _dt = BuildDataTable("Resources");

                    //Contruye las columnas y sus atributos.
                    ColumnOptions _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.IdResource;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                    _columnOptions.IsPrimaryKey = true;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = false;
                    _columnOptions.IsSearchable = false;
                    _columnOptions.AllowNull = false;
                    BuildColumnsDataTable(ref _dt, "IdResource", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Title;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = true;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = false;
                    _columnOptions.IsContextMenuCaption = true;
                    BuildColumnsDataTable(ref _dt, "Title", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = true;
                    BuildColumnsDataTable(ref _dt, "Description", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.Version;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = false;
                    _columnOptions.AllowNull = true;
                    BuildColumnsDataTable(ref _dt, "Version", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.ResourceType;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = true;
                    _columnOptions.IsSearchable = true;
                    _columnOptions.AllowNull = true;
                    BuildColumnsDataTable(ref _dt, "ResourceType", _columnOptions);

                    _columnOptions = new ColumnOptions();
                    _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                    _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                    _columnOptions.IsPrimaryKey = false;
                    _columnOptions.DisplayInCombo = false;
                    _columnOptions.DisplayInManage = false;
                    _columnOptions.IsSearchable = false;
                    _columnOptions.AllowNull = false;
                    BuildColumnsDataTable(ref _dt, "PermissionType", _columnOptions);
                    
                    //Cuando implementemos la opcion de traer todo en un filtro.
                    Boolean _showAll = false;
                    if (ValidateSelectedItemComboBox(param, ref _showAll))
                    {
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.KC.Entities.Resource _resource in EMSLibrary.User.KnowledgeCollaboration.Map.ResourceRoots().Values)
                        {
                            String _version = String.Empty;
                            String _permissionType = String.Empty;
                            //Obtiene el permiso que tiene el usuario para esa organizacion.
                            if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Si es de tipo File, entonces saco la version del current...sino queda vacio.
                            if (_resource.GetType().Name == "ResourceVersion")
                            {
                                Condesus.EMS.Business.KC.Entities.ResourceVersion _resourceFile = (Condesus.EMS.Business.KC.Entities.ResourceVersion)_resource;
                                if (_resourceFile.CurrentVersion != null)
                                {
                                    _version = _resourceFile.CurrentVersion.VersionNumber.ToString();
                                }
                            }

                            _dt.Rows.Add(_resource.IdResource, _resource.LanguageOption.Title, _resource.LanguageOption.Description, _version, _resource.ResourceType.LanguageOption.Name, _permissionType);
                        }
                    }
                    //Retorna el DataTable
                    return _dt;
                }
            #endregion

            #region Project (IA)
                /// <summary>
                /// Construye el DataTable a modo List con los datos de la Coleccion Indicators que son Roots.
                /// </summary>
                /// <param name="param">Parametros opcionales para filtrar</param>
                /// <returns>Un <c>DataTable</c></returns>
                public DataTable ProjectsRoots(Dictionary<String, Object> param)
                {
                    //Construye el datatable
                    DataTable _dt = BuildDataTable("ProjectRoots");

                    //Por ahora no tengo nada que mostrar...

                    //Retorna el DataTable
                    return _dt;
                }
            #endregion

            #region Risk & Potencial
                /// <summary>
                /// Construye el DataTable a modo List con los datos de la Coleccion Indicators que son Roots.
                /// </summary>
                /// <param name="param">Parametros opcionales para filtrar</param>
                /// <returns>Un <c>DataTable</c></returns>
                public DataTable RisksRoots(Dictionary<String, Object> param)
                {
                    //Construye el datatable
                    DataTable _dt = BuildDataTable("RisksRoots");

                    //Por ahora no tengo nada que mostrar...

                    //Retorna el DataTable
                    return _dt;
                }
            #endregion

        #endregion

    }
}
