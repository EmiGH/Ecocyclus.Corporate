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
using Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.ManagementTools.PerformanceAssessment
{
    public partial class TransformationSeries : BasePage
    {
        #region Properties
            public RadGrid rgdMasterGrid;
            private Int64 _IdTransformation
            {
                get
                {
                    object o = ViewState["IdTransformation"];
                    if (o != null)
                    {
                        return Convert.ToInt64(ViewState["IdTransformation"]);
                    }
                    else
                    {
                        _IdTransformation = Convert.ToInt64(Request.QueryString["IdTransformation"]);
                        return Convert.ToInt64(Request.QueryString["IdTransformation"]);
                    }
                }
                set { ViewState["IdTransformation"] = value; }
            }
            private Int64 _IdMeasurement
            {
                get
                {
                    object o = ViewState["IdMeasurement"];
                    if (o != null)
                    {
                        return Convert.ToInt64(ViewState["IdMeasurement"]);
                    }
                    else
                    {
                        _IdMeasurement = Convert.ToInt64(Request.QueryString["IdMeasurement"]);
                        return Convert.ToInt64(Request.QueryString["IdMeasurement"]);
                    }
                }
                set { ViewState["IdMeasurement"] = value; }
            }

            private CalculateOfTransformation _CalculateOfTransformation = null;
            private CalculateOfTransformation CalculateOfTransformation
            {
                get
                {
                    try
                    {
                        if (_CalculateOfTransformation == null)
                        {
                            _CalculateOfTransformation = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_IdMeasurement).Transformation(_IdTransformation);
                        }

                        return _CalculateOfTransformation;
                    }
                    catch { return null; }
                }

                set { _CalculateOfTransformation = value; }
            }
        #endregion

        #region Load Information
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
                InitializeGrid();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);
                base.SetContentTableRowsCss(tblContentForm);

                if (!Page.IsPostBack)
                {
                    base.SetNavigator();
                    LoadData();
                }
            }
            private void LoadData()
            {
                this.Title = "EMS - " + Resources.CommonListManage.Transformation;

                lblTransformationValue.Text = Common.Functions.ReplaceIndexesTags(CalculateOfTransformation.LanguageOption.Name);
                lblBaseValue.Text = Common.Functions.ReplaceIndexesTags(CalculateOfTransformation.BaseTransformer.Name);
                lblIndicatorValue.Text = Common.Functions.ReplaceIndexesTags(CalculateOfTransformation.Indicator.LanguageOption.Name);
                lblMeasurementUnitValue.Text = String.Concat(CalculateOfTransformation.MeasurementUnit.Magnitud.LanguageOption.Name, " - ", CalculateOfTransformation.MeasurementUnit.LanguageOption.Name);
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.TransformationSeries;
                lblTitle.Text = Resources.CommonListManage.TransformationSeries;
                lblDetail.Text = Resources.CommonListManage.TransformationSeriesSubTitle;
                lblIndicator.Text = Resources.CommonListManage.Indicator;
                lblBase.Text = Resources.CommonListManage.Base;
                lblMeasurementUnit.Text = Resources.CommonListManage.MeasurementUnit;
            }
            private void InitializeGrid()
            {
                //Crea y setea los atributos de la grilla
                rgdMasterGrid = new RadGrid();
                rgdMasterGrid.ID = "rgdMasterGrid";
                rgdMasterGrid.Skin = "EMS";
                rgdMasterGrid.EnableEmbeddedSkins = false;
                rgdMasterGrid.AllowPaging = true;
                rgdMasterGrid.AllowSorting = true;
                rgdMasterGrid.Width = Unit.Percentage(100);
                rgdMasterGrid.AutoGenerateColumns = false;
                rgdMasterGrid.GridLines = System.Web.UI.WebControls.GridLines.None;
                rgdMasterGrid.ShowStatusBar = false;
                rgdMasterGrid.PageSize = 18;
                rgdMasterGrid.AllowMultiRowSelection = true;
                rgdMasterGrid.PagerStyle.AlwaysVisible = true;
                rgdMasterGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
                rgdMasterGrid.EnableViewState = true;

                //Crea los metodos de la grilla (Server)
                rgdMasterGrid.NeedDataSource += new GridNeedDataSourceEventHandler(this.rgdMasterGrid_NeedDataSource);
                rgdMasterGrid.SortCommand += new GridSortCommandEventHandler(this.rgdMasterGrid_SortCommand);
                rgdMasterGrid.PageIndexChanged += new GridPageChangedEventHandler(this.rgdMasterGrid_PageIndexChanged);

                //Crea los metodos de la grilla (Cliente)
                rgdMasterGrid.ClientSettings.AllowExpandCollapse = true;
                rgdMasterGrid.ClientSettings.Selecting.AllowRowSelect = true;
                rgdMasterGrid.ClientSettings.Selecting.EnableDragToSelectRows = false;

                //Define los atributos de la MasterGrid
                rgdMasterGrid.MasterTableView.Name = "gridMaster";
                rgdMasterGrid.MasterTableView.EnableViewState = true;
                rgdMasterGrid.MasterTableView.CellPadding = 0;
                rgdMasterGrid.MasterTableView.CellSpacing = 0;
                rgdMasterGrid.MasterTableView.GridLines = System.Web.UI.WebControls.GridLines.None;
                rgdMasterGrid.MasterTableView.GroupsDefaultExpanded = false;
                rgdMasterGrid.MasterTableView.AllowMultiColumnSorting = false;
                rgdMasterGrid.MasterTableView.HierarchyLoadMode = GridChildLoadMode.Client;
                rgdMasterGrid.MasterTableView.ExpandCollapseColumn.Resizable = false;
                rgdMasterGrid.MasterTableView.ExpandCollapseColumn.HeaderStyle.Width = Unit.Pixel(20);
                rgdMasterGrid.MasterTableView.RowIndicatorColumn.Visible = false;
                rgdMasterGrid.MasterTableView.RowIndicatorColumn.HeaderStyle.Width = Unit.Pixel(20);

                rgdMasterGrid.HeaderStyle.Font.Bold = false;
                rgdMasterGrid.HeaderStyle.Font.Italic = false;
                rgdMasterGrid.HeaderStyle.Font.Overline = false;
                rgdMasterGrid.HeaderStyle.Font.Strikeout = false;
                rgdMasterGrid.HeaderStyle.Font.Underline = false;
                rgdMasterGrid.HeaderStyle.Wrap = true;
                //Seteos para la paginacion de la grilla, ahora es culturizable.
                rgdMasterGrid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
                rgdMasterGrid.MasterTableView.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;

                rgdMasterGrid.MasterTableView.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip;    // "Next Pages";
                rgdMasterGrid.MasterTableView.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    // "&gt;";
                rgdMasterGrid.MasterTableView.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
                rgdMasterGrid.MasterTableView.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
                rgdMasterGrid.MasterTableView.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
                rgdMasterGrid.MasterTableView.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
                rgdMasterGrid.MasterTableView.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";

                rgdMasterGrid.PagerStyle.NextPagesToolTip = Resources.Common.GridPagerStyleNextPagesToolTip; //"Next Pages";
                rgdMasterGrid.PagerStyle.NextPageText = Resources.Common.GridPagerStyleNextPageText;    //"&gt;";
                rgdMasterGrid.PagerStyle.NextPageToolTip = Resources.Common.GridPagerStyleNextPageToolTip;  // "Next Page";
                rgdMasterGrid.PagerStyle.PagerTextFormat = Resources.Common.GridPagerStylePagerTextFormat;  // "Change page: {4} &nbsp;Displaying page {0} of {1}, items {2} to {3} of {5}.";
                rgdMasterGrid.PagerStyle.PrevPagesToolTip = Resources.Common.GridPagerStylePrevPagesToolTip;    // "Previous Pages";
                rgdMasterGrid.PagerStyle.PrevPageText = Resources.Common.GrdiPagerStylePrevPageText;    // "&lt;";
                rgdMasterGrid.PagerStyle.PrevPageToolTip = Resources.Common.GridPagerStylePrevPageToolTip;  // "Previous Page";


                //Crea las columnas para la MasterGrid.
                DefineColumns(rgdMasterGrid.MasterTableView);
                //Agrega toda la grilla dentro del panle que ya esta en el html.
                pchControls.Controls.Add(rgdMasterGrid);
            }
            private DataTable ReturnDataGrid()
            {
                //Crea el DataTable para luego devolverlo y asociarlo a la grilla.
                DataTable _dt = new DataTable();
                _dt.TableName = "Root";
                _dt.Columns.Add("TransformationDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("TransformationValue", System.Type.GetType("System.Decimal"));
                _dt.Columns.Add("StartDate", System.Type.GetType("System.DateTime"));
                _dt.Columns.Add("EndDate", System.Type.GetType("System.DateTime"));

                List<Condesus.EMS.Business.PA.Entities.MeasurementPoint> _transformationPoints = CalculateOfTransformation.Series();
                foreach (Condesus.EMS.Business.PA.Entities.MeasurementPoint _transformationPoint in _transformationPoints)
                {
                    _dt.Rows.Add(_transformationPoint.MeasureDate, 
                        _transformationPoint.MeasureValue,
                        _transformationPoint.StartDate,
                        _transformationPoint.EndDate);
                }

                return _dt;
            }
            private void DefineColumns(GridTableView gridTableViewDetails)
            {
                //Add columns bound
                GridBoundColumn boundColumn;

                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                //boundColumn.HeaderStyle.Width = Unit.Pixel(150);
                boundColumn.DataField = "TransformationDate";
                boundColumn.DataType = System.Type.GetType("System.DateTime");

                boundColumn.HeaderText = "Date";
                boundColumn.ItemStyle.Width = Unit.Pixel(150);
                gridTableViewDetails.Columns.Add(boundColumn);

                //Crea y agrega las columnas de tipo Bound
                boundColumn = new GridBoundColumn();
                boundColumn.DataField = "TransformationValue";
                boundColumn.DataType = System.Type.GetType("System.Decimal");

                boundColumn.HeaderText = "Value";
                boundColumn.ItemStyle.Width = Unit.Pixel(150);
                gridTableViewDetails.Columns.Add(boundColumn);
            }
        #endregion

        #region RGDMasterGrid
            protected void rgdMasterGrid_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
            {
                //rgdMasterGrid.DataSource = ReturnDataGrid();
                rgdMasterGrid.MasterTableView.Rebind();
            }
            protected void rgdMasterGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
            {
                //rgdMasterGrid.DataSource = ReturnDataGrid();
                rgdMasterGrid.MasterTableView.Rebind();
            }
            protected void rgdMasterGrid_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
            {
                rgdMasterGrid.DataSource = ReturnDataGrid();
            }
        #endregion

    }
}
