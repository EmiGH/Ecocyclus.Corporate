using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.KC.Entities
{
    public abstract class CatalogFile
    {
        #region Internal Region
        private Int64 _IdResourceFile;
        private Credential _Credential;
        #endregion

        #region External Region
        
        public Int64 IdResourceFile
        {
            get { return _IdResourceFile; }
        }
        public abstract Int64 IdResource { get; }

        internal Credential Credential
        {
            get { return _Credential; }
        }
        #endregion

        internal CatalogFile(Int64 idResourceFile, Credential credential)                            
        {
            _IdResourceFile = idResourceFile;
            _Credential = credential;
        }
    }
}
