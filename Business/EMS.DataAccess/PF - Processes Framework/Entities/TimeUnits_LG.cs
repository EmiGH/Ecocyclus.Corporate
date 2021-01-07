using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class TimeUnits_LG
    {
        internal TimeUnits_LG()
        {
        }

        #region Write Functions

        internal void Create(Int64 idTimeUnit, String idLanguage, String name , Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_TimeUnits_LG_Create");
            _db.AddInParameter(_dbCommand, "IdTimeUnit", DbType.Int64, idTimeUnit);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        /// <summary>
        /// Borra un aopcion de idioma
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdTimeUnit"></param>
        /// <param name="IdLanguage"></param>
        /// <param name="IdLogPerson"></param>
        internal void Delete( Int64 idTimeUnit, String idLanguage, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_TimeUnits_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdTimeUnit", DbType.String, idTimeUnit);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        /// <summary>
        /// Modifica una opcion de idioma 
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdTimeUnit"></param>
        /// <param name="IdLanguage"></param>
        /// <param name="name"></param>
        /// <param name="Description"></param>
        /// <param name="IdLogPerson"></param>
        internal void Update( Int64 idTimeUnit, String idLanguage, String name, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_TimeUnits_LG_Update");
            _db.AddInParameter(_dbCommand, "IdTimeUnit", DbType.Int64, idTimeUnit);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

        #region Read Functions

        /// <summary>
        /// Devuelve todas las opciones de idioma para una ExtendedProperty
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdTimeUnit"></param>
        /// <returns></returns>
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idTimeUnit)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_TimeUnits_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "IdTimeUnit", DbType.Int64, idTimeUnit);
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

        /// <summary>
        /// devuelve la opcion de idioma pedida
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdTimeUnit"></param>
        /// <param name="IdLanguage"></param>
        /// <returns></returns>
        internal IEnumerable<DbDataRecord> ReadById( Int64 idTimeUnit, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_TimeUnits_LG_ReadById");
            _db.AddInParameter(_dbCommand, "IdTimeUnit", DbType.Int64, idTimeUnit);
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
    }
}
