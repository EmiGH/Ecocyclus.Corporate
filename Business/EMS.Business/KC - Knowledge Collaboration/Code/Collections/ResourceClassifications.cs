using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Collections
{
    internal class ResourceClassifications
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdParent; //Id del padre; si se carga <> de cero traer los items que son hijos
            private Entities.ResourceClassification _Parent;
        #endregion

        internal ResourceClassifications(Credential credential)
        {
            _Credential = credential;
            _Parent = null;
            _IdParent = 0;
        }
        internal ResourceClassifications(Entities.ResourceClassification parent, Credential credential)
        {
            _Credential = credential;
            _Parent = parent;
            _IdParent = parent.IdResourceClassification;
        }

        #region Read Functions
        internal Entities.ResourceClassification Item(Int64 idResourceClassification)
        {
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceClassifications_ReadById(idResourceClassification, _Credential.CurrentLanguage.IdLanguage);
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdResourceClassification", _Credential).Filter();
            
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                if ((_IdParent == 0) || (Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourceClassification"], 0)) == _IdParent))
                {                    
                    return new Entities.ResourceClassification(Convert.ToInt64(_dbRecord["IdResourceClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentResourceClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);                    
                }
            }
            return null;
        }
        //internal Dictionary<Int64, Entities.ResourceClassification> Roots()
        //{
        //    DataAccess.KC.ResourceClassifications _dbResourceClassifications = new Condesus.EMS.DataAccess.KC.ResourceClassifications();

        //    Dictionary<Int64, Entities.ResourceClassification> _oItems = new Dictionary<Int64, Entities.ResourceClassification>();

        //    //Traigo los datos de la base
        //    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbResourceClassifications.GetAllRoots(_Credential.CurrentLanguage.IdLanguage, Common.Security.ResourceClassification, _Credential.User.IdPerson); 
            
        //    Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdResourceClassification", _Credential).Filter();

        //    foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
        //    {
        //        Entities.ResourceClassification _ResourceClassification = new Entities.ResourceClassification(Convert.ToInt64(_dbRecord["IdResourceClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentResourceClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

        //        //Lo agrego a la coleccion
        //        _oItems.Add(_ResourceClassification.IdResourceClassification, _ResourceClassification);
        //    }
        //    return _oItems;
        //}

        internal Dictionary<Int64, Entities.ResourceClassification> Items()
        {
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            Dictionary<Int64, Entities.ResourceClassification> _oItems = new Dictionary<Int64, Entities.ResourceClassification>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record;
            if (_IdParent == 0)
            { _record = _dbKnowledgeCollaboration.ResourceClassifications_ReadRoot(_Credential.CurrentLanguage.IdLanguage); }
            else
            { _record = _dbKnowledgeCollaboration.ResourceClassifications_ReadByParent(_IdParent, _Credential.CurrentLanguage.IdLanguage); }

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdResourceClassification", _Credential).Filter();
                        
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {                    
                    Entities.ResourceClassification _ResourceClassification = new Entities.ResourceClassification(Convert.ToInt64(_dbRecord["IdResourceClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentResourceClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_ResourceClassification.IdResourceClassification, _ResourceClassification);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.ResourceClassification> Items(Entities.Resource resource)
        {
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            Dictionary<Int64, Entities.ResourceClassification> _oItems = new Dictionary<Int64, Entities.ResourceClassification>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceClassifications_ReadByResource(resource.IdResource, _Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdResourceClassification", _Credential).Filter();

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                    Entities.ResourceClassification _ResourceClassification = new Entities.ResourceClassification(Convert.ToInt64(_dbRecord["IdResourceClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentResourceClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_ResourceClassification.IdResourceClassification, _ResourceClassification);
            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.ResourceClassification> ItemsSecurity()
        {
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            Dictionary<Int64, Entities.ResourceClassification> _oItems = new Dictionary<Int64, Entities.ResourceClassification>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceClassifications_ReadAll(_Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdResourceClassification", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.ResourceClassification _ResourceClassification = new Entities.ResourceClassification(Convert.ToInt64(_dbRecord["IdResourceClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentResourceClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                //Lo agrego a la coleccion
                _oItems.Add(_ResourceClassification.IdResourceClassification, _ResourceClassification);
            }
            return _oItems;
        }
        #endregion
     

        #region Write Functions
        internal Entities.ResourceClassification Add(String name, String description)
        {
            try
            {              
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idResourceClassification = _dbKnowledgeCollaboration.ResourceClassifications_Create(_IdParent);
                    //Alta del LG
                    _dbKnowledgeCollaboration.ResourceClassifications_LG_Create(_idResourceClassification, _Credential.DefaultLanguage.IdLanguage, name, description);
                    //Log
                    _dbLog.Create("KC_ResourceClassifications", "ResourceClassifications", "Add", "IdResourceClassification=" + _idResourceClassification, _Credential.User.IdPerson);

                    Entities.ResourceClassification _resourceClassification = new Entities.ResourceClassification(_idResourceClassification, _IdParent, name, description, _Credential);
                
                    return _resourceClassification;
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
        internal void Remove(Entities.ResourceClassification resourceClassification)
        {           
            try
            {
                    //Borrado en cascada
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                    //Borra las hijas
                    resourceClassification.Remove();
                    //Borra la relacion con los elementos
                    _dbKnowledgeCollaboration.ResourceClassifications_DeleteByClassification(resourceClassification.IdResourceClassification);
                    //Borra los LG
                    _dbKnowledgeCollaboration.ResourceClassifications_LG_Delete(resourceClassification.IdResourceClassification);
                    //Borra el item
                    _dbKnowledgeCollaboration.ResourceClassifications_Delete(resourceClassification.IdResourceClassification);
                    //Log
                    _dbLog.Create("KC_ResourceClassifications", "ResourceClassifications", "Remove", "IdResourceClassification=" + resourceClassification.IdResourceClassification, _Credential.User.IdPerson);

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
        internal void Modify(Entities.ResourceClassification resourceClassification, String name, String description)
        {
            try
            {
  
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                    _dbKnowledgeCollaboration.ResourceClassifications_LG_Update(resourceClassification.IdResourceClassification, _Credential.DefaultLanguage.IdLanguage, name, description);

                    _dbLog.Create("KC_ResourceClassifications", "ResourceClassifications", "Modify", "IdResourceClassification=" + resourceClassification.IdResourceClassification, _Credential.User.IdPerson);

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
        internal void Modify(Entities.ResourceClassification resourceClassification)
        {
            try
            {

                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                if (_Parent == null)
                {
                    _dbKnowledgeCollaboration.ResourceClassifications_Update(resourceClassification.IdResourceClassification, 0);
                }
                else
                {
                    _dbKnowledgeCollaboration.ResourceClassifications_Update(resourceClassification.IdResourceClassification, _Parent.IdResourceClassification);
                }

                _dbLog.Create("KC_ResourceClassifications", "ResourceClassifications", "Modify", "IdResourceClassification=" + resourceClassification.IdResourceClassification, _Credential.User.IdPerson);

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
