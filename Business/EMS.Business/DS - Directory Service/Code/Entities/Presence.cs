using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class Presence
    {
        #region Internal Properties
            private Credential _Credential;
            public Organization _Organization;
            private GIS.Entities.Facility _Facility;
            private Entities.Post _Post;
        #endregion

        #region External Properties
        public Organization Organization
        {
            get
            {
                return _Organization;
            }
        }
        public Post Post
        {
            get
            {
                return _Post;
            }
        }
        public GIS.Entities.Facility Facility
        {
            get{return _Facility;}
        }
        //{ get 
        //    {
        //        if (_GeographicAreaFacility == null)
        //        {
        //            Condesus.EMS.Business.DS.Collections.GeographicAreas oGeoArea = new Condesus.EMS.Business.DS.Collections.GeographicAreas(Organization);
        //            _GeographicAreaFacility = (GeographicAreaFacilities)oGeoArea.Item(_IdFacility);
        //        }
        //        return _GeographicAreaFacility;
        //    }
        //}
        #endregion

        internal Presence(Post post, GIS.Entities.Facility facility, Credential credential)
        {
            _Credential = credential;
            _Facility = facility;
            _Post = post;
            _Organization = post.Organization;
        }
    }
}
