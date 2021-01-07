using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Factory
{
    internal class FactoryTopic
    {
         internal FactoryTopic() { }

         internal Entities.Topic CreateTopic(Int64 idTopic, Int64 idForum, Int64 idCategory, Int64 idPerson, DateTime postedDate, String title, Int64 maxAttachmentSize, Boolean allowResponses, Boolean isModerated, Boolean isActive, Credential credential)
        {
            if (isActive)
            {
                return new Entities.ActiveTopic(idTopic, idForum, idCategory,  idPerson,  postedDate,  title,  maxAttachmentSize,  allowResponses,  isModerated,  credential);
            }
            else
            {
                return new Entities.InactiveTopic(idTopic, idForum, idCategory, idPerson, postedDate, title, maxAttachmentSize, allowResponses, isModerated, credential);
            }
            
        }

    }
}
