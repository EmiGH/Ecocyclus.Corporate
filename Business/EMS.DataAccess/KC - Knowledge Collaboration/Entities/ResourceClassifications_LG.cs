using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.KC.Entities
{
    internal class ResourceClassifications_LG
    {
    internal ResourceClassifications_LG()

        {           
        }

        #region Write Functions
        
        /// <summary>
        /// Realiza el Add de una opcion de idioma para una clasificacion
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdClassification"></param>
        /// <param name="IdLanguage"></param>
        /// <param name="name"></param>
        /// <param name="Description"></param>
        /// <param name="IdLogPerson"></param>
        internal void Create(Int64 idResourceClassification, String idLanguage, String name, String Description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceClassifications_LG_Create");
            _db.AddInParameter(_dbCommand, "IdResourceClassification", DbType.Int64, idResourceClassification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        /// <summary>
        /// Borra un aopcion de idioma
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdClassification"></param>
        /// <param name="IdLanguage"></param>
        /// <param name="IdLogPerson"></param>
        internal void Delete(Int64 idResourceClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceClassifications_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdResourceClassification", DbType.Int64, idResourceClassification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idResourceClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceClassifications_LG_DeleteByResourceClassification");
            _db.AddInParameter(_dbCommand, "IdResourceClassification", DbType.Int64, idResourceClassification);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        /// <summary>
        /// Modifica una opcion de idioma 
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdClassification"></param>
        /// <param name="IdLanguage"></param>
        /// <param name="name"></param>
        /// <param name="Description"></param>
        /// <param name="IdLogPerson"></param>
        internal void Update(Int64 idResourceClassification, String idLanguage, String name, String Description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceClassifications_LG_Update");
            _db.AddInParameter(_dbCommand, "IdResourceClassification", DbType.Int64, idResourceClassification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        /// <summary>
        /// Devuelve todas las opciones de idioma para una clasificacion
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdClassification"></param>
        /// <returns></returns>
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idResourceClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceClassifications_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "IdResourceClassification", DbType.Int64, idResourceClassification);
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
        /// <param name="IdClassification"></param>
        /// <param name="IdLanguage"></param>
        /// <returns></returns>
        internal IEnumerable<DbDataRecord> ReadById(Int64 idResourceClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceClassifications_LG_ReadById");
            _db.AddInParameter(_dbCommand, "IdResourceClassification", DbType.Int64, idResourceClassification);
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
