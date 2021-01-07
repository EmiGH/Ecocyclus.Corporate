using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.GIS.Entities
{
    public class AddressPerson : Address
    {
        #region internal properties
        private Int64 _IdPerson; 
        #endregion

        #region External Properties
        public Int64 IdPerson
        {
            get { return _IdPerson; }
        }
        #endregion

        internal AddressPerson(Int64 idAddress, Int64 idPerson, Int64 idGeographicArea, String coordinate, 
        String street, String number, String floor, String department, String postCode, Credential credential)
            : base(idAddress, idGeographicArea, coordinate, street, number, 
            floor, department, postCode, credential)
        {
            _IdPerson = idPerson;
        }

        public void Modify(DS.Entities.Person person, GeographicArea geographicArea, String coordinate, 
        String street, String number, String floor, String department, String postCode)
        {
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
            new Collections.Addresses(Credential).Modify(this, person, geographicArea, coordinate, street, number, floor, department, postCode);
            _transactionScope.Complete();
            }
        }

    }
}
