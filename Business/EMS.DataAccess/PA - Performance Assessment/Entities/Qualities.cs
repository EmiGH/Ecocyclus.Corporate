using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class Qualities
    {
        internal Qualities()
        { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadById(Int64 idQuality, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Qualities_ReadById");
            _db.AddInParameter(_dbCommand, "IdQuality", DbType.Int64, idQuality);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Qualities_ReadAll");
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
        internal Int64 Create()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Qualities_Create");
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdQuality", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdQuality"));
        }

        /// <summary>
        /// Borra una clasificacion de una Organizacion
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdClassification"></param>
        /// <param name="IdLogPerson"></param>
        internal void Delete(Int64 idQuality)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Qualities_Delete");
            _db.AddInParameter(_dbCommand, "IdQuality", DbType.Int64, idQuality);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion
    }
}
