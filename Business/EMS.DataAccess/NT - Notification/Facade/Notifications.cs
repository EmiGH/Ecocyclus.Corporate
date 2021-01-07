using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Condesus.EMS.DataAccess.NT
{
    public class Notifications
    {
        public Notifications() { }

        # region Public Properties

        #region NotificationConfigurations

        #region Read Functions

        public IEnumerable<DbDataRecord> NotificationConfigurations_ReadById()
        {
            return new Entities.NotificationConfigurations().Read();
        }
        #endregion

        #region Write Functions
        public void NotificationConfigurations_Create(String emailSender, String host, String userName, String password, Double interval)
        {
            new Entities.NotificationConfigurations().Create(emailSender, host, userName,password,interval);
        }
        public void NotificationConfigurations_Update(String emailSender, String host, String userName, String password, Double interval)
        {
            new Entities.NotificationConfigurations().Update(emailSender, host, userName, password, interval);
        }
        #endregion

        #endregion

        #region NotificationRecipients

        #region Read Functions

        public IEnumerable<DbDataRecord> NotificationRecipients_ReadByProcess(Int64 IdProcess)
        {
            return new Entities.NotificationRecipients().ReadByProcess(IdProcess);
        }
        public IEnumerable<DbDataRecord> NotificationRecipients_ReadByTransformation(Int64 IdTransformation)
        {
            return new Entities.NotificationRecipients().ReadByTransformation(IdTransformation);
        }
        #endregion
        
        #endregion

        #region MailProcessTaskExternal

        #region Read Functions

        public IEnumerable<DbDataRecord> MailProcessTaskExternal_ReadByProcess(Int64 IdProcess)
        {
            return new Entities.MailProcessTaskExternal().ReadByProcess(IdProcess);
        }
        #endregion

        #region Write Functions

        public void MailProcessTaskExternal_Create(Int64 idProcess, String email)
        {
            new Entities.MailProcessTaskExternal().Create(idProcess,email);
        }
        public void MailProcessTaskExternal_Delete(Int64 idProcess)
        {
            new Entities.MailProcessTaskExternal().Delete(idProcess);
        }
        public void MailProcessTaskExternal_Delete(Int64 idProcess, String email)
        {
            new Entities.MailProcessTaskExternal().Delete(idProcess, email);
        }
        public void MailProcessTaskExternal_Update(Int64 idProcess, String email)
        {
            new Entities.MailProcessTaskExternal().Update(idProcess, email);
        }
        #endregion
        #endregion

        #region MailProcessTaskPeople

        #region Read Functions

        public IEnumerable<DbDataRecord> MailProcessTaskPeople_ReadByProcess(Int64 IdProcess)
        {
            return new Entities.MailProcessTaskPeople().ReadByProcess(IdProcess);
        }
        #endregion

        #region Write Functions

        public void MailProcessTaskPeople_Create(Int64 idProcess, Int64 idPerson, Int64 idOrganization, Int64 idContacEmail)
        {
            new Entities.MailProcessTaskPeople().Create(idProcess, idPerson,idOrganization,idContacEmail);
        }
        public void MailProcessTaskPeople_Delete(Int64 idProcess)
        {
            new Entities.MailProcessTaskPeople().Delete(idProcess);
        }
        public void MailProcessTaskPeople_Delete(Int64 idProcess, Int64 idPerson, Int64 idOrganization, Int64 idContacEmail)
        {
            new Entities.MailProcessTaskPeople().Delete(idProcess, idPerson, idOrganization, idContacEmail);
        }
        public void MailProcessTaskPeople_Update(Int64 idProcess, Int64 idPerson, Int64 idOrganization, Int64 idContacEmail)
        {
            new Entities.MailProcessTaskPeople().Update(idProcess, idPerson, idOrganization, idContacEmail);
        }
        #endregion

        #endregion

        #region MailTransformationExternal

        #region Read Functions

        public IEnumerable<DbDataRecord> MailTransformationExternal_ReadByProcess(Int64 IdTransformation)
        {
            return new Entities.MailTransformationExternal().ReadByTransformation(IdTransformation);
        }
        #endregion

        #region Write Functions

        public void MailTransformationExternal_Create(Int64 IdTransformation, String email)
        {
            new Entities.MailTransformationExternal().Create(IdTransformation, email);
        }
        public void MailTransformationExternal_Delete(Int64 IdTransformation)
        {
            new Entities.MailTransformationExternal().Delete(IdTransformation);
        }
        public void MailTransformationExternal_Delete(Int64 IdTransformation, String email)
        {
            new Entities.MailTransformationExternal().Delete(IdTransformation, email);
        }
        public void MailTransformationExternal_Update(Int64 IdTransformation, String email)
        {
            new Entities.MailTransformationExternal().Update(IdTransformation, email);
        }
        #endregion
        #endregion

        #region MailTransformationPeople

        #region Read Functions

        public IEnumerable<DbDataRecord> MailTransformationPeople_ReadByProcess(Int64 IdTransformation)
        {
            return new Entities.MailTransformationPeople().ReadByTransformation(IdTransformation);
        }
        #endregion

        #region Write Functions

        public void MailTransformationPeople_Create(Int64 IdTransformation, Int64 idPerson, Int64 idOrganization, Int64 idContacEmail)
        {
            new Entities.MailTransformationPeople().Create(IdTransformation, idPerson, idOrganization, idContacEmail);
        }
        public void MailTransformationPeople_Delete(Int64 IdTransformation)
        {
            new Entities.MailTransformationPeople().Delete(IdTransformation);
        }
        public void MailTransformationPeople_Delete(Int64 IdTransformation, Int64 idPerson, Int64 idOrganization, Int64 idContacEmail)
        {
            new Entities.MailTransformationPeople().Delete(IdTransformation, idPerson, idOrganization, idContacEmail);
        }
        public void MailTransformationPeople_Update(Int64 IdTransformation, Int64 idPerson, Int64 idOrganization, Int64 idContacEmail)
        {
            new Entities.MailTransformationPeople().Update(IdTransformation, idPerson, idOrganization, idContacEmail);
        }
        #endregion

        #endregion

        #endregion
    }
}
