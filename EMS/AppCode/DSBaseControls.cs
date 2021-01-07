using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Xml.Linq;
using Telerik.Web.UI;

using Condesus.EMS.WebUI.Controls;

namespace Condesus.EMS.WebUI
{
    public class DSBaseControls : BaseControl
    {
        public event DSMenuItemClicked OnMenuClick;
        public void MenuClick(String id, Boolean isUpdate, MenuType menuType)
        {
            if (OnMenuClick != null)
                OnMenuClick(this, new DSMenuEventArgs(id, isUpdate, menuType));
        }
        public String ParentEntity
        {
            get { return Convert.ToString(ViewState["ParentEntity"]); }
            set { ViewState["ParentEntity"] = value; }
        }
        public Int64 ParentEntityId
        {
            get { return Convert.ToInt64(ViewState["ParentEntityId"]); }
            set { ViewState["ParentEntityId"] = value; }
        }
        public Boolean IsUpdate
        {
            get { return Convert.ToBoolean(ViewState["IsUpdate"]); }
            set { ViewState["IsUpdate"] = value.ToString(); }
        }
        public Int64 _IdOrganization
        {
            get { return Convert.ToInt64(ViewState["_IdOrganization"]); }
            set { ViewState["_IdOrganization"] = value.ToString(); }
        }

        #region Grid

        protected void InitializeGrid(ref RadGrid rgGrid, String[] idKeys)
        {
            rgGrid.MasterTableView.DataKeyNames = idKeys;
            rgGrid.ClientSettings.AllowExpandCollapse = false;
            rgGrid.ClientSettings.Selecting.AllowRowSelect = true;
            rgGrid.AllowMultiRowSelection = false;
            rgGrid.ClientSettings.Selecting.EnableDragToSelectRows = false;
            rgGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
        }
        protected virtual void ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e, String entity)
        {
            if (e.Item is GridDataItem)
            {
                HtmlImage oimg = (HtmlImage)e.Item.FindControl("selButton");
                if (!(oimg == null))
                {
                    oimg.Attributes["onclick"] = string.Format("return ShowMenu"+entity+"(event, " + e.Item.RowIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
                }
            }
        }

        #endregion

        protected void LoadContactTypes(Int64 type, ref RadComboBox rcbCombo)
        {
            rcbCombo.Items.Clear();

            Dictionary<Int64, Condesus.EMS.Business.DS.Entities.ContactType> _contactTypes = EMSLibrary.User.DirectoryServices.Configuration.ApplicabilityContactType(type).ContactTypes;

            RadComboBoxItem _li = new RadComboBoxItem(Resources.Common.ComboBoxAll, "0");
            rcbCombo.Items.Add(_li);

            foreach (Condesus.EMS.Business.DS.Entities.ContactType _contactType in _contactTypes.Values)
            {
                _li = new RadComboBoxItem(_contactType.LanguageOption.Name, _contactType.IdContactType.ToString());
                rcbCombo.Items.Add(_li);
            }
        }
        protected void LoadResourceTypes(ref RadComboBox rcbCombo)
        {
            rcbCombo.Items.Clear();

            Dictionary<Int64, Condesus.EMS.Business.KC.Entities.ResourceType> _resourceTypes = EMSLibrary.User.KnowledgeCollaboration.Configuration.ResourceTypes();

            RadComboBoxItem _li = new RadComboBoxItem(Resources.Common.ComboBoxAll, "0");
            rcbCombo.Items.Add(_li);

            foreach (Condesus.EMS.Business.KC.Entities.ResourceType _resourceType in _resourceTypes.Values)
            {
                _li = new RadComboBoxItem(_resourceType.LanguageOption.Name, _resourceType.IdResourceType.ToString());
                rcbCombo.Items.Add(_li);
            }
        }
        protected void LoadRelationshipTypes(ref RadComboBox rcbCombo)
        {
            rcbCombo.Items.Clear();

            Dictionary<long, Condesus.EMS.Business.DS.Entities.OrganizationRelationshipType> _relationshipTypes = EMSLibrary.User.DirectoryServices.Configuration.OrganizationsRelationshipsTypes();

            RadComboBoxItem _li = new RadComboBoxItem(Resources.Common.ComboBoxAll, "0");
            rcbCombo.Items.Add(_li);

            foreach (Condesus.EMS.Business.DS.Entities.OrganizationRelationshipType _relationshipType in _relationshipTypes.Values)
            {
                _li = new RadComboBoxItem(_relationshipType.LanguageOption.Name, _relationshipType.IdOrganizationRelationshipType.ToString());
                rcbCombo.Items.Add(_li);
            }
        }
        protected void LoadJobTitles(ref RadComboBox rcbCombo, String idJobTitle)
        {
            rcbCombo.Items.Clear();

            List<Condesus.EMS.Business.DS.Entities.JobTitle> _jobTitles = EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).JobTitles();

            foreach (Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle in _jobTitles)
            {
                String _id = _jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea.ToString() + ',' + _jobTitle.FunctionalPositions.Position.IdPosition.ToString() + ',' + _jobTitle.GeographicFunctionalAreas.FunctionalArea.IdFunctionalArea.ToString();
                if (_id != idJobTitle)
                {
                    RadComboBoxItem _li = new RadComboBoxItem(_jobTitle.Name().ToString(), _id);
                    rcbCombo.Items.Add(_li);
                }
            }
        }
        //protected void LoadSubordinateClassification(ref RadComboBox rcbCombo, Int64 idOrganization)
        //{
        //    rcbCombo.Items.Clear();
        //    Dictionary<Int64, Condesus.EMS.Business.DS.Entities.SubordinateClassification> _subClassification = EMSLibrary.User.DirectoryServices.Map.Organization(idOrganization).SubordinateClassifications();

        //    RadComboBoxItem _li = new RadComboBoxItem(Resources.ArchivoTemp.BasePage_LoadSubordinateClassification_SelectChart, "-1");
        //    rcbCombo.Items.Add(_li);

        //    _li = new RadComboBoxItem(Resources.ArchivoTemp.RolesProperties_NoClassification, "0");
        //    rcbCombo.Items.Add(_li);

        //    foreach (Condesus.EMS.Business.DS.Entities.SubordinateClassification _subClass in _subClassification.Values)
        //    {
        //        _li = new RadComboBoxItem(_subClass.LanguageOption.Name, _subClass.IdClassification.ToString());
        //        rcbCombo.Items.Add(_li);
        //    }
        //    rcbCombo.SelectedValue = "-1";
        //}
        //protected void LoadSubordinates(ref RadComboBox rcbCombo, Int64 subordinateClassificationItem, Int64 idOrganization)
        //{
        //    rcbCombo.Items.Clear();
        //    //Carga el indice -1 para el mensaje de seleccion.
        //    RadComboBoxItem _li = new RadComboBoxItem(Resources.ArchivoTemp.JobTitlesProperties_NoParent, "0");
        //    rcbCombo.Items.Add(_li);

        //    if (subordinateClassificationItem > 0)
        //    {
        //        List<Condesus.EMS.Business.DS.Entities.JobTitle> _jobTitles = EMSLibrary.User.DirectoryServices.Map.Organization(idOrganization).JobTitles(subordinateClassificationItem);

        //        foreach (Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle in _jobTitles)
        //        {
        //            String _idJT = _jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea.ToString() + "," +
        //            _jobTitle.FunctionalPositions.Position.IdPosition.ToString() + "," +
        //            _jobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea.ToString();
                        

        //            _li = new RadComboBoxItem(_jobTitle.Name(), _idJT);
        //            rcbCombo.Items.Add(_li);

        //            LoadSubordinateChildren(rcbCombo, _idJT, subordinateClassificationItem, Convert.ToChar(160).ToString() + Convert.ToChar(160).ToString(), idOrganization);
        //        }
        //        rcbCombo.SelectedValue = "0";
        //    }
        //}
        //private void LoadSubordinateChildren(RadComboBox rcbComboChild, String idJobTitle, Int64 subordinateClassificationItem, String tab,Int64 idOrganization)
        //{
        //    Int64 _idPosition = Convert.ToInt64(idJobTitle.Split(',')[0]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(idJobTitle.Split(',')[1]);
        //    Int64 _idGeographicArea = Convert.ToInt64(idJobTitle.Split(',')[2]);
        //    Condesus.EMS.Business.DS.Entities.SubordinateClassification _subordinateClassification = EMSLibrary.User.DirectoryServices.Map.Organization(idOrganization).SubordinateClassification(subordinateClassificationItem);
        //    if (_subordinateClassification != null)
        //    {
        //        Dictionary<String, Condesus.EMS.Business.DS.Entities.Subordinate> _subordinates = _subordinateClassification.Subordinates(_idFunctionalArea, _idGeographicArea, _idPosition);
        //        foreach (Condesus.EMS.Business.DS.Entities.Subordinate _subordinateChild in _subordinates.Values)
        //        {
        //            String _idSubordinate = _subordinateChild.Position.IdPosition.ToString() + "," +
        //                _subordinateChild.FunctionalArea.IdFunctionalArea.ToString() + "," +
        //                _subordinateChild.GeographicArea.IdGeographicArea.ToString();

        //            RadComboBoxItem _listItem = new RadComboBoxItem(tab + _subordinateChild.Name().ToString(), _idSubordinate);
        //            rcbComboChild.Items.Add(_listItem);
        //            LoadSubordinateChildren(rcbComboChild, _idSubordinate, subordinateClassificationItem, tab + tab, idOrganization);
        //        }
        //    }
        //}

        //protected void LoadTreeView(ref RadTreeView rtvTreeView, String id, String classType, Boolean manageType)
        //{
        //    switch (classType)
        //    {
        //        case "JobTitle":
        //            LoadJobTitlesTreeView(ref rtvTreeView, Convert.ToInt64(id), manageType);
        //            break;

        //    }
        //}
        //protected void radtv_NodeExpand(object sender, RadTreeNodeEventArgs e, String filter, String classType, Boolean manageType)
        //{
        //    e.Node.Nodes.Clear();

        //    switch (classType)
        //    {
        //        case "JobTitle":
        //            radtvJobTitles_NodeExpand(e, Convert.ToInt64(filter), manageType);
        //            break;
        //    }
        //}

        //private void LoadJobTitlesTreeView(ref RadTreeView rtvTreeView, Int64 idSubordinateClassification, Boolean manageType)
        //{
        //    List<Condesus.EMS.Business.DS.Entities.JobTitle> _jobTitles = null;

        //    if (idSubordinateClassification != -1)
        //    {
        //        //Identifica si carga los JobTitles puros o con clasificacion

        //        _jobTitles = EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).JobTitles(idSubordinateClassification);

        //        foreach (Condesus.EMS.Business.DS.Entities.JobTitle _jobTitle in _jobTitles)
        //        {
        //            RadTreeNode _node = LoadJobTitlesNodes(_jobTitle, manageType, idSubordinateClassification);

        //            rtvTreeView.Nodes.Add(_node);
        //        }
        //    }
        //}
        //private RadTreeNode LoadJobTitlesNodes(Condesus.EMS.Business.DS.Entities.JobTitle jobTitle, Boolean manageType, Int64 idSubordinateClassification)
        //{
        //    Int64 _idGeographicArea = jobTitle.GeographicFunctionalAreas.GeographicArea.IdGeographicArea;
        //    Int64 _idPosition = jobTitle.FunctionalPositions.Position.IdPosition;
        //    Int64 _idFunctionalArea = jobTitle.FunctionalPositions.FunctionalArea.IdFunctionalArea;

        //    RadTreeNode _node = new RadTreeNode();
        //    _node.Text = jobTitle.Name();
        //    _node.Value = _idGeographicArea.ToString() + ',' + _idPosition.ToString() + ',' + _idFunctionalArea.ToString();
        //    SetManageNode(manageType, _node);
        //    //Si tiene hijos agrega el expand...
        //    if (jobTitle.SubordinateByParent(idSubordinateClassification) != null)
        //    {
        //        _node.ExpandMode = TreeNodeExpandMode.ServerSide;
        //    }
        //    return _node;
        //}
        //private RadTreeNode LoadSubordinateNodes(Condesus.EMS.Business.DS.Entities.Subordinate subordinate, Boolean manageType)
        //{
        //    Int64 _idGeographicArea = subordinate.GeographicArea.IdGeographicArea;
        //    Int64 _idPosition = subordinate.Position.IdPosition;
        //    Int64 _idFunctionalArea = subordinate.FunctionalArea.IdFunctionalArea;

        //    RadTreeNode _node = new RadTreeNode();
        //    _node.Text = subordinate.Name();
        //    _node.Value = _idGeographicArea.ToString() + ',' + _idPosition.ToString() + ',' + _idFunctionalArea.ToString();
        //    SetManageNode(manageType, _node);
        //    //Si tiene hijos agrega el expand...
        //    if (subordinate.Children.Count > 0)
        //    {
        //        _node.ExpandMode = TreeNodeExpandMode.ServerSide;
        //    }
        //    return _node;
        //}
        //private void radtvJobTitles_NodeExpand(RadTreeNodeEventArgs e, Int64 idSubordinateClassification, Boolean manageType)
        //{
        //    String[] _id = ((String)e.Node.Value.ToString()).Split(',');
        //    Int64 _idGeographicArea = Convert.ToInt64(_id[0]);
        //    Int64 _idPosition = Convert.ToInt64(_id[1]);
        //    Int64 _idFunctionalArea = Convert.ToInt64(_id[2]);

        //    Condesus.EMS.Business.DS.Entities.SubordinateClassification _subClass = EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).SubordinateClassification(idSubordinateClassification);
        //    if (_subClass != null)
        //    {
        //        Dictionary<String, Condesus.EMS.Business.DS.Entities.Subordinate> _subs = _subClass.Subordinates(_idFunctionalArea, _idGeographicArea, _idPosition);
        //        foreach (Condesus.EMS.Business.DS.Entities.Subordinate _subordinateChild in _subs.Values)
        //        {
        //            e.Node.Nodes.Add(LoadSubordinateNodes(_subordinateChild, manageType));
        //        }
        //        e.Node.Expanded = true;
        //    }
        //}

        private void SetManageNode(Boolean manageType, RadTreeNode node)
        {
            if (manageType)
            {
                node.Text = Common.Functions.ReplaceIndexesTags(node.Text);
                node.Checkable = true;
                //node.ContextMenuName = "rmnSelection";
                node.ContextMenuID = "rmnSelection";
            }
        }

        //private Condesus.EMS.Business.DS.Entities.ContactAddress - OBJECT o un Padre GetEntity()
        //{
        //    switch (ParentEntity)
        //    {
        //        case "PeopleProperties":
        //            return EMSLibrary.User.DirectoryServices.Configuration.Person(IdOrganization, ParentEntityId).ContactAddress(IdAddress);
        //        case "OrganizationProperties":
        //            return EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).ContactAddress(IdAddress);
        //        default:
        //            throw new Exception("Parent Entity not defined. This page cant exist without a Parent");
        //    }
        //}
        //private Condesus.EMS.Business.DS.Entities.ContactAddress AddEntity()
        //{
        //    switch (ParentEntity)
        //    {
        //        case "PeopleProperties":
        //            return EMSLibrary.User.DirectoryServices.Configuration.Person(IdOrganization, ParentEntityId).ContactAddressesAdd(txtStreet.Text, txtNumber.Text, txtFloor.Text, txtApartment.Text, txtZipCode.Text, txtCity.Text, txtState.Text, Convert.ToInt64(ddlCountry.SelectedValue), Convert.ToInt64(ddlContactType.SelectedValue));
        //        case "OrganizationProperties":
        //            return EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).ContactAddressesAdd(txtStreet.Text, txtNumber.Text, txtFloor.Text, txtApartment.Text, txtZipCode.Text, txtCity.Text, txtState.Text, Convert.ToInt64(ddlCountry.SelectedValue), Convert.ToInt64(ddlContactType.SelectedValue));
        //        default:
        //            throw new Exception("Parent Entity not defined. This page cant exist without a Parent");
        //    }
        //}

        //private void ModifyEntity()
        //{
        //    switch (ParentEntity)
        //    {
        //        case "PeopleProperties":
        //            EMSLibrary.User.DirectoryServices.Configuration.Person(IdOrganization, ParentEntityId).ContactAddressesModify(Convert.ToInt64(lblIdValue.Text), txtStreet.Text, txtNumber.Text, txtFloor.Text, txtApartment.Text, txtZipCode.Text, txtCity.Text, txtState.Text, Convert.ToInt64(ddlCountry.SelectedValue), Convert.ToInt64(ddlContactType.SelectedValue));
        //            break;
        //        case "OrganizationProperties":
        //            EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).ContactAddressesModify(Convert.ToInt64(lblIdValue.Text), txtStreet.Text, txtNumber.Text, txtFloor.Text, txtApartment.Text, txtZipCode.Text, txtCity.Text, txtState.Text, Convert.ToInt64(ddlCountry.SelectedValue), Convert.ToInt64(ddlContactType.SelectedValue));
        //            break;
        //        default:
        //            throw new Exception("Parent Entity not defined. This page cant exist without a Parent");
        //    }
        //}

        //private void RemoveEntity()
        //{
        //    switch (ParentEntity)
        //    {
        //        case "PeopleProperties":
        //            EMSLibrary.User.DirectoryServices.Configuration.Person(IdOrganization, ParentEntityId).ContactAddressesRemove(Convert.ToInt32(lblIdValue.Text));
        //            break;
        //        case "OrganizationProperties":
        //            EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).ContactAddressesRemove(Convert.ToInt32(lblIdValue.Text));
        //            break;
        //        default:
        //            throw new Exception("Parent Entity not defined. This page cant exist without a Parent");
        //    }
        //}
    }
}
