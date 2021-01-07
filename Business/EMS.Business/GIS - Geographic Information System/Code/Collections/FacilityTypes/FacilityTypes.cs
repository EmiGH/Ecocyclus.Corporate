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
    public class FacilityTypes
    {
        #region Internal properties
        private Credential _Credential;
        private ICollectionItems _Datasource;
        #endregion

        internal FacilityTypes(Credential credential) 
        {
            _Credential = credential;
            _Datasource = new FacilityTypesRead.FacilityTypesAll(credential);
        }
        internal FacilityTypes(PF.Entities.ProcessGroupProcess process)
        {
            _Credential = process.Credential;
            _Datasource = new FacilityTypesRead.FacilityTypesByProcessWhitMeasurements(process);
        }

        #region Read Functions
        /// <summary>
        /// Retorna ForumForums
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.FacilityType> Items()
        {
            Dictionary<Int64, Entities.FacilityType> _items = new Dictionary<Int64, Entities.FacilityType>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdFacilityType", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.FacilityType _facilityType = new Entities.FacilityType(Convert.ToInt64(_dbRecord["IdFacilityType"]), Convert.ToString(_dbRecord["IconName"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                _items.Add(_facilityType.IdFacilityType, _facilityType);
            }
            return _items;
        }


        /// <summary>
        /// Retorna ForumForums por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.FacilityType Item(Int64 idFacilityType)
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            Entities.FacilityType _facilityType = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbGeographicInformationSystem.FacilityTypes_ReadById(idFacilityType, _Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdFacilityType", _Credential).Filter();

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                return new Entities.FacilityType(Convert.ToInt64(_dbRecord["IdFacilityType"]), Convert.ToString(_dbRecord["IconName"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
            }
            return _facilityType;
        }
        #endregion

        #region Write Functions
        //Crea ForumForums
        internal Entities.FacilityType Create(String IconName, String name, String description)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            Int64 _idFacilityType = _dbGeographicInformationSystem.FacilityTypes_Create(IconName);
            //alta del lg
            _dbGeographicInformationSystem.FacilityTypes_LG_Create(_idFacilityType, _Credential.DefaultLanguage.IdLanguage, name, description);
            //crea el objeto 
            Entities.FacilityType _facilityType = new Entities.FacilityType(_idFacilityType, IconName, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("GIS_FacilityTypes", "FacilityTypes", "Add", "IdFacilityType=" + _idFacilityType, _Credential.User.IdPerson);

            return _facilityType;

        }

        internal void Delete(Entities.FacilityType facilityType)
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            //Borra dependencias 
            facilityType.Remove();

            _dbGeographicInformationSystem.FacilityTypes_Delete(facilityType.IdFacilityType);


            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("GIS_FacilityTypes", "FacilityType", "Delete", "IdFacilityType=" + facilityType.IdFacilityType, _Credential.User.IdPerson);

        }


        internal void Update(Entities.FacilityType facilityType, String IconName, String name, String description)
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            _dbGeographicInformationSystem.FacilityTypes_Update(facilityType.IdFacilityType, IconName);

            _dbGeographicInformationSystem.FacilityTypes_LG_Update(facilityType.IdFacilityType, facilityType.Credential.DefaultLanguage.IdLanguage, name, description);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("GIS_FacilityTypes", "FacilityTypes", "Update", "IdFacilityType=" + facilityType.IdFacilityType, _Credential.User.IdPerson);

        }

        #endregion
    }
}
