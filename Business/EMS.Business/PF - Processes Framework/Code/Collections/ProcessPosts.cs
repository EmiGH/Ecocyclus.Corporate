using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    //TODO:borrar esta clase
    //internal class ProcessPosts
    //{
    //    #region Internal Properties
    //        private Credential _Credential;
    //    #endregion

    //    internal ProcessPosts(Credential credential)
    //    {
    //        _Credential = credential;
    //    }

    //    #region Read Functions
    //        internal Entities.ProcessPost Item(Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea)
    //        {
    //           //Objeto de data layer para acceder a datos
    //            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
    //            //DataAccess.PF.Entities.ProcessPosts _dbProcessPosts = _dbProcessesFramework.ProcessPosts;

    //            Entities.ProcessPost _processPost = null;
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessPosts_ReadById(idProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea);
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                if (_processPost == null)
    //                {
    //                    _processPost = new Condesus.EMS.Business.PF.Entities.ProcessPost(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToInt64(_dbRecord["IdPosition"]), Convert.ToInt64(_dbRecord["IdFunctionalArea"]), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(_dbRecord["IdPermission"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]));
    //                }
    //                else
    //                {
    //                    return new Condesus.EMS.Business.PF.Entities.ProcessPost(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToInt64(_dbRecord["IdPosition"]), Convert.ToInt64(_dbRecord["IdFunctionalArea"]), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(_dbRecord["IdPermission"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]));
    //                }
    //            }
    //            return _processPost;
    //        }
    //        internal List<Entities.ProcessPost> Items(Int64 idProcess, Int64 idPerson)
    //    {
    //         //Objeto de data layer para acceder a datos
    //        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
    //        //DataAccess.PF.Entities.ProcessPosts _dbProcessPosts = _dbProcessesFramework.ProcessPosts;

    //        //Coleccion para devolver las areas funcionales
    //        List<Entities.ProcessPost> _oItems = new List<Entities.ProcessPost>();

    //        IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessPosts_ReadAll(idProcess, idPerson);
    //        foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //        {
    //            Entities.ProcessPost _processPost = new Condesus.EMS.Business.PF.Entities.ProcessPost(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToInt64(_dbRecord["IdPosition"]), Convert.ToInt64(_dbRecord["IdFunctionalArea"]), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(_dbRecord["IdPermission"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]));
    //            //Lo agrego a la coleccion
    //            _oItems.Add(_processPost);
    //        }
    //        return _oItems;
    //    }
    //    #endregion

    //    #region Write Functions
    //        internal Entities.ProcessPost Add(Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime startDate, DateTime endDate, Int64 idPermission)
    //        {
    //            try
    //            {
    //                //Objeto de data layer para acceder a datos
    //                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
    //                //DataAccess.PF.Entities.ProcessPosts _dbProcessPosts = _dbProcessesFramework.ProcessPosts;

    //                //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
    //                _dbProcessesFramework.ProcessPosts_Create(idProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea, startDate, endDate, idPermission, _Credential.User.Person.IdPerson);

    //                //Devuelvo el objeto FunctionalArea creado
    //                return new Entities.ProcessPost(idProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea, idPermission, startDate, endDate);
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
    //        internal void Remove(Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea)
    //        {
    //            try
    //            {
    //                //Objeto de data layer para acceder a datos
    //                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
    //                //DataAccess.PF.Entities.ProcessPosts _dbProcessPosts = _dbProcessesFramework.ProcessPosts;

    //                //Borrar de la base de datos
    //                _dbProcessesFramework.ProcessPosts_Delete(idProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea, _Credential.User.Person.IdPerson);
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
    //        internal void Modify(Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime startDate, DateTime endDate, Int64 idPermission)
    //        {
    //            try
    //            {
    //                //Objeto de data layer para acceder a datos
    //                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
    //                //DataAccess.PF.Entities.ProcessPosts _dbProcessPosts = _dbProcessesFramework.ProcessPosts;

    //                //Modifico los datos de la base
    //                _dbProcessesFramework.ProcessPosts_Update(idProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea, startDate, endDate, idPermission, _Credential.User.Person.IdPerson);
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
