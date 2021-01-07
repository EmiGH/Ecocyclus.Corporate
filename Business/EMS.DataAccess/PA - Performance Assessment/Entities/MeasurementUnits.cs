using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class MeasurementUnits
    {
        internal MeasurementUnits()
        { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idMagnitud, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementUnits_ReadAll");
            _db.AddInParameter(_dbCommand, "IdMagnitud", DbType.Int64, idMagnitud);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idMeasurementUnit, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementUnits_ReadById");
            _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, idMeasurementUnit);
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
        internal Int64 Create(Int64 numerator, Int64 denominator, Int64 exponent, Decimal constant, Boolean isPattern, Int64 idMagnitud, String name, String description, String idLanguage, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementUnits_Create");
            _db.AddInParameter(_dbCommand, "Numerator", DbType.Int64, numerator);
            _db.AddInParameter(_dbCommand, "Denominator", DbType.Int64, denominator);
            _db.AddInParameter(_dbCommand, "Exponent", DbType.Int64, exponent);
            _db.AddInParameter(_dbCommand, "Constant", DbType.Double, constant);
            _db.AddInParameter(_dbCommand, "IsPattern", DbType.Boolean, isPattern);
            _db.AddInParameter(_dbCommand, "IdMagnitud", DbType.Int64, idMagnitud);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdMeasurementUnit"));
        }


        internal void Delete(Int64 idMeasurementUnit, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementUnits_Delete");
            _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, idMeasurementUnit);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 IdMagnitud)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementUnits_DeleteByMagnitud");
            _db.AddInParameter(_dbCommand, "IdMagnitud", DbType.Int64, IdMagnitud);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }



        internal void Update(Int64 idMeasurementUnit, Int64 numerator, Int64 denominator, Int64 exponent, Decimal constant, Boolean isPattern, Int64 idMagnitud, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementUnits_Update");
            _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, idMeasurementUnit);
            _db.AddInParameter(_dbCommand, "Numerator", DbType.Int64, numerator);
            _db.AddInParameter(_dbCommand, "Denominator", DbType.Int64, denominator);
            _db.AddInParameter(_dbCommand, "Exponent", DbType.Int64, exponent);
            _db.AddInParameter(_dbCommand, "Constant", DbType.Double, constant);
            _db.AddInParameter(_dbCommand, "IsPattern", DbType.Boolean, isPattern);
            _db.AddInParameter(_dbCommand, "IdMagnitud", DbType.Int64, idMagnitud);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
