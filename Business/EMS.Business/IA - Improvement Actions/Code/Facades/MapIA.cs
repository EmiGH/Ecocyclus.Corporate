using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.IA.Entities
{
    public class MapIA : ISecurity
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion   

        internal MapIA(Credential credential)
        {
            _Credential = credential;
        }

        #region StrategyClassification
        #endregion

        #region Strategy
        #endregion

        #region Projects Classifications
        public Dictionary<Int64, IA.Entities.ProjectClassification> ProjectClassifications()
        {
            return new IA.Collections.ProjectClassifications(_Credential).Items(); 
        }
        public IA.Entities.ProjectClassification ProjectClassification(Int64 idProjectClassification)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new IA.Collections.ProjectClassifications(_Credential).Item(idProjectClassification);
        }
        public IA.Entities.ProjectClassification ProjectClassificationAdd(Entities.ProjectClassification parent, String name, String description)
        {
            if (parent == null)
            { return ProjectClassificationAddWithOutParent(name, description); }
            else
            { return ProjectClassificationAddWithParent(parent, name, description); }
        }
        internal IA.Entities.ProjectClassification ProjectClassificationAddWithParent(Entities.ProjectClassification parent, String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                {
                    IA.Entities.ProjectClassification _projectClassification = new IA.Collections.ProjectClassifications(parent, _Credential).Add(name, description);
                    _transactionScope.Complete();
                    return _projectClassification;
                }   
        }
        internal IA.Entities.ProjectClassification ProjectClassificationAddWithOutParent(String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //en caso de no tener permisos en ninguna, debe disparar una excepcion.(si hay al menos un permiso, debe hacerlo.)
                using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                {
                    IA.Entities.ProjectClassification _projectClassification = new IA.Collections.ProjectClassifications(_Credential).Add(name, description);
                    _transactionScope.Complete();
                    return _projectClassification;
                }            
        }
        public void Remove(Entities.ProjectClassification projectClassification)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Borra el clasification
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                new IA.Collections.ProjectClassifications(_Credential).Remove(projectClassification);
                _transactionScope.Complete();
            }
        }

        #endregion

        #region Project

        #endregion

        #region Security 15-02-2010

        #region Properties
        public Int64 IdObject
        {
            get { return 0; }
        }
        public String ClassName
        {
            get
            {
                return Common.Security.MapIA;
            }
        }
        #endregion

        #region Read
        #region Permissions
        internal Dictionary<Int64, Security.Entities.Permission> _Permissions;
        public Dictionary<Int64, Security.Entities.Permission> Permissions
        {
            get
            {
                if (_Permissions == null)
                { _Permissions = new Security.Collections.Permissions(_Credential).Items(this); }
                return _Permissions;
            }
        }
        #endregion

        //ALL
        public List<Security.Entities.RightPerson> SecurityPeople()
        {
            return new Security.Collections.Rights(_Credential).ReadPersonByObject(this);
        }

        public List<Security.Entities.RightJobTitle> SecurityJobTitles()
        {
            return new Security.Collections.Rights(_Credential).ReadJobTitleByObject(this);
        }
        //por ID
        public Security.Entities.RightJobTitle ReadJobTitleByID(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            return new Security.Collections.Rights(_Credential).ReadJobTitleByID(jobTitle, permission);
        }
        public Security.Entities.RightPerson ReadPersonByID(DS.Entities.Person person, Security.Entities.Permission permission)
        {
            return new Security.Collections.Rights(_Credential).ReadPersonByID(person, permission);
        }

        #endregion

        #region Write
        //Security Add
        public Security.Entities.RightPerson SecurityPersonAdd(DS.Entities.Person person, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Alta el permiso
            Security.Entities.RightPerson _rightPerson = new Security.Collections.Rights(_Credential).Add(this, person, permission);

            return _rightPerson;
        }
        public Security.Entities.RightJobTitle SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Alta el permiso
            Security.Entities.RightJobTitle _rightJobTitle = new Security.Collections.Rights(_Credential).Add(this, jobTitle, permission);

            return _rightJobTitle;
        }
        //Security Remove
        public void SecurityPersonRemove(DS.Entities.Person person, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Borra el permiso
            new Security.Collections.Rights(_Credential).Remove(person, this, permission);
        }
        public void SecurityJobTitleRemove(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Borra el permiso
            new Security.Collections.Rights(_Credential).Remove(jobTitle, this, permission);
        }
        //Security Modify
        public Security.Entities.RightPerson SecurityPersonModify(Security.Entities.RightPerson oldRightPerson, DS.Entities.Person person, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Se borra con sus herederos
            this.SecurityPersonRemove(person, oldRightPerson.Permission);
            //se da de alta el y sus herederos
            this.SecurityPersonAdd(person, permission);

            return new Condesus.EMS.Business.Security.Entities.RightPerson(permission, person);
        }
        public Security.Entities.RightJobTitle SecurityJobTitleModify(Security.Entities.RightJobTitle oldRightJobTitle, DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Se borra con sus herederos
            this.SecurityJobTitleRemove(jobTitle, oldRightJobTitle.Permission);
            //se da de alta el y sus herederos
            this.SecurityJobTitleAdd(jobTitle, permission);

            return new Condesus.EMS.Business.Security.Entities.RightJobTitle(permission, jobTitle);
        }
        #endregion

        #endregion

    }

    
}
