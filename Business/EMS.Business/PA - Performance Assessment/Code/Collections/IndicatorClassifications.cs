using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PA.Collections
{
    internal class IndicatorClassifications
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.IndicatorClassification _Parent; //Id del padre; si se carga <> de cero traer los items que son hijos
        #endregion

        internal IndicatorClassifications(Credential credential)
        {
            _Credential = credential;
            _Parent = null;
        }
        internal IndicatorClassifications(Entities.IndicatorClassification parent, Credential credential)
        {
            _Credential = credential;
            _Parent = parent;
        }

        //encapsula la toma de decision de cual es el idparent
        private Int64 IdParent
        {
            get
            {
                if (_Parent == null) { return 0; } else { return _Parent.IdIndicatorClassification; }
            }
        }

        #region Read Functions
        internal Entities.IndicatorClassification Item(Int64 idIndicatorClassification)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            Entities.IndicatorClassification _IndicatorClassification = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.IndicatorClassifications_ReadById(idIndicatorClassification, _Credential.CurrentLanguage.IdLanguage);
            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if ((_Parent == null) || (Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdIndicatorClassification"], 0)) == _Parent.IdIndicatorClassification))
                {
                    if (_IndicatorClassification == null)
                    {
                        _IndicatorClassification = new Entities.IndicatorClassification(Convert.ToInt64(_dbRecord["IdIndicatorClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentIndicatorClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _IndicatorClassification;
                        }
                    }
                    else
                    {
                        return new Entities.IndicatorClassification(Convert.ToInt64(_dbRecord["IdIndicatorClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentIndicatorClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
                    }
                }
                else
                {
                    //no es hijo asi que no lo puede devolver......generar el error
                    return null;
                }
            }
            return _IndicatorClassification;
        }
        internal Dictionary<Int64, Entities.IndicatorClassification> Items()
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.IndicatorClassification> _oItems = new Dictionary<Int64, Entities.IndicatorClassification>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record;
            if (_Parent == null)
            { _record = _dbPerformanceAssessments.IndicatorClassifications_ReadRoot(_Credential.CurrentLanguage.IdLanguage); }
            else
            { _record = _dbPerformanceAssessments.IndicatorClassifications_ReadByClassification(_Parent.IdIndicatorClassification, _Credential.CurrentLanguage.IdLanguage); }

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _bInsert = true;

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdIndicatorClassification"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdIndicatorClassification"]));
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
                    Entities.IndicatorClassification _IndicatorClassification = new Entities.IndicatorClassification(Convert.ToInt64(_dbRecord["IdIndicatorClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentIndicatorClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_IndicatorClassification.IdIndicatorClassification, _IndicatorClassification);
                }
                _bInsert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.IndicatorClassification> Items(Entities.Indicator indicator)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.IndicatorClassification> _oItems = new Dictionary<Int64, Entities.IndicatorClassification>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record;
            _record = _dbPerformanceAssessments.IndicatorClassifications_ReadByIndicator(indicator.IdIndicator, _Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _bInsert = true;

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdIndicatorClassification"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdIndicatorClassification"]));
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
                    Entities.IndicatorClassification _IndicatorClassification = new Entities.IndicatorClassification(Convert.ToInt64(_dbRecord["IdIndicatorClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentIndicatorClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_IndicatorClassification.IdIndicatorClassification, _IndicatorClassification);
                }
                _bInsert = true;
            }
            return _oItems;
        }
        internal Dictionary<Int64, Entities.IndicatorClassification> ItemsSecurity()
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.IndicatorClassification> _oItems = new Dictionary<Int64, Entities.IndicatorClassification>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbPerformanceAssessments.IndicatorClassifications_ReadAll(_Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _bInsert = true;

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdIndicatorClassification"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdIndicatorClassification"]));
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
                    Entities.IndicatorClassification _IndicatorClassification = new Entities.IndicatorClassification(Convert.ToInt64(_dbRecord["IdIndicatorClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentIndicatorClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_IndicatorClassification.IdIndicatorClassification, _IndicatorClassification);
                }
                _bInsert = true;
            }
            return _oItems;
        }
        #endregion
        
        #region Write Functions
        internal Entities.IndicatorClassification Add(String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                Int64 _idIndicatorClassification = _dbPerformanceAssessments.IndicatorClassifications_Create(IdParent);

                _dbPerformanceAssessments.IndicatorClassifications_LG_Create(_idIndicatorClassification, _Credential.DefaultLanguage.IdLanguage, name, description);
         
                Entities.IndicatorClassification _indicatorClassification = new Entities.IndicatorClassification(_idIndicatorClassification, IdParent, name, description, _Credential);
                
                _dbLog.Create("PA_IndicatorClassifications", "IndicatorClassifications", "Add", "IdIndicatorClassification=" + _idIndicatorClassification, _Credential.User.IdPerson);
                //Devuelvo el objeto creado
                return _indicatorClassification;
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
        internal void Remove(Entities.IndicatorClassification indicatorClassification)
        {         
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                //Borra las clasificaciones hijas
                indicatorClassification.Remove();
                //Borra las relaciones con los elementos
                _dbPerformanceAssessments.Indicators_DeleteByClassification(indicatorClassification.IdIndicatorClassification);
                //Borra los LG asociados
                _dbPerformanceAssessments.IndicatorClassifications_LG_DeleteByClassification(indicatorClassification.IdIndicatorClassification);
                //Borrar de la base de datos
                _dbPerformanceAssessments.IndicatorClassifications_Delete(indicatorClassification.IdIndicatorClassification);
                //log
                _dbLog.Create("PA_IndicatorClassifications", "IndicatorClassifications", "Remove", "IdIndicatorClassification=" + indicatorClassification.IdIndicatorClassification, _Credential.User.IdPerson);

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
        internal void Modify(Entities.IndicatorClassification indicatorClassification, String name, String description)
        {
            try
            {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                    ////Borrar la seguridad
                    //indicatorClassification.SecurityJobTitleRemove();
                    //indicatorClassification.SecurityPostRemove();

                    ////Seguridad: hereda de la del padre, si no tiene padre hereda del mapa
                    //if (_Parent == null)
                    //{
                    //    Entities.MapPA _mapPA = new Entities.MapPA(_Credential);
                    //    indicatorClassification.InheritPermissions(_mapPA);
                    //}
                    //else
                    //{
                    //    indicatorClassification.InheritPermissions(_Parent);
                    //    //Modifico los datos de la base
                    //    _dbPerformanceAssessments.IndicatorClassifications_Update(indicatorClassification.IdIndicatorClassification, _Parent.IdIndicatorClassification);
                    //}   
                    
                    //LG
                    _dbPerformanceAssessments.IndicatorClassifications_LG_Update(indicatorClassification.IdIndicatorClassification, _Credential.DefaultLanguage.IdLanguage, name, description);
                    //Log
                    _dbLog.Create("PA_IndicatorClassifications", "IndicatorClassifications", "Modify", "IdIndicatorClassification=" + indicatorClassification.IdIndicatorClassification, _Credential.User.IdPerson);
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
        internal void Modify(Entities.IndicatorClassification indicatorClassification)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PA.PerformanceAssessments _dbPerformanceAssessments = new Condesus.EMS.DataAccess.PA.PerformanceAssessments();
                DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                if (_Parent == null)
                {
                    _dbPerformanceAssessments.IndicatorClassifications_Update(indicatorClassification.IdIndicatorClassification, 0);
                }
                else
                {
                    _dbPerformanceAssessments.IndicatorClassifications_Update(indicatorClassification.IdIndicatorClassification, _Parent.IdIndicatorClassification);
                }

                //Log
                _dbLog.Create("PA_IndicatorClassifications", "IndicatorClassifications", "Modify", "IdIndicatorClassification=" + indicatorClassification.IdIndicatorClassification, _Credential.User.IdPerson);
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
