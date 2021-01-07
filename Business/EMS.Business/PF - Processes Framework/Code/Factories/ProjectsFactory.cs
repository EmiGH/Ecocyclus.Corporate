using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Factories
{
    internal class ProjectsFactory
    {
        #region Internal Properties
            Credential _Credential;
        #endregion

        internal ProjectsFactory(Credential credential) 
        {
            _Credential=credential;
        }


        internal PF.Entities.ProcessGroupProcess CreateProcessGroupProcess(Int64 idProcess, Int16 weight, Int16 orderNumber, String idLanguage, String title, String purpose, String description, Int16 threshold, String identification, DateTime currentCampaignStartDate, Int64 idResourcePicture, String Coordinate, Int64 idGeographicArea, Int64 idOrganization, String TwitterUser, String FacebookUser)
        {
            return new PF.Entities.ProcessGroupProcess(idProcess, weight, orderNumber, idLanguage, title, purpose, description, _Credential, threshold, identification, currentCampaignStartDate, idResourcePicture, Coordinate, idGeographicArea, idOrganization, TwitterUser, FacebookUser); 
        }
    }    
}
