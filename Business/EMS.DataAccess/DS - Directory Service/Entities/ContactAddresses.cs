using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    //internal class ContactAddresses
    //{
    //    internal ContactAddresses()
    //    {
    //    }

    //    #region Organizations

    //    #region Write Functions
        
    //    internal Int64 AddByOrganization(String street, String number, String floor, String apartment, String zipCode, String city, String state, Int64 idCountry, Int64 idOrganization, Int64 idContactType, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactAddresses_CreateByOrganization");
    //        _db.AddInParameter(_dbCommand, "Street", DbType.String, street);
    //        _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
    //        _db.AddInParameter(_dbCommand, "Floor", DbType.String, floor);
    //        _db.AddInParameter(_dbCommand, "Apartment", DbType.String, apartment);
    //        _db.AddInParameter(_dbCommand, "ZipCode", DbType.String, zipCode);
    //        _db.AddInParameter(_dbCommand, "City", DbType.String, city);
    //        _db.AddInParameter(_dbCommand, "State", DbType.String, state);
    //        _db.AddInParameter(_dbCommand, "IdCountry", DbType.String, idCountry);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
    //        _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

    //        //Parámetro de salida
    //        _db.AddOutParameter(_dbCommand, "IdContactAddress", DbType.Int64, 18);

    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);

    //        //Retorna el identificador
    //        return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdContactAddress"));
    //    }
    //    internal void RemoveByOrganization(Int64 idContactAddress, Int64 idOrganization, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactAddresses_DeleteByOrganization");
    //        _db.AddInParameter(_dbCommand, "IdContactAddress", DbType.Int64, idContactAddress);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);
    //    }
    //    internal void Update(Int64 idContactAddress, String street, String number, String floor, String apartment, String zipCode, String city, String state, Int64 idCountry, Int64 idContactType, Int64 idOrganization, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactAddresses_UpdateByOrganization");
    //        _db.AddInParameter(_dbCommand, "IdContactAddress", DbType.Int64, idContactAddress);
    //        _db.AddInParameter(_dbCommand, "Street", DbType.String, street);
    //        _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
    //        _db.AddInParameter(_dbCommand, "Floor", DbType.String, floor);
    //        _db.AddInParameter(_dbCommand, "Apartment", DbType.String, apartment);
    //        _db.AddInParameter(_dbCommand, "ZipCode", DbType.String, zipCode);
    //        _db.AddInParameter(_dbCommand, "City", DbType.String, city);
    //        _db.AddInParameter(_dbCommand, "State", DbType.String, state);
    //        _db.AddInParameter(_dbCommand, "IdCountry", DbType.String, idCountry);
    //        _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
        
    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);
    //    }
        
    //    #endregion

    //    #region Read Functions

    //    internal IEnumerable<DbDataRecord> ReadById(Int64 idContactAddresses, Int64 idOrganization)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("[DS_ContactAddresses_ReadByOrganization]");
    //        _db.AddInParameter(_dbCommand, "IdContactAddress", DbType.Int64, idContactAddresses);
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

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactAddresses_ReadAllByOrganization");
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

    //    internal Int64 AddByPerson(Int64 idOrganization, String street, String number, String floor, String apartment, String zipCode, String city, String state, Int64 idCountry, Int64 idPerson, Int64 idContactType, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactAddresses_CreateByPerson");
    //        _db.AddInParameter(_dbCommand, "Street", DbType.String, street);
    //        _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
    //        _db.AddInParameter(_dbCommand, "Floor", DbType.String, floor);
    //        _db.AddInParameter(_dbCommand, "Apartment", DbType.String, apartment);
    //        _db.AddInParameter(_dbCommand, "ZipCode", DbType.String, zipCode);
    //        _db.AddInParameter(_dbCommand, "City", DbType.String, city);
    //        _db.AddInParameter(_dbCommand, "State", DbType.String, state);
    //        _db.AddInParameter(_dbCommand, "IdCountry", DbType.String, idCountry);
    //        _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

    //        //Parámetro de salida
    //        _db.AddOutParameter(_dbCommand, "IdContactAddress", DbType.Int64, 18);

    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);

    //        //Retorna el identificador
    //        return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdContactAddress"));
    //    }
    //    internal void Update(Int64 idPerson, Int64 idContactAddress, Int64 idOrganization, String street, String number, String floor, String apartment, String zipCode, String city, String state, Int64 idCountry, Int64 idContactType, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactAddresses_UpdateByPerson");
    //        _db.AddInParameter(_dbCommand, "IdContactAddress", DbType.Int64, idContactAddress);
    //        _db.AddInParameter(_dbCommand, "Street", DbType.String, street);
    //        _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
    //        _db.AddInParameter(_dbCommand, "Floor", DbType.String, floor);
    //        _db.AddInParameter(_dbCommand, "Apartment", DbType.String, apartment);
    //        _db.AddInParameter(_dbCommand, "ZipCode", DbType.String, zipCode);
    //        _db.AddInParameter(_dbCommand, "City", DbType.String, city);
    //        _db.AddInParameter(_dbCommand, "State", DbType.String, state);
    //        _db.AddInParameter(_dbCommand, "IdCountry", DbType.String, idCountry);
    //        _db.AddInParameter(_dbCommand, "IdContactType", DbType.Int64, idContactType);
    //        _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);
    //    }
    //    internal void RemoveByPerson(Int64 idContactAddress, Int64 idPerson, Int64 idOrganization, Int64 idLogPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactAddresses_DeleteByPerson");
    //        _db.AddInParameter(_dbCommand, "IdContactAddress", DbType.Int64, idContactAddress);
    //        _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
    //        _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
    //        _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

    //        //Ejecuta el comando
    //        _db.ExecuteNonQuery(_dbCommand);
    //    }

    //    #endregion

    //    #region Read Functions

    //    internal IEnumerable<DbDataRecord> ReadByIdPerson(Int64 idContactAddresses, Int64 idOrganization, Int64 idPerson)
    //    {
    //        Database _db = DatabaseFactory.CreateDatabase();

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("[DS_ContactAddresses_ReadByPerson]");
    //        _db.AddInParameter(_dbCommand, "IdContactAddress", DbType.Int64, idContactAddresses);
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

    //        DbCommand _dbCommand = _db.GetStoredProcCommand("DS_ContactAddresses_ReadAllByPerson");
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

   