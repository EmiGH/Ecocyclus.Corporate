using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class Presences
    {
        #region Internal Properties
            private Credential _Credential;
            //Datos del post
            private Entities.Post _Post;
        #endregion

        internal Presences(Entities.Post post)
        {
            _Credential = post.Credential;
            _Post = post;
        }

        #region Read Functions
            internal List<Entities.Presence> Items()
            {
                List<Entities.Presence> _oItems = new List<Entities.Presence>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Presences_ReadAll(_Post.JobTitle.FunctionalPositions.Position.IdPosition, _Post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, _Post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, _Post.Person.IdPerson, _Post.Organization.IdOrganization);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    GIS.Entities.Facility _facilities = (GIS.Entities.Facility)new GIS.Collections.Facilities(_Post.Organization).Item(Convert.ToInt64(_dbRecord["IdFacility"]));
                    Entities.Presence _oPresence = new Entities.Presence(_Post, _facilities, _Credential);
                    _oItems.Add(_oPresence);
                }
                return _oItems;
            }
            internal Entities.Presence Item(GIS.Entities.Facility facility)
            {
                GIS.Entities.Facility _geographicAreaFacilities = (GIS.Entities.Facility)new GIS.Collections.Facilities(_Post.Organization).Item(facility.IdFacility);
                return new Entities.Presence(_Post, _geographicAreaFacilities, _Credential);
            }
        #endregion

        #region Write Function
            internal void Add(GIS.Entities.Facility facility)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //alta del presence
                    _dbDirectoryServices.Presences_Create(_Post.JobTitle.FunctionalPositions.Position.IdPosition, _Post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, _Post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, _Post.Person.IdPerson, facility.IdFacility, _Post.Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
            internal void Remove(Entities.Presence presence)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Borrar de la base de datos
                    _dbDirectoryServices.Presences_Delete(presence.Post.JobTitle.FunctionalPositions.Position.IdPosition, presence.Post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, presence.Post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, presence.Post.Person.IdPerson, presence.Facility.IdFacility, presence.Post.Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
