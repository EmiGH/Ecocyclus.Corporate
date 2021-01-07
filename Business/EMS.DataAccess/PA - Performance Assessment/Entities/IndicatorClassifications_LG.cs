using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class IndicatorClassifications_LG
    {
        internal IndicatorClassifications_LG()
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
        internal void Create(Int64 idIndicatorClassification, String idLanguage, String name, String Description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_LG_Create");
            _db.AddInParameter(_dbCommand, "IdIndicatorClassification", DbType.Int64, idIndicatorClassification);
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
        internal void Delete(Int64 idIndicatorClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdIndicatorClassification", DbType.Int64, idIndicatorClassification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        /// <summary>
        /// Borra todas las opciones de idioma para un classification
        /// </summary>
        /// <param name="idIndicatorClassification"></param>
        /// <param name="idLanguage"></param>
        internal void Delete(Int64 idIndicatorClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_LG_DeleteByClassification");
            _db.AddInParameter(_dbCommand, "IdIndicatorClassification", DbType.Int64, idIndicatorClassification);

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
        internal void Update(Int64 idIndicatorClassification, String idLanguage, String name, String Description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_LG_Update");
            _db.AddInParameter(_dbCommand, "IdIndicatorClassification", DbType.Int64, idIndicatorClassification);
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
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idIndicatorClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "IdIndicatorClassification", DbType.Int64, idIndicatorClassification);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idIndicatorClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_IndicatorClassifications_LG_ReadById");
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
        #endregion
    }
}
