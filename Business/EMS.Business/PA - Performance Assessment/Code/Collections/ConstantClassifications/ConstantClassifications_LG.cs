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
    public class ConstantClassifications_LG
    {
        #region Internal Properties
        private Entities.ConstantClassification _ConstantClassification;
        #endregion

        internal ConstantClassifications_LG(Entities.ConstantClassification constantClassification) 
        {
            _ConstantClassification = constantClassification;
        }

        #region Read Functions
    
        public Dictionary<String, Entities.ConstantClassification_LG> Items()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Dictionary<String, Entities.ConstantClassification_LG> _oItems = new Dictionary<String, Entities.ConstantClassification_LG>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ConstantClassifications_LG_ReadAll(_ConstantClassification.IdConstantClassification);
            //Se modifica con los datos que realmente se tienen que usar...

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Declara e instancia una posicion
                Entities.ConstantClassification_LG _constant_LG = new Entities.ConstantClassification_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

                //Lo agrego a la coleccion
                _oItems.Add(Convert.ToString(_dbRecord["IdLanguage"]), _constant_LG);

            }
            return _oItems;
        }
        
        public Entities.ConstantClassification_LG Item(String IdLanguage)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Entities.ConstantClassification_LG _Item = null;

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ConstantClassifications_LG_ReadById(_ConstantClassification.IdConstantClassification, _ConstantClassification.Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Declara e instancia una posicion
                _Item = new Entities.ConstantClassification_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

            }
            return _Item;

        }

        #endregion
        #region Write Functions
        
        public Entities.ConstantClassification_LG Create(DS.Entities.Language language, String Name, String Description)
        {
              try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.ConstantClassifications_LG_Create(_ConstantClassification.IdConstantClassification, language.IdLanguage, Name, Description);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_ConstantClassifications_LG", "ConstantClassifications_LG", "Add", "IdConstantClassification=" + _ConstantClassification.IdConstantClassification + " and IdLanguage= '" + language.IdLanguage + "'", _ConstantClassification.Credential.User.IdPerson);

                    return new Entities.ConstantClassification_LG(language.IdLanguage, Name, Description);
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
                if (_ConstantClassification.Credential.DefaultLanguage.IdLanguage == language.IdLanguage)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
                }
                try
                {
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    _dbPerformanceAssessments.ConstantClassifications_LG_Delete(_ConstantClassification.IdConstantClassification, language.IdLanguage);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_ConstantClassifications_LG", "ConstantClassifications_LG", "Delete", "IdConstantClassification=" + _ConstantClassification.IdConstantClassification + " and IdLanguage= '" + language.IdLanguage + "'", _ConstantClassification.Credential.User.IdPerson);
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

                _dbPerformanceAssessments.ConstantClassifications_LG_Delete(_ConstantClassification.IdConstantClassification);

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

                    _dbPerformanceAssessments.ConstantClassifications_LG_Update(_ConstantClassification.IdConstantClassification, language.IdLanguage, name, description);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PA_ConstantClassifications_LG", "ConstantClassifications_LG", "Update", "IdConstantClassification=" + _ConstantClassification.IdConstantClassification + " and IdLanguage= '" + language.IdLanguage + "'", _ConstantClassification.Credential.User.IdPerson);
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
