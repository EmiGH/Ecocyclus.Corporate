using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Entities
{
    public class RightPerson : Right
    {
        #region Internal Properties
        private DS.Entities.Person _Person;
        #endregion

        #region External Properties
        public DS.Entities.Person Person
        {
            get { return _Person; }
        }
        #endregion
        internal RightPerson(Permission permission, DS.Entities.Person person)
            : base(permission)
        {
            _Person = person;
        }
    }
}
