using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.NT.Entities
{
    public class NotificationRecipientPerson : NotificationRecipient
    {
        #region Internal Properties
        private Int64 _IdContactEmail;
        private Int64 _IdPerson;
        private Int64 _IdOrganization;
        private DS.Entities.Person _Person;
        private DS.Entities.ContactEmail _ContactEmail;
        private Credential _Credential;
        #endregion

        #region External Properties
        public override String Email
        { get { return ConctactEmail.Email; } }

        //public DS.Entities.Organization Organization
        //{
        //    get
        //    {
        //        return Person.Organization;
        //    }
        //}
        public DS.Entities.Organization Organization
        {
            get
            {
                return new DS.Collections.Organizations(_Credential).Item(_IdOrganization);
            }
        }
        public DS.Entities.Person Person
        {
            get
            {
                if(_Person==null)
                { _Person = new DS.Collections.People(Organization).Item(_IdPerson);}
                return _Person;
            }
        }

        public DS.Entities.ContactEmail ConctactEmail
        {
            get
            {
                if (_ContactEmail == null)
                { _ContactEmail = new DS.Collections.ContactEmails(Person).Item(_IdContactEmail); }
                return _ContactEmail;
            }
        }
        #endregion

        internal NotificationRecipientPerson(Int64 idPerson, Int64 idOrganization, Int64 idContactEmail, Credential credential) 
            : base ()
        {
            _IdPerson = idPerson;
            _IdOrganization = idOrganization;
            _IdContactEmail = idContactEmail;
            _Credential = credential;
        }

        /// <summary>
        /// para uso del frontend
        /// </summary>
        /// <param name="person"></param>
        /// <param name="contactEmail"></param>
        /// <param name="credential"></param>
        public NotificationRecipientPerson(DS.Entities.Person person, DS.Entities.ContactEmail contactEmail)
            : base()
        {
            _IdPerson = person.IdPerson;
            _IdOrganization = person.Organization.IdOrganization;
            _IdContactEmail = contactEmail.IdContactEmail;
            _Credential = person.Credential;
        }
    }
}
