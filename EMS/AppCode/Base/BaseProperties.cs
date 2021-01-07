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
using System.Reflection;
using System.Linq;

using Condesus.EMS.WebUI.MasterControls;
using Condesus.WebUI.Navigation;
 
namespace Condesus.EMS.WebUI
{
    //TODO: Se deberia implementar la generalizacion de delete
    public class BaseProperties : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InyectJavaScript();
            InitializeHandlers();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            //BuildPropertyGeneralOptionsMenu();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);

            //ManageEntityParams = new Dictionary<String, Object>();
        }

        protected virtual void InyectJavaScript()
        {

        }
        protected virtual void InitializeHandlers()
        {

        }

        #region PageTitle

        protected override void SetPagetitle()
        {
            String _pageTitle = GetTitleSectionValueFromLocalResource("PageDescription");//"PageTitle"

            if (_pageTitle != String.Empty)
                base.PageTitle = _pageTitle;
            else
                base.SetPagetitle();
        }

        protected override void SetPageTileSubTitle()
        {
            String _retSubTitle = GetTitleSectionValueFromLocalResource("PageSubtitle");

            if (_retSubTitle == String.Empty)
            {
                _retSubTitle = base.GetTitleSectionValueFromGlobalResource("CommonProperties", "lblSubtitle");
            }

            base.PageTitleSubTitle = _retSubTitle;
        }

        

        #endregion

        

        protected void BuildPropertyGeneralOptionsMenu(Object entity, RadMenuEventHandler menuEventHandler)
        {
            Boolean _isEnabled = (entity != null);
            //Si aun no existe la entidad, no debe poner nada en las opciones generales.
            if (_isEnabled)
            {
                var _itemsMenu = new Dictionary<String, KeyValuePair<String, Boolean>>();
                _itemsMenu.Add("rmiAdd", new KeyValuePair<String, Boolean>(Resources.Common.mnuAdd, _isEnabled));

                if (EntityHasLanguage(entity))
                    _itemsMenu.Add("rmiLanguage", new KeyValuePair<String, Boolean>(Resources.Common.mnuLanguage, _isEnabled));

                _itemsMenu.Add("rmiDelete", new KeyValuePair<String, Boolean>(Resources.Common.mnuDelete, _isEnabled));

                //RadMenu _rmnuGeneralOption = base.BuildGeneralOptionMenu(false, _itemsMenu);
                RadMenu _rmnuGeneralOption = base.BuildGeneralOptionMenu(false, _itemsMenu);
                _rmnuGeneralOption.ItemClick += new RadMenuEventHandler(menuEventHandler);
            }
        }
        //Como el Objeto no implementa una 'Interface', me fijo si tiene Language x Reflection
        private bool EntityHasLanguage(object entity)
        {
            if (entity == null) { return false; }

            Type _type = entity.GetType();
            PropertyInfo _LanguageOptions = _type.GetProperty("LanguagesOptions");

            return (_LanguageOptions != null);
        }


        //TODO: ver si se puede llegar a poner en otra clase!!!
        #region Especializaciones en Properties
        #region Tree View (especificos)
        /// <summary>
        /// Metodo publico que realiza la carga de un TreeView con los proyectos a los que una organizacion tiene participacion.
        /// </summary>
        /// <param name="rtvTreeView">Indica el control del TreeView sobre el cual se realiza la carga</param>
        internal RadTreeView LoadGRCProjectParticipationTreeView(Int64 idOrganization)
        {
            Int32 _pkNode = 0;

            //Arma el TREE
            RadTreeView _rdtvGRCProcessFrameworkByOrganization = new RadTreeView();
            _rdtvGRCProcessFrameworkByOrganization.ID = "rtvMenuGRCProcessFramework";
            _rdtvGRCProcessFrameworkByOrganization.CheckBoxes = false;
            _rdtvGRCProcessFrameworkByOrganization.EnableViewState = true;
            _rdtvGRCProcessFrameworkByOrganization.AllowNodeEditing = false;
            _rdtvGRCProcessFrameworkByOrganization.ShowLineImages = true;
            _rdtvGRCProcessFrameworkByOrganization.Skin = "EMS";
            _rdtvGRCProcessFrameworkByOrganization.EnableEmbeddedSkins = false;

            //Setea el Root de PF
            RadTreeNode _nodePP = new RadTreeNode("Processes Participation", _pkNode++.ToString());
            _nodePP.ToolTip = "Processes Participation";
            _nodePP.PostBack = false;

            //Ahora accede a cuales son los proyectos en los cuales esta participando esta organizacion.
            foreach (Condesus.EMS.Business.PF.Entities.ProcessParticipation _projectParticipation in EMSLibrary.User.DirectoryServices.Map.Organization(idOrganization).ProcessParticipations())
            {
                RadTreeNode _node = new RadTreeNode();
                Int64 _idProcessClassification = _projectParticipation.ProcessGroupProcess.Classifications.First().Value.IdProcessClassification;

                _node.Text = Common.Functions.ReplaceIndexesTags(_projectParticipation.ProcessGroupProcess.LanguageOption.Title + " (" + _projectParticipation.ParticipationType.LanguageOption.Name + ")");
                _node.Value = "IdProcess=" + _projectParticipation.ProcessGroupProcess.IdProcess.ToString() + "&IdProcessClassification=" + _projectParticipation.ProcessGroupProcess.Classifications.First().Value.IdProcessClassification;
                _node.Checkable = false;
                _node.PostBack = true;

                _nodePP.Nodes.Add(_node);
            }
            //Finalmente mete el nodo dentro del TRee.
            _rdtvGRCProcessFrameworkByOrganization.Nodes.Add(_nodePP);

            return _rdtvGRCProcessFrameworkByOrganization;
        }
        #endregion
        #endregion

        protected void ValidatorRequiredField(String entityID, PlaceHolder holder, ref CompareValidator validator, RadComboBox combo, String message)
        {
            holder.Controls.Clear();

            validator = ComboSelectRequiredValidator(entityID, combo.ID, message);

            holder.Controls.Add(validator);
        }
        private CompareValidator ComboSelectRequiredValidator(String entityID, String radComboID, String errorMessage)
        {
            CompareValidator _compareValidator = new CompareValidator();
            _compareValidator.SkinID = "EMS";
            _compareValidator.ID = "cv" + radComboID;
            _compareValidator.Display = ValidatorDisplay.Dynamic;
            _compareValidator.ControlToValidate = radComboID;
            _compareValidator.ValueToCompare = GetComboBoxSelectItemText(entityID);     // Common.Constants.ComboBoxSelectItemDefaultPrefix;
            _compareValidator.Operator = ValidationCompareOperator.NotEqual;
            _compareValidator.ErrorMessage = errorMessage;

            return _compareValidator;
        }

        protected void AddCombo(PlaceHolder holder, ref RadComboBox combo, String entityID,
               String selectedValue, Dictionary<String, Object> paramsEntity, Boolean showAll,
               Boolean showSelectItem, Boolean showNoDependency, Boolean autoPostBack, Boolean autoSize)
        {
            holder.Controls.Clear();

            combo = LoadCombo(entityID, paramsEntity, showAll, showSelectItem, showNoDependency, selectedValue, autoPostBack, autoSize);
            combo.ID = "rdc" + holder.ClientID;
            //Esto es para poder ordenar el combo y ademas le damos la nueva funcionalidad de filtrar al escribir texto.
            combo.Sort = RadComboBoxSort.Ascending;
            combo.Filter = RadComboBoxFilter.Contains;
            combo.SortCaseSensitive = false;
            combo.SortItems();
            //Si muestro el texto de seleccione, lo pongo como seleccionado.
            if (showSelectItem)
                { combo.SelectedValue = Common.Constants.ComboBoxSelectItemValue; }

            holder.Controls.Add(combo);
        }
        protected void AddComboWithTree(PlaceHolder holder, ref RadComboBox combo, ref RadTreeView tree, 
            String entityID, Dictionary<String, Object> paramsEntity, Boolean showAll,
            Boolean showSelectItem, Boolean showNoDependency, ref String filterExpression, RadTreeViewEventHandler eventHandlerNodeExpand, String selectedValueMessage, Boolean autoSize)
        {
            holder.Controls.Clear();

            String _selectedValue = String.Empty;
            if ((combo != null) && (!String.IsNullOrEmpty(combo.Text)))
            { _selectedValue = combo.Text; }
            else
            {
                //Si tiene que mostrar el mensaje Select item..., entonces busca en el resource, sino muestra lo que viene por parametros.
                _selectedValue = showSelectItem == true ? GetComboBoxSelectItemText(entityID) : selectedValueMessage;
                //filterExpression = String.Empty;
            }

            RadTreeView _rtv = new RadTreeView();
            combo = LoadComboWithTree(ref _rtv, eventHandlerNodeExpand, holder, entityID, paramsEntity, showAll, showSelectItem, showNoDependency, _selectedValue, filterExpression, autoSize);
            tree = _rtv;
        }
        protected void AddComboWithTree(PlaceHolder holder, ref RadComboBox combo, ref RadTreeView tree,
            String entityID, Dictionary<String, Object> paramsEntity, Boolean showAll,
            Boolean showSelectItem, Boolean showNoDependency, ref String filterExpression, RadTreeViewEventHandler eventHandlerNodeExpand, RadTreeViewEventHandler eventHandlerNodeClick, String selectedValueMessage, Boolean autoSize)
        {
            holder.Controls.Clear();

            String _selectedValue = String.Empty;
            if ((combo != null) && (!String.IsNullOrEmpty(combo.Text)))
            { _selectedValue = combo.Text; }
            else
            { _selectedValue = showSelectItem == true ? GetComboBoxSelectItemText(entityID) : selectedValueMessage; }

            RadTreeView _rtv = new RadTreeView();
            combo = LoadComboWithTree(ref _rtv, eventHandlerNodeExpand,eventHandlerNodeClick, holder, entityID, paramsEntity, showAll, showSelectItem, showNoDependency, _selectedValue, filterExpression, autoSize);
            tree = _rtv;
        }
        protected void AddComboWithTreeElementMaps(ref PlaceHolder holder, ref RadComboBox combo, ref RadTreeView tree,
            String entityNameMapClassification, String entityNameMapElement, Dictionary<String, Object> paramsEntity, Boolean showAll,
            Boolean showSelectItem, Boolean showNoDependency, ref String filterExpression,
            RadTreeViewEventHandler eventHandlerNodeExpand, RadTreeViewEventHandler eventHandlerNodeClick, String selectedValueMessage, Boolean autoSize)
        {
            holder.Controls.Clear();
            PlaceHolder _phAuxiliar = new PlaceHolder();

            //Arma el texto para mostrar en combo por defecto.
            String _selectedValue = String.Empty;

            //Como se carga un tree con los ElementMaps, se setea este parametro
            if (!paramsEntity.ContainsKey("IsLoadElementMap"))
                { paramsEntity.Add("IsLoadElementMap", true); }

            //Antes lo hacia de esta forma...probar (16-01-09)
            //if (!ManageEntityParams.ContainsKey("IsLoadElementMap"))
            //    { ManageEntityParams.Add("IsLoadElementMap", true); }

            //Esto es para hacer combo con tree de ElementMap....
            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
            BuildGenericDataTable(entityNameMapClassification, paramsEntity);
            BuildGenericDataTable(entityNameMapElement, paramsEntity);
            //Construye el TreeView
            RadTreeView _rtvComboHierarchicalFilter = BuildElementMapsContent(entityNameMapClassification);
            //Asocia el Handler del Expand y click
            _rtvComboHierarchicalFilter.NodeExpand += new RadTreeViewEventHandler(eventHandlerNodeExpand);
            _rtvComboHierarchicalFilter.NodeClick += new RadTreeViewEventHandler(eventHandlerNodeClick);

            //Busca y setea el valor por default que debe mostrar el combo, por ejemplo "Select a item..."
            _selectedValue = GetInitialTextComboWithTreeView(combo, entityNameMapClassification);

            RadTreeNode _nodeRoot = new RadTreeNode(_selectedValue);
            _nodeRoot.Value = "SelectItem=-1";
            //_nodeRoot.PostBack = false;
            _nodeRoot.Expanded = true;
            _nodeRoot.CssClass = String.Empty;
            _rtvComboHierarchicalFilter.Nodes.Add(_nodeRoot);

            //Carga los registros en el Tree
            //base.LoadGenericTreeViewElementMap(ref _rtvComboHierarchicalFilter, entityNameMapClassification, entityNameMapElement, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            base.LoadGenericTreeViewElementMap(ref _nodeRoot, entityNameMapClassification, entityNameMapElement, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false);

            //Contruye el Combo de Filtro.
            combo = BuildComboBoxWithTreeView(_rtvComboHierarchicalFilter, entityNameMapClassification, false, true, false, _selectedValue, _phAuxiliar, autoSize);
            combo.ID = "rdc" + holder.ClientID;

            holder.Controls.Add(combo);

            //Evento Cliente para mostrar el texto en el combo
            _rtvComboHierarchicalFilter.OnClientNodeClicking = "OnClientNodeClicking" + _rtvComboHierarchicalFilter.ClientID;
            //Inyecta el JavaScript del evento cliente para mostrar el texto en el combo
            base.InjectOnClientNodeClicking(combo.ClientID, _rtvComboHierarchicalFilter.ClientID, entityNameMapClassification, _rtvComboHierarchicalFilter.ClientID);

            //RadTreeView _rtv = new RadTreeView();
            //combo = LoadComboWithTree(ref _rtv, eventHandlerNodeExpand, holder, entityID, paramsEntity, showAll, showSelectItem, showNoDependency, _selectedValue, filterExpression);
            tree = _rtvComboHierarchicalFilter;
            
        }

        protected void AddComboWithTreeElementMaps(ref PlaceHolder holder, ref RadComboBox combo, ref RadTreeView tree,
            String entityNameMapClassification, String entityNameMapElement, Dictionary<String, Object> paramsEntity, Boolean showAll,
            Boolean showSelectItem, Boolean showNoDependency, ref String filterExpression,
            RadTreeViewEventHandler eventHandlerNodeExpand, String selectedValueMessage,  Boolean autoSize)
        {
            holder.Controls.Clear();
            PlaceHolder _phAuxiliar = new PlaceHolder();

            //Arma el texto para mostrar en combo por defecto.
            String _selectedValue = String.Empty;

            //Como se carga un tree con los ElementMaps, se setea este parametro
            //Como se carga un tree con los ElementMaps, se setea este parametro
            if (!paramsEntity.ContainsKey("IsLoadElementMap"))
            { paramsEntity.Add("IsLoadElementMap", true); }

            //Antes lo hacia de esta forma...probar (16-01-09)
            //if (!ManageEntityParams.ContainsKey("IsLoadElementMap"))
            //    { ManageEntityParams.Add("IsLoadElementMap", true); }

            
            //Esto es para hacer combo con tree de ElementMap....
            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
            BuildGenericDataTable(entityNameMapClassification, paramsEntity);
            BuildGenericDataTable(entityNameMapElement, paramsEntity);
            //Construye el TreeView
            RadTreeView _rtvComboHierarchicalFilter = BuildElementMapsContent(entityNameMapClassification);
            //Asocia el Handler del Expand y click
            _rtvComboHierarchicalFilter.NodeExpand += new RadTreeViewEventHandler(eventHandlerNodeExpand);
            //_rtvComboHierarchicalFilter.NodeClick += new RadTreeViewEventHandler(eventHandlerNodeClick);

            //Busca y setea el valor por default que debe mostrar el combo, por ejemplo "Select a item..."
            _selectedValue = GetInitialTextComboWithTreeView(combo, entityNameMapClassification);
            
            RadTreeNode _nodeRoot = new RadTreeNode(_selectedValue);
            _nodeRoot.Value = "SelectItem=-1";
            //_nodeRoot.PostBack = false;
            _nodeRoot.Expanded = true;
            _nodeRoot.CssClass = String.Empty;
            _rtvComboHierarchicalFilter.Nodes.Add(_nodeRoot);

            //Carga los registros en el Tree
            //base.LoadGenericTreeViewElementMap(ref _rtvComboHierarchicalFilter, entityNameMapClassification, entityNameMapElement, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty);
            base.LoadGenericTreeViewElementMap(ref _nodeRoot, entityNameMapClassification, entityNameMapElement, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, false);

            //Contruye el Combo de Filtro.
            combo = BuildComboBoxWithTreeView(_rtvComboHierarchicalFilter, entityNameMapClassification, false, true, false, _selectedValue, _phAuxiliar, autoSize);
            combo.ID = "rdc" + holder.ClientID;

            holder.Controls.Add(combo);

            //Evento Cliente para mostrar el texto en el combo
            _rtvComboHierarchicalFilter.OnClientNodeClicking = "OnClientNodeClicking" + _rtvComboHierarchicalFilter.ClientID;
            //Inyecta el JavaScript del evento cliente para mostrar el texto en el combo
            base.InjectOnClientNodeClicking(combo.ClientID, _rtvComboHierarchicalFilter.ClientID, entityNameMapClassification, _rtvComboHierarchicalFilter.ClientID);

            //RadTreeView _rtv = new RadTreeView();
            //combo = LoadComboWithTree(ref _rtv, eventHandlerNodeExpand, holder, entityID, paramsEntity, showAll, showSelectItem, showNoDependency, _selectedValue, filterExpression);
            tree = _rtvComboHierarchicalFilter;
        }

        protected void AddComboTreeSites(ref PlaceHolder holder, ref RadComboBox combo, ref RadTreeView tree,
            RadTreeViewEventHandler eventHandlerNodeExpand)
        {
            holder.Controls.Clear();
            PlaceHolder _phAuxiliar = new PlaceHolder();
            //Arma el texto para mostrar en combo por defecto.
            String _selectedValue = String.Empty;
            Dictionary<String, Object> _paramsEntity = new Dictionary<String, Object>();
            //Como se carga un tree con los ElementMaps, se setea este parametro
            if (!_paramsEntity.ContainsKey("IsLoadElementMap"))
            { _paramsEntity.Add("IsLoadElementMap", true); }

            //Esto es para hacer combo con tree de ElementMap....
            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationClassifications, _paramsEntity);
            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationsRootsWithFacility, _paramsEntity);
            //Construye el TreeView
            RadTreeView _rtvComboHierarchicalFilter = BuildElementMapsContent(Common.ConstantsEntitiesName.DS.OrganizationClassifications);
            //Asocia el Handler del Expand y click
            _rtvComboHierarchicalFilter.NodeExpand += new RadTreeViewEventHandler(eventHandlerNodeExpand);

            //Busca y setea el valor por default que debe mostrar el combo, por ejemplo "Select a item..."
            _selectedValue = GetInitialTextComboWithTreeView(combo, Common.ConstantsEntitiesName.DS.OrganizationClassifications);

            RadTreeNode _nodeRoot = new RadTreeNode(_selectedValue);
            _nodeRoot.Value = "SelectItem=-1";
            //_nodeRoot.PostBack = false;
            _nodeRoot.Expanded = true;
            _nodeRoot.CssClass = String.Empty;
            _rtvComboHierarchicalFilter.Nodes.Add(_nodeRoot);

            //Carga los registros en el Tree
            base.LoadGenericTreeViewElementMap(ref _nodeRoot, Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationsRootsWithFacility, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.Organization, String.Empty, String.Empty, String.Empty, true);

            //Contruye el Combo de Filtro.
            combo = BuildComboBoxWithTreeView(_rtvComboHierarchicalFilter, Common.ConstantsEntitiesName.DS.OrganizationClassifications, false, true, false, _selectedValue, _phAuxiliar, false);

            holder.Controls.Add(combo);
            //Evento Cliente para mostrar el texto en el combo
            _rtvComboHierarchicalFilter.OnClientNodeClicking = "OnClientNodeClicking" + _rtvComboHierarchicalFilter.ClientID;

            //Inyecta el JavaScript del evento cliente para mostrar el texto en el combo
            base.InjectOnClientNodeClicking(combo.ClientID, _rtvComboHierarchicalFilter.ClientID, Common.ConstantsEntitiesName.DS.OrganizationClassifications, _rtvComboHierarchicalFilter.ClientID);

            tree = _rtvComboHierarchicalFilter;
        }

        protected void AddComboTreeSitesByType(ref PlaceHolder holder, ref RadComboBox combo, ref RadTreeView tree, RadTreeViewEventHandler eventHandlerNodeExpand, Int64 idOrganization)
        {
            holder.Controls.Clear();
            PlaceHolder _phAuxiliar = new PlaceHolder();
            //Arma el texto para mostrar en combo por defecto.
            String _selectedValue = String.Empty;
            Dictionary<String, Object> _paramsEntity = new Dictionary<String, Object>();
            //Como se carga un tree con los ElementMaps, se setea este parametro
            if (!_paramsEntity.ContainsKey("IsLoadElementMap"))
            { _paramsEntity.Add("IsLoadElementMap", true); }

            //Si viene el idOrganization entonces lo paso como filtro!
            if (idOrganization != 0)
            {
                _paramsEntity.Add("IdOrganization", idOrganization);
            }

            //Esto es para hacer combo con tree de ElementMap....
            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.FacilityTypes, _paramsEntity);
            //Construye el TreeView
            RadTreeView _rtvComboHierarchicalFilter = BuildElementMapsContent(Common.ConstantsEntitiesName.DS.FacilityTypes);
            _rtvComboHierarchicalFilter.CheckBoxes = true;
            //Asocia el Handler del Expand y click
            _rtvComboHierarchicalFilter.NodeExpand += new RadTreeViewEventHandler(eventHandlerNodeExpand);
            //Evento Cliente para mostrar el texto en el combo
            //_rtvComboHierarchicalFilter.OnClientNodeClicking = "OnClientNodeClicking" + _rtvComboHierarchicalFilter.ClientID;

            //Busca y setea el valor por default que debe mostrar el combo, por ejemplo "Select a item..."
            _selectedValue = GetInitialTextComboWithTreeView(combo, Common.ConstantsEntitiesName.DS.FacilityTypes);

            RadTreeNode _nodeRoot = new RadTreeNode(_selectedValue);
            _nodeRoot.Value = "SelectItem=-1";
            //_nodeRoot.PostBack = false;
            _nodeRoot.Expanded = true;
            _nodeRoot.CssClass = String.Empty;
            _nodeRoot.Checkable = false;
            _rtvComboHierarchicalFilter.Nodes.Add(_nodeRoot);

            //Carga los registros en el Tree
            base.LoadGenericTreeView(ref _rtvComboHierarchicalFilter, Common.ConstantsEntitiesName.DS.FacilityTypes, Common.ConstantsEntitiesName.DS.FacilityType, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty, false, false);
            //base.LoadGenericTreeViewElementMap(ref _nodeRoot, Common.ConstantsEntitiesName.DS.OrganizationClassifications, Common.ConstantsEntitiesName.DS.OrganizationsRootsWithFacility, Common.ConstantsEntitiesName.DS.OrganizationClassification, Common.ConstantsEntitiesName.DS.Organization, String.Empty, String.Empty, String.Empty, true);

            //Contruye el Combo de Filtro.
            combo = BuildComboBoxWithTreeView(_rtvComboHierarchicalFilter, Common.ConstantsEntitiesName.DS.FacilityTypes, false, true, false, _selectedValue, _phAuxiliar, false);

            holder.Controls.Add(combo);

            _rtvComboHierarchicalFilter.OnClientNodeClicking = "OnClientNodeClicking" + _rtvComboHierarchicalFilter.ClientID;

            //Inyecta el JavaScript del evento cliente para mostrar el texto en el combo
            base.InjectOnClientNodeClicking(combo.ClientID, _rtvComboHierarchicalFilter.ClientID, Common.ConstantsEntitiesName.DS.FacilityTypes, _rtvComboHierarchicalFilter.ClientID);

            tree = _rtvComboHierarchicalFilter;
        }
        protected void AddComboTreeConstant(ref PlaceHolder holder, ref RadComboBox combo, ref RadTreeView tree, RadTreeViewEventHandler eventHandlerNodeExpand)
        {
            holder.Controls.Clear();
            PlaceHolder _phAuxiliar = new PlaceHolder();
            //Arma el texto para mostrar en combo por defecto.
            String _selectedValue = String.Empty;
            Dictionary<String, Object> _paramsEntity = new Dictionary<String, Object>();
            //Como se carga un tree con los ElementMaps, se setea este parametro
            if (!_paramsEntity.ContainsKey("IsLoadElementMap"))
            { _paramsEntity.Add("IsLoadElementMap", true); }

            //Esto es para hacer combo con tree de ElementMap....
            //Setea los datos en el DataTable de base, para que luego se carge la grilla.
            BuildGenericDataTable(Common.ConstantsEntitiesName.PA.ConstantClassifications, _paramsEntity);
            //Construye el TreeView
            RadTreeView _rtvComboHierarchicalFilter = BuildElementMapsContent(Common.ConstantsEntitiesName.PA.ConstantClassifications);
            _rtvComboHierarchicalFilter.ID = "rtv" + holder.ClientID;
            //Asocia el Handler del Expand y click
            _rtvComboHierarchicalFilter.NodeExpand += new RadTreeViewEventHandler(eventHandlerNodeExpand);

            //Busca y setea el valor por default que debe mostrar el combo, por ejemplo "Select a item..."
            _selectedValue = GetInitialTextComboWithTreeView(combo, Common.ConstantsEntitiesName.PA.ConstantClassifications);

            RadTreeNode _nodeRoot = new RadTreeNode(_selectedValue);
            _nodeRoot.Value = "SelectItem=-1";
            //_nodeRoot.PostBack = false;
            _nodeRoot.Expanded = true;
            _nodeRoot.CssClass = String.Empty;
            _nodeRoot.Checkable = false;
            _rtvComboHierarchicalFilter.Nodes.Add(_nodeRoot);

            //Carga los registros en el Tree
            base.LoadGenericTreeView(ref _rtvComboHierarchicalFilter, Common.ConstantsEntitiesName.PA.ConstantClassifications, Common.ConstantsEntitiesName.PA.ConstantClassification, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty, false, false);

            //Contruye el Combo de Filtro.
            combo = BuildComboBoxWithTreeView(_rtvComboHierarchicalFilter, Common.ConstantsEntitiesName.PA.ConstantClassifications, false, true, false, _selectedValue, _phAuxiliar, false);
            combo.ID = "rdc" + holder.ClientID;

            holder.Controls.Add(combo);
            //Evento Cliente para mostrar el texto en el combo
            _rtvComboHierarchicalFilter.OnClientNodeClicking = "OnClientNodeClicking" + _rtvComboHierarchicalFilter.ClientID;

            //Inyecta el JavaScript del evento cliente para mostrar el texto en el combo
            base.InjectOnClientNodeClicking(combo.ClientID, _rtvComboHierarchicalFilter.ClientID, Common.ConstantsEntitiesName.PA.ConstantClassifications, _rtvComboHierarchicalFilter.ClientID);

            tree = _rtvComboHierarchicalFilter;
        }

        private RadComboBox LoadCombo(String entityID, Dictionary<String, Object> paramsEntity, Boolean showAll, Boolean showSelectItem, Boolean showNoDependency, String selectedValue, Boolean autoPostBack, Boolean autoSize)
        {
            //Carga el DataTable del combo
            BuildGenericDataTable(entityID, paramsEntity);
            RadComboBox _rdcCombo = BuildComboBox(entityID, showAll, showSelectItem, showNoDependency, selectedValue, autoPostBack, autoSize);

            return _rdcCombo;
        }
        /// <summary>
        /// Este metodo se encarga de Armar un Combo con TreeView y retorna el combo con un tree adentro, y todas sus configuraciones necesarias.
        /// </summary>
        /// <param name="rtvEventHandlerNodeExpand">Evento donde se va a ejecutar el Expand del nodo</param>
        /// <param name="pnlGeneralContent">Panel contenedor que se debe encontrar en la pagina</param>
        /// <param name="entityID">Nombre de la Entidad a cargar los root</param>
        /// <param name="entityParam">Parametros para el acceso a la entidad antes indicada</param>
        /// <param name="showAll">Indica si muestra el mensaje Show All</param>
        /// <param name="showSelectItem">Indica si muestra el mensaje Selecte item</param>
        /// <param name="showNoDependency">Indica si muestra el mensaje No Dependency</param>
        /// <param name="selectedValue">Indica un item para seleccionar</param>
        /// <returns>Un <c>RadComboBox</c></returns>
        private RadComboBox LoadComboWithTree(ref RadTreeView rtvTreeInCombo, RadTreeViewEventHandler rtvEventHandlerNodeExpand, PlaceHolder phGeneralContent, String entityID, Dictionary<String, Object> entityParam, Boolean showAll, Boolean showSelectItem, Boolean showNoDependency, String selectedValue, String filterExpression, Boolean autoSize)
        {
            //Contruye el combo con tree en este metodo interno
            RadComboBox _rdcCombo = BuildComboWithTree(ref rtvTreeInCombo, phGeneralContent, entityID, entityParam, showAll, showSelectItem, showNoDependency, selectedValue, filterExpression, autoSize);

            //Ahora mete los handlers que se pasaron por parametros.
            //Asocia los Handlers del Tree.
            rtvTreeInCombo.NodeExpand += new RadTreeViewEventHandler(rtvEventHandlerNodeExpand);
            
            //Finalmente retorna el combo a quien lo pidio.
            return _rdcCombo;
        }
        /// <summary>
        /// Este metodo se encarga de Armar un Combo con TreeView y retorna el combo con un tree adentro, y todas sus configuraciones necesarias.
        /// Este metodo tiene la posibilidad de agregar el evento NodeClick
        /// </summary>
        /// <param name="rtvEventHandlerNodeExpand">Evento donde se va a ejecutar el Expand del nodo</param>
        /// <param name="rtvEventHandlerNodeClick">Evento donde se va a ejecutar el Click del nodo</param>
        /// <param name="pnlGeneralContent">Panel contenedor que se debe encontrar en la pagina</param>
        /// <param name="entityID">Nombre de la Entidad a cargar los root</param>
        /// <param name="entityParam">Parametros para el acceso a la entidad antes indicada</param>
        /// <param name="showAll">Indica si muestra el mensaje Show All</param>
        /// <param name="showSelectItem">Indica si muestra el mensaje Selecte item</param>
        /// <param name="showNoDependency">Indica si muestra el mensaje No Dependency</param>
        /// <param name="selectedValue">Indica un item para seleccionar</param>
        /// <returns>Un <c>RadComboBox</c></returns>
        private RadComboBox LoadComboWithTree(ref RadTreeView rtvTreeInCombo, RadTreeViewEventHandler rtvEventHandlerNodeExpand, RadTreeViewEventHandler rtvEventHandlerNodeClick, PlaceHolder phGeneralContent, String entityID, Dictionary<String, Object> entityParam, Boolean showAll, Boolean showSelectItem, Boolean showNoDependency, String selectedValue, String filterExpression, Boolean autoSize)
        {
            //Contruye el combo con tree en este metodo interno
            RadComboBox _rdcCombo = BuildComboWithTree(ref rtvTreeInCombo, phGeneralContent, entityID, entityParam, showAll, showSelectItem, showNoDependency, selectedValue, filterExpression, autoSize);

            //Ahora mete los handlers que se pasaron por parametros.
            //Asocia los Handlers del Tree.
            rtvTreeInCombo.NodeExpand += new RadTreeViewEventHandler(rtvEventHandlerNodeExpand);
            rtvTreeInCombo.NodeClick += new RadTreeViewEventHandler(rtvEventHandlerNodeClick);

            //Finalmente retorna el combo a quien lo pidio.
            return _rdcCombo;
        }
        /// <summary>
        /// Metodo privado que se encarga de construir el como con tree y lo retorna para que se le atachen los handlers.
        /// </summary>
        /// <param name="rtvComboWithTree"></param>
        /// <param name="pnlGeneralContent"></param>
        /// <param name="entityID"></param>
        /// <param name="entityParam"></param>
        /// <param name="showAll"></param>
        /// <param name="showSelectItem"></param>
        /// <param name="showNoDependency"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        private RadComboBox BuildComboWithTree(ref RadTreeView rtvComboWithTree, PlaceHolder phGeneralContent, String entityID, Dictionary<String, Object> entityParam, Boolean showAll, Boolean showSelectItem, Boolean showNoDependency, String selectedValue, String filterExpression, Boolean autoSize)
        {
            RadComboBox _rdcCombo;
            PlaceHolder _phComboWithTreeView = new PlaceHolder();
            _phComboWithTreeView.ID = "phComboWithTreeView" + entityID;

            //Aca cargo un combo con TreeView extra en la pagina(combotree generico)
            rtvComboWithTree = BuildHierarchicalInComboContent(entityID);
            //Inserta los valores por defecto dentro del tree (valores del tipo, seleccione un item, sin dependencia, etc.)
            SetInitialTextInTreeView(rtvComboWithTree, entityID, showAll, showSelectItem, showNoDependency);

            //Arma el DataTable con la entidad indicada
            BuildGenericDataTable(entityID, entityParam);

            //Carga los datos en el Tree.
            base.LoadGenericTreeView(ref rtvComboWithTree, entityID, entityID, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, filterExpression, false, false);
            //Contruye el combo con el treeView adentro
            _rdcCombo = BuildComboBoxWithTreeView(rtvComboWithTree, entityID, showAll, showSelectItem, showNoDependency, selectedValue, _phComboWithTreeView, autoSize);
            //Probamos sacarlo!!! y no mejora en nada que este y nos traia problemas...
            //_rdcCombo.OnClientDropDownOpened = "OnClientDropDownOpenedHandler" + rtvComboWithTree.ClientID;

            //Inyecta el combo con tree en el panel de filtro que ya existe en la pagina.
            phGeneralContent.Controls.Add(_rdcCombo);
            //Asocia el Evento de cliente, para mostrar el texto en el combo.
            rtvComboWithTree.OnClientNodeClicking = "OnClientNodeClicking" + rtvComboWithTree.ClientID;

            //Inyecta el javascript para mostrar el texto seleccionado en el combo.
            base.InjectOnClientNodeClicking(_rdcCombo.ClientID, rtvComboWithTree.ClientID, entityID, rtvComboWithTree.ClientID);

            return _rdcCombo;
        }

        /// <summary>
        /// Este metodo publico, permite seleccionar el item parent dentro de un Combo con TreeView.
        /// </summary>
        /// <param name="selectedValue">KeyValues del item al que se desea encontrar su familia</param>
        /// <param name="rtvTreeView">TreeView</param>
        /// <param name="rcbCombo">Combo contenedor del TreeView</param>
        /// <param name="entityID">Nombre entidad que esta representada en el TreeView</param>
        /// <param name="entityChildrenID">Nombre del metodo publico para acceder a los children</param>
        protected void SelectItemTreeViewParent(String selectedValue, ref RadTreeView rtvTreeView, ref RadComboBox rcbCombo, String entityID, String entityChildrenID)
        {
            RadTreeNode _node = null;
            //Busca el nodo que debe quedar seleccionado.
            _node = GetNodeSelected(selectedValue, ref rtvTreeView, entityID, entityChildrenID);

            //Ya tenemos el nodo, ahora setea el texto y el valor en el combo contenedor.
            if (_node != null)
            {
                _node.Selected = true;

                rcbCombo.Items[0].Value = _node.Value;
                rcbCombo.Items[0].Text = Common.Functions.RemoveIndexesTags(_node.Text);
                rcbCombo.Items[0].Selected = true;
            }
        }
        /// <summary>
        /// Este metodo publico, permite seleccionar el item parent dentro de un Combo con TreeView Element Maps.
        /// </summary>
        /// <param name="selectedValue">KeyValues del item al que se desea encontrar su familia</param>
        /// <param name="rtvTreeView">TreeView</param>
        /// <param name="rcbCombo">Combo contenedor del TreeView</param>
        /// <param name="entityID">Nombre entidad que esta representada en el TreeView</param>
        /// <param name="entityChildrenID">Nombre del metodo publico para acceder a los children</param>
        protected void SelectItemTreeViewParentElementMaps(String selectedValueClassification, String selectedValueElement, ref RadTreeView rtvTreeView, ref RadComboBox rcbCombo, String entityID, String entityChildrenID, String entityElementChildrenID)
        {
            RadTreeNode _nodeClass = null;
            RadTreeNode _nodeElement = null;
            Dictionary<String, Object> _parameters = GetKeyValues(selectedValueClassification);

            //Primero trata de obtener el nodo de la Classificacion con el value esperado.
            _nodeClass = rtvTreeView.FindNodeByValue(selectedValueClassification);
            //Si no lo encuentra, entonces tiene que obtener todo el arbol genealogico de la entidad esperada (selectedValue)
            if (_nodeClass == null)
            {
                Stack<String> _parents = new Stack<String>();
                //Obtiene con el factory toda la familia de la entidad esperada.
                _parents = GetFamilyFromChild(entityID + "Family", _parameters);
                //recorre cada uno y va expandiendo el tree, para dejar el padre seleccionado.
                while (_parents.Count > 0)
                {
                    String _parent = _parents.Pop();
                    _nodeClass = rtvTreeView.FindNodeByValue(_parent.ToString());
                    NodeExpand(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_nodeClass), entityChildrenID, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
                }
            }

            //Revisa si encontro al nodo de Classification, para ahora buscar el elemento.
            if (_nodeClass != null)
            {
                //como encontro la clasificacion, debe expandirla para obtener los elementos...
                NodeExpand(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_nodeClass), entityElementChildrenID, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
                //entonces ahora busca el elemento...
                Dictionary<String, Object> _parametersElement = GetKeyValues(selectedValueElement);
                //Primero trata de obtener el nodo de la Classificacion con el value esperado.
                _nodeElement = rtvTreeView.FindNodeByValue(selectedValueElement);
            }
            //Ahora finalmente, vemos si se encontro el nodo del elemento, que es el que realmente se necesita...
            //Ya tenemos el nodo, ahora setea el texto y el valor en el combo contenedor.
            if (_nodeElement != null)
            {
                _nodeElement.Selected = true;

                rcbCombo.Items[0].Value = _nodeElement.Value;
                rcbCombo.Items[0].Text = Common.Functions.RemoveIndexesTags(_nodeElement.Text);
                rcbCombo.Items[0].Selected = true;
            }
        }

        protected Int16 SetProcessOrder(String order)
        {
            if (order != String.Empty)
                return Convert.ToInt16(order);
            else
                return 0;
        }

        #region TreeView Sites
            /// <summary>
            /// Este metodo publico, permite seleccionar el item parent dentro de un Combo con TreeView para los Sites.
            /// </summary>
            /// <param name="selectedValue">KeyValues del item al que se desea encontrar su familia</param>
            /// <param name="rtvTreeView">TreeView</param>
            /// <param name="rcbCombo">Combo contenedor del TreeView</param>
            /// <param name="entityID">Nombre entidad que esta representada en el TreeView</param>
            /// <param name="entityChildrenID">Nombre del metodo publico para acceder a los children</param>
        protected void SelectItemTreeViewParentForSite(String selectedValue, ref RadTreeView rtvTreeView, ref RadComboBox rcbCombo, String entityID, String entityChildrenID, Boolean fullSites)
            {
                RadTreeNode _node = null;
                //Busca el nodo que debe quedar seleccionado.
                _node = GetNodeSelectedForSite(selectedValue, ref rtvTreeView, entityID, entityChildrenID, fullSites);

                //Ya tenemos el nodo, ahora setea el texto y el valor en el combo contenedor.
                if (_node != null)
                {
                    _node.Selected = true;

                    rcbCombo.Items[0].Value = _node.Value;
                    rcbCombo.Items[0].Text = Common.Functions.RemoveIndexesTags(_node.Text);
                    rcbCombo.Items[0].Selected = true;
                }
            }
            /// <summary>
            /// Este metodo privado, permite obtener el Nodo de un Tree Sites para que quede seleccionado.
            /// </summary>
            /// <param name="selectedValue">KeyValues del item al que se desea encontrar su familia</param>
            /// <param name="rtvTreeView">TreeView</param>
            /// <param name="rcbCombo">Combo contenedor del TreeView</param>
            /// <param name="entityID">Nombre entidad que esta representada en el TreeView</param>
            /// <param name="entityChildrenID">Nombre del metodo publico para acceder a los children</param>
            internal RadTreeNode GetNodeSelectedForSite(String selectedValue, ref RadTreeView rtvTreeView, String entityID, String entityChildrenID, Boolean fullSites)
            {
                RadTreeNode _node = null;
                Dictionary<String, Object> _parameters = GetKeyValues(selectedValue);
                _parameters.Add("isFamilyFullSite", "true");

                //Primero trata de obtener el nodo con el value esperado.
                _node = rtvTreeView.FindNodeByValue(selectedValue);
                //Si no lo encuentra, entonces tiene que obtener todo el arbol genealogico de la entidad esperada (selectedValue)
                if (_node == null)
                {
                    Stack<String> _parents = new Stack<String>();
                    //Obtiene con el factory toda la familia de la entidad esperada.
                    _parents = GetFamilyFromChild(entityID + "Family", _parameters);
                    //recorre cada uno y va expandiendo el tree, para dejar el padre seleccionado.
                    while (_parents.Count > 0)
                    {
                        String _parent = _parents.Pop();
                        _node = rtvTreeView.FindNodeByValue(_parent.ToString());
                        //NodeExpand(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_node), entityChildrenID, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
                        NodeExpandSites(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_node), fullSites);    //, entityChildrenID, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
                    }
                }
                return _node;
            }
            /// <summary>
            /// Este metodo publico, permite seleccionar el item parent dentro de un Combo con TreeView para los Sites.
            /// </summary>
            /// <param name="selectedValue">KeyValues del item al que se desea encontrar su familia</param>
            /// <param name="rtvTreeView">TreeView</param>
            /// <param name="rcbCombo">Combo contenedor del TreeView</param>
            /// <param name="entityID">Nombre entidad que esta representada en el TreeView</param>
            /// <param name="entityChildrenID">Nombre del metodo publico para acceder a los children</param>
            protected void SelectItemTreeViewParentElementMapsForSite(String selectedValueClassification, String selectedValueElement, ref RadTreeView rtvTreeView, ref RadComboBox rcbCombo, String entityID, String entityChildrenID, String entityElementChildrenID, Boolean fullSites)
            {
                RadTreeNode _nodeClass = null;
                RadTreeNode _nodeElement = null;
                Dictionary<String, Object> _parameters = GetKeyValues(selectedValueClassification);
                _parameters.Add("isFamilyFullSite", "true");

                //Primero trata de obtener el nodo de la Classificacion con el value esperado.
                _nodeClass = rtvTreeView.FindNodeByValue(selectedValueClassification);
                //Si no lo encuentra, entonces tiene que obtener todo el arbol genealogico de la entidad esperada (selectedValue)
                if (_nodeClass == null)
                {
                    Stack<String> _parents = new Stack<String>();
                    //Obtiene con el factory toda la familia de la entidad esperada.
                    _parents = GetFamilyFromChild(entityID + "Family", _parameters);
                    //recorre cada uno y va expandiendo el tree, para dejar el padre seleccionado.
                    while (_parents.Count > 0)
                    {
                        String _parent = _parents.Pop();
                        _nodeClass = rtvTreeView.FindNodeByValue(_parent.ToString());
                        //NodeExpand(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_nodeClass), entityChildrenID, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
                        NodeExpandSites(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_nodeClass), fullSites);
                    }
                }

                //Revisa si encontro al nodo de Classification, para ahora buscar el elemento.
                if (_nodeClass != null)
                {
                    //como encontro la clasificacion, debe expandirla para obtener los elementos...
                    //NodeExpand(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_nodeClass), entityElementChildrenID, String.Empty, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, false, String.Empty);
                    NodeExpandSites(rtvTreeView, new Telerik.Web.UI.RadTreeNodeEventArgs(_nodeClass), fullSites);
                    //entonces ahora busca el elemento...
                    Dictionary<String, Object> _parametersElement = GetKeyValues(selectedValueElement);
                    //Primero trata de obtener el nodo de la Classificacion con el value esperado.
                    _nodeElement = rtvTreeView.FindNodeByValue(selectedValueElement);
                }
                //Ahora finalmente, vemos si se encontro el nodo del elemento, que es el que realmente se necesita...
                //Ya tenemos el nodo, ahora setea el texto y el valor en el combo contenedor.
                if (_nodeElement != null)
                {
                    _nodeElement.Selected = true;

                    rcbCombo.Items[0].Value = _nodeElement.Value;
                    rcbCombo.Items[0].Text = Common.Functions.RemoveIndexesTags(_nodeElement.Text);
                    rcbCombo.Items[0].Selected = true;
                }
            }
            /// <summary>
            /// Este metodo realiza el Expand del Tree Sites
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            /// <param name="fullSites">Indica si muestra los sectores o no. Si es falso solo muestra hasta los facilities</param>
            public void NodeExpandSites(object sender, RadTreeNodeEventArgs e, Boolean fullSites)
            {
                if (e.Node != null)
                {
                    //Limpio los hijos, para no duplicar al abrir y cerrar.
                    e.Node.Nodes.Clear();
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params = GetKeyValues(e.Node.Value);

                    String _singleEntityName = e.Node.Attributes["SingleEntityName"];
                    switch (_singleEntityName)
                    {
                        case Common.ConstantsEntitiesName.DS.OrganizationClassification:    //esta expandiende una Classificacion!!!
                            //Primero lo hace sobre las Clasificaciones Hijas...
                            //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, _params);
                            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren))
                            {
                                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren].Rows)
                                {
                                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, Common.ConstantsEntitiesName.DS.OrganizationClassification, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                                    e.Node.Nodes.Add(_node);
                                    e.Node.Expanded = true;
                                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                                    SetExpandMode(_node, Common.ConstantsEntitiesName.DS.OrganizationClassificationsChildren, true, false);
                                }
                            }
                            //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.OrganizationsWithFacility, _params);
                            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.OrganizationsWithFacility))
                            {
                                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.OrganizationsWithFacility].Rows)
                                {
                                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.OrganizationsWithFacility, Common.ConstantsEntitiesName.DS.Organization, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                                    //Si esta aca, quiere decir que tiene hijos!
                                    _node.ExpandMode = TreeNodeExpandMode.ServerSide;
                                    e.Node.Nodes.Add(_node);
                                    e.Node.Expanded = true;
                                }
                            }
                            break;

                        case Common.ConstantsEntitiesName.DS.Organization:
                            //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Facilities, _params);
                            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.Facilities))
                            {
                                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.Facilities].Rows)
                                {
                                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Facilities, Common.ConstantsEntitiesName.DS.Facility, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                                    e.Node.Nodes.Add(_node);
                                    e.Node.Expanded = true;
                                    //Si indica que muestra los site completos, quiere decir que debe mostrar todos los sectores.
                                    //Hay casos en los que solo muestra hasta facility, los sectores los oculta...es por eso-
                                    if (fullSites)
                                    {
                                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                                        SetExpandMode(_node, Common.ConstantsEntitiesName.DS.Facilities, true, false);
                                    }
                                }
                            }
                            break;

                        case Common.ConstantsEntitiesName.DS.Facility:
                            //Si indica que muestra los site completos, quiere decir que debe mostrar todos los sectores.
                            //Hay casos en los que solo muestra hasta facility, los sectores los oculta...es por eso-
                            if (fullSites)
                            {
                                //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                                BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Sectors, _params);
                                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.Sectors))
                                {
                                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.Sectors].Rows)
                                    {
                                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Sectors, Common.ConstantsEntitiesName.DS.Sector, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                                        e.Node.Nodes.Add(_node);
                                        e.Node.Expanded = true;
                                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                                        SetExpandMode(_node, Common.ConstantsEntitiesName.DS.SectorsChildren, true, false);
                                    }
                                }
                            }
                            break;

                        case Common.ConstantsEntitiesName.DS.Sector:
                            //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.SectorsChildren, _params);
                            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.SectorsChildren))
                            {
                                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.SectorsChildren].Rows)
                                {
                                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.SectorsChildren, Common.ConstantsEntitiesName.DS.Sector, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                                    e.Node.Nodes.Add(_node);
                                    e.Node.Expanded = true;
                                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                                    SetExpandMode(_node, Common.ConstantsEntitiesName.DS.SectorsChildren, true, false);
                                }
                            }
                            break;
                    }
                }
            }

            /// <summary>
            /// Este metodo realiza el Expand del Tree Sites
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            /// <param name="fullSites">Indica si muestra los sectores o no. Si es falso solo muestra hasta los facilities</param>
            public void NodeExpandSitesByType(object sender, RadTreeNodeEventArgs e, Boolean fullSites, Boolean checkable, Int64 idOrganization)
            {
                if (e.Node != null)
                {
                    //Limpio los hijos, para no duplicar al abrir y cerrar.
                    e.Node.Nodes.Clear();
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params = GetKeyValues(e.Node.Value);
                    if (idOrganization > 0)
                    {
                        if (_params.ContainsKey("IdOrganization"))
                        {
                            _params.Remove("IdOrganization");
                        }
                        _params.Add("IdOrganization", idOrganization);
                    }

                    String _singleEntityName = e.Node.Attributes["SingleEntityName"];
                    switch (_singleEntityName)
                    {
                        case Common.ConstantsEntitiesName.DS.FacilityType:
                            //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Facilities, _params);
                            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.Facilities))
                            {
                                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.Facilities].Rows)
                                {
                                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Facilities, Common.ConstantsEntitiesName.DS.Facility, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                                    _node.Checkable = checkable;
                                    e.Node.Nodes.Add(_node);
                                    e.Node.Expanded = true;
                                    //Si indica que muestra los site completos, quiere decir que debe mostrar todos los sectores.
                                    //Hay casos en los que solo muestra hasta facility, los sectores los oculta...es por eso-
                                    if (fullSites)
                                    {
                                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                                        SetExpandMode(_node, Common.ConstantsEntitiesName.DS.Facilities, true, false);
                                    }
                                }
                            }
                            break;

                        case Common.ConstantsEntitiesName.DS.Facility:
                            //Si indica que muestra los site completos, quiere decir que debe mostrar todos los sectores.
                            //Hay casos en los que solo muestra hasta facility, los sectores los oculta...es por eso-
                            if (fullSites)
                            {
                                //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                                BuildGenericDataTable(Common.ConstantsEntitiesName.DS.Sectors, _params);
                                if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.Sectors))
                                {
                                    foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.Sectors].Rows)
                                    {
                                        RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.Sectors, Common.ConstantsEntitiesName.DS.Sector, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                                        _node.Checkable = checkable;
                                        e.Node.Nodes.Add(_node);
                                        e.Node.Expanded = true;
                                        //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                                        SetExpandMode(_node, Common.ConstantsEntitiesName.DS.SectorsChildren, true, false);
                                    }
                                }
                            }
                            break;

                        case Common.ConstantsEntitiesName.DS.Sector:
                            //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                            BuildGenericDataTable(Common.ConstantsEntitiesName.DS.SectorsChildren, _params);
                            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.DS.SectorsChildren))
                            {
                                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.DS.SectorsChildren].Rows)
                                {
                                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.DS.SectorsChildren, Common.ConstantsEntitiesName.DS.Sector, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                                    _node.Checkable = checkable;
                                    e.Node.Nodes.Add(_node);
                                    e.Node.Expanded = true;
                                    //Como es un Mapa de Elementos, indica true para que cargue los elementos debajo de la classificacion.
                                    SetExpandMode(_node, Common.ConstantsEntitiesName.DS.SectorsChildren, true, false);
                                }
                            }
                            break;
                    }
                }
            }

            /// <summary>
            /// Este metodo realiza el Expand del Tree Sites
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            /// <param name="fullSites">Indica si muestra los sectores o no. Si es falso solo muestra hasta los facilities</param>
            public void NodeExpandConstants(object sender, RadTreeNodeEventArgs e)
            {
                if (e.Node != null)
                {
                    //Limpio los hijos, para no duplicar al abrir y cerrar.
                    e.Node.Nodes.Clear();
                    Dictionary<String, Object> _params = new Dictionary<String, Object>();
                    _params = GetKeyValues(e.Node.Value);

                    String _singleEntityName = e.Node.Attributes["SingleEntityName"];
                    switch (_singleEntityName)
                    {
                        case Common.ConstantsEntitiesName.PA.ConstantClassification:    //esta expandiende una Classificacion!!!
                            //Primero lo hace sobre las Clasificaciones Hijas...
                            //Actualiza el Data table con los hijos, para poder asociarlas al nodo expadido.
                            BuildGenericDataTable(Common.ConstantsEntitiesName.PA.ConstantClassificationChildren, _params);
                            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.PA.ConstantClassificationChildren))
                            {
                                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.PA.ConstantClassificationChildren].Rows)
                                {
                                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.PA.ConstantClassificationChildren, Common.ConstantsEntitiesName.PA.ConstantClassification, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, String.Empty);
                                    e.Node.Nodes.Add(_node);
                                    e.Node.Expanded = true;
                                    SetExpandMode(_node, Common.ConstantsEntitiesName.PA.ConstantClassificationChildren, true, false);
                                }
                            }
                            //Ahora lo hace sobre los Elementos Hijos de la Clasificacion expandida.
                            BuildGenericDataTable(Common.ConstantsEntitiesName.PA.Constants, _params);
                            if (DataTableListManage.ContainsKey(Common.ConstantsEntitiesName.PA.Constants))
                            {
                                foreach (DataRow _drRecord in DataTableListManage[Common.ConstantsEntitiesName.PA.Constants].Rows)
                                {
                                    RadTreeNode _node = SetElementMapsNodeTreeView(_drRecord, Common.ConstantsEntitiesName.PA.Constants, Common.ConstantsEntitiesName.PA.Constant, Condesus.EMS.WebUI.Common.Constants.ExtendedPropertiesColumnDataTable.DisplayCombo, String.Empty, String.Empty, Common.ConstantsEntitiesName.PA.Constant);
                                    _node.CssClass = "Document";
                                    //Si esta aca, quiere decir que tiene hijos!
                                    //_node.ExpandMode = TreeNodeExpandMode.ServerSide;
                                    e.Node.Nodes.Add(_node);
                                    //e.Node.Expanded = true;
                                }
                            }
                            break;
                    }
                }
            }

        #endregion

        #region Navigator
        protected void NavigatePropertyEntity(String url, String actionTitleDecorator, String entityName, String entityPropertyName, NavigateMenuAction menuAction)
            {
                String _entityClassName = base.GetValueFromGlobalResource("CommonListManage", entityName);
                NavigateMenuEventArgs _navArgs = BuildMenuEventArgs(_entityClassName, entityPropertyName, NavigateMenuType.PropertyPageMenu, menuAction);

                String _title = String.Concat(entityPropertyName, " [", _entityClassName, "]", actionTitleDecorator);

                Navigate(url, _title, _navArgs);
            }
            protected void NavigatePropertyEntity(String entityName, String entityPropertyName, NavigateMenuAction menuAction)
            {
                String _actionTitleDecorator = String.Empty;
                if (menuAction == NavigateMenuAction.Add)
                {
                    _actionTitleDecorator = String.Concat(" [", Resources.Common.mnuView, "]");
                    //Solo cuando estoy en Elementos...
                    if (_SelectedModuleSection != "Admin")
                    {
                        //Como viene de un ADD, hay que limpiar el XML del tree global, por las dudas!!!...
                        ValidateClearXMLTreeViewGlobalMenu(entityName);
                    }
                }
                NavigatePropertyEntity(GetPageViewerByEntity(entityName), _actionTitleDecorator, entityName, entityPropertyName, menuAction);
            }
        #endregion
    }
}
