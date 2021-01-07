using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.KC.Entities
{
    public abstract class Catalog : CatalogFile
    {
        #region Internal Region
        private ResourceCatalog _ResourceCatalog;
        private DateTime _TimeStamp;
        private Int64 _IdPerson;
        private ResourceVersionHistory _FinallyResourceVersionHistory;
        private Dictionary<Int64, ResourceVersionHistory> _ResourceVersionHistories;
        #endregion

        #region External Region
        public override Int64 IdResource
        {
            get { return _ResourceCatalog.IdResource; }
        }
        public ResourceCatalog ResourceCatalog
        {
            get { return _ResourceCatalog; }
        }        
        public DateTime TimeStamp
        {
            get { return _TimeStamp; }
        }
        public Int64 IdPerson
        {
            get { return _IdPerson; }
        }        

        #endregion

        internal Catalog(Int64 idResourceFile, ResourceCatalog resourceCatalog,  DateTime timeStamp, Int64 idPerson, Credential credential)
            : base (idResourceFile, credential)
        {
            _ResourceCatalog = resourceCatalog;
            _TimeStamp = timeStamp;
            _IdPerson = idPerson;
        }


    }
}
