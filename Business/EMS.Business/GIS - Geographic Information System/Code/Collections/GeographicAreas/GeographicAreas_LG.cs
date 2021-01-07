using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.GIS.Collections
{
    public class GeographicAreas_LG
    {
        #region Internal Properties
        private Credential _Credential;
        private Entities.GeographicArea _GeographicArea; //El identificador del Area geografica 
        #endregion

        internal GeographicAreas_LG(Entities.GeographicArea GeographicArea, Credential credential)
        {
            _Credential = credential;
            _GeographicArea = GeographicArea;
        }

        #region Read Functions
        public Entities.GeographicArea_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            Entities.GeographicArea_LG _item = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbGeographicInformationSystem.GeographicAreas_LG_ReadById(_GeographicArea.IdGeographicArea, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _item = new Entities.GeographicArea_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
            }
            return _item;
        }
        public Dictionary<String, Entities.GeographicArea_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            Dictionary<String, Entities.GeographicArea_LG> _oGeographicAreas_LG = new Dictionary<String, Entities.GeographicArea_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbGeographicInformationSystem.GeographicAreas_LG_ReadAll(_GeographicArea.IdGeographicArea);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.GeographicArea_LG _oGeographicArea_LG = new Entities.GeographicArea_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                _oGeographicAreas_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oGeographicArea_LG);
            }

            return _oGeographicAreas_LG;
        }
        #endregion

        #region Write Functions
        public Entities.GeographicArea_LG Add(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();


                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbGeographicInformationSystem.GeographicAreas_LG_Create(_GeographicArea.IdGeographicArea, idLanguage, name, description);
                return new Entities.GeographicArea_LG(idLanguage, name, description);
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
        public Entities.GeographicArea_LG Modify(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();


                _dbGeographicInformationSystem.GeographicAreas_LG_Update(_GeographicArea.IdGeographicArea, idLanguage, name, description);
                return new Entities.GeographicArea_LG(idLanguage, name, description);
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
                //Acceso a datos para la opción de idioma
                DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();


                _dbGeographicInformationSystem.GeographicAreas_LG_Delete(_GeographicArea.IdGeographicArea, idLanguage);
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
        /// <summary>
        /// Borra todas las opciones de lenguage para un AG
        /// </summary>
        public void Remove()
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

                _dbGeographicInformationSystem.GeographicAreas_LG_DeleteByGeographicArea(_GeographicArea.IdGeographicArea);
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
