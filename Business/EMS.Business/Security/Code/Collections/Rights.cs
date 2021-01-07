using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.Security.Collections
{
    public class Rights
    {
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal Rights(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
        
        internal Entities.RightJobTitle ReadByJobTitleAndObject(DS.Entities.JobTitle jobTitle, ISecurity Object)
        {
            ////Objeto de data layer para acceder a datos
            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

            //Coleccion para devolver las areas funcionales
            Entities.RightJobTitle _oItem = null;

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Rights_ReadByJobTitleAndObject(jobTitle.Organization.IdOrganization, jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, jobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea, jobTitle.FunctionalPositions.Position.IdPosition, Object.ClassName, Object.IdObject);

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Permission _permission = new Collections.Permissions(_Credential).Item(Convert.ToInt64(_dbRecord["IdPermission"]));

                Entities.RightJobTitle _rightJobTitle = new Entities.RightJobTitle(_permission, jobTitle);
                //Lo agrego a la coleccion
                _oItem = _rightJobTitle;
            }
            return _oItem;

        }

        internal List<Entities.RightPerson> ReadPersonByObject(ISecurity Object)
        {
            ////Objeto de data layer para acceder a datos
            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

            //Coleccion para devolver las areas funcionales
            List<Entities.RightPerson> _oItems = new List<Entities.RightPerson>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Rights_ReadPersonByObject(Object.ClassName, Object.IdObject);

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                DS.Entities.Organization _organization = new DS.Collections.Organizations(_Credential).Item(Convert.ToInt64(_dbRecord["IdOrganization"]));
                //DS.Entities.FunctionalArea _functionalArea = new DS.Collections.FunctionalAreas(_organization).Item(Convert.ToInt64(_dbRecord["IdFunctionalArea"]));
                //DS.Entities.Position _position = new DS.Collections.Positions(_organization).Item(Convert.ToInt64(_dbRecord["IdPosition"]));
                //GIS.Entities.GeographicArea _geographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(Convert.ToInt64(_dbRecord["IdGeographicArea"]));
                //DS.Entities.GeographicFunctionalArea _geographicFunctionalArea = new DS.Collections.GeographicFunctionalAreas(_organization).Item(_functionalArea,_geographicArea);
                //DS.Entities.FunctionalPosition _functionalPosition = new DS.Collections.FunctionalPositions(_organization).Item(_position, _functionalArea);
                //DS.Entities.JobTitle _jobTitle = new DS.Collections.JobTitles(_organization).Item(_geographicFunctionalArea,_functionalPosition);
                DS.Entities.Person _person = new DS.Collections.People(_organization).Item(Convert.ToInt64(_dbRecord["IdPerson"]));

                Entities.Permission _permission = new Collections.Permissions(_Credential).Item(Convert.ToInt64(_dbRecord["IdPermission"]));

                Entities.RightPerson _rightPerson = new Entities.RightPerson(_permission, _person);
                //Lo agrego a la coleccion
                _oItems.Add(_rightPerson);
            }
            return _oItems;

        }
        internal List<Entities.RightJobTitle> ReadJobTitleByObject(ISecurity Object)
        {
            ////Objeto de data layer para acceder a datos
            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

            //Coleccion para devolver las areas funcionales
            List<Entities.RightJobTitle> _oItems = new List<Entities.RightJobTitle>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Rights_ReadJobTitleByObject(Object.ClassName, Object.IdObject);

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                //DS.Entities.Organization _organization = new DS.Collections.Organizations(_Credential).Item(Convert.ToInt64(_dbRecord["IdOrganization"]));
                //DS.Entities.Position _position = new DS.Collections.Positions(_organization).Item(Convert.ToInt64(_dbRecord["IdPosition"]));
                //DS.Entities.FunctionalArea _functionalArea = new DS.Collections.FunctionalAreas(_organization).Item(Convert.ToInt64(_dbRecord["IdFunctionalArea"]));
                //GIS.Entities.GeographicArea _geographicArea = new GIS.Collections.GeographicAreas(_Credential).Item(Convert.ToInt64(_dbRecord["IdGeographicArea"]));
                //DS.Entities.GeographicFunctionalArea _geographicFunctionalArea = new DS.Collections.GeographicFunctionalAreas(_organization).Item(_functionalArea, _geographicArea);
                //DS.Entities.FunctionalPosition _functionalPosition = new DS.Collections.FunctionalPositions(_organization).Item(_position, _functionalArea);

                DS.Entities.JobTitle _jobTitle = new DS.Collections.JobTitles(_Credential).Item(Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(_dbRecord["IdGeographicArea"]), Convert.ToInt64(_dbRecord["IdFunctionalArea"]), Convert.ToInt64(_dbRecord["IdPosition"]));

                Entities.Permission _permission = new Collections.Permissions(_Credential).Item(Convert.ToInt64(_dbRecord["IdPermission"]));

                Entities.RightJobTitle _rightJobTitle = new Entities.RightJobTitle(_permission, _jobTitle);
                //Lo agrego a la coleccion
                _oItems.Add(_rightJobTitle);
            }
            return _oItems;

        }

        internal List<Entities.Right> ReadClassByJobTitleAndClassName(DS.Entities.JobTitle jobTitle, String ClassName)
        {
            ////Objeto de data layer para acceder a datos
            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

            //Coleccion para devolver las areas funcionales
            List<Entities.Right> _oItems = new List<Entities.Right>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Rights_ReadByJobTitleAndClassName(jobTitle.Organization.IdOrganization, jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, jobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, jobTitle.FunctionalPositions.Position.IdPosition, ClassName);

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Permission _permission = new Collections.Permissions(_Credential).Item(Convert.ToInt64(_dbRecord["IdPermission"]));
                //Declara e instancia una posicion

                Entities.Right _right;

                switch (ClassName)
                {
                    case Common.Security.MapKC:
                        //construye el objeto del tipo correspondiente
                        KC.Entities.MapKC _mapKC = new KC.Entities.MapKC(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapKC(_permission, _mapKC);
                        break;
                    case Common.Security.MapDS:
                        //construye el objeto del tipo correspondiente
                        DS.Entities.MapDS _mapDS = new DS.Entities.MapDS(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapDS(_permission, _mapDS);
                        break;
                    case Common.Security.MapIA:
                        //construye el objeto del tipo correspondiente
                        IA.Entities.MapIA _mapIA = new IA.Entities.MapIA(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapIA(_permission, _mapIA);
                        break;
                    case Common.Security.MapRM:
                        //construye el objeto del tipo correspondiente
                        RM.Entities.MapRM _mapRM = new RM.Entities.MapRM(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapRM(_permission, _mapRM);
                        break;
                    case Common.Security.MapPA:
                        //construye el objeto del tipo correspondiente
                        PA.Entities.MapPA _mapPA = new PA.Entities.MapPA(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapPA(_permission, _mapPA);
                        break;
                    case Common.Security.MapPF:
                        //construye el objeto del tipo correspondiente
                        PF.Entities.MapPF _mapPF = new PF.Entities.MapPF(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapPF(_permission, _mapPF);
                        break;
                    case Common.Security.ConfigurationDS:
                        //construye el objeto del tipo correspondiente
                        DS.Entities.ConfigurationDS _configurationDS = new DS.Entities.ConfigurationDS(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationDS(_permission, _configurationDS);
                        break;
                    case Common.Security.ConfigurationIA:
                        //construye el objeto del tipo correspondiente
                        IA.Entities.ConfigurationIA _ConfigurationIA = new IA.Entities.ConfigurationIA(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationIA(_permission, _ConfigurationIA);
                        break;
                    case Common.Security.ConfigurationKC:
                        //construye el objeto del tipo correspondiente
                        KC.Entities.ConfigurationKC _configurationKC = new KC.Entities.ConfigurationKC(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationKC(_permission, _configurationKC);
                        break;
                    case Common.Security.ConfigurationPA:
                        //construye el objeto del tipo correspondiente
                        PA.Entities.ConfigurationPA _configurationPA = new PA.Entities.ConfigurationPA(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationPA(_permission, _configurationPA);
                        break;
                    case Common.Security.ConfigurationPF:
                        //construye el objeto del tipo correspondiente
                        PF.Entities.ConfigurationPF _configurationPF = new PF.Entities.ConfigurationPF(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationPF(_permission, _configurationPF);
                        break;
                    case Common.Security.ConfigurationRM:
                        //construye el objeto del tipo correspondiente
                        RM.Entities.ConfigurationRM _configurationRM = new RM.Entities.ConfigurationRM(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationRM(_permission, _configurationRM);
                        break;
                    case Common.Security.Organization: 
                        //construye el objeto del tipo correspondiente
                        DS.Entities.Organization _Organization = new DS.Collections.Organizations(_Credential).Item(Convert.ToInt64(_dbRecord["IdObject"]));
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightOrganization(_permission, _Organization);
                        break;
                    case Common.Security.Process: 
                        //construye el objeto del tipo correspondiente
                        PF.Entities.Process _Process = new PF.Collections.Processes(_Credential).Item(Convert.ToInt64(_dbRecord["IdObject"]));
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightProcess(_permission, (PF.Entities.ProcessGroupProcess)_Process);
                        break;
                    default:
                        _right = null;
                        break;
                }
                //Lo agrego a la coleccion
                _oItems.Add(_right);
            }
            return _oItems;
        }

        internal List<Entities.Right> ReadByJobTitle(DS.Entities.JobTitle jobTitle)
        {
            ////Objeto de data layer para acceder a datos
            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

            //Coleccion para devolver las areas funcionales
            List<Entities.Right> _oItems = new List<Entities.Right>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Rights_ReadByJobTitle(jobTitle.Organization.IdOrganization, jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, jobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, jobTitle.FunctionalPositions.Position.IdPosition);

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Permission _permission = new Collections.Permissions(_Credential).Item(Convert.ToInt64(_dbRecord["IdPermission"]));
                //Declara e instancia una posicion

                Entities.Right _right;

                switch (Convert.ToString( _dbRecord["ClassName"]))
                {
                    case Common.Security.MapKC:
                        //construye el objeto del tipo correspondiente
                        KC.Entities.MapKC _mapKC = new KC.Entities.MapKC(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapKC(_permission, _mapKC);
                        break;
                    case Common.Security.MapDS:
                        //construye el objeto del tipo correspondiente
                        DS.Entities.MapDS _mapDS = new DS.Entities.MapDS(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapDS(_permission, _mapDS);
                        break;
                    case Common.Security.MapIA:
                        //construye el objeto del tipo correspondiente
                        IA.Entities.MapIA _mapIA = new IA.Entities.MapIA(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapIA(_permission, _mapIA);
                        break;
                    case Common.Security.MapRM:
                        //construye el objeto del tipo correspondiente
                        RM.Entities.MapRM _mapRM = new RM.Entities.MapRM(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapRM(_permission, _mapRM);
                        break;
                    case Common.Security.MapPA:
                        //construye el objeto del tipo correspondiente
                        PA.Entities.MapPA _mapPA = new PA.Entities.MapPA(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapPA(_permission, _mapPA);
                        break;
                    case Common.Security.MapPF:
                        //construye el objeto del tipo correspondiente
                        PF.Entities.MapPF _mapPF = new PF.Entities.MapPF(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapPF(_permission, _mapPF);
                        break;
                    case Common.Security.ConfigurationDS:
                        //construye el objeto del tipo correspondiente
                        DS.Entities.ConfigurationDS _configurationDS = new DS.Entities.ConfigurationDS(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationDS(_permission, _configurationDS);
                        break;
                    case Common.Security.ConfigurationIA:
                        //construye el objeto del tipo correspondiente
                        IA.Entities.ConfigurationIA _ConfigurationIA = new IA.Entities.ConfigurationIA(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationIA(_permission, _ConfigurationIA);
                        break;
                    case Common.Security.ConfigurationKC:
                        //construye el objeto del tipo correspondiente
                        KC.Entities.ConfigurationKC _configurationKC = new KC.Entities.ConfigurationKC(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationKC(_permission, _configurationKC);
                        break;
                    case Common.Security.ConfigurationPA:
                        //construye el objeto del tipo correspondiente
                        PA.Entities.ConfigurationPA _configurationPA = new PA.Entities.ConfigurationPA(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationPA(_permission, _configurationPA);
                        break;
                    case Common.Security.ConfigurationPF:
                        //construye el objeto del tipo correspondiente
                        PF.Entities.ConfigurationPF _configurationPF = new PF.Entities.ConfigurationPF(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationPF(_permission, _configurationPF);
                        break;
                    case Common.Security.ConfigurationRM:
                        //construye el objeto del tipo correspondiente
                        RM.Entities.ConfigurationRM _configurationRM = new RM.Entities.ConfigurationRM(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationRM(_permission, _configurationRM);
                        break;
                    case Common.Security.Organization:
                        //construye el objeto del tipo correspondiente
                        DS.Entities.Organization _Organization = new DS.Collections.Organizations(_Credential).Item(Convert.ToInt64(_dbRecord["IdObject"]));
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightOrganization(_permission, _Organization);
                        break;
                    case Common.Security.Process:
                        //construye el objeto del tipo correspondiente
                        PF.Entities.Process _Process = new PF.Collections.Processes(_Credential).Item(Convert.ToInt64(_dbRecord["IdObject"]));
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightProcess(_permission, (PF.Entities.ProcessGroupProcess)_Process);
                        break;
                    default:
                        _right = null;
                        break;
                }
                //Lo agrego a la coleccion
                _oItems.Add(_right);
            }
            return _oItems;
        }

        internal List<Entities.Right> ReadByPersonAndClassName(DS.Entities.Person person, String ClassName)
        {
            ////Objeto de data layer para acceder a datos
            DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

            //Coleccion para devolver las areas funcionales
            List<Entities.Right> _oItems = new List<Entities.Right>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Rights_ReadByPersonAndClassName(person.Organization.IdOrganization, person.IdPerson, ClassName);

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                Entities.Permission _permission = new Collections.Permissions(_Credential).Item(Convert.ToInt64(_dbRecord["IdPermission"]));
                //Declara e instancia una posicion
                
                Entities.Right _right;

                switch (ClassName)
                {
                    case Common.Security.MapKC:
                        //construye el objeto del tipo correspondiente
                        KC.Entities.MapKC _mapKC = new KC.Entities.MapKC(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapKC(_permission, _mapKC);
                        break;
                    case Common.Security.MapDS:
                        //construye el objeto del tipo correspondiente
                        DS.Entities.MapDS _mapDS = new DS.Entities.MapDS(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapDS(_permission, _mapDS);
                        break;
                    case Common.Security.MapIA:
                        //construye el objeto del tipo correspondiente
                        IA.Entities.MapIA _mapIA = new IA.Entities.MapIA(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapIA(_permission, _mapIA);
                        break;
                    case Common.Security.MapRM:
                        //construye el objeto del tipo correspondiente
                        RM.Entities.MapRM _mapRM = new RM.Entities.MapRM(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapRM(_permission, _mapRM);
                        break;
                    case Common.Security.MapPA:
                        //construye el objeto del tipo correspondiente
                        PA.Entities.MapPA _mapPA = new PA.Entities.MapPA(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapPA(_permission, _mapPA);
                        break;
                    case Common.Security.MapPF:
                        //construye el objeto del tipo correspondiente
                        PF.Entities.MapPF _mapPF= new PF.Entities.MapPF(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightMapPF(_permission, _mapPF);
                        break;
                    case Common.Security.ConfigurationDS:
                        //construye el objeto del tipo correspondiente
                        DS.Entities.ConfigurationDS _configurationDS = new DS.Entities.ConfigurationDS(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationDS(_permission, _configurationDS);
                        break;
                    case Common.Security.ConfigurationIA:
                        //construye el objeto del tipo correspondiente
                        IA.Entities.ConfigurationIA _ConfigurationIA = new IA.Entities.ConfigurationIA(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationIA(_permission, _ConfigurationIA);
                        break;
                    case Common.Security.ConfigurationKC:
                        //construye el objeto del tipo correspondiente
                        KC.Entities.ConfigurationKC _configurationKC = new KC.Entities.ConfigurationKC(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationKC(_permission, _configurationKC);
                        break;
                    case Common.Security.ConfigurationPA:
                        //construye el objeto del tipo correspondiente
                        PA.Entities.ConfigurationPA _configurationPA = new PA.Entities.ConfigurationPA(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationPA(_permission, _configurationPA);
                        break;
                    case Common.Security.ConfigurationPF:
                        //construye el objeto del tipo correspondiente
                        PF.Entities.ConfigurationPF _configurationPF = new PF.Entities.ConfigurationPF(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationPF(_permission, _configurationPF);
                        break;
                    case Common.Security.ConfigurationRM:
                        //construye el objeto del tipo correspondiente
                        RM.Entities.ConfigurationRM _configurationRM = new RM.Entities.ConfigurationRM(_Credential);
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightConfigurationRM(_permission, _configurationRM);
                        break;
                    case Common.Security.Organization: 
                        //construye el objeto del tipo correspondiente
                        DS.Entities.Organization _Organization = new DS.Collections.Organizations(_Credential).Item(Convert.ToInt64(_dbRecord["IdObject"]));
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightOrganization(_permission, _Organization);
                        break;
                    case Common.Security.Process: 
                        //construye el objeto del tipo correspondiente
                        PF.Entities.Process _Process = new PF.Collections.Processes(_Credential).Item(Convert.ToInt64(_dbRecord["IdObject"]));
                        //genera el tipo de right correspondiente
                        _right = new Entities.RightProcess(_permission, (PF.Entities.ProcessGroupProcess)_Process);
                        break;
                    default:
                        return null;
                }
                //Lo agrego a la coleccion
                _oItems.Add(_right);
            }
            return _oItems;
        }

        internal Entities.RightJobTitle ReadJobTitleByID(DS.Entities.JobTitle jobTitle, Entities.Permission permission)
        {
            //no accedo a la base, solo lo creo aca           
            return new Entities.RightJobTitle(permission, jobTitle);
        }

        internal Entities.RightPerson ReadPersonByID(DS.Entities.Person person, Entities.Permission permission)
        {
            //no accedo a la base, solo lo creo aca
            return new Entities.RightPerson(permission, person);
        }

        #endregion

        #region Write Functions

        
        internal Entities.RightPerson Add(ISecurity Object, DS.Entities.Person person, Entities.Permission permission)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                _dbSecuritySystems.Rights_Create(person.Organization.IdOrganization, person.IdPerson, permission.IdPermission, Object.ClassName, Object.IdObject);
                
                Entities.Permission _Permission = new Entities.Permission(permission.IdPermission, permission.LanguageOption.Name, permission.LanguageOption.Description, _Credential);

                return new Entities.RightPerson(_Permission, person);
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
        internal Entities.RightJobTitle Add(ISecurity Object, DS.Entities.JobTitle jobTitle, Entities.Permission permission)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                _dbSecuritySystems.Rights_Create(jobTitle.Organization.IdOrganization, jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, jobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, jobTitle.FunctionalPositions.Position.IdPosition, permission.IdPermission, Object.ClassName, Object.IdObject);

                Entities.Permission _Permission = new Entities.Permission(permission.IdPermission, permission.LanguageOption.Name, permission.LanguageOption.Description, _Credential);

                return new Entities.RightJobTitle(_Permission, jobTitle);
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

        internal void Remove(DS.Entities.Person person, ISecurity Object, Entities.Permission permission)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                _dbSecuritySystems.Rights_DeletePeople(person.Organization.IdOrganization, person.IdPerson, Object.ClassName, Object.IdObject);

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
        internal void Remove(DS.Entities.JobTitle jobTitle, ISecurity Object, Entities.Permission permission)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                _dbSecuritySystems.Rights_DeleteJobTitles(jobTitle.Organization.IdOrganization, jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, jobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, jobTitle.FunctionalPositions.Position.IdPosition, Object.ClassName, Object.IdObject);

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
        internal void Remove(DS.Entities.JobTitle jobTitle)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                _dbSecuritySystems.Rights_DeleteJobTitles(jobTitle.Organization.IdOrganization, jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea, jobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea, jobTitle.FunctionalPositions.Position.IdPosition);

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
        internal void Remove(DS.Entities.Person person)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                _dbSecuritySystems.Rights_DeletePeople(person.Organization.IdOrganization, person.IdPerson);

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

        internal void Remove(ISecurity Object)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                ////Borra los que el creo
                _dbSecuritySystems.Rights_DeleteJobTitles(Object.IdObject, Object.ClassName);
                //se borra el 
                _dbSecuritySystems.Rights_DeletePeople(Object.IdObject, Object.ClassName);

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
