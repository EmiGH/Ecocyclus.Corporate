using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Collections
{
    public class ResourceTypes
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdParent;
        #endregion

            internal ResourceTypes(Credential credential)
        {
            _Credential = credential;
            _IdParent = 0;
        }

            internal ResourceTypes(Credential credential, Int64 idparentResourceType)
            {
                _Credential = credential;
                _IdParent = idparentResourceType;
            }

        #region Read Functions
            //Trae el tipo pedido
            internal Entities.ResourceType Item(Int64 idResourceType)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            Entities.ResourceType _ResourceType = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceTypes_ReadById(idResourceType, _Credential.CurrentLanguage.IdLanguage);
            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if ((_IdParent == 0) || (Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourceType"], 0)) == _IdParent))
                {
                    if (_ResourceType == null)
                    {
                        _ResourceType = new Entities.ResourceType(Convert.ToInt64(_dbRecord["IdResourceType"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentResourceType"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _ResourceType;
                        }
                    }
                    else
                    {
                        return new Entities.ResourceType(Convert.ToInt64(_dbRecord["IdResourceType"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentResourceType"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                    }
                }
                else
                {
                    //no es hijo asi que no lo puede devolver......generar el error
                    return null;
                }
            }
            return _ResourceType;
        }
            //Trae todos los tipos
            internal Dictionary<Int64, Entities.ResourceType> Items()
        {
            //Objeto de data layer para acceder a datos
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.ResourceType> _oItems = new Dictionary<Int64, Entities.ResourceType>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record;
            if (_IdParent == 0)
            { _record = _dbKnowledgeCollaboration.ResourceTypes_ReadRoot(_Credential.CurrentLanguage.IdLanguage); }
            else
            { _record = _dbKnowledgeCollaboration.ResourceTypes_ReadByParent(_IdParent, _Credential.CurrentLanguage.IdLanguage); }

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _bInsert = true;

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdResourceType"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdResourceType"]));
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
                    Entities.ResourceType _resourceType = new Entities.ResourceType(Convert.ToInt64(_dbRecord["IdResourceType"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentResourceType"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_resourceType.IdResourceType, _resourceType);
                }
                _bInsert = true;
            }
            return _oItems;
        }

        #endregion

        #region Write Functions
            internal Entities.ResourceType Add(String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //aca es donde se arma una transaccion y primero se graba la tarea de recovery
                    //y luego se graban las fechas de medicion que se habilitan para recuperar.
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idResourceType = _dbKnowledgeCollaboration.ResourceTypes_Create(_IdParent, _Credential.User.IdPerson);

                    _dbKnowledgeCollaboration.ResourceTypes_LG_Create(_idResourceType, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.IdPerson);

                    _transactionScope.Complete();

                    //Devuelvo el objeto FunctionalArea creado
                    return new Entities.ResourceType(_idResourceType, _IdParent, name, description, _Credential);

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
            internal void Remove(Entities.ResourceType resourceType)
            {
                if (Item(resourceType.IdResourceType).Children.Count != 0)
                {
                    throw new DuplicateNameException(Condesus.EMS.Business.Common.Resources.Errors.RemoveDependency);
                }
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //aca es donde se arma una transaccion y primero se graba la tarea de recovery
                    //y luego se graban las fechas de medicion que se habilitan para recuperar.
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        _dbKnowledgeCollaboration.ResourceTypes_LG_Delete(resourceType.IdResourceType, _Credential.User.Person.IdPerson);
                        //Borrar de la base de datos
                        _dbKnowledgeCollaboration.ResourceTypes_Delete(resourceType.IdResourceType, _Credential.User.Person.IdPerson);

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
            internal void Modify(Entities.ResourceType resourceType, Int64 idParent, String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //aca es donde se arma una transaccion y primero se graba la tarea de recovery
                    //y luego se graban las fechas de medicion que se habilitan para recuperar.
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        //Modifico los datos de la base
                        _dbKnowledgeCollaboration.ResourceTypes_Update(resourceType.IdResourceType, idParent, _Credential.User.IdPerson);

                        _dbKnowledgeCollaboration.ResourceTypes_LG_Update(resourceType.IdResourceType, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.IdPerson);

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
