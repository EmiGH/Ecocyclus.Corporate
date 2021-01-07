using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Condesus.EMS.Business;
using Condesus.EMS.DataAccess;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.NT.Collections
{
    internal class NotificationRecipients
    {
        #region Internal properties
        private Credential _Credential;
        private ICollectionItems _Datasource;
        #endregion

        internal NotificationRecipients(PF.Entities.ProcessTask processTask) 
        {
            _Credential = processTask.Credential;
            _Datasource = new NotificationRecipientRead.NotificationRecipientByTask(processTask);
        }
        internal NotificationRecipients(KC.Entities.Version version)
        {
            _Credential = version.Credential;
            _Datasource = new NotificationRecipientRead.NotificationRecipientByResource(version);
        }

        internal NotificationRecipients(PA.Entities.CalculateOfTransformation calculateOfTransformation)
        {
            _Credential = calculateOfTransformation.Credential;
            _Datasource = new NotificationRecipientRead.NotificationRecipientByTransformation(calculateOfTransformation);
        }

        #region Read Functions
        /// <summary>
        /// Retorna NotificationRecipient
        /// </summary>
        /// <returns></returns>
        internal List<Entities.NotificationRecipient> Items()
        {
            List<Entities.NotificationRecipient> _items = new List<Entities.NotificationRecipient>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();
            
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.NotificationRecipient _notificationRecipient = new NotificationRecipientFactory().CreateNotificationRecipient(Convert.ToString(_dbRecord["Email"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdContactEmail"],0)), _Credential);
                _items.Add(_notificationRecipient);
            }
            return _items;
        }

        //internal List<Entities.NotificationRecipient> Item()
        //{
        //    List<Entities.NotificationRecipient> _items = new List<Entities.NotificationRecipient>();

        //    IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

        //    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
        //    {
        //        Entities.NotificationRecipient _notificationRecipient = new NotificationRecipientFactory().CreateNotificationRecipient(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToString(_dbRecord["Email"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdPerson"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdOrganization"], 0)), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdContacEmail"], 0)), _Credential);
        //        _items.Add(_notificationRecipient);
        //    }
        //    return _items;
        //}
        #endregion


        #region Write Functions

        #region task
        public Entities.NotificationRecipientEmail Add(PF.Entities.ProcessTask task, String email)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailProcessTaskExternal_Create(task.IdProcess, email);

                return new Entities.NotificationRecipientEmail(email);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }     
        public void Remove(PF.Entities.ProcessTask task, String email)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailProcessTaskExternal_Delete(task.IdProcess, email);

            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        public void Update(PF.Entities.ProcessTask task, String email)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailProcessTaskExternal_Update(task.IdProcess, email);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        public void Remove(PF.Entities.ProcessTask task)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailProcessTaskExternal_Delete(task.IdProcess);
                _dbNotifications.MailProcessTaskPeople_Delete(task.IdProcess);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        public Entities.NotificationRecipientPerson Add(PF.Entities.ProcessTask task, DS.Entities.Person person, DS.Entities.ContactEmail contactEmail)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailProcessTaskPeople_Create(task.IdProcess, person.IdPerson, person.Organization.IdOrganization, contactEmail.IdContactEmail);

                return new Entities.NotificationRecipientPerson(person.IdPerson, person.Organization.IdOrganization, contactEmail.IdContactEmail, _Credential);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        public void Remove(PF.Entities.ProcessTask task, DS.Entities.Person person, DS.Entities.ContactEmail contactEmail)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailProcessTaskPeople_Delete(task.IdProcess, person.IdPerson, person.Organization.IdOrganization, contactEmail.IdContactEmail);

            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        public void Update(Entities.NotificationRecipientPerson notificationRecipientPerson, PF.Entities.ProcessTask task, DS.Entities.ContactEmail contactEmail)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailProcessTaskPeople_Update(task.IdProcess, notificationRecipientPerson.Person.IdPerson, notificationRecipientPerson.Person.Organization.IdOrganization, contactEmail.IdContactEmail);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        #endregion

        #region Transformation
        public Entities.NotificationRecipientEmail Add(PA.Entities.CalculateOfTransformation transformation, String email)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailTransformationExternal_Create(transformation.IdTransformation, email);

                return new Entities.NotificationRecipientEmail(email);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        public void Remove(PA.Entities.CalculateOfTransformation transformation, Entities.NotificationRecipientEmail notificationRecipientEmail)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailTransformationExternal_Delete(transformation.IdTransformation, notificationRecipientEmail.Email);

            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        public void Update(PA.Entities.CalculateOfTransformation transformation, Entities.NotificationRecipientEmail notificationRecipientEmail)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailTransformationExternal_Update(transformation.IdTransformation, notificationRecipientEmail.Email);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        public void Remove(PA.Entities.CalculateOfTransformation transformation)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailTransformationExternal_Delete(transformation.IdTransformation);
                _dbNotifications.MailTransformationPeople_Delete(transformation.IdTransformation);

            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        public Entities.NotificationRecipientPerson Add(PA.Entities.CalculateOfTransformation transformation, DS.Entities.Person person, DS.Entities.ContactEmail contactEmail)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailTransformationPeople_Create(transformation.IdTransformation, person.IdPerson, person.Organization.IdOrganization, contactEmail.IdContactEmail);

                return new Entities.NotificationRecipientPerson(person.IdPerson, person.Organization.IdOrganization, contactEmail.IdContactEmail, _Credential);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        public void Remove(PA.Entities.CalculateOfTransformation transformation,DS.Entities.Person person, DS.Entities.ContactEmail contactEmail)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailTransformationPeople_Delete(transformation.IdTransformation, person.IdPerson, person.Organization.IdOrganization, contactEmail.IdContactEmail);

            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        public void Update(Entities.NotificationRecipientPerson notificationRecipientPerson, PA.Entities.CalculateOfTransformation transformation, DS.Entities.ContactEmail contactEmail)
        {
            try
            {
                DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

                _dbNotifications.MailTransformationPeople_Update(transformation.IdTransformation, notificationRecipientPerson.Person.IdPerson, notificationRecipientPerson.Person.Organization.IdOrganization, contactEmail.IdContactEmail);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }
        #endregion

        #endregion

        

    }
}
