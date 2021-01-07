using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class CalculationCertificates
    {
        internal CalculationCertificates()
        { }

        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadById(Int64 idCalculation, Int64 idCertificated)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationCertificated_ReadById");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "IdCertificated", DbType.Int64, idCertificated);
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
            internal IEnumerable<DbDataRecord> ReadByCalculation(Int64 idCalculation)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationCertificated_ReadAll");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
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
            internal Int64 Create(Int64 idCalculation, DateTime startDate, DateTime endDate, Decimal value, Int64 idOrganizationDOE, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationCertificated_Create");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _db.AddInParameter(_dbCommand, "Value", DbType.Decimal, value);
                _db.AddInParameter(_dbCommand, "IdOrganizationDoE", DbType.Int64, idOrganizationDOE);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdCertificated", DbType.Int64, 18);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdCertificated"));
            }
            internal void Delete(Int64 idCalculation, Int64 idCertificated, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationCertificated_Delete");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "idCertificated", DbType.Int64, idCertificated);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Update(Int64 idCalculation, Int64 idCertificated, DateTime startDate, DateTime endDate, Decimal value, Int64 idOrganizationDOE, Int64 idLogPerson)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationCertificated_Update");
                _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
                _db.AddInParameter(_dbCommand, "idCertificated", DbType.Int64, idCertificated);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);
                _db.AddInParameter(_dbCommand, "Value", DbType.Decimal, value);
                _db.AddInParameter(_dbCommand, "IdOrganizationDoE", DbType.Int64, idOrganizationDOE);
                _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

    }
}
