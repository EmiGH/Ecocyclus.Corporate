using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class Responsibilities
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.JobTitle _JobTitle;
        #endregion

        internal Responsibilities(Entities.JobTitle jobtitle, Credential credential)
        {
            _Credential = credential;
            _JobTitle = jobtitle;
        }

        #region Read Functions
            internal List<Entities.Responsibility> Items()
            {
                List<Entities.Responsibility> _oItems = new List<Entities.Responsibility>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Responsibilities_ReadAll(_JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, _JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, _JobTitle.FunctionalPositions.Position.IdPosition, _JobTitle.Organization.IdOrganization);
                Entities.Organization _organization = new Organizations(_Credential).Item(_JobTitle.Organization.IdOrganization);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {

                    GIS.Entities.GeographicArea _geographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(Convert.ToInt64(_dbRecord["IdGeographicAreaResponsibility"]));
                    Entities.FunctionalArea _functionalArea = new FunctionalAreas(_organization).Item(Convert.ToInt64(_dbRecord["IdFunctionalAreaResponsibility"]));
                    Entities.GeographicFunctionalArea _geographicFunctionalAreaResponsability = new GeographicFunctionalAreas(_organization).Item(_functionalArea,_geographicArea);
                    Entities.Responsibility _oResponsibility = new Entities.Responsibility(_JobTitle,_geographicFunctionalAreaResponsability, _Credential);
                    _oItems.Add(_oResponsibility);
                }
                return _oItems;
            }
            internal Entities.Responsibility Item(Int64 idFunctionalAreaResponsibility, Int64 idGeographicAreaResponsibility)
            {
                //No la leo porque los parametros ya la construyen
                Entities.Organization _organization = new Organizations(_Credential).Item(_JobTitle.Organization.IdOrganization);
                GIS.Entities.GeographicArea _geographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(idGeographicAreaResponsibility);
                Entities.FunctionalArea _functionalArea = new FunctionalAreas(_organization).Item(idFunctionalAreaResponsibility);
                Entities.GeographicFunctionalArea _geographicFunctionalAreaResponsability = new GeographicFunctionalAreas(_organization).Item(_functionalArea, _geographicArea);
                return new Entities.Responsibility(_JobTitle,_geographicFunctionalAreaResponsability, _Credential);
            }
        #endregion

        #region Write Function
            internal void Add(Entities.FunctionalArea functionalAreaResponsibility, GIS.Entities.GeographicArea geographicAreaResponsibility)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //alta del Functional position
                    _dbDirectoryServices.Responsibilities_Create(_JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, _JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, _JobTitle.FunctionalPositions.Position.IdPosition, functionalAreaResponsibility.IdFunctionalArea, geographicAreaResponsibility.IdGeographicArea, _JobTitle.Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
            internal void Remove(Entities.Responsibility responsibility)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Borrar de la base de datos
                    _dbDirectoryServices.Responsibilities_Delete(_JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, _JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, _JobTitle.FunctionalPositions.Position.IdPosition, responsibility.GeographicFunctionalAreaResponsibility.FunctionalArea.IdFunctionalArea, responsibility.GeographicFunctionalAreaResponsibility.GeographicArea.IdGeographicArea, _JobTitle.Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
            /// Borra todos los responsabilities para este jobtitle
            /// </summary>
            internal void Remove()
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Borrar de la base de datos
                    _dbDirectoryServices.Responsibilities_DeleteByJobTitle(_JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, _JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, _JobTitle.FunctionalPositions.Position.IdPosition, _JobTitle.Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
