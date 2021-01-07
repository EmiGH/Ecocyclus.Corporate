using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.EP.Collections
{
    public class ExtendedPropertyClassifications_LG
    {
        #region Internal Properties
        private Credential _Credential;
            private Int64 _IdExtendedPropertyClassification;
        #endregion

        internal ExtendedPropertyClassifications_LG(Int64 idExtendedPropertyClassification, Credential credential)
        {
            _Credential = credential;
            _IdExtendedPropertyClassification = idExtendedPropertyClassification;
        }

        #region Read Functions
        public Entities.ExtendedPropertyClassification_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

            Entities.ExtendedPropertyClassification_LG _item = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExtendedProperties.ExtendedPropertyClassifications_LG_ReadById(_IdExtendedPropertyClassification, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _item = new Entities.ExtendedPropertyClassification_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
            }
            return _item;
        }
        public Dictionary<String, Entities.ExtendedPropertyClassification_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

            Dictionary<String, Entities.ExtendedPropertyClassification_LG> _extendedPropertyClassifications_LG = new Dictionary<String, Entities.ExtendedPropertyClassification_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExtendedProperties.ExtendedPropertyClassifications_LG_ReadAll (_IdExtendedPropertyClassification);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.ExtendedPropertyClassification_LG _extendedPropertyClassification_LG = new Entities.ExtendedPropertyClassification_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                _extendedPropertyClassifications_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _extendedPropertyClassification_LG);
            }

            return _extendedPropertyClassifications_LG;
        }
        #endregion

        #region Write Functions
        public Entities.ExtendedPropertyClassification_LG Add(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbExtendedProperties.ExtendedPropertyClassifications_LG_Create(_IdExtendedPropertyClassification, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.ExtendedPropertyClassification_LG(idLanguage, name, description);
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
        public Entities.ExtendedPropertyClassification_LG Modify(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                _dbExtendedProperties.ExtendedPropertyClassifications_LG_Update(_IdExtendedPropertyClassification, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.ExtendedPropertyClassification_LG(idLanguage, name, description );
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
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                _dbExtendedProperties.ExtendedPropertyClassifications_LG_Delete(_IdExtendedPropertyClassification, idLanguage, _Credential.User.Person.IdPerson);
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
