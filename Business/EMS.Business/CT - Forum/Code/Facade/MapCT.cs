using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.CT.Entities
{
    public class MapCT
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

         internal MapCT(Credential credential)
        {
            _Credential = credential;
        }

         #region Forum
         /// <summary>
         /// Foro q tiene asociado por default
         /// </summary>
         public CT.Entities.Forum Forum(Int64 idForum)
         {
             return new CT.Collections.Forums(_Credential).Item(idForum);
         }
         /// <summary>
         /// Todos los foros q tiene asociados
         /// </summary>
         public Dictionary<Int64, CT.Entities.Forum> Forums
         {
             get
             {
                 return new CT.Collections.Forums(_Credential).Items();
             }
         }

         public Forum ForumAdd(String name, String description)
         {
             using (TransactionScope _transactionScope = new TransactionScope())
             {
                 Forum _forum = new Collections.Forums(_Credential).Create(name, description);
                 _transactionScope.Complete();
                 return _forum;
             }
         }
         public void Remove(Forum forum)
         {
             using (TransactionScope _transactionScope = new TransactionScope())
             {
                 new Collections.Forums(_Credential).Delete(forum);
                 _transactionScope.Complete();
             }
         }
         #endregion
    }
}
