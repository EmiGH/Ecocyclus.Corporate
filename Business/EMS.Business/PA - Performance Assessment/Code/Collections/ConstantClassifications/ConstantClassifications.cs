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
    public class ConstantClassifications
    {
        #region Internal properties
        private Credential _Credential;
        private ICollectionItems _Datasource;
        #endregion

        internal ConstantClassifications(Entities.ConfigurationPA configurationPA)
        {
            _Credential = configurationPA.Credential;
            _Datasource = new ConstantClassificationsRead.ConstantClassificationRoot(configurationPA);
        }

        internal ConstantClassifications(Entities.ConstantClassification constantClassification)
        {
            _Credential = constantClassification.Credential;
            _Datasource = new ConstantClassificationsRead.ConstantClassificationByConstantClassification(constantClassification);
        }

        internal ConstantClassifications(Entities.Constant constant)
        {
            _Credential = constant.Credential;
        }

        #region Read Functions
        /// <summary>
        /// Retorna ForumForums
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.ConstantClassification> Items()
        {
            Dictionary<Int64, Entities.ConstantClassification> _items = new Dictionary<Int64, Entities.ConstantClassification>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdConstantClassification", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.ConstantClassification _constantClassification = new Entities.ConstantClassification(Convert.ToInt64(_dbRecord["IdConstantClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentConstantClassification"],0)), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                _items.Add(_constantClassification.IdConstantClassification, _constantClassification);
            }
            return _items;
        }

        internal Dictionary<Int64, Entities.ConstantClassification> Items(Entities.Constant constant)
        {
            Dictionary<Int64, Entities.ConstantClassification> _items = new Dictionary<Int64, Entities.ConstantClassification>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdConstantClassification", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.ConstantClassification _constantClassification = new Entities.ConstantClassification(Convert.ToInt64(_dbRecord["IdConstantClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentConstantClassification"],0)), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                _items.Add(_constantClassification.IdConstantClassification, _constantClassification);
            }
            return _items;
        }


        /// <summary>
        /// Retorna ForumForums por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.ConstantClassification Item(Int64 idConstantClassification)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.ConstantClassification _constantClassification = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ConstantClassifications_ReadById(idConstantClassification, _Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdConstantClassification", _Credential).Filter();

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                return new Entities.ConstantClassification(Convert.ToInt64(_dbRecord["IdConstantClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentConstantClassification"],0)), Convert.ToString(_dbRecord["idLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
            }
            return _constantClassification;
        }
        #endregion

        #region Write Functions
        //Crea ForumForums
        internal Entities.ConstantClassification Create(Entities.ConstantClassification parentConstantClassification, String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                Int64 _idParentConstantClassification = parentConstantClassification == null ? 0 : parentConstantClassification.IdConstantClassification;
                //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                Int64 _idConstantClassification = _dbPerformanceAssessments.ConstantClassifications_Create(_idParentConstantClassification);
                //alta del lg
                _dbPerformanceAssessments.ConstantClassifications_LG_Create(_idConstantClassification, _Credential.DefaultLanguage.IdLanguage, name, description);
                //crea el objeto 
                Entities.ConstantClassification _constantClassification = new Entities.ConstantClassification(_idConstantClassification, _idParentConstantClassification, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential);

                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                _dbLog.Create("PA_ConstantClassifications", "ConstantClassifications", "Add", "IdConstantClassification=" + _idConstantClassification, _Credential.User.IdPerson);

                return _constantClassification;
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

        internal void Delete(Entities.ConstantClassification constantClassification)
        {
            try
            {
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Borra dependencias 
                constantClassification.Remove();

                _dbPerformanceAssessments.ConstantClassifications_Delete(constantClassification.IdConstantClassification);

                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                _dbLog.Create("PA_ConstantClassifications", "ConstantClassifications", "Delete", "IdConstantClassification=" + constantClassification.IdConstantClassification, _Credential.User.IdPerson);

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

        internal void Update(Entities.ConstantClassification constantClassification, Entities.ConstantClassification parent, String name, String description)
        {
            try
            {
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                Int64 _idParentConstantClassification = parent == null ? 0 : parent.IdConstantClassification;

                _dbPerformanceAssessments.ConstantClassifications_Update(constantClassification.IdConstantClassification, _idParentConstantClassification);

                _dbPerformanceAssessments.ConstantClassifications_LG_Update(constantClassification.IdConstantClassification, constantClassification.Credential.DefaultLanguage.IdLanguage, name, description);

                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                _dbLog.Create("PA_ConstantClassifications", "ConstantClassifications", "Update", "IdConstantClassification=" + constantClassification.IdConstantClassification, _Credential.User.IdPerson);
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
