using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.GIS.Entities
{
    public class AddressFacility :Address
    {
            #region internal properties
            private Int64 _IdFacility;
            private Entities.Facility _Facility;
            #endregion

            #region External Properties
            public Int64 IdFacility
            {
                get { return _IdFacility; }
            }
            public Entities.Facility Facility
            {
                get
                {
                    if (_Facility == null)
                    { _Facility = (Entities.Facility)new Collections.Facilities(Credential).Item(_IdFacility); }
                    return _Facility;
                }
            }
            #endregion

            internal AddressFacility(Int64 idAddress, Int64 idFacility, Int64 idGeographicArea, String coordinate, 
            String street, String number, String floor, String department, String postCode, Credential credential)
                : base(idAddress, idGeographicArea, coordinate, street, number, 
                floor, department, postCode, credential)
            {
                _IdFacility = idFacility;
            }

            public void Modify(Entities.Site site, GeographicArea geographicArea, String coordinate, 
            String street, String number, String floor, String department, String postCode)
            {
             using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
               new Collections.Addresses(Credential).Modify(this, site, geographicArea, coordinate, street, number, floor, department, postCode);
               _transactionScope.Complete();
            }
            }

    }
}
