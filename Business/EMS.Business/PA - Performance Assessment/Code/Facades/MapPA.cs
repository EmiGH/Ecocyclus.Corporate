using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Entities
{
    public class MapPA : ISecurity
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion
        #region External Properties
        internal Credential Credential
        {
            get { return _Credential; }
        }
        #endregion

        internal MapPA(Credential credential)
        {
            _Credential = credential;
        }

        #region IndicatorClassifications
        /// <summary>
        /// Devuelve una lista de Clasificaciones uso seguridad
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, IndicatorClassification> ClassificationsSecurity()
        {
            return new PA.Collections.IndicatorClassifications(_Credential).ItemsSecurity();
        }
        public Dictionary<Int64, PA.Entities.IndicatorClassification> IndicatorClassifications()
        {
            return  new PA.Collections.IndicatorClassifications(_Credential).Items(); 
        }
        public PA.Entities.IndicatorClassification IndicatorClassification(Int64 idIndicatorClassification)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.IndicatorClassifications(_Credential).Item(idIndicatorClassification);
        }
        /// <summary>
        /// Alta de un IndicatorClassification
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public PA.Entities.IndicatorClassification IndicatorClassificationAdd(Entities.IndicatorClassification parent, String name, String description)
        {
            if (parent == null)
            { return IndicatorClassificationAddWithOutParent(name, description); }
            else
            { return IndicatorClassificationAddWithParent(parent, name, description); }
        }
        internal PA.Entities.IndicatorClassification IndicatorClassificationAddWithParent(Entities.IndicatorClassification parent, String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                using (TransactionScope _transactionScope = new TransactionScope())
                {                    
                    PA.Entities.IndicatorClassification _indicatorClassification = new PA.Collections.IndicatorClassifications(parent, _Credential).Add(name, description);
                    _transactionScope.Complete();
                    return _indicatorClassification;
                }
        }
        internal PA.Entities.IndicatorClassification IndicatorClassificationAddWithOutParent(String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    PA.Entities.IndicatorClassification _indicatorClassification = new PA.Collections.IndicatorClassifications(_Credential).Add(name, description);
                    _transactionScope.Complete();
                    return _indicatorClassification;
                }
        }
        /// <summary>
        /// Baja de un IndicatorClassification
        /// </summary>
        /// <param name="indicatorClassification"></param>
        public void Remove(IndicatorClassification indicatorClassification)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
            {
                new PA.Collections.IndicatorClassifications(_Credential).Remove(indicatorClassification);
                _transactionScope.Complete();
            }
        }  
        #endregion

        #region Indicators
        /// <summary>
        /// Devuelve el indicador pedido
        /// </summary>
        /// <param name="idIndicator"></param>
        /// <returns></returns>
        public PA.Entities.Indicator Indicator(Int64 idIndicator)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.View);
            return new PA.Collections.Indicators(_Credential).Item(idIndicator);
        }
        /// <summary>
        /// ddevuelve todos los indicators
        /// </summary>
        /// <returns></returns>
        public Dictionary<Int64, Indicator> Indicators()
        {
            return new Collections.Indicators(_Credential).Items();
        }
        /// <summary>
        /// Devuelve los indicator con permiso y sin permiso en la clasificacion 
        /// y los indicator con permiso y sin clasificacion (Uso FE Tree)
        /// </summary>
        /// <returns></returns>
        public Dictionary<Int64, Indicator> IndicatorRoots()
        {
            return new Collections.Indicators(_Credential).ReadRoot();
        }
        /// <summary>
        /// Devuelve una lista de Indicadores, uso seguridad
        /// </summary>
        /// <returns></returns>
        internal Dictionary<Int64, PA.Entities.Indicator> ElementsSecurity()
        {
            return new PA.Collections.Indicators(_Credential).Items();
        }
        /// <summary>
        /// Alta de un Indicator
        /// </summary>
        /// <param name="magnitud"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="indicatorClassifications"></param>
        /// <returns></returns>
        public PA.Entities.Indicator IndicatorAdd(Entities.Magnitud magnitud, Boolean IsCumulative, String name, String description, String scope, String limitation, String definition, Dictionary<Int64, PA.Entities.IndicatorClassification> indicatorClassifications)
        {
            //Realiza las validaciones de autorizacion 
            //new Security.Authority(_Credential).Authorize(this.ClassName, this.IdObject, _Credential.User.IdPerson, Common.Permissions.Manage);
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                PA.Entities.Indicator _indicator = new PA.Collections.Indicators(_Credential).Add(magnitud, IsCumulative, name, description, scope, limitation, definition, indicatorClassifications);
                //Recorre para todas las clasificaciones e inserta una por una.
                _transactionScope.Complete();
                return _indicator;
            }
        }
        /// <summary>
        /// Baja de un Indicator
        /// </summary>
        /// <param name="indicator"></param>
        public void Remove(Entities.Indicator indicator)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new PA.Collections.Indicators(_Credential).Remove(indicator);
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
                return Common.Security.MapPA;
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
