using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Collections.TopicsRead
{
    internal class TopicsByPerson : ICollectionItems
    {
        private DS.Entities.Person _Person;

        internal TopicsByPerson(DS.Entities.Person person)
        {
            _Person = person;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();

            return _dbCollaborationTools.ForumTopics_ReadByPerson(_Person.IdPerson);
        }
    }
}
