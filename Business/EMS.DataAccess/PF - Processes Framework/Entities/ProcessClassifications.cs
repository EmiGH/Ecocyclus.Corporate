using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    /// <summary>
    /// Gestor de acceso a datos de ProcessClasification hace el Add, 
    /// delete, modify y las lecturas de la base
    /// </summary>
    internal class ProcessClassifications
    {
       
        /// <summary>
        /// Constructor de un Process Classification
        /// </summary>
        internal ProcessClassifications()
        {}

        #region Read Functions
        /// <summary>
        /// El metodo <c>Get</c> obtiene un listado de todos las clasificaciones para una organizacion 
        /// </summary>
        /// <returns>Un <c>DataTable</c> con los campos: idCountry, idLanguage, alpha, name, internationalCode</returns>
        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassifications_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idProcessClassification, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassifications_ReadById");
            _db.AddInParameter(_dbCommand, "IdProcessClassification", DbType.Int64, idProcessClassification);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassifications_ReadRoot");
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
        internal IEnumerable<DbDataRecord> ReadByParent(Int64 idParentProcessClassifications, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassifications_ReadByParent");
            _db.AddInParameter(_dbCommand, "IdParentProcessClassification", DbType.Int64, idParentProcessClassifications);
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
        internal IEnumerable<DbDataRecord> ReadByProcess(Int64 idParentProcessClassifications, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassifications_ReadByProcess");
            _db.AddInParameter(_dbCommand, "IdParentProcessClassification", DbType.Int64, idParentProcessClassifications);
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
        internal Int64 Create(Int64 idParentProcessClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassifications_Create");            
            _db.AddInParameter(_dbCommand, "IdParentProcessClassification", DbType.Int64, Common.Common.CastValueToNull(idParentProcessClassification, DBNull.Value));
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdProcessClassification", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdProcessClassification"));
        }
  
        /// <summary>
        /// Borra una clasificacion de una Organizacion
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdClassification"></param>
        /// <param name="IdLogPerson"></param>
        internal void Delete(Int64 idProcessClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassifications_Delete");            
            _db.AddInParameter(_dbCommand, "IdProcessClassification", DbType.Int64, idProcessClassification);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        
        /// <summary>
        /// Modifica el nombre o la descripcion de una classificacion pero usa el de SP de LG 
        /// </summary>
        /// <param name="IdClassification"></param>
        internal void Update(Int64 idProcessClassification, Int64 idParentProcessClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassifications_Update");
            _db.AddInParameter(_dbCommand, "IdProcessClassification", DbType.Int64, idProcessClassification);
            _db.AddInParameter(_dbCommand, "IdParentProcessClassification", DbType.Int64, Common.Common.CastValueToNull(idParentProcessClassification, DBNull.Value));

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion


        #region ProjectClassification related with Projects
        internal void DeleteRaltionship(Int64 idProcessCalssification)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassificationProcesses_DeleteAllByClassification");
                _db.AddInParameter(_dbCommand, "idProcessCalssification", DbType.Int64, idProcessCalssification);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }


        internal void DeleteScenarioTypes(Int64 idProcessCalssification)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassificationCalculationScenarioTypes_DeleteByClassification");
                _db.AddInParameter(_dbCommand, "IdProcessClassification", DbType.Int64, idProcessCalssification);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }

        #endregion
    }
}
