using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.DS.Entities
{
    public class PersonwithUser : Person
    {
        private List<Entities.Post> _Posts;   //puestos de la persona

        #region External Properties
   
        #region User
        public Entities.User User
        {
            get
            {
                return new Condesus.EMS.Business.DS.Collections.Users(Organization, Credential).Item(IdPerson);
            }
        }
        public void Remove(User user)
        {
            Collections.Users _users = new Condesus.EMS.Business.DS.Collections.Users(Organization, Credential);
            _users.Remove(user);
        }
        public Entities.User UsersModify(String userName, Boolean active, Boolean changePasswordOnNextLogin, Boolean cannotChangePassword, Boolean passwordNeverExpires, Boolean ViewGlobalMenu)
        {
            Collections.Users _users = new Condesus.EMS.Business.DS.Collections.Users(Organization, Credential);
            _users.Modify(IdPerson, userName, active, changePasswordOnNextLogin, cannotChangePassword, passwordNeverExpires, ViewGlobalMenu);
            return _users.Item(IdPerson);
        }
        public Entities.User UsersModify(String userName, String password)
        {
            Collections.Users _users = new Condesus.EMS.Business.DS.Collections.Users(Organization, Credential);
            _users.Modify(userName, password);
            return _users.Item(userName);
        }
        #endregion

        #region Posts
        public List<Entities.Post> Posts
        {
            get
            {
                if (_Posts == null)
                {
                    _Posts = new Condesus.EMS.Business.DS.Collections.Posts(this).Items();
                }
                return _Posts;
            }
        }
        public Entities.Post Post(JobTitle jobTitle)
        {
            return new Condesus.EMS.Business.DS.Collections.Posts(this).Item(this.IdPerson, jobTitle.IdOrganization, jobTitle.IdGeographicArea, jobTitle.IdFunctionalArea, jobTitle.IdPosition);
        }
        public Entities.Post PostsAdd(GIS.Entities.GeographicArea geographicArea, Entities.Position position, Entities.FunctionalArea functionalArea, DateTime startDate, DateTime endDate)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Collections.Posts _posts = new Condesus.EMS.Business.DS.Collections.Posts(this);
                Entities.Post _post =  _posts.Add(functionalArea, geographicArea, position, startDate, endDate);
                _transactionScope.Complete();
                return _post;
            }

        }
        public void Remove(Post post)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Collections.Posts _posts = new Condesus.EMS.Business.DS.Collections.Posts(this);
                _posts.Remove(post);
                _transactionScope.Complete();
            }
        }
        #endregion       

        #endregion

        internal override void  Remove()
        {
            base.Remove();

            //Borra los post
            foreach (Post _post in Posts)
            {
                this.Remove(_post);
            }
            //Borra las relaciones con contactURL
            this.Remove(User);
        }

        internal PersonwithUser(Int64 idPerson, String idLanguage, String firstName, String lastName, String nickName, String posName, Int64 idSalutationType, Organization organization, KC.Entities.ResourceCatalog resourcePicture, Credential credential) :
            base ( idPerson,  idLanguage,  firstName,  lastName,  nickName,  posName,  idSalutationType,  organization, resourcePicture, credential)
        {
            
        }

       
    }
}
