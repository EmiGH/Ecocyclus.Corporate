using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.Entities
{
    internal class ExtendedPropertyClassifications_LG
    {
        internal ExtendedPropertyClassifications_LG()
        {
        }

        #region Write Functions

        /// <summary>
        /// Alta de una opcion de idioma para una Extended Property Classification
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdExtendedPropertyClassification"></param>
        /// <param name="IdLanguage"></param>
        /// <param name="name"></param>
        /// <param name="Description"></param>
        /// <param name="IdLogPerson"></param>
        internal void Create(Int64 idExtendedPropertyClassification, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ExtendedPropertyClassifications_LG_Create");
            _db.AddInParameter(_dbCommand, "IdExtendedPropertyClassification", DbType.Int64, idExtendedPropertyClassification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        /// <summary>
        /// Baja de una opcion de idioma para una Extended Property Classification
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdExtendedPropertyClassification"></param>
        /// <param name="IdLanguage"></param>
        /// <param name="IdLogPerson"></param>
        internal void Delete(Int64 idExtendedPropertyClassification, String idLanguage, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ExtendedPropertyClassifications_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdExtendedPropertyClassification", DbType.Int64, idExtendedPropertyClassification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        /// <summary>
        /// Modificacion de una opcion de idioma para una Extended Property Classification
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdExtendedPropertyClassification"></param>
        /// <param name="IdLanguage"></param>
        /// <param name="name"></param>
        /// <param name="Description"></param>
        /// <param name="IdLogPerson"></param>
        internal void Update(Int64 idExtendedPropertyClassification, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ExtendedPropertyClassifications_LG_Update");
            _db.AddInParameter(_dbCommand, "IdExtendedPropertyClassification", DbType.Int64, idExtendedPropertyClassification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

        #region Read Functions

        /// <summary>
        /// devuelve todas las opciones de idioma para una Extended Property Classification
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdExtendedPropertyClassification"></param>
        /// <returns></returns>
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idExtendedPropertyClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ExtendedPropertyClassifications_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "IdExtendedPropertyClassification", DbType.Int64, idExtendedPropertyClassification);
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
        /// devuelve una opcion de idioma para una Extended Property Classification
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdExtendedPropertyClassification"></param>
        /// <param name="IdLanguage"></param>
        /// <returns></returns>
        internal IEnumerable<DbDataRecord> ReadById(Int64 idExtendedPropertyClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ExtendedPropertyClassifications_LG_ReadById");
            _db.AddInParameter(_dbCommand, "IdExtendedPropertyClassification", DbType.Int64, idExtendedPropertyClassification);
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
