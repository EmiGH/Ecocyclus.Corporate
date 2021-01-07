using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.PA.Entities
{
    internal class AccountingActivities
    {
        internal AccountingActivities()
        { }

        #region Read Functions
            internal IEnumerable<DbDataRecord> ReadById(Int64 idActivity, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingActivities_ReadById");
                _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, idActivity);
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
            internal IEnumerable<DbDataRecord> ReadRoot(String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingActivities_ReadRoot");
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
            internal IEnumerable<DbDataRecord> ReadByActivity(Int64 idParentActivity, String idLanguage)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingActivities_ReadByParent");
                _db.AddInParameter(_dbCommand, "IdParentActivity", DbType.Int64, idParentActivity);
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
            internal IEnumerable<DbDataRecord> ReadTotalMeasurementResultByIndicator(Int64 idScope, Int64 idActivity, Int64 idIndicatorColumnGas, DateTime startDate, DateTime endDate)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingActivities_ReadTotalMeasurementResultByIndicator");
                _db.AddInParameter(_dbCommand, "IdScope", DbType.Int64, idScope);
                _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, idActivity);
                _db.AddInParameter(_dbCommand, "IdIndicatorColumnGas", DbType.Int64, idIndicatorColumnGas);
                _db.AddInParameter(_dbCommand, "StartDate", DbType.DateTime, startDate);
                _db.AddInParameter(_dbCommand, "EndDate", DbType.DateTime, endDate);

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
            /// <summary>
            /// Realiza el alta de una classificacion 
            /// </summary>
            /// <param name="IdOrganization"></param>
            /// <param name="IdLanguage"></param>
            /// <param name="name"></param>
            /// <param name="Description"></param>
            /// <param name="IdLogPerson"></param>
            /// <returns>Retorna el id de la clasificacion creada</returns>
            internal Int64 Create(Int64 idParentActivity)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingActivities_Create");
                _db.AddInParameter(_dbCommand, "IdParentActivity", DbType.Int64, Common.Common.CastValueToNull(idParentActivity, DBNull.Value));
                //Parámetro de salida
                _db.AddOutParameter(_dbCommand, "IdActivity", DbType.Int64, 18);
                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);

                //Retorna el identificador
                return Convert.ToInt64(_db.GetParameterValue(_dbCommand, "IdActivity"));
            }

            /// <summary>
            /// Borra una clasificacion de una Organizacion
            /// </summary>
            /// <param name="IdOrganization"></param>
            /// <param name="IdClassification"></param>
            /// <param name="IdLogPerson"></param>
            internal void Delete(Int64 idActivity)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingActivities_Delete");
                _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, idActivity);

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }

            /// <summary>
            /// Modifica el nombre o la descripcion de una classificacion pero usa el de SP de LG 
            /// </summary>
            /// <param name="IdClassification"></param>
            /// <param name="IdOrganization"></param>
            /// <param name="IdLanguage"></param>
            /// <param name="name"></param>
            /// <param name="Description"></param>
            /// <param name="IdLogPerson"></param>
            internal void Update(Int64 idActivity, Int64 idParentActivity)
            {
                Database _db = DatabaseFactory.CreateDatabase();

                DbCommand _dbCommand = _db.GetStoredProcCommand("PA_AccountingActivities_Update");
                _db.AddInParameter(_dbCommand, "IdActivity", DbType.Int64, idActivity);
                _db.AddInParameter(_dbCommand, "IdParentActivity", DbType.Int64, Common.Common.CastValueToNull(idParentActivity, DBNull.Value));

                //Ejecuta el comando
                _db.ExecuteNonQuery(_dbCommand);
            }
        #endregion

    }
}
