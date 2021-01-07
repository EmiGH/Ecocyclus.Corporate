using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    internal class Processes
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

            internal Processes(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Common Functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idProcess"></param>
        /// <returns></returns>
            internal Entities.Process Item(Int64 idProcess)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.Processes_ReadById(idProcess, _Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    return Factories.ProcessFactory.CreateProcess(idProcess, Convert.ToString(_dbRecord["Type"]),_Credential);
                }
                return null;
            }
        /// <summary>
        /// devuelve los process que tiene un resource, puede ser processGroupProcess, Node o Task
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
            internal Dictionary<Int64, Entities.Process> Items(KC.Entities.Resource resource)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.Process> _oItems = new Dictionary<Int64, Entities.Process>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.Processes_ReadByResource(resource.IdResource);

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {

                    //Declara e instancia una posicion
                    Entities.Process _process = Item(Convert.ToInt16(_dbRecord["IdProcess"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_process.IdProcess, _process);

                }
                return _oItems;
            }
        #endregion

        #region Write Functions
            /// <summary>
            /// Aca se maneja la cascada q depende de process
            /// </summary>
            /// <param name="process"></param>
            internal void Remove(Entities.Process process)
            {
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                DataAccess.ExtendedProperties _dbExtendedProperties = new Condesus.EMS.DataAccess.ExtendedProperties();

                //Borra resources
                _dbProcessesFramework.ProcessResources_Delete(process.IdProcess);
                //Borra las extended properties
                _dbExtendedProperties.ProcessExtendedProperties_Delete(process.IdProcess);
                //Borra los LG
                _dbProcessesFramework.Processes_LG_Delete(process.IdProcess);
                //Borra el process
                _dbProcessesFramework.Processes_Delete(process.IdProcess);
            }
        #endregion

    }
}
