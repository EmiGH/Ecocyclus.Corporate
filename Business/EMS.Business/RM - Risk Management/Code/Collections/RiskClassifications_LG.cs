using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.RM.Collections
{
    public class RiskClassifications_LG
    {
    #region Internal Properties
        private Credential _Credential;
        private Int64 _IdRiskClassification; //El identificador del Risk classification
        #endregion

        internal RiskClassifications_LG(Int64 idRiskClassification, Credential credential)
        {
            _Credential = credential;
            _IdRiskClassification = idRiskClassification;
        }

        #region Read Functions
        public Entities.RiskClassification_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();

            Entities.RiskClassification_LG _RiskClassification_LG = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbRiskManagement.RiskClassifications_LG_ReadById(_IdRiskClassification, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_RiskClassification_LG == null)
                {
                    _RiskClassification_LG = new Entities.RiskClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _RiskClassification_LG;
                    }
                }
                else
                {
                    return new Entities.RiskClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                }
            }
            return _RiskClassification_LG;

        }
        public Dictionary<String, Entities.RiskClassification_LG> Items()
        {
            
            //Acceso a datos para la opción de idioma
            DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();

            Dictionary<String, Entities.RiskClassification_LG> _RiskClassifications_LG = new Dictionary<String, Entities.RiskClassification_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbRiskManagement.RiskClassifications_LG_ReadAll(_IdRiskClassification);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.RiskClassification_LG _RiskClassification_LG = new Entities.RiskClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                _RiskClassifications_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _RiskClassification_LG);
            }

            return _RiskClassifications_LG;
        }
        #endregion

        #region Write Functions
        public Entities.RiskClassification_LG Add(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbRiskManagement.RiskClassifications_LG_Create(_IdRiskClassification, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.RiskClassification_LG(name, description, idLanguage);
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
        public Entities.RiskClassification_LG Modify(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();

                _dbRiskManagement.RiskClassifications_LG_Update(_IdRiskClassification, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.RiskClassification_LG(name, description, idLanguage);
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
        public void Remove(String idLanguage)
        {
            //controla que no se borre el lenguage default
            if (_Credential.DefaultLanguage.IdLanguage == idLanguage)
            {
                throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
            }
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();

                _dbRiskManagement.RiskClassifications_LG_Delete(_IdRiskClassification, idLanguage, _Credential.User.Person.IdPerson);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                }
                throw ex;
            }
        }
        #endregion
    }
}
