using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.NT.Entities
{
    internal class MailTransformationPeople
    {
        internal MailTransformationPeople() { }

        #region Read Functions

        internal IEnumerable<DbDataRecord> ReadByTransformation(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailTransformationPeople_ReadByTransformation");
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

        #endregion

        #region Write Functions

        internal void Create(Int64 IdTransformation, Int64 idPerson, Int64 idOrganization, Int64 idContacEmail)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailTransformationPeople_Create");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdContactEmail", DbType.Int64, idContacEmail); 
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);

        }

        internal void Delete(Int64 IdTransformation)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailTransformationPeople_DeleteByTransformation");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Delete(Int64 IdTransformation, Int64 idPerson, Int64 idOrganization, Int64 idContacEmail)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailTransformationPeople_Delete");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdContactEmail", DbType.Int64, idContacEmail); 
            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }

        internal void Update(Int64 IdTransformation, Int64 idPerson, Int64 idOrganization, Int64 idContacEmail)
        {
            Database _db = DatabaseFactory.CreateDatabase();

            DbCommand _dbCommand = _db.GetStoredProcCommand("NT_MailTransformationPeople_Update");
            _db.AddInParameter(_dbCommand, "IdTransformation", DbType.Int64, IdTransformation);
            _db.AddInParameter(_dbCommand, "IdPerson", DbType.Int64, idPerson);
            _db.AddInParameter(_dbCommand, "IdOrganization", DbType.Int64, idOrganization);
            _db.AddInParameter(_dbCommand, "IdContactEmail", DbType.Int64, idContacEmail); 

            //Ejecuta el comando
            _db.ExecuteNonQuery(_dbCommand);
        }
        #endregion
    }
}
