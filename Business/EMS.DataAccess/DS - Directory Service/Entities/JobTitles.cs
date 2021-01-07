using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.DS
{
    internal class JobTitles
    {
        internal JobTitles()
        {
        }

        #region Write Functions

        internal void Delete(Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idLogPerson, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_JobTitles_Delete");
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);
            

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void Create(Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idLogPerson, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_JobTitles_Create");
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdLogPerson", DbType.Int64, idLogPerson);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }

        internal void DeleteRelationship(Int64 idOrganizationalChart, Int64 idOrganization, Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalChartJobTitles_Delete");
            _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, idOrganizationalChart);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteRelationship(Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalChartJobTitles_DeleteByJobtitle");
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void DeleteRelationship(Int64 idOrganizationalChart, Int64 idOrganization)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalChartJobTitles_DeleteByOrganizationalChart");
            _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, idOrganizationalChart);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        internal void CreateRelationship(Int64 idOrganizationalChart, Int64 idOrganization, Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicAreaParent, Int64 idPositionParent, Int64 idFunctionalAreaParent)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalChartJobTitles_Create");
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "idOrganizationalChart", DbType.Int64, idOrganizationalChart);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            _db.AddInParameter(_dbCommand, "idGeographicAreaParent", DbType.Int64, Common.Common.CastValueToNull(idGeographicAreaParent,0));
            _db.AddInParameter(_dbCommand, "idPositionParent", DbType.Int64, Common.Common.CastValueToNull(idPositionParent,0));
            _db.AddInParameter(_dbCommand, "idFunctionalAreaParent", DbType.Int64, Common.Common.CastValueToNull(idFunctionalAreaParent,0));


            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        internal void UpdateRelationship(Int64 idOrganizationalChart, Int64 idOrganization, Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicAreaParent, Int64 idPositionParent, Int64 idFunctionalAreaParent)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalChartJobTitles_Update");
            //Where
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "idOrganizationalChart", DbType.Int64, idOrganizationalChart);
            _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
            _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
            _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
            //Modify
            _db.AddInParameter(_dbCommand, "idGeographicAreaParent", DbType.Int64, Common.Common.CastValueToNull(idGeographicAreaParent, 0));
            _db.AddInParameter(_dbCommand, "idPositionParent", DbType.Int64, Common.Common.CastValueToNull(idPositionParent, 0));
            _db.AddInParameter(_dbCommand, "idFunctionalAreaParent", DbType.Int64, Common.Common.CastValueToNull(idFunctionalAreaParent, 0));

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }
        #endregion

        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadAll(Int64 idOrganization)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_JobTitles_ReadAll");
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
            internal IEnumerable<DbDataRecord> ReadById(Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idOrganization)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_JobTitles_ReadById");
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
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
            internal IEnumerable<DbDataRecord> GetByClassification(Int64 idClassification, Int64 idOrganization)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_JobTitles_ReadByClassification");
                _db.AddInParameter(_dbCommand, "IdClassification", DbType.Int64, idClassification);
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
            internal IEnumerable<DbDataRecord> ReadParent(Int64 idOrganizationalChart, Int64 idOrganization, Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalChartJobTitles_ReadParent");
                _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, idOrganizationalChart);
                _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                _db.AddInParameter(_dbCommand, "IdGeographicArea", DbType.Int64, idGeographicArea);
                _db.AddInParameter(_dbCommand, "IdPosition", DbType.Int64, idPosition);
                _db.AddInParameter(_dbCommand, "IdFunctionalArea", DbType.Int64, idFunctionalArea);
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
            internal IEnumerable<DbDataRecord> ReadRoot(Int64 idOrganizationalChart, Int64 idOrganization)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalChartJobTitles_ReadRoot");
                _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, idOrganizationalChart);
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
            internal IEnumerable<DbDataRecord> ReadByJobTitle(Int64 idOrganizationalChart, Int64 idOrganization, Int64 idGeographicAreaParent, Int64 idPositionParent, Int64 idFunctionalAreaParent)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("DS_OrganizationalChartJobTitles_ReadByParent");
                _db.AddInParameter(_dbCommand, "IdOrganizationalChart", DbType.Int64, idOrganizationalChart);
                _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
                _db.AddInParameter(_dbCommand, "IdGeographicAreaParent", DbType.Int64, idGeographicAreaParent);
                _db.AddInParameter(_dbCommand, "IdPositionParent", DbType.Int64, idPositionParent);
                _db.AddInParameter(_dbCommand, "IdFunctionalAreaParent", DbType.Int64, idFunctionalAreaParent);
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
