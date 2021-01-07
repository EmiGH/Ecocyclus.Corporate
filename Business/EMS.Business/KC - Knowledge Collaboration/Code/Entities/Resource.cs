using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;
using System.Data.SqlClient;

namespace Condesus.EMS.Business.KC.Entities
{
    public abstract class Resource : IExtendedProperty
    {
        #region Internal Region
        private Int64 _IdResource;       
        private ResourceType _ResourceType;
        private Credential _Credential;
        private Resource_LG _LanguageOption;
        private Collections.Resources_LG _LanguagesOptions;
        private Dictionary<Int64, ResourceClassification> _ResourceClassifications;
        #endregion

        #region External Region
        public Int64 IdResource
        {
            get { return _IdResource; }
        }
        public Resource_LG LanguageOption
        { 
            get {return _LanguageOption; } 
        }
        internal Credential Credential
        {
            get { return _Credential; }
        }
        public Collections.Resources_LG LanguagesOptions
        {
            get
            {
                if (_LanguagesOptions == null)
                { _LanguagesOptions = new Collections.Resources_LG(this, _Credential); }
                return _LanguagesOptions;
            }
        }

        public ResourceType ResourceType
        {
            get
            {              
                return _ResourceType;
            }
        }

        internal virtual void RemoveAllFiles()
        {
            //Borra los extended properties
            foreach (EP.Entities.ExtendedPropertyValue _extendedPropertyValue in ExtendedPropertyValues)
            {
                this.Remove(_extendedPropertyValue);
            }

        }

        #region Process
        public Dictionary<Int64, PF.Entities.Process> ProcessesAssociated
        {
            get
            {
                return new PF.Collections.Processes(_Credential).Items(this);
            }
        }
        #endregion

        #region Classifications
        public Dictionary<Int64, ResourceClassification> Classifications
        {
            get
            {
                if (_ResourceClassifications == null)
                { _ResourceClassifications = new Collections.ResourceClassifications(_Credential).Items(this); }
                return _ResourceClassifications;
            }
        }
        #endregion

        #region Extended Properties
        private List<EP.Entities.ExtendedPropertyValue> _ExtendedPropertyValue; //puntero a extended properties          
        public List<EP.Entities.ExtendedPropertyValue> ExtendedPropertyValues
        {
            get
            {
                if (_ExtendedPropertyValue == null)
                { _ExtendedPropertyValue = new EP.Collections.ExtendedPropertyValues(this).Items(); }
                return _ExtendedPropertyValue;
            }
        }
        public EP.Entities.ExtendedPropertyValue ExtendedPropertyValue(Int64 idExtendedProperty)
        {
            return new EP.Collections.ExtendedPropertyValues(this).Item(idExtendedProperty);
        }
        public void ExtendedPropertyValueAdd(EP.Entities.ExtendedProperty extendedProperty, String value)
        {
            new EP.Collections.ExtendedPropertyValues(this).Add(extendedProperty, value);
        }
        public void Remove(EP.Entities.ExtendedPropertyValue extendedPropertyValue)
        {
            new EP.Collections.ExtendedPropertyValues(this).Remove(extendedPropertyValue);
        }
        public void ExtendedPropertyValueModify(EP.Entities.ExtendedPropertyValue extendedPropertyValue, String value)
        {
            new EP.Collections.ExtendedPropertyValues(this).Modify(extendedPropertyValue, value);
        }
        #endregion
        #endregion

        internal Resource(Int64 idResource, ResourceType resourceType, String title, String description, Credential credential)
        {
            _Credential = credential;
            _IdResource = idResource;
            _ResourceType = resourceType;
            _LanguageOption = new Resource_LG(_Credential.CurrentLanguage.IdLanguage, title, description);
        }

        internal Resource(Int64 idResource, Credential credential)
        {
            try
            {
                Resource _resource = new Collections.Resources(credential).Item(idResource);
                _Credential = credential;
                _IdResource = _resource.IdResource;
                _ResourceType = _resource.ResourceType;
                _LanguageOption = _resource.LanguageOption;
            }
            catch
            {
                throw new Exception(Common.Resources.Errors.IdCodeInvalid);
            }
        }

        public void Modify(Entities.ResourceType resourceType, String title, String description, Dictionary<Int64, Entities.ResourceClassification> resourceClassifications)
        {
            using (TransactionScope _transactionScope = new TransactionScope())
            {
                new Collections.Resources(_Credential).Modify(this, resourceType, title, description, resourceClassifications);
                _transactionScope.Complete();
            }
        }
        
        public void Modify(Int64 currentFile)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //Modifico los datos de la base
                    _dbKnowledgeCollaboration.Resources_UpdateCurrentFile(_IdResource, currentFile);
                    _transactionScope.Complete();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
                {
                    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
                }
                throw ex;
            }
        }

    }
}
