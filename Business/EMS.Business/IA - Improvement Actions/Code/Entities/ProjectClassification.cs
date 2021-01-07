using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.IA.Entities
{
    public class ProjectClassification
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdProjectClassification;
            private Int64 _IdParentProjectClassification;
            private ProjectClassification_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.ProjectClassifications_LG _LanguagesOptions; //Coleccion con los datos dependientes del idioma actual elegido por el usuario
            private Dictionary<Int64, Entities.ProjectClassification> _Children; //Coleccion de hijas
            private Dictionary<Int64, Project> _Projects;
        #endregion

        #region External Properties
            public Int64 IdProjectClassification
            {
                get { return _IdProjectClassification; }
            }
            public Int64 IdParentProjectClassification
            {
                get { return _IdParentProjectClassification; }
            }

            public ProjectClassification_LG  LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Collections.ProjectClassifications_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de es posicion
                        _LanguagesOptions = new Collections.ProjectClassifications_LG(_IdProjectClassification, _Credential);
                    }

                    return _LanguagesOptions;
                }
            }

            public Dictionary<Int64, Entities.ProjectClassification> ChildrenClassifications
            {
                get
                {
                    if (_Children == null)
                    {
                        _Children = new Collections.ProjectClassifications(this, _Credential).Items();
                    }

                    return _Children;
                }
            }
            public Dictionary<Int64, Project> ChildrenElements
            {
                get
                {
                    if (_Projects == null)
                    { _Projects = new Collections.Projects(_Credential).Items(_IdProjectClassification); }
                    return _Projects;
                }
            }

            /// <summary>
            /// Borra las clasificaciones hijas 
            /// </summary>
            internal void Remove()
            {
                Collections.ProjectClassifications _projectClassifications = new IA.Collections.ProjectClassifications(_Credential);
                foreach (ProjectClassification _projectClassification in ChildrenClassifications.Values)
                {
                    _projectClassifications.Remove(_projectClassification);
                }
            }
            //public IA.Entities.Project Project(Int64 idProject)
            //{
            //    return new IA.Collections.Projects(idOrganization, _Credential).Item(idProject);
            //}
            
        #endregion

            internal ProjectClassification(Int64 idProjectClassification, Int64 idParentProjectClassification, String name, String description, Credential credential)
        {
            _Credential = credential;
            _IdProjectClassification = idProjectClassification;
            _IdParentProjectClassification = idParentProjectClassification;
            //Carga el nombre para el lenguage seleccionado
            _LanguageOption = new ProjectClassification_LG(name, description, _Credential.CurrentLanguage.IdLanguage); 
        }


            /// <summary>
            /// Modifica el parent (para el drug&drop)
            /// </summary>
            /// <param name="parent"></param>
            public void Move(ProjectClassification parent)
            {

                //Manejo de la transaccion
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    new Collections.ProjectClassifications(parent, _Credential).Modify(this);
                    _transactionScope.Complete();
                }
            }
            /// <summary>
            /// modifica solo el nombre y description
            /// </summary>
            /// <param name="name"></param>
            /// <param name="description"></param>
            public void Modify(String name, String description)
            {
                //Manejo de la transaccion
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    new Collections.ProjectClassifications(_Credential).Modify(this, name, description);
                    _transactionScope.Complete();
                }
            }
    }
}
