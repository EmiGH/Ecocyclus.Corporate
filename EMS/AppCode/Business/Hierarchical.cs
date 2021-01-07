using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Condesus.EMS.Business.GIS.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.IA.Entities;
using Condesus.EMS.Business.RM.Entities;

namespace Condesus.EMS.WebUI.Business
{
    public partial class Collections : Base
    {
        //Aca estan los metodos que retornan DataTables de cada Entidad.
        #region Public Methods (Esta clase contiene todos los metodos publicos de Entidades Jerarquicas)

            #region Directory Service

                #region Organization Classification
                    /// <summary>
                    /// Construye las columnas del datatable de OrganizationClassification
                    /// </summary>
                    /// <param name="dt"></param>
                    private void OrganizationClassificationColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdOrganizationClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdOrganizationClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentOrganizationClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdParentOrganizationClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.OrganizationClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Organization Classification (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable OrganizationClassifications(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("OrganizationClassification");

                        //Contruye las columnas y sus atributos.
                        OrganizationClassificationColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            //Obtiene el permiso que tiene el usuario para esa organizacion.
                            String _permissionType = String.Empty;
                            if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            Dictionary<Int64, OrganizationClassification> _organizationClassifications = new Dictionary<long, OrganizationClassification>();
                            //Verifica que ahora si viene el idOrg, entonces trae todas las classificaciones de esa org.
                            if (param.ContainsKey("IdOrganization"))
                            {
                                //Obtiene el OrganizationClassification
                                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                                _organizationClassifications = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Classifications;
                                ////Ya esta armado el DataTable, ahora lo carga
                                //foreach (OrganizationClassification _organizationClassification in EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Classifications.Values)
                                //{
                                //    _dt.Rows.Add(_organizationClassification.IdOrganizationClassification, _organizationClassification.IdParentOrganizationClassification, _organizationClassification.LanguageOption.Name, _organizationClassification.LanguageOption.Description, _permissionType);
                                //}
                            }
                            else
                            {
                                _organizationClassifications = EMSLibrary.User.DirectoryServices.Map.OrganizationClassifications();
                                ////Ya esta armado el DataTable, ahora lo carga
                                //foreach (OrganizationClassification _organizationClassification in EMSLibrary.User.DirectoryServices.Map.OrganizationClassifications().Values)
                                //{
                                //    _dt.Rows.Add(_organizationClassification.IdOrganizationClassification, _organizationClassification.IdParentOrganizationClassification, _organizationClassification.LanguageOption.Name, _organizationClassification.LanguageOption.Description, _permissionType);
                                //}
                            }
                            var _lnqOrganizationClass = from organizationClass in _organizationClassifications.Values
                                                   orderby organizationClass.LanguageOption.Name ascending
                                                   select organizationClass;
                            foreach (OrganizationClassification _organizationClassification in _lnqOrganizationClass)
                            {
                                _dt.Rows.Add(_organizationClassification.IdOrganizationClassification, _organizationClassification.IdParentOrganizationClassification, _organizationClassification.LanguageOption.Name, _organizationClassification.LanguageOption.Description, _permissionType);
                            }

                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un OrganizationClassification.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable OrganizationClassificationsChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("OrganizationClassification");

                        //Contruye las columnas y sus atributos.
                        OrganizationClassificationColumns(ref _dt);

                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        Int64 _idOrganizationClassification = Convert.ToInt64(param["IdOrganizationClassification"]);
                        var _lnqOrganizationClass = from organizationClass in EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification).ChildrenClassifications.Values
                                                    orderby organizationClass.LanguageOption.Name ascending
                                                    select organizationClass;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (OrganizationClassification _organizationClassification in _lnqOrganizationClass)
                        {
                            _dt.Rows.Add(_organizationClassification.IdOrganizationClassification, _organizationClassification.IdParentOrganizationClassification, _organizationClassification.LanguageOption.Name, _organizationClassification.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa Geographic Area tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean OrganizationClassificationsHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idParentOrganizationClassification = Convert.ToInt64(param["IdOrganizationClassification"]);
                        //Este parametro lo pasa en true cuando se carga un arbol de mapas.(entonces se debe hacer una doble pregunta para saber si tiene o no hijos)
                        Boolean _isLoadElementMap = false;
                        if (param.ContainsKey("IsLoadElementMap"))
                        { _isLoadElementMap = Convert.ToBoolean(param["IsLoadElementMap"]); }

                        if (_isLoadElementMap)
                        {
                            if ((EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idParentOrganizationClassification).ChildrenClassifications.Count > 0) ||
                                (EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idParentOrganizationClassification).ChildrenElements.Count > 0))
                            { return true; }
                        }
                        else
                        {
                            //El hijo solo va a ser clasificacion
                            if (EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idParentOrganizationClassification).ChildrenClassifications.Count > 0)
                            { return true; }
                        }
                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del GeographicArea.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable OrganizationClassification(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("OrganizationClassification");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        Int64 _idOrganizationClassification = Convert.ToInt64(param["IdOrganizationClassification"]);

                        OrganizationClassification _organizationClassification = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _organizationClassification.LanguageOption.Name);

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();

                        //Carga los datos
                        if (_organizationClassification.ParentOrganizationClassification != null)
                        {
                            _valueLink.Add("IdOrganizationClassification", _organizationClassification.IdParentOrganizationClassification);
                            _dt.Rows.Add(Resources.CommonListManage.ParentEntity, _organizationClassification.ParentOrganizationClassification.LanguageOption.Name,
                                GetValueLink(Common.ConstantsEntitiesName.DS.OrganizationClassification, _valueLink));
                        }
                        //_dt.Rows.Add(Resources.CommonListManage.IdOrganizationClassification, _organizationClassification.IdOrganizationClassification, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _organizationClassification.LanguageOption.Language.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Name, _organizationClassification.LanguageOption.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _organizationClassification.LanguageOption.Description, "#" + i++);

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> OrganizationClassificationFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idOrganizationClassification = Convert.ToInt64(param["IdOrganizationClassification"]);

                        //Accede al objeto y arma el KeyValue para insertar en la pila.
                        OrganizationClassification _organizationClassification = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification);
                        _keyValues = "IdOrganizationClassification=" + _idOrganizationClassification.ToString() + "& IdParentOrganizationClassification=" + _organizationClassification.IdParentOrganizationClassification.ToString();
                        //Inserta el primer dato en la pila.
                        _parents.Push(_keyValues);
                        //Ahora recorre toda su familia y guarda los keys en la pila.
                        while (_organizationClassification.IdParentOrganizationClassification != 0)
                        {
                            _keyValues = "IdOrganizationClassification=" + _organizationClassification.IdParentOrganizationClassification.ToString() + "& IdParentOrganizationClassification=" + _organizationClassification.ParentOrganizationClassification.IdParentOrganizationClassification.ToString();
                            _parents.Push(_keyValues);
                            //Sigue con el proximo.
                            _organizationClassification = _organizationClassification.ParentOrganizationClassification; // EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_organizationClassification.IdParentOrganizationClassification);
                        }

                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                #endregion

                #region Organizations
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Organization.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Organizations(Dictionary<String, Object> param)
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

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            Dictionary<Int64, Organization> _organizations = new Dictionary<Int64, Organization>();
                            //Verifica que ahora en los parametros venga el key que se esta esperando....
                            if (param.ContainsKey("IdOrganizationClassification"))
                            {
                                //Obtiene el OrganizationClassification
                                Int64 _idOrganizationClassification = Convert.ToInt64(param["IdOrganizationClassification"]);

                                _organizations = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification).ChildrenElements;
                                ////Ya esta armado el DataTable, ahora lo carga
                                //foreach (Organization _organization in EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification).ChildrenElements.Values)
                                //{
                                //    String _permissionType = String.Empty;
                                //    //Obtiene el permiso que tiene el usuario para esa organizacion.
                                //    if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                //        { _permissionType = Common.Constants.PermissionManageName; }
                                //    else
                                //        { _permissionType = Common.Constants.PermissionViewName; }

                                //    _dt.Rows.Add(_organization.IdOrganization, _organization.CorporateName, _organization.Name, _organization.FiscalIdentification, _permissionType);
                                //}
                            }
                            else
                            {
                                _organizations = EMSLibrary.User.DirectoryServices.Map.OrganizationRoots();
                                ////Ya esta armado el DataTable, ahora lo carga
                                //foreach (Organization _organization in EMSLibrary.User.DirectoryServices.Map.OrganizationRoots().Values)
                                //{
                                //    String _permissionType = String.Empty;
                                //    //Obtiene el permiso que tiene el usuario para esa organizacion.
                                //    if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                //    { _permissionType = Common.Constants.PermissionManageName; }
                                //    else
                                //    { _permissionType = Common.Constants.PermissionViewName; }

                                //    _dt.Rows.Add(_organization.IdOrganization, _organization.CorporateName, _organization.Name, _organization.FiscalIdentification, _permissionType);
                                //}
                            }
                            var _lnqOrganization = from organization in _organizations.Values
                                                   orderby organization.CorporateName ascending
                                                   select organization;
                            //Ya esta armado el DataTable, ahora lo carga
                            foreach (Organization _organization in _lnqOrganization)
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
                    /// Construye el DataTable a modo Property con los datos del Organization.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Organization(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Organization");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));                        

                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);

                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _organization.CorporateName);

                        //Carga los datos
                        //_dt.Rows.Add(Resources.CommonListManage.IdOrganization, _organization.IdOrganization);
                        _dt.Rows.Add(Resources.CommonListManage.CorporateName, _organization.CorporateName);
                        _dt.Rows.Add(Resources.CommonListManage.Name, _organization.Name);
                        _dt.Rows.Add(Resources.CommonListManage.FiscalIdentification, _organization.FiscalIdentification);

                        //Retorna el DataTable.
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa Organization tiene hijos o no.(Los elementos no tienen hijos...)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean OrganizationsHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);

                        if (EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Posts().Count > 0)
                        { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Indica si esta Organization tiene OrganizationalChart.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean OrganizationsHasOrganizationalChart(Dictionary<String, Object> param)
                    {
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);

                        if (EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).OrganizationalCharts.Count > 0)
                        { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Construye el Dictionary de catalogos para la Organizacion
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> OrganizationPictures(Dictionary<String, Object> param)
                    {
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);

                        return EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Pictures;
                    }

                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Organization WithFacility.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable OrganizationsWithFacility(Dictionary<String, Object> param)
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

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            Dictionary<Int64, Organization> _organizations = new Dictionary<Int64, Organization>();
                            //Verifica que ahora en los parametros venga el key que se esta esperando....
                            if (param.ContainsKey("IdOrganizationClassification"))
                            {
                                //Obtiene el OrganizationClassification
                                Int64 _idOrganizationClassification = Convert.ToInt64(param["IdOrganizationClassification"]);

                                _organizations = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification).ChildrenElements;
                                ////Ya esta armado el DataTable, ahora lo carga
                                //foreach (Organization _organization in EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification).ChildrenElements.Values)
                                //{
                                //    //Solo carga si tiene facilities
                                //    if (_organization.Facilities.Count > 0)
                                //    {
                                //        String _permissionType = String.Empty;
                                //        //Obtiene el permiso que tiene el usuario para esa organizacion.
                                //        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                //        { _permissionType = Common.Constants.PermissionManageName; }
                                //        else
                                //        { _permissionType = Common.Constants.PermissionViewName; }

                                //        _dt.Rows.Add(_organization.IdOrganization, _organization.CorporateName, _organization.Name, _organization.FiscalIdentification, _permissionType);

                                //    }
                                //}
                            }
                            else
                            {
                                _organizations = EMSLibrary.User.DirectoryServices.Map.OrganizationRoots();
                                ////Ya esta armado el DataTable, ahora lo carga
                                //foreach (Organization _organization in EMSLibrary.User.DirectoryServices.Map.OrganizationRoots().Values)
                                //{
                                //    //Solo carga si tiene facilities
                                //    if (_organization.Facilities.Count > 0)
                                //    {
                                //        String _permissionType = String.Empty;
                                //        //Obtiene el permiso que tiene el usuario para esa organizacion.
                                //        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                //        { _permissionType = Common.Constants.PermissionManageName; }
                                //        else
                                //        { _permissionType = Common.Constants.PermissionViewName; }

                                //        _dt.Rows.Add(_organization.IdOrganization, _organization.CorporateName, _organization.Name, _organization.FiscalIdentification, _permissionType);
                                //    }
                                //}
                            }
                            var _lnqOrganization = from organization in _organizations.Values
                                                   where organization.Facilities != null
                                                   && organization.Facilities.Count > 0
                                                   orderby organization.CorporateName ascending
                                                   select organization;
                            //Ya esta armado el DataTable, ahora lo carga
                            foreach (Organization _organization in _lnqOrganization)
                            {
                                //Solo carga si tiene facilities
                                //if (_organization.Facilities.Count > 0)
                                //{
                                    String _permissionType = String.Empty;
                                    //Obtiene el permiso que tiene el usuario para esa organizacion.
                                    if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                    { _permissionType = Common.Constants.PermissionManageName; }
                                    else
                                    { _permissionType = Common.Constants.PermissionViewName; }

                                    _dt.Rows.Add(_organization.IdOrganization, _organization.CorporateName, _organization.Name, _organization.FiscalIdentification, _permissionType);
                                //}
                            }
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa Organization WithFacility tiene hijos o no.(Los elementos no tienen hijos...)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean OrganizationsWithFacilityHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);

                        if (EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Facilities.Count > 0)
                        { return true; }

                        return false;
                    }

                    ///// <summary>
                    ///// Indica si esa Organizacion tiene hijos o no.
                    ///// </summary>
                    ///// <param name="param">Parametros opcionales para filtrar</param>
                    ///// <returns>Un<c>Boolean</c></returns>
                    //public Boolean OrganizationsHasChildren(Dictionary<String, Object> param)
                    //{
                    //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);

                    //    if (EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).SubordinateClassifications().Count > 0)
                    //    { return true; }

                    //    return false;
                    //}
                #endregion

                #region Functional Areas
                    /// <summary>
                    /// Construye las columnas del datatable de FunctionalArea
                    /// </summary>
                    /// <param name="dt"></param>
                    private void FunctionalAreaColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdFunctionalArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdFunctionalArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdOrganization;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdOrganization", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentFunctionalArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "IdParentFunctionalArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.FunctionalArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion FunctionalArea (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable FunctionalAreas(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("FunctionalArea");

                        //Contruye las columnas y sus atributos.
                        FunctionalAreaColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            //Verifica que ahora en los parametros venga el key que se esta esperando....
                            if (param.ContainsKey("IdOrganization"))
                            {
                                //Obtiene el Applicability
                                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                                String _permissionType = String.Empty;
                                //Obtiene el permiso que tiene el usuario para esa organizacion.
                                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                    { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                    { _permissionType = Common.Constants.PermissionViewName; }

                                var _lnqFunArea = from funArea in _organization.FunctionalAreas().Values
                                                  orderby funArea.LanguageOption.Name ascending
                                                  select funArea;
                                //Ya esta armado el DataTable, ahora lo carga 
                                foreach (FunctionalArea _functionalArea in _lnqFunArea)
                                {
                                    _dt.Rows.Add(_functionalArea.IdFunctionalArea, _functionalArea.IdOrganization, _functionalArea.IdParentFunctionalArea, _functionalArea.LanguageOption.Name, _permissionType);
                                }
                            }
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un Functional Area.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable FunctionalAreasChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("FunctionalArea");

                        //Contruye las columnas y sus atributos.
                        FunctionalAreaColumns(ref _dt);

                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idParentFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqFunArea = from funArea in _organization.FunctionalArea(_idParentFunctionalArea).Children.Values
                                          orderby funArea.LanguageOption.Name ascending
                                          select funArea;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (FunctionalArea _functionalArea in _lnqFunArea)
                        {
                            _dt.Rows.Add(_functionalArea.IdFunctionalArea, _functionalArea.IdOrganization, _functionalArea.IdParentFunctionalArea, _functionalArea.LanguageOption.Name, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa Functional Area tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean FunctionalAreasHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idParentFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);

                        if (EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).FunctionalArea(_idParentFunctionalArea).Children.Count > 0)
                        { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del FunctionalArea.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable FunctionalArea(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("FunctionalArea");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);

                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        FunctionalArea _functionalArea = _organization.FunctionalArea(_idFunctionalArea);
                        FunctionalArea _parentFunctionalArea = _organization.FunctionalArea(_functionalArea.IdParentFunctionalArea);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _functionalArea.LanguageOption.Name);

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();
                        _valueLink.Add("IdOrganization", _functionalArea.IdOrganization);

                        //Carga los datos
                        if (_parentFunctionalArea != null)
                        {
                            _valueLink.Add("IdFunctionalArea", _functionalArea.IdParentFunctionalArea);
                            _dt.Rows.Add(Resources.CommonListManage.ParentEntity, _parentFunctionalArea.LanguageOption.Name,
                                GetValueLink(Common.ConstantsEntitiesName.DS.FunctionalArea, _valueLink));
                        }
                        //_dt.Rows.Add(Resources.CommonListManage.IdFunctionalArea, _functionalArea.IdFunctionalArea, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Organization, _organization.CorporateName,
                            GetValueLink(Common.ConstantsEntitiesName.DS.Organization, _valueLink));
                        _dt.Rows.Add(Resources.CommonListManage.Name, _functionalArea.LanguageOption.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _functionalArea.LanguageOption.Language.Name,i++);
                        

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> FunctionalAreaFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);

                        //Si no hay organizacion, no puede crear nada.
                        if (_idOrganization != 0)
                        {
                            //Accede al objeto y arma el KeyValue para insertar en la pila.
                            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                            FunctionalArea _functionalArea = _organization.FunctionalArea(_idFunctionalArea);
                            _keyValues = "IdFunctionalArea=" + _idFunctionalArea.ToString() + "& IdOrganization=" + _idOrganization.ToString();
                            //Inserta el primer dato en la pila.
                            _parents.Push(_keyValues);
                            //Ahora recorre toda su familia y guarda los keys en la pila.
                            while (_functionalArea.IdParentFunctionalArea != 0)
                            {
                                _keyValues = "IdFunctionalArea=" + _functionalArea.IdParentFunctionalArea.ToString() + "& IdOrganization=" + _idOrganization.ToString();
                                _parents.Push(_keyValues);
                                //Sigue con el proximo.
                                _functionalArea = _organization.FunctionalArea(_functionalArea.IdParentFunctionalArea);
                            }
                        }
                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                #endregion

                #region Functional Positions
                    /// <summary>
                    /// Construye las columnas del datatable de FunctionalPositions
                    /// </summary>
                    /// <param name="dt"></param>
                    private void FunctionalPositionColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdFunctionalArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdFunctionalArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdPosition;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdPosition", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdOrganization;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdOrganization", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentFunctionalArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "IdParentFunctionalArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentPosition;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "IdParentPosition", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.FunctionalPosition;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion FunctionalPositions (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable FunctionalPositions(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("FunctionalPosition");

                        //Contruye las columnas y sus atributos.
                        FunctionalPositionColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            //Verifica que ahora en los parametros venga el key que se esta esperando....
                            if (param.ContainsKey("IdOrganization"))
                            {
                                //Obtiene el Applicability
                                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                                String _permissionType = String.Empty;
                                //Obtiene el permiso que tiene el usuario para esa organizacion.
                                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                var _lnqFunPos = from funPos in _organization.FunctionalPositions()
                                                 orderby funPos.Name() ascending
                                                 select funPos;
                                //Ya esta armado el DataTable, ahora lo carga
                                foreach (FunctionalPosition _functionalPosition in _lnqFunPos)
                                {
                                    _dt.Rows.Add(_functionalPosition.FunctionalArea.IdFunctionalArea, _functionalPosition.Position.IdPosition, _idOrganization, _functionalPosition.FunctionalArea.IdFunctionalArea, _functionalPosition.Position.IdPosition, _functionalPosition.Name(), _permissionType);
                                }
                            }
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un Functional Position.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable FunctionalPositionsChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("FunctionalPosition");

                        //Contruye las columnas y sus atributos.
                        FunctionalPositionColumns(ref _dt);

                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idParentPosition = Convert.ToInt64(param["IdPosition"]);
                        Int64 _idParentFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
                        //Ya esta armado el DataTable, ahora lo carga
                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        Position _position = _organization.Position(_idParentPosition);
                        FunctionalArea _funArea = _organization.FunctionalArea(_idParentFunctionalArea);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqFunPos = from funPos in _organization.FunctionalPosition(_position, _funArea).Children
                                         orderby funPos.Name() ascending
                                         select funPos;
                        foreach (FunctionalPosition _functionalPosition in _lnqFunPos)
                        {
                            _dt.Rows.Add(_functionalPosition.FunctionalArea.IdFunctionalArea, _functionalPosition.Position.IdPosition, _idOrganization, _functionalPosition.IdParentFunctionalArea, _functionalPosition.IdParentPosition, _functionalPosition.Name(), _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa Functional Position tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean FunctionalPositionsHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idParentPosition = Convert.ToInt64(param["IdPosition"]);
                        Int64 _idParentFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);

                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        Position _position = _organization.Position(_idParentPosition);
                        FunctionalArea _funArea = _organization.FunctionalArea(_idParentFunctionalArea);

                        if (_organization.FunctionalPosition(_position, _funArea).Children.Count > 0)
                        { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del FunctionalPosition.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable FunctionalPosition(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("FunctionalPosition");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
                        Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);

                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        Position _position = _organization.Position(_idPosition);
                        FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);

                        FunctionalPosition _functionalPosition = _organization.FunctionalPosition(_position, _funArea);
                        FunctionalPosition _parentFunctionalPosition = _functionalPosition.ParentFunctionalPosition;

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _functionalPosition.Name());

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();
                        _valueLink.Add("IdOrganization", _organization.IdOrganization);

                        //Carga los datos
                        if (_parentFunctionalPosition != null)
                        {
                            _valueLink.Add("IdFunctionalArea", _parentFunctionalPosition.FunctionalArea.IdFunctionalArea);
                            _valueLink.Add("IdPosition", _parentFunctionalPosition.Position.IdPosition);

                            _dt.Rows.Add(Resources.CommonListManage.ParentEntity, _parentFunctionalPosition.Name(),
                                GetValueLink(Common.ConstantsEntitiesName.DS.FunctionalPosition, _valueLink));
                            _valueLink.Remove("IdFunctionalArea");
                            _valueLink.Remove("IdPosition");
                        }

                        _valueLink.Add("IdFunctionalArea", _functionalPosition.FunctionalArea.IdFunctionalArea);
                        _valueLink.Add("IdPosition", _functionalPosition.Position.IdPosition);

                        _dt.Rows.Add(Resources.CommonListManage.FunctionalArea, _functionalPosition.FunctionalArea.LanguageOption.Name,
                            GetValueLink(Common.ConstantsEntitiesName.DS.FunctionalArea, _valueLink));
                        _dt.Rows.Add(Resources.CommonListManage.Position, _functionalPosition.Position.LanguageOption.Name,
                            GetValueLink(Common.ConstantsEntitiesName.DS.Position, _valueLink));
                        _dt.Rows.Add(Resources.CommonListManage.Organization, _organization.CorporateName,
                            GetValueLink(Common.ConstantsEntitiesName.DS.Organization, _valueLink));
                        _dt.Rows.Add(Resources.CommonListManage.Name, _functionalPosition.Name(), "#" + i++);

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> FunctionalPositionFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
                        Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);

                        //Si no hay organizacion, no puede crear nada.
                        if (_idOrganization != 0)
                        {
                            //Accede al objeto y arma el KeyValue para insertar en la pila.
                            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                            Position _position = _organization.Position(_idPosition);
                            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);

                            FunctionalPosition _functionalPosition = _organization.FunctionalPosition(_position, _funArea);
                            _keyValues = "IdFunctionalArea=" + _idFunctionalArea.ToString() + "& IdPosition=" + _idPosition.ToString() + "& IdOrganization=" + _idOrganization.ToString();
                            //Inserta el primer dato en la pila.
                            _parents.Push(_keyValues);
                            //Ahora recorre toda su familia y guarda los keys en la pila.
                            while (_functionalPosition.IdParentFunctionalArea != 0)
                            {
                                _keyValues = "IdFunctionalArea=" + _functionalPosition.IdParentFunctionalArea.ToString() + "& IdPosition=" + _functionalPosition.IdParentPosition.ToString() + "& IdOrganization=" + _idOrganization.ToString();
                                _parents.Push(_keyValues);
                                //Sigue con el proximo.
                                _functionalPosition = _functionalPosition.ParentFunctionalPosition; // _organization.FunctionalPosition(_functionalPosition.Position, _functionalPosition.FunctionalArea);
                            }
                        }
                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                #endregion

                #region Geographic Areas
                    /// <summary>
                    /// Construye las columnas del datatable de GeographicArea
                    /// </summary>
                    /// <param name="dt"></param>
                    private void GeographicAreaColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdGeographicArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdGeographicArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentGeographicArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "IdParentGeographicArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.GeographicArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Alpha;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "Alpha", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion GeographicArea (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable GeographicAreas(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("GeographicArea");

                        //Contruye las columnas y sus atributos.
                        GeographicAreaColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            String _permissionType = String.Empty;
                            //Obtiene el permiso que tiene el usuario para esa organizacion.
                            if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            var _lnqGeoArea = from g in EMSLibrary.User.GeographicInformationSystem.GeographicAreas().Values
                                              orderby g.LanguageOption.Name ascending
                                              select g;
                            //Ya esta armado el DataTable, ahora lo carga
                            foreach (GeographicArea _geographicArea in _lnqGeoArea)
                            {
                                _dt.Rows.Add(_geographicArea.IdGeographicArea, _geographicArea.IdParentGeographicArea, _geographicArea.LanguageOption.Name, _geographicArea.LanguageOption.Description, _geographicArea.ISOCode, _permissionType);
                            }
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un Geographic Area.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable GeographicAreasChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("GeographicArea");

                        //Contruye las columnas y sus atributos.
                        GeographicAreaColumns(ref _dt);

                        //Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idParentGeoArea = Convert.ToInt64(param["IdGeographicArea"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        //Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqGeoArea = from g in EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idParentGeoArea).Children.Values
                                          orderby g.LanguageOption.Name ascending
                                          select g;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (GeographicArea _geographicArea in _lnqGeoArea)
                        {
                            _dt.Rows.Add(_geographicArea.IdGeographicArea, _geographicArea.IdParentGeographicArea, _geographicArea.LanguageOption.Name, _geographicArea.LanguageOption.Description, _geographicArea.ISOCode, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa Geographic Area tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean GeographicAreasHasChildren(Dictionary<String, Object> param)
                    {
                        //Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idParentGeoArea = Convert.ToInt64(param["IdGeographicArea"]);

                        if (EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idParentGeoArea).Children.Count > 0)
                        { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del GeographicArea.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable GeographicArea(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("GeographicArea");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        //Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);

                        //Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        GeographicArea _geographicArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                        GeographicArea _parentGeographicArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_geographicArea.IdParentGeographicArea);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _geographicArea.LanguageOption.Name);

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();
                        //_valueLink.Add("IdOrganization", _organization.IdOrganization);

                        //Carga los datos
                        if (_parentGeographicArea != null)
                        {
                            _valueLink.Add("IdGeographicArea", _geographicArea.IdParentGeographicArea);
                            _dt.Rows.Add(Resources.CommonListManage.ParentEntity, _parentGeographicArea.LanguageOption.Name,
                                GetValueLink(Common.ConstantsEntitiesName.DS.GeographicArea, _valueLink));
                        }
                        //_dt.Rows.Add(Resources.CommonListManage.Organization, _organization.CorporateName,
                            //GetValueLink(Common.ConstantsEntitiesName.DS.Organization, _valueLink));  
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _geographicArea.LanguageOption.Language.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Name, _geographicArea.LanguageOption.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _geographicArea.LanguageOption.Description, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Alpha, _geographicArea.ISOCode, "#" + i++);

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> GeographicAreaFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        //Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);

                        //Si no hay organizacion, no puede crear nada.
                        //if (_idOrganization != 0)
                        //{
                            //Accede al objeto y arma el KeyValue para insertar en la pila.
                            //Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                            GeographicArea _geographicArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                            _keyValues = "IdGeographicArea=" + _idGeographicArea.ToString();
                            //Inserta el primer dato en la pila.
                            _parents.Push(_keyValues);
                            //Ahora recorre toda su familia y guarda los keys en la pila.
                            while (_geographicArea.IdParentGeographicArea != 0)
                            {
                                _keyValues = "IdGeographicArea=" + _geographicArea.IdParentGeographicArea.ToString();
                                _parents.Push(_keyValues);
                                //Sigue con el proximo.
                                _geographicArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_geographicArea.IdParentGeographicArea);
                            }
                        //}
                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                    ///// <summary>
                    ///// Construye el Dictionary de catalogos para un Facility
                    ///// </summary>
                    ///// <param name="param">Parametros opcionales para filtrar</param>
                    ///// <returns>Un <c>DataTable</c></returns>
                    //public Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> GeographicAreaPictures(Dictionary<String, Object> param)
                    //{
                    //    return GeographicAreaFacilityPictures(param);
                    //}
                #endregion

                #region Facility
                    /// <summary>
                    /// Construye las columnas del datatable de Facility
                    /// </summary>
                    /// <param name="dt"></param>
                    private void FacilityColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdOrganization;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdOrganization", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdFacility;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdFacility", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Facility;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.FacilityType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "FacilityType", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Facilities
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Facilities(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Facilities");

                        //Contruye las columnas y sus atributos.
                        FacilityColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            if (param.ContainsKey("IdFacilityType"))
                            {
                                Int64 _idFacilityType = Convert.ToInt64(param["IdFacilityType"]);
                                FacilityType _facilityType = EMSLibrary.User.GeographicInformationSystem.FacilityType(_idFacilityType);

                                String _permissionType = String.Empty;
                                //Obtiene el permiso que tiene el usuario para esa organizacion.
                                if (EMSLibrary.User.DirectoryServices.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                if (param.ContainsKey("IdOrganization"))
                                {
                                    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                                    var _lnqFacilities = from _facilities in _facilityType.Facilities.Values
                                                         where _facilities.Organization.IdOrganization == _idOrganization
                                                         orderby _facilities.LanguageOption.Name ascending
                                                         select _facilities;

                                    //Ya esta armado el DataTable, ahora lo carga
                                    foreach (Facility _facility in _lnqFacilities)
                                    {
                                        _dt.Rows.Add(_facility.IdOrganization, _facility.IdFacility, _facility.LanguageOption.Name, _facility.LanguageOption.Description, _facility.FacilityType.LanguageOption.Name, _permissionType);
                                    }
                                }
                                else
                                {
                                    //Ya esta armado el DataTable, ahora lo carga
                                    foreach (Facility _facility in _facilityType.Facilities.Values)
                                    {
                                        _dt.Rows.Add(_facility.IdOrganization, _facility.IdFacility, _facility.LanguageOption.Name, _facility.LanguageOption.Description, _facility.FacilityType.LanguageOption.Name, _permissionType);
                                    }
                                }
                            }
                            else
                            {
                                if (param.ContainsKey("IdOrganization"))
                                {
                                    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);

                                    String _permissionType = String.Empty;
                                    //Obtiene el permiso que tiene el usuario para esa organizacion.
                                    if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                    { _permissionType = Common.Constants.PermissionManageName; }
                                    else
                                    { _permissionType = Common.Constants.PermissionViewName; }

                                    //Ya esta armado el DataTable, ahora lo carga
                                    foreach (Facility _facility in _organization.Facilities.Values)
                                    {
                                        _dt.Rows.Add(_idOrganization, _facility.IdFacility, _facility.LanguageOption.Name, _facility.LanguageOption.Description, _facility.FacilityType.LanguageOption.Name, _permissionType);
                                    }
                                }                                
                            }
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del Facility.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Facility(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Facility");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);

                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        Facility _facility = _organization.Facility(_idFacility);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _facility.LanguageOption.Name);

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();
                        _valueLink.Add("IdOrganization", _organization.IdOrganization);
                        _valueLink.Add("IdFacilityType", _facility.FacilityType.IdFacilityType);

                        _dt.Rows.Add(Resources.CommonListManage.Organization, _organization.CorporateName,
                            GetValueLink(Common.ConstantsEntitiesName.DS.Organization, _valueLink));
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _facility.LanguageOption.Language.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Name, _facility.LanguageOption.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _facility.LanguageOption.Description, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.FacilityType, _facility.FacilityType.LanguageOption.Name,
                            GetValueLink(Common.ConstantsEntitiesName.DS.FacilityType, _valueLink));

                        String _geographicAreaName = Resources.Common.Unassigned;
                        if (_facility.GeographicArea != null)
                        {
                            _geographicAreaName = _facility.GeographicArea.LanguageOption.Name;
                        }
                        _dt.Rows.Add(Resources.CommonListManage.GeographicArea, _geographicAreaName, "#" + i++);

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esta Facilities tiene hijos o no.(Los elementos no tienen hijos...)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean FacilitiesHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);

                        if (EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Facility(_idFacility).Sectors.Count > 0)
                        { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Indica si esta Facilities tiene hijos o no.(Los elementos no tienen hijos...)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean FacilityTypesHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idFacilityType = Convert.ToInt64(param["IdFacilityType"]);

                        if (EMSLibrary.User.GeographicInformationSystem.FacilityType(_idFacilityType).Facilities.Count > 0)
                        { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> FacilityFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);

                        //Si no hay organizacion, no puede crear nada.
                        if (_idOrganization != 0)
                        {
                            //Accede al objeto y arma el KeyValue para insertar en la pila.
                            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                            Facility _facility = _organization.Facility(_idFacility);
                            //Como no hay jerarquia en el facility, Primero agrega el facility
                            _keyValues = "IdOrganization=" + _idOrganization.ToString() + "& IdFacility=" + _idFacility.ToString();
                            //Inserta el primer dato en la pila.
                            _parents.Push(_keyValues);

                            //Ahora agrega la organizacion
                            _keyValues = "IdOrganization=" + _idOrganization.ToString();
                            //Inserta el primer dato en la pila.
                            _parents.Push(_keyValues);
                                                    }
                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                    /// <summary>
                    /// Construye el Dictionary de catalogos para un Facility
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> FacilityPictures(Dictionary<String, Object> param)
                    {
                        //Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);

                        //return EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).Facility(_idFacility).Pictures;
                        return EMSLibrary.User.GeographicInformationSystem.Site(_idFacility).Pictures;
                    }
                #endregion

                #region Sectores
                    /// <summary>
                    /// Construye las columnas del datatable de Sectores
                    /// </summary>
                    /// <param name="dt"></param>
                    private void SectorColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdOrganization;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdOrganization", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdFacility;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdFacility", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdSector;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdSector", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentSector;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "IdParentSector", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Sector;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Sector (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Sectors(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Sector");

                        //Contruye las columnas y sus atributos.
                        SectorColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            String _permissionType = String.Empty;
                            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                            Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                            //Facility _facility = _organization.Facility(_idFacility);
                            Site _facility = EMSLibrary.User.GeographicInformationSystem.Site(_idFacility);

                            //Obtiene el permiso que tiene el usuario para esa organizacion.
                            if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Ya esta armado el DataTable, ahora lo carga
                            foreach (Sector _sector in _facility.Sectors.Values)
                            {
                                Int64 _idParentSector = 0;
                                if (_sector.Parent.IdFacility != _idFacility) //si son iguales, quiere decir que no tiene sectores superiores.
                                {
                                    //En este caso, quiere decir que tiene un sector superior, entonces lo mete en el objeto.
                                    _idParentSector = _sector.Parent.IdFacility; //Lo guarda aca, para seguir usandolo luego.
                                }

                                _dt.Rows.Add(_sector.IdOrganization, _idFacility, _sector.IdFacility, _idParentSector, _sector.LanguageOption.Name, _sector.LanguageOption.Description, _permissionType);
                            }
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un Sector.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable SectorsChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Sector");

                        //Contruye las columnas y sus atributos.
                        SectorColumns(ref _dt);

                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                        Int64 _idSector = Convert.ToInt64(param["IdSector"]);
                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        Facility _facility = _organization.Facility(_idFacility);

                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Sector _sector in _facility.Sector(_idSector).Sectors.Values)
                        {
                            Int64 _idParentSector = 0;
                            if (_sector.Parent.IdFacility != _idFacility) //si son iguales, quiere decir que no tiene sectores superiores.
                            {
                                //En este caso, quiere decir que tiene un sector superior, entonces lo mete en el objeto.
                                _idParentSector = _sector.Parent.IdFacility; //Lo guarda aca, para seguir usandolo luego.
                            }
                            _dt.Rows.Add(_sector.IdOrganization, _idFacility, _sector.IdFacility, _idParentSector, _sector.LanguageOption.Name, _sector.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si ese Sector tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean SectorsHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                        Int64 _idSector = Convert.ToInt64(param["IdSector"]);
                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        Facility _facility = _organization.Facility(_idFacility);

                        if (_facility.Sector(_idSector).Sectors.Count > 0)
                        { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del Sector.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable Sector(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Sector");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                        Int64 _idSector = Convert.ToInt64(param["IdSector"]);
                        Int64 _idParentSector = 0;
                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        Facility _facility = _organization.Facility(_idFacility);
                        Sector _sector = _facility.Sector(_idSector);
                        
                        Sector _parentSector = null;
                        if (_sector.Parent.IdFacility != _idFacility) //si son iguales, quiere decir que no tiene sectores superiores.
                        {
                            //En este caso, quiere decir que tiene un sector superior, entonces lo mete en el objeto.
                            _idParentSector = _sector.Parent.IdFacility; //Lo guarda aca, para seguir usandolo luego.
                            _parentSector = _facility.Sector(_idParentSector);
                        }
                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _sector.LanguageOption.Name);

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();
                        _valueLink.Add("IdOrganization", _idOrganization);
                        _valueLink.Add("IdFacility", _idFacility);
                        _valueLink.Add("IdSector", _idSector);
                        _valueLink.Add("IdFacilityType", _sector.FacilityType.IdFacilityType);

                        //Carga los datos
                        if (_parentSector != null)
                        {
                            _valueLink.Add("IdParentSector", _idParentSector);
                            _dt.Rows.Add(Resources.CommonListManage.ParentEntity, _parentSector.LanguageOption.Name,
                                GetValueLink(Common.ConstantsEntitiesName.DS.Sector, _valueLink));
                        }
                        _dt.Rows.Add(Resources.CommonListManage.Organization, _organization.CorporateName,
                            GetValueLink(Common.ConstantsEntitiesName.DS.Organization, _valueLink));  
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _sector.LanguageOption.Language.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Name, _sector.LanguageOption.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _sector.LanguageOption.Description, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.FacilityType, _sector.FacilityType.LanguageOption.Name,
                            GetValueLink(Common.ConstantsEntitiesName.DS.FacilityType, _valueLink));

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> SectorFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                        Int64 _idSector = Convert.ToInt64(param["IdSector"]);

                        Boolean _isFamilyFullSite = false;
                        if (param.ContainsKey("isFamilyFullSite"))
                        {
                            _isFamilyFullSite = Convert.ToBoolean(param["isFamilyFullSite"]);
                        }
                            //Si no hay organizacion, no puede crear nada.
                        if (_idOrganization != 0)
                        {
                            //Accede al objeto y arma el KeyValue para insertar en la pila.
                            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                            Facility _facility = _organization.Facility(_idFacility);
                            Sector _sector = _facility.Sector(_idSector);

                            _keyValues = "IdOrganization=" + _idOrganization.ToString() + "& IdFacility=" + _idFacility.ToString() + "& IdSector=" + _idSector.ToString();
                            //Inserta el primer dato en la pila.
                            _parents.Push(_keyValues);
                            //Ahora recorre toda su familia y guarda los keys en la pila.
                            while (_sector.Parent.IdFacility != _idFacility) //Si el id del parent es = al idfacility que ya tengo quiere decir que es un root de sector.
                            {
                                _keyValues = "IdOrganization=" + _idOrganization.ToString() + "& IdFacility=" + _idFacility.ToString() + "& IdSector=" + _sector.Parent.IdFacility.ToString();
                                _parents.Push(_keyValues);
                                //Sigue con el proximo.
                                _sector = _facility.Sector(_sector.Parent.IdFacility);
                            }
                            //Si esta indicado como que tiene que reconstruir el arbol del Site (completo), agrega los key del facility y de la organizacion
                            if (_isFamilyFullSite)
                            {
                                //Al salir de este ciclo, tengo el facility!!!
                                _keyValues = "IdOrganization=" + _idOrganization.ToString() + "& IdFacility=" + _idFacility.ToString();
                                _parents.Push(_keyValues);

                                //y ahora agrego la organizacion!!
                                _keyValues = "IdOrganization=" + _idOrganization.ToString();
                                _parents.Push(_keyValues);
                            }
                        }
                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                    //public Stack<String> SectorFamily(Dictionary<String, Object> param)
                    //{
                    //    //Define la pila donde se guardaran los KeyValues
                    //    Stack<String> _parents = new Stack<String>();
                    //    String _keyValues = String.Empty;   //string con el KeyValues para armar.
                    //    //Obtiene los parametros inciales del registro a buscar (sus PK)
                    //    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                    //    Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                    //    Int64 _idSector = Convert.ToInt64(param["IdSector"]);

                    //    //Si no hay organizacion, no puede crear nada.
                    //    if (_idOrganization != 0)
                    //    {
                    //        //Accede al objeto y arma el KeyValue para insertar en la pila.
                    //        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                    //        Facility _facility = _organization.Facility(_idFacility);
                    //        Sector _sector = _facility.Sector(_idSector);

                    //        _keyValues = "IdOrganization=" + _idOrganization.ToString() + "& IdFacility=" + _idFacility.ToString() + "& IdSector=" + _idSector.ToString();
                    //        //Inserta el primer dato en la pila.
                    //        _parents.Push(_keyValues);
                    //        //Ahora recorre toda su familia y guarda los keys en la pila.
                    //        while (_sector.Parent.IdFacility != _idFacility) //Si el id del parent es = al idfacility que ya tengo quiere decir que es un root de sector.
                    //        {
                    //            _keyValues = "IdOrganization=" + _idOrganization.ToString() + "& IdFacility=" + _idFacility.ToString() + "& IdSector=" + _sector.Parent.IdFacility.ToString();
                    //            _parents.Push(_keyValues);
                    //            //Sigue con el proximo.
                    //            _sector = _facility.Sector(_sector.Parent.IdFacility);
                    //        }
                    //    }
                    //    //Finalmente retorna la pila cargada.
                    //    return _parents;
                    //}

                    /// <summary>
                    /// Construye el Dictionary de catalogos para un Sector
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> SectorPictures(Dictionary<String, Object> param)
                    {
                        //Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                        //Int64 _idSector = Convert.ToInt64(param["IdSector"]);
                        //Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        //Facility _facility = _organization.Facility(_idFacility);
                        //Sector _sector = _facility.Sector(_idSector);

                        //return _sector.Pictures;

                        return EMSLibrary.User.GeographicInformationSystem.Site(_idFacility).Pictures;
                    }
                #endregion

                #region Geographic Functional Areas
                    /// <summary>
                    /// Construye las columnas del datatable de GeographicFunctionalArea
                    /// </summary>
                    /// <param name="dt"></param>
                    private void GeographicFunctionalAreaColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdGeographicArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdGeographicArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdFunctionalArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdFunctionalArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdOrganization;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdOrganization", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentGeographicArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "IdParentGeographicArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentFunctionalArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "IdParentFunctionalArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.GeographicFunctionalArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion GeographicFunctionalArea (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable GeographicFunctionalAreas(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("GeographicFunctionalArea");

                        //Contruye las columnas y sus atributos.
                        GeographicFunctionalAreaColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            //Verifica que ahora en los parametros venga el key que se esta esperando....
                            if (param.ContainsKey("IdOrganization"))
                            {
                                //Obtiene el Applicability
                                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                                String _permissionType = String.Empty;
                                //Obtiene el permiso que tiene el usuario para esa organizacion.
                                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                //Ya esta armado el DataTable, ahora lo carga
                                foreach (GeographicFunctionalArea _geographicFunctionalArea in _organization.GeographicFunctionalAreas())
                                {
                                    _dt.Rows.Add(_geographicFunctionalArea.GeographicArea.IdGeographicArea, _geographicFunctionalArea.FunctionalArea.IdFunctionalArea, _idOrganization, _geographicFunctionalArea.IdParentGeographicArea, _geographicFunctionalArea.IdParentFunctionalArea, _geographicFunctionalArea.Name(), _permissionType);
                                }
                            }
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un Geographic Functional Area.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable GeographicFunctionalAreasChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("GeographicFunctionalArea");

                        //Contruye las columnas y sus atributos.
                        GeographicFunctionalAreaColumns(ref _dt);

                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idParentFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
                        Int64 _idParentGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);

                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        FunctionalArea _funArea = _organization.FunctionalArea(_idParentFunctionalArea);
                        GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idParentGeographicArea);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                        else
                            { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (GeographicFunctionalArea _geographicFunctionalArea in _organization.GeographicFunctionalArea(_funArea, _geoArea).Children)
                        {
                            _dt.Rows.Add(_geographicFunctionalArea.GeographicArea.IdGeographicArea, _geographicFunctionalArea.FunctionalArea.IdFunctionalArea, _idOrganization, _geographicFunctionalArea.IdParentGeographicArea, _geographicFunctionalArea.IdParentFunctionalArea, _geographicFunctionalArea.Name(), _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa Geographic Functional Area tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean GeographicFunctionalAreasHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idParentFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
                        Int64 _idParentGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);

                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        FunctionalArea _funArea = _organization.FunctionalArea(_idParentFunctionalArea);
                        GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idParentGeographicArea);

                        if (_organization.GeographicFunctionalArea(_funArea, _geoArea).Children.Count > 0)
                        { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del GeographicFunctionalArea.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable GeographicFunctionalArea(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("GeographicFunctionalArea");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
                        Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);

                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                        FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);

                        GeographicFunctionalArea _geographicFunctionalArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        GeographicFunctionalArea _parentGeographicFunctionalArea = _geographicFunctionalArea.ParentGeographicFunctionalArea;

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _geographicFunctionalArea.Name());

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();
                        _valueLink.Add("IdOrganization", _organization.IdOrganization);

                        //Carga los datos
                        if (_parentGeographicFunctionalArea != null)
                        {
                            _valueLink.Add("IdFunctionalArea", _parentGeographicFunctionalArea.FunctionalArea.IdFunctionalArea);
                            _valueLink.Add("IdGeographicArea", _parentGeographicFunctionalArea.GeographicArea.IdGeographicArea);
                            _dt.Rows.Add(Resources.CommonListManage.GeographicFunctionalArea, _parentGeographicFunctionalArea.Name(),
                                GetValueLink(Common.ConstantsEntitiesName.DS.GeographicFunctionalArea, _valueLink));
                            _valueLink.Remove("IdFunctionalArea");
                            _valueLink.Remove("IdGeographicArea");
                        }
                        _valueLink.Add("IdFunctionalArea", _geographicFunctionalArea.FunctionalArea.IdFunctionalArea);
                        _valueLink.Add("IdGeographicArea", _geographicFunctionalArea.GeographicArea.IdGeographicArea);
                        _dt.Rows.Add(Resources.CommonListManage.FunctionalArea, _geographicFunctionalArea.FunctionalArea.LanguageOption.Name,
                            GetValueLink(Common.ConstantsEntitiesName.DS.FunctionalArea, _valueLink));
                        _dt.Rows.Add(Resources.CommonListManage.GeographicArea, _geographicFunctionalArea.GeographicArea.LanguageOption.Name,
                            GetValueLink(Common.ConstantsEntitiesName.DS.GeographicArea, _valueLink));
                        _dt.Rows.Add(Resources.CommonListManage.Organization, _organization.CorporateName,
                        GetValueLink(Common.ConstantsEntitiesName.DS.Organization, _valueLink));
                        _dt.Rows.Add(Resources.CommonListManage.Name, _geographicFunctionalArea.Name(), "#" + i++);

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> GeographicFunctionalAreaFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
                        Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);

                        //Si no hay organizacion, no puede crear nada.
                        if (_idOrganization != 0)
                        {
                            //Accede al objeto y arma el KeyValue para insertar en la pila.
                            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                            GeographicFunctionalArea _geographicFunctionalArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                            _keyValues = "IdGeographicArea=" + _idGeographicArea.ToString() + "& IdFunctionalArea=" + _idFunctionalArea.ToString() + "& IdOrganization=" + _idOrganization.ToString();
                            //Inserta el primer dato en la pila.
                            _parents.Push(_keyValues);
                            //Ahora recorre toda su familia y guarda los keys en la pila.
                            while (_geographicFunctionalArea.IdParentFunctionalArea != 0)
                            {
                                _keyValues = "IdGeographicArea=" + _geographicFunctionalArea.IdParentGeographicArea.ToString() + "& IdFunctionalArea=" + _geographicFunctionalArea.IdParentFunctionalArea.ToString() + "& IdOrganization=" + _idOrganization.ToString();
                                _parents.Push(_keyValues);
                                //Sigue con el proximo.
                                _geographicFunctionalArea = _geographicFunctionalArea.ParentGeographicFunctionalArea;   // _organization.GeographicFunctionalArea(_geographicFunctionalArea.ParentGeographicFunctionalArea.FunctionalArea, _geographicFunctionalArea.ParentGeographicFunctionalArea.GeographicArea);
                            }
                        }
                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                #endregion

                #region Job Titles
                    /// <summary>
                    /// Construye las columnas del datatable de Job Title
                    /// </summary>
                    /// <param name="dt"></param>
                    private void JobTitleColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdGeographicArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdGeographicArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdPosition;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdPosition", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdFunctionalArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdFunctionalArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdOrganizationalChart;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdOrganizationalChart", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdOrganization;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdOrganization", _columnOptions);

                        //Parents...
                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentGeographicArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdParentGeographicArea", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentPosition;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdParentPosition", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentFunctionalArea;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdParentFunctionalArea", _columnOptions);


                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.JobTitle;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion JobTitles (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable JobTitles(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("JobTitles");

                        //Contruye las columnas y sus atributos.
                        JobTitleColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            //Verifica que ahora en los parametros venga el key que se esta esperando....
                            //if ((param.ContainsKey("IdOrganization")) && (param.ContainsKey("IdSubordinateClassification")))
                            if ((param.ContainsKey("IdOrganization")) && (param.ContainsKey("IdOrganizationalChart")))
                            {
                                //Obtiene el Applicability
                                Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                                Int64 _idOrganizationalChart = Convert.ToInt64(param["IdOrganizationalChart"]);
                                String _permissionType = String.Empty;
                                //Obtiene el permiso que tiene el usuario para esa organizacion.
                                Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                                if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }
                                

                                //Ya esta armado el DataTable, ahora lo carga
                                foreach (JobtitleWithChart _jobTitle in _organization.OrganizationalChart(_idOrganizationalChart).JobTitlesRoots())
                                {
                                    Int64 _idGeographicArea = _jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea;
                                    Int64 _idPosition = _jobTitle.FunctionalPositions.Position.IdPosition;
                                    Int64 _idFunctionalArea = _jobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea;

                                    //_dt.Rows.Add(_idGeographicArea, _idPosition, _idFunctionalArea, _jobTitle.IdOrganization, _jobTitle.Name());
                                    Int64 _idGeographicAreaParent=0;
                                    Int64 _idPositionParent = 0;
                                    Int64 _idFunctionalAreaParent = 0;
                                    if (_jobTitle.Parent != null)   //Si es null, quiere decir que es root.
                                    {
                                        _idGeographicAreaParent = _jobTitle.IdParentGeographicArea;
                                        _idPositionParent = _jobTitle.IdParentPosition;
                                        _idFunctionalAreaParent = _jobTitle.IdParentFunctionalArea;
                                    }

                                    _dt.Rows.Add(_idGeographicArea, _idPosition, _idFunctionalArea, _idOrganizationalChart,
                                        _jobTitle.Organization.IdOrganization,
                                        _idGeographicAreaParent,
                                        _idPositionParent,
                                        _idFunctionalAreaParent,
                                        _jobTitle.Name(), _permissionType);

                                }
                            }
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un JobTitle.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable JobTitlesChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("FunctionalArea");

                        //Contruye las columnas y sus atributos.
                        JobTitleColumns(ref _dt);

                        Int64 _idOrganizationalChart = Convert.ToInt64(param["IdOrganizationalChart"]);
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);

                        Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
                        Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
                        Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
                        //Int64 _idSubordinateClassification = Convert.ToInt64(param["IdSubordinateClassification"]);

                        //Ya esta armado el DataTable, ahora lo carga
                        //foreach (Subordinate _subordinate in EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).SubordinateClassification(_idSubordinateClassification).Subordinates().Values)
                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                        Position _position = _organization.Position(_idPosition);
                        FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                        FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        JobtitleWithChart _jobTitles = (JobtitleWithChart)_organization.OrganizationalChart(_idOrganizationalChart).JobTitle(_geoFunArea, _funPos);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                        else
                            { _permissionType = Common.Constants.PermissionViewName; }
                                

                        foreach (JobtitleWithChart _jobTitle in _jobTitles.Childrens)
                        {
                            Int64 _idGeographicAreaParent = 0;
                            Int64 _idPositionParent = 0;
                            Int64 _idFunctionalAreaParent = 0;
                            if (_jobTitle.Parent != null)   //Si es null, quiere decir que es root.
                            {
                                _idGeographicAreaParent = _jobTitle.IdParentGeographicArea;
                                _idPositionParent = _jobTitle.IdParentPosition;
                                _idFunctionalAreaParent = _jobTitle.IdParentFunctionalArea;
                            }

                            _dt.Rows.Add(_jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, 
                                _jobTitle.FunctionalPositions.Position.IdPosition, 
                                _jobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, 
                                _idOrganizationalChart,
                                _idOrganization,
                                _idGeographicAreaParent,
                                _idPositionParent,
                                _idFunctionalAreaParent,
                                _jobTitle.Name(), _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del JobTitle.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable JobTitle(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("JobTitle");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
                        Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
                        Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);

                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                        Position _position = _organization.Position(_idPosition);
                        FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                        FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        JobTitle _jobTitle = null;
                        //Si viene el OrganizationalChart, lo construyo con eso.
                        if (param.ContainsKey("IdOrganizationalChart"))
                        {
                            Int64 _idOrganizationalChart = Convert.ToInt64(param["IdOrganizationalChart"]);
                            _jobTitle = _organization.OrganizationalChart(_idOrganizationalChart).JobTitle(_geoFunArea, _funPos);
                        }
                        else
                        {//Sino el jt solo.
                            _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);
                        }
                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _jobTitle.Name());

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();
                        _valueLink.Add("IdOrganization", _jobTitle.Organization.IdOrganization);

                        //Carga los datos
                        JobtitleWithChart _jobTitleWithChart;
                        if (_jobTitle.GetType().Name == "JobTitleWithChart")
                        {
                            _jobTitleWithChart = (JobtitleWithChart)_jobTitle;
                            if (_jobTitleWithChart.Parent != null)   //Si es null, quiere decir que es root.
                            {
                                _valueLink.Add("IdGeographicArea", _jobTitleWithChart.IdParentGeographicArea);
                                _valueLink.Add("IdFunctionalArea", _jobTitleWithChart.IdParentFunctionalArea);
                                _valueLink.Add("IdPosition", _jobTitleWithChart.IdParentPosition);
                                _dt.Rows.Add(Resources.CommonListManage.ParentEntity, _jobTitleWithChart.Parent.Name(),
                                    GetValueLink(Common.ConstantsEntitiesName.DS.JobTitle, _valueLink));
                                _valueLink.Remove("IdGeographicArea");
                                _valueLink.Remove("IdFunctionalArea");
                                _valueLink.Remove("IdPosition");
                            }
                            else
                            {
                                _dt.Rows.Add(Resources.CommonListManage.ParentEntity, Resources.Common.ComboBoxNoDependency, "#" + i++);
                            }
                        }
                        _valueLink.Add("IdGeographicArea", _jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea);
                        _valueLink.Add("IdFunctionalArea", _jobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea);
                        _valueLink.Add("IdPosition", _jobTitle.FunctionalPositions.Position.IdPosition);
                        _dt.Rows.Add(Resources.CommonListManage.Organization, _jobTitle.Organization.CorporateName,
                        GetValueLink(Common.ConstantsEntitiesName.DS.Organization, _valueLink));
                        _dt.Rows.Add(Resources.CommonListManage.GeographicArea, _jobTitle.GeographicFunctionalAreas.GeographicArea.LanguageOption.Name,
                            GetValueLink(Common.ConstantsEntitiesName.DS.GeographicArea, _valueLink));
                        _dt.Rows.Add(Resources.CommonListManage.Position, _jobTitle.FunctionalPositions.Position.LanguageOption.Name,
                            GetValueLink(Common.ConstantsEntitiesName.DS.Position, _valueLink));
                        _dt.Rows.Add(Resources.CommonListManage.FunctionalArea, _jobTitle.GeographicFunctionalAreas.FunctionalArea.LanguageOption.Name,
                            GetValueLink(Common.ConstantsEntitiesName.DS.FunctionalArea, _valueLink));               
                        _dt.Rows.Add(Resources.CommonListManage.Name, _jobTitle.Name(), "#" + i++);

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa Organizacion tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean JobTitlesHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idOrganizationalChart = Convert.ToInt64(param["IdOrganizationalChart"]);
                        Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
                        Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
                        Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);

                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                        Position _position = _organization.Position(_idPosition);
                        FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                        FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        JobtitleWithChart _jobTitle = (JobtitleWithChart)_organization.OrganizationalChart(_idOrganizationalChart).JobTitle(_geoFunArea, _funPos);

                        if (_jobTitle.Childrens.Count > 0)
                        { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> JobTitleFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
                        Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
                        Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
                        Int64 _idOrganizationalChart = Convert.ToInt64(param["IdOrganizationalChart"]);

                        //Si no hay organizacion, no puede crear nada.
                        if (_idOrganization != 0)
                        {
                            //Accede al objeto y arma el KeyValue para insertar en la pila.
                            Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                            GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                            Position _position = _organization.Position(_idPosition);
                            FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                            FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                            GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                            JobtitleWithChart _jobTitle = (JobtitleWithChart)_organization.OrganizationalChart(_idOrganizationalChart).JobTitle(_geoFunArea, _funPos);

                            //JobTitle _jobTitle = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization).OrganizationalChart(_idOrganizationalChart).JobTitle(_idGeographicArea, _idPosition, _idFunctionalArea);

                            Int64 _idParentGeographicArea = 0;
                            Int64 _idParentPosition = 0;
                            Int64 _idParentFunctionalArea = 0;
                            if (_jobTitle.Parent != null)
                            {
                                _idParentGeographicArea = _jobTitle.IdParentGeographicArea;
                                _idParentPosition = _jobTitle.IdParentPosition;
                                _idParentFunctionalArea = _jobTitle.IdParentFunctionalArea;
                            }

                            _keyValues = "IdGeographicArea=" + _idGeographicArea.ToString() 
                                + "& IdPosition=" + _idPosition.ToString() 
                                + "& IdFunctionalArea=" + _idFunctionalArea.ToString()
                                + "& IdOrganizationalChart=" + _idOrganizationalChart.ToString()
                                + "& IdOrganization=" + _idOrganization.ToString()
                                + "& IdParentGeographicArea=" + _idParentGeographicArea.ToString()
                                + "& IdParentPosition=" + _idParentPosition.ToString()
                                + "& IdParentFunctionalArea=" + _idParentFunctionalArea.ToString();

                            //Inserta el primer dato en la pila.
                            _parents.Push(_keyValues);
                            //Ahora recorre toda su familia y guarda los keys en la pila.
                            while (_jobTitle.Parent != null)
                            {
                                Int64 _idGeographicAreaParent = 0;
                                Int64 _idPositionParent = 0;
                                Int64 _idFunctionalAreaParent = 0;
                                _idGeographicAreaParent = _jobTitle.IdParentGeographicArea;
                                _idPositionParent = _jobTitle.IdParentPosition;
                                _idFunctionalAreaParent = _jobTitle.IdParentFunctionalArea;

                                _idParentGeographicArea = 0;
                                _idParentPosition = 0;
                                _idParentFunctionalArea = 0;
                                if (_jobTitle.Parent.Parent != null)
                                {
                                    _idParentGeographicArea = _jobTitle.Parent.Parent.GeographicFunctionalAreas.GeographicArea.IdGeographicArea;
                                    _idParentPosition = _jobTitle.Parent.Parent.FunctionalPositions.Position.IdPosition;
                                    _idParentFunctionalArea = _jobTitle.Parent.Parent.FunctionalPositions.FunctionalArea.IdFunctionalArea;
                                }

                                _keyValues = "IdGeographicArea=" + _idGeographicAreaParent.ToString()
                                        + "& IdPosition=" + _idPositionParent.ToString()
                                        + "& IdFunctionalArea=" + _idFunctionalAreaParent.ToString()
                                        + "& IdOrganizationalChart=" + _idOrganizationalChart.ToString()
                                        + "& IdOrganization=" + _idOrganization.ToString()
                                        + "& IdParentGeographicArea=" + _idParentGeographicArea.ToString()
                                        + "& IdParentPosition=" + _idParentPosition.ToString()
                                        + "& IdParentFunctionalArea=" + _idParentFunctionalArea.ToString();

                                _parents.Push(_keyValues);
                                //Sigue con el proximo.
                                GeographicArea _geoAreaParent = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicAreaParent);
                                Position _positionParent=_organization.Position(_idPositionParent);
                                FunctionalArea _funAreaParent =_organization.FunctionalArea(_idFunctionalAreaParent);
                                FunctionalPosition _funPosParent = _organization.FunctionalPosition(_positionParent, _funAreaParent);
                                GeographicFunctionalArea _geoFunAreaParent = _organization.GeographicFunctionalArea(_funAreaParent, _geoAreaParent);

                                _jobTitle = (JobtitleWithChart)_organization.OrganizationalChart(_idOrganizationalChart).JobTitle(_geoFunArea, _funPosParent);
                            }
                        }
                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                    /// <summary>
                    /// Indica si esta JobTitle tiene Post.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean JobTitlesHasPost(Dictionary<String, Object> param)
                    {
                        Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                        Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
                        Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
                        Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);
                        Int64 _idOrganizationalChart = Convert.ToInt64(param["IdOrganizationalChart"]);

                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                        GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                        Position _position = _organization.Position(_idPosition);
                        FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                        FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        JobTitle _jobTitle = _organization.OrganizationalChart(_idOrganizationalChart).JobTitle(_geoFunArea, _funPos);

                        if (_jobTitle.Posts().Count > 0)
                            { return true; }

                        return false;
                    }
                #endregion

            #endregion
                
            #region Improvement Actions

                #region Project Classifications
                    /// <summary>
                    /// Construye las columnas del datatable de ProjectClassification
                    /// </summary>
                    /// <param name="dt"></param>
                    private void ProjectClassificationColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdProjectClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdProjectClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentProjectClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "IdParentProjectClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.ProjectClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ProjectClassification (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ProjectClassifications(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ProjectClassification");

                        //Contruye las columnas y sus atributos.
                        ProjectClassificationColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.ImprovementAction.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            //Ya esta armado el DataTable, ahora lo carga
                            foreach (ProjectClassification _projectClassification in EMSLibrary.User.ImprovementAction.Map.ProjectClassifications().Values)
                            {
                                _dt.Rows.Add(_projectClassification.IdProjectClassification, _projectClassification.IdParentProjectClassification, _projectClassification.LanguageOption.Name, _projectClassification.LanguageOption.Description, _permissionType);
                            }
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un ProjectClassification.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ProjectClassificationsChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ProjectClassification");

                        //Contruye las columnas y sus atributos.
                        ProjectClassificationColumns(ref _dt);

                        Int64 _idParentProjectClassification = Convert.ToInt64(param["IdProjectClassification"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.ImprovementAction.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (ProjectClassification _projectClassification in EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_idParentProjectClassification).ChildrenClassifications.Values)
                        {
                            _dt.Rows.Add(_projectClassification.IdProjectClassification, _projectClassification.IdParentProjectClassification, _projectClassification.LanguageOption.Name, _projectClassification.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa ProjectClassification tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean ProjectClassificationsHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idParentProjectClassification = Convert.ToInt64(param["IdProjectClassification"]);

                        if ((EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_idParentProjectClassification).ChildrenClassifications.Count > 0) ||
                            (EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_idParentProjectClassification).ChildrenElements.Count > 0))
                            { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del ProjectClassification.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ProjectClassification(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ProjectClassification");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        Int64 _idProjectClassification = Convert.ToInt64(param["IdProjectClassification"]);

                        ProjectClassification _projectClassification = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_idProjectClassification);
                        ProjectClassification _parentProjectClassification = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_projectClassification.IdParentProjectClassification);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _projectClassification.LanguageOption.Name);

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();

                        //Carga los datos
                        if (_parentProjectClassification != null)
                        {
                            _valueLink.Add("IdProjectClassification", _parentProjectClassification.IdProjectClassification);
                            _dt.Rows.Add(Resources.CommonListManage.ParentEntity, _parentProjectClassification.LanguageOption.Name,
                                GetValueLink(Common.ConstantsEntitiesName.IA.ProjectClassification, _valueLink));
                        }
                        //_dt.Rows.Add(Resources.CommonListManage.IdProjectClassification, _projectClassification.IdProjectClassification, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _projectClassification.LanguageOption.Language.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Name, _projectClassification.LanguageOption.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _projectClassification.LanguageOption.Description, "#" + i++);

                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion
                
            #endregion

            #region Knowledge Collaboration

                #region Resource Classifications
                    /// <summary>
                    /// Construye las columnas del datatable de ResourceClassification
                    /// </summary>
                    /// <param name="dt"></param>
                    private void ResourceClassificationColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdResourceClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdResourceClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentResourceClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "IdParentResourceClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.ResourceClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ResourceClassification (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ResourceClassifications(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ResourceClassification");

                        //Contruye las columnas y sus atributos.
                        ResourceClassificationColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            Dictionary<Int64, Condesus.EMS.Business.KC.Entities.ResourceClassification> _resourceClassifications = new Dictionary<Int64, Condesus.EMS.Business.KC.Entities.ResourceClassification>();
                            //Verifica que ahora si viene el idOrg, entonces trae todas las classificaciones de esa org.
                            if (param.ContainsKey("IdResource"))
                            {
                                //Obtiene el OrganizationClassification
                                Int64 _idResource = Convert.ToInt64(param["IdResource"]);
                                _resourceClassifications = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource).Classifications;
                                //Ya esta armado el DataTable, ahora lo carga
                                //foreach (Condesus.EMS.Business.KC.Entities.ResourceClassification _resourceClassification in EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource).Classifications.Values)
                                //{
                                //    _dt.Rows.Add(_resourceClassification.IdResourceClassification, _resourceClassification.IdParentResourceClassification, _resourceClassification.LanguageOption.Name, _resourceClassification.LanguageOption.Description, _permissionType);
                                //}
                            }
                            else
                            {
                                _resourceClassifications = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassifications();
                                ////Ya esta armado el DataTable, ahora lo carga
                                //foreach (Condesus.EMS.Business.KC.Entities.ResourceClassification _resourceClassification in EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassifications().Values)
                                //{
                                //    _dt.Rows.Add(_resourceClassification.IdResourceClassification, _resourceClassification.IdParentResourceClassification, _resourceClassification.LanguageOption.Name, _resourceClassification.LanguageOption.Description, _permissionType);
                                //}
                            }
                            var _lnqResourceClassification = from resourceClassification in _resourceClassifications.Values
                                                             orderby resourceClassification.LanguageOption.Name ascending
                                                             select resourceClassification;
                            //Ya esta armado el DataTable, ahora lo carga
                            foreach (Condesus.EMS.Business.KC.Entities.ResourceClassification _resourceClassification in _lnqResourceClassification)
                            {
                                _dt.Rows.Add(_resourceClassification.IdResourceClassification, _resourceClassification.IdParentResourceClassification, _resourceClassification.LanguageOption.Name, _resourceClassification.LanguageOption.Description, _permissionType);
                            }

                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un ResourceClassification.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ResourceClassificationsChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ResourceClassification");

                        //Contruye las columnas y sus atributos.
                        ResourceClassificationColumns(ref _dt);

                        Int64 _idParentResourceClassification = Convert.ToInt64(param["IdResourceClassification"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqResourceClassification = from resourceClassification in EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idParentResourceClassification).ChildrenClassifications.Values
                                                         orderby resourceClassification.LanguageOption.Name ascending
                                                         select resourceClassification;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.KC.Entities.ResourceClassification _resourceClassification in _lnqResourceClassification)
                        {
                            _dt.Rows.Add(_resourceClassification.IdResourceClassification, _resourceClassification.IdParentResourceClassification, _resourceClassification.LanguageOption.Name, _resourceClassification.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa ResourceClassification tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean ResourceClassificationsHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idParentResourceClassification = Convert.ToInt64(param["IdResourceClassification"]);
                        //Este parametro lo pasa en true cuando se carga un arbol de mapas.(entonces se debe hacer una doble pregunta para saber si tiene o no hijos)
                        Boolean _isLoadElementMap = false;
                        Boolean _showOnlyCatalog = false;
                        if (param.ContainsKey("IsLoadElementMap"))
                            { _isLoadElementMap = Convert.ToBoolean(param["IsLoadElementMap"]); }

                        if (_isLoadElementMap)
                        {
                            //Como esta mostrando los elementos, ahora verifica si solo pide mostrar los catalogos...
                            if (param.ContainsKey("ShowOnlyCatalog"))
                                { _showOnlyCatalog = Convert.ToBoolean(param["ShowOnlyCatalog"]); }

                            if (_showOnlyCatalog)
                            {
                                //Reviso si hay catalogos en los elementos de esta clasificacion...
                                Boolean _hasCatalog = false;
                                foreach (Condesus.EMS.Business.KC.Entities.Resource _resource in EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idParentResourceClassification).ChildrenElements.Values)
                                {
                                    if (_resource.GetType().Name == Common.ConstantsEntitiesName.KC.ResourceCatalog)
                                    {
                                        //Como hay catalogos, lo pongo en true.
                                        _hasCatalog = true;
                                    }
                                }
                                //revisa si tiene clasificaciones como hijos o si tiene catalogo.
                                if ((EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idParentResourceClassification).ChildrenClassifications.Count > 0) ||
                                    (_hasCatalog))
                                { return true; }
                            }
                            else
                            {
                                if ((EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idParentResourceClassification).ChildrenClassifications.Count > 0) ||
                                    (EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idParentResourceClassification).ChildrenElements.Count > 0))
                                { return true; }
                            }
                        }
                        else
                        {
                            //El hijo solo va a ser clasificacion
                            if (EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idParentResourceClassification).ChildrenClassifications.Count > 0)
                            { return true; }
                        }
                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del ResourceClassification.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ResourceClassification(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ResourceClassification");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        Int64 _idResourceClassification = Convert.ToInt64(param["IdResourceClassification"]);

                        Condesus.EMS.Business.KC.Entities.ResourceClassification _resourceClassification = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idResourceClassification);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _resourceClassification.LanguageOption.Name);

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();        

                        //Carga los datos
                        if (_resourceClassification.ParentResourceClassification != null)
                        {
                            _valueLink.Add("IdResourceClassification", _resourceClassification.ParentResourceClassification.IdResourceClassification);
                            _dt.Rows.Add(Resources.CommonListManage.ParentEntity, _resourceClassification.ParentResourceClassification.LanguageOption.Name,
                                GetValueLink(Common.ConstantsEntitiesName.KC.ResourceClassification, _valueLink));
                        }

                        //Carga los datos
                        //_dt.Rows.Add(Resources.CommonListManage.IdResourceClassification, _resourceClassification.IdResourceClassification, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _resourceClassification.LanguageOption.Language.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Name, _resourceClassification.LanguageOption.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _resourceClassification.LanguageOption.Description, "#" + i++);

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> ResourceClassificationFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idResourceClassification = Convert.ToInt64(param["IdResourceClassification"]);

                        //Accede al objeto y arma el KeyValue para insertar en la pila.
                        Condesus.EMS.Business.KC.Entities.ResourceClassification _resourceClassification = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idResourceClassification);
                        _keyValues = "IdResourceClassification=" + _idResourceClassification;
                        //Inserta el primer dato en la pila.
                        _parents.Push(_keyValues);
                        //Ahora recorre toda su familia y guarda los keys en la pila.
                        while (_resourceClassification.IdParentResourceClassification != 0)
                        {
                            _keyValues = "IdResourceClassification=" + _resourceClassification.IdParentResourceClassification.ToString();
                            _parents.Push(_keyValues);
                            //Sigue con el proximo.
                            _resourceClassification = _resourceClassification.ParentResourceClassification; // EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_resourceClassification.IdParentResourceClassification);
                        }

                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                #endregion
                
                #region Resource Types
                    /// <summary>
                    /// Construye las columnas del datatable de ResourceType
                    /// </summary>
                    /// <param name="dt"></param>
                    private void ResourceTypeColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdResourceType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdResourceType", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentResourceType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "IdParentResourceType", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.ResourceType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ResourceType (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ResourceTypes(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ResourceType");

                        //Contruye las columnas y sus atributos.
                        ResourceTypeColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            String _permissionType = String.Empty;
                            //Obtiene el permiso que tiene el usuario para esa organizacion.
                            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                            else
                                { _permissionType = Common.Constants.PermissionViewName; }

                            var _lnqResourceType = from resourceType in EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceTypes().Values
                                                   orderby resourceType.LanguageOption.Name ascending
                                                   select resourceType;
                            //Ya esta armado el DataTable, ahora lo carga
                            foreach (Condesus.EMS.Business.KC.Entities.ResourceType _resourceType in _lnqResourceType)
                            {
                                Int64 _idParent = (_resourceType.ParentResourceType == null) ? 0 :_resourceType.ParentResourceType.IdResourceType;
                                _dt.Rows.Add(_resourceType.IdResourceType, _idParent, _resourceType.LanguageOption.Name, _resourceType.LanguageOption.Description, _permissionType);
                            }
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un ResourceType.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ResourceTypesChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ResourceType");

                        //Contruye las columnas y sus atributos.
                        ResourceTypeColumns(ref _dt);

                        Int64 _idParentResourceType = Convert.ToInt64(param["IdResourceType"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                        else
                            { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqResourceType = from resourceType in EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceType(_idParentResourceType).Children.Values
                                               orderby resourceType.LanguageOption.Name ascending
                                               select resourceType;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (Condesus.EMS.Business.KC.Entities.ResourceType _resourceType in _lnqResourceType)
                        {
                            Int64 _idParent = (_resourceType.ParentResourceType == null) ? 0 : _resourceType.ParentResourceType.IdResourceType;
                            _dt.Rows.Add(_resourceType.IdResourceType, _idParent, _resourceType.LanguageOption.Name, _resourceType.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa ResourceType tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean ResourceTypesHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idParentResourceType = Convert.ToInt64(param["IdResourceType"]);

                        if (EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceType(_idParentResourceType).Children.Count > 0)
                            { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del ResourceType.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ResourceType(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ResourceType");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        Int64 _idResourceType = Convert.ToInt64(param["IdResourceType"]);

                        Condesus.EMS.Business.KC.Entities.ResourceType _resourceType = EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceType(_idResourceType);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _resourceType.LanguageOption.Name);

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();

                        //Carga los datos
                        if (_resourceType.ParentResourceType != null)
                        {
                            _valueLink.Add("IdResourceType", _resourceType.ParentResourceType.IdResourceType);
                            _dt.Rows.Add(Resources.CommonListManage.ParentEntity, _resourceType.ParentResourceType.LanguageOption.Name,
                                GetValueLink(Common.ConstantsEntitiesName.KC.ResourceType, _valueLink));
                        }
                        //_dt.Rows.Add(Resources.CommonListManage.IdResourceType, _resourceType.IdResourceType, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _resourceType.LanguageOption.Language.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Name, _resourceType.LanguageOption.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _resourceType.LanguageOption.Description, "#" + i++);

                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion

            #endregion

            #region Performance Assessment

                //ParameterGroupsHasChildren
                public Boolean ParameterGroupsHasChildren(Dictionary<String, Object> param)
                {   //Siempre devuelve Falso, es porque a esta entidad la usamos en un tree por los CHECKS
                    return false;
                }

                #region Constant Classification
                    /// <summary>
                    /// Construye las columnas del datatable de ConstantClassification
                    /// </summary>
                    /// <param name="dt"></param>
                    private void ConstantClassificationColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdConstantClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdConstantClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentConstantClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdParentConstantClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.ConstantClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        _columnOptions.IsSortedBy = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);

                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Constants.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ConstantClassifications(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ConstantClassification");
                        //Contruye las columnas y sus atributos.
                        ConstantClassificationColumns(ref _dt);

                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqConstantClassification = from constantClassification in EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassifications().Values
                                                     orderby constantClassification.LanguageOption.Name ascending
                                                     select constantClassification;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (ConstantClassification _constantClassification in _lnqConstantClassification)
                        {
                            _dt.Rows.Add(_constantClassification.IdConstantClassification, _constantClassification.IdParentConstantClassification, Common.Functions.ReplaceIndexesTags(_constantClassification.LanguageOption.Name), _constantClassification.LanguageOption.Description, _permissionType);
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un Constant Classification
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ConstantClassificationsChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ConstantClassification");

                        //Contruye las columnas y sus atributos.
                        ConstantClassificationColumns(ref _dt);

                        Int64 _idParentConstantClassification = Convert.ToInt64(param["IdConstantClassification"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqConstantClassification = from constantClassification in EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idParentConstantClassification).Childrens.Values
                                                         orderby constantClassification.LanguageOption.Name ascending
                                                         select constantClassification;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (ConstantClassification _constantClassification in _lnqConstantClassification)
                        {
                            _dt.Rows.Add(_constantClassification.IdConstantClassification, _constantClassification.IdParentConstantClassification, Common.Functions.ReplaceIndexesTags(_constantClassification.LanguageOption.Name), _constantClassification.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa Constant Classification tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean ConstantClassificationsHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idParentConstantClassification = Convert.ToInt64(param["IdConstantClassification"]);
                        //Este parametro lo pasa en true cuando se carga un arbol de mapas.(entonces se debe hacer una doble pregunta para saber si tiene o no hijos)
                        Boolean _isLoadElementMap = false;
                        if (param.ContainsKey("IsLoadElementMap"))
                        { _isLoadElementMap = Convert.ToBoolean(param["IsLoadElementMap"]); }

                        ConstantClassification _constantClassification = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idParentConstantClassification);

                        if (_isLoadElementMap)
                        {   //Verifica si hay constantes...
                            //if (EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idParentConstantClassification).Constants.Count > 0)
                            if ((_constantClassification.Childrens.Count > 0) || (_constantClassification.Constants.Count > 0)) 
                            { return true; }
                        }
                        else
                        {   //El hijo solo va a ser clasificacion
                            if (_constantClassification.Childrens.Count > 0)
                            { return true; }
                        }
                        
                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del Constants.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ConstantClassification(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ConstantClassification");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));

                        Int64 _idConstantClassification = Convert.ToInt64(param["IdConstantClassification"]);

                        ConstantClassification _constantClassification = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClassification);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", Common.Functions.ReplaceIndexesTags(_constantClassification.LanguageOption.Name));

                        //Carga los datos
                        _dt.Rows.Add(Resources.CommonListManage.Name, _constantClassification.LanguageOption.Name);
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _constantClassification.LanguageOption.Language.Name);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _constantClassification.LanguageOption.Description);

                        //Retorna el DataTable.
                        return _dt;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> ConstantClassificationFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idConstantClassification = Convert.ToInt64(param["IdConstantClassification"]);

                        //Accede al objeto y arma el KeyValue para insertar en la pila.
                        ConstantClassification _constantClassification = EMSLibrary.User.PerformanceAssessments.Configuration.ConstantClassification(_idConstantClassification);
                        _keyValues = "IdConstantClassification=" + _idConstantClassification.ToString() + "& IdParentConstantClassification=" + _constantClassification.IdParentConstantClassification.ToString(); ;
                        //Inserta el primer dato en la pila.
                        _parents.Push(_keyValues);
                        //Ahora recorre toda su familia y guarda los keys en la pila.
                        while (_constantClassification.IdParentConstantClassification != 0)
                        {
                            _keyValues = "IdConstantClassification=" + _constantClassification.IdParentConstantClassification.ToString() + "& IdParentConstantClassification=" + _constantClassification.Parent.IdParentConstantClassification.ToString();
                            _parents.Push(_keyValues);
                            //Sigue con el proximo.
                            _constantClassification = _constantClassification.Parent;
                        }

                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                #endregion

                #region AccountingActivity
                    /// <summary>
                    /// Construye las columnas del datatable de AccountingActivity
                    /// </summary>
                    /// <param name="dt"></param>
                    private void AccountingActivityColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdActivity;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdActivity", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentActivity;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdParentActivity", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.AccountingActivity;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        _columnOptions.IsSortedBy = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);

                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la AccountingActivity.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable AccountingActivities(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("AccountingActivities");
                        //Contruye las columnas y sus atributos.
                        AccountingActivityColumns(ref _dt);

                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqAccountingActivity = from accountingActivity in EMSLibrary.User.PerformanceAssessments.Configuration.AccountingActivities().Values
                                                     orderby accountingActivity.LanguageOption.Name ascending
                                                     select accountingActivity;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (AccountingActivity _accountingActivity in _lnqAccountingActivity)
                        {
                            _dt.Rows.Add(_accountingActivity.IdActivity, _accountingActivity.IdParentActivity, Common.Functions.ReplaceIndexesTags(_accountingActivity.LanguageOption.Name), _accountingActivity.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un Constant Classification
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable AccountingActivitiesChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("AccountingActivities");

                        //Contruye las columnas y sus atributos.
                        AccountingActivityColumns(ref _dt);

                        Int64 _idParentActivity = Convert.ToInt64(param["IdActivity"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqAccountingActivity = from accountingActivity in EMSLibrary.User.PerformanceAssessments.Configuration.AccountingActivity(_idParentActivity).Childrens.Values
                                                     orderby accountingActivity.LanguageOption.Name ascending
                                                     select accountingActivity;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (AccountingActivity _accountingActivity in _lnqAccountingActivity)
                        {
                            _dt.Rows.Add(_accountingActivity.IdActivity, _accountingActivity.IdParentActivity, Common.Functions.ReplaceIndexesTags(_accountingActivity.LanguageOption.Name), _accountingActivity.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa AccountingActivity tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean AccountingActivitiesHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idParentActivity = Convert.ToInt64(param["IdActivity"]);
                        //Este parametro lo pasa en true cuando se carga un arbol de mapas.(entonces se debe hacer una doble pregunta para saber si tiene o no hijos)
                        Boolean _isLoadElementMap = false;
                        if (param.ContainsKey("IsLoadElementMap"))
                        { _isLoadElementMap = Convert.ToBoolean(param["IsLoadElementMap"]); }

                        //El hijo solo va a ser clasificacion
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.AccountingActivity(_idParentActivity).Childrens.Count > 0)
                        { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del AccountingActivity.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable AccountingActivity(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("AccountingActivity");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));

                        Int64 _idActivity = Convert.ToInt64(param["IdActivity"]);

                        AccountingActivity _accountingActivity = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingActivity(_idActivity);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", Common.Functions.ReplaceIndexesTags(_accountingActivity.LanguageOption.Name));

                        //Carga los datos
                        _dt.Rows.Add(Resources.CommonListManage.Name, _accountingActivity.LanguageOption.Name);
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _accountingActivity.LanguageOption.Language.Name);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _accountingActivity.LanguageOption.Description);

                        //Retorna el DataTable.
                        return _dt;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> AccountingActivityFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idActivity = Convert.ToInt64(param["IdActivity"]);

                        //Accede al objeto y arma el KeyValue para insertar en la pila.
                        AccountingActivity _accountingActivity = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingActivity(_idActivity);
                        _keyValues = "IdActivity=" + _idActivity.ToString() + "& IdParentActivity=" + _accountingActivity.IdParentActivity.ToString(); ;
                        //Inserta el primer dato en la pila.
                        _parents.Push(_keyValues);
                        //Ahora recorre toda su familia y guarda los keys en la pila.
                        while (_accountingActivity.IdParentActivity != 0)
                        {
                            _keyValues = "IdActivity=" + _accountingActivity.IdParentActivity.ToString() + "& IdParentActivity=" + _accountingActivity.Parent.IdParentActivity.ToString();
                            _parents.Push(_keyValues);
                            //Sigue con el proximo.
                            _accountingActivity = _accountingActivity.Parent;
                        }

                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                #endregion

                #region AccountingSector
                    /// <summary>
                    /// Construye las columnas del datatable de AccountingSector
                    /// </summary>
                    /// <param name="dt"></param>
                    private void AccountingSectorColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdSector;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdSector", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentSector;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdParentSector", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.AccountingActivity;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        _columnOptions.IsSortedBy = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);

                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion AccountingSector.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable AccountingSectors(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("AccountingSectors");
                        //Contruye las columnas y sus atributos.
                        AccountingSectorColumns(ref _dt);

                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqAccountingSector = from accountingSector in EMSLibrary.User.PerformanceAssessments.Configuration.AccountingSectors().Values
                                                   orderby accountingSector.LanguageOption.Name ascending
                                                   select accountingSector;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (AccountingSector _accountingSector in _lnqAccountingSector)
                        {
                            _dt.Rows.Add(_accountingSector.IdSector, _accountingSector.IdParentSector, Common.Functions.ReplaceIndexesTags(_accountingSector.LanguageOption.Name), _accountingSector.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un AccountingSector
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable AccountingSectorsChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("AccountingSectors");

                        //Contruye las columnas y sus atributos.
                        AccountingSectorColumns(ref _dt);

                        Int64 _idParentSector = Convert.ToInt64(param["IdSector"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqAccountingSector = from accountingSector in EMSLibrary.User.PerformanceAssessments.Configuration.AccountingSector(_idParentSector).Childrens.Values
                                                   orderby accountingSector.LanguageOption.Name ascending
                                                   select accountingSector;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (AccountingSector _accountingSector in _lnqAccountingSector)
                        {
                            _dt.Rows.Add(_accountingSector.IdSector, _accountingSector.IdParentSector, Common.Functions.ReplaceIndexesTags(_accountingSector.LanguageOption.Name), _accountingSector.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa AccountingSector tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean AccountingSectorsHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idParentSector = Convert.ToInt64(param["IdSector"]);
                        //Este parametro lo pasa en true cuando se carga un arbol de mapas.(entonces se debe hacer una doble pregunta para saber si tiene o no hijos)
                        Boolean _isLoadElementMap = false;
                        if (param.ContainsKey("IsLoadElementMap"))
                        { _isLoadElementMap = Convert.ToBoolean(param["IsLoadElementMap"]); }

                        //El hijo solo va a ser clasificacion
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.AccountingSector(_idParentSector).Childrens.Count > 0)
                        { return true; }

                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del Accounting Sector.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable AccountingSector(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("AccountingSector");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));

                        Int64 _idSector = Convert.ToInt64(param["IdSector"]);

                        AccountingSector _accountingSector = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingSector(_idSector);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", Common.Functions.ReplaceIndexesTags(_accountingSector.LanguageOption.Name));

                        //Carga los datos
                        _dt.Rows.Add(Resources.CommonListManage.Name, _accountingSector.LanguageOption.Name);
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _accountingSector.LanguageOption.Language.Name);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _accountingSector.LanguageOption.Description);

                        //Retorna el DataTable.
                        return _dt;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> AccountingSectorFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idSector = Convert.ToInt64(param["IdSector"]);

                        //Accede al objeto y arma el KeyValue para insertar en la pila.
                        AccountingSector _accountingSector = EMSLibrary.User.PerformanceAssessments.Configuration.AccountingSector(_idSector);
                        _keyValues = "IdSector=" + _idSector.ToString() + "& IdParentSector=" + _accountingSector.IdParentSector.ToString(); ;
                        //Inserta el primer dato en la pila.
                        _parents.Push(_keyValues);
                        //Ahora recorre toda su familia y guarda los keys en la pila.
                        while (_accountingSector.IdParentSector != 0)
                        {
                            _keyValues = "IdSector=" + _accountingSector.IdParentSector.ToString() + "& IdParentSector=" + _accountingSector.Parent.IdParentSector.ToString();
                            _parents.Push(_keyValues);
                            //Sigue con el proximo.
                            _accountingSector = _accountingSector.Parent;
                        }

                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                #endregion

                #region Indicator Classifications
                    /// <summary>
                    /// Construye las columnas del datatable de IndicatorClassification
                    /// </summary>
                    /// <param name="dt"></param>
                    private void IndicatorClassificationColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdIndicatorClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdIndicatorClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentIndicatorClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdParentIndicatorClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IndicatorClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion IndicatorClassification (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable IndicatorClassifications(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("IndicatorClassification");

                        //Contruye las columnas y sus atributos.
                        IndicatorClassificationColumns(ref _dt);

                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        Dictionary<Int64, IndicatorClassification> _indicatorClassifications = new Dictionary<Int64, IndicatorClassification>();
                        //Verifica que ahora si viene el idOrg, entonces trae todas las classificaciones de esa org.
                        if (param.ContainsKey("IdIndicator"))
                        {
                            //Obtiene el OrganizationClassification
                            Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
                            _indicatorClassifications = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).Classifications;
                            ////Ya esta armado el DataTable, ahora lo carga
                            //foreach (IndicatorClassification _indicatorClassification in EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator).Classifications.Values)
                            //{
                            //    _dt.Rows.Add(_indicatorClassification.IdIndicatorClassification, _indicatorClassification.IdParentIndicatorClassification, _indicatorClassification.LanguageOption.Name, _indicatorClassification.LanguageOption.Description, _permissionType);
                            //}
                        }
                        else
                        {
                            _indicatorClassifications = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassifications();
                            ////Ya esta armado el DataTable, ahora lo carga
                            //foreach (IndicatorClassification _indicatorClassification in EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassifications().Values)
                            //{
                            //    _dt.Rows.Add(_indicatorClassification.IdIndicatorClassification, _indicatorClassification.IdParentIndicatorClassification, _indicatorClassification.LanguageOption.Name, _indicatorClassification.LanguageOption.Description, _permissionType);
                            //}
                        }
                        var _lnqIndicatorClass = from indicatorClass in _indicatorClassifications.Values
                                                 orderby indicatorClass.LanguageOption.Name ascending
                                                 select indicatorClass;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (IndicatorClassification _indicatorClassification in _lnqIndicatorClass)
                        {
                            _dt.Rows.Add(_indicatorClassification.IdIndicatorClassification, _indicatorClassification.IdParentIndicatorClassification, _indicatorClassification.LanguageOption.Name, _indicatorClassification.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un Indicator Classification
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable IndicatorClassificationsChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("IndicatorClassification");

                        //Contruye las columnas y sus atributos.
                        IndicatorClassificationColumns(ref _dt);

                        Int64 _idParentIndicatorClassification = Convert.ToInt64(param["IdIndicatorClassification"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqIndicatorClass = from indicatorClass in EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idParentIndicatorClassification).ChildrenClassifications.Values
                                                 orderby indicatorClass.LanguageOption.Name ascending
                                                 select indicatorClass;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (IndicatorClassification _indicatorClassification in _lnqIndicatorClass)
                        {
                            _dt.Rows.Add(_indicatorClassification.IdIndicatorClassification, _indicatorClassification.IdParentIndicatorClassification, _indicatorClassification.LanguageOption.Name, _indicatorClassification.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa Indicator Classification tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean IndicatorClassificationsHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idParentIndicatorClassification = Convert.ToInt64(param["IdIndicatorClassification"]);
                        //Este parametro lo pasa en true cuando se carga un arbol de mapas.(entonces se debe hacer una doble pregunta para saber si tiene o no hijos)
                        Boolean _isLoadElementMap = false;
                        if (param.ContainsKey("IsLoadElementMap"))
                            { _isLoadElementMap = Convert.ToBoolean(param["IsLoadElementMap"]); }

                        if (_isLoadElementMap)
                        {
                            //Indican que esta cargando un arbol de mapas, debe verificar que un hijo puede ser clasificacion o elemento
                            if ((EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idParentIndicatorClassification).ChildrenClassifications.Count > 0) ||
                                (EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idParentIndicatorClassification).ChildrenElements.Count > 0))
                            { return true; }
                        }
                        else
                        {
                            //El hijo solo va a ser clasificacion
                            if (EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idParentIndicatorClassification).ChildrenClassifications.Count > 0)
                            { return true; }
                        }
                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del Indicator Classification.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable IndicatorClassification(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("IndicatorClassification");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        Int64 _idIndicatorClassification = Convert.ToInt64(param["IdIndicatorClassification"]);

                        IndicatorClassification _indicatorClassification = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idIndicatorClassification);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _indicatorClassification.LanguageOption.Name);

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();

                        //Carga los datos
                        if (_indicatorClassification.ParentIndicatorClassification != null)
                        {
                            _valueLink.Add("IdIndicatorClassification", _indicatorClassification.ParentIndicatorClassification.IdIndicatorClassification);
                            _dt.Rows.Add(Resources.CommonListManage.ParentEntity, _indicatorClassification.ParentIndicatorClassification.LanguageOption.Name,
                                GetValueLink(Common.ConstantsEntitiesName.PA.IndicatorClassification, _valueLink));
                        }
                        //_dt.Rows.Add(Resources.CommonListManage.IdIndicatorClassification, _indicatorClassification.IdIndicatorClassification, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _indicatorClassification.LanguageOption.Language.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Name, Common.Functions.ReplaceIndexesTags(_indicatorClassification.LanguageOption.Name), "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _indicatorClassification.LanguageOption.Description, "#" + i++);

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> IndicatorClassificationFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idIndicatorClassification = Convert.ToInt64(param["IdIndicatorClassification"]);

                        //Accede al objeto y arma el KeyValue para insertar en la pila.
                        IndicatorClassification _indicatorClassification = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idIndicatorClassification);
                        _keyValues = "IdIndicatorClassification=" + _idIndicatorClassification.ToString() + "& IdParentIndicatorClassification=" + _indicatorClassification.IdParentIndicatorClassification.ToString(); ;
                        //Inserta el primer dato en la pila.
                        _parents.Push(_keyValues);
                        //Ahora recorre toda su familia y guarda los keys en la pila.
                        while (_indicatorClassification.IdParentIndicatorClassification != 0)
                        {
                            _keyValues = "IdIndicatorClassification=" + _indicatorClassification.IdParentIndicatorClassification.ToString() + "& IdParentIndicatorClassification=" + _indicatorClassification.ParentIndicatorClassification.IdParentIndicatorClassification.ToString(); 
                            _parents.Push(_keyValues);
                            //Sigue con el proximo.
                            _indicatorClassification = _indicatorClassification.ParentIndicatorClassification;  // EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_indicatorClassification.IdParentIndicatorClassification);
                        }

                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                #endregion

                #region Magnitudes / Units
                    /// <summary>
                    /// Indica si esa magnitud tiene asociadas unidades de medida
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean MagnitudesHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idMagnitud = Convert.ToInt64(param["IdMagnitud"]);
                        //Indica si la magnitud tiene unidades de medida asociadas.
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Magnitud(_idMagnitud).MeasurementUnits.Count > 0)
                            { return true; }
                        
                        return false;
                    }
                    /// <summary>
                    /// Indica si esa Unidad de medida tiene hijos
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean MeasurementUnitsHasChildren(Dictionary<String, Object> param)
                    {
                        //Las unidades de medida no tiene Hijos
                        return false;
                    }
                #endregion

                #region Calculation of Transformations (Incluye cosas de mediciones)
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion BasedMeasurementsOfTheTransformations.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable BasedMeasurementsOfTheTransformations(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("BasedMeasurementsOfTheTransformations");

                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdTransformation;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdTransformation", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdMeasurement;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdMeasurement", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdProcess;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdProcess", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdIndicator;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        _columnOptions.IsContextMenuCaption = false;
                        _columnOptions.IsSortedBy = false;
                        BuildColumnsDataTable(ref _dt, "IdIndicator", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdMeasurementUnit;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        _columnOptions.IsContextMenuCaption = false;
                        _columnOptions.IsSortedBy = false;
                        BuildColumnsDataTable(ref _dt, "IdMeasurementUnit", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Name;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.MeasurementUnit;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsCellLink = true;
                        _columnOptions.EntityName = Common.ConstantsEntitiesName.PA.MeasurementUnit;
                        _columnOptions.EntityNameGrid = Common.ConstantsEntitiesName.PA.MeasurementUnits;
                        BuildColumnsDataTable(ref _dt, "MeasurementUnit", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Indicator;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsCellLink = true;
                        _columnOptions.EntityName = Common.ConstantsEntitiesName.PA.Indicator;
                        _columnOptions.EntityNameGrid = Common.ConstantsEntitiesName.PA.Indicators;
                        BuildColumnsDataTable(ref _dt, "Indicator", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Base;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "Base", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Formula;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "Formula", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Operands;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "Operands", _columnOptions);

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
                            if (param.ContainsKey("IdMeasurement"))
                            {
                                String _permissionType = String.Empty;
                                //Obtiene el permiso que tiene el usuario para esa organizacion.
                                if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                Int64 _idMeasurement = 0;
                                Int64 _idProcess = 0;
                                Dictionary<Int64, CalculateOfTransformation> _transformations = new Dictionary<Int64, CalculateOfTransformation>();
                                //Si viene el idtask, quiere decir que viene desde una tarea de medicion (del element map)
                                if (param.ContainsKey("IdMeasurement"))
                                {
                                    _idMeasurement = Convert.ToInt64(param["IdMeasurement"]);
                                    Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement);
                                    _idProcess = _measurement.ProcessTask.Parent.IdProcess;
                                    _transformations = _measurement.Transformations;
                                }

                                var _lnqTransformation = from t in _transformations.Values
                                                         orderby Common.Functions.ReplaceIndexesTags(t.LanguageOption.Name) ascending
                                                         select t;
                                //Ya esta armado el DataTable, ahora lo carga
                                foreach (CalculateOfTransformation _calculateOfTransformation in _lnqTransformation)
                                {
                                    String _operands = String.Empty;
                                    foreach (KeyValuePair<String, CalculateOfTransformationParameter> _item in _calculateOfTransformation.Parameters)
                                    {
                                        //Hay que acceder al name desde el Language option. (ver si se puede acceder igual para todos los casos)
                                        _operands += _item.Key + "&nbsp; = &nbsp;" +
                                                Common.Functions.ReplaceIndexesTags(_item.Value.Operand.Name) + "<br/>";
                                    }

                                    if (_calculateOfTransformation.BaseTransformer.GetType().Name.Contains(Common.ConstantsEntitiesName.PA.Measurement))
                                    {
                                        _idMeasurement = ((Measurement)_calculateOfTransformation.BaseTransformer).IdMeasurement;
                                    }
                                    else
                                    {
                                        Condesus.EMS.Business.ITransformer _calcOfTransf = _calculateOfTransformation.BaseTransformer;
                                        //Mientras no sea una medicion....
                                        while (!_calcOfTransf.GetType().Name.Contains(Common.ConstantsEntitiesName.PA.Measurement))
                                        {
                                            if (((CalculateOfTransformation)_calcOfTransf).BaseTransformer.GetType().Name.Contains(Common.ConstantsEntitiesName.PA.Measurement))
                                            {
                                                _idMeasurement = ((Measurement)((CalculateOfTransformation)_calcOfTransf).BaseTransformer).IdMeasurement;
                                            }
                                            _calcOfTransf = ((CalculateOfTransformation)_calcOfTransf).BaseTransformer;
                                        }
                                        if (_idMeasurement != 0)
                                        {
                                            _idMeasurement = ((Measurement)_calcOfTransf).IdMeasurement;
                                        }
                                    }
                                    _dt.Rows.Add(_calculateOfTransformation.IdTransformation, _idMeasurement, _idProcess, _calculateOfTransformation.Indicator.IdIndicator,
                                        _calculateOfTransformation.MeasurementUnit.IdMeasurementUnit, Common.Functions.ReplaceIndexesTags(_calculateOfTransformation.LanguageOption.Name),
                                        _calculateOfTransformation.LanguageOption.Description, Common.Functions.ReplaceIndexesTags(_calculateOfTransformation.MeasurementUnit.LanguageOption.Name),
                                        Common.Functions.ReplaceIndexesTags(_calculateOfTransformation.Indicator.LanguageOption.Name), _calculateOfTransformation.BaseTransformer.Name,
                                        Common.Functions.ReplaceIndexesTags(_calculateOfTransformation.Formula), _operands, _permissionType);
                                }
                            }
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion Transformation de transformacion.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable TransformationsByTransformation(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("Transformation");

                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdTransformation;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdTransformation", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdMeasurement;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdMeasurement", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdProcess;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "IdProcess", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdIndicator;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        _columnOptions.IsContextMenuCaption = false;
                        _columnOptions.IsSortedBy = false;
                        BuildColumnsDataTable(ref _dt, "IdIndicator", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdMeasurementUnit;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        _columnOptions.IsContextMenuCaption = false;
                        _columnOptions.IsSortedBy = false;
                        BuildColumnsDataTable(ref _dt, "IdMeasurementUnit", _columnOptions);

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
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.MeasurementUnit;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsCellLink = true;
                        _columnOptions.EntityName = Common.ConstantsEntitiesName.PA.MeasurementUnit;
                        _columnOptions.EntityNameGrid = Common.ConstantsEntitiesName.PA.MeasurementUnits;
                        BuildColumnsDataTable(ref _dt, "MeasurementUnit", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Indicator;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsCellLink = true;
                        _columnOptions.EntityName = Common.ConstantsEntitiesName.PA.Indicator;
                        _columnOptions.EntityNameGrid = Common.ConstantsEntitiesName.PA.Indicators;
                        BuildColumnsDataTable(ref _dt, "Indicator", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Base;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "Base", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Formula;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "Formula", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Operands;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref _dt, "Operands", _columnOptions);

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
                            if (param.ContainsKey("IdMeasurement") && param.ContainsKey("IdTransformation"))
                            {
                                String _permissionType = String.Empty;
                                //Obtiene el permiso que tiene el usuario para esa organizacion.
                                if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                { _permissionType = Common.Constants.PermissionManageName; }
                                else
                                { _permissionType = Common.Constants.PermissionViewName; }

                                Int64 _idMeasurement = 0;
                                Int64 _idTransformation = 0;
                                Int64 _idProcess = 0;
                                Dictionary<Int64, CalculateOfTransformation> _transformations = new Dictionary<Int64, CalculateOfTransformation>();

                                _idMeasurement = Convert.ToInt64(param["IdMeasurement"]);
                                _idTransformation = Convert.ToInt64(param["IdTransformation"]);

                                Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement);
                                _idProcess = _measurement.ProcessTask.Parent.IdProcess;
                                _transformations = _measurement.Transformation(_idTransformation).Transformations;

                                var _lnqTransformation = from t in _transformations.Values
                                                         orderby Common.Functions.ReplaceIndexesTags(t.LanguageOption.Name) ascending
                                                         select t;
                                //Ya esta armado el DataTable, ahora lo carga
                                foreach (CalculateOfTransformation _calculateOfTransformation in _transformations.Values)
                                {
                                    String _operands = String.Empty;
                                    foreach (KeyValuePair<String, CalculateOfTransformationParameter> _item in _calculateOfTransformation.Parameters)
                                    {
                                        //Hay que acceder al name desde el Language option. (ver si se puede acceder igual para todos los casos)
                                        _operands += _item.Key + "&nbsp; = &nbsp;" +
                                                Common.Functions.ReplaceIndexesTags(_item.Value.Operand.Name) + "<br/>";
                                    }

                                    _dt.Rows.Add(_calculateOfTransformation.IdTransformation, _idMeasurement, _idProcess, _calculateOfTransformation.Indicator.IdIndicator,
                                        _calculateOfTransformation.MeasurementUnit.IdMeasurementUnit, Common.Functions.ReplaceIndexesTags(_calculateOfTransformation.LanguageOption.Name),
                                        _calculateOfTransformation.LanguageOption.Description, Common.Functions.ReplaceIndexesTags(_calculateOfTransformation.MeasurementUnit.LanguageOption.Name),
                                        Common.Functions.ReplaceIndexesTags(_calculateOfTransformation.Indicator.LanguageOption.Name), _calculateOfTransformation.BaseTransformer.ClassName,
                                        Common.Functions.ReplaceIndexesTags(_calculateOfTransformation.Formula), _operands, _permissionType);
                                }
                            }
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    public Boolean MeasurementsOfTransformationHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idMeasurement = Convert.ToInt64(param["IdMeasurement"]);
                        
                        //Si la medicion tiene transformaciones asociadas, retorna true!
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement).Transformations.Count > 0)
                        { return true; }

                        return false;
                    }
                    public Boolean BasedMeasurementsOfTheTransformationsHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idMeasurement = Convert.ToInt64(param["IdMeasurement"]);
                        Int64 _idTransformation = Convert.ToInt64(param["IdTransformation"]);

                        //Si la transformacion tiene mas transformaciones asociadas, retorna true!
                        if (EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement).Transformation(_idTransformation).Transformations.Count > 0)
                        { return true; }

                        return false;
                    }
                    public Boolean TransformationsByTransformationHasChildren(Dictionary<String, Object> param)
                    {
                        return BasedMeasurementsOfTheTransformationsHasChildren(param);
                    }
                #endregion

            #endregion

            #region Process Framework
                private Int64 GetDuration(DateTime endDate, DateTime startDate)
                {
                    Int64 _duration = 0;
                    _duration = (Int64)Math.Ceiling(endDate.Subtract(startDate).TotalDays);
                    if (_duration < 0) { _duration = 0; }
                    return _duration;
                }

                #region Process Classifications
                    /// <summary>
                    /// Construye las columnas del datatable de ProcessClassification
                    /// </summary>
                    /// <param name="dt"></param>
                    private void ProcessClassificationColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdProcessClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdProcessClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentProcessClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdParentProcessClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.ProcessClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);

                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion ProcessClassification (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ProcessClassifications(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ProcessClassification");

                        //Contruye las columnas y sus atributos.
                        ProcessClassificationColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            Dictionary<Int64, ProcessClassification> _processClassifications = new Dictionary<Int64, ProcessClassification>();
                            //Verifica que ahora si viene el idOrg, entonces trae todas las classificaciones de esa org.
                            if (param.ContainsKey("IdProcess"))
                            {
                                //Obtiene el OrganizationClassification
                                Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                                _processClassifications = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProcess).Classifications;
                                ////Ya esta armado el DataTable, ahora lo carga
                                //foreach (ProcessClassification _processClassification in EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProcess).Classifications.Values)
                                //{
                                //    _dt.Rows.Add(_processClassification.IdProcessClassification, _processClassification.IdParentProcessClassification, _processClassification.LanguageOption.Name, _processClassification.LanguageOption.Description, _permissionType);
                                //}
                            }
                            else
                            {
                                _processClassifications = EMSLibrary.User.ProcessFramework.Map.ProcessClassifications();
                                ////Ya esta armado el DataTable, ahora lo carga
                                //foreach (ProcessClassification _processClassification in EMSLibrary.User.ProcessFramework.Map.ProcessClassifications().Values)
                                //{
                                //    _dt.Rows.Add(_processClassification.IdProcessClassification, _processClassification.IdParentProcessClassification, _processClassification.LanguageOption.Name, _processClassification.LanguageOption.Description, _permissionType);
                                //}
                            }
                            var _lnqProcessClass = from processClass in _processClassifications.Values
                                                   orderby processClass.LanguageOption.Name ascending
                                                   select processClass;
                            //Ya esta armado el DataTable, ahora lo carga
                            foreach (ProcessClassification _processClassification in _lnqProcessClass)
                            {
                                _dt.Rows.Add(_processClassification.IdProcessClassification, _processClassification.IdParentProcessClassification, _processClassification.LanguageOption.Name, _processClassification.LanguageOption.Description, _permissionType);
                            }
                        }
                        
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un ProcessClassification.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ProcessClassificationsChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ProcessClassification");

                        //Contruye las columnas y sus atributos.
                        ProcessClassificationColumns(ref _dt);

                        Int64 _idParentProcessClassification = Convert.ToInt64(param["IdProcessClassification"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        var _lnqProcessClass = from processClass in EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idParentProcessClassification).ChildrenClassifications.Values
                                               orderby processClass.LanguageOption.Name ascending
                                               select processClass;
                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (ProcessClassification _processClassification in _lnqProcessClass)
                        {
                            _dt.Rows.Add(_processClassification.IdProcessClassification, _processClassification.IdParentProcessClassification, _processClassification.LanguageOption.Name, _processClassification.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa ProcessClassification tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean ProcessClassificationsHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idParentProcessClassification = Convert.ToInt64(param["IdProcessClassification"]);
                        //Este parametro lo pasa en true cuando se carga un arbol de mapas.(entonces se debe hacer una doble pregunta para saber si tiene o no hijos)
                        Boolean _isLoadElementMap = false;
                        if (param.ContainsKey("IsLoadElementMap"))
                            { _isLoadElementMap = Convert.ToBoolean(param["IsLoadElementMap"]); }

                        if (_isLoadElementMap)
                        {
                            if ((EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idParentProcessClassification).ChildrenClassifications.Count > 0) ||
                                (EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idParentProcessClassification).ChildrenElements.Count > 0))
                            { return true; }
                        }
                        else
                        {
                            //El hijo solo va a ser clasificacion
                            if (EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idParentProcessClassification).ChildrenClassifications.Count > 0)
                            { return true; }
                        }
                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del ProcessClassification.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable ProcessClassification(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("ProcessClassification");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        Int64 _idProcessClassification = Convert.ToInt64(param["IdProcessClassification"]);

                        ProcessClassification _processClassification = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idProcessClassification);
                        ProcessClassification _parentProcessClassification = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_processClassification.IdParentProcessClassification);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _processClassification.LanguageOption.Name);

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();

                        //Carga los datos
                        if (_parentProcessClassification != null)
                        {
                            _valueLink.Add("IdProcessClassification", _parentProcessClassification.IdProcessClassification);
                            _dt.Rows.Add(Resources.CommonListManage.ParentEntity, _parentProcessClassification.LanguageOption.Name,
                                GetValueLink(Common.ConstantsEntitiesName.PF.ProcessClassification, _valueLink));
                        }
                        //_dt.Rows.Add(Resources.CommonListManage.IdProcessClassification, _processClassification.IdProcessClassification, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _processClassification.LanguageOption.Language.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Name, _processClassification.LanguageOption.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _processClassification.LanguageOption.Description, "#" + i++);

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye la pila con todo el arbol genealogico de una entidad especifica.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Stack</c></returns>
                    public Stack<String> ProcessClassificationFamily(Dictionary<String, Object> param)
                    {
                        //Define la pila donde se guardaran los KeyValues
                        Stack<String> _parents = new Stack<String>();
                        String _keyValues = String.Empty;   //string con el KeyValues para armar.
                        //Obtiene los parametros inciales del registro a buscar (sus PK)
                        Int64 _idProcessClassification = Convert.ToInt64(param["IdProcessClassification"]);

                        //Accede al objeto y arma el KeyValue para insertar en la pila.
                        Condesus.EMS.Business.PF.Entities.ProcessClassification _processClassification = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idProcessClassification);
                        _keyValues = "IdProcessClassification=" + _idProcessClassification.ToString() + "& IdParentProcessClassification=" + _processClassification.IdParentProcessClassification.ToString();
                        //Inserta el primer dato en la pila.
                        _parents.Push(_keyValues);
                        //Ahora recorre toda su familia y guarda los keys en la pila.
                        while (_processClassification.IdParentProcessClassification != 0)
                        {
                            _keyValues = "IdProcessClassification=" + _processClassification.IdParentProcessClassification.ToString() + "& IdParentProcessClassification=" + _processClassification.ParentProcessClassification.IdParentProcessClassification.ToString();
                            _parents.Push(_keyValues);
                            //Sigue con el proximo.
                            _processClassification = _processClassification.ParentProcessClassification;
                        }

                        //Finalmente retorna la pila cargada.
                        return _parents;
                    }
                #endregion

                #region Process Group Process
                    /// <summary>
                    /// Indica si esa ProcessGroupProcesses tiene hijos o no. (si tiene nodos o no.)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean ProcessGroupProcessesHasChildren(Dictionary<String, Object> param)
                    {
                        try
                        {
                            Int64 _idProcessGroupProcess = Convert.ToInt64(param["IdProcess"]);
                            switch (EMSLibrary.User.ProcessFramework.Map.Process(_idProcessGroupProcess).GetType().Name)
                            {
                                case Common.ConstantsEntitiesName.PF.ProcessGroupProcess:
                                    //if (EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProcessGroupProcess).ChildrenNodes.Count > 0)
                                    //{ return true; }
                                    return false;
                                    break;
                                //case Common.ConstantsEntitiesName.PF.ProcessGroupNode:
                                //    if (((ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(_idProcessGroupProcess)).ChildrenNodes.Count > 0)
                                //    { return true; }
                                //    break;

                                //TODO: Ver si hay que seguir agregando mas types...
                            }

                            return false;
                        }
                        catch { return false; }
                    }
                #endregion

                //#region Process Group Node
                //    /// <summary>
                //    /// Construye las columnas del datatable de ProcessClassification
                //    /// </summary>
                //    /// <param name="dt"></param>
                //    private void ProcessGroupNodeColumns(ref DataTable dt)
                //    {
                //        //Contruye las columnas y sus atributos.
                //        ColumnOptions _columnOptions = new ColumnOptions();
                //        _columnOptions.ColumnCaption = Resources.CommonListManage.IdProcess;
                //        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                //        _columnOptions.IsPrimaryKey = true;
                //        _columnOptions.DisplayInCombo = false;
                //        _columnOptions.DisplayInManage = false;
                //        _columnOptions.IsSearchable = false;
                //        _columnOptions.AllowNull = false;
                //        BuildColumnsDataTable(ref dt, "IdProcess", _columnOptions);

                //        _columnOptions = new ColumnOptions();
                //        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentProcess;
                //        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                //        _columnOptions.IsPrimaryKey = true;
                //        _columnOptions.DisplayInCombo = false;
                //        _columnOptions.DisplayInManage = false;
                //        _columnOptions.IsSearchable = false;
                //        _columnOptions.AllowNull = false;
                //        BuildColumnsDataTable(ref dt, "IdParentProcess", _columnOptions);

                //        _columnOptions = new ColumnOptions();
                //        _columnOptions.ColumnCaption = Resources.CommonListManage.Title;
                //        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                //        _columnOptions.IsPrimaryKey = false;
                //        _columnOptions.DisplayInCombo = true;
                //        _columnOptions.DisplayInManage = true;
                //        _columnOptions.IsSearchable = true;
                //        _columnOptions.AllowNull = false;
                //        _columnOptions.IsContextMenuCaption = true;
                //        BuildColumnsDataTable(ref dt, "Title", _columnOptions);

                //        _columnOptions = new ColumnOptions();
                //        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                //        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                //        _columnOptions.IsPrimaryKey = false;
                //        _columnOptions.DisplayInCombo = false;
                //        _columnOptions.DisplayInManage = true;
                //        _columnOptions.IsSearchable = true;
                //        _columnOptions.AllowNull = true;
                //        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                //        _columnOptions = new ColumnOptions();
                //        _columnOptions.ColumnCaption = Resources.CommonListManage.StartDate;
                //        _columnOptions.ColumnDataType = System.Type.GetType("System.DateTime");
                //        _columnOptions.IsPrimaryKey = false;
                //        _columnOptions.DisplayInCombo = false;
                //        _columnOptions.DisplayInManage = true;
                //        _columnOptions.IsSearchable = false;
                //        _columnOptions.AllowNull = false;
                //        BuildColumnsDataTable(ref dt, "StartDate", _columnOptions);

                //        _columnOptions = new ColumnOptions();
                //        _columnOptions.ColumnCaption = Resources.CommonListManage.EndDate;
                //        _columnOptions.ColumnDataType = System.Type.GetType("System.DateTime");
                //        _columnOptions.IsPrimaryKey = false;
                //        _columnOptions.DisplayInCombo = false;
                //        _columnOptions.DisplayInManage = true;
                //        _columnOptions.IsSearchable = false;
                //        _columnOptions.AllowNull = false;
                //        BuildColumnsDataTable(ref dt, "EndDate", _columnOptions);

                //        _columnOptions = new ColumnOptions();
                //        _columnOptions.ColumnCaption = Resources.CommonListManage.Result;
                //        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                //        _columnOptions.IsPrimaryKey = false;
                //        _columnOptions.DisplayInCombo = false;
                //        _columnOptions.DisplayInManage = true;
                //        _columnOptions.IsSearchable = false;
                //        _columnOptions.AllowNull = false;
                //        BuildColumnsDataTable(ref dt, "Result", _columnOptions);

                //        _columnOptions = new ColumnOptions();
                //        _columnOptions.ColumnCaption = Resources.CommonListManage.Completed;
                //        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                //        _columnOptions.IsPrimaryKey = false;
                //        _columnOptions.DisplayInCombo = false;
                //        _columnOptions.DisplayInManage = true;
                //        _columnOptions.IsSearchable = false;
                //        _columnOptions.AllowNull = false;
                //        BuildColumnsDataTable(ref dt, "Completed", _columnOptions);

                //        _columnOptions = new ColumnOptions();
                //        _columnOptions.ColumnCaption = Resources.CommonListManage.Duration;
                //        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                //        _columnOptions.IsPrimaryKey = false;
                //        _columnOptions.DisplayInCombo = false;
                //        _columnOptions.DisplayInManage = true;
                //        _columnOptions.IsSearchable = false;
                //        _columnOptions.AllowNull = false;
                //        BuildColumnsDataTable(ref dt, "Duration", _columnOptions);

                //        _columnOptions = new ColumnOptions();
                //        _columnOptions.ColumnCaption = Resources.CommonListManage.State;
                //        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                //        _columnOptions.IsPrimaryKey = false;
                //        _columnOptions.DisplayInCombo = false;
                //        _columnOptions.DisplayInManage = true;
                //        _columnOptions.IsSearchable = false;
                //        _columnOptions.AllowNull = false;
                //        BuildColumnsDataTable(ref dt, "State", _columnOptions);

                //        _columnOptions = new ColumnOptions();
                //        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                //        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                //        _columnOptions.IsPrimaryKey = false;
                //        _columnOptions.DisplayInCombo = false;
                //        _columnOptions.DisplayInManage = false;
                //        _columnOptions.IsSearchable = false;
                //        _columnOptions.AllowNull = false;
                //        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);

                //    }
                //    /// <summary>
                //    /// Construye el DataTable a modo List con los hijos de un ProcessGroupNodes.
                //    /// </summary>
                //    /// <param name="param">Parametros opcionales para filtrar</param>
                //    /// <returns>Un <c>DataTable</c></returns>
                //    public DataTable ProcessGroupNodesChildren(Dictionary<String, Object> param)
                //    {
                //        //Construye el datatable
                //        DataTable _dt = BuildDataTable("ProcessGroupNodes");

                //        //Contruye las columnas y sus atributos.
                //        ProcessGroupNodeColumns(ref _dt);

                //        Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                //        String _permissionType = String.Empty;
                //        ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).ProcessGroupProcess;
                //        //Obtiene el permiso que tiene el usuario para esa organizacion.
                //        if (_processGroupProcess.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                //            { _permissionType = Common.Constants.PermissionManageName; }
                //        else
                //            { _permissionType = Common.Constants.PermissionViewName; }

                //        //Ya esta armado el DataTable, ahora lo carga
                //        foreach (ProcessGroupNode _processNode in ((ProcessGroup)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).ChildrenNodes.Values)
                //        {
                //            _dt.Rows.Add(_processNode.IdProcess, _processNode.Parent.IdProcess, Common.Functions.ReplaceIndexesTags(_processNode.LanguageOption.Title),
                //                _processNode.LanguageOption.Description,
                //                _processNode.StartDate, _processNode.EndDate,
                //                _processNode.Result, _processNode.Completed, GetDuration(_processNode.EndDate, _processNode.StartDate),
                //                _processNode.State, _permissionType);
                //        }

                //        //Retorna el DataTable
                //        return _dt;
                //    }
                //    /// <summary>
                //    /// Indica si esa ProcessGroupNodes tiene hijos o no.
                //    /// </summary>
                //    /// <param name="param">Parametros opcionales para filtrar</param>
                //    /// <returns>Un<c>Boolean</c></returns>
                //    public Boolean ProcessGroupNodesHasChildren(Dictionary<String, Object> param)
                //    {
                //        Int64 _idParentProcessNode = Convert.ToInt64(param["IdProcess"]);
                //        //Este parametro lo pasa en true cuando se carga un arbol de mapas.(entonces se debe hacer una doble pregunta para saber si tiene o no hijos)
                //        Boolean _isLoadElementMap = false;
                //        if (param.ContainsKey("IsLoadElementMap"))
                //        { _isLoadElementMap = Convert.ToBoolean(param["IsLoadElementMap"]); }

                //        ProcessGroupNode _processGroupNode = (ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(_idParentProcessNode);

                //        if (_isLoadElementMap)
                //        {
                //            if ((_processGroupNode.ChildrenNodes.Count > 0) ||
                //                (_processGroupNode.ChildrenTask.Count > 0))
                //            { return true; }
                //        }
                //        else
                //        {
                //            //El hijo solo va a ser clasificacion
                //            if (_processGroupNode.ChildrenNodes.Count > 0)
                //            { return true; }
                //        }
                //        return false;
                //    }
                //    /// <summary>
                //    /// Construye el DataTable a modo Property con los datos del Process Task.
                //    /// </summary>
                //    /// <param name="param">Parametros opcionales para filtrar</param>
                //    /// <returns>Un <c>DataTable</c></returns>
                //    public DataTable ProcessGroupNode(Dictionary<String, Object> param)
                //    {
                //        //Construye el datatable
                //        DataTable _dt = BuildDataTable("ProcessGroupNode");

                //        //Contruye las columnas y sus atributos.
                //        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                //        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));

                //        Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);

                //        ProcessGroupNode _processGroupNode = (ProcessGroupNode)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess);

                //        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                //        _dt.ExtendedProperties.Add("PageTitle", _processGroupNode.LanguageOption.Title);

                //        //Carga los datos
                //        _dt.Rows.Add(Resources.CommonListManage.IdProcess, _idProcess);
                //        _dt.Rows.Add(Resources.CommonListManage.Title, _processGroupNode.LanguageOption.Title);
                //        _dt.Rows.Add(Resources.CommonListManage.Description, _processGroupNode.LanguageOption.Description);

                //        //Retorna el DataTable.
                //        return _dt;
                //    }
                //    /// <summary>
                //    /// Construye el DataTable a modo List con los ProcessGroupNodes.
                //    /// </summary>
                //    /// <param name="param">Parametros opcionales para filtrar</param>
                //    /// <returns>Un <c>DataTable</c></returns>
                //    public DataTable ProcessGroupNodes(Dictionary<String, Object> param)
                //    {
                //        //Construye el datatable
                //        DataTable _dt = BuildDataTable("ProcessGroupNodes");

                //        //Contruye las columnas y sus atributos.
                //        ProcessGroupNodeColumns(ref _dt);

                //        Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                //        String _permissionType = String.Empty;
                //        ProcessGroupProcess _processGroupProcess = (ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).ProcessGroupProcess;
                //        //Obtiene el permiso que tiene el usuario para esa organizacion.
                //        if (_processGroupProcess.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                //            { _permissionType = Common.Constants.PermissionManageName; }
                //        else
                //            { _permissionType = Common.Constants.PermissionViewName; }

                //        //Ya esta armado el DataTable, ahora lo carga
                //        foreach (ProcessGroupNode _processNode in ((ProcessGroup)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).ChildrenNodes.Values)
                //        {
                //            _dt.Rows.Add(_processNode.IdProcess, _processNode.Parent.IdProcess, Common.Functions.ReplaceIndexesTags(_processNode.LanguageOption.Title),
                //                _processNode.LanguageOption.Description,
                //                _processNode.StartDate, _processNode.EndDate,
                //                _processNode.Result, _processNode.Completed, GetDuration(_processNode.EndDate, _processNode.StartDate),
                //                _processNode.State, _permissionType);
                //        }

                //        //Retorna el DataTable
                //        return _dt;
                //    }

                //#endregion

            #endregion

            #region Risk Management
                #region Risk Classifications
                    /// <summary>
                    /// Construye las columnas del datatable de RiskClassification
                    /// </summary>
                    /// <param name="dt"></param>
                    private void RiskClassificationColumns(ref DataTable dt)
                    {
                        //Contruye las columnas y sus atributos.
                        ColumnOptions _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdRiskClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdRiskClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.IdParentRiskClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.Int64");
                        _columnOptions.IsPrimaryKey = true;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "IdParentRiskClassification", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.RiskClassification;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = true;
                        _columnOptions.DisplayInManage = true;
                        _columnOptions.IsSearchable = true;
                        _columnOptions.AllowNull = false;
                        _columnOptions.IsContextMenuCaption = true;
                        BuildColumnsDataTable(ref dt, "Name", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.Description;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = true;
                        BuildColumnsDataTable(ref dt, "Description", _columnOptions);

                        _columnOptions = new ColumnOptions();
                        _columnOptions.ColumnCaption = Resources.CommonListManage.PermissionType;
                        _columnOptions.ColumnDataType = System.Type.GetType("System.String");
                        _columnOptions.IsPrimaryKey = false;
                        _columnOptions.DisplayInCombo = false;
                        _columnOptions.DisplayInManage = false;
                        _columnOptions.IsSearchable = false;
                        _columnOptions.AllowNull = false;
                        BuildColumnsDataTable(ref dt, "PermissionType", _columnOptions);
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los datos de la Coleccion RiskClassification (ROOTs)
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable RiskClassifications(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("RiskClassification");

                        //Contruye las columnas y sus atributos.
                        RiskClassificationColumns(ref _dt);

                        //Cuando implementemos la opcion de traer todo en un filtro.
                        Boolean _showAll = false;
                        if (ValidateSelectedItemComboBox(param, ref _showAll))
                        {
                            String _permissionType = String.Empty;
                            //Obtiene el permiso que tiene el usuario para esa organizacion.
                            if (EMSLibrary.User.RiskManagement.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Ya esta armado el DataTable, ahora lo carga
                            foreach (RiskClassification _riskClassification in EMSLibrary.User.RiskManagement.Map.RiskClassifications().Values)
                            {
                                _dt.Rows.Add(_riskClassification.IdRiskClassification, _riskClassification.IdParentRiskClassification, _riskClassification.LanguageOption.Name, _riskClassification.LanguageOption.Description, _permissionType);
                            }
                        }
                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo List con los hijos de un RiskClassification.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable RiskClassificationsChildren(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("RiskClassification");

                        //Contruye las columnas y sus atributos.
                        RiskClassificationColumns(ref _dt);

                        Int64 _idParentRiskClassification = Convert.ToInt64(param["IdRiskClassification"]);
                        String _permissionType = String.Empty;
                        //Obtiene el permiso que tiene el usuario para esa organizacion.
                        if (EMSLibrary.User.RiskManagement.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                        { _permissionType = Common.Constants.PermissionManageName; }
                        else
                        { _permissionType = Common.Constants.PermissionViewName; }

                        //Ya esta armado el DataTable, ahora lo carga
                        foreach (RiskClassification _riskClassification in EMSLibrary.User.RiskManagement.Map.RiskClassification(_idParentRiskClassification).ChildrenClassifications.Values)
                        {
                            _dt.Rows.Add(_riskClassification.IdRiskClassification, _riskClassification.IdParentRiskClassification, _riskClassification.LanguageOption.Name, _riskClassification.LanguageOption.Description, _permissionType);
                        }

                        //Retorna el DataTable
                        return _dt;
                    }
                    /// <summary>
                    /// Indica si esa RiskClassification tiene hijos o no.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un<c>Boolean</c></returns>
                    public Boolean RiskClassificationsHasChildren(Dictionary<String, Object> param)
                    {
                        Int64 _idParentRiskClassification = Convert.ToInt64(param["IdRiskClassification"]);
                        //Este parametro lo pasa en true cuando se carga un arbol de mapas.(entonces se debe hacer una doble pregunta para saber si tiene o no hijos)
                        Boolean _isLoadElementMap = false;
                        if (param.ContainsKey("IsLoadElementMap"))
                            { _isLoadElementMap = Convert.ToBoolean(param["IsLoadElementMap"]); }

                        //Para cuando se implemente risk, hay que hacer esto, como en todos los mapas.
                        //if (_isLoadElementMap)
                        //{
                            if (EMSLibrary.User.RiskManagement.Map.RiskClassification(_idParentRiskClassification).ChildrenClassifications.Count > 0)
                            { return true; }
                        //}
                        return false;
                    }
                    /// <summary>
                    /// Construye el DataTable a modo Property con los datos del RiskClassification.
                    /// </summary>
                    /// <param name="param">Parametros opcionales para filtrar</param>
                    /// <returns>Un <c>DataTable</c></returns>
                    public DataTable RiskClassification(Dictionary<String, Object> param)
                    {
                        //Construye el datatable
                        DataTable _dt = BuildDataTable("RiskClassification");

                        //Contruye las columnas y sus atributos.
                        BuildColumnsDataTable(ref _dt, "Property", SetColumnViewer(Resources.Common.Property, false));
                        BuildColumnsDataTable(ref _dt, "Value", SetColumnViewer(Resources.Common.Value, true));
                        BuildColumnsDataTable(ref _dt, "KeyValueLink", SetColumnLinkViewer());

                        //Ya esta armado el DataTable, ahora lo carga
                        Int64 _idRiskClassification = Convert.ToInt64(param["IdRiskClassification"]);

                        RiskClassification _riskClassification = EMSLibrary.User.RiskManagement.Map.RiskClassification(_idRiskClassification);
                        RiskClassification _parentRiskClassification = EMSLibrary.User.RiskManagement.Map.RiskClassification(_riskClassification.IdParentRiskClassification);

                        //Con esto seteamos el nombre de la entidad que queremos mostar en la pagina Viewer...
                        _dt.ExtendedProperties.Add("PageTitle", _riskClassification.LanguageOption.Name);

                        Int16 i = 0;//Se genera este contador, para evitar claves duplicadas, es porque no todos los registros tienen link.
                        Dictionary<String, Object> _valueLink = new Dictionary<String, Object>();

                        //Carga los datos
                        if (_parentRiskClassification != null)
                        {
                            _valueLink.Add("IdRiskClassification", _parentRiskClassification.IdRiskClassification);
                            _dt.Rows.Add(Resources.CommonListManage.ParentEntity, _parentRiskClassification.LanguageOption.Name,
                                GetValueLink(Common.ConstantsEntitiesName.RM.RiskClassification, _valueLink));
                        }
                        //_dt.Rows.Add(Resources.CommonListManage.IdRiskClassification, _riskClassification.IdRiskClassification, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.LanguageName, _riskClassification.LanguageOption.Language.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Name, _riskClassification.LanguageOption.Name, "#" + i++);
                        _dt.Rows.Add(Resources.CommonListManage.Description, _riskClassification.LanguageOption.Description, "#" + i++);

                        //Retorna el DataTable
                        return _dt;
                    }
                #endregion
               
            #endregion

        #endregion
    }
}