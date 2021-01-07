using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.CT.Entities
{
    public class ActiveTopic :Topic
    {
        #region Internal Region
        private Dictionary<Int64, Entities.Message> _Messages; //Mensajes
        #endregion

        //Borra dependencias
        internal override void Remove()
        {
            foreach (Message _item in Messages.Values)
            {
                Remove(_item);                
            }
        }

        internal ActiveTopic(Int64 idTopic, Int64 idForum, Int64 idCategory, Int64 idPerson, DateTime postedDate, String title, Int64 maxAttachmentSize, Boolean allowResponses, Boolean isModerated, Credential credential) :
            base(idTopic, idForum, idCategory, idPerson, postedDate, title, maxAttachmentSize,  allowResponses,  isModerated,  credential)
        { }

        public override void ChangeState()
        {
            new Collections.Topics(this.Credential).Update(this, false);
        }

        #region Messages
        public Entities.Message Message(Int64 idMessage)
        {
            return new Collections.Messages(this).Item(idMessage);
        }
        public Dictionary<Int64, Entities.Message> Messages
        {
            get
            {
                if (_Messages == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _Messages = new Collections.Messages(this).Items();
                }
                return _Messages;
            }
        }
        public Condesus.EMS.Business.CT.Entities.Message MessageAdd(String messageText)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Message _message = new Collections.Messages(Credential).Create(this, DateTime.Now, DateTime.Now, messageText);
                _transactionScope.Complete();
                return _message;
            }
        }
        public void Remove(Message message)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.Messages(Credential).Remove(message);
                _transactionScope.Complete();
            }
        }

        #endregion
    }
}
