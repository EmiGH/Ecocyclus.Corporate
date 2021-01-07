using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.EP.Collections
{
    public class ExtendedProperties_LG
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdExtendedProperty;
        #endregion

        internal ExtendedProperties_LG(Int64 idExtendedProperty, Credential credential)
        {
            _Credential = credential;
            _IdExtendedProperty = idExtendedProperty;        
        }

        #region Read Functions
        public Entities.ExtendedProperty_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

            Entities.ExtendedProperty_LG _item = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExtendedProperties.ExtendedProperties_LG_ReadById(_IdExtendedProperty, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _item = new Entities.ExtendedProperty_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
            }
            return _item;
        }
        public Dictionary<String, Entities.ExtendedProperty_LG> Items()
        {        
            //Acceso a datos para la opción de idioma
            DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

            Dictionary<String, Entities.ExtendedProperty_LG> _extendedProperties_LG = new Dictionary<String, Entities.ExtendedProperty_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbExtendedProperties.ExtendedProperties_LG_ReadAll(_IdExtendedProperty);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.ExtendedProperty_LG _extendedProperty_LG = new Entities.ExtendedProperty_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                _extendedProperties_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _extendedProperty_LG);
            }

            return _extendedProperties_LG;
        }
        #endregion

        #region Write Functions
        public Entities.ExtendedProperty_LG Add(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbExtendedProperties.ExtendedProperties_LG_Create(_IdExtendedProperty, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.ExtendedProperty_LG(idLanguage, name, description);
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
        public Entities.ExtendedProperty_LG Modify(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                _dbExtendedProperties.ExtendedProperties_LG_Update(_IdExtendedProperty, idLanguage, name, description, _Credential.User.Person.IdPerson);
                return new Entities.ExtendedProperty_LG(idLanguage, name, description);
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
            if (_Credential.CurrentLanguage.IdLanguage == idLanguage)
            {
                throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
            }
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                _dbExtendedProperties.ExtendedProperties_LG_Delete(_IdExtendedProperty, idLanguage, _Credential.User.Person.IdPerson);
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
