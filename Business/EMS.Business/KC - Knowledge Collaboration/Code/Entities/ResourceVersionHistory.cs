using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data.SqlClient;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Entities
{
    public class ResourceVersionHistory
    {
        #region Internal Properties
        private Credential _Credential;
        private CatalogFile _CatalogFile;
        private DS.Entities.Post _Post;
        private Int64 _IdResourceFile;
        private Int64 _IdResource;
        private Int64 _IdResourceState;
        private DateTime _Date;
        //private Int64 _IdFunctionalArea;
        //private Int64 _IdPosition;
        //private Int64 _IdPerson;
        //private Int64 _IdOrganizationPost;
        private ResourceHistoryState _ResourceHistoryState; 
        #endregion

        #region External Properties
        public ResourceHistoryState ResourceHistoryState
        {
            get
            {
                return _ResourceHistoryState;
            }
        }
        #endregion

        //internal ResourceFileHistory(Catalog catalog, ResourceFileState resourceFileState, DateTime date, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition, Int64 idPerson, Int64 idOrganizationPost, Credential credential)
        //{
        //    _Credential = credential;
        //    _IdResourceFile = catalog.IdResourceFile;
        //    _IdResource = catalog.ResourceCatalog.IdResource;
        //    _ResourceFileState = resourceFileState;
        //    _Date = date;
        //    _IdGeographicArea = idGeographicArea;
        //    _IdFunctionalArea = idFunctionalArea;
        //    _IdPosition = idPosition;
        //    _IdPerson = idPerson;
        //    _IdOrganizationPost = idOrganizationPost;
        //}

        //internal ResourceFileHistory(VersionFile versionFile, ResourceFileState resourceFileState, DateTime date, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition, Int64 idPerson, Int64 idOrganizationPost, Credential credential)
        //{
        //    _Credential = credential;
        //    _IdResourceFile = versionFile.IdResourceFile;
        //    _IdResource = versionFile.ResourceFile.IdResource;
        //    _ResourceFileState = resourceFileState;
        //    _Date = date;
        //    _IdGeographicArea = idGeographicArea;
        //    _IdFunctionalArea = idFunctionalArea;
        //    _IdPosition = idPosition;
        //    _IdPerson = idPerson;
        //    _IdOrganizationPost = idOrganizationPost;
        //}

        internal ResourceVersionHistory(CatalogFile catalogFile, ResourceHistoryState ResourceHistoryState, DateTime date, DS.Entities.Post post, Credential credential)
        {
            _Credential = credential;
            _CatalogFile = catalogFile;
            _ResourceHistoryState = ResourceHistoryState;
            _Date = date;
            _Post = post;
            //_IdGeographicArea = idGeographicArea;
            //_IdFunctionalArea = idFunctionalArea;
            //_IdPosition = idPosition;
            //_IdPerson = idPerson;
            //_IdOrganizationPost = idOrganizationPost;
        }

    }
}
