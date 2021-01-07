using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class Parameters
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.ParameterGroup _ParameterGroup;
        #endregion

        internal Parameters(Credential credential)
        {
            _Credential = credential;
        }

        internal Parameters(Entities.ParameterGroup parameterGroup, Credential credential)
        {
            _ParameterGroup = parameterGroup;
            _Credential = credential;
        }

        #region Read Functions
        internal Entities.Parameter Item(Int64 idParameter)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.Parameter _parameter = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Parameters_ReadById(idParameter, _Credential.CurrentLanguage.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_parameter == null)
                {
                    _parameter = new Entities.Parameter(Convert.ToInt64(_dbRecord["IdParameter"]), Convert.ToInt64(_dbRecord["IdParameterGroup"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["Sign"]), Convert.ToBoolean(_dbRecord["RaiseException"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _parameter;
                    }
                }
                else
                {
                    return new Entities.Parameter( Convert.ToInt64(_dbRecord["IdParameter"]), Convert.ToInt64(_dbRecord["IdParameterGroup"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["Sign"]), Convert.ToBoolean(_dbRecord["RaiseException"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                }
            }
            return _parameter;
        }
        internal Dictionary<Int64, Entities.Parameter> Items()
        {
            //Coleccion para devolver los Parameter
            Dictionary<Int64, Entities.Parameter> _oItems = new Dictionary<Int64, Entities.Parameter>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Parameters_ReadAll(_ParameterGroup.IdParameterGroup, _Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdParameter"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdParameter"]));
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
                        Entities.Parameter _parameter = new Entities.Parameter(Convert.ToInt64(_dbRecord["IdParameter"]), Convert.ToInt64(_dbRecord["IdParameterGroup"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["Sign"]), Convert.ToBoolean(_dbRecord["RaiseException"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_parameter.IdParameter, _parameter);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.Parameter _parameter = new Entities.Parameter(Convert.ToInt64(_dbRecord["IdParameter"]), Convert.ToInt64(_dbRecord["IdParameterGroup"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["Sign"]), Convert.ToBoolean(_dbRecord["RaiseException"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_parameter.IdParameter, _parameter);
                }

            }
            return _oItems;
        }
      
        #endregion

        #region Write Functions
        internal Entities.Parameter Add(String description, String sign, Boolean raiseException)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                Int64 _idParameter = _dbPerformanceAssessments.Parameters_Create(_ParameterGroup.IdParameterGroup, _ParameterGroup.Indicator.IdIndicator, description, sign, raiseException, _Credential.DefaultLanguage.IdLanguage, _Credential.User.Person.IdPerson);
                //Devuelvo el objeto creado
                return new Entities.Parameter(_idParameter, _ParameterGroup.IdParameterGroup, description, sign, raiseException, _Credential.DefaultLanguage.IdLanguage,_Credential);
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
        internal void Remove(Entities.Parameter parameter)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                //Borra las dependencias
                parameter.Remove();
                //Borrar de la base de datos
                _dbPerformanceAssessments.Parameters_Delete(parameter.IdParameter, _Credential.User.Person.IdPerson);
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
        internal void Modify(Entities.Parameter parameter, String description, String sign, Boolean raiseException)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Modifico los datos de la base
                _dbPerformanceAssessments.Parameters_Update(parameter.IdParameter, _ParameterGroup.IdParameterGroup,_ParameterGroup.Indicator.IdIndicator,_Credential.DefaultLanguage.IdLanguage, description, sign, raiseException, _Credential.User.Person.IdPerson);
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
