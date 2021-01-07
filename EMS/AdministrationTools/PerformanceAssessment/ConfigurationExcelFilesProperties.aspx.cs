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
using Condesus.EMS.Business.PA.Entities;

namespace Condesus.EMS.WebUI.AdministrationTools.PerformanceAssessment
{
    public partial class ConfigurationExcelFilesProperties : BaseProperties
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

            private RadComboBox _RdcMeasurement;
            private Dictionary<String, String> _Operands
            {
                get
                {
                    if (ViewState["Operands"] == null)
                    {
                        ViewState["Operands"] = new Dictionary<String, String>();
                    }
                    return (Dictionary<String, String>)ViewState["Operands"];
                }
                set { ViewState["Operands"] = value; }
            }
            private RadComboBox _RdcSite;
            private RadTreeView _RtvSite;
        #endregion

        #region PageLoad & Init
            protected override void InitializeHandlers()
            {
                base.InitializeHandlers();
                //Le paso el Save a la MasterContentToolbar
                EventHandler _saveEventHandler = new EventHandler(btnSave_Click);
                FwMasterPage.ContentNavigatorToolbarFileActionAdd(_saveEventHandler, MasterFwContentToolbarAction.Save, true);

                btnAddOperand.Click += new EventHandler(btnAddOperand_Click);
                btnRemoveOperand.Click += new EventHandler(btnRemoveOperand_Click);
            }
            protected override void OnInit(EventArgs e)
            {
                base.OnInit(e);
                base.InjectCheckIndexesTags();
                LoadTextLabels();
            }
            protected override void OnLoad(EventArgs e)
            {
                base.OnLoad(e);

                AddComboSites();
                Int64 _idSite = 0;
                if (_RtvSite.SelectedNode != null)
                {
                    if (_RtvSite.SelectedNode.Value.Contains("IdSector"))
                    {
                        _idSite = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdSector"));
                    }
                    else
                    {
                        _idSite = Convert.ToInt64(GetKeyValue(_RtvSite.SelectedNode.Value, "IdFacility"));
                    }
                }
                AddComboMeasurement(_idSite);

                if (!Page.IsPostBack)
                {
                    //Inicializo el Form
                    if (Entity == null)
                    { Add(); }
                    else
                    {
                        //Edit.
                        LoadData();
                    }
                    //Form
                    base.SetContentTableRowsCss(tblContentForm);
                }
            }
            protected override void SetPagetitle()
            {
                base.PageTitle = (Entity != null) ? Entity.Name : Resources.CommonListManage.AccountingActivity;
            }
            protected override void SetPageTileSubTitle()
            {
                base.PageTitleSubTitle = Resources.Common.PageSubtitleProperties;
            }
        #endregion

        #region Private Methods
            private void LoadTextLabels()
            {
                Page.Title = Resources.CommonListManage.ConfigurationExcelFiles;
                lblDatesIndexStart.Text = Resources.CommonListManage.DatesIndexStart;
                lblDatesIndexEnd.Text = Resources.CommonListManage.DatesIndexEnd;
                lblIsDataRows.Text = Resources.CommonListManage.IsDataRows;
                lblMeasurement.Text = Resources.CommonListManage.Measurement;
                lblName.Text = Resources.CommonListManage.Name;
                lblOperand.Text = Resources.CommonListManage.Measurements;
                lblStartIndexOfDataCols.Text = Resources.CommonListManage.StartIndexOfDataCols;
                lblStartIndexOfDataRows.Text = Resources.CommonListManage.StartIndexOfDataRows;
                lblIndexMesurement.Text = Resources.CommonListManage.IndexMeasurement;
                lblIndexMesurementDate.Text = Resources.CommonListManage.IndexMeasurementDate;
                rfv1.ErrorMessage = Resources.ConstantMessage.ValidationRequiredField;
            }
            private void Add()
            {
                base.StatusBar.Clear();
            }
            private void LoadData()
            {
                base.PageTitle = Entity.Name;

                txtName.Text = Entity.Name;
                txtDatesIndexStart.Text = Entity.IndexStartDate;
                txtDatesIndexEnd.Text = Entity.IndexEndDate;
                txtStartIndexOfDataCols.Text = Entity.StartIndexOfDataCols;
                txtStartIndexOfDataRows.Text = Entity.StartIndexOfDataRows;
                chkIsDataRows.Checked = Entity.ValuesInRows;

                LoadParameters();
            }
            private void AddComboMeasurement(Int64 idSite)
            {
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                if (_params.ContainsKey("IdSite"))
                {
                    _params.Remove("IdSite");
                }
                _params.Add("IdSite", idSite);

                AddCombo(phMeasurement, ref _RdcMeasurement, Common.ConstantsEntitiesName.PA.Measurements, String.Empty, _params, false, true, false, false, false);
            }
            private void LoadParameters()
            {
                //Carga el listBox y tambien el dictionary interno
                foreach (KeyValuePair<String, ConfigurationAsociationMeasurementExcelFile> _item in Entity.Measurements)
                {
                    String _idLetter = _item.Value.IndexValue.Trim() + ";" + _item.Value.IndexDate.Trim();  //_item.Key.Trim();
                    String _operandName = _item.Value.Measurement.LanguageOption.Name;
                    String _operand = String.Empty;
                    String _keyValue = String.Empty;
                    RadComboBoxItem _rdcbItem;

                    //Arma el string que muestra el listBox y el KeyValue, para el dictionary interno
                    _operand = _operandName;
                    _keyValue = "IdMeasurement=" + _item.Value.Measurement.IdMeasurement.ToString();
                    _Operands.Add(_idLetter, _keyValue);
                    //Elimina el item del combo medicion, para que no se pueda volver a seleccionar.
                    _rdcbItem = _RdcMeasurement.FindItemByValue(_keyValue);
                    if (_rdcbItem != null)
                    {
                        //Lo encontro, entonces lo borra.
                        _rdcbItem.Remove();
                    }
                
                    //Agrega el parametro al ListBox
                    //lstbOperands.Items.Add(_idLetter + " = " + _operandName);
                    RadListBoxItem _rlbitem = new RadListBoxItem(_idLetter + " = " + _operandName);
                    rdlstbOperands.Items.Add(_rlbitem);
                }
            }
            private void AddComboSites()
            {
                String _filterExpression = String.Empty;
                //Combo de Organizaciones
                Dictionary<String, Object> _params = new Dictionary<String, Object>();
                AddComboTreeSites(ref phSite, ref _RdcSite, ref _RtvSite, new RadTreeViewEventHandler(rtvSite_NodeExpand));
                _RtvSite.NodeClick += new RadTreeViewEventHandler(_RtvSite_NodeClick);
            }
        #endregion

        #region Page Events
            protected void rtvSite_NodeExpand(object sender, RadTreeNodeEventArgs e)
            {
                NodeExpandSites(sender, e, true);
            }
            protected void _RtvSite_NodeClick(object sender, RadTreeNodeEventArgs e)
            {
                Int64 _idSite = 0;
                if (e.Node.Value.Contains("IdSector"))
                {
                    _idSite = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdSector"));
                }
                else
                {
                    _idSite = Convert.ToInt64(GetKeyValue(e.Node.Value, "IdFacility"));
                }

                AddComboMeasurement(_idSite);
            }
            protected void btnAddOperand_Click(object sender, EventArgs e)
            {
                String _operand = String.Empty;

                //Identifica que cual es el source de datos para los operand
                Int64 _idMeasurement = Convert.ToInt64(GetKeyValue(_RdcMeasurement.SelectedValue, "IdMeasurement"));
                _operand = txtIndexMesurement.Text + ";" + txtIndexMesurementDate.Text + " = " + EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(_idMeasurement).LanguageOption.Name;

                _Operands.Add(txtIndexMesurement.Text + ";" + txtIndexMesurementDate.Text, _RdcMeasurement.SelectedValue);
                //Elimina el item del combo, para que no se pueda volver a seleccionar.
                _RdcMeasurement.SelectedItem.Remove();
                _RdcMeasurement.SelectedValue = Common.Constants.ComboBoxSelectItemValue;

                //Agrega la informacion al listBox de la pagina.
                //lstbOperands.Items.Add(_operand);
                RadListBoxItem _rlbitem = new RadListBoxItem(_operand);
                rdlstbOperands.Items.Add(_rlbitem);
            }
            protected void btnRemoveOperand_Click(object sender, EventArgs e)
            {
                //if (lstbOperands.SelectedValue != String.Empty)
                if (rdlstbOperands.SelectedValue !=String.Empty)
                {
                    String _idIndexMeasurement = rdlstbOperands.SelectedValue.Split('=')[0].Trim(); // lstbOperands.SelectedValue.Split('=')[0].Trim();

                    //Agrega la letra al combo, para que pueda volver a ser utilizada.
                    RadComboBoxItem _rcbItem;

                    //Agrega el parametro al combo original
                    String _item = _Operands[_idIndexMeasurement];
                    if (_item.Contains("IdMeasurement"))
                    {
                        //Esta es medicion
                        Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(Convert.ToInt64(GetKeyValue(_item, "IdMeasurement")));
                        //Agrega el item al combo de mediciones
                        _rcbItem = new RadComboBoxItem(_measurement.LanguageOption.Name, _item);
                        _RdcMeasurement.Items.Add(_rcbItem);
                    }
                    //Elimina el item del dictionary Interno.
                    _Operands.Remove(_idIndexMeasurement);
                    //Elimina el item del ListBox.
                    //lstbOperands.Items.Remove(lstbOperands.SelectedItem);
                    rdlstbOperands.Items.Remove(rdlstbOperands.SelectedItem);
                }
            }

            protected void btnSave_Click(object sender, EventArgs e)
            {
                PropertiesSave();
            }
            private void PropertiesSave()
            {
                try
                {
                    Dictionary<String, ConfigurationAsociationMeasurementExcelFile> _asociationMeasurements = new Dictionary<String, ConfigurationAsociationMeasurementExcelFile>();
                    //Aca recorre el ArrayList con los id que fueron chequeado, y contruye un Dictionary, para pasar al ADD.
                    foreach (KeyValuePair<String, String> _item in _Operands)
                    {
                        //Esta es medicion
                        Measurement _measurement = EMSLibrary.User.PerformanceAssessments.Configuration.Measurement(Convert.ToInt64(GetKeyValue(_item.Value, "IdMeasurement")));
                        String _indexMeasurement = _item.Key.Split('=')[0].Split(';')[0];
                        String _indexMeasurementDate = _item.Key.Split('=')[0].Split(';')[1];
                        ConfigurationAsociationMeasurementExcelFile _asociationMeasurement = new ConfigurationAsociationMeasurementExcelFile(_measurement, _indexMeasurement, _indexMeasurementDate);

                        //construye el diccionario de operadores con el key = letra y el operador = medicion,constante o transformacion.
                        _asociationMeasurements.Add(_item.Key, _asociationMeasurement);
                    }

                    String _Name; //el nombre como lo vas a encontrar
                    String _StartIndexOfDataRows;  //donde comienzan a leerse los rows (ej: 4)
                    String _StartIndexOfDataCols;  //donde comienza a leerse las colum (ej: F)
                    Boolean _IsDataRows; //aca true: recoremos columnas (ABCDE…), false recorremos rows (12345…)
                    String _DatesIndexStart; //aca va el row o colum donde esta la fecha inicial (ej: 4) o (ej: G)
                    String _DatesIndexEnd; //aca va el row o colum donde esta la fecha final (ej: 4) o (ej: G)

                    if (Entity == null)
                    {
                        //Alta
                        Entity = EMSLibrary.User.PerformanceAssessments.Configuration.ConfigurationExcelFileAdd(txtName.Text, txtStartIndexOfDataRows.Text, txtStartIndexOfDataCols.Text, chkIsDataRows.Checked, txtDatesIndexStart.Text, txtDatesIndexEnd.Text, _asociationMeasurements);
                    }
                    else
                    {
                        //Modificacion
                        Entity.Modify(txtName.Text, txtStartIndexOfDataRows.Text, txtStartIndexOfDataCols.Text, chkIsDataRows.Checked, txtDatesIndexStart.Text, txtDatesIndexEnd.Text, _asociationMeasurements);
                    }

                    base.NavigatorAddTransferVar("IdExcelFile", Entity.IdExcelFile);
                    String _pkValues = "IdExcelFile=" + Entity.IdExcelFile.ToString();
                    base.NavigatorAddPkEntityIdTransferVar("PkCompost", _pkValues);
                    base.NavigatorAddTransferVar("EntityName", Common.ConstantsEntitiesName.PA.ConfigurationExcelFile);
                    base.NavigatorAddTransferVar("EntityNameContextInfo", String.Empty);

                    String _entityPropertyName = String.Concat(Entity.Name);
                    NavigatePropertyEntity(Entity.GetType().Name, _entityPropertyName, Condesus.WebUI.Navigation.NavigateMenuAction.Add);

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
