using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class Posts
    {
        #region Internal Properties
            private Credential _Credential;
            private ICollectionItems _Datasource; 
            private Entities.Organization _Organization;
            private Entities.Person _Person;
            private Entities.JobTitle _JobTitle;

        #endregion


        internal Posts(Credential credential)
        {
            _Credential = credential;
        }

        internal Posts(PF.Entities.ProcessTask processTask)
        {
            _Credential = processTask.Credential;
        }

        internal Posts(PF.Entities.ProcessTaskExecution processTaskExecution)
        {
            _Credential = processTaskExecution.Credential;
        }

        internal Posts(Entities.Person person)
        {
            _Credential = person.Organization.Credential;
            _Datasource = new PostRead.PostByPerson(person);
            _Person = person;
        }
        internal Posts(Entities.Organization organization)
        {
            //Este contructor es para poder acceder desde la organizacion a todos los puestos de una organizacion, sin person.
            _Credential = organization.Credential;
            _Datasource = new PostRead.PostByOrganization(organization);
            _Organization = organization;
        }
        internal Posts(Entities.JobTitle jobTitle)
        {
            //Este contructor es para poder acceder desde la organizacion a todos los puestos de una organizacion, sin person.
            _Credential = jobTitle.Organization.Credential;
            _Datasource = new PostRead.PostByJobTitle(jobTitle);
            _JobTitle = jobTitle;
        }

        #region Read Functions
            internal List<Entities.Post> Items()
            {
                List<Entities.Post> _oItems = new List<Entities.Post>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Post _oPost = new Entities.Post(Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(_dbRecord["IdFunctionalArea"]), Convert.ToInt64(_dbRecord["IdPosition"]), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["Enddate"], DateTime.MinValue)), _Credential);
                    _oItems.Add(_oPost);
                }
                return _oItems;
            }

            internal Entities.Post Item(Int64 idPerson, Int64 idOrganization, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.Post _item = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Posts_ReadById(idPosition, idGeographicArea, idFunctionalArea, idPerson, idOrganization);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _item = new Entities.Post(idPerson, idOrganization, idGeographicArea, idFunctionalArea, idPosition, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["Enddate"], DateTime.MinValue)), _Credential);
                }
                return _item;
            }

            internal List<PF.Entities.ProcessTask> ProcessTaskOperator(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idPosition)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //Coleccion para devolver las areas funcionales
                List<PF.Entities.ProcessTask> _oItems = new List<PF.Entities.ProcessTask>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskPermissions_ReadByPost(_Organization.IdOrganization, idGeographicArea, idFunctionalArea, idPosition, _Person.IdPerson);

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia una posicion
                    PF.Entities.ProcessTask _processTask = new PF.Collections.ProcessTasks(_Credential).Item(Convert.ToInt64(_dbRecord["IdProcess"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processTask);

                }
                return _oItems;
            }

        #endregion

        #region Write Function
            internal Entities.Post Add(Entities.FunctionalArea functionalArea, GIS.Entities.GeographicArea geographicArea, Entities.Position position, DateTime startDate, DateTime endDate)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //alta del Functional position
                    _dbDirectoryServices.Posts_Create(position.IdPosition, geographicArea.IdGeographicArea, functionalArea.IdFunctionalArea, _Person.IdPerson, _Person.Organization.IdOrganization, startDate, endDate, _Credential.User.Person.IdPerson);

                    return new Entities.Post(_Person.IdPerson, _Person.Organization.IdOrganization, geographicArea.IdGeographicArea, functionalArea.IdFunctionalArea, position.IdPosition, startDate, endDate, _Credential);
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
            internal void Modify(Entities.Post post, DateTime startDate, DateTime endDate)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //alta del Functional position
                    _dbDirectoryServices.Posts_Update(post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, post.Person.IdPerson, post.Organization.IdOrganization, startDate, endDate, _Credential.User.Person.IdPerson);
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
            internal void Remove(Entities.Post post)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    //Borra dependencias
                    post.Remove();
                    //Borrar de la base de datos
                    _dbDirectoryServices.Posts_Delete(post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, post.Person.IdPerson, post.Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
