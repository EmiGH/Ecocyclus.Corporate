using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Condesus.EMS.Business.PF.Collections
{
    public class ProcessParticipations
    {
            #region Internal Properties
            private Credential _Credential;
            private Entities.Process _Process;
            private DS.Entities.Organization _Organization;
        #endregion

        /// <summary>
        /// para Crearlo desde un process
        /// </summary>
        /// <param name="credential"></param>
        /// <param name="process"></param>
        internal ProcessParticipations(Credential credential, Entities.Process process)
        {
            _Credential = credential;
            _Process = process;
            _Organization = null;
        }
        /// <summary>
        /// Para crearlo desde una organizacion
        /// </summary>
        /// <param name="credential"></param>
        /// <param name="organization"></param>
        internal ProcessParticipations(Credential credential, DS.Entities.Organization organization)
        {
            _Credential = credential;
            _Organization = organization;
            _Process = null;
        }

         

        #region Read Functions
            //Trae el tipo pedido
            internal Entities.ProcessParticipation Item(Int64 idOrganization, Int64 IdParticipationsType)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
            //DataAccess.PF.Entities.ProcessParticipations _dbProcessParticipations = _dbProcessesFramework.ProcessParticipations;

            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessParticipations_ReadById(_Process.IdProcess, idOrganization, IdParticipationsType);
            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                return new Entities.ProcessParticipation(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(_dbRecord["IdParticipationType"]), Convert.ToString(_dbRecord["Comment"]), _Credential);
            }
            return null;
        }
            //Trae todos los tipos
            internal List<Entities.ProcessParticipation> Items()
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessParticipations _dbProcessParticipations = _dbProcessesFramework.ProcessParticipations;

                //Coleccion para devolver las areas funcionales
                List<Entities.ProcessParticipation> _oItems = new List<Entities.ProcessParticipation>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessParticipations_ReadByProcess(_Process.IdProcess);

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {                    
                    //Declara e instancia una posicion
                    Entities.ProcessParticipation _projectParticipation = new Entities.ProcessParticipation(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(_dbRecord["IdParticipationType"]), Convert.ToString(_dbRecord["Comment"]), _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_projectParticipation);
                
                }
                return _oItems;
            }

        #endregion

        #region Write Functions
            internal Entities.ProcessParticipation Add(Int64 idOrganization, Int64 idParticipationType, String comment)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    //DataAccess.PF.Entities.ProcessParticipations _dbProcessParticipations = _dbProcessesFramework.ProcessParticipations;

                    
                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbProcessesFramework.ProcessParticipations_Create(_Process.IdProcess, idOrganization, idParticipationType, comment, _Credential.User.IdPerson);

                    return new Entities.ProcessParticipation(_Process.IdProcess, idOrganization, idParticipationType, comment, _Credential);

                    
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
            internal void Remove(Entities.ProcessParticipation processParticipation)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    _dbProcessesFramework.ProcessParticipations_Delete(_Process.IdProcess, processParticipation.Organization.IdOrganization, processParticipation.IdParticipationType, _Credential.User.IdPerson);
  
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
            /// <summary>
            /// Borra por proceso
            /// </summary>
            internal void RemoveByProcess()
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    //DataAccess.PF.Entities.ProcessParticipations _dbProcessParticipations = _dbProcessesFramework.ProcessParticipations;

                    _dbProcessesFramework.ProcessParticipations_DeleteByProcess(_Process.IdProcess);

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
        /// <summary>
        /// Borra por organization
        /// </summary>
            internal void RemoveByOrganization()
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    //DataAccess.PF.Entities.ProcessParticipations _dbProcessParticipations = _dbProcessesFramework.ProcessParticipations;

                    _dbProcessesFramework.ProcessParticipations_DeleteByOrganization(_Organization.IdOrganization);

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

            internal void Modify(Int64 idOrganization, Int64 idParticipationTypes, String comment)
            {
                try
                {
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    //DataAccess.PF.Entities.ProcessParticipations _dbProcessParticipations = _dbProcessesFramework.ProcessParticipations;

                    _dbProcessesFramework.ProcessParticipations_Update(_Process.IdProcess, idOrganization, idParticipationTypes, comment, _Credential.User.IdPerson);

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
