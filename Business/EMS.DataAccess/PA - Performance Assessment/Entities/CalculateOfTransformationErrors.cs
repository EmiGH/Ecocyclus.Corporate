using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class CalculateOfTransformationErrors
    {
        internal CalculateOfTransformationErrors() { }

        #region Read Functions
        //Trae todos los resources 
        internal IEnumerable<DbDataRecord> ReadAll(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationErrors_ReadByTransformation");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
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
        //trae el resource pedido
        //internal IEnumerable<DbDataRecord> ReadById(Int64 IdTransformation, String IdLanguage)
        //{
        //    Database _db = DatabaseFactory.CreateDatabase();

        //    DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadById");
        //    _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
        //    _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
        //    SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

        //    try
        //    {
        //        foreach (DbDataRecord _record in _reader)
        //        {
        //            yield return _record;
        //        }
        //    }
        //    finally
        //    {
        //        _reader.Close();
        //    }
        //}
        //internal IEnumerable<DbDataRecord> ReadByMeasurement(Int64 IdMeasurement, String IdLanguage)
        //{
        //    Database _db = DatabaseFactory.CreateDatabase();

        //    DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadByMeasurement");
        //    _db.AddInParameter(_dbCommand, "IdMeasurement", DbType.Int64, IdMeasurement);
        //    _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
        //    SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

        //    try
        //    {
        //        foreach (DbDataRecord _record in _reader)
        //        {
        //            yield return _record;
        //        }
        //    }
        //    finally
        //    {
        //        _reader.Close();
        //    }
        //}
        //internal IEnumerable<DbDataRecord> ReadByProcess(Int64 IdProcess, String IdLanguage)
        //{
        //    Database _db = DatabaseFactory.CreateDatabase();

        //    DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadByProcess");
        //    _db.AddInParameter(_dbCommand, "IdProcess", DbType.Int64, IdProcess);
        //    _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
        //    SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

        //    try
        //    {
        //        foreach (DbDataRecord _record in _reader)
        //        {
        //            yield return _record;
        //        }
        //    }
        //    finally
        //    {
        //        _reader.Close();
        //    }
        //}
        //internal IEnumerable<DbDataRecord> ReadByTransformation(Int64 IdTransformationTransformer, String IdLanguage)
        //{
        //    Database _db = DatabaseFactory.CreateDatabase();

        //    DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformations_ReadByTransformation");
        //    _db.AddInParameter(_dbCommand, "IdTransformationTransformer", DbType.Int64, IdTransformationTransformer);
        //    _db.AddInParameter(_dbCommand, "IdLanguage", DbType.String, IdLanguage);
        //    SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

        //    try
        //    {
        //        foreach (DbDataRecord _record in _reader)
        //        {
        //            yield return _record;
        //        }
        //    }
        //    finally
        //    {
        //        _reader.Close();
        //    }
        //}
        //internal IEnumerable<DbDataRecord> ReadOperateValue(Int64 IdTransformation, DateTime TransformationDate)
        //{
        //    Database _db = DatabaseFactory.CreateDatabase();

        //    DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformation_ReadOperateValue");
        //    _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
        //    _db.AddInParameter(_dbCommand, "TransformationDate", DbType.DateTime, TransformationDate);
        //    SqlDataReader _reader = (SqlDataReader)_db.ExecuteReader(_dbCommand);

        //    try
        //    {
        //        foreach (DbDataRecord _record in _reader)
        //        {
        //            yield return _record;
        //        }
        //    }
        //    finally
        //    {
        //        _reader.Close();
        //    }
        //}
        #endregion
        
        #region Write Functions
        internal void Create(Int64 IdTransformation, Boolean Reported, String Description)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationErrors_Create");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "Reported", DbType.Boolean, Reported);
            _db.AddInParameter(_dbCommand, "Description", DbType.String, Description);

            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 IdError, Int64 IdTransformation, Boolean Reported)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationErrors_Update");
            _db.AddInParameter(_dbCommand, "IdError", DbType.Int64, IdError); 
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "Reported", DbType.Boolean, Reported);

            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationErrors_Delete");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);

            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteAll()
        {
            Database _db = DatabaseFactory.CreateDatabase();
            DbCommand _dbCommand = _db.GetStoredProcCommand("PA_CalculateOfTransformationErrors_DeleteAll");

            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion


    }
}
