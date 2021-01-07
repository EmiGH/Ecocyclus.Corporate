using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class Methodologies
    {
        internal Methodologies()
        { 
        }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadById(Int64 idMethodology, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Methodologies_ReadById");
            _db.AddInParameter(_dbCommand, "IdMethodology", DbType.Int64, idMethodology);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Methodologies_ReadAll");
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
        internal Int64 Create(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Methodologies_Create");
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, Common.Common.CastValueToNull(idResource, DBNull.Value));
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdMethodology", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdMethodology"));
        }

        /// <summary>
        /// Borra una clasificacion de una Organizacion
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdClassification"></param>
        /// <param name="IdLogPerson"></param>
        internal void Delete(Int64 idMethodology)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Methodologies_Delete");
            _db.AddInParameter(_dbCommand, "IdMethodology", DbType.Int64, idMethodology);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idMethodology, Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_Methodologies_Update");
            _db.AddInParameter(_dbCommand, "IdMethodology", DbType.Int64, idMethodology);
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, Common.Common.CastValueToNull(idResource, DBNull.Value));

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

    }
}
