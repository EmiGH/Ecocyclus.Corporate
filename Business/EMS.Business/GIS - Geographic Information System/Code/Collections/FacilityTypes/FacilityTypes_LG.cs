using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Condesus.EMS.Business;
using Condesus.EMS.DataAccess;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.GIS.Collections
{
    public class FacilityTypes_LG
    {
        #region Internal Properties
        private Entities.FacilityType _FacilityType;
        #endregion

        internal FacilityTypes_LG(Entities.FacilityType facilityType)
        {
            _FacilityType = facilityType;
        }

        #region Read Functions
        /// <summary>
        /// Retorna ForumCategories_LG
        /// </summary>
        /// <returns></returns>
        public Dictionary<String, Entities.FacilityType_LG> Items()
        {
            DataAccess.GIS.GeographicInformationSystem _dbgeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            //Coleccion para devolver las areas funcionales
            Dictionary<String, Entities.FacilityType_LG> _oItems = new Dictionary<String, Entities.FacilityType_LG>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbgeographicInformationSystem.FacilityTypes_LG_ReadAll(_FacilityType.IdFacilityType);
            //Se modifica con los datos que realmente se tienen que usar...

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Declara e instancia una posicion
                Entities.FacilityType_LG _facilityType_LG = new Entities.FacilityType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

                //Lo agrego a la coleccion
                _oItems.Add(Convert.ToString(_dbRecord["IdLanguage"]), _facilityType_LG);

            }
            return _oItems;
        }
        /// <summary>
        /// Retorna ForumCategories_LG por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        public Entities.FacilityType_LG Item(String IdLanguage)
        {
            DataAccess.GIS.GeographicInformationSystem _dbgeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            //Coleccion para devolver las areas funcionales
            Entities.FacilityType_LG _Item = null;

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbgeographicInformationSystem.FacilityTypes_LG_ReadById(_FacilityType.IdFacilityType, _FacilityType.Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //Declara e instancia una posicion
                _Item = new Entities.FacilityType_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));

            }
            return _Item;

        }

        #endregion
        #region Write Functions
        //Crea ForumCategories_LG
        public Entities.FacilityType_LG Create(DS.Entities.Language language, String Name, String Description)
        {
            try
            {
                DataAccess.GIS.GeographicInformationSystem _dbgeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

                _dbgeographicInformationSystem.FacilityTypes_LG_Create(_FacilityType.IdFacilityType, language.IdLanguage, Name, Description);

                //Log
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                _dbLog.Create("GIS_FacilityTypes_LG", "FacilityTypes_LG", "Add", "IdFacilityType=" + _FacilityType.IdFacilityType + " and IdLanguage= '" + language.IdLanguage + "'", _FacilityType.Credential.User.IdPerson);

                return new Entities.FacilityType_LG(language.IdLanguage, Name, Description);
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
            if (_FacilityType.Credential.DefaultLanguage.IdLanguage == language.IdLanguage)
            {
                throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
            }
            try
            {
                DataAccess.GIS.GeographicInformationSystem _dbgeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

                _dbgeographicInformationSystem.FacilityTypes_LG_Delete(_FacilityType.IdFacilityType, language.IdLanguage);

                //Log
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                _dbLog.Create("GIS_FacilityTypes_LG", "FacilityTypes_LG", "Delete", "IdFacilityType=" + _FacilityType.IdFacilityType + " and IdLanguage= '" + language.IdLanguage + "'", _FacilityType.Credential.User.IdPerson);
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
                DataAccess.GIS.GeographicInformationSystem _dbgeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

                _dbgeographicInformationSystem.FacilityTypes_LG_Delete(_FacilityType.IdFacilityType);

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
                DataAccess.GIS.GeographicInformationSystem _dbgeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

                _dbgeographicInformationSystem.FacilityTypes_LG_Update(_FacilityType.IdFacilityType, language.IdLanguage, Name, Description);

                //Log
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                _dbLog.Create("GIS_FacilityTypes_LG", "FacilityTypes_LG", "Update", "IdFacilityType=" + _FacilityType.IdFacilityType + " and IdLanguage= '" + language.IdLanguage + "'", _FacilityType.Credential.User.IdPerson);
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
