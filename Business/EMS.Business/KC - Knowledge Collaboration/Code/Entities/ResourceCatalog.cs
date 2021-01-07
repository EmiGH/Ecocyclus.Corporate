using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Entities
{
    public class ResourceCatalog : Resource
    {
        #region Internal Region
        private Catalog _Catalog;
        private Dictionary<Int64, Catalog> _Catalogues;

        #endregion

        #region External Region
        #region Catalog (Region Facade)
        public Catalog Catalog(Int64 idResourceFile)
        {
            if (_Catalog == null)
            { _Catalog = new Collections.Catalogues(this, Credential).Item(idResourceFile); }
            return _Catalog;
        }
        public Dictionary<Int64, Catalog> Catalogues
        {
            get
            {
                if (_Catalogues == null)
                { _Catalogues = new Collections.Catalogues(this, Credential).Items(); }
                return _Catalogues;
            }
        }

        public Catalog CatalogAdd(DateTime timeStamp, String url)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Catalog _catalog = new Collections.Catalogues(this, Credential).Add(timeStamp, url);
                _transactionScope.Complete();
                return _catalog;
            }
        }
        public Catalog CatalogAdd(DateTime timeStamp, String docType, String docSize, String fileName, Byte[] fileStream)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Catalog _catalog = new Collections.Catalogues(this, Credential).Add(timeStamp, docType, docSize, fileName, fileStream);
                _transactionScope.Complete();
                return _catalog;
            }
        }
        public void Remove(Catalog catalog)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.Catalogues(this, Credential).Remove(catalog);
                _transactionScope.Complete();
            }
        }
        /// <summary>
        /// Borra todos los archivos catalogos que tiene asociado
        /// </summary>
        internal override void RemoveAllFiles()
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {

                foreach (Catalog _catalog in Catalogues.Values)
                {
                    new Collections.Catalogues(this, Credential).Remove(_catalog);
                }

                base.RemoveAllFiles();

                _transactionScope.Complete();
            }
        }
        #endregion
        #endregion

        internal ResourceCatalog(Int64 idResource, ResourceType resourceType, String title, String description, Credential credential)
            : base (idResource, resourceType, title, description, credential)
        {
        }

    }
}
