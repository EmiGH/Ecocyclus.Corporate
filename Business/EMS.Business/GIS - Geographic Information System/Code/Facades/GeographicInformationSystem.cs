using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.GIS
{
    public class GeographicInformationSystem : IModule
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        #region External Properties
            public String ModuleName
            { 
                get { return "GIS"; } 
            }
            public String ClassName //usa ConfigurationDS porque valida con esa seguridad
            {
                get
                {
                    return Common.Security.ConfigurationDS;
                }
            }

            #region Facility Types
            public GIS.Entities.FacilityType FacilityType(Int64 idFacilityType)
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, 0, _Credential.User.IdPerson, Common.Permissions.View);
                return new GIS.Collections.FacilityTypes(_Credential).Item(idFacilityType);
            }
            public Dictionary<Int64, GIS.Entities.FacilityType> FacilityTypes()
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, 0, _Credential.User.IdPerson, Common.Permissions.View);
                return new GIS.Collections.FacilityTypes(_Credential).Items();
            }
            public GIS.Entities.FacilityType FacilityTypeAdd(String IconName, String name, String description)
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, 0, _Credential.User.IdPerson, Common.Permissions.Manage);
                using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                {
                    GIS.Entities.FacilityType _facilityType = new GIS.Collections.FacilityTypes(_Credential).Create(IconName , name, description);
                    _transactionScope.Complete();
                    return _facilityType;
                }
            }
            public void Remove(Entities.FacilityType facilityType)
            {
                //Realiza las validaciones de autorizacion 
                //new Security.Authority(_Credential).Authorize(this.ClassName, 0, _Credential.User.IdPerson, Common.Permissions.Manage);
                using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                {
                    new GIS.Collections.FacilityTypes(_Credential).Delete(facilityType);
                    _transactionScope.Complete();
                }
            }
            #endregion            
            
            #region Geographic Areas
            public Dictionary<Int64, Entities.GeographicArea> GeographicAreas()
            {
                //Realiza las validaciones de autorizacion 
                new Security.Authority(_Credential).Authorize(this.ClassName, 0, _Credential.User.IdPerson, Common.Permissions.View);
                return new Collections.GeographicAreas(_Credential).Items();
            }
            public Entities.GeographicArea GeographicArea(Int64 idGeographicArea)
            {
                //Realiza las validaciones de autorizacion 
                new Security.Authority(_Credential).Authorize(this.ClassName, 0, _Credential.User.IdPerson, Common.Permissions.View);
                return new Collections.GeographicAreas(_Credential).Item(idGeographicArea);
            }
            public Entities.GeographicArea GeographicAreasAdd(Entities.GeographicArea parentGeographicArea, String coordinate, String isoCode, DS.Entities.Organization organization, String name, String description, String Layer)
            {
                //Realiza las validaciones de autorizacion 
                new Security.Authority(_Credential).Authorize(this.ClassName, 0, _Credential.User.IdPerson, Common.Permissions.Manage);
                using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                {
                    GIS.Entities.GeographicArea _geographicArea  = new Collections.GeographicAreas(_Credential).Add(parentGeographicArea, coordinate, isoCode, organization, name, description, Layer);
                    _transactionScope.Complete();
                    return _geographicArea;
                }
            }
            public void Remove(Entities.GeographicArea geographicArea)
            {
                //Realiza las validaciones de autorizacion 
                new Security.Authority(_Credential).Authorize(this.ClassName, 0, _Credential.User.IdPerson, Common.Permissions.Manage);
                using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                {
                    new Collections.GeographicAreas(_Credential).Remove(geographicArea);
                    _transactionScope.Complete();
                }
            }
            #endregion

            #region Site (puente para dibujar el mapa)
            
            public Entities.Site Site(Int64 idFacility)
            {
                return new Collections.Facilities(_Credential).Item(idFacility);
            }
            #endregion

        #endregion

        internal GeographicInformationSystem(Credential credential)
        {
            _Credential = credential;
        }
    }
}
