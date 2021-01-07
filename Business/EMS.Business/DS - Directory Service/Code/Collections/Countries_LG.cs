using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    //public class Countries_LG
    //{
    //    #region Internal Properties
    //        private Credential _Credential;
    //        private Int64 _IdCountry; //El identificador del país 
    //    #endregion

    //    internal Countries_LG(Int64 idCountry, Credential credential)
    //    {
    //        _Credential = credential;
    //        _IdCountry = idCountry;
    //    }
        
    //    #region Read Functions
    //        public Entities.Country_LG Item(String idLanguage)
    //        {
    //            //Acceso a datos para la opción de idioma
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //            Entities.Country_LG _item = null;
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Countries_LG_ReadById(_IdCountry, idLanguage);
    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                _item = new Entities.Country_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
    //            }            
    //            return _item;
    //        }
    //        public Dictionary<String, Entities.Country_LG> Items()
    //        {
    //            //Acceso a datos para la opción de idioma
    //            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //            Dictionary<String, Entities.Country_LG> _oCountries_LG = new Dictionary<String, Entities.Country_LG>();
    //            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Countries_LG_ReadAll(_IdCountry);

    //            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
    //            {
    //                Entities.Country_LG _oCountry_LG = new Entities.Country_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]));
    //                _oCountries_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oCountry_LG);
    //            }

    //            return _oCountries_LG;
    //        }    
    //    #endregion

    //    #region Write Functions
    //        public Entities.Country_LG Add(String idLanguage, String name)
    //        {
    //            try
    //            {
    //                //Acceso a datos para la opción de idioma
    //                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
    //                _dbDirectoryServices.Countries_LG_Create(_IdCountry, idLanguage, name, _Credential.User.Person.IdPerson);
    //                return new Entities.Country_LG(idLanguage, name);
    //            }
    //            catch (SqlException ex)
    //            {
    //                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                {
    //                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                }
    //                throw ex;
    //            }
    //        }
    //        public Entities.Country_LG Modify(String idLanguage, String name)
    //        {
    //            try
    //            {
    //                //Acceso a datos para la opción de idioma
    //                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                _dbDirectoryServices.Countries_LG_Update(_IdCountry, idLanguage, name, _Credential.User.Person.IdPerson);
    //                return new Entities.Country_LG(idLanguage, name);
    //            }
    //            catch (SqlException ex)
    //            {
    //                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
    //                {
    //                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
    //                }
    //                throw ex;
    //            }
    //        }
    //        public void Remove(String idLanguage)
    //        {
    //            //Check to verify that the language option to be deleted is not default language
    //            if (_Credential.DefaultLanguage.IdLanguage == idLanguage)
    //            {
    //                throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
    //            }
    //            try
    //            {
    //                //Acceso a datos para la opción de idioma
    //                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

    //                _dbDirectoryServices.Countries_LG_Delete(_IdCountry, idLanguage, _Credential.User.Person.IdPerson);
    //            }
    //            catch (SqlException ex)
    //            {
    //                if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
    //                {
    //                    throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
    //                }
    //                throw ex;
    //            }
    //        }
    //    #endregion

    //}
}
