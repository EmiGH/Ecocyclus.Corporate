using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.PF.Collections
{
    internal class ProcessGroupProcesses
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

            internal ProcessGroupProcesses(Credential credential)
        {
            _Credential = credential;
        }

            #region Read Functions

            internal Entities.ProcessGroupProcess Item(Int64 idProcess)
            {

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadById(idProcess, _Credential.CurrentLanguage.IdLanguage);

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {

                    return new PF.Factories.ProjectsFactory(_Credential).CreateProcessGroupProcess(idProcess, Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));
                    
                }
                return null;
            }
            internal Dictionary<Int64, Entities.ProcessGroupProcess> Items()
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadAll(_Credential.CurrentLanguage.IdLanguage);
                //Se modifica con los datos que realmente se tienen que usar...

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                PF.Factories.ProjectsFactory _ProjectsFactory = new PF.Factories.ProjectsFactory(_Credential);
                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia una posicion
                    Entities.ProcessGroupProcess _processGroupProcess = _ProjectsFactory.CreateProcessGroupProcess(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);

                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessGroupProcess> Items(Int64 idClassification)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadByClassification(idClassification, _Credential.CurrentLanguage.IdLanguage, Common.Security.Process, _Credential.User.IdPerson);

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                PF.Factories.ProjectsFactory _ProjectsFactory = new PF.Factories.ProjectsFactory(_Credential);
                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia una posicion
                    Entities.ProcessGroupProcess _processGroupProcess = _ProjectsFactory.CreateProcessGroupProcess(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);

                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessGroupProcess> Items(GIS.Entities.Site site)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadByFacility(_Credential.CurrentLanguage.IdLanguage, _Credential.User.IdPerson, site.IdFacility);

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                PF.Factories.ProjectsFactory _ProjectsFactory = new PF.Factories.ProjectsFactory(_Credential);
                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia una posicion
                    Entities.ProcessGroupProcess _processGroupProcess = _ProjectsFactory.CreateProcessGroupProcess(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);

                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessGroupProcess> Items(Entities.ProcessGroupProcess processGroupProcess)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadProcessParticipationsByProcess(processGroupProcess.IdProcess, _Credential.CurrentLanguage.IdLanguage, Common.Security.Process, _Credential.User.IdPerson);

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                PF.Factories.ProjectsFactory _ProjectsFactory = new PF.Factories.ProjectsFactory(_Credential);
                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia una posicion
                    Entities.ProcessGroupProcess _processGroupProcess = _ProjectsFactory.CreateProcessGroupProcess(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);

                }
                return _oItems;
            }

            internal Dictionary<Int64, Entities.ProcessGroupProcess> Items(GIS.Entities.GeographicArea geographicArea)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadByGeographicArea(geographicArea.IdGeographicArea, _Credential.CurrentLanguage.IdLanguage, Common.Security.Process, _Credential.User.IdPerson);

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                PF.Factories.ProjectsFactory _ProjectsFactory = new PF.Factories.ProjectsFactory(_Credential);
                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia una posicion
                    Entities.ProcessGroupProcess _processGroupProcess = _ProjectsFactory.CreateProcessGroupProcess(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);

                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessGroupProcess> Items(Int64 idGeographicAreaParent, Int64 idProcessClassification)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadByGeographicAreaLayerAndProcessType(idGeographicAreaParent, idProcessClassification, _Credential.CurrentLanguage.IdLanguage, Common.Security.Process, _Credential.User.IdPerson);

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                PF.Factories.ProjectsFactory _ProjectsFactory = new PF.Factories.ProjectsFactory(_Credential);
                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia una posicion
                    Entities.ProcessGroupProcess _processGroupProcess = _ProjectsFactory.CreateProcessGroupProcess(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);

                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessGroupProcess> Items(PF.Entities.ProcessClassification clasification, GIS.Entities.GeographicArea geographicArea)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadByClassificationAndGeographicArea(clasification.IdProcessClassification, geographicArea.IdGeographicArea, _Credential.CurrentLanguage.IdLanguage, Common.Security.Process, _Credential.User.IdPerson);

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                PF.Factories.ProjectsFactory _ProjectsFactory = new PF.Factories.ProjectsFactory(_Credential);
                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia una posicion
                    Entities.ProcessGroupProcess _processGroupProcess = _ProjectsFactory.CreateProcessGroupProcess(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);

                }
                return _oItems;
            }

            internal Dictionary<Int64, Entities.ProcessGroupProcess> Items(DS.Entities.Organization organization)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadByOrganization(organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage, Common.Security.Process, _Credential.User.IdPerson);

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                PF.Factories.ProjectsFactory _ProjectsFactory = new PF.Factories.ProjectsFactory(_Credential);
                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia una posicion
                    Entities.ProcessGroupProcess _processGroupProcess = _ProjectsFactory.CreateProcessGroupProcess(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);

                }
                return _oItems;
            }            
            internal Dictionary<Int64, Entities.ProcessGroupProcess> Items(PF.Entities.ProcessClassification clasification, DS.Entities.Organization organization)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadByClassificationAndOrganization(clasification.IdProcessClassification, organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage, Common.Security.Process, _Credential.User.IdPerson);

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                PF.Factories.ProjectsFactory _ProjectsFactory = new PF.Factories.ProjectsFactory(_Credential);
                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia una posicion
                    Entities.ProcessGroupProcess _processGroupProcess = _ProjectsFactory.CreateProcessGroupProcess(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);

                }
                return _oItems;
            }


            internal Dictionary<Int64, Entities.ProcessGroupProcess> ReadRoot()
            {
                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();
                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadRoot(_Credential.CurrentLanguage.IdLanguage, Common.Security.Process, _Credential.User.IdPerson);
                //Se modifica con los datos que realmente se tienen que usar...
                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                PF.Factories.ProjectsFactory _ProjectsFactory = new PF.Factories.ProjectsFactory(_Credential);
                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia una posicion
                    Entities.ProcessGroupProcess _processGroupProcess = _ProjectsFactory.CreateProcessGroupProcess(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);

                }
                return _oItems;
            }

            internal Dictionary<Int64, Entities.ProcessClassification> ClassificationByProcess(Entities.ProcessGroupProcess process)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessClassification> _oItems = new Dictionary<Int64, Entities.ProcessClassification>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.ProcessGroupProcesses _dbProcessGroupProcesses = _dbProcessesFramework.ProcessGroupProcesses;

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ClassificationsByProject(process.IdProcess, _Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdProcessClassification"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdProcessClassification"]));
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
                            Entities.ProcessClassification _processClassification = new Entities.ProcessClassification(Convert.ToInt64(_dbRecord["IdProcessClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentProcessClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                            //Lo agrego a la coleccion
                            _oItems.Add(_processClassification.IdProcessClassification, _processClassification);
                        }
                        _oInsert = true;
                    }
                    else
                    {
                        //Declara e instancia  
                        Entities.ProcessClassification _processClassification = new Entities.ProcessClassification(Convert.ToInt64(_dbRecord["IdProcessClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentProcessClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_processClassification.IdProcessClassification, _processClassification);
                    }

                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessGroupProcess> ItemsByCalcualtion(PA.Entities.Calculation calculation)
            {
                          //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadByCalculations(calculation.IdCalculation);

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia  
                    Entities.ProcessGroupProcess _processGroupProcess = Item(Convert.ToInt64(_dbRecord["IdProcess"]));
                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);
                }
                return _oItems;
            }
            internal Dictionary<Int64, Entities.ProcessGroupProcess> ItemsByPost(DS.Entities.Person person)
            {
                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadByPerson(_Credential.CurrentLanguage.IdLanguage,person.IdPerson,"Process");

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                PF.Factories.ProjectsFactory _ProjectsFactory = new PF.Factories.ProjectsFactory(_Credential);
                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia una posicion
                    Entities.ProcessGroupProcess _processGroupProcess = _ProjectsFactory.CreateProcessGroupProcess(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);

                }
                return _oItems;

            }
            internal Dictionary<Int64, Entities.ProcessGroupProcess> ItemsByOrganizationName(String name)
            {

                //Coleccion para devolver los ExtendedProperty
                Dictionary<Int64, Entities.ProcessGroupProcess> _oItems = new Dictionary<Int64, Entities.ProcessGroupProcess>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.ProcessGroupProcesses_ReadByTitle(name, Common.Security.Process, _Credential.User.IdPerson);

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdProcess", _Credential).Filter();

                PF.Factories.ProjectsFactory _ProjectsFactory = new PF.Factories.ProjectsFactory(_Credential);
                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia  
                    Entities.ProcessGroupProcess _processGroupProcess = _ProjectsFactory.CreateProcessGroupProcess(Convert.ToInt64(_dbRecord["IdProcess"]), Convert.ToInt16(_dbRecord["Weight"]), Convert.ToInt16(_dbRecord["OrderNumber"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Title"]), Convert.ToString(_dbRecord["Purpose"]), Convert.ToString(_dbRecord["Description"]), Convert.ToInt16(_dbRecord["Threshold"]), Convert.ToString(_dbRecord["Identification"]), Convert.ToDateTime(_dbRecord["CurrentCampaignStartDate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdResourcePicture"], 0)), Convert.ToString(_dbRecord["Coordinate"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdGeographicArea"], 0)), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["TwitterUser"]), Convert.ToString(_dbRecord["FacebookUser"]));

                    //Lo agrego a la coleccion
                    _oItems.Add(_processGroupProcess.IdProcess, _processGroupProcess);

                }
                return _oItems;
            }
            #endregion

            #region ProcessGroupProcess
            #region Write Function
            internal Entities.ProcessGroupProcess Add(Int16 weight, Int16 orderNumber, String title, String purpose, String description,
                    Int16 threshold, String identification, DateTime currentCampaignStartDate, KC.Entities.ResourceCatalog resourcePicture,
                    String Coordinate, GIS.Entities.GeographicArea geographicArea, DS.Entities.Organization organization, String TwitterUser, String FacebookUser,
                    Dictionary<Int64, Entities.ProcessClassification> processClassifications)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    Int64 _idResourcePicture = resourcePicture == null ? 0 : resourcePicture.IdResource;
                    
                    Int64 _idGeographicArea = geographicArea == null ? 0 : geographicArea.IdGeographicArea;

                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idProcess = _dbProcessesFramework.Processes_Create(weight, orderNumber);

                    _dbProcessesFramework.Processes_LG_Create(_idProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

                    _dbProcessesFramework.ProcessGroups_Create(_idProcess, threshold);

                    _dbProcessesFramework.ProcessGroupProcesses_Create(_idProcess, identification, currentCampaignStartDate, _idResourcePicture, Coordinate, _idGeographicArea, organization.IdOrganization, TwitterUser, FacebookUser);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PF_ProcessGroupProcesses", "ProcessGroupProcesses", "Create", "IdProcess=" + _idProcess, _Credential.User.IdPerson);
                        
                    Entities.ProcessGroupProcess _processGroupProcess = new Factories.ProjectsFactory(_Credential).CreateProcessGroupProcess(_idProcess, weight, orderNumber, _Credential.CurrentLanguage.IdLanguage, title, purpose, description, threshold, identification, currentCampaignStartDate, _idResourcePicture, Coordinate, _idGeographicArea, organization.IdOrganization, TwitterUser, FacebookUser);

                    //seguridad: heredar los permisos que existen en el mapa
                    Entities.MapPF _mapPF = new Entities.MapPF(_Credential);
                    //_processGroupProcess.InheritPermissions(_mapPF);

                    //Recorre para todas las clasificaciones e inserta una por una.
                    foreach (PF.Entities.ProcessClassification _processClassification in processClassifications.Values)
                    {
                        _processClassification.ProcessGroupProcessAdd(_processGroupProcess);
                    }

                    //Dar de alta seguridad al creador
                    Security.Entities.Permission _permision = new Security.Collections.Permissions(_Credential).Item(Common.Permissions.Manage);
                    new Security.Collections.Rights(_Credential).Add(_processGroupProcess, _Credential.User.Person, _permision);

                    //Devuelvo el objeto creado                
                    return _processGroupProcess; 
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

            internal void Remove(Entities.ProcessGroupProcess processGroupProcess)
            {
                try
                {
                    //Borra dependencias
                    processGroupProcess.Remove();
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                    
                    //Borrar el process group process
                    _dbProcessesFramework.ProcessGroupProcesses_Delete(processGroupProcess.IdProcess);
                    //Borrar el process group
                    _dbProcessesFramework.ProcessGroups_Delete(processGroupProcess.IdProcess);
                    //Borrar el process 
                    _dbProcessesFramework.Processes_Delete(processGroupProcess.IdProcess);
                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PF_ProcessGroupProcesses", "ProcessGroupProcesses", "Delete", "IdProcess=" + processGroupProcess.IdProcess, _Credential.User.IdPerson);

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

            internal void Modify(Entities.ProcessGroupProcess process, Int16 weight, Int16 orderNumber, String title, String purpose, String description, Int16 threshold,
                            String identification, DateTime currentCampaignStartDate, KC.Entities.ResourceCatalog resourcePicture,
                            String Coordinate, GIS.Entities.GeographicArea geographicArea, DS.Entities.Organization organization,String TwitterUser, String FacebookUser,
                            Dictionary<Int64, PF.Entities.ProcessClassification> processClassifications)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    Int64 _idResourcePicture = resourcePicture == null ? 0 : resourcePicture.IdResource;
                    
                    Int64 _idGeographicArea = geographicArea == null ? 0 : geographicArea.IdGeographicArea;
                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbProcessesFramework.Processes_Update(process.IdProcess, weight, orderNumber);

                    _dbProcessesFramework.Processes_LG_Update(process.IdProcess, _Credential.DefaultLanguage.IdLanguage, title, purpose, description);

                    _dbProcessesFramework.ProcessGroups_Update(process.IdProcess, threshold);

                    _dbProcessesFramework.ProcessGroupProcesses_Update(process.IdProcess, identification, currentCampaignStartDate, _idResourcePicture, Coordinate, _idGeographicArea, organization.IdOrganization, TwitterUser, FacebookUser);

                    //Log
                    DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();
                    _dbLog.Create("PF_ProcessGroupProcesses", "ProcessGroupProcesses", "Modify", "IdProcess=" + process.IdProcess, _Credential.User.IdPerson);

                    //Borra todas las relaciones con classifications que tengo permiso 
                    foreach (Entities.ProcessClassification _classification in process.Classifications.Values)
                    {
                        _classification.Remove(process);
                    }
                    //inserta las nuevas relaciones
                    //Recorre para todas las clasificaciones e inserta una por una.
                    foreach (PF.Entities.ProcessClassification _processClassification in processClassifications.Values)
                    {
                        _processClassification.ProcessGroupProcessAdd(process);
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
        #endregion

        #region Projects related with ProcessClassification
            internal void AddRelationship(Entities.ProcessGroupProcess processGroupProcess, Entities.ProcessClassification processClassification)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbProcessesFramework.ProcessGroupProcesses_Create(processGroupProcess.IdProcess, processClassification.IdProcessClassification);
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
            internal void RemoveRelationship(Entities.ProcessGroupProcess processGroupProcess, Entities.ProcessClassification processClassification)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    //Borrar de la base de datos
                    _dbProcessesFramework.ProcessGroupProcesses_Delete(processGroupProcess.IdProcess, processClassification.IdProcessClassification);
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

        #region Projects related with Forum
            internal void AddRelationship(Entities.ProcessGroupProcess processGroupProcess, CT.Entities.Forum Forum)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    _dbProcessesFramework.ProcessGroupProcessForums_Create(processGroupProcess.IdProcess, Forum.IdForum);
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
            internal void RemoveRelationship(Entities.ProcessGroupProcess processGroupProcess, CT.Entities.Forum Forum)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();

                    //Borrar de la base de datos
                    _dbProcessesFramework.ProcessGroupProcesses_Delete(processGroupProcess.IdProcess, Forum.IdForum);
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
    }
}
