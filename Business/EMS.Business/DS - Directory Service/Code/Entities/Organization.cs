using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.DS.Entities
{
    public partial class Organization : ISecurityEntity, IExtendedProperty
    {
      

        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdOrganization;//Identificador de la organizacion
        private String _CorporateName;//Nombre de la organizacion
        private String _Name;//Nombre fantasia de la organizacion
        private String _FiscalIdentification;//Identificador fiscal de la organizacion (CUIT)
        private OrganizationClassification _Classification;
        private KC.Entities.ResourceCatalog _ResourcePicture;
        private Dictionary<Int64, GIS.Entities.GeographicArea> _GeographicAreas;
        #endregion

        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public Int64 IdOrganization
        {
            get { return _IdOrganization; }
        }
        public String CorporateName
        {
            get { return _CorporateName; }
        }
        public String Name
        {
            get { return _Name; }
        }
        public String FiscalIdentification
        {
            get { return _FiscalIdentification; }
        }
        public OrganizationClassification Classification(Int64 idOrganizationClassification)
        {
            if (_Classification == null)
            {
                _Classification = new Collections.OrganizationClassifications(_Credential).Item(idOrganizationClassification);
            }
            return _Classification;
        }
        public Dictionary<Int64, OrganizationClassification> Classifications
        {
            get
            {
                return new Collections.OrganizationClassifications(_Credential).Items(this);
            }
        }

        public Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessGroupProcess()
        {
            return new PF.Collections.ProcessGroupProcesses(_Credential).Items(this);
        }
        public Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessGroupProcess(PF.Entities.ProcessClassification clasification)
        {
            return new PF.Collections.ProcessGroupProcesses(_Credential).Items(clasification, this);
        }

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

        #region Extended Properties
        private List<EP.Entities.ExtendedPropertyValue> _ExtendedPropertyValue; //puntero a extended properties          
        public List<EP.Entities.ExtendedPropertyValue> ExtendedPropertyValues
        {
            get
            {
                if (_ExtendedPropertyValue == null)
                { _ExtendedPropertyValue = new EP.Collections.ExtendedPropertyValues(this).Items(); }
                return _ExtendedPropertyValue;
            }
        }
        public EP.Entities.ExtendedPropertyValue ExtendedPropertyValue(Int64 idExtendedProperty)
        {
            return new EP.Collections.ExtendedPropertyValues(this).Item(idExtendedProperty);
        }
        public void ExtendedPropertyValueAdd(EP.Entities.ExtendedProperty extendedProperty, String value)
        {
            new EP.Collections.ExtendedPropertyValues(this).Add(extendedProperty, value);
        }
        public void Remove(EP.Entities.ExtendedPropertyValue extendedPropertyValue)
        {
            new EP.Collections.ExtendedPropertyValues(this).Remove(extendedPropertyValue);
        }
        public void ExtendedPropertyValueModify(EP.Entities.ExtendedPropertyValue extendedPropertyValue, String value)
        {
            new EP.Collections.ExtendedPropertyValues(this).Modify(extendedPropertyValue, value);
        }
        #endregion
        #endregion


        internal Organization(Int64 idOrganization, String corporateName, String name, String fiscalIdentification, KC.Entities.ResourceCatalog resourcePicture, Credential credential)
        {
            _Credential = credential;
            //Aca directamente se asigna el valor a las propiedades sin pasar por la DB
            _IdOrganization = idOrganization;
            _CorporateName = corporateName;
            _Name = name;
            _FiscalIdentification = fiscalIdentification;
            _ResourcePicture = resourcePicture;
        }

        public Entities.Organization Modify(String corparteName, String name, String fiscalIdentification,  KC.Entities.ResourceCatalog resourcePicture, Dictionary<Int64, Entities.OrganizationClassification> organizationClassifications)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Borra el organizationclasification
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                new Collections.Organizations(_Credential).Modify(this, corparteName, name, fiscalIdentification, resourcePicture,organizationClassifications);
                _transactionScope.Complete();
            }
            _CorporateName = corparteName;
            _Name = name;
            _FiscalIdentification = fiscalIdentification;
            return this;
        }

        /// <summary>
        /// Borra todas sus dependencias
        /// </summary>
        internal void Remove()
        { 
            //Borra relaciones con clasifications
            foreach (OrganizationClassification _OrganizationClassification in Classifications.Values)
            {
                this.Remove(_OrganizationClassification);
            }
            //Borra los extended properties
            foreach (EP.Entities.ExtendedPropertyValue _extendedPropertyValue in ExtendedPropertyValues)
            {
                this.Remove(_extendedPropertyValue);
            }
            //Borra los organizationalChart
            foreach (OrganizationalChart _organizationalChart in OrganizationalCharts.Values)
            {
                this.Remove(_organizationalChart);
            }
            //Borra los organizationalChart
            foreach (OrganizationRelationship _organizationRelationship in OrganizationRelationships())
            {
                this.Remove(_organizationRelationship);
            }
            //Borra personas
            foreach (Person _person in People.Values)
            {
                new Collections.People(this).Remove(_person);
            }
            ////Borra jobtitles de la base
            foreach (JobTitle _jobtitle in JobTitles())
            {
                new Collections.JobTitles(this).Remove(_jobtitle);
            }
            //Borra los process participations
            foreach (PF.Entities.ProcessParticipation _ProcessParticipation in ProcessParticipations())
            {
                new PF.Collections.ProcessParticipations(_Credential,this).RemoveByOrganization();
            }

            //Borra las relaciones con contactURL
            foreach (ContactURL _contactURL in ContactURLs().Values)
            {
                this.Remove(_contactURL);
            }
         
            //Borra las relaciones lod facilities
            foreach (GIS.Entities.Facility _facility in this.Facilities.Values)
            {
                this.Remove(_facility);
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
            //Borra las FunctionalPosition
            foreach (FunctionalPosition _functionalPosition in FunctionalPositions())
            {
                this.Remove(_functionalPosition);
            }
            //Borra las GeographicFunctionalArea 
            foreach (GeographicFunctionalArea _geographicFunctionalArea in GeographicFunctionalAreas())
            {
                this.Remove(_geographicFunctionalArea);
            }
            //Borra las functionalAreas
            foreach (FunctionalArea _functionalArea in FunctionalAreas().Values)
            {
                this.Remove(_functionalArea);
            }
            //Borra las Position
            foreach (Position _position in Positions().Values)
            {
                this.Remove(_position);
            }
            //Borra las GeographicArea
            new GIS.Collections.GeographicAreas(_Credential).ModifyDeleteOrganization(this);
            


        }


        #region Security 15-02-2010

        #region Properties
        public Int64 IdObject
        {
            get { return IdOrganization; }
        }
        public String ClassName
        {
            get
            {
                return Common.Security.Organization;
            }
        }
        #endregion

        #region Read
        #region Permissions
        internal Dictionary<Int64, Security.Entities.Permission> _Permissions;
        public Dictionary<Int64, Security.Entities.Permission> Permissions
        {
            get
            {
                if (_Permissions == null)
                { _Permissions = new Security.Collections.Permissions(_Credential).Items(this); }
                return _Permissions;
            }
        }
        #endregion

        //ALL
        public List<Security.Entities.RightPerson> SecurityPeople()
        {
            return new Security.Collections.Rights(_Credential).ReadPersonByObject(this);
        }

        public List<Security.Entities.RightJobTitle> SecurityJobTitles()
        {
            return new Security.Collections.Rights(_Credential).ReadJobTitleByObject(this);
        }
        //por ID
        public Security.Entities.RightJobTitle ReadJobTitleByID(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            return new Security.Collections.Rights(_Credential).ReadJobTitleByID(jobTitle, permission);
        }
        public Security.Entities.RightPerson ReadPersonByID(DS.Entities.Person person, Security.Entities.Permission permission)
        {
            return new Security.Collections.Rights(_Credential).ReadPersonByID(person, permission);
        }

        #endregion

        #region Write
        //Security Add
        public Security.Entities.RightPerson SecurityPersonAdd(DS.Entities.Person person, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Alta el permiso
            Security.Entities.RightPerson _rightPerson = new Security.Collections.Rights(_Credential).Add(this, person, permission);

            return _rightPerson;
        }
        public Security.Entities.RightJobTitle SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Alta el permiso
            Security.Entities.RightJobTitle _rightJobTitle = new Security.Collections.Rights(_Credential).Add(this, jobTitle, permission);

            return _rightJobTitle;
        }
        //Security Remove
        public void SecurityPersonRemove(DS.Entities.Person person, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Borra el permiso
            new Security.Collections.Rights(_Credential).Remove(person, this, permission);
        }
        public void SecurityJobTitleRemove(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Borra el permiso
            new Security.Collections.Rights(_Credential).Remove(jobTitle, this, permission);
        }
        //Security Modify
        public Security.Entities.RightPerson SecurityPersonModify(Security.Entities.RightPerson oldRightPerson, DS.Entities.Person person, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Se borra con sus herederos
            this.SecurityPersonRemove(person, oldRightPerson.Permission);
            //se da de alta el y sus herederos
            this.SecurityPersonAdd(person, permission);

            return new Condesus.EMS.Business.Security.Entities.RightPerson(permission, person);
        }
        public Security.Entities.RightJobTitle SecurityJobTitleModify(Security.Entities.RightJobTitle oldRightJobTitle, DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Se borra con sus herederos
            this.SecurityJobTitleRemove(jobTitle, oldRightJobTitle.Permission);
            //se da de alta el y sus herederos
            this.SecurityJobTitleAdd(jobTitle, permission);

            return new Condesus.EMS.Business.Security.Entities.RightJobTitle(permission, jobTitle);
        }
        #endregion
        #region ISecurityEntity 15-02-2010
        //Security Remove
        public void SecurityPersonRemove()
        {//se usa cuando la baja se hace de este elemento
            new Security.Collections.Rights(Credential).Remove(this);
        }
        public void SecurityJobTitleRemove()
        {//se usa cuando la baja se hace de este elemento
            new Security.Collections.Rights(Credential).Remove(this);
        }
        #endregion
        #endregion     

        #region Asociated Classifications 
        /// <summary>
        /// Borra una classificacion asociada (uso desde la )
        /// </summary>
        /// <param name="classification"></param>
        public void Remove(Entities.OrganizationClassification classification)
        {
            new Collections.Organizations(_Credential).Remove(this, classification);
        }
        #endregion
        
        #region Facades External Properties Organization (DS)

        #region Contact Emails
        public Dictionary<Int64, Entities.ContactEmail> ContactEmails()
        {
            return new Collections.ContactEmails(this).Items();
        }
        public Entities.ContactEmail ContactEmail(Int64 idContactEmail)
        {
            return new Collections.ContactEmails(this).Item(idContactEmail);
        }
        public Entities.ContactEmail ContactEmailsAdd(String email, ContactType contactType)
        {
            return new Collections.ContactEmails(this).AddByOrganization(email, contactType);
        }
        public void Remove(ContactEmail contactEmail)
        {
            new Collections.ContactEmails(this).RemoveByOrganization(contactEmail);
        }
        public void ContactEmailsModify(ContactEmail contactEmail, String email, ContactType contactType)
        {
            new Collections.ContactEmails(this).ModifyByOrganization(contactEmail, email, contactType);
        }
        #endregion

        #region Contact Messengers
        public Dictionary<Int64, Entities.ContactMessenger> ContactMessengers()
        {
            return new Collections.ContactMessengers(this).Items();
        }
        public Entities.ContactMessenger ContactMessenger(Int64 idContactMessenger)
        {
            return new Collections.ContactMessengers(this).Item(idContactMessenger);
        }
        public Entities.ContactMessenger ContactMessengersAdd(String provider, String application, String data, ContactType contactType)
        {            
            return new Collections.ContactMessengers(this).AddByOrganization(provider, application, data, contactType);
        }
        public void Remove(ContactMessenger contactMessenger)
        {            
            new Collections.ContactMessengers(this).RemoveByOrganization(contactMessenger);
        }
        public void ContactMessengersModify(ContactMessenger contactMessenger, String provider, String application, String data, ContactType contactType)
        {
            new Collections.ContactMessengers(this).ModifyByOrganization(contactMessenger, provider, application, data, contactType);
        }
        #endregion

        #region Contact URLs
        public Dictionary<Int64, Entities.ContactURL> ContactURLs()
        {
            return new Collections.ContactURLs(this).Items();
        }
        public Entities.ContactURL ContactURL(Int64 idContactURL)
        {
            return new Collections.ContactURLs(this).Item(idContactURL);
        }
        public Entities.ContactURL ContactURLsAdd(String url, String name, String description, ContactType contactType)
        {
            return new Collections.ContactURLs(this).AddByOrganization(url, name, description, contactType);
        }
        public void Remove(ContactURL contactURL)
        {
            new Collections.ContactURLs(this).RemoveByOrganization(contactURL);
        }
        public void ContactURLsModify(ContactURL contactURL, String url, String name, String description, ContactType contactType)
        {
            new Collections.ContactURLs(this).ModifyByOrganization(contactURL, url, name, description, contactType);
        }
        #endregion

        #region Functional Areas
        /// <summary>
        /// Devuelve todas las functional Areas de esta Organizacion
        /// </summary>
        /// <returns></returns>
        public Dictionary<Int64, Entities.FunctionalArea> FunctionalAreas()
        {
            return new Collections.FunctionalAreas(this).Items();
        }
        /// <summary>
        /// Devuelve el Fuctional Area pedida
        /// </summary>
        /// <param name="idFunctionalArea"></param>
        /// <returns></returns>
        public Entities.FunctionalArea FunctionalArea(Int64 idFunctionalArea)
        {
            return new Collections.FunctionalAreas(this).Item(idFunctionalArea);
        }
        /// <summary>
        /// Alta de un functional area para esta organizacion
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="mnemo"></param>
        /// <returns></returns>
        public Entities.FunctionalArea FunctionalAreasAdd(FunctionalArea parent, String name, String mnemo)
        {
            if (parent == null)
            { return FunctionalAreasAddWithOutParent(name, mnemo); }
            else
            { return FunctionalAreasAddWithParent(parent, name, mnemo); }
        }
        internal Entities.FunctionalArea FunctionalAreasAddWithParent(FunctionalArea parent, String name, String mnemo)
        {
            return new Collections.FunctionalAreas(parent, this).Add(name, mnemo);
        }
        internal Entities.FunctionalArea FunctionalAreasAddWithOutParent(String name, String mnemo)
        {
            return new Collections.FunctionalAreas(this).Add(name, mnemo);
        }
        /// <summary>
        /// Borra un functional Area y sus functional areas dependientes
        /// </summary>
        /// <param name="functionalArea"></param>
        public void Remove(FunctionalArea functionalArea)
        {
            new Collections.FunctionalAreas(this).Remove(functionalArea);
        }
        /// <summary>
        /// Modifica un functional Area
        /// </summary>
        /// <param name="functionalArea"></param>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="mnemo"></param>
        public void FunctionalAreasModify(FunctionalArea functionalArea, FunctionalArea parent, String name, String mnemo)
        {
            if (parent == null)
            { FunctionalAreasModifyWithOutParent(functionalArea, name, mnemo); }
            else
            { FunctionalAreasModifyWithParent(functionalArea, parent, name, mnemo); }
        }
        internal void FunctionalAreasModifyWithParent(FunctionalArea functionalArea, FunctionalArea parent, String name, String mnemo)
        {
            new Collections.FunctionalAreas(parent, this).Modify(functionalArea, name, mnemo);
        }
        internal void FunctionalAreasModifyWithOutParent(FunctionalArea functionalArea,String name, String mnemo)
        {
            new Collections.FunctionalAreas(this).Modify(functionalArea, name, mnemo);
        }
        #endregion

        #region Functional Positions
        public List<Entities.FunctionalPosition> FunctionalPositions()
        {
            return new Collections.FunctionalPositions(this).Items();
        }
        public Entities.FunctionalPosition FunctionalPosition(Position position, FunctionalArea functionalArea)
        {
            return new Collections.FunctionalPositions(this).Item(position, functionalArea);
        }
        public Entities.FunctionalPosition FunctionalPositionsAdd(Position position, FunctionalArea functionalArea, FunctionalPosition parentFunctionalPosition)
        {
            Entities.FunctionalPosition _functionalPosition;
            if (parentFunctionalPosition == null)
            { _functionalPosition = this.FunctionalPositionsAddWithOutParent(position, functionalArea); }
            else
            { _functionalPosition = this.FunctionalPositionsAddWithParet(position, functionalArea, parentFunctionalPosition); }
            return _functionalPosition;
        }
        public Entities.FunctionalPosition FunctionalPositionsAddWithParet(Position position, FunctionalArea functionalArea, FunctionalPosition parentFunctionalPosition)
        {
            return new Collections.FunctionalPositions(parentFunctionalPosition, this).Add(position, functionalArea);
        }
        public Entities.FunctionalPosition FunctionalPositionsAddWithOutParent(Position position, FunctionalArea functionalArea)
        {
            return new Collections.FunctionalPositions(this).Add(position, functionalArea);
        }
        public void Remove(FunctionalPosition functionalPosition)
        {
            new Collections.FunctionalPositions(this).Remove(functionalPosition);
        }
        public void FunctionalPositionsModify(FunctionalPosition functionalPosition, FunctionalPosition parentFunctionalPosition)
        {
            if (parentFunctionalPosition == null)
            { this.FunctionalPositionsModifyWithOutParent(functionalPosition); }
            else
            { this.FunctionalPositionsModifyWithParent(functionalPosition, parentFunctionalPosition); }
        }
        public void FunctionalPositionsModifyWithParent(FunctionalPosition FunctionalPosition, FunctionalPosition parentFunctionalPosition)
        {
            new Collections.FunctionalPositions(parentFunctionalPosition,this).Modify(FunctionalPosition);
        }
        public void FunctionalPositionsModifyWithOutParent(FunctionalPosition FunctionalPosition)
        {
            new Collections.FunctionalPositions(this).Modify(FunctionalPosition);
        }
        #endregion        

        #region Geographic Functional Areas
        public List<Entities.GeographicFunctionalArea> GeographicFunctionalAreas()
        {
            return new Collections.GeographicFunctionalAreas(this).Items();
        }
        public Entities.GeographicFunctionalArea GeographicFunctionalArea(FunctionalArea functionalArea, GIS.Entities.GeographicArea geographicArea)
        {
            return new Collections.GeographicFunctionalAreas(this).Item(functionalArea, geographicArea);
        }
        public Entities.GeographicFunctionalArea GeographicFunctionalAreasAdd(FunctionalArea functionalArea, GIS.Entities.GeographicArea geographicArea, GeographicFunctionalArea parentGeographicFunctionalArea)
        {
            Entities.GeographicFunctionalArea _geographicFunctionalArea;
            if (parentGeographicFunctionalArea == null)
            { _geographicFunctionalArea = GeographicFunctionalAreasAddWithOutParent(functionalArea, geographicArea); }
            else
            { _geographicFunctionalArea = GeographicFunctionalAreasAddWithParent(functionalArea, geographicArea, parentGeographicFunctionalArea); }
            return _geographicFunctionalArea;
        }
        internal Entities.GeographicFunctionalArea GeographicFunctionalAreasAddWithParent(FunctionalArea functionalArea, GIS.Entities.GeographicArea geographicArea, GeographicFunctionalArea parentGeographicFunctionalArea)
        {
            return new Collections.GeographicFunctionalAreas(parentGeographicFunctionalArea, this).Add(functionalArea, geographicArea);
        }
        internal Entities.GeographicFunctionalArea GeographicFunctionalAreasAddWithOutParent(FunctionalArea functionalArea, GIS.Entities.GeographicArea geographicArea)
        {
            return new Collections.GeographicFunctionalAreas(this).Add(functionalArea, geographicArea);
        }
        public void Remove(GeographicFunctionalArea geographicFunctionalArea)
        {
            new Collections.GeographicFunctionalAreas(this).Remove(geographicFunctionalArea);
        }
        public void GeographicFunctionalAreasModify(GeographicFunctionalArea geographicFunctionalArea, GeographicFunctionalArea parentGeographicFunctionalArea)
        {
            if (parentGeographicFunctionalArea == null)
            { GeographicFunctionalAreasModifyWithOutParent(geographicFunctionalArea); }
            else
            { GeographicFunctionalAreasModifyWithParent(geographicFunctionalArea, parentGeographicFunctionalArea); }
        }
        internal void GeographicFunctionalAreasModifyWithParent(GeographicFunctionalArea geographicFunctionalArea, GeographicFunctionalArea parentGeographicFunctionalArea)
        {
            new Collections.GeographicFunctionalAreas(parentGeographicFunctionalArea,this).Modify(geographicFunctionalArea);
        }
        internal void GeographicFunctionalAreasModifyWithOutParent(GeographicFunctionalArea geographicFunctionalArea)
        {
            new Collections.GeographicFunctionalAreas(this).Modify(geographicFunctionalArea);
        }
        #endregion

        #region Organizations Relationships
        public List<Entities.OrganizationRelationship> OrganizationRelationships()
        {
            return new Collections.OrganizationsRelationships(this).Items();
        }
        //TODO:ver con ruben si se pede dar esto de construir asi
        public Entities.OrganizationRelationship OrganizationRelationship(Int64 idOrganization1, Int64 idOrganization2, Int64 idOrganizationRelationshipType)
        {
            return new Collections.OrganizationsRelationships(this).Item(idOrganization1, idOrganization2, idOrganizationRelationshipType);
        }
        public void OrganizationRelationshipsAdd(Organization organizationRelated, OrganizationRelationshipType organizationRelationshipType)
        {
            new Collections.OrganizationsRelationships(this).Add(organizationRelated, organizationRelationshipType);
        }
        public void Remove(OrganizationRelationship organizationRelationship)
        {
            new Collections.OrganizationsRelationships(this).Remove(organizationRelationship);
        }
        #endregion

        #region Organizational Charts
        public Dictionary<Int64, OrganizationalChart> OrganizationalCharts
        {
            get
            { return new Collections.OrganizationalCharts(this).Items(); }
        }
        public OrganizationalChart OrganizationalChart(Int64 idOrganizationalChart)
        {
            return new Collections.OrganizationalCharts(this).Item(idOrganizationalChart);
        }
        public OrganizationalChart OrganizationalChartAdd(String name, String description)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                OrganizationalChart _organizationalChart = new Collections.OrganizationalCharts(this).Add(name, description);
                _transactionScope.Complete();
                return _organizationalChart;
            }
        }
        public void Remove(OrganizationalChart organizationalChart)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.OrganizationalCharts(this).Remove(organizationalChart);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region Process Participations
        public List<PF.Entities.ProcessParticipation> ProcessParticipations()
        {
            return new Collections.Organizations(_Credential).ProcessParticipationsItems(this);
        }
        #endregion

        #region Positions
        public Dictionary<Int64, Entities.Position> Positions()
        {
            return new Collections.Positions(this).Items();
        }
        public Entities.Position Position(Int64 idPosition)
        {
            return new Collections.Positions(this).Item(idPosition);
        }
        public Entities.Position PositionsAdd(String name, String description)
        {
            return new Collections.Positions(this).Add(name, description);
        }
        public void Remove(Position position)
        {
            new Collections.Positions(this).Remove(position);
        }
        public void PositionsModify(Position position, String name, String description)
        {
            new Collections.Positions(this).Modify(position, name, description);
        }
        #endregion

        #region JobTitles
        public List<Entities.JobTitle> JobTitles()
        {
            return new Collections.JobTitles(this).Items();
        }
        public Entities.JobTitle JobTitle(Entities.GeographicFunctionalArea geographicFunctionalArea, Entities.FunctionalPosition functionalPosition)
        {
            return new Collections.JobTitles(this).Item(geographicFunctionalArea.IdOrganization, geographicFunctionalArea.IdGeographicArea, geographicFunctionalArea.IdFunctionalArea,functionalPosition.IdPosition);
        }
        /// <summary>
        /// Borra el JobTilte de la base de datos
        /// </summary>
        /// <param name="jobTitle"></param>
        public void Remove(JobTitle jobTitle)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.JobTitles(this).Remove(jobTitle);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region Posts
        public List<Entities.Post> Posts()
        {
            return new Collections.Posts(this).Items();
        }
        public Entities.Post Post(JobTitle jobTitle, Person person)
        {
            //Entities.Person _person = Person(idPerson); 
            return new Collections.Posts(this).Item(person.IdPerson, jobTitle.IdOrganization, jobTitle.IdGeographicArea, jobTitle.IdFunctionalArea, jobTitle.IdPosition);
        }
        #endregion

        #region Users
        /// <summary>
        /// Devuelve a traves del id de persona un usuario
        /// </summary>
        /// <param name="IdPerson"></param>
        /// <returns></returns>
        public Entities.User User (Int64 IdPerson)
        {
            return new Collections.Users(this, _Credential).Item(IdPerson);
        }
        /// <summary>
        /// Devuelve para esta organizacion la lista de usuarios que tiene
        /// </summary>
        public Dictionary<String, Entities.User> Users
        {
            get
            {
                return new Collections.Users(this, _Credential).Items();
            }
        }
        #endregion

        #region People
        public Dictionary<Int64, Entities.Person> People
        {
            get
            {
                return new Collections.People(this).Items();
            }
        }
        public Entities.Person Person(Int64 idPerson)
        {
            return new Collections.People(this).Item(idPerson);
        }

        public Entities.Person PeopleAdd(SalutationType salutationType, String lastName, String firstName, String posName, String nickName, KC.Entities.ResourceCatalog resourceCatalog)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Entities.Person _person = new Collections.People(this).Add(salutationType, lastName, firstName, posName, nickName, resourceCatalog);
                _transactionScope.Complete();
                return _person;
            }
        }
        public void Remove(Person person)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.People(this).Remove(person);
                _transactionScope.Complete();
            }
        }
        
        #endregion

        #endregion


        #region new

        #region Geographic Areas
        private Dictionary<Int64, GIS.Entities.GeographicArea> GeographicAreas
        {
            get
            {
                _GeographicAreas = new Dictionary<long, Condesus.EMS.Business.GIS.Entities.GeographicArea>();
                foreach (GIS.Entities.Facility _facility in Facilities.Values)
                {
                    foreach (GIS.Entities.AddressFacility _addressFacility in _facility.Addresses.Values)
                    {
                        if (!_GeographicAreas.ContainsValue(_addressFacility.GeographicArea))
                        { _GeographicAreas.Add(_addressFacility.GeographicArea.IdGeographicArea, _addressFacility.GeographicArea); }
                    }
                }
                return _GeographicAreas;
            }
        }
        #endregion

        #region Facilities
        public Dictionary<Int64, GIS.Entities.Facility> Facilities
        {
            get
            {
                return new GIS.Collections.Facilities(this).Facility();
            }
        }
        public GIS.Entities.Facility Facility(Int64 idFacility)
        {
            return (GIS.Entities.Facility)new GIS.Collections.Facilities(this).Item(idFacility);
        }
        public GIS.Entities.Facility FacilityAdd(String coordinate, String name, String description, KC.Entities.ResourceCatalog resourcePicture, GIS.Entities.FacilityType facilityType, GIS.Entities.GeographicArea geographicArea, Boolean active)
        {
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                GIS.Entities.Facility _facility = new GIS.Collections.Facilities(this).Add(this, coordinate, name, description, resourcePicture, facilityType, geographicArea, active);
                _transactionScope.Complete();
                return _facility;
            }
        }
        public void Remove(GIS.Entities.Facility facility)
        {
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                new GIS.Collections.Facilities(this).Remove(facility);
                _transactionScope.Complete();
            }
        }

        #endregion

        //#region Geographic Areas
        //public Dictionary<Int64, Entities.GeographicArea> GeographicAreas()
        //{
        //    return new Collections.GeographicAreas(this).Items();
        //}
        //public Entities.GeographicArea GeographicArea(Int64 idGeographicArea)
        //{
        //    return new Collections.GeographicAreas(this).Item(idGeographicArea);
        //}
        //public Entities.GeographicArea GeographicAreasAdd(GeographicArea parent, Boolean allowFacilities, String mnemo, String name, String description, KC.Entities.ResourceCatalog resourceCatalog)
        //{
        //    if (parent == null)
        //    { return GeographicAreasAddWithOutParent(allowFacilities, mnemo, name, description, resourceCatalog); }
        //    else
        //    { return GeographicAreasAddWithParent(parent, allowFacilities, mnemo, name, description, resourceCatalog); }
        //}
        //public Entities.GeographicArea GeographicAreasAddWithParent(GeographicArea parent, Boolean allowFacilities, String mnemo, String name, String description, KC.Entities.ResourceCatalog resourceCatalog)
        //{
        //    return new Collections.GeographicAreas(parent, this).Add(allowFacilities, mnemo, name, description, resourceCatalog);
        //}
        //public Entities.GeographicArea GeographicAreasAddWithOutParent(Boolean allowFacilities, String mnemo, String name, String description, KC.Entities.ResourceCatalog resourceCatalog)
        //{
        //    return new Collections.GeographicAreas(this).Add(allowFacilities, mnemo, name, description, resourceCatalog);
        //}
        //public void Remove(GeographicArea geographicArea)
        //{
        //    Collections.GeographicAreas _geographicAreas = new Collections.GeographicAreas(this);
        //    _geographicAreas.Remove(geographicArea);
        //}
        //public void GeographicAreasModify(GeographicArea geographicArea, GeographicArea parent, Boolean allowFacilities, String mnemo, String name, String description, KC.Entities.ResourceCatalog resourceCatalog)
        //{
        //    if (parent == null)
        //    { GeographicAreasModifyWithOutParent(geographicArea, allowFacilities, mnemo, name, description, resourceCatalog); }
        //    else
        //    { GeographicAreasModifyWithParent(geographicArea, parent, allowFacilities, mnemo, name, description, resourceCatalog); }
        //}
        //public void GeographicAreasModifyWithParent(GeographicArea geographicArea, GeographicArea parent, Boolean allowFacilities, String mnemo, String name, String description, KC.Entities.ResourceCatalog resourceCatalog)
        //{
        //    new Collections.GeographicAreas(parent, this).Modify(geographicArea, allowFacilities, mnemo, name, description, resourceCatalog);
        //}
        //public void GeographicAreasModifyWithOutParent(GeographicArea geographicArea, Boolean allowFacilities, String mnemo, String name, String description, KC.Entities.ResourceCatalog resourceCatalog)
        //{
        //    new Collections.GeographicAreas(this).Modify(geographicArea, allowFacilities, mnemo, name, description, resourceCatalog);
        //}
        //#endregion

        //#region Contact Addresses
        //public Dictionary<Int64, Entities.ContactAddress> ContactAddresses()
        //{
        //    return new Collections.ContactAddresses(this).Items();
        //}
        //public Entities.ContactAddress ContactAddress(Int64 idContactAddress)
        //{
        //    return new Collections.ContactAddresses(this).Item(idContactAddress);
        //}
        //public Entities.ContactAddress ContactAddressesAdd(String street, String number, String floor, String apartment, String zipCode, String city, String state, Entities.Country country, Entities.ContactType contactType)
        //{
        //    return new Collections.ContactAddresses(this).AddByOrganization(street, number, floor, apartment, zipCode, city, state, country, contactType);
        //}
        //public void Remove(ContactAddress contactAddress)
        //{
        //    new Collections.ContactAddresses(this).RemoveByOrganization(contactAddress);
        //}
        //public void ContactAddressesModify(ContactAddress contactAddress, String street, String number, String floor, String apartment, String zipCode, String city, String state, Entities.Country country, Entities.ContactType contactType)
        //{
        //    new Collections.ContactAddresses(this).ModifyByOrganization(contactAddress, street, number, floor, apartment, zipCode, city, state, country, contactType);
        //}
        //#endregion
        //#region Contact Telephones
        //public Dictionary<Int64, Entities.ContactTelephone> ContactTelephones()
        //{
        //    return new Collections.ContactTelephones(this).Items();
        //}
        //public Entities.ContactTelephone ContactTelephone(Int64 idContactTelephone)
        //{
        //    return new Collections.ContactTelephones(this).Item(idContactTelephone);
        //}
        //public Entities.ContactTelephone ContactTelephonesAdd(String areaCode, String number, String extension, Entities.Country country, Entities.ContactType contactType)
        //{
        //    return new Collections.ContactTelephones(this).AddByOrganization(areaCode, number, extension, country, contactType);
        //}
        //public void Remove(ContactTelephone contactTelephone)
        //{
        //    new Collections.ContactTelephones(this).RemoveByOrganization(contactTelephone);
        //}
        //public void ContactTelephonesModify(ContactTelephone contactTelephone, String areaCode, String number, String extension, Entities.Country country, Entities.ContactType contactType)
        //{
        //    new Collections.ContactTelephones(this).ModifyByOrganization(contactTelephone, areaCode, number, extension, country, contactType);
        //}
        //#endregion
        #endregion
    }
}
