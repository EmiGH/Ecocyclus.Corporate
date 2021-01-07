using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
namespace Condesus.EMS.Business.CT.Entities
{
    class BannedMessage : Message
   {
       //Borra sus dependencias
       internal override void Remove()
       {
           Dictionary<Int64, Entities.Poll>  _Polls = new Collections.Polls(this).Items();
           foreach (Poll _poll in _Polls.Values)
           {
               new Collections.Polls(this).Delete(_poll);
           }
       }
       
        internal BannedMessage(Int64 idMessage, Int64 idTopic, Int64 idPerson, DateTime postedDate, DateTime lastEditedDate, String text, Credential credential) :
           base(idMessage, idTopic, idPerson, postedDate, lastEditedDate, text, credential)
       { }


       public override void ChangeType()
       {
           new Collections.Messages(Credential).Update(this,true);
       }

      

    }
}
