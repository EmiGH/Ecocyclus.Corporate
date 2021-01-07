using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class ParameterGroups
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.Indicator _Indicator;
        #endregion

            internal ParameterGroups(Credential credential)
        {
            _Credential = credential;
        }

        internal ParameterGroups(Entities.Indicator indicator, Credential credential)
        {
            _Credential = credential;
            _Indicator = indicator;
        }

        #region Read Functions
        internal Entities.ParameterGroup Item(Int64 idParameterGroup)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.ParameterGroup _parameterGroup = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ParameterGroups_ReadById(idParameterGroup, _Credential.CurrentLanguage.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_parameterGroup == null)
                {
                    _parameterGroup = new Entities.ParameterGroup(Convert.ToInt64(_dbRecord["IdParameterGroup"]), Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _parameterGroup;
                    }
                }
                else
                {
                    return new Entities.ParameterGroup(Convert.ToInt64(_dbRecord["IdParameterGroup"]), Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                }
            }
            return _parameterGroup;
        }

        internal Dictionary<Int64, Entities.ParameterGroup> Items(Int64 idMeasurement)
        {
            //Coleccion para devolver los ParameterGroup
            Dictionary<Int64, Entities.ParameterGroup> _oItems = new Dictionary<Int64, Entities.ParameterGroup>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ParameterGroups_ReadByMeasurement(idMeasurement, _Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdParameterGroup"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdParameterGroup"]));
                    }
                    else
                    {
                        //No debe insertar en la coleccion ya que existe el idioma correcto.
                        _oInsert = false;
                    }

                    //Solo inserta si es necesario.
                    if (_oInsert)
                    {
                        //Declara e instancia  
                        Entities.ParameterGroup _parameterGroup = new Entities.ParameterGroup(Convert.ToInt64(_dbRecord["IdParameterGroup"]), Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_parameterGroup.IdParameterGroup, _parameterGroup);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.ParameterGroup _parameterGroup = new Entities.ParameterGroup(Convert.ToInt64(_dbRecord["IdParameterGroup"]), Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_parameterGroup.IdParameterGroup, _parameterGroup);
                }

            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.ParameterGroup> Items()
        {
            //Coleccion para devolver los ParameterGroup
            Dictionary<Int64, Entities.ParameterGroup> _oItems = new Dictionary<Int64, Entities.ParameterGroup>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.ParameterGroups_ReadAll(_Indicator.IdIndicator,_Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdParameterGroup"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdParameterGroup"]));
                    }
                    else
                    {
                        //No debe insertar en la coleccion ya que existe el idioma correcto.
                        _oInsert = false;
                    }

                    //Solo inserta si es necesario.
                    if (_oInsert)
                    {
                        //Declara e instancia  
                        Entities.ParameterGroup _parameterGroup = new Entities.ParameterGroup(Convert.ToInt64(_dbRecord["IdParameterGroup"]), Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_parameterGroup.IdParameterGroup, _parameterGroup);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.ParameterGroup _parameterGroup = new Entities.ParameterGroup(Convert.ToInt64(_dbRecord["IdParameterGroup"]), Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_parameterGroup.IdParameterGroup, _parameterGroup);
                }

            }
            return _oItems;
        }
      
        #endregion

        #region Write Functions
            internal Entities.ParameterGroup Add(String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                Int64 _idParameterGroup = _dbPerformanceAssessments.ParameterGroups_Create(_Indicator.IdIndicator, name, description, _Credential.DefaultLanguage.IdLanguage, _Credential.User.Person.IdPerson);
                //Devuelvo el objeto creado
                return new Entities.ParameterGroup(_idParameterGroup, _Indicator.IdIndicator, name, description, _Credential.DefaultLanguage.IdLanguage,_Credential);
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
            internal void Remove(Entities.ParameterGroup parameterGroup)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                //Borra todas las dependencias
                parameterGroup.Remove();
                //Borrar de la base de datos
                _dbPerformanceAssessments.ParameterGroups_Delete(parameterGroup.IdParameterGroup, _Credential.User.Person.IdPerson);
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
            internal void Remove(Entities.Indicator indicator)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //Borrar de la base de datos
                    _dbPerformanceAssessments.ParameterGroups_Delete(indicator.IdIndicator);
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
            internal void Modify(Entities.ParameterGroup parameterGroup, String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Modifico los datos de la base
                _dbPerformanceAssessments.ParameterGroups_Update(parameterGroup.IdParameterGroup, parameterGroup.Indicator.IdIndicator, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
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
