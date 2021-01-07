using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data.SqlClient;

namespace Condesus.EMS.Business.IA.Collections
{
    internal class ExceptionHistories
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal ExceptionHistories(Credential credential)
        {
            _Credential = credential;
        }

        #region Write Functions
        public void Remove(Entities.Exception exception)
        {
            try
            {
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                _dbImprovementActions.ExceptionHistories_Delete(exception.IdException);

            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
            #endregion
    }
}
