using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class OrganizationsRelationshipsTypes
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal OrganizationsRelationshipsTypes(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            internal Entities.OrganizationRelationshipType Item(Int64 idOrganizationsRelationshipsTypes)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.OrganizationRelationshipType _organizationRelationshipType = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationsRelationshipsTypes_ReadById(idOrganizationsRelationshipsTypes, _Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_organizationRelationshipType == null)
                    {
                        _organizationRelationshipType = new Entities.OrganizationRelationshipType(Convert.ToInt64(_dbRecord["IdOrganizationRelationshipType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _organizationRelationshipType;
                        }
                    }
                    else
                    {
                        return new Entities.OrganizationRelationshipType(Convert.ToInt64(_dbRecord["IdOrganizationRelationshipType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                    }
                }
                return _organizationRelationshipType;
            }
            internal Dictionary<Int64, Entities.OrganizationRelationshipType> Items()
            {
                //Coleccion para devolver los OrganizationRelationshipType
                Dictionary<Int64, Entities.OrganizationRelationshipType> _oItems = new Dictionary<Int64, Entities.OrganizationRelationshipType>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationsRelationshipsTypes_ReadAll(_Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id OrganizationRelationshipType igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdOrganizationRelationshipType"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdOrganizationRelationshipType"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }
                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia un OrganizationRelationshipType 
                            Entities.OrganizationRelationshipType _oOrganizationRelationshipType = new Entities.OrganizationRelationshipType(Convert.ToInt64(_dbRecord["IdOrganizationRelationshipType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
                            //Lo agrego a la coleccion
                            _oItems.Add(_oOrganizationRelationshipType.IdOrganizationRelationshipType, _oOrganizationRelationshipType);
                        }
                        _oInsert = true;
                    }
                    else
                    {
                        //Declara e instancia un OrganizationRelationshipType 
                        Entities.OrganizationRelationshipType _oOrganizationRelationshipType = new Entities.OrganizationRelationshipType(Convert.ToInt64(_dbRecord["IdOrganizationRelationshipType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);

                        //Lo agrego a la coleccion
                        _oItems.Add(_oOrganizationRelationshipType.IdOrganizationRelationshipType, _oOrganizationRelationshipType);
                    }
                }
                return _oItems;
            }
        #endregion

        #region Write Functions
            internal Entities.OrganizationRelationshipType Add(String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idOrganizationsRelationshipsTypes = _dbDirectoryServices.OrganizationsRelationshipsTypes_Create(_Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
                    //Devuelvo el objeto direccion creado
                    return new Entities.OrganizationRelationshipType(_idOrganizationsRelationshipsTypes, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                    {
                        throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                    }
                    throw ex;
                }
            }
            internal void Modify(Int64 idOrganizationsRelationshipsTypes, String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Modifico los datos de la base
                    _dbDirectoryServices.OrganizationsRelationshipsTypes_Update(idOrganizationsRelationshipsTypes, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                    {
                        throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                    }
                    throw ex;
                }
            }
            internal void Remove(Int64 idOrganizationsRelationshipsTypes)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Borrar de la base de datos
                    _dbDirectoryServices.OrganizationsRelationshipsTypes_Delete(idOrganizationsRelationshipsTypes, _Credential.DefaultLanguage.IdLanguage, _Credential.User.Person.IdPerson);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                    {
                        throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                    }
                    throw ex;
                }
            }
        #endregion
    }
}
