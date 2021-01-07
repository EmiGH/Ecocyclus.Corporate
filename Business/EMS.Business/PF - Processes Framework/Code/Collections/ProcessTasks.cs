using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    internal class ProcessTasks
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal ProcessTasks(Credential credential)
        {
            _Credential = credential;
        } 

        #region Read Common Functions
            internal Entities.ProcessTask Item(Int64 idProcess)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                Entities.ProcessTask _processTask = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTasks_ReadById(idProcess, _Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_processTask == null)
                    {
                        _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _processTask;
                        }
                    }
                    else
                    {
                        return Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                    }
                }
                return _processTask;
            }
            internal Entities.ProcessTask Item(IA.Entities.Exception exception)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessTasks _dbProcessTasks = _dbProcessesFramework.ProcessTasks;
                
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTasks_ReadByException(exception.IdException);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {                   
                    return Item(Convert.ToInt64(_dbRecord["IdProcess"]));
                }
                return null;
            }
            internal Dictionary<Int64, Entities.ProcessTask> Items(Int64 idProcessParent)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessTask> _oItems = new Dictionary<Int64, Entities.ProcessTask>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessTasks _dbProcessTasks = _dbProcessesFramework.ProcessTasks;

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTasks_ReadByParent(idProcessParent, _Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdProcess"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdProcess"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }

                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia  
                            Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                            //Lo agrego a la coleccion
                            _oItems.Add(_processTask.IdProcess, _processTask);
                        }
                        _oInsert = true;
                    }
                    else
                    {   //Declara e instancia  
                        Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                        //Lo agrego a la coleccion
                        _oItems.Add(_processTask.IdProcess, _processTask);
                    }

                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessTask> ItemsAdvanceNotice(Int64 idProcessParent)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessTask> _oItems = new Dictionary<Int64, Entities.ProcessTask>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessTasks _dbProcessTasks = _dbProcessesFramework.ProcessTasks;

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTasks_ReadByParentAdvanceNotice(idProcessParent, _Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdProcess"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdProcess"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }

                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia  
                            Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                            //Lo agrego a la coleccion
                            _oItems.Add(_processTask.IdProcess, _processTask);
                        }
                        _oInsert = true;
                    }
                    else
                    {   //Declara e instancia  
                        Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                        //Lo agrego a la coleccion
                        _oItems.Add(_processTask.IdProcess, _processTask);
                    }

                }
                return _oItems;
            }
            internal Entities.ProcessTask Item(PA.Entities.Measurement measurement)
            {
                 //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                Entities.ProcessTask _processTask = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTasks_ReadByMeasurement(measurement.IdMeasurement, _Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_processTask == null)
                    {
                        _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                        if (Convert.ToString(_dbRecord["IdLanguage"]) != _Credential.DefaultLanguage.IdLanguage)
                        {
                            return _processTask;
                        }
                    }
                    else
                    {
                        return Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                    }
                }
                return _processTask;
            }
            internal Dictionary<Int64, Entities.ProcessTask> Items(PA.Entities.MeasurementDevice measurementDevice)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessTask> _oItems = new Dictionary<Int64, Entities.ProcessTask>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTasks_ReadByMeasurementDevice(measurementDevice.IdMeasurementDevice, _Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdProcess"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdProcess"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }

                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia  
                            Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                            //Lo agrego a la coleccion
                            _oItems.Add(_processTask.IdProcess, _processTask);
                        }
                        _oInsert = true;
                    }
                    else
                    {   //Declara e instancia  
                        Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                        //Lo agrego a la coleccion
                        _oItems.Add(_processTask.IdProcess, _processTask);
                    }

                }
                return _oItems;
            }
            internal List<DateTime> ReadByDataRecoveryMeasurement(Int64 idProcess)
            {
                 //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTasks_ReadByDataRecoveryMeasurement(idProcess );
               
                List<DateTime> _fechas = new List<DateTime>();
                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {                            
                    //Lo agrego a la coleccion
                    _fechas.Add(Convert.ToDateTime(_dbRecord["MeasurementDate"]));
                }
                 
                return _fechas;
            }

            internal Dictionary<Int64, Entities.ProcessTask> Items(GIS.Entities.Site site, Entities.ProcessGroupProcess process)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessTask> _oItems = new Dictionary<Int64, Entities.ProcessTask>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskMeasurements_ReadByFacility(process.IdProcess, site.IdFacility, _Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdProcess"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdProcess"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }

                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia  
                            Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                            //Lo agrego a la coleccion
                            _oItems.Add(_processTask.IdProcess, _processTask);
                        }
                        _oInsert = true;
                    }
                    else
                    {   //Declara e instancia  
                        Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                        //Lo agrego a la coleccion
                        _oItems.Add(_processTask.IdProcess, _processTask);
                    }

                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessTask> Items(Entities.ProcessGroupProcess process)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessTask> _oItems = new Dictionary<Int64, Entities.ProcessTask>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskMeasurements_ReadByProcessWhitOutFacility(process.IdProcess, _Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdProcess"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdProcess"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }

                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia  
                            Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                            //Lo agrego a la coleccion
                            _oItems.Add(_processTask.IdProcess, _processTask);
                        }
                        _oInsert = true;
                    }
                    else
                    {   //Declara e instancia  
                        Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                        //Lo agrego a la coleccion
                        _oItems.Add(_processTask.IdProcess, _processTask);
                    }

                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessTask> ItemsTaskCalibrations(Entities.ProcessGroupProcess process)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessTask> _oItems = new Dictionary<Int64, Entities.ProcessTask>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTasks_ReadTaskCalibration(process.IdProcess, _Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdProcess"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdProcess"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }

                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia  
                            Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                            //Lo agrego a la coleccion
                            _oItems.Add(_processTask.IdProcess, _processTask);
                        }
                        _oInsert = true;
                    }
                    else
                    {   //Declara e instancia  
                        Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                        //Lo agrego a la coleccion
                        _oItems.Add(_processTask.IdProcess, _processTask);
                    }

                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessTask> ItemsTaskOperation(Entities.ProcessGroupProcess process)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessTask> _oItems = new Dictionary<Int64, Entities.ProcessTask>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTasks_ReadTaskOperation(process.IdProcess, _Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdProcess"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdProcess"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }

                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia  
                            Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                            //Lo agrego a la coleccion
                            _oItems.Add(_processTask.IdProcess, _processTask);
                        }
                        _oInsert = true;
                    }
                    else
                    {   //Declara e instancia  
                        Entities.ProcessTask _processTask = Factories.TaskFactory.CreateTask(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), _Credential, Convert.ToInt64(_dbRecord["IdParentProcess"]), Convert.ToDateTime(_dbRecord["StartDate"]), Convert.ToDateTime(_dbRecord["EndDate"]), Convert.ToInt32(_dbRecord["Duration"]), Convert.ToInt32(_dbRecord["Interval"]), Convert.ToInt64(_dbRecord["MaxNumberExecutions"]), Convert.ToBoolean(_dbRecord["Result"]), Convert.ToInt32(_dbRecord["Completed"]), Convert.ToInt64(_dbRecord["TimeUnitDuration"]), Convert.ToInt64(_dbRecord["TimeUnitInterval"]), Convert.ToString(_dbRecord["TypeExecution"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdFacility"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskInstruction"], 0)), Convert.ToBoolean(_dbRecord["ExecutionStatus"]), Convert.ToString(_dbRecord["Comment"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurementDevice"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdMeasurement"], 0)), Convert.ToString(_dbRecord["Type"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdTaskParent"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdScope"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdActivity"], 0)), Convert.ToString(_dbRecord["Reference"]), Convert.ToBoolean(Common.Common.CastNullValues(_dbRecord["MeasurementStatus"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["timeUnitAdvanceNotice"], 0)), Convert.ToInt16(Common.Common.CastNullValues(_dbRecord["AdvanceNotice"], 0)));
                        //Lo agrego a la coleccion
                        _oItems.Add(_processTask.IdProcess, _processTask);
                    }

                }
                return _oItems;
            }
        #endregion

        #region Write Function
            #region Process Task Operation
                internal Entities.ProcessTaskOperation AddTask(Int16 weight, Int16 orderNumber, String title, String purpose, String description, 
                    PF.Entities.ProcessGroupProcess process, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, 
                    Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, 
                    PF.Entities.TimeUnit timeUnitInterval, String comment, String typeExecution, List<DS.Entities.Post> post,
                    GIS.Entities.Site site, KC.Entities.Resource taskInstruction, List<NT.Entities.NotificationRecipient> notificationRecipients, PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        Int64 _IdFacility = site == null ? 0 : site.IdFacility;
                        Int64 _IdTaskInstruction = taskInstruction == null ? 0 : taskInstruction.IdResource;
                        Int64 _timeUnitAdvanceNotice = timeUnitAdvanceNotice == null ? 0 : timeUnitAdvanceNotice.IdTimeUnit;
                        //Cuando es de tipo Scheduler, debe calcular la fecha de finalizacion...
                        if (typeExecution == "Scheduler")
                        {
                            endDate = Common.Common.CalculateEndDate(timeUnitDuration.IdTimeUnit, startDate, duration);
                        }
                        if (typeExecution == "Spontaneous")
                        {
                            //Para todas las tareas el result es OK, salvo en las Operativas Repetitivas y Programadas que el usuario debe ingresarlo al momento de ejecutar.
                            result = true;
                        }

                        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idProcess = _dbProcessesFramework.Processes_Create(weight, orderNumber);

                        _dbProcessesFramework.Processes_LG_Create(_idProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

                        _dbProcessesFramework.ProcessTasks_Create(_idProcess, process.IdProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration.IdTimeUnit, timeUnitInterval.IdTimeUnit, typeExecution, _IdFacility, _IdTaskInstruction, _timeUnitAdvanceNotice, advanceNotice);

                        _dbProcessesFramework.ProcessTaskOperation_Create(_idProcess, comment);

                        _dbProcessesFramework.ProcessTaskExecutionsAll_Create(_idProcess, startDate, endDate, interval, timeUnitInterval.IdTimeUnit, typeExecution, result);
                        //Log
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                        _dbLog.Create("PF_ProcessTasks", "ProcessTasks", "Create", "IdProcess=" + _idProcess, _Credential.User.IdPerson);

                        //Devuelvo el objeto creado
                        Entities.ProcessTaskOperation _processTaskOperation = new Entities.ProcessTaskOperation(_idProcess, weight, orderNumber, _Credential.DefaultLanguage.IdLanguage, title, purpose, description, _Credential, process.IdProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration.IdTimeUnit, _timeUnitAdvanceNotice, typeExecution, _IdFacility, _IdTaskInstruction, false, comment, timeUnitAdvanceNotice.IdTimeUnit, advanceNotice);

                        foreach (DS.Entities.Post _item in post)
                        {
                            _processTaskOperation.AddExecutionPermission(_item);                    		 
                        }
                        //Alta de los emails
                        foreach (NT.Entities.NotificationRecipient _notificationRecipient in notificationRecipients)
                        {
                            switch (_notificationRecipient.GetType().Name)
                            {
                                case "NotificationRecipientEmail":
                                    _processTaskOperation.NotificationRecipientAdd(_notificationRecipient.Email);
                                    break;
                                case "NotificationRecipientPerson":
                                    NT.Entities.NotificationRecipientPerson _notificationRecipientPerson = (NT.Entities.NotificationRecipientPerson)_notificationRecipient;
                                    _processTaskOperation.NotificationRecipientPersonAdd(_notificationRecipientPerson.Person, _notificationRecipientPerson.ConctactEmail);
                                    break;
                                default:
                                    break;
                            }
                        }
                        return _processTaskOperation;
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
                internal void RemoveTask(Entities.ProcessTaskOperation processTaskOperation)
                {
                    try
                    {
                        //Borra sus dependencias
                        processTaskOperation.Remove();
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                        //Borra el registro
                        _dbProcessesFramework.ProcessTaskOperation_Delete(processTaskOperation.IdProcess);
                        //Borra la task
                        _dbProcessesFramework.ProcessTasks_Delete(processTaskOperation.IdProcess);
                        //Borrar el process 
                        _dbProcessesFramework.Processes_Delete(processTaskOperation.IdProcess);
                        //Log
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                        _dbLog.Create("PF_ProcessTasks", "ProcessTasks", "Delete", "IdProcess=" + processTaskOperation.IdProcess, _Credential.User.IdPerson);
                        
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
                internal void ModifyTask(Entities.ProcessTaskOperation processTaskOperation, Int16 weight, Int16 orderNumber, String title, 
                    String purpose, String description, PF.Entities.ProcessGroupProcess process, DateTime startDate, DateTime endDate, 
                    Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, 
                    PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, String comment, String typeExecution,
                    List<DS.Entities.Post> post, GIS.Entities.Site site, KC.Entities.Resource taskInstruction, List<NT.Entities.NotificationRecipient> notificationRecipients, PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        Int64 _IdFacility = site == null ? 0 : site.IdFacility;
                        Int64 _IdTaskInstruction = taskInstruction == null ? 0 : taskInstruction.IdResource;
                        Int64 _timeUnitAdvanceNotice = timeUnitAdvanceNotice == null ? 0 : timeUnitAdvanceNotice.IdTimeUnit;
                        //Cuando es de tipo Scheduler, debe calcular la fecha de finalizacion...
                        if (typeExecution == "Scheduler")
                        {
                            endDate = Common.Common.CalculateEndDate(timeUnitDuration.IdTimeUnit, startDate, duration);
                        }

                        //borra todos los post relacionados
                        processTaskOperation.RemoveExecutionPermission();

                        //da de alta los nuevos post
                        foreach (DS.Entities.Post _item in post)
                        {
                            processTaskOperation.AddExecutionPermission(_item);
                        }

                        //Modifico los datos de la base
                        _dbProcessesFramework.Processes_Update(processTaskOperation.IdProcess, weight, orderNumber);

                        _dbProcessesFramework.Processes_LG_Update(processTaskOperation.IdProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

                        _dbProcessesFramework.ProcessTasks_Update(processTaskOperation.IdProcess, process.IdProcess, processTaskOperation.StartDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration.IdTimeUnit, timeUnitInterval.IdTimeUnit, typeExecution, _IdFacility, _IdTaskInstruction, _timeUnitAdvanceNotice, advanceNotice);

                        _dbProcessesFramework.ProcessTaskOperation_Update(processTaskOperation.IdProcess, comment);

                        _dbProcessesFramework.ProcessTaskExecutionsAll_Update(processTaskOperation.IdProcess, startDate, endDate, interval, timeUnitInterval.IdTimeUnit, typeExecution);
                        //Borra los email de notificacion
                        processTaskOperation.NotificationRecipientRemove();
                        // alta de los email de notificacion
                        //Alta de los emails
                        foreach (NT.Entities.NotificationRecipient _notificationRecipient in notificationRecipients)
                        {
                            switch (_notificationRecipient.GetType().Name)
                            {
                                case "NotificationRecipientEmail":
                                    processTaskOperation.NotificationRecipientAdd(_notificationRecipient.Email);
                                    break;
                                case "NotificationRecipientPerson":
                                    NT.Entities.NotificationRecipientPerson _notificationRecipientPerson = (NT.Entities.NotificationRecipientPerson)_notificationRecipient;
                                    processTaskOperation.NotificationRecipientPersonAdd(_notificationRecipientPerson.Person, _notificationRecipientPerson.ConctactEmail);
                                    break;
                                default:
                                    break;
                            }
                        }
                        //Log
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                        _dbLog.Create("PF_ProcessTasks", "ProcessTasks", "Modify", "IdProcess=" + processTaskOperation.IdProcess, _Credential.User.IdPerson);

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

            #region Process Task Calibration
                internal Entities.ProcessTaskCalibration AddTask(Int16 weight, Int16 orderNumber, String title, String purpose, String description, 
                    PF.Entities.ProcessGroupProcess process, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, 
                    Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, 
                    PF.Entities.TimeUnit timeUnitInterval, PA.Entities.MeasurementDevice measurementDevice, String typeExecution,
                    List<DS.Entities.Post> post, GIS.Entities.Site site, KC.Entities.Resource taskInstruction,
                    List<NT.Entities.NotificationRecipient> notificationRecipients, PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        Int64 _IdFacility = site == null ? 0 : site.IdFacility;
                        Int64 _IdTaskInstruction = taskInstruction == null ? 0 : taskInstruction.IdResource;
                        Int64 _timeUnitAdvanceNotice = timeUnitAdvanceNotice == null ? 0 : timeUnitAdvanceNotice.IdTimeUnit;
                        //Cuando es de tipo Scheduler, debe calcular la fecha de finalizacion...
                        if (typeExecution == "Scheduler")
                        {
                            endDate = Common.Common.CalculateEndDate(timeUnitDuration.IdTimeUnit, startDate, duration);
                        }
                        //Para todas las tareas el result es OK, salvo en las Operativas Repetitivas y Programadas que el usuario debe ingresarlo al momento de ejecutar.
                        result = true;
                        
                        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idProcess = _dbProcessesFramework.Processes_Create(weight, orderNumber);

                        _dbProcessesFramework.Processes_LG_Create(_idProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

                        _dbProcessesFramework.ProcessTasks_Create(_idProcess, process.IdProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration.IdTimeUnit, timeUnitInterval.IdTimeUnit, typeExecution, _IdFacility, _IdTaskInstruction, _timeUnitAdvanceNotice, advanceNotice);

                        _dbProcessesFramework.ProcessTaskCalibrations_Create(_idProcess, measurementDevice.IdMeasurementDevice);

                        _dbProcessesFramework.ProcessTaskExecutionsAll_Create(_idProcess, startDate, endDate, interval, timeUnitInterval.IdTimeUnit, typeExecution, result);
                        //Log
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                        _dbLog.Create("PF_ProcessTasks", "ProcessTasks", "Create", "IdProcess=" + _idProcess, _Credential.User.IdPerson);

                        //Devuelvo el objeto creado
                        Entities.ProcessTaskCalibration _processTaskCalibration = new Entities.ProcessTaskCalibration(_idProcess, weight, orderNumber, _Credential.DefaultLanguage.IdLanguage, title, purpose, description, _Credential, process.IdProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration.IdTimeUnit, timeUnitInterval.IdTimeUnit, typeExecution, _IdFacility, _IdTaskInstruction, false,  measurementDevice.IdMeasurementDevice, _timeUnitAdvanceNotice, advanceNotice);

                        foreach (DS.Entities.Post _item in post)
                        {
                            _processTaskCalibration.AddExecutionPermission(_item);
                        }
                        //Alta de los emails
                        foreach (NT.Entities.NotificationRecipient _notificationRecipient in notificationRecipients)
                        {
                            switch (_notificationRecipient.GetType().Name)
                            {
                                case "NotificationRecipientEmail":
                                    _processTaskCalibration.NotificationRecipientAdd(_notificationRecipient.Email);
                                    break;
                                case "NotificationRecipientPerson":
                                    NT.Entities.NotificationRecipientPerson _notificationRecipientPerson = (NT.Entities.NotificationRecipientPerson)_notificationRecipient;
                                    _processTaskCalibration.NotificationRecipientPersonAdd(_notificationRecipientPerson.Person, _notificationRecipientPerson.ConctactEmail);
                                    break;
                                default:
                                    break;
                            }
                        }
                        return _processTaskCalibration;
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
                internal void RemoveTask(Entities.ProcessTaskCalibration processTaskCalibration)
                {
                    try
                    {
                        //Borra dependencias
                        processTaskCalibration.Remove();
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                        //Borrar de la base de datos
                        _dbProcessesFramework.ProcessTaskCalibrations_Delete(processTaskCalibration.IdProcess);
                        //Borra la task
                        _dbProcessesFramework.ProcessTasks_Delete(processTaskCalibration.IdProcess);
                        //Borrar el process 
                        _dbProcessesFramework.Processes_Delete(processTaskCalibration.IdProcess);
                        //Log
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                        _dbLog.Create("PF_ProcessTasks", "ProcessTasks", "Delete", "IdProcess=" + processTaskCalibration.IdProcess, _Credential.User.IdPerson);

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
                internal void ModifyTask(Entities.ProcessTaskCalibration processTaskCalibration, Int16 weight, Int16 orderNumber, String title, 
                    String purpose, String description, PF.Entities.ProcessGroupProcess process, DateTime startDate, DateTime endDate, 
                    Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, 
                    PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, PA.Entities.MeasurementDevice measurementDevice,
                    String typeExecution, List<DS.Entities.Post> post, GIS.Entities.Site site, KC.Entities.Resource taskInstruction,
                    List<NT.Entities.NotificationRecipient> notificationRecipients, PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        Int64 _IdFacility = site == null ? 0 : site.IdFacility;
                        Int64 _IdTaskInstruction = taskInstruction == null ? 0 : taskInstruction.IdResource;
                        Int64 _timeUnitAdvanceNotice = timeUnitAdvanceNotice == null ? 0 : timeUnitAdvanceNotice.IdTimeUnit;

                        //Cuando es de tipo Scheduler, debe calcular la fecha de finalizacion...
                        if (typeExecution == "Scheduler")
                        {
                            endDate = Common.Common.CalculateEndDate(timeUnitDuration.IdTimeUnit, startDate, duration);
                        }

                        //borra todos los post relacionados
                        processTaskCalibration.RemoveExecutionPermission();

                        //da de alta los nuevos post
                        foreach (DS.Entities.Post _item in post)
                        {
                            processTaskCalibration.AddExecutionPermission(_item);
                        }
                        //Modifico los datos de la base
                        _dbProcessesFramework.Processes_Update(processTaskCalibration.IdProcess, weight, orderNumber);

                        _dbProcessesFramework.Processes_LG_Update(processTaskCalibration.IdProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

                        _dbProcessesFramework.ProcessTasks_Update(processTaskCalibration.IdProcess, process.IdProcess, processTaskCalibration.StartDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration.IdTimeUnit, timeUnitInterval.IdTimeUnit, typeExecution, _IdFacility, _IdTaskInstruction, _timeUnitAdvanceNotice, advanceNotice);

                        _dbProcessesFramework.ProcessTaskCalibrations_Update(processTaskCalibration.IdProcess, measurementDevice.IdMeasurementDevice);

                        _dbProcessesFramework.ProcessTaskExecutionsAll_Update(processTaskCalibration.IdProcess, startDate, endDate, interval, timeUnitInterval.IdTimeUnit, typeExecution);
                        //Borra los email de notificacion
                        processTaskCalibration.NotificationRecipientRemove();
                        // alta de los email de notificacion
                        //Alta de los emails
                        foreach (NT.Entities.NotificationRecipient _notificationRecipient in notificationRecipients)
                        {
                            switch (_notificationRecipient.GetType().Name)
                            {
                                case "NotificationRecipientEmail":
                                    processTaskCalibration.NotificationRecipientAdd(_notificationRecipient.Email);
                                    break;
                                case "NotificationRecipientPerson":
                                    NT.Entities.NotificationRecipientPerson _notificationRecipientPerson = (NT.Entities.NotificationRecipientPerson)_notificationRecipient;
                                    processTaskCalibration.NotificationRecipientPersonAdd(_notificationRecipientPerson.Person, _notificationRecipientPerson.ConctactEmail);
                                    break;
                                default:
                                    break;
                            }
                        }
                        //Log
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                        _dbLog.Create("PF_ProcessTasks", "ProcessTasks", "Modify", "IdProcess=" + processTaskCalibration.IdProcess, _Credential.User.IdPerson);

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

            #region Process Task Measurement
                internal Entities.ProcessTaskMeasurement AddTask(Int16 weight, Int16 orderNumber, String title, String purpose, String description, 
                    PF.Entities.ProcessGroupProcess process, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, 
                    Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval,
                    PA.Entities.Measurement measurement, String typeExecution, List<DS.Entities.Post> post, GIS.Entities.Site site, KC.Entities.Resource taskInstruction,
                    PA.Entities.AccountingScope accountingScope, PA.Entities.AccountingActivity accountingActivity, String Reference, List<NT.Entities.NotificationRecipient> notificationRecipients,
                    PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        Int64 _IdFacility = site == null ? 0 : site.IdFacility;
                        Int64 _IdTaskInstruction = taskInstruction == null ? 0 : taskInstruction.IdResource;
                        Int64 _IdScope = accountingScope == null ? 0 : accountingScope.IdScope;
                        Int64 _IdActivity = accountingActivity == null ? 0 : accountingActivity.IdActivity;
                        Int64 _timeUnitAdvanceNotice = timeUnitAdvanceNotice == null ? 0 : timeUnitAdvanceNotice.IdTimeUnit;

                        //Cuando es de tipo Scheduler, debe calcular la fecha de finalizacion...
                        if (typeExecution == "Scheduler")
                        {
                            endDate = Common.Common.CalculateEndDate(timeUnitDuration.IdTimeUnit, startDate, duration);
                        }
                        //Para todas las tareas el result es OK, salvo en las Operativas Repetitivas y Programadas que el usuario debe ingresarlo al momento de ejecutar.
                        result = true;

                        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idProcess = _dbProcessesFramework.Processes_Create(weight, orderNumber);

                        _dbProcessesFramework.Processes_LG_Create(_idProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

                        _dbProcessesFramework.ProcessTasks_Create(_idProcess, process.IdProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration.IdTimeUnit, timeUnitInterval.IdTimeUnit, typeExecution, _IdFacility, _IdTaskInstruction, _timeUnitAdvanceNotice, advanceNotice);

                        _dbProcessesFramework.ProcessTaskMeasurements_Create(_idProcess, measurement.IdMeasurement, _IdScope, _IdActivity, Reference);

                        _dbProcessesFramework.ProcessTaskExecutionsAll_Create(_idProcess, startDate, endDate, interval, timeUnitInterval.IdTimeUnit, typeExecution, result);
                        //Log
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                        _dbLog.Create("PF_ProcessTasks", "ProcessTasks", "Create", "IdProcess=" + _idProcess, _Credential.User.IdPerson);
                        
                        //Devuelvo el objeto creado
                        Entities.ProcessTaskMeasurement _processTaskMeasurement = new Entities.ProcessTaskMeasurement(_idProcess, weight, orderNumber, _Credential.DefaultLanguage.IdLanguage, title, purpose, description, _Credential, process.IdProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration.IdTimeUnit, timeUnitInterval.IdTimeUnit, typeExecution, _IdFacility, _IdTaskInstruction, false, measurement.IdMeasurement, _IdScope, _IdActivity, Reference, false, _timeUnitAdvanceNotice, advanceNotice);

                        foreach (DS.Entities.Post _item in post)
                        {
                            _processTaskMeasurement.AddExecutionPermission(_item);
                        }
                        //Alta de los emails
                        foreach (NT.Entities.NotificationRecipient _notificationRecipient in notificationRecipients)
                        {
                            switch (_notificationRecipient.GetType().Name)
                            {
                                case "NotificationRecipientEmail":
                                    _processTaskMeasurement.NotificationRecipientAdd(_notificationRecipient.Email);
                                    break;
                                case "NotificationRecipientPerson":
                                    NT.Entities.NotificationRecipientPerson _notificationRecipientPerson = (NT.Entities.NotificationRecipientPerson)_notificationRecipient;
                                    _processTaskMeasurement.NotificationRecipientPersonAdd(_notificationRecipientPerson.Person, _notificationRecipientPerson.ConctactEmail);
                                    break;
                                default:
                                    break;
                            }
                        }

                        return _processTaskMeasurement;
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
                internal void RemoveTask(Entities.ProcessTaskMeasurement processTaskMeasurement)
                {
                    try
                    {
                        processTaskMeasurement.Remove();
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                        //Borrar de la base de datos
                        _dbProcessesFramework.ProcessTaskMeasurements_Delete(processTaskMeasurement.IdProcess);
                        //Borra la medicion
                        new PA.Collections.Measurements(_Credential).Remove(processTaskMeasurement.Measurement);
                        //Borra la task
                        _dbProcessesFramework.ProcessTasks_Delete(processTaskMeasurement.IdProcess);
                        //Borrar el process 
                        _dbProcessesFramework.Processes_Delete(processTaskMeasurement.IdProcess);
                        //Log
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                        _dbLog.Create("PF_ProcessTasks", "ProcessTasks", "Delete", "IdProcess=" + processTaskMeasurement.IdProcess, _Credential.User.IdPerson);

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
                internal void ModifyTask(Entities.ProcessTaskMeasurement processTaskMeasurement, Int16 weight, Int16 orderNumber, String title, String purpose, 
                    String description, PF.Entities.ProcessGroupProcess process, DateTime startDate, DateTime endDate, Int32 duration, 
                    Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, 
                    PF.Entities.TimeUnit timeUnitInterval, PA.Entities.Measurement measurement, String typeExecution, List<DS.Entities.Post> post,
                    GIS.Entities.Site site, KC.Entities.Resource taskInstruction, PA.Entities.AccountingScope accountingScope,
                    PA.Entities.AccountingActivity accountingActivity, String Reference, List<NT.Entities.NotificationRecipient> notificationRecipients,
                    PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        Int64 _IdFacility = site == null ? 0 : site.IdFacility;
                        Int64 _IdTaskInstruction = taskInstruction == null ? 0 : taskInstruction.IdResource;
                        Int64 _IdScope = accountingScope == null ? 0 : accountingScope.IdScope;
                        Int64 _IdActivity = accountingActivity == null ? 0 : accountingActivity.IdActivity;
                        Int64 _timeUnitAdvanceNotice = timeUnitAdvanceNotice == null ? 0 : timeUnitAdvanceNotice.IdTimeUnit;
                        //Cuando es de tipo Scheduler, debe calcular la fecha de finalizacion...
                        if (typeExecution == "Scheduler")
                        {
                            endDate = Common.Common.CalculateEndDate(timeUnitDuration.IdTimeUnit, startDate, duration);
                        }

                        //borra todos los post relacionados
                        processTaskMeasurement.RemoveExecutionPermission();

                        //da de alta los nuevos post
                        foreach (DS.Entities.Post _item in post)
                        {
                            processTaskMeasurement.AddExecutionPermission(_item);
                        }

                        //Modifico los datos de la base
                        _dbProcessesFramework.Processes_Update(processTaskMeasurement.IdProcess, weight, orderNumber);

                        _dbProcessesFramework.Processes_LG_Update(processTaskMeasurement.IdProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

                        _dbProcessesFramework.ProcessTasks_Update(processTaskMeasurement.IdProcess, process.IdProcess, processTaskMeasurement.StartDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration.IdTimeUnit, timeUnitInterval.IdTimeUnit, typeExecution, _IdFacility, _IdTaskInstruction, _timeUnitAdvanceNotice, advanceNotice);

                        _dbProcessesFramework.ProcessTaskMeasurements_Update(processTaskMeasurement.IdProcess, measurement.IdMeasurement, _IdScope, _IdActivity, Reference);

                        _dbProcessesFramework.ProcessTaskExecutionsAll_Update(processTaskMeasurement.IdProcess, startDate, endDate, interval, timeUnitInterval.IdTimeUnit, typeExecution);
                        //Borra los email de notificacion
                        processTaskMeasurement.NotificationRecipientRemove();
                        // alta de los email de notificacion
                        //Alta de los emails
                        foreach (NT.Entities.NotificationRecipient _notificationRecipient in notificationRecipients)
                        {
                            switch (_notificationRecipient.GetType().Name)
                            {
                                case "NotificationRecipientEmail":
                                    processTaskMeasurement.NotificationRecipientAdd(_notificationRecipient.Email);
                                    break;
                                case "NotificationRecipientPerson":
                                    NT.Entities.NotificationRecipientPerson _notificationRecipientPerson = (NT.Entities.NotificationRecipientPerson)_notificationRecipient;
                                    processTaskMeasurement.NotificationRecipientPersonAdd(_notificationRecipientPerson.Person, _notificationRecipientPerson.ConctactEmail);
                                    break;
                                default:
                                    break;
                            }
                        }
                        //Log
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                        _dbLog.Create("PF_ProcessTasks", "ProcessTasks", "Modify", "IdProcess=" + processTaskMeasurement.IdProcess, _Credential.User.IdPerson);

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

            #region Process Task Data Recovery
                internal Entities.ProcessTaskDataRecovery AddTask(Int16 weight, Int16 orderNumber, String title, String purpose, String description,
                PF.Entities.ProcessGroupException process, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, 
                    Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, 
                    String typeExecution, PF.Entities.ProcessTaskMeasurement processTaskMeasurement, List<DS.Entities.Post> post,
                    GIS.Entities.Site site, KC.Entities.Resource taskInstruction, List<NT.Entities.NotificationRecipient> notificationRecipients,
                    PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
                {
                  try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        Int64 _IdFacility = site == null ? 0 : site.IdFacility;
                        Int64 _IdTaskInstruction = taskInstruction == null ? 0 : taskInstruction.IdResource;
                        Int64 _timeUnitAdvanceNotice = timeUnitAdvanceNotice == null ? 0 : timeUnitAdvanceNotice.IdTimeUnit;
                        //DataAccess.PF.Entities.ProcessTasks _dbProcessTasks = _dbProcessesFramework.ProcessTasks;

                        //Cuando es de tipo Scheduler, debe calcular la fecha de finalizacion...
                        if (typeExecution == "Scheduler")
                        {
                            endDate = Common.Common.CalculateEndDate(timeUnitDuration.IdTimeUnit, startDate, duration);

                            //Para todas las tareas el result es OK, salvo en las Operativas Repetitivas y Programadas que el usuario debe ingresarlo al momento de ejecutar.
                            result = true;

                            //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                            Int64 _idProcess = _dbProcessesFramework.Processes_Create(weight, orderNumber);

                            _dbProcessesFramework.Processes_LG_Create(_idProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

                            _dbProcessesFramework.ProcessTasks_Create(_idProcess, process.IdProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration.IdTimeUnit, timeUnitInterval.IdTimeUnit, typeExecution, _IdFacility, _IdTaskInstruction, _timeUnitAdvanceNotice, advanceNotice);

                            _dbProcessesFramework.ProcessTaskDataRecoveries_Create(_idProcess, processTaskMeasurement.IdProcess);

                            _dbProcessesFramework.ProcessTaskExecutionsAll_Create(_idProcess, startDate, endDate, interval, timeUnitInterval.IdTimeUnit, typeExecution, result);
                            //Log
                            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                            _dbLog.Create("PF_ProcessTasks", "ProcessTasks", "Create", "IdProcess=" + _idProcess, _Credential.User.IdPerson);
                        
                            //Devuelvo el objeto creado
                            Entities.ProcessTaskDataRecovery _processTaskDataRecovery = new Entities.ProcessTaskDataRecovery(_idProcess, weight, orderNumber, _Credential.DefaultLanguage.IdLanguage, title, purpose, description, _Credential, process.IdProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration.IdTimeUnit, timeUnitInterval.IdTimeUnit, typeExecution, _IdFacility, _IdTaskInstruction, false, processTaskMeasurement.IdProcess, _timeUnitAdvanceNotice, advanceNotice);

                            foreach (DS.Entities.Post _item in post)
                            {
                                _processTaskDataRecovery.AddExecutionPermission(_item);
                            }
                            //Alta de los emails
                            foreach (NT.Entities.NotificationRecipient _notificationRecipient in notificationRecipients)
                            {
                                switch (_notificationRecipient.GetType().Name)
                                {
                                    case "NotificationRecipientEmail":
                                        _processTaskDataRecovery.NotificationRecipientAdd(_notificationRecipient.Email);
                                        break;
                                    case "NotificationRecipientPerson":
                                        NT.Entities.NotificationRecipientPerson _notificationRecipientPerson = (NT.Entities.NotificationRecipientPerson)_notificationRecipient;
                                        _processTaskDataRecovery.NotificationRecipientPersonAdd(_notificationRecipientPerson.Person, _notificationRecipientPerson.ConctactEmail);
                                        break;
                                    default:
                                        break;
                                }
                            }

                            return _processTaskDataRecovery;

                        }
                        else
                        {
                            throw new Exception(Common.Resources.Errors.InvalidTypeExecutionTaskRecovery);
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
                internal void RemoveTask(Entities.ProcessTaskDataRecovery processTaskDataRecovery)
                {
                    try
                    {
                        processTaskDataRecovery.Remove();
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                        //Borrar de la base de datos
                        _dbProcessesFramework.ProcessTaskDataRecoveries_Delete(processTaskDataRecovery.IdProcess);
                        //Borra la task
                        _dbProcessesFramework.ProcessTasks_Delete(processTaskDataRecovery.IdProcess);
                        //Borrar el process 
                        _dbProcessesFramework.Processes_Delete(processTaskDataRecovery.IdProcess);
                        //Log
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                        _dbLog.Create("PF_ProcessTasks", "ProcessTasks", "Delete", "IdProcess=" + processTaskDataRecovery.IdProcess, _Credential.User.IdPerson);

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
                public void ModifyTask(Entities.ProcessTaskDataRecovery processTaskDataRecovery, Int16 weight, Int16 orderNumber, String title, 
                    String purpose, String description, PF.Entities.ProcessGroupProcess process, DateTime startDate, DateTime endDate, 
                    Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, 
                    PF.Entities.TimeUnit timeUnitInterval, String typeExecution, PF.Entities.ProcessTaskMeasurement processTaskMeasurement,
                    List<DateTime> measurementDateToRecovery, List<DS.Entities.Post> post, GIS.Entities.Site site, 
                    KC.Entities.Resource taskInstruction,List<NT.Entities.NotificationRecipient> notificationRecipients,
                    PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        Int64 _IdFacility = site == null ? 0 : site.IdFacility;
                        Int64 _IdTaskInstruction = taskInstruction == null ? 0 : taskInstruction.IdResource;
                        Int64 _timeUnitAdvanceNotice = timeUnitAdvanceNotice == null ? 0 : timeUnitAdvanceNotice.IdTimeUnit;

                        //Cuando es de tipo Scheduler, debe calcular la fecha de finalizacion...
                        if (typeExecution == "Scheduler")
                        {
                            endDate = Common.Common.CalculateEndDate(timeUnitDuration.IdTimeUnit, startDate, duration);
                        }
                            //borra todos los post relacionados
                            processTaskDataRecovery.RemoveExecutionPermission();

                            //da de alta los nuevos post
                            foreach (DS.Entities.Post _item in post)
                            {
                                processTaskDataRecovery.AddExecutionPermission(_item);
                            }

                            //Modifico los datos de la base
                            _dbProcessesFramework.Processes_Update(processTaskDataRecovery.IdProcess, weight, orderNumber);

                            _dbProcessesFramework.Processes_LG_Update(processTaskDataRecovery.IdProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

                            _dbProcessesFramework.ProcessTasks_Update(processTaskDataRecovery.IdProcess, process.IdProcess, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration.IdTimeUnit, timeUnitInterval.IdTimeUnit, typeExecution, _IdFacility, _IdTaskInstruction, _timeUnitAdvanceNotice,advanceNotice);

                            _dbProcessesFramework.ProcessTaskExecutionsAll_Update(processTaskDataRecovery.IdProcess, startDate, endDate, interval, timeUnitInterval.IdTimeUnit, typeExecution);
                            //Borra los email de notificacion
                            processTaskDataRecovery.NotificationRecipientRemove();
                            // alta de los email de notificacion
                            //Alta de los emails
                            foreach (NT.Entities.NotificationRecipient _notificationRecipient in notificationRecipients)
                            {
                                switch (_notificationRecipient.GetType().Name)
                                {
                                    case "NotificationRecipientEmail":
                                        processTaskDataRecovery.NotificationRecipientAdd(_notificationRecipient.Email);
                                        break;
                                    case "NotificationRecipientPerson":
                                        NT.Entities.NotificationRecipientPerson _notificationRecipientPerson = (NT.Entities.NotificationRecipientPerson)_notificationRecipient;
                                        processTaskDataRecovery.NotificationRecipientPersonAdd(_notificationRecipientPerson.Person, _notificationRecipientPerson.ConctactEmail);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            //Log
                            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                            _dbLog.Create("PF_ProcessTasks", "ProcessTasks", "Modify", "IdProcess=" + processTaskDataRecovery.IdProcess, _Credential.User.IdPerson);

                            //2° borra las fechas de medicion a recuperar
                            new PF.Collections.ProcessTasks(_Credential).RemoveDataRecoveryMeasurement((PF.Entities.ProcessTaskDataRecovery)processTaskDataRecovery);

                            //3° vuelve a grabar las fechas de medicion a recuperar
                            new PF.Collections.ProcessTasks(_Credential).AddDataRecoveryMeasurement((PF.Entities.ProcessTaskDataRecovery)processTaskDataRecovery, measurementDateToRecovery);

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
                internal void AddDataRecoveryMeasurement(PF.Entities.ProcessTaskDataRecovery processTaskDataRecovery, 
                    List<DateTime> measurementDateToRecovery)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                        //DataAccess.PF.Entities.ProcessTasks _dbProcessTasks = _dbProcessesFramework.ProcessTasks;

                        foreach (DateTime _measurementDateToRecovery in measurementDateToRecovery)
                        {
                            //Ejecuta el insert en la base de datos
                            _dbProcessesFramework.ProcessTasks_CreateTaskDataRecoveryMeasurement(processTaskDataRecovery.IdProcess, _measurementDateToRecovery, _Credential.User.Person.IdPerson);
                            
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
                internal void RemoveDataRecoveryMeasurement(PF.Entities.ProcessTaskDataRecovery processTaskDataRecovery)
                {
                    //Check for permission
                    //Condesus.EMS.Business.Security.Authority.Authorize("ProcessesFramework", "ExtendedProperties", "Write");
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                        //DataAccess.PF.Entities.ProcessTasks _dbProcessTasks = _dbProcessesFramework.ProcessTasks;

                        //Borrar de la base de datos
                        _dbProcessesFramework.ProcessTasks_DeleteTaskDataRecoveryMeasurement(processTaskDataRecovery.IdProcess, _Credential.User.Person.IdPerson);
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

        #region Execution Permissions
            #region Read Execution Permissions

                internal List<DS.Entities.Post> ReadExecutionPermissions(Entities.ProcessTask processTask)
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    //Traigo los datos de la base
                    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskPermissions_ReadByProcess(processTask.IdProcess);

                    List<DS.Entities.Post> _posts = new List<DS.Entities.Post>();
                    //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                    {

                        DS.Entities.Post _post = new DS.Collections.Posts(processTask).Item(Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(_dbRecord["IdFunctionalArea"]), Convert.ToInt64(_dbRecord["IdPosition"]));
                        //Lo agrego a la coleccion
                        _posts.Add(_post);
                    }
                    return _posts;
                }
                internal DS.Entities.Post ReadExecutionPermission(Entities.ProcessTask processTask, DS.Entities.Post post)
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    //Traigo los datos de la base
                    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskPermissions_ReadById(processTask.IdProcess, post.Organization.IdOrganization, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, post.JobTitle.FunctionalPositions.Position.IdPosition, post.Person.IdPerson);

                    DS.Entities.Post _post;
                    //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                    {
                        return _post = new DS.Collections.Posts(processTask).Item(Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(_dbRecord["IdFunctionalArea"]), Convert.ToInt64(_dbRecord["IdPosition"]));
                    }
                    return null;
                }
                
                internal List<Entities.ProcessTask> ReadExecutionPermissions(DS.Entities.Post post)
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    //Traigo los datos de la base
                    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessTaskPermissions_ReadByPost(post.Organization.IdOrganization, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, post.JobTitle.FunctionalPositions.Position.IdPosition, post.Person.IdPerson);

                    List<Entities.ProcessTask> _processTasks = new List<Entities.ProcessTask>();
                    //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                    {
                        Entities.ProcessTask _processTask = new Collections.ProcessTasks(_Credential).Item(Convert.ToInt64(_dbRecord["IdProcess"]));
                        //Lo agrego a la coleccion
                        _processTasks.Add(_processTask);
                    }
                    return _processTasks;
                }

                #endregion

            #region Write Execution Permissions
                internal void AddExecutionPermission(Entities.ProcessTask processTask, DS.Entities.Post post)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                        
                        //Ejecuta el insert 
                        _dbProcessesFramework.ProcessTaskPermissions_Create(processTask.IdProcess, post.Organization.IdOrganization, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, post.JobTitle.FunctionalPositions.Position.IdPosition, post.Person.IdPerson);

                        //Alta de  view para el process
                        Security.Entities.Permission _permission = new Security.Collections.Permissions(_Credential).Item(Common.Permissions.View);
                        processTask.Parent.SecurityPersonAdd(post.Person, _permission);
                        PF.Entities.ConfigurationPF _configurationPF = new PF.Entities.ConfigurationPF(_Credential);
                        Security.Entities.RightPerson _rightPerson1 = new Security.Collections.Rights(_Credential).Add(_configurationPF, post.Person, _permission);
                        KC.Entities.MapKC _mapKC = new KC.Entities.MapKC(_Credential);
                        Security.Entities.RightPerson _rightPerson2 = new Security.Collections.Rights(_Credential).Add(_mapKC, post.Person, _permission);

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
                internal void RemoveExecutionPermission(Entities.ProcessTask processTask, DS.Entities.Post post)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        //Ejecuta el delete
                        _dbProcessesFramework.ProcessTaskPermissions_Delete(processTask.IdProcess, post.Organization.IdOrganization, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, post.JobTitle.FunctionalPositions.Position.IdPosition, post.Person.IdPerson);

                        //borra los permisos de view sobre el process
                        Security.Entities.Permission _permission = new Security.Collections.Permissions(_Credential).Item(Common.Permissions.View);
                        processTask.Parent.SecurityPersonRemove(post.Person, _permission);
                        PF.Entities.ConfigurationPF _configurationPF = new PF.Entities.ConfigurationPF(_Credential);
                        new Security.Collections.Rights(_Credential).Remove(post.Person, _configurationPF, _permission);
                        KC.Entities.MapKC _mapKC = new KC.Entities.MapKC(_Credential);
                        new Security.Collections.Rights(_Credential).Remove(post.Person, _mapKC, _permission);

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
                internal void RemoveExecutionPermission(DS.Entities.Post post)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                        //Ejecuta el insert 
                        _dbProcessesFramework.ProcessTaskPermissions_Delete(post.Organization.IdOrganization, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, post.JobTitle.FunctionalPositions.Position.IdPosition, post.Person.IdPerson);
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
                internal void RemoveExecutionPermission(Entities.ProcessTask processTask)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                        
                        //borra el registro
                        _dbProcessesFramework.ProcessTaskPermissions_Delete(processTask.IdProcess);
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

    }
}
