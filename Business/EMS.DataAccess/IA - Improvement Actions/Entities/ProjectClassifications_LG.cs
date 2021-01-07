using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.IA.Entities
{
    internal class ProjectClassifications_LG
    {
        internal ProjectClassifications_LG()
        {
            
        }

        #region Write Functions
        
        
        internal void Create(Int64 idProjectClassification, String idLanguage, String name, String Description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ProjectClassifications_LG_Create");
            _db.AddInParameter(_dbCommand, "IdProjectClassification", DbType.Int64, idProjectClassification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "Name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }


        internal void Delete(Int64 idProjectClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ProjectClassifications_LG_Delete");
            _db.AddInParameter(_dbCommand, "IdProjectClassification", DbType.Int64, idProjectClassification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 idProjectClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ProjectClassifications_LG_DeleteByProject");
            _db.AddInParameter(_dbCommand, "IdProjectClassification", DbType.Int64, idProjectClassification);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idProjectClassification, String idLanguage, String name, String Description)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ProjectClassifications_LG_Update");
            _db.AddInParameter(_dbCommand, "IdProjectClassification", DbType.Int64, idProjectClassification);
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
        internal IEnumerable<DbDataRecord> ReadAll(Int64 idProjectClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ProjectClassifications_LG_ReadAll");
            _db.AddInParameter(_dbCommand, "idProjectClassification", DbType.Int64, idProjectClassification);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idProjectClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("IA_ProjectClassifications_LG_ReadById");
            _db.AddInParameter(_dbCommand, "IdProjectClassification", DbType.Int64, idProjectClassification);
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
