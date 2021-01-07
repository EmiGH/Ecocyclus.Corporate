using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.GIS.Collections.FacilitiesRead
{
    internal class FacilityByProcess : ICollectionItems
    {
        private PF.Entities.ProcessGroupProcess _Process;

        internal FacilityByProcess(PF.Entities.ProcessGroupProcess process)
        {
            _Process = process;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.GIS.GeographicInformationSystem _dbGeographicInformationSystem = new Condesus.EMS.DataAccess.GIS.GeographicInformationSystem();

            return _dbGeographicInformationSystem.Sites_ReadByProcess(_Process.IdProcess, _Process.Credential.CurrentLanguage.IdLanguage);
        }

    }
}
