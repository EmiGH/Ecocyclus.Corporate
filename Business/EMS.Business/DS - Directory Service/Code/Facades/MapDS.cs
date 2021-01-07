using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;
using System.Transactions;

namespace Condesus.EMS.Business.DS.Entities
{
    public class MapDS : ISecurity
    {

        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal MapDS(Credential credential)
        {
            _Credential = credential;
        }

        #region OrganizationClassification
        /// <summary>
        /// Devuelve una lista de Clasificaciones 
        /// </summary>
        /// <returns></returns>
        public Dictionary<Int64, OrganizationClassification> OrganizationClassifications()
        {
            return new DS.Collections.OrganizationClassifications(_Credential).Items();             
        }
        /// <summary>
        /// Devuelve una lista de Clasificaciones uso seguridad
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, OrganizationClassification> ClassificationsSecurity()
        {
            return new DS.Collections.OrganizationClassifications(_Credential).ItemsSecurity();
        }
        /// <summary>
        /// Devuelve una clasificacion pedida
        /// </summary>
        /// <param name="idOrganizationClassification"></param>
        /// <returns></returns>
        public DS.Entities.OrganizationClassification OrganizationClassification(Int64 idOrganizationClassification)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new DS.Collections.OrganizationClassifications(_Credential).Item(idOrganizationClassification);
        }
        /// <summary>
        /// Alta de una clasificacion para una organizacion
        /// </summary>
        /// <param name="idParent"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public DS.Entities.OrganizationClassification OrganizationClassificationAdd(OrganizationClassification parent, String name, String description)
        {
            if (parent == null)
            { return OrganizationClassificationAddWithOutParent(name, description); }
            else
            { return OrganizationClassificationAddWithParent(parent, name, description); }
        }
        internal DS.Entities.OrganizationClassification OrganizationClassificationAddWithParent(OrganizationClassification parent, String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                DS.Entities.OrganizationClassification _organizationClassification = new DS.Collections.OrganizationClassifications(parent, _Credential).Add(name, description);
                _transactionScope.Complete();
                return _organizationClassification;
            }
        }
        internal DS.Entities.OrganizationClassification OrganizationClassificationAddWithOutParent(String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                DS.Entities.OrganizationClassification _organizationClassification = new DS.Collections.OrganizationClassifications(_Credential).Add(name, description);
                _transactionScope.Complete();
                return _organizationClassification;
            }
        }
        /// <summary>
        /// Baja de una clasificacion para una organizacion
        /// </summary>
        /// <param name="organizationClassification"></param>
        public void Remove(OrganizationClassification organizationClassification)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Borra el organizationclasification
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                new DS.Collections.OrganizationClassifications(_Credential).Remove(organizationClassification);
                _transactionScope.Complete();
            }
        }
 
        #endregion

        #region Organizations
        /// <summary>
        /// Devuelve una lista de organizaciones, uso seguridad
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, Entities.Organization> ElementsSecurity()
        {
            return new Collections.Organizations(_Credential).Items();
        }
        /// <summary>
        /// Devuelve la organizacion pedida
        /// </summary>
        /// <param name="idOrganization"></param>
        /// <returns></returns>
        public Entities.Organization Organization(Int64 idOrganization)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(Common.Security.Organization, idOrganization, _Credential.User.IdPerson, Common.Permissions.View);
            return new Condesus.EMS.Business.DS.Collections.Organizations(_Credential).Item(idOrganization);
        }
        /// <summary>
        /// devuelve todas las organizaciones en las que tiene permiso el usr
        /// </summary>
        /// <param name="idOrganization"></param>
        /// <returns></returns>
        public Dictionary<Int64, Entities.Organization> Organizations()
        {
            return new Condesus.EMS.Business.DS.Collections.Organizations(_Credential).ReadAllByPerson();
        }
        /// <summary>
        /// Devuelve las organizaciones con permiso y sin permiso en la clasificacion 
        /// y las organizaciones con permiso y sin clasificacion (Uso FE Tree)
        /// </summary>
        /// <returns></returns>
        public Dictionary<Int64, DS.Entities.Organization> OrganizationRoots()
        {            
            return new DS.Collections.Organizations(_Credential).ReadRoot();
        }
        /// <summary>
        /// Devuelve todas las organizaciones que tiene una clasificacion, con permisos
        /// </summary>
        /// <param name="organizationClassification"></param>
        /// <returns></returns>
        public Dictionary<Int64, DS.Entities.Organization> Organizations(OrganizationClassification organizationClassification)
        {
            return new DS.Collections.Organizations(_Credential).Items(organizationClassification);
        }
        /// <summary>
        /// Alta de una Organizacion
        /// </summary>
        /// <param name="corparteName"></param>
        /// <param name="name"></param>
        /// <param name="fiscalIdentification"></param>
        /// <param name="organizationClassifications"></param>
        /// <returns></returns>
        public Entities.Organization OrganizationsAdd(String corparteName, String name, String fiscalIdentification, KC.Entities.ResourceCatalog resourcePicture, Dictionary<Int64, Entities.OrganizationClassification> organizationClassifications)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Manejo de la transaccion
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                Entities.Organization _organization = new Condesus.EMS.Business.DS.Collections.Organizations(_Credential).Add(corparteName, name, fiscalIdentification, resourcePicture, organizationClassifications);
                _transactionScope.Complete();
                return _organization;
            }
        }
        /// <summary>
        /// Borra una Organizacion
        /// </summary>
        /// <param name="organization"></param>
        public void Remove(Organization organization)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(organization.ClassName, organization.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Manejo de la transaccion
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Condesus.EMS.Business.DS.Collections.Organizations(_Credential).Remove(organization);
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
                return Common.Security.MapDS;
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
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Alta el permiso
            Security.Entities.RightPerson _rightPerson = new Security.Collections.Rights(_Credential).Add(this, person, permission);

            return _rightPerson;
        }
        public Security.Entities.RightJobTitle SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Alta el permiso
            Security.Entities.RightJobTitle _rightJobTitle = new Security.Collections.Rights(_Credential).Add(this, jobTitle, permission);

            return _rightJobTitle;
        }
        //Security Remove
        public void SecurityPersonRemove(DS.Entities.Person person, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Borra el permiso
            new Security.Collections.Rights(_Credential).Remove(person, this, permission);
        }
        public void SecurityJobTitleRemove(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Borra el permiso
            new Security.Collections.Rights(_Credential).Remove(jobTitle, this, permission);
        }
        //Security Modify
        public Security.Entities.RightPerson SecurityPersonModify(Security.Entities.RightPerson oldRightPerson, DS.Entities.Person person, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Se borra con sus herederos
            this.SecurityPersonRemove(person, oldRightPerson.Permission);
            //se da de alta el y sus herederos
            this.SecurityPersonAdd(person, permission);

            return new Condesus.EMS.Business.Security.Entities.RightPerson(permission, person);
        }
        public Security.Entities.RightJobTitle SecurityJobTitleModify(Security.Entities.RightJobTitle oldRightJobTitle, DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
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
