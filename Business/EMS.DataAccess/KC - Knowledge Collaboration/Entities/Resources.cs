using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.KC.Entities
{
    internal class Resources
    {
        internal Resources() { }

        #region Read Functions

        //Trae todos los resources (no creo que se use)
        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_Resources_ReadAll");
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

        //trae el resource pedido
        internal IEnumerable<DbDataRecord> ReadById(Int64 idResource, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_Resources_ReadById");
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);
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

        //trae todos los que no tiene classificacion o su padre no tiene permisos y el si
        internal IEnumerable<DbDataRecord> ReadRoot(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_Resources_ReadRoot");
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

        //Trae todos los resources para una clasificacion dada
        internal IEnumerable<DbDataRecord> ReadByClassification(Int64 idResourceClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_Resources_ReadByClassification");
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


        //Trae todos los resources para una tipo dado
        internal IEnumerable<DbDataRecord> ReadByType(Int64 idResourceType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_Resources_ReadByType");
            _db.AddInParameter(_dbCommand, "idResourceType", DbType.Int64, idResourceType);
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
        /// Traee todos los process con los que tiene relacion el resource
        /// </summary>
        /// <param name="idResourceType"></param>
        /// <param name="idLanguage"></param>
        /// <param name="className"></param>
        /// <param name="idPerson"></param>
        /// <returns></returns>
        internal IEnumerable<DbDataRecord> ReadProcess(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_Resources_ReadProcess");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
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

        internal Int64 Create(Int64 idResourceType, Int64 currentFile, String type)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_Resources_Create");
            _db.AddInParameter(_dbCommand, "idResourceType", DbType.Int64, idResourceType);
            _db.AddInParameter(_dbCommand, "currentFile", DbType.Int64, currentFile);
            _db.AddInParameter(_dbCommand, "type", DbType.String, type);
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdResource", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdResource"));
        }

        internal void Delete(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_Resources_Delete");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 idResource, Int64 idResourceType)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_Resources_Update");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "idResourceType", DbType.Int64, idResourceType);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void UpdateCurrentFile(Int64 idResource, Int64 currentFile)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_Resources_UpdateCurrentFile");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "currentFile", DbType.Int64, currentFile);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region ResourceClassificationResource

        internal void Create(Int64 idResource, Int64 idResourceClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceClassificationResources_Create");
            _db.AddInParameter(_dbCommand, "IdResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "IdResourceClassification", DbType.Int64, idResourceClassification);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void Delete(Int64 idResource, Int64 idResourceClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceClassificationResources_Delete");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);
            _db.AddInParameter(_dbCommand, "idResourceClassification", DbType.Int64, idResourceClassification);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteByResource(Int64 idResource)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceClassificationResources_DeleteByResource");
            _db.AddInParameter(_dbCommand, "idResource", DbType.Int64, idResource);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteByClassification(Int64 idResourceClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("KC_ResourceClassificationResources_DeleteByClassification");
            _db.AddInParameter(_dbCommand, "idResourceClassification", DbType.Int64, idResourceClassification);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion



    }
}
