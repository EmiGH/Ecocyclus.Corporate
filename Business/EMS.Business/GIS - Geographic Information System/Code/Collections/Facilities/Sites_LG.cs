using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.GIS.Collections
{
    public class Sites_LG
    {
        #region Internal Properties
        private Credential _Credential;
        private Entities.Site _Facility; //El identificador del Area geografica 
        #endregion

        internal Sites_LG(Entities.Site Facility, Credential credential)
        {
            _Credential = credential;
            _Facility = Facility;
        }

        #region Read Functions
        public Entities.Site_LG Item(String idLanguage)
        {
            //Acceso a datos para la opción de idioma
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            Entities.Site_LG _item = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbGeographicInformationSystem.Sites_LG_ReadById(_Facility.IdFacility, idLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _item = new Entities.Site_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
            }
            return _item;
        }
        public Dictionary<String, Entities.Site_LG> Items()
        {
            //Acceso a datos para la opción de idioma
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            Dictionary<String, Entities.Site_LG> _oFacilitys_LG = new Dictionary<String, Entities.Site_LG>();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbGeographicInformationSystem.Sites_LG_ReadAll(_Facility.IdFacility);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Site_LG _oSite_LG = new Entities.Site_LG(Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]));
                _oFacilitys_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oSite_LG);
            }

            return _oFacilitys_LG;
        }
        #endregion

        #region Write Functions
        public Entities.Site_LG Add(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();


                //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                _dbGeographicInformationSystem.Sites_LG_Create(_Facility.IdFacility, idLanguage, name, description);
                return new Entities.Site_LG(idLanguage, name, description);
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
        public Entities.Site_LG Modify(String idLanguage, String name, String description)
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();


                _dbGeographicInformationSystem.Sites_LG_Update(_Facility.IdFacility, idLanguage, name, description);
                return new Entities.Site_LG(idLanguage, name, description);
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


                _dbGeographicInformationSystem.Sites_LG_Delete(_Facility.IdFacility, idLanguage);
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
        /// Borra todas las opciones de LG para un facility
        /// </summary>
        public void Remove()
        {
            try
            {
                //Acceso a datos para la opción de idioma
                DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

                _dbGeographicInformationSystem.Sites_LG_DeleteByFacility(_Facility.IdFacility);
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
