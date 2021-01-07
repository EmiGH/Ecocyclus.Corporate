using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Condesus.EMS.WebUI.Business
{
    public class MoveEntityNode : Base
    {
        #region Internal Properties
        #endregion

        public MoveEntityNode(String commandName)
        {
            _CommandName = commandName;
        }
        public MoveEntityNode Create(String commandName)
        {
            return new MoveEntityNode(commandName);
        }

        #region Private Methods
        #endregion

        #region Public Methods (Aca estan los metodos que via Reflection nos permite evitar el SWITCH)
            #region Directory Service
                #region Organization Classification
                    public void OrganizationClassificationMove(Dictionary<String, Object> paramSource, Dictionary<String, Object> paramDestination)
                    {
                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idOrganizationClassification = Convert.ToInt64(paramSource["IdOrganizationClassification"]);
                        Int64 _idParentValue = 0; //Si es root, queda 0
                        //Verifica cual es el nodo Destino
                        //Si el destino es al ROOT, no pasa pasa por aca entonces el id queda en 0
                        //if (_pkValueParent != "NodeRootTitle")
                        if (paramDestination.ContainsKey("IdOrganizationClassification"))
                        {
                            //El destino es otra classificacion, entonces busca el id.
                            _idParentValue = Convert.ToInt64(paramDestination["IdOrganizationClassification"]);
                        }
                        //Construye el objeto con el nodo que se esta intentando modificar (Origen)
                        Condesus.EMS.Business.DS.Entities.OrganizationClassification _orgClassSource = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idOrganizationClassification);
                        //Construye el objeto con el nodo destino o en Cero(0) en caso de ser el root.
                        Condesus.EMS.Business.DS.Entities.OrganizationClassification _orgClassParent = EMSLibrary.User.DirectoryServices.Map.OrganizationClassification(_idParentValue);

                        //Finalmente modifica el padre del elemento origen.
                        _orgClassSource.Move(_orgClassParent);

                    }
                #endregion
            #endregion

            #region Performance Assessment
                #region Indicator Classification
                    public void IndicatorClassificationMove(Dictionary<String, Object> paramSource, Dictionary<String, Object> paramDestination)
                    {
                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idIndicatorClassification = Convert.ToInt64(paramSource["IdIndicatorClassification"]);
                        Int64 _idParentValue = 0; //Si es root, queda 0
                        //Verifica cual es el nodo Destino
                        //Si el destino es al ROOT, no pasa pasa por aca entonces el id queda en 0
                        if (paramDestination.ContainsKey("IdIndicatorClassification"))
                        {
                            //El destino es otra classificacion, entonces busca el id.
                            _idParentValue = Convert.ToInt64(paramDestination["IdIndicatorClassification"]);
                        }
                        //Construye el objeto con el nodo que se esta intentando modificar (Origen)
                        Condesus.EMS.Business.PA.Entities.IndicatorClassification _indicatorClassSource = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idIndicatorClassification);
                        //Construye el objeto con el nodo destino o en Cero(0) en caso de ser el root.
                        Condesus.EMS.Business.PA.Entities.IndicatorClassification _indicatorClassParent = EMSLibrary.User.PerformanceAssessments.Map.IndicatorClassification(_idParentValue);

                        //Finalmente modifica el padre del elemento origen.
                        _indicatorClassSource.Move(_indicatorClassParent);
                    }
                #endregion
            #endregion

            #region Process Framework
                #region Process Classification
                    public void ProcessClassificationMove(Dictionary<String, Object> paramSource, Dictionary<String, Object> paramDestination)
                    {
                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idProcessClassification = Convert.ToInt64(paramSource["IdProcessClassification"]);
                        Int64 _idParentValue = 0; //Si es root, queda 0
                        //Verifica cual es el nodo Destino
                        //Si el destino es al ROOT, no pasa pasa por aca entonces el id queda en 0
                        if (paramDestination.ContainsKey("IdProcessClassification"))
                        {
                            //El destino es otra classificacion, entonces busca el id.
                            _idParentValue = Convert.ToInt64(paramDestination["IdProcessClassification"]);
                        }
                        //Construye el objeto con el nodo que se esta intentando modificar (Origen)
                        Condesus.EMS.Business.PF.Entities.ProcessClassification _processClassSource = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idProcessClassification);
                        //Construye el objeto con el nodo destino o en Cero(0) en caso de ser el root.
                        Condesus.EMS.Business.PF.Entities.ProcessClassification _processClassParent = EMSLibrary.User.ProcessFramework.Map.ProcessClassification(_idParentValue);

                        //Finalmente modifica el padre del elemento origen.
                        _processClassSource.Move(_processClassParent);

                    }
                #endregion
            #endregion

            #region Knowledge Collaboration
                #region Resource Classification
                    public void ResourceClassificationMove(Dictionary<String, Object> paramSource, Dictionary<String, Object> paramDestination)
                    {
                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idResourceClassification = Convert.ToInt64(paramSource["IdResourceClassification"]);
                        Int64 _idParentValue = 0; //Si es root, queda 0
                        //Verifica cual es el nodo Destino
                        //Si el destino es al ROOT, no pasa pasa por aca entonces el id queda en 0
                        //if (_pkValueParent != "NodeRootTitle")
                        if (paramDestination.ContainsKey("IdResourceClassification"))
                        {
                            //El destino es otra classificacion, entonces busca el id.
                            _idParentValue = Convert.ToInt64(paramDestination["IdResourceClassification"]);
                        }
                        //Construye el objeto con el nodo que se esta intentando modificar (Origen)
                        Condesus.EMS.Business.KC.Entities.ResourceClassification _resourceClassSource = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idResourceClassification);
                        //Construye el objeto con el nodo destino o en Cero(0) en caso de ser el root.
                        Condesus.EMS.Business.KC.Entities.ResourceClassification _resourceClassParent = EMSLibrary.User.KnowledgeCollaboration.Map.ResourceClassification(_idParentValue);

                        //Finalmente modifica el padre del elemento origen.
                        _resourceClassSource.Move(_resourceClassParent);

                    }
                #endregion
            #endregion

            #region Improvement Action
                #region Project Classification
                    public void ProjectClassificationMove(Dictionary<String, Object> paramSource, Dictionary<String, Object> paramDestination)
                    {
                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idProjectClassification = Convert.ToInt64(paramSource["IdProjectClassification"]);
                        Int64 _idParentValue = 0; //Si es root, queda 0
                        //Verifica cual es el nodo Destino
                        //Si el destino es al ROOT, no pasa pasa por aca entonces el id queda en 0
                        //if (_pkValueParent != "NodeRootTitle")
                        if (paramDestination.ContainsKey("IdProjectClassification"))
                        {
                            //El destino es otra classificacion, entonces busca el id.
                            _idParentValue = Convert.ToInt64(paramDestination["IdProjectClassification"]);
                        }
                        //Construye el objeto con el nodo que se esta intentando modificar (Origen)
                        Condesus.EMS.Business.IA.Entities.ProjectClassification _projectClassSource = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_idProjectClassification);
                        //Construye el objeto con el nodo destino o en Cero(0) en caso de ser el root.
                        Condesus.EMS.Business.IA.Entities.ProjectClassification _projectClassParent = EMSLibrary.User.ImprovementAction.Map.ProjectClassification(_idParentValue);

                        //Finalmente modifica el padre del elemento origen.
                        _projectClassSource.Move(_projectClassParent);

                    }
                #endregion
            #endregion

            #region Risk & Potencial
                #region Risk Classification
                    public void RiskClassificationMove(Dictionary<String, Object> paramSource, Dictionary<String, Object> paramDestination)
                    {
                        //Ya esta el DataTable armado, ahora se trae el item.
                        Int64 _idRiskClassification = Convert.ToInt64(paramSource["IdRiskClassification"]);
                        Int64 _idParentValue = 0; //Si es root, queda 0
                        //Verifica cual es el nodo Destino
                        //Si el destino es al ROOT, no pasa pasa por aca entonces el id queda en 0
                        //if (_pkValueParent != "NodeRootTitle")
                        if (paramDestination.ContainsKey("IdRiskClassification"))
                        {
                            //El destino es otra classificacion, entonces busca el id.
                            _idParentValue = Convert.ToInt64(paramDestination["IdRiskClassification"]);
                        }
                        //Construye el objeto con el nodo que se esta intentando modificar (Origen)
                        Condesus.EMS.Business.RM.Entities.RiskClassification _riskClassSource = EMSLibrary.User.RiskManagement.Map.RiskClassification(_idRiskClassification);
                        //Construye el objeto con el nodo destino o en Cero(0) en caso de ser el root.
                        Condesus.EMS.Business.RM.Entities.RiskClassification _riskClassParent = EMSLibrary.User.RiskManagement.Map.RiskClassification(_idParentValue);

                        //Finalmente modifica el padre del elemento origen.
                        _riskClassSource.Move(_riskClassParent);

                    }
                #endregion
            #endregion

        #endregion

    }
}
