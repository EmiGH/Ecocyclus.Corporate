using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Entities
{
    public class InactiveForum : Forum
    {

        //Borra sus dependencias
        internal override void Remove()
        {
            Dictionary<Int64, Entities.Topic> _Topics = new Collections.Topics(this).Items();
            foreach (Topic _item in _Topics.Values)
            {
                new Collections.Topics(this).Delete(_item);
            }

            base.Remove();
        }

        internal InactiveForum(Int64 idforum, String idLanguage, String name, String description, Credential credential) :
            base(idforum, idLanguage, name, description, credential)
        { }

        #region State
        public Boolean IsActive
        {
            get { return false; }
        }
        #endregion

        public override void ChangeState()
        {
            new Collections.Forums(Credential).Update(this, true);
        }
    }
}
