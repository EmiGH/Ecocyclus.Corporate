using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Transactions;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.NT.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;
using Condesus.EMS.Business.Security.Entities;

namespace Condesus.EMS.WebUI.Wizard
{
    public partial class CreateUsersForAllOrganizationCommunity : BasePropertiesTask
    {
        #region Internal Properties
        #endregion

        #region PageLoad & Init
            protected override void InyectJavaScript()
            {
                base.InjectCheckIndexesTags();
            }
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                base.InjectCheckIndexesTags();

                if (!Page.IsPostBack)
                {
                    //Form
                    //base.SetContentTableRowsCss(tblContentForm);
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonListManage.ScriptEngine;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Quality;
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                Int64 _idorganizationGlobal = 0;
                try
                {
                    //Construye el Scope de la transaccion (todo lo que este dentro va en transaccion)
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        //Aca son todas las escuelas de CABA, entonces uso fijo el GEO...
                        //GeographicArea _geographicArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(6431);

                        //Dictionary<Int64, Organization> _organizations = EMSLibrary.User.DirectoryServices.Map.Organizations();
                        var _lnqOrganizations = from o in EMSLibrary.User.DirectoryServices.Map.Organizations().Values
                                                where o.IdOrganization > Convert.ToInt64(txtIdOrgDesde.Text) && o.IdOrganization <= Convert.ToInt64(txtIdOrgHasta.Text)   // > 3731 //&& o.IdOrganization <= 3731
                                                select o;
                        foreach (Organization _organization in _lnqOrganizations)
                        {   //La Organizacion 1 Es Condesus esa ya tiene todo cargado...
                            if (_organization.IdOrganization != 1)
                            {
                                _idorganizationGlobal = _organization.IdOrganization;
                                //por cada Organizacion, Dar de ALTA 3 Usuarios y darle seguridad a los mismos (esto implica crear desde el functionalArea hasta el POST)
                                FunctionalArea _functionalArea;
                                Position _positionAdmin;
                                Position _positionReader;
                                Position _positionOperator;
                                FunctionalPosition _functionalPositionAdmin;
                                FunctionalPosition _functionalPositionReader;
                                FunctionalPosition _functionalPositionOperator;
                                GeographicFunctionalArea _geographicFunctionalArea;
                                OrganizationalChart _organizationalChart;
                                JobTitle _jobTitleAdmin;
                                JobTitle _jobTitleReader;
                                JobTitle _jobTitleOperator;
                                Person _personAdmin;
                                Person _personReader;
                                Person _personOperator;
                                User _userAdmin;
                                User _userReader;
                                User _userOperator;
                                //Post _postAdmin;
                                //Post _postReader;
                                //Post _postOperator;
                                Permission _permissionManager = EMSLibrary.User.Security.Permission(Common.Constants.PermissionManageKey);
                                Permission _permissionReader = EMSLibrary.User.Security.Permission(Common.Constants.PermissionViewKey);
                                
                                //Esto es para dar de alta los usuario para cada provincia y municipio
                                GeographicArea _geographicArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(Convert.ToInt64(_organization.FiscalIdentification));
                                //No necesito hacer uno por cada, sino que es el mismo para todos, esta arriba!!!!!!!!!!
                                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

                                String _funAreaName = _organization.CorporateName.Replace("Government of the ", String.Empty).Replace("Provincial Government of ", String.Empty).Replace("Municipal Government of ", string.Empty).Replace("Municipio de ", String.Empty).Replace("Provincia de ", String.Empty);
                                
                                //Alta de FUNCTIONAL AREA
                                _functionalArea = _organization.FunctionalAreasAdd(null, _funAreaName, String.Empty);

                                //Alta de POSITION
                                _positionAdmin = _organization.PositionsAdd("Administrator", String.Empty);
                                _positionReader = _organization.PositionsAdd("Reader", String.Empty);
                                _positionOperator = _organization.PositionsAdd("Operator", String.Empty);

                                //Alta de FUNCTIONAL POSITIONS
                                _functionalPositionAdmin = _organization.FunctionalPositionsAdd(_positionAdmin, _functionalArea, null);
                                _functionalPositionReader = _organization.FunctionalPositionsAdd(_positionReader, _functionalArea, null);
                                _functionalPositionOperator = _organization.FunctionalPositionsAdd(_positionOperator, _functionalArea, null);

                                //Alta de GEOGRAPHIC FUNCTIONAL AREAS
                                _geographicFunctionalArea = _organization.GeographicFunctionalAreasAdd(_functionalArea, _geographicArea, null);

                                //Alta de ORGANIZATIONAL CHART
                                _organizationalChart = _organization.OrganizationalChartAdd("General Flowchart", String.Empty);

                                //Alta de JOBTITLE
                                _jobTitleAdmin = _organizationalChart.JobTitlesAdd(_geographicArea.IdGeographicArea, _positionAdmin.IdPosition, _functionalArea.IdFunctionalArea, 0, 0, 0);
                                _jobTitleReader = _organizationalChart.JobTitlesAdd(_geographicArea.IdGeographicArea, _positionReader.IdPosition, _functionalArea.IdFunctionalArea, 0, 0, 0);
                                _jobTitleOperator = _organizationalChart.JobTitlesAdd(_geographicArea.IdGeographicArea, _positionOperator.IdPosition, _functionalArea.IdFunctionalArea, 0, 0, 0);

                                //Alta de PERSON
                                SalutationType _salutationType = EMSLibrary.User.DirectoryServices.Configuration.SalutationType(1);
                                _personAdmin = _organization.PeopleAdd(_salutationType, "Admin", "Admin", "Admin", "Admin", null);
                                _personReader = _organization.PeopleAdd(_salutationType, "Reader", "Reader", "Reader", "Reader", null);
                                _personOperator = _organization.PeopleAdd(_salutationType, "Operator", "Operator", "Operator", "Operator", null);

                                //Alta del USER
                                String _nameOrg = _organization.FiscalIdentification;   //.CorporateName.Replace("Municipio de ", String.Empty).Replace("Provincia de ", String.Empty).Replace(" ", String.Empty).ToLower();
                                Int32 _length = 0;
                                if (_nameOrg.Length > 18)
                                {
                                    _length = 18;
                                }
                                else
                                {
                                    _length = _nameOrg.Length;
                                }

                                String _userNameAdmin = "a." + _nameOrg.Substring(0, _length);
                                String _userNameReader = "r." + _nameOrg.Substring(0, _length);
                                String _userNameOperator = "o." + _nameOrg.Substring(0, _length);
                                _userAdmin = ((PersonwithoutUser)_personAdmin).UsersAdd(_userNameAdmin, "12345", true, false, false, true, true);
                                _userReader = ((PersonwithoutUser)_personReader).UsersAdd(_userNameReader, "12345", true, false, false, true, true);
                                _userOperator = ((PersonwithoutUser)_personOperator).UsersAdd(_userNameOperator, "12345", true, false, false, true, true);

                                //Alta del POST
                                ((PersonwithUser)_userAdmin.Person).PostsAdd(_geographicArea, _positionAdmin, _functionalArea, Convert.ToDateTime(DateTime.Now.ToShortDateString()), DateTime.MinValue);
                                ((PersonwithUser)_userReader.Person).PostsAdd(_geographicArea, _positionReader, _functionalArea, Convert.ToDateTime(DateTime.Now.ToShortDateString()), DateTime.MinValue);
                                ((PersonwithUser)_userOperator.Person).PostsAdd(_geographicArea, _positionOperator, _functionalArea, Convert.ToDateTime(DateTime.Now.ToShortDateString()), DateTime.MinValue);


                                //El Operator es Solo para las tareas
                                //Da Seguridad al ADMIN Por Entidad (aca solo ORGANIZATION)
                                _organization.SecurityPersonAdd(_personAdmin, _permissionManager);
                                //Seguridad al MAPA
                                EMSLibrary.User.DirectoryServices.Map.SecurityPersonAdd(_personAdmin, _permissionManager);
                                EMSLibrary.User.ImprovementAction.Map.SecurityPersonAdd(_personAdmin, _permissionManager);
                                EMSLibrary.User.KnowledgeCollaboration.Map.SecurityPersonAdd(_personAdmin, _permissionManager);
                                EMSLibrary.User.PerformanceAssessments.Map.SecurityPersonAdd(_personAdmin, _permissionManager);
                                EMSLibrary.User.ProcessFramework.Map.SecurityPersonAdd(_personAdmin, _permissionManager);
                                EMSLibrary.User.RiskManagement.Map.SecurityPersonAdd(_personAdmin, _permissionManager);
                                //Seguridad al CONFIG
                                EMSLibrary.User.DirectoryServices.Configuration.SecurityPersonAdd(_personAdmin, _permissionManager);
                                EMSLibrary.User.ImprovementAction.Configuration.SecurityPersonAdd(_personAdmin, _permissionManager);
                                EMSLibrary.User.KnowledgeCollaboration.Configuration.SecurityPersonAdd(_personAdmin, _permissionManager);
                                EMSLibrary.User.PerformanceAssessments.Configuration.SecurityPersonAdd(_personAdmin, _permissionManager);
                                EMSLibrary.User.ProcessFramework.Configuration.SecurityPersonAdd(_personAdmin, _permissionManager);
                                EMSLibrary.User.RiskManagement.Configuration.SecurityPersonAdd(_personAdmin, _permissionManager);

                                //Da Seguridad al READER Por Entidad (aca solo ORGANIZATION)
                                _organization.SecurityPersonAdd(_personReader, _permissionReader);
                                //Seguridad al MAPA
                                EMSLibrary.User.DirectoryServices.Map.SecurityPersonAdd(_personReader, _permissionReader);
                                EMSLibrary.User.ImprovementAction.Map.SecurityPersonAdd(_personReader, _permissionReader);
                                EMSLibrary.User.KnowledgeCollaboration.Map.SecurityPersonAdd(_personReader, _permissionReader);
                                EMSLibrary.User.PerformanceAssessments.Map.SecurityPersonAdd(_personReader, _permissionReader);
                                EMSLibrary.User.ProcessFramework.Map.SecurityPersonAdd(_personReader, _permissionReader);
                                EMSLibrary.User.RiskManagement.Map.SecurityPersonAdd(_personReader, _permissionReader);
                                //Seguridad al CONFIG
                                EMSLibrary.User.DirectoryServices.Configuration.SecurityPersonAdd(_personReader, _permissionReader);
                                EMSLibrary.User.ImprovementAction.Configuration.SecurityPersonAdd(_personReader, _permissionReader);
                                EMSLibrary.User.KnowledgeCollaboration.Configuration.SecurityPersonAdd(_personReader, _permissionReader);
                                EMSLibrary.User.PerformanceAssessments.Configuration.SecurityPersonAdd(_personReader, _permissionReader);
                                EMSLibrary.User.ProcessFramework.Configuration.SecurityPersonAdd(_personReader, _permissionReader);
                                EMSLibrary.User.RiskManagement.Configuration.SecurityPersonAdd(_personReader, _permissionReader);

                                //Aqui finaliza y pasa a la proxima organizacion...
                            }
                        }

                        //Finaliza la transaccion
                        _transactionScope.Complete();
                    }

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }
            }
        #endregion
        
    }
}
