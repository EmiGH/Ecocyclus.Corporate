using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class People
    {
        internal People()
        {
        }

        #region Write Functions

        internal Int64 Create(Int64 idSalutationType, Int64 idOrganization, String lastName, String firstName, String posName, String nickName, Int64 idResourcePicture)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_People_Create");
            _db.AddInParameter(_dbCommand, "IdSalutationType", DbType.Int64, idSalutationType);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "lastName", DbType.String, lastName);
            _db.AddInParameter(_dbCommand, "firstName", DbType.String, firstName);
            _db.AddInParameter(_dbCommand, "posName", DbType.String, posName);
            _db.AddInParameter(_dbCommand, "nickName", DbType.String, nickName);
            _db.AddInParameter(_dbCommand, "IdResourcePicture", DbType.Int64, Common.Common.CastValueToNull(idResourcePicture, 0));      

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdPerson", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdPerson"));
        }
        internal void Delete(Int64 idPerson, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_People_Delete");
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson); 

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idPerson, Int64 idSalutationType, Int64 idOrganization, String lastName, String firstName, String posName, String nickName, Int64 idResourcePicture)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_People_Update");
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdSalutationType", DbType.Int64, idSalutationType);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "lastName", DbType.String, lastName);
            _db.AddInParameter(_dbCommand, "firstName", DbType.String, firstName);
            _db.AddInParameter(_dbCommand, "posName", DbType.String, posName);
            _db.AddInParameter(_dbCommand, "nickName", DbType.String, nickName);
            _db.AddInParameter(_dbCommand, "idResourcePicture", DbType.Int64, Common.Common.CastValueToNull(idResourcePicture, 0)); 

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_People_ReadAll");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
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
        //internal IEnumerable<DbDataRecord> ReadById(Int64 idPerson, Int64 idOrganization)
        internal IEnumerable<DbDataRecord> ReadById(Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_People_ReadById");
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            //_db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

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

        internal IEnumerable<DbDataRecord> Exists(Int64 idPerson, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_PeopleUsers_Exists");
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

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
