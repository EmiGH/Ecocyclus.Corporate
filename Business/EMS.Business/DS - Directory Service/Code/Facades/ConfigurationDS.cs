using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.DS.Entities
{
    public class ConfigurationDS : ISecurity
    {
      

        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal ConfigurationDS(Credential credential)
        {
            _Credential = credential;
        }

       

        #region SalutationTypes
        public Dictionary<Int64, Entities.SalutationType> SalutationTypes()
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Collections.SalutationTypes(_Credential).Items();
        }
        public Entities.SalutationType SalutationType(Int64 idSalutationType)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Condesus.EMS.Business.DS.Collections.SalutationTypes(_Credential).Item(idSalutationType);
        }
        public Entities.SalutationType SalutationTypesAdd(String idLanguage, String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                Entities.SalutationType _salutationType = new Condesus.EMS.Business.DS.Collections.SalutationTypes(_Credential).Add(idLanguage, name, description);
                _transactionScope.Complete();
                return _salutationType;
            }
        }
        public void SalutationTypesRemove(Int64 idSalutationType)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                Collections.SalutationTypes _SalutationTypes = new Condesus.EMS.Business.DS.Collections.SalutationTypes(_Credential);
                _SalutationTypes.Remove(idSalutationType);
                _transactionScope.Complete();
            }
        }
        public Entities.SalutationType SalutationTypesModify(Int64 idSalutationType, String idLanguage, String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                Collections.SalutationTypes _SalutationTypes = new Condesus.EMS.Business.DS.Collections.SalutationTypes(_Credential);
                _SalutationTypes.Modify(idSalutationType, idLanguage, name, description);
                Entities.SalutationType _SalutationType = _SalutationTypes.Item(idSalutationType);
                _transactionScope.Complete();
                return _SalutationType;
            }
        }
        #endregion

        #region ApplicabilityContactTypes
        public Dictionary<Int64, Entities.ApplicabilityContactType> ApplicabilityContactTypes()
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Collections.ApplicabilityContactTypes(_Credential).Items();
        }
        public Entities.ApplicabilityContactType ApplicabilityContactType(Int64 idApplicabilityContactType)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Condesus.EMS.Business.DS.Collections.ApplicabilityContactTypes(_Credential).Item(idApplicabilityContactType);
        }
        public Entities.ApplicabilityContactType ApplicabilityContactType(String name)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Condesus.EMS.Business.DS.Collections.ApplicabilityContactTypes(_Credential).Item(name);
        }
        #endregion

        #region ContactMessengersApplications
        public Dictionary<String, Entities.ContactMessengerApplication> ContactMessengersApplications(String provider)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Collections.ContactMessengersApplications(provider, _Credential).Items();        
        }
        public Entities.ContactMessengerApplication ContactMessengerApplication(String provider, String application)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Condesus.EMS.Business.DS.Collections.ContactMessengersApplications(provider, _Credential).Item(application);
        }
        public Entities.ContactMessengerApplication ContactMessengersApplicationsAdd(String provider, String application)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                Collections.ContactMessengersApplications _contactMessengersApplications = new Condesus.EMS.Business.DS.Collections.ContactMessengersApplications(provider, _Credential);
                _contactMessengersApplications.Add(application); //Agrega una nueva aplicacion

                //Ahora instancia el objeto para despues agregarlo al dictionary
                Entities.ContactMessengerApplication _contactMessengerApplication = new Condesus.EMS.Business.DS.Entities.ContactMessengerApplication(provider, application);
                _transactionScope.Complete();
                return _contactMessengerApplication;
            }
        }
        public void ContactMessengersApplicationsRemove(String provider, String application)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                Collections.ContactMessengersApplications _contactMessengersApplications = new Condesus.EMS.Business.DS.Collections.ContactMessengersApplications(provider, _Credential);
                _contactMessengersApplications.Remove(application);
                _transactionScope.Complete();
            }
        }
        #endregion

        #region ContactMessengersProviders
        public Dictionary<String, Entities.ContactMessengerProvider> ContactMessengersProviders()
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Collections.ContactMessengersProviders(_Credential).Items();
   
        }
        public Entities.ContactMessengerProvider ContactMessengerProvider(String provider)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Condesus.EMS.Business.DS.Collections.ContactMessengersProviders(_Credential).Item(provider);
        }
        public Entities.ContactMessengerProvider ContactMessengersProvidersAdd(String provider)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            Collections.ContactMessengersProviders _contactMessengersProviders = new Condesus.EMS.Business.DS.Collections.ContactMessengersProviders(_Credential);
            _contactMessengersProviders.Add(provider);

            Entities.ContactMessengerProvider _contactMessengerProvider = new Condesus.EMS.Business.DS.Entities.ContactMessengerProvider(provider);
  
            return _contactMessengerProvider;
        }
        public void ContactMessengersProvidersRemove(String provider)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            Collections.ContactMessengersProviders _contactMessengerProvider = new Condesus.EMS.Business.DS.Collections.ContactMessengersProviders(_Credential);
            _contactMessengerProvider.Remove(provider);
          
        }
        #endregion

        #region ContactTypes
        public Dictionary<Int64, Entities.ContactType> ContactTypes(Int64 applicability)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Collections.ContactTypes(applicability, _Credential).Items();
   
        }  
        public Entities.ContactType ContactType(Int64 applicability, Int64 idContactType)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Condesus.EMS.Business.DS.Collections.ContactTypes(applicability, _Credential).Item(idContactType);
        }
        public Entities.ContactType ContactTypesAdd(Int64 applicability, String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            Entities.ContactType _contactType = new Condesus.EMS.Business.DS.Collections.ContactTypes(applicability, _Credential).Add(name, description);
            return _contactType;
        }
        public void ContactTypesRemove(Int64 applicability, Int64 idContactType)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            Collections.ContactTypes _contactType = new Condesus.EMS.Business.DS.Collections.ContactTypes(applicability, _Credential);
            _contactType.Remove(idContactType);
        }
        public Entities.ContactType ContactTypesModify(Int64 applicability, Int64 idContactType, String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            Collections.ContactTypes _contactType = new Condesus.EMS.Business.DS.Collections.ContactTypes(applicability, _Credential);
            _contactType.Modify(idContactType, name, description);

            return _contactType.Item(idContactType);
        }
        #endregion

        #region Languages
        public Entities.Language DefaultLanguage
        {
            get { return _Credential.DefaultLanguage; }
        }
        public Dictionary<String, Entities.Language> LanguagesEnabled()
        {
            return new Collections.Languages(_Credential).Items();       
        }
        public Dictionary<String, Entities.Language> Languages()
        {
            return new Collections.Languages(_Credential).AllItems();       
        }
        //me parece que no va, ya que es igual a Languages() (que esta arriba, y options es publica para todo la solucion)
        public static Dictionary<String, Entities.Language> LanguagesOptions()
        {
            return Collections.Languages.Options();
        }
        public Entities.Language Language(String idLanguage)
        {
            return new Condesus.EMS.Business.DS.Collections.Languages(_Credential).Item(idLanguage);
        }
        public Entities.Language LanguagesAdd(String idLanguage, String name, Boolean enable)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            Entities.Language _language = new Condesus.EMS.Business.DS.Collections.Languages(_Credential).Add(idLanguage, name, enable);
            return _language;
        }
        public void LanguagesRemove(String idLanguage)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            Collections.Languages _language = new Condesus.EMS.Business.DS.Collections.Languages(_Credential);
            _language.Remove(idLanguage);
        }
        public Entities.Language LanguagesModify(String idLanguage, String name, Boolean enable)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            Collections.Languages _language = new Condesus.EMS.Business.DS.Collections.Languages(_Credential);
            _language.Modify(idLanguage, name, enable);
            return _language.Item(idLanguage);
        }
        #endregion

        #region Organization Relationship Types
        public Dictionary<Int64, Entities.OrganizationRelationshipType> OrganizationsRelationshipsTypes()
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Collections.OrganizationsRelationshipsTypes(_Credential).Items();       
        }
        public Entities.OrganizationRelationshipType OrganizationRelationshipType(Int64 idOrganizationRelationshipType)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new Condesus.EMS.Business.DS.Collections.OrganizationsRelationshipsTypes(_Credential).Item(idOrganizationRelationshipType);
        }
        public Entities.OrganizationRelationshipType OrganizationsRelationshipsTypesAdd(String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            Entities.OrganizationRelationshipType _organizationRelationshipType = new Condesus.EMS.Business.DS.Collections.OrganizationsRelationshipsTypes(_Credential).Add(name, description);
            return _organizationRelationshipType;
        }
        public void OrganizationsRelationshipsTypesRemove(Int64 idOrganizationRelationshipType)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            Collections.OrganizationsRelationshipsTypes _organizationRelationshipType = new Condesus.EMS.Business.DS.Collections.OrganizationsRelationshipsTypes(_Credential);
            _organizationRelationshipType.Remove(idOrganizationRelationshipType);
        }
        public Entities.OrganizationRelationshipType OrganizationsRelationshipsTypesModify(Int64 idOrganizationRelationshipType, String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            Collections.OrganizationsRelationshipsTypes _organizationRelationshipType = new Condesus.EMS.Business.DS.Collections.OrganizationsRelationshipsTypes(_Credential);
            _organizationRelationshipType.Modify(idOrganizationRelationshipType, name, description);

            //if (_OrganizationsRelationshipsTypes != null)
            //{
            //    _OrganizationsRelationshipsTypes[idOrganizationRelationshipType] = _organizationRelationshipType.Item(idOrganizationRelationshipType);
            //}
            return _organizationRelationshipType.Item(idOrganizationRelationshipType);
        }
        #endregion

        #region Security 15-02-2010

        #region Properties
        public Int64 IdObject
        {
            get { return 0; }
        }
        public String ClassName
        {
            get
            {
                return Common.Security.ConfigurationDS;
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

        #endregion

    }

}
