using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class Organizations
    {
        internal Organizations()
        {
        }

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadAll()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Organizations_ReadAll");
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
        internal IEnumerable<DbDataRecord> ReadById(Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Organizations_ReadById");
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

        //trae todos los que no tiene classificacion o su padre no tiene permisos y el si
        internal IEnumerable<DbDataRecord> ReadRoot(String className, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Organizations_ReadRoot");
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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

        //Trae todos los resources para una clasificacion dada
        internal IEnumerable<DbDataRecord> ReadByClassification(Int64 idOrganizationClassification, String className, Int64 idPerson)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Organizations_ReadByClassification");
            _db.AddInParameter(_dbCommand, "IdOrganizationClassification", DbType.Int64, idOrganizationClassification);
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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


        internal IEnumerable<DbDataRecord> ReadByPerson(Int64 idPerson, String className)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Organizations_ReadByPerson");
            _db.AddInParameter(_dbCommand, "className", DbType.String, className);
            _db.AddInParameter(_dbCommand, "idPerson", DbType.Int64, idPerson);
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

        internal Int64 Create(String name, String corporateName, String fiscalIdentification, Int64 idResourcePicture)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Organizations_Create");
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "corporateName", DbType.String, corporateName);
            _db.AddInParameter(_dbCommand, "fiscalIdentification", DbType.String, fiscalIdentification);
            _db.AddInParameter(_dbCommand, "IdResourcePicture", DbType.Int64, Common.Common.CastValueToNull(idResourcePicture, 0));

            //Parámetro de salida
            _db.AddOutParameter(_dbCommand, "IdOrganization", DbType.Int64, 18);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

            //Retorna el identificador
            return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdOrganization"));
        }
        internal void Delete(Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Organizations_Delete");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Delete()
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("SD_OrganizationsRemoveAll");

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Update(Int64 idOrganization, String name, String corporateName, String fiscalIdentification, Int64 idResourcePicture)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_Organizations_Update");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "name", DbType.String, name);
            _db.AddInParameter(_dbCommand, "corporateName", DbType.String, corporateName);
            _db.AddInParameter(_dbCommand, "fiscalIdentification", DbType.String, fiscalIdentification);
            _db.AddInParameter(_dbCommand, "IdResourcePicture", DbType.Int64, Common.Common.CastValueToNull(idResourcePicture, 0));
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }


        #endregion

        #region OrganizationClassificationOrganizations

        internal void Create(Int64 idOrganization, Int64 idOrganizationClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationClassificationOrganizations_Create");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdOrganizationClassification", DbType.Int64, idOrganizationClassification);
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void Delete(Int64 idOrganization, Int64 idOrganizationClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationClassificationOrganizations_Delete");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdOrganizationClassification", DbType.Int64, idOrganizationClassification);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteByOrganization(Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationClassificationOrganizations_DeleteByOrganization");
            _db.AddInParameter(_dbCommand, "idOrganization", DbType.Int64, idOrganization);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteByClassification(Int64 idOrganizationClassification)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationClassificationOrganizations_DeleteByClassification");
            _db.AddInParameter(_dbCommand, "idOrganizationClassification", DbType.Int64, idOrganizationClassification);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion

    }


    
}
