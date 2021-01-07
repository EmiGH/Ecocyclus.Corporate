using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.GIS.Entities
{
    public class Facility : Site
    {
        #region Internal Properties        
        private FacilityType _FacilityType;
        //private Int64 _IdFacilityType;
        //private Int64 _IdGeographicArea;
        private GeographicArea _GeographicArea;
        #endregion

        public Dictionary<Int64, PA.Entities.Measurement> Measurements(PA.Entities.AccountingScope accountingScope, PA.Entities.AccountingActivity accountingActivity)
        {
            return new PA.Collections.Measurements(Credential).Items(this, accountingScope, accountingActivity);
        }

        public FacilityType FacilityType
        {
            get
            {
                if (_FacilityType == null)
                { _FacilityType = new Collections.FacilityTypes(Credential).Item(IdFacilityType); }
                return _FacilityType;
            }
        }
        public GeographicArea GeographicArea
        {
            get
            {
                if (_GeographicArea == null)
                { _GeographicArea = new Collections.GeographicAreas(Credential).Item(IdGeographicArea); }
                return _GeographicArea;
            }
        }

        //Reporte
        public Decimal ReadTotalMeasurementResultByIndicator(PA.Entities.AccountingScope scope, PA.Entities.AccountingActivity activity, PA.Entities.Indicator indicatorColumnGas, DateTime? startDate, DateTime? endDate)
        {
            return new Collections.Facilities(this).ReadTotalMeasurementResultByIndicator(scope, this, activity, indicatorColumnGas, startDate, endDate);
        }


        internal Facility(Int64 idFacility, Int64 idOrganization, String coordinate, String name, String description, Int64 IdResourcePicture, Credential credential, Int64 idFacilityType, Int64 idGeographicArea, Boolean active) 
            : base (idFacility, idOrganization, coordinate, name, description, IdResourcePicture, credential, idFacilityType, idGeographicArea, active)
        {

        }

        public void Modify(String coordinate, String name, String description, KC.Entities.ResourceCatalog resourcePicture, Entities.FacilityType facilityType, Entities.GeographicArea geographicArea, Boolean active)
        {
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                new GIS.Collections.Facilities(this).Modify(this, this.Organization, coordinate, name, description, resourcePicture, facilityType, geographicArea, active);

                //modifica el facility type y el area geografica de sus hijos
                foreach (Sector _sector in this.Sectors.Values)
                {
                    _sector.Modify(this, _sector.Coordinate, _sector.LanguageOption.Name, _sector.LanguageOption.Description, _sector.FacilityType, _sector.ResourcePicture, _sector.Active);
                }

                _transactionScope.Complete();
            }
        }

        public Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessesAssociated
        {
            get
            {
                Dictionary<Int64, PF.Entities.ProcessGroupProcess> _processes = new Dictionary<long, Condesus.EMS.Business.PF.Entities.ProcessGroupProcess>();
                
                _processes = new PF.Collections.ProcessGroupProcesses(Credential).Items(this);

                foreach (Sector _sector in this.Sectors.Values)
                {
                    ProcessesAssociatedSector(_sector, ref _processes);
                }

                return _processes;
            }
        }

        private void ProcessesAssociatedSector(Sector sector, ref Dictionary<Int64, PF.Entities.ProcessGroupProcess> processes)
        {         
            foreach (PF.Entities.ProcessGroupProcess _process in sector.ProcessesAssociated.Values)
            {
                if (!processes.ContainsKey(_process.IdProcess))
                {
                    processes.Add(_process.IdProcess, _process);
                }
            }

            foreach (Sector _sector in sector.Sectors.Values)
            {
                ProcessesAssociatedSector(_sector, ref processes);
            }
        }

    }
}
