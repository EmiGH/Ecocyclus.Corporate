using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PF.Entities
{
    public class MapPF : ISecurity
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal MapPF(Credential credential)
        {
            _Credential = credential;
        }

        #region ProcessClassifications
        /// <summary>
        /// Devuelve una lista de clasificaciones
        /// </summary>
        /// <returns></returns>
        public Dictionary<Int64, PF.Entities.ProcessClassification> ProcessClassifications()
        {
            return new PF.Collections.ProcessClassifications(_Credential).Items(); 
        }
        /// <summary>
        /// Devuelve una lista de Clasificaciones uso seguridad
        /// </summary>
        /// <returns></returns>
        //internal Dictionary<Int64, ProcessClassification> ClassificationsSecurity()
        //{
        //    return new PF.Collections.ProcessClassifications(_Credential).ItemsSecurity();
        //}
        /// <summary>
        /// Decuelve la clasificacion pedida
        /// </summary>
        /// <param name="idProcessClassification"></param>
        /// <returns></returns>
        public PF.Entities.ProcessClassification ProcessClassification(Int64 idProcessClassification)
        {            
            return new PF.Collections.ProcessClassifications(_Credential).Item(idProcessClassification);
        }
        /// <summary>
        /// Alta de una clasificacion
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public PF.Entities.ProcessClassification ProcessClassificationAdd(ProcessClassification parent, String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
        
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                PF.Entities.ProcessClassification _processClassification = new PF.Collections.ProcessClassifications(parent, _Credential).Add(parent, name, description);
                _transactionScope.Complete();
                return _processClassification;
            }
        
        }
        /// <summary>
        /// Borra una classificacion
        /// </summary>
        /// <param name="processClassification"></param>
        public void Remove(ProcessClassification processClassification)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            //Borra el clasification
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                new PF.Collections.ProcessClassifications(_Credential).Remove(processClassification);
                _transactionScope.Complete();
            }
        }
       
        #endregion

        #region Process general
        public PF.Entities.Process Process(Int64 idProcess)
        {
            //TODO: agregar validaciones de permiso por ejecutor para las tareas y minimo view para tareas, nodos
            PF.Entities.Process _process = new PF.Collections.Processes(_Credential).Item(idProcess);
            if (_process != null)
            {
                if (_process.GetType().Name == "ProcessGroupProcess")
                {
                    //Realiza las validaciones de autorizacion 
                    new Security.Authority(_Credential).Authorize(((ProcessGroupProcess)_process).ClassName, ((ProcessGroupProcess)_process).IdObject, _Credential.User.IdPerson, Common.Permissions.View);
                }
            }
            return _process;
        }
        public String ProcessTitle(Int64 idProcess)
        {
            PF.Entities.Process_LG _process_lg = new PF.Collections.Processes_LG(idProcess, _Credential).Item(_Credential.CurrentLanguage.IdLanguage);
            
            return _process_lg.Title;
        }
        #endregion

        #region Process
        /// <summary>
        /// Devuelve una lista process group process , uso seguridad
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, PF.Entities.ProcessGroupProcess> ElementsSecurity()
        {
            return new PF.Collections.ProcessGroupProcesses(_Credential).Items();
        }
        /// <summary>
        /// Devuelve el process pedido
        /// </summary>
        /// <param name="idProcessGroupProcess"></param>
        /// <returns></returns>
        public PF.Entities.ProcessGroupProcess ProcessGroupProcess(Int64 idProcessGroupProcess)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(Common.Security.Process, idProcessGroupProcess, _Credential.User.IdPerson, Common.Permissions.View);
            return new PF.Collections.ProcessGroupProcesses(_Credential).Item(idProcessGroupProcess);
        }
        /// <summary>
        /// Devuelve los process con permiso y sin permiso en la clasificacion 
        /// y los process con permiso y sin clasificacion (Uso FE Tree)
        /// </summary>
        /// <returns></returns>
        public Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessGroupProcessRoots()
        {
            return new PF.Collections.ProcessGroupProcesses(_Credential).ReadRoot();
        }
        /// <summary>
        /// Devuelve todas los process que tienen una clasificacion, con permisos
        /// </summary>
        /// <param name="organizationClassification"></param>
        /// <returns></returns>
        public Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessGroupProcesses(Int64 idProcessClassification)
        {
            return new PF.Collections.ProcessGroupProcesses(_Credential).Items(idProcessClassification);
        }

        public Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessGroupProcessesByLayerAndType(Int64 idGeographicAreaParent, Int64 idProcessClassification)
        {
            return new PF.Collections.ProcessGroupProcesses(_Credential).Items(idGeographicAreaParent, idProcessClassification);
        }
        /// <summary>
        /// Devuelve todas los process que tienen el nombre o nombre corporativo de organizacion,y con permisos
        /// </summary>
        /// <param name="organizationClassification"></param>
        /// <returns></returns>
        public Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessGroupProcessesByOrganizationName(String name)
        {
            return new PF.Collections.ProcessGroupProcesses(_Credential).ItemsByOrganizationName(name);
        }
        /// <summary>
        /// Devuelve todas los process que ve una persona
        /// </summary>
        /// <param name="organizationClassification"></param>
        /// <returns></returns>
        public Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessGroupProcesses()
        {
            return new PF.Collections.ProcessGroupProcesses(_Credential).ItemsByPost(_Credential.User.Person);
        }
        /// <summary>
        /// Alta de un process
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="orderNumber"></param>
        /// <param name="title"></param>
        /// <param name="purpose"></param>
        /// <param name="description"></param>
        /// <param name="threshold"></param>
        /// <param name="identification"></param>
        /// <param name="currentCampaignStartDate"></param>
        /// <param name="idResourcePicture"></param>
        /// <param name="processClassification"></param>
        /// <returns></returns>
        public PF.Entities.ProcessGroupProcess ProcessGroupProcessAdd(Int16 weight, Int16 orderNumber, String title, String purpose, String description, short threshold, String identification, DateTime currentCampaignStartDate, KC.Entities.ResourceCatalog resourcePicture, String Coordinate, GIS.Entities.GeographicArea geographicArea, Dictionary<Int64, PF.Entities.ProcessClassification> processClassification, DS.Entities.Organization organization, String TwitterUser, String FacebookUser)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                PF.Entities.ProcessGroupProcess _processGroupProcess = new PF.Collections.ProcessGroupProcesses(_Credential).Add(weight, orderNumber, title, purpose, description, threshold, identification, currentCampaignStartDate, resourcePicture, Coordinate, geographicArea, organization, TwitterUser, FacebookUser, processClassification);
                _transactionScope.Complete();
                return (PF.Entities.ProcessGroupProcess)_processGroupProcess;
            }

        }
        /// <summary>
        /// Borra un proces Group Process y todo lo q depende de el
        /// </summary>
        /// <param name="processGroupProcess"></param>
        public void Remove(ProcessGroupProcess processGroupProcess)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                (new PF.Collections.ProcessGroupProcesses(_Credential)).Remove(processGroupProcess);
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
                return Common.Security.MapPF;
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
