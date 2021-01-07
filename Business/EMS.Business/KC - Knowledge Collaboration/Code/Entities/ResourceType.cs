using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.KC.Entities
{
    public class ResourceType
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdResourceType;
            private Int64 _IdParentResourceType;
            private ResourceType _ParentResourceType;
            private ResourceType_LG _LanguageOption;
            private Collections.ResourceTypes_LG _LanguagesOptions;
            private Dictionary<Int64, ResourceType> _Children;
        #endregion

        #region External Properties
            public Int64 IdResourceType
            {
                get { return _IdResourceType; }
            }
            public ResourceType ParentResourceType
            {
                get 
                {
                    if (_ParentResourceType == null && _IdParentResourceType == 0)
                    { _ParentResourceType = null; }//retornar un obj resourcetype cuando este el collection
                    else
                    { _ParentResourceType = new Collections.ResourceTypes(_Credential).Item(_IdParentResourceType); }
                    return _ParentResourceType; 
                }
            }
            public ResourceType_LG LanguageOption
            {
                get { return _LanguageOption; }             
            }
            public Collections.ResourceTypes_LG LanguagesOptions
            {
                get 
                {
                    if (_LanguagesOptions == null)
                    { _LanguagesOptions = new Collections.ResourceTypes_LG(this, _Credential); }
                    return _LanguagesOptions; 
                }
            }

            public Dictionary<Int64, ResourceType> Children
            {
                get 
                {
                    if (_Children==null)
                    { _Children = new Collections.ResourceTypes(_Credential, _IdResourceType).Items();}
                    return _Children;
                }
            }
        #endregion

        internal ResourceType(Int64 idResourceType, Int64 idParentResourceType, String name, String description, Credential credential) 
        {
            _Credential = credential;
            _IdResourceType = idResourceType;
            _IdParentResourceType = idParentResourceType;
            _LanguageOption = new ResourceType_LG(_Credential.CurrentLanguage.IdLanguage, name,description);
        }

        public void Modify(Int64 idParent, String name, String description)
        {
            //Realiza las validaciones de autorizacion 
            new Security.Authority(_Credential).Authorize(Common.Security.ConfigurationKC, 0, _Credential.User.IdPerson, Common.Permissions.Manage);
            new KC.Collections.ResourceTypes(_Credential).Modify(this, idParent, name, description);
        }

    }
}
