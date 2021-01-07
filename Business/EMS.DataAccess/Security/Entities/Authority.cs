using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.Security.Entities
{
    internal class Authority
    {
        internal Authority()
        {
            
        }

        internal Boolean Authenticate(String username, String password, String ipAddress)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_PeopleUsers_Login");
            _db.AddInParameter(_dbCommand, "Username", DbType.String, username);
            _db.AddInParameter(_dbCommand, "Password", DbType.String, password);
            _db.AddInParameter(_dbCommand, "CurrentIP", DbType.String, ipAddress);
            _db.AddOutParameter(_dbCommand, "Authentication", DbType.Boolean, 1);

            _db.ExecuteNonQuery(_dbCommand);
            return (Boolean)_db.GetParameterValue(_dbCommand, "Authentication");
        }
        internal IEnumerable<DbDataRecord> Authorize(String className, Int64 idObject, Int64 idPerson, String idLanguage)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_Security_Authorize_Object");
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, className);
            _db.AddInParameter(_dbCommand, "IdObject", DbType.Int64, idObject);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
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

        internal IEnumerable<DbDataRecord> IsAdmin(Int64 IdOrganization, Int64 IdGeographicArea ,Int64 IdFunctionalArea ,Int64 IdPosition 
            ,Int64 IdPerson,Int64 IdPermission)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_Security_IsAdmin");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);
            _db.AddInParameter(_dbCommand, "IdPermission", DbType.Int64, IdPermission);

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
    }
}
