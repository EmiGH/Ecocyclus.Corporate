using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Entities
{
    public class InactiveTopic : Topic
    {
        //Borra dependencias
        internal override void Remove()
        {
            Dictionary<Int64, Entities.Message> _Messages = new Collections.Messages(this).Items();
            foreach (Message _item in _Messages.Values)
            {
                new Collections.Messages(Credential).Remove(_item);
            }
        }


        internal InactiveTopic(Int64 idTopic, Int64 idForum, Int64 idCategory, Int64 idPerson, DateTime postedDate, String title, Int64 maxAttachmentSize, Boolean allowResponses, Boolean isModerated, Credential credential) :
            base(idTopic, idForum, idCategory, idPerson, postedDate, title, maxAttachmentSize, allowResponses, isModerated, credential)
        { }

        public override void ChangeState()
        {
            new Collections.Topics(Credential).Update(this, true); 
        }
    }
}
