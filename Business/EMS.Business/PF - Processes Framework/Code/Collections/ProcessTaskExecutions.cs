using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PF.Collections
{
    internal class ProcessTaskExecutions
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal ProcessTaskExecutions(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Common Functions
            internal DateTime FirstExecution(Int64 idProcess)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                DateTime _minDate = DateTime.MinValue;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadFirstExecution(idProcess);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _minDate = Convert.ToDateTime(_dbRecord["date"]);
                }
                return _minDate;
            }
            internal Entities.ProcessTaskExecution Item(Int64 idProcess, Int64 idExecution)
            {
   
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                Entities.ProcessTaskExecution _processTaskExecution = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadById(idProcess, idExecution);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_processTaskExecution == null)
                    {
                        _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.Now)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    }
                    else
                    {
                        return Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.Now)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    }
                }
                return _processTaskExecution;
            }
            internal Dictionary<Int64, Entities.ProcessTaskExecution> Items(Int64 idProcess)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.ProcessTaskExecution> _oItems = new Dictionary<Int64, Entities.ProcessTaskExecution>();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadAll(idProcess);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ProcessTaskExecution _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    //Lo agrego a la coleccion
                    if (!_oItems.ContainsKey(_processTaskExecution.IdExecution))
                    {
                        _oItems.Add(_processTaskExecution.IdExecution, _processTaskExecution);
                    }
                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessTaskExecution> ItemsOnlyExecution(Int64 idProcess)
            {
      
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessTaskExecutions _dbProcessTaskExecutions = _dbProcessesFramework.ProcessTaskExecutions;

                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.ProcessTaskExecution> _oItems = new Dictionary<Int64, Entities.ProcessTaskExecution>();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadAllOnlyExecution(idProcess);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ProcessTaskExecution _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    //Lo agrego a la coleccion
                    _oItems.Add(_processTaskExecution.IdExecution, _processTaskExecution);
                }
                return _oItems;
            }
            /// <summary>
            /// Devuelve la ultima ejecucion ingresada
            /// </summary>
            /// <param name="idProcess"></param>
            /// <param name="idExecution"></param>
            /// <returns>Un objeto ProcessTaskExecution</returns>
            internal Entities.ProcessTaskExecution LastExecution(Int64 idProcess, Int64 idExecution)
            {
    
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessTaskExecutions _dbProcessTaskExecutions = _dbProcessesFramework.ProcessTaskExecutions;

                Entities.ProcessTaskExecution _processTaskExecution = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadById(idProcess, idExecution);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                }
                //retorna la ultima ejecucion ingresada (que seria el ultimo valor tomado...)
                return _processTaskExecution;
            }
            /// <summary>
            /// Devuelve la ultima ejecucion realizada para una Tarea determinada
            /// </summary>
            /// <param name="idProcess"></param>
            /// <returns>Un objeto ProcessTaskExecution</returns>
            internal Entities.ProcessTaskExecution LastExecution(Int64 idProcessTask)
            {
               //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessTaskExecutions _dbProcessTaskExecutions = _dbProcessesFramework.ProcessTaskExecutions;

                Entities.ProcessTaskExecution _processTaskExecution = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadLastExecution(idProcessTask);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_processTaskExecution == null)
                    {
                        _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.Now)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    }
                    else
                    {
                        return Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.Now)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    }
                }
                return _processTaskExecution;
            }
            /// <summary>
            /// Devuelve la ultima ejecucion realizada para una Tarea de medicion determinada
            /// </summary>
            /// <param name="idProcess"></param>
            /// <returns>Un objeto ProcessTaskExecution</returns>
            internal Entities.ProcessTaskExecutionMeasurement LastExecutionMeasurement(Int64 idProcessTask)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessTaskExecutions _dbProcessTaskExecutions = _dbProcessesFramework.ProcessTaskExecutions;

                Entities.ProcessTaskExecution _processTaskExecution = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadLastExecutionMeasurement(idProcessTask);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {                   
                    _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.Now)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));                    
                }
                return (Entities.ProcessTaskExecutionMeasurement)_processTaskExecution;
            }
            /// <summary>
            /// Devuelve la ejecucion Actual para una Tarea determinada
            /// </summary>
            /// <param name="idProcess"></param>
            /// <returns>Un objeto ProcessTaskExecution</returns>
            internal Entities.ProcessTaskExecution NowExecution(Int64 idProcessTask)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessTaskExecutions _dbProcessTaskExecutions = _dbProcessesFramework.ProcessTaskExecutions;

                PF.Entities.ProcessTask _processTask = new Condesus.EMS.Business.PF.Collections.ProcessTasks(_Credential).Item(idProcessTask);
                //Obtiene la duracion y en que unidad esta medida.
                Int32 _duration = _processTask.Duration;
                Int64 _timeUnitDuration = _processTask.TimeUnitDuration;

                Entities.ProcessTaskExecution _processTaskExecution = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadNowExecution(idProcessTask, _timeUnitDuration, _duration);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_processTaskExecution == null)
                    {
                        _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.Now)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    }
                    else
                    {
                        return Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.Now)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    }
                }
                return _processTaskExecution;
            }
            /// <summary>
            /// Devuelve la/s proximas ejecuciones para una Tarea determinada
            /// </summary>
            /// <param name="idProcess"></param>
            /// <returns>Un objeto ProcessTaskExecution</returns>
            internal Entities.ProcessTaskExecution NextExecution(Int64 idProcessTask, DateTime startDate, DateTime endDate)
            {
                    //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessTaskExecutions _dbProcessTaskExecutions = _dbProcessesFramework.ProcessTaskExecutions;

                Entities.ProcessTaskExecution _processTaskExecution = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadNextExecution(idProcessTask, startDate, endDate);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_processTaskExecution == null)
                    {
                        _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.Now)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    }
                    else
                    {
                        return Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.Now)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    }
                }
                return _processTaskExecution;
            }
            /// <summary>
            /// Devuelve la proximas ejecuciones para una Tarea determinada para ser notificada
            /// </summary>
            /// <param name="idProcess"></param>
            /// <returns>Un objeto ProcessTaskExecution</returns>
            internal Entities.ProcessTaskExecution NextExecutionNotifice(Int64 idProcessTask)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                Entities.ProcessTaskExecution _processTaskExecution = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadExecutionNextNotifice(idProcessTask);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_processTaskExecution == null)
                    {
                        _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), 
                            Convert.ToInt64(_dbRecord["IdExecution"]), 
                            Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), 
                            Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), 
                            Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), 
                            Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), 
                            Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), 
                            Convert.ToDateTime(_dbRecord["Date"]), 
                            Convert.ToString(_dbRecord["Comment"]), 
                            (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]),
                            Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["AdvanceNotify"], false)), 
                            _Credential, 
                            Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), 
                            Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), 
                            Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), 
                            Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), 
                            Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), 
                            Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.Now)), 
                            Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), 
                            Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), 
                            Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), 
                            Convert.ToString(_dbRecord["Type"]));
                    }
                    else
                    {
                        return Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["AdvanceNotify"], false)), _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.Now)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    }
                }
                return _processTaskExecution;
            }

            internal Dictionary<Int64, Entities.ProcessTaskExecution> ItemsPlanned()
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.ProcessTaskExecution> _oItems = new Dictionary<Int64, Entities.ProcessTaskExecution>();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadByPlanned(_Credential.User.IdPerson);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ProcessTaskExecution _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    //Lo agrego a la coleccion
                    _oItems.Add(_processTaskExecution.IdExecution, _processTaskExecution);
                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessTaskExecution> ItemsPlanned(DateTime plannedDate)
            {
   
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.ProcessTaskExecution> _oItems = new Dictionary<Int64, Entities.ProcessTaskExecution>();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadByPlanned(plannedDate, _Credential.User.IdPerson);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ProcessTaskExecution _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    //Lo agrego a la coleccion
                    _oItems.Add(_processTaskExecution.IdExecution,_processTaskExecution);
                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessTaskExecution> ItemsWorking()
            {
    
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                
                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.ProcessTaskExecution> _oItems = new Dictionary<Int64, Entities.ProcessTaskExecution>();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadByWorking(_Credential.User.IdPerson);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ProcessTaskExecution _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    //Lo agrego a la coleccion
                    _oItems.Add(_processTaskExecution.IdExecution,_processTaskExecution);
                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessTaskExecution> ItemsFinished()
            {
               //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                
                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.ProcessTaskExecution> _oItems = new Dictionary<Int64, Entities.ProcessTaskExecution>();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadByFinished(_Credential.User.IdPerson);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ProcessTaskExecution _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    //Lo agrego a la coleccion
                    _oItems.Add(_processTaskExecution.IdExecution,_processTaskExecution);
                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessTaskExecution> ItemsOverdue()
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                
                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.ProcessTaskExecution> _oItems = new Dictionary<Int64, Entities.ProcessTaskExecution>();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskExecutions_ReadByOverdue(_Credential.User.IdPerson);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.ProcessTaskExecution _processTaskExecution = Factories.ExecutionFactory.CreateExecution(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPosition"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFunctionalArea"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToDateTime(_dbRecord["Date"]), Convert.ToString(_dbRecord["Comment"]), (Byte[])Common.Common.CastNullValues(_dbRecord["Attachment"]), false, _Credential, Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationStart"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["ValidationEnd"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToDouble(Common.Common.CastNullValues(_dbRecord["MeasureValue"], 0.0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["MeasureDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["TimeStamp"], DateTime.MinValue)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementUnit"], 0)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["StartDate"], DateTime.MinValue)), Convert.ToDateTime(Common.Common.CastNullValues(_dbRecord["EndDate"], DateTime.MinValue)), Convert.ToString(_dbRecord["Type"]));
                    //Lo agrego a la coleccion
                    _oItems.Add(_processTaskExecution.IdExecution,_processTaskExecution);
                }
                return _oItems;
            }

            internal Entities.ProcessTaskExecution Item(IA.Entities.Exception exception)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTasks_ReadByException(exception.IdException);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    return Item(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdExecution"]));
                }
                return null;
            }

        #endregion

        #region Write Function
            #region Process Task Execution Calibration
                internal Entities.ProcessTaskExecutionCalibration AddExecution(Entities.ProcessTaskCalibration processTask, DS.Entities.Post post, DateTime date, String comment, Byte[]attachment,  DateTime validationStart, DateTime validationEnd, PA.Entities.MeasurementDevice measuremenDevice)
                {
                    //Agrega una ejecucion de calibracion completo(todo en uno)

                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idExecution = _dbProcessesFramework.ProcessTaskExecutions_CreateExecution(processTask.IdProcess, post.JobTitle.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, date, comment, attachment, validationStart, validationEnd, measuremenDevice.IdMeasurementDevice, _Credential.User.Person.IdPerson);
                        //Devuelvo el objeto creado
                        return new Entities.ProcessTaskExecutionCalibration(_idExecution, processTask.IdProcess, post.Person.IdPerson, post.Organization.IdOrganization, post.JobTitle.IdGeographicArea, post.JobTitle.IdFunctionalArea, post.JobTitle.IdPosition, date, comment, attachment, false, validationStart, validationEnd, measuremenDevice.IdMeasurementDevice, _Credential);
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
                internal Entities.ProcessTaskExecutionCalibration AddExecution(Entities.ProcessTaskCalibration processTask, DS.Entities.Post post, Entities.ProcessTaskExecution processTaskExecution, String comment, Byte[] attachment, DateTime validationStart, DateTime validationEnd, PA.Entities.MeasurementDevice measuremenDevice)
                {
                    //Agrega solamente una ejecucion de calibracion La ejecucion ya existe, ahora se especifica de que tipo

                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        _dbProcessesFramework.ProcessTaskExecutions_CreateExecution(processTask.IdProcess, processTaskExecution.IdExecution, post.JobTitle.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, comment, attachment, validationStart, validationEnd, measuremenDevice.IdMeasurementDevice, _Credential.User.Person.IdPerson);
                        //Devuelvo el objeto creado
                        return new Entities.ProcessTaskExecutionCalibration(processTaskExecution.IdExecution, processTask.IdProcess, post.Person.IdPerson, post.Organization.IdOrganization, post.JobTitle.IdGeographicArea, post.JobTitle.IdFunctionalArea, post.JobTitle.IdPosition, processTaskExecution.Date, comment, attachment, false, validationStart, validationEnd, measuremenDevice.IdMeasurementDevice, _Credential);
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
                internal Entities.ProcessTaskExecutionCalibration AddExecution(ref Int64 idFile, Entities.ProcessTaskCalibration processTask, DS.Entities.Post post, Entities.ProcessTaskExecution processTaskExecution, String fileName, Byte[] fileStream, DateTime validationStart, DateTime validationEnd, PA.Entities.MeasurementDevice measuremenDevice)
                {
                    //Agrega solamente una ejecucion de calibracion con su archivo adjunto, La ejecucion ya existe, ahora se especifica de que tipo

                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        //Ver la forma de que una vez creada la ejecucion tenga el puntero al file que se acaba de insertar....
                        idFile = _dbProcessesFramework.ProcessTaskExecutions_CreateExecution(processTask.IdProcess, processTaskExecution.IdExecution, post.JobTitle.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, processTaskExecution.Comment, processTaskExecution.Attachment, fileName, fileStream, validationStart, validationEnd, measuremenDevice.IdMeasurementDevice, _Credential.User.Person.IdPerson);
                        //Devuelvo el objeto creado
                        return new Entities.ProcessTaskExecutionCalibration(processTaskExecution.IdExecution, processTask.IdProcess, post.Person.IdPerson, post.Organization.IdOrganization, post.JobTitle.IdGeographicArea, post.JobTitle.IdFunctionalArea, post.JobTitle.IdPosition, processTaskExecution.Date, processTaskExecution.Comment, processTaskExecution.Attachment, false, validationStart, validationEnd, measuremenDevice.IdMeasurementDevice, _Credential);
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
                internal Entities.ProcessTaskExecutionCalibration AddExecution(ref Int64 idFile, Entities.ProcessTaskCalibration processTask, DS.Entities.Post post, DateTime date, String comment, Byte[] attachment, String fileName, Byte[] fileStream, DateTime validationStart, DateTime validationEnd, PA.Entities.MeasurementDevice measuremenDevice)
                {
                    //Agrega todo junto..., una ejecucion, una ejecucion de calibracion con su archivo adjunto.h
                  try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                        //DataAccess.PF.Entities.ProcessTaskExecutions _dbProcessTaskExecutions = _dbProcessesFramework.ProcessTaskExecutions;

                        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        //Ver la forma de que una vez creada la ejecucion tenga el puntero al file que se acaba de insertar....
                        Int64 _idExecution=0;

                        _dbProcessesFramework.ProcessTaskExecutions_CreateExecution(ref _idExecution, ref idFile, processTask.IdProcess, post.JobTitle.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, date, comment, attachment, fileName, fileStream, validationStart, validationEnd, measuremenDevice.IdMeasurementDevice, _Credential.User.Person.IdPerson);
                        //Devuelvo el objeto creado
                        return new Entities.ProcessTaskExecutionCalibration(_idExecution, processTask.IdProcess, post.Person.IdPerson, post.Organization.IdOrganization, post.JobTitle.IdGeographicArea, post.JobTitle.IdFunctionalArea, post.JobTitle.IdPosition, date, comment, attachment, false, validationStart, validationEnd, measuremenDevice.IdMeasurementDevice, _Credential);
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
                internal void RemoveExecution(Entities.ProcessTaskExecutionCalibration processTaskExecutionCalibration)
                {
                    try
                    {
                        processTaskExecutionCalibration.Remove();
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                        //Borrar de la base de datos
                        _dbProcessesFramework.ProcessTaskExecutions_DeleteExecutionCalibration(processTaskExecutionCalibration.ProcessTask.IdProcess, processTaskExecutionCalibration.IdExecution);

                        _dbProcessesFramework.ProcessTaskExecutions_DeleteExecution(processTaskExecutionCalibration.ProcessTask.IdProcess, processTaskExecutionCalibration.IdExecution);
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

            #region Process Task Execution Measurements
                ////espontaneo
                internal Entities.ProcessTaskExecutionMeasurement AddExecution(Entities.ProcessTaskMeasurement processTask, DS.Entities.Post post, DateTime date, String comment, Byte[] attachment, Double measureValue, DateTime measureDate, PA.Entities.MeasurementDevice measuremenDevice, PA.Entities.MeasurementUnit measurementUnit,Boolean chargeNotice)
                {
                    //este se usa para el caso de crear execution y graba la medicion al mismo tiempo
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        PF.Entities.ProcessTaskMeasurement _processTaskMeasurement = (PF.Entities.ProcessTaskMeasurement)new PF.Collections.ProcessTasks(_Credential).Item(processTask.IdProcess);
                        //PA.Collections.ParameterRanges _parameterRange = new Condesus.EMS.Business.PA.Collections.ParameterRanges(_Credential);

                        //PA.Entities.Parameter _parameter = _parameterRange.ValidateValueRange(_processTaskMeasurement.Measurement.ParameterGroup, measureValue);
                        //if (_parameter != null)
                        //{
                            DateTime timeStamp = DateTime.Now;
                            Int64 _idExecution;

                            DateTime _startDate = DateTime.Now;
                            DateTime _endDate = DateTime.Now;
                            CalculateStartEndDate(_processTaskMeasurement.Measurement.TimeUnitFrequency, _processTaskMeasurement.Measurement.Frequency, _processTaskMeasurement.Measurement.IsRegressive, measureDate, ref _startDate, ref _endDate);

                            Entities.ProcessTaskExecutionMeasurement _processTaskExecutionMeasurement;
                            //Si no hay equipo de medicion, entonces se debe pasar un cero
                            if (measuremenDevice != null)
                            {
                                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                                _idExecution = _dbProcessesFramework.ProcessTaskExecutions_CreateExecution(processTask.IdProcess, post.JobTitle.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, date, comment, attachment, measureValue, measureDate, ref timeStamp, measuremenDevice.IdMeasurementDevice, measurementUnit.IdMeasurementUnit, _startDate, _endDate, _Credential.User.Person.IdPerson);
                                //Devuelvo el objeto creado
                                _processTaskExecutionMeasurement = new Entities.ProcessTaskExecutionMeasurement(_idExecution, processTask.IdProcess, post.Person.IdPerson, post.Organization.IdOrganization, post.JobTitle.IdGeographicArea, post.JobTitle.IdFunctionalArea, post.JobTitle.IdPosition, date, comment, attachment, measureValue, measureDate, timeStamp, measuremenDevice.IdMeasurementDevice, measurementUnit.IdMeasurementUnit, _startDate, _endDate, false, _Credential);
                            }
                            else
                            {
                                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                                _idExecution = _dbProcessesFramework.ProcessTaskExecutions_CreateExecution(processTask.IdProcess, post.JobTitle.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, date, comment, attachment, measureValue, measureDate, ref timeStamp, 0, measurementUnit.IdMeasurementUnit, _startDate, _endDate, _Credential.User.Person.IdPerson);
                                //Devuelvo el objeto creado
                                _processTaskExecutionMeasurement = new Entities.ProcessTaskExecutionMeasurement(_idExecution, processTask.IdProcess, post.Person.IdPerson, post.Organization.IdOrganization, post.JobTitle.IdGeographicArea, post.JobTitle.IdFunctionalArea, post.JobTitle.IdPosition, date, comment, attachment, measureValue, measureDate, timeStamp, 0, measurementUnit.IdMeasurementUnit, _startDate, _endDate, false, _Credential);
                            }

                            //evalua los multiples parametros
                            PA.Collections.ParameterRanges _parameterRange = new Condesus.EMS.Business.PA.Collections.ParameterRanges(_Credential);
                            foreach (PA.Entities.ParameterGroup _parameterGroup in _processTaskMeasurement.Measurement.ParameterGroups.Values)
                            {
                                PA.Entities.Parameter _parameter = _parameterRange.ValidateValueRange(_parameterGroup, measureValue);

                                if (_parameter != null)
                                {
                                    if (_parameter.RaiseException) // ver si se genera un objeto que resuelva la operacion
                                    { 
                                        new IA.Collections.Exceptions(_Credential).Add(_processTaskMeasurement.IdProcess, _processTaskExecutionMeasurement.IdExecution, Common.Constants.ExceptionTypeMeasurementOutofRange, _parameterGroup.LanguageOption.Name + "/" + _parameter.LanguageOption.Description + ": " + measureValue.ToString() + " - " + measureDate.ToString());
                                        _dbProcessesFramework.ProcessTaskMeasurements_Update(_processTaskMeasurement.IdProcess, true);
                                    }
                                }
                            }
                            //si quieren mandar email por la carga de datos
                            if (chargeNotice)
                            {
                                List<String> _To = new List<string>();
                                foreach (NT.Entities.NotificationRecipient _notifRecipient in _processTaskMeasurement.NotificationRecipient)
                                {
                                    _To.Add(_notifRecipient.Email);
                                }
                                String _subject = "Task: " + _processTaskMeasurement.LanguageOption.Title + " Date: " + DateTime.Today.ToString() + " value: " + measureValue.ToString() + "Date: " + measureDate.ToString();
                                String _body = Common.Resources.ConstantMessage.Subject_NotificationMessageChargeNotice;
                                new NT.Entities.Email().PrepareMail(_To, _subject, _body);
                            }


                        //    if (_parameter.RaiseException) // ver si se genera un objeto que resuelva la operacion
                        //    { new IA.Collections.Exceptions(_Credential).Add(processTask.IdProcess, _idExecution, Common.Constants.ExceptionTypeMeasurementOutofRange, measureValue + " - " + measureDate); }
                            return _processTaskExecutionMeasurement;
                        //}
                        //else
                        //{
                        //    throw new Exception(Common.Resources.Errors.IncorrectParameterRange);
                        //}
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
                //manual
                //internal void AddExecution(Entities.ProcessTaskMeasurement processTaskMeasurement, DS.Entities.Post post, Entities.ProcessTaskExecution processTaskExecution, String comment, Byte[] attachment, Double measureValue, DateTime measureDate, PA.Entities.MeasurementDevice measurementDevice, PA.Entities.MeasurementUnit measurementUnit, Boolean skipValidation, Boolean chargeNotice)
                //{
                //    //Solo inserta en la ejecucion de medicion, la ejecucion madre ya existe.
                //    //este add xcecution se usa para mediciones y datarecovery sin archivo, solo con valor desde paguina
                //    try
                //    {
                //        //Objeto de data layer para acceder a datos
                //        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //        Int64 _TimeUnitFrequency;
                //        Int32 _Frequency;
                //        Boolean _IsRegressive;

                //        //creo las vairables aca para poder usarlas en todos lados
                //        DateTime timeStamp = DateTime.Now;
                //        DateTime _startDate = DateTime.Now;
                //        DateTime _endDate = DateTime.Now;

                //        _TimeUnitFrequency = processTaskMeasurement.Measurement.TimeUnitFrequency;
                //        _Frequency = processTaskMeasurement.Measurement.Frequency;
                //        _IsRegressive = processTaskMeasurement.Measurement.IsRegressive;
                //        //realiza la validacion de la fecha de medicion nueva.
                //        if (ValidateMeasurementDateExecution(processTaskMeasurement.IdProcess, processTaskExecution.IdExecution, measureDate, skipValidation))
                //        {
                //            CalculateStartEndDate(_TimeUnitFrequency, _Frequency, _IsRegressive, measureDate, ref _startDate, ref _endDate);
                //        }
                //        else
                //        {
                //            throw new Exception(Common.Resources.Errors.IncorrectMeasurementDate);
                //        }



                //        Int64 _idMeasurementDevice = measurementDevice == null ? 0 : measurementDevice.IdMeasurementDevice;

                //        using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                //        {
                //            //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                //            Int64 _IdExecutionMeasurement = _dbProcessesFramework.ProcessTaskExecutions_CreateExecution(processTaskMeasurement.IdProcess, processTaskExecution.IdExecution, post.JobTitle.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, comment, attachment, measureValue, measureDate, ref timeStamp, _idMeasurementDevice, measurementUnit.IdMeasurementUnit, _startDate, _endDate, _Credential.User.Person.IdPerson);

                //            //evalua los multiples parametros
                //            PA.Collections.ParameterRanges _parameterRange = new Condesus.EMS.Business.PA.Collections.ParameterRanges(_Credential);
                //            foreach (PA.Entities.ParameterGroup _parameterGroup in processTaskMeasurement.Measurement.ParameterGroups.Values)
                //            {
                //                PA.Entities.Parameter _parameter = _parameterRange.ValidateValueRange(_parameterGroup, measureValue);

                //                if (_parameter != null)
                //                {
                //                    if (_parameter.RaiseException) // ver si se genera un objeto que resuelva la operacion
                //                    { 
                //                        new IA.Collections.Exceptions(_Credential).Add(processTaskMeasurement.IdProcess, processTaskExecution.IdExecution, measureValue, measureDate, Common.Constants.ExceptionTypeMeasurementOutofRange, _parameterGroup.LanguageOption.Name + "/" + _parameter.LanguageOption.Description + ": " + measureValue.ToString() + " - " + measureDate.ToString());
                //                        _dbProcessesFramework.ProcessTaskMeasurements_Update(processTaskMeasurement.IdProcess, true);
                //                    }
                //                }
                //            }
                //            Double _minuteValue = 0;

                //            if (processTaskMeasurement.Measurement.Indicator.IsCumulative)
                //            {
                //                //saca la diferencia de horas
                //                TimeSpan _difTime = _endDate.Subtract(_startDate);
                //                //lo pasa a minutos
                //                Double _minutes = Convert.ToDouble(_difTime.TotalMinutes);
                //                //saca el valor del minuto
                //                _minuteValue = measureValue / _minutes;
                //            }
                //            else
                //            {
                //                _minuteValue = measureValue;
                //            }
                //            _dbProcessesFramework.ProcessTaskExecutions_CreateExecutionForCalculate(processTaskMeasurement.IdProcess, processTaskExecution.IdExecution, measureValue, measureDate, measurementUnit.IdMeasurementUnit, _idMeasurementDevice, _startDate, _endDate, _minuteValue);

                //            _transactionScope.Complete();
                //        }

                //        //si quieren mandar email por la carga de datos
                //        if (chargeNotice)
                //        {
                //            List<String> _To = new List<string>();
                //            foreach(NT.Entities.NotificationRecipient _notifRecipient in processTaskMeasurement.NotificationRecipient)
                //            {
                //                _To.Add(_notifRecipient.Email);
                //            }
                //            String _subject = "Task: " + processTaskMeasurement.LanguageOption.Title + " Date: " + DateTime.Today.ToString() + " value: " + measureValue.ToString() + "Date: " + measureDate.ToString();
                //            String _body= Common.Resources.ConstantMessage.Subject_NotificationMessageChargeNotice;
                //            new NT.Entities.Email().PrepareMail(_To, _subject, _body);
                //        }

                //    }
                //    catch (SqlException ex)
                //    {
                //        if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                //        {
                //            throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                //        }
                //        throw ex;
                //    }

                //}

                //con start y end date

                internal void AddExecutionTVP(Entities.ProcessTaskMeasurement processTaskMeasurement, DS.Entities.Post post,
                   String comment, Byte[] attachment, DataTable dtTVPValues,PA.Entities.MeasurementDevice measurementDevice, 
                    PA.Entities.MeasurementUnit measurementUnit, Boolean skipValidation,Boolean chargeNotice)
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    //crea la ejecucion
                    Int64 _IdExecution = _dbProcessesFramework.ProcessTaskExecutions_CreateExecution(processTaskMeasurement.IdProcess, 
                        post.JobTitle.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, 
                        post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, 
                        DateTime.Now, comment, false, attachment, _Credential.User.Person.IdPerson);
                    //Devuelvo el objeto creado
                    Entities.ProcessTaskExecution _processTaskExecution = new Entities.ProcessTaskExecution(_IdExecution, 
                        processTaskMeasurement.IdProcess, post.Person.IdPerson, post.Organization.IdOrganization, post.JobTitle.IdGeographicArea, 
                        post.JobTitle.IdFunctionalArea, post.JobTitle.IdPosition, DateTime.Now, comment, attachment, chargeNotice, _Credential);
                            
                    AddExecutionTVP(processTaskMeasurement, post, _processTaskExecution, comment, attachment, dtTVPValues, measurementDevice, 
                        measurementUnit, skipValidation, chargeNotice);
                }
                internal void AddExecutionTVP(Entities.ProcessTaskMeasurement processTaskMeasurement, DS.Entities.Post post, 
                    Entities.ProcessTaskExecution processTaskExecution, String comment, Byte[] attachment, DataTable dtTVPValues, 
                    PA.Entities.MeasurementDevice measurementDevice, PA.Entities.MeasurementUnit measurementUnit, Boolean skipValidation, 
                    Boolean chargeNotice)
                {
                    //Solo inserta en la ejecucion de medicion, la ejecucion madre ya existe.
                    //este add xcecution se usa para mediciones y datarecovery sin archivo, solo con valor desde paguina
                    try
                    {
                      
                        Int64 _idMeasurementDevice = measurementDevice == null ? 0 : measurementDevice.IdMeasurementDevice;
                        
                        //realiza la validacion de la fecha de medicion nueva.
                        ValidateMeasurementDateExecutionTVP(processTaskMeasurement, processTaskExecution.IdExecution, skipValidation, dtTVPValues);

                        using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                        {
                            //Objeto de data layer para acceder a datos
                            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                            //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                            Int64 _IdExecutionMeasurement = _dbProcessesFramework.ProcessTaskExecutions_CreateExecutionTVP(
                                processTaskMeasurement.IdProcess, 
                                processTaskExecution.IdExecution, 
                                post.JobTitle.Organization.IdOrganization, 
                                post.Person.IdPerson, 
                                post.JobTitle.FunctionalPositions.Position.IdPosition, 
                                post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, 
                                post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, 
                                comment, 
                                attachment, 
                                dtTVPValues,
                                _idMeasurementDevice, 
                                measurementUnit.IdMeasurementUnit, 
                                _Credential.User.Person.IdPerson);

                            //Pasa al sp el parametro para calcular el minutevalue
                            Boolean _isCumulative = processTaskMeasurement.Measurement.Indicator.IsCumulative;
                            //Graba en la tabla For Calculate
                            _dbProcessesFramework.ProcessTaskExecutions_CreateExecutionTVPForCalculate(
                                processTaskMeasurement.IdProcess, 
                                dtTVPValues,
                                _idMeasurementDevice, 
                                measurementUnit.IdMeasurementUnit,
                                _isCumulative);

                            //Avalua los parametros
                            EvaluateParameteersTVP(processTaskMeasurement, processTaskExecution, dtTVPValues);    
                       
                            _transactionScope.Complete();
                        }

                        //si quieren mandar email por la carga de datos
                        if (chargeNotice)
                        {
                            ReportresultsTVP(processTaskMeasurement, dtTVPValues);
                        }

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
                internal Boolean ValidateMeasurementDateExecutionTVP(Entities.ProcessTaskMeasurement processTaskMeasurement, Int64 idExecution, Boolean skipValidation, DataTable dtTVPValues)
                {
                    //Esta rutina ahora se publica en la fachada para que se puede validar el archivo previamente a grabar los datos
                    //para que el usuario pueda definir si de todas maneras inserta con huecos.

                    //Si se indica que se debe saltear la validacion, entonces retorna TRUE directamente, sino sigue con todo lo que debe hacer la rutina.
                    if (skipValidation)
                    {
                        //Como no hay que validar, los huecos, tengo que obtener los valores del archivo para meterlos en el DataTable.                       
                        return true;
                    }

                    //Primero obtiene los valores de la ultima ejecucion
                    PF.Entities.ProcessTaskExecution _processTaskExecution = LastExecution(processTaskMeasurement.IdProcess, idExecution);
                    //Busca la tarea de medicion para luego obtener la frecuencia de la medicion.

                    Int32 _frequency = processTaskMeasurement.Measurement.Frequency;
                    Int64 _timeUnitFrequency = processTaskMeasurement.Measurement.TimeUnitFrequency;

                    DateTime? _lastMeasurementDate = null;

                    //Si aun no hay ninguna ejecucion para esta tarea, va tomar como valida el primer registro del archivo.
                    if (_processTaskExecution.GetType().Name == "ProcessTaskExecutionMeasurement")
                    {
                        //ya existen ejecuciones para esta tarea, entonces busca la ultima ejecucion y la frequencia
                        PF.Entities.ProcessTaskExecutionMeasurement _processTaskExecutionMeasurementLast = (PF.Entities.ProcessTaskExecutionMeasurement)_processTaskExecution;
                        //Obtiene la fecha de la ultima medicion tomada
                        _lastMeasurementDate = _processTaskExecutionMeasurementLast.MeasureDate;
                    }

                    //Obtiene la frecuencia y la unidad de tiempo utilizada en la medicion.
                    DateTime _nextMeasurementDate;

                    //Recorre el DT y obtiene cada medicion
                    Double _value = 0;
                    DateTime _date = DateTime.Now;
                    DateTime _startDate = DateTime.Now;
                    DateTime _endDate = DateTime.Now;

                    foreach (DataRow _row in dtTVPValues.Rows)
                    {
                        _value = Convert.ToDouble(_row["MeasurementValue"]);
                        _date = Convert.ToDateTime(_row["MeasurementDate"]);
                        _startDate = Convert.ToDateTime(_row["StartDate"]);
                        _endDate = Convert.ToDateTime(_row["EndDate"]);
                                                             
                        //Si la ultima fecha de medicion viene en null, quiere decir que es la primera carga para esta tarea, 
                        //entonces tomo el primer valor del archivo.
                        if (_lastMeasurementDate == null)
                        {
                            _lastMeasurementDate = _date;
                        }
                        else
                        {
                            //Calcula la proxima fecha de medicion.
                            _nextMeasurementDate = CalculateNextMeasurementDate(_timeUnitFrequency, _frequency, Convert.ToDateTime(_lastMeasurementDate));

                            if (_nextMeasurementDate != _date)
                            {
                                //return false; 
                                throw new Exception(Common.Resources.Errors.IncorrectMeasurementDate + " - " + _date + ";" + _value);
                            }
                            //como la fecha del archivo esta bien, ahora la guarda como proxima ejecucion, y sigue validando el proximo registro...
                            _lastMeasurementDate = _nextMeasurementDate;
                        }

                    }

                    return true;
                }
                //Notifica los resultados
                private void ReportresultsTVP(Entities.ProcessTaskMeasurement processTaskMeasurement, DataTable dtTVPValues)
                {
                    Double _value = 0;
                    DateTime _date = DateTime.Now;
                    DateTime _startDate = DateTime.Now;
                    DateTime _endDate = DateTime.Now;

                    foreach (DataRow _row in dtTVPValues.Rows)
                    {
                        _value = Convert.ToDouble(_row["MeasurementValue"]);
                        _date = Convert.ToDateTime(_row["MeasurementDate"]);
                        _startDate = Convert.ToDateTime(_row["StartDate"]);
                        _endDate = Convert.ToDateTime(_row["EndDate"]);

                        List<String> _To = new List<string>();
                        foreach (NT.Entities.NotificationRecipient _notifRecipient in processTaskMeasurement.NotificationRecipient)
                        {
                            _To.Add(_notifRecipient.Email);
                        }
                        String _subject = "Task: " + processTaskMeasurement.LanguageOption.Title + " Date: " + DateTime.Today.ToString() + " value: " + _value.ToString() + "Date: " + _date.ToString();
                        String _body = Common.Resources.ConstantMessage.Subject_NotificationMessageChargeNotice;
                        new NT.Entities.Email().PrepareMail(_To, _subject, _body);
                    }
                }
                //evalua los parametros
                private void EvaluateParameteersTVP(Entities.ProcessTaskMeasurement processTaskMeasurement, 
                    Entities.ProcessTaskExecution processTaskExecution, DataTable dtTVPValues)
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    
                    Double _value = 0;
                    DateTime _date = DateTime.Now;
                    DateTime _startDate = DateTime.Now;
                    DateTime _endDate = DateTime.Now;

                    foreach (DataRow _row in dtTVPValues.Rows)
                    {
                        _value = Convert.ToDouble(_row["MeasurementValue"]);
                        _date = Convert.ToDateTime(_row["MeasurementDate"]);
                        _startDate = Convert.ToDateTime(_row["StartDate"]);
                        _endDate = Convert.ToDateTime(_row["EndDate"]);

                        //evalua los multiples parametros
                        PA.Collections.ParameterRanges _parameterRange = new Condesus.EMS.Business.PA.Collections.ParameterRanges(_Credential);
                        foreach (PA.Entities.ParameterGroup _parameterGroup in processTaskMeasurement.Measurement.ParameterGroups.Values)
                        {
                            PA.Entities.Parameter _parameter = _parameterRange.ValidateValueRange(_parameterGroup, _value);

                            if (_parameter != null)
                            {
                                if (_parameter.RaiseException) // ver si se genera un objeto que resuelva la operacion
                                {
                                    new IA.Collections.Exceptions(_Credential).Add(processTaskMeasurement.IdProcess,
                                        processTaskExecution.IdExecution, _value, _date, Common.Constants.ExceptionTypeMeasurementOutofRange,
                                        _parameterGroup.LanguageOption.Name + "/" + _parameter.LanguageOption.Description + ": " + _value.ToString()
                                        + " - " + _date.ToString());

                                    _dbProcessesFramework.ProcessTaskMeasurements_Update(processTaskMeasurement.IdProcess, true);
                                }
                            }
                        }
                    }
                }




                //con archivo
                internal void AddExecution(Entities.ProcessTaskMeasurement processTaskMeasurement, DS.Entities.Post post, Entities.ProcessTaskExecution processTaskExecution, String fileName, String fileStream, Byte[] fileStreamBinary, String comment, Byte[] attachment, PA.Entities.MeasurementDevice measuremenDevice, PA.Entities.MeasurementUnit measurementUnit, Boolean skipValidation, Boolean chargeNotice)
                {
                    //Solo inserta en la ejecucion de medicion, la ejecucion madre ya existe. medicion con file
                     try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        //Construye la tabla de parametros
                        DataTable _dtTVPMeasurements = BuildDataTableValueParamMeasurements();
                        
                        //Valida todas las fechas que estan en el archivo, si no coinciden con la frecuencia, tira la excepcion y no graba nada.
                        if (ValidateMeasurementDateExecution(processTaskMeasurement, processTaskExecution.IdExecution, fileStream, skipValidation, ref _dtTVPMeasurements))
                        {

                            //DateTime timeStamp = DateTime.Now;
                            //DateTime measureDate = DateTime.Now;

                            //Si no hay equipo de medicion, entonces se debe pasar un cero
                            Int64 _idMeasurementDevice = measuremenDevice == null ? 0 : measuremenDevice.IdMeasurementDevice;
                            
                            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                            {
                                
                                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                                Int64 _IdExecutionMeasurement = _dbProcessesFramework.ProcessTaskExecutions_CreateExecution(processTaskMeasurement.IdProcess, processTaskExecution.IdExecution, post.JobTitle.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, comment, attachment, _dtTVPMeasurements, _idMeasurementDevice, measurementUnit.IdMeasurementUnit, _Credential.User.Person.IdPerson);

                                if (processTaskMeasurement.Measurement.Indicator.IsCumulative)
                                {
                                    //graba en la tabla donde consume calculos
                                    _dbProcessesFramework.ProcessTaskExecutions_CreateExecutionForCalculateCumulative(processTaskMeasurement.IdProcess, _dtTVPMeasurements, _idMeasurementDevice, measurementUnit.IdMeasurementUnit);
                                }
                                else
                                {
                                    _dbProcessesFramework.ProcessTaskExecutions_CreateExecutionForCalculateNotCumulative(processTaskMeasurement.IdProcess, _dtTVPMeasurements, _idMeasurementDevice, measurementUnit.IdMeasurementUnit);
                                }
                                //La generacion de las excepciones se hacen afuera de la transaccion.
                                RaiseExceptionsForFiles(processTaskMeasurement, processTaskExecution.IdExecution, _IdExecutionMeasurement, fileStream);

                                //Por ultimo guardo el archivo en el fileattach asociado al process.
                                _dbProcessesFramework.ProcessTaskExecutions_CreateExecutionFileAttach(processTaskMeasurement.IdProcess, processTaskExecution.IdExecution, fileName, fileStreamBinary, _Credential.User.Person.IdPerson);

                                _transactionScope.Complete();
                            }

                            //si quieren mandar email por la carga de datos
                            if (chargeNotice)
                            {
                                List<String> _To = new List<string>();
                                foreach (NT.Entities.NotificationRecipient _notifRecipient in processTaskMeasurement.NotificationRecipient)
                                {
                                    _To.Add(_notifRecipient.Email);
                                }
                                String _subject = "Task: " + processTaskMeasurement.LanguageOption.Title + " Date: " + DateTime.Today.ToString() + " values: " + fileStream;
                                String _body = Common.Resources.ConstantMessage.Subject_NotificationMessageChargeNotice;
                                new NT.Entities.Email().PrepareMail(_To, _subject, _body);
                            }
                        }

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
                //con excell
                internal void AddExecution(Entities.ProcessTaskMeasurement processTaskMeasurement, DS.Entities.Post post, String fileName, String fileStream, Byte[] fileStreamBinary, String comment, Byte[] attachment, PA.Entities.MeasurementUnit measurementUnit, Boolean skipValidation, Boolean chargeNotice)
                {
                    //crea la ejecucion madre e inserta en la ejecucion de medicion
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        DateTime _timeStamp = DateTime.Now;

                        //crea la ejecucion
                        Int64 _IdExecution = _dbProcessesFramework.ProcessTaskExecutions_CreateExecution(processTaskMeasurement.IdProcess, post.JobTitle.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, _timeStamp, comment, false, attachment, _Credential.User.Person.IdPerson);

                        //Construye la tabla de parametros
                        DataTable _dtTVPMeasurements = BuildDataTableValueParamMeasurements();

                        //Valida todas las fechas que estan en el archivo, si no coinciden con la frecuencia, tira la excepcion y no graba nada.
                        if (ValidateMeasurementDateExecution(processTaskMeasurement, _IdExecution, fileStream, skipValidation, ref _dtTVPMeasurements))
                        {

                            //Si no hay equipo de medicion, entonces se debe pasar un cero
                            Int64 _idMeasurementDevice = 0;

                            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                            {
                                
                                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                                Int64 _IdExecutionMeasurement = _dbProcessesFramework.ProcessTaskExecutions_CreateExecution(processTaskMeasurement.IdProcess, _IdExecution, post.JobTitle.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, comment, attachment, _dtTVPMeasurements, _idMeasurementDevice, measurementUnit.IdMeasurementUnit, _Credential.User.Person.IdPerson);

                                if (processTaskMeasurement.Measurement.Indicator.IsCumulative)
                                {
                                    //graba en la tabla donde consume calculos
                                    _dbProcessesFramework.ProcessTaskExecutions_CreateExecutionForCalculateCumulative(processTaskMeasurement.IdProcess, _dtTVPMeasurements, _idMeasurementDevice, measurementUnit.IdMeasurementUnit);
                                }
                                else
                                {
                                    _dbProcessesFramework.ProcessTaskExecutions_CreateExecutionForCalculateNotCumulative(processTaskMeasurement.IdProcess, _dtTVPMeasurements, _idMeasurementDevice, measurementUnit.IdMeasurementUnit);
                                }
                                //La generacion de las excepciones se hacen afuera de la transaccion.
                                RaiseExceptionsForFiles(processTaskMeasurement, _IdExecution, _IdExecutionMeasurement, fileStream);

                                //Por ultimo guardo el archivo en el fileattach asociado al process.
                                _dbProcessesFramework.ProcessTaskExecutions_CreateExecutionFileAttach(processTaskMeasurement.IdProcess, _IdExecution, fileName, fileStreamBinary, _Credential.User.Person.IdPerson);

                                _transactionScope.Complete();
                            }
                            //si quieren mandar email por la carga de datos
                            if (chargeNotice)
                            {
                                List<String> _To = new List<string>();
                                foreach (NT.Entities.NotificationRecipient _notifRecipient in processTaskMeasurement.NotificationRecipient)
                                {
                                    _To.Add(_notifRecipient.Email);
                                }
                                String _subject = "Task: " + processTaskMeasurement.LanguageOption.Title + " Date: " + DateTime.Today.ToString() + " values: " + fileStream;
                                String _body = Common.Resources.ConstantMessage.Subject_NotificationMessageChargeNotice;
                                new NT.Entities.Email().PrepareMail(_To, _subject, _body);
                            }
                        }

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



             
                internal void RemoveExecution(Entities.ProcessTaskExecutionMeasurement processTaskExecutionMeasurement)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        processTaskExecutionMeasurement.Remove();
                        //Borrar de la base de datos
                        _dbProcessesFramework.ProcessTaskExecutions_DeleteExecutionForCalculate(processTaskExecutionMeasurement.ProcessTask.IdProcess);

                        _dbProcessesFramework.ProcessTaskExecutions_DeleteExecutionMeasurement(processTaskExecutionMeasurement.ProcessTask.IdProcess, processTaskExecutionMeasurement.IdExecution);

                        _dbProcessesFramework.ProcessTaskExecutions_DeleteExecution(processTaskExecutionMeasurement.ProcessTask.IdProcess, processTaskExecutionMeasurement.IdExecution);
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

            #region Process Task Execution General
                internal Entities.ProcessTaskExecution AddExecution(Entities.ProcessTask processTask, DS.Entities.Post post, DateTime date, String comment, Byte[] attachment, Boolean result)
                {
                     try
                    {
                        if (!ValidateNumberExecutions(processTask.IdProcess))
                        {
                            throw new Exception(Common.Resources.Errors.MaxNumberExecutionValidator);
                        }

                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        result = ValidateResult(processTask.IdProcess, result);
                        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idExecution = _dbProcessesFramework.ProcessTaskExecutions_CreateExecution(processTask.IdProcess, post.JobTitle.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition, post.JobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, date, comment, result, attachment, _Credential.User.Person.IdPerson);
                        //Devuelvo el objeto creado
                        return new Entities.ProcessTaskExecution(_idExecution, processTask.IdProcess, post.Person.IdPerson, post.Organization.IdOrganization, post.JobTitle.IdGeographicArea, post.JobTitle.IdFunctionalArea, post.JobTitle.IdPosition, date, comment, attachment, false, _Credential);
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
                internal void RemoveExecution(Entities.ProcessTaskExecution processTaskExecution)
                {
                       try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        processTaskExecution.Remove();
                        //Borrar de la base de datos
                        _dbProcessesFramework.ProcessTaskExecutions_DeleteExecution(processTaskExecution.ProcessTask.IdProcess, processTaskExecution.IdExecution);
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
                internal void ResetExecution(Entities.ProcessTaskExecution processTaskExecution)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                        
                        if (processTaskExecution.GetType().Name == "ProcessTaskExecutionMeasurement")
                        {
                            Entities.ProcessTaskExecutionMeasurement _processTaskExecutionMeasurement = (Entities.ProcessTaskExecutionMeasurement)processTaskExecution;

                            PF.Entities.ProcessTaskMeasurement _processTaskMeasurement = (PF.Entities.ProcessTaskMeasurement)_processTaskExecutionMeasurement.ProcessTask;

                             //borra la transformaciones donde participa
                            foreach (PA.Entities.CalculateOfTransformation _calculate in _processTaskMeasurement.Measurement.Transformations.Values)
                            {
                                new PA.Collections.CalculateOfTransformationResults(_calculate).Remove(_calculate);
                            }
                            foreach (PA.Entities.CalculateOfTransformation _calculate in new PA.Collections.CalculateOfTransformations(_Credential).ItemsAsParameter(_processTaskMeasurement.Measurement).Values)
                            {
                                new PA.Collections.CalculateOfTransformationResults(_calculate).Remove(_calculate);
                            }
                             
                        }

                        //resetea la ejecucion
                        _dbProcessesFramework.ProcessTaskExecutions_ResetExecution(processTaskExecution.ProcessTask.IdProcess, processTaskExecution.IdExecution);
                        //borra el file
                        _dbProcessesFramework.ProcessTaskExecutions_DeleteExecutionFileAttach(processTaskExecution.ProcessTask.IdProcess, processTaskExecution.IdExecution);

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
        #endregion
              

        private DataTable BuildDataTableValueParamMeasurements()
                {
                    DataTable _dtTVPMeasurements = new DataTable();
                    _dtTVPMeasurements.TableName = "tvp";
                    DataColumn _column = new DataColumn();
                    _column.DataType = System.Type.GetType("System.DateTime");
                    _column.Caption = "MeasurementDate";
                    //Inserta la columna definida en el DAtaTable.
                    _dtTVPMeasurements.Columns.Add(_column);

                    _column = new DataColumn();
                    _column.DataType = System.Type.GetType("System.Decimal");
                    _column.Caption = "MeasurementValue";
                    //Inserta la columna definida en el DAtaTable.
                    _dtTVPMeasurements.Columns.Add(_column);

                    _column = new DataColumn();
                    _column.DataType = System.Type.GetType("System.DateTime");
                    _column.Caption = "StartDate";
                    //Inserta la columna definida en el DAtaTable.
                    _dtTVPMeasurements.Columns.Add(_column);

                    _column = new DataColumn();
                    _column.DataType = System.Type.GetType("System.DateTime");
                    _column.Caption = "EndDate";
                    //Inserta la columna definida en el DAtaTable.
                    _dtTVPMeasurements.Columns.Add(_column);

                    return _dtTVPMeasurements;
                }
        private Boolean ValidateResult(Int64 idProcess, Boolean result)
        {
            //Este metodo se encarga de validar el Result 
            //al ser una tarea operativa si es de tipo Espontanea debe poner automaticamente TRUE
            //En caso contrario, si es Programada o Repetitiva y esta repeticion sea la ultima, debe tomar la que viene por FrontEnd
            PF.Collections.ProcessTasks _processTasks = new ProcessTasks(_Credential);
            String _typeExecution = _processTasks.Item(idProcess).TypeExecution;

            if (_typeExecution == "Spontaneous")
            {
                return true;
            }
            return result;
        }
        private Boolean ValidateNumberExecutions(Int64 idProcess)
        {
            PF.Collections.ProcessTasks _processTasks = new ProcessTasks(_Credential);
            Int64 _maxExecution = _processTasks.Item(idProcess).MaxNumberExecution;
            //Si el valor de cantidad de ejecuciones para la tarea es cero (0), quiere decir que es infinito, retorna true. y evita preguntar cuantos hay grabados actualmente.
            if (_maxExecution == 0)
                { return true; }

            //Ahora busca cuantas ejecuciones hay grabadas para esta tarea.
            Int64 _countExecutionReal = Items(idProcess).Count;
            //Si la cantidad maxima seteada es mayor a las que ya existen, deja que graben la proxima
            if (_maxExecution > _countExecutionReal)
            { return true; }

            //como no coincide con ninguna de las anteriores, retorna false y no deja grabar.
            return false;
        }
        private Boolean ValidateMeasurementDateExecution(Int64 idProcess, Int64 idExecution, DateTime newMeasurementDate, Boolean skipValidation)
        {
            //Si se indica que se debe saltear la validacion, entonces retorna TRUE directamente, sino sigue con todo lo que debe hacer la rutina.
            if (skipValidation) { return true; }


            PF.Entities.ProcessTaskExecution _processTaskExecution = LastExecution(idProcess, idExecution);

            //Si aun no hay ninguna ejecucion para esta tarea, retorna true y no valida nada...
            if (_processTaskExecution.GetType().Name == "ProcessTaskExecution")
                { return true; }

            PF.Entities.ProcessTaskExecutionMeasurement _processTaskExecutionMeasurementLast = (PF.Entities.ProcessTaskExecutionMeasurement)_processTaskExecution;
            PF.Entities.ProcessTask _processTask = _processTaskExecutionMeasurementLast.ProcessTask;

            DateTime _nextMeasurementDate = DateTime.MinValue;
            //Obtiene la fecha de la ultima medicion tomada
            DateTime _lastMeasurementDate = _processTaskExecutionMeasurementLast.MeasureDate;
            //Obtiene la frecuencia y la unidad de tiempo utilizada
            Int32 _frequency = ((PF.Entities.ProcessTaskMeasurement)_processTask).Measurement.Frequency;
            Int64 _timeUnitFrequency = ((PF.Entities.ProcessTaskMeasurement)_processTask).Measurement.TimeUnitFrequency;

            //Obtiene la proxima fecha de medicion
            _nextMeasurementDate = CalculateNextMeasurementDate(_timeUnitFrequency, _frequency, _lastMeasurementDate);

            //si la fecha de medicion ingresada es igual a la calculada en base a la duracion, retorna ok
            if (_nextMeasurementDate == newMeasurementDate)
            {
                return true;
            }
            //en caso contrario es falso y el que la llamo, dispara la excepcion.
            throw new Exception(Common.Resources.Errors.IncorrectMeasurementDate);
        }

        private void RaiseExceptionsForFiles(Entities.ProcessTaskDataRecovery processTaskDataRecovery, Int64 idExecution, String fileStream)
        {
            //Recorre el archivo y obtiene cada medicion
            String[] _separatorRow = new String[] { "\r\n" };
            String[] _records = fileStream.Split(_separatorRow, StringSplitOptions.RemoveEmptyEntries);

            PA.Collections.ParameterRanges _parameterRange = new Condesus.EMS.Business.PA.Collections.ParameterRanges(_Credential);
           

            foreach (PA.Entities.ParameterGroup _parameterGroup in processTaskDataRecovery.ProcessTaskMeasurementToRecovery.Measurement.ParameterGroups.Values)
            {
                for (int i = 0; i < _records.LongLength; i++)
                {
                    //Obtiene los datos del registro en donde esta parado.
                    DateTime _measureDate = Convert.ToDateTime(_records[i].Split(';')[0]);
                    Double _measureValue = Convert.ToDouble(_records[i].Split(';')[1]);

                    PA.Entities.Parameter _parameter = _parameterRange.ValidateValueRange(_parameterGroup, _measureValue);

                    if (_parameter.RaiseException) // ver si se genera un objeto que resuelva la operacion
                    { new IA.Collections.Exceptions(_Credential).Add(processTaskDataRecovery.IdProcess, idExecution, Common.Constants.ExceptionTypeMeasurementOutofRange, _measureValue + " - " + _measureDate); }

                }
            }
        }
        private void RaiseExceptionsForFiles(Entities.ProcessTaskMeasurement processTaskMeasurement, Int64 idExecution, Int64 idExecutionMeasurement, String fileStream)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //Recorre el archivo y obtiene cada medicion
                String[] _separatorRow = new String[] { "\r\n" };
                String[] _records = fileStream.Split(_separatorRow, StringSplitOptions.RemoveEmptyEntries);

                PA.Collections.ParameterRanges _parameterRange = new Condesus.EMS.Business.PA.Collections.ParameterRanges(_Credential);
                
                foreach(PA.Entities.ParameterGroup _parameterGroup in processTaskMeasurement.Measurement.ParameterGroups.Values)
                {
                    for (int i = 0; i < _records.LongLength; i++)
                    {
                        //Obtiene los datos del registro en donde esta parado.
                        DateTime _measureDate = Convert.ToDateTime(_records[i].Split(';')[0]);
                        Double _measureValue = Convert.ToDouble(_records[i].Split(';')[1]);

                        PA.Entities.Parameter _parameter = _parameterRange.ValidateValueRange(_parameterGroup, _measureValue);

                        if (_parameter.RaiseException) // ver si se genera un objeto que resuelva la operacion
                        { 
                            new IA.Collections.Exceptions(_Credential).Add(processTaskMeasurement.IdProcess, idExecution, _measureValue, _measureDate, Common.Constants.ExceptionTypeMeasurementOutofRange, _parameterGroup.LanguageOption.Name + "/" + _parameter.LanguageOption.Description + ": " + _measureValue.ToString() + " - " + _measureDate.ToString());
                            _dbProcessesFramework.ProcessTaskMeasurements_Update(processTaskMeasurement.IdProcess, true);
                        }
                    }
                } 
            }
            catch
            {
            }
        }

        private Boolean ValidateExistMeasurementDateForDataRecovery(Entities.ProcessTaskDataRecovery processTaskDataRecovery, Int64 idExecution, String fileStream, Boolean skipValidation, ref DataTable dtTVPMeasurements)
        {
            //Recorre el archivo y obtiene cada medicion
            String[] _separatorRow = new String[] { "\r\n" };
            String[] _records = fileStream.Split(_separatorRow, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < _records.LongLength; i++)
            {
                //Obtiene los datos del registro en donde esta parado.
                DateTime _measureDate = Convert.ToDateTime(_records[i].Split(';')[0]);
                Decimal? _measureValue = null;
                if (_records[i].Split(';')[1] != "")
                {
                    _measureValue = Convert.ToDecimal(_records[i].Split(';')[1]);
                }

                if (!processTaskDataRecovery.DataRecoveryMeasurementDates.Contains(_measureDate))
                { return false; }

                //Como no hay que validar, los huecos, tengo que obtener los valores del archivo para meterlos en el DataTable.
                dtTVPMeasurements.Rows.Add(_measureDate, _measureValue);
            }
            return true;
        }

        internal Boolean ValidateMeasurementDateExecution(Entities.ProcessTaskMeasurement processTaskMeasurement, Int64 idExecution, String fileStream, Boolean skipValidation, ref DataTable dtTVPMeasurements)
        {
            //Esta rutina ahora se publica en la fachada para que se puede validar el archivo previamente a grabar los datos
            //para que el usuario pueda definir si de todas maneras inserta con huecos.
            
            //Si se indica que se debe saltear la validacion, entonces retorna TRUE directamente, sino sigue con todo lo que debe hacer la rutina.
            if (skipValidation)
            {
                //Como no hay que validar, los huecos, tengo que obtener los valores del archivo para meterlos en el DataTable.
                LoadDataTableValueParamMeasurements(processTaskMeasurement, fileStream, ref dtTVPMeasurements);
                return true;
            }

            //Primero obtiene los valores de la ultima ejecucion
            PF.Entities.ProcessTaskExecution _processTaskExecution = LastExecution(processTaskMeasurement.IdProcess, idExecution);
            //Busca la tarea de medicion para luego obtener la frecuencia de la medicion.

            Int32 _frequency = processTaskMeasurement.Measurement.Frequency;
            Int64 _timeUnitFrequency = processTaskMeasurement.Measurement.TimeUnitFrequency;
            
            DateTime? _lastMeasurementDate = null;

            //Si aun no hay ninguna ejecucion para esta tarea, va tomar como valida el primer registro del archivo.
            if (_processTaskExecution.GetType().Name == "ProcessTaskExecutionMeasurement")
            {
                //ya existen ejecuciones para esta tarea, entonces busca la ultima ejecucion y la frequencia
                PF.Entities.ProcessTaskExecutionMeasurement _processTaskExecutionMeasurementLast = (PF.Entities.ProcessTaskExecutionMeasurement)_processTaskExecution;
                //Obtiene la fecha de la ultima medicion tomada
                _lastMeasurementDate = _processTaskExecutionMeasurementLast.MeasureDate;
            }

            //Obtiene la frecuencia y la unidad de tiempo utilizada en la medicion.
            DateTime _nextMeasurementDate;

            //Recorre el archivo y obtiene cada medicion
            String[] _separatorRow = new String[] { "\r\n" };
            String[] _records = fileStream.Split(_separatorRow, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < _records.LongLength; i++)
            {
                //Obtiene los datos del registro en donde esta parado.
                DateTime _measureDate = Convert.ToDateTime(_records[i].Split(';')[0]);
                Decimal? _measureValue = null;
                if (_records[i].Split(';')[1] != "")
                {
                    _measureValue = Convert.ToDecimal(_records[i].Split(';')[1]);
                }

                if (_measureDate == null)
                {
                    _measureDate = DateTime.Now;
                }
                //Si la ultima fecha de medicion viene en null, quiere decir que es la primera carga para esta tarea, 
                //entonces tomo el primer valor del archivo.
                if (_lastMeasurementDate == null)
                {
                    _lastMeasurementDate = _measureDate;
                }
                else
                {
                    //Calcula la proxima fecha de medicion.
                    _nextMeasurementDate = CalculateNextMeasurementDate(_timeUnitFrequency, _frequency, Convert.ToDateTime(_lastMeasurementDate));

                    if (_nextMeasurementDate != _measureDate)
                    {
                        //return false; 
                        throw new Exception(Common.Resources.Errors.IncorrectMeasurementDate + " - " + _measureDate + ";" + _measureValue);
                    }
                    //como la fecha del archivo esta bien, ahora la guarda como proxima ejecucion, y sigue validando el proximo registro...
                    _lastMeasurementDate = _nextMeasurementDate;
                }

                DateTime _startDate = DateTime.Now;
                DateTime _endDate = DateTime.Now;
                CalculateStartEndDate(_timeUnitFrequency, _frequency, processTaskMeasurement.Measurement.IsRegressive, _measureDate, ref _startDate, ref _endDate);
                //Agrego el registro al DataTable
                dtTVPMeasurements.Rows.Add(_measureDate, _measureValue, _startDate, _endDate);
            }
            
            return true;
        }
        private DateTime CalculateNextMeasurementDate(Int64 timeUnitFrequency, Int32 frequency, DateTime lastMeasurementDate)
        {
            DateTime _nextMeasurementDate = DateTime.MinValue;

            //ahora calcula la proxima fecha de ejecucion, con todos los datos obtenidos.
            switch (timeUnitFrequency)
            {
                case 1: //Por año
                    _nextMeasurementDate = lastMeasurementDate.AddYears(frequency);
                    break;

                case 2: //Por mes
                    _nextMeasurementDate = lastMeasurementDate.AddMonths(frequency);
                    break;

                case 3: //Por dia
                    _nextMeasurementDate = lastMeasurementDate.AddDays(frequency);
                    break;

                //case 4: //por semana no se calcula...
                //    _nextMeasurementDate = _lastMeasurementDate.AddYears(frequency);
                //    break;

                case 5: //por Hora
                    _nextMeasurementDate = lastMeasurementDate.AddHours(frequency);
                    break;

                case 6: //por minuto
                    _nextMeasurementDate = lastMeasurementDate.AddMinutes(frequency);
                    break;

                case 7: //por segundo
                    _nextMeasurementDate = lastMeasurementDate.AddSeconds(frequency);
                    break;
            }

            return _nextMeasurementDate;
        }
        private void CalculateStartEndDate(Int64 timeUnitFrequency, Int32 frequency, Boolean isRegressive, DateTime measurementDate, ref DateTime startDate, ref DateTime endDate)
        {
            //Pregunta si la medicion es Regresiva o Progresiva
            if (isRegressive)
            {
                //Si es regresivo, quiere decir que tengo que ir para atras, la fecha de medicion que pasan es la final y se calcula la inicial
                startDate = CalculateNextMeasurementDate(timeUnitFrequency, -(frequency), measurementDate);
                endDate = measurementDate;
            }
            else
            {
                //Si es progresivo, quiere decir que tengo que ir para adelante, la fecha de medicion que pasan es la inicial y se calcula la final.
                startDate = measurementDate;
                endDate = CalculateNextMeasurementDate(timeUnitFrequency, frequency, measurementDate);
            }
        }
        private Boolean ValidateExistMeasurementForDataRecovery()
        {
            return true;
        }
        /// <summary>
        /// Este metodo carga el DataTable con los datos del File de las mediciones. Por ahora solo se usa cuando no hay que hacer validaciones sobre el file. (buscar huecos por ej.)
        /// </summary>
        /// <param name="fileStream">Stream del archivo</param>
        /// <param name="dtTVPMeasurements">DataTable donde se retornan los datos</param>
        private void LoadDataTableValueParamMeasurements(Entities.ProcessTaskMeasurement processTaskMeasurement, String fileStream, ref DataTable dtTVPMeasurements)
        {
            //Recorre el archivo y obtiene cada medicion
            String[] _separatorRow = new String[] { "\r\n" };
            String[] _records = fileStream.Split(_separatorRow, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < _records.LongLength; i++)
            {
                //Obtiene los datos del registro en donde esta parado.
                DateTime _measureDate = Convert.ToDateTime(_records[i].Split(';')[0]);
                Decimal? _measureValue = null;
                if (_records[i].Split(';')[1] != "")
                {
                    _measureValue = Convert.ToDecimal(_records[i].Split(';')[1]);
                }

                DateTime _startDate = DateTime.Now;
                DateTime _endDate = DateTime.Now;
                CalculateStartEndDate(processTaskMeasurement.Measurement.TimeUnitFrequency, processTaskMeasurement.Measurement.Frequency, processTaskMeasurement.Measurement.IsRegressive, _measureDate, ref _startDate, ref _endDate);
                //Agrego el registro al DataTable
                dtTVPMeasurements.Rows.Add(_measureDate, _measureValue, _startDate, _endDate);
            }
        }

        internal void ChangeStatusNotify(Int64 idProcess, Int64 IdExecution)
        {
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

            _dbProcessesFramework.ProcessTaskExecutions_Update(idProcess, IdExecution, true);
        }

        /// <summary>
        /// Metodo que hace ek update de los valores de medicion
        /// </summary>
        /// <param name="dtMeasurement"></param>
        internal void UpdateDataSeries(DataTable dtDataSerie)
        {
            try
            {
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //cra el dt
                DataTable _dtDataSeriesUpdate = BuildDataTableValueDataSeriesUpdate();


                Double _minuteValue = 0;
                //calcula el minutevalue
                foreach (DataRow _row in dtDataSerie.Rows)
                {
                    if (Convert.ToBoolean(_row["IsCumulative"]))
                    {
                        //saca la diferencia de horas
                        TimeSpan _difTime = Convert.ToDateTime(_row["EndDate"]).Subtract(Convert.ToDateTime(_row["StartDate"]));
                        //lo pasa a minutos
                        Double _minutes = Convert.ToDouble(_difTime.TotalMinutes);
                        //saca el valor del minuto
                        _minuteValue = Convert.ToDouble(_row["MeasurementValue"]) / _minutes;

                        _dtDataSeriesUpdate.Rows.Add(Convert.ToInt64(_row["IdProcess"]),
                                                        Convert.ToInt64(_row["IdExecution"]),
                                                        Convert.ToInt64(_row["IdExecutionMeasurement"]),
                                                        Convert.ToDouble(_row["MeasurementValue"]), 
                                                        Convert.ToDateTime(_row["MeasurementDate"]),                                                        
                                                        _minuteValue);
                    }
                    else
                    {
                        _dtDataSeriesUpdate.Rows.Add(Convert.ToInt64(_row["IdProcess"]),
                                                        Convert.ToInt64(_row["IdExecution"]),
                                                        Convert.ToInt64(_row["IdExecutionMeasurement"]),
                                                        Convert.ToDouble(_row["MeasurementValue"]), 
                                                        Convert.ToDateTime(_row["MeasurementDate"]),                                                        
                                                        Convert.ToDouble(_row["MeasurementValue"]));
                    }
                }

                using (TransactionScope _transactionScope = new TransactionScope())
                {

                    //update de measurement
                    _dbProcessesFramework.ProcessTaskExecutions_UpdateMeasurementExecution(_dtDataSeriesUpdate);

                    //update de forcalculate
                    _dbProcessesFramework.ProcessTaskExecutions_UpdateMeasurementExecutionForCalculate(_dtDataSeriesUpdate);
                 
                    //borra los calculos
                    //lo hace en la medicion

                    //borrar las excepciones
                    DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                    foreach (DataRow _row in dtDataSerie.Rows)
                    {
                        _dbImprovementActions.Exceptions_DeleteExcutionMeasurementExceptions(Convert.ToInt64(_row["IdProcess"]), Convert.ToInt64(_row["IdExecution"]), Convert.ToInt64(_row["IdExecutionMeasurement"]));
                    }

                    //generar nuevas excepciones de valor 
                    RaiseExceptionsForFiles(dtDataSerie);

                    _transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Update Error", ex);
            }
        }
        private DataTable BuildDataTableValueDataSeriesUpdate()
        {
            DataTable _dtDataSeries = new DataTable();
            _dtDataSeries.TableName = "dtDataSeries";
            DataColumn _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.Int64");
            _column.Caption = "IdProcess";
            //Inserta la columna definida en el DAtaTable.
            _dtDataSeries.Columns.Add(_column);

            _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.Int64");
            _column.Caption = "IdExecution";
            //Inserta la columna definida en el DAtaTable.
            _dtDataSeries.Columns.Add(_column);

            _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.Int64");
            _column.Caption = "IdExecutionMeasurement";
            //Inserta la columna definida en el DAtaTable.
            _dtDataSeries.Columns.Add(_column);
          
            _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.Double");
            _column.Caption = "MeasurementValue";
            //Inserta la columna definida en el DAtaTable.
            _dtDataSeries.Columns.Add(_column);

            _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.DateTime");
            _column.Caption = "MeasurementDate";
            //Inserta la columna definida en el DAtaTable.
            _dtDataSeries.Columns.Add(_column);


            _column = new DataColumn();
            _column.DataType = System.Type.GetType("System.Double");
            _column.Caption = "MinuteValue";
            //Inserta la columna definida en el DAtaTable.
            _dtDataSeries.Columns.Add(_column);


            return _dtDataSeries;
        }
        private void RaiseExceptionsForFiles(DataTable dtDataSerie)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //Recorre el archivo y obtiene cada medicion
                foreach (DataRow _row in dtDataSerie.Rows)
                {
                    PA.Collections.ParameterRanges _parameterRange = new Condesus.EMS.Business.PA.Collections.ParameterRanges(_Credential);

                    PF.Entities.ProcessTaskMeasurement _processTaskMeasurement = (PF.Entities.ProcessTaskMeasurement)new PF.Collections.ProcessTasks(_Credential).Item(Convert.ToInt64(_row["IdProcess"]));

                    foreach (PA.Entities.ParameterGroup _parameterGroup in _processTaskMeasurement.Measurement.ParameterGroups.Values)
                    {
                        
                            //Obtiene los datos del registro en donde esta parado.
                        DateTime _measureDate = Convert.ToDateTime(_row["MeasurementDate"]);
                            Double _measureValue =  Convert.ToDouble(_row["MeasurementValue"]);

                            PA.Entities.Parameter _parameter = _parameterRange.ValidateValueRange(_parameterGroup, _measureValue);

                            if (_parameter.RaiseException) // ver si se genera un objeto que resuelva la operacion
                            {
                                new IA.Collections.Exceptions(_Credential).Add(_processTaskMeasurement.IdProcess, Convert.ToInt64(_row["IdExecution"]), _measureValue, _measureDate, Common.Constants.ExceptionTypeMeasurementOutofRange, _parameterGroup.LanguageOption.Name + "/" + _parameter.LanguageOption.Description + ": " + _measureValue.ToString() + " - " + _measureDate.ToString());
                                _dbProcessesFramework.ProcessTaskMeasurements_Update(_processTaskMeasurement.IdProcess, true);
                            }
                        
                    }
                }
            }
            catch
            {
            }
        }
    }
}
