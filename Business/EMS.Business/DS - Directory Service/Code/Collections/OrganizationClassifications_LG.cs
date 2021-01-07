using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.DS.Collections
{
    public class OrganizationClassifications_LG
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdOrganizationClassification; //El identificador del Organization classification
        #endregion

        internal OrganizationClassifications_LG(Int64 idOrganizationClassification, Credential credential)
        {
            _Credential = credential;
            _IdOrganizationClassification = idOrganizationClassification;
        }

        #region Read Functions
        public Entities.OrganizationClassification_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            Entities.OrganizationClassification_LG _OrganizationClassification_LG = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationClassifications_LG_ReadById(_IdOrganizationClassification, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_OrganizationClassification_LG == null)
                {
                    _OrganizationClassification_LG = new Entities.OrganizationClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _OrganizationClassification_LG;
                    }
                }
                else
                {
                    return new Entities.OrganizationClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                }
            }
            return _OrganizationClassification_LG;

        }
        public Dictionary<String, Entities.OrganizationClassification_LG> Items()
        {

            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            Dictionary<String, Entities.OrganizationClassification_LG> _OrganizationClassifications_LG = new Dictionary<String, Entities.OrganizationClassification_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationClassifications_LG_ReadAll(_IdOrganizationClassification);
            
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.OrganizationClassification_LG _OrganizationClassification_LG = new Entities.OrganizationClassification_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                _OrganizationClassifications_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _OrganizationClassification_LG);
            }

            return _OrganizationClassifications_LG;
        }
        #endregion

        #region Write Functions
        public Entities.OrganizationClassification_LG Add(String idLanguage, String name, String description)
        {
            try
            {
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                DataAccess.Log.Log _log = new Condesus.EMS.DataAccess.Log.Log();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbDirectoryServices.OrganizationClassifications_LG_Create(_IdOrganizationClassification, idLanguage, name, description);

                _log.Create("DS_OrganizationClassifications_LG", "OrganizationClassifications_LG", "Add", "IdOrganizationClassification = " + _IdOrganizationClassification + " and idlanguage = '" + idLanguage + "'", _Credential.User.IdPerson);

                return new Entities.OrganizationClassification_LG(name, description, idLanguage);
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
        public Entities.OrganizationClassification_LG Modify(String idLanguage, String name, String description)
        {
            try
            {
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                DataAccess.Log.Log _log = new Condesus.EMS.DataAccess.Log.Log();

                _dbDirectoryServices.OrganizationClassifications_LG_Update(_IdOrganizationClassification, idLanguage, name, description);

                _log.Create("DS_OrganizationClassifications_LG", "OrganizationClassifications_LG", "Modify", "IdOrganizationClassification = " + _IdOrganizationClassification + " and idlanguage = '" + idLanguage + "'", _Credential.User.IdPerson);

                return new Entities.OrganizationClassification_LG(name, description, idLanguage);
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
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                DataAccess.Log.Log _log = new Condesus.EMS.DataAccess.Log.Log();

                _dbDirectoryServices.OrganizationClassifications_LG_Delete(_IdOrganizationClassification, idLanguage);

                _log.Create("DS_OrganizationClassifications_LG", "OrganizationClassifications_LG", "Remove", "IdOrganizationClassification = " + _IdOrganizationClassification + " and idlanguage = '" + idLanguage + "'", _Credential.User.IdPerson);

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
