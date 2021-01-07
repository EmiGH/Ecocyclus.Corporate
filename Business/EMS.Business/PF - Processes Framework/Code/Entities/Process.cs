using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Entities
{
    public abstract class Process : IExtendedProperty
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdProcess;
            private Int16 _Weight;
            private Int16 _OrderNumber;            
            private Process_LG _LanguageOption;//puntero a lenguages
            private Collections.Processes_LG _LanguagesOptions;//puntero a lenguajes
            //private List<ProcessPost> _ProcessPosts;
            private List<ProcessResource> _ProcessResourceFiles;
            private List<ProcessResource> _ProcessResources;
            private ProcessGroupProcess _Project;
            private DS.Entities.Organization _Organization;
        #endregion

        #region External Properties
            internal Credential Credential
            {
                get { return _Credential; }
            }

            public Int64 IdProcess
            {
                get { return _IdProcess; }
            }
            public Int16 Weight
            {
                get { return _Weight; }              
            }
            public Int16 OrderNumber
            {
                get { return _OrderNumber; }
            }
           
            public Process_LG LanguageOption
            {
                get
                {
                    return _LanguageOption;
                }               
            }
            public Collections.Processes_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de es posicion
                        _LanguagesOptions = new Collections.Processes_LG(_IdProcess, _Credential);
                    }

                    return _LanguagesOptions;
                }
            }

            public abstract ProcessGroupProcess Parent
            {
                get;
            }

            /// <summary>
            /// devuelve el padre ya sea node o ProcessGroupProcess
            /// </summary>
            //public abstract Process Parent
            //{
            //    get;
            //}
            /// <summary>
            /// Devuelve el ProcessGroupProcess al q pertenece este process
            /// </summary>
            //public ProcessGroupProcess ProcessGroupProcess
            //{
            //    get
            //    {                    
            //        if (_Project == null)
            //        {
            //            if (this is ProcessGroupProcess)
            //            { _Project = (ProcessGroupProcess)this; }
            //            else
            //            { _Project = (ProcessGroupProcess)FindProjet(this.Parent); }                        
            //        }
            //        return (ProcessGroupProcess)_Project;
            //    }
            //}
            //private Process FindProjet(Process process)
            //{
            //    switch (process.GetType().Name)
            //    {
            //        case "ProcessGroupProcess":
            //            return process;
                        
            //        case "ProcessGroupException":
            //            return FindProjet(((ProcessGroupException)process).associatedtask); 
                        
            //        default:
            //            return FindProjet(process.Parent); 
                        
            //    }
            //    //if (process is ProcessGroupProjectStandard)
            //    //{ return process; }
            //    //else
            //    //{ return FindProjet(process.ProcessParent); }                
            //}
            

            #region Extended Properties
            private List<EP.Entities.ExtendedPropertyValue> _ExtendedPropertyValue; //puntero a extended properties          
            public List<EP.Entities.ExtendedPropertyValue> ExtendedPropertyValues
                {
                    get
                    {
                        if (_ExtendedPropertyValue == null)
                        { _ExtendedPropertyValue = new EP.Collections.ExtendedPropertyValues(this).Items(); }
                        return _ExtendedPropertyValue;
                    }
                }
            public EP.Entities.ExtendedPropertyValue ExtendedPropertyValue(Int64 idExtendedProperty)
                {
                    return new EP.Collections.ExtendedPropertyValues(this).Item(idExtendedProperty);
                }
            public void ExtendedPropertyValueAdd(EP.Entities.ExtendedProperty extendedProperty, String value)
                {
                    new EP.Collections.ExtendedPropertyValues(this).Add(extendedProperty, value);
                }
            public void Remove(EP.Entities.ExtendedPropertyValue extendedPropertyValue)
                {
                    new EP.Collections.ExtendedPropertyValues(this).Remove(extendedPropertyValue);
                }
            public void ExtendedPropertyValueModify(EP.Entities.ExtendedPropertyValue extendedPropertyValue, String value)
                {
                    new EP.Collections.ExtendedPropertyValues(this).Modify(extendedPropertyValue,value);
                }
            #endregion

            #region Resources
                public List<ProcessResource> ProcessResources
                {
                    get
                    {
                        if (_ProcessResources == null)
                        { _ProcessResources = new Collections.ProcessResources(_Credential).Items(this); }
                        return _ProcessResources;
                    }
                }
                public ProcessResource ProcessResource(Int64 idResource)
                {
                    return new Collections.ProcessResources(_Credential).Item(_IdProcess, idResource);
                }
                public List<ProcessResource> ProcessResourceFiles
                {
                    get
                    {
                        if (_ProcessResourceFiles == null)
                        {
                            List<ProcessResource> _ProcessResources = new Collections.ProcessResources(_Credential).Items(this);
                            foreach (ProcessResource _processResource in _ProcessResources)
                            {
                                if (_processResource.Resource.GetType().Name == Common.Constants.TypeResourceFile)
                                { _ProcessResourceFiles.Add(_processResource); }
                            }
                        }
                        return _ProcessResourceFiles;
                    }
                }


                public void ResourceAdd(KC.Entities.Resource resource, String comment)
                {
                    new Collections.ProcessResources(_Credential).Add(this, resource, comment);
                }
                public void Remove(ProcessResource processResource)
                {
                    new Collections.ProcessResources( _Credential).Remove(processResource);
                }
                public void ResourceModify(Int64 idResource, String comment)
                {
                    new Collections.ProcessResources(_Credential).Item(_IdProcess, idResource).Modify(comment);
                }
              #endregion


            //#region Process Posts
            //public List<PF.Entities.ProcessPost> ProcessPosts(Int64 idPerson)
            //    {
            //        if (_ProcessPosts == null)
            //        { 
            //            _ProcessPosts = new PF.Collections.ProcessPosts(_Credential).Items(_IdProcess, idPerson); 
            //        }
            //        return _ProcessPosts;
            //    }
            //    public PF.Entities.ProcessPost ProcessPost(Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea)
            //    {
            //        return new PF.Collections.ProcessPosts(_Credential).Item(_IdProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea);
            //    }
            //    public PF.Entities.ProcessPost ProcessPostsAdd(Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime startDate, DateTime endDate, Int64 idPermission)
            //    {
            //        PF.Entities.ProcessPost _processPost = new PF.Collections.ProcessPosts(_Credential).Add(_IdProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea, startDate, endDate, idPermission);

            //        if (_ProcessPosts != null)
            //        {
            //            _ProcessPosts.Add(_processPost);
            //        }
            //        return _processPost;
            //    }
            //    public void ProcessPostsRemove(Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea)
            //    {
            //        Collections.ProcessPosts _processPosts = new PF.Collections.ProcessPosts( _Credential);
            //        _processPosts.Remove(_IdProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea);
                    
            //        if (_ProcessPosts != null)
            //        {
            //            _ProcessPosts.Remove(_processPosts.Item(_IdProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea));
            //        }
            //    }
            //    public void ProcessPostsModify(Int64 idPerson, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicArea, DateTime startDate, DateTime endDate, Int64 idPermission)
            //    {
            //        new PF.Collections.ProcessPosts(_Credential).Modify(_IdProcess, idPerson, idPosition, idFunctionalArea, idGeographicArea, startDate, endDate, idPermission);
            //    }
            //#endregion

                public abstract String State
                {
                    get;
                }
                public abstract DateTime StartDate
                {
                    get;
                }
                public abstract DateTime EndDate
                {
                    get;
                }
                public abstract String Result
                {
                    get;
                }
                public abstract Int32 Completed
                {
                    get;
                }

                public abstract Dictionary<Int64, IA.Entities.Exception> Exceptions
                {
                    get;
                }

        #endregion

        internal Process(Int64 idProcess, Int16 weight, Int16 orderNumber, String idLanguage, String title, String purpose, String description, Credential credential)
        {
            _Credential = credential;
            _IdProcess = idProcess;
            _Weight = weight;
            _OrderNumber = orderNumber;
            _LanguageOption = new Process_LG(idLanguage, title, purpose, description);            
        }

        /// <summary>
        /// Borra dependencias
        /// </summary>
        internal virtual void Remove()
        {
            //Borra resources
            foreach (Entities.ProcessResource _processResource in ProcessResources)
            {
                this.Remove(_processResource);             
            }
            //Borra las extended properties
            foreach (EP.Entities.ExtendedPropertyValue _extendedPropertyValue in ExtendedPropertyValues)
            {
                this.Remove(_extendedPropertyValue);
            }
            //Borra los LG
            new Collections.Processes_LG(_IdProcess, Credential).Remove();
        }

    }
}
