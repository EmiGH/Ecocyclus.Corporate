using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Condesus.EMS.Business.Security.Collections
{
    public class Permissions
    {
        #region Internal Properties
            private Credential _Credential;        
        #endregion

            internal Permissions(Credential credential)
        {
            _Credential = credential;
        }

            #region Read Functions
            internal Entities.Permission Item(Int64 idPermission)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                Entities.Permission _permission = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Permissions_ReadById(idPermission, _Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_permission == null)
                    {
                        _permission = new Entities.Permission(Convert.ToInt64(_dbRecord["IdPermission"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _permission;
                        }
                    }
                    else
                    {
                        return new Entities.Permission(Convert.ToInt64(_dbRecord["IdPermission"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                    }
                }
                return _permission;
            }
            //internal Entities.Permission Item(Int64 idPermission, String className, Int64 idObject)
            //{
            //    //Objeto de data layer para acceder a datos
            //    DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

            //    Entities.Permission _permission = null;
            //    IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Permissions_ReadById(idPermission, _Credential.CurrentLanguage.IdLanguage);
            //    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            //    {
            //        if (_permission == null)
            //        {
            //            _permission = new Entities.Permission(Convert.ToInt64(_dbRecord["IdPermission"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
            //            if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
            //            {
            //                return _permission;
            //            }
            //        }
            //        else
            //        {
            //            return new Entities.Permission(Convert.ToInt64(_dbRecord["IdPermission"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
            //        }
            //    }
            //    return _permission;
            //}

            internal Dictionary<Int64, Entities.Permission> Items()
            {
                //Objeto de data layer para acceder a datos
                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                //Coleccion para devolver las clases
                Dictionary<Int64, Entities.Permission> _oItems = new Dictionary<Int64, Entities.Permission>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Permissions_ReadAll(_Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _bInsert = true;
                //busca si hay mas de un id modulo igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdPermission"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper().Trim() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdPermission"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _bInsert = false;
                        }

                        //Solo inserta si es necesario.
                        if (_bInsert)
                        {
                            //Declara e instancia un modulo
                            Entities.Permission _oPermission = new Entities.Permission(Convert.ToInt64(_dbRecord["IdPermission"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                            //Lo agrego a la coleccion
                            _oItems.Add(_oPermission.IdPermission, _oPermission);
                        }
                        _bInsert = true;
                    }
                    else
                    {
                        //Declara e instancia un modulo
                        Entities.Permission _oPermission = new Entities.Permission(Convert.ToInt64(_dbRecord["IdPermission"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_oPermission.IdPermission, _oPermission);
                    }
                }
                return _oItems;
            }
            //esta funcion devuelve los permisos de una entidad
            internal Dictionary<Int64, Entities.Permission> Items(ISecurity securityObject)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();

                //Coleccion para devolver las clases
                Dictionary<Int64, Entities.Permission> _oItems = new Dictionary<Int64, Entities.Permission>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Authorize(securityObject.ClassName,securityObject.IdObject,_Credential.User.IdPerson,_Credential.CurrentLanguage.IdLanguage);

                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    //Declara e instancia un modulo
                    Entities.Permission _oPermission = Item(Convert.ToInt64(_dbRecord["IdPermission"]));
                    //Lo agrego a la coleccion
                    _oItems.Add(_oPermission.IdPermission, _oPermission);
                }
                return _oItems;
            }
            #endregion

    }
}
