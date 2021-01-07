using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class Magnitudes_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdMagnitud;
        #endregion

        internal Magnitudes_LG(Int64 idMagnitud, Credential credential)
        {
            _Credential = credential;
            _IdMagnitud = idMagnitud;
        }

        #region Read Functions
        public Entities.Magnitud_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.Magnitud_LG _item = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Magnitudes_LG_ReadById(_IdMagnitud, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _item = new Entities.Magnitud_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
            }
            return _item;
        }
        public Dictionary<String, Entities.Magnitud_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Dictionary<String, Entities.Magnitud_LG> _Magnitudes_LG = new Dictionary<String, Entities.Magnitud_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Magnitudes_LG_ReadAll(_IdMagnitud);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Magnitud_LG _Magnitud_LG = new Entities.Magnitud_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
                _Magnitudes_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _Magnitud_LG);
            }

            return _Magnitudes_LG;
        }
        #endregion

        #region Write Functions
        public Entities.Magnitud_LG Add(String idLanguage, String name)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbPerformanceAssessments.Magnitudes_LG_Create(_IdMagnitud, idLanguage, name, _Credential.User.Person.IdPerson);
                return new Entities.Magnitud_LG(idLanguage, name);
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
        public Entities.Magnitud_LG Modify(String idLanguage, String name)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                _dbPerformanceAssessments.Magnitudes_LG_Update(_IdMagnitud, idLanguage, name, _Credential.User.Person.IdPerson);
                return new Entities.Magnitud_LG(idLanguage, name);
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

                _dbPerformanceAssessments.Magnitudes_LG_Delete(_IdMagnitud, idLanguage, _Credential.User.Person.IdPerson);
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
