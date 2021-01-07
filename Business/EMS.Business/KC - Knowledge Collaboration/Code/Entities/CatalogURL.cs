using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Condesus.EMS.Business.Security;
using System.Data.SqlClient;
 
namespace Condesus.EMS.Business.KC.Entities
{
    public class CatalogURL : Catalog
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

        internal CatalogURL(Int64 idResourceFile, ResourceCatalog resourceCatalog, DateTime timeStamp, Int64 idPerson, Credential credential, 
            String url)
            : base(idResourceFile, resourceCatalog, timeStamp, idPerson, credential)
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
