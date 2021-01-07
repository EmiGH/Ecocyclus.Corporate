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
using System.Transactions;
using Condesus.EMS.WebUI.MasterControls;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.PF.Entities;
using Condesus.EMS.Business.NT.Entities;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.GIS.Entities;
using System.Globalization;
using System.Threading;
using System.Linq;

namespace Condesus.EMS.WebUI.Wizard
{
    public partial class ExecutionSelectedTask : BasePropertiesTask
    {
        #region Internal Properties
            private RadComboBox _RdcProcess;
            private RadTreeView _RtvTasks;
            private ArrayList _TasksAux //Estructura interna para guardar los id de emails que son seleccionados.
            {
                get
                {
                    if (ViewState["TasksAux"] == null)
                    {
                        ViewState["TasksAux"] = new ArrayList();
                    }
                    return (ArrayList)ViewState["TasksAux"];
                }
                set { ViewState["TasksAux"] = value; }
            }
        #endregion

        #region PageLoad & Init
            protected override void InyectJavaScript()
            {
                base.InjectCheckIndexesTags();
            }
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

                base.InjectCheckIndexesTags();
                AddComboProcess();
                AddTreeViewTasks();

                if (!Page.IsPostBack)
                {
                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = Resources.CommonListManage.ScriptEngine;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = "Ejecutar automático";
            }
            private void AddComboProcess()
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddCombo(phProcess, ref _RdcProcess, Common.ConstantsEntitiesName.PF.ProcessGroupProcessesWithoutClassification, String.Empty, _params, false, true, false, true, false);
                _RdcProcess.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(_RdcProcess_SelectedIndexChanged);
            }
            private void AddTreeViewTasks()
            {
                //Setea los datos en el DataTable de base, para que luego se carge la grilla.
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (_params.ContainsKey("IdProcess"))
                { _params.Remove("IdProcess"); }
                _params.Add("IdProcess", Convert.ToInt64(GetKeyValue(_RdcProcess.SelectedValue, "IdProcess")));

                BuildGenericDataTable(Common.ConstantsEntitiesName.PF.ProcessTaskMeasurementsByOperator, _params);

                //Arma tree con todos los roots.
                phTask.Controls.Clear();
                //Uso un tree, porque es mas comodo y mas lindo visiblemente, pero esta entidad, no tiene jerarquia, ya que el adapter entrega todos los emails y personas plano.
                _RtvTasks = base.BuildHierarchicalListManageContent(Common.ConstantsEntitiesName.PF.ProcessTaskMeasurementsByOperator, "Form");

                LoadDataTasks();
                //Ya tengo el Tree le attacho el Handlers
                //_RtvTasks.NodeCheck += new RadTreeViewEventHandler(_RtvTasks_NodeCheck);
                phTask.Controls.Add(_RtvTasks);
            }
            private void LoadDataTasks()
            {
                _RtvTasks.Nodes.Clear();
                //Con el tree ya armado, ahora hay que llenarlo con datos.
                base.LoadGenericTreeView(ref _RtvTasks, Common.ConstantsEntitiesName.PF.ProcessTaskMeasurementsByOperator, Common.ConstantsEntitiesName.PF.ProcessTaskMeasurementsByOperator, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, true, String.Empty, false, false);
            }
            private void Execution(Int64 idProcess, Int64 idTask, ProcessTaskExecution processTaskExecution, DateTime measurementDate, Double measurementValue, Post post, MeasurementDevice measurementDevice, MeasurementUnit measurementUnit)
            {
                DateTime dateExecution = DateTime.Now;
                //Verifica si esta configurado, para enviar notificacion siempre que se ejecuta...
                Boolean _sendNotificationMeasurementExecution = false;
                if (ConfigurationManager.AppSettings["SendNotificationMeasurementExecution"] != null)
                {
                    _sendNotificationMeasurementExecution = Convert.ToBoolean(ConfigurationManager.AppSettings["SendNotificationMeasurementExecution"].ToString());
                }
                Byte[] _attach = null;

                //COmentado por Ahora, primero lo hacemos en la pagina de ejecucion....
                //((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(idTask)).ProcessTaskExecutionsAdd(post, processTaskExecution, String.Empty,_attach, measurementValue, measurementDate, measurementDevice, measurementUnit, true, _sendNotificationMeasurementExecution);

            }
        #endregion

        #region Page Events
            protected void _RdcProcess_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
            {
                AddTreeViewTasks();
            }
            protected void _RtvTasks_NodeCheck(object sender, RadTreeNodeEventArgs e)
            {
                RadTreeNode _node = e.Node;

                if (_TasksAux.Contains(_node.Value))
                {
                    if (!_node.Checked)
                    {
                        _TasksAux.Remove(_node.Value);
                    }
                }
                else
                {
                    _TasksAux.Add(_node.Value);
                }
            }

            protected void btnSave_Click(object sender, EventArgs e)
            {
                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _cultureUSA = new CultureInfo("en-US");
                //Me guarda la actual, para luego volver a esta...
                CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                //Seta la cultura estandard
                Thread.CurrentThread.CurrentCulture = _cultureUSA;

                try
                {
                    //Se trae el primer post que tiene asociado esta persona. por eso utiliza el "[0]".
                    Post _post = EMSLibrary.User.Person.Posts.First();
                    MeasurementDevice _measurementDevice = null;
                    MeasurementUnit _measurementUnit = null;

                    DateTime _measurementDate = Convert.ToDateTime(rdtMeasurementDate.SelectedDate);
                    Double _measurementValue = Convert.ToDouble(txtMeasurementValue.Text);

                    //Construye el Scope de la transaccion (todo lo que este dentro va en transaccion)
                    using (TransactionScope _transactionScope = new TransactionScope())
                    {
                        for (int i = 0; i < _RtvTasks.Nodes.Count; i++)
                        {
                            if (_RtvTasks.Nodes[i].Checked)
                            { //Como esta chequeado, entonces lo ejecuto!!!
                                String _pkValue = _RtvTasks.Nodes[i].Value;
                                Int64 _idProcess = Convert.ToInt64(GetKeyValue(_pkValue, "IdProcess"));
                                Int64 _idTask = Convert.ToInt64(GetKeyValue(_pkValue, "IdTask"));
                                _measurementDevice = ((ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_idTask)).Measurement.Device;
                                _measurementUnit = ((ProcessTaskMeasurement)EMSLibrary.User.ProcessFramework.Map.Process(_idTask)).Measurement.MeasurementUnit;

                                ProcessTaskExecution _processTaskExecution = ((ProcessTask)EMSLibrary.User.ProcessFramework.Map.Process(_idTask)).ProcessTaskExecutionNow();
                                if (_processTaskExecution != null)
                                {
                                    Execution(_idProcess, _idTask, _processTaskExecution, _measurementDate, _measurementValue, _post, _measurementDevice, _measurementUnit);
                                }
                            }
                        }
                        

                        //Finaliza la transaccion
                        _transactionScope.Complete();
                    }

                    //Vuelve a la cultura original...
                    Thread.CurrentThread.CurrentCulture = _currentCulture;


                    //Todo termino bien...limpio el tree...
                    _RtvTasks.Nodes.Clear();
                    AddTreeViewTasks();
                    LoadDataTasks();

                    base.StatusBar.ShowMessage(Resources.Common.SaveOK);
                }
                catch (Exception ex)
                {
                    base.StatusBar.ShowMessage(ex);
                }

                //Vuelve a la cultura original...
                Thread.CurrentThread.CurrentCulture = _currentCulture;
            }
        #endregion
     
    }
}
