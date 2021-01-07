using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    public class Positions_LG
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdPosition; //El identificador de la posicion
            private Int64 _IdOrganization; 
        #endregion

        internal Positions_LG(Int64 idPosition, Int64 idOrganization, Credential credential)
        {
            _Credential = credential;
            _IdPosition = idPosition;                        
            _IdOrganization = idOrganization;
        }

        #region Read Functions
            public Entities.Position_LG Item(String idLanguage)
            {
                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.Position_LG _position_LG = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Positions_LG_ReadById(_IdPosition, _IdOrganization, idLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_position_LG == null)
                    {
                        _position_LG = new Entities.Position_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.CurrentLanguage.IdLanguage.ToUpper())
                        {
                            return _position_LG;
                        }
                    }
                    else
                    {
                        return new Entities.Position_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                    }
                }
                return _position_LG;
            }
            public Dictionary<String, Entities.Position_LG> Items()
            {

                //Acceso a datos para la opción de idioma
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Dictionary<String, Entities.Position_LG> _oPositions_LG = new Dictionary<String, Entities.Position_LG>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Positions_LG_ReadAll(_IdPosition, _IdOrganization);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    Entities.Position_LG _oPosition_LG = new Entities.Position_LG(Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]));
                    _oPositions_LG.Add(Convert.ToString(_dbRecord["IdLanguage"]), _oPosition_LG);
                }

                return _oPositions_LG;
            }
        #endregion

        #region Write Functions
            public Entities.Position_LG Add(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert a la base y devuelve el ID que le asigno en la bd            
                    _dbDirectoryServices.Positions_LG_Create(_IdPosition, _IdOrganization, idLanguage, description, name, _Credential.User.Person.IdPerson);
                    return new Entities.Position_LG(name, description, idLanguage);
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
            public Entities.Position_LG Modify(String idLanguage, String name, String description)
            {
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.Positions_LG_Update(_IdPosition, _IdOrganization, idLanguage, name, description, _Credential.User.Person.IdPerson);
                    return new Entities.Position_LG(name, description, idLanguage);
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
                if (_Credential.CurrentLanguage.IdLanguage == idLanguage)
                {
                    throw new Exception(Common.Resources.Errors.RemoveDefaultCulture);
                }
                try
                {
                    //Acceso a datos para la opción de idioma
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    _dbDirectoryServices.Positions_LG_Delete(_IdPosition, _IdOrganization, idLanguage, _Credential.User.Person.IdPerson);
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
