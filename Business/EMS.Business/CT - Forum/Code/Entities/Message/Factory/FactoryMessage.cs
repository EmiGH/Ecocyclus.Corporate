using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Factory
{
    public class FactoryMessage
     {
        internal FactoryMessage() { }

        internal Entities.Message CreateMessage(Int64 idMessage, Int64 idTopic, Int64 idPerson, DateTime postedDate, DateTime lastEditedDate, String text, Boolean isNormal, Credential credential)
        {
            if(isNormal)
            {
                return new Entities.NormalMessage(idMessage,idTopic,idPerson, postedDate, lastEditedDate, text, credential);
            }
            else
            {
                return new Entities.BannedMessage(idMessage, idTopic, idPerson, postedDate, lastEditedDate, text, credential);        
            }
            
        }
    }
}
