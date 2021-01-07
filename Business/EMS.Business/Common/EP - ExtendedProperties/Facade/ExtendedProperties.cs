using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.EP
{
    public class ExtendedProperties
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal ExtendedProperties(Credential credential) 
        { 
            _Credential = credential; 
        }

        #region ExtendedPropertyClassifications
        public Dictionary<Int64, EP.Entities.ExtendedPropertyClassification> ExtendedPropertyClassifications()
        {
            return new EP.Collections.ExtendedPropertyClassifications(_Credential).Items();
        }
        public EP.Entities.ExtendedPropertyClassification ExtendedPropertyClassification(Int64 idExtendedPropertyClassification)
        {
            return new EP.Collections.ExtendedPropertyClassifications(_Credential).Item(idExtendedPropertyClassification);
        }
        public EP.Entities.ExtendedPropertyClassification ExtendedPropertyClassificationAdd(String name, String description)
        {
            return new EP.Collections.ExtendedPropertyClassifications(_Credential).Add(name, description);
        }
        public void ExtendedPropertyClassificationRemove(Int64 idExtendedPropertyClassification)
        {
            new EP.Collections.ExtendedPropertyClassifications(_Credential).Remove(idExtendedPropertyClassification);
        }
        #endregion

        #region ExtendedProperties
        public Dictionary<Int64, EP.Entities.ExtendedProperty> ExtendedPropertyes()
        {
            return new EP.Collections.ExtendedProperties(_Credential).Items();
        }
        public EP.Entities.ExtendedProperty ExtendedProperty(Int64 idExtendedProperty)
        {
            return new EP.Collections.ExtendedProperties(_Credential).Item(idExtendedProperty);
        }
        public EP.Entities.ExtendedProperty ExtendedPropertyAdd(Int64 idExtendedPropertyClasification, String name, String description)
        {
            return new EP.Collections.ExtendedProperties(_Credential).Add(idExtendedPropertyClasification, name, description);
        }
        public void ExtendedPropertyRemove(Int64 idExtendedProperty)
        {
            new EP.Collections.ExtendedProperties(_Credential).Remove(idExtendedProperty);
        }
        #endregion
    }
}
