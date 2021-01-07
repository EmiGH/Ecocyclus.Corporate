using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;


namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class CalculationScenarioTypesProcessClassification
    {
        internal CalculationScenarioTypesProcessClassification() { }

        #region Read Functions
            //Trae todos los types
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idProcessClassification)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassificationCalculationScenarioTypes_ReadAll");
                _db.AddInParameter(_dbCommand, "idProcessClassification", DbType.Int64, idProcessClassification);
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
            internal IEnumerable<DbDataRecord> ReadById(Int64 idProcessClassification, Int64 idScenarioType)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassificationCalculationScenarioTypes_ReadById");
                _db.AddInParameter(_dbCommand, "idProcessClassification", DbType.Int64, idProcessClassification);
                _db.AddInParameter(_dbCommand, "idScenarioType", DbType.Int64, idScenarioType);
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
            internal IEnumerable<DbDataRecord> ReadByType(Int64 idScenarioType)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassificationCalculationScenarioTypes_ReadByType");
                _db.AddInParameter(_dbCommand, "idScenarioType", DbType.Int64, idScenarioType);
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
            internal void Create(Int64 idProcessClassification, Int64 idScenarioType, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassificationCalculationScenarioTypes_Create");
                _db.AddInParameter(_dbCommand, "idProcessClassification", DbType.Int64, idProcessClassification);
                _db.AddInParameter(_dbCommand, "idScenarioType", DbType.Int64, idScenarioType);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
                
            }
            internal void Delete(Int64 idProcessClassification, Int64 idScenarioType, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassificationCalculationScenarioTypes_Delete");
                _db.AddInParameter(_dbCommand, "idProcessClassification", DbType.Int64, idProcessClassification);
                _db.AddInParameter(_dbCommand, "idScenarioType", DbType.Int64, idScenarioType);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(Int64 idProcessClassification, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassificationCalculationScenarioTypes_DeleteAll");
                _db.AddInParameter(_dbCommand, "idProcessClassification", DbType.Int64, idProcessClassification);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void DeleteRelatedClassification(Int64 idScenarioType, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassificationCalculationScenarioTypes_DeleteByScenarioType");
                _db.AddInParameter(_dbCommand, "idScenarioType", DbType.Int64, idScenarioType);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

    }
}
