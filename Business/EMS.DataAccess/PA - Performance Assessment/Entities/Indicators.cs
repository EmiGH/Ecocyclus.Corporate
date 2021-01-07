using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class Indicators
    {
        internal Indicators()
        { }


        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_ReadAll");               
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
            internal IEnumerable<DbDataRecord> ReadByClassification(Int64 idIndicatorClassification, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_ReadByClassification");
                _db.AddInParameter(_dbCommand, "IdIndicatorClassification", DbType.Int64, idIndicatorClassification);
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
            internal IEnumerable<DbDataRecord> ReadById(Int64 idIndicator, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_ReadById");
                _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
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
            //trae todos los que no tiene classificacion o su padre no tiene permisos y el si
            internal IEnumerable<DbDataRecord> ReadRoot(String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_ReadRoot");
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
        #endregion

        #region Write Functions
            internal Int64 Create(Int64 idMagnitud, Boolean IsCumulative, String name, String description, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_Create");
                _db.AddInParameter(_dbCommand, "IdMagnitud", DbType.Int64, idMagnitud);
                _db.AddInParameter(_dbCommand, "IsCumulative", DbType.Boolean, IsCumulative);               
                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdIndicator", DbType.Int64, 18);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdIndicator"));
            }
            internal void Delete(Int64 idIndicator)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_Delete");
                _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idIndicator, Int64 idMagnitud)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Indicators_Update");
                _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
                _db.AddInParameter(_dbCommand, "IdMagnitud", DbType.Int64, idMagnitud);
              

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }

           

        #endregion

        #region IndicatorClassificationIndicator

                internal void Create(Int64 idIndicator, Int64 idIndicatorClassification)
                {
                    Database _db = DatabaseFactory.CreateDatabase();

                    DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassificationIndicators_Create");
                    _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
                    _db.AddInParameter(_dbCommand, "IdIndicatorClassification", DbType.Int64, idIndicatorClassification);
                    //Ejecuta el comando
                    _db.ExecuteNonQuery(_dbCommand);

                }
                internal void Delete(Int64 idIndicator, Int64 idIndicatorClassification)
                {
                    Database _db = DatabaseFactory.CreateDatabase();

                    DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassificationIndicators_Delete");
                    _db.AddInParameter(_dbCommand, "idIndicator", DbType.Int64, idIndicator);
                    _db.AddInParameter(_dbCommand, "idIndicatorClassification", DbType.Int64, idIndicatorClassification);

                    //Ejecuta el comando
                    _db.ExecuteNonQuery(_dbCommand);
                }
                internal void DeleteByIndicator(Int64 idIndicator)
                {
                    Database _db = DatabaseFactory.CreateDatabase();

                    DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassificationIndicators_DeleteByIndicator");
                    _db.AddInParameter(_dbCommand, "idIndicator", DbType.Int64, idIndicator);

                    //Ejecuta el comando
                    _db.ExecuteNonQuery(_dbCommand);
                }
                internal void DeleteByClassification(Int64 idIndicatorClassification)
                {
                    Database _db = DatabaseFactory.CreateDatabase();

                    DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassificationIndicators_DeleteByClassification");
                    _db.AddInParameter(_dbCommand, "idIndicatorClassification", DbType.Int64, idIndicatorClassification);

                    //Ejecuta el comando
                    _db.ExecuteNonQuery(_dbCommand);
                }
                #endregion
    }
}
