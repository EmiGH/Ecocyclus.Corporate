using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    //internal class GeographicAreas
    //{
    //    #region Internal Properties
    //        private Credential _Credential;
    //        private Entities.Organization _Organization;
    //        private Entities.GeographicArea _Parent;
    //    #endregion

    //    internal GeographicAreas(Credential credential)
    //    {
    //        _Credential = credential;
    //        _Organization = null;
    //    }

    //    internal GeographicAreas(Entities.Organization organization)
    //    {
    //        _Credential = organization.Credential;
    //        _Organization = organization;
    //    }
    //    internal GeographicAreas(Entities.GeographicArea parent, Entities.Organization organization)
    //    {
    //        _Organization = organization;
    //        _Parent = parent;
    //        _Credential = organization.Credential;
    //    }

    //    //encapsula la toma de decision de cual es el idparent
    //    private Int64 IdParent
    //    {
    //        get
    //        {
    //            if (_Parent == null) { return 0; } else { return _Parent.IdGeographicArea; }
    //        }
    //    }
    //    #region Read Functions
    //        internal Entities.GeographicArea Item(Int64 idGeographicArea)
    //        {
    //            //Objeto de data layer para acceder a datos
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //            Entities.GeographicArea _geographicArea = null;
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.GeographicAreas_ReadById(idGeographicArea, _Credential.CurrentLanguage.IdLanguage);
    //            //si no trae nada retorno 0 para que no de error
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                if ((IdParent == 0)||(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentGeographicArea"], 0)) == IdParent))
    //                {
    //                    if (_geographicArea == null)
    //                    {
    //                        KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));

    //                        _geographicArea = DS.Factory.GeographicAreaFactory.CreateGeographicArea(Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToString(_dbRecord["Mnemo"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentGeographicArea"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _resourcecatalog, _Credential);
    //                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
    //                        {
    //                            return _geographicArea;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));
    //                        //Declara e instancia un Area Geografica a traves de la Factory
    //                        return DS.Factory.GeographicAreaFactory.CreateGeographicArea(Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToString(_dbRecord["Mnemo"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentGeographicArea"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _resourcecatalog, _Credential);
    //                    }
    //                }
    //                else
    //                {
    //                    //no es hijo asi que no lo puede devolver......generar el error
    //                    return null;
    //                }
    //            }
    //            return _geographicArea;
    //        }
    //        internal Dictionary<Int64, Entities.GeographicArea> Items()
    //        {
    //            //Coleccion para devolver las areas geograficas
    //            Dictionary<Int64, Entities.GeographicArea > _oItems = new Dictionary<Int64, Entities.GeographicArea>();

    //            //Objeto de data layer para acceder a datos
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //            //Traigo los datos de la base
    //            IEnumerable<System.Data.Common.DbDataRecord> _record;
    //            if (IdParent == 0)
    //            { _record = _dbDirectoryServices.GeographicAreas_GetRoot(_Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage); }
    //            else
    //            { _record = _dbDirectoryServices.GeographicAreas_GetByParent(IdParent, _Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage); }
                
    //            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
    //            Boolean _bInsert = true;

    //            //busca si hay mas de un id Area Geografica igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdGeographicArea"])))
    //                {
    //                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
    //                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
    //                    {
    //                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdGeographicArea"]));
    //                    }
    //                    else
    //                    {
    //                        //No debe insertar en la coleccion ya que existe el idioma correcto.
    //                        _bInsert = false;
    //                    }
    //                }
    //                //Solo inserta si es necesario.
    //                if (_bInsert)
    //                {
    //                    KC.Entities.ResourceCatalog _resourcecatalog = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(_Credential).Item(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)));
    //                    Entities.GeographicArea _oGeographicArea;
    //                    if (_dbRecord["Mnemo"] == null)
    //                    {

    //                        //Declara e instancia un Area Geografica a traves de la Factory
    //                        _oGeographicArea = DS.Factory.GeographicAreaFactory.CreateGeographicArea(Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToString(Common.Common.CastNullValues(_dbRecord["Mnemo"], String.Empty)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentFacility"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _resourcecatalog, _Credential);
    //                    }
    //                    else
    //                    {
    //                        //Declara e instancia un Area Geografica
    //                        _oGeographicArea = DS.Factory.GeographicAreaFactory.CreateGeographicArea(Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToString(Common.Common.CastNullValues(_dbRecord["Mnemo"], String.Empty)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentGeographicArea"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _resourcecatalog, _Credential);
    //                    }
    //                    //Lo agrego a la coleccion
    //                    _oItems.Add(_oGeographicArea.IdGeographicArea, _oGeographicArea);
    //                }
    //                _bInsert = true;
    //            }
    //            return _oItems;
    //        }
    //    #endregion

    //    #region Write Functions
    //        internal Entities.GeographicArea Add(Boolean allowFacilities, String mnemo, String name, String description, KC.Entities.ResourceCatalog resourceCatalog)
    //        {
    //            try
    //            {
    //                Int64 _idGeographicArea;  //declara El identificador de la nueva area geografica
    //                //Objeto de data layer para acceder a datos
    //                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                //ejecuta el insert y devuelve el identificador que le asigno en la base de datos (si AllowFacilities esta chequeado manda mnemo)
    //                if (allowFacilities == true)
    //                {
    //                    //validacio para los null
    //                    Int64 _IdResourceCatalog = 0; if (resourceCatalog != null) { _IdResourceCatalog = resourceCatalog.IdResource; }

    //                    _idGeographicArea = _dbDirectoryServices.GeographicAreas_Create(_Organization.IdOrganization, IdParent, _Credential.DefaultLanguage.IdLanguage, mnemo, name, description, _IdResourceCatalog,_Credential.User.Person.IdPerson);
    //                }
    //                else
    //                {
    //                    _idGeographicArea = _dbDirectoryServices.GeographicAreas_Create(_Organization.IdOrganization, IdParent, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
    //                }

    //                //Declara e instancia un Area Geografica a traves de la Factory
    //                return DS.Factory.GeographicAreaFactory.CreateGeographicArea(_idGeographicArea, mnemo, _Organization.IdOrganization, IdParent, name, description, resourceCatalog, _Credential);
    //            }
    //            catch (SqlException ex)
    //            {
    //                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                {
    //                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                }
    //                throw ex;
    //            }
    //        }
    //        internal void Remove(Entities.GeographicArea geographicArea)
    //        {
    //            try
    //            {
    //                //Objeto de data layer para acceder a datos
    //                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
    //                //borra dependencias
    //                geographicArea.Remove();
    //                //Borrar de la base de datos
    //                _dbDirectoryServices.GeographicAreas_Delete(_Organization.IdOrganization, geographicArea.IdGeographicArea, _Credential.User.Person.IdPerson);
    //            }
    //            catch (SqlException ex)
    //            {
    //                if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
    //                {
    //                    throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
    //                }
    //                throw ex;
    //            }
    //        }
    //        internal void Modify(Entities.GeographicArea geographicArea, Boolean allowFacilities, String mnemo, String name, String description, KC.Entities.ResourceCatalog resourceCatalog)
    //        {
    //            try
    //            {
    //                //Objeto de data layer para acceder a datos
    //                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                //Modifico los datos de la base
    //                if (allowFacilities == true)
    //                {
    //                    //validacio para los null
    //                    Int64 _IdResourceCatalog = 0; if (resourceCatalog != null) { _IdResourceCatalog = resourceCatalog.IdResource; }

    //                    _dbDirectoryServices.GeographicAreas_Update(geographicArea.IdGeographicArea, IdParent, _Organization.IdOrganization, _Credential.DefaultLanguage.IdLanguage, mnemo, name, description, _IdResourceCatalog, _Credential.User.Person.IdPerson);
    //                }
    //                else
    //                {
    //                    _dbDirectoryServices.GeographicAreas_Update(geographicArea.IdGeographicArea, IdParent, _Organization.IdOrganization, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
    //                }
    //            }
    //            catch (SqlException ex)
    //            {
    //                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                {
    //                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                }
    //                throw ex;
    //            }
    //        }
    //    #endregion

    //}
}
