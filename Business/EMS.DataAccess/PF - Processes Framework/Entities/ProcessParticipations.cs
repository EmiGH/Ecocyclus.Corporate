using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PF.Entities
{
    internal class ProcessParticipations
    {
        internal ProcessParticipations() { }

        #region Read Functions

        //Trae todos los types
        internal IEnumerable<DbDataRecord> ReadByProcess(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessParticipations_ReadAll");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
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

        internal IEnumerable<DbDataRecord> ReadByOrganization(Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessParticipations_ReadAllByOrganizations");
            _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idProcess, Int64 idOrganization, Int64 IdParticipationType)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessParticipations_ReadById");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdParticipationType", DbType.Int64, IdParticipationType);
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

        internal void Create(Int64 idProcess, Int64 idOrganization, Int64 IdParticipationType, String Comment, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessParticipations_Create");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdParticipationType", DbType.Int64, IdParticipationType);
            _db.AddInParameter(_dbCommand, "Comment", DbType.String, Comment); 
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
            
        }

        internal void Delete(Int64 idProcess, Int64 idOrganization, Int64 idParticipationType, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessParticipations_Delete");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdParticipationType", DbType.Int64, idParticipationType);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        //internal void Delete(Int64 idProcess, Int64 idLogPerson)
        //{
        //    Database _db = DatabaseFactory.CreateDatabase();

        //    DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ParticipationTypes_DeleteByProcess");
        //    _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
        //    _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

        //    //Ejecuta el comando
        //    _db.ExecuteNonQuery(_dbCommand);
        //}

        internal void DeleteByOrganization(Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessParticipations_DeleteByOrganization");
            _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void DeleteByProcess(Int64 idProcess)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessParticipations_DeleteByProcess");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }



        internal void Update(Int64 idProcess, Int64 idOrganization, Int64 IdParticipationType, String Comment, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessParticipations_Update");
            _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
            _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdParticipationType", DbType.Int64, IdParticipationType);
            _db.AddInParameter(_dbCommand, "Comment", DbType.String, Comment);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
