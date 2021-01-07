using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    public class OrganizationalCharts_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.OrganizationalChart _OrganizationalChart;
        #endregion

            internal OrganizationalCharts_LG(Entities.OrganizationalChart organizationalChart, Credential credential)
        {
            _Credential = credential;
            _OrganizationalChart = organizationalChart;
        }

       #region Read Functions
            public Entities.OrganizationalChart_LG Item(DS.Entities.Language language)
        {
           
            //Acceso a datos para la opción de idioma
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            Entities.OrganizationalChart_LG _organizationalChart_LG = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationalCharts_LG_ReadById(_OrganizationalChart.IdOrganizationalChart, _OrganizationalChart.IdOrganization, language.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                return new Entities.OrganizationalChart_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),Convert.ToString(_dbRecord["IdLanguage"]));
            }            
            return _organizationalChart_LG;

        }
            public Dictionary<String, Entities.OrganizationalChart_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            Dictionary<String, Entities.OrganizationalChart_LG> _Items = new Dictionary<String, Entities.OrganizationalChart_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationalCharts_LG_ReadAll(_OrganizationalChart.IdOrganizationalChart, _OrganizationalChart.IdOrganization);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.OrganizationalChart_LG _organizationalChart_LG = new Entities.OrganizationalChart_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                _Items.Add(Convert.ToString(_dbRecord["IdLanguage"]), _organizationalChart_LG);
            }
            return _Items;
        }
        #endregion

        #region Write Functions
        public Entities.OrganizationalChart_LG Add(DS.Entities.Language language, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbDirectoryServices.OrganizationalCharts_LG_Create(_OrganizationalChart.IdOrganizationalChart, _OrganizationalChart.IdOrganization,language.IdLanguage, name, description);

                _dbLog.Create("DS_OrganizationalCharts_LG", "OrganizationalCharts_LG", "Add", "IdOrganizationalChart=" + _OrganizationalChart.IdOrganizationalChart + " and IdOrganization=" + _OrganizationalChart.IdOrganization + " and IdLanguage= '" + language.IdLanguage + "'", _Credential.User.IdPerson);

                return new Entities.OrganizationalChart_LG(name, description,language.IdLanguage);
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
        public Entities.OrganizationalChart_LG Modify(DS.Entities.Language language, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbDirectoryServices.OrganizationalCharts_LG_Update(_OrganizationalChart.IdOrganizationalChart, _OrganizationalChart.IdOrganization, language.IdLanguage, name, description);

                _dbLog.Create("DS_OrganizationalCharts_LG", "OrganizationalCharts_LG", "Modify", "IdOrganizationalChart=" + _OrganizationalChart.IdOrganizationalChart + " and IdOrganization=" + _OrganizationalChart.IdOrganization + " and IdLanguage= '" + language.IdLanguage + "'", _Credential.User.IdPerson);

                return new Entities.OrganizationalChart_LG(name, description, language.IdLanguage);
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
        public void Remove(DS.Entities.Language language)
        {
            //controla que no se borre el lenguage default
            if (_Credential.DefaultLanguage.IdLanguage == language.IdLanguage)
            {
                throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
            }
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                _dbDirectoryServices.OrganizationalCharts_LG_Delete(_OrganizationalChart.IdOrganizationalChart, _OrganizationalChart.IdOrganization, language.IdLanguage);

                _dbLog.Create("DS_OrganizationalCharts_LG", "OrganizationalCharts_LG", "Remove", "IdOrganizationalChart=" + _OrganizationalChart.IdOrganizationalChart + " and IdOrganization=" + _OrganizationalChart.IdOrganization + " and IdLanguage= '" + language.IdLanguage + "'", _Credential.User.IdPerson);

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
