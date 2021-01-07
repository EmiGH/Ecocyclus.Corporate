using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    public class Processes_LG
    {
        #region Internal Properties
        private Credential _Credential;
            private Int64 _IdProcess; //El identificador del process classification
        #endregion

        internal Processes_LG(Int64 idProcess, Credential credential)
        {
            _Credential = credential;
            _IdProcess = idProcess;
        }

        #region Read Functions

        public Entities.Process_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
            //DataAccess.PF.Entities.Processes_LG _dbProcesses_LG = _dbProcessesFramework.Processes_LG;

            Entities.Process_LG _process_LG = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.Processes_LG_ReadById(_IdProcess, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_process_LG == null)
                {
                    _process_LG = new Entities.Process_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]));
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _process_LG;
                    }
                }
                else
                {
                    return new Entities.Process_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]));
                }
            }
            return _process_LG;

        }
        public Dictionary<String, Entities.Process_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
            //DataAccess.PF.Entities.Processes_LG _dbProcesses_LG = _dbProcessesFramework.Processes_LG;

            Dictionary<String, Entities.Process_LG> _processes_LG = new Dictionary<String, Entities.Process_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.Processes_LG_ReadAll(_IdProcess);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Process_LG _process_LG = new Entities.Process_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]));
                _processes_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _process_LG);
            }

            return _processes_LG;
        }

      
        #endregion

        #region Write Functions
        public Entities.Process_LG Add(String idLanguage, String title, String purpose, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                
                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbProcessesFramework.Processes_LG_Create(_IdProcess, idLanguage, title, purpose, description);

                return new Entities.Process_LG(idLanguage, title, purpose, description);
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
        public Entities.Process_LG Modify(String idLanguage, String title, String purpose, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                
                _dbProcessesFramework.Processes_LG_Update(_IdProcess, idLanguage, title, purpose, description);

                return new Entities.Process_LG(idLanguage, title, purpose, description);
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
                
                _dbProcessesFramework.Processes_LG_Delete(_IdProcess, idLanguage);
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
        /// <summary>
        /// Borra por idProcess, todos los lenguajes
        /// </summary>
        public void Remove()
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                _dbProcessesFramework.Processes_LG_Delete(_IdProcess);
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
