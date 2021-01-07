using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class Telephones
    {
        internal Telephones()
        {
        }

        #region Write Functions Addresses

        internal Int64 Create(String areaCode, String number, String extension, String internationalCode)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Telephones_Create");
            _db.AddInParameter(_dbCommand, "AreaCode", DbType.String, areaCode);
            _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
            _db.AddInParameter(_dbCommand, "Extension", DbType.String, extension);
            _db.AddInParameter(_dbCommand, "InternationalCode", DbType.String, internationalCode);
            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdTelephone", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdTelephone"));
        }
        internal void Delete(Int64 idTelephone)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Telephones_Delete");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idTelephone, String areaCode, String number, String extension, String internationalCode)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Telephones_Update");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);
            _db.AddInParameter(_dbCommand, "AreaCode", DbType.String, areaCode);
            _db.AddInParameter(_dbCommand, "Number", DbType.String, number);
            _db.AddInParameter(_dbCommand, "Extension", DbType.String, extension);
            _db.AddInParameter(_dbCommand, "InternationalCode", DbType.String, internationalCode);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        #endregion

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Telephones_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idTelephone)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Telephones_ReadById");
            _db.AddInParameter(_dbCommand, "IdTelephone", DbType.Int64, idTelephone);

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
