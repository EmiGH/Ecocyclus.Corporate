using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    //public class ContactAddress
    //{
    //    #region Internal Properties
    //        private Credential _Credential;
    //        private Int64 _IdContactAddress;
    //        private String _Street;
    //        private String _Number;
    //        private String _Floor;
    //        private String _Apartment;
    //        private String _ZipCode;
    //        private String _City;
    //        private String _State;
    //        private Int64 _IdCountry;
    //        private Int64 _IdContactType;
    //        private Entities.Country _Country;
    //        private Entities.ContactType _ContactType;   
    //    #endregion

    //    #region External Properties
    //        public Int64 IdContactAddress
    //        {
    //            get { return _IdContactAddress; }            
    //        }
    //        public String Street
    //        {
    //            get { return _Street; }            
    //        }
    //        public String Number
    //        {
    //            get { return _Number; }           
    //        }
    //        public String Floor
    //        {
    //            get { return _Floor; }           
    //        }
    //        public String Apartment
    //        {
    //            get { return _Apartment; }            
    //        }
    //        public String ZipCode
    //        {
    //            get { return _ZipCode; }           
    //        }
    //        public String City
    //        {
    //            //Localidad
    //            get { return _City; }            
    //        }
    //        public String State
    //        {
    //            //Provincia
    //            get { return _State; }            
    //        }
    //        public Entities.ContactType ContactType
    //        {
    //            get
    //            {
    //                Collections.ApplicabilityContactTypes oApplicabilityContactTypes = new Condesus.EMS.Business.DS.Collections.ApplicabilityContactTypes(_Credential);

    //                if (_ContactType == null)
    //                {
    //                    //_ContactType = new Collections.ContactTypes(oApplicabilityContactTypes.Item("Direccion").IdApplicabilityContactType,_Credential).Item(_IdContactType);
    //                    _ContactType = new Collections.ContactTypes(Common.Constants.ContactTypeAddress, _Credential).Item(_IdContactType);
    //                }
    //                return _ContactType;
    //            }
    //        }
    //        public Entities.Country Country
    //        {
    //            get
    //            {
    //                if (_Country == null)
    //                {
    //                    _Country = new Collections.Countries(_Credential).Item(_IdCountry);
    //                }
    //                return _Country;
    //            }
    //        }
    //    #endregion

    //    internal ContactAddress(Int64 idContactAddress, String street, String number, 
    //        String floor, String apartment, String zipCode, String city, String state, 
    //        Int64 idCountry, Int64 idContactType, Credential credential)
    //    {
    //        _Credential = credential;
    //        _IdContactAddress = idContactAddress;
    //        _Street = street;
    //        _Number = number;
    //        _Floor = floor;
    //        _Apartment = apartment;
    //        _ZipCode = zipCode;
    //        _City = city;
    //        _State = state;
    //        _IdCountry = idCountry;
    //        _IdContactType = idContactType;
    //    }
    //}
}
