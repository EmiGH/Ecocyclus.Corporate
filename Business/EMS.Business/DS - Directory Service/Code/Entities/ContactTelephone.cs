using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    // public class ContactTelephone
    //{
    //    #region Internal Properties
    //        private Credential _Credential;
    //        private Int64 _IdContactTelephone;//Identificador del Telefono de contacto
    //        private Int64 _IdCountry;//Identificador del pais del telefono de contacto
    //        private Int64 _IdContactType;//Identificador del Tipo de contacto del telefono de contacto     
    //        private String _AreaCode;//Codigo de area del telefono del telefono de contacto
    //        private String _Number;//Numero de telefono del telefono de contacto
    //        private String _Extension;//Interno del telefono del telefono de contacto  
    //        //private Entities.Country _Country;
    //        private Entities.ContactType _ContactType;
    //    #endregion

    //    #region External Properties
    //        public Int64 IdContactTelephone
    //        {
    //            get { return _IdContactTelephone; }            
    //        }
    //        public String AreaCode
    //        {
    //            get { return _AreaCode; }            
    //        }
    //        public String Number
    //        {
    //            get { return _Number; }           
    //        }
    //        public String Extension
    //        {
    //            get { return _Extension; }           
    //        }
    //        public Entities.ContactType ContactType
    //        {
    //            get
    //            {
    //                Collections.ApplicabilityContactTypes oApplicabilityContactTypes = new Condesus.EMS.Business.DS.Collections.ApplicabilityContactTypes(_Credential);

    //                if (_ContactType == null)
    //                {
    //                    //_ContactType = new Collections.ContactTypes(oApplicabilityContactTypes.Item("Telefono").IdApplicabilityContactType, _Credential).Item(_IdContactType);
    //                    _ContactType = new Collections.ContactTypes(Common.Constants.ContactTypeTelephone, _Credential).Item(_IdContactType);
    //                }
    //                return _ContactType;
    //            }
    //        }
    //        //public Entities.Country Country
    //        //{
    //        //    get
    //        //    {
    //        //        if (_Country == null)
    //        //        {
    //        //            _Country = new Collections.Countries(_Credential).Item(_IdCountry);
    //        //        }
    //        //        return _Country;
    //        //    }
    //        //}
    //    #endregion

    //     internal ContactTelephone(Int64 idContactTelephone, String areaCode, String number,
    //        String extension, Int64 idCountry, Int64 idContactType, Credential credential)
    //    {
    //        _Credential = credential;
    //        //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
    //        _IdContactTelephone = idContactTelephone;
    //        _AreaCode = areaCode;
    //        _Number = number;
    //        _Extension = extension;           
    //        _IdCountry = idCountry;
    //        _IdContactType = idContactType;
    //    }
    //}
}
