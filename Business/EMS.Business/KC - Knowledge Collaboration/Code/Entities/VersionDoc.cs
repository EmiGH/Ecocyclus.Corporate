using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data.SqlClient;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Entities
{
    public class VersionDoc : Version 
    {
        #region Internal Properties
            private String _DocType;
            private String _DocSize;   
            private Int64 _IdFile;
            private FileAttach _FileAttach;
        #endregion

            #region External Properties
            public String DocType
            {
                get { return _DocType; }
            }
            public String DocSize
            {
                get { return _DocSize; }
            }
            public FileAttach FileAttach
            {
                get 
                {
                    if (_FileAttach == null)
                    { _FileAttach = new Collections.FileAttachs().Item(_IdFile); }
                    return _FileAttach;
                }
            }
        #endregion

            internal VersionDoc(Int64 idResourceFile, ResourceVersion resourceVersion, DateTime timeStamp, Int64 idPerson, DateTime validForm, DateTime validThrough, String version, Credential credential,
            String docType, String docSize, Int64 idFile) 
            : base (idResourceFile, resourceVersion, timeStamp, idPerson, validForm, validThrough, version, credential)
        {
            _DocType = docType;
            _DocSize = docSize;
            _IdFile = idFile;            
        }

            public void Modify(Int64 idResourceFile, Int64 idResource, DateTime timeStamp, DateTime validFrom, DateTime validTrhough, String version, String docType, String docSize, Int64 idFile, String fileName, Byte[] fileStream)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                //DataAccess.KC.ResourceFiles _dbResourceFiles = new Condesus.EMS.DataAccess.KC.ResourceFiles();
                //DataAccess.KC.ResourceFileDocs _dbResourceFileDocs = new Condesus.EMS.DataAccess.KC.ResourceFileDocs();
                //DataAccess.KC.FileAttaches _dbFileAttaches = new Condesus.EMS.DataAccess.KC.FileAttaches();
                //aca es donde se arma una transaccion y primero se graba la tarea de recovery
                //y luego se graban las fechas de medicion que se habilitan para recuperar.
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbKnowledgeCollaboration.ResourceVersions_Update(idResourceFile, idResource, timeStamp, validFrom, validTrhough, version, Credential.User.IdPerson);

                    //hacer el addd del doc
                    _dbKnowledgeCollaboration.FileAttaches_Update(idFile, fileName, fileStream);

                    _dbKnowledgeCollaboration.ResourceDocs_Update(idResourceFile, idResource, docType, docSize, idFile);

                    _transactionScope.Complete();
                }
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
    }
}
