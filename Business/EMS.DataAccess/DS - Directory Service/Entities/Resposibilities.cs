using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class Responsibilities
    {
        internal Responsibilities()
        {
        }

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll(Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Responsibilities_ReadAll");
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idPosition, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idGeographicAreaResponsibility, Int64 idFunctionalAreaResponsibility, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Responsibilities_ReadById");
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdGeographicAreaResponsibility", DbType.Int64, idGeographicAreaResponsibility);
            _db.AddInParameter(_dbCommand, "IdFunctionalAreaResponsibility", DbType.Int64, idFunctionalAreaResponsibility);
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

        #region Write Functions

        internal void Delete(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalAreaResponsibility, Int64 idGeographicAreaResponsibility, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Responsibilities_Delete");
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdGeographicAreaResponsibility", DbType.Int64, idGeographicAreaResponsibility);
            _db.AddInParameter(_dbCommand, "IdFunctionalAreaResponsibility", DbType.Int64, idFunctionalAreaResponsibility);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idPosition, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Responsibilities_DeleteByJobTitle");
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Create(Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalAreaResponsibility, Int64 idGeographicAreaResponsibility, Int64 idOrganization, Int64 idLogPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Responsibilities_Create");
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdGeographicAreaResponsibility", DbType.Int64, idGeographicAreaResponsibility);
            _db.AddInParameter(_dbCommand, "IdFunctionalAreaResponsibility", DbType.Int64, idFunctionalAreaResponsibility);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }

        #endregion

    }
}
