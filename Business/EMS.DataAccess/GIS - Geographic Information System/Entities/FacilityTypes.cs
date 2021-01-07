using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.GIS.Entities
{
    internal class FacilityTypes
    {
        internal FacilityTypes()
        {
        }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadById(Int64 idFacilityType, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_FacilityTypes_ReadById");
            _db.AddInParameter(_dbCommand, "IdFacilityType", DbType.Int64, idFacilityType);
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
        internal IEnumerable<DbDataRecord> ReadAll(String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_FacilityTypes_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadByProcessWhitMeasurements(Int64 IdProcess, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_FacilityTypes_ReadByProcessWhitMeasurements");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
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
        internal Int64 Create(String IconName)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_FacilityTypes_Create");
            _db.AddInParameter(_dbCommand, "IconName", DbType.String, IconName);
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdFacilityType", DbType.Int64, 18);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdFacilityType"));
        }

        /// <summary>
        /// Borra una clasificacion de una Organizacion
        /// </summary>
        /// <param name="IdOrganization"></param>
        /// <param name="IdClassification"></param>
        /// <param name="IdLogPerson"></param>
        internal void Delete(Int64 idFacilityType)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_FacilityTypes_Delete");
            _db.AddInParameter(_dbCommand, "IdFacilityType", DbType.Int64, idFacilityType);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }


        internal void Update(Int64 idFacilityType, String IconName)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("GIS_FacilityTypes_Update");
            _db.AddInParameter(_dbCommand, "IdFacilityType", DbType.Int64, idFacilityType);
            _db.AddInParameter(_dbCommand, "IconName", DbType.String, IconName);
           
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        
        }

        #endregion
    }
}
