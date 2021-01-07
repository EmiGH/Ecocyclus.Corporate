using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace Condesus.EMS.WebUI.Dashboard.Controls
{
    public partial class ProjectSummary : BaseControl
    {
        #region Internal Properties
        #endregion

        #region Page Event
            protected void Page_Init(object source, System.EventArgs e)
            {
                CheckSecurity();
            }
            protected void Page_Load(object sender, EventArgs e)
            {
            }
            #region Master Grid
                protected void rgdMasterGrid_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
                {
                    if (e.Item is GridDataItem)
                    {
                        HtmlImage oimg = (HtmlImage)e.Item.FindControl("rptButton");
                        if (!(oimg == null))
                        {
                            //oimg.Attributes["onclick"] = string.Format("return ShowReport(event, " + e.Item.RowIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
                            oimg.Attributes["onclick"] = string.Format("return ShowReport(event, " + e.Item.ItemIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
                        }

                        HtmlImage oimg2 = (HtmlImage)e.Item.FindControl("selButton");
                        if (!(oimg2 == null))
                        {
                            oimg2.Attributes["onclick"] = string.Format("return ShowMenu(event, " + e.Item.RowIndex + ", '" + e.Item.Parent.Parent.UniqueID + "');");
                        }

                    }
                }
                protected void rgdMasterGrid_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
                {
                    rgdMasterGrid.MasterTableView.Rebind();
                }
                protected void rgdMasterGrid_PageIndexChanged(object source, GridPageChangedEventArgs e)
                {
                    rgdMasterGrid.MasterTableView.Rebind();
                }
                protected void rgdMasterGrid_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
                {
                    rgdMasterGrid.DataSource = LoadProjectSummary();
                }
            #endregion
            #region Menu
                protected void rmnSelection_ItemClick(object sender, Telerik.Web.UI.RadMenuEventArgs e)
                {
                    int radGridClickedRowIndex;
                    string UId;

                    //carga la grilla seleccionada
                    radGridClickedRowIndex = Convert.ToInt32(Request.Form["radGridClickedRowIndex"]);
                    //carga la tabla seleccionada
                    UId = Request.Form["radGridClickedTableId"];

                    GridTableView tableView;
                    //carga la tabla en el GridTableView 
                    tableView = this.Page.FindControl(UId) as GridTableView;
                    //selecciona el ragistro donde se abrio el menu
                    ((tableView.Controls[0] as Table).Rows[radGridClickedRowIndex] as GridItem).Selected = true;

                    switch (e.Item.ID)
                    {
                        case "m0":  //VIEW
                            Context.Items.Add("IdValue", rgdMasterGrid.SelectedValue);
                            //Context.Items.Add("ItemOptionSelected", "VIEW");


                            Int64 _idProcess = Convert.ToInt64(rgdMasterGrid.SelectedValue);
                            Int64 _idProcessClassification = EMSLibrary.User.ProcessFramework.Map.ProcessGroupProcess(_idProcess).Classifications.First().Key;

                            ////Ahora debe ir al Mapa de Procesos.
                            //Context.Items.Add("IdProcess", rgdMasterGrid.SelectedValue);
                            //Context.Items.Add("IdProcessClassification", _idProcessClassification);
                            //Server.Transfer("~/ManagementTools/ProcessesMap/ProcessTest.aspx");

                            String _idProcessTransfer = "IdProcess=" + rgdMasterGrid.SelectedValue.ToString();
                            String _idProcessClassificationTransfer = "IdProcessClassification=" + _idProcessClassification.ToString();
                            Response.Redirect("~/ManagementTools/ProcessesMap/ProcessTest.aspx?" + _idProcessTransfer + '&' + _idProcessClassificationTransfer);

                            //Server.Transfer("~/Dashboard/ProjectDetails.aspx");
                            break;
                    }
                }
            #endregion
        #endregion

        #region Generic Method
            private DataTable LoadProjectSummary()
            {
                DataTable _dt = new DataTable();
                _dt.TableName = "Root";
                _dt.Columns.Add("IdProject");
                _dt.Columns.Add("ProjectTitle");
                _dt.Columns.Add("Category");
                _dt.Columns.Add("Completed");
                _dt.Columns.Add("CampaignStartDate");
                _dt.Columns.Add("State");

                //set primary keys.
                DataColumn[] keys = new DataColumn[1];
                DataColumn column = new DataColumn();
                column = _dt.Columns["IdProject"];
                keys[0] = column;
                _dt.PrimaryKey = keys;

                ////List<Condesus.EMS.Business.PF.Entities.ProcessRole> _projectRoles = EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).Dashboard.ProcessRoles;
                //Int64 _idProyectRoleProjectLeader;
                //Int64.TryParse(ConfigurationManager.AppSettings["IdProjectRoleProjectLeader"], out _idProyectRoleProjectLeader);
                //List<Condesus.EMS.Business.PF.Entities.ProcessRole> _projectRoles = EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).Dashboard.ProcessRoleProcessLeader(_idProyectRoleProjectLeader);
                //foreach (Condesus.EMS.Business.PF.Entities.ProcessRole _projectRole in _projectRoles)
                //{
                //    //Si ya existe en la grilla, no lo agrego...(como son roles, puede cambiar, pero a mi me sirve mostrar un solo proyecto.)
                //    if (!_dt.Rows.Contains(_projectRole.ProcessGroupProcess.IdProcess))
                //    {
                //        String _category = String.Empty;
                //        Condesus.EMS.Business.PF.Entities.ProcessExtendedProperty _extendedProperty;
                //        _extendedProperty = _projectRole.ProcessGroupProcess.ProcessExtendedProperty(Convert.ToInt64(ConfigurationManager.AppSettings["IdExtendedPropertyCategory"]));
                //        if (_extendedProperty != null)
                //        { _category = _extendedProperty.Value.ToString(); }

                //        _dt.Rows.Add(_projectRole.ProcessGroupProcess.IdProcess,
                //                       _projectRole.ProcessGroupProcess.LanguageOption.Title,
                //                       _category,
                //                       _projectRole.ProcessGroupProcess.Completed,
                //                       _projectRole.ProcessGroupProcess.CurrentCampaignStartDate.ToShortDateString(),
                //                       _projectRole.ProcessGroupProcess.State);
                //    }
                //}
                return _dt;
            }
            private void LoadChildrenNodes(DataTable _dt, Condesus.EMS.Business.PF.Entities.ProcessClassification processClassification)
            {
                foreach (Condesus.EMS.Business.PF.Entities.ProcessClassification _processClassification in processClassification.ChildrenClassifications.Values)
                {
                    //Load children nodes
                    LoadChildrenNodes(_dt, _processClassification);
                }
                foreach (Condesus.EMS.Business.PF.Entities.ProcessGroupProcess _processRoot in processClassification.ChildrenElements.Values)
                {
                    _dt.Rows.Add(_processRoot.IdProcess,
                                                _processRoot.LanguageOption.Title,
                                                _processRoot.Completed,
                                                _processRoot.State);
                }
            }
            private void CheckSecurity()
            {
                //Boolean _editItem = EMSLibrary.User.Security.Authorize("DirectoryServices", "Countries", "Modify");
               
                //Menu de Seleccion
                RadMenuItem rmiEdit = new RadMenuItem(Resources.Common.mnuView);
                rmiEdit.Value = "Edit";
                Common.Functions.DoRadItemSecurity(rmiEdit, true);
                rmnSelection.Items.Add(rmiEdit);
            }
        #endregion

    }
}