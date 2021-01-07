using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class IndicatorClassifications
    {
        internal IndicatorClassifications()
        {}

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_ReadAll");
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
        
        internal IEnumerable<DbDataRecord> ReadById(Int64 idIndicatorClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_ReadById");
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

        internal IEnumerable<DbDataRecord> ReadRoot(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_ReadRoot");
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

        internal IEnumerable<DbDataRecord> ReadByClassification(Int64 idIndicatorClassificationsParent, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_ReadByParent");
            _db.AddInParameter(_dbCommand, "IdParentIndicatorClassification", DbType.Int64, idIndicatorClassificationsParent);
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
        internal IEnumerable<DbDataRecord> ReadByIndicator(Int64 idIndicator, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_ReadByIndicator");
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
        internal Int64 Create(Int64 idParentIndicatorClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_Create");
            _db.AddInParameter(_dbCommand, "IdParentIndicatorClassification", DbType.Int64, Common.Common.CastValueToNull(idParentIndicatorClassification, DBNull.Value));
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdIndicatorClassification", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdIndicatorClassification"));
        }
  
        /// <summary>
        /// Borra una clasificacion de una Organizacion
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdClassification"></param>
        /// <param name="IdLogPerson"></param>
        internal void Delete(Int64 idIndicatorClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_Delete");
            _db.AddInParameter(_dbCommand, "IdIndicatorClassification", DbType.Int64, idIndicatorClassification);

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
        internal void Update(Int64 idIndicatorClassification, Int64 idParentIndicatorClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_Update");
            _db.AddInParameter(_dbCommand, "IdIndicatorClassification", DbType.Int64, idIndicatorClassification);
            _db.AddInParameter(_dbCommand, "IdParentIndicatorClassification", DbType.Int64, Common.Common.CastValueToNull(idParentIndicatorClassification, DBNull.Value));

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

    }
}
