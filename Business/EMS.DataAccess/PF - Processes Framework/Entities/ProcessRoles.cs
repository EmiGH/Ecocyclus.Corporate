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
    //internal class ProcessRoles
    //{
    //    internal ProcessRoles() { }

    //    #region Read Functions

    //    //Trae todos los types
    //    internal IEnumerable<DbDataRecord> ReadAll(Int64 idProcess)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessRoles_ReadAll");
    //        _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
    //        SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

    //        try
    //        {
    //            foreach (DbDataRecord _record in _reader)
    //            {
    //                yield return _record;
    //            }
    //        }
    //        finally
    //        {
    //            _reader.Close();
    //        }
    //    }

    //    internal IEnumerable<DbDataRecord> ReadById(Int64 idRoleType, Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessRoles_ReadById");
    //        _db.AddInParameter(_dbCommand, "idRoleType", DbType.Int64, idRoleType);
    //        _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
    //        _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "idPosition", DbType.Int64, idPosition);
    //        _db.AddInParameter(_dbCommand, "idFunctionalArea", DbType.Int64, idFunctionalArea);
    //        _db.AddInParameter(_dbCommand, "idGeographicArea", DbType.Int64, idGeographicArea);
    //        _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
    //        SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

    //        try
    //        {
    //            foreach (DbDataRecord _record in _reader)
    //            {
    //                yield return _record;
    //            }
    //        }
    //        finally
    //        {
    //            _reader.Close();
    //        }
    //    }

    //    internal IEnumerable<DbDataRecord> ReadAll(Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessRoles_ReadAllByRole");         
    //        _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "idPosition", DbType.Int64, idPosition);
    //        _db.AddInParameter(_dbCommand, "idFunctionalArea", DbType.Int64, idFunctionalArea);
    //        _db.AddInParameter(_dbCommand, "idGeographicArea", DbType.Int64, idGeographicArea);
    //        _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
    //        SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

    //        try
    //        {
    //            foreach (DbDataRecord _record in _reader)
    //            {
    //                yield return _record;
    //            }
    //        }
    //        finally
    //        {
    //            _reader.Close();
    //        }
    //    }
    //    #endregion

    //    #region Write Functions

    //    internal void Create(Int64 idRoleType, Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization, String Comment, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessRoles_Create");
    //        _db.AddInParameter(_dbCommand, "idRoleType", DbType.Int64, idRoleType);
    //        _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
    //        _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "idPosition", DbType.Int64, idPosition);
    //        _db.AddInParameter(_dbCommand, "idFunctionalArea", DbType.Int64, idFunctionalArea);
    //        _db.AddInParameter(_dbCommand, "idGeographicArea", DbType.Int64, idGeographicArea);
    //        _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
    //        _db.AddInParameter(_dbCommand, "Comment", DbType.String, Comment); 
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);
            
    //    }

    //    internal void Delete(Int64 idRoleType, Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessRoles_Delete");
    //        _db.AddInParameter(_dbCommand, "idRoleType", DbType.Int64, idRoleType);
    //        _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
    //        _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "idPosition", DbType.Int64, idPosition);
    //        _db.AddInParameter(_dbCommand, "idFunctionalArea", DbType.Int64, idFunctionalArea);
    //        _db.AddInParameter(_dbCommand, "idGeographicArea", DbType.Int64, idGeographicArea);
    //        _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);
    //    }

    //    //internal void Delete(Int64 idProcess, Int64 idOrganizationProcess, Int64 idLogPerson)
    //    //{
    //    //    Database _db = DatabaseFactory.CreateDatabase();

    //    //    DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ParticipationTypes_DeleteByProcess");
    //    //    _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
    //    //    _db.AddInParameter(_dbCommand, "idOrganizationProcess", DbType.Int64, idOrganizationProcess);
    //    //    _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

    //    //    //Ejecuta el comando
    //    //    _db.ExecuteNonQuery(_dbCommand);
    //    //}


    //    internal void Update(Int64 idRoleType, Int64 idProcess, Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, Int64 idOrganization, String Comment, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("PF_ProcessRoles_Update");
    //        _db.AddInParameter(_dbCommand, "idRoleType", DbType.Int64, idRoleType);
    //        _db.AddInParameter(_dbCommand, "idProcess", DbType.Int64, idProcess);
    //        _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "idPosition", DbType.Int64, idPosition);
    //        _db.AddInParameter(_dbCommand, "idFunctionalArea", DbType.Int64, idFunctionalArea);
    //        _db.AddInParameter(_dbCommand, "idGeographicArea", DbType.Int64, idGeographicArea);
    //        _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);
    //        _db.AddInParameter(_dbCommand, "Comment", DbType.String, Comment);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);
    //    }
    //    #endregion
    //}
}
