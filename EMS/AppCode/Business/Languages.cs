using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Condesus.EMS.Business.EP.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;
using Condesus.EMS.Business.CT.Entities;

namespace Condesus.EMS.WebUI.Business
{
    public partial class Collections : Base
    {
        //Para las LG
        //Aca estan los metodos que retornan DataTables de cada Entidad.
        #region Public Methods (Esta clase contiene todos los metodos publicos de Entidades Planas)

            #region Directory Service

                #region Organization Classification
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Organization Classification LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable OrganizationClassification_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("OrganizationClassification_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdOrganizationClassification", Resources.CommonListManage.IdOrganizationClassification, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idOrganizationClassification = Convert.ToInt64(param["IdOrganizationClassification"]);
                        Condesus.EMS.Business.DS.Entities.OrganizationClassification _organizationClassification = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                        else
                            { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.DS.Entities.OrganizationClassification_LG _organizationClassification_LG in _organizationClassification.LanguagesOptions.Items().Values)
                        {
                            String _idLanguage = _organizationClassification_LG.Language.IdLanguage;
                            _dt.Rows.Add(_idOrganizationClassification, _idLanguage, Global.Languages[_idLanguage].Name, _organizationClassification_LG.Name.ToString(), _organizationClassification_LG.Description.ToString(), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Organizational Chart
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Organizational Chart
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable OrganizationalChart_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("OrganizationalChart_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdOrganizationalChart", Resources.CommonListManage.IdOrganizationalChart, true);
                        
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdOrganization;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdOrganization", _columnOptions);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idOrganizationalChart = Convert.ToInt64(param["IdOrganizationalChart"]);
                        Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.DS.Entities.OrganizationalChart_LG _organizationalChart_LG in EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).OrganizationalChart(_idOrganizationalChart).LanguagesOptions.Items().Values)
                        {
                            String _idLanguage = _organizationalChart_LG.Language.IdLanguage;
                            _dt.Rows.Add(_idOrganizationalChart, _idLanguage, Global.Languages[_idLanguage].Name, _organizationalChart_LG.Name.ToString(), _organizationalChart_LG.Description.ToString(), _permissionType, _idOrganization);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Applicabilities
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Applicability LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Applicability_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Applicability_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdApplicability", Resources.CommonListManage.IdApplicability, false);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idApplicability = Convert.ToInt64(param["IdApplicability"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.DS.Entities.ApplicabilityContactType_LG _applicabilityContactType_LG in EMSLibrary.User.DirectoryServices.Configuration.ApplicabilityContactType(_idApplicability).LanguagesOptions.Items().Values)
                        {
                            String _idLanguage = _applicabilityContactType_LG.Language.IdLanguage;
                            _dt.Rows.Add(_idApplicability, _idLanguage, Global.Languages[_idLanguage].Name, _applicabilityContactType_LG.Name.ToString(), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Contact Types
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ContactType LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ContactType_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ContactType_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdContactType", Resources.CommonListManage.IdContactType, true);

                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdApplicability;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdApplicability", _columnOptions);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idApplicability = Convert.ToInt64(param["IdApplicability"]);
                        Int64 _idContactType = Convert.ToInt64(param["IdContactType"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.DS.Entities.ContactType_LG _contactType_LG in EMSLibrary.User.DirectoryServices.Configuration.ContactType(_idApplicability, _idContactType).LanguagesOptions.Items().Values)
                        {
                            String _idLanguage = _contactType_LG.Language.IdLanguage;
                            _dt.Rows.Add(_idContactType, _idLanguage, Global.Languages[_idLanguage].Name, _contactType_LG.Name.ToString(), _contactType_LG.Description.ToString(), _permissionType, _idApplicability);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Contact Urls
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ContactUrl LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ContactUrl_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ContactUrl_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdContactURL", Resources.CommonListManage.IdContactUrl, true);

                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.ParentEntity;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "ParentEntity", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdOrganization;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdOrganization", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdPerson;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdPerson", _columnOptions);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idPerson = 0;
                        Int64 _idContactUrl = Convert.ToInt64(param["IdContactURL"]);
                        String _parentEntity = param["ParentEntity"].ToString();
                        
                        String _permissionType = String.Empty;
                        Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }


                        Dictionary<String, Condesus.EMS.Business.DS.Entities.ContactURL_LG> _contacts = null;
                        switch (_parentEntity)
                        {
                            case "Person":
                            _idPerson = Convert.ToInt64(param["IdPerson"]);
                            _contacts = _organization.Person(_idPerson).ContactURL(_idContactUrl).LanguagesOptions.Items();
                            break;
                            case "Organization":
                            _contacts = _organization.ContactURL(_idContactUrl).LanguagesOptions.Items();
                            break;
                        }
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.DS.Entities.ContactURL_LG _contactURL_LG in _contacts.Values)
                        {
                            String _idLanguage = _contactURL_LG.Language.IdLanguage;
                            _dt.Rows.Add(_idContactUrl, _idLanguage, Global.Languages[_idLanguage].Name, _contactURL_LG.Name.ToString(), _contactURL_LG.Description.ToString(), _permissionType, _parentEntity, _idOrganization, _idPerson);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Organization Relationship Types
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion OrganizationRelationshipType LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable OrganizationRelationshipType_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("OrganizationRelationshipType_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdOrganizationRelationshipType", Resources.CommonListManage.IdOrganizationRelationshipType, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idOrganizationRelationshipType = Convert.ToInt64(param["IdOrganizationRelationshipType"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.DS.Entities.OrganizationRelationshipType_LG _organizationRelationshipType_LG in EMSLibrary.User.DirectoryServices.Configuration.OrganizationRelationshipType(_idOrganizationRelationshipType).LanguagesOptions.Items().Values)
                        {
                            String _idLanguage = _organizationRelationshipType_LG.Language.IdLanguage;

                            _dt.Rows.Add(_idOrganizationRelationshipType,_idLanguage, Global.Languages[_idLanguage].Name, Common.Functions.ReplaceIndexesTags(_organizationRelationshipType_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_organizationRelationshipType_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Geographic Areas
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Geographic Area LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable GeographicArea_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("GeographicArea_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdGeographicArea", Resources.CommonListManage.IdGeographicArea, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
                        //Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        String _permissionType = String.Empty;
                        //Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.GIS.Entities.GeographicArea_LG _geographiArea_LG in EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea).LanguagesOptions.Items().Values)
                        {
                            String _idLanguage = _geographiArea_LG.Language.IdLanguage;

                            _dt.Rows.Add(_idGeographicArea, _idLanguage, Global.Languages[_idLanguage].Name, Common.Functions.ReplaceIndexesTags(_geographiArea_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_geographiArea_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Facility
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Facility LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Facility_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Facility_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdFacility", Resources.CommonListManage.IdFacility, true);

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

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);

                        String _permissionType = String.Empty;
                        Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.GIS.Entities.Site_LG _facility_LG in _organization.Facility(_idFacility).LanguagesOptions.Items().Values)
                        {
                            String _idLanguage = _facility_LG.Language.IdLanguage;

                            _dt.Rows.Add(_idFacility, _idLanguage, Global.Languages[_idLanguage].Name, Common.Functions.ReplaceIndexesTags(_facility_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_facility_LG.Description.ToString()), _permissionType, _idOrganization);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Sector
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Sector LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Sector_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Sector_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdSector", Resources.CommonListManage.IdFacility, true);

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

                        //Contruye las columnas y sus atributos.
                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdFacility;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdFacility", _columnOptions);
                        
                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idSector = Convert.ToInt64(param["IdSector"]);

                        String _permissionType = String.Empty;
                        Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.GIS.Entities.Site_LG _facility_LG in _organization.Facility(_idFacility).Sector(_idSector).LanguagesOptions.Items().Values)
                        {
                            String _idLanguage = _facility_LG.Language.IdLanguage;

                            _dt.Rows.Add(_idSector, _idLanguage, Global.Languages[_idLanguage].Name, Common.Functions.ReplaceIndexesTags(_facility_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_facility_LG.Description.ToString()), _permissionType, _idOrganization, _idFacility);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Functional Areas
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Functional Area LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable FunctionalArea_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("FunctionalArea_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdFunctionalArea", Resources.CommonListManage.IdFunctionalArea, false);

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

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        String _permissionType = String.Empty;
                        Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                        else
                            { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.DS.Entities.FunctionalArea_LG _functionalArea_LG in _organization.FunctionalArea(_idFunctionalArea).LanguagesOptions.Items().Values)
                        {
                            String _idLanguage = _functionalArea_LG.Language.IdLanguage;

                            _dt.Rows.Add(_idFunctionalArea, _idLanguage, Global.Languages[_idLanguage].Name, Common.Functions.ReplaceIndexesTags(_functionalArea_LG.Name.ToString()), _permissionType, _idOrganization);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Positions
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Position LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Position_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Position_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdPosition", Resources.CommonListManage.IdPosition, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        String _permissionType = String.Empty;
                        Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                        else
                            { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.DS.Entities.Position_LG _position_LG in _organization.Position(_idPosition).LanguagesOptions.Items().Values)
                        {
                            String _idLanguage = _position_LG.Language.IdLanguage;

                            _dt.Rows.Add(_idPosition, _idLanguage, Global.Languages[_idLanguage].Name, Common.Functions.ReplaceIndexesTags(_position_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_position_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Salutation Types
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion SalutationType LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable SalutationType_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("SalutationType_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdSalutationType", Resources.CommonListManage.IdSalutationType, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idSalutationType = Convert.ToInt64(param["IdSalutationType"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.DS.Entities.SalutationType_LG _salutationType_LG in EMSLibrary.User.DirectoryServices.Configuration.SalutationType(_idSalutationType).LanguagesOptions.Items().Values)
                        {
                            String _idLanguage = _salutationType_LG.Language.IdLanguage;
                            _dt.Rows.Add(_idSalutationType, _idLanguage, Global.Languages[_idLanguage].Name, Common.Functions.ReplaceIndexesTags(_salutationType_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_salutationType_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Facility Type
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Facility Type LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable FacilityType_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("FacilityType_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdFacilityType", Resources.CommonListManage.IdFacilityType, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idFacilityType = Convert.ToInt64(param["IdFacilityType"]);
                        FacilityType _facilityType = EMSLibrary.User.GeographicInformationSystem.FacilityType(_idFacilityType);
                        
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (FacilityType_LG _facilityType_LG in _facilityType.LanguagesOptions.Values)
                        {
                            _dt.Rows.Add(_idFacilityType, _facilityType_LG.Language.IdLanguage, Global.Languages[_facilityType_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_facilityType_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_facilityType_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

            #endregion

            #region Improvement Actions

                #region Project Classifications
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ProjectClassification LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ProjectClassification_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ProjectClassification_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdProjectClassification", Resources.CommonListManage.IdProjectClassification, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idProjectClassification = Convert.ToInt64(param["IdProjectClassification"]);
                        Condesus.EMS.Business.IA.Entities.ProjectClassification _projectClassification = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_idProjectClassification);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.ImprovementAction.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.IA.Entities.ProjectClassification_LG _projectClassification_LG in EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_idProjectClassification).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idProjectClassification, _projectClassification_LG.Language.IdLanguage, Global.Languages[_projectClassification_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_projectClassification_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_projectClassification_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

            #endregion

            #region Knowledge Collaboration
                #region Resources
                    public DataTable KCResource_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Resource_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdResource", Resources.CommonListManage.IdResource, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idResource = Convert.ToInt64(param["IdResource"]);
                        Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.KC.Entities.Resource_LG _resource_LG in _resource.LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idResource, _resource_LG.Language.IdLanguage, Global.Languages[_resource_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_resource_LG.Title.ToString()), Common.Functions.ReplaceIndexesTags(_resource_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    public DataTable Resource_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Resource_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdResource", Resources.CommonListManage.IdResource, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idResource = Convert.ToInt64(param["IdResource"]);
                        Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.KC.Entities.Resource_LG _resource_LG in _resource.LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idResource, _resource_LG.Language.IdLanguage, Global.Languages[_resource_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_resource_LG.Title.ToString()), Common.Functions.ReplaceIndexesTags(_resource_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Resource Classifications
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ResourceClassification LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ResourceClassification_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ResourceClassification_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdResourceClassification", Resources.CommonListManage.IdResourceClassification, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idResourceClassification = Convert.ToInt64(param["IdResourceClassification"]);                    //Ya esta armado el DataTable, ahora lo carga
                        Condesus.EMS.Business.KC.Entities.ResourceClassification _resourceClassification = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idResourceClassification);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        foreach (Condesus.EMS.Business.KC.Entities.ResourceClassification_LG _resourceClassification_LG in EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idResourceClassification).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idResourceClassification, _resourceClassification_LG.Language.IdLanguage, Global.Languages[_resourceClassification_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_resourceClassification_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_resourceClassification_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Resource File States
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ResourceHistoryState LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ResourceHistoryState_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ResourceHistoryState_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdResourceFileState", Resources.CommonListManage.IdResourceFileState, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idResourceFileState = Convert.ToInt64(param["IdResourceFileState"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.KnowledgeCollaboration.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.KC.Entities.ResourceHistoryState_LG _resourceFileState_LG in EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceHistoryState(_idResourceFileState).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idResourceFileState, _resourceFileState_LG.Language.IdLanguage, Global.Languages[_resourceFileState_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_resourceFileState_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_resourceFileState_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Resource Types
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ResourceType LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ResourceType_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ResourceType_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdResourceType", Resources.CommonListManage.IdResourceType, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idResourceType = Convert.ToInt64(param["IdResourceType"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.KnowledgeCollaboration.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.KC.Entities.ResourceType_LG _resourceType_LG in EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceType(_idResourceType).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idResourceType, _resourceType_LG.Language.IdLanguage, Global.Languages[_resourceType_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_resourceType_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_resourceType_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

            #endregion

            #region Performance Assessment

                #region Calculation Scenario Types
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion CalculationScenarioType LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable CalculationScenarioType_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("CalculationScenarioType_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdScenarioType", Resources.CommonListManage.IdScenarioType, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idScenarioType = Convert.ToInt64(param["IdScenarioType"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.PA.Entities.CalculationScenarioType_LG _calculationScenarioType_LG in EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(_idScenarioType).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idScenarioType, _calculationScenarioType_LG.Language.IdLanguage, Global.Languages[_calculationScenarioType_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_calculationScenarioType_LG.Name.ToString()), _calculationScenarioType_LG.Description, _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Calculations
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Calculation LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Calculation_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Calculation_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdCalculation", Resources.CommonListManage.IdScenarioType, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idCalculation = Convert.ToInt64(param["IdCalculation"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.PA.Entities.Calculation_LG _calculation_LG in EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_idCalculation).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idCalculation, _calculation_LG.Language.IdLanguage, Global.Languages[_calculation_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_calculation_LG.Name.ToString()), _calculation_LG.Description, _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Formulas
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Formula LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Formula_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Formula_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdFormula", Resources.CommonListManage.IdFormula, false);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idFormula = Convert.ToInt64(param["IdFormula"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.PA.Entities.Formula_LG _formula_LG in EMSLibrary.User.PerformanceAssessments.Configuration.Formula(_idFormula).LanguageOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idFormula, _formula_LG.Language.IdLanguage, Global.Languages[_formula_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_formula_LG.Name.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Indicator Classifications
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion IndicatorClassification LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable IndicatorClassification_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("IndicatorClassification_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdIndicatorClassification", Resources.CommonListManage.IdIndicatorClassification, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idIndicatorClassification = Convert.ToInt64(param["IdIndicatorClassification"]);
                        Condesus.EMS.Business.PA.Entities.IndicatorClassification _indicatorClassification = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idIndicatorClassification);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.PA.Entities.IndicatorClassification_LG _indicatorClassification_LG in _indicatorClassification.LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idIndicatorClassification, _indicatorClassification_LG.Language.IdLanguage, Global.Languages[_indicatorClassification_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_indicatorClassification_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_indicatorClassification_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Indicators
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Indicator LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Indicator_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Indicator_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdIndicator", Resources.CommonListManage.IdIndicator, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
                        Condesus.EMS.Business.PA.Entities.Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.PA.Entities.Indicator_LG _indicator_LG in _indicator.LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idIndicator, _indicator_LG.Language.IdLanguage, Global.Languages[_indicator_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_indicator_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_indicator_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Magnitudes
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Magnitud LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Magnitud_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Magnitud_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdMagnitud", Resources.CommonListManage.IdMagnitud, false);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idMagnitud = Convert.ToInt64(param["IdMagnitud"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.PA.Entities.Magnitud_LG _magnitud_LG in EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idMagnitud, _magnitud_LG.Language.IdLanguage, Global.Languages[_magnitud_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_magnitud_LG.Name.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Measurement Device Types
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion MeasurementDeviceType LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable MeasurementDeviceType_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("MeasurementDeviceType_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdMeasurementDeviceType", Resources.CommonListManage.IdMeasurementDeviceType, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idMeasurementDeviceType = Convert.ToInt64(param["IdMeasurementDeviceType"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.PA.Entities.MeasurementDeviceType_LG _measurementDeviceType_LG in EMSLibrary.User.PerformanceAssessments.Configuration.MeasurementDeviceType(_idMeasurementDeviceType).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idMeasurementDeviceType, _measurementDeviceType_LG.Language.IdLanguage, Global.Languages[_measurementDeviceType_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_measurementDeviceType_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_measurementDeviceType_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion
                
                #region Measurements
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Measurement LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Measurement_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Measurement_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdMeasurement", Resources.CommonListManage.IdMeasurement, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idMeasurement = Convert.ToInt64(param["IdMeasurement"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.PA.Entities.Measurement_LG _measurement_LG in EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idMeasurement, _measurement_LG.Language.IdLanguage, Global.Languages[_measurement_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_measurement_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_measurement_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Measurement Units
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion MeasurementUnit LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable MeasurementUnit_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("MeasurementUnit_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdMeasurementUnit", Resources.CommonListManage.IdMeasurementUnit, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idMeasurementUnit = Convert.ToInt64(param["IdMeasurementUnit"]);
                        Int64 _idMagnitud = Convert.ToInt64(param["IdMagnitud"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.PA.Entities.MeasurementUnit_LG _measurementUnit_LG in EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud).MeasurementUnit(_idMeasurementUnit).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idMeasurementUnit, _measurementUnit_LG.Language.IdLanguage, Global.Languages[_measurementUnit_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_measurementUnit_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_measurementUnit_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Parameter Groups
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ParameterGroup LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ParameterGroup_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ParameterGroup_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdParameterGroup", Resources.CommonListManage.IdParameterGroup, true);
                        //Agrega columnas adicionales para PK
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdIndicator;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdIndicator", _columnOptions);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idParameterGroup = Convert.ToInt64(param["IdParameterGroup"]);
                        Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
                        Condesus.EMS.Business.PA.Entities.Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.PA.Entities.ParameterGroup_LG _parameterGroup_LG in _indicator.ParameterGroup(_idParameterGroup).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idParameterGroup, _parameterGroup_LG.Language.IdLanguage, Global.Languages[_parameterGroup_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_parameterGroup_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_parameterGroup_LG.Description.ToString()), _permissionType, _idIndicator);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Parameters
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Parameter LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Parameter_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Parameter_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdParameter", Resources.CommonListManage.IdParameter, false);
                        //Agrega columnas adicionales para PK
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
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParameterGroup;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdParameterGroup", _columnOptions);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idParameterGroup = Convert.ToInt64(param["IdParameterGroup"]);
                        Int64 _idParameter = Convert.ToInt64(param["IdParameter"]);
                        Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
                        Condesus.EMS.Business.PA.Entities.Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.PA.Entities.Parameter_LG _parameter_LG in EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).ParameterGroup(_idParameterGroup).Parameter(_idParameter).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idParameter, _parameter_LG.Language.IdLanguage, Global.Languages[_parameter_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_parameter_LG.Description.ToString()), _permissionType, _idIndicator, _idParameterGroup);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Constant Classification
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Constant Classification LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ConstantClassification_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ConstantClassification_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdConstantClassification", Resources.CommonListManage.IdConstantClassification, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idConstantClassification = Convert.ToInt64(param["IdConstantClassification"]);
                        ConstantClassification _constantClassification = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClassification);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (ConstantClassification_LG _constantClassification_LG in _constantClassification.LanguagesOptions.Values)
                        {
                            _dt.Rows.Add(_idConstantClassification, _constantClassification_LG.Language.IdLanguage, Global.Languages[_constantClassification_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_constantClassification_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_constantClassification_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Constant
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Constant LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Constant_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Constant_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdConstant", Resources.CommonListManage.IdConstant, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idConstant = Convert.ToInt64(param["IdConstant"]);
                        Int64 _idConstantClassification = Convert.ToInt64(param["IdConstantClassification"]);
                        Constant _constant = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClassification).Constant(_idConstant);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Constant_LG _constant_LG in _constant.LanguagesOptions.Values)
                        {
                            _dt.Rows.Add(_idConstant, _constant_LG.Language.IdLanguage, Global.Languages[_constant_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_constant_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_constant_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Accounting Activity
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion AccountingActivity LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable AccountingActivity_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("AccountingActivity_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdActivity", Resources.CommonListManage.IdActivity, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idActivity = Convert.ToInt64(param["IdActivity"]);
                        AccountingActivity _accountingActivity = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingActivity(_idActivity);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (AccountingActivity_LG _accountingActivity_LG in _accountingActivity.LanguagesOptions.Values)
                        {
                            _dt.Rows.Add(_idActivity, _accountingActivity_LG.Language.IdLanguage, Global.Languages[_accountingActivity_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_accountingActivity_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_accountingActivity_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Accounting Sector
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion AccountingSector LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable AccountingSector_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("AccountingSector_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdSector", Resources.CommonListManage.IdSector, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idSector = Convert.ToInt64(param["IdSector"]);
                        AccountingSector _accountingSector = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingSector(_idSector);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (AccountingSector_LG _accountingSector_LG in _accountingSector.LanguagesOptions.Values)
                        {
                            _dt.Rows.Add(_idSector, _accountingSector_LG.Language.IdLanguage, Global.Languages[_accountingSector_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_accountingSector_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_accountingSector_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Accounting Scenario
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Accounting Scenario LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable AccountingScenario_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("AccountingScenario_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdScenario", Resources.CommonListManage.IdScenario, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idScenario = Convert.ToInt64(param["IdScenario"]);
                        AccountingScenario _accountingScenario = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScenario(_idScenario);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (AccountingScenario_LG _accountingScenario_LG in _accountingScenario.LanguagesOptions.Values)
                        {
                            _dt.Rows.Add(_idScenario, _accountingScenario_LG.Language.IdLanguage, Global.Languages[_accountingScenario_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_accountingScenario_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_accountingScenario_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Accounting Scope
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Accounting Scenario LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable AccountingScope_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("AccountingScope_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdScope", Resources.CommonListManage.IdScope, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idScope = Convert.ToInt64(param["IdScope"]);
                        AccountingScope _accountingScope = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingScope(_idScope);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (AccountingScope_LG _accountingScope_LG in _accountingScope.LanguagesOptions.Values)
                        {
                            _dt.Rows.Add(_idScope, _accountingScope_LG.Language.IdLanguage, Global.Languages[_accountingScope_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_accountingScope_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_accountingScope_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Methodology
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Methodology LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Methodology_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Methodology_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdMethodology", Resources.CommonListManage.IdScope, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idMethodology = Convert.ToInt64(param["IdMethodology"]);
                        Methodology _methodology = EMSLibrary.User.PerformanceAssessments.Configuration.Methodology(_idMethodology);
                        String _permissionType = String.Empty;

                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Methodology_LG _methodology_LG in _methodology.LanguagesOptions.Values)
                        {
                            _dt.Rows.Add(_idMethodology, _methodology_LG.Language.IdLanguage, Global.Languages[_methodology_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_methodology_LG.MethodName.ToString()), Common.Functions.ReplaceIndexesTags(_methodology_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Quality
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Quality LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Quality_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Quality_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdQuality", Resources.CommonListManage.IdScope, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idQuality = Convert.ToInt64(param["IdQuality"]);
                        Quality _quality = EMSLibrary.User.PerformanceAssessments.Configuration.Quality(_idQuality);
                        String _permissionType = String.Empty;

                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Quality_LG _quality_LG in _quality.LanguagesOptions.Values)
                        {
                            _dt.Rows.Add(_idQuality, _quality_LG.Language.IdLanguage, Global.Languages[_quality_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_quality_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_quality_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Transformation
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Transformation LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Transformation_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Transformation_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdTransformation", Resources.CommonListManage.IdTransformation, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idMeasurement = Convert.ToInt64(param["IdMeasurement"]);
                        Int64 _idTransformation = Convert.ToInt64(param["IdTransformation"]);
                        CalculateOfTransformation _calculateOfTransformation = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement).Transformation(_idTransformation);
                        String _permissionType = String.Empty;

                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (CalculateOfTransformation_LG _calculateOfTransformation_LG in _calculateOfTransformation.LanguagesOptions.Values)
                        {
                            _dt.Rows.Add(_idTransformation, _calculateOfTransformation_LG.Language.IdLanguage, Global.Languages[_calculateOfTransformation_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_calculateOfTransformation_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_calculateOfTransformation_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Transformation LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable TransformationByTransformation_LG(Dictionary<String, Object> param)
                    {
                        return Transformation_LG(param);
                    }
                #endregion

            #endregion

            #region Process Framework

                #region Extended Properties
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ExtendedProperty LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ExtendedProperty_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ExtendedProperty_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdExtendedProperty", Resources.CommonListManage.IdExtendedProperty, true);

                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdExtendedPropertyClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdExtendedPropertyClassification", _columnOptions);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idExtendedProperty = Convert.ToInt64(param["IdExtendedProperty"]);
                        Int64 _idExtendedPropertyClassification = Convert.ToInt64(param["IdExtendedPropertyClassification"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (ExtendedProperty_LG _extendedProperty_LG in EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassification(_idExtendedPropertyClassification).ExtendedProperty(_idExtendedProperty).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idExtendedProperty, _extendedProperty_LG.Language.IdLanguage, Global.Languages[_extendedProperty_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_extendedProperty_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_extendedProperty_LG.Description.ToString()), _permissionType, _idExtendedPropertyClassification);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Extended Property Classifications
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ExtendedPropertyClassification LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ExtendedPropertyClassification_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ExtendedPropertyClassification_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdExtendedPropertyClassification", Resources.CommonListManage.IdExtendedPropertyClassification, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idExtendedPropertyClassification = Convert.ToInt64(param["IdExtendedPropertyClassification"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (ExtendedPropertyClassification_LG _extendedPropertyClassification_LG in EMSLibrary.User.ExtendedProperties.ExtendedPropertyClassification(_idExtendedPropertyClassification).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idExtendedPropertyClassification, _extendedPropertyClassification_LG.Language.IdLanguage, Global.Languages[_extendedPropertyClassification_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_extendedPropertyClassification_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_extendedPropertyClassification_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Participation Types
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ParticipationType LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ParticipationType_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ParticipationType_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdParticipationType", Resources.CommonListManage.IdParticipationType, false);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idParticipationType = Convert.ToInt64(param["IdParticipationType"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (ParticipationType_LG _participationType_LG in EMSLibrary.User.ProcessFramework.Configuration.ParticipationType(_idParticipationType).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idParticipationType, _participationType_LG.Language.IdLanguage, Global.Languages[_participationType_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_participationType_LG.Name.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Process Classifications
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ProcessClassification LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ProcessClassification_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ProcessClassification_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdProcessClassification", Resources.CommonListManage.IdProcessClassification, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idProcessClassification = Convert.ToInt64(param["IdProcessClassification"]);
                        ProcessClassification _processClassification = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idProcessClassification);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (ProcessClassification_LG _processClassification_LG in _processClassification.LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idProcessClassification, _processClassification_LG.Language.IdLanguage, Global.Languages[_processClassification_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_processClassification_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_processClassification_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

                #region Processes
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Process LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Process_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Process_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdProcess", Resources.CommonListManage.IdProcess, true);

                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Purpose;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref _dt, "Purpose", _columnOptions);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                        ProcessGroupProcess _process = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Process_LG _process_LG in EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idProcess, _process_LG.Language.IdLanguage, Global.Languages[_process_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_process_LG.Title.ToString()), Common.Functions.ReplaceIndexesTags(_process_LG.Description.ToString()), _permissionType, _process_LG.Purpose.ToString());
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ProcessGroupProcess LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ProcessGroupProcess_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ProcessGroupProcess_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdProcess", Resources.CommonListManage.IdProcess, true);

                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Purpose;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref _dt, "Purpose", _columnOptions);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                        ProcessGroupProcess _process = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Process_LG _process_LG in EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idProcess, _process_LG.Language.IdLanguage, Global.Languages[_process_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_process_LG.Title.ToString()), Common.Functions.ReplaceIndexesTags(_process_LG.Description.ToString()), _permissionType, _process_LG.Purpose.ToString());
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ProcessTaskMeasurement LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ProcessTaskMeasurement_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ProcessTaskMeasurement_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdProcess", Resources.CommonListManage.IdProcess, true);

                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Purpose;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref _dt, "Purpose", _columnOptions);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                        Int64 _idTask = Convert.ToInt64(param["IdTask"]);
                        ProcessGroupProcess _process = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                        ProcessTask _processTask = _process.ProcessTask(_idTask);

                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Process_LG _process_LG in _processTask.LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idProcess, _process_LG.Language.IdLanguage, Global.Languages[_process_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_process_LG.Title.ToString()), Common.Functions.ReplaceIndexesTags(_process_LG.Description.ToString()), _permissionType, _process_LG.Purpose.ToString());
                        }
                        //Retorna el DataTable
                        return _dt;
                    }

                #endregion

                //#region Role Types
                //    /// <summary>
                //    /// Construye el DataTable a modo List con los datos de la Coleccion RoleType LG.
                //    /// </summary>
                //    /// <param name="param">Parametros opcionales para filtrar</param>
                //    /// <returns>Un <c>DataTable</c></returns>
                //    public DataTable RoleType_LG(Dictionary<String, Object> param)
                //    {
                //        //Construye el datatable
                //        DataTable _dt = BuildDataTable("RoleType_LG");

                //        //Contruye las columnas y sus atributos.
                //        SetColumnsLanguage(ref _dt, "IdRoleType", Resources.CommonListManage.IdRoleType, false);

                //        //Ya esta el DataTable armado, ahora se trae el item.
                //        Int64 _idRoleType = Convert.ToInt64(param["IdRoleType"]);
                //        String _permissionType = String.Empty;
                //        //Obtiene el permiso que tiene el usuario para esa organizacion.
                //        if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                //        { _permissionType = Common.Constants.PermissionManageName; }
                //        else
                //        { _permissionType = Common.Constants.PermissionViewName; }
                        
                //        //Ya esta armado el DataTable, ahora lo carga
                //        foreach (Condesus.EMS.Business.Security.Entities.RoleType_LG _roleType_LG in EMSLibrary.User.Security.RoleType(_idRoleType).LanguagesOptions.Items().Values)
                //        {
                //            _dt.Rows.Add(_idRoleType, _roleType_LG.Language.IdLanguage, Global.Languages[_roleType_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_roleType_LG.Name.ToString()), _permissionType);
                //        }
                //        //Retorna el DataTable
                //        return _dt;
                //    }
                //#endregion

                #region Time Units
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion TimeUnit LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable TimeUnit_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("TimeUnit_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdTimeUnit", Resources.CommonListManage.IdTimeUnit, false);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idTimeUnit = Convert.ToInt64(param["IdTimeUnit"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.ProcessFramework.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }
                        

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (TimeUnit_LG _timeUnit_LG in EMSLibrary.User.ProcessFramework.Configuration.TimeUnit(_idTimeUnit).LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idTimeUnit, _timeUnit_LG.Language.IdLanguage, Global.Languages[_timeUnit_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_timeUnit_LG.Name.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

            #endregion

            #region Risk Management

                #region Risk Classifications
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion RiskClassification LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable RiskClassification_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("RiskClassification_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdRiskClassification", Resources.CommonListManage.IdRiskClassification, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idRiskClassification = Convert.ToInt64(param["IdRiskClassification"]);
                        Condesus.EMS.Business.RM.Entities.RiskClassification _riskClassification = EMSLibrary.User.RiskManagement.Map.RiskClassification(_idRiskClassification);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.RiskManagement.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.RM.Entities.RiskClassification_LG _riskClassification_LG in _riskClassification.LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idRiskClassification, _riskClassification_LG.Language.IdLanguage, Global.Languages[_riskClassification_LG.Language.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_riskClassification_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_riskClassification_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

            #endregion

            #region Collaboration Tools
                #region Forum
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Forum LG.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Forum_LG(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Forum_LG");

                        //Contruye las columnas y sus atributos.
                        SetColumnsLanguage(ref _dt, "IdForum", Resources.CommonListManage.IdForum, true);
                        //SetColumnsLanguage(ref _dt, "IdProcess", Resources.CommonListManage.IdProcess, true);

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idForum = Convert.ToInt64(param["IdForum"]);
                        Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                        Forum _forum = EMSLibrary.User.CollaborationTools.Map.Forum(_idForum);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Forum_LG _forum_LG in _forum.LanguagesOptions.Items().Values)
                        {
                            _dt.Rows.Add(_idForum, _forum_LG.IdLanguage, Global.Languages[_forum_LG.IdLanguage].Name, Common.Functions.ReplaceIndexesTags(_forum_LG.Name.ToString()), Common.Functions.ReplaceIndexesTags(_forum_LG.Description.ToString()), _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion
            #endregion

        #endregion
    }
}
