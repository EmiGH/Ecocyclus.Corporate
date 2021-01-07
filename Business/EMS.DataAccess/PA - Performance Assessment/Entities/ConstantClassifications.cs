using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class ConstantClassifications
    {
        internal ConstantClassifications()
        {}

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadById(Int64 idConstantClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConstantClassifications_ReadById");
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
        internal IEnumerable<DbDataRecord> ReadRoot(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConstantClassifications_ReadRoot");
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
        internal IEnumerable<DbDataRecord> ReadByConstantClassification(Int64 idParentConstantClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConstantClassifications_ReadByParent");
            _db.AddInParameter(_dbCommand, "IdParentConstantClassification", DbType.Int64, idParentConstantClassification);
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
        internal Int64 Create(Int64 idParentConstantClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConstantClassifications_Create");
            _db.AddInParameter(_dbCommand, "IdParentConstantClassification", DbType.Int64, Common.Common.CastValueToNull(idParentConstantClassification, DBNull.Value));
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdConstantClassification", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdConstantClassification"));
        }

        /// <summary>
        /// Borra una clasificacion de una Organizacion
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdClassification"></param>
        /// <param name="IdLogPerson"></param>
        internal void Delete(Int64 idConstantClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConstantClassifications_Delete");
            _db.AddInParameter(_dbCommand, "IdConstantClassification", DbType.Int64, idConstantClassification);

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
        internal void Update(Int64 idConstantClassification, Int64 idParentConstantClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_ConstantClassifications_Update");
            _db.AddInParameter(_dbCommand, "IdConstantClassification", DbType.Int64, idConstantClassification);
            _db.AddInParameter(_dbCommand, "IdParentConstantClassification", DbType.Int64, Common.Common.CastValueToNull(idParentConstantClassification, DBNull.Value));

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
