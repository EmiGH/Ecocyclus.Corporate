using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.DS.Entities
{
    public abstract class Person
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdPerson;//Identificador de la persona      
            private Int64 _IdSalutationType;//Identificador del nombramiento de la persona (Sr., Dr., etc.)   
            private String _LastName;//Apellido de la persona
            private String _FirstName;//Nombre de la persona
            private String _PosName;
            private String _NickName;//Apodo de la persona
            private String _IdLanguage;

            private SalutationType _SalutationType;
            private Dictionary<Int64, DS.Entities.Telephone> _Telephones;
            private Dictionary<Int64, GIS.Entities.Address> _Addresses;//Objeto con los datos de direccion de la organizacion
            private Dictionary<Int64, Entities.ContactEmail> _ContactEmails;//Objeto con los datos de email de la organizacion
            private Dictionary<Int64, Entities.ContactMessenger> _ContactMessengers;//Objeto con los datos de mensajeros de la organizacion
            //private Dictionary<Int64, Entities.ContactTelephone> _ContactTelephones;//Objeto con los datos de los telefonos de la organizacion
            private Dictionary<Int64, Entities.ContactURL> _ContactURLs;//Objeto con los datos de direeciones web de la organizacion
            private List<Entities.Post> _Posts;   //puestos de la persona

            private Entities.Organization _Organization; //Objeto con los datos de la organizacion.
            private KC.Entities.ResourceCatalog _ResourcePicture;
        #endregion
        
        #region External Properties
            public String FullName
            {
                get { return String.Concat(_LastName,", ", _FirstName, " ", _PosName); }
            }
            public String NickName
            {
                get { return _NickName; }         
            }
            public String PosName
            {
                get { return _PosName; }       
            }
            public String FirstName
            {
                get { return _FirstName; }         
            }
            public String LastName
            {
                get { return _LastName; }         
            }
            public Int64 IdPerson
            {
                get { return _IdPerson; }         
            }
            public SalutationType SalutationType
            {
                get
                {
                    if (_SalutationType == null)
                    { _SalutationType = new Collections.SalutationTypes(_Credential).Item(_IdSalutationType); }
                    return _SalutationType;
                }
            }
            public Organization Organization
            {
                get { return _Organization; }
            }
       
            internal Credential Credential
            {
                get
                { return _Credential; }
            }

            //#region Contact Addresses
            //public Dictionary<Int64, Entities.ContactAddress> ContactAddresses()
            //{
            //    if (_ContactAddresses == null)
            //    {
            //        _ContactAddresses = new Collections.ContactAddresses(this).Items();
            //    }
            //    return _ContactAddresses;
            //}
            //public Entities.ContactAddress ContactAddress(Int64 idContactAddress)
            //{
            //    return new Collections.ContactAddresses(this).Item(idContactAddress);
            //}
            //public Entities.ContactAddress ContactAddressesAdd(String street, String number, String floor, String apartment, String zipCode, String city, String state, Entities.Country country, Entities.ContactType contactType)
            //{
            //    Entities.ContactAddress _contactAddress = new Collections.ContactAddresses(this).AddByPerson(street, number, floor, apartment, zipCode, city, state, country, contactType);

            //    return _contactAddress;
            //}
            //public void Remove(ContactAddress contactAddress)
            //{
            //    new Collections.ContactAddresses(this).RemoveByPerson(contactAddress);
            //}
            //public Entities.ContactAddress ContactAddressesModify(ContactAddress contactAddress, String street, String number, String floor, String apartment, String zipCode, String city, String state, Entities.Country country, Entities.ContactType contactType)
            //{
            //    Collections.ContactAddresses _contactAddresses = new Collections.ContactAddresses(this);
            //    _contactAddresses.ModifyByPerson(contactAddress, street, number, floor, apartment, zipCode, city, state, country, contactType);
            //    return _contactAddresses.Item(contactAddress.IdContactAddress);
            //}
            //#endregion

            #region Addresses
            public Dictionary<Int64, GIS.Entities.Address> Addresses()
            {
                if (_Addresses == null)
                {
                    _Addresses = new GIS.Collections.Addresses(this).Items();
                }
                return _Addresses;
            }
            public GIS.Entities.AddressPerson Address(Int64 idAddress)
            {
                return (GIS.Entities.AddressPerson)new GIS.Collections.Addresses(this).Item(idAddress);
            }
            public GIS.Entities.Address AddressesAdd(GIS.Entities.GeographicArea geographicArea, String coordinate, 
            String street, String number, String floor, String department, String postCode)
            {
                GIS.Entities.Address _Address = new GIS.Collections.Addresses(this).Add(this, geographicArea, coordinate, street, number, floor, department, postCode);

                return _Address;
            }
            public void Remove(GIS.Entities.Address address)
            {
                new GIS.Collections.Addresses(this).Remove(address);
            }
            #endregion

            #region Telephnes
            public Dictionary<Int64, DS.Entities.Telephone> Telephones
            {
                get
                {
                    if (_Telephones == null)
                    { _Telephones = new DS.Collections.Telephones(this).Items(); }
                    return _Telephones;
                }
            }
            public DS.Entities.Telephone Telephone(Int64 idTelephone)
            {
                return new DS.Collections.Telephones(this).Item(idTelephone);
            }
            public DS.Entities.Telephone TelephoneAdd(String areaCode, String number, String extension, String internationalCode, String reason)
            {
                DS.Entities.Telephone _Telephone = new DS.Collections.Telephones(this).Add(this, areaCode, number, extension, internationalCode, reason);

                return _Telephone;
            }
            public void Remove(DS.Entities.Telephone telephone)
            {
                new DS.Collections.Telephones(this).Remove(telephone);
            }
            #endregion

            #region Contact Emails
                public Dictionary<Int64, Entities.ContactEmail> ContactEmails()
                {
                    if (_ContactEmails == null)
                    {
                        _ContactEmails = new Collections.ContactEmails(this).Items();
                    }
                    return _ContactEmails;
                }
                public Entities.ContactEmail ContactEmail(Int64 idContactEmail)
                {
                    return new Collections.ContactEmails(this).Item(idContactEmail);
                }
                public Entities.ContactEmail ContactEmailsAdd(String email, ContactType contactType)
                {
                    Entities.ContactEmail _contactEmail = new Collections.ContactEmails(this).AddByPerson(email, contactType);
                    return _contactEmail;
                }
                public void Remove(ContactEmail contactEmail)
                {
                    Collections.ContactEmails _contactEmails = new Collections.ContactEmails(this);
                    _contactEmails.RemoveByPerson(contactEmail);
                }
                public Entities.ContactEmail ContactEmailsModify(ContactEmail contactEmail, String email, ContactType contactType)
                {
                    Collections.ContactEmails _contactEmails = new Collections.ContactEmails(this);
                    _contactEmails.ModifyByPerson(contactEmail, email, contactType);
                    return _contactEmails.Item(contactEmail.IdContactEmail);
                }
            #endregion

            #region Contact Messengers
                public Dictionary<Int64, Entities.ContactMessenger> ContactMessengers()
                {
                    if (_ContactMessengers == null)
                    {
                        _ContactMessengers = new Collections.ContactMessengers(this).Items();
                    }
                    return _ContactMessengers;
                }
                public Entities.ContactMessenger ContactMessenger(Int64 idContactMessenger)
                {
                    return new Collections.ContactMessengers(this).Item(idContactMessenger);
                }
                public Entities.ContactMessenger ContactMessengersAdd(String provider, String application, String data, ContactType contactType)
                {
                    Entities.ContactMessenger _contactMessenger = new Collections.ContactMessengers(this).AddByPerson(provider, application, data, contactType);

                    return _contactMessenger;
                }
                public void Remove(ContactMessenger contactMessenger)
                {
                    Collections.ContactMessengers _contactMessengers = new Collections.ContactMessengers(this);
                    _contactMessengers.RemoveByPerson(contactMessenger);

                }
                public Entities.ContactMessenger ContactMessengersModify(ContactMessenger contactMessenger, String provider, String application, String data, ContactType contactType)
                {
                    Collections.ContactMessengers _contactMessengers = new Collections.ContactMessengers(this);
                    _contactMessengers.ModifyByPerson(contactMessenger, provider, application, data, contactType);

                    return _contactMessengers.Item(contactMessenger.IdContactMessenger);
                }
            #endregion

            //#region Contact Telephones
            //    public Dictionary<Int64, Entities.ContactTelephone> ContactTelephones()
            //    {
            //        if (_ContactTelephones == null)
            //        {
            //            _ContactTelephones = new Collections.ContactTelephones(this).Items();
            //        }
            //        return _ContactTelephones;
            //    }
            //    public Entities.ContactTelephone ContactTelephone(Int64 idContactTelephone)
            //    {
            //        return new Collections.ContactTelephones(this).Item(idContactTelephone);
            //    }
            //    public Entities.ContactTelephone ContactTelephonesAdd(String areaCode, String number, String extension, Entities.Country country, Entities.ContactType contactType)
            //    {
            //        Entities.ContactTelephone _contactTelephone = new Collections.ContactTelephones(this).AddByPerson(areaCode, number, extension, country, contactType);
            //        return _contactTelephone;
            //    }
            //    public void Remove(ContactTelephone contactTelephone)
            //    {
            //        Collections.ContactTelephones _contactTelephones = new Collections.ContactTelephones(this);
            //        _contactTelephones.RemoveByPerson(contactTelephone);

            //    }
            //    public Entities.ContactTelephone ContactTelephonesModify(ContactTelephone contactTelephone, String areaCode, String number, String extension, Entities.Country country, Entities.ContactType contactType)
            //    {
            //        Collections.ContactTelephones _contactTelephones = new Collections.ContactTelephones(this);
            //        _contactTelephones.ModifyByPerson(contactTelephone, areaCode, number, extension, country, contactType);
            //        return _contactTelephones.Item(contactTelephone.IdContactTelephone);
            //    }
            //#endregion

            #region Contact URLs
                public Dictionary<Int64, Entities.ContactURL> ContactURLs()
                {
                    if (_ContactURLs == null)
                    {
                        _ContactURLs = new Collections.ContactURLs(this).Items();
                    }
                    return _ContactURLs;
                }
                public Entities.ContactURL ContactURL(Int64 idContactURL)
                {
                    return new Collections.ContactURLs(this).Item(idContactURL);
                }
                public Entities.ContactURL ContactURLsAdd(String url, String name, String description, ContactType contactType)
                {
                    Entities.ContactURL _contactURL = new Condesus.EMS.Business.DS.Collections.ContactURLs(this).AddByPerson(contactType, url, name, description);
                    return _contactURL;
                }
                public void Remove(Entities.ContactURL contactURL)
                {
                    new Collections.ContactURLs(this).RemoveByPerson(contactURL);
                }
                public void ContactURLsModify(Entities.ContactURL contactURL, String url, String name, String description, ContactType contactType)
            {
                new Condesus.EMS.Business.DS.Collections.ContactURLs(this).ModifyByPerson(contactURL, contactType, url, name, description);
            }
            #endregion

            #region Messages
                public Dictionary<Int64, CT.Entities.Message> MessagesPost
                {
                    get
                    {
                        return new CT.Collections.Messages(this).Items();
                    }
                }
            #endregion

         
            #region Resources
                /// <summary>
                /// Retorna la coleccion de Imagenes que tiene asociado. (a travez de CatalogDoc)
                /// Key = IdResourceFile
                /// </summary>
                public Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> Pictures
                {
                    get
                    {
                        Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> _pictures = new Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc>();
                        //Si el proyecto no tiene ningun ResourcePicture Asociado...entrega vacio.
                        if (this.ResourcePicture != null)
                        {
                            foreach (Condesus.EMS.Business.KC.Entities.Catalog _catalog in this.ResourcePicture.Catalogues.Values)
                            {
                                if (_catalog.GetType().Name == "CatalogDoc")
                                {
                                    //Lo castea a tipo Doc
                                    Condesus.EMS.Business.KC.Entities.CatalogDoc _catalogDoc = (Condesus.EMS.Business.KC.Entities.CatalogDoc)_catalog;

                                    //Solo nos quedamos con los que son tipo image
                                    if (_catalogDoc.DocType.Contains("image"))
                                    {
                                        _pictures.Add(_catalogDoc.IdResourceFile, _catalogDoc);
                                    }
                                }
                            }
                        }
                        return _pictures;
                    }
                }

                public KC.Entities.ResourceCatalog ResourcePicture
                {
                    get
                    {
                        return _ResourcePicture;
                    }
                }
                #endregion

            /// <summary>
            /// Borra sus dependencias
            /// </summary>
            internal virtual void Remove()
                {
                    //Borra las relaciones con contactURL
                    foreach (ContactURL _contactURL in ContactURLs().Values)
                    {
                        this.Remove(_contactURL);
                    }
                    ////Borra las relaciones con ContactTelephone
                    //foreach (ContactTelephone _contactTelephone in ContactTelephones().Values)
                    //{
                    //    this.Remove(_contactTelephone);
                    //}
                    ////Borra las relaciones con Addresses
                    //foreach (ContactAddress _contactAddress in ContactAddresses().Values)
                    //{
                    //    this.Remove(_contactAddress);
                    //}
                    //Borra las relaciones con ContactEmail
                    foreach (ContactEmail _contactEmail in ContactEmails().Values)
                    {
                        this.Remove(_contactEmail);
                    }
                    //Borra las relaciones con ContactMessenger
                    foreach (ContactMessenger _contactMessenger in ContactMessengers().Values)
                    {
                        this.Remove(_contactMessenger);
                    }
                    //borra de seguridad
                    new Security.Collections.Rights(_Credential).Remove(this);
                }
            
        #endregion

        internal Person(Int64 idPerson, String idLanguage, String firstName, String lastName, String nickName, String posName, Int64 idSalutationType, Organization organization, KC.Entities.ResourceCatalog resourcePicture, Credential credential)
        {
            _Credential = credential;
            _IdLanguage = idLanguage;
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _IdPerson = idPerson;
            _FirstName = firstName;
            _LastName = lastName;
            _NickName = nickName;
            _PosName = posName;
            _IdSalutationType = idSalutationType;
            _Organization = organization;
            _ResourcePicture = resourcePicture;
        }

        public void Modify(SalutationType salutationType, String lastName, String firstName, String posName, String nickName, KC.Entities.ResourceCatalog resourcePicture)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Condesus.EMS.Business.DS.Collections.People(Organization).Modify(this, salutationType, lastName, firstName, posName, nickName, resourcePicture);
                _transactionScope.Complete();
            }
        }

        #region Security
        public void SecurityClassAdd(List<Security.Entities.RightEntity> rightEntity)
        {
            foreach (Security.Entities.RightEntity _item in rightEntity)
            {
                new Security.Collections.Rights(_Credential).Add(_item.Entity, this, _item.Permission);
            }
        }
        public void SecurityClassAdd(ISecurity Object, Security.Entities.Permission permission)
        {
            new Security.Collections.Rights(_Credential).Add(Object, this, permission);
        }
        public void SecurityJobTitleRemove(ISecurity Object, Security.Entities.Permission permission)
        {
            new Security.Collections.Rights(_Credential).Remove(this, Object, permission);
        }


        #region Read
        public List<Security.Entities.RightMapKC> RightMapKC()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightMapKC> _RightMapKC = new List<Security.Entities.RightMapKC>();

            _Rights = new Security.Collections.Rights(_Credential).ReadByPersonAndClassName(this, Common.Security.MapKC);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightMapKC.Add((Security.Entities.RightMapKC)_right);
            }
            return _RightMapKC;
        }
        public List<Security.Entities.RightMapDS> RightMapDS()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightMapDS> _RightMapDS = new List<Security.Entities.RightMapDS>();

            _Rights = new Security.Collections.Rights(_Credential).ReadByPersonAndClassName(this, Common.Security.MapDS);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightMapDS.Add((Security.Entities.RightMapDS)_right);
            }
            return _RightMapDS;
        }
        public List<Security.Entities.RightMapIA> RightMapIA()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightMapIA> _RightMapIA = new List<Security.Entities.RightMapIA>();

            _Rights = new Security.Collections.Rights(_Credential).ReadByPersonAndClassName(this, Common.Security.MapIA);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightMapIA.Add((Security.Entities.RightMapIA)_right);
            }
            return _RightMapIA;
        }
        public List<Security.Entities.RightMapRM> RightMapRM()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightMapRM> _RightMapRM = new List<Security.Entities.RightMapRM>();

            _Rights = new Security.Collections.Rights(_Credential).ReadByPersonAndClassName(this, Common.Security.MapRM);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightMapRM.Add((Security.Entities.RightMapRM)_right);
            }
            return _RightMapRM;
        }
        public List<Security.Entities.RightMapPA> RightMapPA()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightMapPA> _RightMapPA = new List<Security.Entities.RightMapPA>();

            _Rights = new Security.Collections.Rights(_Credential).ReadByPersonAndClassName(this, Common.Security.MapPA);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightMapPA.Add((Security.Entities.RightMapPA)_right);
            }
            return _RightMapPA;
        }
        public List<Security.Entities.RightMapPF> RightMapPF()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightMapPF> _RightMapPF = new List<Security.Entities.RightMapPF>();

            _Rights = new Security.Collections.Rights(_Credential).ReadByPersonAndClassName(this, Common.Security.MapPF);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightMapPF.Add((Security.Entities.RightMapPF)_right);
            }
            return _RightMapPF;
        }
        public List<Security.Entities.RightOrganization> RightOrganizations()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightOrganization> _RightOrganization = new List<Security.Entities.RightOrganization>();

            _Rights = new Security.Collections.Rights(_Credential).ReadByPersonAndClassName(this, Common.Security.Organization);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightOrganization.Add((Security.Entities.RightOrganization)_right);
            }
            return _RightOrganization;
        }
        public List<Security.Entities.RightProcess> RightProcesses()
        {
            List<Security.Entities.Right> _Rights = new List<Condesus.EMS.Business.Security.Entities.Right>();
            List<Security.Entities.RightProcess> _RightProcess = new List<Security.Entities.RightProcess>();

            _Rights = new Security.Collections.Rights(_Credential).ReadByPersonAndClassName(this, Common.Security.Process);

            foreach (Security.Entities.Right _right in _Rights)
            {
                _RightProcess.Add((Security.Entities.RightProcess)_right);
            }
            return _RightProcess;
        }

        #endregion
        #endregion
    }
}
