using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class Formulas
    {
          internal Formulas()
        { }


        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Formulas_ReadAll");
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

            internal IEnumerable<DbDataRecord> ReadByIndicator(Int64 IdIndicator, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Formulas_ReadByIndicator");
                _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, IdIndicator);
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
            internal IEnumerable<DbDataRecord> ReadById(Int64 idFormula, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Formulas_ReadById");
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

            internal IEnumerable<DbDataRecord> HasCalculation(Int64 idFormula)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Formulas_HasCalculation");
                _db.AddInParameter(_dbCommand, "idFormula", DbType.Int64, idFormula);
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
            internal Int64 Create(DateTime creationDate, String literalFormula, String schemaSP, Int64 idIndicator, Int64 idMeasurementUnit, Int64 idResourceVersion, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Formulas_Create");
                _db.AddInParameter(_dbCommand, "creationDate", DbType.DateTime, creationDate);
                _db.AddInParameter(_dbCommand, "literalFormula", DbType.String, literalFormula);
                _db.AddInParameter(_dbCommand, "schemaSP", DbType.String, schemaSP);
                _db.AddInParameter(_dbCommand, "idIndicator", DbType.Int64, idIndicator);
                _db.AddInParameter(_dbCommand, "idMeasurementUnit", DbType.Int64, idMeasurementUnit);
                //_db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
                //_db.AddInParameter(_dbCommand, "Name", DbType.String, name);
                _db.AddInParameter(_dbCommand, "IdResourceVersion", DbType.Int64, Common.Common.CastValueToNull(idResourceVersion, 0));
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "idFormula", DbType.Int64, 18);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "idFormula"));
            }
            internal void Delete(Int64 idFormula, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Formulas_Delete");
                _db.AddInParameter(_dbCommand, "idFormula", DbType.Int64, idFormula);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idFormula, String literalFormula, Int64 idResourceVersion, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Formulas_Update");
                _db.AddInParameter(_dbCommand, "IdFormula", DbType.Int64, idFormula);
                _db.AddInParameter(_dbCommand, "literalFormula", DbType.String, literalFormula);
                _db.AddInParameter(_dbCommand, "IdResourceVersion", DbType.Int64, Common.Common.CastValueToNull(idResourceVersion,0));
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        
        #endregion
    }
}
