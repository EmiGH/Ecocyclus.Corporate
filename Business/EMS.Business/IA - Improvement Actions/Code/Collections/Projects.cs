using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.IA.Collections
{
    internal class Projects
    {
        #region Internal Properties
        private Credential _Credential;
        private Int64 _IdClassification;  
        #endregion

        internal Projects(Credential credential)
        {
            _Credential = credential;
        }
        #region Read Functions
        internal Entities.Project Item(Int64 idProject)
        {
            //Objeto de data layer para acceder a datos
            DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

            Entities.Project _project = null;
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.Projects_ReadById(idProject);
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_project == null)
                {
                    _project = new Entities.Project(Convert.ToInt64(_dbRecord["IdProject"]), _Credential);
                    if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                    {
                        return _project;
                    }
                }
                else
                {
                    return new Entities.Project(Convert.ToInt64(_dbRecord["IdProject"]), _Credential);
                }
            }
            return _project;
        }
        internal Dictionary<Int64, Entities.Project> Items(Int64 idProjectClassification)
        {
            //Coleccion para devolver los Project
            Dictionary<Int64, Entities.Project> _oItems = new Dictionary<Int64, Entities.Project>();

            //Objeto de data layer para acceder a datos
            DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

            //Traigo los datos de la base
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.Projects_ReadAll(idProjectClassification);

 
            //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
            foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
            {
                if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdProject"])))
                {
                    //Declara e instancia  
                    Entities.Project _project = new Entities.Project(Convert.ToInt64(_dbRecord["IdProject"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_project.IdProject, _project);
                }
                else
                {   //Declara e instancia  
                    Entities.Project _project = new Entities.Project(Convert.ToInt64(_dbRecord["IdProject"]), _Credential);
                    //Lo agrego a la coleccion
                    _oItems.Add(_project.IdProject, _project);
                }

            }
            return _oItems;
        }
        #endregion
    }
}
