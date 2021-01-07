using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class Calculations
    {
        internal Calculations()
        { }

        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadById(Int64 idCalculation, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Calculations_ReadById");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
                SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

                try
                {
                    foreach (DbDataRecord _record in _reader)
                    {
                        yield return _record;
                    }
                }
                finally
                {
                    _reader.Close();
                }
            }
            internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Calculations_ReadAll");
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
                SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

                try
                {
                    foreach (DbDataRecord _record in _reader)
                    {
                        yield return _record;
                    }
                }
                finally
                {
                    _reader.Close();
                }
            }
            internal IEnumerable<DbDataRecord> ReadByProcess(Int64 idProcess, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationProcessGroupProcesses_ReadByProject");
                _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
                SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

                try
                {
                    foreach (DbDataRecord _record in _reader)
                    {
                        yield return _record;
                    }
                }
                finally
                {
                    _reader.Close();
                }
            }
            internal IEnumerable<DbDataRecord> ReadByProcessIndicator(Int64 idProcess, Int64 indicator, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Calculations_ReadByIndicator");
                _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, indicator);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
                SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

                try
                {
                    foreach (DbDataRecord _record in _reader)
                    {
                        yield return _record;
                    }
                }
                finally
                {
                    _reader.Close();
                }
            }
            internal IEnumerable<DbDataRecord> ReadByFormula(Int64 idFormula, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Calculations_ReadByFormula");
                _db.AddInParameter(_dbCommand, "idFormula", DbType.Int64, idFormula);
                _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
                SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

                try
                {
                    foreach (DbDataRecord _record in _reader)
                    {
                        yield return _record;
                    }
                }
                finally
                {
                    _reader.Close();
                }
            }

            internal IEnumerable<DbDataRecord> ReadSeries(Int64 idCalculation, DateTime? startDate, DateTime? endDate)
            {
                Database _db = DatabaseFactory.CreateDatabase();
                DbCommand _dbCommand;

                _dbCommand = _db.GetStoredProcCommand("PA_CalculationResultHistories_SeriesByMonthSumCummulative");
                _db.AddInParameter(_dbCommand, "IdCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);

                SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

                try
                {
                    foreach (DbDataRecord _record in _reader)
                    {
                        yield return _record;
                    }
                }
                finally
                {
                    _reader.Close();
                }
            }
            internal IEnumerable<DbDataRecord> ReadSeriesForecasted(Int64 idCalculation, DateTime? startDate, DateTime? endDate, Int64 idScenarioType)
            {
                Database _db = DatabaseFactory.CreateDatabase();
                DbCommand _dbCommand;

                _dbCommand = _db.GetStoredProcCommand("PA_CalculationUserEstimated_SeriesByMonthSumCummulative");
                _db.AddInParameter(_dbCommand, "IdCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _db.AddInParameter(_dbCommand, "IdScenarioType", DbType.Int64, idScenarioType);

                SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

                try
                {
                    foreach (DbDataRecord _record in _reader)
                    {
                        yield return _record;
                    }
                }
                finally
                {
                    _reader.Close();
                }
            }
            internal IEnumerable<DbDataRecord> ReadSeriesVerificated(Int64 idCalculation, DateTime? startDate, DateTime? endDate)
            {
                Database _db = DatabaseFactory.CreateDatabase();
                DbCommand _dbCommand;

                _dbCommand = _db.GetStoredProcCommand("PA_CalculationCertificated_SeriesByMonthSumCummulative");
                _db.AddInParameter(_dbCommand, "IdCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);

                SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

                try
                {
                    foreach (DbDataRecord _record in _reader)
                    {
                        yield return _record;
                    }
                }
                finally
                {
                    _reader.Close();
                }
            }
        #endregion

        #region Write Functions
            internal Int64 Create(Int64 idFormula, DateTime creationDate, Int64 idMeasurementUnit, Int16 Frequency, Int64 IdTimeUnitFrequency, Boolean isRelevant, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Calculations_Create");
                _db.AddInParameter(_dbCommand, "idFormula", DbType.Int64, idFormula);
                _db.AddInParameter(_dbCommand, "creationDate", DbType.DateTime, creationDate);
                _db.AddInParameter(_dbCommand, "idMeasurementUnit", DbType.Int64, idMeasurementUnit);
                _db.AddInParameter(_dbCommand, "Frequency", DbType.Int16, Frequency);
                _db.AddInParameter(_dbCommand, "IdTimeUnitFrequency", DbType.Int64, IdTimeUnitFrequency);
                _db.AddInParameter(_dbCommand, "IsRelevant", DbType.Boolean, isRelevant);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdCalculation", DbType.Int64, 18);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdCalculation"));
            }
            internal void Delete(Int64 idCalculation,  Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Calculations_Delete");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idCalculation, Int64 idMeasurementUnit, Int16 Frequency, Int64 IdTimeUnitFrequency, Boolean isRelevant, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Calculations_Update");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "idMeasurementUnit", DbType.Int64, idMeasurementUnit);
                _db.AddInParameter(_dbCommand, "Frequency", DbType.Int16, Frequency);
                _db.AddInParameter(_dbCommand, "IdTimeUnitFrequency", DbType.Int64, IdTimeUnitFrequency);
                _db.AddInParameter(_dbCommand, "IsRelevant", DbType.Boolean, isRelevant);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idCalculation, Decimal lastResult, DateTime dateLastResult, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Calculations_UpdateLastResult");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "LastResult", DbType.Decimal, lastResult);
                _db.AddInParameter(_dbCommand, "DateLastResult", DbType.DateTime, dateLastResult);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void CreateHistoryResult(Int64 idCalculation, DateTime timeStamp, Decimal result, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationResultHistories_Create");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "timeStamp", DbType.DateTime, timeStamp);
                _db.AddInParameter(_dbCommand, "result", DbType.Decimal, result);
                //_db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void DeleteHistoryResult(Int64 idCalculation)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationResultHistories_Delete");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }

            #region AssociatedProjets
                internal void CreateCalculationProcessGroupPrjects(Int64 idCalculation, Int64 idProcess)
                {
                    Database _db = DatabaseFactory.CreateDatabase();

                    DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationProcessGroupProcesses_Create");
                    _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                    _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
                    //Ejecuta el comando
                    _db.ExecuteNonQuery(_dbCommand);
                }
                internal void DeleteCalculationProcessGroupPrjects(Int64 idCalculation, Int64 idProcess)
                {
                    Database _db = DatabaseFactory.CreateDatabase();

                    DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationProcessGroupProcesses_Delete");
                    _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                    _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);

                    //Ejecuta el comando
                    _db.ExecuteNonQuery(_dbCommand);
                }

                internal void DeleteCalculationProcessGroupPrjects(Int64 idCalculation)
                {
                    Database _db = DatabaseFactory.CreateDatabase();

                    DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationProcessGroupProcesses_DeleteByCalculation");
                    _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);

                    //Ejecuta el comando
                    _db.ExecuteNonQuery(_dbCommand);
                }
            #endregion
        #endregion

        #region Calculate Functions

            //internal IEnumerable<DbDataRecord> Calculate(String spName, Dictionary<String, Int64> parameters, DateTime startDate, DateTime endDate)
            //{
            //    Database _db = DatabaseFactory.CreateDatabase();
            //    DbCommand _dbCommand = _db.GetStoredProcCommand(spName);
            //    //Crear el objeto para meter todos los parametros necesarios para el SP.
            //    DbParameter _dbParameters = _dbCommand.CreateParameter();

            //    DataTable _dtParams = new DataTable();
            //    _dtParams.TableName = "spParams";
            //    _dtParams.Columns.Add("ParameterOrder");
            //    _dtParams.Columns.Add("ParameterName");
            //    _dtParams.Columns.Add("DataType");

            //    _dtParams = GetStoredProcedureParameters(spName);

            //    foreach (DataRow dr in _dtParams.Rows)
            //    {
            //        _dbParameters = _dbCommand.CreateParameter();

            //        _dbParameters.ParameterName = dr["ParameterName"].ToString();
            //        _dbParameters.DbType = GetDBType(dr["DataType"].ToString());
            //        //Son parametros que estan fuera del dictionary, entonces los busco y asigno manualmente.
            //        if (_dbParameters.ParameterName == "FilterStartDate")
            //        {
            //            _dbParameters.Value = startDate; 
            //        }
            //        else
            //        {
            //            if (_dbParameters.ParameterName == "FilterEndDate")
            //            {
            //                _dbParameters.Value = endDate; 
            //            }
            //            else
            //            {
            //                _dbParameters.Value = parameters[_dbParameters.ParameterName];
            //            }
            //        }                    

            //        _dbCommand.Parameters.Add(_dbParameters);
            //    }
            //    //Ejecuta el comando
            //    SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);
            //    try
            //    {
            //        foreach (DbDataRecord _record in _reader)
            //        {
            //            yield return _record;
            //        }
            //    }
            //    finally
            //    {
            //        _reader.Close();
            //    }

            //}
            //internal IEnumerable<DbDataRecord> Calculate(String spName, Dictionary<String, Int64> parameters)
            //{
            //    Database _db = DatabaseFactory.CreateDatabase();
            //    DbCommand _dbCommand = _db.GetStoredProcCommand(spName);
            //    //Crear el objeto para meter todos los parametros necesarios para el SP.
            //    DbParameter _dbParameters = _dbCommand.CreateParameter();

            //    DataTable _dtParams = new DataTable();
            //    _dtParams.TableName = "spParams";
            //    _dtParams.Columns.Add("ParameterOrder");
            //    _dtParams.Columns.Add("ParameterName");
            //    _dtParams.Columns.Add("DataType");

            //    _dtParams = GetStoredProcedureParameters(spName);

            //    foreach (DataRow dr in _dtParams.Rows)
            //    {
            //        _dbParameters = _dbCommand.CreateParameter();

            //        _dbParameters.ParameterName = dr["ParameterName"].ToString();
            //        _dbParameters.DbType = GetDBType(dr["DataType"].ToString());
            //        //Son parametros que estan fuera del dictionary, entonces los busco y asigno manualmente.
            //        if (_dbParameters.ParameterName == "FilterStartDate")
            //        {
            //            _dbParameters.Value = DBNull.Value;
            //        }
            //        else
            //        {
            //            if (_dbParameters.ParameterName == "FilterEndDate")
            //            {
            //                _dbParameters.Value = DBNull.Value;
            //            }
            //            else
            //            {
            //                _dbParameters.Value = parameters[_dbParameters.ParameterName];
            //            }
            //        }          

            //        _dbCommand.Parameters.Add(_dbParameters);
            //    }
            //    //Ejecuta el comando
            //    SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);
            //    try
            //    {
            //        foreach (DbDataRecord _record in _reader)
            //        {
            //            yield return _record;
            //        }
            //    }
            //    finally
            //    {
            //        _reader.Close();
            //    }

            //}

            internal Decimal Calculate(String spName, Dictionary<String, Int64> parameters, DateTime startDate, DateTime endDate)
            {
                Database _db = DatabaseFactory.CreateDatabase();
                DbCommand _dbCommand = _db.GetStoredProcCommand(spName);
                //Crear el objeto para meter todos los parametros necesarios para el SP.
                DbParameter _dbParameters = _dbCommand.CreateParameter();

                DataTable _dtParams = new DataTable();
                _dtParams.TableName = "spParams";
                _dtParams.Columns.Add("ParameterOrder");
                _dtParams.Columns.Add("ParameterName");
                _dtParams.Columns.Add("DataType");

                _dtParams = GetStoredProcedureParameters(spName);

                foreach (DataRow dr in _dtParams.Rows)
                {
                    _dbParameters = _dbCommand.CreateParameter();

                    _dbParameters.ParameterName = dr["ParameterName"].ToString();
                    _dbParameters.DbType = GetDBType(dr["DataType"].ToString());
                    //Son parametros que estan fuera del dictionary, entonces los busco y asigno manualmente.
                    if (_dbParameters.ParameterName == "FilterStartDate")
                    {
                        _dbParameters.Value = startDate;
                    }
                    else
                    {
                        if (_dbParameters.ParameterName == "FilterEndDate")
                        {
                            _dbParameters.Value = endDate;
                        }
                        else
                        {
                            _dbParameters.Value = parameters[_dbParameters.ParameterName];
                        }
                    }

                    _dbCommand.Parameters.Add(_dbParameters);
                }

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "ResultOut", DbType.Decimal, 0);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                if (_db.GetParameterValue(_dbCommand, "ResultOut") != DBNull.Value)
                {
                    return Convert.ToDecimal(_db.GetParameterValue(_dbCommand, "ResultOut"));
                }
                else
                {
                    return 0;
                }

            }
            internal Decimal Calculate(String spName, Dictionary<String, Int64> parameters)
            {
                Database _db = DatabaseFactory.CreateDatabase();
                DbCommand _dbCommand = _db.GetStoredProcCommand(spName);
                //Crear el objeto para meter todos los parametros necesarios para el SP.
                DbParameter _dbParameters = _dbCommand.CreateParameter();

                DataTable _dtParams = new DataTable();
                _dtParams.TableName = "spParams";
                _dtParams.Columns.Add("ParameterOrder");
                _dtParams.Columns.Add("ParameterName");
                _dtParams.Columns.Add("DataType");

                _dtParams = GetStoredProcedureParameters(spName);

                foreach (DataRow dr in _dtParams.Rows)
                {
                    _dbParameters = _dbCommand.CreateParameter();

                    _dbParameters.ParameterName = dr["ParameterName"].ToString();
                    _dbParameters.DbType = GetDBType(dr["DataType"].ToString());
                    //Son parametros que estan fuera del dictionary, entonces los busco y asigno manualmente.
                    if (_dbParameters.ParameterName == "FilterStartDate")
                    {
                        _dbParameters.Value = DBNull.Value;
                    }
                    else
                    {
                        if (_dbParameters.ParameterName == "FilterEndDate")
                        {
                            _dbParameters.Value = DBNull.Value;
                        }
                        else
                        {
                            _dbParameters.Value = parameters[_dbParameters.ParameterName];
                        }
                    }

                    _dbCommand.Parameters.Add(_dbParameters);
                }

                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "ResultOut", DbType.Decimal, 0);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                if (_db.GetParameterValue(_dbCommand, "ResultOut") != DBNull.Value)
                {
                    return Convert.ToDecimal(_db.GetParameterValue(_dbCommand, "ResultOut"));
                }
                else
                {
                    return 0;
                }

            }

            private DbType GetDBType(String dataType)
            {
                switch (dataType.ToLower())
                {
                    case "numeric":
                        return DbType.Int64;
                    case "datetime":
                        return DbType.DateTime;
                    default:
                        return DbType.String;
                }
            }
            private DataTable GetStoredProcedureParameters(String spName)
            {
                DataTable _dtParams = new DataTable();
                _dtParams.TableName = "spParams";
                _dtParams.Columns.Add("ParameterOrder");
                _dtParams.Columns.Add("ParameterName");
                _dtParams.Columns.Add("DataType");

                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_FormulaStoredProcedureParameters_GetAll");
                _db.AddInParameter(_dbCommand, "StoredProcedureName", DbType.String, spName);
                SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

                try
                {
                    foreach (DbDataRecord _record in _reader)
                    {
                        if (Convert.ToString(_record["ParameterMode"]) == "IN")
                            { _dtParams.Rows.Add(_record["ParameterOrder"], _record["ParameterName"], _record["DataType"]); }
                    }
                }
                finally
                {
                    _reader.Close();
                }

                return _dtParams;
            }
        #endregion
    }
}
