using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Collections
{
    public class Resources
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal Resources( Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
        internal Entities.Resource Item(Int64 idResource)
        {
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            Entities.Resource _resource = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.Resources_ReadById(idResource, _Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdResource", _Credential).Filter();

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.ResourceType _ResourceType = new Collections.ResourceTypes(_Credential).Item(Convert.ToInt64(_dbRecord["IdResourceType"]));
                return Factories.ResourceFactory.CreateResource(Convert.ToInt64(_dbRecord["IdResource"]), _ResourceType, Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["CurrentFile"], 0)), Convert.ToString(_dbRecord["Type"]), _Credential);
            }
            return _resource;          
        }
        internal Dictionary<Int64, Entities.Resource> Items()
        {
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.Resource> _oItems = new Dictionary<Int64, Entities.Resource>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.Resources_ReadAll(_Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdResource", _Credential).Filter();

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.ResourceType _ResourceType = new Collections.ResourceTypes(_Credential).Item(Convert.ToInt64(_dbRecord["IdResourceType"]));
                //Declara e instancia una posicion
                Entities.Resource _resource = Factories.ResourceFactory.CreateResource(Convert.ToInt64(_dbRecord["IdResource"]), _ResourceType, Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["CurrentFile"], 0)), Convert.ToString(_dbRecord["Type"]), _Credential);

                //Lo agrego a la coleccion
                _oItems.Add(_resource.IdResource, _resource);

            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.Resource> ReadRoot()
        {
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            Dictionary<Int64, Entities.Resource> _oItems = new Dictionary<Int64, Entities.Resource>();
            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.Resources_ReadRoot(_Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdResource", _Credential).Filter();

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.ResourceType _ResourceType = new Collections.ResourceTypes(_Credential).Item(Convert.ToInt64(_dbRecord["IdResourceType"]));
                //Declara e instancia una posicion
                Entities.Resource _resource = Factories.ResourceFactory.CreateResource(Convert.ToInt64(_dbRecord["IdResource"]), _ResourceType, Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["CurrentFile"], 0)), Convert.ToString(_dbRecord["Type"]), _Credential);

                //Lo agrego a la coleccion
                _oItems.Add(_resource.IdResource, _resource);

            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.Resource> Items(Entities.ResourceClassification resourceClassification)
        {
            ////Objeto de data layer para acceder a datos
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.Resource> _oItems = new Dictionary<Int64, Entities.Resource>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.Resources_ReadByClassification(resourceClassification.IdResourceClassification,  _Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdResource", _Credential).Filter();
       
            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.ResourceType _ResourceType = new Collections.ResourceTypes(_Credential).Item(Convert.ToInt64(_dbRecord["IdResourceType"]));
                //Declara e instancia una posicion
                Entities.Resource _resource = Factories.ResourceFactory.CreateResource(Convert.ToInt64(_dbRecord["IdResource"]), _ResourceType, Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["CurrentFile"], 0)), Convert.ToString(_dbRecord["Type"]), _Credential);

                //Lo agrego a la coleccion
                _oItems.Add(_resource.IdResource, _resource);

            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.Resource> Items(Entities.ResourceType resourceType)
        {            
            ////Objeto de data layer para acceder a datos
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.Resource> _oItems = new Dictionary<Int64, Entities.Resource>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.Resources_ReadByType(resourceType.IdResourceType, _Credential.CurrentLanguage.IdLanguage);

            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdResource", _Credential).Filter();

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.ResourceType _ResourceType = new Collections.ResourceTypes(_Credential).Item(Convert.ToInt64(_dbRecord["IdResourceType"]));
                //Declara e instancia una posicion
                Entities.Resource _resource = Factories.ResourceFactory.CreateResource(Convert.ToInt64(_dbRecord["IdResource"]), _ResourceType, Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["CurrentFile"], 0)), Convert.ToString(_dbRecord["Type"]), _Credential);

                //Lo agrego a la coleccion
                _oItems.Add(_resource.IdResource, _resource);

            }
            return _oItems;

            ////Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            //Boolean _bInsert = true;

            ////busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            //foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            //{
            //    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdResource"])))
            //    {
            //        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
            //        if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
            //        {
            //            _oItems.Remove(Convert.ToInt64(_dbRecord["IdResource"]));
            //        }
            //        else
            //        {
            //            //No debe insertar en la coleccion ya que existe el idioma correcto.
            //            _bInsert = false;
            //        }
            //    }
            //    //Solo inserta si es necesario.
            //    if (_bInsert)
            //    {
            //        //Declara e instancia una posicion
            //        Entities.Resource _resource = Factories.ResourceFactory.CreateResource(Convert.ToInt64(_dbRecord["IdResource"]), Convert.ToInt64(_dbRecord["IdResourceType"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["CurrentFile"], 0)), Convert.ToString(_dbRecord["Type"]), _Credential);

            //        //Lo agrego a la coleccion
            //        _oItems.Add(_resource.IdResource, _resource);
            //    }
            //    _bInsert = true;
            //}
            //return _oItems;
        }

        #endregion

        #region Write Functions
        internal Entities.Resource Add(Entities.ResourceType resourceType, String title, String description, Int64 currentFile, String type, Dictionary<Int64, Entities.ResourceClassification> resourceClassifications)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                Int64 _idResource = _dbKnowledgeCollaboration.Resources_Create(resourceType.IdResourceType, currentFile, type);
                //crea el objeto 
                Entities.Resource _resource = Factories.ResourceFactory.CreateResource(_idResource, resourceType, title, description, currentFile, type, _Credential);
                //alta del lg
                _dbKnowledgeCollaboration.Resources_LG_Create(_idResource,  _Credential.DefaultLanguage.IdLanguage, title, description);

                _dbLog.Create("KC_Resources", "Resources", "Add", "IdResource=" + _idResource, _Credential.User.IdPerson);

                foreach (Entities.ResourceClassification _resourceClassification in resourceClassifications.Values)
                {
                    _dbKnowledgeCollaboration.Resources_Create( _idResource, _resourceClassification.IdResourceClassification);
                }

                return _resource;
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

        internal void Remove(Entities.Resource resource)
        {
            try
            {
                //Valida que no este relacionado con algun process
                if (resource.ProcessesAssociated.Count != 0) { throw new DuplicateNameException(Condesus.EMS.Business.Common.Resources.Errors.RemoveDependency); }

                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                //Borra todos sus archivos asiciados, catalogos o version files, con los files de file attach y sus historias
                resource.RemoveAllFiles();

                //LG
                _dbKnowledgeCollaboration.Resources_LG_Delete(resource.IdResource);
                //borra la relacion con classifications
                _dbKnowledgeCollaboration.Resources_DeleteByResource(resource.IdResource);
                //borra el registro
                _dbKnowledgeCollaboration.Resources_Delete(resource.IdResource);
                //log
                _dbLog.Create("KC_Resources", "Resources", "Remove", "IdResource=" + resource.IdResource, _Credential.User.IdPerson);

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

        internal void Modify(Entities.Resource resource, Entities.ResourceType resourceType, String title, String description, Dictionary<Int64, Entities.ResourceClassification> resourceClassifications)
        {
            try
            {
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                //Modifico los datos de la base
                _dbKnowledgeCollaboration.Resources_Update(resource.IdResource, resourceType.IdResourceType);
                _dbKnowledgeCollaboration.Resources_LG_Update(resource.IdResource, _Credential.DefaultLanguage.IdLanguage, title, description);

                //Borra todas las relaciones con classifications que tengo permiso 
                foreach (Entities.ResourceClassification _classification in resource.Classifications.Values)
                {
                    _dbKnowledgeCollaboration.ResourceClassifications_Delete(resource.IdResource, _classification.IdResourceClassification);
                } 
                //da de alta las nuevas relaciones con classifications
                foreach (Entities.ResourceClassification item in resourceClassifications.Values)
                {
                    _dbKnowledgeCollaboration.Resources_Create(resource.IdResource, item.IdResourceClassification);
                }
                _dbLog.Create("KC_Resources", "Resources", "Modify", "IdResource=" + resource.IdResource, _Credential.User.IdPerson);
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

        public void Modify(Entities.ResourceVersion resourceFile, Entities.Version currentFile)
        {
            try
            {
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                //Modifico los datos de la base
                _dbKnowledgeCollaboration.Resources_UpdateCurrentFile(resourceFile.IdResource, currentFile.IdResourceFile);
                _dbLog.Create("KC_Resources", "Resources", "Modify", "IdResource=" + resourceFile.IdResource, _Credential.User.IdPerson);
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
