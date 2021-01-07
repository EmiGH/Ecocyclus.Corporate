using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class CalculateOfTransformationErrors
    {
        #region Internal Properties
        #endregion

        internal CalculateOfTransformationErrors()
        {
        
        }

        
        #region Write Functions
        
        internal void DeleteAll()
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                
                _dbPerformanceAssessments.CalculateOfTransformationErrors_DeleteAll();
                
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

    }
}
