using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessGroupProcesses
    {
        internal ProcessGroupProcesses() { }

        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadById(Int64 idProcess, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadById");
            _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
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

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadByPerson(String idLanguage, Int64 idPerson, String className)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadByPerson");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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
        internal IEnumerable<DbDataRecord> ReadByPersonAndRoleType(Int64 idPerson, Int64 idRoleType)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_Security_ReadProcessByRoleType");
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdRoleType", DbType.Int64, idRoleType);
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
        internal IEnumerable<DbDataRecord> ReadRoot(String idLanguage, String className, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadRoot");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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
        internal IEnumerable<DbDataRecord> ReadByClassification(Int64 idClassification, String idLanguage, String className, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadByClassification");
            _db.AddInParameter(_dbCommand, "IdClassification", DbType.Int64, idClassification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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

        internal IEnumerable<DbDataRecord> ReadByGeographicArea(Int64 IdGeographicArea, String idLanguage, String className, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadByGeographicArea");
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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

        internal IEnumerable<DbDataRecord> ReadByGeographicAreaLayerAndProcessType(Int64 IdGeographicAreaParent, Int64 IdProcessClassification, String idLanguage, String className, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadByGeographicAreaLayerAndProcessType");
            _db.AddInParameter(_dbCommand, "IdGeographicAreaParent", DbType.Int64, IdGeographicAreaParent);
            _db.AddInParameter(_dbCommand, "IdProcessClassification", DbType.Int64, IdProcessClassification);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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

        internal IEnumerable<DbDataRecord> ReadByClassificationAndGeographicArea(Int64 idClassification, Int64 IdGeographicArea, String idLanguage, String className, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadByClassificationAndGeographicArea");
            _db.AddInParameter(_dbCommand, "IdClassification", DbType.Int64, idClassification);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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

        internal IEnumerable<DbDataRecord> ReadByOrganization(Int64 IdOrganization, String idLanguage, String className, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadByOrganization");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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

        internal IEnumerable<DbDataRecord> ReadByClassificationAndOrganization(Int64 idClassification, Int64 IdOrganization, String idLanguage, String className, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadByClassificationAndIdOrganization");
            _db.AddInParameter(_dbCommand, "IdClassification", DbType.Int64, idClassification);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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

        internal IEnumerable<DbDataRecord> ReadProcessParticipationsByProcess(Int64 idProcess, String idLanguage, String className, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadProcessParticipationsByProcess");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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

        internal IEnumerable<DbDataRecord> ClassificationsByProject(Int64 idProcess, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ClassificationByProject");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
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
        internal IEnumerable<DbDataRecord> ReadByCalculations(Int64 idCalculation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationProcessGroupProcesses_ReadByCalculation");
            _db.AddInParameter(_dbCommand, "idCalculation", DbType.Int64, idCalculation);
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
        internal IEnumerable<DbDataRecord> ReadByTitle(String Title, String className, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadByTitle");
            _db.AddInParameter(_dbCommand, "Title", DbType.String, Title);
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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
        internal IEnumerable<DbDataRecord> ReadByFacility(String idLanguage, Int64 idPerson, Int64 IdFacility)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_ReadByFacility");
            _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, idLanguage);            
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdFacility", DbType.Int64, IdFacility);
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

        #region Write Process
        internal void Create(Int64 idProcess, String identification, DateTime currentCampaignStartDate, Int64 idResourcePicture, String Coordinate,
                Int64 idGeographicArea, Int64 idOrganization, String TwitterUser, String FacebookUser)
                {
                    Database _db = DatabaseFactory.CreateDatabase();

                    DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_Create");
                
                    _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                    _db.AddInParameter(_dbCommand, "Identification", DbType.String, identification);
                    _db.AddInParameter(_dbCommand, "CurrentCampaignStartDate", DbType.DateTime, currentCampaignStartDate);
                    _db.AddInParameter(_dbCommand, "IdResourcePicture", DbType.Int64, Common.Common.CastValueToNull(idResourcePicture, 0));
                    _db.AddInParameter(_dbCommand, "Coordinate", DbType.String, Coordinate);
                    _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, Common.Common.CastValueToNull(idGeographicArea, 0));
                    _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                    _db.AddInParameter(_dbCommand, "TwitterUser", DbType.String, TwitterUser);
                    _db.AddInParameter(_dbCommand, "FacebookUser", DbType.String, FacebookUser);
                    //Ejecuta el comando
                    _db.ExecuteNonQuery(_dbCommand);

                }      
            
            internal void Update(Int64 idProcess, String identification, DateTime currentCampaignStartDate, Int64 idResourcePicture,
                    String Coordinate, Int64 idGeographicArea, Int64 idOrganization, String TwitterUser, String FacebookUser)
                {
                    Database _db = DatabaseFactory.CreateDatabase();

                    DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_Update");

                    _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                    _db.AddInParameter(_dbCommand, "Identification", DbType.String, identification);
                    _db.AddInParameter(_dbCommand, "CurrentCampaignStartDate", DbType.DateTime, currentCampaignStartDate);
                    _db.AddInParameter(_dbCommand, "IdResourcePicture", DbType.Int64, Common.Common.CastValueToNull(idResourcePicture, 0));
                    _db.AddInParameter(_dbCommand, "Coordinate", DbType.String, Coordinate);
                    _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, Common.Common.CastValueToNull(idGeographicArea, 0));
                    _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                    _db.AddInParameter(_dbCommand, "TwitterUser", DbType.String, TwitterUser);
                    _db.AddInParameter(_dbCommand, "FacebookUser", DbType.String, FacebookUser);
                    //Ejecuta el comando
                    _db.ExecuteNonQuery(_dbCommand);
                }
            internal void Delete(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessGroupProcesses_Delete");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion  

        #region Projects related with ProjectClassification
            internal void Create(Int64 idProcess, Int64 idProcessClassification)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassificationProcesses_Create");
                
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdProcessClassification", DbType.Int64, idProcessClassification);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void Delete(Int64 idProcess, Int64 idProcessClassification)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessClassificationProcesses_Delete");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);
                _db.AddInParameter(_dbCommand, "IdProcessClassification", DbType.Int64, idProcessClassification);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
            internal void DeleteRelationshipCalculation(Int64 idProcess)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculationProcessGroupProcesses_DeleteByProcess");
                _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, idProcess);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

    }
}
