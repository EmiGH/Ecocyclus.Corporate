using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.RM
{
   
    public class RiskManagement
    {
        public RiskManagement()
        { }

        # region RiskClassifications
        #region Read Functions

        public IEnumerable<DbDataRecord> RiskClassifications_ReadAll(String idLanguage, String className, Int64 idPerson)
        {
            return new Entities.RiskClassifications().ReadAll(idLanguage);
            //return new Entities.RiskClassifications().ReadAll(idLanguage, className, idPerson);
        }
        public IEnumerable<DbDataRecord> RiskClassifications_ReadById(Int64 idRiskClassification, String idLanguage, String className, Int64 idPerson)
        {
            return new Entities.RiskClassifications().ReadById(idRiskClassification, idLanguage);
            //return new Entities.RiskClassifications().ReadById(idRiskClassification, idLanguage, className, idPerson);
        }
        //public IEnumerable<DbDataRecord> GetAllRoots(String idLanguage, String className, Int64 idPerson)
        //{
        //    return new Entities.RiskClassifications().GetAllRoots(idLanguage, className, idPerson);
        //}
        public IEnumerable<DbDataRecord> RiskClassifications_GetRoot(String idLanguage, String className, Int64 idPerson)
        {
            return new Entities.RiskClassifications().GetRoot(idLanguage);
            //return new Entities.RiskClassifications().GetRoot(idLanguage, className, idPerson);
        }
        public IEnumerable<DbDataRecord> RiskClassifications_GetByParent(Int64 idRiskClassificationParent, String idLanguage, String className, Int64 idPerson)
        {
            return new Entities.RiskClassifications().GetByParent(idRiskClassificationParent, idLanguage);
            //return new Entities.RiskClassifications().GetByParent(idRiskClassificationParent, idLanguage, className, idPerson);
        }
        #endregion

        #region Write Functions
        public Int64 RiskClassifications_Create(Int64 idParentRiskClassification, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            return new Entities.RiskClassifications().Create(idParentRiskClassification, idLanguage, name, Description, idLogPerson);
        }
        public void RiskClassifications_Delete(Int64 idRiskClassification, Int64 idLogPerson)
        {
            new Entities.RiskClassifications().Delete(idRiskClassification, idLogPerson);
        }
        public void RiskClassifications_Update(Int64 idRiskClassification, Int64 idParentRiskClassification, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            new Entities.RiskClassifications().Update(idRiskClassification, idParentRiskClassification, idLanguage, name, Description, idLogPerson);
        }
        #endregion
        #endregion

        # region RiskClassifications_LG
        #region Read Functions

        public IEnumerable<DbDataRecord> RiskClassifications_LG_ReadAll(Int64 idRiskClassification)
        {
            return new Entities.RiskClassifications_LG().ReadAll(idRiskClassification);
        }

        public IEnumerable<DbDataRecord> RiskClassifications_LG_ReadById(Int64 idRiskClassification, String idLanguage)
        {
            return new Entities.RiskClassifications_LG().ReadById(idRiskClassification, idLanguage);
        }

        #endregion

        #region Write Functions

        public void RiskClassifications_LG_Create(Int64 idRiskClassification, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            new Entities.RiskClassifications_LG().Create(idRiskClassification, idLanguage, name, Description, idLogPerson);
        }

        public void RiskClassifications_LG_Delete(Int64 idRiskClassification, String idLanguage, Int64 idLogPerson)
        {
            new Entities.RiskClassifications_LG().Delete(idRiskClassification, idLanguage, idLogPerson);
        }

        public void RiskClassifications_LG_Update(Int64 idRiskClassification, String idLanguage, String name, String Description, Int64 idLogPerson)
        {
            new Entities.RiskClassifications_LG().Update(idRiskClassification, idLanguage, name, Description, idLogPerson);
        }

        #endregion
        #endregion

    }
}
