using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{   
    internal class SalutationTypes
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal SalutationTypes(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Functions
            internal Entities.SalutationType Item(Int64 idSalutationType)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                Entities.SalutationType _SalutationType = null;

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.SalutationTypes_ReadById(idSalutationType, _Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_SalutationType == null)
                    {
                        _SalutationType = new Entities.SalutationType(Convert.ToInt64(_dbRecord["IdSalutationType"]), Convert.ToString(_dbRecord["IdLanguage"]),Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]) != _Credential.DefaultLanguage.IdLanguage) 
                        { 
                            return _SalutationType; 
                        }
                    }
                    else 
                    {
                        return new Entities.SalutationType(Convert.ToInt64(_dbRecord["IdSalutationType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                    }
                }
                return _SalutationType;
            }
            internal Dictionary<Int64, Entities.SalutationType> Items()
            {
                //Coleccion para devolver los SalutationType
                Dictionary<Int64, Entities.SalutationType> _oItems = new Dictionary<Int64, Entities.SalutationType>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                
                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.SalutationTypes_ReadAll(_Credential.CurrentLanguage.IdLanguage);
                
                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;
                //busca si hay mas de un id SalutationType igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdSalutationType"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdSalutationType"]));
                            //No debe insertar en la coleccion ya que existe el idioma correcto.                          
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }
                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia un SalutationType 
                            Entities.SalutationType _oSalutationType = new Entities.SalutationType(Convert.ToInt64(_dbRecord["IdSalutationType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);

                            //Lo agrego a la coleccion
                            _oItems.Add(_oSalutationType.IdSalutationType, _oSalutationType);
                        }
                        _oInsert = true;
                    }
                    else
                    {
                        //Declara e instancia un SalutationType 
                        Entities.SalutationType _oSalutationType = new Entities.SalutationType(Convert.ToInt64(_dbRecord["IdSalutationType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);

                        //Lo agrego a la coleccion
                        _oItems.Add(_oSalutationType.IdSalutationType, _oSalutationType);
                    }
                }
                return _oItems;
            }
        #endregion

        #region Write Functions
            internal Entities.SalutationType Add(String idLanguage, String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _oIdSalutationType = _dbDirectoryServices.SalutationTypes_Create(idLanguage, name, description, _Credential.User.Person.IdPerson);

                    //Devuelvo el objeto país creado
                    return new Entities.SalutationType(_oIdSalutationType, _Credential.DefaultLanguage.IdLanguage, name, description, _Credential);
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
            internal void Remove(Int64 idSalutationType)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Borrar de la base de datos
                    _dbDirectoryServices.SalutationTypes_Delete(idSalutationType, _Credential.User.Person.IdPerson);
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
            internal void Modify(Int64 idSalutationType, String idLanguage, String name, String description)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Modifico los datos de la base
                    _dbDirectoryServices.SalutationTypes_Update(idSalutationType, idLanguage, name, description, _Credential.User.Person.IdPerson);
                }
                catch (SqlException  ex)
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
