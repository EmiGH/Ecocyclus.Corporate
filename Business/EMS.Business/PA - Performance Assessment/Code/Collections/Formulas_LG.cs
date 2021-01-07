using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class Formulas_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdFormula;
        #endregion

            internal Formulas_LG(Int64 idFormula, Credential credential)
        {
            _Credential = credential;
            _IdFormula = idFormula;
        }

        #region Read Functions
            public Entities.Formula_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.Formula_LG _item = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Formulas_LG_ReadById(_IdFormula, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _item = new Entities.Formula_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
            }
            return _item;
        }
            public Dictionary<String, Entities.Formula_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Dictionary<String, Entities.Formula_LG> _formulas_LG = new Dictionary<String, Entities.Formula_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Formulas_LG_ReadAll(_IdFormula);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Formula_LG _formula_LG = new Entities.Formula_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]));
                _formulas_LG.Add(_dbRecord["IdLanguage"].ToString(), _formula_LG);
            }

            return _formulas_LG;
        }
        #endregion

        #region Write Functions
            public Entities.Formula_LG Add(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbPerformanceAssessments.Formulas_LG_Create(_IdFormula, idLanguage,name, description, _Credential.User.Person.IdPerson);
                return new Entities.Formula_LG(idLanguage,name, description);
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
            public Entities.Formula_LG Modify(String idLanguage,String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                _dbPerformanceAssessments.Formulas_LG_Update(_IdFormula, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.Formula_LG(idLanguage,name, description);
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

                _dbPerformanceAssessments.Formulas_LG_Delete(_IdFormula, idLanguage, _Credential.User.Person.IdPerson);
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
