using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    public class ProcessClassifications_LG
    {
        #region Internal Properties
        private Credential _Credential;
        private Entities.ProcessClassification _ProcessClassification; //El identificador del process classification
        #endregion

        internal ProcessClassifications_LG(Entities.ProcessClassification processClassification, Credential credential)
        {
            _Credential = credential;
            _ProcessClassification = processClassification;                        
        }

        #region Read Functions
        public Entities.ProcessClassification_LG Item(String idLanguage)
        {
           //Acceso a datos para la opción de idioma
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
            //DataAccess.PF.Entities.ProcessClassifications_LG _dbProcessClassifications_LG = _dbProcessesFramework.ProcessClassifications_LG;
            //DataAccess.PF.ProcessClassifications_LG _dbProcessClassifications_LG = new Condesus.EMS.DataAccess.PF.ProcessClassifications_LG();

            Entities.ProcessClassification_LG _processClassification_LG = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessClassifications_LG_ReadById(_ProcessClassification.IdProcessClassification, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_processClassification_LG == null)
                {
                    _processClassification_LG = new Entities.ProcessClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _processClassification_LG;
                    }
                }
                else
                {
                    return new Entities.ProcessClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                }
            }
            return _processClassification_LG;

        }
        public Dictionary<String, Entities.ProcessClassification_LG> Items()
        {
    
            //Acceso a datos para la opción de idioma
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
            //DataAccess.PF.Entities.ProcessClassifications_LG _dbProcessClassifications_LG = _dbProcessesFramework.ProcessClassifications_LG;
            //DataAccess.PF.ProcessClassifications_LG _dbProcessClassifications_LG = new Condesus.EMS.DataAccess.PF.ProcessClassifications_LG();

            Dictionary<String, Entities.ProcessClassification_LG> _processClassifications_LG = new Dictionary<String, Entities.ProcessClassification_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessClassifications_LG_ReadAll(_ProcessClassification.IdProcessClassification);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.ProcessClassification_LG _processClassification_LG = new Entities.ProcessClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                _processClassifications_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _processClassification_LG);
            }

            return _processClassifications_LG;
        }
        #endregion

        #region Write Functions
        public Entities.ProcessClassification_LG Add(String idLanguage, String name , String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbProcessesFramework.ProcessClassifications_LG_Create(_ProcessClassification.IdProcessClassification, idLanguage, name, description);
                return new Entities.ProcessClassification_LG(name, description, idLanguage);
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
        public Entities.ProcessClassification_LG Modify(String idLanguage, String name, String description)
        {
           try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessClassifications_LG _dbProcessClassifications_LG = _dbProcessesFramework.ProcessClassifications_LG;
                //DataAccess.PF.ProcessClassifications_LG _dbProcessClassifications_LG = new Condesus.EMS.DataAccess.PF.ProcessClassifications_LG();

                _dbProcessesFramework.ProcessClassifications_LG_Update(_ProcessClassification.IdProcessClassification, idLanguage, name, description);
                return new Entities.ProcessClassification_LG(name, description, idLanguage);
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
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                _dbProcessesFramework.ProcessClassifications_LG_Delete(_ProcessClassification.IdProcessClassification, idLanguage);
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
