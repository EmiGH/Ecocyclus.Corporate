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
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Drawing;
using Telerik.Charting;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.PM
{
    public partial class BulkLoadMeasurementsProperties : BaseProperties
    {
        #region Internal Properties
            private ConfigurationExcelFile _Entity = null;
            private Int64 _IdExcelFile
            {
                get
                {
                    return base.NavigatorContainsTransferVar("IdExcelFile") ? Convert.ToInt64(base.NavigatorGetTransferVar<Object>("IdExcelFile")) : 0;
                }
            }
            private ConfigurationExcelFile Entity
            {
                get
                {
                    try
                    {
                        if (_Entity == null)
                            _Entity = EMSLibrary.User.PerformanceAssessments.Configuration.ConfigurationExcelFile(_IdExcelFile);

                        return _Entity;
                    }
                    catch { return null; }
                }

                set { _Entity = value; }
            }
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();

                //if ((Entity == null) || (Entity.GetType().Name != Common.ConstantsEntitiesName.PF.ProcessTaskExecutionMeasurement))
                //{
                    //Le paso el Save a la MasterContentToolbar
                    EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                    //esta pagina, tiene un FileUpload, entonces el boton save, tiene que ser PostbackTrigger
                    FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, false);
                //}
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                        { Add(); }
                    else
                        { LoadData(); }//Edit.

                    //Form
                    base.SetContentTableRowsCss(tblContentFormUploadFile);
                }
            }
            protected override void SetPagetitle()
            {
                if (Entity != null)
                {
                    String _title = Entity.Name;
                    base.PageTitle = _title;
                }
                else
                {
                    base.PageTitle = Resources.CommonListManage.BulkLoad;
                }
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.BulkLoad;
                lblFile.Text = Resources.CommonListManage.FileUploadMeasurements;
                lblName.Text = Resources.CommonListManage.Name;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationUploadFile;
                revFileUpload.ErrorMessage = Resources.ConstantMessage.FileTypeNotAllowed;
            }
            private void ShowUploadFile()
            {
                tblContentFormUploadFile.Style.Add("display", "block");
                rfv1.Enabled = true;
            }
            private void Add()
            {
                base.StatusBar.Clear();
            }
            private void LoadData()
            {
                lblNameValue.Text = Entity.Name;
            }
        #endregion

        #region Page Events
            protected void btnSave_Click(object sender, EventArgs e)
            {
                try
                {
                    String _pathBulkLoad = "BulkLoad";
                    if (ConfigurationManager.AppSettings["PathBulkLoad"] != null)
                    {
                        _pathBulkLoad = ConfigurationManager.AppSettings["PathBulkLoad"].ToString();
                    }
                    //Verifica si esta configurado, para enviar notificacion siempre que se ejecuta...
                    Boolean _sendNotificationMeasurementExecution = false;
                    if (ConfigurationManager.AppSettings["SendNotificationMeasurementExecution"] != null)
                    {
                        _sendNotificationMeasurementExecution = Convert.ToBoolean(ConfigurationManager.AppSettings["SendNotificationMeasurementExecution"].ToString());
                    }

                    //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                    CultureInfo _cultureUSA = new CultureInfo("en-US");
                    //Me guarda la actual, para luego volver a esta...
                    CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                    //Seta la cultura estandard
                    Thread.CurrentThread.CurrentCulture = _cultureUSA;

                    //Teniendo el archivo, debemos guardarlo en un directorio del servidor, para que luego la libreria se encargue de leerlo y subir la info a las mediciones...
                    if (fileUploadMeasurement.HasFile)
                    {
                        //Al nombre real del archivo le concatenamos "BL" y la fecha y hora de carga (Ejemplo BL20110811182833_nombreoriginal.xls)
                        String _uniqueName = "BL" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "_";
                        _pathBulkLoad = Request.PhysicalApplicationPath + _pathBulkLoad + "\\";
                        String _fileName = _uniqueName + fileUploadMeasurement.FileName;
                        //Graba el archivo en el servidor
                        fileUploadMeasurement.SaveAs(_pathBulkLoad + _fileName);
                        
                        //Ahora ejecuta el bulk
                        Entity.ExecuteAttach(_pathBulkLoad, _fileName, _sendNotificationMeasurementExecution);
                    }
                    
                    //Vuelve a la cultura original...
                    Thread.CurrentThread.CurrentCulture = _currentCulture;

                    //Volvemos al listado....
                    NavigateBack();

                    //base.NavigatorAddTransferVar("IdExcelFile", Entity.IdExcelFile);
                    //String _pkValues = "IdExcelFile=" + Entity.IdExcelFile.ToString();
                    //base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    //base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.DB.BulkLoad);
                    //base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);

                    //String _entityPropertyName = String.Concat(Entity.Name);
                    //NavigatePropertyEntity(Entity.GetType().Name, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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