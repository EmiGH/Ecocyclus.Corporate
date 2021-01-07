using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class PostsProperties : BaseProperties
    {
        #region Internal Properties
        CompareValidator _CvJobTitle;
        CompareValidator _CvOrganizationalChart;
        private RadComboBox _RdcOrganizationalChart;
        private RadComboBox _RdcJobTitle;
        private RadTreeView _RtvJobTitle;
        private Int64 _IdFunctionalArea
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdFunctionalArea") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdFunctionalArea")) : 0;
            }
        }
        private Int64 _IdGeographicArea
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdGeographicArea") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdGeographicArea")) : 0;
            }
        }
        private Int64 _IdPosition
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdPosition") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdPosition")) : 0;
            }
        }
        private Int64 _IdOrganization
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
            }
        }
        private Int64 _IdPerson
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdPerson") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdPerson")) : Convert.ToInt64(GetPKfromNavigator("IdPerson"));
            }
        }
        private Post _Entity = null;
        private Post Entity
        {
            get
            {
                try
                {
                    if (_Entity == null)
                    {
                        Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                        GeographicArea _geoArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_IdGeographicArea);
                        Position _position = _organization.Position(_IdPosition);
                        FunctionalArea _funArea = _organization.FunctionalArea(_IdFunctionalArea);
                        FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _funArea);
                        GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_funArea, _geoArea);
                        JobTitle _jobTitle = _organization.JobTitle(_geoFunArea, _funPos);
                        Person _person = _organization.Person(_IdPerson);
                        _Entity = _organization.Post(_jobTitle, _person);
                        //_Entity = ((PersonwithUser)EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson)).Post(_jobTitle);

                    }
                    return _Entity;
                }
                catch { return null; }
            }

            set { _Entity = value; }
        }

        private RadComboBox _RdcPerson;
        CompareValidator _CvPerson;
        #endregion

        #region PageLoad & Init
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

                AddCombos();

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    { LoadData(); } //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);

                    Condesus.EMS.Business.DS.Entities.Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                    lblOrganizationValue.Text = _organization.CorporateName;
                    if (_IdPerson > 0)
                    {
                        _RdcPerson.Enabled = false;

                    }
                    else
                    {
                        _RdcPerson.Enabled = true;                    
                    }
                }

                if (Entity != null)
                {   //Porque si es edit y graba, debe inhabilitar esto!
                    _CvJobTitle.EnableClientScript = false;
                    _CvOrganizationalChart.EnableClientScript = false;
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.Person.FirstName + ' ' + Entity.Person.LastName : Resources.CommonListManage.Post;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.Post;
                lblEndDate.Text = Resources.CommonListManage.EndDate;
                lblJobTitle.Text = Resources.CommonListManage.JobTitle;
                lblOrganization.Text = Resources.CommonListManage.Organization;
                lblOrganizationalChart.Text = Resources.CommonListManage.OrganizationalChart;
                lblPerson.Text = Resources.CommonListManage.Person;
                lblStartDate.Text = Resources.CommonListManage.StartDate;
                rfvStartDate.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                lblStartDateValue.Text = String.Empty;
                lblEndDateValue.Text = String.Empty;
                rdtStartDate.SelectedDate = null;
                rdtEndDate.SelectedDate = null;
            }
            private void LoadData()
            {
                //Solo se puede modificar la fecha inicio y fin.
                _RdcJobTitle.Enabled = false;
                _RdcOrganizationalChart.Enabled = false;
                if (Entity.StartDate != DateTime.MinValue)
                {
                    rdtStartDate.SelectedDate = Entity.StartDate;
                }
                if (Entity.EndDate != DateTime.MinValue)
                {
                    rdtEndDate.SelectedDate = Entity.EndDate;
                }
                SetJobTitle();
            }
            private void AddComboPeople()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);
                String _selectedValue = String.Empty;
                if (_IdPerson > 0)
                {
                    Person _person = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson);
                    _selectedValue = "IdPerson=" + _IdPerson.ToString() + "& " + "IdOrganization=" + _IdOrganization.ToString() + "& " + "IdSalutationType=" + _person.SalutationType.IdSalutationType.ToString();
                }
                AddCombo(phPerson, ref _RdcPerson, Common.ConstantsEntitiesName.DS.People, _selectedValue, _params, false, true, false, false, false);
                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.People, phPersonValidator, ref _CvPerson, _RdcPerson, Resources.ConstantMessage.SelectAPerson);

                if (_IdPerson > 0)
                {
                    _RdcPerson.SelectedValue = _selectedValue;
                }
            }
            private void AddComboJobTitles()
            {
                String _filterExpression = String.Empty;
                //Combo de GeographicArea Parent
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);

                if (GetKeyValue(_RdcOrganizationalChart.SelectedValue, "IdOrganizationalChart") != null)
                {
                    _params.Add("IdOrganizationalChart", Convert.ToInt64(GetKeyValue(_RdcOrganizationalChart.SelectedValue, "IdOrganizationalChart")));
                }
                AddComboWithTree(phJobTitle, ref _RdcJobTitle, ref _RtvJobTitle,
                    Common.ConstantsEntitiesName.DS.JobTitles, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvHierarchicalTreeViewInCombo_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.JobTitles, phJobTitleValidator, ref _CvJobTitle, _RdcJobTitle, Resources.ConstantMessage.SelectAJobTitle);
            }
            private void AddCombos()
            {
                AddComboPeople();
                AddComboOrganizationalChart();
                AddComboJobTitles();
            }
            private void AddComboOrganizationalChart()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                _params.Add("IdOrganization", _IdOrganization);

                String _selectedValue = String.Empty;   // = "IdOrganization=" + _IdOrganization.ToString();
                AddCombo(phOrganizationalChart, ref _RdcOrganizationalChart, Common.ConstantsEntitiesName.DS.OrganizationalCharts, _selectedValue, _params, false, true, false, true, false);
                _RdcOrganizationalChart.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_RdcOrganizationalChart_SelectedIndexChanged);

                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationalCharts, phOrganizationalChartValidator, ref _CvOrganizationalChart, _RdcOrganizationalChart, Resources.ConstantMessage.SelectAnOrganizationalChart);
            }
            private void SetJobTitle()
            {
                _RdcJobTitle.Style.Add("display", "none");
                _RdcOrganizationalChart.Style.Add("display", "none");
                _CvJobTitle.EnableClientScript = false;
                _CvOrganizationalChart.EnableClientScript = false;

                Label _lblJobTitleName = new Label();
                _lblJobTitleName.Text = Entity.JobTitle.Name();
                phJobTitle.Controls.Add(_lblJobTitleName);

                Label _lblOrganizationalChart = new Label();
                _lblOrganizationalChart.Text = Resources.Common.Unavailable;
                phOrganizationalChart.Controls.Add(_lblOrganizationalChart);
            }
        #endregion

        #region Page Events
        protected void rtvHierarchicalTreeViewInCombo_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            NodeExpand(sender, e, Common.ConstantsEntitiesName.DS.JobTitleChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
        }
        void _RdcOrganizationalChart_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (e.OldValue != e.Value)
            {
                AddComboJobTitles();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (Entity == null)
                {
                    //Alta
                    Int64 _idPerson = Convert.ToInt64(GetKeyValue(_RdcPerson.SelectedValue, "IdPerson"));
                    //Obtiene el key necesario.
                    Object _obj = GetKeyValue(_RtvJobTitle.SelectedNode.Value, "IdFunctionalArea");   //Si lo saco del tree, funciona!!!.
                    //Si el key obtenido no llega a exister devuelve null.
                    Int64 _idFunctionalArea = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    _obj = GetKeyValue(_RtvJobTitle.SelectedNode.Value, "IdGeographicArea");   //Si lo saco del tree, funciona!!!.
                    Int64 _idGeographicArea = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.
                    _obj = GetKeyValue(_RtvJobTitle.SelectedNode.Value, "IdPosition");   //Si lo saco del tree, funciona!!!.
                    Int64 _idPosition = Convert.ToInt64((_obj == null ? 0 : _obj));    //Si queda en cero 0, quiere decir que esta dando trabajando con un ROOT.

                    Organization _organization = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization);
                    GeographicArea _geographicArea = EMSLibrary.User.GeographicInformationSystem.GeographicArea(_idGeographicArea);
                    Position _position = _organization.Position(_idPosition);
                    FunctionalArea _functionalArea = _organization.FunctionalArea(_idFunctionalArea);

                    GeographicFunctionalArea _geoFunArea = _organization.GeographicFunctionalArea(_functionalArea, _geographicArea);
                    FunctionalPosition _funPos = _organization.FunctionalPosition(_position, _functionalArea);
                    JobTitle _jobtitle = _organization.JobTitle(_geoFunArea, _funPos);

                    PersonwithUser _person = ((Condesus.EMS.Business.DS.Entities.PersonwithUser)EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_idPerson));


                    Entity = _person.PostsAdd(_geographicArea, _position, _functionalArea, GetStartDate(), GetEndDate());
                }
                else
                {
                    //Modificacion
                    Entity.Modify(GetStartDate(), GetEndDate());
                    ////EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).Person(_IdPerson).PostsModify(_IdGeographicArea, _IdPosition, _IdFunctionalArea, GetStartDate(), GetEndDate());
                    //base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);
                    //base.NavigatorAddTransferVar("IdPerson", _IdPerson);
                    //base.NavigatorAddTransferVar("IdFunctionalArea", _IdFunctionalArea);
                    //base.NavigatorAddTransferVar("IdGeographicArea", _IdGeographicArea);
                    //base.NavigatorAddTransferVar("IdPosition", _IdPosition);
                    //base.Navigate(HttpContext.Current.Request.Url.AbsolutePath, Entity.Person.FirstName + ' ' + Entity.Person.LastName + " " + Resources.Common.Edit, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                }
                
                base.NavigatorAddTransferVar("IdOrganization", _IdOrganization);
                base.NavigatorAddTransferVar("IdPerson", Entity.Person.IdPerson);
                base.NavigatorAddTransferVar("IdFunctionalArea", Entity.JobTitle.FunctionalArea.IdFunctionalArea);
                base.NavigatorAddTransferVar("IdGeographicArea", Entity.JobTitle.GeographicArea.IdGeographicArea);
                base.NavigatorAddTransferVar("IdPosition", Entity.JobTitle.Position.IdPosition);

                String _pkValues = "IdOrganization=" + _IdOrganization.ToString()
                    + "& IdPerson=" + Entity.Person.IdPerson.ToString()
                    + "& IdFunctionalArea=" + Entity.JobTitle.FunctionalArea.IdFunctionalArea.ToString()
                    + "& IdGeographicArea=" + Entity.JobTitle.GeographicArea.IdGeographicArea.ToString()
                    + "& IdPosition=" + Entity.JobTitle.Position.IdPosition.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.Post);
                base.NavigatorAddTransferVar("EntityNameContextInfo", base.NavigatorGetTransferVar<String>("EntityNameContextInfo"));

                String _entityPropertyName = String.Concat(Entity.Person.FullName, " - ", Entity.JobTitle.Name());
                NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.Post, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

                base.StatusBar.ShowMessage(Resources.Common.SaveOK);
            }
            catch (Exception ex)
            {
                base.StatusBar.ShowMessage(ex);
            }
        }
        private DateTime GetStartDate()
        {
            return (DateTime)rdtStartDate.SelectedDate;
        }
        private DateTime GetEndDate()
        {
            return (rdtEndDate.SelectedDate == null) ? DateTime.MinValue : (DateTime)rdtEndDate.SelectedDate;
        }

        #endregion
    }
}