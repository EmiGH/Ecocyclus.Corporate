using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Condesus.EMS.Business;
using Condesus.EMS.DataAccess;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    public class AccountingSectors_LG
    {
        #region Internal Properties
        private Entities.AccountingSector _AccountingSector;
        #endregion

        internal AccountingSectors_LG(Entities.AccountingSector accountingSector) 
        {
            _AccountingSector = accountingSector;
        }

        #region Read Functions
        /// <summary>
        /// Retorna ForumCategories_LG
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, Entities.AccountingSector_LG> Items()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Dictionary<String, Entities.AccountingSector_LG> _oItems = new Dictionary<String, Entities.AccountingSector_LG>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.AccountingSectors_LG_ReadAll(_AccountingSector.IdSector);
            //Se modifica con los datos que realmente se tienen que usar...

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Declara e instancia una posicion
                Entities.AccountingSector_LG _accountingSector_LG = new Entities.AccountingSector_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

                //Lo agrego a la coleccion
                _oItems.Add(Convert.ToString(_dbRecord["IdLanguage"]), _accountingSector_LG);

            }
            return _oItems;
        }
        /// <summary>
        /// Retorna ForumCategories_LG por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        public Entities.AccountingSector_LG Item(String IdLanguage)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Entities.AccountingSector_LG _Item = null;

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.AccountingSectors_LG_ReadById(_AccountingSector.IdSector, _AccountingSector.Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Declara e instancia una posicion
                _Item = new Entities.AccountingSector_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

            }
            return _Item;

        }

        #endregion
        #region Write Functions
        //Crea ForumCategories_LG
        public Entities.AccountingSector_LG Create(DS.Entities.Language language, String Name, String Description)
        {
              try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.AccountingSectors_LG_Create(_AccountingSector.IdSector, language.IdLanguage, Name, Description);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_AccountingSectors_LG", "AccountingSectors_LG", "Add", "IdSector=" + _AccountingSector.IdSector + " and IdLanguage= '" + language.IdLanguage + "'", _AccountingSector.Credential.User.IdPerson);

                    return new Entities.AccountingSector_LG(language.IdLanguage, Name, Description);
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
        public void Delete(DS.Entities.Language language)
        {
            //Check to verify that the language option to be deleted is not default language
                if (_AccountingSector.Credential.DefaultLanguage.IdLanguage == language.IdLanguage)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
                }
                try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.AccountingSectors_LG_Delete(_AccountingSector.IdSector, language.IdLanguage);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_AccountingSectors_LG", "AccountingSectors_LG", "Delete", "IdSector=" + _AccountingSector.IdSector + " and IdLanguage= '" + language.IdLanguage + "'", _AccountingSector.Credential.User.IdPerson);
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

        internal void Delete()
        {
            try
            {
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                _dbPerformanceAssessments.AccountingSectors_LG_Delete(_AccountingSector.IdSector);

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

        public void Update(DS.Entities.Language language, String Name, String Description)
        {
                try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.AccountingSectors_LG_Update(_AccountingSector.IdSector, language.IdLanguage, Name, Description);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_AccountingSectors_LG", "AccountingSectors_LG", "Update", "IdSector=" + _AccountingSector.IdSector + " and IdLanguage= '" + language.IdLanguage + "'", _AccountingSector.Credential.User.IdPerson);
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
        #endregion
    }
}
