using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class Positions
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.Organization _Organization; //Organizacion a la que pertenece
        #endregion

        internal Positions(Entities.Organization organization)
        {
            _Credential = organization.Credential;
            _Organization = organization;
        }

        #region Read Functions
            internal Entities.Position Item(Int64 idPosition)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.Position _position = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Positions_ReadById(idPosition, _Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_position == null)
                    {
                        _position = new Entities.Position(Convert.ToInt64(_dbRecord["IdPosition"]), _Organization.IdOrganization, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _position;
                        }
                    }
                    else
                    {
                        //si no trae nada retorno 0 para que no de error
                        return new Entities.Position(Convert.ToInt64(_dbRecord["IdPosition"]), _Organization.IdOrganization, Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                    }
                }
                return _position;
            }
            internal Dictionary<Int64, Entities.Position> Items()
            {
                //Coleccion para devolver las posiciones
                Dictionary<Int64, Entities.Position> _oItems = new Dictionary<Int64, Entities.Position>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.Positions_ReadAll(_Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage);
                
                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _bInsert = true;
                //busca si hay mas de un id de posicion igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdPosition"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdPosition"]));
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
                        Entities.Position _oPosition = new Entities.Position(Convert.ToInt64(_dbRecord["IdPosition"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), Convert.ToString(_dbRecord["IdLanguage"]), _Credential);

                        //Lo agrego a la coleccion
                        _oItems.Add(_oPosition.IdPosition, _oPosition);
                    }
                    _bInsert = true;
                }
                return _oItems;
            }
        #endregion

        #region Write Functions
            internal Entities.Position Add(String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idPosition = _dbDirectoryServices.Positions_Create(_Organization.IdOrganization, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
                    //Devuelvo el objeto Position creado
                    return new Entities.Position(_idPosition, _Organization.IdOrganization, name, description, _Credential.DefaultLanguage.IdLanguage, _Credential);
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
            internal void Remove(Entities.Position position)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    
                    //Borrar de la base de datos
                    _dbDirectoryServices.Positions_Delete(position.IdPosition, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
            internal void Modify(Entities.Position position, String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Modifico los datos de la base
                    _dbDirectoryServices.Positions_Update(position.IdPosition, _Organization.IdOrganization, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential.User.Person.IdPerson);
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
