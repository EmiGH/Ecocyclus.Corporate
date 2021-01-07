using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;


namespace Condesus.EMS.Business.DS.Collections
{
    public class FunctionalAreas_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdFunctionalArea; //El identificador de la area funcional
            private Int64 _IdOrganization;      
        #endregion

        internal FunctionalAreas_LG(Int64 idFunctionalArea, Int64 idOrganization, Credential credential)
        {
            _Credential = credential;
            _IdFunctionalArea = idFunctionalArea;                        
            _IdOrganization = idOrganization;
        }

        #region Read Functions
            public Entities.FunctionalArea_LG  Item(String idLanguage)
            {                
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
             
                Entities.FunctionalArea_LG _functionalArea_LG = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.FunctionalAreas_LG_ReadById(_IdFunctionalArea, _IdOrganization,  idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_functionalArea_LG == null)
                    {
                        _functionalArea_LG = new Entities.FunctionalArea_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["IdLanguage"]));
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _functionalArea_LG;
                        }
                    }
                    else
                    {
                        return new Entities.FunctionalArea_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["IdLanguage"]));
                    }
                }
                return _functionalArea_LG;
                
            }
            public Dictionary<String, Entities.FunctionalArea_LG> Items()
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Dictionary<String, Entities.FunctionalArea_LG> _oFunctionalAreas_LG = new Dictionary<String, Entities.FunctionalArea_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.FunctionalAreas_LG_ReadAll(_IdFunctionalArea, _IdOrganization);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.FunctionalArea_LG _oFunctionalArea_LG = new Entities.FunctionalArea_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["IdLanguage"]));
                    _oFunctionalAreas_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oFunctionalArea_LG);
                }

                return _oFunctionalAreas_LG;
            }
        #endregion

        #region Write Functions
            public Entities.FunctionalArea_LG Add(String idLanguage, String name)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbDirectoryServices.FunctionalAreas_LG_Create(_IdFunctionalArea, _IdOrganization, idLanguage, name, _Credential.User.Person.IdPerson);
                    return new Entities.FunctionalArea_LG(name, idLanguage);
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
            public Entities.FunctionalArea_LG Modify(String idLanguage, String name )
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.FunctionalAreas_LG_Update(_IdFunctionalArea, _IdOrganization, idLanguage, name, _Credential.User.Person.IdPerson);
                    return new Entities.FunctionalArea_LG(name, idLanguage);
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
            public void Remove(String idLanguage)
            {
                //controla que no se borre el lenguage default
                if (_Credential.DefaultLanguage.IdLanguage == idLanguage)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
                }
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.FunctionalAreas_LG_Delete(_IdFunctionalArea, _IdOrganization, idLanguage, _Credential.User.Person.IdPerson);
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
