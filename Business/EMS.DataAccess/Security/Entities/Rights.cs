using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.Security.Entities
{
    internal class Rights
    {
        internal Rights() { }

        #region Write Functions
        internal void Create(Int64 IdOrganization, Int64 IdPerson, Int64 idPermission, String ClassName, Int64 IdObject)
        {//ok
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_SecurityPeople_Create");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);
            _db.AddInParameter(_dbCommand, "IdPermission", DbType.Int64, idPermission);
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);
            _db.AddInParameter(_dbCommand, "IdObject", DbType.Int64, IdObject);
                      
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Create(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 idPermission, String ClassName, Int64 IdObject)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_SecurityJobTitles_Create");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);
            _db.AddInParameter(_dbCommand, "IdPermission", DbType.Int64, idPermission);
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);
            _db.AddInParameter(_dbCommand, "IdObject", DbType.Int64, IdObject);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void DeleteJobTitles(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_SecurityJobTitles_DeleteByJobTitle");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteJobTitles(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, String ClassName, Int64 IdObject)
        {//ok
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_SecurityJobTitles_DeleteByJobTitleObject");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);            
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);
            _db.AddInParameter(_dbCommand, "IdObject", DbType.Int64, IdObject);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteJobTitles(Int64 IdObject, String ClassName)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_SecurityJobTitles_DeleteByObject");
            _db.AddInParameter(_dbCommand, "IdObject", DbType.Int64, IdObject);
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }


        internal void DeletePeople(Int64 IdOrganization, Int64 IdPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_SecurityPeople_DeleteByPerson");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeletePeople(Int64 IdObject, String ClassName)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_SecurityPeople_DeleteByObject");
            _db.AddInParameter(_dbCommand, "IdObject", DbType.Int64, IdObject);
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeletePeople(Int64 IdOrganization, Int64 IdPerson, String ClassName, Int64 IdObject)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_SecurityPeople_DeleteByPersonObject");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);
            _db.AddInParameter(_dbCommand, "IdObject", DbType.Int64, IdObject);
            
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion


        #region Read Functions
        internal IEnumerable<DbDataRecord> ReadPermission(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, Int64 IdPerson, String ClassName, Int64 IdObject)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_Security_ReadPermission");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);
            _db.AddInParameter(_dbCommand, "IdObject", DbType.Int64, IdObject); 
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

        internal IEnumerable<DbDataRecord> ReadByPersonAndClassName(Int64 IdOrganization, Int64 IdPerson, String ClassName)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_Security_ReadByPersonAndClassName");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);
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
        internal IEnumerable<DbDataRecord> ReadByPersonAndObject(Int64 IdOrganization, Int64 IdPerson, String ClassName, Int64 IdObject)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_Security_ReadByPersonAndObject");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, IdPerson);
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);
            _db.AddInParameter(_dbCommand, "IdObject", DbType.Int64, IdObject);
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
        internal IEnumerable<DbDataRecord> ReadPersonByObject(String ClassName, Int64 IdObject)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_Security_ReadPersonByObject");
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);
            _db.AddInParameter(_dbCommand, "IdObject", DbType.Int64, IdObject);
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


        internal IEnumerable<DbDataRecord> ReadByJobTitle(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_Security_ReadByJobTitle");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);
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
        internal IEnumerable<DbDataRecord> ReadByJobTitleAndObject(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, String ClassName, Int64 IdObject)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_Security_ReadByJobTitleAndObject");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);
            _db.AddInParameter(_dbCommand, "IdObject", DbType.Int64, IdObject);
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
        internal IEnumerable<DbDataRecord> ReadJobTitleByObject(String ClassName, Int64 IdObject)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_Security_ReadJobTitleByObject");
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);
            _db.AddInParameter(_dbCommand, "IdObject", DbType.Int64, IdObject);
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
        internal IEnumerable<DbDataRecord> ReadByJobTitleAndClassName(Int64 IdOrganization, Int64 IdGeographicArea, Int64 IdFunctionalArea, Int64 IdPosition, String ClassName)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SS_Security_ReadByJobTitleAndClassName");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, IdOrganization);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, IdGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, IdFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, IdPosition);
            _db.AddInParameter(_dbCommand, "ClassName", DbType.String, ClassName);
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
