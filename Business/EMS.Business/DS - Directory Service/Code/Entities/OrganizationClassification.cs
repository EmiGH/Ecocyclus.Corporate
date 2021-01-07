using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    public class OrganizationClassification 
    {

        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdOrganizationClassification;
            private Int64 _IdParentOrganizationClassification;
            private Dictionary<Int64, Organization> _Organizations;
            private Entities.OrganizationClassification_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.OrganizationClassifications_LG _LanguagesOptions; //Coleccion con los datos dependientes del idioma actual elegido por el usuario
            private Dictionary<Int64, Entities.OrganizationClassification> _Children; //Coleccion de hijas
            private OrganizationClassification _Parent;
        #endregion

        #region External Properties
            public Int64 IdOrganizationClassification
            {
                get { return _IdOrganizationClassification; }
            }
            public Int64 IdParentOrganizationClassification
            {
                get { return _IdParentOrganizationClassification; }
            }
            public OrganizationClassification ParentOrganizationClassification
            {
                get
                {
                    if (_Parent == null)
                    { _Parent = new Collections.OrganizationClassifications(_Credential).Item(_IdParentOrganizationClassification); }
                    return _Parent;
                }
            }

            public OrganizationClassification_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Collections.OrganizationClassifications_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de es posicion
                        _LanguagesOptions = new Collections.OrganizationClassifications_LG(_IdOrganizationClassification, _Credential);
                    }

                    return _LanguagesOptions;
                }
            }

            public Dictionary<Int64, Entities.OrganizationClassification> ChildrenClassifications
            {
                get
                {
                    if (_Children == null)
                    {
                        _Children = new Collections.OrganizationClassifications(this , _Credential).Items();
                    }

                    return _Children;
                }
            }
            public Dictionary<Int64, Organization> ChildrenElements
            {
                get
                {
                    if (_Organizations == null)
                    { _Organizations = new Collections.Organizations(_Credential).Items(this); }
                    return _Organizations;
                }
            }
            /// <summary>
            /// Borra las clasificaciones hijas 
            /// </summary>
            internal void Remove()
            {
                Collections.OrganizationClassifications _organizationClassifications = new DS.Collections.OrganizationClassifications(_Credential);
                foreach (OrganizationClassification _organizationClassification in ChildrenClassifications.Values)
                {
                    _organizationClassifications.Remove(_organizationClassification);
                }
            }

        #endregion

        
        internal OrganizationClassification(Int64 idOrganizationClassification, Int64 idParentOrganizationClassification, String name, String description, Credential credential)
        {
            _Credential = credential;
            _IdOrganizationClassification = idOrganizationClassification;
            _IdParentOrganizationClassification = idParentOrganizationClassification;
           _LanguageOption = new OrganizationClassification_LG(name, description, _Credential.CurrentLanguage.IdLanguage); 
        }

     
        /// <summary>
        /// Modifica el parent (para el drug&drop)
        /// </summary>
        /// <param name="parent"></param>
        public void Move(OrganizationClassification parent)
        {
            //Manejo de la transaccion
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.OrganizationClassifications(parent, _Credential).Modify(this);
                _transactionScope.Complete();
            }
        }
        /// <summary>
        /// modifica solo el nombre y description
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public void Modify(String name, String description)
        {
             //Manejo de la transaccion
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.OrganizationClassifications(_Credential).Modify(this, name, description);
                _transactionScope.Complete();
            }
        }
      
   }

}
