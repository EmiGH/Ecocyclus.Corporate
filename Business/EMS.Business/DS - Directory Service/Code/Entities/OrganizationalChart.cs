using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.DS.Entities
{
    public class OrganizationalChart
    {
        #region Internal Properties
            private Credential _Credential;
            private Int64 _IdOrganizationalChart;
            private Int64 _IdOrganization;
            private String _IdLanguage;
            private Entities.OrganizationalChart_LG _LanguageOption; //Objeto con los datos dependientes del idioma actual elegido por el usuario
            private Collections.OrganizationalCharts_LG _LanguagesOptions; //Coleccion con los datos dependientes del idioma actual elegido por el usuario
        #endregion

        #region External Properties
            public Int64 IdOrganization
            {
                get { return _IdOrganization; }
            }
            public Organization Organization
            {
                get
                { return new Collections.Organizations(_Credential).Item(_IdOrganization); }
            }
            public Int64 IdOrganizationalChart
            {
                get { return _IdOrganizationalChart; }
            }
            public OrganizationalChart_LG LanguageOption
            {
                get { return _LanguageOption; }
            }
            public Collections.OrganizationalCharts_LG LanguagesOptions
            {
                get
                {
                    if (_LanguagesOptions == null)
                    {
                        //Carga la coleccion de lenguages de es posicion
                        _LanguagesOptions = new Collections.OrganizationalCharts_LG(this,_Credential);
                    }

                    return _LanguagesOptions;
                }
            }


            #region JobTitles
            public Entities.JobTitle JobTitle(GeographicFunctionalArea geographicFunctionalArea, FunctionalPosition functionalPosition)
            {
                return new Condesus.EMS.Business.DS.Collections.JobTitles(this).Item(geographicFunctionalArea.IdOrganization, geographicFunctionalArea.IdGeographicArea, geographicFunctionalArea.IdFunctionalArea,functionalPosition.IdPosition);
            }
            public List<Entities.JobtitleWithChart> JobTitlesRoots()
            {
                return new Collections.JobTitles(this).ItemsRoot();
            }
            /// <summary>
            /// Da de alta el jobtitle een el chart y en la base de datos
            /// </summary>
            /// <param name="idGeographicArea"></param>
            /// <param name="idPosition"></param>
            /// <param name="idFunctionalArea"></param>
            /// <param name="idGeographicAreaParent"></param>
            /// <param name="idPositionParent"></param>
            /// <param name="idFunctionalAreaParent"></param>
            public Entities.JobTitle JobTitlesAdd(Int64 idGeographicArea, Int64 idPosition, Int64 idFunctionalArea, Int64 idGeographicAreaParent, Int64 idPositionParent, Int64 idFunctionalAreaParent)
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    Collections.JobTitles _jobTitles = new Condesus.EMS.Business.DS.Collections.JobTitles(this);
                    Entities.JobTitle _jobtitle =  _jobTitles.Add(idGeographicArea, idPosition, idFunctionalArea, idGeographicAreaParent, idPositionParent, idFunctionalAreaParent);
                    _transactionScope.Complete();
                    return _jobtitle;

                }
            }
            /// <summary>
            /// Solo borra la relacion con el chart, pero no lo saca de la base de datos
            /// </summary>
            /// <param name="jobTitle"></param>
            public void Remove(JobTitle jobTitle)
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    Collections.JobTitles _jobTitles = new Condesus.EMS.Business.DS.Collections.JobTitles(this);
                    _jobTitles.RemoveRelationship(jobTitle);
                    _transactionScope.Complete();
                }                
            }
            #endregion
        #endregion

            internal OrganizationalChart(Int64 idOrganizationalChart, Int64 idOrganization, String name, String description, String idLanguage, Credential credential) 
        {
            _Credential = credential;
            _IdOrganizationalChart = idOrganizationalChart;
            _IdOrganization = idOrganization;
            _IdLanguage = idLanguage;
            _LanguageOption = new OrganizationalChart_LG(name, description, idLanguage); 
        }

            public OrganizationalChart Modify(String name, String description)
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                new Collections.OrganizationalCharts(Organization).Modify(this,name, description);
                _LanguageOption = new OrganizationalChart_LG(name, description, _IdLanguage);
                
                _transactionScope.Complete();
                return this;
                }   
            }

        /// <summary>
        /// Borra sus dependencias
        /// </summary>
        internal void Remove()
        {
            //Borra las relaciones con jobtitle
            new Collections.JobTitles(this).Remove();
        }
    }
}
