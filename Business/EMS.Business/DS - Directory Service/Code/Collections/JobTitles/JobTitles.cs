using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class JobTitles
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.Organization _Organization;
            private Entities.OrganizationalChart _OrganizationalChart;
        #endregion

        //solo se usa cuando se consumen todos de organizations
        internal JobTitles(Credential credential)
        {
            _Credential = credential;
        }

        //solo se usa cuando se consumen todos de organizations
        internal JobTitles(Entities.Organization organization)
        {
            _Credential = organization.Credential;
            _Organization = organization;            
        }
        //se usa cuando se consume desde OrganizationalChart 
        internal JobTitles(Entities.OrganizationalChart organizationalChart)
        {
            _Credential = organizationalChart.Organization.Credential;
            _Organization = organizationalChart.Organization;
            _OrganizationalChart = organizationalChart;
        }

        #region Read Functions
        /// <summary>
        /// item por ids
        /// </summary>
        /// <param name="idposition"></param>
        /// <param name="idGeographicArea"></param>
        /// <param name="idFunctionalArea"></param>
        /// <returns></returns>
        internal Entities.JobTitle Item(Int64 idOrganization, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idposition)
        {
            return new Entities.FactoryJobtitle().CreateJobTitle(idOrganization, idGeographicArea, idFunctionalArea, idposition, _Credential, _OrganizationalChart); 
            //DS.Entities.FunctionalArea _functionalArea = new DS.Collections.FunctionalAreas(_Organization).Item(idFunctionalArea);
            //GIS.Entities.GeographicArea _geographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(idGeographicArea);
            //DS.Entities.Position _position = new DS.Collections.Positions(_Organization).Item(idposition);
            //DS.Entities.GeographicFunctionalArea _geographicFunctionalArea = new DS.Collections.GeographicFunctionalAreas(_Organization).Item(_functionalArea, _geographicArea);
            //DS.Entities.FunctionalPosition _functionalPosition = new DS.Collections.FunctionalPositions(_Organization).Item(_position, _functionalArea);
            //return Item(_geographicFunctionalArea, _functionalPosition);
        }
        /// <summary>
        /// item por objeto
        /// </summary>
        /// <param name="geographicFunctionalArea"></param>
        /// <param name="functionalPosition"></param>
        /// <returns></returns>
        //internal Entities.JobTitle Item(Entities.GeographicFunctionalArea geographicFunctionalArea, Entities.FunctionalPosition functionalPosition)
        //{
        //    return Entities.FactoryJobtitle.CreateJobTitle(geographicFunctionalArea, functionalPosition, _OrganizationalChart);
        //}

        /// <summary>
        /// se usa para devolver el parent
        /// </summary>
        /// <param name="jobTitle"></param>
        /// <returns></returns>
        internal Entities.JobtitleWithChart Item(Entities.JobtitleWithChart jobTitle)
        {
            //Coleccion a devolver
            Entities.JobtitleWithChart _oJobTitle = null;

            //Objeto de data layer para acceder a datos
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.JobTitles_ReadParent(_OrganizationalChart.IdOrganizationalChart, _OrganizationalChart.IdOrganization, jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, jobTitle.FunctionalPositions.Position.IdPosition, jobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //GIS.Entities.GeographicArea _geographicAreaParent = new GIS.Collections.GeographicAreas(_Credential).Item(Common.Common.CastNullValues(Convert.ToInt64(_dbRecord["IdGeographicAreaParent"]), 0));
                //Entities.Position _positionParent = new Positions(_Organization).Item(Common.Common.CastNullValues(Convert.ToInt64(_dbRecord["IdPositionParent"]), 0));
                //Entities.FunctionalArea _functionalAreaParent = new FunctionalAreas(_Organization).Item(Common.Common.CastNullValues(Convert.ToInt64(_dbRecord["IdFunctionalAreaParent"]), 0));
                //Entities.FunctionalPosition _parentFunctionalPosition = new FunctionalPositions(_Organization).Item(_positionParent, _functionalAreaParent);
                //Entities.GeographicFunctionalArea _parentGeographicFunctionalArea = new GeographicFunctionalAreas(_Organization).Item(_functionalAreaParent,_geographicAreaParent);
                _oJobTitle = (Entities.JobtitleWithChart) new Entities.FactoryJobtitle().CreateJobTitle(_Organization.IdOrganization,  Common.Common.CastNullValues(Convert.ToInt64(_dbRecord["IdGeographicAreaParent"]), 0), Common.Common.CastNullValues(Convert.ToInt64(_dbRecord["IdFunctionalAreaParent"]), 0), Common.Common.CastNullValues(Convert.ToInt64(_dbRecord["IdPositionParent"]), 0), _Credential, _OrganizationalChart);
            }
            return _oJobTitle;
        }
        /// <summary>
        /// Devuelve los JT que son roots en un chart
        /// </summary>
        /// <returns></returns>
        internal List<Entities.JobtitleWithChart> ItemsRoot()
        {
            //Coleccion a devolver
            List<Entities.JobtitleWithChart> _oItems = new List<Entities.JobtitleWithChart>();

            //Objeto de data layer para acceder a datos
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.JobTitles_ReadRoot(_OrganizationalChart.IdOrganizationalChart, _OrganizationalChart.IdOrganization);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //GIS.Entities.GeographicArea _geographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(Convert.ToInt64(_dbRecord["IdGeographicArea"]));
                //Entities.Position _position = new Positions(_Organization).Item(Convert.ToInt64(_dbRecord["IdPosition"]));
                //Entities.FunctionalArea _functionalArea = new FunctionalAreas(_Organization).Item(Convert.ToInt64(_dbRecord["IdFunctionalArea"]));
                //DS.Entities.GeographicFunctionalArea _geographicFunctionalArea = new DS.Collections.GeographicFunctionalAreas(_Organization).Item(_functionalArea, _geographicArea);
                //DS.Entities.FunctionalPosition _functionalPosition = new DS.Collections.FunctionalPositions(_Organization).Item(_position, _functionalArea);
                Entities.JobtitleWithChart _oJobTitle = (Entities.JobtitleWithChart)Item(_Organization.IdOrganization, Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(_dbRecord["IdFunctionalArea"]), Convert.ToInt64(_dbRecord["IdPosition"]));
                _oItems.Add(_oJobTitle);
            }
            return _oItems;
        }
        /// <summary>
        /// devuelve los hijo de ese JT en el chart
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        internal List<Entities.JobtitleWithChart> Items(Entities.JobtitleWithChart parent)
        {
            //Coleccion a devolver
            List<Entities.JobtitleWithChart> _oItems = new List<Entities.JobtitleWithChart>();

            //Objeto de data layer para acceder a datos
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.JobTitles_ReadByJobTitle(_OrganizationalChart.IdOrganizationalChart, _OrganizationalChart.IdOrganization,parent.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, parent.FunctionalPositions.Position.IdPosition, parent.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //GIS.Entities.GeographicArea _geographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(Convert.ToInt64(_dbRecord["IdGeographicArea"]));
                //Entities.Position _position = new Positions(_Organization).Item(Convert.ToInt64(_dbRecord["IdPosition"]));
                //Entities.FunctionalArea _functionalArea = new FunctionalAreas(_Organization).Item(Convert.ToInt64(_dbRecord["IdFunctionalArea"]));
                //DS.Entities.GeographicFunctionalArea _geographicFunctionalArea = new DS.Collections.GeographicFunctionalAreas(_Organization).Item(_functionalArea, _geographicArea);
                //DS.Entities.FunctionalPosition _functionalPosition = new DS.Collections.FunctionalPositions(_Organization).Item(_position, _functionalArea);

                Entities.JobtitleWithChart _oJobTitle = (Entities.JobtitleWithChart)Item(_Organization.IdOrganization, Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(_dbRecord["IdFunctionalArea"]),  Convert.ToInt64(_dbRecord["IdPosition"]));
                _oItems.Add(_oJobTitle);
            }
            return _oItems;
        }
        /// <summary>
        /// JT de una organiacion
        /// </summary>
        /// <returns></returns>
        internal List<Entities.JobTitle> Items()
        {
            //Coleccion a devolver
            List<Entities.JobTitle> _oItems = new List<Entities.JobTitle>();

            //Objeto de data layer para acceder a datos
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.JobTitles_ReadAll(_Organization.IdOrganization);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //GIS.Entities.GeographicArea _geographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(Convert.ToInt64(_dbRecord["IdGeographicArea"]));
                //Entities.Position _position = new Positions(_Organization).Item(Convert.ToInt64(_dbRecord["IdPosition"]));
                //Entities.FunctionalArea _functionalArea = new FunctionalAreas(_Organization).Item(Convert.ToInt64(_dbRecord["IdFunctionalArea"]));
                //DS.Entities.GeographicFunctionalArea _geographicFunctionalArea = new DS.Collections.GeographicFunctionalAreas(_Organization).Item(_functionalArea, _geographicArea);
                //DS.Entities.FunctionalPosition _functionalPosition = new DS.Collections.FunctionalPositions(_Organization).Item(_position, _functionalArea);

                Entities.JobTitle _oJobTitle = Item(_Organization.IdOrganization, Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(_dbRecord["IdFunctionalArea"]), Convert.ToInt64(_dbRecord["IdPosition"]));
                _oItems.Add(_oJobTitle);
            }
            return _oItems;
        }
        #endregion

        #region Write Functions
            internal Entities.JobTitle Add(Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicAreaParent, Int64 idPositionParent, Int64 idFunctionalAreaParent)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbDirectoryServices.JobTitles_Create(idGeographicArea, idPosition, idFunctionalArea, _Credential.User.Person.IdPerson, _OrganizationalChart.IdOrganization);

                    _dbDirectoryServices.JobTitles_CreateRelationship(_OrganizationalChart.IdOrganizationalChart, _OrganizationalChart.IdOrganization, idGeographicArea, idPosition, idFunctionalArea, idGeographicAreaParent, idPositionParent, idFunctionalAreaParent);

                    return new Entities.JobTitle(_Organization.IdOrganization, idGeographicArea, idFunctionalArea, idPosition, _Credential);
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
            internal void Modify(Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicAreaParent, Int64 idPositionParent, Int64 idFunctionalAreaParent)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.JobTitles_UpdateRelationship(_OrganizationalChart.IdOrganizationalChart, _OrganizationalChart.IdOrganization, idGeographicArea, idPosition, idFunctionalArea, idGeographicAreaParent, idPositionParent, idFunctionalAreaParent);
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
        
            internal void Remove(Entities.JobTitle jobTitle)
            {
                try
                {                    
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    //Borra dependencias
                    jobTitle.Remove();

                    _dbDirectoryServices.JobTitles_DeleteRelationship(jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, jobTitle.FunctionalPositions.Position.IdPosition, jobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea);
                    //Borrar de la base de datos
                    _dbDirectoryServices.JobTitles_Delete(jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea,jobTitle.FunctionalPositions.Position.IdPosition, jobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, _Credential.User.Person.IdPerson, jobTitle.Organization.IdOrganization);
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
            internal void RemoveRelationship(Entities.JobTitle jobTitle)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.JobTitles_DeleteRelationship(_OrganizationalChart.IdOrganizationalChart, _OrganizationalChart.IdOrganization, jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, jobTitle.FunctionalPositions.Position.IdPosition, jobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea);
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
            /// Borra todas las relaciones para un chart
            /// </summary>
            internal void Remove()
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.JobTitles_DeleteRelationship(_OrganizationalChart.IdOrganizationalChart, _OrganizationalChart.IdOrganization);
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
