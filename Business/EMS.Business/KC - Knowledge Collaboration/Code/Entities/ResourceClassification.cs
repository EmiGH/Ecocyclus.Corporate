using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Entities
{
    public class ResourceClassification
    {
     #region Internal Properties
            private Credential _Credential;
            private Int64 _IdResourceClassification;
            private Int64 _IdParentResourceClassification;
            private Dictionary<Int64, Resource> _Resources;
            private Entities.ResourceClassification_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.ResourceClassifications_LG _LanguagesOptions; //Coleccion con los datos dependientes del idioma actual elegido por el usuario
            private Dictionary<Int64, Entities.ResourceClassification> _Children; //Coleccion de hijas
            private ResourceClassification _ParentResourceClassification;
        #endregion

        #region External Properties
            public Int64 IdResourceClassification
            {
                get { return _IdResourceClassification; }
            }
            public Int64 IdParentResourceClassification
            {
                get { return _IdParentResourceClassification; }
            }
            public ResourceClassification ParentResourceClassification
            {
                get
                {
                    if (_ParentResourceClassification == null)
                    { _ParentResourceClassification = new Collections.ResourceClassifications(_Credential).Item(_IdParentResourceClassification); }
                    return _ParentResourceClassification;
                }
            }

            public ResourceClassification_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Collections.ResourceClassifications_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de es posicion
                        _LanguagesOptions = new Collections.ResourceClassifications_LG(this, _Credential);
                    }

                    return _LanguagesOptions;
                }
            }

            public Dictionary<Int64, Entities.ResourceClassification> ChildrenClassifications
            {
                get
                {
                    if (_Children == null)
                    {
                        _Children = new Collections.ResourceClassifications(this, _Credential).Items();
                    }

                    return _Children;
                }
            }
            /// <summary>
            /// Borra las clasificaciones hijas 
            /// </summary>
            internal void Remove()
            {
                Collections.ResourceClassifications _resourceClassifications = new KC.Collections.ResourceClassifications(_Credential);
                foreach (ResourceClassification _resourceClassification in ChildrenClassifications.Values)
                {
                    _resourceClassifications.Remove(_resourceClassification);                    
                }
            }
            public Dictionary<Int64, Resource> ChildrenElements
            {
                get
                {
                    if (_Resources == null)
                    { _Resources = new Collections.Resources(_Credential).Items(this); }
                    return _Resources;
                }
            }
        #endregion
        
        internal ResourceClassification(Int64 idResourceClassification, Int64 idParentResourceClassification, String name, String description, Credential credential)
        {
            _Credential = credential;
            _IdResourceClassification = idResourceClassification;
            _IdParentResourceClassification = idParentResourceClassification;
           _LanguageOption = new ResourceClassification_LG(name, description, _Credential.CurrentLanguage.IdLanguage); 
        }

        internal ResourceClassification(Int64 idResourceClassification, Credential credential)
        {//construye por id            
            try
            {
                ResourceClassification _resourceClassification = new Collections.ResourceClassifications(_Credential).Item(idResourceClassification);
                _Credential = credential;
                _IdResourceClassification = idResourceClassification;
                _IdParentResourceClassification = _resourceClassification._IdParentResourceClassification;
                _LanguageOption = _resourceClassification._LanguageOption;
            }
            catch
            {
                throw new Exception(Common.Resources.Errors.IdCodeInvalid);
            }
        }

        /// <summary>
        /// Modifica el parent (para el drug&drop)
        /// </summary>
        /// <param name="parent"></param>
        public void Move(ResourceClassification parent)
        {
         
            //Manejo de la transaccion
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.ResourceClassifications(parent, _Credential).Modify(this);
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
                new Collections.ResourceClassifications(_Credential).Modify(this, name, description);
                _transactionScope.Complete();
            }
        }
     
    }
}
