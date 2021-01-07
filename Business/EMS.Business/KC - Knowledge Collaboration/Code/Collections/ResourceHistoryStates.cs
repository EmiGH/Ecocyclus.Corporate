using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.KC.Collections
{
    public class ResourceHistoryStates

    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

            internal ResourceHistoryStates(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            //Trae el estado pedido
            public Entities.ResourceHistoryState Item(Int64 idResourceHistoryState)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            //Entities.ResourceFileState _Resource = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceHistoryStates_ReadById(idResourceHistoryState, _Credential.CurrentLanguage.IdLanguage);
            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                return new Entities.ResourceHistoryState(Convert.ToInt64(_dbRecord["IdResourceFileState"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
            }
            return null;
        }
            //Trae todos los estados
            public Dictionary<Int64, Entities.ResourceHistoryState> Items()
        {

            //Objeto de data layer para acceder a datos
            DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.ResourceHistoryState> _oItems = new Dictionary<Int64, Entities.ResourceHistoryState>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbKnowledgeCollaboration.ResourceHistoryStates_ReadAll(_Credential.CurrentLanguage.IdLanguage);
       
            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _bInsert = true;

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdResourceFileState"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdResourceFileState"]));
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
                    Entities.ResourceHistoryState _ResourceHistoryState = new Entities.ResourceHistoryState(Convert.ToInt64(_dbRecord["IdResourceFileState"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_ResourceHistoryState.IdResourceFileState, _ResourceHistoryState);
                }
                _bInsert = true;
            }
            return _oItems;
            
        }

        #endregion

        #region Write Functions
            public Entities.ResourceHistoryState Add(String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();
                 
                    ////aca es donde se arma una transaccion y primero se graba la tarea de recovery
                    //y luego se graban las fechas de medicion que se habilitan para recuperar.
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {   
       
                        //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idResourceHistoryState = _dbKnowledgeCollaboration.ResourceHistoryStates_Create(_Credential.User.IdPerson);

                        _dbKnowledgeCollaboration.ResourceHistoryStates_LG_Create(_idResourceHistoryState, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.IdPerson);

                        _transactionScope.Complete();

                        //Devuelvo el objeto FunctionalArea creado
                        return new Entities.ResourceHistoryState(_idResourceHistoryState, name, description, _Credential);
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
            public void Remove(Int64 idResourceHistoryState)
            {
                try
                {
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //aca es donde se arma una transaccion y primero se graba la tarea de recovery
                    //y luego se graban las fechas de medicion que se habilitan para recuperar.
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        _dbKnowledgeCollaboration.ResourceHistoryStates_LG_Delete(idResourceHistoryState, _Credential.User.IdPerson);

                        //Borrar de la base de datos
                        _dbKnowledgeCollaboration.ResourceHistoryStates_Delete(idResourceHistoryState, _Credential.User.IdPerson);

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
            public void Modify(Int64 idResourceHistoryState, String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.KC.KnowledgeCollaboration _dbKnowledgeCollaboration = new Condesus.EMS.DataAccess.KC.KnowledgeCollaboration();

                    //Modifico los datos de la base
                    _dbKnowledgeCollaboration.ResourceHistoryStates_LG_Update(idResourceHistoryState, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
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
