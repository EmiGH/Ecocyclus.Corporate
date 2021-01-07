using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.RM.Collections
{
    internal class RiskClassifications
    {
    #region Internal Properties
        private Credential _Credential;
        private Entities.RiskClassification _Parent; 
        #endregion

        internal RiskClassifications(Credential credential)
        {
            _Credential = credential; 
            _Parent = null;
        }
        internal RiskClassifications(Entities.RiskClassification parent, Credential credential)
        {
            _Credential = credential;
            _Parent = parent;
        }

        //encapsula la toma de decision de cual es el idparent
        private Int64 IdParent
        {
            get
            {
                if (_Parent == null) { return 0; } else { return _Parent.IdRiskClassification; }
            }
        }

        #region Read Functions
        internal Entities.RiskClassification Item(Int64 idRiskClassification)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();

            Entities.RiskClassification _RiskClassification = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbRiskManagement.RiskClassifications_ReadById(idRiskClassification, _Credential.CurrentLanguage.IdLanguage, Common.Security.ResourceClassification, _Credential.User.IdPerson);
            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if ((IdParent == 0) || (Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdRiskClassification"], 0)) == IdParent))
                {
                    if (_RiskClassification == null)
                    {
                        _RiskClassification = new Entities.RiskClassification(Convert.ToInt64(_dbRecord["IdRiskClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentRiskClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _RiskClassification;
                        }
                    }
                    else
                    {
                        return new Entities.RiskClassification(Convert.ToInt64(_dbRecord["IdRiskClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentRiskClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
                    }
                }
                else
                {
                    //no es hijo asi que no lo puede devolver......generar el error
                    return null;
                }
            }
            return _RiskClassification;
        }
        internal Dictionary<Int64, Entities.RiskClassification> Items()
        {
            //Objeto de data layer para acceder a datos
            DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();
            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.RiskClassification> _oItems = new Dictionary<Int64, Entities.RiskClassification>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record;
            if (IdParent == 0)
            { _record = _dbRiskManagement.RiskClassifications_GetRoot(_Credential.CurrentLanguage.IdLanguage, Common.Security.ResourceClassification, _Credential.User.IdPerson); }
            else
            { _record = _dbRiskManagement.RiskClassifications_GetByParent(IdParent, _Credential.CurrentLanguage.IdLanguage, Common.Security.ResourceClassification, _Credential.User.IdPerson); }

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _bInsert = true;

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdRiskClassification"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdRiskClassification"]));
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
                    Entities.RiskClassification _RiskClassification = new Entities.RiskClassification(Convert.ToInt64(_dbRecord["IdRiskClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentRiskClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_RiskClassification.IdRiskClassification, _RiskClassification);
                }
                _bInsert = true;
            }
            return _oItems;
        }
        #endregion

        #region Write Functions
        internal Entities.RiskClassification Add(String name, String description)
        {
            try
            {
                    //Objeto de data layer para acceder a datos
                    DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();
                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idRiskClassification = _dbRiskManagement.RiskClassifications_Create(IdParent, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);

                    Entities.RiskClassification _riskClassification = new Entities.RiskClassification(_idRiskClassification, IdParent, name, description, _Credential);

                    //Devuelvo el objeto FunctionalArea creado
                    return _riskClassification;
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
        internal void Remove(Entities.RiskClassification riskClassification)
        {
            if (riskClassification.ChildrenClassifications.Count != 0)
            {
                throw new DuplicateNameException(Condesus.EMS.Business.Common.Resources.Errors.RemoveDependency);
            }
            try
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                //Objeto de data layer para acceder a datos
                    DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();

            
                //Borrar de la base de datos
                _dbRiskManagement.RiskClassifications_Delete(riskClassification.IdRiskClassification, _Credential.User.Person.IdPerson);
                // Completar la transacción
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
        internal void Modify(Entities.RiskClassification riskClassification, String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();
                //      //Borrar la seguridad
                //riskClassification.SecurityJobTitleRemove();
                //riskClassification.SecurityPostRemove();

                ////Seguridad: hereda de la del padre, si no tiene padre hereda del mapa
                //if (idParent == 0)
                //{
                //    Entities.MapRM _mapRM = new Entities.MapRM(_Credential);
                //    riskClassification.InheritPermissions(_mapRM);
                //}
                //else
                //{
                //    Entities.RiskClassification _riskClassificationParent = Item(idParent);
                //    riskClassification.InheritPermissions(_riskClassificationParent);
                //}                

                //Modifico los datos de la base
                _dbRiskManagement.RiskClassifications_LG_Update(riskClassification.IdRiskClassification, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
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
        internal void Modify(Entities.RiskClassification riskClassification)
        {
            try
            {
                    //Objeto de data layer para acceder a datos
                    DataAccess.RM.RiskManagement _dbRiskManagement = new DataAccess.RM.RiskManagement();

                    //Seguridad: hereda de la del padre, si no tiene padre hereda del mapa
                    if (_Parent == null)
                    {
                        _dbRiskManagement.RiskClassifications_Update(riskClassification.IdRiskClassification, 0, _Credential.DefaultLanguage.IdLanguage, riskClassification.LanguageOption.Name, riskClassification.LanguageOption.Description, _Credential.User.Person.IdPerson);
                    }
                    else
                    {
                        _dbRiskManagement.RiskClassifications_Update(riskClassification.IdRiskClassification, IdParent, _Credential.DefaultLanguage.IdLanguage, riskClassification.LanguageOption.Name, riskClassification.LanguageOption.Description, _Credential.User.Person.IdPerson);
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
