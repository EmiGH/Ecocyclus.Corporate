using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.CT.Entities
{
    public class NormalMessage : Message
    {
        #region Internal Properties
        Dictionary<Int64, Entities.Poll> _Polls;
        #endregion

        //Borra sus dependencias
        internal override void Remove()
        {
            foreach (Poll _poll in Polls.Values)
            {
                Remove(_poll);
            }
        }

        internal NormalMessage(Int64 idMessage, Int64 idTopic, Int64 idPerson, DateTime postedDate, DateTime lastEditedDate, String text, Credential credential) :
            base(idMessage,idTopic,idPerson,postedDate,lastEditedDate, text, credential)
        { }

        public override void ChangeType()
        {
            new Collections.Messages(Credential).Update(this, false);
        }

        #region Polls
        public Entities.Poll Poll(Int64 idPoll)
        {
            //Carga la coleccion de lenguages de es posicion
            return new Collections.Polls(this).Item(idPoll);
        }
        public Dictionary<Int64, Entities.Poll> Polls
        {
            get
            {
                if (_Polls == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _Polls = new Collections.Polls(this).Items();
                }
                return _Polls;
            }
        }
        public Poll pollAdd(Int16 value)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Poll _poll = new Collections.Polls(this).Create(this, value);
                _transactionScope.Complete();
                return _poll;
            }
        }
        internal void Remove(Poll poll)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.Polls(this).Delete(poll);
                _transactionScope.Complete();
            }
        }
        #endregion
    }
}
