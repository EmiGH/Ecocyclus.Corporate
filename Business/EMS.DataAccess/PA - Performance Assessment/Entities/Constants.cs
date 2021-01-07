using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class Constants
    {
        internal Constants()
        { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadById(Int64 idConstant, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Constants_ReadById");
            _db.AddInParameter(_dbCommand, "IdConstant", DbType.Int64, idConstant);
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
        internal IEnumerable<DbDataRecord> ReadByConstantClassification(Int64 idConstantClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Constants_ReadByConstantClassification");
            _db.AddInParameter(_dbCommand, "IdConstantClassification", DbType.Int64, idConstantClassification);
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
        /// <summary>
        /// Realiza el alta de una classificacion 
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdLanguage"></param>
        /// <param name="name"></param>
        /// <param name="Description"></param>
        /// <param name="IdLogPerson"></param>
        /// <returns>Retorna el id de la clasificacion creada</returns>
        internal Int64 Create(String symbol, Double value, Int64 IdMeasurementUnit, Int64 idConstantClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Constants_Create");
            _db.AddInParameter(_dbCommand, "@symbol", DbType.String, Common.Common.CastValueToNull(symbol, DBNull.Value));
            _db.AddInParameter(_dbCommand, "@value", DbType.Double, value);
            _db.AddInParameter(_dbCommand, "@IdMeasurementUnit", DbType.Int64, Common.Common.CastValueToNull(IdMeasurementUnit, DBNull.Value));
            _db.AddInParameter(_dbCommand, "@IdConstantClassification", DbType.Int64, idConstantClassification);
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdConstant", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdConstant"));
        }

        /// <summary>
        /// Borra una clasificacion de una Organizacion
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdClassification"></param>
        /// <param name="IdLogPerson"></param>
        internal void Delete(Int64 idConstant)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Constants_Delete");
            _db.AddInParameter(_dbCommand, "IdConstant", DbType.Int64, idConstant);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        /// <summary>
        /// Modifica el nombre o la descripcion de una classificacion pero usa el de SP de LG 
        /// </summary>
        /// <param name="IdClassification"></param>
        /// <param name="IdOrganization"></param>
        /// <param name="IdLanguage"></param>
        /// <param name="name"></param>
        /// <param name="Description"></param>
        /// <param name="IdLogPerson"></param>
        internal void Update(Int64 idConstant, String symbol, Double value, Int64 IdMeasurementUnit, Int64 idConstantClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Constants_Update");
            _db.AddInParameter(_dbCommand, "IdConstant", DbType.Int64, idConstant);
            _db.AddInParameter(_dbCommand, "symbol", DbType.String, Common.Common.CastValueToNull(symbol, DBNull.Value));
            _db.AddInParameter(_dbCommand, "value", DbType.Double, value);
            _db.AddInParameter(_dbCommand, "IdMeasurementUnit", DbType.Int64, Common.Common.CastValueToNull(IdMeasurementUnit, DBNull.Value));
            _db.AddInParameter(_dbCommand, "@IdConstantClassification", DbType.Int64, idConstantClassification);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

    }
}
