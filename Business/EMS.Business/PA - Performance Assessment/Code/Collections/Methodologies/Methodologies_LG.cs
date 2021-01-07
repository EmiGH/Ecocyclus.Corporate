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
    public class Methodologies_LG
    {
        #region Internal Properties
        private Entities.Methodology _Methodology;
        #endregion

        internal Methodologies_LG(Entities.Methodology methodology) 
        {
            _Methodology = methodology;
        }

        #region Read Functions
        /// <summary>
        /// Retorna ForumCategories_LG
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, Entities.Methodology_LG> Items()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Dictionary<String, Entities.Methodology_LG> _oItems = new Dictionary<String, Entities.Methodology_LG>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Methodologies_LG_ReadAll(_Methodology.IdMethodology);
            //Se modifica con los datos que realmente se tienen que usar...

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Declara e instancia una posicion
                Entities.Methodology_LG _methodology_LG = new Entities.Methodology_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["MethodName"]), Convert.ToString(_dbRecord["MethodType"]), Convert.ToString(_dbRecord["Description"]));

                //Lo agrego a la coleccion
                _oItems.Add(Convert.ToString(_dbRecord["IdLanguage"]), _methodology_LG);

            }
            return _oItems;
        }
        /// <summary>
        /// Retorna ForumCategories_LG por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        public Entities.Methodology_LG Item(String IdLanguage)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Entities.Methodology_LG _Item = null;

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Methodologies_LG_ReadById(_Methodology.IdMethodology, _Methodology.Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Declara e instancia una posicion
                _Item = new Entities.Methodology_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["MethodName"]), Convert.ToString(_dbRecord["MethodType"]), Convert.ToString(_dbRecord["Description"]));

            }
            return _Item;

        }

        #endregion

        #region Write Functions
        //Crea ForumCategories_LG
        public Entities.Methodology_LG Create(DS.Entities.Language language, String methodName, String methodType, String description)
        {
              try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.Methodologies_LG_Create(_Methodology.IdMethodology, language.IdLanguage, methodName,methodType, description);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_Methodologies_LG", "Methodologies_LG", "Add", "IdMethodology=" + _Methodology.IdMethodology + " and IdLanguage= '" + language.IdLanguage + "'", _Methodology.Credential.User.IdPerson);

                    return new Entities.Methodology_LG(language.IdLanguage,methodName ,methodType, description);
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
            if (_Methodology.Credential.DefaultLanguage.IdLanguage == language.IdLanguage)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
                }
                try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.Methodologies_LG_Delete(_Methodology.IdMethodology, language.IdLanguage);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_Methodologies_LG", "Methodologies_LG", "Delete", "IdMethodology=" + _Methodology.IdMethodology + " and IdLanguage= '" + language.IdLanguage + "'", _Methodology.Credential.User.IdPerson);
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

                _dbPerformanceAssessments.Methodologies_LG_Delete(_Methodology.IdMethodology);

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

        public void Update(DS.Entities.Language language, String methodName, String methodType, String description)
        {
                try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.Methodologies_LG_Update(_Methodology.IdMethodology, language.IdLanguage, methodName,methodType , description);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_Methodologies_LG", "Methodologies_LG", "Update", "IdMethodology=" + _Methodology.IdMethodology + " and IdLanguage= '" + language.IdLanguage + "'", _Methodology.Credential.User.IdPerson);
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
