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
    public class Qualities_LG
    {
        #region Internal Properties
        private Entities.Quality _Quality;
        #endregion

        internal Qualities_LG(Entities.Quality quality) 
        {
            _Quality = quality;
        }

        #region Read Functions
        /// <summary>
        /// Retorna ForumCategories_LG
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, Entities.Quality_LG> Items()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Dictionary<String, Entities.Quality_LG> _oItems = new Dictionary<String, Entities.Quality_LG>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Qualities_LG_ReadAll(_Quality.IdQuality);
            //Se modifica con los datos que realmente se tienen que usar...

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Declara e instancia una posicion
                Entities.Quality_LG _quality_LG = new Entities.Quality_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

                //Lo agrego a la coleccion
                _oItems.Add(Convert.ToString(_dbRecord["IdLanguage"]), _quality_LG);

            }
            return _oItems;
        }
        /// <summary>
        /// Retorna ForumCategories_LG por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        public Entities.Quality_LG Item(String IdLanguage)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Entities.Quality_LG _Item = null;

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Qualities_LG_ReadById(_Quality.IdQuality, _Quality.Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Declara e instancia una posicion
                _Item = new Entities.Quality_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

            }
            return _Item;

        }

        #endregion
        #region Write Functions
        //Crea ForumCategories_LG
        public Entities.Quality_LG Create(DS.Entities.Language language, String name, String description)
        {
              try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.Qualities_LG_Create(_Quality.IdQuality, language.IdLanguage, name, description);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_Qualities_LG", "Qualities_LG", "Add", "IdQuality=" + _Quality.IdQuality + " and IdLanguage= '" + language.IdLanguage + "'", _Quality.Credential.User.IdPerson);

                    return new Entities.Quality_LG(language.IdLanguage, name, description);
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
            if (_Quality.Credential.DefaultLanguage.IdLanguage == language.IdLanguage)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
                }
                try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.Qualities_LG_Delete(_Quality.IdQuality, language.IdLanguage);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_Qualities_LG", "Qualities_LG", "Delete", "IdQuality=" + _Quality.IdQuality + " and IdLanguage= '" + language.IdLanguage + "'", _Quality.Credential.User.IdPerson);
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

                _dbPerformanceAssessments.Qualities_LG_Delete(_Quality.IdQuality);

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

        public void Update(DS.Entities.Language language, String name, String description)
        {
                try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.Qualities_LG_Update(_Quality.IdQuality, language.IdLanguage, name, description);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_Qualities_LG", "Qualities_LG", "Update", "IdQuality=" + _Quality.IdQuality + " and IdLanguage= '" + language.IdLanguage + "'", _Quality.Credential.User.IdPerson);
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
