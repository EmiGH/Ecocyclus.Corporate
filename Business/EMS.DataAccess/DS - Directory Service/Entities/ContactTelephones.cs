using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    //internal class ContactTelephones
    //{
    //    internal ContactTelephones()
    //    {
    //    }

    //    #region Organizations

    //    #region Write Functions
        
    //    internal Int64 AddByOrganization(String areaCode, String number, String extension, Int64 idCountry, Int64 idOrganization, Int64 idContactType, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactTelephones_CreateByOrganization");
    //        _db.AddInParameter(_dbCommand, "AreaCode", DbType.String, areaCode);
    //        _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
    //        _db.AddInParameter(_dbCommand, "Extension", DbType.String, extension);
    //        _db.AddInParameter(_dbCommand, "IdCountry", DbType.String, idCountry);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
    //        _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

    //        //Parámetro de salida
    //        _db.AddOutParameter(_dbCommand, "IdContactTelephone", DbType.Int64, 18);

    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);

    //        //Retorna el identificador
    //        return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdContactTelephone"));
    //    }       
    //    internal void RemoveByOrganization(Int64 idContactTelephone, Int64 idOrganization, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactTelephones_DeleteByOrganization");
    //        _db.AddInParameter(_dbCommand, "IdContactTelephone", DbType.Int64, idContactTelephone);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);
    //    }
    //    internal void Update(Int64 idContactTelephone, String areaCode, String number, String extension, Int64 idCountry, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactTelephones_UpdateByOrganization");
    //        _db.AddInParameter(_dbCommand, "IdContactTelephone", DbType.Int64, idContactTelephone);
    //        _db.AddInParameter(_dbCommand, "AreaCode", DbType.String, areaCode);
    //        _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
    //        _db.AddInParameter(_dbCommand, "Extension", DbType.String, extension);           
    //        _db.AddInParameter(_dbCommand, "IdCountry", DbType.String, idCountry);
    //        _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
        
    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);
    //    }

    //    #endregion

    //    #region Read Functions

    //    internal IEnumerable<DbDataRecord> ReadById(Int64 idContactTelephone, Int64 idOrganization)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("[DS_ContactTelephones_ReadByOrganization]");
    //        _db.AddInParameter(_dbCommand, "IdContactTelephone", DbType.Int64, idContactTelephone);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
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
    //    internal IEnumerable<DbDataRecord> GetByOrganization(Int64 idOrganization)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactTelephones_ReadAllByOrganization");
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
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

    //    #endregion

    //    #region People

    //    #region Write Functions
        
    //    internal Int64 AddByPerson(String areaCode, String number, String extension, Int64 idCountry, Int64 idPerson, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactTelephones_CreateByPerson");
    //        _db.AddInParameter(_dbCommand, "AreaCode", DbType.String, areaCode);
    //        _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
    //        _db.AddInParameter(_dbCommand, "Extension", DbType.String, extension);
    //        _db.AddInParameter(_dbCommand, "IdCountry", DbType.String, idCountry);
    //        _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

    //        //Parámetro de salida
    //        _db.AddOutParameter(_dbCommand, "IdContactTelephone", DbType.Int64, 18);

    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);

    //        //Retorna el identificador
    //        return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdContactTelephone"));
    //    }
    //    internal void RemoveByPerson(Int64 idContactTelephone, Int64 idPerson, Int64 idOrganization, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactTelephones_DeleteByPerson");
    //        _db.AddInParameter(_dbCommand, "IdContactTelephone", DbType.Int64, idContactTelephone);
    //        _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);
    //    }
    //    internal void Update(Int64 idPerson, Int64 idContactTelephone, String areaCode, String number, String extension, Int64 idCountry, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactTelephones_UpdateByPerson");
    //        _db.AddInParameter(_dbCommand, "IdContactTelephone", DbType.Int64, idContactTelephone);
    //        _db.AddInParameter(_dbCommand, "AreaCode", DbType.String, areaCode);
    //        _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
    //        _db.AddInParameter(_dbCommand, "Extension", DbType.String, extension);
    //        _db.AddInParameter(_dbCommand, "IdCountry", DbType.String, idCountry);
    //        _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
    //        _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);
    //    }

    //    #endregion

    //    #region Read Functions

    //    internal IEnumerable<DbDataRecord> ReadByIdPerson(Int64 idContactTelephone, Int64 idOrganization, Int64 idPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("[DS_ContactTelephones_ReadByPerson]");
    //        _db.AddInParameter(_dbCommand, "IdContactTelephone", DbType.Int64, idContactTelephone);
    //        _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

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
    //    internal IEnumerable<DbDataRecord> GetByPerson(Int64 idPerson, Int64 idOrganization)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactTelephones_ReadAllByPerson");
    //        _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

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

    //    #endregion
    //}
}

   