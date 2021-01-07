using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;
using System.Data.SqlClient;

namespace Condesus.EMS.Business.EP.Collections.ExtendedPropertyValuesRead
{
    internal class ExtendedPropertiesValuesByParameterGroup : IExtendedPropertiesCollection
    {
        private PA.Entities.ParameterGroup _ParameterGroup;
        private Credential _Credential;

        internal ExtendedPropertiesValuesByParameterGroup(PA.Entities.ParameterGroup parameterGroup)
        {
            _Credential= parameterGroup.Credential;
            _ParameterGroup = parameterGroup;
        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItem(Int64 idExtendedProperty)
        {
            DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

            return _dbExtendedProperties.ParameterGroupExtendedProperties_ReadById(_ParameterGroup.IdParameterGroup, idExtendedProperty);

        }

        public IEnumerable<System.Data.Common.DbDataRecord> getItems()
        {
            DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

            return _dbExtendedProperties.ParameterGroupExtendedProperties_ReadAll(_ParameterGroup.IdParameterGroup);

        }

        #region Write Functions
        public Entities.ExtendedPropertyValue Add(Entities.ExtendedProperty extendedProperty, String value)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                _dbExtendedProperties.ParameterGroupExtendedProperties_Create(extendedProperty.IdExtendedProperty, _ParameterGroup.IdParameterGroup, value);

                //Devuelvo el objeto FunctionalArea creado
                return new Entities.ExtendedPropertyValue(extendedProperty.IdExtendedProperty, value, _Credential);
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
        public void Remove(Entities.ExtendedPropertyValue extendedPropertyValue)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                //Borrar de la base de datos
                _dbExtendedProperties.ParameterGroupExtendedProperties_Delete(extendedPropertyValue.ExtendedProperty.IdExtendedProperty, _ParameterGroup.IdParameterGroup);
            }
            catch (SqlException ex)
            {
                if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
                }
                throw ex;
            }
        }
        public void Modify(Entities.ExtendedPropertyValue extendedPropertyValue, String value)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                //Modifico los datos de la base
                _dbExtendedProperties.ParameterGroupExtendedProperties_Update(extendedPropertyValue.ExtendedProperty.IdExtendedProperty, _ParameterGroup.IdParameterGroup, value);
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
        #endregion
    }
}
