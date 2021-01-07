using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Entities
{
    public class MapKC : ISecurity
    {
        #region Internal Properties
        private Credential _Credential;       
        #endregion

        internal MapKC(Credential credential)
        {
            _Credential = credential;
        }

        #region ResourceClassifications   
        /// <summary>
        /// Devuelve una lista de Clasificaciones uso seguridad
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, ResourceClassification> ClassificationsSecurity()
        {
            return new KC.Collections.ResourceClassifications(_Credential).ItemsSecurity();
        }
        /// <summary>
        /// Listado de resource clasifications
        /// </summary>
        /// <returns></returns>
        public Dictionary<Int64, KC.Entities.ResourceClassification> ResourceClassifications()
        {            
            return new KC.Collections.ResourceClassifications(_Credential).Items();             
        }
        public KC.Entities.ResourceClassification ResourceClassification(Int64 idResourceClassification)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new KC.Collections.ResourceClassifications(_Credential).Item(idResourceClassification);
        }
        /// <summary>
        /// Alta de un ResourceClassification
        /// </summary>
        /// <param name="idParent"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public KC.Entities.ResourceClassification ResourceClassificationAdd(Entities.ResourceClassification parent, String name, String description)
        {
            if (parent == null)
            { return ResourceClassificationAddWithOutParent(name, description); }
            else
            { return ResourceClassificationAddWithParent(parent, name, description); }
        }
        private KC.Entities.ResourceClassification ResourceClassificationAddWithParent(Entities.ResourceClassification parent, String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {                      
                        KC.Entities.ResourceClassification _resourceClassification = new KC.Collections.ResourceClassifications(parent, _Credential).Add(name, description);
                        _transactionScope.Complete();
                        return _resourceClassification;
                    }
            
        }
        private KC.Entities.ResourceClassification ResourceClassificationAddWithOutParent(String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    KC.Entities.ResourceClassification _resourceClassification = new KC.Collections.ResourceClassifications(_Credential).Add(name, description);
                    _transactionScope.Complete();
                    return _resourceClassification;
                }
            
        }
        /// <summary>
        /// Baja de un ResourceClassification
        /// </summary>
        /// <param name="_resourceClassification"></param>
        public void Remove(ResourceClassification resourceClassification)
        {
            //Borra el resourceclasification
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                //Borra el resourceclasification
                new KC.Collections.ResourceClassifications(_Credential).Remove(resourceClassification);
                _transactionScope.Complete();
            }
        }
        
        #endregion

        #region Resource
        /// <summary>
        /// Devuelve el resouce pedido
        /// </summary>
        /// <param name="idResource"></param>
        /// <returns></returns>
        public KC.Entities.Resource Resource(Int64 idResource)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new KC.Collections.Resources(_Credential).Item(idResource);
        }
        /// <summary>
        /// Devuelve los resources con permiso y sin permiso en la clasificacion 
        /// y los resources con permiso y sin clasificacion (Uso FE Tree)
        /// </summary>
        /// <returns></returns>
        public Dictionary<Int64, KC.Entities.Resource> ResourceRoots()
        {
            return new KC.Collections.Resources(_Credential).ReadRoot();
        }
        /// <summary>
        /// devuelve todos los resources (uso seguridad)
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, KC.Entities.Resource> Resources()
        {
            return new KC.Collections.Resources(_Credential).Items(); 
        }
        /// <summary>
        /// Devuelve todos los resources para una clasificacion, con permisos
        /// </summary>
        /// <param name="resourceClassification"></param>
        /// <returns></returns>
        public Dictionary<Int64, KC.Entities.Resource> Resources(ResourceClassification resourceClassification)
        {
            return new KC.Collections.Resources(_Credential).Items(resourceClassification);             
        }
        /// <summary>
        /// Devuelve todos los resources de un typo
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public Dictionary<Int64, KC.Entities.Resource> Resources(KC.Entities.ResourceType resourceType)
        {
            return new KC.Collections.Resources(_Credential).Items(resourceType);            
        }

        /// <summary>
        /// Alta de un Resource
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        /// <param name="resourceClassifications"></param>
        /// <returns></returns>
        public KC.Entities.Resource ResourceAdd(KC.Entities.ResourceType resourceType, String title, String description, String type, Dictionary<Int64, Condesus.EMS.Business.KC.Entities.ResourceClassification> resourceClassifications)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Manejo de la transaccion
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                KC.Entities.Resource _resource = new KC.Collections.Resources(_Credential).Add(resourceType, title, description, 0, type, resourceClassifications);
                _transactionScope.Complete();
                return _resource;
            }
        }
        /// <summary>
        /// Baja de un resource
        /// </summary>
        /// <param name="resource"></param>
        public void Remove(Resource resource)
        {
            //Manejo de la transaccion
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new KC.Collections.Resources(_Credential).Remove(resource);
                _transactionScope.Complete();
            }
        }
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
                return Common.Security.MapKC;
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
