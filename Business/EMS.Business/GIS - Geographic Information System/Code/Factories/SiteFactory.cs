using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.GIS
{
    internal class SiteFactory
    {
        internal SiteFactory() { }

        internal Entities.Site CreateFacilitySector(Int64 IdFacility, Int64 IdOrganization, Int64 IdParentFacility, String Coordinate, String Name, String Description, Int64 IdResourcePicture, Credential Credential, Int64 idFacilityType, Int64 idGeographicArea, Boolean active)
        {
            if (IdParentFacility == 0)
            { return new Entities.Facility(IdFacility, IdOrganization, Coordinate, Name, Description, IdResourcePicture, Credential, idFacilityType, idGeographicArea, active); }
            else
            { return new Entities.Sector(IdFacility, IdOrganization, IdParentFacility, Coordinate, Name, Description, IdResourcePicture, Credential, idFacilityType, idGeographicArea, active); }
        }

    }
}
