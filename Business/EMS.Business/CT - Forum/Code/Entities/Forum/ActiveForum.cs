using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.CT.Entities
{
    public class ActiveForum : Forum
    {
        
        private Dictionary<Int64, Topic> _Topics;//puntero a Topic

        //Borra sus dependencias
        internal override void Remove()
        {
            foreach (Topic _item in Topics.Values)
            {
                Remove(_item);
            }

            base.Remove();
        }

        internal ActiveForum(Int64 idforum, String idLanguage, String name, String description, Credential credential) :
            base(idforum, idLanguage, name, description, credential)
        { }

        #region State
        public Boolean IsActive
        {
            get { return true; }
        }
        #endregion

        public override void ChangeState()
        {
            new Collections.Forums(Credential).Update(this, false);
        }
        
        #region Topics
        public Topic Topic(Int64 idTopic)
        {
            return new Collections.Topics(this).Item(idTopic);
        }

        public Dictionary<Int64, Topic> Topics
        {
            get
            {
                if (_Topics == null)
                {
                    //Carga la coleccion de lenguages de es posicion
                    _Topics = new Collections.Topics(this).Items();
                }

                return _Topics;
            }

        }

        public Topic TopicAdd(Category category, String title, Int64 maxAttachmentSize,Boolean allowResponses, Boolean isModerated)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {

                Topic _topic = new Collections.Topics(this).Create(this, category, title, maxAttachmentSize, allowResponses, isModerated);
                _transactionScope.Complete();
                return _topic;
            }
        }
        public void Remove(Topic topic)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.Topics(this).Delete(topic);
                _transactionScope.Complete();
            }
        }
        #endregion
        
    }
}
