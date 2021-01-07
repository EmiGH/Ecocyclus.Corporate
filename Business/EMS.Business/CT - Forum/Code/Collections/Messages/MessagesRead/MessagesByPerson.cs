using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Condesus.EMS.Business.CT.Collections.MessagesRead
{
    internal class MessagesByPerson : ICollectionItems
    {
        private DS.Entities.Person _Person;

        internal MessagesByPerson(DS.Entities.Person person)
        {
            _Person = person;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.CT.CollaborationTools _dbCollaborationTools = new Condesus.EMS.DataAccess.CT.CollaborationTools();

            return _dbCollaborationTools.ForumMessages_ReadByPerson(_Person.IdPerson);
        }
    }
}
