using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Condesus.EMS.WebUI.Business
{
    public class ViewerEntityRelated : Base
    {
        #region Internal Properties
        #endregion

        public ViewerEntityRelated(String commandName)
        {
            _CommandName = commandName;
        }
        public ViewerEntityRelated Create(String commandName)
        {
            return new ViewerEntityRelated(commandName);
        }

        #region Private Methods
        #endregion

        #region Public Methods (Aca estan los metodos que via Reflection nos permite evitar el SWITCH)

        #region Directory Service
        public String[,] Organization(Dictionary<String, Object> param)
        {
            String[,] _entityNames = new String[3, 5]; //Entidad, Class, ClassSelected, titulo, Entidad Simple

            _entityNames[0, 0] = Common.ConstantsEntitiesName.PF.ProcessParticipations;
            _entityNames[0, 1] = "contentListViewProcessParticipations";   //Class
            _entityNames[0, 2] = "contentListViewProcessParticipationsOpen";   //Selected Class
            _entityNames[0, 3] = Resources.CommonListManage.ProcessParticipation;   //Titulo
            _entityNames[0, 4] = Common.ConstantsEntitiesName.PF.ProcessParticipation;
            _entityNames[1, 0] = Common.ConstantsEntitiesName.DS.Posts;
            _entityNames[1, 1] = "contentListViewPosts";   //Class
            _entityNames[1, 2] = "contentListViewPostsOpen";   //Selected Class
            _entityNames[1, 3] = Resources.CommonListManage.Posts; //Titulo
            _entityNames[1, 4] = Common.ConstantsEntitiesName.DS.Post;
            _entityNames[2, 0] = Common.ConstantsEntitiesName.DS.Users;
            _entityNames[2, 1] = "contentListViewUsers";   //Class
            _entityNames[2, 2] = "contentListViewUsersOpen";   //Selected Class
            _entityNames[2, 3] = Resources.CommonListManage.Users; //Titulo
            _entityNames[2, 4] = Common.ConstantsEntitiesName.DS.User;

            return _entityNames;
        }
        public String[,] Person(Dictionary<String, Object> param)
        {
            String[,] _entityNames = new String[2, 5]; //Entidad, Class, ClassSelected, titulo, Entidad simple

            _entityNames[0, 0] = Common.ConstantsEntitiesName.DS.Users;
            _entityNames[0, 1] = "contentListViewReportImages";   //Class
            _entityNames[0, 2] = "contentListViewReportImagesOpen";   //Selected Class
            _entityNames[0, 3] = Resources.CommonListManage.Users;   //Titulo
            _entityNames[0, 4] = Common.ConstantsEntitiesName.DS.User;
            _entityNames[1, 0] = Common.ConstantsEntitiesName.DS.Posts;
            _entityNames[1, 1] = "contentListViewPosts";   //Class
            _entityNames[1, 2] = "contentListViewPostsOpen";   //Selected Class
            _entityNames[1, 3] = Resources.CommonListManage.Posts; //Titulo
            _entityNames[1, 4] = Common.ConstantsEntitiesName.DS.Post;

            return _entityNames;
        }
        public String[,] Facility(Dictionary<String, Object> param)
        {
            String[,] _entityNames = new String[2, 5]; //Entidad, Class, ClassSelected, titulo, Entidad simple

            _entityNames[0, 0] = Common.ConstantsEntitiesName.DS.Addresses;
            _entityNames[0, 1] = "contentListViewAddress";   //Class
            _entityNames[0, 2] = "contentListViewAddressOpen";   //Selected Class
            _entityNames[0, 3] = Resources.CommonListManage.Addresses;   //Titulo
            _entityNames[0, 4] = Common.ConstantsEntitiesName.DS.Address;
            _entityNames[1, 0] = Common.ConstantsEntitiesName.DS.Telephones;
            _entityNames[1, 1] = "contentListViewTelephone";   //Class
            _entityNames[1, 2] = "contentListViewTelephoneOpen";   //Selected Class
            _entityNames[1, 3] = Resources.CommonListManage.Telephones;   //Titulo
            _entityNames[1, 4] = Common.ConstantsEntitiesName.DS.Telephone;

            return _entityNames;
        }
        public String[,] Sector(Dictionary<String, Object> param)
        {
            String[,] _entityNames = new String[2, 5]; //Entidad, Class, ClassSelected, titulo, Entidad simple

            _entityNames[0, 0] = Common.ConstantsEntitiesName.DS.Addresses;
            _entityNames[0, 1] = "contentListViewAddress";   //Class
            _entityNames[0, 2] = "contentListViewAddressOpen";   //Selected Class
            _entityNames[0, 3] = Resources.CommonListManage.Addresses;   //Titulo
            _entityNames[0, 4] = Common.ConstantsEntitiesName.DS.Address;
            _entityNames[1, 0] = Common.ConstantsEntitiesName.DS.Telephones;
            _entityNames[1, 1] = "contentListViewTelephone";   //Class
            _entityNames[1, 2] = "contentListViewTelephoneOpen";   //Selected Class
            _entityNames[1, 3] = Resources.CommonListManage.Telephones;   //Titulo
            _entityNames[1, 4] = Common.ConstantsEntitiesName.DS.Telephone;

            return _entityNames;
        }
        public String[,] FacilityType(Dictionary<String, Object> param)
        {
            String[,] _entityNames = new String[1, 5]; //Entidad, Class, ClassSelected, titulo, Entidad simple

            _entityNames[0, 0] = Common.ConstantsEntitiesName.DS.Facilities;
            _entityNames[0, 1] = "contentListViewFacility";   //Class
            _entityNames[0, 2] = "contentListViewFacilityOpen";   //Selected Class
            _entityNames[0, 3] = Resources.CommonListManage.Facilities;   //Titulo
            _entityNames[0, 4] = Common.ConstantsEntitiesName.DS.Facility; //Entidad simple

            return _entityNames;
        }
        #endregion

        #region Process Framework
        public String[,] ProcessGroupProcess(Dictionary<String, Object> param)
        {
            String[,] _entityNames = new String[6, 5]; //Entidad, Class, ClassSelected, titulo, entidad simple

            _entityNames[0, 0] = Common.ConstantsEntitiesName.PF.ProcessClassifications;
            _entityNames[0, 1] = "contentListViewClassifications";   //Class
            _entityNames[0, 2] = "contentListViewClassificationsOpen";   //Selected Class
            _entityNames[0, 3] = Resources.CommonListManage.ProcessClassification;   //Titulo
            _entityNames[0, 4] = Common.ConstantsEntitiesName.PF.ProcessClassification;
            _entityNames[1, 0] = Common.ConstantsEntitiesName.PF.ProcessExtendedProperties;
            _entityNames[1, 1] = "contentListViewProcessExtendedProperties";   //Class
            _entityNames[1, 2] = "contentListViewProcessExtendedPropertiesOpen";   //Selected Class
            _entityNames[1, 3] = Resources.CommonListManage.ProcessExtendedProperty; //Titulo
            _entityNames[1, 4] = Common.ConstantsEntitiesName.PF.ProcessExtendedProperty;
            _entityNames[2, 0] = Common.ConstantsEntitiesName.PF.ProcessParticipations;
            _entityNames[2, 1] = "contentListViewProcessParticipations";   //Class
            _entityNames[2, 2] = "contentListViewProcessParticipationsOpen";   //Selected Class
            _entityNames[2, 3] = Resources.CommonListManage.ProcessParticipation; //Titulo
            _entityNames[2, 4] = Common.ConstantsEntitiesName.PF.ProcessParticipation;
            _entityNames[3, 0] = Common.ConstantsEntitiesName.PA.KeyIndicators;
            _entityNames[3, 1] = "contentListViewKeyIndicators";   //Class
            _entityNames[3, 2] = "contentListViewKeyIndicatorsOpen";   //Selected Class
            _entityNames[3, 3] = Resources.CommonListManage.KeyParameters; //Titulo
            _entityNames[3, 4] = Common.ConstantsEntitiesName.PA.KeyIndicator;
            _entityNames[4, 0] = Common.ConstantsEntitiesName.PA.Calculations;
            _entityNames[4, 1] = "contentListViewCalculations";   //Class
            _entityNames[4, 2] = "contentListViewCalculationsOpen";   //Selected Class
            _entityNames[4, 3] = Resources.CommonListManage.Calculation; //Titulo
            _entityNames[4, 4] = Common.ConstantsEntitiesName.PA.Calculation;
            _entityNames[5, 0] = Common.ConstantsEntitiesName.PF.ProcessResources;
            _entityNames[5, 1] = "contentListViewProcessResources";   //Class
            _entityNames[5, 2] = "contentListViewProcessResourcesOpen";   //Selected Class
            _entityNames[5, 3] = Resources.CommonListManage.ProcessResource; //Titulo
            _entityNames[5, 4] = Common.ConstantsEntitiesName.PF.ProcessResource;

            return _entityNames;
        }
        public String[,] ProcessTaskMeasurement(Dictionary<String, Object> param)
        {
            String[,] _entityNames = new String[4, 5]; //Entidad, Class, ClassSelected, titulo, Entidad simple

            _entityNames[0, 0] = Common.ConstantsEntitiesName.PA.StatisticsOfMeasurements;
            _entityNames[0, 1] = "contentListViewStats";   //Class
            _entityNames[0, 2] = "contentListViewStatsOpen";   //Selected Class
            _entityNames[0, 3] = Resources.CommonListManage.Statistics;   //Titulo
            _entityNames[0, 4] = Common.ConstantsEntitiesName.PA.MeasurementStatistical;

            _entityNames[1, 0] = Common.ConstantsEntitiesName.PA.MeasurementDataSeries;
            _entityNames[1, 1] = "contentListViewDataSeries";   //Class
            _entityNames[1, 2] = "contentListViewDataSeriesOpen";   //Selected Class
            _entityNames[1, 3] = Resources.Common.DataSeries;   //Titulo
            _entityNames[1, 4] = Common.ConstantsEntitiesName.PA.Measurement;

            _entityNames[2, 0] = Common.ConstantsEntitiesName.IA.Exceptions;
            _entityNames[2, 1] = "contentListViewExceptions";   //Class
            _entityNames[2, 2] = "contentListViewExceptionsOpen";   //Selected Class
            _entityNames[2, 3] = Resources.CommonListManage.Exceptions;   //Titulo
            _entityNames[2, 4] = Common.ConstantsEntitiesName.IA.Exception;

            _entityNames[3, 0] = Common.ConstantsEntitiesName.PF.ProcessTaskExecutions;
            _entityNames[3, 1] = "contentListViewCalculations";   //Class
            _entityNames[3, 2] = "contentListViewCalculationsOpen";   //Selected Class
            _entityNames[3, 3] = Resources.CommonListManage.ProcessTaskExecution;   //Titulo
            _entityNames[3, 4] = Common.ConstantsEntitiesName.PF.ProcessTaskExecution;

            return _entityNames;
        }
        #endregion

        #region Performance Assessment
        public String[,] Measurement(Dictionary<String, Object> param)
        {   
            String[,] _entityNames = new String[4, 5]; //Entidad, Class, ClassSelected, titulo, Entidad simple

            _entityNames[0, 0] = Common.ConstantsEntitiesName.PA.StatisticsOfMeasurements;
            _entityNames[0, 1] = "contentListViewStats";   //Class
            _entityNames[0, 2] = "contentListViewStatsOpen";   //Selected Class
            _entityNames[0, 3] = Resources.CommonListManage.Statistics;   //Titulo
            _entityNames[0, 4] = Common.ConstantsEntitiesName.PA.MeasurementStatistical;

            _entityNames[1, 0] = Common.ConstantsEntitiesName.PA.MeasurementDataSeries;
            _entityNames[1, 1] = "contentListViewDataSeries";   //Class
            _entityNames[1, 2] = "contentListViewDataSeriesOpen";   //Selected Class
            _entityNames[1, 3] = Resources.Common.DataSeries;   //Titulo
            _entityNames[1, 4] = Common.ConstantsEntitiesName.PA.Measurement;

            _entityNames[2, 0] = Common.ConstantsEntitiesName.IA.Exceptions;
            _entityNames[2, 1] = "contentListViewExceptions";   //Class
            _entityNames[2, 2] = "contentListViewExceptionsOpen";   //Selected Class
            _entityNames[2, 3] = Resources.CommonListManage.Exceptions;   //Titulo
            _entityNames[2, 4] = Common.ConstantsEntitiesName.IA.Exception;

            _entityNames[3, 0] = Common.ConstantsEntitiesName.PF.ProcessTaskExecutions;
            _entityNames[3, 1] = "contentListViewCalculations";   //Class
            _entityNames[3, 2] = "contentListViewCalculationsOpen";   //Selected Class
            _entityNames[3, 3] = Resources.CommonListManage.ProcessTaskExecution;   //Titulo
            _entityNames[3, 4] = Common.ConstantsEntitiesName.PF.ProcessTaskExecution;

            return _entityNames;
        }
        public String[,] KeyIndicator(Dictionary<String, Object> param)
        {
            return Measurement(param);
        }
        public String[,] Transformation(Dictionary<String, Object> param)
        {
            String[,] _entityNames = new String[1, 5]; //Entidad, Class, ClassSelected, titulo, Entidad simple

            _entityNames[0, 0] = Common.ConstantsEntitiesName.PA.TransformationDataSeries;
            _entityNames[0, 1] = "contentListViewDataSeries";   //Class
            _entityNames[0, 2] = "contentListViewDataSeriesOpen";   //Selected Class
            _entityNames[0, 3] = Resources.Common.DataSeries;   //Titulo
            _entityNames[0, 4] = Common.ConstantsEntitiesName.PA.Measurement;

            return _entityNames;
        }
        public String[,] TransformationByTransformation(Dictionary<String, Object> param)
        {
            String[,] _entityNames = new String[1, 5]; //Entidad, Class, ClassSelected, titulo, Entidad simple

            _entityNames[0, 0] = Common.ConstantsEntitiesName.PA.TransformationDataSeries;
            _entityNames[0, 1] = "contentListViewDataSeries";   //Class
            _entityNames[0, 2] = "contentListViewDataSeriesOpen";   //Selected Class
            _entityNames[0, 3] = Resources.Common.DataSeries;   //Titulo
            _entityNames[0, 4] = Common.ConstantsEntitiesName.PA.Measurement;

            return _entityNames;
        }
        #endregion

        #region Improvement Action
        #endregion

        #region Knowledge Collaboration
        #endregion

        #region Risk & Potencial
        #endregion

        #endregion

    }
}
