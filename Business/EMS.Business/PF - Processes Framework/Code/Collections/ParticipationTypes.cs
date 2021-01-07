using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PF.Collections
{
    public class ParticipationTypes
    {
        #region Internal Properties
            private Credential _Credential;         
        #endregion

            internal ParticipationTypes(Credential credential)
        {
            _Credential = credential;            
        }

         

        #region Read Functions
            //Trae el tipo pedido
            internal Entities.ParticipationType Item(Int64 idParticipationType)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
            //DataAccess.PF.Entities.ParticipationTypes _dbParticipationTypes = _dbProcessesFramework.ParticipationTypes;
            //DataAccess.PF.ParticipationTypes _dbParticipationTypes = new Condesus.EMS.DataAccess.PF.ParticipationTypes();

            Entities.ParticipationType _ParticipationType = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ParticipationTypes_ReadById(idParticipationType, _Credential.CurrentLanguage.IdLanguage);
            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {                
                if (_ParticipationType == null)
                {
                    _ParticipationType = new Entities.ParticipationType(Convert.ToInt64(_dbRecord["IdParticipationType"]), Convert.ToString(_dbRecord["Name"]), _Credential);
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _ParticipationType;
                    }
                }
                else
                {
                    return new Entities.ParticipationType(Convert.ToInt64(_dbRecord["IdParticipationType"]), Convert.ToString(_dbRecord["Name"]), _Credential);
                }             
            }
            return _ParticipationType;
        }
            //Trae todos los tipos
            internal Dictionary<Int64, Entities.ParticipationType> Items()
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ParticipationTypes _dbParticipationTypes = _dbProcessesFramework.ParticipationTypes;
                //DataAccess.PF.ParticipationTypes _dbParticipationTypes = new Condesus.EMS.DataAccess.PF.ParticipationTypes();

                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.ParticipationType> _oItems = new Dictionary<Int64, Entities.ParticipationType>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ParticipationTypes_ReadAll(_Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _bInsert = true;

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdParticipationType"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdParticipationType"]));
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
                        Entities.ParticipationType _participationType = new Entities.ParticipationType(Convert.ToInt64(_dbRecord["IdParticipationType"]), Convert.ToString(_dbRecord["Name"]), _Credential);

                        //Lo agrego a la coleccion
                        _oItems.Add(_participationType.IdParticipationType, _participationType);
                    }
                    _bInsert = true;
                }
                return _oItems;
            }

        #endregion

        #region Write Functions
            internal Entities.ParticipationType Add(String name)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    //DataAccess.PF.ParticipationTypes _dbParticipationTypes = new Condesus.EMS.DataAccess.PF.ParticipationTypes();
                    //DataAccess.PF.Entities.ParticipationTypes _dbParticipationTypes = _dbProcessesFramework.ParticipationTypes;
                    //DataAccess.PF.ParticipationTypes_LG _dbParticipationTypes_LG = _dbProcessesFramework.ParticipationTypes_LG();
                    //DataAccess.PF.ParticipationTypes_LG _dbParticipationTypes_LG = new Condesus.EMS.DataAccess.PF.ParticipationTypes_LG();
                    //aca es donde se arma una transaccion y primero se graba la tarea de recovery
                    //y luego se graban las fechas de medicion que se habilitan para recuperar.
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idParticipationTypes = _dbProcessesFramework.ParticipationTypes_Create(_Credential.User.IdPerson);

                    _dbProcessesFramework.ParticipationTypes_LG_Create(_idParticipationTypes, _Credential.DefaultLanguage.IdLanguage, name, _Credential.User.IdPerson);

                    _transactionScope.Complete();

                    //Devuelvo el objeto FunctionalArea creado
                    return new Entities.ParticipationType(_idParticipationTypes, name, _Credential);

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
            internal void Remove(Int64 idParticipationTypes)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    //DataAccess.PF.Entities.ParticipationTypes _dbParticipationTypes = _dbProcessesFramework.ParticipationTypes;
                    //DataAccess.PF.ParticipationTypes _dbParticipationTypes = new Condesus.EMS.DataAccess.PF.ParticipationTypes();
                    //DataAccess.PF.Entities.ParticipationTypes_LG _dbParticipationTypes_LG = _dbProcessesFramework.ParticipationTypes_LG;
                    //DataAccess.PF.ParticipationTypes_LG _dbParticipationTypes_LG = new Condesus.EMS.DataAccess.PF.ParticipationTypes_LG();
                    //aca es donde se arma una transaccion y primero se graba la tarea de recovery
                    //y luego se graban las fechas de medicion que se habilitan para recuperar.
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        //Borrar de la base de datos
                        //Primero la LG
                        _dbProcessesFramework.ParticipationTypes_LG_Delete(idParticipationTypes, _Credential.User.IdPerson);
                        //Segundo la entidad.
                        _dbProcessesFramework.ParticipationTypes_Delete(idParticipationTypes, _Credential.User.IdPerson);

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
            internal void Modify(Int64 idParticipationTypes, String name)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    //DataAccess.PF.Entities.ParticipationTypes _dbParticipationTypes = _dbProcessesFramework.ParticipationTypes;
                    //DataAccess.PF.ParticipationTypes _dbParticipationTypes = new Condesus.EMS.DataAccess.PF.ParticipationTypes();
                    //DataAccess.PF.Entities.ParticipationTypes_LG _dbParticipationTypes_LG = _dbProcessesFramework.ParticipationTypes_LG;
                    //DataAccess.PF.ParticipationTypes_LG _dbParticipationTypes_LG = new Condesus.EMS.DataAccess.PF.ParticipationTypes_LG();
                    //aca es donde se arma una transaccion y primero se graba la tarea de recovery
                    //y luego se graban las fechas de medicion que se habilitan para recuperar.
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        //Modifico los datos de la base
                        //_dbParticipationTypes.Update(idParticipationTypes, _Credential.User.IdPerson);

                        _dbProcessesFramework.ParticipationTypes_LG_Update(idParticipationTypes, _Credential.DefaultLanguage.IdLanguage, name, _Credential.User.IdPerson);

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
