using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Linq;
using System.Transactions;
using System.Data;

namespace Condesus.EMS.Business.PF.Entities
{
    public class ProcessGroupProcess : ProcessGroup, ISecurityEntity
    {
        #region Internal Properties
            private String _Identification;
            private Dictionary<Int64, ProcessClassification> _ProcessClassifications;
            private Dictionary<Int64, IA.Entities.Exception> _Exeptions;
            private Dictionary<Int64, PA.Entities.Calculation> _AssociatedCalculations;
            private Dictionary<Int64, PA.Entities.Calculation> _AssociatedCalculationsByIndicators;
            private List<Entities.ProcessParticipation> _ProcessParticipations;
            private DateTime _CurrentCampaignStartDate;
            private Int64 _IdResourcePicture;
            private KC.Entities.ResourceCatalog _ResourcePicture;
            private String _Coordinate;            
            private Int64 _IdGeographicArea;
            private GIS.Entities.GeographicArea _GeographicArea;
            private Dictionary<Int64, PA.Entities.CalculateOfTransformation> _CalculateOfTransformations;
            private Dictionary<Int64, PF.Entities.ProcessTask> _ProcessTask;
            private Int64 _IdOrganization;
            private DS.Entities.Organization _Organization;
            private String _TwitterUser;
            private String _FacebookUser;
        #endregion 

        #region External Properties
            public String TwitterUser
            {
                get { return _TwitterUser; }
            }
            public String FacebookUser
            {
                get { return _FacebookUser; }
            }
            public Int64 IdOrganization
            {
                get { return _IdOrganization; }
            }
            public DS.Entities.Organization Organization
            {
                get
                {
                    if (_Organization == null)
                    { _Organization = new DS.Collections.Organizations(Credential).Item(_IdOrganization); }
                    return _Organization;
                }
            }
            public DateTime CurrentCampaignStartDate
            { 
                get { return _CurrentCampaignStartDate; } 
            }
            public override ProcessGroupProcess Parent
            {
                get{return this; }
            }
            public String Identification
            {
                get { return _Identification; }
            }
            public override String State
            {
                get 
                { 
                    if (DateTime.Now < StartDate)
                    { return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Planned; }

                    //Si la fecha actual esta dentro de las fechas planeada de inicio y fin de la tarea
                    if ((DateTime.Now >= StartDate) && (DateTime.Now <= EndDate))
                    {
                        //y si el % de completitud es 100%, entonces la tarea esta terminada.
                        if (Completed == 100)
                        {
                            return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Finished;
                        }
                        else
                        {
                            //sino esta trabajando.
                            return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Working;
                        }
                    }
                    //Si la fecha actual es mayor a la planificada como fecha final y no llego al 100% de completitud, quiere decir que esta vencida!!!
                    if ((DateTime.Now > EndDate) && (Completed < 100))
                    { return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_OverDue; }

                    return Condesus.EMS.Business.Common.Resources.ConstantMessage.ProcessState_Working;
                }
            }
            public override DateTime StartDate
            {
                get
                {
                    DateTime _startDate = DateTime.MaxValue;
                    Dictionary<Int64, ProcessTask> _tasks = ChildrenTask;
                    foreach (ProcessTask _task in _tasks.Values)
                    {
                        if (_task.StartDate < _startDate) { _startDate = _task.StartDate; }
                    }            
                    return _startDate;
                }
            }
            public override DateTime EndDate
            {
                get
                {
                    DateTime _endDate = DateTime.MinValue;
                    Dictionary<Int64, ProcessTask> _tasks = ChildrenTask;
                    foreach (ProcessTask _task in _tasks.Values)
                    {
                        if (_task.EndDate > _endDate) { _endDate = _task.EndDate; }
                    }                  
                    return _endDate;
                }
            }
            public override Int32 Completed
            {
                get 
                {
                    ///si o si el peso (Weight), no puede pasar el 100%
                    Int32 _completed = 0;
                    Int32 _countSisters = 0;
                    Dictionary<Int64, ProcessTask> _tasks = ChildrenTask;
                    foreach (ProcessTask _task in _tasks.Values)
                    {
                        //_completed = _completed + (_task.Completed * _task.Weight) / 100;
                        _completed = _completed + (_task.Completed);
                        _countSisters = _countSisters + 1;
                    }
              
                    if (_countSisters == 0) { _countSisters = 1; }
                    _completed = _completed / _countSisters ;
                    if (_completed > 100) { _completed = 100; }
                    return _completed;
                }
            }
            public override String Result
            {
                get 
                {
                    String _result = "---";
                    Int32 _percentageCompleted = 0;
                    Dictionary<Int64, ProcessTask> _tasks = ChildrenTask;
                    foreach (ProcessTask _task in _tasks.Values)
                    {
                        //el resultado me importa si el proceso esta completo
                        if (_task.Completed == 100 && _task.Result == Common.Resources.ConstantMessage.ProcessResult_Ok) { _percentageCompleted = _percentageCompleted + _task.Weight; }
                    }
                    if (_percentageCompleted >= Threshold) { _result = Common.Resources.ConstantMessage.ProcessResult_Ok; }
                    else { if (this.Completed == 100) { _result = Common.Resources.ConstantMessage.ProcessResult_NotOk; } }

                    return _result;
                }
            }
            public Dictionary<Int64, ProcessClassification> Classifications
            {
                get
                {
                    if (_ProcessClassifications == null)
                    { _ProcessClassifications = new Collections.ProcessGroupProcesses(Credential).ClassificationByProcess(this); }
                    return _ProcessClassifications;
                }
            }
            public String Coordinate
            { get { return _Coordinate; } }
            public GIS.Entities.GeographicArea GeographicArea
            {
                get
                {
                    if (_GeographicArea == null)
                    { _GeographicArea = (GIS.Entities.GeographicArea)new GIS.Collections.GeographicAreas(Credential).Item(_IdGeographicArea); }
                    return _GeographicArea;
                }
            }
            public Dictionary<Int64, GIS.Entities.Site> Sites
            {
                get
                {
                    return new GIS.Collections.Facilities(this).Items();
                }
            }

            public override Dictionary<Int64, IA.Entities.Exception> Exceptions
            {
                get
                {
                    Dictionary<Int64, ProcessTask> _Tasks = new Dictionary<Int64, ProcessTask>();
                    if (_Exeptions == null)
                    {
                        _Exeptions = new Dictionary<Int64, Condesus.EMS.Business.IA.Entities.Exception>();
                        _Tasks = ChildrenTask;
                        foreach (ProcessTask _Task in _Tasks.Values)
                        {
                            if (_Task.Exceptions != null)
                            {
                                foreach (IA.Entities.Exception _exeption in _Task.Exceptions.Values)
                                {
                                    _Exeptions.Add(_exeption.IdException, _exeption);
                                }
                            }
                        }                    
                    }
                    return _Exeptions;
                }
            }

        /// <summary>
        /// devuelve todos lo FT que estene en este process y que tengan asociado un facility a una tarea
        /// </summary>
            public Dictionary<Int64, GIS.Entities.FacilityType> FacilityTypesWhitMeasurements
            {
                get
                {
                    return new GIS.Collections.FacilityTypes(this).Items();
                }
            }
            public Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDevice> Devices
            {
                get
                {
                    Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDevice> _measurementDevicesByProcess = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDevice>();

                    ProcessMapChildren(ref _measurementDevicesByProcess);

                    return _measurementDevicesByProcess;
                }
            }
            public Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Measurement> Measurements
            {
                get
                {
                    Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Measurement> _measurementsByProcess = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Measurement>();

                    ProcessMapChildren(ref _measurementsByProcess);

                    return _measurementsByProcess;
                }
            }
            public Dictionary<Int64, PA.Entities.CalculateOfTransformation> CalculateOfTransformations
            {
                get
                {
                    if (_CalculateOfTransformations == null)
                    {
                        _CalculateOfTransformations = new PA.Collections.CalculateOfTransformations(this).Items();
                    }
                    return _CalculateOfTransformations;
                }

            }
            public PA.Entities.CalculateOfTransformation CalculateOfTransformation(Int64 IdCalculateOfTransformation)
            {                
                return new PA.Collections.CalculateOfTransformations(this).Item(IdCalculateOfTransformation);                    
            }
            public Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Measurement> MeasurementsByIndicator(Int64 idIndicator)
            {
                Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Measurement> _measurementsByProcess = new Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Measurement>();

                foreach (Condesus.EMS.Business.PF.Entities.ProcessTask _processGroupNode in this.ChildrenTask.Values)
                {
                    //pido hijos
                    ProcessMapChildren(ref _measurementsByProcess, idIndicator);
                }

                return _measurementsByProcess;
            }
            public Dictionary<Int64, PA.Entities.Calculation> AssociatedCalculations
            {
                get
                {
                    if (_AssociatedCalculations == null)
                    { _AssociatedCalculations = new PA.Collections.Calculations(Credential).ItemsByProcess(this); }
                    return _AssociatedCalculations;
                }
            }
            public Dictionary<Int64, PA.Entities.Calculation> AssociatedCalculationsByIndicators (Int64 idIndicator)
            {
                    if (_AssociatedCalculationsByIndicators == null)
                    { _AssociatedCalculationsByIndicators = new PA.Collections.Calculations(Credential).ItemsByProcessIndicators(this, idIndicator); }
                    return _AssociatedCalculationsByIndicators;
            }
            /// <summary>
            /// Retorna el total Estimado para un calculo en un Escenario. Tomando en cuenta la Fecha de inicio del Periodo.
            /// </summary>
            /// <param name="calculationScenarioType"></param>
            /// <returns>Decimal</returns>
            public Decimal TotalEstimatesByScenario(PA.Entities.CalculationScenarioType calculationScenarioType)
            {
                try
                {
                    //Por ahora toma el primer calculo asociado al proyecto.
                    Int64 _firstIdCalculation = AssociatedCalculations.First().Key;
                    //Ejecuta el metodo Estimate y le pasa la fecha inicio del periodo y el escenario para que realice el calculo con su prorrateo.
                    return AssociatedCalculations[_firstIdCalculation].Estimate(_CurrentCampaignStartDate, calculationScenarioType);
                }
                catch
                {
                    return 0;
                }
            }
            /// <summary>
            /// Retorna el total Estimado para un calculo en un Escenario en todo el tiempo de vida del proyecto.
            /// </summary>
            /// <param name="calculationScenarioType"></param>
            /// <returns>Decimal</returns>
            public Decimal TotalFullEstimatesByScenario(PA.Entities.CalculationScenarioType calculationScenarioType)
            {
                try
                {
                    Decimal _total = 0;

                    //Por ahora toma el primer calculo asociado al proyecto.
                    Int64 _firstIdCalculation = AssociatedCalculations.First().Key;
                    foreach (PA.Entities.CalculationEstimated _calculationEstimated in AssociatedCalculations[_firstIdCalculation].CalculationEstimates)
                    {
                        _total += _calculationEstimated.Value;
                    }
                    
                    //Retorna el total sumado.
                    return _total;
                }
                catch
                {
                    return 0;
                }
            }
            /// <summary>
            /// Retorna el resultado del calculo para el Primer calculo asociado al proyecto. y Utiliza el CurrentCampaign como fecha inicial y Today como fin.
            /// </summary>
            /// <returns></returns>
            public Decimal FirstCalculationResult()
            {
                try
                {
                    //Por ahora toma el primer calculo asociado al proyecto.
                    Int64 _firstIdCalculation = AssociatedCalculations.First().Key;
                    //Ejecuta el calculo, para el primer calculo asociado.
                    return AssociatedCalculations[_firstIdCalculation].Calculate(_CurrentCampaignStartDate, DateTime.Today);
                }
                catch
                {
                    return 0;
                }
            }
            /// <summary>
            /// Retorna el resultado del calculo para el Primer calculo asociado al proyecto. Y lo hace para toda la vida del proyecto.
            /// </summary>
            /// <returns>Un <c>Decimal</c></returns>
            public Decimal FirstCalculationTotalResult()
            {
                try
                {
                    //Por ahora toma el primer calculo asociado al proyecto.
                    Int64 _firstIdCalculation = AssociatedCalculations.First().Key;
                    //Ejecuta el calculo, para el primer calculo asociado.
                    return AssociatedCalculations[_firstIdCalculation].Calculate();
                }
                catch
                {
                    return 0;
                }
            }

            #region Issued (Verificated o Certificated todos =)
            /// <summary>
            /// Este metodo retorna en un dictionary todos los periodos Certificados (Issued) y la Key es el IdCertificated
            /// </summary>
            /// <returns>Un<c>Dictionary</c></returns>
            public Dictionary<Int64, DateTime[]> GetIssuedPeriods()
            {
                Dictionary<Int64, DateTime[]> _periods = new Dictionary<Int64, DateTime[]>();

                Dictionary<Int64, PA.Entities.Calculation> _calculations = this.AssociatedCalculations;
                if (_calculations.Count > 0)
                {
                    foreach (PA.Entities.CalculationCertificated _calcCert in _calculations.First().Value.CalculationCertificates)
                    {
                        //Construye el array de fecha inicio-fin
                        DateTime[] _periodDate = new DateTime[2];
                        _periodDate[0] = _calcCert.StartDate;
                        _periodDate[1] = _calcCert.EndDate;
                        //Lo inserta en el diccionario con la key, que es el orden del periodo.
                        _periods.Add(_calcCert.IdCertificated, _periodDate);
                    }
                }
                //Retorna el dictionary con los periodos de ISSUED.
                return _periods;
            }
        #endregion

            #region Resources
            /// <summary>
            /// Retorna la coleccion de Imagenes que tiene asociado el proyecto. (a travez de CatalogDoc)
            /// Key = IdResourceFile
            /// </summary>
            public Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> Pictures
            {
                get
                {
                    Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> _pictures = new Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc>();
                    //Si el proyecto no tiene ningun ResourcePicture Asociado...entrega vacio.
                    if (this.ResourcePicture != null)
                    {
                        foreach (Condesus.EMS.Business.KC.Entities.Catalog _catalog in this.ResourcePicture.Catalogues.Values)
                        {
                            if (_catalog.GetType().Name == "CatalogDoc")
                            {
                                //Lo castea a tipo Doc
                                Condesus.EMS.Business.KC.Entities.CatalogDoc _catalogDoc = (Condesus.EMS.Business.KC.Entities.CatalogDoc)_catalog;

                                //Solo nos quedamos con los que son tipo image
                                if (_catalogDoc.DocType.Contains("image"))
                                {
                                    _pictures.Add(_catalogDoc.IdResourceFile, _catalogDoc);
                                }
                            }
                        }
                    }
                    return _pictures;
                }
            }
            public KC.Entities.ResourceCatalog ResourcePicture
            {
                  get
                {
                    if (_ResourcePicture == null)
                    { _ResourcePicture = (KC.Entities.ResourceCatalog)new KC.Collections.Resources(Credential).Item(_IdResourcePicture); }
                    return _ResourcePicture;
                }
            }
            ////TODO:ver para q se usa este metodo
            //public List<ProcessResource> ProcessResourceCatalogues
            //{
            //    get
            //    {
            //        if (_ProcessResourceCatalogues == null)
            //        {
            //            List<ProcessResource> _ProcessResources = new Collections.ProcessResources(Credential).Items(this);
            //            foreach (ProcessResource _processResource in _ProcessResources)
            //            {
            //                if (_processResource.Resource.GetType().Name == Common.Constants.TypeResourceCatalog)
            //                { _ProcessResourceCatalogues.Add(_processResource); }
            //            }
            //        }
            //        return _ProcessResourceCatalogues;
            //    }
            //}
        #endregion

            #region Forum
            /// <summary>
            /// Foro q tiene asociado por default
            /// </summary>
            public CT.Entities.Forum Forum(Int64 idForum)
            {
                return new CT.Collections.Forums(Credential).Item(idForum);
            }
            ///// <summary>
            ///// Foro q tiene asociado por default
            ///// </summary>
            //public CT.Entities.Forum ForumDefault
            //{
            //    get
            //    {
            //        return new CT.Collections.Forums(Credential).Item(this);
            //    }
            //}
            /// <summary>
            /// Todos los foros q tiene asociados
            /// </summary>
            public Dictionary<Int64, CT.Entities.Forum> Forums
            {
                get 
                {
                    return new CT.Collections.Forums(this).Items();
                }
            }
            /// <summary>
            /// Asocia un foro al process, pero no lo pone como default
            /// </summary>
            /// <param name="forum"></param>
            public void AssociateForum(CT.Entities.Forum forum)
            {                
                new Collections.ProcessGroupProcesses(Credential).AddRelationship(this, forum);
            }
            /// <summary>
            /// Desasocia un foro
            /// </summary>
            /// <param name="forum"></param>
            public void DisAssociateForum(CT.Entities.Forum forum)
            {
                new Collections.ProcessGroupProcesses(Credential).RemoveRelationship(this, forum);
            }

            #endregion

            #region Participations
            public Dictionary<Int64, PF.Entities.ProcessGroupProcess> ProcessParicipationsByProcess()
            {
                return new PF.Collections.ProcessGroupProcesses(Credential).Items(this);
            }

            public List<Entities.ProcessParticipation> ProcessParticipations
            {
                get
                {
                    if (_ProcessParticipations == null)
                    { _ProcessParticipations = new Collections.ProcessParticipations(Credential, this).Items(); }
                    return _ProcessParticipations;
                }
            }
            public ProcessParticipation ProcessParticipation(Int64 idOrganization, Int64 idParticipationType)
            {
                return new Collections.ProcessParticipations(Credential, this).Item(idOrganization, idParticipationType); 
            }
            public ProcessParticipation ProcessParticipationAdd(Int64 idOrganization, Int64 idParticipationType, String comment)
            {
                return new Collections.ProcessParticipations(Credential, this).Add(idOrganization, idParticipationType, comment);
            }
            public void Remove(ProcessParticipation processParticipation)
            {
                new Collections.ProcessParticipations(Credential, this).Remove(processParticipation);
            }

            #endregion

            #region Process Task

            public Dictionary<Int64, Entities.ProcessTask> TaskMeasurementWhitOutSite
            {
                get
                {
                    return new Collections.ProcessTasks(Credential).Items(this);
                }
            }
            public Dictionary<Int64, Entities.ProcessTask> TaskCalibrations
            {
                get
                {
                    return new Collections.ProcessTasks(Credential).ItemsTaskCalibrations(this);
                }
            }
            public Dictionary<Int64, Entities.ProcessTask> TaskOperations
            {
                get
                {
                    return new Collections.ProcessTasks(Credential).ItemsTaskOperation(this);
                }
            }

            #region Task Operation
            public PF.Entities.ProcessTask ProcessTasksAdd(Int16 weight, Int16 orderNumber, String title, String purpose, String description, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, String comment, String typeExecution, List<DS.Entities.Post> post, GIS.Entities.Site site, KC.Entities.Resource taskInstruction, List<NT.Entities.NotificationRecipient> notificationRecipients, PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    PF.Entities.ProcessTask _processTask = new PF.Collections.ProcessTasks(Credential).AddTask(weight, orderNumber, title, purpose, description, this, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, comment, typeExecution, post, site, taskInstruction, notificationRecipients, timeUnitAdvanceNotice, advanceNotice);
                    _transactionScope.Complete();
                    return _processTask;
                }

            }
            public void ProcessTasksRemove(PF.Entities.ProcessTaskOperation processTaskOperation)
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    new PF.Collections.ProcessTasks(Credential).RemoveTask(processTaskOperation);
                    if (_ProcessTask != null)
                    {
                        _ProcessTask.Remove(processTaskOperation.IdProcess);
                    }
                    _transactionScope.Complete();
                }
            }
            #endregion

            #region Task Calibration
            public PF.Entities.ProcessTask ProcessTasksAdd(Int16 weight, Int16 orderNumber, String title, String purpose, String description, DateTime startDate, DateTime endDate, Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed, PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, PA.Entities.MeasurementDevice measurementDevice, String typeExecution, List<DS.Entities.Post> post, GIS.Entities.Site site, KC.Entities.Resource taskInstruction, List<NT.Entities.NotificationRecipient> notificationRecipients, PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    PF.Entities.ProcessTask _processTask = new PF.Collections.ProcessTasks(Credential).AddTask(weight, orderNumber, title, purpose, description, this, startDate, endDate, duration, interval, maxNumberExecutions, result, completed, timeUnitDuration, timeUnitInterval, measurementDevice, typeExecution, post, site, taskInstruction, notificationRecipients, timeUnitAdvanceNotice, advanceNotice);
                    _transactionScope.Complete();
                    return _processTask;
                }
            }
            public void ProcessTasksRemove(PF.Entities.ProcessTaskCalibration processTaskCalibration)
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    new PF.Collections.ProcessTasks(Credential).RemoveTask(processTaskCalibration);
                    if (_ProcessTask != null)
                    {
                        _ProcessTask.Remove(processTaskCalibration.IdProcess);
                    }
                    _transactionScope.Complete();
                }
            }
            #endregion

            #region Task Measurement
            public PF.Entities.ProcessTask ProcessTasksAdd(PA.Entities.MeasurementDevice measurementDevice, List<PA.Entities.ParameterGroup> parametersGroups,
                PA.Entities.Indicator indicator, String measurementName, String measurementDescription, PF.Entities.TimeUnit timeUnitFrequency,
                Int32 frequency, PA.Entities.MeasurementUnit measurementUnit, Boolean isRegressive, Boolean isRelevant,
                Int16 weight, Int16 orderNumber, String title, String purpose, String description, DateTime startDate, DateTime endDate, 
                Int32 duration, Int32 interval, Int64 maxNumberExecutions, Boolean result, Int32 completed,
                PF.Entities.TimeUnit timeUnitDuration, PF.Entities.TimeUnit timeUnitInterval, String typeExecution, List<DS.Entities.Post> post,
                GIS.Entities.Site site, KC.Entities.Resource taskInstruction, String measurementSource, String measurementFrequencyAtSource,
                Decimal measurementUncertainty, PA.Entities.Quality measurementQuality, PA.Entities.Methodology measurementMethodology,
                PA.Entities.AccountingScope accountingScope, PA.Entities.AccountingActivity accountingActivity, String reference,
                List<NT.Entities.NotificationRecipient> notificationRecipients, PF.Entities.TimeUnit timeUnitAdvanceNotice, Int16 advanceNotice)
            {
                using (TransactionScope _transactionScope = new TransactionScope())
                {
                    PA.Entities.Measurement _measurement = new PA.Collections.Measurements(Credential).Add(measurementDevice, parametersGroups,
                        indicator, measurementName, measurementDescription, timeUnitFrequency, frequency, measurementUnit, 
                        isRegressive, isRelevant, measurementSource, measurementFrequencyAtSource, measurementUncertainty, measurementQuality,
                        measurementMethodology);

                    PF.Entities.ProcessTask _processTask = new PF.Collections.ProcessTasks(Credential).AddTask(weight, orderNumber, title, purpose,
                        description, this, startDate, endDate, duration, interval, maxNumberExecutions, result, completed,
                        timeUnitDuration, timeUnitInterval, _measurement, typeExecution, post, site, taskInstruction, accountingScope,
                        accountingActivity, reference, notificationRecipients, timeUnitAdvanceNotice, advanceNotice);

                    _transactionScope.Complete();
                    return _processTask;
                }
            }
            public void ProcessTasksRemove(PF.Entities.ProcessTaskMeasurement processTaskMeasurement)
            {
                using (TransactionScope _transactionScope = new TransactionScope(new TransactionScopeOption(), new TimeSpan(0, 10, 0)))
                {
                    new PF.Collections.ProcessTasks(Credential).RemoveTask(processTaskMeasurement);
                    if (_ProcessTask != null)
                    {
                        _ProcessTask.Remove(processTaskMeasurement.IdProcess);
                    }
                    _transactionScope.Complete();

                }
            }
            #endregion

            #endregion

            #region Reports
            public RG.Entities.Report_O_S_A_FT_F Report_O_S_A_FT_F
            {
                get
                {
                    return new RG.ReportGraphic(this).Report_O_S_A_FT_F;
                }
            }
            public RG.Entities.Report_GA_S_A_FT_F Report_GA_S_A_FT_F
            {
                get
                {
                    return new RG.ReportGraphic(this).Report_GA_S_A_FT_F;
                }
            }
            public RG.Entities.Report_GA_FT_F_S_A Report_GA_FT_F_S_A
            {
                get
                {
                    return new RG.ReportGraphic(this).Report_GA_FT_F_S_A;
                }
            }
            public RG.Entities.Report_S_GA_A_FT_F Report_S_GA_A_FT_F
            {
                get
                {
                    return new RG.ReportGraphic(this).Report_S_GA_A_FT_F;
                }
            }
            public RG.Entities.Report_S_A_FT_F Report_S_A_FT_F
            {
                get
                {
                    return new RG.ReportGraphic(this).Report_S_A_FT_F;
                }
            }
            public RG.Entities.Report_FT_F_S_A Report_FT_F_S_A
            {
                get
                {
                    return new RG.ReportGraphic(this).Report_FT_F_S_A;
                }
            }
            
            public DataTable Report_A_F_by_I_COL(Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
            Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
            Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
            Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S,
            Int64 idIndicatorPM, Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
            {
                return new RG.ReportGraphic(this).Report_A_F_by_I_COL(idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,
           idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,
           idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,
           idIndicatorPM, idIndicatorPM10, startDate, endDate);
            }

            public RG.Entities.FacilitiesByIndicators FacilitiesByIndicators
            {
                get { return new RG.ReportGraphic(this).FacilitiesByIndicators(); }
            }

           
            #endregion

            #region Evolutionary
            public List<RG.Entities.Evolutionary_O> EvolutionaryItems(Int64 IdScope, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
        Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
        Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
        Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S,
        Int64 idIndicatorPM, Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
            {
                return new Condesus.EMS.Business.RG.ReportGraphic(this).EvolutionaryItems(IdScope, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,
              idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,
              idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,
              idIndicatorPM, idIndicatorPM10, startDate, endDate);
            }
            #endregion


            #region Graphics

            public RG.Entities.Graphic_Pie Graphic_Pie
            {
                get
                {
                    return new RG.ReportGraphic(this).Graphic_Pie;
                }
            }
            public RG.Entities.Graphic_Bar Graphic_Bar
            {
                get
                {
                    return new RG.ReportGraphic(this).Graphic_Bar;
                }
            }
            #endregion
        #endregion

            internal ProcessGroupProcess(Int64 idProcess, Int16 weight, Int16 orderNumber, String idLanguage, String title, String purpose, String description, Credential credential, Int16 threshold,
                     String identification, DateTime currentCampaignStartDate, Int64 idResourcePicture, String Coordinate, Int64 idGeographicArea, Int64 idOrganization, String TwitterUser, String FacebookUser)
                : base(idProcess, weight, orderNumber, idLanguage, title, purpose, description, credential, threshold)
            {
                _Identification = identification;
                _CurrentCampaignStartDate = currentCampaignStartDate;
                _IdResourcePicture = idResourcePicture;
                _Coordinate = Coordinate;
                _IdGeographicArea = idGeographicArea;
                _IdOrganization = idOrganization;
                _TwitterUser = TwitterUser;
                _FacebookUser = FacebookUser;
            }

            public void Modify(Int16 weight, Int16 orderNumber, String title, String purpose, String description, Int16 threshold,
                            String identification, DateTime currentCampaignStartDate, KC.Entities.ResourceCatalog resourcePicture, String coordinate, GIS.Entities.GeographicArea geographicArea, Dictionary<Int64, PF.Entities.ProcessClassification> processClassifications, DS.Entities.Organization organization, String TwitterUser, String FacebookUser)
            {
                new Collections.ProcessGroupProcesses(Credential).Modify(this, weight, orderNumber, title, purpose, description, threshold,
                             identification, currentCampaignStartDate, resourcePicture, coordinate, geographicArea, organization, TwitterUser, FacebookUser, processClassifications);
            }

            /// <summary>
            /// Borra sus dependencias
            /// </summary>
            internal override void Remove()
            {
                //Borra las tareas
                foreach (ProcessTask _task in ChildrenTask.Values)
                {
                    if (_task.GetType().Name == "ProcessTaskOperation") { this.ProcessTasksRemove((ProcessTaskOperation)_task); }
                    if (_task.GetType().Name == "ProcessTaskCalibration") { this.ProcessTasksRemove((ProcessTaskCalibration)_task); }
                    if (_task.GetType().Name == "ProcessTaskMeasurement") { this.ProcessTasksRemove((ProcessTaskMeasurement)_task); }
                }
                //Calssifications
                foreach (ProcessClassification _processClassification in Classifications.Values)
                {
                    new Collections.ProcessGroupProcesses(Credential).RemoveRelationship(this, _processClassification);
                }
                //ProcessParticipations
                foreach (ProcessParticipation _processParticipation in ProcessParticipations)
                {
                    this.Remove(_processParticipation);
                }
                //Borra calculos asociados
                foreach (PA.Entities.Calculation _Calculation in this.AssociatedCalculations.Values)
                {
                    //Borra la relacion
                    new PA.Collections.Calculations(Credential).Remove(_Calculation, this);
                    //Si solo esta asociado a este proceso, borro al calculo, si trae mas de 1 es porque esta asociado a mas procesos y no debo borrarlo
                    if (_Calculation.AssociatedProcess.Count == 0)
                    { new PA.Collections.Calculations(Credential).Remove(_Calculation.IdCalculation); }
                }
                //Resources ???

                base.Remove();
            }

            #region Internal Methods
            //estos metodos los usan las propiedades externas
            private void ProcessMapChildren(ref Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Measurement> measurementsByProcess)
            {
                foreach (Condesus.EMS.Business.PF.Entities.ProcessTask _processTask in this.ChildrenTask.Values)
                {
                    //Verifica que sean tareas de medicion
                    if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                    {
                        //Se guarda la medicion
                        Condesus.EMS.Business.PA.Entities.Measurement _measurement = ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_processTask).Measurement;
                        //Inserta la medicion en el diccionario de mediciones para luego retornarlas.
                        measurementsByProcess.Add(_measurement.IdMeasurement, _measurement);
                    }
                }
            }
            private void ProcessMapChildren(ref Dictionary<Int64, Condesus.EMS.Business.PA.Entities.Measurement> measurementsByProcess, Int64 idIndicator)
            {
                foreach (Condesus.EMS.Business.PF.Entities.ProcessTask _processTask in this.ChildrenTask.Values)
                {
                    //Verifica que sean tareas de medicion
                    if (_processTask.GetType().Name == "ProcessTaskMeasurement")
                    {
                        //Se guarda la medicion
                        Condesus.EMS.Business.PA.Entities.Measurement _measurement = ((Condesus.EMS.Business.PF.Entities.ProcessTaskMeasurement)_processTask).Measurement;
                        if (_measurement.Indicator.IdIndicator == idIndicator)
                        {
                            //Inserta la medicion en el diccionario de mediciones para luego retornarlas.
                            measurementsByProcess.Add(_measurement.IdMeasurement, _measurement);
                        }
                    }
                }
            }
            private void ProcessMapChildren(ref Dictionary<Int64, Condesus.EMS.Business.PA.Entities.MeasurementDevice> measurementDevicesByProcess)
            {
                foreach (Condesus.EMS.Business.PF.Entities.ProcessTask _processTask in this.ChildrenTask.Values)
                {
                    //Verifica que sean tareas de calibracion, para obtener los equipos
                    if (_processTask.GetType().Name == "ProcessTaskCalibration")
                    {
                        //Se guarda el equipo
                        Condesus.EMS.Business.PA.Entities.MeasurementDevice _measurementDevice = ((Condesus.EMS.Business.PF.Entities.ProcessTaskCalibration)_processTask).MeasurementDevice;
                        //Si ya lo contiene, no lo inserta...
                        if (!measurementDevicesByProcess.ContainsKey(_measurementDevice.IdMeasurementDevice))
                        {
                            //Inserta la medicion en el diccionario de mediciones para luego retornarlas.
                            measurementDevicesByProcess.Add(_measurementDevice.IdMeasurementDevice, _measurementDevice);
                        }
                    }
                }
            }
            #endregion

        #region Security 15-02-2010

            #region Properties
            public Int64 IdObject
            {
                get { return IdProcess; }
            }
            public String ClassName
            {
                get
                {
                    return Common.Security.Process;
                }
            }
            #endregion

            #region Read
            #region Permissions
            internal Dictionary<Int64, Security.Entities.Permission> _Permissions;
            public Dictionary<Int64, Security.Entities.Permission> Permissions
            {
                get
                {
                    if (_Permissions == null)
                    { _Permissions = new Security.Collections.Permissions(Credential).Items(this); }
                    return _Permissions;
                }
            }
            #endregion

            //ALL
            public List<Security.Entities.RightPerson> SecurityPeople()
            {
                return new Security.Collections.Rights(Credential).ReadPersonByObject(this);
            }

            public List<Security.Entities.RightJobTitle> SecurityJobTitles()
            {
                return new Security.Collections.Rights(Credential).ReadJobTitleByObject(this);
            }
            //por ID
            public Security.Entities.RightJobTitle ReadJobTitleByID(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
            {
                return new Security.Collections.Rights(Credential).ReadJobTitleByID(jobTitle, permission);
            }
            public Security.Entities.RightPerson ReadPersonByID(DS.Entities.Person person, Security.Entities.Permission permission)
            {
                return new Security.Collections.Rights(Credential).ReadPersonByID(person, permission);
            }

            #endregion

            #region Write
            //Security Add
            public Security.Entities.RightPerson SecurityPersonAdd(DS.Entities.Person person, Security.Entities.Permission permission)
            {
                //Realiza las validaciones de autorizacion 
                new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage);
                //Alta el permiso
                Security.Entities.RightPerson _rightPerson = new Security.Collections.Rights(Credential).Add(this, person, permission);

                return _rightPerson;
            }
            public Security.Entities.RightJobTitle SecurityJobTitleAdd(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
            {
                //Realiza las validaciones de autorizacion 
                new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage);
                //Alta el permiso
                Security.Entities.RightJobTitle _rightJobTitle = new Security.Collections.Rights(Credential).Add(this, jobTitle, permission);

                return _rightJobTitle;
            }
            //Security Remove
            public void SecurityPersonRemove(DS.Entities.Person person, Security.Entities.Permission permission)
            {
                //Realiza las validaciones de autorizacion 
                new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage);
                //Borra el permiso
                new Security.Collections.Rights(Credential).Remove(person, this, permission);
            }
            public void SecurityJobTitleRemove(DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
            {
                //Realiza las validaciones de autorizacion 
                new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage);
                //Borra el permiso
                new Security.Collections.Rights(Credential).Remove(jobTitle, this, permission);
            }
            //Security Modify
            public Security.Entities.RightPerson SecurityPersonModify(Security.Entities.RightPerson oldRightPerson, DS.Entities.Person person, Security.Entities.Permission permission)
            {
                //Realiza las validaciones de autorizacion 
                new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage);
                //Se borra con sus herederos
                this.SecurityPersonRemove(person, oldRightPerson.Permission);
                //se da de alta el y sus herederos
                this.SecurityPersonAdd(person, permission);

                return new Condesus.EMS.Business.Security.Entities.RightPerson(permission, person);
            }
            public Security.Entities.RightJobTitle SecurityJobTitleModify(Security.Entities.RightJobTitle oldRightJobTitle, DS.Entities.JobTitle jobTitle, Security.Entities.Permission permission)
            {
                //Realiza las validaciones de autorizacion 
                new Security.Authority(Credential).Authorize(this.ClassName, this.IdObject, Credential.User.IdPerson, Common.Permissions.Manage);
                //Se borra con sus herederos
                this.SecurityJobTitleRemove(jobTitle, oldRightJobTitle.Permission);
                //se da de alta el y sus herederos
                this.SecurityJobTitleAdd(jobTitle, permission);

                return new Condesus.EMS.Business.Security.Entities.RightJobTitle(permission, jobTitle);
            }
            #endregion

            #region ISecurityEntity 15-02-2010
            //Security Remove
            public void SecurityPersonRemove()
            {//se usa cuando la baja se hace de este elemento
                new Security.Collections.Rights(Credential).Remove(this);
            }
            public void SecurityJobTitleRemove()
            {//se usa cuando la baja se hace de este elemento
                new Security.Collections.Rights(Credential).Remove(this);
            }
            #endregion
            

        #endregion


     
    }
}
