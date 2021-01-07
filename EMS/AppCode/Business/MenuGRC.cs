using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.DS.Entities;

using EBPA = Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.Business
{
    public partial class MenuGRC : Base
    {
        #region Generic Public Methods
            public MenuGRC(String commandName)
            {
                _CommandName = commandName;
            }
            public MenuGRC Create(String commandName)
            {
                return new MenuGRC(commandName);
            }
        #endregion

        #region Public Methods (Build ContentCustomPanel ContextInformation)

            private RadTreeNode CreateRootNode(String nodeText, String nodeValue, String toolTip,
                    String cssClass, Boolean postBack)
            {
                RadTreeNode _node = new RadTreeNode(nodeText, nodeValue);
                _node.ToolTip = toolTip;
                _node.CssClass = cssClass;
                _node.PostBack = postBack;

                return _node;
            }

            private RadTreeNode CreateNode(String nodeText, String nodeValue, String toolTip, String pk_ParentEntity,
                String entityNameGrid, String entityName, String entityNameContextInfo,
                String url, String permissionType, String cssClass, Boolean postBack)
            {
                RadTreeNode _node = new RadTreeNode(nodeText, nodeValue);
                _node.ToolTip = toolTip;
                _node.Attributes.Add("PK_ParentEntity", pk_ParentEntity);
                _node.Attributes.Add("EntityNameGrid", entityNameGrid);
                _node.Attributes.Add("EntityName", entityName);
                _node.Attributes.Add("EntityNameContextInfo", entityNameContextInfo);
                _node.Attributes.Add("URL", url);
                _node.Attributes.Add("PermissionType", permissionType);
                _node.PostBack = postBack;
                _node.CssClass = cssClass;

                return _node;
            }

            #region Directory Services

                #region JobTitles
                    public RadTreeView BuildContextInfoMenuJobTitle(Dictionary<String, Object> param)
            {
                RadTreeView _rdtvGRCProcessFrameworkByJobTitle = new RadTreeView();

                if ((param.ContainsKey("IdPosition")) && (param.ContainsKey("IdOrganization")))
                {
                    //Arma un Tree Vacio para que no de error.
                    Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                    Int64 _idPosition = Convert.ToInt64(param["IdPosition"]);
                    Int64 _idGeographicArea = Convert.ToInt64(param["IdGeographicArea"]);
                    Int64 _idFunctionalArea = Convert.ToInt64(param["IdFunctionalArea"]);

                    Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                    String _pageTitle = String.Empty;
                    if (!param.ContainsKey("PageTitle"))
                    {
                        Condesus.EMS.Business.GIS.Entities.GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                        Condesus.EMS.Business.DS.Entities.Position _position = _organization.Position(_idPosition);
                        Condesus.EMS.Business.DS.Entities.FunctionalArea _funArea = _organization.FunctionalArea(_idFunctionalArea);
                        Condesus.EMS.Business.DS.Entities.FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        Condesus.EMS.Business.DS.Entities.GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);
                        _pageTitle = _jobTitle.Name();
                    }
                    else
                    {
                        _pageTitle = Convert.ToString(param["PageTitle"]);
                    }
                    String _permissionType = String.Empty;
                    if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    { _permissionType = Common.Constants.PermissionManageName; }
                    else
                    { _permissionType = Common.Constants.PermissionViewName; }

                    //Contruye el Tree
                    Int32 _pkNode = 0;

                    //Arma el TREE
                    _rdtvGRCProcessFrameworkByJobTitle.ID = "rtvMenuContextInformation";
                    _rdtvGRCProcessFrameworkByJobTitle.CheckBoxes = false;
                    _rdtvGRCProcessFrameworkByJobTitle.EnableViewState = true;
                    _rdtvGRCProcessFrameworkByJobTitle.AllowNodeEditing = false;
                    _rdtvGRCProcessFrameworkByJobTitle.ShowLineImages = true;
                    _rdtvGRCProcessFrameworkByJobTitle.CausesValidation = false;
                    _rdtvGRCProcessFrameworkByJobTitle.Skin = "EMS";
                    _rdtvGRCProcessFrameworkByJobTitle.EnableEmbeddedSkins = false;
                    _rdtvGRCProcessFrameworkByJobTitle.Attributes.Add("PageTitle", _pageTitle);
                    _rdtvGRCProcessFrameworkByJobTitle.Attributes.Add("PK_IdOrganization", _idOrganization.ToString());
                    _rdtvGRCProcessFrameworkByJobTitle.Attributes.Add("PK_IdPosition", _idPosition.ToString());
                    _rdtvGRCProcessFrameworkByJobTitle.Attributes.Add("PK_IdGeographicArea", _idGeographicArea.ToString());
                    _rdtvGRCProcessFrameworkByJobTitle.Attributes.Add("PK_IdFunctionalArea", _idFunctionalArea.ToString());

                    #region DS
                    RadTreeNode _nodeRootModuleDS = new RadTreeNode(Resources.CommonMenu.radMnuModuleDS, _pkNode++.ToString());
                    _nodeRootModuleDS.ToolTip = Resources.CommonMenu.radMnuModuleDS;
                    _nodeRootModuleDS.CssClass = "Folder";
                    _nodeRootModuleDS.PostBack = false;

                    RadTreeNode _node = new RadTreeNode(Resources.CommonListManage.OrganizationalChart, _pkNode++.ToString());
                    _node.ToolTip = Resources.CommonListManage.OrganizationalChart;
                    _node.Checkable = false;
                    _node.CssClass = "OrganizationalChart";
                    _node.PostBack = true;
                    _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.DS.OrganizationalCharts);
                    _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.OrganizationalChart);
                    _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.JobTitle);
                    _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                    _node.Attributes.Add("PermissionType", _permissionType);
                    //Mete Class dentro del modulo.
                    _nodeRootModuleDS.Nodes.Add(_node);

                    //_node = new RadTreeNode(Resources.CommonListManage.Responsibility, _pkNode++.ToString());
                    //_node.ToolTip = Resources.CommonListManage.Responsibility;
                    //_node.Checkable = false;
                    //_node.CssClass = "Responsibility";
                    //_node.PostBack = true;
                    //_node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.DS.Responsibilities);
                    //_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Responsibility);
                    //_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.JobTitle);
                    //_node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                    //_node.Attributes.Add("PermissionType", _permissionType);
                    ////Mete Class dentro del modulo.
                    //_nodeRootModuleDS.Nodes.Add(_node);

                    //Mete el modulo como raiz.
                    _rdtvGRCProcessFrameworkByJobTitle.Nodes.Add(_nodeRootModuleDS);
                    #endregion
                }
                return _rdtvGRCProcessFrameworkByJobTitle;
            }
                #endregion

                #region Organization
                    public RadTreeView BuildContextInfoMenuOrganization(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCDirectoryServicesByOrganization = new RadTreeView();
                        if (param.ContainsKey("IdOrganization"))
                        {
                            Int32 _pkNode = 0;
                            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                            Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                            String _permissionType = String.Empty;
                            if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _organization.Name;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }

                            _rdtvGRCDirectoryServicesByOrganization.ID = "rtvMenuContextInformation";
                            _rdtvGRCDirectoryServicesByOrganization.CheckBoxes = false;
                            _rdtvGRCDirectoryServicesByOrganization.EnableViewState = true;
                            _rdtvGRCDirectoryServicesByOrganization.AllowNodeEditing = false;
                            _rdtvGRCDirectoryServicesByOrganization.ShowLineImages = true;
                            _rdtvGRCDirectoryServicesByOrganization.Skin = "EMS";
                            _rdtvGRCDirectoryServicesByOrganization.EnableEmbeddedSkins = false;
                            _rdtvGRCDirectoryServicesByOrganization.CausesValidation = false;
                            //Se ponen atributos generales, para usarlos en el click.
                            _rdtvGRCDirectoryServicesByOrganization.Attributes.Add("PK_IdOrganization", _idOrganization.ToString());
                            _rdtvGRCDirectoryServicesByOrganization.Attributes.Add("PageTitle", _pageTitle);
                            //_rdtvGRCDirectoryServicesByOrganization.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                            _rdtvGRCDirectoryServicesByOrganization.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Organization);

                            //Preparando para que no haya seleccion de modulo !!!....
                            #region GRC DS
                            RadTreeNode _nodeRootModuleDS = CreateRootNode(Resources.CommonMenu.radMnuModuleDS, _pkNode++.ToString(), Resources.CommonMenu.radMnuModuleDS, "Folder", false);
                            RadTreeNode _node = null;

                            #region Contacts
                            //RootContact
                            RadTreeNode _nodeContacts = CreateRootNode(Resources.CommonListManage.Contacts, _pkNode++.ToString(), Resources.CommonListManage.Contacts, "Folder", false);

                            //Hijos de Contacts
                            ////Address
                            //_node = CreateNode(Resources.CommonListManage.Addresses, _pkNode++.ToString(), Resources.CommonListManage.Addresses, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Addresses, Common.ConstantsEntitiesName.DS.Address, Common.ConstantsEntitiesName.DS.Organization, "~/Managers/ListManageAndView.aspx", _permissionType, "Address", true);
                            //_nodeContacts.Nodes.Add(_node);

                            ////Telephones
                            //_node = CreateNode(Resources.CommonListManage.Telephones, _pkNode++.ToString(), Resources.CommonListManage.Telephones, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Telephones, Common.ConstantsEntitiesName.DS.Telephone, Common.ConstantsEntitiesName.DS.Organization, "~/Managers/ListManageAndView.aspx", _permissionType, "Telephone", true);
                            //_nodeContacts.Nodes.Add(_node);

                            //Emails
                            _node = CreateNode(Resources.CommonListManage.Emails, _pkNode++.ToString(), Resources.CommonListManage.Emails, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.ContactEmails, Common.ConstantsEntitiesName.DS.ContactEmail, Common.ConstantsEntitiesName.DS.Organization, "~/Managers/ListManageAndView.aspx", _permissionType, "Email", true);
                            _nodeContacts.Nodes.Add(_node);

                            //URLs
                            _node = CreateNode(Resources.CommonListManage.URLs, _pkNode++.ToString(), Resources.CommonListManage.URLs, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.ContactURLs, Common.ConstantsEntitiesName.DS.ContactURL, Common.ConstantsEntitiesName.DS.Organization, "~/Managers/ListManageAndView.aspx", _permissionType, "URL", true);
                            _nodeContacts.Nodes.Add(_node);

                            //Messengers
                            _node = CreateNode(Resources.CommonListManage.Messengers, _pkNode++.ToString(), Resources.CommonListManage.Messengers, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.ContactMessengers, Common.ConstantsEntitiesName.DS.ContactMessenger, Common.ConstantsEntitiesName.DS.Organization, "~/Managers/ListManageAndView.aspx", _permissionType, "Messenger", true);
                            _nodeContacts.Nodes.Add(_node);

                            //Mete los contact dentro del Modulo.
                            _nodeRootModuleDS.Nodes.Add(_nodeContacts);
                            #endregion

                            #region Organization Classifications

                            _node = CreateNode(Resources.CommonListManage.Classifications, _pkNode++.ToString(), Resources.CommonListManage.Classifications, String.Empty, Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassification, "~/Managers/ListManageAndView.aspx", _permissionType, "OrganizationClassification", true);
                            _nodeRootModuleDS.Nodes.Add(_node);

                            #endregion

                            #region Organization Relationships

                            //_node = CreateNode(Resources.CommonListManage.OrganizationRelationships, _pkNode++.ToString(), Resources.CommonListManage.OrganizationRelationships, String.Empty, Common.ConstantsEntitiesName.DS.OrganizationRelationships, Common.ConstantsEntitiesName.DS.OrganizationRelationship, Common.ConstantsEntitiesName.DS.Organization, "~/Managers/ListManageAndView.aspx", _permissionType, "OrganizationRelationship", true);
                            //_nodeRootModuleDS.Nodes.Add(_node);

                            #endregion

                            #region Functional Areas

                            _node = new RadTreeNode(Resources.CommonListManage.FunctionalAreas, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.FunctionalAreas;
                            _node.Attributes.Add("EntityNameGrid", String.Empty);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.FunctionalArea);
                            _node.Attributes.Add("EntityNameHierarchical", Common.ConstantsEntitiesName.DS.FunctionalAreas);
                            _node.Attributes.Add("EntityNameHierarchicalChildren", Common.ConstantsEntitiesName.DS.FunctionalAreaChildren);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                            _node.Attributes.Add("URL", "~/Managers/HierarchicalListManage.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            _node.PostBack = true;
                            _node.CssClass = "FunctionalArea";
                            _nodeRootModuleDS.Nodes.Add(_node);

                            #endregion

                            #region Facilities
                                _node = CreateNode(Resources.CommonListManage.Facilities, _pkNode++.ToString(), Resources.CommonListManage.Facility, String.Empty, Common.ConstantsEntitiesName.DS.Facilities, Common.ConstantsEntitiesName.DS.Facility, Common.ConstantsEntitiesName.DS.Facility, "~/Managers/GeographicManagerFacilities.aspx", _permissionType, "Facility", true);
                                _nodeRootModuleDS.Nodes.Add(_node);
                            #endregion

                            #region Positions

                            _node = CreateNode(Resources.CommonListManage.Positions, _pkNode++.ToString(), Resources.CommonListManage.Positions, String.Empty, Common.ConstantsEntitiesName.DS.Positions, Common.ConstantsEntitiesName.DS.Position, Common.ConstantsEntitiesName.DS.Organization, "~/Managers/ListManageAndView.aspx", _permissionType, "Position", true);
                            _nodeRootModuleDS.Nodes.Add(_node);

                            #endregion

                            #region Functional Positions

                            _node = new RadTreeNode(Resources.CommonListManage.FunctionalPositions, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.FunctionalPositions;
                            _node.Attributes.Add("EntityNameGrid", String.Empty);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.FunctionalPosition);
                            _node.Attributes.Add("EntityNameHierarchical", Common.ConstantsEntitiesName.DS.FunctionalPositions);
                            _node.Attributes.Add("EntityNameHierarchicalChildren", Common.ConstantsEntitiesName.DS.FunctionalPositionChildren);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                            _node.Attributes.Add("URL", "~/Managers/HierarchicalListManage.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            _node.PostBack = true;
                            _node.CssClass = "FunctionalPosition";
                            _nodeRootModuleDS.Nodes.Add(_node);

                            #endregion

                            #region Geographic Functional Areas

                            _node = new RadTreeNode(Resources.CommonListManage.GeographicFunctionalAreas, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.GeographicFunctionalAreas;
                            _node.Attributes.Add("EntityNameGrid", String.Empty);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.GeographicFunctionalArea);
                            _node.Attributes.Add("EntityNameHierarchical", Common.ConstantsEntitiesName.DS.GeographicFunctionalAreas);
                            _node.Attributes.Add("EntityNameHierarchicalChildren", Common.ConstantsEntitiesName.DS.GeographicFunctionalAreaChildren);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                            _node.Attributes.Add("URL", "~/Managers/HierarchicalListManage.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            _node.PostBack = true;
                            _node.CssClass = "GeographicFunctionalArea";
                            _nodeRootModuleDS.Nodes.Add(_node);

                            #endregion

                            #region Organizational Charts

                            _node = CreateNode(Resources.CommonListManage.OrganizationalCharts, _pkNode++.ToString(), Resources.CommonListManage.OrganizationalCharts, String.Empty, Common.ConstantsEntitiesName.DS.OrganizationalCharts, Common.ConstantsEntitiesName.DS.OrganizationalChart, Common.ConstantsEntitiesName.DS.Organization, "~/Managers/ListManageAndView.aspx", _permissionType, "OrganizationalChart", true);
                            _nodeRootModuleDS.Nodes.Add(_node);

                            #endregion

                            #region Job Titles

                            _node = new RadTreeNode(Resources.CommonListManage.JobTitles, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.JobTitles;
                            _node.Attributes.Add("EntityNameGrid", String.Empty);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.JobTitle);
                            _node.Attributes.Add("EntityNameHierarchical", Common.ConstantsEntitiesName.DS.JobTitles);
                            _node.Attributes.Add("EntityNameHierarchicalChildren", Common.ConstantsEntitiesName.DS.JobTitleChildren);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.JobTitle);
                            //PAra tener un combo de Filtro...
                            _node.Attributes.Add("EntityNameComboFilter", Common.ConstantsEntitiesName.DS.OrganizationalCharts);
                            _node.Attributes.Add("IsFilterHierarchy", "false");
                            _node.Attributes.Add("EntityNameChildrenComboFilter", String.Empty);
                            _node.Attributes.Add("EntityNameMapClassification", String.Empty);
                            _node.Attributes.Add("EntityNameMapClassificationChildren", String.Empty);
                            _node.Attributes.Add("EntityNameMapElement", String.Empty);
                            _node.Attributes.Add("EntityNameMapElementChildren", String.Empty);
                            _node.Attributes.Add("URL", "~/Managers/HierarchicalListManage.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            _node.PostBack = true;
                            _node.CssClass = "JobTitle";
                            _nodeRootModuleDS.Nodes.Add(_node);

                            #endregion

                            #region People

                            _node = CreateNode(Resources.CommonListManage.People, _pkNode++.ToString(), Resources.CommonListManage.People, String.Empty, Common.ConstantsEntitiesName.DS.People, Common.ConstantsEntitiesName.DS.Person, Common.ConstantsEntitiesName.DS.Person, "~/Managers/ListManageAndView.aspx", _permissionType, "Person", true);
                            _nodeRootModuleDS.Nodes.Add(_node);

                            #endregion

                            #region Users

                            _node = CreateNode(Resources.CommonListManage.Users, _pkNode++.ToString(), Resources.CommonListManage.Users, String.Empty, Common.ConstantsEntitiesName.DS.Users, Common.ConstantsEntitiesName.DS.User, Common.ConstantsEntitiesName.DS.Organization, "~/Managers/ListManageAndView.aspx", _permissionType, "User", true);
                            _nodeRootModuleDS.Nodes.Add(_node);

                            #endregion

                            #region Posts

                            _node = CreateNode(Resources.CommonListManage.Posts, _pkNode++.ToString(), Resources.CommonListManage.Posts, String.Empty, Common.ConstantsEntitiesName.DS.Posts, Common.ConstantsEntitiesName.DS.Post, Common.ConstantsEntitiesName.DS.Organization, "~/Managers/ListManageAndView.aspx", _permissionType, "Post", true);
                            _nodeRootModuleDS.Nodes.Add(_node);

                            #endregion

                            #region Organization Extended Property
                                _node = CreateNode(Resources.CommonListManage.ExtendedProperties, _pkNode++.ToString(), Resources.CommonListManage.ExtendedProperties, String.Empty, Common.ConstantsEntitiesName.DS.OrganizationExtendedProperties, Common.ConstantsEntitiesName.DS.OrganizationExtendedProperty, Common.ConstantsEntitiesName.DS.Organization, "~/Managers/ListManageAndView.aspx", _permissionType, "ExtendedProperties", true);

                                _nodeRootModuleDS.Nodes.Add(_node);
                            #endregion

                            //Mete el modulo como raiz.
                            _rdtvGRCDirectoryServicesByOrganization.Nodes.Add(_nodeRootModuleDS);

                            #endregion

                            //#region GRC PF
                            //RadTreeNode _nodeRootModulePF = new RadTreeNode(Resources.CommonMenu.radMnuModulePF, _pkNode++.ToString());
                            //_nodeRootModulePF.ToolTip = Resources.CommonMenu.radMnuModulePF;
                            //_nodeRootModulePF.CssClass = "Folder";
                            //_nodeRootModulePF.PostBack = false;

                            //#region Process Participation

                            //_node = new RadTreeNode(Resources.CommonListManage.ProcessParticipation, _pkNode++.ToString());
                            //_node.ToolTip = Resources.CommonListManage.ProcessParticipation;
                            //_node.Checkable = false;
                            //_node.PostBack = true;
                            //_node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessParticipations);
                            //_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessParticipation);
                            //_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                            //_node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            //_node.Attributes.Add("PermissionType", _permissionType);
                            //_node.CssClass = "ProcessParticipation";
                            ////Mete el Process dentro del modulo.
                            //_nodeRootModulePF.Nodes.Add(_node);

                            //#endregion

                            ////Mete el modulo como raiz.
                            //_rdtvGRCDirectoryServicesByOrganization.Nodes.Add(_nodeRootModulePF);
                            //#endregion
                        }
                        return _rdtvGRCDirectoryServicesByOrganization;
                    }
                #endregion

                #region Organization Classification
                    public RadTreeView BuildContextInfoMenuOrganizationClassification(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByOrganizationClassification = new RadTreeView();
                        if (param.ContainsKey("IdOrganizationClassification"))
                        {
                            Int64 _idOrganizationClassification = Convert.ToInt64(param["IdOrganizationClassification"]);
                            Condesus.EMS.Business.DS.Entities.OrganizationClassification _organizationClassification = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification);
                            String _permissionType = String.Empty;
                            if (EMSLibrary.User.DirectoryServices.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _organizationClassification.LanguageOption.Name;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByOrganizationClassification.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByOrganizationClassification.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByOrganizationClassification.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByOrganizationClassification.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByOrganizationClassification.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByOrganizationClassification.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByOrganizationClassification.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByOrganizationClassification.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByOrganizationClassification.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByOrganizationClassification.Attributes.Add("PK_IdOrganizationClassification", _idOrganizationClassification.ToString());

                            #region DS
                            RadTreeNode _nodeRootModuleDS = new RadTreeNode(Resources.CommonMenu.radMnuModuleDS, _pkNode++.ToString());
                            _nodeRootModuleDS.ToolTip = Resources.CommonMenu.radMnuModuleDS;
                            _nodeRootModuleDS.CssClass = "Folder";
                            _nodeRootModuleDS.PostBack = false;

                            RadTreeNode _node = new RadTreeNode(Resources.CommonListManage.Classifications, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Classifications;
                            _node.Checkable = false;
                            _node.CssClass = "OrganizationClassification";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.OrganizationClassification);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.OrganizationClassification);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete Class dentro del modulo.
                            _nodeRootModuleDS.Nodes.Add(_node);

                            _node = new RadTreeNode(Resources.CommonListManage.Organizations, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Organizations;
                            _node.Checkable = false;
                            _node.CssClass = "Organization";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.DS.Organizations);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Organization);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Organization);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete Class dentro del modulo.
                            _nodeRootModuleDS.Nodes.Add(_node);

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByOrganizationClassification.Nodes.Add(_nodeRootModuleDS);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByOrganizationClassification;
                    }
                    #endregion

                #region Person
                    public RadTreeView BuildContextInfoMenuPerson(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCDirectoryServicesByPerson = new RadTreeView();

                        if ((param.ContainsKey("IdPerson")) && (param.ContainsKey("IdOrganization")))
                        {
                            Int32 _pkNode = 0;
                            Int64 _idPerson = Convert.ToInt64(param["IdPerson"]);
                            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                            Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                Condesus.EMS.Business.DS.Entities.Person _person = _organization.Person(_idPerson);
                                _pageTitle = _person.LastName + ", " + _person.FirstName;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            _rdtvGRCDirectoryServicesByPerson.ID = "rtvMenuContextInformation";
                            _rdtvGRCDirectoryServicesByPerson.CheckBoxes = false;
                            _rdtvGRCDirectoryServicesByPerson.EnableViewState = true;
                            _rdtvGRCDirectoryServicesByPerson.AllowNodeEditing = false;
                            _rdtvGRCDirectoryServicesByPerson.ShowLineImages = true;
                            _rdtvGRCDirectoryServicesByPerson.Skin = "EMS";
                            _rdtvGRCDirectoryServicesByPerson.EnableEmbeddedSkins = false;
                            _rdtvGRCDirectoryServicesByPerson.CausesValidation = false;
                            //Se ponen atributos generales, para usarlos en el click.
                            _rdtvGRCDirectoryServicesByPerson.Attributes.Add("PK_IdOrganization", _idOrganization.ToString());
                            _rdtvGRCDirectoryServicesByPerson.Attributes.Add("PK_IdPerson", _idPerson.ToString());
                            _rdtvGRCDirectoryServicesByPerson.Attributes.Add("PageTitle", _pageTitle);
                            //_rdtvGRCDirectoryServicesByPerson.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Person);
                            //_rdtvGRCDirectoryServicesByPerson.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Person);

                            //Preparando para que no haya seleccion de modulo !!!....
                            #region GRC DS
                            RadTreeNode _nodeRootModuleDS = new RadTreeNode(Resources.CommonMenu.radMnuModuleDS, _pkNode++.ToString());
                            _nodeRootModuleDS.ToolTip = Resources.CommonMenu.radMnuModuleDS;
                            _nodeRootModuleDS.PostBack = false;
                            _nodeRootModuleDS.CssClass = "Folder";

                            //Users
                            RadTreeNode _node = new RadTreeNode(Resources.CommonListManage.Users, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Users;
                            _node.Attributes.Add("PK_ParentEntity", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.DS.Users);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.User);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Person);
                            String _urlProperties = GetGlobalResourceObject("URLEntityProperties", Common.ConstantsEntitiesName.DS.User).ToString();
                            _node.Attributes.Add("URL", _urlProperties);
                            _node.Attributes.Add("PermissionType", _permissionType);
                            _node.PostBack = true;
                            _node.CssClass = "User";
                            _nodeRootModuleDS.Nodes.Add(_node);


                            //Posts
                            _node = new RadTreeNode(Resources.CommonListManage.Posts, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Posts;
                            _node.Attributes.Add("PK_ParentEntity", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.DS.Posts);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Post);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            _node.PostBack = true;
                            _node.CssClass = "Post";
                            _nodeRootModuleDS.Nodes.Add(_node);


                            //RootContact
                            RadTreeNode _nodeContacts = new RadTreeNode(Resources.CommonListManage.Contacts, _pkNode++.ToString());
                            _nodeContacts.ToolTip = Resources.CommonListManage.Contacts;
                            _nodeContacts.PostBack = false;
                            _nodeContacts.CssClass = "Folder";

                            //Hijos de Contacts
                            //Address
                            _node = new RadTreeNode(Resources.CommonListManage.Addresses, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Addresses;
                            _node.Attributes.Add("PK_ParentEntity", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.DS.Addresses);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Address);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            _node.PostBack = true;
                            _node.CssClass = "Addresse";
                            _nodeContacts.Nodes.Add(_node);
                            //Telephones
                            _node = new RadTreeNode(Resources.CommonListManage.Telephones, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Telephones;
                            _node.Attributes.Add("PK_ParentEntity", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.DS.Telephones);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Telephone);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            _node.PostBack = true;
                            _node.CssClass = "Telephone";
                            _nodeContacts.Nodes.Add(_node);
                            //Emails
                            _node = new RadTreeNode(Resources.CommonListManage.Emails, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Emails;
                            _node.Attributes.Add("PK_ParentEntity", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.DS.ContactEmails);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.ContactEmail);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            _node.PostBack = true;
                            _node.CssClass = "Email";
                            _nodeContacts.Nodes.Add(_node);
                            //URLs
                            _node = new RadTreeNode(Resources.CommonListManage.URLs, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.URLs;
                            _node.Attributes.Add("PK_ParentEntity", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.DS.ContactURLs);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.ContactURL);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            _node.PostBack = true;
                            _node.CssClass = "URL";
                            _nodeContacts.Nodes.Add(_node);
                            //Messengers
                            _node = new RadTreeNode(Resources.CommonListManage.Messengers, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Messengers;
                            _node.Attributes.Add("PK_ParentEntity", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.DS.ContactMessengers);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.ContactMessenger);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Person);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            _node.PostBack = true;
                            _node.CssClass = "Messenger";
                            _nodeContacts.Nodes.Add(_node);

                            //Mete los contactos dentro del modulo
                            _nodeRootModuleDS.Nodes.Add(_nodeContacts);
                            //Mete el modulo como raiz.
                            _rdtvGRCDirectoryServicesByPerson.Nodes.Add(_nodeRootModuleDS);
                            #endregion
                        }
                        return _rdtvGRCDirectoryServicesByPerson;
                    }
                    #endregion

                #region Facility
                    public RadTreeView BuildContextInfoMenuFacility(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCDirectoryServicesByFacility = new RadTreeView();

                        if ((param.ContainsKey("IdOrganization")) && (param.ContainsKey("IdFacility")))
                        {
                            Int32 _pkNode = 0;
                            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                            Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                            Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                            Condesus.EMS.Business.GIS.Entities.Facility _facility = _organization.Facility(_idFacility);

                            String _permissionType = String.Empty;
                            if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _facility.LanguageOption.Name;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }

                            _rdtvGRCDirectoryServicesByFacility.ID = "rtvMenuContextInformation";
                            _rdtvGRCDirectoryServicesByFacility.CheckBoxes = false;
                            _rdtvGRCDirectoryServicesByFacility.EnableViewState = true;
                            _rdtvGRCDirectoryServicesByFacility.AllowNodeEditing = false;
                            _rdtvGRCDirectoryServicesByFacility.ShowLineImages = true;
                            _rdtvGRCDirectoryServicesByFacility.Skin = "EMS";
                            _rdtvGRCDirectoryServicesByFacility.EnableEmbeddedSkins = false;
                            _rdtvGRCDirectoryServicesByFacility.CausesValidation = false;
                            //Se ponen atributos generales, para usarlos en el click.
                            _rdtvGRCDirectoryServicesByFacility.Attributes.Add("PK_IdOrganization", _idOrganization.ToString());
                            _rdtvGRCDirectoryServicesByFacility.Attributes.Add("PK_IdFacility", _idFacility.ToString());
                            _rdtvGRCDirectoryServicesByFacility.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCDirectoryServicesByFacility.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Facility);

                            //Preparando para que no haya seleccion de modulo !!!....
                            #region GRC DS
                            RadTreeNode _nodeRootModuleDS = CreateRootNode(Resources.CommonMenu.radMnuModuleDS, _pkNode++.ToString(), Resources.CommonMenu.radMnuModuleDS, "Folder", false);
                            RadTreeNode _node = null;

                            #region Contacts
                            //RootContact
                            RadTreeNode _nodeContacts = CreateRootNode(Resources.CommonListManage.Contacts, _pkNode++.ToString(), Resources.CommonListManage.Contacts, "Folder", false);

                            //Hijos de Contacts
                            //Address
                            _node = CreateNode(Resources.CommonListManage.Addresses, _pkNode++.ToString(), Resources.CommonListManage.Addresses, Common.ConstantsEntitiesName.DS.Facility, Common.ConstantsEntitiesName.DS.Addresses, Common.ConstantsEntitiesName.DS.Address, Common.ConstantsEntitiesName.DS.Facility, "~/Managers/ListManageAndView.aspx", _permissionType, "Address", true);
                            _node.Attributes.Add("PK_ParentEntity", Common.ConstantsEntitiesName.DS.Facility);
                            _nodeContacts.Nodes.Add(_node);

                            //Telephones
                            _node = CreateNode(Resources.CommonListManage.Telephones, _pkNode++.ToString(), Resources.CommonListManage.Telephones, Common.ConstantsEntitiesName.DS.Facility, Common.ConstantsEntitiesName.DS.Telephones, Common.ConstantsEntitiesName.DS.Telephone, Common.ConstantsEntitiesName.DS.Facility, "~/Managers/ListManageAndView.aspx", _permissionType, "Telephone", true);
                            _node.Attributes.Add("PK_ParentEntity", Common.ConstantsEntitiesName.DS.Facility);
                            _nodeContacts.Nodes.Add(_node);

                            //Mete los contact dentro del Modulo.
                            _nodeRootModuleDS.Nodes.Add(_nodeContacts);
                            #endregion

                            #region Sector
                            _node = new RadTreeNode(Resources.CommonListManage.Sectors, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Sectors;
                            _node.Attributes.Add("EntityNameGrid", String.Empty);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.DS.Sector);
                            _node.Attributes.Add("EntityNameHierarchical", Common.ConstantsEntitiesName.DS.Sectors);
                            _node.Attributes.Add("EntityNameHierarchicalChildren", Common.ConstantsEntitiesName.DS.SectorsChildren);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Sector);
                            _node.Attributes.Add("URL", "~/Managers/HierarchicalListManage.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            _node.PostBack = true;
                            _node.CssClass = "Sector";
                            _nodeRootModuleDS.Nodes.Add(_node);
                            #endregion

                            //Mete el modulo como raiz.
                            _rdtvGRCDirectoryServicesByFacility.Nodes.Add(_nodeRootModuleDS);

                            #endregion
                        }
                        return _rdtvGRCDirectoryServicesByFacility;
                    }
                #endregion

                #region Facility
                    public RadTreeView BuildContextInfoMenuSector(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCDirectoryServicesBySector = new RadTreeView();
                        if ((param.ContainsKey("IdOrganization")) && (param.ContainsKey("IdFacility")) && (param.ContainsKey("IdSector")))
                        {
                            Int32 _pkNode = 0;
                            Int64 _idOrganization = Convert.ToInt64(param["IdOrganization"]);
                            Int64 _idFacility = Convert.ToInt64(param["IdFacility"]);
                            Int64 _idSector = Convert.ToInt64(param["IdSector"]);
                            Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_idOrganization);
                            Condesus.EMS.Business.GIS.Entities.Facility _facility = _organization.Facility(_idFacility);
                            Condesus.EMS.Business.GIS.Entities.Sector _sector = _facility.Sector(_idSector);

                            String _permissionType = String.Empty;
                            if (_organization.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _sector.LanguageOption.Name;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }

                            _rdtvGRCDirectoryServicesBySector.ID = "rtvMenuContextInformation";
                            _rdtvGRCDirectoryServicesBySector.CheckBoxes = false;
                            _rdtvGRCDirectoryServicesBySector.EnableViewState = true;
                            _rdtvGRCDirectoryServicesBySector.AllowNodeEditing = false;
                            _rdtvGRCDirectoryServicesBySector.ShowLineImages = true;
                            _rdtvGRCDirectoryServicesBySector.Skin = "EMS";
                            _rdtvGRCDirectoryServicesBySector.EnableEmbeddedSkins = false;
                            _rdtvGRCDirectoryServicesBySector.CausesValidation = false;
                            //Se ponen atributos generales, para usarlos en el click.
                            _rdtvGRCDirectoryServicesBySector.Attributes.Add("PK_IdOrganization", _idOrganization.ToString());
                            _rdtvGRCDirectoryServicesBySector.Attributes.Add("PK_IdFacility", _idFacility.ToString());
                            _rdtvGRCDirectoryServicesBySector.Attributes.Add("PK_IdSector", _idSector.ToString());
                            _rdtvGRCDirectoryServicesBySector.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCDirectoryServicesBySector.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.DS.Sector);

                            //Preparando para que no haya seleccion de modulo !!!....
                            #region GRC DS
                            RadTreeNode _nodeRootModuleDS = CreateRootNode(Resources.CommonMenu.radMnuModuleDS, _pkNode++.ToString(), Resources.CommonMenu.radMnuModuleDS, "Folder", false);
                            RadTreeNode _node = null;

                            #region Contacts
                                //RootContact
                                RadTreeNode _nodeContacts = CreateRootNode(Resources.CommonListManage.Contacts, _pkNode++.ToString(), Resources.CommonListManage.Contacts, "Folder", false);

                                //Hijos de Contacts
                                //Address
                                _node = CreateNode(Resources.CommonListManage.Addresses, _pkNode++.ToString(), Resources.CommonListManage.Addresses, Common.ConstantsEntitiesName.DS.Facility, Common.ConstantsEntitiesName.DS.Addresses, Common.ConstantsEntitiesName.DS.Address, Common.ConstantsEntitiesName.DS.Facility, "~/Managers/ListManageAndView.aspx", _permissionType, "Address", true);
                                _node.Attributes.Add("PK_ParentEntity", Common.ConstantsEntitiesName.DS.Sector);
                                _nodeContacts.Nodes.Add(_node);

                                //Telephones
                                _node = CreateNode(Resources.CommonListManage.Telephones, _pkNode++.ToString(), Resources.CommonListManage.Telephones, Common.ConstantsEntitiesName.DS.Facility, Common.ConstantsEntitiesName.DS.Telephones, Common.ConstantsEntitiesName.DS.Telephone, Common.ConstantsEntitiesName.DS.Facility, "~/Managers/ListManageAndView.aspx", _permissionType, "Telephone", true);
                                _node.Attributes.Add("PK_ParentEntity", Common.ConstantsEntitiesName.DS.Sector);
                                _nodeContacts.Nodes.Add(_node);

                                //Mete los contact dentro del Modulo.
                                _nodeRootModuleDS.Nodes.Add(_nodeContacts);
                            #endregion

                            //Mete el modulo como raiz.
                            _rdtvGRCDirectoryServicesBySector.Nodes.Add(_nodeRootModuleDS);

                            #endregion
                        }
                        return _rdtvGRCDirectoryServicesBySector;
                    }
                #endregion

            #endregion

            #region Perfomance Assessment
                #region Indicator Classification
                    public RadTreeView BuildContextInfoMenuIndicatorClassification(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByIndicatorClassification = new RadTreeView();
                        if (param.ContainsKey("IdIndicatorClassification"))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idIndicatorClassification = Convert.ToInt64(param["IdIndicatorClassification"]);
                            Condesus.EMS.Business.PA.Entities.IndicatorClassification _indicatorClassification = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idIndicatorClassification);
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _indicatorClassification.LanguageOption.Name;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;

                            if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByIndicatorClassification.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByIndicatorClassification.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByIndicatorClassification.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByIndicatorClassification.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByIndicatorClassification.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByIndicatorClassification.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByIndicatorClassification.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByIndicatorClassification.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByIndicatorClassification.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByIndicatorClassification.Attributes.Add("PK_IdIndicatorClassification", _idIndicatorClassification.ToString());

                            #region PA
                            RadTreeNode _nodeRootModulePA = new RadTreeNode(Resources.CommonMenu.radMnuModulePA, _pkNode++.ToString());
                            _nodeRootModulePA.ToolTip = Resources.CommonMenu.radMnuModulePA;
                            _nodeRootModulePA.CssClass = "PA";
                            _nodeRootModulePA.PostBack = false;

                            RadTreeNode _node = new RadTreeNode(Resources.CommonListManage.Classifications, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Classifications;
                            _node.Checkable = false;
                            _node.CssClass = "Classification";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.IndicatorClassificationChildren);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.IndicatorClassification);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.IndicatorClassification);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePA.Nodes.Add(_node);

                            _node = new RadTreeNode(Resources.CommonListManage.Indicators, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Indicators;
                            _node.Checkable = false;
                            _node.CssClass = "Indicator";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.Indicators);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Indicator);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Indicator);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePA.Nodes.Add(_node);

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByIndicatorClassification.Nodes.Add(_nodeRootModulePA);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByIndicatorClassification;
                    }
                #endregion

                #region Indicator
                    public RadTreeView BuildContextInfoMenuIndicator(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByIndicator = new RadTreeView();
                        if (param.ContainsKey("IdIndicator"))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
                            Condesus.EMS.Business.PA.Entities.Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator);
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = Common.Functions.ReplaceIndexesTags(_indicator.LanguageOption.Name);
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByIndicator.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByIndicator.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByIndicator.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByIndicator.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByIndicator.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByIndicator.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByIndicator.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByIndicator.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByIndicator.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByIndicator.Attributes.Add("PK_IdIndicator", _idIndicator.ToString());

                            #region PA
                            RadTreeNode _nodeRootModulePA = CreateRootNode(Resources.CommonMenu.radMnuModulePA, _pkNode++.ToString(), Resources.CommonMenu.radMnuModulePA, "Folder", false);
                            RadTreeNode _node;
                            
                            #region Indicator Extended Property
                                _node = CreateNode(Resources.CommonListManage.ExtendedProperties, _pkNode++.ToString(), Resources.CommonListManage.ExtendedProperties, String.Empty, Common.ConstantsEntitiesName.PA.IndicatorExtendedProperties, Common.ConstantsEntitiesName.PA.IndicatorExtendedProperty, Common.ConstantsEntitiesName.PA.Indicator, "~/Managers/ListManageAndView.aspx", _permissionType, "ExtendedProperties", true);
                                _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            #region Calculation
                            //_node = new RadTreeNode(Resources.CommonListManage.Calculations, _pkNode++.ToString());
                            //_node.ToolTip = Resources.CommonListManage.Calculations;
                            //_node.Checkable = false;
                            //_node.CssClass = "Calculation";
                            //_node.PostBack = true;
                            //_node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.Calculations);
                            //_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Calculation);
                            //_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Calculation);
                            //_node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            //_node.Attributes.Add("PermissionType", _permissionType);
                            ////Mete el Process dentro del modulo.
                            //_nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            #region Indicator Classification
                            _node = new RadTreeNode(Resources.CommonListManage.Classifications, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Classifications;
                            _node.Checkable = false;
                            _node.CssClass = "Classification";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.IndicatorClassifications);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.IndicatorClassification);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.IndicatorClassification);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            #region Measurement
                            //_node = new RadTreeNode(Resources.CommonListManage.Measurements, _pkNode++.ToString());
                            //_node.ToolTip = Resources.CommonListManage.Measurements;
                            //_node.Checkable = false;
                            //_node.CssClass = "Measurement";
                            //_node.PostBack = true;
                            //_node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.Measurements);
                            //_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Measurement);
                            //_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Indicator);
                            //_node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            //_node.Attributes.Add("PermissionType", _permissionType);
                            ////Mete el Process dentro del modulo.
                            //_nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            #region Parameter Group
                            _node = new RadTreeNode(Resources.CommonListManage.ParameterGroup, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ParameterGroup;
                            _node.Checkable = false;
                            _node.CssClass = "ParameterGroup";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.ParameterGroups);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.ParameterGroup);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.ParameterGroup);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByIndicator.Nodes.Add(_nodeRootModulePA);
                            #endregion

                            //#region CT
             
                            //RadTreeNode _nodeRootModuleCT = CreateRootNode(Resources.CommonMenu.radMnuModuleCT, _pkNode++.ToString(), Resources.CommonMenu.radMnuModuleCT, "CT", false);

                            //#region Forum
                            //_node = new RadTreeNode(Resources.CommonListManage.Forums, _pkNode++.ToString());
                            //_node.ToolTip = Resources.CommonListManage.Forums;
                            //_node.CssClass = "Forum";
                            //_node.PostBack = true;
                            //_node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.CT.Forums);
                            //_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.CT.Forum);
                            //_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.CT.Forum);
                            //_node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            //_node.Attributes.Add("PermissionType", _permissionType);
                            ////Mete el Process dentro del modulo.
                            //_nodeRootModuleCT.Nodes.Add(_node);
                            //#endregion

                            ////Mete el modulo como raiz.
                            //_rdtvGRCProcessFrameworkByIndicator.Nodes.Add(_nodeRootModuleCT);
                            //#endregion
                        }
                        return _rdtvGRCProcessFrameworkByIndicator;
                    }
                #endregion

                #region Calculation
                    public RadTreeView BuildContextInfoMenuCalculation(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByCalculation = new RadTreeView();
                        if (param.ContainsKey("IdCalculation"))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idCalculation = Convert.ToInt64(param["IdCalculation"]);
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = EMSLibrary.User.PerformanceAssessments.Configuration.Calculation(_idCalculation).LanguageOption.Name;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByCalculation.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByCalculation.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByCalculation.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByCalculation.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByCalculation.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByCalculation.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByCalculation.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByCalculation.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByCalculation.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByCalculation.Attributes.Add("PK_IdCalculation", _idCalculation.ToString());

                            #region PA
                            RadTreeNode _nodeRootModulePA = CreateRootNode(Resources.CommonMenu.radMnuModulePA, _pkNode++.ToString(), Resources.CommonMenu.radMnuModulePA, "Folder", false);
                            RadTreeNode _node;

                            #region Calculation Extended Property
                                _node = CreateNode(Resources.CommonListManage.ExtendedProperties, _pkNode++.ToString(), Resources.CommonListManage.ExtendedProperties, String.Empty, Common.ConstantsEntitiesName.PA.CalculationExtendedProperties, Common.ConstantsEntitiesName.PA.CalculationExtendedProperty, Common.ConstantsEntitiesName.PA.Calculation, "~/Managers/ListManageAndView.aspx", _permissionType, "ExtendedProperties", true);
                                _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            _node = new RadTreeNode(Resources.CommonListManage.CalculationCertificated, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.CalculationCertificated;
                            _node.Checkable = false;
                            _node.CssClass = "CalculationCertificated";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.CalculationCertificates);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.CalculationCertificate);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Calculation);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePA.Nodes.Add(_node);

                            _node = new RadTreeNode(Resources.CommonListManage.CalculationEstimatedForecasted, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.CalculationEstimatedForecasted;
                            _node.Checkable = false;
                            _node.CssClass = "CalculationEstimated";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.CalculationEstimates);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.CalculationEstimate);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Calculation);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePA.Nodes.Add(_node);

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByCalculation.Nodes.Add(_nodeRootModulePA);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByCalculation;
                    }
                #endregion

                #region Parameter Group
                    public RadTreeView BuildContextInfoMenuParameterGroup(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByParameterGroup = new RadTreeView();
                        if ((param.ContainsKey("IdParameterGroup")) && (param.ContainsKey("IdIndicator")))
                        {
                            Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
                            Int64 _idParameterGroup = Convert.ToInt64(param["IdParameterGroup"]);
                            Condesus.EMS.Business.PA.Entities.Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator);
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _indicator.ParameterGroup(_idParameterGroup).LanguageOption.Name;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByParameterGroup.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByParameterGroup.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByParameterGroup.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByParameterGroup.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByParameterGroup.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByParameterGroup.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByParameterGroup.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByParameterGroup.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByParameterGroup.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByParameterGroup.Attributes.Add("PK_IdIndicator", _idIndicator.ToString());
                            _rdtvGRCProcessFrameworkByParameterGroup.Attributes.Add("PK_IdParameterGroup", _idParameterGroup.ToString());
                            //_rdtvGRCProcessFrameworkByParameterGroup.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Parameter);

                            #region PA
                            RadTreeNode _nodeRootModulePA = new RadTreeNode(Resources.CommonMenu.radMnuModulePA, _pkNode++.ToString());
                            _nodeRootModulePA.ToolTip = Resources.CommonMenu.radMnuModulePA;
                            _nodeRootModulePA.CssClass = "Folder";
                            _nodeRootModulePA.PostBack = false;

                            RadTreeNode _node;

                            #region Parameter Group Extended Property
                                _node = CreateNode(Resources.CommonListManage.ExtendedProperties, _pkNode++.ToString(), Resources.CommonListManage.ExtendedProperties, String.Empty, Common.ConstantsEntitiesName.PA.ParameterGroupExtendedProperties, Common.ConstantsEntitiesName.PA.ParameterGroupExtendedProperty, Common.ConstantsEntitiesName.PA.ParameterGroup, "~/Managers/ListManageAndView.aspx", _permissionType, "ExtendedProperties", true);
                                _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            _node = new RadTreeNode(Resources.CommonListManage.Parameter, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Parameter;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "Parameter";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.Parameters);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Parameter);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Parameter);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);

                            //Mete el Process dentro del modulo.
                            _nodeRootModulePA.Nodes.Add(_node);
                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByParameterGroup.Nodes.Add(_nodeRootModulePA);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByParameterGroup;
                    }
                #endregion

                #region Parameter
                    public RadTreeView BuildContextInfoMenuParameter(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByParameter = new RadTreeView();
                        if ((param.ContainsKey("IdParameter")) && (param.ContainsKey("IdIndicator")))
                        {
                            Int64 _idIndicator = Convert.ToInt64(param["IdIndicator"]);
                            Int64 _idParameterGroup = Convert.ToInt64(param["IdParameterGroup"]);
                            Int64 _idParameter = Convert.ToInt64(param["IdParameter"]);
                            Condesus.EMS.Business.PA.Entities.Indicator _indicator = EMSLibrary.User.PerformanceAssessments.Map.Indicator(_idIndicator);
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _indicator.ParameterGroup(_idParameterGroup).Parameter(_idParameter).LanguageOption.Description;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (EMSLibrary.User.PerformanceAssessments.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByParameter.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByParameter.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByParameter.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByParameter.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByParameter.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByParameter.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByParameter.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByParameter.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByParameter.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByParameter.Attributes.Add("PK_IdIndicator", _idIndicator.ToString());
                            _rdtvGRCProcessFrameworkByParameter.Attributes.Add("PK_IdParameterGroup", _idParameterGroup.ToString());
                            _rdtvGRCProcessFrameworkByParameter.Attributes.Add("PK_IdParameter", _idParameter.ToString());
                            //_rdtvGRCProcessFrameworkByParameter.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Parameter);

                            #region PA
                            RadTreeNode _nodeRootModulePA = new RadTreeNode(Resources.CommonMenu.radMnuModulePA, _pkNode++.ToString());
                            _nodeRootModulePA.ToolTip = Resources.CommonMenu.radMnuModulePA;
                            _nodeRootModulePA.PostBack = false;
                            _nodeRootModulePA.CssClass = "Folder";

                            RadTreeNode _node = new RadTreeNode(Resources.CommonListManage.ParameterRange, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ParameterRange;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "ParameterRange";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.ParameterRanges);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.ParameterRange);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Parameter);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);

                            //Mete el Process dentro del modulo.
                            _nodeRootModulePA.Nodes.Add(_node);
                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByParameter.Nodes.Add(_nodeRootModulePA);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByParameter;
                    }
                #endregion

                #region Measurement
                    public RadTreeView BuildContextInfoMenuMeasurement(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByMeasurement = new RadTreeView();
                        if (param.ContainsKey("IdMeasurement"))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idMeasurement = Convert.ToInt64(param["IdMeasurement"]);
                            Condesus.EMS.Business.PA.Entities.Measurement _measurement = null;
                            //Si viene el process, acceso al measurement a traves del process
                            if (param.ContainsKey("IdProcess"))
                            {
                                Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                                _measurement = ((ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess)).Measurements[_idMeasurement];
                            }
                            else
                            {
                                _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement);
                            }
                            //Condesus.EMS.Business.PA.Entities.Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement);
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _measurement.LanguageOption.Name;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByMeasurement.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByMeasurement.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByMeasurement.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByMeasurement.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByMeasurement.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByMeasurement.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByMeasurement.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByMeasurement.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByMeasurement.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByMeasurement.Attributes.Add("PK_IdMeasurement", _idMeasurement.ToString());

                            #region PA
                            RadTreeNode _nodeRootModulePA = CreateRootNode(Resources.CommonMenu.radMnuModulePA, _pkNode++.ToString(), Resources.CommonMenu.radMnuModulePA, "Folder", false);
                            RadTreeNode _node;

                            //oimg.Attributes["onclick"] = string.Format("return ShowChart(event, " + _idMeasurement + ");");

                            #region Chart
                                _node = new RadTreeNode(Resources.Common.Chart, _pkNode++.ToString());
                                _node.ToolTip = Resources.Common.Chart;
                                _node.Checkable = false;
                                _node.CssClass = "Measurement";
                                _node.PostBack = false;

                                String _title = _measurement.LanguageOption.Name;
                                //Aca hacemos el replace para evitar errores por seguridad de los navegadores.
                                _title = _title.Replace("<sub>", "__").Replace("</sub>", "__").Replace("<sup>", "--").Replace("</sup>", "--");

                                String _keyValues = "Title=" + _title
                                    + "&IdMeasurement=" + _measurement.IdMeasurement.ToString()
                                    + "&EntityName=" + Common.ConstantsEntitiesName.PA.Measurement;

                                //Solo para este caso, que esta dentro de un iFrame, se reemplaza el & por | (pipe)
                                _node.Attributes.Add("PkCompost", _keyValues.Replace("&", "|"));
                                _node.Attributes.Add("Text", _title);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Measurement);
                                _node.Attributes.Add("EntityNameGrid", String.Empty);
                                _node.Attributes.Add("EntityNameContextInfo", String.Empty);
                                _node.Attributes.Add("EntityNameContextElement", String.Empty);
                                _node.Attributes["onclick"] = string.Format("return ShowChart(event, " + _idMeasurement + ");");

                                //Mete el Process dentro del modulo.
                                _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            #region Series
                                _node = new RadTreeNode(Resources.Common.DataSeries, _pkNode++.ToString());
                                _node.ToolTip = Resources.Common.DataSeries;
                                _node.Checkable = false;
                                _node.CssClass = "Measurement";
                                _node.PostBack = true;

                                _title = _measurement.LanguageOption.Name;
                                //Aca hacemos el replace para evitar errores por seguridad de los navegadores.
                                _title = _title.Replace("<sub>", "__").Replace("</sub>", "__").Replace("<sup>", "--").Replace("</sup>", "--");

                                _keyValues = "Title=" + _title
                                    + "&IdMeasurement=" + _measurement.IdMeasurement.ToString()
                                    + "&EntityName=" + Common.ConstantsEntitiesName.PA.Measurement;

                                _node.Attributes.Add("PK_IdMeasurement", _measurement.IdMeasurement.ToString());
                                _node.Attributes.Add("PkCompost", _keyValues.Replace("&", "|"));
                                _node.Attributes.Add("PageTitle", _title);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Measurement);
                                _node.Attributes.Add("EntityNameGrid", String.Empty);
                                _node.Attributes.Add("EntityNameContextInfo", String.Empty);
                                _node.Attributes.Add("EntityNameContextElement", String.Empty);
                                _node.Attributes.Add("URL", "~/Managers/IndicatorSeries.aspx");
                                
                                //Mete el Process dentro del modulo.
                                _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            #region Calculation of Transformation
                                _node = CreateNode(Resources.CommonListManage.Transformations, _pkNode++.ToString(), Resources.CommonListManage.Transformations, String.Empty, Common.ConstantsEntitiesName.PA.Transformations, Common.ConstantsEntitiesName.PA.Transformation, Common.ConstantsEntitiesName.PA.Measurement, "~/Managers/ListManageAndView.aspx", _permissionType, "Transformation", true);
                                _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            #region Attachmets
                                _node = new RadTreeNode(Resources.CommonListManage.Attachments, _pkNode++.ToString());
                                _node.ToolTip = Resources.CommonListManage.Attachments;
                                _node.Checkable = false;
                                _node.PostBack = true;
                                _node.CssClass = "Measurement";
                                _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessTaskExecutionsAttachments);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskExecutionsAttachments);
                                _node.Attributes.Add("EntityNameContextInfo", String.Empty);
                                _node.Attributes.Add("EntityNameContextElement", String.Empty);
                                _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                                _node.Attributes.Add("PermissionType", _permissionType);
                                //Mete el Process dentro del modulo.
                                _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            #region Update Data Series
                                //Solo agregamos esto si es Manager en el Process!
                                if (_measurement.ProcessTask.Parent.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                {
                                    _node = CreateNode(Resources.CommonListManage.UpdateDataSeries, _pkNode++.ToString(), Resources.CommonListManage.UpdateDataSeries, 
                                        String.Empty, Common.ConstantsEntitiesName.PA.MeasurementDataSeries, Common.ConstantsEntitiesName.PA.MeasurementDataSeries,
                                        Common.ConstantsEntitiesName.PA.Measurement, "~/AdministrationTools/PerformanceAssessment/UpdateDataSeries.aspx", _permissionType, "Transformation", true);
                                    _nodeRootModulePA.Nodes.Add(_node);
                                }
                            #endregion

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByMeasurement.Nodes.Add(_nodeRootModulePA);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByMeasurement;
                    }
                #endregion

                #region Transformation
                    public RadTreeView BuildContextInfoMenuTransformation(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByTransformation = new RadTreeView();
                        if (param.ContainsKey("IdMeasurement") && param.ContainsKey("IdTransformation"))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idMeasurement = Convert.ToInt64(param["IdMeasurement"]);
                            Int64 _idTransformation = Convert.ToInt64(param["IdTransformation"]);
                            Condesus.EMS.Business.PA.Entities.CalculateOfTransformation _calculateOfTransformation = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement).Transformation(_idTransformation);
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _calculateOfTransformation.LanguageOption.Name;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (EMSLibrary.User.PerformanceAssessments.Configuration.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByTransformation.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByTransformation.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByTransformation.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByTransformation.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByTransformation.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByTransformation.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByTransformation.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByTransformation.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByTransformation.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByTransformation.Attributes.Add("PK_IdMeasurement", _idMeasurement.ToString());
                            _rdtvGRCProcessFrameworkByTransformation.Attributes.Add("PK_IdTransformation", _idTransformation.ToString());

                            #region PA
                            RadTreeNode _nodeRootModulePA = CreateRootNode(Resources.CommonMenu.radMnuModulePA, _pkNode++.ToString(), Resources.CommonMenu.radMnuModulePA, "Folder", false);
                            RadTreeNode _node;

                            #region Calculation of Transformation
                            _node = CreateNode(Resources.CommonListManage.Transformations, _pkNode++.ToString(), Resources.CommonListManage.Transformations, String.Empty, Common.ConstantsEntitiesName.PA.TransformationsByTransformation, Common.ConstantsEntitiesName.PA.Transformation, Common.ConstantsEntitiesName.PA.Transformation, "~/Managers/ListManageAndView.aspx", _permissionType, "Transformation", true);
                            _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByTransformation.Nodes.Add(_nodeRootModulePA);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByTransformation;
                    }
                #endregion

            #endregion

            #region Process Framework
                #region Process Classification
                    public RadTreeView BuildContextInfoMenuProcessClassification(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByProcessClassification = new RadTreeView();
                        if (param.ContainsKey("IdProcessClassification"))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idProcessClassification = Convert.ToInt64(param["IdProcessClassification"]);
                            Condesus.EMS.Business.PF.Entities.ProcessClassification _processClassification = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idProcessClassification);
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _processClassification.LanguageOption.Name;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (EMSLibrary.User.ProcessFramework.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByProcessClassification.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByProcessClassification.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByProcessClassification.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByProcessClassification.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByProcessClassification.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByProcessClassification.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByProcessClassification.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByProcessClassification.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByProcessClassification.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByProcessClassification.Attributes.Add("PK_IdProcessClassification", _idProcessClassification.ToString());

                            RadTreeNode _node = null;
                            #region PF
                            RadTreeNode _nodeRootModulePF = new RadTreeNode(Resources.CommonMenu.radMnuModulePF, _pkNode++.ToString());
                            _nodeRootModulePF.ToolTip = Resources.CommonMenu.radMnuModulePF;
                            _nodeRootModulePF.CssClass = "Folder";
                            _nodeRootModulePF.PostBack = false;

                            //TODO: Cuando exista el metodo que devuelva las ejecuciones de un proceso... se habilita.
                            _node = new RadTreeNode(Resources.CommonListManage.Classifications, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Classifications;
                            _node.Checkable = false;
                            _node.CssClass = "Classification";
                            _node.PostBack = true; //Despues habilitar, para que realmente lo muestre...
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessClassificationChildren);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessClassification);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePF.Nodes.Add(_node);

                            _node = new RadTreeNode(Resources.CommonListManage.Processes, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Processes;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "Processe";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessGroupProcesses);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePF.Nodes.Add(_node);

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByProcessClassification.Nodes.Add(_nodeRootModulePF);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByProcessClassification;
                    }
                #endregion

                #region Process
                    public RadTreeView BuildContextInfoMenuProcess(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByProcess = new RadTreeView();
                        if (param.ContainsKey("IdProcess"))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                            String _pageTitle = String.Empty;
                            Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _process = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).LanguageOption.Title;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByProcess.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByProcess.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByProcess.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByProcess.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByProcess.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByProcess.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByProcess.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByProcess.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByProcess.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByProcess.Attributes.Add("PK_IdProcess", _idProcess.ToString());
                            //_rdtvGRCProcessFrameworkByProcess.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);

                            RadTreeNode _node;

                            #region DS
                            RadTreeNode _nodeRootModuleDS = new RadTreeNode(Resources.CommonMenu.radMnuModuleDS, _pkNode++.ToString());
                            _nodeRootModuleDS.ToolTip = Resources.CommonMenu.radMnuModuleDS;
                            _nodeRootModuleDS.PostBack = false;
                            _nodeRootModuleDS.CssClass = "Folder";

                            _node = new RadTreeNode(Resources.CommonListManage.ProcessParticipation, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ProcessParticipation;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "ProcessParticipation";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessParticipations);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessParticipation);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);

                            //Mete el Process dentro del modulo.
                            _nodeRootModuleDS.Nodes.Add(_node);
                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByProcess.Nodes.Add(_nodeRootModuleDS);
                            #endregion

                            #region PF
                            RadTreeNode _nodeRootModulePF = new RadTreeNode(Resources.CommonMenu.radMnuModulePF, _pkNode++.ToString());
                            _nodeRootModulePF.ToolTip = Resources.CommonMenu.radMnuModulePF;
                            _nodeRootModulePF.CssClass = "Folder";
                            _nodeRootModulePF.PostBack = false;

                            _node = new RadTreeNode(Resources.CommonListManage.Classifications, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Classifications;
                            _node.Checkable = false;
                            _node.CssClass = "Classification";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessClassifications);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessClassification);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessClassification);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePF.Nodes.Add(_node);

                            //_node = new RadTreeNode(Resources.CommonListManage.ProcessGroupNode, _pkNode++.ToString());
                            //_node.ToolTip = Resources.CommonListManage.ProcessGroupNode;
                            //_node.Checkable = false;
                            //_node.PostBack = true;
                            //_node.CssClass = "ExtendedProperties";
                            //_node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessGroupNodes);
                            //_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessGroupNode);
                            //_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessGroupNode);
                            //_node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            //_node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            //_node.Attributes.Add("PermissionType", _permissionType);
                            ////Mete el Process dentro del modulo.
                            //_nodeRootModulePF.Nodes.Add(_node);


                            _node = new RadTreeNode(Resources.CommonListManage.ExtendedProperties, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ExtendedProperties;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "ExtendedProperties";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessExtendedProperties);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessExtendedProperty);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePF.Nodes.Add(_node);

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByProcess.Nodes.Add(_nodeRootModulePF);
                            #endregion

                            #region PA
                            RadTreeNode _nodeRootModulePA = new RadTreeNode(Resources.CommonMenu.radMnuModulePA, _pkNode++.ToString());
                            _nodeRootModulePA.ToolTip = Resources.CommonMenu.radMnuModulePA;
                            _nodeRootModulePA.CssClass = "Folder";
                            _nodeRootModulePA.PostBack = false;

                            //_node = new RadTreeNode(Resources.CommonListManage.Calculations, _pkNode++.ToString());
                            //_node.ToolTip = Resources.CommonListManage.Calculations;
                            //_node.Checkable = false;
                            //_node.CssClass = "Calculation";
                            //_node.PostBack = true;
                            //_node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.Calculations);
                            //_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Calculation);
                            //_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Calculation);
                            //_node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            //_node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            //_node.Attributes.Add("PermissionType", _permissionType);
                            ////Mete el Process dentro del modulo.
                            //_nodeRootModulePA.Nodes.Add(_node);

                            //Con este se muestran todas las mediciones no importa si es relevante
                            _node = new RadTreeNode(Resources.CommonListManage.Measurements, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Measurements;
                            _node.Checkable = false;
                            _node.CssClass = "Indicator";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.AllKeyIndicators);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.KeyIndicator);
                            _node.Attributes.Add("EntityNameComboFilter", Common.ConstantsEntitiesName.PA.AccountingActivities);
                            _node.Attributes.Add("EntityNameChildrenComboFilter", Common.ConstantsEntitiesName.PA.AccountingActivitiesChildren);
                            _node.Attributes.Add("IsFilterHierarchy", "true");
                            //_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Measurement);
                            //_node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);

                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Measurement);
                            //_node.Attributes.Add("EntityNameContextInfo", String.Empty);
                            _node.Attributes.Add("EntityNameContextElement", String.Empty);

                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePA.Nodes.Add(_node);

                            //_node = new RadTreeNode(Resources.CommonListManage.KeyParameters, _pkNode++.ToString());
                            //_node.ToolTip = Resources.CommonListManage.KeyParameters;
                            //_node.Checkable = false;
                            //_node.CssClass = "Indicator";
                            //_node.PostBack = true;
                            //_node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.KeyIndicators);
                            //_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.KeyIndicator);
                            //_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                            //_node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            //_node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            //_node.Attributes.Add("PermissionType", _permissionType);
                            ////Mete el Process dentro del modulo.
                            //_nodeRootModulePA.Nodes.Add(_node);

                            #region Calculation of Transformation
                                _node = new RadTreeNode(Resources.CommonListManage.Transformations, _pkNode++.ToString());
                                _node.ToolTip = Resources.CommonListManage.Transformations;
                                _node.Attributes.Add("EntityNameGrid", String.Empty);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.TransformationByTransformation);
                                _node.Attributes.Add("EntityNameHierarchical", Common.ConstantsEntitiesName.PA.MeasurementsOfTransformation);
                                _node.Attributes.Add("EntityNameHierarchicalChildren", Common.ConstantsEntitiesName.PA.BasedMeasurementsOfTheTransformations);
                                _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PA.Transformation);
                            
                                _node.Attributes.Add("EntityNameComboFilter", Common.ConstantsEntitiesName.PA.AccountingActivities);
                                _node.Attributes.Add("EntityNameChildrenComboFilter", Common.ConstantsEntitiesName.PA.AccountingActivitiesChildren);
                                _node.Attributes.Add("IsFilterHierarchy", "true");

                                _node.Attributes.Add("URL", "~/Managers/HierarchicalListManage.aspx");
                                _node.Attributes.Add("PermissionType", _permissionType);
                                _node.PostBack = true;
                                _node.CssClass = "Transformation";
                                _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByProcess.Nodes.Add(_nodeRootModulePA);
                            #endregion

                            #region KC
                            RadTreeNode _nodeRootModuleKC = new RadTreeNode(Resources.CommonMenu.radMnuModuleKC, _pkNode++.ToString());
                            _nodeRootModuleKC.ToolTip = Resources.CommonMenu.radMnuModuleKC;
                            _nodeRootModuleKC.CssClass = "Folder";
                            _nodeRootModuleKC.PostBack = false;

                            _node = new RadTreeNode(Resources.CommonListManage.Resources, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Resources;
                            _node.Checkable = false;
                            _node.CssClass = "Resource";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessResources);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessResource);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModuleKC.Nodes.Add(_node);

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByProcess.Nodes.Add(_nodeRootModuleKC);
                            #endregion

                            #region REPORT
                                RadTreeNode _nodeRootModuleRPT = CreateRootNode(Resources.CommonMenu.radMnuModuleRPT, _pkNode++.ToString(), Resources.CommonMenu.radMnuModuleRPT, "Folder", false);
                                _nodeRootModuleRPT.EnableContextMenu = false;

                                #region Reportes viejos
                                //_node = new RadTreeNode(Resources.CommonMenu.mnuContextInfoGeneralProperties, _pkNode++.ToString());
                                //_node.ToolTip = Resources.CommonMenu.mnuContextInfoGeneralProperties;
                                //_node.CssClass = "GeneralProperties";
                                ////Solo para este caso. Se hace un evento cliente, ya que tiene que abrir una nueva pagina con el contenido del reporte.
                                //_node.PostBack = false;
                                //_node.EnableContextMenu = false;
                                //_node.Attributes.Add("onclick", "return ShowReportToPrintProjectBuyerSummary(event, " + _idProcess.ToString() + ");");
                                //_node.Attributes.Add("onmouseover", "this.style.cursor = 'hand'");
                                ////Mete el Process dentro del modulo.
                                //_nodeRootModuleRPT.Nodes.Add(_node);

                                //_node = new RadTreeNode(Resources.CommonMenu.mnuContextInfoSummaryForOwner, _pkNode++.ToString());
                                //_node.ToolTip = Resources.CommonMenu.mnuContextInfoSummaryForOwner;
                                //_node.CssClass = "SummaryForOwner";
                                //_node.PostBack = true;
                                //_node.EnableContextMenu = false;
                                ////_node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.CT.FullTopics);
                                ////_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.CT.Message);
                                ////_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.CT.Forum);
                                //_node.Attributes.Add("URL", "~/Dashboard/ProyectBuyerSummaryContent.aspx");
                                ////_node.Attributes.Add("PermissionType", _permissionType);
                                
                                ////Mete el Process dentro del modulo.
                                //_nodeRootModuleRPT.Nodes.Add(_node);


                                //_node = new RadTreeNode(Resources.CommonMenu.mnuContextInfoEmissionsReporting, _pkNode++.ToString());
                                //_node.ToolTip = Resources.CommonMenu.mnuContextInfoEmissionsReporting;
                                //_node.CssClass = "SummaryForOwner";
                                //_node.PostBack = true;
                                //_node.EnableContextMenu = false;
                                //_node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                //_node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                //_node.Attributes.Add("EntityNameContextInfo", String.Empty);
                                //_node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                ////Mete el Process dentro del modulo.
                                //_nodeRootModuleRPT.Nodes.Add(_node);
                                #endregion

                                #region Intra-Observatory
                                    RadTreeNode _nodeRootModuleIntraObservatory = CreateRootNode(Resources.Common.IntraObservatory, _pkNode++.ToString(), Resources.Common.IntraObservatory, "Folder", false);
                                    _nodeRootModuleIntraObservatory.EnableContextMenu = false;

                                    #region General
                                        RadTreeNode _nodeRootModuleGeneral = CreateRootNode(Resources.Common.General, _pkNode++.ToString(), Resources.Common.General, "Folder", false);
                                        _nodeRootModuleGeneral.EnableContextMenu = false;

                                        #region CrossCheck
                                            _node = new RadTreeNode(Resources.CommonMenu.mnuContextInfoEmissionsByFacility, _pkNode++.ToString());
                                            _node.ToolTip = Resources.CommonMenu.mnuContextInfoEmissionsByFacility;
                                            _node.CssClass = "SummaryForOwner";
                                            _node.PostBack = true;
                                            _node.EnableContextMenu = false;
                                            _node.Attributes.Add("EntityNameGrid", "EmissionByFacility");
                                            _node.Attributes.Add("EntityName", "EmissionByFacility");
                                            _node.Attributes.Add("EntityNameContextInfo", String.Empty);
                                            _node.Attributes.Add("URL", "~/Dashboard/FilterListEmissionsByFacility.aspx");
                                            //Mete el Process dentro del modulo.
                                            _nodeRootModuleGeneral.Nodes.Add(_node);
                                        #endregion

                                        #region Indicator Tracker
                                            _node = new RadTreeNode(Resources.CommonMenu.mnuContextInfoIndicatorTracker, _pkNode++.ToString());
                                            _node.ToolTip = Resources.CommonMenu.mnuContextInfoIndicatorTracker;
                                            _node.CssClass = "SummaryForOwner";
                                            _node.PostBack = true;
                                            _node.EnableContextMenu = false;
                                            _node.Attributes.Add("EntityNameGrid", String.Empty);
                                            _node.Attributes.Add("EntityName", String.Empty);
                                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                            _node.Attributes.Add("URL", "~/Dashboard/ReportIndicatorTracker.aspx");
                                            //Mete el Process dentro del modulo.
                                            _nodeRootModuleGeneral.Nodes.Add(_node);
                                        #endregion

                                        #region Facility Analyzer
                                            //Falta hacerlo
                                            _node = new RadTreeNode(Resources.CommonMenu.mnuContextInfoFacilityAnalyzer, _pkNode++.ToString());
                                            _node.ToolTip = Resources.CommonMenu.mnuContextInfoFacilityAnalyzer;
                                            _node.CssClass = "SummaryForOwner";
                                            _node.PostBack = true;
                                            _node.EnableContextMenu = false;
                                            _node.Attributes.Add("EntityNameGrid", String.Empty);
                                            _node.Attributes.Add("EntityName", String.Empty);
                                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                            _node.Attributes.Add("URL", "~/Dashboard/ReportFacilityAnalyzer.aspx");
                                            //Mete el Process dentro del modulo.
                                            _nodeRootModuleGeneral.Nodes.Add(_node);
                                        #endregion

                                    #endregion

                                    #region GEI
                                        RadTreeNode _nodeRootModuleGEI = CreateRootNode(Resources.Common.GEI, _pkNode++.ToString(), Resources.Common.GEI, "Folder", false);
                                        _nodeRootModuleGEI.EnableContextMenu = false;

                                        _node = new RadTreeNode(Resources.Common.GA_S_A_FT_F, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.GA_S_A_FT_F;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "GEI");
                                        _node.Attributes.Add("PK_ReportType", "GA_S_A_FT_F");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleGEI.Nodes.Add(_node);

                                        _node = new RadTreeNode(Resources.Common.GA_FT_F_S_A, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.GA_FT_F_S_A;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "GEI");
                                        _node.Attributes.Add("PK_ReportType", "GA_FT_F_S_A");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleGEI.Nodes.Add(_node);

                                        _node = new RadTreeNode(Resources.Common.S_GA_A_FT_F, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.S_GA_A_FT_F;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "GEI");
                                        _node.Attributes.Add("PK_ReportType", "S_GA_A_FT_F");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleGEI.Nodes.Add(_node);

                                        _node = new RadTreeNode(Resources.Common.S_A_FT_F, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.S_A_FT_F;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "GEI");
                                        _node.Attributes.Add("PK_ReportType", "S_A_FT_F");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleGEI.Nodes.Add(_node);

                                        _node = new RadTreeNode(Resources.Common.FT_F_S_A, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.FT_F_S_A;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "GEI");
                                        _node.Attributes.Add("PK_ReportType", "FT_F_S_A");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleGEI.Nodes.Add(_node);

                                        _node = new RadTreeNode(Resources.Common.O_S_A_FT_F, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.O_S_A_FT_F;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "GEI");
                                        _node.Attributes.Add("PK_ReportType", "O_S_A_FT_F");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleGEI.Nodes.Add(_node);

                                        _node = new RadTreeNode(Resources.Common.Evolution, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.Evolution;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "GEI");
                                        _node.Attributes.Add("PK_ReportType", "Evolution");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleGEI.Nodes.Add(_node);
                                    #endregion

                                    #region Contaminantes Locales
                                        RadTreeNode _nodeRootModuleCL = CreateRootNode(Resources.Common.CL, _pkNode++.ToString(), Resources.Common.CL, "Folder", false);
                                        _nodeRootModuleCL.EnableContextMenu = false;

                                        _node = new RadTreeNode(Resources.Common.GA_S_A_FT_F, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.GA_S_A_FT_F;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "CL");
                                        _node.Attributes.Add("PK_ReportType", "GA_S_A_FT_F");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleCL.Nodes.Add(_node);

                                        _node = new RadTreeNode(Resources.Common.GA_FT_F_S_A, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.GA_FT_F_S_A;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "CL");
                                        _node.Attributes.Add("PK_ReportType", "GA_FT_F_S_A");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleCL.Nodes.Add(_node);

                                        _node = new RadTreeNode(Resources.Common.S_GA_A_FT_F, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.S_GA_A_FT_F;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "CL");
                                        _node.Attributes.Add("PK_ReportType", "S_GA_A_FT_F");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleCL.Nodes.Add(_node);

                                        _node = new RadTreeNode(Resources.Common.S_A_FT_F, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.S_A_FT_F;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "CL");
                                        _node.Attributes.Add("PK_ReportType", "S_A_FT_F");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleCL.Nodes.Add(_node);

                                        _node = new RadTreeNode(Resources.Common.FT_F_S_A, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.FT_F_S_A;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "CL");
                                        _node.Attributes.Add("PK_ReportType", "FT_F_S_A");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleCL.Nodes.Add(_node);

                                        _node = new RadTreeNode(Resources.Common.O_S_A_FT_F, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.O_S_A_FT_F;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "CL");
                                        _node.Attributes.Add("PK_ReportType", "O_S_A_FT_F");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleCL.Nodes.Add(_node);

                                        _node = new RadTreeNode(Resources.Common.Evolution, _pkNode++.ToString());
                                        _node.ToolTip = Resources.Common.Evolution;
                                        _node.CssClass = "SummaryForOwner";
                                        _node.PostBack = true;
                                        _node.EnableContextMenu = false;
                                        _node.Attributes.Add("EntityNameGrid", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityName", "ReportTransformationByScope");
                                        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                        _node.Attributes.Add("PK_Report", "CL");
                                        _node.Attributes.Add("PK_ReportType", "Evolution");
                                        _node.Attributes.Add("URL", "~/Dashboard/FilterRptCalculationsOfTransformation.aspx");
                                        //Mete el Process dentro del modulo.
                                        _nodeRootModuleCL.Nodes.Add(_node);
                                    #endregion

                                #endregion

                                _nodeRootModuleIntraObservatory.Nodes.Add(_nodeRootModuleGeneral);
                                _nodeRootModuleIntraObservatory.Nodes.Add(_nodeRootModuleGEI);
                                _nodeRootModuleIntraObservatory.Nodes.Add(_nodeRootModuleCL);

                                //Ya tenemos todos los reportes, ahora insertamos el nodo GEI
                                _nodeRootModuleRPT.Nodes.Add(_nodeRootModuleIntraObservatory);

                                #region Inter-Observatory
                                    RadTreeNode _nodeRootModuleInterObservatory = CreateRootNode(Resources.Common.InterObservatory, _pkNode++.ToString(), Resources.Common.InterObservatory, "Folder", false);
                                    _nodeRootModuleInterObservatory.EnableContextMenu = false;

                                    RadTreeNode _nodeRootModuleGEIInter = CreateRootNode(Resources.Common.GEI, _pkNode++.ToString(), Resources.Common.GEI, "Folder", false);
                                    _nodeRootModuleInterObservatory.EnableContextMenu = false;

                                    _nodeRootModuleInterObservatory.Nodes.Add(_nodeRootModuleGEIInter);

                                    _node = new RadTreeNode(Resources.CommonListManage.MultiObservatory, _pkNode++.ToString());
                                    _node.ToolTip = Resources.CommonListManage.MultiObservatory;
                                    _node.CssClass = "SummaryForOwner";
                                    _node.PostBack = true;
                                    _node.EnableContextMenu = false;
                                    _node.Attributes.Add("EntityNameGrid", String.Empty);
                                    _node.Attributes.Add("EntityName", String.Empty);
                                    _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                    _node.Attributes.Add("URL", "~/Dashboard/ReportMultiObservatory.aspx");
                                    //Mete el Process dentro del modulo.
                                    _nodeRootModuleGEIInter.Nodes.Add(_node);
                            
                                    _nodeRootModuleRPT.Nodes.Add(_nodeRootModuleGEIInter);
                            
                                    //Ya tenemos todos los reportes, ahora insertamos el nodo GEI
                                    _nodeRootModuleRPT.Nodes.Add(_nodeRootModuleInterObservatory);

                                #endregion

                                //Mete el modulo como raiz.
                                _rdtvGRCProcessFrameworkByProcess.Nodes.Add(_nodeRootModuleRPT);
                            #endregion

                            #region IA
                                RadTreeNode _nodeRootModuleIA = new RadTreeNode(Resources.CommonMenu.radMnuModuleIA, _pkNode++.ToString());
                                _nodeRootModuleIA.ToolTip = Resources.CommonMenu.radMnuModuleIA;
                                _nodeRootModuleIA.CssClass = "Folder";
                                _nodeRootModuleIA.PostBack = false;
                                _nodeRootModuleIA.EnableContextMenu = false;

                                _node = new RadTreeNode(Resources.CommonListManage.Exceptions, _pkNode++.ToString());
                                _node.ToolTip = Resources.CommonListManage.Exceptions;
                                _node.Checkable = false;
                                _node.CssClass = "Exception";
                                _node.PostBack = true;
                                _node.EnableContextMenu = false;
                                _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.IA.Exceptions);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.IA.Exception);
                                _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                                _node.Attributes.Add("PermissionType", _permissionType);
                                //Mete el Process dentro del modulo.
                                _nodeRootModuleIA.Nodes.Add(_node);

                                //Mete el modulo como raiz.
                                _rdtvGRCProcessFrameworkByProcess.Nodes.Add(_nodeRootModuleIA);
                            #endregion

                            #region CT
                            //RadTreeNode _nodeRootModuleCT = CreateRootNode(Resources.CommonMenu.radMnuModuleCT, _pkNode++.ToString(), Resources.CommonMenu.radMnuModuleCT, "Folder", false);

                            //#region Forum
                            //_node = new RadTreeNode(Resources.CommonListManage.Forums, _pkNode++.ToString());
                            //_node.ToolTip = Resources.CommonListManage.Forums;
                            //_node.CssClass = "Forum";
                            //_node.PostBack = true;
                            //_node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.CT.Forums);
                            //_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.CT.Forum);
                            //_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.CT.Forum);
                            //_node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            //_node.Attributes.Add("PermissionType", _permissionType);
                            ////Mete el Process dentro del modulo.
                            //_nodeRootModuleCT.Nodes.Add(_node);

                            //_node = new RadTreeNode(Resources.CommonListManage.AllTopics, _pkNode++.ToString());
                            //_node.ToolTip = Resources.CommonListManage.AllTopics;
                            //_node.CssClass = "AllTopic";
                            //_node.PostBack = true;
                            //_node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.CT.FullTopics);
                            //_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.CT.Message);
                            //_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.CT.Forum);
                            //_node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            //_node.Attributes.Add("PermissionType", _permissionType);
                            ////Mete el Process dentro del modulo.
                            //_nodeRootModuleCT.Nodes.Add(_node);
                            //#endregion

                            ////Mete el modulo como raiz.
                            //_rdtvGRCProcessFrameworkByProcess.Nodes.Add(_nodeRootModuleCT);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByProcess;
                    }
                    private void LoadAllParameter(ref RadTreeNode node)
                    {
                        //Carga el DataTable.
                        Int64 _idIndicator_tnCO2e;
                        Int64 _idIndicator_CO2;
                        Int64 _idIndicator_CH4;
                        Int64 _idIndicator_N2O;
                        Int64 _idIndicator_PFC;
                        Int64 _idIndicator_HFC;
                        Int64 _idIndicator_SF6;

                        Int64 _idIndicator_HCNM;
                        Int64 _idIndicator_HCT;
                        Int64 _idIndicator_CO;
                        Int64 _idIndicator_NOx;
                        Int64 _idIndicator_SOx;
                        Int64 _idIndicator_MP;
                        Int64 _idIndicator_SO2;
                        Int64 _idIndicator_H2S;
                        Int64 _idIndicator_MP10;
                        Int64 _idIndicator_C2H6;
                        Int64 _idIndicator_C3H8;
                        Int64 _idIndicator_C4H10;

                        #region Indicator GAS Config
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_tnCO2e"], out _idIndicator_tnCO2e);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_CO2"], out _idIndicator_CO2);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_CH4"], out _idIndicator_CH4);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_N2O"], out _idIndicator_N2O);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_PFC"], out _idIndicator_PFC);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_HFC"], out _idIndicator_HFC);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_SF6"], out _idIndicator_SF6);

                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_HCNM"], out _idIndicator_HCNM);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_HCT"], out _idIndicator_HCT);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_CO"], out _idIndicator_CO);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_NOx"], out _idIndicator_NOx);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_SOx"], out _idIndicator_SOx);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_PM"], out _idIndicator_MP);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_SO2"], out _idIndicator_SO2);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_H2S"], out _idIndicator_H2S);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_PM10"], out _idIndicator_MP10);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_C2H6"], out _idIndicator_C2H6);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_C3H8"], out _idIndicator_C3H8);
                        Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_C4H10"], out _idIndicator_C4H10);

                        node.Attributes.Add("PK_IdIndicator_tnCO2e", _idIndicator_tnCO2e.ToString());
                        node.Attributes.Add("PK_IdIndicator_CO2", _idIndicator_CO2.ToString());
                        node.Attributes.Add("PK_IdIndicator_CH4", _idIndicator_CH4.ToString());
                        node.Attributes.Add("PK_IdIndicator_N2O", _idIndicator_N2O.ToString());
                        node.Attributes.Add("PK_IdIndicator_PFC", _idIndicator_PFC.ToString());
                        node.Attributes.Add("PK_IdIndicator_HFC", _idIndicator_HFC.ToString());
                        node.Attributes.Add("PK_IdIndicator_SF6", _idIndicator_SF6.ToString());
                        node.Attributes.Add("PK_IdIndicator_HCNM", _idIndicator_HCNM.ToString());
                        node.Attributes.Add("PK_IdIndicator_HCT", _idIndicator_HCT.ToString());
                        node.Attributes.Add("PK_IdIndicator_CO", _idIndicator_CO.ToString());
                        node.Attributes.Add("PK_IdIndicator_NOx", _idIndicator_NOx.ToString());
                        node.Attributes.Add("PK_IdIndicator_SOx", _idIndicator_SOx.ToString());
                        node.Attributes.Add("PK_IdIndicator_PM", _idIndicator_MP.ToString());
                        node.Attributes.Add("PK_IdIndicator_SO2", _idIndicator_SO2.ToString());
                        node.Attributes.Add("PK_IdIndicator_H2S", _idIndicator_H2S.ToString());
                        node.Attributes.Add("PK_IdIndicator_PM10", _idIndicator_MP10.ToString());
                        node.Attributes.Add("PK_IdIndicator_C2H6", _idIndicator_C2H6.ToString());
                        node.Attributes.Add("PK_IdIndicator_C3H8", _idIndicator_C3H8.ToString());
                        node.Attributes.Add("PK_IdIndicator_C4H10", _idIndicator_C4H10.ToString());                        

                        #endregion

                    }
                #endregion

                #region Process Group Node
                    public RadTreeView BuildContextInfoMenuProcessGroupNode(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByProcessGroupNode = new RadTreeView();
                        if (param.ContainsKey("IdProcess"))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                            Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _process = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).LanguageOption.Title;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByProcessGroupNode.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByProcessGroupNode.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByProcessGroupNode.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByProcessGroupNode.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByProcessGroupNode.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByProcessGroupNode.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByProcessGroupNode.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByProcessGroupNode.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByProcessGroupNode.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByProcessGroupNode.Attributes.Add("PK_IdProcess", _idProcess.ToString());
                            //_rdtvGRCProcessFrameworkByProcessGroupNode.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);

                            RadTreeNode _node = null;
                            #region PF
                            RadTreeNode _nodeRootModulePF = new RadTreeNode(Resources.CommonMenu.radMnuModulePF, _pkNode++.ToString());
                            _nodeRootModulePF.ToolTip = Resources.CommonMenu.radMnuModulePF;
                            _nodeRootModulePF.CssClass = "PF";
                            _nodeRootModulePF.PostBack = false;

                            _node = new RadTreeNode(Resources.CommonListManage.ExtendedProperties, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ExtendedProperties;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "ExtendedProperties";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessExtendedProperties);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessExtendedProperty);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePF.Nodes.Add(_node);

                            //Nodo de Nodo...
                            _node = new RadTreeNode(Resources.CommonListManage.ProcessGroupNode, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ProcessGroupNode;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "ExtendedProperties";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessGroupNodes);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessGroupNode);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessGroupNode);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePF.Nodes.Add(_node);


                            RadTreeNode _nodeRootTasks = new RadTreeNode(Resources.CommonListManage.ProcessTasks, _pkNode++.ToString());
                            _nodeRootTasks.ToolTip = Resources.CommonListManage.ProcessTasks;
                            _nodeRootTasks.CssClass = "PF";
                            _nodeRootTasks.PostBack = false;

                            _node = new RadTreeNode(Resources.CommonListManage.ProcessTaskCalibration, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ProcessTaskCalibration;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "ExtendedProperties";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessTaskCalibrations);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskCalibration);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskCalibration);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete las tasks dentro del root de trask
                            _nodeRootTasks.Nodes.Add(_node);

                            _node = new RadTreeNode(Resources.CommonListManage.ProcessTaskOperation, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ProcessTaskOperation;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "ExtendedProperties";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessTaskOperations);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskOperation);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskOperation);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete las tasks dentro del root de trask
                            _nodeRootTasks.Nodes.Add(_node);

                            _node = new RadTreeNode(Resources.CommonListManage.ProcessTaskMeasurement, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ProcessTaskMeasurement;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "ExtendedProperties";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessTaskMeasurements);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete las tasks dentro del root de trask
                            _nodeRootTasks.Nodes.Add(_node);

                            //Mete el RootTask dentro del root module.
                            _nodeRootModulePF.Nodes.Add(_nodeRootTasks);

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByProcessGroupNode.Nodes.Add(_nodeRootModulePF);
                            #endregion

                            #region IA
                                RadTreeNode _nodeRootModuleIA = new RadTreeNode(Resources.CommonMenu.radMnuModuleIA, _pkNode++.ToString());
                                _nodeRootModuleIA.ToolTip = Resources.CommonMenu.radMnuModuleIA;
                                _nodeRootModuleIA.CssClass = "IA";
                                _nodeRootModuleIA.PostBack = false;
                                _nodeRootModuleIA.EnableContextMenu = false;

                                _node = new RadTreeNode(Resources.CommonListManage.Exceptions, _pkNode++.ToString());
                                _node.ToolTip = Resources.CommonListManage.Exceptions;
                                _node.Checkable = false;
                                _node.CssClass = "Exception";
                                _node.PostBack = true;
                                _node.EnableContextMenu = false; 
                                _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.IA.Exceptions);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.IA.Exception);
                                _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                                _node.Attributes.Add("PermissionType", _permissionType);
                                //Mete el Process dentro del modulo.
                                _nodeRootModuleIA.Nodes.Add(_node);

                                //Mete el modulo como raiz.
                                _rdtvGRCProcessFrameworkByProcessGroupNode.Nodes.Add(_nodeRootModuleIA);
                            #endregion
                           
                        }
                        return _rdtvGRCProcessFrameworkByProcessGroupNode;
                    }
                #endregion

                #region Process Task Calibration
                    public RadTreeView BuildContextInfoMenuProcessTaskCalibration(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByProcessTaskCalibration = new RadTreeView();
                        if ((param.ContainsKey("IdProcess")) && (param.ContainsKey("IdTask")))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                            Int64 _idTask = Convert.ToInt64(param["IdTask"]);
                            Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _process = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = EMSLibrary.User.ProcessFramework.Map.Process(_idTask).LanguageOption.Title;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.Attributes.Add("PK_IdProcess", _idProcess.ToString());
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.Attributes.Add("PK_IdTask", _idTask.ToString());
                            //_rdtvGRCProcessFrameworkByProcessTaskCalibration.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);

                            RadTreeNode _node = null;

                            #region PF
                            RadTreeNode _nodeRootModulePF = new RadTreeNode(Resources.CommonMenu.radMnuModulePF, _pkNode++.ToString());
                            _nodeRootModulePF.ToolTip = Resources.CommonMenu.radMnuModulePF;
                            _nodeRootModulePF.CssClass = "PF";
                            _nodeRootModulePF.PostBack = false;

                            //TODO: Cuando exista el metodo que devuelva las ejecuciones de un proceso... se habilita.
                            _node = new RadTreeNode(Resources.CommonListManage.Executions, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Executions;
                            _node.Checkable = false;
                            _node.CssClass = "Execution";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessTaskExecutions);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskExecutionCalibration);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskExecutionCalibration);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePF.Nodes.Add(_node);

                            _node = new RadTreeNode(Resources.CommonListManage.ExtendedProperties, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ExtendedProperties;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "ExtendedProperties";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessExtendedProperties);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessExtendedProperty);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskCalibration);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePF.Nodes.Add(_node);

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.Nodes.Add(_nodeRootModulePF);
                            #endregion

                            #region PA
                            RadTreeNode _nodeRootModulePA = new RadTreeNode(Resources.CommonMenu.radMnuModulePA, _pkNode++.ToString());
                            _nodeRootModulePA.ToolTip = Resources.CommonMenu.radMnuModulePA;
                            _nodeRootModulePA.CssClass = "PA";
                            _nodeRootModulePA.PostBack = false;

                            _node = new RadTreeNode(Resources.CommonListManage.MeasurementDevices, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.MeasurementDevices;
                            _node.Checkable = false;
                            _node.CssClass = "MeasurementDevice";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.MeasurementDevicesByTask);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.MeasurementDevice);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskCalibration);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);

                            //Mete el Process dentro del modulo.
                            _nodeRootModulePA.Nodes.Add(_node);
                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.Nodes.Add(_nodeRootModulePA);
                            #endregion

                            #region IA
                                RadTreeNode _nodeRootModuleIA = new RadTreeNode(Resources.CommonMenu.radMnuModuleIA, _pkNode++.ToString());
                                _nodeRootModuleIA.ToolTip = Resources.CommonMenu.radMnuModuleIA;
                                _nodeRootModuleIA.CssClass = "IA";
                                _nodeRootModuleIA.PostBack = false;
                                _nodeRootModuleIA.EnableContextMenu = false;

                                _node = new RadTreeNode(Resources.CommonListManage.Exceptions, _pkNode++.ToString());
                                _node.ToolTip = Resources.CommonListManage.Exceptions;
                                _node.Checkable = false;
                                _node.CssClass = "Exception";
                                _node.PostBack = true;
                                _node.EnableContextMenu = false;
                                _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.IA.Exceptions);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.IA.Exception);
                                _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                                _node.Attributes.Add("PermissionType", _permissionType);
                                //Mete el Process dentro del modulo.
                                _nodeRootModuleIA.Nodes.Add(_node);

                                //Mete el modulo como raiz.
                                _rdtvGRCProcessFrameworkByProcessTaskCalibration.Nodes.Add(_nodeRootModuleIA);
                            #endregion
                            
                        }

                        return _rdtvGRCProcessFrameworkByProcessTaskCalibration;
                    }
                    public RadTreeView BuildContextInfoMenuProcessTaskExecutionCalibration(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByProcessTaskCalibration = new RadTreeView();
                        if ((param.ContainsKey("IdProcess")) && (param.ContainsKey("IdTask")))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                            Int64 _idTask = Convert.ToInt64(param["IdTask"]);
                            Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _process = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = EMSLibrary.User.ProcessFramework.Map.Process(_idTask).LanguageOption.Title;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.Attributes.Add("PK_IdProcess", _idProcess.ToString());
                            _rdtvGRCProcessFrameworkByProcessTaskCalibration.Attributes.Add("PK_IdTask", _idTask.ToString());
                            //_rdtvGRCProcessFrameworkByProcessTaskCalibration.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);

                            RadTreeNode _node = null;

                            #region IA
                                RadTreeNode _nodeRootModuleIA = new RadTreeNode(Resources.CommonMenu.radMnuModuleIA, _pkNode++.ToString());
                                _nodeRootModuleIA.ToolTip = Resources.CommonMenu.radMnuModuleIA;
                                _nodeRootModuleIA.CssClass = "IA";
                                _nodeRootModuleIA.PostBack = false;
                                _nodeRootModuleIA.EnableContextMenu = false;

                                _node = new RadTreeNode(Resources.CommonListManage.Exceptions, _pkNode++.ToString());
                                _node.ToolTip = Resources.CommonListManage.Exceptions;
                                _node.Checkable = false;
                                _node.CssClass = "Exception";
                                _node.PostBack = true;
                                _node.EnableContextMenu = false;
                                _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.IA.Exceptions);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.IA.Exception);
                                _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                                _node.Attributes.Add("PermissionType", _permissionType);
                                //Mete el Process dentro del modulo.
                                _nodeRootModuleIA.Nodes.Add(_node);

                                //Mete el modulo como raiz.
                                _rdtvGRCProcessFrameworkByProcessTaskCalibration.Nodes.Add(_nodeRootModuleIA);
                            #endregion

                        }

                        return _rdtvGRCProcessFrameworkByProcessTaskCalibration;
                    }
                #endregion

                #region Process Task Measurement
                    public RadTreeView BuildContextInfoMenuProcessTaskMeasurement(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByProcessTaskMeasurement = new RadTreeView();
                        if ((param.ContainsKey("IdProcess")) && (param.ContainsKey("IdTask")))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                            Int64 _idTask = Convert.ToInt64(param["IdTask"]);
                            Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _process = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = EMSLibrary.User.ProcessFramework.Map.Process(_idTask).LanguageOption.Title;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }


                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.Attributes.Add("PK_IdProcess", _idProcess.ToString());
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.Attributes.Add("PK_IdTask", _idTask.ToString());
                            //_rdtvGRCProcessFrameworkByProcessTaskMeasurement.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);

                            RadTreeNode _node = null;

                            #region PF
                            RadTreeNode _nodeRootModulePF = new RadTreeNode(Resources.CommonMenu.radMnuModulePF, _pkNode++.ToString());
                            _nodeRootModulePF.ToolTip = Resources.CommonMenu.radMnuModulePF;
                            _nodeRootModulePF.CssClass = "PF";
                            _nodeRootModulePF.PostBack = false;

                            //TODO: Cuando exista el metodo que devuelva las ejecuciones de un proceso... se habilita.
                            _node = new RadTreeNode(Resources.CommonListManage.Executions, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Executions;
                            _node.Checkable = false;
                            _node.CssClass = "Execution";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessTaskExecutions);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePF.Nodes.Add(_node);

                            _node = new RadTreeNode(Resources.CommonListManage.ExtendedProperties, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ExtendedProperties;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "ExtendedProperties";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessExtendedProperties);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessExtendedProperty);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePF.Nodes.Add(_node);

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.Nodes.Add(_nodeRootModulePF);
                            #endregion

                            #region PA
                                RadTreeNode _nodeRootModulePA = new RadTreeNode(Resources.CommonMenu.radMnuModulePA, _pkNode++.ToString());
                                _nodeRootModulePA.ToolTip = Resources.CommonMenu.radMnuModulePA;
                                _nodeRootModulePA.CssClass = "PA";
                                _nodeRootModulePA.PostBack = false;

                                Measurement _measurement = ((ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_idTask)).Measurement;

                            #region Chart
                                _node = new RadTreeNode(Resources.Common.Chart, _pkNode++.ToString());
                                _node.ToolTip = Resources.Common.Chart;
                                _node.Checkable = false;
                                _node.CssClass = "Measurement";
                                _node.PostBack = false;

                                String _title = _measurement.LanguageOption.Name;
                                //Aca hacemos el replace para evitar errores por seguridad de los navegadores.
                                _title = _title.Replace("<sub>", "__").Replace("</sub>", "__").Replace("<sup>", "--").Replace("</sup>", "--");

                                String _keyValues = "Title=" + _title
                                    + "&IdMeasurement=" + _measurement.IdMeasurement.ToString()
                                    + "&EntityName=" + Common.ConstantsEntitiesName.PA.Measurement;

                                //Solo para este caso, que esta dentro de un iFrame, se reemplaza el & por | (pipe)
                                _node.Attributes.Add("PkCompost", _keyValues.Replace("&", "|"));
                                _node.Attributes.Add("Text", _title);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Measurement);
                                _node.Attributes.Add("EntityNameGrid", String.Empty);
                                _node.Attributes.Add("EntityNameContextInfo", String.Empty);
                                _node.Attributes.Add("EntityNameContextElement", String.Empty);
                                _node.Attributes["onclick"] = string.Format("return ShowChart(event, " + _measurement.IdMeasurement + ");");

                                //Mete el Process dentro del modulo.
                                _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            #region Data Series
                                _node = new RadTreeNode(Resources.Common.DataSeries, _pkNode++.ToString());
                                _node.ToolTip = Resources.Common.DataSeries;
                                _node.Checkable = false;
                                _node.CssClass = "Measurement";
                                _node.PostBack = true;
                                //_node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PA.MeasurementsByTask);
                                //_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Measurement);
                                //_node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskMeasurement);
                                //_node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                //_node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                                //_node.Attributes.Add("PermissionType", _permissionType);

                                //Measurement _measurement = ((ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_idTask)).Measurement;

                                _title = _measurement.LanguageOption.Name;
                                //Aca hacemos el replace para evitar errores por seguridad de los navegadores.
                                _title = _title.Replace("<sub>", "__").Replace("</sub>", "__").Replace("<sup>", "--").Replace("</sup>", "--");

                                _keyValues = "Title=" + _title
                                    + "&IdMeasurement=" + _measurement.IdMeasurement.ToString()
                                    + "&EntityName=" + Common.ConstantsEntitiesName.PA.Measurement;

                                ////Solo para este caso, que esta dentro de un iFrame, se reemplaza el & por | (pipe)
                                //_node.Attributes.Add("PkCompost", _keyValues.Replace("&", "|"));
                                //_node.Attributes.Add("Text", _title);
                                //_node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Measurement);
                                //_node.Attributes.Add("EntityNameGrid", String.Empty);
                                //_node.Attributes.Add("EntityNameContextInfo", String.Empty);
                                //_node.Attributes.Add("EntityNameContextElement", String.Empty);
                                //_node.Attributes["onclick"] = string.Format("javascript:ShowSeries(this, event);");

                                _node.Attributes.Add("PK_IdMeasurement", _measurement.IdMeasurement.ToString());
                                _node.Attributes.Add("PkCompost", _keyValues.Replace("&", "|"));
                                _node.Attributes.Add("PageTitle", _title);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PA.Measurement);
                                _node.Attributes.Add("EntityNameGrid", String.Empty);
                                _node.Attributes.Add("EntityNameContextInfo", String.Empty);
                                _node.Attributes.Add("EntityNameContextElement", String.Empty);
                                _node.Attributes.Add("URL", "~/Managers/IndicatorSeries.aspx");
                                
                                //String _args = "ContextInfoNavigation_" + _keyValues.Replace("&", "|");
                                //NavigateMenuEventArgs _menuArgs = GetNavigateMenuEventArgs(NavigateMenuType.ContextInfoMenu, _args);

                                //String _entityClassName = String.Concat(Common.Functions.ReplaceIndexesTags(_GTNText), " [", GetValueFromGlobalResource("CommonListManage", _GTNEntityName), "]", " [", Resources.Common.mnuView, "]");
                                //Navigate("~/Managers/IndicatorSeries.aspx", _entityClassName, _menuArgs);

                                //Mete el Process dentro del modulo.
                                _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            #region Calculation of Transformation
                            _node = CreateNode(Resources.CommonListManage.Transformations, _pkNode++.ToString(), Resources.CommonListManage.Transformations, String.Empty, Common.ConstantsEntitiesName.PA.Transformations, Common.ConstantsEntitiesName.PA.Transformation, Common.ConstantsEntitiesName.PA.Measurement, "~/Managers/ListManageAndView.aspx", _permissionType, "Transformation", true);
                                _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            #region Attachmets
                                _node = new RadTreeNode(Resources.CommonListManage.Attachments, _pkNode++.ToString());
                                _node.ToolTip = Resources.CommonListManage.Attachments;
                                _node.Checkable = false;
                                _node.PostBack = true;
                                _node.CssClass = "Measurement";
                                _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessTaskExecutionsAttachments);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskExecutionsAttachments);
                                _node.Attributes.Add("EntityNameContextInfo", String.Empty);
                                _node.Attributes.Add("EntityNameContextElement", String.Empty);
                                _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                                _node.Attributes.Add("PermissionType", _permissionType);
                                //Mete el Process dentro del modulo.
                                _nodeRootModulePA.Nodes.Add(_node);
                            #endregion

                            #region Update Data Series
                                //Solo agregamos esto si es Manager en el Process!
                                if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                                {
                                    _node = CreateNode(Resources.CommonListManage.UpdateDataSeries, _pkNode++.ToString(), Resources.CommonListManage.UpdateDataSeries,
                                        String.Empty, Common.ConstantsEntitiesName.PA.MeasurementDataSeries, Common.ConstantsEntitiesName.PA.MeasurementDataSeries,
                                        Common.ConstantsEntitiesName.PA.Measurement, "~/AdministrationTools/PerformanceAssessment/UpdateDataSeries.aspx", _permissionType, "Transformation", true);
                                    _nodeRootModulePA.Nodes.Add(_node);
                                }
                            #endregion

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.Nodes.Add(_nodeRootModulePA);
                            #endregion

                            #region IA
                                RadTreeNode _nodeRootModuleIA = new RadTreeNode(Resources.CommonMenu.radMnuModuleIA, _pkNode++.ToString());
                                _nodeRootModuleIA.ToolTip = Resources.CommonMenu.radMnuModuleIA;
                                _nodeRootModuleIA.CssClass = "IA";
                                _nodeRootModuleIA.PostBack = false;
                                _nodeRootModuleIA.EnableContextMenu = false;

                                _node = new RadTreeNode(Resources.CommonListManage.Exceptions, _pkNode++.ToString());
                                _node.ToolTip = Resources.CommonListManage.Exceptions;
                                _node.Checkable = false;
                                _node.CssClass = "Exception";
                                _node.PostBack = true;
                                _node.EnableContextMenu = false;
                                _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.IA.Exceptions);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.IA.Exception);
                                _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                                _node.Attributes.Add("PermissionType", _permissionType);
                                //Mete el Process dentro del modulo.
                                _nodeRootModuleIA.Nodes.Add(_node);

                                //Mete el modulo como raiz.
                                _rdtvGRCProcessFrameworkByProcessTaskMeasurement.Nodes.Add(_nodeRootModuleIA);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByProcessTaskMeasurement;
                    }
                    public RadTreeView BuildContextInfoMenuProcessTaskExecutionMeasurement(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByProcessTaskMeasurement = new RadTreeView();
                        if ((param.ContainsKey("IdProcess")) && (param.ContainsKey("IdTask")))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                            Int64 _idTask = Convert.ToInt64(param["IdTask"]);
                            Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _process = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = EMSLibrary.User.ProcessFramework.Map.Process(_idTask).LanguageOption.Title;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }


                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.Attributes.Add("PK_IdProcess", _idProcess.ToString());
                            _rdtvGRCProcessFrameworkByProcessTaskMeasurement.Attributes.Add("PK_IdTask", _idTask.ToString());
                            //_rdtvGRCProcessFrameworkByProcessTaskMeasurement.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);

                            RadTreeNode _node = null;

                            #region IA
                                RadTreeNode _nodeRootModuleIA = new RadTreeNode(Resources.CommonMenu.radMnuModuleIA, _pkNode++.ToString());
                                _nodeRootModuleIA.ToolTip = Resources.CommonMenu.radMnuModuleIA;
                                _nodeRootModuleIA.CssClass = "IA";
                                _nodeRootModuleIA.PostBack = false;
                                _nodeRootModuleIA.EnableContextMenu = false;

                                _node = new RadTreeNode(Resources.CommonListManage.Exceptions, _pkNode++.ToString());
                                _node.ToolTip = Resources.CommonListManage.Exceptions;
                                _node.Checkable = false;
                                _node.CssClass = "Exception";
                                _node.PostBack = true;
                                _node.EnableContextMenu = false;
                                _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.IA.Exceptions);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.IA.Exception);
                                _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                                _node.Attributes.Add("PermissionType", _permissionType);
                                //Mete el Process dentro del modulo.
                                _nodeRootModuleIA.Nodes.Add(_node);

                                //Mete el modulo como raiz.
                                _rdtvGRCProcessFrameworkByProcessTaskMeasurement.Nodes.Add(_nodeRootModuleIA);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByProcessTaskMeasurement;
                    }
                #endregion

                #region Process Task Operation
                    public RadTreeView BuildContextInfoMenuProcessTaskOperation(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByProcessTaskOperation = new RadTreeView();
                        if ((param.ContainsKey("IdProcess")) && (param.ContainsKey("IdTask")))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                            Int64 _idTask = Convert.ToInt64(param["IdTask"]);
                            Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _process = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = EMSLibrary.User.ProcessFramework.Map.Process(_idTask).LanguageOption.Title;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.Attributes.Add("PK_IdProcess", _idProcess.ToString());
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.Attributes.Add("PK_IdTask", _idTask.ToString());
                            //_rdtvGRCProcessFrameworkByProcessTaskOperation.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);

                            RadTreeNode _node = null;

                            #region PF
                            RadTreeNode _nodeRootModulePF = new RadTreeNode(Resources.CommonMenu.radMnuModulePF, _pkNode++.ToString());
                            _nodeRootModulePF.ToolTip = Resources.CommonMenu.radMnuModulePF;
                            _nodeRootModulePF.CssClass = "PF";
                            _nodeRootModulePF.PostBack = false;

                            //TODO: Cuando exista el metodo que devuelva las ejecuciones de un proceso... se habilita.
                            _node = new RadTreeNode(Resources.CommonListManage.Executions, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Executions;
                            _node.Checkable = false;
                            _node.CssClass = "Execution";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessTaskExecutions);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessTaskExecutionOperation);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskExecutionOperation);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePF.Nodes.Add(_node);

                            _node = new RadTreeNode(Resources.CommonListManage.ExtendedProperties, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ExtendedProperties;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.CssClass = "ExtendedProperties";
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.PF.ProcessExtendedProperties);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.PF.ProcessExtendedProperty);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.ProcessTaskOperation);
                            _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModulePF.Nodes.Add(_node);

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.Nodes.Add(_nodeRootModulePF);
                            #endregion

                            #region IA
                                RadTreeNode _nodeRootModuleIA = new RadTreeNode(Resources.CommonMenu.radMnuModuleIA, _pkNode++.ToString());
                                _nodeRootModuleIA.ToolTip = Resources.CommonMenu.radMnuModuleIA;
                                _nodeRootModuleIA.CssClass = "IA";
                                _nodeRootModuleIA.PostBack = false;
                                _nodeRootModuleIA.EnableContextMenu = false;

                                _node = new RadTreeNode(Resources.CommonListManage.Exceptions, _pkNode++.ToString());
                                _node.ToolTip = Resources.CommonListManage.Exceptions;
                                _node.Checkable = false;
                                _node.CssClass = "Exception";
                                _node.PostBack = true;
                                _node.EnableContextMenu = false;
                                _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.IA.Exceptions);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.IA.Exception);
                                _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                                _node.Attributes.Add("PermissionType", _permissionType);
                                //Mete el Process dentro del modulo.
                                _nodeRootModuleIA.Nodes.Add(_node);

                                //Mete el modulo como raiz.
                                _rdtvGRCProcessFrameworkByProcessTaskOperation.Nodes.Add(_nodeRootModuleIA);
                            #endregion

                        }
                        return _rdtvGRCProcessFrameworkByProcessTaskOperation;
                    }
                    public RadTreeView BuildContextInfoMenuProcessTaskExecutionOperation(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByProcessTaskOperation = new RadTreeView();
                        if ((param.ContainsKey("IdProcess")) && (param.ContainsKey("IdTask")))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                            Int64 _idTask = Convert.ToInt64(param["IdTask"]);
                            Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _process = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_idProcess).Parent;
                            String _pageTitle = String.Empty;
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = EMSLibrary.User.ProcessFramework.Map.Process(_idTask).LanguageOption.Title;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (_process.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.Attributes.Add("PK_IdProcess", _idProcess.ToString());
                            _rdtvGRCProcessFrameworkByProcessTaskOperation.Attributes.Add("PK_IdTask", _idTask.ToString());
                            //_rdtvGRCProcessFrameworkByProcessTaskOperation.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);

                            RadTreeNode _node = null;

                            #region IA
                                RadTreeNode _nodeRootModuleIA = new RadTreeNode(Resources.CommonMenu.radMnuModuleIA, _pkNode++.ToString());
                                _nodeRootModuleIA.ToolTip = Resources.CommonMenu.radMnuModuleIA;
                                _nodeRootModuleIA.CssClass = "IA";
                                _nodeRootModuleIA.PostBack = false;
                                _nodeRootModuleIA.EnableContextMenu = false;

                                _node = new RadTreeNode(Resources.CommonListManage.Exceptions, _pkNode++.ToString());
                                _node.ToolTip = Resources.CommonListManage.Exceptions;
                                _node.Checkable = false;
                                _node.CssClass = "Exception";
                                _node.PostBack = true;
                                _node.EnableContextMenu = false;
                                _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.IA.Exceptions);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.IA.Exception);
                                _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                                _node.Attributes.Add("EntityNameContextElement", Common.ConstantsEntitiesName.PF.ProcessGroupProcess);
                                _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                                _node.Attributes.Add("PermissionType", _permissionType);
                                //Mete el Process dentro del modulo.
                                _nodeRootModuleIA.Nodes.Add(_node);

                                //Mete el modulo como raiz.
                                _rdtvGRCProcessFrameworkByProcessTaskOperation.Nodes.Add(_nodeRootModuleIA);
                            #endregion

                        }
                        return _rdtvGRCProcessFrameworkByProcessTaskOperation;
                    }
                #endregion

            #endregion

            #region Knowledge Collaboration

                #region Resource Classification
                    public RadTreeView BuildContextInfoMenuResourceClassification(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByResourceClassification = new RadTreeView();
                        if (param.ContainsKey("IdResourceClassification"))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idResourceClassification = Convert.ToInt64(param["IdResourceClassification"]);
                            String _pageTitle = String.Empty;
                            Condesus.EMS.Business.KC.Entities.ResourceClassification _resourceClassification = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idResourceClassification);
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _resourceClassification.LanguageOption.Name;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }


                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByResourceClassification.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByResourceClassification.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByResourceClassification.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByResourceClassification.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByResourceClassification.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByResourceClassification.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByResourceClassification.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByResourceClassification.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByResourceClassification.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByResourceClassification.Attributes.Add("PK_IdResourceClassification", _idResourceClassification.ToString());

                            #region KC
                            RadTreeNode _nodeRootModuleKC = new RadTreeNode(Resources.CommonMenu.radMnuModuleKC, _pkNode++.ToString());
                            _nodeRootModuleKC.ToolTip = Resources.CommonMenu.radMnuModuleKC;
                            _nodeRootModuleKC.CssClass = "KC";
                            _nodeRootModuleKC.PostBack = false;

                            RadTreeNode _node = new RadTreeNode(Resources.CommonListManage.Classifications, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Classifications;
                            _node.Checkable = false;
                            _node.CssClass = "Classification";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.KC.ResourceClassificationChildren);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.KC.ResourceClassification);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.KC.ResourceClassification);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModuleKC.Nodes.Add(_node);

                            _node = new RadTreeNode(Resources.CommonListManage.Resources, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Resources;
                            _node.Checkable = false;
                            _node.CssClass = "Resource";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.KC.Resources);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.KC.Resource);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.KC.Resource);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModuleKC.Nodes.Add(_node);

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByResourceClassification.Nodes.Add(_nodeRootModuleKC);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByResourceClassification;
                    }
                #endregion

                #region Resource
                    public RadTreeView BuildContextInfoMenuKCResource(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByResource = new RadTreeView();
                        if (param.ContainsKey("IdResource"))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idResource = Convert.ToInt64(param["IdResource"]);
                            String _pageTitle = String.Empty;
                            Condesus.EMS.Business.KC.Entities.Resource _resource = EMSLibrary.User.KnowledgeCollaboration.Map.Resource(_idResource);
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _resource.LanguageOption.Title;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            String _permissionType = String.Empty;
                            if (EMSLibrary.User.KnowledgeCollaboration.Map.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            { _permissionType = Common.Constants.PermissionManageName; }
                            else
                            { _permissionType = Common.Constants.PermissionViewName; }

                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByResource.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByResource.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByResource.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByResource.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByResource.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByResource.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByResource.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByResource.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByResource.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByResource.Attributes.Add("PK_IdResource", _idResource.ToString());

                            #region KC
                            RadTreeNode _nodeRootModuleKC = new RadTreeNode(Resources.CommonMenu.radMnuModuleKC, _pkNode++.ToString());
                            _nodeRootModuleKC.ToolTip = Resources.CommonMenu.radMnuModuleKC;
                            _nodeRootModuleKC.CssClass = "KC";
                            _nodeRootModuleKC.PostBack = false;

                            RadTreeNode _node = new RadTreeNode(Resources.CommonListManage.Classifications, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Classifications;
                            _node.Checkable = false;
                            _node.CssClass = "Classification";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.KC.ResourceClassifications);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.KC.ResourceClassification);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.KC.ResourceClassification);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModuleKC.Nodes.Add(_node);

                            
                            //Esto es comun para ambos casos
                            _node = new RadTreeNode(Resources.CommonListManage.ResourceFiles, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.ResourceFiles;
                            _node.Checkable = false;
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameContextInfo", String.Empty);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            _node.Attributes.Add("PermissionType", _permissionType);
                            //Muestra el ResourceVersion o el ResourceCatalog...
                            if (_resource.GetType().Name == Common.ConstantsEntitiesName.KC.ResourceVersion)
                            {
                                _node.CssClass = "ResourceVersion";
                                _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.KC.ResourceFiles);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.KC.ResourceVersion);
                            }
                            else
                            {
                                _node.CssClass = "ResourceCatalog";
                                _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.KC.ResourceCatalogues);
                                _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.KC.ResourceCatalog);
                            }
                            //Mete el Resource dentro del modulo.
                            _nodeRootModuleKC.Nodes.Add(_node);

                            #region Resource Extended Property
                                _node = CreateNode(Resources.CommonListManage.ExtendedProperties, _pkNode++.ToString(), Resources.CommonListManage.ExtendedProperties, String.Empty, Common.ConstantsEntitiesName.KC.ResourceExtendedProperties, Common.ConstantsEntitiesName.KC.ResourceExtendedProperty, Common.ConstantsEntitiesName.KC.Resource, "~/Managers/ListManageAndView.aspx", _permissionType, "ExtendedProperties", true);
                                _nodeRootModuleKC.Nodes.Add(_node);
                            #endregion

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByResource.Nodes.Add(_nodeRootModuleKC);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByResource;
                    }
                #endregion

            #endregion

            #region Collaboration Tools

                #region Forum

                    public RadTreeView BuildContextInfoMenuForum(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByForum = new RadTreeView();
                        if (param.ContainsKey("IdForum") && param.ContainsKey("IdProcess"))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idForum = Convert.ToInt64(param["IdForum"]);
                            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                            String _pageTitle = String.Empty;
                            //Condesus.EMS.Business.CT.Entities.Forum _forum = EMSLibrary.User.CollaborationTools.Map.Forum(_idForum);
                            Condesus.EMS.Business.CT.Entities.Forum _forum = ((Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProcess)).Forum(_idForum);
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _forum.LanguageOption.Name;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            //String _permissionType = String.Empty;
                            //if (_forum.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            //{ _permissionType = Common.Constants.PermissionManageName; }
                            //else
                            //{ _permissionType = Common.Constants.PermissionViewName; }


                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByForum.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByForum.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByForum.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByForum.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByForum.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByForum.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByForum.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByForum.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByForum.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByForum.Attributes.Add("PK_IdForum", _idForum.ToString());
                            _rdtvGRCProcessFrameworkByForum.Attributes.Add("PK_IdProcess", _idProcess.ToString());

                            #region CT
                            RadTreeNode _nodeRootModuleCT = CreateRootNode(Resources.CommonMenu.radMnuModuleCT, _pkNode++.ToString(), Resources.CommonMenu.radMnuModuleCT, "Folder", false);

                            #region Topics
                            RadTreeNode _node = new RadTreeNode(Resources.CommonListManage.Topics, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Topics;
                            _node.CssClass = "Topic";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.CT.Topics);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.CT.Topic);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.CT.Topic);
                            _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            //_node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModuleCT.Nodes.Add(_node);
                            #endregion

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByForum.Nodes.Add(_nodeRootModuleCT);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByForum;
                    }

                    //public RadTreeView BuildContextInfoMenuCategory(Dictionary<String, Object> param)
                    //{
                    //    RadTreeView _rdtvGRCProcessFrameworkByCategory = new RadTreeView();
                    //    if ((param.ContainsKey("IdForum")) && (param.ContainsKey("IdCategory")))
                    //    {
                    //        //Arma un Tree Vacio para que no de error.
                    //        Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                    //        Int64 _idForum = Convert.ToInt64(param["IdForum"]);
                    //        Int64 _idCategory = Convert.ToInt64(param["IdCategory"]);
                    //        String _pageTitle = String.Empty;
                    //        //Condesus.EMS.Business.CT.Entities.Category _category = EMSLibrary.User.CollaborationTools.Map.Forum(_idForum).Category(_idCategory);
                    //        Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _processGroupProcess = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProcess);
                    //        //Condesus.EMS.Business.CT.Entities.Category _category = ((Condesus.EMS.Business.CT.Entities.ActiveForum)_processGroupProcess.Forum(_idForum)).Category(_idCategory);
                    //        if (!param.ContainsKey("PageTitle"))
                    //        {
                    //            _pageTitle = _category.LanguageOption.Name;
                    //        }
                    //        else
                    //        {
                    //            _pageTitle = Convert.ToString(param["PageTitle"]);
                    //        }
                    //        //String _permissionType = String.Empty;
                    //        //if (_category.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                    //        //{ _permissionType = Common.Constants.PermissionManageName; }
                    //        //else
                    //        //{ _permissionType = Common.Constants.PermissionViewName; }


                    //        //Contruye el Tree
                    //        Int32 _pkNode = 0;

                    //        //Arma el TREE
                    //        _rdtvGRCProcessFrameworkByCategory.ID = "rtvMenuContextInformation";
                    //        _rdtvGRCProcessFrameworkByCategory.CheckBoxes = false;
                    //        _rdtvGRCProcessFrameworkByCategory.EnableViewState = true;
                    //        _rdtvGRCProcessFrameworkByCategory.AllowNodeEditing = false;
                    //        _rdtvGRCProcessFrameworkByCategory.ShowLineImages = true;
                    //        _rdtvGRCProcessFrameworkByCategory.CausesValidation = false;
                    //        _rdtvGRCProcessFrameworkByCategory.Skin = "EMS";
                    //        _rdtvGRCProcessFrameworkByCategory.EnableEmbeddedSkins = false;
                    //        _rdtvGRCProcessFrameworkByCategory.Attributes.Add("PageTitle", _pageTitle);
                    //        _rdtvGRCProcessFrameworkByCategory.Attributes.Add("PK_IdProcess", _idProcess.ToString());
                    //        _rdtvGRCProcessFrameworkByCategory.Attributes.Add("PK_IdForum", _idForum.ToString());
                    //        _rdtvGRCProcessFrameworkByCategory.Attributes.Add("PK_IdCategory", _idCategory.ToString());

                    //        #region CT
                    //        RadTreeNode _nodeRootModuleCT = CreateRootNode(Resources.CommonMenu.radMnuModuleCT, _pkNode++.ToString(), Resources.CommonMenu.radMnuModuleCT, "CT", false);

                    //        #region Topics
                    //        RadTreeNode _node = new RadTreeNode(Resources.CommonListManage.Topics, _pkNode++.ToString());
                    //        _node.ToolTip = Resources.CommonListManage.Categories;
                    //        _node.CssClass = "Topic";
                    //        _node.PostBack = true;
                    //        _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.CT.Topics);
                    //        _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.CT.Topic);
                    //        _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.CT.Topic);
                    //        _node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                    //        //_node.Attributes.Add("PermissionType", _permissionType);
                    //        //Mete el Process dentro del modulo.
                    //        _nodeRootModuleCT.Nodes.Add(_node);
                    //        #endregion

                    //        //Mete el modulo como raiz.
                    //        _rdtvGRCProcessFrameworkByCategory.Nodes.Add(_nodeRootModuleCT);
                    //        #endregion
                    //    }
                    //    return _rdtvGRCProcessFrameworkByCategory;
                    //}

                    public RadTreeView BuildContextInfoMenuTopic(Dictionary<String, Object> param)
                    {
                        RadTreeView _rdtvGRCProcessFrameworkByTopic = new RadTreeView();
                        if ((param.ContainsKey("IdForum")) && (param.ContainsKey("IdCategory")) && (param.ContainsKey("IdTopic")))
                        {
                            //Arma un Tree Vacio para que no de error.
                            Int64 _idProcess = Convert.ToInt64(param["IdProcess"]);
                            Int64 _idForum = Convert.ToInt64(param["IdForum"]);
                            Int64 _idCategory = Convert.ToInt64(param["IdCategory"]);
                            Int64 _idTopic = Convert.ToInt64(param["IdTopic"]);
                            String _pageTitle = String.Empty;
                            //Condesus.EMS.Business.CT.Entities.Topic _topic = EMSLibrary.User.CollaborationTools.Map.Forum(_idForum).Category(_idCategory).Topic(_idTopic);
                            Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _processGroupProcess = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProcess);
                            Condesus.EMS.Business.CT.Entities.Topic _topic = ((Condesus.EMS.Business.CT.Entities.ActiveForum)_processGroupProcess.Forum(_idForum)).Topic(_idTopic);
                            if (!param.ContainsKey("PageTitle"))
                            {
                                _pageTitle = _topic.Title;
                            }
                            else
                            {
                                _pageTitle = Convert.ToString(param["PageTitle"]);
                            }
                            //String _permissionType = String.Empty;
                            //if (_topic.Permissions.ContainsKey(Common.Constants.PermissionManageKey))
                            //{ _permissionType = Common.Constants.PermissionManageName; }
                            //else
                            //{ _permissionType = Common.Constants.PermissionViewName; }


                            //Contruye el Tree
                            Int32 _pkNode = 0;

                            //Arma el TREE
                            _rdtvGRCProcessFrameworkByTopic.ID = "rtvMenuContextInformation";
                            _rdtvGRCProcessFrameworkByTopic.CheckBoxes = false;
                            _rdtvGRCProcessFrameworkByTopic.EnableViewState = true;
                            _rdtvGRCProcessFrameworkByTopic.AllowNodeEditing = false;
                            _rdtvGRCProcessFrameworkByTopic.ShowLineImages = true;
                            _rdtvGRCProcessFrameworkByTopic.CausesValidation = false;
                            _rdtvGRCProcessFrameworkByTopic.Skin = "EMS";
                            _rdtvGRCProcessFrameworkByTopic.EnableEmbeddedSkins = false;
                            _rdtvGRCProcessFrameworkByTopic.Attributes.Add("PageTitle", _pageTitle);
                            _rdtvGRCProcessFrameworkByTopic.Attributes.Add("PK_IdProcess", _idProcess.ToString());
                            _rdtvGRCProcessFrameworkByTopic.Attributes.Add("PK_IdForum", _idForum.ToString());
                            _rdtvGRCProcessFrameworkByTopic.Attributes.Add("PK_IdCategory", _idCategory.ToString());
                            _rdtvGRCProcessFrameworkByTopic.Attributes.Add("PK_IdTopic", _idTopic.ToString());

                            #region CT
                            RadTreeNode _nodeRootModuleCT = CreateRootNode(Resources.CommonMenu.radMnuModuleCT, _pkNode++.ToString(), Resources.CommonMenu.radMnuModuleCT, "Folder", false);

                            #region Messages
                            RadTreeNode _node = new RadTreeNode(Resources.CommonListManage.Messages, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.Messages;
                            _node.CssClass = "Message";
                            _node.PostBack = true;
                            _node.Attributes.Add("EntityNameGrid", Common.ConstantsEntitiesName.CT.Messages);
                            _node.Attributes.Add("EntityName", Common.ConstantsEntitiesName.CT.Message);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.CT.Message);
                            _node.Attributes.Add("URL", "~/Managers/MessagesList.aspx");
                            //_node.Attributes.Add("URL", "~/Managers/ListManageAndView.aspx");
                            //_node.Attributes.Add("PermissionType", _permissionType);
                            //Mete el Process dentro del modulo.
                            _nodeRootModuleCT.Nodes.Add(_node);
                            #endregion

                            //Mete el modulo como raiz.
                            _rdtvGRCProcessFrameworkByTopic.Nodes.Add(_nodeRootModuleCT);
                            #endregion
                        }
                        return _rdtvGRCProcessFrameworkByTopic;
                    }

                #endregion

            #endregion

            #region Dashboard
                public RadTreeView BuildContextInfoMenuDashboardMonitoring(Dictionary<String, Object> param)
                {
                    RadTreeView _rdtvGRCPDashboardMonitoring = new RadTreeView();
                    String _pageTitle = String.Empty;
                    if (!param.ContainsKey("PageTitle"))
                    {
                        _pageTitle = Resources.Common.Dashboard;
                    }
                    else
                    {
                        _pageTitle = Convert.ToString(param["PageTitle"]);
                    }

                    //Contruye el Tree
                    Int32 _pkNode = 0;

                    //Arma el TREE
                    _rdtvGRCPDashboardMonitoring.ID = "rtvMenuContextInformation";
                    _rdtvGRCPDashboardMonitoring.CheckBoxes = false;
                    _rdtvGRCPDashboardMonitoring.EnableViewState = true;
                    _rdtvGRCPDashboardMonitoring.AllowNodeEditing = false;
                    _rdtvGRCPDashboardMonitoring.ShowLineImages = true;
                    _rdtvGRCPDashboardMonitoring.CausesValidation = false;
                    _rdtvGRCPDashboardMonitoring.Skin = "EMS";
                    _rdtvGRCPDashboardMonitoring.EnableEmbeddedSkins = false;
                    _rdtvGRCPDashboardMonitoring.Attributes.Add("PageTitle", _pageTitle);

                    #region REPORT
                        RadTreeNode _nodeRootModuleRPT = CreateRootNode(Resources.CommonMenu.radMnuModuleRPT, _pkNode++.ToString(), Resources.CommonMenu.radMnuModuleRPT, "Folder", false);
                        _nodeRootModuleRPT.EnableContextMenu = false;

                        #region Inter-Observatory
                            RadTreeNode _nodeRootModuleInterObservatory = CreateRootNode(Resources.Common.InterObservatory, _pkNode++.ToString(), Resources.Common.InterObservatory, "Folder", false);
                            _nodeRootModuleInterObservatory.EnableContextMenu = false;

                            RadTreeNode _nodeRootModuleGEIInter = CreateRootNode(Resources.Common.GEI, _pkNode++.ToString(), Resources.Common.GEI, "Folder", false);
                            _nodeRootModuleInterObservatory.EnableContextMenu = false;

                            _nodeRootModuleInterObservatory.Nodes.Add(_nodeRootModuleGEIInter);

                            RadTreeNode _node = new RadTreeNode(Resources.CommonListManage.MultiObservatory, _pkNode++.ToString());
                            _node.ToolTip = Resources.CommonListManage.MultiObservatory;
                            _node.CssClass = "SummaryForOwner";
                            _node.PostBack = true;
                            _node.EnableContextMenu = false;
                            _node.Attributes.Add("EntityNameGrid", String.Empty);
                            _node.Attributes.Add("EntityName", String.Empty);
                            _node.Attributes.Add("EntityNameContextInfo", Common.ConstantsEntitiesName.PF.Process);
                            _node.Attributes.Add("URL", "~/Dashboard/ReportMultiObservatory.aspx");
                            //Mete el Process dentro del modulo.
                            _nodeRootModuleGEIInter.Nodes.Add(_node);

                            //_nodeRootModuleRPT.Nodes.Add(_nodeRootModuleGEIInter);

                            //Ya tenemos todos los reportes, ahora insertamos el nodo GEI
                            _nodeRootModuleRPT.Nodes.Add(_nodeRootModuleInterObservatory);
                        #endregion

                        //Mete el modulo como raiz.
                        _rdtvGRCPDashboardMonitoring.Nodes.Add(_nodeRootModuleRPT);
                    #endregion


                    return _rdtvGRCPDashboardMonitoring;
                }
            #endregion

        #endregion
    }
}
