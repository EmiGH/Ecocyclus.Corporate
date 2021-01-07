using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Collections
{
    public class CalculationScenarioTypes
    {
        #region Internal Properties
            private Credential _Credential;         
        #endregion

        internal CalculationScenarioTypes(Credential credential)
        {
            _Credential = credential;            
        }

        #region Read Functions
            //Trae el tipo pedido
            internal Entities.CalculationScenarioType Item(Int64 idScenarioType)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                Entities.CalculationScenarioType _ParticipationType = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationScenarioTypes_ReadById(idScenarioType, _Credential.CurrentLanguage.IdLanguage);
                //si no trae nada retorno 0 para que no de error
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {                
                    if (_ParticipationType == null)
                    {
                        _ParticipationType = new Entities.CalculationScenarioType(Convert.ToInt64(_dbRecord["IdScenarioType"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _ParticipationType;
                        }
                    }
                    else
                    {
                        return new Entities.CalculationScenarioType(Convert.ToInt64(_dbRecord["IdScenarioType"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                    }             
                }
                return _ParticipationType;
            }
            //Trae todos los tipos
            internal Dictionary<Int64, Entities.CalculationScenarioType> Items()
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();                

                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.CalculationScenarioType> _oItems = new Dictionary<Int64, Entities.CalculationScenarioType>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationScenarioTypes_ReadAll(_Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _bInsert = true;

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdScenarioType"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdScenarioType"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _bInsert = false;
                        }
                    }
                    //Solo inserta si es necesario.
                    if (_bInsert)
                    {
                        //Declara e instancia una posicion
                        Entities.CalculationScenarioType _calculationScenarioType = new Entities.CalculationScenarioType(Convert.ToInt64(_dbRecord["IdScenarioType"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                        //Lo agrego a la coleccion
                        _oItems.Add(_calculationScenarioType.IdScenarioType, _calculationScenarioType);
                    }
                    _bInsert = true;
                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.CalculationScenarioType> Items(Int64 idProcessClassification)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.CalculationScenarioType> _oItems = new Dictionary<Int64, Entities.CalculationScenarioType>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.CalculationScenarioTypes_ReadByClassification(idProcessClassification, _Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _bInsert = true;

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdScenarioType"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdScenarioType"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _bInsert = false;
                        }
                    }
                    //Solo inserta si es necesario.
                    if (_bInsert)
                    {
                        //Declara e instancia una posicion
                        Entities.CalculationScenarioType _calculationScenarioType = new Entities.CalculationScenarioType(Convert.ToInt64(_dbRecord["IdScenarioType"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                        //Lo agrego a la coleccion
                        _oItems.Add(_calculationScenarioType.IdScenarioType, _calculationScenarioType);
                    }
                    _bInsert = true;
                }
                return _oItems;
            }
        #endregion

        #region Write Functions
            internal Entities.CalculationScenarioType Add(String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //aca es donde se arma una transaccion y primero se graba la tarea de recovery
                    //y luego se graban las fechas de medicion que se habilitan para recuperar.
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idScenarioTypes = _dbPerformanceAssessments.CalculationScenarioTypes_Create(_Credential.User.IdPerson);

                    _dbPerformanceAssessments.CalculationScenarioTypes_LG_Create(_idScenarioTypes, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.IdPerson);

                    _transactionScope.Complete();

                    //Devuelvo el objeto FunctionalArea creado
                    return new Entities.CalculationScenarioType(_idScenarioTypes, name, description, _Credential);

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
            internal void Remove(Int64 idScenarioTypes)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //aca es donde se arma una transaccion y primero se graba la tarea de recovery
                    //y luego se graban las fechas de medicion que se habilitan para recuperar.
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        _dbPerformanceAssessments.CalculationScenarioTypes_LG_Delete(idScenarioTypes, _Credential.User.IdPerson);
                        //Borrar de la base de datos
                        _dbPerformanceAssessments.CalculationScenarioTypes_Delete(idScenarioTypes, _Credential.User.IdPerson);

                        _transactionScope.Complete();
                    }
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
            internal void Modify(Int64 idScenarioTypes, String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

                    //aca es donde se arma una transaccion y primero se graba la tarea de recovery
                    //y luego se graban las fechas de medicion que se habilitan para recuperar.
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        _dbPerformanceAssessments.CalculationScenarioTypes_LG_Update(idScenarioTypes, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.IdPerson);

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
        #endregion

    }
}
