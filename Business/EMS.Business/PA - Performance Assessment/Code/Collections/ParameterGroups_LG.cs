using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class ParameterGroups_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdParameterGroup;
            private Int64 _IdIndicator;
        #endregion

            internal ParameterGroups_LG(Int64 idIndicator, Int64 idParameterGroup, Credential credential)
        {
            _Credential = credential;
            _IdIndicator = idIndicator;
            _IdParameterGroup = idParameterGroup;
        }

        #region Read Functions
            public Entities.ParameterGroup_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.ParameterGroup_LG _item = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ParameterGroups_LG_ReadById(_IdParameterGroup, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _item = new Entities.ParameterGroup_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
            }
            return _item;
        }
            public Dictionary<String, Entities.ParameterGroup_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Dictionary<String, Entities.ParameterGroup_LG> _parameterGroups_LG = new Dictionary<String, Entities.ParameterGroup_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ParameterGroups_LG_ReadAll(_IdParameterGroup);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.ParameterGroup_LG _parameterGroup_LG = new Entities.ParameterGroup_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                _parameterGroups_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _parameterGroup_LG);
            }

            return _parameterGroups_LG;
        }
        #endregion

        #region Write Functions
            public Entities.ParameterGroup_LG Add(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbPerformanceAssessments.ParameterGroups_LG_Create(_IdIndicator, _IdParameterGroup, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.ParameterGroup_LG(idLanguage, name, description);
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
            public Entities.ParameterGroup_LG Modify(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                _dbPerformanceAssessments.ParameterGroups_LG_Update(_IdIndicator, _IdParameterGroup, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.ParameterGroup_LG(idLanguage, name, description);
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
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                _dbPerformanceAssessments.ParameterGroups_LG_Delete(_IdParameterGroup, idLanguage, _Credential.User.Person.IdPerson);
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
