using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class Parameters_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdParameter;
        #endregion

            internal Parameters_LG(Int64 idParameter, Credential credential)
        {
            _Credential = credential;
            _IdParameter = idParameter;
        }

        #region Read Functions
            public Entities.Parameter_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.Parameter_LG _item = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Parameters_LG_ReadById(_IdParameter, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _item = new Entities.Parameter_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Description"]));
            }
            return _item;
        }
            public Dictionary<String, Entities.Parameter_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Dictionary<String, Entities.Parameter_LG> _parameters_LG = new Dictionary<String, Entities.Parameter_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Parameters_LG_ReadAll(_IdParameter);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Parameter_LG _parameter_LG = new Entities.Parameter_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Description"]));
                _parameters_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _parameter_LG);
            }

            return _parameters_LG;
        }
        #endregion

        #region Write Functions
            public Entities.Parameter_LG Add(String idLanguage, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbPerformanceAssessments.Parameters_LG_Create(_IdParameter, idLanguage, description, _Credential.User.Person.IdPerson);
                return new Entities.Parameter_LG(idLanguage, description);
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
            public Entities.Parameter_LG Modify(String idLanguage, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                _dbPerformanceAssessments.Parameters_LG_Update(_IdParameter, idLanguage, description, _Credential.User.Person.IdPerson);
                return new Entities.Parameter_LG(idLanguage, description);
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

                _dbPerformanceAssessments.Parameters_LG_Delete(_IdParameter, idLanguage, _Credential.User.Person.IdPerson);
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
