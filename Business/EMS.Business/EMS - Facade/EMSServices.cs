using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business
{
    public class EMSServices
    {
        #region Internal Properties
            private Credential _Credential;            
        #endregion

            #region Public Properties (Facades of System)

            public NT.Notifications Notifications
            {
                get { return new NT.Notifications(_Credential); }
            }

            public PA.Transformations Transformations
            {
                get { return new PA.Transformations(_Credential); }
            }
            /// <summary>
            /// pone la tareas en over due
            /// </summary>
            public void ExceptionAutomaticAlert()
            {
                new IA.ImprovementAction(_Credential).ExceptionAutomaticAlert();
            }

            //Delete All Errors
            public void DeleteAllErrors()
            {
                new PA.Collections.CalculateOfTransformationErrors().DeleteAll(); 

            }

            //flaguea las tareas y las mediciones si tienen exepciones
            public void OverdueInTaskAndMeasurements()
            {
                new DataAccess.Entities.Service().OverdueInTaskAndMeasurements();
            }
            #endregion

            internal EMSServices(Credential credential)
            {
                _Credential = credential;
            }
    }
}
