using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.GIS.Entities
{
    public class Sector : Site
    {
        #region Internal Properties
        private Int64 _IdParentFacility;
        private Site _ParentFacility;
        private FacilityType _FacilityType;
        #endregion

        #region External Properties
        public Site Parent
        {
            get
            { 
                if (_ParentFacility==null)
                { _ParentFacility = new Collections.Facilities(this).Item(_IdParentFacility); }
                return _ParentFacility;
            }
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
        #endregion


        internal Sector(Int64 idFacility, Int64 idOrganization, Int64 idParentFacility, String coordinate, String name, String description, Int64 IdResourcePicture, Credential credential, Int64 idFacilityType, Int64 idGeographicArea, Boolean active) 
            : base (idFacility, idOrganization, coordinate, name, description, IdResourcePicture, credential, idFacilityType , idGeographicArea, active)
        {
            _IdParentFacility = idParentFacility;
        }

        public void Modify(Entities.Site parent, String coordinate, String name, String description, Entities.FacilityType facilityType, KC.Entities.ResourceCatalog resourcePicture, Boolean active)
        {
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                new GIS.Collections.Facilities(this).Modify(this, parent, coordinate, name, description, resourcePicture, facilityType, parent.IdGeographicArea, active);

                //modifica el facility type y el area geografica de sus hijos
                foreach (Sector _sector in this.Sectors.Values)
                {
                    _sector.Modify(this, _sector.Coordinate, _sector.LanguageOption.Name, _sector.LanguageOption.Description, facilityType, _sector.ResourcePicture, _sector.Active);
                }

                _transactionScope.Complete();
            }
        }

        internal Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessesAssociated
        {
            get
            {
                return new PF.Collections.ProcessGroupProcesses(Credential).Items(this);
            }
        }

    }
}
