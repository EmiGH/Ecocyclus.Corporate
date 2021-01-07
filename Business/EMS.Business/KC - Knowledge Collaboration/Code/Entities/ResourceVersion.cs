using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Entities
{
    public class ResourceVersion : Resource
    {
        #region Internal Region
        private Int64 _CurrentFile;
        private Version _Version;
        private Dictionary<Int64, Version> _Versions;
        private Dictionary<Int64, IA.Entities.Exception> _Exceptions;
        #endregion

        #region External Region
        #region ResourceFile
        public Version CurrentVersion //resource de la current version
        {
            get
            {
                if (_Version == null)
                { _Version = new Collections.Versions(this, Credential).Item(); }
                return _Version;
            }
        }
        public Version Version(Int64 idResourceFile)
        {
            if (_Version == null)
            { _Version = new Collections.Versions(this, Credential).Item(idResourceFile); }
            return _Version;
        }
        public Dictionary<Int64, Version> Versions
        {
            get
            {
                if (_Versions == null)
                { _Versions = new Collections.Versions(this, Credential).Items(); }
                return _Versions;
            }
        }

        public Version VersionAdd(DateTime timeStamp, DateTime validFrom, DateTime validThrough, String version, String url)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Version _version = new Collections.Versions(this, Credential).Add(timeStamp, validFrom, validThrough, version, url);
                _transactionScope.Complete();
                return _version;
            }
        }
        public Version VersionAdd(DateTime timeStamp, DateTime validFrom, DateTime validThrough, String version, String docType, String docSize, String fileName, Byte[] fileStream)
        {
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                Version _version = new Collections.Versions(this, Credential).Add(timeStamp, validFrom, validThrough, version, docType, docSize, fileName, fileStream);
                _transactionScope.Complete();
                return _version;
            }
        }
        public void Remove(Version version)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.Versions(this, Credential).Remove(version);
                _transactionScope.Complete();
            }
        }
        /// <summary>
        /// Borra todos los archivos version que tiene asociado
        /// </summary>
        internal override void RemoveAllFiles()
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                foreach (Version _version in Versions.Values)
                {
                    new Collections.Versions(this, Credential).Remove(_version);
                }

                base.RemoveAllFiles();

                _transactionScope.Complete();
            }
        }

        #endregion

        #region Exceptions
        //devuelve todas las excepciones de cada uno de lo archivos versionables
        public Dictionary<Int64, IA.Entities.Exception> Exceptions
        {
            get
            {
                if (_Exceptions == null)
                {
                    foreach (Version _item in Versions.Values)
                    {
                        _Exceptions = _item.Exceptions;
                    }
                }
                return _Exceptions;
            }
        }


        #endregion

        #endregion

        internal ResourceVersion(Int64 idResource, Entities.ResourceType resourceType, String title, String description, Credential credential,
                Int64 currentFile)
                : base (idResource, resourceType, title, description, credential)
        {
            _CurrentFile = currentFile;            
        }

        public void Modify(Version currentFile)
        {
            new Collections.Resources(Credential).Modify(this, currentFile);
        }
    }
}
