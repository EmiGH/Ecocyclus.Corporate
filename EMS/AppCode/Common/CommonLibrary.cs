using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Globalization;
using System.Threading;
using System.Diagnostics;

namespace Condesus.EMS.WebUI.Common
{
    public enum GlobalMenus { Undefined, MTDirectoryMap, MTProcessMap, MTPerformanceMap, MTKnowledgeMap, MTRiskMap, MTImprovementActionMap, ATDirectoryService, ATProcessesMap, ATPerformanceAssessment, ATRiskManagement, ATKnowledgeCollaboration, ATImprovementActions, PTDashboard, PTProfile }

    internal class Functions
    {
        /// <summary>
        /// Esta propiedad nos devuelve el path absoluto del Tema configurado en el sistema.
        /// </summary>
        internal static String GetAbsolutePathAppThemes
        {
            get { return "/App_Themes/" + ((Page)HttpContext.Current.CurrentHandler).Theme; }
        }

        internal static void DoLBSecurity(LinkButton lbButton, Boolean Enabled)
        {
            if (!Enabled)
            {
                lbButton.ToolTip = Resources.Common.ActionNotAllowed;
                lbButton.Enabled = Enabled;
            }
        }
        internal static void DoRadItemSecurity(RadMenuItem radItem, Boolean Enabled)
        {
            if (!Enabled)
            {
                radItem.ToolTip = Resources.Common.ActionNotAllowed;
                radItem.Enabled = Enabled;
            }
        }
        internal static void DoTabItemSecurity(AjaxControlToolkit.TabPanel tp, Boolean Enabled)
        {
            if (!Enabled)
            {
                tp.ToolTip = Resources.Common.ActionNotAllowed;
                tp.Enabled = Enabled;                
            }
        }

        internal static String ReplaceIndexesTags(String name)
        {
            Boolean _cierre = false;
            String _aux;
    
            //Reemplaza subIndices
            int _pos = name.IndexOf("__");
            while (_pos > -1)
            {
                if (_cierre)
                {
                    _aux = "</sub>";
                    _cierre = false;
                }
                else
                {
                    _aux = "<sub>";
                    _cierre = true;
                }
                name = name.Substring(0, _pos) + _aux + name.Substring(_pos+2);
                _pos = name.IndexOf("__");
            }
            
            //Reemplaza super indices 
            _pos = name.IndexOf("--");
            while (_pos > -1)
            {
                if (_cierre)
                {
                    _aux = "</sup>";
                    _cierre = false;
                }
                else
                {
                    _aux = "<sup>";
                    _cierre = true;
                }
                name = name.Substring(0, _pos) + _aux + name.Substring(_pos + 2);
                _pos = name.IndexOf("--");
            }

            return name;
        }
        internal static String RemoveIndexesTags(String name)
        {
            String _name = name;
            _name = _name.Replace("__", "");
            _name = _name.Replace("--", "");
            //Si ya vienen convertidos, tambien lo saco.
            _name = _name.Replace("<sub>", "");
            _name = _name.Replace("</sub>", "");
            _name = _name.Replace("<sup>", "");
            _name = _name.Replace("</sup>", "");
            return _name;
        }
        private static String FormatFormula(String name)
        {
            String _name = name;

           //Greek Letters
            _name = _name.Replace("function", "&fnof;");
            _name = _name.Replace("Alpha", "&Alpha;");
            _name = _name.Replace("Beta", "&Beta;");
            _name = _name.Replace("Gamma", "&Gamma;");
            _name = _name.Replace("Delta", "&Delta;");
            _name = _name.Replace("Epsilon", "&Epsilon;");
            _name = _name.Replace("Zeta", "&Zeta;");
            _name = _name.Replace("Eta", "&Eta;");
            _name = _name.Replace("Theta", "&Theta;");
            _name = _name.Replace("Iota", "&Iota;");
            _name = _name.Replace("Kappa", "&Kappa;");
            _name = _name.Replace("Lambda", "&Lambda;");
            _name = _name.Replace("Mu", "&Mu;");
            _name = _name.Replace("Nu", "&Nu;");
            _name = _name.Replace("Xi", "&Xi;");
            _name = _name.Replace("Omicron", "&Omicron;");
            _name = _name.Replace("Pi", "&Pi;");
            _name = _name.Replace("Rho", "&Rho;");
            _name = _name.Replace("Sigma", "&Sigma;");
            _name = _name.Replace("Tau", "&Tau;");
            _name = _name.Replace("Upsilon", "&Upsilon;");
            _name = _name.Replace("Phi", "&Phi;");
            _name = _name.Replace("Chi", "&Chi;");
            _name = _name.Replace("Psi", "&Psi;");
            _name = _name.Replace("Omega", "&Omega;");
            _name = _name.Replace("alpha", "&alpha;");
            _name = _name.Replace("beta", "&beta;");
            _name = _name.Replace("gamma", "&gamma;");
            _name = _name.Replace("delta", "&delta;");
            _name = _name.Replace("epsilon", "&epsilon;");
            _name = _name.Replace("zeta", "&zeta;");
            _name = _name.Replace("eta", "&eta;");
            _name = _name.Replace("theta", "&theta;");
            _name = _name.Replace("iota", "&iota;");
            _name = _name.Replace("kappa", "&kappa;");
            _name = _name.Replace("lambda", "&lambda;");
            _name = _name.Replace("mu", "&mu;");
            _name = _name.Replace("nu", "&nu;");
            _name = _name.Replace("xi", "&xi;");
            _name = _name.Replace("omicron", "&omicron;");
            _name = _name.Replace("pi", "&pi;");
            _name = _name.Replace("rho", "&rho;");
            _name = _name.Replace("sigmaf", "&sigmaf;");
            _name = _name.Replace("sigma", "&sigma;");
            _name = _name.Replace("tau", "&tau;");
            _name = _name.Replace("upsilon", "&upsilon;");
            _name = _name.Replace("phi", "&phi;");
            _name = _name.Replace("chi", "&chi;");
            _name = _name.Replace("psi", "&psi;");
            _name = _name.Replace("omega", "&omega;");
            _name = _name.Replace("thetasym", "&thetasym;");
            _name = _name.Replace("upsih", "&upsih;");
            _name = _name.Replace("piv", "&piv;");

            //Math & Logical Symbols
            _name = _name.Replace("image", "&image;");
            _name = _name.Replace("real", "&real;");
            _name = _name.Replace("alefsym", "&alefsym;");
            _name = _name.Replace("larr", "&larr;");
            _name = _name.Replace("uarr", "&uarr;");
            _name = _name.Replace("rarr", "&rarr;");
            _name = _name.Replace("darr", "&darr;");
            _name = _name.Replace("harr", "&harr;");
            _name = _name.Replace("lArr", "&lArr;");
            _name = _name.Replace("uArr", "&uArr;");
            _name = _name.Replace("rArr", "&rArr;");
            _name = _name.Replace("dArr", "&dArr;");
            _name = _name.Replace("hArr", "&hArr;");
            _name = _name.Replace("forall", "&forall;");
            _name = _name.Replace("part", "&part;");
            _name = _name.Replace("exist", "&exist;");
            _name = _name.Replace("empty", "&empty;");
            _name = _name.Replace("nabla", "&nabla;");
            _name = _name.Replace("isin", "&isin;");
            _name = _name.Replace("notin", "&notin;");
            _name = _name.Replace("ni", "&ni;");
            _name = _name.Replace("prod", "&prod;");
            _name = _name.Replace("sum", "&sum;");
            _name = _name.Replace("minus", "&minus;");
            _name = _name.Replace("lowast", "&lowast;");
            _name = _name.Replace("radic", "&radic;");
            _name = _name.Replace("prop", "&prop;");
            _name = _name.Replace("infin", "&infin;");
            _name = _name.Replace("ang", "&ang;");
            _name = _name.Replace("and", "&and;");
            _name = _name.Replace("or", "&or;");
            _name = _name.Replace("cap", "&cap;");
            _name = _name.Replace("cup", "&cup;");
            _name = _name.Replace("int", "&int;");
            _name = _name.Replace("there4", "&there4;");
            _name = _name.Replace("sim", "&sim;");
            _name = _name.Replace("cong", "&cong;");
            _name = _name.Replace("asymp", "&asymp;");
            _name = _name.Replace("ne", "&ne;");
            _name = _name.Replace("equiv", "&equiv;");
            _name = _name.Replace("le", "&le;");
            _name = _name.Replace("ge", "&ge;");
            _name = _name.Replace("sub", "&sub;");
            _name = _name.Replace("sup", "&sup;");
            _name = _name.Replace("nsub", "&nsub;");
            _name = _name.Replace("sube", "&sube;");
            _name = _name.Replace("supe", "&supe;");
            _name = _name.Replace("oplus", "&oplus;");
            _name = _name.Replace("otimes", "&otimes;");
            _name = _name.Replace("perp", "&perp;");
            _name = _name.Replace("sdot", "&sdot;");
            _name = _name.Replace("lceil", "&lceil;");
            _name = _name.Replace("rceil", "&rceil;");
            _name = _name.Replace("lfloor", "&lfloor;");
            _name = _name.Replace("rfloor", "&rfloor;");
            _name = _name.Replace("lang", "&lang;");
            _name = _name.Replace("rang", "&rang;");
            _name = _name.Replace("loz", "&loz;");
            _name = _name.Replace("zwnj", "&zwnj;");
            _name = _name.Replace("zwj", "&zwj;");
            _name = _name.Replace("dagger", "&dagger;");
            _name = _name.Replace("Dagger", "&Dagger;");

            return _name;
        } 
        internal static Boolean ValidateIndexesTags(String name)
        {
            Int32 _count=0;

            Int32 _pos = name.IndexOf("__");
            while (_pos > -1)
            {
                _count += 1;
                name = name.Substring(0, _pos) + name.Substring(_pos + 2);
                _pos = name.IndexOf("__");
            }

            _pos = name.IndexOf("--");
            while (_pos > -1)
            {
                _count += 1;
                name = name.Substring(0, _pos) + name.Substring(_pos + 2);
                _pos = name.IndexOf("--");
            }

            return ((_count % 4) == 0);
        }

        /// <summary>
        /// En las paginas que haya que redondear el criterio es el siguiente:
        /// Si la parte entera del numero a redondear es >=1 entonces redondeo con 3 decimales 
        /// Si es < 1' entonces muestro el numero como es 
        /// La idea es que si el numero es 1234.2345678 mostramos 1234.235
        /// y si el numero es 0,0000007 va como es
        /// </summary>
        /// <param name="value">El numero decimal que se quiere formatear</param>
        /// <returns>Un String formateado a 3 decimales con separador de miles la coma o el mismo valor si la parte entera es menor a 1</returns>
        internal static String CustomEMSRound(Decimal value)
        {
            String _valueReturn = String.Empty;
            try
            {
                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _cultureUSA = new CultureInfo("en-US");
                //Me guarda la actual, para luego volver a esta...
                CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                //Seta la cultura estandard
                Thread.CurrentThread.CurrentCulture = _cultureUSA;
                Thread.CurrentThread.CurrentUICulture = _cultureUSA;

                //Si es Cero no hay nada que hacer...
                if (value.ToString() == "0")
                {
                    _valueReturn = value.ToString();
                }
                else
                {
                    //Para cualquier otro numero...hace el formateo y Redondeo
                    if (value >= Convert.ToDecimal(0.01))
                    {
                        _valueReturn = value.ToString("#,##0.00");
                        if (_valueReturn.Substring(_valueReturn.IndexOf(".")) == ".00")
                        {
                            _valueReturn = value.ToString("#,##0");
                        }
                    }
                    else
                    {
                        _valueReturn = value.ToString().TrimEnd('0') == "0." ? "0" : value.ToString().TrimEnd('0');
                    }
                }
                //Vuelve a la cultura original...
                Thread.CurrentThread.CurrentCulture = _currentCulture;
                Thread.CurrentThread.CurrentUICulture = _currentCulture;

            }
            catch (Exception ex)
            {
                String strError = ex.Message.ToString();
                String strSource = ex.Source.ToString();
                String strEventLog = "Condesus EMS Log";

                // Create the event log if it does not exist
                if (!System.Diagnostics.EventLog.SourceExists(strEventLog)) System.Diagnostics.EventLog.CreateEventSource(strSource, strEventLog);
                // Write to the event log
                System.Diagnostics.EventLog objEventLog = new System.Diagnostics.EventLog();
                objEventLog.Source = strSource;
                objEventLog.WriteEntry(strError, System.Diagnostics.EventLogEntryType.Error);
            }
            return _valueReturn;
        }
        internal static String CustomEMSRound(Double value)
        {
            String _valueReturn = String.Empty;

            try
            {
                //Seteo la cultura en USA, para poder trabajar con la fecha en MM/DD/YYYY HH:mm:ss
                CultureInfo _cultureUSA = new CultureInfo("en-US");
                //Me guarda la actual, para luego volver a esta...
                CultureInfo _currentCulture = CultureInfo.CurrentCulture;
                //Seta la cultura estandard
                Thread.CurrentThread.CurrentCulture = _cultureUSA;
                Thread.CurrentThread.CurrentUICulture = _cultureUSA;
                
                //Si es Cero no hay nada que hacer...
                if (value.ToString() == "0")
                {
                    _valueReturn = value.ToString();
                }
                else
                {
                    //Para cualquier otro numero...hace el formateo y Redondeo
                    if ((value >= Convert.ToDouble(0.01)) || (value <= Convert.ToDouble(-0.01)))
                    {
                        _valueReturn = value.ToString("#,##0.00");
                        if (_valueReturn.Substring(_valueReturn.IndexOf(".")) == ".00")
                        {
                            _valueReturn = value.ToString("#,##0");
                        }
                    }
                    else
                    {
                        _valueReturn = value.ToString().TrimEnd('0') == "0." ? "0" : value.ToString().TrimEnd('0');
                    }
                }
                //Vuelve a la cultura original...
                Thread.CurrentThread.CurrentCulture = _currentCulture;
                Thread.CurrentThread.CurrentUICulture = _currentCulture;

            }
            catch (Exception ex)
            {
                String strError = ex.Message.ToString();
                String strSource = ex.Source.ToString();
                String strEventLog = "Condesus EMS Log";

                // Create the event log if it does not exist
                if (!System.Diagnostics.EventLog.SourceExists(strEventLog)) System.Diagnostics.EventLog.CreateEventSource(strSource, strEventLog);
                // Write to the event log
                System.Diagnostics.EventLog objEventLog = new System.Diagnostics.EventLog();
                objEventLog.Source = strSource;
                objEventLog.WriteEntry(strError, System.Diagnostics.EventLogEntryType.Error);
            }
            
            return _valueReturn;
        }

        private static String ExcelColumnLetter(int intCol)
        {
            if (intCol > 16384)
            {
                throw new Exception("Index exceeds maximum columns allowed.");
            }
            string strColumn;
            char letter1, letter2, FirstLetter;
            int InitialLetter = ((intCol) / 676);
            int intFirstLetter = ((intCol % 676) / 26);
            int intSecondLetter = (intCol % 26);
            InitialLetter = InitialLetter + 64;
            intFirstLetter = intFirstLetter + 65;
            intSecondLetter = intSecondLetter + 65;

            if (InitialLetter > 64)
            {
                FirstLetter = (char)InitialLetter;
            }
            else
            {
                FirstLetter = ' ';
            }

            if (intFirstLetter > 64)
            {
                letter1 = (char)intFirstLetter;
            }
            else
            {
                letter1 = ' ';
            }
            letter2 = (char)intSecondLetter;
            strColumn = FirstLetter + string.Concat(letter1, letter2);
            return strColumn.Trim();
        }
        internal static String ExcelColumnLetterFull(int intCol)
        {
            string _colum = "";

            if (intCol < 26)
            {
                int _numcol = intCol + 65;
                _colum = Char.ConvertFromUtf32(_numcol);
            }
            else
            {
                int _numcol = intCol - 26;
                _colum = ExcelColumnLetter(_numcol);
            }

            return _colum;
        }

        internal static DateTime CalculateNextDate(Int64 timeUnitFrequency, Int32 frequency, DateTime date)
        {
            DateTime _nextDate = DateTime.MinValue;

            //ahora calcula la proxima fecha, con todos los datos obtenidos.
            switch (timeUnitFrequency)
            {
                case 1: //Por año
                    _nextDate = date.AddYears(frequency);
                    break;

                case 2: //Por mes
                    _nextDate = date.AddMonths(frequency);
                    break;

                case 3: //Por dia
                    _nextDate = date.AddDays(frequency);
                    break;

                case 5: //por Hora
                    _nextDate = date.AddHours(frequency);
                    break;

                case 6: //por minuto
                    _nextDate = date.AddMinutes(frequency);
                    break;

                case 7: //por segundo
                    _nextDate = date.AddSeconds(frequency);
                    break;
            }

            return _nextDate;
        }
    }
    
    #region Navigator

    internal class Navigator
    {
        #region Internal Properties

        private List<Location> _History;
        private Location _DefaultLocation;

        #endregion

    
        internal Navigator()
        {
            _DefaultLocation = new Location((String)ConfigurationSettings.AppSettings["DefaultPage"]);
        }

        #region External Methods

        internal void Back()
        {
            if (_History != null)
            {
                _History.RemoveAt(_History.Count - 1);
            }
        }
        internal Location Current
        {
            get
            {
                if (_History != null)
                {
                    return _History[_History.Count - 1];
                }
                return _DefaultLocation;
            }
            set
            {
                //Si esta vacia la pila, la creo
                if (_History == null)
                {
                    _History = new List<Location>();
                }

                //Si el url es el mismo entonces seguro estoy volviendo 
                //a una pagina anterior asi que reemplazo la entrada en la pila
                if (_History.Count > 0)
                {
                    if (_History[_History.Count-1].Url == value.Url)
                    {
                        _History[_History.Count-1] = value;
                        return;
                    }
                }
                _History.Add(value);
            }
        }
        internal Location Previous
        {
            get
            {
                if (_History != null)
                {
                    if (_History.Count > 1)
                    {
                        return _History[_History.Count-2];    
                    }
                }
                return _DefaultLocation;
            }
        }
        
        #endregion
    }
    internal class Location
    {
        #region Internal Properties

        private String _Url;
        private IDictionary _Items;

        #endregion

        #region External Properties

        internal String Url
        {
            get { return _Url; }
        }
        public IDictionary Items
        {
            get { return _Items; }
        }

        #endregion

        #region Public Methods

        public void Add(DictionaryEntry value)
        {
            Add(value.Key, value.Value);
        }
        public void Add(Object key, Object value)
        {
            if (!_Items.Contains(key))
            {
                _Items.Add(key, value);
            }
        }

        #endregion

        internal Location(String Url)
        {
            _Url = Url;
            _Items = new Dictionary<Object, Object>();
        }
    }

    #endregion

    #region Constants
        internal class Constants
        {
            internal const Int16 PermissionViewKey = 1;
            internal const Int16 PermissionManageKey = 2;
            internal const String PermissionViewName = "View";
            internal const String PermissionManageName = "Manage";

            internal const Int64 ContactTypeAddress = 0;
            internal const Int64 ContactTypeTelephone = 1;
            internal const Int64 ContactTypeEmail = 2;
            internal const Int64 ContactTypeUrl = 3;
            internal const Int64 ContactTypeMessenger = 4;

            internal const String InitializeTabAjax = "InitializeTabAjax";
            internal const String InitializePostBackAjax = "InitializePostBackAjax";
            internal const String InitializeBCCJSGlobalVars = "InitializeBCCJSGlobalVars";

            internal const String ComboBoxSelectItemDefaultPrefix = "Select a Item...";
            internal const String ComboBoxSelectItemPrefix = "comboboxselectitem";
            internal const String SubFixMethodHierarchicalChildren = "Children";
            internal const String SubFixMethodHierarchicalHasChildren = "HasChildren";
            
            internal const String ComboBoxNoDependencyValue = "NoDependency=0";
            internal const String ComboBoxSelectItemValue = "SelectItem=-1";
            internal const String ComboBoxShowAllValue = "ShowAll=-2";

            internal const String ContextInformationKey = "ContextInfo";
            internal const String ContextElementMapsKey = "ContextElementMaps";

            internal static KeyValuePair<String, Object> ComboBoxNoDependencyKeyValue = new KeyValuePair<String, Object>("NoDependency", "0");
            internal static KeyValuePair<String, Object> ComboBoxSelectItemKeyValue = new KeyValuePair<String, Object>("SelectItem", "-1");
            internal static KeyValuePair<String, Object> ComboBoxShowAllKeyValue = new KeyValuePair<String, Object>("ShowAll", "-2");

            internal const String MessageDuplicateKeyContent = "Cannot insert duplicate key row in object";

            //Para identificar el tipo de operacion que se realiza en la exceptionproperties.
            internal const String ExceptionStateCloseName = "ExceptionStateClose";
            internal const String ExceptionStateCreateName = "ExceptionStateCreate";
            internal const String ExceptionStateTreatName = "ExceptionStateTreat";

            internal const Int64 idPowerPlant = 1;
            internal const Int64 idShopsAndServices = 2;
            internal const Int64 idHomes = 3;
            internal const Int64 idRefineries = 4;
            internal const Int64 idServiceStations = 5;
            internal const Int64 idWaterTreatmentPlants = 7;
            internal const Int64 idIndustries = 8;
            internal const Int64 idFarms = 9;
            internal const Int64 idWasteTreatmentPlants = 10;
            internal const Int64 idLandfill = 11;
            internal const Int64 idLand = 12;

            internal const Int64 idOffice = 13;
            internal const Int64 idUnspecified = 14;
            internal const Int64 idOilPipeline = 15;
            internal const Int64 idGasPipeline = 16;
            internal const Int64 idBatteries = 17;
            internal const Int64 idMotorCompressorStation = 19;
            internal const Int64 idOilTreatmentPlant = 20;
            internal const Int64 idConditioningPlantDewpoint = 21;
            internal const Int64 idSeparationPlantOfLiquefiedGases = 22;
            internal const Int64 idSaltWaterInjectionPlant = 23;
            internal const Int64 idFreshWaterInjectionPlant = 24;
            internal const Int64 idFreshWaterTransferPlant = 25;
            internal const Int64 idThermalPowerPlant = 27;
            internal const Int64 idOilWell = 28;
            internal const Int64 idFleetVehicles = 29;
            internal const Int64 idGlobal = 30;

            //Nuevos
            internal const Int64 idMining = 75;
            internal const Int64 idServices = 77;

            internal const Int64 idRestaurant = 83;
            internal const Int64 idFastfood = 84;

            public enum ExtendedPropertiesColumnDataTable
            {
                DisplayManage = 0,
                DisplayCombo,
                IsSearchable,
                IsContextMenuCaption,
                IsCellLink,
                EntityName,
                EntityNameGrid,
                EntityNameContextInfo,
                EntityNameContextElement,
                IsSortedBy,
                IsBinaryImage,
                SortOrderType
            };
        }
        internal class ConstantsEntitiesName
        {
            internal class SS
            {
                internal const String RightJobTitleOrganization = "RightJobTitleOrganization";
                internal const String RightJobTitleOrganizations = "RightJobTitleOrganizations";
                internal const String RightPersonOrganization = "RightPersonOrganization";
                internal const String RightPersonOrganizations = "RightPersonOrganizations";
                internal const String RightJobTitleOrganizationClassification = "RightJobTitleOrganizationClassification";
                internal const String RightJobTitleOrganizationClassifications = "RightJobTitleOrganizationClassifications";
                internal const String RightPersonOrganizationClassification = "RightPersonOrganizationClassification";
                internal const String RightPersonOrganizationClassifications = "RightPersonOrganizationClassifications";
                internal const String RightJobTitleProcessClassification = "RightJobTitleProcessClassification";
                internal const String RightJobTitleProcessClassifications = "RightJobTitleProcessClassifications";
                internal const String RightPersonProcessClassification = "RightPersonProcessClassification";
                internal const String RightPersonProcessClassifications = "RightPersonProcessClassifications";
                internal const String RightJobTitleProcess = "RightJobTitleProcess";
                internal const String RightJobTitleProcesses = "RightJobTitleProcesses";
                internal const String RightPersonProcess = "RightPersonProcess";
                internal const String RightPersonProcesses = "RightPersonProcesses";
                internal const String RightJobTitleIndicatorClassification = "RightJobTitleIndicatorClassification";
                internal const String RightJobTitleIndicatorClassifications = "RightJobTitleIndicatorClassifications";
                internal const String RightPersonIndicatorClassification = "RightPersonIndicatorClassification";
                internal const String RightPersonIndicatorClassifications = "RightPersonIndicatorClassifications";
                internal const String RightJobTitleIndicator = "RightJobTitleIndicator";
                internal const String RightJobTitleIndicators = "RightJobTitleIndicators";
                internal const String RightPersonIndicator = "RightPersonIndicator";
                internal const String RightPersonIndicators = "RightPersonIndicators";
                internal const String RightJobTitleResourceClassification = "RightJobTitleResourceClassification";
                internal const String RightJobTitleResourceClassifications = "RightJobTitleResourceClassifications";
                internal const String RightPersonResourceClassification = "RightPersonResourceClassification";
                internal const String RightPersonResourceClassifications = "RightPersonResourceClassifications";
                internal const String RightJobTitleResource = "RightJobTitleResource";
                internal const String RightJobTitleResources = "RightJobTitleResources";
                internal const String RightPersonResource = "RightPersonResource";
                internal const String RightPersonResources = "RightPersonResources";
                internal const String RightJobTitleProjectClassification = "RightJobTitleProjectClassification";
                internal const String RightJobTitleProjectClassifications = "RightJobTitleProjectClassifications";
                internal const String RightPersonProjectClassification = "RightPersonProjectClassification";
                internal const String RightPersonProjectClassifications = "RightPersonProjectClassifications";
                internal const String RightJobTitleRiskClassification = "RightJobTitleRiskClassification";
                internal const String RightJobTitleRiskClassifications = "RightJobTitleRiskClassifications";
                internal const String RightPersonRiskClassification = "RightPersonRiskClassification";
                internal const String RightPersonRiskClassifications = "RightPersonRiskClassifications";

                internal const String RightPersonMapsDS = "RightPersonMapsDS";
                internal const String RightPersonMapDS = "RightPersonMapDS";
                internal const String RightJobTitleMapsDS = "RightJobTitleMapsDS";
                internal const String RightJobTitleMapDS = "RightJobTitleMapDS";
                internal const String RightPersonMapsPA = "RightPersonMapsPA";
                internal const String RightPersonMapPA = "RightPersonMapPA";
                internal const String RightJobTitleMapsPA = "RightJobTitleMapsPA";
                internal const String RightJobTitleMapPA = "RightJobTitleMapPA";
                internal const String RightPersonMapsIA = "RightPersonMapsIA";
                internal const String RightPersonMapIA = "RightPersonMapIA";
                internal const String RightJobTitleMapsIA = "RightJobTitleMapsIA";
                internal const String RightJobTitleMapIA = "RightJobTitleMapIA";
                internal const String RightPersonMapsKC = "RightPersonMapsKC";
                internal const String RightPersonMapKC = "RightPersonMapKC";
                internal const String RightJobTitleMapsKC = "RightJobTitleMapsKC";
                internal const String RightJobTitleMapKC = "RightJobTitleMapKC";
                internal const String RightPersonMapsPF = "RightPersonMapsPF";
                internal const String RightPersonMapPF = "RightPersonMapPF";
                internal const String RightJobTitleMapsPF = "RightJobTitleMapsPF";
                internal const String RightJobTitleMapPF = "RightJobTitleMapPF";
                internal const String RightPersonMapsRM = "RightPersonMapsRM";
                internal const String RightPersonMapRM = "RightPersonMapRM";
                internal const String RightJobTitleMapsRM = "RightJobTitleMapsRM";
                internal const String RightJobTitleMapRM = "RightJobTitleMapRM";

                internal const String RightPersonConfigurationsDS = "RightPersonConfigurationsDS";
                internal const String RightPersonConfigurationDS = "RightPersonConfigurationDS";
                internal const String RightJobTitleConfigurationsDS = "RightJobTitleConfigurationsDS";
                internal const String RightJobTitleConfigurationDS = "RightJobTitleConfigurationDS";
                internal const String RightPersonConfigurationsPA = "RightPersonConfigurationsPA";
                internal const String RightPersonConfigurationPA = "RightPersonConfigurationPA";
                internal const String RightJobTitleConfigurationsPA = "RightJobTitleConfigurationsPA";
                internal const String RightJobTitleConfigurationPA = "RightJobTitleConfigurationPA";
                internal const String RightPersonConfigurationsIA = "RightPersonConfigurationsIA";
                internal const String RightPersonConfigurationIA = "RightPersonConfigurationIA";
                internal const String RightJobTitleConfigurationsIA = "RightJobTitleConfigurationsIA";
                internal const String RightJobTitleConfigurationIA = "RightJobTitleConfigurationIA";
                internal const String RightPersonConfigurationsKC = "RightPersonConfigurationsKC";
                internal const String RightPersonConfigurationKC = "RightPersonConfigurationKC";
                internal const String RightJobTitleConfigurationsKC = "RightJobTitleConfigurationsKC";
                internal const String RightJobTitleConfigurationKC = "RightJobTitleConfigurationKC";
                internal const String RightPersonConfigurationsPF = "RightPersonConfigurationsPF";
                internal const String RightPersonConfigurationPF = "RightPersonConfigurationPF";
                internal const String RightJobTitleConfigurationsPF = "RightJobTitleConfigurationsPF";
                internal const String RightJobTitleConfigurationPF = "RightJobTitleConfigurationPF";
                internal const String RightPersonConfigurationsRM = "RightPersonConfigurationsRM";
                internal const String RightPersonConfigurationRM = "RightPersonConfigurationRM";
                internal const String RightJobTitleConfigurationsRM = "RightJobTitleConfigurationsRM";
                internal const String RightJobTitleConfigurationRM = "RightJobTitleConfigurationRM";

                internal const String Permissions = "Permissions";
                internal const String RoleTypes = "RoleTypes";
                internal const String RoleType = "RoleType";
            }
            internal class DS
            {
                internal const String OrganizationExtendedProperty = "OrganizationExtendedProperty";
                internal const String OrganizationExtendedProperties = "OrganizationExtendedProperties";

                internal const String OrganizationClassification = "OrganizationClassification";
                internal const String OrganizationClassifications = "OrganizationClassifications";
                internal const String OrganizationClassificationsChildren = "OrganizationClassificationsChildren";
                internal const String OrganizationsRoots = "OrganizationsRoots";
                internal const String OrganizationsRootsWithFacility = "OrganizationsRootsWithFacility";
                internal const String OrganizationsWithFacility = "OrganizationsWithFacility";

                internal const String Organization = "Organization";
                internal const String Organizations = "Organizations";
                internal const String FunctionalArea = "FunctionalArea";
                internal const String FunctionalAreas = "FunctionalAreas";
                internal const String FunctionalAreaChildren = "FunctionalAreasChildren";
                internal const String FacilityType = "FacilityType";
                internal const String FacilityTypes = "FacilityTypes";
                internal const String Facility = "Facility";
                internal const String Facilities = "Facilities";
                internal const String FacilitiesByProcess = "FacilitiesByProcess";
                internal const String Sector = "Sector";
                internal const String Sectors = "Sectors";
                internal const String SectorsChildren = "SectorsChildren";
                internal const String GeographicArea = "GeographicArea";
                internal const String GeographicAreas = "GeographicAreas";
                internal const String GeographicAreaChildren = "GeographicAreasChildren";
                internal const String Position = "Position";
                internal const String Positions = "Positions";
                internal const String FunctionalPosition = "FunctionalPosition";
                internal const String FunctionalPositions = "FunctionalPositions";
                internal const String FunctionalPositionChildren = "FunctionalPositionsChildren";
                internal const String GeographicFunctionalArea = "GeographicFunctionalArea";
                internal const String GeographicFunctionalAreas = "GeographicFunctionalAreas";
                internal const String GeographicFunctionalAreaChildren = "GeographicFunctionalAreasChildren";
                internal const String SubordinateClassification = "SubordinateClassification";
                internal const String SubordinateClassifications = "SubordinateClassifications";
                internal const String SubordinateClassificationChildren = "SubordinatesClassificationChildren";
                internal const String OrganizationalChart = "OrganizationalChart";
                internal const String OrganizationalCharts = "OrganizationalCharts";
                internal const String OrganizationalChartChildren = "OrganizationalChartsChildren";
                internal const String JobTitle = "JobTitle";
                internal const String JobTitles = "JobTitles";
                internal const String JobTitleChildren = "JobTitlesChildren";
                internal const String Person = "Person";
                internal const String People = "People";
                internal const String PeopleByJobTitle = "PeopleByJobTitle";
                internal const String SalutationType = "SalutationType";
                internal const String SalutationTypes = "SalutationTypes";
                internal const String ContactType = "ContactType";
                internal const String ContactTypes = "ContactTypes";
                internal const String ContactMessengerProvider = "ContactMessengerProvider";
                internal const String ContactMessengerProviders = "ContactMessengerProviders";
                internal const String ContactMessengerApplication = "ContactMessengerApplication";
                internal const String ContactMessengerApplications = "ContactMessengerApplications";
                internal const String Country = "Country";
                internal const String Countries = "Countries";
                internal const String Language = "Language";
                internal const String Languages = "Languages";
                internal const String Applicability = "Applicability";
                internal const String Applicabilities = "Applicabilities";
                internal const String Applicability_LG = "Applicability_LG";
                internal const String Applicabilities_LG = "Applicabilities_LG";

                internal const String OrganizationRelationshipType = "OrganizationRelationshipType";
                internal const String OrganizationRelationshipTypes = "OrganizationRelationshipTypes";
                internal const String OrganizationRelationship = "OrganizationRelationship";
                internal const String OrganizationRelationships = "OrganizationRelationships";
                internal const String Address = "Address";
                internal const String Addresses = "Addresses";
                internal const String ContactEmail = "ContactEmail";
                internal const String ContactEmails = "ContactEmails";
                internal const String AllSystemPersonEmails = "AllSystemPersonEmails";
                internal const String Telephone = "Telephone";
                internal const String Telephones = "Telephones";
                internal const String ContactMessenger = "ContactMessenger";
                internal const String ContactMessengers = "ContactMessengers";
                internal const String ContactURL = "ContactUrl";
                internal const String ContactURLs = "ContactUrls";
                internal const String Presence = "Presence";
                internal const String User = "User";
                internal const String Users = "Users";
                internal const String Responsibility = "Responsibility";
                internal const String Responsibilities = "Responsibilities";
                internal const String Post = "Post";
                internal const String Posts = "Posts";
                internal const String PostsByOrganization = "PostsByOrganization";
            }
            internal class PF
            {
                internal const String AuditPlan = "AuditPlan";
                internal const String ProcessClassification = "ProcessClassification";
                internal const String ProcessClassifications = "ProcessClassifications";
                internal const String ProcessClassificationChildren = "ProcessClassificationsChildren";
                internal const String ProcessGroupProcessesRoots = "ProcessGroupProcessesRoots";
                internal const String ProcessGroupProcessesWithoutClassification = "ProcessGroupProcessesWithoutClassification";
                internal const String ProcessGroupProcess = "ProcessGroupProcess";
                internal const String ProcessGroupProcesses = "ProcessGroupProcesses";
                internal const String ProcessGroupNode = "ProcessGroupNode";
                internal const String ProcessGroupNodes = "ProcessGroupNodes";
                internal const String ProcessGroupNodesChildren = "ProcessGroupNodesChildren";
                internal const String ProcessTask = "ProcessTask";
                internal const String ProcessTasks = "ProcessTasks";
                
                internal const String ExtendedPropertyClassification = "ExtendedPropertyClassification";
                internal const String ExtendedPropertyClassifications = "ExtendedPropertyClassifications";
                internal const String ExtendedProperty = "ExtendedProperty";
                internal const String ExtendedProperties = "ExtendedProperties";
                internal const String ParticipationType = "ParticipationType";
                internal const String ParticipationTypes = "ParticipationTypes";
                //internal const String RoleType = "RoleType";
                //internal const String RoleTypes = "RoleTypes";
                internal const String ProcessParticipation = "ProcessParticipation";
                internal const String ProcessParticipations = "ProcessParticipations";
                internal const String TimeUnit = "TimeUnit";
                internal const String TimeUnits = "TimeUnits";

                internal const String Process = "Process";
                internal const String Processes = "Processes";
                internal const String ProcessExtendedProperty = "ProcessExtendedProperty";
                internal const String ProcessExtendedProperties = "ProcessExtendedProperties";
                internal const String ProcessTaskExecutionsAttachments = "ProcessTaskExecutionsAttachments";
                internal const String ProcessTaskExecution = "ProcessTaskExecution";
                internal const String ProcessTaskExecutions = "ProcessTaskExecutions";

                internal const String ProcessTaskExecutionCalibration = "ProcessTaskExecutionCalibration";
                internal const String ProcessTaskExecutionOperation = "ProcessTaskExecutionOperation";
                internal const String ProcessTaskExecutionMeasurement = "ProcessTaskExecutionMeasurement";

                internal const String ProcessTaskCalibration = "ProcessTaskCalibration";
                internal const String ProcessTaskCalibrations = "ProcessTaskCalibrations";
                internal const String ProcessTaskOperation = "ProcessTaskOperation";
                internal const String ProcessTaskOperations = "ProcessTaskOperations";
                internal const String ProcessTaskMeasurement = "ProcessTaskMeasurement";
                internal const String ProcessTaskMeasurements = "ProcessTaskMeasurements";
                internal const String ProcessTaskMeasurementsByOperator = "ProcessTaskMeasurementsByOperator";

                internal const String ProcessResources = "ProcessResources";
                internal const String ProcessResource = "ProcessResource";
                internal const String TaskState = "TaskState";

                internal const String ProcessAsociatedToFacility = "ProcessAsociatedToFacility";
            }
            internal class PA
            {
                internal const String IndicatorClassification = "IndicatorClassification";
                internal const String IndicatorClassifications = "IndicatorClassifications";
                internal const String IndicatorClassificationChildren = "IndicatorClassificationsChildren";
                internal const String Indicator = "Indicator";
                internal const String Indicators = "Indicators";
                internal const String IndicatorGHG = "IndicatorGHG";
                internal const String IndicatorGHGs = "IndicatorGHGs";
                internal const String IndicatorsGHGRoots = "IndicatorsGHGRoots";
                internal const String IndicatorGHGSubstance = "IndicatorGHGSubstance";
                internal const String Formula = "Formula";
                internal const String Formulas = "Formulas";
                internal const String IndicatorsRoots = "IndicatorsRoots";
                internal const String Magnitud = "Magnitud";
                internal const String Magnitudes = "Magnitudes";
                internal const String StatisticsOfMeasurements = "StatisticsOfMeasurements";
                internal const String MeasurementDataSeries = "MeasurementDataSeries";
                internal const String TransformationDataSeries = "TransformationDataSeries";
                internal const String MeasurementStatistical = "MeasurementStatistical";
                internal const String Measurement = "Measurement";
                internal const String Measurements = "Measurements";

                internal const String MeasurementExtensive = "MeasurementExtensive";
                internal const String MeasurementIntensive = "MeasurementIntensive";
                internal const String MeasurementsByTask = "MeasurementsByTask";
                internal const String MeasurementUnit = "MeasurementUnit";
                internal const String MeasurementUnits = "MeasurementUnits";
                internal const String MeasurementDeviceType = "MeasurementDeviceType";
                internal const String MeasurementDeviceTypes = "MeasurementDeviceTypes";
                internal const String MeasurementDevice = "MeasurementDevice";
                internal const String MeasurementDevices = "MeasurementDevices";
                internal const String MeasurementDevicesByTask = "MeasurementDevicesByTask";
                internal const String CalculationScenarioType = "CalculationScenarioType";
                internal const String CalculationScenarioTypes = "CalculationScenarioTypes";
                internal const String ParameterGroup = "ParameterGroup";
                internal const String ParameterGroups = "ParameterGroups";
                internal const String Parameter = "Parameter";
                internal const String Parameters = "Parameters";
                internal const String ParameterRange = "ParameterRange";
                internal const String ParameterRanges = "ParameterRanges";
                internal const String Calculation = "Calculation";
                internal const String Calculations = "Calculations";
                internal const String KeyIndicators = "KeyIndicators";
                internal const String AllMeasurementByFacility = "AllMeasurementByFacility";
                internal const String AllKeyIndicators = "AllKeyIndicators";
                internal const String KeyIndicator = "KeyIndicator";

                internal const String CalculationEstimates = "CalculationEstimates";
                internal const String CalculationEstimate = "CalculationEstimate";
                internal const String CalculationCertificates = "CalculationCertificates";
                internal const String CalculationCertificate = "CalculationCertificate";

                internal const String IndicatorExtendedProperty = "IndicatorExtendedProperty";
                internal const String IndicatorExtendedProperties = "IndicatorExtendedProperties";
                internal const String FormulaExtendedProperty = "FormulaExtendedProperty";
                internal const String FormulaExtendedProperties = "FormulaExtendedProperties";
                internal const String CalculationExtendedProperty = "CalculationExtendedProperty";
                internal const String CalculationExtendedProperties = "CalculationExtendedProperties";
                internal const String ParameterGroupExtendedProperty = "ParameterGroupExtendedProperty";
                internal const String ParameterGroupExtendedProperties = "ParameterGroupExtendedProperties";

                internal const String ConstantClassification = "ConstantClassification";
                internal const String ConstantClassifications = "ConstantClassifications";
                internal const String ConstantClassificationChildren = "ConstantClassificationsChildren";
                internal const String Constant = "Constant";
                internal const String Constants = "Constants";
                internal const String AccountingScenarios="AccountingScenarios";
                internal const String AccountingScenario = "AccountingScenario";
                internal const String AccountingScopes = "AccountingScopes";
                internal const String AccountingScope = "AccountingScope";
                internal const String AccountingActivity = "AccountingActivity";
                internal const String AccountingActivities = "AccountingActivities";
                
                internal const String ConfigurationExcelFile = "ConfigurationExcelFile";
                internal const String ConfigurationExcelFiles = "ConfigurationExcelFiles";

                internal const String AccountingActivitiesChildren = "AccountingActivitiesChildren";
                internal const String AccountingSector = "AccountingSector";
                internal const String AccountingSectors = "AccountingSectors";
                internal const String AccountingSectorsChildren = "AccountingSectorsChildren";

                internal const String Methodologies = "Methodologies";
                internal const String Methodology = "Methodology";
                internal const String Qualities = "Qualities";
                internal const String Quality = "Quality";

                internal const String CalculateOfTransformation = "CalculateOfTransformation";
                internal const String Transformation = "Transformation";
                internal const String Transformations = "Transformations";
                internal const String TransformationsByTransformation = "TransformationsByTransformation";
                internal const String TransformationByTransformation = "TransformationByTransformation";

                internal const String BasedMeasurementsOfTheTransformations = "BasedMeasurementsOfTheTransformations";
                internal const String TransformationsByMeasurement = "TransformationsByMeasurement";
                internal const String MeasurementsOfTransformation = "MeasurementsOfTransformation";

                internal const String Alphabet = "Alphabet";
            }
            internal class KC
            {
                internal const String ResourceExtendedProperty = "ResourceExtendedProperty";
                internal const String ResourceExtendedProperties = "ResourceExtendedProperties";
                internal const String Catalog = "Catalog";
                internal const String VersionFile = "VersionFile";
                internal const String ResourceVersion = "ResourceVersion";
                internal const String ResourceVersions = "ResourceVersions";
                internal const String ResourceFiles = "ResourceFiles";
                internal const String ResourceCatalogues = "ResourceCatalogues";
                internal const String ResourceCatalog = "ResourceCatalog";
                internal const String ResourceClassification = "ResourceClassification";
                internal const String ResourceClassifications = "ResourceClassifications";
                internal const String ResourceClassificationChildren = "ResourceClassificationsChildren";
                internal const String ResourceType = "ResourceType";
                internal const String ResourceTypes = "ResourceTypes";
                internal const String ResourceTypeChildren = "ResourceTypesChildren";
                internal const String ResourceHistoryState = "ResourceHistoryState";
                internal const String ResourceFileStates = "ResourceFileStates";
                internal const String ResourcesRoots = "ResourcesRoots";
                internal const String Resource = "KCResource";
                internal const String Resources = "KCResources";
            }
            internal class CT
            {
                internal const String Forum = "Forum";
                internal const String Forums = "Forums";
                internal const String Category = "Category";
                internal const String Categories = "Categories";
                internal const String Topic = "Topic";
                internal const String Topics = "Topics";
                internal const String FullTopics = "FullTopics";
                internal const String Message = "Message";
                internal const String Messages = "Messages";
            }
            internal class IA
            {
                internal const String ProjectClassification = "ProjectClassification";
                internal const String ProjectClassifications = "ProjectClassifications";
                internal const String ProjectClassificationChildren = "ProjectClassificationsChildren";
                internal const String Exceptions = "Exceptions";
                internal const String Exception = "Exception";
            }
            internal class RM
            {
                internal const String RiskClassification = "RiskClassification";
                internal const String RiskClassifications = "RiskClassifications";
                internal const String RiskClassificationChildren = "RiskClassificationsChildren";
            }
            internal class DB
            {
                internal const String ActiveTasks = "ActiveTasks";
                internal const String PlannedTasks = "PlannedTasks";
                internal const String FinishedTasks = "FinishedTasks";
                internal const String OverDueTasks = "OverDueTasks";
                internal const String OpenedExceptions = "OpenedExceptions";
                internal const String ClosedExceptions = "ClosedExceptions";
                internal const String WorkingExceptions = "WorkingExceptions";
                internal const String AllMyExecutions = "AllMyExecutions";
                internal const String ProcessTaskExecutionExecuted = "ProcessTaskExecutionExecuted";
                internal const String BulkLoad = "BulkLoad";

                internal const String DatesOfOverdueTasks = "DatesOfOverdueTasks";
                internal const String DatesOfFinishedTasks = "DatesOfFinishedTasks";
                internal const String DatesOfWorkingTasks = "DatesOfWorkingTasks";
                internal const String DatesOfPlannedTasks = "DatesOfPlannedTasks";
            }

            internal class RPT
            {
                internal const String ReportTransformationByScope = "ReportTransformationByScope";
                internal const String ChartTransformationTotalGases = "ChartTransformationTotalGases";
                internal const String ChartTransformationTotalGasByActivity="ChartTransformationTotalGasByActivity";
                internal const String ChartTransformationTotalActivityByGas="ChartTransformationTotalActivityByGas";
                internal const String ChartTransformationTotaltnCO2ByActivity="ChartTransformationTotaltnCO2ByActivity";
                internal const String ReportTransformationMeasurement = "ReportTransformationMeasurement";
            }

            internal class RG
            {
                internal const String ChartPieScopeByIndicator = "ChartPieScopeByIndicator";
                internal const String ChartPieScopeByFacility = "ChartPieScopeByFacility";
                internal const String ChartBarFacilityTypeByScopeGEI = "ChartBarFacilityTypeByScopeGEI";
                internal const String ChartBarFacilityTypeByScopeCL = "ChartBarFacilityTypeByScopeCL";
                internal const String ChartBarActivityByScopeCL = "ChartBarActivityByScopeCL";
                internal const String ChartBarActivityByScopeGEI = "ChartBarActivityByScopeGEI";
                internal const String ChartBarActivityByScopeAndFacilityCL = "ChartBarActivityByScopeAndFacilityCL";
                internal const String ChartBarActivityByScopeAndFacilityGEI = "ChartBarActivityByScopeAndFacilityGEI";
                internal const String ChartBarStateByScopeCL = "ChartBarStateByScopeCL";
                internal const String ChartBarStateByScopeGEI = "ChartBarStateByScopeGEI";

                internal const String ChartEvolution = "ChartEvolution";
                internal const String ChartEvolutionOnlyCO2e = "ChartEvolutionOnlyCO2e";
                internal const String ChartEvolutionCO2 = "ChartEvolutionCO2";
                internal const String ChartEvolutionCH4 = "ChartEvolutionCH4";
                internal const String ChartEvolutionN2O = "ChartEvolutionN2O";

                internal const String ReportFacilityAnalyzer = "ReportFacilityAnalyzer";
                internal const String ReportIndicatorTracker = "ReportIndicatorTracker";
                internal const String ChartIndicatorTracker = "ChartIndicatorTracker";
                internal const String ReportMultiObservatory = "ReportMultiObservatory";

            }
        }
        internal class ConstantsStyleClassName
        {
            internal const String cssNameGRCMenuSelected = "contentToolbarGRCOpen";
            internal const String cssNameGRCMenuUnSelected = "contentToolbarGRC";

            internal const String cssNameGeneralOptionMenuSelected = "contentToolbarOptionOpen";
            internal const String cssNameGeneralOptionMenuUnSelected = "contentToolbarOption";

            internal const String cssNameSecurityOptionMenuSelected = "contentToolbarSecurityOpen";
            internal const String cssNameSecurityOptionMenuUnSelected = "contentToolbarSecurity";

            internal const String cssNameButtonReturn = "contentToolbarReturn";
            internal const String cssNameButtonNext = "contentToolbarNext";
            internal const String cssNameButtonSave = "contentToolbarSave";

            internal const String cssNameContextElementMapContentButton = "contentToolbarMap";

            //Process, Nodes, Task con su Estado.
            internal const String cssNameProcessGroupProcessesFinished = "ProcessGroupProcessesFinished";
            internal const String cssNameProcessNodeFinished = "ProcessNodeFinished";
            internal const String cssNameProcessTaskOperationFinished = "ProcessTaskOperationFinished";
            internal const String cssNameProcessTaskCalibrationFinished = "ProcessTaskCalibrationFinished";
            internal const String cssNameProcessTaskMeasurementFinished = "ProcessTaskMeasurementFinished";

            internal const String cssNameProcessGroupProcessesWorking = "ProcessGroupProcessesWorking";
            internal const String cssNameProcessNodeWorking = "ProcessNodeWorking";
            internal const String cssNameProcessTaskOperationWorking = "ProcessTaskOperationWorking";
            internal const String cssNameProcessTaskCalibrationWorking = "ProcessTaskCalibrationWorking";
            internal const String cssNameProcessTaskMeasurementWorking = "ProcessTaskMeasurementWorking";

            internal const String cssNameProcessGroupProcessesOverdue = "ProcessGroupProcessesOverdue";
            internal const String cssNameProcessNodeOverdue = "ProcessNodeOverdue";
            internal const String cssNameProcessTaskOperationOverdue = "ProcessTaskOperationOverdue";
            internal const String cssNameProcessTaskCalibrationOverdue = "ProcessTaskCalibrationOverdue";
            internal const String cssNameProcessTaskMeasurementOverdue = "ProcessTaskMeasurementOverdue";

            internal const String cssNameProcessGroupProcessesPlanned = "ProcessGroupProcessesPlanned";
            internal const String cssNameProcessNodePlanned = "ProcessNodePlanned";
            internal const String cssNameProcessTaskOperationPlanned = "ProcessTaskOperationPlanned";
            internal const String cssNameProcessTaskCalibrationPlanned = "ProcessTaskCalibrationPlanned";
            internal const String cssNameProcessTaskMeasurementPlanned = "ProcessTaskMeasurementPlanned";


        }
    #endregion

}
