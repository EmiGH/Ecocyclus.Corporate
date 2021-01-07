using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.KC.Entities
{
    public abstract class Version : CatalogFile
    {
        #region Internal Region
        private ResourceVersion _ResourceVersion;
        private DateTime _TimeStamp;
        private Int64 _IdPerson;
        private DateTime _ValidFrom;
        private DateTime _ValidThrough;
        private String _Version;
        private ResourceVersionHistory _FinallyResourceVersionHistory;
        private Dictionary<Int64, ResourceVersionHistory> _ResourceVersionHistories;
        private Dictionary<Int64, IA.Entities.Exception> _Exceptions;
        #endregion

        #region External Region
        public override Int64 IdResource
        {
            get { return _ResourceVersion.IdResource; }
        }
        public ResourceVersion ResourceVersion
        {
            get { return _ResourceVersion; }
        }
        public DateTime TimeStamp
        {
            get { return _TimeStamp; }
        }
        public Int64 IdPerson
        {
            get { return _IdPerson; }
        }
        public DateTime ValidFrom
        {
            get { return _ValidFrom; }
        }
        public DateTime ValidThrough
        {
            get { return _ValidThrough; }
        }
        public String VersionNumber
        {
            get { return _Version; }
        }

        #region Exceptions
        //mis exceptions
        public Dictionary<Int64, IA.Entities.Exception> Exceptions
        {
            get
            {
                if (_Exceptions == null)
                { _Exceptions = new IA.Collections.Exceptions(Credential).ItemsByResourceVersion(this); }
                return _Exceptions;
            }
        }


        #endregion

        #region Notifocation
        public List<NT.Entities.NotificationRecipient> NotificationRecipient
        {
            get
            {
                return new NT.Collections.NotificationRecipients(this).Items();
            }
        }

        //public NT.Entities.NotificationRecipientEmail NotificationRecipientAdd(String email)
        //{
        //    return new NT.Collections.NotificationRecipients(this).Add(this, email);
        //}
        //public void Remove(NT.Entities.NotificationRecipientEmail notificationRecipientEmail)
        //{
        //    new NT.Collections.NotificationRecipients(this).Remove(notificationRecipientEmail);
        //}
        //public NT.Entities.NotificationRecipientPerson NotificationRecipientPersonAdd(DS.Entities.Person person, DS.Entities.ContactEmail contactEmail)
        //{
        //    return new NT.Collections.NotificationRecipients(this).Add(this, person, contactEmail);
        //}
        //public void Remove(NT.Entities.NotificationRecipientPerson notificationRecipientPerson)
        //{
        //    new NT.Collections.NotificationRecipients(this).Remove(notificationRecipientPerson);
        //}

        #endregion

        #region Histories
            public ResourceVersionHistory FinallyResourceVersionHistory
            {
                get 
                {
                    if (_FinallyResourceVersionHistory == null)
                    { _FinallyResourceVersionHistory = new Collections.ResourceVersionHistories(this, Credential).Item(); }
                    return _FinallyResourceVersionHistory;
                }
            }
            public Dictionary<Int64, ResourceVersionHistory> ResourceVersionHistories
            { 
                get 
                {   
                    if(_ResourceVersionHistories == null)
                    { _ResourceVersionHistories = new Collections.ResourceVersionHistories(this, Credential).Items(); }
                    return _ResourceVersionHistories;
                }
            }
            public ResourceVersionHistory ResourceVersionHistoryAdd(ResourceHistoryState ResourceHistoryState, DateTime date, DS.Entities.Post post)
            {
                return new Collections.ResourceVersionHistories(this, Credential).Add(ResourceHistoryState, date, post);
            }
            
            public void Remove(ResourceHistoryState ResourceHistoryState)
            {
                new Collections.ResourceVersionHistories(this, Credential).Remove(ResourceHistoryState);
            }                       
            #endregion

        #endregion

            internal Version(Int64 idResourceFile, ResourceVersion resourceVersion, DateTime timeStamp, Int64 idPerson, DateTime validForm, DateTime validThrough, String version, Credential credential)
                : base(idResourceFile, credential)
        {
            _ResourceVersion = resourceVersion;
            _TimeStamp = timeStamp;
            _ValidFrom = validForm;
            _ValidThrough = validThrough;
            _IdPerson = idPerson;
            _Version = version;
            
        }

        //borra dependencias
            internal void Remove()
            {
                new Collections.ResourceVersionHistories(this, Credential).Remove();
                //borra excepciones
                foreach (IA.Entities.Exception _excep in this.Exceptions.Values)
                {
                    new IA.Collections.Exceptions(Credential).Remove(_excep);
                }
            }
    }
}
