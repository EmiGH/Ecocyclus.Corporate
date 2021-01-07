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
    internal class NotificationConfigurations
    {
        #region Internal properties
        private Credential _Credential;
        #endregion

        internal NotificationConfigurations(Credential credential)
        {
            _Credential = credential;
        }

        internal NotificationConfigurations() 
        {
        }

        #region Read Functions
        /// <summary>
        /// Retorna por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.NotificationConfiguration Item()
        {
            DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

            Entities.NotificationConfiguration _notificationConfiguration = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbNotifications.NotificationConfigurations_ReadById();

            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _notificationConfiguration = new Entities.NotificationConfiguration(Convert.ToString(_dbRecord["EmailSender"]), Convert.ToString(_dbRecord["Host"]), Convert.ToString(_dbRecord["UserName"]), Convert.ToString(_dbRecord["Password"]), Convert.ToDouble(_dbRecord["Interval"]));
            }
            return _notificationConfiguration;    
        }
        #endregion


        #region Write Functions
        //Crea ForumForums
        internal Entities.NotificationConfiguration Add(String emailSender, String host, String userName, String password, Double interval)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

            //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
            _dbNotifications.NotificationConfigurations_Create(emailSender, host , userName, password, interval );
            //crea el objeto 
            Entities.NotificationConfiguration _notificationConfiguration = new Entities.NotificationConfiguration(emailSender, host, userName, password, interval);

            //DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            //_dbLog.Create("NT_MailConfiguration", "NotificationConfiguration", "Add", "e=" + _idForum, _Credential.User.IdPerson);

            return _notificationConfiguration;

        }

        internal void Modify(String emailSender, String host, String userName, String password, Double interval)
        {
            DataAccess.NT.Notifications _dbNotifications = new Condesus.EMS.DataAccess.NT.Notifications();

            _dbNotifications.NotificationConfigurations_Update(emailSender, host, userName, password, interval);

            //DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            //_dbLog.Create("CT_ForumForums", "Forums", "Update", "IdForum=" + forum.IdForum, _Credential.User.IdPerson);

        }

        #endregion
    }
}
