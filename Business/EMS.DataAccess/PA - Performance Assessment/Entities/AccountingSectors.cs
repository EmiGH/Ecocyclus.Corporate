using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class AccountingSectors
    {
        internal AccountingSectors()
        { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadById(Int64 idSector, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingSectors_ReadById");
            _db.AddInParameter(_dbCommand, "IdSector", DbType.Int64, idSector);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingSectors_ReadRoot");
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
        internal IEnumerable<DbDataRecord> ReadBySector(Int64 idParentSector, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingSectors_ReadByParent");
            _db.AddInParameter(_dbCommand, "IdParentSector", DbType.Int64, idParentSector);
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
        internal Int64 Create(Int64 idParentSector)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingSectors_Create");
            _db.AddInParameter(_dbCommand, "IdParentSector", DbType.Int64, Common.Common.CastValueToNull(idParentSector, DBNull.Value));
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdSector", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdSector"));
        }

        /// <summary>
        /// Borra una clasificacion de una Organizacion
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdClassification"></param>
        /// <param name="IdLogPerson"></param>
        internal void Delete(Int64 idSector)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingSectors_Delete");
            _db.AddInParameter(_dbCommand, "IdSector", DbType.Int64, idSector);

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
        internal void Update(Int64 idSector, Int64 idParentSector)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingSectors_Update");
            _db.AddInParameter(_dbCommand, "IdSector", DbType.Int64, idSector);
            _db.AddInParameter(_dbCommand, "IdParentSector", DbType.Int64, Common.Common.CastValueToNull(idParentSector, DBNull.Value));

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
