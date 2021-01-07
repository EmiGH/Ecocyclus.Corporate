using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class OrganizationsRelationships
    {
        #region Internal Properties
        private Credential _Credential;
        private Entities.Organization _Organization;
        #endregion

        internal OrganizationsRelationships(Entities.Organization organization)
        {
            _Credential = organization.Credential;
            _Organization = organization;
        }

        #region Read Functions
            internal Entities.OrganizationRelationship Item(Int64 idOrganization1, Int64 idOrganization2, Int64 idOrganizationRelationshipType)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.OrganizationRelationship _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationsRelationships_ReadById(idOrganization1, idOrganization2, idOrganizationRelationshipType);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Organization _organization = new Organizations(_Credential).Item(Convert.ToInt64(_dbRecord["IdOrganizationRelated"]));
                    Entities.Organization _organizationRelated = new Organizations(_Credential).Item(Convert.ToInt64(_dbRecord["IdOrganizationRelated"]));
                    Entities.OrganizationRelationshipType _organizationRelationshipType = new OrganizationsRelationshipsTypes(_Credential).Item(Convert.ToInt64(_dbRecord["IdOrganizationRelationshipType"]));
                    _item = new Entities.OrganizationRelationship(_organization, _organizationRelated, _organizationRelationshipType,_Credential);
                }
                return _item;
            }
            internal List<Entities.OrganizationRelationship> Items()
            {
                //Colección de OrganizationRelationship a devolver
                List<Entities.OrganizationRelationship> _oItems = new List<Entities.OrganizationRelationship>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationsRelationships_GetAllByIdOrganization(_Organization.IdOrganization);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Organization _organizationRelated = new Organizations(_Credential).Item(Convert.ToInt64(_dbRecord["IdOrganizationRelated"]));
                    Entities.OrganizationRelationshipType _organizationRelationshipType = new OrganizationsRelationshipsTypes(_Credential).Item(Convert.ToInt64(_dbRecord["IdOrganizationRelationshipType"]));
                    //Declara e instancia un lenguaje
                    Entities.OrganizationRelationship _oOrganizationRelationship = new Entities.OrganizationRelationship(_Organization, _organizationRelated, _organizationRelationshipType, _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_oOrganizationRelationship);
                }
                return _oItems;
            }
        #endregion

        #region Write Functions
            internal void Add(Entities.Organization organizationRelated, Entities.OrganizationRelationshipType organizationRelationshipType)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbDirectoryServices.OrganizationsRelationships_Create(_Organization.IdOrganization, organizationRelated.IdOrganization, organizationRelationshipType.IdOrganizationRelationshipType, _Credential.User.Person.IdPerson);
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
            internal void Remove(Entities.OrganizationRelationship organizationRelationship)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    //Borrar de la base de datos
                    _dbDirectoryServices.OrganizationsRelationships_Delete(organizationRelationship.Organization.IdOrganization, organizationRelationship.OrganizationRelated.IdOrganization, organizationRelationship.OrganizationRelationshipType.IdOrganizationRelationshipType, _Credential.User.Person.IdPerson);
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
        /// <summary>
        /// borra todas las relaciones para una organizacion
        /// </summary>
        /// <param name="organization"></param>
            internal void Remove(Entities.Organization organization)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Borrar de la base de datos
                    _dbDirectoryServices.OrganizationsRelationships_Delete(organization.IdOrganization);
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
