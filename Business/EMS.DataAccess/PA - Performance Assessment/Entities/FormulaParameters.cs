using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class FormulaParameters
    {
        internal FormulaParameters()
        { }


        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idFormula)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_FormulaParameters_ReadAll");
                _db.AddInParameter(_dbCommand, "idFormula", DbType.String, idFormula);
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
            internal void Create(Int64 idFormula, Int64 positionParameter, Int64 idIndicator, Int64 idMeasurementUnit, String parameterName)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_FormulaParameters_Create");
                _db.AddInParameter(_dbCommand, "idFormula", DbType.Int64, idFormula);
                _db.AddInParameter(_dbCommand, "positionParameter", DbType.Int64, positionParameter);               
                _db.AddInParameter(_dbCommand, "idIndicator", DbType.Int64, idIndicator);
                _db.AddInParameter(_dbCommand, "idMeasurementUnit", DbType.Int64, idMeasurementUnit);
                _db.AddInParameter(_dbCommand, "ParameterName", DbType.String, parameterName);
                
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(Int64 idFormula, Int64 positionParameter, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_FormulaParameters_Delete");
                _db.AddInParameter(_dbCommand, "idFormula", DbType.Int64, idFormula);
                _db.AddInParameter(_dbCommand, "positionParameter", DbType.Int64, positionParameter);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idFormula, Int64 positionParameter, Int64 idIndicator, Int64 idMeasurementUnit,String parameterName, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_FormulaParameters_Update");
                _db.AddInParameter(_dbCommand, "idFormula", DbType.Int64, idFormula);
                _db.AddInParameter(_dbCommand, "positionParameter", DbType.Int64, positionParameter);
                _db.AddInParameter(_dbCommand, "idIndicator", DbType.Int64, idIndicator);
                _db.AddInParameter(_dbCommand, "idMeasurementUnit", DbType.Int64, idMeasurementUnit);
                _db.AddInParameter(_dbCommand, "parameterName", DbType.String, parameterName);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        
        #endregion
    }
}
