using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PF.Collections
{
    internal class ProcessClassifications
    {
        #region Internal Properties
        private Credential _Credential;
        private Entities.ProcessClassification _Parent; //Id del padre; si se carga <> de cero traer los items que son hijos
        #endregion

        internal ProcessClassifications(Credential credential)        
        {
            _Credential = credential;            
            _Parent = null;
        }
        internal ProcessClassifications(Entities.ProcessClassification parent, Credential credential)
        {
            _Credential = credential;            
            _Parent = parent;
        }
        //encapsula la toma de decision de cual es el idparent
        private Int64 IdParent
        {
            get
            {
                if (_Parent == null) { return 0; } else { return _Parent.IdProcessClassification; }
            }
        }

        #region Read Functions
        internal Entities.ProcessClassification Item(Int64 idProcessClassification)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

            Entities.ProcessClassification _processClassification = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessClassifications_ReadById(idProcessClassification, _Credential.CurrentLanguage.IdLanguage);
            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if ((IdParent == 0) || (Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentProcessClassification"], 0)) == IdParent))
                {
                    if (_processClassification == null)
                    {
                        _processClassification = new Entities.ProcessClassification(Convert.ToInt64(_dbRecord["IdProcessClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentProcessClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _processClassification;
                        }
                    }
                    else
                    {
                        return new Entities.ProcessClassification(Convert.ToInt64(_dbRecord["IdProcessClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentProcessClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                    }
                }
                else
                {
                    //no es hijo asi que no lo puede devolver......generar el error
                    return null;
                }
            }
            return _processClassification;
        }
        internal Dictionary<Int64, Entities.ProcessClassification> Items()
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

            Dictionary<Int64, Entities.ProcessClassification> _oItems = new Dictionary<Int64, Entities.ProcessClassification>();

            Int64 _idParent = _Parent == null ? 0 : _Parent.IdProcessClassification;
            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record;
            if (_idParent == 0)
            { _record = _dbProcessesFramework.ProcessClassifications_ReadRoot(_Credential.CurrentLanguage.IdLanguage); }
            else
            { _record = _dbProcessesFramework.ProcessClassifications_ReadByParent(_idParent, _Credential.CurrentLanguage.IdLanguage); }
            //Filtro de lenguaje
            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcessClassification", _Credential).Filter();

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                Entities.ProcessClassification _processClassification = new Entities.ProcessClassification(Convert.ToInt64(_dbRecord["IdProcessClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentProcessClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);

                //Lo agrego a la coleccion
                _oItems.Add(_processClassification.IdProcessClassification, _processClassification);
            }
            return _oItems;
        }
        /// <summary>
        /// Devuelve todos los clasification sin filtro de seguridad, se usa para dar de alta la seguridad
        /// </summary>
        /// <returns></returns>
        //internal Dictionary<Int64, Entities.ProcessClassification> ItemsSecurity()
        //{
        //    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

        //    Dictionary<Int64, Entities.ProcessClassification> _oItems = new Dictionary<Int64, Entities.ProcessClassification>();

        //    //Traigo los datos de la base
        //    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessClassifications_ReadAll(_Credential.CurrentLanguage.IdLanguage);

        //    Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcessClassification", _Credential).Filter();

        //    foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
        //    {
        //        Entities.ProcessClassification _processClassification = new Entities.ProcessClassification(Convert.ToInt64(_dbRecord["IdProcessClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentProcessClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

        //        //Lo agrego a la coleccion
        //        _oItems.Add(_processClassification.IdProcessClassification, _processClassification);
        //    }
        //    return _oItems;
        //}
        #endregion

        #region Write Functions
        internal Entities.ProcessClassification Add(Entities.ProcessClassification parent, String name, String description)
        {
            try
            {
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    DataAccess.Log.Log _log = new Condesus.EMS.DataAccess.Log.Log();

                    Int64 _idParent = parent == null ? 0 : parent.IdProcessClassification;
                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idProcessClassification = _dbProcessesFramework.ProcessClassifications_Create(_idParent);

                    _dbProcessesFramework.ProcessClassifications_LG_Create(_idProcessClassification, _Credential.DefaultLanguage.IdLanguage, name, description);

                    _log.Create("PF_ProcessClassifications", "ProcessClassifications", "Add", "IdProcessClassification = " + _idProcessClassification, _Credential.User.IdPerson);

                    Entities.ProcessClassification _processClassifications = new Entities.ProcessClassification(_idProcessClassification, _idParent, name, description, _Credential);
          
                    //Devuelvo el objeto FunctionalArea creado
                    return _processClassifications;
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
        internal void Remove(Entities.ProcessClassification processClassification)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                DataAccess.Log.Log _log = new Condesus.EMS.DataAccess.Log.Log();
                //Borra los hijos
                processClassification.Remove();
                //Borra la relacion con los elementos
                _dbProcessesFramework.ProcessClassifications_DeleteRaltionship(processClassification.IdProcessClassification);
                _dbProcessesFramework.ProcessClassifications_DeleteScenarioTypes(processClassification.IdProcessClassification);
                //Borra los LG
                _dbProcessesFramework.ProcessClassifications_LG_Delete(processClassification.IdProcessClassification);
                //Borrar de la base de datos
                _dbProcessesFramework.ProcessClassifications_Delete(processClassification.IdProcessClassification);
                //Log
                _log.Create("PF_ProcessClassifications", "ProcessClassifications", "Remove", "IdProcessClassification = " + processClassification.IdProcessClassification, _Credential.User.IdPerson);
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
        internal void Modify(Entities.ProcessClassification processClassification, String name, String description)
        {
            try
            {
                 //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                DataAccess.Log.Log _log = new Condesus.EMS.DataAccess.Log.Log();
            
                //Modifico los datos de la base                    
                _dbProcessesFramework.ProcessClassifications_LG_Update(processClassification.IdProcessClassification, _Credential.DefaultLanguage.IdLanguage, name, description);

                _log.Create("PF_ProcessClassifications", "ProcessClassifications", "Modify", "IdProcessClassification = " + processClassification.IdProcessClassification, _Credential.User.IdPerson);

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
        internal void Modify(Entities.ProcessClassification processClassification)
        {
            try
            {
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                if (_Parent == null)
                {
                    Entities.MapPF _mapPF = new Entities.MapPF(_Credential);
                    
                    _dbProcessesFramework.ProcessClassifications_Update(processClassification.IdProcessClassification, 0);
                }
                else
                {
                    _dbProcessesFramework.ProcessClassifications_Update(processClassification.IdProcessClassification, _Parent.IdProcessClassification);
                }
                DataAccess.Log.Log _log = new Condesus.EMS.DataAccess.Log.Log();
                _log.Create("PF_ProcessClassifications", "ProcessClassifications", "Modify", "IdProcessClassification = " + processClassification.IdProcessClassification, _Credential.User.IdPerson);

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
