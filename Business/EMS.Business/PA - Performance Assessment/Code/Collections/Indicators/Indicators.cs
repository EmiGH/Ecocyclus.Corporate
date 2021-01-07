using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class Indicators
    {
        #region Internal Properties
        private ICollectionItems _Datasource;
        private Credential _Credential;
        #endregion

        internal Indicators(Credential credential)
        {
            _Credential = credential;
        }

        internal Indicators(Entities.Indicator indicator)
        {
            _Credential = indicator.Credential;
        }


        internal Indicators(Entities.IndicatorClassification indicatorClassification)
        {
            _Datasource = new IndicatorsRead.IndicatorsByIndicatorClassification(indicatorClassification);
            _Credential = indicatorClassification.Credential;
        }

        #region Read Functions
        internal Entities.Indicator Item(Int64 idIndicator)
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

            Entities.Indicator _indicator = null;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Indicators_ReadById(idIndicator, _Credential.CurrentLanguage.IdLanguage);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdIndicator", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                _indicator = new IndicatorFactory().CreateIndicator(Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToBoolean(_dbRecord["IsCumulative"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["Scope"]), Convert.ToString(_dbRecord["Limitation"]), Convert.ToString(_dbRecord["Definition"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
            }
            return _indicator;
        }
        internal Dictionary<Int64, Entities.Indicator> Items()
        {
            Dictionary<Int64, Entities.Indicator> _items = new Dictionary<Int64, Entities.Indicator>();

            IEnumerable<System.Data.Common.DbDataRecord> _record = _Datasource.getItems();

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdIndicator", _Credential).Filter();

            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.Indicator _indicator = new IndicatorFactory().CreateIndicator(Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToBoolean(_dbRecord["IsCumulative"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["Scope"]), Convert.ToString(_dbRecord["Limitation"]), Convert.ToString(_dbRecord["Definition"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                _items.Add(_indicator.IdIndicator, _indicator);
            }
            return _items;
        }

        //internal Dictionary<Int64, Entities.Indicator> Items() 
        //{
        //    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

        //    //Coleccion para devolver las areas funcionales
        //    Dictionary<Int64, Entities.Indicator> _oItems = new Dictionary<Int64, Entities.Indicator>();

        //    //Traigo los datos de la base
        //    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Indicators_ReadAll(_Credential.CurrentLanguage.IdLanguage);
        //    //Se modifica con los datos que realmente se tienen que usar...

        //    Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdIndicator", _Credential).Filter();

        //    //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
        //    foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
        //    {
        //        //Declara e instancia una posicion
        //        Entities.Indicator _indicator = new Entities.Indicator(Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);

        //        //Lo agrego a la coleccion
        //        _oItems.Add(_indicator.IdIndicator, _indicator);

        //    }
        //    return _oItems;
        //}
        //internal Dictionary<Int64, Entities.Indicator> Items(Entities.IndicatorClassification indicatorClassification)
        //{
        //    //Coleccion para devolver los Indicator
        //    Dictionary<Int64, Entities.Indicator> _oItems = new Dictionary<Int64, Entities.Indicator>();

        //    //Objeto de data layer para acceder a datos
        //    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

        //    //Traigo los datos de la base
        //    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Indicators_ReadByClassification(indicatorClassification.IdIndicatorClassification, _Credential.CurrentLanguage.IdLanguage, Common.Security.Indicator, _Credential.User.IdPerson);

        //    //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
        //    Boolean _oInsert = true;

        //    //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
        //    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
        //    {
        //        if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdIndicator"])))
        //        {
        //            ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
        //            if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
        //            {
        //                _oItems.Remove(Convert.ToInt64(_dbRecord["IdIndicator"]));
        //            }
        //            else
        //            {
        //                //No debe insertar en la coleccion ya que existe el idioma correcto.
        //                _oInsert = false;
        //            }

        //            //Solo inserta si es necesario.
        //            if (_oInsert)
        //            {
        //                //Declara e instancia  
        //                Entities.Indicator _indicator = new Entities.Indicator(Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
        //                //Lo agrego a la coleccion
        //                _oItems.Add(_indicator.IdIndicator, _indicator);
        //            }
        //            _oInsert = true;
        //        }
        //        else
        //        {   //Declara e instancia  
        //            Entities.Indicator _indicator = new Entities.Indicator(Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
        //            //Lo agrego a la coleccion
        //            _oItems.Add(_indicator.IdIndicator, _indicator);
        //        }

        //    }
        //    return _oItems;
        //}
        internal Dictionary<Int64, Entities.Indicator> ReadRoot()
        {
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

            Dictionary<Int64, Entities.Indicator> _items = new Dictionary<Int64, Entities.Indicator>();
            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.Indicators_ReadRoot(_Credential.CurrentLanguage.IdLanguage);
            //Se modifica con los datos que realmente se tienen que usar...
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdIndicator", _Credential).Filter();

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.Indicator _indicator = new IndicatorFactory().CreateIndicator(Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToBoolean(_dbRecord["IsCumulative"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["Scope"]), Convert.ToString(_dbRecord["Limitation"]), Convert.ToString(_dbRecord["Definition"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                _items.Add(_indicator.IdIndicator, _indicator);
            }
            return _items;
        }

        #endregion

        #region Write Functions 
        #region Indicators
        internal Entities.Indicator Add(Entities.Magnitud magnitud, Boolean IsCumulative, String name, String description, String scope, String limitation, String definition, Dictionary<Int64, Entities.IndicatorClassification> indicatorClassifications)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idIndicator = _dbPerformanceAssessments.Indicators_Create(magnitud.IdMagnitud, IsCumulative, name, description, _Credential.DefaultLanguage.IdLanguage);
                    //crea el objeto 
                    Entities.Indicator _indicator = new Entities.Indicator(_idIndicator, magnitud.IdMagnitud, IsCumulative, name, description, scope,limitation,definition, _Credential.DefaultLanguage.IdLanguage, _Credential);
                    //alta del lg
                    _dbPerformanceAssessments.Indicators_LG_Create(_idIndicator, _Credential.DefaultLanguage.IdLanguage, name, description, scope,limitation,definition);
                    //Log
                    _dbLog.Create("PA_Indicators", "Indicators", "Add", "IdIndicator=" + _idIndicator, _Credential.User.IdPerson);

                    foreach (Entities.IndicatorClassification _indicatorClassification in indicatorClassifications.Values)
                    {
                        _dbPerformanceAssessments.Indicators_Create(_idIndicator, _indicatorClassification.IdIndicatorClassification);
                    }
                    //Devuelvo el objeto creado
                    return _indicator;
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
        internal void Modify(Entities.Indicator indicator, Entities.Magnitud magnitud, String name, String description, String scope, String limitation, String definition, Dictionary<Int64, PA.Entities.IndicatorClassification> indicatorClassifications)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                    //Modifico los datos de la base
                    _dbPerformanceAssessments.Indicators_Update(indicator.IdIndicator, magnitud.IdMagnitud);
                    //alta del lg
                    _dbPerformanceAssessments.Indicators_LG_Update(indicator.IdIndicator, _Credential.DefaultLanguage.IdLanguage, name, description, scope,limitation,definition);
                    //Borra todas las relaciones con classifications que tengo permiso 
                    foreach (Entities.IndicatorClassification _classification in indicator.Classifications.Values)
                    {
                        _dbPerformanceAssessments.Indicators_DeleteByIndicator(indicator.IdIndicator);
                    } 
                    //inserta las nuevas relaciones
                    //Recorre para todas las clasificaciones e inserta una por una.
                    foreach (PA.Entities.IndicatorClassification _indicatorClassification in indicatorClassifications.Values)
                    {
                        new PA.Collections.Indicators(_Credential).Add(indicator.IdIndicator, _indicatorClassification.IdIndicatorClassification);
                    }
                    //Log
                    _dbLog.Create("PA_Indicators", "Indicators", "Modify", "IdIndicator=" + indicator.IdIndicator, _Credential.User.IdPerson);
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

        //#region IndicatorsGHG
        //    internal Entities.IndicatorGHG Add(Entities.Magnitud magnitud, String name, String description, String scope, String limitation, String definition, Dictionary<Int64, Entities.IndicatorClassification> indicatorClassifications, Decimal GWP, Entities.Constant constant)
        //    {
        //        try
        //        {
        //            //Objeto de data layer para acceder a datos
        //            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
        //            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

        //            //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
        //            Int64 _idIndicator = _dbPerformanceAssessments.Indicators_Create(magnitud.IdMagnitud, name, description, _Credential.DefaultLanguage.IdLanguage);
        //            //inserta la variedad GHG
        //            _dbPerformanceAssessments.IndicatorGHGs_Create(_idIndicator, GWP, constant.IdConstant);
        //            //alta del lg
        //            _dbPerformanceAssessments.Indicators_LG_Create(_idIndicator, _Credential.DefaultLanguage.IdLanguage, name, description, scope,limitation,definition);
        //            //Log
        //            _dbLog.Create("PA_Indicators", "Indicators", "Add", "IdIndicator=" + _idIndicator, _Credential.User.IdPerson);

        //            //crea el objeto 
        //            Entities.IndicatorGHG _indicator = new Entities.IndicatorGHG(_idIndicator, magnitud.IdMagnitud, name, description, scope, limitation, definition, _Credential.DefaultLanguage.IdLanguage, _Credential, GWP,constant.IdConstant);

        //            //seguridad: heredar los permisos que existen en el mapa
        //            Entities.MapPA _mapPA = new Entities.MapPA(_Credential);
        //            _indicator.InheritPermissions(_mapPA);

        //            foreach (Entities.IndicatorClassification _indicatorClassification in indicatorClassifications.Values)
        //            {
        //                _dbPerformanceAssessments.Indicators_Create(_idIndicator, _indicatorClassification.IdIndicatorClassification);

        //                //seguridad: heredar los permisos que existen en la classificacion que se esta dando de alta
        //                _indicator.InheritPermissions(_indicatorClassification);
        //            }
        //            //Devuelvo el objeto creado
        //            return _indicator;
        //        }
        //        catch (SqlException ex)
        //        {
        //            if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
        //            {
        //                throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
        //            }
        //            throw ex;
        //        }
        //    }            
        //    internal void Modify(Entities.IndicatorGHG indicator, Entities.Magnitud magnitud, String name, String description, String scope, String limitation, String definition, Dictionary<Int64, PA.Entities.IndicatorClassification> indicatorClassifications, Decimal GWP, Entities.Constant constant)
        //    {
        //        try
        //        {
        //            //Objeto de data layer para acceder a datos
        //            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
        //            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

        //            //Modifico los datos de la base
        //            _dbPerformanceAssessments.Indicators_Update(indicator.IdIndicator, magnitud.IdMagnitud, _Credential.DefaultLanguage.IdLanguage, name, description);
        //            _dbPerformanceAssessments.IndicatorGHGs_Update(indicator.IdIndicator, GWP, constant.IdConstant);
        //            //modify del lg
        //            _dbPerformanceAssessments.Indicators_LG_Update(indicator.IdIndicator, _Credential.DefaultLanguage.IdLanguage, name, description, scope, limitation, definition);
        //            //Borra todas las relaciones con classifications que tengo permiso 
        //            foreach (Entities.IndicatorClassification _classification in indicator.Classifications.Values)
        //            {
        //                _dbPerformanceAssessments.Indicators_Delete(indicator.IdIndicator, _classification.IdIndicatorClassification);
        //            }
        //            //inserta las nuevas relaciones
        //            //Recorre para todas las clasificaciones e inserta una por una.
        //            foreach (PA.Entities.IndicatorClassification _indicatorClassification in indicatorClassifications.Values)
        //            {
        //                new PA.Collections.Indicators(_Credential).Add(indicator.IdIndicator, _indicatorClassification.IdIndicatorClassification);
        //            }
        //            //Log
        //            _dbLog.Create("PA_Indicators", "Indicators", "Modify", "IdIndicator=" + indicator.IdIndicator, _Credential.User.IdPerson);
        //        }
        //        catch (SqlException ex)
        //        {
        //            if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
        //            {
        //                throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
        //            }
        //            throw ex;
        //        }
        //    }
        //#endregion

        //#region IndicatorsGHGSubstance
        //    internal Entities.IndicatorGHGSubstance Add(Entities.Magnitud magnitud, String name, String description, String scope, String limitation, String definition, Dictionary<Int64, Entities.IndicatorClassification> indicatorClassifications, Dictionary<Int64, Entities.IndicatorGHG> indicatorGHGs)
        //    {
        //        try
        //        {
        //            //Objeto de data layer para acceder a datos
        //            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
        //            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

        //            //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
        //            Int64 _idIndicator = _dbPerformanceAssessments.Indicators_Create(magnitud.IdMagnitud, name, description, _Credential.DefaultLanguage.IdLanguage);
        //            //inserta la variedad GHG
        //            _dbPerformanceAssessments.IndicatorGHGSubstances_Create(_idIndicator);
        //            //alta del lg
        //            _dbPerformanceAssessments.Indicators_LG_Create(_idIndicator, _Credential.DefaultLanguage.IdLanguage, name, description, scope, limitation, definition);
        //            //Log
        //            _dbLog.Create("PA_Indicators", "Indicators", "Add", "IdIndicator=" + _idIndicator, _Credential.User.IdPerson);

        //            //crea el objeto 
        //            Entities.IndicatorGHGSubstance _indicator = new Entities.IndicatorGHGSubstance(_idIndicator, magnitud.IdMagnitud, name, description, scope, limitation, definition, _Credential.DefaultLanguage.IdLanguage, _Credential);

        //            //seguridad: heredar los permisos que existen en el mapa
        //            Entities.MapPA _mapPA = new Entities.MapPA(_Credential);
        //            _indicator.InheritPermissions(_mapPA);
                    
        //            foreach (Entities.IndicatorGHG _indicatorGHG in indicatorGHGs.Values)
        //            {
        //                _dbPerformanceAssessments.IndicatorGHGSubstanceIndicatorGHGs_Create(_idIndicator, _indicatorGHG.IdIndicator);
        //            }
        //            foreach (Entities.IndicatorClassification _indicatorClassification in indicatorClassifications.Values)
        //            {
        //                _dbPerformanceAssessments.Indicators_Create(_idIndicator, _indicatorClassification.IdIndicatorClassification);

        //                //seguridad: heredar los permisos que existen en la classificacion que se esta dando de alta
        //                _indicator.InheritPermissions(_indicatorClassification);
        //            }
        //            //Devuelvo el objeto creado
        //            return _indicator;
        //        }
        //        catch (SqlException ex)
        //        {
        //            if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
        //            {
        //                throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
        //            }
        //            throw ex;
        //        }
        //    }
        //    internal void Modify(Entities.IndicatorGHGSubstance indicator, Entities.Magnitud magnitud, String name, String description, String scope, String limitation, String definition, Dictionary<Int64, PA.Entities.IndicatorClassification> indicatorClassifications, Dictionary<Int64, Entities.IndicatorGHG> indicatorGHGs)
        //    {
        //        try
        //        {
        //            //Objeto de data layer para acceder a datos
        //            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
        //            DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

        //            //Modifico los datos de la base
        //            _dbPerformanceAssessments.Indicators_Update(indicator.IdIndicator, magnitud.IdMagnitud, _Credential.DefaultLanguage.IdLanguage, name, description);
        //            //modify del lg
        //            _dbPerformanceAssessments.Indicators_LG_Update(indicator.IdIndicator, _Credential.DefaultLanguage.IdLanguage, name, description, scope, limitation, definition);
                    
        //            //Borra todas las relaciones con GHG
        //            foreach (Entities.IndicatorGHG _indicatorGHG in indicatorGHGs.Values)
        //            {
        //                _dbPerformanceAssessments.IndicatorGHGSubstanceIndicatorGHGs_Delete(indicator.IdIndicator, _indicatorGHG.IdIndicator);
        //            }
        //            //inserta las relaciones con GHG
        //            foreach (Entities.IndicatorGHG _indicatorGHG in indicatorGHGs.Values)
        //            {
        //                _dbPerformanceAssessments.IndicatorGHGSubstanceIndicatorGHGs_Create(indicator.IdIndicator, _indicatorGHG.IdIndicator);
        //            }

        //            //Borra todas las relaciones con classifications que tengo permiso 
        //            foreach (Entities.IndicatorClassification _classification in indicator.Classifications.Values)
        //            {
        //                _dbPerformanceAssessments.Indicators_Delete(indicator.IdIndicator, _classification.IdIndicatorClassification);
        //            }
        //            //inserta las nuevas relaciones
        //            //Recorre para todas las clasificaciones e inserta una por una.
        //            foreach (PA.Entities.IndicatorClassification _indicatorClassification in indicatorClassifications.Values)
        //            {
        //                new PA.Collections.Indicators(_Credential).Add(indicator.IdIndicator, _indicatorClassification.IdIndicatorClassification);
        //            }
        //            //Log
        //            _dbLog.Create("PA_Indicators", "Indicators", "Modify", "IdIndicator=" + indicator.IdIndicator, _Credential.User.IdPerson);
        //        }
        //        catch (SqlException ex)
        //        {
        //            if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
        //            {
        //                throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
        //            }
        //            throw ex;
        //        }
        //    }
        //    #endregion

        /// <summary>
        /// Borra todos los tipo de Indicators
        /// </summary>
        /// <param name="indicator"></param>
        internal void Remove(Entities.Indicator indicator)
        {
            try
            {
                //Valida que no este relacionado con alguna Medicion
                if (indicator.Measurements.Count != 0) { throw new DuplicateNameException(Condesus.EMS.Business.Common.Resources.Errors.RemoveDependency); }
                //Valida que no este relacionado con alguna Formula
                if (indicator.Formulas.Count != 0) { throw new DuplicateNameException(Condesus.EMS.Business.Common.Resources.Errors.RemoveDependency); }
                //Valida que no este relacionado con ninguna transformacion
                if (indicator.Transformations.Count != 0) { throw new DuplicateNameException(Condesus.EMS.Business.Common.Resources.Errors.RemoveDependency); }

                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                //Borra LG
                _dbPerformanceAssessments.Indicators_LG_Delete(indicator.IdIndicator);
                //Borra relacion con classification
                _dbPerformanceAssessments.Indicators_DeleteByIndicator(indicator.IdIndicator);
                //Borra relacion con parameter group
                indicator.Remove();
                //Borra indicator
                _dbPerformanceAssessments.Indicators_Delete(indicator.IdIndicator);
                //Log
                _dbLog.Create("PA_Indicators", "Indicators", "Remove", "IdIndicator=" + indicator.IdIndicator, _Credential.User.IdPerson);

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

        #region Indicator related with IndicatorClassification
            internal void Add(Int64 idIndicator, Int64 idIndicatorClassification)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbPerformanceAssessments.Indicators_Create(idIndicator, idIndicatorClassification);
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
            internal void Remove(Int64 idIndicator, Int64 idIndicatorClassification)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();
                    //Borrar de la base de datos
                    _dbPerformanceAssessments.Indicators_Delete(idIndicator, idIndicatorClassification);
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
            internal void RemoveReltatedClassification(Int64 idIndicator)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new DataAccess.PA.PerformanceAssessments();

                    //Borrar de la base de datos
                    _dbPerformanceAssessments.Indicators_DeleteByIndicator(idIndicator);
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
            #endregion
        #endregion

        #region Read Functions Indicators By Classification
        //internal Entities.Indicator ItemByClassification(Int64 idIndicator)
        //{
        //    //Check for permission
        //    Condesus.EMS.Business.Security.Authority.Authorize("PerformanceAssessment", "Indicators", "Read");

        //    //Objeto de data layer para acceder a datos
        //    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
        //    DataAccess.PA.Entities.ClassificationIndicators _dbClassificationIndicators = _dbPerformanceAssessments.ClassificationIndicators;

        //    Entities.Indicator _indicator = null;
        //    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbClassificationIndicators.ReadById(_IdClassification, idIndicator, _Credential.CurrentLanguage.IdLanguage);
        //    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
        //    {
        //        if (_indicator == null)
        //        {
        //            _indicator = new Entities.Indicator(Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToInt64(_dbRecord["IdIndicatorClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
        //            if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
        //            {
        //                return _indicator;
        //            }
        //        }
        //        else
        //        {
        //            return new Entities.Indicator(Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToInt64(_dbRecord["IdIndicatorClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
        //        }
        //    }
        //    return _indicator;
        //}
        //internal Dictionary<Int64, Entities.Indicator> ItemsByClassification()
        //{
        //    //Check for permission
        //    Condesus.EMS.Business.Security.Authority.Authorize("PerformanceAssessment", "Indicators", "Read");

        //    //Coleccion para devolver los Indicator
        //    Dictionary<Int64, Entities.Indicator> _oItems = new Dictionary<Int64, Entities.Indicator>();

        //    //Objeto de data layer para acceder a datos
        //    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
        //    DataAccess.PA.Entities.ClassificationIndicators _dbClassificationIndicators = _dbPerformanceAssessments.ClassificationIndicators;

        //    //Traigo los datos de la base
        //    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbClassificationIndicators.ReadAll(_IdOrganization, _IdClassification, _Credential.CurrentLanguage.IdLanguage);

        //    //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
        //    Boolean _oInsert = true;

        //    //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
        //    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
        //    {
        //        if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdIndicator"])))
        //        {
        //            ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
        //            if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
        //            {
        //                _oItems.Remove(Convert.ToInt64(_dbRecord["IdIndicator"]));
        //            }
        //            else
        //            {
        //                //No debe insertar en la coleccion ya que existe el idioma correcto.
        //                _oInsert = false;
        //            }

        //            //Solo inserta si es necesario.
        //            if (_oInsert)
        //            {
        //                //Declara e instancia  
        //                Entities.Indicator _indicator = new Entities.Indicator(Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToInt64(_dbRecord["IdIndicatorClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
        //                //Lo agrego a la coleccion
        //                _oItems.Add(_indicator.IdIndicator, _indicator);
        //            }
        //            _oInsert = true;
        //        }
        //        else
        //        {   //Declara e instancia  
        //            Entities.Indicator _indicator = new Entities.Indicator(Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(_dbRecord["IdIndicator"]), Convert.ToInt64(_dbRecord["IdMagnitud"]), Convert.ToInt64(_dbRecord["IdIndicatorClassification"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
        //            //Lo agrego a la coleccion
        //            _oItems.Add(_indicator.IdIndicator, _indicator);
        //        }

        //    }
        //    return _oItems;
        //}
        #endregion

        //Ver aca ruben!!!
        #region Write Functions Indicators By Classification
        //internal void AddByClassification(Int64 idIndicator)
        //{
        //    //Check for permission
        //    Condesus.EMS.Business.Security.Authority.Authorize("PerformanceAssessment", "Indicators", "AddByClassification");
        //    try
        //    {
        //        //Objeto de data layer para acceder a datos
        //        DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
        //        DataAccess.PA.Entities.ClassificationIndicators _dbClassificationIndicators = _dbPerformanceAssessments.ClassificationIndicators;

        //        //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
        //        _dbClassificationIndicators.Create(_IdOrganization, _IdClassification,  idIndicator, _Credential.User.Person.IdPerson);
                
        //    }
        //    catch (SqlException ex)
        //    {
        //        if (ex.Number == Common.Constants.ErrorDataBaseDuplicatedKey)
        //        {
        //            throw new Exception(Common.Resources.Errors.DuplicatedDataRecord, ex);
        //        }
        //        throw ex;
        //    }
        //}
        //internal void RemoveByClassification(Int64 idIndicator)
        //{
        //    //Check for permission
        //    Condesus.EMS.Business.Security.Authority.Authorize("PerformanceAssessment", "Indicators", "RemoveByClassification");
        //    try
        //    {
        //        //Objeto de data layer para acceder a datos
        //        DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
        //        DataAccess.PA.Entities.ClassificationIndicators _dbClassificationIndicators = _dbPerformanceAssessments.ClassificationIndicators;

        //        //Borrar de la base de datos
        //        _dbClassificationIndicators.Delete(_IdOrganization, _IdClassification, idIndicator, _Credential.User.Person.IdPerson);
        //    }
        //    catch (SqlException ex)
        //    {
        //        if (ex.Number == Common.Constants.ErrorDataBaseDeleteReferenceConstraints)
        //        {
        //            throw new Exception(Common.Resources.Errors.RemoveDependency, ex);
        //        }
        //        throw ex;
        //    }
        //}
        #endregion

    }
}
