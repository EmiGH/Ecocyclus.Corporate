using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;


namespace Condesus.EMS.Business.DS.Entities
{
    public class FactoryJobtitle
    {
        internal FactoryJobtitle() { }

        internal Condesus.EMS.Business.DS.Entities.JobTitle CreateJobTitle(Int64 idOrganization, Int64 idGeographicArea, Int64 idFunctionalArea, Int64 idPosition, Credential credential, OrganizationalChart organizationalChart)
        {
            if (organizationalChart == null)
            {
                return new Condesus.EMS.Business.DS.Entities.JobTitle(idOrganization, idGeographicArea, idFunctionalArea, idPosition, credential);
            }
            else
            {
                return new Condesus.EMS.Business.DS.Entities.JobtitleWithChart(idOrganization, idGeographicArea, idFunctionalArea, idPosition, credential, organizationalChart);
            }
        }
    }
}
