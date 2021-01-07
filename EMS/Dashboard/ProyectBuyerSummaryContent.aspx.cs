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
using Dundas.Charting.WebControl;

using Condesus.EMS.WebUI.MasterControls;
using EBPE = Condesus.EMS.Business.PF.Entities;
using EBPAE = Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.EP.Entities;

namespace Condesus.EMS.WebUI.Dashboard
{
    public partial class ProyectBuyerSummaryContent : BasePage
    {
        #region Internal Properties
        //Estructura interna para guardar los totales
        private Decimal _EstimationTotal
        {
            get { return (Decimal)ViewState["EstimationTotal"]; }
            set { ViewState["EstimationTotal"] = value; }
        }
        private Decimal _CalculationTotal
        {
            get { return (Decimal)ViewState["CalculationTotal"]; }
            set { ViewState["CalculationTotal"] = value; }
        }
        private Decimal _CertificatedTotal
        {
            get { return (Decimal)ViewState["CertificatedTotal"]; }
            set { ViewState["CertificatedTotal"] = value; }
        }

        private Condesus.EMS.WebUI.Dashboard.Controls.ProjectSummary _ProjectSummary;

        EBPE.ProcessGroupProcess _ProcessRoot = null;

        private Int64 _IdEntity
        {
            get
            {
                return base.NavigatorContainsTransferVar("IdProcess") ? base.NavigatorGetTransferVar<Int64>("IdProcess") : Convert.ToInt64(GetPKfromNavigator("IdProcess"));
            }
        }

        private Dundas.Charting.WebControl.Series _ChartSeriesForecasted
        {
            get { return chtIndicator.Series["PDD ERs"]; }
        }
        private Dundas.Charting.WebControl.Series _ChartSeriesCalculated
        {
            get { return chtIndicator.Series["Current ERs"]; }
        }
        private Dundas.Charting.WebControl.Series _ChartSeriesVerificated
        {
            get { return chtIndicator.Series["Issued ERs"]; }
        }

        #endregion

        #region Load & Init

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InitializeHandlers();
        }

        private void InitializeHandlers()
        {
            btnPrevPicture.Click += new ImageClickEventHandler(PagerPicture_Click);
            btnNextPicture.Click += new ImageClickEventHandler(PagerPicture_Click);
            imgBtnRefreshChart.Click += new ImageClickEventHandler(imgBtnRefreshChart_Click);
            imgBtnResetChart.Click += new ImageClickEventHandler(imgBtnResetChart_Click);
        }

        private void InitializeMainEntity()
        {
            if (_ProcessRoot == null)
                _ProcessRoot = (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess)EMSLibrary.User.ProcessFramework.Map.Process(_IdEntity);

            if (_ProcessRoot == null)
                throw new Exception("The page could not instantiate the Main Entity");

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                _EstimationTotal = 0;
                _CertificatedTotal = 0;
                _CalculationTotal = 0;

                InitializeMainEntity();

                //Main Data
                InitializeMainData();

                //Init Image Viewer
                SetImageViewerContent(0);
                SetPagerStatus(0);

                //Set date options

                LoadChartData(null, null);
                //LoadBarChartData();
                LoadPerformanceChart();
            }

            InitializeMainEntity();

            //Load de los demas Controles Dinamicos
            BuildControls();

         }
        protected override void SetPagetitle()
        {
            base.PageTitle = _ProcessRoot.LanguageOption.Title;
        }
        protected override void SetPageTileSubTitle()
        {
            base.PageTitleSubTitle = GetLocalResourceObject("PageTitleSubTitle").ToString();
        }
        private void InitializeMainData()
        {
            //'Main' Properties
            lblNameProject.Text = _ProcessRoot.LanguageOption.Title;
            //lblCategoryValue.Text = _ProcessRoot.ProcessClassifications.First().Value.LanguageOption.Name;
            lblDescriptionValue.Text = _ProcessRoot.LanguageOption.Description;
            lblStatusDescriptionValue.Text = _ProcessRoot.State;

            //Extended Properties
            ExtendedPropertyValue _bufExtProp = null;
            Int64 _idErType = 0;//IdExtendedPropertyERType
            Int64 _idLocation = 0;//IdExtendedPropertyLocation
            Int64 _idStatus = 0;//IdExtendedPropertyStatus
            Int64 _idCategory = 0;//IdExtendedPropertyCategory

            Int64 _idCountry = 0;//IdExtendedPropertyCountry
            Int64 _idLastVisit = 0;//IdExtendedPropertyLastVisit
            Int64 _idNextVisit = 0;//IdExtendedPropertyNextVisit

            if (Int64.TryParse(ConfigurationManager.AppSettings["IdExtendedPropertyCategory"], out _idCategory))
            {
                _bufExtProp = _ProcessRoot.ExtendedPropertyValue(_idCategory);
                if (_bufExtProp != null)
                {
                    lblCategory.Text = _bufExtProp.ExtendedProperty.LanguageOption.Name;
                    lblCategoryValue.Text = _bufExtProp.Value;
                }
            }

            if (Int64.TryParse(ConfigurationManager.AppSettings["IdExtendedPropertyERType"], out _idErType))
            {
                _bufExtProp = _ProcessRoot.ExtendedPropertyValue(_idErType);
                if (_bufExtProp != null)
                {
                    lblErType.Text = _bufExtProp.ExtendedProperty.LanguageOption.Name;
                    lblErTypeValue.Text = _bufExtProp.Value;
                }
            }

            if (Int64.TryParse(ConfigurationManager.AppSettings["IdExtendedPropertyLocation"], out _idLocation))
            {
                _bufExtProp = _ProcessRoot.ExtendedPropertyValue(_idLocation);
                if (_bufExtProp != null)
                {
                    lblLocation.Text = _bufExtProp.ExtendedProperty.LanguageOption.Name;
                    lblLocationValue.Text = _bufExtProp.Value;
                }
            }

            if (Int64.TryParse(ConfigurationManager.AppSettings["IdExtendedPropertyStatus"], out _idStatus))
            {
                _bufExtProp = _ProcessRoot.ExtendedPropertyValue(_idStatus);
                if (_bufExtProp != null)
                {
                    lblStatusDescription.Text = _bufExtProp.ExtendedProperty.LanguageOption.Name;
                    lblStatusDescriptionValue.Text = _bufExtProp.Value;
                }
            }

            if (Int64.TryParse(ConfigurationManager.AppSettings["IdExtendedPropertyCountry"], out _idCountry))
            {
                _bufExtProp = _ProcessRoot.ExtendedPropertyValue(_idCountry);
                if (_bufExtProp != null)
                {
                    lblCountry.Text = _bufExtProp.ExtendedProperty.LanguageOption.Name;
                    lblCountryValue.Text = _bufExtProp.Value;
                }
            }
            if (Int64.TryParse(ConfigurationManager.AppSettings["IdExtendedPropertyLastVisit"], out _idLastVisit))
            {
                _bufExtProp = _ProcessRoot.ExtendedPropertyValue(_idLastVisit);
                if (_bufExtProp != null)
                {
                    lblLastVisit.Text = _bufExtProp.ExtendedProperty.LanguageOption.Name;
                    lblLastVisitValue.Text = _bufExtProp.Value;
                }
            }
            if (Int64.TryParse(ConfigurationManager.AppSettings["IdExtendedPropertyNextVisit"], out _idNextVisit))
            {
                _bufExtProp = _ProcessRoot.ExtendedPropertyValue(_idNextVisit);
                if (_bufExtProp != null)
                {
                    lblNextVisit.Text = _bufExtProp.ExtendedProperty.LanguageOption.Name;
                    lblNextVisitValue.Text = _bufExtProp.Value;
                }
            }

        }

        private void SetImageViewerContent(Int32 index)
        {
            Int64 _idResource = -1;
            Int64 _idResourceFile = -1;
            try
            {
                if (_ProcessRoot.Pictures.Count == 0)
                {
                    imgShowSlide.ImageUrl = "Skins/Images/NoImagesAvailable.gif";   // "~/App_Themes/" + Page.Theme + "/Images/NoImagesAvailable.gif";
                    hdn_ImagePosition.Value = "0";
                    return;
                }

                _idResource = _ProcessRoot.Pictures.ElementAt(index).Value.IdResource;
                _idResourceFile = _ProcessRoot.Pictures.ElementAt(index).Value.IdResourceFile;
            }
            catch (IndexOutOfRangeException ex)
            {
                _idResource = _ProcessRoot.Pictures.ElementAt(_ProcessRoot.Pictures.Count - 1).Value.IdResource;
                _idResourceFile = _ProcessRoot.Pictures.ElementAt(_ProcessRoot.Pictures.Count - 1).Value.IdResourceFile;
                hdn_ImagePosition.Value = (_ProcessRoot.Pictures.Count - 1).ToString();
            }

            imgShowSlide.ImageUrl = "~/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx?IdResource=" + _idResource.ToString() + "&IdResourceFile=" + _idResourceFile.ToString();
        }

        private void SetPagerStatus(Int32 index)
        {
            btnPrevPicture.Enabled = true;
            btnNextPicture.Enabled = true;

            if (index == 0)
                btnPrevPicture.Enabled = false;
            if (index == _ProcessRoot.Pictures.Count - 1 || index > _ProcessRoot.Pictures.Count - 1)
                btnNextPicture.Enabled = false;

            //Si no hay ninguna foto en la coleccion
            if (_ProcessRoot.Pictures.Count == 0)
                btnPrevPicture.Enabled = btnNextPicture.Enabled = false;

            btnPrevPicture.ImageUrl = "~/Skins/Images/Trans.gif";
            btnNextPicture.ImageUrl = "~/Skins/Images/Trans.gif";
            
            //btnPrevPicture.ImageUrl = (btnPrevPicture.Enabled) ? "~/App_Themes/" + Page.Theme + "/Images/Tras.gif" : "~/App_Themes/" + Page.Theme + "/Images/Tras.gif";
            //btnNextPicture.ImageUrl = (btnNextPicture.Enabled) ? "~/App_Themes/" + Page.Theme + "/Images/Tras.gif" : "~/App_Themes/" + Page.Theme + "/Images/Tras.gif";
        }

        #endregion

        #region Methods

        private void BuildControls()
        {
            ClearContainers();

            BuildResourceContent();
            BuildVerificationContent();
            BuildCurrentContent();
            BuildKeyIndicatorContent();
        }

        private void ClearContainers()
        {
            pnlDocuments.Controls.Clear();
            pnlVerifications.Controls.Clear();
            pnlSummary.Controls.Clear();
            pnlKeyIndicator.Controls.Clear();
        }

        private static void InitCommonGridProperties(RadGrid grid)
        {
            grid.AllowPaging = false;
            grid.AllowSorting = false;
            grid.Skin = "EMS";
            grid.EnableEmbeddedSkins = false;
            grid.Width = Unit.Percentage(100);
            grid.AutoGenerateColumns = false;
            //grid.EnableAJAX = false;
            grid.GridLines = System.Web.UI.WebControls.GridLines.None;
            grid.ShowStatusBar = false;
            grid.PageSize = 18;
            grid.AllowMultiRowSelection = false;
            //grid.EnableAJAXLoadingTemplate = true;
            //grid.LoadingTemplateTransparency = 25;
            grid.PagerStyle.AlwaysVisible = true;
            grid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
            grid.MasterTableView.Width = Unit.Percentage(100);
            grid.EnableViewState = false;

            //Crea los metodos de la grilla (Cliente)
            grid.ClientSettings.AllowExpandCollapse = false;
            //grid.ClientSettings.EnableClientKeyValues = true;
            grid.AllowMultiRowSelection = false;
            grid.ClientSettings.Selecting.AllowRowSelect = true;
            grid.ClientSettings.Selecting.EnableDragToSelectRows = false;
            grid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;

            //Define los atributos de la MasterGrid
            grid.MasterTableView.EnableViewState = false;
            grid.MasterTableView.CellPadding = 0;
            grid.MasterTableView.CellSpacing = 0;
            grid.MasterTableView.GridLines = System.Web.UI.WebControls.GridLines.None;
            grid.MasterTableView.GroupsDefaultExpanded = false;
            grid.MasterTableView.AllowMultiColumnSorting = false;
            grid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
            grid.MasterTableView.ExpandCollapseColumn.Resizable = false;
            grid.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = Unit.Pixel(20);
            grid.MasterTableView.RowIndicatorColumn.Visible = false;
            grid.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = Unit.Pixel(20);
            grid.MasterTableView.EditMode = GridEditMode.InPlace;

            grid.HeaderStyle.Font.Bold = false;
            grid.HeaderStyle.Font.Italic = false;
            grid.HeaderStyle.Font.Overline = false;
            grid.HeaderStyle.Font.Strikeout = false;
            grid.HeaderStyle.Font.Underline = false;
            grid.HeaderStyle.Wrap = true;
        }

        #region Current ER
        private void BuildCurrentContent()
        {
            RadGrid _rdgCurrent = new RadGrid();

            BuildCurrentData(_rdgCurrent);

            pnlSummary.Controls.Add(_rdgCurrent);
        }
        private void BuildCurrentData(RadGrid rdgCurrent)
        {
            #region Grilla
            rdgCurrent.ID = "rgdMasterGridCurrentClass";
            InitCommonGridProperties(rdgCurrent);
            rdgCurrent.AllowPaging = false;

            //Crea los metodos de la grilla (Server)
            rdgCurrent.NeedDataSource += new GridNeedDataSourceEventHandler(rdgCurrent_NeedDataSource);

            //Define los atributos de la MasterGrid
            rdgCurrent.MasterTableView.Name = "gridMasterCurrent";

            //Crea las columnas para la MasterGrid.
            DefineColumnsCurrent(rdgCurrent.MasterTableView);

            #endregion
        }

        private void DefineColumnsCurrent(GridTableView gridTableViewDetails)
        {
            //Add columns bound
            GridBoundColumn boundColumn;

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "CurrentDate";
            boundColumn.HeaderText = "Current Date";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Forecasted";
            boundColumn.HeaderText = "PDD ERs";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Calculated";
            boundColumn.HeaderText = "Calculated";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Deviation";
            boundColumn.HeaderText = "Deviation %";
            gridTableViewDetails.Columns.Add(boundColumn);
        }

        private DataTable ReturnDataGridCurrent()
        {
            DataTable _dt = new DataTable();
            _dt.TableName = "Root";
            _dt.Columns.Add("CurrentDate");
            _dt.Columns.Add("Forecasted");
            _dt.Columns.Add("Calculated");
            _dt.Columns.Add("Deviation");

            if (_ProcessRoot != null)
            {
                Decimal _state;
                Decimal _deviationTotal = 0;
                Decimal _currentTotal = _ProcessRoot.FirstCalculationResult();  //Esto tomal el valor del calculo para el periodo actual.

                _EstimationTotal = _ProcessRoot.TotalEstimatesByScenario(EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(Convert.ToInt64(ConfigurationManager.AppSettings["PDDEstimated"])));

                if (_EstimationTotal == 0)
                { _state = 0; }
                else
                { _state = Math.Round((_currentTotal / _EstimationTotal * 100), 2); }

                //Totals
                //_EstimationTotal += _estimated;
                //_CurrentTotal += _current;

                _dt.Rows.Add(_ProcessRoot.CurrentCampaignStartDate.ToShortDateString(),
                               Math.Round(_EstimationTotal, 2),
                               Math.Round(_currentTotal, 2),
                               _state);

                if (_EstimationTotal == 0)
                { _deviationTotal = 0; }
                else
                { _deviationTotal = Math.Round(_currentTotal / _EstimationTotal * 100, 2); }

            }
            return _dt;
        }

        #region Eventos
        void rdgCurrent_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridCurrent();
        }
        #endregion

        #endregion

        #region Resources

        private void BuildResourceContent()
        {
            RadGrid _rdgResources = new RadGrid();

            BuildResourceData(_rdgResources);

            pnlDocuments.Controls.Add(_rdgResources);

            InjectLocalJS(_rdgResources.ClientID);
        }

        private void BuildResourceData(RadGrid rgdResources)
        {
            #region Grilla
            rgdResources.ID = "rgdMasterGridResourceClass";
            InitCommonGridProperties(rgdResources);

            //Crea los metodos de la grilla (Server)
            rgdResources.NeedDataSource += new GridNeedDataSourceEventHandler(_RgdMasterGridProcessResources_NeedDataSource);
            //rgdResources.ItemCreated += new GridItemEventHandler(_RgdMasterGridProcessResources_ItemCreated);
            rgdResources.ItemDataBound += new GridItemEventHandler(rgdResources_ItemDataBound);
            rgdResources.SortCommand += new GridSortCommandEventHandler(_RgdMasterGridProcessResources_SortCommand);
            rgdResources.PageIndexChanged += new GridPageChangedEventHandler(_RgdMasterGridProcessResources_PageIndexChanged);

            //Define los atributos de la MasterGrid
            rgdResources.MasterTableView.DataKeyNames = new string[] { "IdResource", "IdResourceType", "IdResourceFile", "ResourceType", "FileURL" };
            rgdResources.MasterTableView.Name = "gridMasterResources";

            //Crea las columnas para la MasterGrid.
            DefineColumnsProcessResources(rgdResources.MasterTableView);

            #endregion
        }

        

        private void DefineColumnsProcessResources(GridTableView gridTableViewDetails)
        {
            GridTemplateColumn template;

            //Add columns bound
            GridBoundColumn boundColumn;

            //Columnas que no se ven...
            //Crea y agrega las columnas de tipo Bound
            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "IdResource";
            boundColumn.HeaderText = "IdResource";
            boundColumn.Display = false;
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "IdResourceType";
            boundColumn.HeaderText = "IdResourceType";
            boundColumn.Display = false;
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "IdResourceFile";
            boundColumn.HeaderText = "IdResourceFile";
            boundColumn.Display = false;
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "ResourceType";
            boundColumn.HeaderText = "ResourceType";
            boundColumn.Display = false;
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "FileURL";
            boundColumn.HeaderText = "FileURL";
            boundColumn.Display = false;
            gridTableViewDetails.Columns.Add(boundColumn);

            //Crea y agrega la columna del Tipo de Resource
            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Type";
            boundColumn.HeaderText = "Type";
            boundColumn.ItemStyle.Width = Unit.Pixel(200);
            boundColumn.Display = true;
            gridTableViewDetails.Columns.Add(boundColumn);

            //Crea y agrega las columnas de tipo Bound
            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Name";
            boundColumn.HeaderText = "Name";
            boundColumn.ItemStyle.Width = Unit.Pixel(200);
            boundColumn.Display = true;
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Comment";
            boundColumn.HeaderText = "Comment";
            boundColumn.Display = true;
            gridTableViewDetails.Columns.Add(boundColumn);

            TableItemStyle headerStyle;
            template = new GridTemplateColumn();
            template.UniqueName = "OpenFile";
            template.ItemTemplate = new MyTemplateReport("Open File"); //DBImageButtonTemplate("btnOpenFile");     //MyTemplateReport("Open File");
            template.ItemStyle.Width = Unit.Pixel(21);
            headerStyle = template.HeaderStyle;
            headerStyle.Width = Unit.Pixel(21);
            template.HeaderText = "Open File";
            template.AllowFiltering = false;
            gridTableViewDetails.Columns.Add(template);

        }

        private DataTable ReturnDataGridResources()
        {
            //Crea el DataTable para luego devolverlo y asociarlo a la grilla.
            DataTable _dt = new DataTable();
            _dt.TableName = "Root";
            _dt.Columns.Add("IdResource");
            _dt.Columns.Add("IdResourceType");  //Indica el Tipo de resource
            _dt.Columns.Add("IdResourceFile");  //Id del archivo
            _dt.Columns.Add("ResourceType");   //Indica si es URL o DOC
            _dt.Columns.Add("FileURL");     //Indica el nombre del archivo o la direccion URL
            _dt.Columns.Add("Type");
            _dt.Columns.Add("Name");
            _dt.Columns.Add("Comment");

            foreach (Condesus.EMS.Business.PF.Entities.ProcessResource oProcessResource in _ProcessRoot.ProcessResources)
            {
                if (oProcessResource.Resource.GetType().Name == "ResourceVersion")
                {
                    Condesus.EMS.Business.KC.Entities.ResourceVersion _resource = (Condesus.EMS.Business.KC.Entities.ResourceVersion)oProcessResource.Resource;
                    String _name = Common.Functions.ReplaceIndexesTags(_resource.LanguageOption.Title);

                    String _fileUrl = String.Empty;
                    String _resourceType = String.Empty;

                    if (_resource.CurrentVersion != null)
                    {
                        //Solo deberia mostrar el file que corresponde al Activo.
                        if (_resource.CurrentVersion.GetType().Name == "VersionURL")
                        {
                            _fileUrl = ((Condesus.EMS.Business.KC.Entities.VersionURL)_resource.CurrentVersion).Url;
                            _resourceType = "URL";
                        }
                        else
                        {
                            _fileUrl = ((Condesus.EMS.Business.KC.Entities.VersionDoc)_resource.CurrentVersion).FileAttach.FileName;
                            _resourceType = "DOC";
                        }

                        _dt.Rows.Add(_resource.IdResource, _resource.ResourceType.IdResourceType, _resource.CurrentVersion.IdResourceFile,
                            _resourceType, _fileUrl, _resource.ResourceType.LanguageOption.Name, _name, oProcessResource.Comment);
                    }
                    else
                    {
                        _dt.Rows.Add(_resource.IdResource, _resource.ResourceType.IdResourceType, 0,
                            String.Empty, String.Empty, _resource.ResourceType.LanguageOption.Name, _name, oProcessResource.Comment);
                    }
                }
            }

            return _dt;
        }

        private void InjectLocalJS(String gridClientId)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();

            //Esta funcion es la encargada de llamar a la pagina que carga el contenido de los archivos.
            //Parametros    <e> event
            //          <idGridRow> el indice del row en donde esta parado
            //          <idGridTable> el id de la tabla donde esta parado(por si es jerarquica)
            _sbBuffer.Append("function ShowFile(e, resourceType, urlName, idResource, idResourceFile)                                                          \n");
            _sbBuffer.Append("{                                                                                                     \n");
            _sbBuffer.Append("      var _IEXPLORER = 'Microsoft Internet Explorer';                                             \n");
            _sbBuffer.Append("      var _FIREFOX = 'Netscape';                                                                  \n");
            _sbBuffer.Append("      var _BrowserName = navigator.appName;                                                       \n");

            //_sbBuffer.Append("      StopEvent(e);                                                                                   \n");
            _sbBuffer.Append("      if (resourceType.indexOf('DOC') >= 0)                                                                          \n");
            _sbBuffer.Append("      {                                                                                                     \n");
            //Abre una nueva ventana con el archivo.
            _sbBuffer.Append("          if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("          {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("              var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx?IdResource=' + idResource + '&IdResourceFile=' + idResourceFile, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("          }                                                                                           \n");
            _sbBuffer.Append("          else                                                                                        \n");
            _sbBuffer.Append("          {   //FireFox                                                                               \n");
            _sbBuffer.Append("              var newWindow = window.parent.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx?IdResource=' + idResource + '&IdResourceFile=' + idResourceFile, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("          }                                                                                           \n");
            //_sbBuffer.Append("          var newWindow = window.open('http://" + Request.ServerVariables["HTTP_HOST"] + "/ManagementTools/KnowledgeCollaboration/FilesViewer.aspx?IdResource=' + idResource + '&IdResourceFile=' + idResourceFile, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');     \n");
            //_sbBuffer.Append("          newWindow.focus();                                                                                \n");
            ////frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            //_sbBuffer.Append("          StopEvent(e);     //window.event.returnValue = false;                                                                   \n");
            _sbBuffer.Append("      }                                                                                                       \n");
            _sbBuffer.Append("      else                                                                                                             \n");
            _sbBuffer.Append("      {                                                                                                     \n");
            _sbBuffer.Append("          if (_BrowserName == _IEXPLORER)                                                             \n");
            _sbBuffer.Append("          {   //IE and Opera                                                                          \n");
            _sbBuffer.Append("              var newWindow = window.open('http://' + urlName, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("          }                                                                                           \n");
            _sbBuffer.Append("          else                                                                                        \n");
            _sbBuffer.Append("          {   //FireFox                                                                               \n");
            _sbBuffer.Append("              var newWindow = window.parent.open('http://' + urlName, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');      \n");
            _sbBuffer.Append("          }                                                                                           \n");
            //_sbBuffer.Append("          var newWindow = window.open('http://' + urlName, 'Reports', 'toolbar=yes,location=yes,resizable=yes,status=yes,menubar=yes,scrollbars=yes');     \n");
            //_sbBuffer.Append("          newWindow.focus();                                                                                \n");
            ////frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            //_sbBuffer.Append("          StopEvent(e);     //window.event.returnValue = false;                                                                   \n");
            _sbBuffer.Append("      }                                                                                                       \n");
            _sbBuffer.Append("      newWindow.focus();                                                                                \n");
            //frena la ejecucion de todo y queda esperando a la confirmacion en el popup.
            _sbBuffer.Append("      StopEvent(e);     //window.event.returnValue = false;                                                                   \n");
            _sbBuffer.Append("}                                                                                                             \n");

            ScriptManager.RegisterClientScriptBlock(this.Page, typeof(UpdatePanel), "JSShowFile", _sbBuffer.ToString(), true);
        }

        #region Eventos
        void rgdResources_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //String _idMeasurement = ((DataRowView)e.Item.DataItem).Row["IdMeasurement"].ToString();
                String _resourceType = ((DataRowView)e.Item.DataItem).Row["ResourceType"].ToString();
                String _urlName = ((DataRowView)e.Item.DataItem).Row["FileURL"].ToString();
                Int64 _idResource = Convert.ToInt64(((DataRowView)e.Item.DataItem).Row["IdResource"]);
                Int64 _idResourceFile = Convert.ToInt64(((DataRowView)e.Item.DataItem).Row["IdResourceFile"]);

                //String _resourceType = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ResourceType"].ToString();
                //String _urlName = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FileURL"].ToString();
                //Int64 _idResource = Convert.ToInt64(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["idResource"]);
                //Int64 _idResourceFile = Convert.ToInt64(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IdResourceFile"]);
                //Si el file es 0 no tiene que poner el icono

                HtmlImage oimg2 = (HtmlImage)e.Item.FindControl("rptButton");
                //ImageButton oimg2 = (ImageButton)e.Item.FindControl("btnOpenFile");
                if (!(oimg2 == null))
                {
                    if (_idResourceFile > 0)
                    {
                        oimg2.Attributes["onclick"] = string.Format("return ShowFile(event, '" + _resourceType + "','" + _urlName + "'," + _idResource + ", " + _idResourceFile + ");");
                        oimg2.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                    }
                    else
                    {
                        //No quiero que salga el icono, pero que no pierda el estilo la celda.
                        oimg2.Attributes["class"] = String.Empty;
                        //oimg2.Visible = false;
                    }
                }
            }
        }
        void _RgdMasterGridProcessResources_ItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    Int64 _idResourceFile = Convert.ToInt64(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IdResourceFile"]);
            //    //Si el file es 0 no tiene que poner el icono

            //    HtmlImage oimg2 = (HtmlImage)e.Item.FindControl("imgOpenFile");
            //    if (!(oimg2 == null))
            //    {
            //        if (_idResourceFile > 0)
            //        {
            //            oimg2.Attributes["onclick"] = string.Format("return ShowFile(event, " + e.Item.ItemIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
            //            oimg2.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
            //        }
            //        else
            //        {
            //            oimg2.Visible = false;
            //        }
            //    }
            //}
        }
        void _RgdMasterGridProcessResources_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridResources();
            handler.MasterTableView.Rebind();
        }
        void _RgdMasterGridProcessResources_SortCommand(object source, GridSortCommandEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridResources();
            handler.MasterTableView.Rebind();
        }
        void _RgdMasterGridProcessResources_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridResources();
        }
        #endregion

        #endregion

        #region Verifications

        private void BuildVerificationContent()
        {
            RadGrid _rdgVerifications = new RadGrid();

            BuildVerificationData(_rdgVerifications);

            pnlVerifications.Controls.Add(_rdgVerifications);
        }

        private void BuildVerificationData(RadGrid rgdVerifications)
        {
            #region Grilla
            rgdVerifications.ID = "rgdMasterGridVerificationClass";

            InitCommonGridProperties(rgdVerifications);

            //Crea los metodos de la grilla (Server)
            rgdVerifications.NeedDataSource += new GridNeedDataSourceEventHandler(rgdVerifications_NeedDataSource);
            //rgdVerifications.SortCommand += new GridSortCommandEventHandler(rgdVerifications_SortCommand);
            rgdVerifications.PageIndexChanged += new GridPageChangedEventHandler(rgdVerifications_PageIndexChanged);

            //Define los atributos de la MasterGrid
            rgdVerifications.MasterTableView.Name = "gridMasterVerification";

            //Crea las columnas para la MasterGrid.
            DefineColumnsProcessVerifications(rgdVerifications.MasterTableView);

            #endregion
        }

        private void DefineColumnsProcessVerifications(GridTableView gridTableViewDetails)
        {
            //Add columns bound
            GridBoundColumn boundColumn;

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "StartDate";
            boundColumn.HeaderText = "Start Date";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "EndDate";
            boundColumn.HeaderText = "End Date";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Forecasted";
            boundColumn.HeaderText = "PDD ERs";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Verification";
            boundColumn.HeaderText = "Issued";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Deviation";
            boundColumn.HeaderText = "Deviation %";
            gridTableViewDetails.Columns.Add(boundColumn);
        }


        private DataTable ReturnDataGridVerifications()
        {
            DataTable _dt = new DataTable();
            _dt.TableName = "Root";
            _dt.Columns.Add("StartDate");
            _dt.Columns.Add("EndDate");
            _dt.Columns.Add("Verification");
            _dt.Columns.Add("Forecasted");
            _dt.Columns.Add("Deviation");

            EBPAE.CalculationScenarioType _scenarioType = EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(Convert.ToInt64(ConfigurationManager.AppSettings["PDDEstimated"]));
            Decimal _estimationTotal = 0;
            Decimal _verificatedTotal = 0;

            Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Calculation> _calculations = _ProcessRoot.AssociatedCalculations;
            if (_calculations.Count > 0)
            {
                foreach (EBPAE.CalculationCertificated _calcCert in _calculations.First().Value.CalculationCertificates)
                {
                    Decimal _deviation;
                    if (_calcCert.CertificationDeviation(_scenarioType) == 0)
                    { _deviation = 0; }
                    else
                    { _deviation = Math.Round((_calcCert.Value / _calcCert.CertificationDeviation(_scenarioType)) * 100, 2); }

                    Decimal _verification = Math.Round(_calcCert.Value, 2);
                    Decimal _estimation = Math.Round(_calcCert.CertificationDeviation(_scenarioType), 2);

                    //Totales
                    _estimationTotal += _calcCert.CertificationDeviation(_scenarioType);
                    //_CertificatedTotal += _calcCert.Value;
                    _verificatedTotal += _calcCert.Value;

                    _dt.Rows.Add(_calcCert.StartDate.ToShortDateString(), _calcCert.EndDate.ToShortDateString(), _verification, _estimation, _deviation);
                }
            }
            //Saca el total de la desviacion.
            Decimal _deviationTotal;
            if (_estimationTotal == 0)
            { _deviationTotal = 0; }
            else
            { _deviationTotal = Math.Round(_verificatedTotal / _estimationTotal * 100, 2); }

            //_dt.Rows.Add(_ProcessRoot.CurrentCampaignStartDate.ToShortDateString(), "N/A", "N/A", "N/A", "N/A");
            _dt.Rows.Add("", "<b>Total</b>", "<b>" + Math.Round(_verificatedTotal, 2).ToString() + "</b>", "<b>" + Math.Round(_estimationTotal, 2).ToString() + "</b>", "<b>" + _deviationTotal.ToString() + "</b>");
            return _dt;
        }

        #region Eventos

        void rgdVerifications_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridVerifications();
            handler.MasterTableView.Rebind();
        }

        void rgdVerifications_SortCommand(object source, GridSortCommandEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridVerifications();
            handler.MasterTableView.Rebind();
        }

        void rgdVerifications_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridVerifications();
        }

        #endregion

        #endregion

        #region Key Indicators
        private void BuildKeyIndicatorContent()
        {
            RadGrid _rdgKeyIndicator = new RadGrid();

            BuildKeyIndicatorData(_rdgKeyIndicator);

            pnlKeyIndicator.Controls.Add(_rdgKeyIndicator);
        }
        private void BuildKeyIndicatorData(RadGrid rdgKeyIndicator)
        {
            #region Grilla
            rdgKeyIndicator.ID = "rgdMasterGridKeyIndicatorClass";
            InitCommonGridProperties(rdgKeyIndicator);
            rdgKeyIndicator.AllowPaging = false;
            //rdgKeyIndicator.ClientSettings.EnableClientKeyValues = true;
            rdgKeyIndicator.MasterTableView.DataKeyNames = new String[] { "IdMeasurement" };
            //Crea los metodos de la grilla (Server)
            rdgKeyIndicator.NeedDataSource += new GridNeedDataSourceEventHandler(rdgKeyIndicator_NeedDataSource);
            //rdgKeyIndicator.ItemCreated += new GridItemEventHandler(rdgKeyIndicator_ItemCreated);
            rdgKeyIndicator.ItemDataBound += new GridItemEventHandler(rdgKeyIndicator_ItemDataBound);
            //Define los atributos de la MasterGrid
            rdgKeyIndicator.MasterTableView.Name = "gridMasterKeyIndicator";

            //Crea las columnas para la MasterGrid.
            DefineColumnsKeyIndicator(rdgKeyIndicator.MasterTableView);

            #endregion


        }

       

        private void DefineColumnsKeyIndicator(GridTableView gridTableViewDetails)
        {
            //Add columns bound
            GridBoundColumn boundColumn;


            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "IdMeasurement";
            boundColumn.HeaderText = "IdMeasurement";
            boundColumn.Display = false;
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "MeasurementName";
            boundColumn.HeaderText = "Measurement Name";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "IndicatorName";
            boundColumn.HeaderText = "Indicator Name";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "DeviceName";
            boundColumn.HeaderText = "Device Name";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Value";
            boundColumn.HeaderText = "Value";
            gridTableViewDetails.Columns.Add(boundColumn);

            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Type";
            boundColumn.HeaderText = "Type";
            gridTableViewDetails.Columns.Add(boundColumn);

            //Muestra la fecha
            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "Period";
            boundColumn.HeaderText = "Period";
            gridTableViewDetails.Columns.Add(boundColumn);

            //muestra la unidad de medida...
            boundColumn = new GridBoundColumn();
            boundColumn.DataField = "MeasurementUnit";
            boundColumn.HeaderText = "Measurement Unit";
            gridTableViewDetails.Columns.Add(boundColumn);


            GridTemplateColumn _rdgTempCol;
            TableItemStyle headerStyle;
            _rdgTempCol = new GridTemplateColumn();
            _rdgTempCol.UniqueName = "TemplateColumnChart";
            _rdgTempCol.ItemTemplate = new DBImageButtonTemplate("btnChartLink");
            _rdgTempCol.ItemStyle.Width = Unit.Pixel(21);
            headerStyle = _rdgTempCol.HeaderStyle;
            headerStyle.Width = Unit.Pixel(21);
            _rdgTempCol.HeaderText = "Chart";
            _rdgTempCol.AllowFiltering = false;
            gridTableViewDetails.Columns.Add(_rdgTempCol);

            _rdgTempCol = new GridTemplateColumn();
            _rdgTempCol.UniqueName = "TemplateColumnSeries";
            _rdgTempCol.ItemTemplate = new DBImageButtonTemplate("btnSeriesLink");
            _rdgTempCol.ItemStyle.Width = Unit.Pixel(21);
            headerStyle = _rdgTempCol.HeaderStyle;
            headerStyle.Width = Unit.Pixel(21);
            _rdgTempCol.HeaderText = "Series";
            _rdgTempCol.AllowFiltering = false;
            gridTableViewDetails.Columns.Add(_rdgTempCol);
        }

        private DataTable ReturnDataGridKeyIndicator()
        {
            DataTable _dt = new DataTable();
            _dt.TableName = "Root";
            _dt.Columns.Add("IdMeasurement");
            _dt.Columns.Add("MeasurementName");
            _dt.Columns.Add("IndicatorName");
            _dt.Columns.Add("DeviceName");
            _dt.Columns.Add("Value");
            _dt.Columns.Add("Type");
            _dt.Columns.Add("Period");
            _dt.Columns.Add("MeasurementUnit");

            if (_ProcessRoot != null)
            {
                foreach (Condesus.EMS.Business.PA.Entities.Measurement _measurement in _ProcessRoot.Measurements.Values)
                {
                    if (_measurement.IsRelevant)
                    {
                        String _value;
                        String _period;
                        Double _measureValue = 0;
                        DateTime? _firsDateSeries = null;   //Esto lo necesita para pasarlo al totalmeasurement. y esperar la primer fecha cuando es cumulative.
                        Condesus.EMS.Business.PA.Entities.MeasurementPoint _measurementPoint = _measurement.TotalMeasurement(ref _firsDateSeries);

                        if (_measurementPoint != null)
                        {
                            _measureValue = _measurementPoint.MeasureValue;
                            //Cuando es Acumulativa, entonces se guarda la fecha la primer fecha de la medicion y la ultima.
                            if (_measurement.GetType().Name == Common.ConstantsEntitiesName.PA.MeasurementExtensive)
                            {
                                _value = "Cummulative";
                            }
                            else
                            {
                                _value = "Non Cummulative";
                                //_period = _measurementPoint.MeasureDate.ToShortDateString();  //Solo la ultima fecha
                            }
                            _period = _measurementPoint.StartDate.ToShortDateString() + " - " + _measurementPoint.EndDate.ToShortDateString();
                        }
                        else
                        {
                            //como no hay datos, solo pongo el tipo.
                            _value = (_measurement.GetType().Name == Common.ConstantsEntitiesName.PA.MeasurementExtensive) ? "Cummulative" : "Non Cummulative";
                            _period = " - ";
                            _measureValue = 0;
                        }

                        String _deviceName = (_measurement.Device != null) ? _measurement.Device.FullName : String.Empty;
                        String _indicatorName = Common.Functions.ReplaceIndexesTags(_measurement.Indicator.LanguageOption.Name);
                        String _measurementUnit = Common.Functions.ReplaceIndexesTags(_measurement.MeasurementUnit.LanguageOption.Name);

                        _dt.Rows.Add(_measurement.IdMeasurement,
                                _measurement.LanguageOption.Name,
                                _indicatorName,
                                _deviceName,
                                _measureValue,
                                _value,
                                _period,
                                _measurementUnit);
                    }
                }
            }
            return _dt;
        }

        #region Eventos
        void rdgKeyIndicator_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            RadGrid handler = (RadGrid)source;
            handler.DataSource = ReturnDataGridKeyIndicator();

            
        }
        void rdgKeyIndicator_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                String _idMeasurement = ((DataRowView)e.Item.DataItem).Row["IdMeasurement"].ToString();

                ImageButton oimg = (ImageButton)e.Item.FindControl("btnChartLink");
                if (!(oimg == null))
                {
                    oimg.Attributes["onclick"] = string.Format("return ShowChart(event, " + _idMeasurement + ");");
                    oimg.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                }

                ImageButton oimg2 = (ImageButton)e.Item.FindControl("btnSeriesLink");
                if (!(oimg2 == null))
                {
                    oimg2.Attributes["onclick"] = string.Format("return ShowSeries(event,  " + _idMeasurement + ");");
                    oimg2.Attributes["onmouseover"] = "this.style.cursor = 'pointer'";
                }
            }
        }
        protected void rdgKeyIndicator_ItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    ImageButton oimg = (ImageButton)e.Item.FindControl("btnChartLink");
            //    if (!(oimg == null))
            //    {
            //        oimg.Attributes["onclick"] = string.Format("return ShowChart(event, " + e.Item.ItemIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
            //    }

            //    ImageButton oimg2 = (ImageButton)e.Item.FindControl("btnSeriesLink");
            //    if (!(oimg2 == null))
            //    {
            //        oimg2.Attributes["onclick"] = string.Format("return ShowSeries(event, " + e.Item.ItemIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
            //    }
            //}
        }
        #endregion

        #endregion

        #endregion

        #region Events

        protected void PagerPicture_Click(object sender, ImageClickEventArgs e)
        {
            IButtonControl _pagerButton = (IButtonControl)sender;

            //Muevo la Posicion
            Int32 _index = Int32.Parse(hdn_ImagePosition.Value);
            _index += Int32.Parse(_pagerButton.CommandArgument);
            hdn_ImagePosition.Value = _index.ToString();

            //Con el Index Rearmo la Pantalla con la foto nueva
            SetPagerStatus(_index);

            SetImageViewerContent(_index);

            BuildControls();
        }

        #endregion

        #region Chart

        void imgBtnResetChart_Click(object sender, ImageClickEventArgs e)
        {
            LoadChartData(null, null);
            radcalStartDate.SelectedDate = null;
            radcalEndDate.SelectedDate = null;
        }

        void imgBtnRefreshChart_Click(object sender, ImageClickEventArgs e)
        {
            DateTime? _startDate;
            _startDate = radcalStartDate.SelectedDate;

            DateTime? _endDate;
            _endDate = radcalEndDate.SelectedDate;

            LoadChartData(_startDate, _endDate);
        }

        private void LoadChartData(DateTime? startDate, DateTime? endDate)
        {
            Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Calculation> _calculations = _ProcessRoot.AssociatedCalculations;

            if (_calculations.Count > 0)
            {
                Condesus.EMS.Business.PA.Entities.Calculation _calculation = _calculations.First().Value;
                List<Condesus.EMS.Business.PA.Entities.CalculationPoint> _series = _calculation.Series(startDate, endDate);
                EBPAE.CalculationScenarioType _scenarioType = EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(Convert.ToInt64(ConfigurationManager.AppSettings["PDDEstimated"]));

                FeedChart(_series, _ChartSeriesCalculated);
                FeedChart(_calculation.SeriesVerificated(startDate, endDate), _ChartSeriesVerificated);
                if (_scenarioType != null)
                {
                    FeedChart(_calculation.SeriesForecasted(startDate, endDate, _scenarioType), _ChartSeriesForecasted);
                }
            }

            //Si no hay series de datos, no tengo nada para mostrar en en el grafico.
            //if (_series.Count > 0)
            //{
            //    chtIndicator.DataManipulator.InsertEmptyPoints(1, IntervalType.Months, "Calculated, Forecasted, Verificated");
            //}
        }

        private void FeedChart(List<Condesus.EMS.Business.PA.Entities.CalculationPoint> series, Series chartSerie)
        {
            chartSerie.Points.Clear();
            chartSerie.Type = SeriesChartType.Line;
            //Int32 _interval = (Int32)(series.Count / 22);
            //_interval = (_interval == 0) ? 1 : _interval;
            //chtIndicator.ChartAreas["AreaData"].AxisX.MajorTickMark.Interval = _interval;
            //chtIndicator.ChartAreas["AreaData"].AxisX.MajorGrid.Interval = _interval;
            //chtIndicator.ChartAreas["AreaData"].AxisX.LabelStyle.Interval = _interval;
            chtIndicator.ChartAreas["AreaData"].AxisX.LabelsAutoFit = true;
            chtIndicator.ChartAreas["AreaData"].AxisY.Title = "tCO2";


            foreach (Condesus.EMS.Business.PA.Entities.CalculationPoint _point in series)
            {
                chartSerie.Points.AddXY(_point.CalculationDate, Convert.ToDouble(_point.CalculationValue));
            }
        }

        private void LoadBarChartData()
        {
            //Series _serieCalculated = new Series("Calculated");
            //_serieCalculated.Points.AddXY("Calculated", 150);
            //_serieCalculated.Label = "Calculated";
            //_serieCalculated.Type = SeriesChartType.Column;
            //_serieCalculated.Color = System.Drawing.Color.Blue;
            //_serieCalculated.XValueType = ChartValueTypes.String;
            //chtTotals.Series.Add(_serieCalculated);

            //Series _seriePDD = new Series("PDD ERs");
            //_seriePDD.Points.AddXY("PDD ERs", 50);
            //_seriePDD.Label = "PDD ERs";
            //_seriePDD.Type = SeriesChartType.Column;
            //_seriePDD.Color = System.Drawing.Color.Red;
            //_seriePDD.XValueType = ChartValueTypes.String;
            //chtTotals.Series.Add(_seriePDD);

            //Series _serieIssue = new Series("Issue");
            //_serieIssue.Points.AddXY("Issue", 200);
            //_serieIssue.Label = "Issue";
            //_serieIssue.Type = SeriesChartType.Column;
            //_serieIssue.Color = System.Drawing.Color.Green;
            //_serieIssue.XValueType = ChartValueTypes.String;
            //chtTotals.Series.Add(_serieIssue);

            //Este es el total pero para el periodo!!!
            _EstimationTotal = _ProcessRoot.TotalEstimatesByScenario(EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(Convert.ToInt64(ConfigurationManager.AppSettings["PDDEstimated"])));
            _CalculationTotal = _ProcessRoot.FirstCalculationTotalResult();
            foreach (EBPAE.CalculationCertificated _calcCert in _ProcessRoot.AssociatedCalculations.First().Value.CalculationCertificates)
            {
                _CertificatedTotal += _calcCert.Value;
            }

            DataPoint _pointPDD = new DataPoint();
            _pointPDD.YValues = new double[] { Convert.ToDouble(_ProcessRoot.TotalFullEstimatesByScenario(EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(Convert.ToInt64(ConfigurationManager.AppSettings["PDDEstimated"])))) };
            _pointPDD.ShowLabelAsValue = true;
            _pointPDD.Label = "PDD ERs";
            _pointPDD.Color = System.Drawing.Color.Red;

            DataPoint _pointCalculated = new DataPoint();
            _pointCalculated.YValues = new double[] { Convert.ToDouble(_CalculationTotal) };
            _pointCalculated.ShowLabelAsValue = true;
            _pointCalculated.Label = "Calculated";
            _pointCalculated.Color = System.Drawing.Color.Blue;

            DataPoint _pointIssue = new DataPoint();
            _pointIssue.YValues = new double[] { Convert.ToDouble(_CertificatedTotal) };
            _pointIssue.ShowLabelAsValue = true;
            _pointIssue.Label = "Issued";
            _pointIssue.Color = System.Drawing.Color.Green;

            Series _serie = new Series();
            _serie.Type = SeriesChartType.Column;
            _serie.Points.Add(_pointPDD);
            _serie.Points.Add(_pointCalculated);
            _serie.Points.Add(_pointIssue);

            chtTotals.ChartAreas[0].AxisY.Title = "tCO2";
            chtTotals.Series.Add(_serie);
            chtTotals.Legends.Clear();




            //Series _serie = new Series();
            //_serie.Points.AddXY("Calculated", 150);
            //_serie.Points.AddXY("PDD ERs", 50);
            //_serie.Points.AddXY("Issue", 200);
            //_serie.Type = SeriesChartType.Column;
            //_serie.Color = System.Drawing.Color.Blue;
            //_serie.XValueType = ChartValueTypes.String;

            //chtTotals.Series.Add(_serie);

            //chtTotals.Series["Calculated"].Type = SeriesChartType.Column;
            //chtTotals.Series["PDDERs"].Type = SeriesChartType.Column;
            //chtTotals.Series["Issue"].Type = SeriesChartType.Column;

            //chtTotals.Series["Calculated"].Points.AddXY("Calculated",150); //(_CurrentTotal);
            //chtTotals.Series["PDDERs"].Points.AddXY("PDD ERs", 50); //(_EstimationTotal);
            //chtTotals.Series["Issue"].Points.AddXY("Issue", 200); //(_CertificatedTotal);
        }

        private void LoadPerformanceChart()
        {
            Dictionary<String, KeyValuePair<String, Decimal>> _performanceSeries = new Dictionary<String, KeyValuePair<String, Decimal>>();
            EBPAE.CalculationScenarioType _scenarioType = EMSLibrary.User.PerformanceAssessments.Configuration.CalculationScenarioType(Convert.ToInt64(ConfigurationManager.AppSettings["PDDEstimated"]));

            //1° Hay que obtener todas las fechas de certificacion que hay.
            Dictionary<Int64, DateTime[]> _issuedPeriods = _ProcessRoot.GetIssuedPeriods();

            chtTotals.Series["Issued"].Type = SeriesChartType.Column;
            chtTotals.Series["PDD"].Type = SeriesChartType.Column;
            chtTotals.Series["Calculated"].Type = SeriesChartType.Column;
            Decimal _totalIssued = 0;
            Decimal _totalPDD = 0;
            Decimal _totalCalculated = 0;
            //2° Por cada fecha recorro y me quedo con el periodo y el valor.
            foreach (KeyValuePair<Int64, DateTime[]> _period in _issuedPeriods)
            {
                //3° Con esto obtengo el inicio-fin
                DateTime _startDate = _period.Value[0];   //Inicial
                DateTime _endDate = _period.Value[1];     //final

                String _xValue = _startDate.ToShortDateString() + " - " + _endDate.ToShortDateString();
                Decimal _yValueIssued = _ProcessRoot.AssociatedCalculations.First().Value.CalculationCertificated(_period.Key).Value;
                Decimal _yValuePDD = _ProcessRoot.AssociatedCalculations.First().Value.CalculationCertificated(_period.Key).CertificationDeviation(_scenarioType);
                Decimal _yValueCalculated = _ProcessRoot.AssociatedCalculations.First().Value.Calculate(_startDate, _endDate);

                chtTotals.Series["Issued"].Points.AddXY(_xValue, _yValueIssued);
                chtTotals.Series["PDD"].Points.AddXY(_xValue, _yValuePDD);
                chtTotals.Series["Calculated"].Points.AddXY(_xValue, _yValueCalculated);

                _totalIssued = _totalIssued + _yValueIssued;
                _totalPDD = _totalPDD + _yValuePDD;
                _totalCalculated = _totalCalculated + _yValueCalculated;
                ////Por ahora arma el dictionary con el Key indicando el tipo, despues tiene una fecha desde-hasta y el valor.
                ////4° Obtiene el valor del Issued (del periodo indicado)
                //_performanceSeries.Add("Issued", new KeyValuePair<String, Decimal>(_startDate.ToShortDateString() + " - " + _endDate.ToShortDateString(), _ProcessRoot.AssociatedCalculations.First().Value.CalculationCertificated(_period.Key).Value));
                ////5° Obtiene el valor del PDD prorrateado para la fecha del issued.
                //_performanceSeries.Add("PDD", new KeyValuePair<String, Decimal>(_startDate.ToShortDateString() + " - " + _endDate.ToShortDateString(), _ProcessRoot.AssociatedCalculations.First().Value.CalculationCertificated(_period.Key).CertificationDeviation(_scenarioType)));
                ////6° Obtiene el valor del calculo para la fecha del Issued
                //_performanceSeries.Add("Calculated", new KeyValuePair<String, Decimal>(_startDate.ToShortDateString() + " - " + _endDate.ToShortDateString(), _ProcessRoot.AssociatedCalculations.First().Value.Calculate(_startDate, _endDate)));
            }
            chtTotals.Series["Issued"].Points.AddXY("Total", _totalIssued);
            chtTotals.Series["PDD"].Points.AddXY("Total", _totalPDD);
            chtTotals.Series["Calculated"].Points.AddXY("Total", _totalCalculated);

        }

        #endregion

    }
}
