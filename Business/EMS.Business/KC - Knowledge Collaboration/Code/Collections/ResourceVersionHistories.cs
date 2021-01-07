using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.KC.Collections
{
    public class ResourceVersionHistories
    {
      #region Internal Properties
            private Credential _Credential;            
            private Int64 _IdResourceFile;
            private Int64 _IdResource;
            private Entities.CatalogFile _CatalogFile;
        #endregion

            internal ResourceVersionHistories(Entities.Catalog catalog, Credential credential)
        {
            _IdResource = catalog.IdResource;
            _IdResourceFile = catalog.IdResourceFile;
            _Credential = credential;
            _CatalogFile = catalog;
        }

            internal ResourceVersionHistories(Entities.Version version, Credential credential)
        {
            _IdResource = version.ResourceVersion.IdResource;
            _IdResourceFile = version.IdResourceFile;
            _Credential = credential;
            _CatalogFile = version;
        }
        //para que lo construya post
            internal ResourceVersionHistories(Credential credential)
        {
            _IdResource = 0;
            _IdResourceFile = 0;
            _Credential = credential;
            _CatalogFile = null;
        }

        #region Read Functions
            //Trae el ultimo filtrado por fecha
            internal Entities.ResourceVersionHistory Item()
        {
            //Objeto de data layer para acceder a datos
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            Entities.ResourceVersionHistory _ResourceVersionHistory = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceVersionHistories_ReadById(_IdResourceFile, _IdResource, _Credential.CurrentLanguage.IdLanguage);
            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.ResourceHistoryState _ResourceHistoryState = new Condesus.EMS.Business.KC.Collections.ResourceHistoryStates(_Credential).Item(Convert.ToInt64(_dbRecord["IdResourceFileState"]));
                
                DS.Entities.Post _post = new DS.Collections.Posts(_Credential).Item(Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(_dbRecord["IdFunctionalArea"]), Convert.ToInt64(_dbRecord["IdPosition"]));
                _ResourceVersionHistory = new Entities.ResourceVersionHistory(_CatalogFile, _ResourceHistoryState, Convert.ToDateTime(_dbRecord["Date"]), _post, _Credential);
            }
            return _ResourceVersionHistory;
            
        }
            //Trae toda la historia para el idresourcefile
            internal Dictionary<Int64, Entities.ResourceVersionHistory> Items()
        {
            ////Objeto de data layer para acceder a datos
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.ResourceVersionHistory> _oItems = new Dictionary<Int64, Entities.ResourceVersionHistory>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceVersionHistories_ReadAll(_IdResource, _Credential.CurrentLanguage.IdLanguage);           

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.ResourceHistoryState _ResourceHistoryState = new Condesus.EMS.Business.KC.Collections.ResourceHistoryStates(_Credential).Item(Convert.ToInt64(_dbRecord["IdResourceFileState"]));
                    //Declara e instancia una posicion

                DS.Entities.Post _post = new DS.Collections.Posts(_Credential).Item(Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(_dbRecord["IdFunctionalArea"]), Convert.ToInt64(_dbRecord["IdPosition"]));
                Entities.ResourceVersionHistory _ResourceVersionHistory = new Entities.ResourceVersionHistory(_CatalogFile, _ResourceHistoryState, Convert.ToDateTime(_dbRecord["Date"]), _post, _Credential);

                    //Lo agrego a la coleccion
                _oItems.Add(_ResourceVersionHistory.ResourceHistoryState.IdResourceFileState, _ResourceVersionHistory);
            }
            return _oItems;
             
            }

        #endregion

        #region Write Functions
            internal Entities.ResourceVersionHistory Add(Entities.ResourceHistoryState ResourceHistoryState, DateTime date, DS.Entities.Post post)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbKnowledgeCollaboration.ResourceVersionHistories_Create(_IdResourceFile, ResourceHistoryState.IdResourceFileState, _IdResource, date, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, post.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition);

                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("KC_ResourceVersionHistories", "ResourceVersionHistories", "Add", "IdResource=" + _IdResource + " and IdresourceFile=" + _IdResourceFile + " and IdresourceFileState=" + ResourceHistoryState.IdResourceFileState, _Credential.User.IdPerson);

                    //Devuelvo el objeto FunctionalArea creado

                    return new Entities.ResourceVersionHistory(_CatalogFile, ResourceHistoryState, date, post, _Credential);
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
            internal void Remove(Entities.ResourceHistoryState ResourceHistoryState)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //Borrar de la base de datos
                    _dbKnowledgeCollaboration.ResourceVersionHistories_Delete(_IdResourceFile, _IdResource, ResourceHistoryState.IdResourceFileState);

                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("KC_ResourceVersionHistories", "ResourceVersionHistories", "Delete", "IdResource=" + _IdResource + " and IdresourceFile=" + _IdResourceFile + " and IdresourceFileState=" + ResourceHistoryState.IdResourceFileState, _Credential.User.IdPerson);

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
            internal void Remove()
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //Borrar de la base de datos
                    _dbKnowledgeCollaboration.ResourceVersionHistories_Delete(_IdResource);

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
            internal void Remove(DS.Entities.Post post)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //Borrar de la base de datos
                    _dbKnowledgeCollaboration.ResourceVersionHistories_Delete(post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea,post.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition);

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
            public void Modify(Entities.ResourceHistoryState ResourceHistoryState, DateTime date, DS.Entities.Post post)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbKnowledgeCollaboration.ResourceVersionHistories_Update(_IdResourceFile, ResourceHistoryState.IdResourceFileState, _IdResource, date, post.JobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, post.JobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, post.Organization.IdOrganization, post.Person.IdPerson, post.JobTitle.FunctionalPositions.Position.IdPosition);

                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("KC_ResourceVersionHistories", "ResourceVersionHistories", "Modify", "IdResource=" + _IdResource + " and IdresourceFile=" + _IdResourceFile + " and IdresourceFileState=" + ResourceHistoryState.IdResourceFileState, _Credential.User.IdPerson);

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
