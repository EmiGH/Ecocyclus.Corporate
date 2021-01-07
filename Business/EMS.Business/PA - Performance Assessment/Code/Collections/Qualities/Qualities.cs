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
    public class Qualities
    {
        #region Internal properties
        private Credential _Credential;
        private ICollectionItems _Datasource;
        #endregion

        internal Qualities(Credential credential) 
        {
            _Credential = credential;
            _Datasource = new QualitiesRead.QualitiesReadAll(credential);
        }
        internal Qualities(Entities.Measurement measurement)
        {
            _Credential = measurement.Credential;
        }

        #region Read Functions
        /// <summary>
        /// Retorna ForumForums
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.Quality> Items()
        {
            Dictionary<Int64, Entities.Quality> _items = new Dictionary<Int64, Entities.Quality>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdQuality", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.Quality _quality = new Entities.Quality(Convert.ToInt64(_dbRecord["IdQuality"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                _items.Add(_quality.IdQuality, _quality);
            }
            return _items;
        }


        /// <summary>
        /// Retorna ForumForums por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.Quality Item(Int64 idQuality)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.Quality _quality = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Qualities_ReadById(idQuality, _Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdQuality", _Credential).Filter();

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                return new Entities.Quality(Convert.ToInt64(_dbRecord["IdQuality"]), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
            }
            return _quality;
        }
        #endregion

        #region Write Functions
        //Crea ForumForums
        internal Entities.Quality Create(String name, String description)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            Int64 _idQuality = _dbPerformanceAssessments.Qualities_Create();
            //alta del lg
            _dbPerformanceAssessments.Qualities_LG_Create(_idQuality, _Credential.DefaultLanguage.IdLanguage, name, description);
            //crea el objeto 
            Entities.Quality _quality = new Entities.Quality(_idQuality, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_Qualities", "Qualities", "Add", "IdQuality=" + _idQuality, _Credential.User.IdPerson);

            return _quality;

        }

        internal void Delete(Entities.Quality quality)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Borra dependencias 
            quality.Remove();

            _dbPerformanceAssessments.Qualities_Delete(quality.IdQuality);


            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_Qualities", "Qualities", "Delete", "IdQuality=" + quality.IdQuality, _Credential.User.IdPerson);

        }


        internal void Update(Entities.Quality quality, String name, String description)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            _dbPerformanceAssessments.Qualities_LG_Update(quality.IdQuality, quality.Credential.DefaultLanguage.IdLanguage, name, description);

            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("PA_Qualities", "Qualities", "Update", "IdQuality=" + quality.IdQuality, _Credential.User.IdPerson);

        }

        #endregion
    }
}
