using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class ParameterGroups
    {
        internal ParameterGroups()
        { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idIndicator, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterGroups_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadByMeasurement(Int64 idMeasurement, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterGroups_ReadByMeasurement");
            _db.AddInParameter(_dbCommand, "idMeasurement", DbType.Int64, idMeasurement);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idParameterGroup, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterGroups_ReadById");
            _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.Int64, idParameterGroup);
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
        internal Int64 Create(Int64 idIndicator, String name, String description, String idLanguage, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterGroups_Create");
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdParameterGroup", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdParameterGroup"));
        }


        internal void Delete(Int64 idParameterGroup, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterGroups_Delete");
            _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.Int64, idParameterGroup);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idIndicator)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterGroups_DeleteByIndicator");
            _db.AddInParameter(_dbCommand, "idIndicator", DbType.Int64, idIndicator);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }


        internal void Update(Int64 idParameterGroup, Int64 idIndicator, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ParameterGroups_Update");
            _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.Int64, idParameterGroup);
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

        #region Write Functions relationship measurement
        internal void Create(Int64 idMeasurement, Int64 idParameterGroup, Int64 idIndicator)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementParameterGroups_Create");
            _db.AddInParameter(_dbCommand, "idMeasurement", DbType.Int64, idMeasurement);
            _db.AddInParameter(_dbCommand, "idParameterGroup", DbType.Int64, idParameterGroup);
            _db.AddInParameter(_dbCommand, "IdIndicator", DbType.Int64, idIndicator);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }


        internal void DeleteByPGP(Int64 idParameterGroup, Int64 idIndicator)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementParameterGroups_DeleteByPGP");
            _db.AddInParameter(_dbCommand, "IdParameterGroup", DbType.Int64, idParameterGroup);
            _db.AddInParameter(_dbCommand, "idIndicator", DbType.Int64, idIndicator);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void DeleteByMeasurement(Int64 idMeasurement)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_MeasurementParameterGroups_Delete");
            _db.AddInParameter(_dbCommand, "idMeasurement", DbType.Int64, idMeasurement);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }


        #endregion
    }
}
