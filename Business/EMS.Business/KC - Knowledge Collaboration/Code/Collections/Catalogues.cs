using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Collections
{
    public class Catalogues
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.ResourceCatalog _ResourceCatalog;
        #endregion


        internal Catalogues(Credential credential)
        {
            _Credential = credential;
        }

            internal Catalogues(Entities.ResourceCatalog resourceCatalog, Credential credential)
        {
            _Credential = credential;
            _ResourceCatalog = resourceCatalog;
        }


        #region Read Functions
            //Trae el file pedido con la version vigente
            internal Entities.Catalog Item(Int64 idResourceFile)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
            
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.Catalogues_ReadById(idResourceFile, _ResourceCatalog.IdResource);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                return Factories.CatalogFactory.CreateCatalog(Convert.ToInt64(_dbRecord["IdResourceFile"]), _ResourceCatalog, Convert.ToDateTime(_dbRecord["TimeStamp"]), Convert.ToInt64(_dbRecord["IdPerson"]), _Credential, Convert.ToString(_dbRecord["url"]), Convert.ToString(_dbRecord["DocType"]), Convert.ToString(_dbRecord["DocSize"]), Convert.ToInt64(_dbRecord["IdFile"]));                                
            }
            return null;
        }

            //Trae todos los files para un recurso
            internal Dictionary<Int64, Entities.Catalog> Items()
        {
            ////Objeto de data layer para acceder a datos
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.Catalog> _oItems = new Dictionary<Int64, Entities.Catalog>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.Catalogues_ReadAll(_ResourceCatalog.IdResource);

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                    //Declara e instancia una posicion
                Entities.Catalog _catalog = Factories.CatalogFactory.CreateCatalog(Convert.ToInt64(_dbRecord["IdResourceFile"]), _ResourceCatalog, Convert.ToDateTime(_dbRecord["TimeStamp"]), Convert.ToInt64(_dbRecord["IdPerson"]), _Credential, Convert.ToString(_dbRecord["url"]), Convert.ToString(_dbRecord["DocType"]), Convert.ToString(_dbRecord["DocSize"]), Convert.ToInt64(_dbRecord["IdFile"]));                                

                    //Lo agrego a la coleccion
                    _oItems.Add(_catalog.IdResourceFile, _catalog);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions
            internal Entities.CatalogURL Add(DateTime timeStamp, String url)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idResourceFile = _dbKnowledgeCollaboration.Catalogues_Create(_ResourceCatalog.IdResource, timeStamp, _Credential.User.IdPerson);
                    
                    //hacer el addd del urllll
                    _dbKnowledgeCollaboration.ResourceUrls_Create(_idResourceFile, _ResourceCatalog.IdResource, url);

                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("KC_ResourceCatalogues", "Catalogues", "Add", "IdResource=" + _ResourceCatalog.IdResource + " and IdresourceFile=" + _idResourceFile, _Credential.User.IdPerson);
 
                    //Devuelvo el objeto FunctionalArea creado
                    return new Entities.CatalogURL(_idResourceFile, _ResourceCatalog, timeStamp, _Credential.User.IdPerson, _Credential, url);
          
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
            internal Entities.CatalogDoc Add(DateTime timeStamp, String docType, String docSize, String fileName, Byte[] fileStream)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idResourceFile = _dbKnowledgeCollaboration.Catalogues_Create(_ResourceCatalog.IdResource, timeStamp, _Credential.User.IdPerson);

                    //hacer el addd del doc
                    Int64 _idFile = _dbKnowledgeCollaboration.FileAttaches_Create(fileName, fileStream);

                    //Crea el resource
                    _dbKnowledgeCollaboration.ResourceDocs_Create(_idResourceFile, _ResourceCatalog.IdResource, docType, docSize, _idFile);

                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("KC_ResourceCatalogues", "Catalogues", "Add", "IdResource=" + _ResourceCatalog.IdResource + " and IdresourceFile=" + _idResourceFile, _Credential.User.IdPerson);

                    return new Entities.CatalogDoc(_idResourceFile, _ResourceCatalog, timeStamp, _Credential.User.IdPerson, _Credential, docType, docSize, _idFile);
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
            internal void Remove(Entities.Catalog catalog)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    if (catalog.GetType().Name == "CatalogDoc")
                    {
                        Int64 _idFile = ((Entities.CatalogDoc)catalog).FileAttach.IdFile;
                        _dbKnowledgeCollaboration.ResourceDocs_Delete(catalog.IdResourceFile, _ResourceCatalog.IdResource);
                        _dbKnowledgeCollaboration.FileAttaches_Delete(_idFile);
                    }
                    else
                    {
                        _dbKnowledgeCollaboration.ResourceUrls_Delete(catalog.IdResourceFile, _ResourceCatalog.IdResource);
                    }
                    _dbKnowledgeCollaboration.ResourceVersionHistories_Delete(catalog.IdResourceFile, catalog.IdResource);
                    _dbKnowledgeCollaboration.Catalogues_Delete(catalog.IdResourceFile, _ResourceCatalog.IdResource);

                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("KC_ResourceCatalogues", "Catalogues", "Delete", "IdResource=" + _ResourceCatalog.IdResource + " and IdresourceFile=" + catalog.IdResourceFile, _Credential.User.IdPerson);

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
