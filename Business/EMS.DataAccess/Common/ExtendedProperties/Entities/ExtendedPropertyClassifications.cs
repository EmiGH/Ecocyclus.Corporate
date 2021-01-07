using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.Entities
{
    internal class ExtendedPropertyClassifications
    {

        internal ExtendedPropertyClassifications()
        {

        }

        #region Read Functions
      
        /// <summary>
        /// Devuelve todas las Extended Properties Classification para una organizacion
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <returns></returns>
        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ExtendedPropertyClassifications_ReadAll");
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

        /// <summary>
        /// devuelve lo pedido, si no existe devuelve el lenguje default
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdExtendedPropertyClassification"></param>
        /// <param name="IdLanguage"></param>
        /// <returns></returns>
        internal IEnumerable<DbDataRecord> ReadById(Int64 idExtendedPropertyClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ExtendedPropertyClassifications_ReadById");
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

        #region Write Functions
        
        /// <summary>
        /// Alta de una Extended Propertie Classification
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdLanguage"></param>
        /// <param name="name"></param>
        /// <param name="Description"></param>
        /// <param name="IdLogPerson"></param>
        /// <returns></returns>
        internal Int64 Create(String idLanguage, String name, String description, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ExtendedPropertyClassifications_Create");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdExtendedPropertyClassification", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdExtendedPropertyClassification"));
        }

        /// <summary>
        /// Baja de una Extended Propertie Classification
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdExtendedPropertyClassification"></param>
        /// <param name="IdLogPerson"></param>
        internal void Delete(Int64 idExtendedPropertyClassification, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ExtendedPropertyClassifications_Delete");
            _db.AddInParameter(_dbCommand, "IdExtendedPropertyClassification", DbType.Int64, idExtendedPropertyClassification);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idExtendedPropertyClassification, String idLanguage, String name, String description, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("EP_ExtendedPropertyClassifications_Update");
            _db.AddInParameter(_dbCommand, "IdExtendedPropertyClassification", DbType.Int64, idExtendedPropertyClassification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, description);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

    }
}
