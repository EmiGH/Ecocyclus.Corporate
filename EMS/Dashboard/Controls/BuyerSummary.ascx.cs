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

namespace Condesus.EMS.WebUI
{
    public partial class BuyerSummary : BaseControl
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
                    rgdMasterGrid.DataSource = LoadBuyerSummary();
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
                            //No se usa mas details
                            Server.Transfer("~/Dashboard/ProjectDetails.aspx");
                            break;
                    }
                }
            #endregion
        #endregion

        #region Generic Method
            private DataTable LoadBuyerSummary()
            {
                DataTable _dt = new DataTable();
                _dt.TableName = "Root";
                _dt.Columns.Add("IdProject");
                _dt.Columns.Add("ProjectTitle");
                _dt.Columns.Add("Estimated");
                _dt.Columns.Add("Actual");
                _dt.Columns.Add("Issued");
                _dt.Columns.Add("Status");

                //Int64 _idProjectRoleBuyer = Convert.ToInt64(ConfigurationSettings.AppSettings["IdProjectRoleBuyer"]);

                ////Te sirven para el detalle....
                //Int64 _idExtendedPropertyStatus = Convert.ToInt64(ConfigurationSettings.AppSettings["IdExtendedPropertyStatus"]);
                //Int64 _idExtendedPropertyLocation = Convert.ToInt64(ConfigurationSettings.AppSettings["IdExtendedPropertyLocation"]);
                //Int64 _idExtendedPropertyERType = Convert.ToInt64(ConfigurationSettings.AppSettings["IdExtendedPropertyERType"]);

                //List<Condesus.EMS.Business.PF.Entities.ProcessRole> _projectRoles = EMSLibrary.User.DirectoryServices.Map.Organization(IdOrganization).Dashboard.ProcessRoleBuyers(_idProjectRoleBuyer);

                //foreach (Condesus.EMS.Business.PF.Entities.ProcessRole _projectRole in _projectRoles)
                //{
                //    //Por cada proyecto, se tiene que buscar el calculo que tiene para mostrar, y poner su valor.
                //    //Tambien se debe sumar los Estimados y los Certificados.
                //    //Por ultimo nos muestra el Status de las Propiedades extendidas.

                //    Decimal _actualResult = _projectRole.ProcessGroupProcess.AssociatedCalculations[4].LastResult;
                //    Decimal _estimatedResult = 15.34M; //_projectRole.ProcessGroupProcess.AssociatedCalculations[4].caLastResult;
                //    Decimal _issuedResult = 3243.32M;

                //    _dt.Rows.Add(_projectRole.ProcessGroupProcess.IdProcess,
                //                   _projectRole.ProcessGroupProcess.LanguageOption.Title,
                //                   _estimatedResult, _actualResult, _issuedResult,
                //                   _projectRole.ProcessGroupProcess.Project.ProcessExtendedProperty(_idExtendedPropertyStatus).Value);

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