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
    public class CalculateOfTransformations_LG
    {
        #region Internal Properties
        private Entities.CalculateOfTransformation _CalculateOfTransformation;
        #endregion

        internal CalculateOfTransformations_LG(Entities.CalculateOfTransformation calculateOfTransformation) 
        {
            _CalculateOfTransformation = calculateOfTransformation;
        }

        #region Read Functions
        /// <summary>
        /// Retorna ForumCategories_LG
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, Entities.CalculateOfTransformation_LG> Items()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Dictionary<String, Entities.CalculateOfTransformation_LG> _oItems = new Dictionary<String, Entities.CalculateOfTransformation_LG>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Constants_LG_ReadAll(_CalculateOfTransformation.IdTransformation);
            //Se modifica con los datos que realmente se tienen que usar...

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Declara e instancia una posicion
                Entities.CalculateOfTransformation_LG _CalculateOfTransformation_LG = new Entities.CalculateOfTransformation_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

                //Lo agrego a la coleccion
                _oItems.Add(Convert.ToString(_dbRecord["IdLanguage"]), _CalculateOfTransformation_LG);

            }
            return _oItems;
        }
        /// <summary>
        /// Retorna ForumCategories_LG por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        public Entities.CalculateOfTransformation_LG Item(String IdLanguage)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Entities.CalculateOfTransformation_LG _Item = null;

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Constants_LG_ReadById(_CalculateOfTransformation.IdTransformation, _CalculateOfTransformation.Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Declara e instancia una posicion
                _Item = new Entities.CalculateOfTransformation_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

            }
            return _Item;

        }

        #endregion
        #region Write Functions
        //Crea ForumCategories_LG
        public Entities.CalculateOfTransformation_LG Create(DS.Entities.Language language, String Name, String Description)
        {
              try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.CalculateOfTransformations_LG_Create(_CalculateOfTransformation.IdTransformation, language.IdLanguage, Name, Description);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_CalculateOfTransformations_LG", "CalculateOfTransformations_LG", "Add", "IdTransformation=" + _CalculateOfTransformation.IdTransformation + " and IdLanguage= '" + language.IdLanguage + "'", _CalculateOfTransformation.Credential.User.IdPerson);

                    return new Entities.CalculateOfTransformation_LG(language.IdLanguage, Name, Description);
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
                if (_CalculateOfTransformation.Credential.DefaultLanguage.IdLanguage == language.IdLanguage)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
                }
                try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.CalculateOfTransformations_LG_Delete(_CalculateOfTransformation.IdTransformation, language.IdLanguage);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_CalculateOfTransformations_LG", "CalculateOfTransformations_LG", "Delete", "IdTransformation=" + _CalculateOfTransformation.IdTransformation + " and IdLanguage= '" + language.IdLanguage + "'", _CalculateOfTransformation.Credential.User.IdPerson);
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

                _dbPerformanceAssessments.CalculateOfTransformations_LG_Delete(_CalculateOfTransformation.IdTransformation);

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

                    _dbPerformanceAssessments.CalculateOfTransformations_LG_Update(_CalculateOfTransformation.IdTransformation, language.IdLanguage, Name, Description);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_CalculateOfTransformations_LG", "CalculateOfTransformations_LG", "Update", "IdTransformation=" + _CalculateOfTransformation.IdTransformation + " and IdLanguage= '" + language.IdLanguage + "'", _CalculateOfTransformation.Credential.User.IdPerson);
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
