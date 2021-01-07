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
using Condesus.WebUI.Navigation;
using Condesus.EMS.Business.KC.Entities;
using System.Reflection;

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class ReportTransformationByScope : BaseProperties
    {
        #region Internal Properties
            private Int64 _IdOrganization;
            private Int64 _IdScope;
            private RadComboBox _RdcOrganization;
            private RadTreeView _RtvOrganization;
            private RadComboBox _RdcAccountingScope;
            private CompareValidator _CvOrganization;
            private CompareValidator _CvAccountingScope;
        #endregion

        #region Page Load & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                base.InjectValidateDateTimePicker(rdtFrom.ClientID, rdtThrough.ClientID, "ReportFilter");

                customvEndDate.ClientValidationFunction = "ValidateDateTimeRangeReportFilter";
                btnList.Click += new EventHandler(btnList_Click);
            }
            protected override void OnPreInit(EventArgs e)
            {
                base.OnPreInit(e);

                //Registro Mis Custom MenuPanels
                RegisterCustomMenuPanels();
                IsGridPageIndexChanged = false;
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                //Arma la grilla completa
                //LoadListManage();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddComboOrganization();
                AddComboAccountingScope();

                if (Assembly.GetAssembly(typeof(ScriptManager)).FullName.IndexOf("3.5") != -1)
                {
                    rgdHierarchy.MasterTableView.FilterExpression = @"it[""IdParent""] = Convert.DBNull";
                }
                else
                {
                    rgdHierarchy.MasterTableView.FilterExpression = "IdParent IS NULL";
                }

                customvEndDate.ErrorMessage = Resources.ConstantMessage.ValidationDateFromTo;

                if (!Page.IsPostBack)
                {
                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }
            
            }
            protected override void SetPagetitle()
            {
                try
                {
                    //Busca el nombre de la pagina si es que se lo pasaron. Sino usa el nombre de la entidad.
                    String _pageTitle = base.NavigatorGetTransferVar<String>("PageTitle");
                    if (String.IsNullOrEmpty(_pageTitle))
                    {
                        base.PageTitle = "Report";
                    }
                    else
                    {
                        base.PageTitle = _pageTitle;
                    }
                }
                catch { base.PageTitle = String.Empty; }
            }
            protected override void SetPageTileSubTitle()
            {
                try
                {
                    String _pageSubTitle = base.NavigatorGetTransferVar<String>("PageSubTitle");
                    if (String.IsNullOrEmpty(_pageSubTitle))
                    {
                        base.PageTitleSubTitle = Resources.CommonListManage.lblSubtitle;
                    }
                    else
                    {
                        base.PageTitleSubTitle = _pageSubTitle;
                    }
                }
                catch
                { base.PageTitleSubTitle = String.Empty; }
            }
        #endregion

        #region Private Methods
            private void AddComboOrganization()
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboWithTreeElementMaps(ref phOrganization, ref _RdcOrganization, ref _RtvOrganization,
                    Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationsRoots, _params, false, true, false, ref _filterExpression,
                    new RadTreeViewEventHandler(rtvOrganizations_NodeExpand),
                    Resources.Common.ComboBoxNoDependency, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.DS.OrganizationClassifications, phOrganizationValidator, ref _CvOrganization, _RdcOrganization, Resources.ConstantMessage.SelectAnOrganization);
            }
            private void AddComboAccountingScope()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();

                String _selectedValue = String.Empty;
                AddCombo(phAccountingScope, ref _RdcAccountingScope, Common.ConstantsEntitiesName.PA.AccountingScopes, _selectedValue, _params, false, true, false, false, false);

                ValidatorRequiredField(Common.ConstantsEntitiesName.PA.AccountingScopes, phAccountingScopeValidator, ref _CvAccountingScope, _RdcAccountingScope, Resources.ConstantMessage.SelectAAccountingScope);
                //_RdcAccountingScope.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(RdcAccountingScope_SelectedIndexChanged);
            }
            private void RegisterCustomMenuPanels()
            {
                List<String> _menuPanels = new List<String>();
                _menuPanels.Add(Common.Constants.ContextInformationKey);
                _menuPanels.Add(Common.Constants.ContextElementMapsKey);

                FwMasterPage.ContentNavigatorCustomMenuPanels(_menuPanels);
            }
            private void LoadListManage()
            {
                //Carga el DataTable.
                Int64 _idIndicator_tnCO2e;
                Int64 _idIndicator_CO2;
                Int64 _idIndicator_CH4;
                Int64 _idIndicator_N2O;
                Int64 _idIndicator_PFC;
                Int64 _idIndicator_HFC;
                Int64 _idIndicator_SF6;

                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_tnCO2e"], out _idIndicator_tnCO2e);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_CO2"], out _idIndicator_CO2);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_CH4"], out _idIndicator_CH4);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_N2O"], out _idIndicator_N2O);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_PFC"], out _idIndicator_PFC);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_HFC"], out _idIndicator_HFC);
                Int64.TryParse(ConfigurationManager.AppSettings["IdIndicator_SF6"], out _idIndicator_SF6);

                if (ManageEntityParams.ContainsKey("IdOrganization"))
                    {ManageEntityParams.Remove("IdOrganization");}
                ManageEntityParams.Add("IdOrganization", _IdOrganization);
                
                if (ManageEntityParams.ContainsKey("IdScope"))
                    { ManageEntityParams.Remove("IdScope"); }
                ManageEntityParams.Add("IdScope", _IdScope);

                if (ManageEntityParams.ContainsKey("IdIndicator_tnCO2e"))
                    { ManageEntityParams.Remove("IdIndicator_tnCO2e"); }
                ManageEntityParams.Add("IdIndicator_tnCO2e", _idIndicator_tnCO2e);
                
                if (ManageEntityParams.ContainsKey("IdIndicator_CO2"))
                    { ManageEntityParams.Remove("IdIndicator_CO2"); }
                ManageEntityParams.Add("IdIndicator_CO2", _idIndicator_CO2);

                if (ManageEntityParams.ContainsKey("IdIndicator_CH4"))
                    { ManageEntityParams.Remove("IdIndicator_CH4"); }
                ManageEntityParams.Add("IdIndicator_CH4", _idIndicator_CH4);

                if (ManageEntityParams.ContainsKey("IdIndicator_N2O"))
                    { ManageEntityParams.Remove("IdIndicator_N2O"); }
                ManageEntityParams.Add("IdIndicator_N2O", _idIndicator_N2O);

                if (ManageEntityParams.ContainsKey("IdIndicator_PFC"))
                    { ManageEntityParams.Remove("IdIndicator_PFC"); }
                ManageEntityParams.Add("IdIndicator_PFC", _idIndicator_PFC);

                if (ManageEntityParams.ContainsKey("IdIndicator_HFC"))
                    { ManageEntityParams.Remove("IdIndicator_HFC"); }
                ManageEntityParams.Add("IdIndicator_HFC", _idIndicator_HFC);

                if (ManageEntityParams.ContainsKey("IdIndicator_SF6"))
                    { ManageEntityParams.Remove("IdIndicator_SF6"); }
                ManageEntityParams.Add("IdIndicator_SF6", _idIndicator_SF6);
                
                if (ManageEntityParams.ContainsKey("StartDate"))
                    { ManageEntityParams.Remove("StartDate"); }
                ManageEntityParams.Add("StartDate", rdtFrom.SelectedDate);
                
                if (ManageEntityParams.ContainsKey("EndDate"))
                    { ManageEntityParams.Remove("EndDate"); }
                ManageEntityParams.Add("EndDate", rdtThrough.SelectedDate);

                BuildGenericDataTable("ReportAG_S_A_FT_F", ManageEntityParams);
                rgdHierarchy.Rebind();
            }
            private void HideExpandColumnRecursive(GridTableView tableView)
            {
                GridItem[] nestedViewItems = tableView.GetItems(GridItemType.NestedView);
                foreach (GridNestedViewItem nestedViewItem in nestedViewItems)
                {
                    foreach (GridTableView nestedView in nestedViewItem.NestedTableViews)
                    {
                        if (nestedView.Items.Count == 0)
                        {

                            TableCell cell = nestedView.ParentItem["ExpandColumn"];
                            cell.CssClass = "ExpandColumn";
                            cell.Controls[0].Visible = false;
                            nestedViewItem.Visible = false;
                        }
                        if (nestedView.HasDetailTables)
                        {
                            HideExpandColumnRecursive(nestedView);
                            TableCell cell = nestedView.ParentItem["ExpandColumn"];
                            cell.CssClass = "ExpandColumn";
                        }
                    }
                }
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
            protected void rgdHierarchy_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
            {
                //Si esta el DataTable lo carga en la grilla
                if (DataTableListManage.ContainsKey("ReportAG_S_A_FT_F"))
                {
                    rgdHierarchy.DataSource = DataTableListManage["ReportAG_S_A_FT_F"]; //CreateTestTable();
                }
            }
            protected void rgdHierarchy_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
            {
                if (e.Column.IsBoundToFieldName("Id") || e.Column.IsBoundToFieldName("IdParent") || e.Column.IsBoundToFieldName("IdFacility") || e.Column.IsBoundToFieldName("IdMeasurement"))
                {
                    e.Column.Visible = false;
                }
                else if (e.Column.IsBoundToFieldName(Resources.CommonListManage.AccountingActivity))
                {
                    e.OwnerTableView.HorizontalAlign = HorizontalAlign.Left;
                    //e.Column.HeaderStyle.Width = Unit.Pixel(180);
                    e.Column.ItemStyle.CssClass = "rgdHierarchyColumn";
                    //e.Column.ItemStyle.BorderWidth = Unit.Pixel(0);          
                }
                else if (e.Column.IsBoundToFieldName("Facility"))
                {
                    e.OwnerTableView.HorizontalAlign = HorizontalAlign.Left;
                    //e.Column.HeaderStyle.Width = Unit.Pixel(130);
                    e.Column.ItemStyle.CssClass = "rgdHierarchyColumn";

                    //e.Column.ItemStyle.BorderWidth = Unit.Pixel(0);
                }
                else if (e.Column.IsBoundToFieldName("Measurement"))
                {
                    e.OwnerTableView.HorizontalAlign = HorizontalAlign.Left;
                    //e.Column.HeaderStyle.Width = Unit.Pixel(130);
                    e.Column.ItemStyle.CssClass = "rgdHierarchyColumn";

                    //e.Column.ItemStyle.BorderWidth = Unit.Pixel(0);
                }
                else if (e.Column is GridBoundColumn)
                {
                    e.Column.HeaderStyle.Width = Unit.Pixel(80);
                    e.Column.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
                    e.Column.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    e.Column.ItemStyle.CssClass = "rgdHierarchyColumn";

                    //e.Column.ItemStyle.BorderWidth = Unit.Pixel(0);
                }
            }
            protected void rgdHierarchy_ItemCreated(object sender, GridItemEventArgs e)
            {
                if ((e.Item is GridHeaderItem && e.Item.OwnerTableView != rgdHierarchy.MasterTableView) || (e.Item is GridNoRecordsItem))
                {
                    e.Item.Style["display"] = "none";
                }
                if (e.Item is GridNestedViewItem)
                {
                    e.Item.Style["display"] = "none";
                }
            }
            protected void rgdHierarchy_PreRender(object sender, EventArgs e)
            {
                HideExpandColumnRecursive(rgdHierarchy.MasterTableView);
            }
            protected void btnList_Click(object sender, EventArgs e)
            {
                _IdOrganization = Convert.ToInt64(GetKeyValue(_RdcOrganization.SelectedValue, "IdOrganization"));
                _IdScope = Convert.ToInt64(GetKeyValue(_RdcAccountingScope.SelectedValue, "IdScope"));

                LoadListManage();
            }
        #endregion
    }
}
