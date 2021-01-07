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
using System.Linq;

using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.DS.Entities;

namespace Condesus.EMS.WebUI.DS
{
    public partial class OrganizationalChartsProperties : BaseProperties
    {
        #region Internal Properties
            //CompareValidator _CvOrganization;
            //RadComboBox _RdcOrganization;
            //private RadTreeView _RtvOrganization;
            private Int64 _IdOrganizationalChart
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganizationalChart") ? base.NavigatorGetTransferVar<Int64>("IdOrganizationalChart") : 0;
                }
            }
            private Int64 _IdOrganization
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdOrganization") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdOrganization")) : Convert.ToInt64(GetPKfromNavigator("IdOrganization"));
                }
            }
            private OrganizationalChart _Entity = null;
            private OrganizationalChart Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).OrganizationalChart(_IdOrganizationalChart);

                        return _Entity;
                    }
                    catch
                    {
                        return null;
                    }
                }

                set { _Entity = value; }
            }
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
                lblOrganizationValue.Text = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).CorporateName;

                if (!Page.IsPostBack)
                {
                    //InitFkVars();

                    //Inicializo el Form
                    if (Entity == null)
                        Add();
                    else
                        LoadData(); //Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentForm);                    
                    this.txtName.Focus();
                }
            }
            protected override void SetPagetitle()
            {
                try
                {
                    base.PageTitle = (Entity != null) ? Entity.LanguagesOptions.Item(Global.DefaultLanguage).Name : Resources.CommonListManage.OrganizationalChart;
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.OrganizationalChart;
                lblDescription.Text = Resources.CommonListManage.Description;
                lblLanguage.Text = Resources.CommonListManage.Language;
                lblName.Text = Resources.CommonListManage.Name;
                lblOrganization.Text = Resources.CommonListManage.Organization;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();

                //Inicializa el Formulario
                txtName.Text = String.Empty;
                txtDescription.Text = String.Empty;
                lblLanguageValue.Text = Global.DefaultLanguage.Name;
                //_RdcOrganization.SelectedValue = Common.Constants.ComboBoxSelectItemValue;
            }
            private void LoadData()
            {
                //carga los datos en pantalla
                txtName.Text = Entity.LanguageOption.Name;
                txtDescription.Text = Entity.LanguageOption.Description;
                lblLanguageValue.Text = Entity.LanguageOption.Language.Name;

                ////Seteamos la organizacion...
                ////Realiza el seteo del parent en el Combo-Tree.
                //Organization _oganization = EMSLibrary.User.DirectoryServices.Map.Organization(Entity.IdOrganization);
                //String _keyValuesElement = "IdOrganization=" + _oganization.IdOrganization.ToString();
                //if (_oganization.Classifications.Count > 0)
                //{
                //    String _keyValuesClassification = "IdOrganizationClassification=" + _oganization.Classifications.First().Value.IdOrganizationClassification;
                //    SelectItemTreeViewParentElementMaps(_keyValuesClassification, _keyValuesElement, ref _RtvOrganization, ref _RdcOrganization, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.Organizations);
                //}
                //else
                //{
                //    SelectItemTreeViewParent(_keyValuesElement, ref _RtvOrganization, ref _RdcOrganization, Common.ConstantsEntitiesName.DS.Organization, Common.ConstantsEntitiesName.DS.Organizations);
                //}
            }
        #endregion

        #region Page Events
        protected void rtvOrganizations_NodeExpand(object sender, RadTreeNodeEventArgs e)
        {
            //Limpio los hijos, para no duplicar al abrir y cerrar.
            e.Node.Nodes.Clear();
            Dictionary<String, Object> _params = new Dictionary<String, Object>();
            _params = GetKeyValues(e.Node.Value);

            //Primero lo hace sobre las Clasificaciones Hijas...
            //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, _params);
            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren))
            {
                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren].Rows)
                {
                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                    e.Node.Nodes.Add(_node);
                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                    SetExpandMode(_node, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, true, false);
                }
            }

            //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Organizations, _params);
            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.Organizations))
            {
                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.Organizations].Rows)
                {
                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Organizations, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                    e.Node.Nodes.Add(_node);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_RdcOrganization.SelectedValue, "IdOrganization"));
                //Int64 _idOrganization = Convert.ToInt64(GetKeyValue(_RtvOrganization.SelectedNode.Value, "IdOrganization"));

                if (Entity == null)
                {
                    //Alta
                    Entity = EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).OrganizationalChartAdd(txtName.Text, txtDescription.Text);
                }
                else
                {
                    //Modificacion
                    EMSLibrary.User.DirectoryServices.Map.Organization(_IdOrganization).OrganizationalChart(_IdOrganizationalChart).Modify(txtName.Text, txtDescription.Text);
                }
                base.NavigatorAddTransferVar("IdOrganizationalChart", Entity.IdOrganizationalChart);
                base.NavigatorAddTransferVar("IdOrganization", Entity.IdOrganization);
                String _pkValues = "IdOrganizationalChart=" + Entity.IdOrganizationalChart.ToString()
                    + "& IdOrganization=" + Entity.IdOrganization.ToString();
                base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);

                base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DS.OrganizationalChart);
                base.NavigatorAddTransferVar("EntityNameContextInfo", Common.ConstantsEntitiesName.DS.Organization);
                //Navigate("~/MainInfo/ListViewer.aspx", Resources.CommonListManage.OrganizationalChart + " " + Entity.LanguageOption.Name, Condesus.WebUI.Navigation.DeleteType.DeleteModifiedEntity);
                String _entityPropertyName = String.Concat(Entity.LanguageOption.Name);
                NavigatePropertyEntity(Common.ConstantsEntitiesName.DS.OrganizationalChart, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
