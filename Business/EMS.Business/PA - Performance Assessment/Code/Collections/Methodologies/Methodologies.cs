using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Condesus.EMS.Business;
using Condesus.EMS.DataAccess;
using Condesus.EMS.Business.Security;
using Condesus.EMS.Business.KC.Entities;

namespace Condesus.EMS.Business.PA.Collections
{
    public class Methodologies
    {
        #region Internal properties
        private Credential _Credential;
        private ICollectionItems _Datasource;
        #endregion

        internal Methodologies(Credential credential) 
        {
            _Credential = credential;
            _Datasource = new MethodologiesRead.MethodologyReadAll(credential);
        }

        internal Methodologies(Entities.Measurement measurement)
        {
            _Credential = measurement.Credential;            
        }

        #region Read Functions
        /// <summary>
        /// Retorna ForumForums
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.Methodology> Items()
        {
            Dictionary<Int64, Entities.Methodology> _items = new Dictionary<Int64, Entities.Methodology>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdMethodology", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.Methodology _methodology = new Entities.Methodology(Convert.ToInt64(_dbRecord["IdMethodology"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResource"],0)), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["MethodName"]), Convert.ToString(_dbRecord["MethodType"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                _items.Add(_methodology.IdMethodology, _methodology);
            }
            return _items;
        }


        /// <summary>
        /// Retorna ForumForums por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.Methodology Item(Int64 idMethodology)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.Methodology _methodology = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Methodologies_ReadById(idMethodology, _Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdMethodology", _Credential).Filter();

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                return new Entities.Methodology(Convert.ToInt64(_dbRecord["IdMethodology"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResource"],0)), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["MethodName"]), Convert.ToString(_dbRecord["MethodType"]), Convert.ToString(_dbRecord["Description"]), _Credential);
            }
            return _methodology;
        }
        #endregion

        #region Write Functions
        //Crea ForumForums
        internal Entities.Methodology Create(Resource resource, String methodName, String methodType, String description)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Int64 _idResource = resource == null ? 0 : resource.IdResource;

            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            Int64 _idMethodology = _dbPerformanceAssessments.Methodologies_Create(_idResource);
            //alta del lg
            _dbPerformanceAssessments.Methodologies_LG_Create(_idMethodology, _Credential.DefaultLanguage.IdLanguage, methodName ,methodType, description);
            //crea el objeto 
            Entities.Methodology _methodology = new Entities.Methodology(_idMethodology,_idResource, _Credential.DefaultLanguage.IdLanguage, methodName,methodType, description, _Credential);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_Methodologies", "Methodologies", "Add", "IdMethodology=" + _idMethodology, _Credential.User.IdPerson);

            return _methodology;

        }

        internal void Delete(Entities.Methodology methodology)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Borra dependencias 
            methodology.Remove();

            _dbPerformanceAssessments.Methodologies_Delete(methodology.IdMethodology);


            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_Methodologies", "Methodologies", "Delete", "IdMethodology=" + methodology.IdMethodology, _Credential.User.IdPerson);

        }

        internal void Update(Entities.Methodology methodology, Resource resource, String methodName, String methodType, String description)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Int64 _idResource = resource == null ? 0 : resource.IdResource;

            _dbPerformanceAssessments.Methodologies_Update(methodology.IdMethodology, _idResource);

            _dbPerformanceAssessments.Methodologies_LG_Update(methodology.IdMethodology, methodology.Credential.DefaultLanguage.IdLanguage, methodName ,methodType, description);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_Methodologies", "Methodologies", "Update", "IdMethodology=" + methodology.IdMethodology, _Credential.User.IdPerson);

        }

        #endregion
    }
}
