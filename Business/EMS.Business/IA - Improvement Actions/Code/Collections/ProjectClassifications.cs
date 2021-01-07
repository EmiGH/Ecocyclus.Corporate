using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.IA.Collections
{
    public class ProjectClassifications
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.ProjectClassification _Parent;
        #endregion

        internal ProjectClassifications(Credential credential)
        {
            _Credential = credential;
            _Parent = null;
        }
        internal ProjectClassifications(Entities.ProjectClassification parent, Credential credential)
        {
            _Credential = credential;
            _Parent = parent;
        }

        //encapsula la toma de decision de cual es el idparent
        private Int64 IdParent
        {
            get
            {
                if (_Parent == null) { return 0; } else { return _Parent.IdProjectClassification; }
            }
        }


        #region Read Functions
        internal Entities.ProjectClassification Item(Int64 idProjectClassification)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

            Entities.ProjectClassification _ProjectClassification = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.ProjectClassifications_ReadById(idProjectClassification, _Credential.CurrentLanguage.IdLanguage);
            //si no trae nada retorno 0 para que no de error
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if ((IdParent == 0) || (Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdProjectClassification"], 0)) == IdParent))
                {
                    if (_ProjectClassification == null)
                    {
                        _ProjectClassification = new Entities.ProjectClassification(Convert.ToInt64(_dbRecord["IdProjectClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentProjectClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _ProjectClassification;
                        }
                    }
                    else
                    {
                        return new Entities.ProjectClassification(Convert.ToInt64(_dbRecord["IdProjectClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentProjectClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                    }
                }
                else
                {
                    //no es hijo asi que no lo puede devolver......generar el error
                    return null;
                }
            }
            return _ProjectClassification;
        }
        internal Dictionary<Int64, Entities.ProjectClassification> Items()
        {
            DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.ProjectClassification> _oItems = new Dictionary<Int64, Entities.ProjectClassification>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record;
            if (IdParent == 0)
            { _record = _dbImprovementActions.ProjectClassifications_ReadRoot(_Credential.CurrentLanguage.IdLanguage); }
            else
            { _record = _dbImprovementActions.ProjectClassifications_ReadByParent(IdParent, _Credential.CurrentLanguage.IdLanguage); }

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _bInsert = true;

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdProjectClassification"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdProjectClassification"]));
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
                    Entities.ProjectClassification _ProjectClassification = new Entities.ProjectClassification(Convert.ToInt64(_dbRecord["IdProjectClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentProjectClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_ProjectClassification.IdProjectClassification, _ProjectClassification);
                }
                _bInsert = true;
            }
            return _oItems;
        }

        internal Dictionary<Int64, Entities.ProjectClassification> Items(Entities.Project project)
        {
            DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

            //Coleccion para devolver las areas funcionales
            Dictionary<Int64, Entities.ProjectClassification> _oItems = new Dictionary<Int64, Entities.ProjectClassification>();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.ProjectClassifications_ReadByProject(project.IdProject, _Credential.CurrentLanguage.IdLanguage);

            //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
            Boolean _bInsert = true;

            //busca si hay mas de un id de area funcional igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdProjectClassification"])))
                {
                    ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                    if ((Convert.ToString(_dbRecord["IdLanguage"])).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        _oItems.Remove(Convert.ToInt64(_dbRecord["IdProjectClassification"]));
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
                    Entities.ProjectClassification _ProjectClassification = new Entities.ProjectClassification(Convert.ToInt64(_dbRecord["IdProjectClassification"]), Convert.ToInt64(Common.Common.CastNullValues(_dbRecord["IdParentProjectClassification"], 0)), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);

                    //Lo agrego a la coleccion
                    _oItems.Add(_ProjectClassification.IdProjectClassification, _ProjectClassification);
                }
                _bInsert = true;
            }
            return _oItems;
        }
        #endregion

        #region Write Functions
        internal Entities.ProjectClassification Add(String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();
                DataAccess.Log.Log _dbLog = new DataAccess.Log.Log();

                //TODO: ojo el exec q ejecuta esto anula la transaccion
                //if (_dbLog.GenericExist("IA_ProjectClassifications_LG", "Name = '" + name + "'"))
                //{
                //    throw new Exception(Common.Resources.Errors.DuplicatedDataRecord);
                //}  
           
                //ejecuta el insert y devuelve el identificador que le asigno en la base de datos
                Int64 _idProjectClassifications = _dbImprovementActions.ProjectClassifications_Create(IdParent);

                _dbImprovementActions.ProjectClassifications_LG_Create(_idProjectClassifications, _Credential.DefaultLanguage.IdLanguage, name, description);

                _dbLog.Create("IA_ProjectClassifications", "ProjectClassifications", "Add", "IdProjectClassification = " + _idProjectClassifications, _Credential.User.IdPerson);

                Entities.ProjectClassification _projectClassification = new Entities.ProjectClassification(_idProjectClassifications, IdParent, name, description, _Credential);

                //Devuelvo el objeto FunctionalArea creado
                return _projectClassification;
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
        internal void Remove(Entities.ProjectClassification projectClassification)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();
                DataAccess.Log.Log _dbLog = new DataAccess.Log.Log();
                
                //Borra las hijas
                projectClassification.Remove();
                //Borra la relacion con projects
                _dbImprovementActions.ProjectClassifications_DeleteRelationships(projectClassification.IdProjectClassification);
                //Borra los lg
                _dbImprovementActions.ProjectClassifications_LG_Delete(projectClassification.IdProjectClassification);
                //Borrar de la base de datos
                _dbImprovementActions.ProjectClassifications_Delete(projectClassification.IdProjectClassification);
                //Log
                _dbLog.Create("IA_ProjectClassifications", "ProjectClassifications", "Delete", "IdProjectClassification = " + projectClassification.IdProjectClassification, _Credential.User.IdPerson);

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

        internal void Modify(Entities.ProjectClassification projectClassification, String name, String description)
        {
            try
            {
                //Objeto de data layer para acceder a datos
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();
                DataAccess.Log.Log _dbLog = new DataAccess.Log.Log();

                _dbImprovementActions.ProjectClassifications_LG_Update(projectClassification.IdProjectClassification, _Credential.DefaultLanguage.IdLanguage, name, description);

                _dbLog.Create("IA_ProjectClassifications", "ProjectClassifications", "Modify", "IdProjectClassification = " + projectClassification.IdProjectClassification, _Credential.User.IdPerson);

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
        internal void Modify(Entities.ProjectClassification projectClassification)
        {
            try
            {
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();
                DataAccess.Log.Log _dbLog = new DataAccess.Log.Log();

                //Seguridad: hereda de la del padre, si no tiene padre hereda del mapa
                if (_Parent == null)
                {
                    _dbImprovementActions.ProjectClassifications_Update(projectClassification.IdProjectClassification, 0);
                }
                else
                {
                    _dbImprovementActions.ProjectClassifications_Update(projectClassification.IdProjectClassification, IdParent);
                }

                _dbLog.Create("IA_ProjectClassifications", "ProjectClassifications", "Modify", "IdProjectClassification = " + projectClassification.IdProjectClassification, _Credential.User.IdPerson);

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
