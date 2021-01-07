using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Collections
{
    public class Formulas
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal Formulas(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
        internal Entities.Formula Item(Int64 idFormula)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();         

            Entities.Formula _formula = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Formulas_ReadById(idFormula, _Credential.CurrentLanguage.IdLanguage);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_formula == null)
                {
                    _formula = new Entities.Formula(Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToString(_dbRecord["LiteralFormula"]), Convert.ToString(_dbRecord["SchemaSP"]), Convert.ToInt64(_dbRecord["idIndicator"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourceVersion"],0)), _Credential);
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _formula;
                    }
                }
                else
                {
                    return new Entities.Formula(Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToString(_dbRecord["LiteralFormula"]), Convert.ToString(_dbRecord["SchemaSP"]), Convert.ToInt64(_dbRecord["idIndicator"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourceVersion"], 0)), _Credential);
                }
            }
            return _formula;
        }
        internal Dictionary<Int64, Entities.Formula> Items()
        {

            //Coleccion para devolver los Indicator
            Dictionary<Int64, Entities.Formula> _oItems = new Dictionary<Int64, Entities.Formula>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Formulas_ReadAll(_Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdFormula"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdFormula"]));
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
                        Entities.Formula _formula = new Entities.Formula(Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToString(_dbRecord["LiteralFormula"]), Convert.ToString(_dbRecord["SchemaSP"]), Convert.ToInt64(_dbRecord["idIndicator"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourceVersion"], 0)), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_formula.IdFormula, _formula);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.Formula _formula = new Entities.Formula(Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToString(_dbRecord["LiteralFormula"]), Convert.ToString(_dbRecord["SchemaSP"]), Convert.ToInt64(_dbRecord["idIndicator"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourceVersion"], 0)), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_formula.IdFormula, _formula);
                }

            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.Formula> ItemsByIndicator(Int64 idIndicator)
        {
            //Coleccion para devolver los Indicator
            Dictionary<Int64, Entities.Formula> _oItems = new Dictionary<Int64, Entities.Formula>();

            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Formulas_ReadByIndicator(idIndicator ,_Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _oInsert = true;

            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdFormula"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdFormula"]));
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
                        Entities.Formula _formula = new Entities.Formula(Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToString(_dbRecord["LiteralFormula"]), Convert.ToString(_dbRecord["SchemaSP"]), Convert.ToInt64(_dbRecord["idIndicator"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourceVersion"], 0)), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_formula.IdFormula, _formula);
                    }
                    _oInsert = true;
                }
                else
                {   //Declara e instancia  
                    Entities.Formula _formula = new Entities.Formula(Convert.ToInt64(_dbRecord["IdFormula"]), Convert.ToDateTime(_dbRecord["CreationDate"]), Convert.ToString(_dbRecord["LiteralFormula"]), Convert.ToString(_dbRecord["SchemaSP"]), Convert.ToInt64(_dbRecord["idIndicator"]), Convert.ToInt64(_dbRecord["idMeasurementUnit"]), Convert.ToString(_dbRecord["name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourceVersion"], 0)), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_formula.IdFormula, _formula);
                }

            }
            return _oItems;
        }
        internal Boolean HasCalculation(Int64 idFormula)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Formulas_HasCalculation(idFormula);
           
            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Write Functions
        //Add con resource Version
        internal Entities.Formula Add(DateTime creationDate, String literalFormula, String schemaSP, Int64 idIndicator, Int64 idMeasurementUnit, String name, String description, DataTable parameters, KC.Entities.ResourceVersion resourceVersion)
            {
                try
                {
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        //Validacion de Objet null
                        Int64 _idResourceVersion = resourceVersion == null ? 0 : resourceVersion.IdResource; 

                        //Objeto de data layer para acceder a datos
                        DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idFormula = _dbPerformanceAssessments.Formulas_Create(creationDate, literalFormula, schemaSP, idIndicator, idMeasurementUnit, _idResourceVersion, _Credential.User.Person.IdPerson);

                        _dbPerformanceAssessments.Formulas_LG_Create(_idFormula, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);

                        foreach (DataRow item in parameters.Rows)
                        {
                            _dbPerformanceAssessments.FormulaParameters_Create(_idFormula, Convert.ToInt64(item["PositionParameter"]), Convert.ToInt64(item["IdIndicator"]), Convert.ToInt64(item["IdMeasurementUnit"]), Convert.ToString(item["ParameterName"]));
                        }

                        // Completar la transacción
                        _transactionScope.Complete();
                        //Devuelvo el objeto creado
                        return new Entities.Formula(_idFormula, creationDate, literalFormula, schemaSP, idIndicator, idMeasurementUnit, name, description, _idResourceVersion, _Credential);
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

        internal void Remove(Int64 idFormula)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                    
                    //Borrar de la base de datos
                    _dbPerformanceAssessments.Formulas_Delete(idFormula, _Credential.User.Person.IdPerson);
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
        internal void Modify(Int64 idFormula, String literalFormula, String schemaSP, Entities.Indicator indicator, Entities.MeasurementUnit measurementUnit, String name, String description, DataTable parameters, KC.Entities.ResourceVersion resourceVersion)
        {
            try
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                    
                    //Validacion de Objet null
                    Int64 _idResourceVersion = resourceVersion == null ? 0 : resourceVersion.IdResource; 
                    
                    //Modifico los datos de la base
                    _dbPerformanceAssessments.Formulas_Update(idFormula, literalFormula, _idResourceVersion, _Credential.User.Person.IdPerson);

                    _dbPerformanceAssessments.Formulas_LG_Update(idFormula, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.IdPerson);

                    if (parameters != null)
                    {
                        if (!HasCalculation(idFormula))
                        {
                            foreach (DataRow item in parameters.Rows)
                            {
                                _dbPerformanceAssessments.FormulaParameters_Update(idFormula, Convert.ToInt64(item["PositionParameter"]), Convert.ToInt64(item["IdIndicator"]), Convert.ToInt64(item["IdMeasurementUnit"]), Convert.ToString(item["ParameterName"]), _Credential.User.IdPerson);
                            }
                        }
                        else
                        {
                            throw new Exception(Common.Resources.Errors.HasCalculate);
                        }
                    }
                    // Completar la transacción
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
