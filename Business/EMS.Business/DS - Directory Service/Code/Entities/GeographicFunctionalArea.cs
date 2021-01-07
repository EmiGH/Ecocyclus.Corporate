using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class GeographicFunctionalArea
    {
        #region Internal Properties
            private Int64 _IdGeographicArea;
            private Int64 _IdFunctionalArea;
            private Int64 _IdOrganization;
            private Int64 _IdParentGeographicArea;
            private Int64 _IdParentFunctionalArea; 
            private Credential _Credential;
            private FunctionalArea _FunctionalArea;
            private GIS.Entities.GeographicArea _GeographicArea;
            private GeographicFunctionalArea _ParentGeographicFunctionalArea;            
            private Entities.Organization _Organization;
            private List<Entities.GeographicFunctionalArea> _Children;  //Areas geofuncionales hijas
        #endregion

        #region External Properties
            public Int64 IdGeographicArea
            {
                get
                {
                    if (_GeographicArea == null) { _IdGeographicArea = 0; } else { _IdGeographicArea = GeographicArea.IdGeographicArea; }
                    return _IdGeographicArea;
                }
            }
            public Int64 IdFunctionalArea
            {
                get
                {
                    if (_FunctionalArea == null) { _IdFunctionalArea = 0; } else { _IdFunctionalArea = FunctionalArea.IdFunctionalArea; }
                    return _IdFunctionalArea;
                }
            }
            public Int64 IdParentGeographicArea
            {
                get
                {
                    if (_ParentGeographicFunctionalArea == null) { _IdParentGeographicArea = 0; } else { _IdParentGeographicArea = _ParentGeographicFunctionalArea.GeographicArea.IdGeographicArea; }
                    return _IdParentGeographicArea;
                }
            }
            public Int64 IdParentFunctionalArea
            {
                get
                {
                    if (_ParentGeographicFunctionalArea == null) { _IdParentFunctionalArea = 0; } else { _IdParentFunctionalArea = _ParentGeographicFunctionalArea.FunctionalArea.IdFunctionalArea; }
                    return _IdParentFunctionalArea;
                }
            }
            public Int64 IdOrganization
            {
                get
                {
                    if (_IdOrganization == null) { _IdOrganization = 0; } else { _IdOrganization = Organization.IdOrganization; }
                    return _IdOrganization;
                }
            }


            public String Name()
            {
                StringBuilder _compositeName = new StringBuilder();

                _compositeName.Append(GeographicArea.LanguageOption.Name.ToString());
                _compositeName.Append(" - ");
                _compositeName.Append(FunctionalArea.LanguageOption.Name.ToString());
                return _compositeName.ToString();
            }
            public Organization Organization
            {
                get
                {
                    return _Organization;
                }
            }
            public Entities.FunctionalArea FunctionalArea
            {
                get
                {
                    return _FunctionalArea;
                }
            }
            public GIS.Entities.GeographicArea GeographicArea
            {
                get
                {
                    return _GeographicArea;                
                }
            }
            public Entities.GeographicFunctionalArea ParentGeographicFunctionalArea
            {
                get
                {
                    return _ParentGeographicFunctionalArea;
                }
            }
            public List<Entities.GeographicFunctionalArea> Children
            {
                get
                {
                    if (_Children == null)
                    {
                        _Children = new Collections.GeographicFunctionalAreas(this,Organization).Items();
                    }
                    return _Children;
                }
            }
        #endregion

            internal GeographicFunctionalArea(FunctionalArea functionalArea, GIS.Entities.GeographicArea geographicArea, GeographicFunctionalArea parentGeographicFunctionalArea, Credential credential) 
        {
            _Credential = credential;
            _FunctionalArea = functionalArea;
            _GeographicArea = geographicArea;
            _Organization = functionalArea.Organization;
            _ParentGeographicFunctionalArea = parentGeographicFunctionalArea;
        }
    }
}
