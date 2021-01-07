using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.EP.Collections
{
    internal class ExtendedPropertyValues
    {
        #region Internal Properties
        private Credential _Credential;
        private IExtendedPropertiesCollection _Datasource;
        #endregion

        internal ExtendedPropertyValues(PF.Entities.Process process)
        {
            _Credential = process.Credential;
            _Datasource = new ExtendedPropertyValuesRead.ExtendedPropertiesValuesByProcess(process);
        }

        internal ExtendedPropertyValues(PA.Entities.Indicator indicator)
        {
            _Credential = indicator.Credential;
            _Datasource = new ExtendedPropertyValuesRead.ExtendedPropertyValuesByIndicators(indicator);
        }

        internal ExtendedPropertyValues(KC.Entities.Resource resource)
        {
            _Credential = resource.Credential;
            _Datasource = new ExtendedPropertyValuesRead.ExtendedPropertyValuesByResources(resource);
        }

        internal ExtendedPropertyValues(DS.Entities.Organization organization)
        {
            _Credential = organization.Credential;
            _Datasource = new ExtendedPropertyValuesRead.ExtendedPropertyValuesByOrganizations(organization);
        }

        internal ExtendedPropertyValues(PA.Entities.Formula formula)
        {
            _Credential = formula.Credential;
            _Datasource = new ExtendedPropertyValuesRead.ExtendedPropertiesValuesByFormula(formula);
        }

        internal ExtendedPropertyValues(PA.Entities.Calculation calculation)
        {
            _Credential = calculation.Credential;
            _Datasource = new ExtendedPropertyValuesRead.ExtendedPropertiesValuesByCalculate(calculation);
        }

        internal ExtendedPropertyValues(PA.Entities.ParameterGroup parameterGroup)
        {
            _Credential = parameterGroup.Credential;
            _Datasource = new ExtendedPropertyValuesRead.ExtendedPropertiesValuesByParameterGroup(parameterGroup);
        }

        internal ExtendedPropertyValues(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
        internal Entities.ExtendedPropertyValue Item(Int64 idExtendedProperty)
            {
                Entities.ExtendedPropertyValue _extendedPropertyValue = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItem(idExtendedProperty);
                
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    _extendedPropertyValue = new Condesus.EMS.Business.EP.Entities.ExtendedPropertyValue(Convert.ToInt64(_dbRecord["IdExtendedProperty"]), Convert.ToString(_dbRecord["Value"]), _Credential);
                }
                return _extendedPropertyValue;
            }

        internal List<Entities.ExtendedPropertyValue> Items()
        {
            List<Entities.ExtendedPropertyValue> _items = new List<Entities.ExtendedPropertyValue>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.ExtendedPropertyValue _extendedPropertyValue = new Entities.ExtendedPropertyValue(Convert.ToInt64(_dbRecord["IdExtendedProperty"]), Convert.ToString(_dbRecord["Value"]), _Credential);
                _items.Add(_extendedPropertyValue);
            }
            return _items;
        }

        #endregion

        #region Write Functions
        internal Entities.ExtendedPropertyValue Add(Entities.ExtendedProperty extendedProperty, String value)
        {
            return _Datasource.Add(extendedProperty, value);
        }
        internal void Remove(Entities.ExtendedPropertyValue extendedPropertyValue)
        {
            _Datasource.Remove(extendedPropertyValue);
        }
        internal void Modify(Entities.ExtendedPropertyValue extendedPropertyValue, String value)
        {
            _Datasource.Modify(extendedPropertyValue, value);
        }

        #endregion


    }
}
