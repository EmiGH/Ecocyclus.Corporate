using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Collections
{
    public class Versions
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.ResourceVersion _ResourceVersion;
            private IA.Entities.ExceptionResourceVersion _ExceptionResourceVersion;
        #endregion

            internal Versions(Entities.ResourceVersion resourceVersion, Credential credential)
        {
            _Credential = credential;
            _ResourceVersion = resourceVersion;
        }

        internal Versions(IA.Entities.ExceptionResourceVersion exceptionResourceVersion)
        {
            _ExceptionResourceVersion = exceptionResourceVersion;
            _Credential = exceptionResourceVersion.Credential;
        }

        #region Read Functions
            //Trae el file pedido con la version vigente
            internal Entities.Version Item(Int64 idResourceFile)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceVersions_ReadById(idResourceFile, _ResourceVersion.IdResource);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                return Factories.VersionFactory.CreateVersion(Convert.ToInt64(_dbRecord["IdResourceFile"]), _ResourceVersion, Convert.ToDateTime(_dbRecord["TimeStamp"]), Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToDateTime(_dbRecord["validFrom"]), Convert.ToDateTime(_dbRecord["validThrough"]), Convert.ToString(_dbRecord["Version"]), _Credential, Convert.ToString(_dbRecord["url"]), Convert.ToString(_dbRecord["DocType"]), Convert.ToString(_dbRecord["DocSize"]), Convert.ToInt64(_dbRecord["IdFile"]));                                
            }
            return null;
        }
            internal Entities.Version Item()
            {
                //Objeto de data layer para acceder a datos
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceVersions_ReadById(_ResourceVersion.IdResource);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    return Factories.VersionFactory.CreateVersion(Convert.ToInt64(_dbRecord["IdResourceFile"]), _ResourceVersion, Convert.ToDateTime(_dbRecord["TimeStamp"]), Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToDateTime(_dbRecord["validFrom"]), Convert.ToDateTime(_dbRecord["validThrough"]), Convert.ToString(_dbRecord["Version"]), _Credential, Convert.ToString(_dbRecord["url"]), Convert.ToString(_dbRecord["DocType"]), Convert.ToString(_dbRecord["DocSize"]), Convert.ToInt64(_dbRecord["IdFile"]));
                }
                return null;
            }

            internal Entities.Version Item(IA.Entities.ExceptionResourceVersion exceptionResourceVersion)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceVersions_ReadByException(exceptionResourceVersion.IdException);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_ResourceVersion == null) { _ResourceVersion = (KC.Entities.ResourceVersion)new Resources(_Credential).Item(Convert.ToInt64(_dbRecord["IdResource"])); }

                    return Factories.VersionFactory.CreateVersion(Convert.ToInt64(_dbRecord["IdResourceFile"]), _ResourceVersion, Convert.ToDateTime(_dbRecord["TimeStamp"]), Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToDateTime(_dbRecord["validFrom"]), Convert.ToDateTime(_dbRecord["validThrough"]), Convert.ToString(_dbRecord["Version"]), _Credential, Convert.ToString(_dbRecord["url"]), Convert.ToString(_dbRecord["DocType"]), Convert.ToString(_dbRecord["DocSize"]), Convert.ToInt64(_dbRecord["IdFile"]));
                }
                return null;
            }

            //Trae todos los files para un recurso
            internal Dictionary<Int64, Entities.Version> Items()
        {
            ////Objeto de data layer para acceder a datos
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.Version> _oItems = new Dictionary<Int64, Entities.Version>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceVersions_ReadAll(_ResourceVersion.IdResource);

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                    //Declara e instancia una posicion
                Entities.Version _version = Factories.VersionFactory.CreateVersion(Convert.ToInt64(_dbRecord["IdResourceFile"]), _ResourceVersion, Convert.ToDateTime(_dbRecord["TimeStamp"]), Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToDateTime(_dbRecord["validFrom"]), Convert.ToDateTime(_dbRecord["validThrough"]), Convert.ToString(_dbRecord["Version"]), _Credential, Convert.ToString(_dbRecord["url"]), Convert.ToString(_dbRecord["DocType"]), Convert.ToString(_dbRecord["DocSize"]), Convert.ToInt64(_dbRecord["IdFile"]));                                

                    //Lo agrego a la coleccion
                    _oItems.Add(_version.IdResourceFile, _version);
            }
            return _oItems;
        }

        #endregion

        #region Write Functions
            internal Entities.VersionURL Add(DateTime timeStamp, DateTime validFrom, DateTime validTrhough, String version, String url)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idResourceFile = _dbKnowledgeCollaboration.ResourceVersions_Create(_ResourceVersion.IdResource, timeStamp, validFrom, validTrhough, version, _Credential.User.IdPerson);
                    
                    //hacer el addd del urllll
                    _dbKnowledgeCollaboration.ResourceUrls_Create(_idResourceFile, _ResourceVersion.IdResource, url);

                    //esto es para que si es el primer resource ponga por default el current file
                    Dictionary<Int64, Entities.Version> _Items = Items();
                    if (_Items.Count == 1) { _dbKnowledgeCollaboration.Resources_UpdateCurrentFile(_ResourceVersion.IdResource, _idResourceFile); }

                    
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("KC_ResourceVersions", "Versions", "Add", "IdResource=" + _ResourceVersion.IdResource + " and IdResourceFile=" + _idResourceFile, _Credential.User.IdPerson);
                    //Devuelvo el objeto FunctionalArea creado
                    return new Entities.VersionURL(_idResourceFile, _ResourceVersion, timeStamp, _Credential.User.IdPerson, validFrom, validTrhough, version, _Credential, url);
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
            internal Entities.VersionDoc Add(DateTime timeStamp, DateTime validFrom, DateTime validTrhough, String version, String docType, String docSize, String fileName, Byte[] fileStream)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                    ////esto es para que si es el primer resource ponga por default el current file
                    //Dictionary<Int64, Entities.Version> _Items = Items();
                    
                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idResourceFile = _dbKnowledgeCollaboration.ResourceVersions_Create(_ResourceVersion.IdResource, timeStamp, validFrom, validTrhough, version, _Credential.User.IdPerson);

                    //hacer el addd del doc
                    Int64 _idFile = _dbKnowledgeCollaboration.FileAttaches_Create(fileName, fileStream);

                    //Crea el resource
                    _dbKnowledgeCollaboration.ResourceDocs_Create(_idResourceFile, _ResourceVersion.IdResource, docType, docSize, _idFile);

                    //esto es para que si es el primer resource ponga por default el current file
                    Dictionary<Int64, Entities.Version> _Items = Items();
                    if (_Items.Count == 1) { _dbKnowledgeCollaboration.Resources_UpdateCurrentFile(_ResourceVersion.IdResource, _idResourceFile); }

                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("KC_ResourceVersions", "Versions", "Add", "IdResource=" + _ResourceVersion.IdResource + " and IdresourceFile=" + _idResourceFile, _Credential.User.IdPerson);

                    return new Entities.VersionDoc(_idResourceFile, _ResourceVersion, timeStamp, _Credential.User.IdPerson, validFrom, validTrhough, version, _Credential, docType, docSize, _idFile);
                
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
            internal void Remove(Entities.Version version)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    if (version.GetType().Name == "VersionDoc")
                    {
                        Int64 _idFile = ((Entities.VersionDoc)version).FileAttach.IdFile;
                        _dbKnowledgeCollaboration.ResourceDocs_Delete(version.IdResourceFile, _ResourceVersion.IdResource);
                        _dbKnowledgeCollaboration.FileAttaches_Delete(_idFile);
                    }
                    else
                    {
                        _dbKnowledgeCollaboration.ResourceUrls_Delete(version.IdResourceFile, _ResourceVersion.IdResource);
                    }
                    version.Remove();
                    _dbKnowledgeCollaboration.ResourceVersionHistories_Delete(version.IdResourceFile, version.IdResource);
                    _dbKnowledgeCollaboration.ResourceVersions_Delete(version.IdResourceFile, _ResourceVersion.IdResource);
 
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("KC_ResourceVersions", "Versions", "Delete", "IdResource=" + _ResourceVersion.IdResource + " and IdresourceFile=" + version.IdResourceFile, _Credential.User.IdPerson);

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
