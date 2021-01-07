using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class FunctionalAreas
    {
        #region Internal Properties
            private Credential _Credential;           
            private Entities.Organization _Organization;
            private Entities.FunctionalArea _Parent;
        #endregion

            internal FunctionalAreas(Entities.Organization organization)        
        {
            _Credential = organization.Credential;
            _Organization = organization;             
        }
            internal FunctionalAreas(Entities.FunctionalArea parent, Entities.Organization organization)
        {
            _Organization = organization;
            _Parent = parent;
            _Credential = parent.Credential;
        }

        //encapsula la toma de decision de cual es el idparent
        private Int64 IdParent
        {            
            get
            {            
                if (_Parent == null) { return 0; } else { return _Parent.IdFunctionalArea; }
            }
        }
        #region Read Functions
            internal Entities.FunctionalArea Item(Int64 idFunctionalArea)
            {         
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                Entities.FunctionalArea _functionalArea = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.FunctionalAreas_ReadById(idFunctionalArea, _Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage);
                //si no trae nada retorno 0 para que no de error
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if ((IdParent == 0)||(Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentFunctionalArea"], 0)) == IdParent))
                    {
                        if (_functionalArea == null)
                        {
                            _functionalArea = new Entities.FunctionalArea(Convert.ToInt64(_dbRecord["IdFunctionalArea"]), _Organization.IdOrganization, Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentFunctionalArea"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Mnemo"]), _Credential);
                            if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                            {
                                return _functionalArea;
                            }
                        }
                        else
                        {
                            return new Entities.FunctionalArea(Convert.ToInt64(_dbRecord["IdFunctionalArea"]), _Organization.IdOrganization, Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentFunctionalArea"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Mnemo"]), _Credential);
                        }
                    }
                    else
                    {
                        //no es hijo asi que no lo puede devolver......generar el error
                        return null;
                    }
                }
                return _functionalArea; 
            }
            internal Dictionary<Int64, Entities.FunctionalArea> Items()
            {
                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                //Coleccion para devolver las areas funcionales
                Dictionary<Int64, Entities.FunctionalArea> _oItems = new Dictionary<Int64, Entities.FunctionalArea>();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record;
                if (IdParent == 0)
                { _record = _dbDirectoryServices.FunctionalAreas_GetRoot(_Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage); }
                else
                { _record = _dbDirectoryServices.FunctionalAreas_GetByParent(IdParent, _Organization.IdOrganization, _Credential.CurrentLanguage.IdLanguage); }
                
                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _bInsert = true;

                //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdFunctionalArea"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdFunctionalArea"]));
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
                        Entities.FunctionalArea _oFunctionalArea = new Entities.FunctionalArea(Convert.ToInt64(_dbRecord["IdFunctionalArea"]), Convert.ToInt64(_dbRecord["IdOrganization"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentFunctionalArea"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Mnemo"]), _Credential);

                        //Lo agrego a la coleccion
                        _oItems.Add(_oFunctionalArea.IdFunctionalArea, _oFunctionalArea);
                    }
                    _bInsert = true;
                }
                return _oItems;
            }
        #endregion

        #region Write Functions
            internal Entities.FunctionalArea Add(String name, String mnemo)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                    Int64 _idFunctionalArea = _dbDirectoryServices.FunctionalAreas_Create(_Organization.IdOrganization, IdParent, _Credential.DefaultLanguage.IdLanguage, mnemo, name, _Credential.User.Person.IdPerson);

                    //Devuelvo el objeto FunctionalArea creado
                    return new Entities.FunctionalArea(_idFunctionalArea, _Organization.IdOrganization, IdParent, name, mnemo, _Credential);
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
            internal void Remove(Entities.FunctionalArea functionalArea)
            {
                //if (functionalArea.Children.Count != 0) //TODO: Analizar el borrado en cascada
                //{
                //    throw new DuplicateNameException(Condesus.EMS.Business.Common.Resources.Errors.RemoveDependency);
                //}
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                    //Borra sus dependencias
                    functionalArea.Remove();
                    //Borrar de la base de datos
                    _dbDirectoryServices.FunctionalAreas_Delete(functionalArea.IdFunctionalArea, _Organization.IdOrganization, _Credential.User.Person.IdPerson);
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
            internal void Modify(Entities.FunctionalArea functionalArea, String name, String mnemo)
            {
                try
                {
                    //Objeto de data layer para acceder a datos
                    DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();

                    //Modifico los datos de la base
                    _dbDirectoryServices.FunctionalAreas_Update(functionalArea.IdFunctionalArea, IdParent, _Organization.IdOrganization, _Credential.DefaultLanguage.IdLanguage, name, mnemo, _Credential.User.Person.IdPerson);
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
