using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data.SqlClient;

namespace Condesus.EMS.Business.IA.Collections
{
    internal class Exceptions
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal Exceptions(Credential credential)
        {
            _Credential = credential;
        }


        //read y write
        #region Read Functions
            public List<Entities.Exception> ItemByExecution(Int64 idExecution, Int64 idProcess)
            {
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                List<Entities.Exception> _exceptions = new List<Condesus.EMS.Business.IA.Entities.Exception>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.Exceptions_ReadByExcecution(idExecution, idProcess);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //if (_exceptions == null)
                    //{
                        _exceptions.Add(new Factories.ExceptionFactory().CreateException(Convert.ToInt64(_dbRecord["idException"]), Convert.ToInt64(_dbRecord["IdExceptionType"]), Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["comment"]), Convert.ToDateTime(_dbRecord["exceptionDate"]), _Credential));
                    //}
                    //else
                    //{
                    //    return new Factories.ExceptionFactory().CreateException(Convert.ToInt64(_dbRecord["idException"]), Convert.ToInt64(_dbRecord["IdExceptionType"]), Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["comment"]), Convert.ToDateTime(_dbRecord["exceptionDate"]), _Credential);
                    //}
                }
                return _exceptions;
            }
            public Entities.Exception Item(Int64 idException)
            {
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                Entities.Exception _exceptions = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.Exceptions_Read(idException);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_exceptions == null)
                    {
                        _exceptions = new Factories.ExceptionFactory().CreateException(Convert.ToInt64(_dbRecord["idException"]), Convert.ToInt64(_dbRecord["IdExceptionType"]), Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["comment"]), Convert.ToDateTime(_dbRecord["exceptionDate"]), _Credential);
                    }
                    else
                    {
                        return new Factories.ExceptionFactory().CreateException(Convert.ToInt64(_dbRecord["idException"]), Convert.ToInt64(_dbRecord["IdExceptionType"]), Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["comment"]), Convert.ToDateTime(_dbRecord["exceptionDate"]), _Credential);
                    }
                }
                return _exceptions;
            }
            public Dictionary<Int64, Entities.Exception> ItemsByTask(Int64 idProcess)
            {
                Dictionary<Int64, Entities.Exception> _oItems = new Dictionary<Int64, Entities.Exception>();

                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.Exceptions_ReadByTask(idProcess);

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Exception _oException = new Factories.ExceptionFactory().CreateException(Convert.ToInt64(_dbRecord["idException"]), Convert.ToInt64(_dbRecord["IdExceptionType"]), Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["comment"]), Convert.ToDateTime(_dbRecord["exceptionDate"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_oException.IdException, _oException);
                }
                return _oItems;
            }
            public Dictionary<Int64, Entities.Exception> ItemsByTaskOutOfRange(Int64 idProcess)
            {
                Dictionary<Int64, Entities.Exception> _oItems = new Dictionary<Int64, Entities.Exception>();

                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.Exceptions_ReadByTaskOutOfRange(idProcess);

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Exception _oException = new Factories.ExceptionFactory().CreateException(Convert.ToInt64(_dbRecord["idException"]), Convert.ToInt64(_dbRecord["IdExceptionType"]), Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["comment"]), Convert.ToDateTime(_dbRecord["exceptionDate"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_oException.IdException, _oException);
                }
                return _oItems;
            }
            public Dictionary<Int64, Entities.Exception> ItemsByMeasurement(Int64 idExecutionMeasurement)
            {
                Dictionary<Int64, Entities.Exception> _oItems = new Dictionary<Int64, Entities.Exception>();

                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.Exceptions_ReadByMeasurement(idExecutionMeasurement);

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Exception _oException = new Factories.ExceptionFactory().CreateException(Convert.ToInt64(_dbRecord["idException"]), Convert.ToInt64(_dbRecord["IdExceptionType"]), Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["comment"]), Convert.ToDateTime(_dbRecord["exceptionDate"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_oException.IdException, _oException);
                }
                return _oItems;
            }
            public Dictionary<Int64, Entities.Exception> ItemsByResources(KC.Entities.ResourceVersion resourceVersion)
            {
                Dictionary<Int64, Entities.Exception> _oItems = new Dictionary<Int64, Entities.Exception>();

                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.Exceptions_ReadByResource(resourceVersion.IdResource);

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Exception _oException = new Factories.ExceptionFactory().CreateException(Convert.ToInt64(_dbRecord["idException"]), Convert.ToInt64(_dbRecord["IdExceptionType"]), Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["comment"]), Convert.ToDateTime(_dbRecord["exceptionDate"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_oException.IdException, _oException);
                }
                return _oItems;
            }
            public Dictionary<Int64, Entities.Exception> ItemsByResourceVersion(KC.Entities.Version version)
            {
                Dictionary<Int64, Entities.Exception> _oItems = new Dictionary<Int64, Entities.Exception>();

                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.Exceptions_ReadByResourceVersion(version.IdResource, version.IdResourceFile);

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Exception _oException = new Factories.ExceptionFactory().CreateException(Convert.ToInt64(_dbRecord["idException"]), Convert.ToInt64(_dbRecord["IdExceptionType"]), Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["comment"]), Convert.ToDateTime(_dbRecord["exceptionDate"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_oException.IdException, _oException);
                }
                return _oItems;
            }

            public Dictionary<Int64, Entities.Exception> ItemsForNotification()
            {
                Dictionary<Int64, Entities.Exception> _oItems = new Dictionary<Int64, Entities.Exception>();

                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.Exceptions_ReadForNotification();

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Exception _oException = new Factories.ExceptionFactory().CreateException(Convert.ToInt64(_dbRecord["idException"]), Convert.ToInt64(_dbRecord["IdExceptionType"]), Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["comment"]), Convert.ToDateTime(_dbRecord["exceptionDate"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_oException.IdException, _oException);
                }
                return _oItems;
            }

            public IEnumerable<System.Data.Common.DbDataRecord> ItemsByExecutionsOutOfRange(Int64 idProcess)
            {

                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.Exceptions_ReadExceptions(idProcess);

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                
                return _record;
            }
            
        #endregion

        #region Write Functions

            //Remove por medicion fuera de rango
            public void Remove(Int64 idProcess, Int64 idException, Int64 idExecutionMeasurement)
            {
                try
                {
                    DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    _dbImprovementActions.Exceptions_DeleteExcutionMeasurementExceptions(idProcess, idException, idExecutionMeasurement);

                    
                }
                catch (SqlException ex)
                {                    
                    throw ex;
                }
            }


            public Entities.Exception Add(Int64 idProcess, Int64 idExcecution, Double measureValue, DateTime measureDate, Int16 idExceptionType, String comment)
            {
                try
                {
                    DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    Int64 _idException = _dbImprovementActions.Exceptions_Create(idProcess, idExcecution, measureValue, measureDate, idExceptionType, comment, _Credential.User.Person.IdPerson);

                    return Item(_idException);
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
            //Add por tarea overdue
            public Entities.Exception Add(Int64 idProcess, Int64 idExcecution, Int16 idExceptionType, String comment)
            {
                try
                {
                    DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    Int64 _idException = _dbImprovementActions.Exceptions_Create(idProcess, idExcecution, idExceptionType, comment, _Credential.User.Person.IdPerson);

                    return Item(_idException);
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
            public void Remove(Entities.ExceptionProcessTask exception)
            {
                try
                {
                    DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    exception.Remove();

                    _dbImprovementActions.Exceptions_Delete(exception.AssociateTask.IdProcess, exception.IdException, exception.AssociateExecution.IdExecution);

                    _dbImprovementActions.ExceptionHistories_Delete(exception.IdException);

                    _dbImprovementActions.Exceptions_Delete(exception.IdException);
                    
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
            public void Remove(Entities.Exception exception)
            {
                try
                {
                    DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    exception.Remove();

                    _dbImprovementActions.Exceptions_DeleteResourceVersionExceptions(exception.IdException);

                    _dbImprovementActions.ExceptionHistories_Delete(exception.IdException);

                    _dbImprovementActions.Exceptions_Delete(exception.IdException);

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

            internal void Modify(Entities.Exception exception, Boolean notify)
            {
                try
                {
                    DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    _dbImprovementActions.Exceptions_Update(exception.IdException, notify);

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
            
            public void AddExceptionProcessGroupException(Int64 idProcess, Int64 idException)
            {
                try
                {
                    DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    _dbImprovementActions.Exceptions_Create(idProcess, idException);
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


            public void ExceptionAutomaticAlert()
            {
                try
                {
                    DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    _dbImprovementActions.Exceptions_ExceptionAutomaticAlert(_Credential.User.IdPerson);
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
        #endregion

    }
}
