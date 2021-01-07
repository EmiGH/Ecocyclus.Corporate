// Generated by Pnyx Generation tool at :06/04/2009 03:46:02 p.m.
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using Condesus.EMS.Business;
using Condesus.EMS.Business.Security;
using Condesus.EMS.DataAccess;

namespace Condesus.EMS.Business.CT.Collections
{
    internal class Messages
    {
        #region Internal Properties
        private Credential _Credential;
        private ICollectionItems _Datasource;
        #endregion

        internal Messages(Entities.Topic topic)
        {
            _Credential = topic.Credential;
            _Datasource = new MessagesRead.MessagesByTopic(topic);
        }

        internal Messages(DS.Entities.Person person)
        {
            _Credential = person.Credential;
            _Datasource = new MessagesRead.MessagesByPerson(person);
        }

        internal Messages(Credential credential)
        {
            _Credential = credential;
            _Datasource = new MessagesRead.MessagesAll(credential);
        }

        #region Read Functions
        internal Dictionary<Int64, Entities.Message> Items()
        {
            Dictionary<Int64, Entities.Message> _items = new Dictionary<Int64, Entities.Message>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Message _message = new Factory.FactoryMessage().CreateMessage(Convert.ToInt64(_dbRecord["IdMessage"]), Convert.ToInt64(_dbRecord["IdTopic"]), Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToDateTime(_dbRecord["PostedDate"]), Convert.ToDateTime(_dbRecord["LastEditedDate"]), Convert.ToString(_dbRecord["Text"]), Convert.ToBoolean(_dbRecord["IsNormal"]), _Credential);
                _items.Add(_message.IdMessage, _message);
            }
            return _items;
        }

        /// <summary>
        /// Retorna ForumMessages por ID
        /// </summary>
        /// <param name="IdMessage"></param>
        /// <returns></returns>
        internal Entities.Message Item(Int64 idMessage)
        {
            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();

            Entities.Message _message = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbCollaborationTools.ForumMessages_ReadById(idMessage);

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                _message = new Factory.FactoryMessage().CreateMessage(Convert.ToInt64(_dbRecord["IdMessage"]), Convert.ToInt64(_dbRecord["IdTopic"]), Convert.ToInt64(_dbRecord["IdPerson"]), Convert.ToDateTime(_dbRecord["PostedDate"]), Convert.ToDateTime(_dbRecord["LastEditedDate"]), Convert.ToString(_dbRecord["Text"]), Convert.ToBoolean(_dbRecord["IsNormal"]), _Credential);
            }

            return _message;
        }
        #endregion

        #region Write Functions
        //Crea ForumMessages
        internal Entities.Message Create(Entities.Topic topic, DateTime PostedDate, DateTime LastEditedDate, String Text)
        {
            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();

            Boolean _isNormal = true; //siempre q se da de alta es true
            Int64 _idMessage = _dbCollaborationTools.ForumMessages_Create(topic.IdTopic, _Credential.User.Person.IdPerson, PostedDate, LastEditedDate, Text, _isNormal);

            //Log
            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("CT_ForumMessages", "Messages", "Add", "IdMessage=" + _idMessage, _Credential.User.IdPerson);

            return new Factory.FactoryMessage().CreateMessage(_idMessage, topic.IdTopic, _Credential.User.Person.IdPerson, PostedDate, LastEditedDate, Text, _isNormal, _Credential);

        }
        internal void Remove(Entities.Message message)
        {
            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();
            //borra dependencias
            message.Remove();

            _dbCollaborationTools.ForumMessages_Delete(message.IdMessage);

            //Log
            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("CT_ForumMessages", "Messages", "Delete", "IdMessage=" + message.IdMessage, _Credential.User.IdPerson);

        }

        internal void Update(Entities.Message message, String text)
        {

            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();

            _dbCollaborationTools.ForumMessages_Update(message.IdMessage, DateTime.Now, text);

            //Log
            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("CT_ForumMessages", "Messages", "Update", "IdMessage=" + message.IdMessage, _Credential.User.IdPerson);

        }

        internal void Update(Entities.Message message, Boolean isNormal)
        {

            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();

            _dbCollaborationTools.ForumMessages_Update(message.IdMessage, isNormal);

            //Log
            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
            _dbLog.Create("CT_ForumMessages", "Messages", "Update", "IdMessage=" + message.IdMessage, _Credential.User.IdPerson);

        }

        #endregion

    }
}

