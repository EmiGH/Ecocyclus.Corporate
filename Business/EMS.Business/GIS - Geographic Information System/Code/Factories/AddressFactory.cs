using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.GIS
{
    internal class AddressFactory
    {
        internal AddressFactory() { }

        internal Entities.Address CreateAddress(Int64 idAddress, Int64 idFacility, Int64 idGeographicArea, Int64 idPerson, String coordinate, 
            String street, String number, String floor, String department, String postCode, Credential credential)
        {
            if (idFacility != 0)
            { return new Entities.AddressFacility(idAddress, idFacility, idGeographicArea, coordinate, street, number, floor, department, postCode, credential); }
            else // if (idPerson != 0)
            { return new Entities.AddressPerson(idAddress, idPerson, idGeographicArea, coordinate, street, number, floor, department, postCode, credential); }
        }

    }
}
