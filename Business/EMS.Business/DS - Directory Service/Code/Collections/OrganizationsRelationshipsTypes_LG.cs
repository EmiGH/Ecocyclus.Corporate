using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    public class OrganizationsRelationshipsTypes_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdOrganizationRelationshipsType; //El identificador del tipo de relacion de organizacion         
        #endregion

        internal OrganizationsRelationshipsTypes_LG(Int64 idOrganizationRelationshipsType, Credential credential)
        {
            _Credential = credential;
            _IdOrganizationRelationshipsType = idOrganizationRelationshipsType;
        }

        #region Read Functions
            public Entities.OrganizationRelationshipType_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.OrganizationRelationshipType_LG _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationsRelationShipsTypes_LG_ReadById(_IdOrganizationRelationshipsType, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.OrganizationRelationshipType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                }
                return _item;
            }
            public Dictionary<String, Entities.OrganizationRelationshipType_LG> Items()
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Dictionary<String, Entities.OrganizationRelationshipType_LG> _oOrganizationRelationshipType_LG = new Dictionary<String, Entities.OrganizationRelationshipType_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationsRelationShipsTypes_LG_ReadAll(_IdOrganizationRelationshipsType);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.OrganizationRelationshipType_LG _oOrganizationRelationshipType = new Entities.OrganizationRelationshipType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                    _oOrganizationRelationshipType_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oOrganizationRelationshipType);
                }

                return _oOrganizationRelationshipType_LG;
            }
        #endregion

        #region Write Functions
            public Entities.OrganizationRelationshipType_LG Add(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbDirectoryServices.OrganizationsRelationShipsTypes_LG_Create(_IdOrganizationRelationshipsType, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.OrganizationRelationshipType_LG(idLanguage, name, description);
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
            public Entities.OrganizationRelationshipType_LG Modify(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.OrganizationsRelationShipsTypes_LG_Update(_IdOrganizationRelationshipsType, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.OrganizationRelationshipType_LG(idLanguage, name, description);
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
            public void Remove(String idLanguage)
            {
                if (_Credential.DefaultLanguage.IdLanguage == idLanguage)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
                }
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.OrganizationsRelationShipsTypes_LG_Delete(_IdOrganizationRelationshipsType, idLanguage, _Credential.User.Person.IdPerson);
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
