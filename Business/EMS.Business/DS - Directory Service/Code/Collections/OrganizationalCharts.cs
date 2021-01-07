using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class OrganizationalCharts
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.Organization _Organization; //Organizacion a la que pertenece
        #endregion

        internal OrganizationalCharts(Entities.Organization organization)
        {
            _Credential = organization.Credential;
            _Organization = organization;
        }

        #region Read Functions
        internal Entities.OrganizationalChart Item(Int64 idOrganizationalChart)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.OrganizationalChart _organizationalChart = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationalCharts_ReadById(idOrganizationalChart, _Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage);
                //Se modifica con los datos que realmente se tienen que usar...
                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdOrganizationalChart", _Credential).Filter();

                //si no trae nada retorno 0 para que no de error
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    return new Entities.OrganizationalChart(Convert.ToInt64(_dbRecord["IdOrganizationalChart"]), _Organization.IdOrganization, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                }
                return _organizationalChart;
            }
        internal Dictionary<Int64, Entities.OrganizationalChart> Items()
            {
                //Coleccion para devolver los roles
                Dictionary<Int64, Entities.OrganizationalChart> _oItems = new Dictionary<Int64, Entities.OrganizationalChart>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationalCharts_ReadAll(_Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage);

                Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdOrganizationalChart", _Credential).Filter();

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
                {
                    //Declara e instancia una posicion
                    Entities.OrganizationalChart _organizationalChart = new Entities.OrganizationalChart(Convert.ToInt64(_dbRecord["IdOrganizationalChart"]), _Organization.IdOrganization, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_organizationalChart.IdOrganizationalChart, _organizationalChart);

                }
                return _oItems;
            }
        internal Dictionary<Int64, Entities.OrganizationalChart> Items(Entities.JobTitle jobTitle)
        {
            //Coleccion para devolver los roles
            Dictionary<Int64, Entities.OrganizationalChart> _oItems = new Dictionary<Int64, Entities.OrganizationalChart>();

            //Objeto de data layer para acceder a datos
            DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.OrganizationalCharts_ReadByJobTitle(jobTitle.IdGeographicArea, jobTitle.IdFunctionalArea, jobTitle.IdPosition, jobTitle.IdOrganization);

            Dictionary<Object, System.Data.Common.DbDataRecord> _recordFilter = new Common.FilterLanguages(_record, "IdOrganizationalChart", _Credential).Filter();

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _recordFilter.Values)
            {
                //Declara e instancia una posicion
                Entities.OrganizationalChart _organizationalChart = new Entities.OrganizationalChart(Convert.ToInt64(_dbRecord["IdOrganizationalChart"]), _Organization.IdOrganization, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);

                //Lo agrego a la coleccion
                _oItems.Add(_organizationalChart.IdOrganizationalChart, _organizationalChart);

            }
            return _oItems;
        }                
        #endregion

        #region Write Functions
            #region Write Roles
                internal Entities.OrganizationalChart Add(String name, String description)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                        //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                        Int64 _idOrganizationalChart = _dbDirectoryServices.OrganizationalCharts_Create(_Organization.IdOrganization);

                        _dbDirectoryServices.OrganizationalCharts_LG_Create(_idOrganizationalChart, _Organization.IdOrganization, _Credential.DefaultLanguage.IdLanguage, name, description);

                        _dbLog.Create("DS_OrganizationalCharts", "OrganizationalCharts", "Add", "IdOrganizationalChart=" + _idOrganizationalChart + " and IdOrganization=" + _Organization.IdOrganization, _Credential.User.IdPerson);
                        //Devuelvo el objeto creado
                        return new Entities.OrganizationalChart(_idOrganizationalChart, _Organization.IdOrganization, name, description, _Credential.DefaultLanguage.IdLanguage, _Credential);
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
                internal void Remove(Entities.OrganizationalChart organizationalChart)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                        //Borra todas sus dependencias
                        organizationalChart.Remove();
                        //LG
                        _dbDirectoryServices.OrganizationalCharts_LG_Delete(organizationalChart.IdOrganizationalChart, _Organization.IdOrganization);
                        //Borrar de la base de datos
                        _dbDirectoryServices.OrganizationalCharts_Delete(_Organization.IdOrganization, organizationalChart.IdOrganizationalChart);

                        _dbLog.Create("DS_OrganizationalCharts", "OrganizationalCharts", "Remove", "IdOrganizationalChart=" + organizationalChart.IdOrganizationalChart + " and IdOrganization=" + _Organization.IdOrganization, _Credential.User.IdPerson);

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

                internal void Modify(Entities.OrganizationalChart organizationalChart, String name, String description)
                {
                    try
                    {
                        //Objeto de data layer para acceder a datos
                        DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                        DataAccess.Log.Log _dbLog = new Condesus.EMS.DataAccess.Log.Log();

                        //Modifico los datos de la base
                        _dbDirectoryServices.OrganizationalCharts_LG_Update(organizationalChart.IdOrganizationalChart, _Organization.IdOrganization, _Credential.DefaultLanguage.IdLanguage, name, description);

                        _dbLog.Create("DS_OrganizationalCharts", "OrganizationalCharts", "Modify", "IdOrganizationalChart=" + organizationalChart.IdOrganizationalChart + " and IdOrganization=" + _Organization.IdOrganization, _Credential.User.IdPerson);

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
    }
}
