using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    public class TimeUnits_LG
    {

        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdTimeUnit;
        #endregion

        internal TimeUnits_LG(Int64 idTimeUnit, Credential credential)
        {
            _Credential = credential;
            _IdTimeUnit = idTimeUnit;
        }

        #region Read Functions
        public Entities.TimeUnit_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
            //DataAccess.PF.Entities.TimeUnits_LG _dbTimeUnits_LG = _dbProcessesFramework.TimeUnits_LG;

            Entities.TimeUnit_LG _item = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.TimeUnits_LG_ReadById(_IdTimeUnit, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _item = new Entities.TimeUnit_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
            }
            return _item;
        }
        public Dictionary<String, Entities.TimeUnit_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
            //DataAccess.PF.Entities.TimeUnits_LG _dbTimeUnits_LG = _dbProcessesFramework.TimeUnits_LG;

            Dictionary<String, Entities.TimeUnit_LG> _timeUnits_LG = new Dictionary<String, Entities.TimeUnit_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.TimeUnits_LG_ReadAll(_IdTimeUnit);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.TimeUnit_LG _extendedProperty_LG = new Entities.TimeUnit_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
                _timeUnits_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _extendedProperty_LG);
            }

            return _timeUnits_LG;
        }
        #endregion

        #region Write Functions
        public Entities.TimeUnit_LG Add(String idLanguage, String name)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.TimeUnits_LG _dbTimeUnits_LG = _dbProcessesFramework.TimeUnits_LG;

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbProcessesFramework.TimeUnits_LG_Create(_IdTimeUnit, idLanguage, name, _Credential.User.Person.IdPerson);
                return new Entities.TimeUnit_LG(idLanguage, name);
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
        public Entities.TimeUnit_LG Modify(String idLanguage, String name)
        {
             try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.TimeUnits_LG _dbTimeUnits_LG = _dbProcessesFramework.TimeUnits_LG;

                _dbProcessesFramework.TimeUnits_LG_Update(_IdTimeUnit, idLanguage, name, _Credential.User.Person.IdPerson);
                return new Entities.TimeUnit_LG(idLanguage, name);
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
              //Check to verify that the language option to be deleted is not default language
            if (_Credential.DefaultLanguage.IdLanguage == idLanguage)
            {
                throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
            }
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.TimeUnits_LG _dbTimeUnits_LG = _dbProcessesFramework.TimeUnits_LG;

                _dbProcessesFramework.TimeUnits_LG_Delete(_IdTimeUnit, idLanguage, _Credential.User.Person.IdPerson);
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
