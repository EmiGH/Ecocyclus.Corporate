using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data.SqlClient;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Entities
{
    public class VersionURL : Version
    {
        #region Internal Region
        private String _Url;
        #endregion

        #region External Region
        public String Url
        {
            get { return _Url; }
        }
        #endregion

        internal VersionURL(Int64 idResourceFile, ResourceVersion resourceVersion, DateTime timeStamp, Int64 idPerson, DateTime validForm, DateTime validThrough, String version, Credential credential, 
            String url)
            : base(idResourceFile, resourceVersion, timeStamp, idPerson, validForm, validThrough, version, credential)
        {
            _Url = url;
        }

        public void Modify(Int64 idResourceFile, Int64 idResource, DateTime timeStamp, DateTime validFrom, DateTime validTrhough, String version, String url)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                using (TransactionScope _transactionScope = new TransactionScope())
                {

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbKnowledgeCollaboration.ResourceVersions_Update(idResourceFile, idResource, timeStamp, validFrom, validTrhough, version, Credential.User.IdPerson);

                    //hacer el addd del urllll
                    _dbKnowledgeCollaboration.ResourceUrls_Update(idResourceFile, idResource, url);

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
