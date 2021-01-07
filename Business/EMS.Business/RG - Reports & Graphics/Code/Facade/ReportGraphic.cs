using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Data;

namespace Condesus.EMS.Business.RG
{
    public class ReportGraphic
    {
        private Credential _Credential;
        private PF.Entities.ProcessGroupProcess _ProcessGroupProcess;
        private Condesus.EMS.Business.RG.Entities.Report_GA_S_A_FT_F _Report_GA_S_A_FT_F;
        private Condesus.EMS.Business.RG.Entities.Report_O_S_A_FT_F _Report_O_S_A_FT_F;
        private Condesus.EMS.Business.RG.Entities.Report_GA_FT_F_S_A _Report_GA_FT_F_S_A;
        private Condesus.EMS.Business.RG.Entities.Report_S_GA_A_FT_F _Report_S_GA_A_FT_F;

        private Condesus.EMS.Business.RG.Entities.Report_FT_F_S_A _Report_FT_F_S_A;
        private Condesus.EMS.Business.RG.Entities.Report_S_A_FT_F _Report_S_A_FT_F;

        private Condesus.EMS.Business.RG.Entities.Graphic_Pie _Graphic_Pie;
        private Condesus.EMS.Business.RG.Entities.Graphic_Bar _Graphic_Bar;

        internal ReportGraphic(PF.Entities.ProcessGroupProcess processGroupProcess)
        { 
            _Credential = processGroupProcess.Credential;
            _ProcessGroupProcess = processGroupProcess;
        }
        internal ReportGraphic(DS.Entities.User user)
        {
            _Credential = user.Credential;
        }

        #region Reports
        internal Entities.Report_GA_S_A_FT_F Report_GA_S_A_FT_F
        {
            get
            {
                if(_Report_GA_S_A_FT_F == null)
                { _Report_GA_S_A_FT_F = new Condesus.EMS.Business.RG.Entities.Report_GA_S_A_FT_F(_Credential, _ProcessGroupProcess);}
                return _Report_GA_S_A_FT_F;
            }
            
        }
        internal Entities.Report_O_S_A_FT_F Report_O_S_A_FT_F
        {
            get
            {
                if (_Report_O_S_A_FT_F == null)
                { _Report_O_S_A_FT_F = new Condesus.EMS.Business.RG.Entities.Report_O_S_A_FT_F(_Credential, _ProcessGroupProcess); }
                return _Report_O_S_A_FT_F;
            }

        }
        internal Entities.Report_GA_FT_F_S_A Report_GA_FT_F_S_A
        {
            get
            {
                if (_Report_GA_FT_F_S_A == null)
                { _Report_GA_FT_F_S_A = new Condesus.EMS.Business.RG.Entities.Report_GA_FT_F_S_A(_Credential, _ProcessGroupProcess); }
                return _Report_GA_FT_F_S_A;
            }            
        }
        internal Entities.Report_S_GA_A_FT_F Report_S_GA_A_FT_F
        {
            get
            {
                if (_Report_S_GA_A_FT_F == null)
                { _Report_S_GA_A_FT_F = new Condesus.EMS.Business.RG.Entities.Report_S_GA_A_FT_F(_Credential, _ProcessGroupProcess); }
                return _Report_S_GA_A_FT_F;
            }            
        }

        internal Entities.Report_FT_F_S_A Report_FT_F_S_A
        {
            get
            {
                if (_Report_FT_F_S_A == null)
                { _Report_FT_F_S_A = new Condesus.EMS.Business.RG.Entities.Report_FT_F_S_A(_Credential, _ProcessGroupProcess); }
                return _Report_FT_F_S_A;
            }
        }
        internal Entities.Report_S_A_FT_F Report_S_A_FT_F
        {
            get
            {
                if (_Report_S_A_FT_F == null)
                { _Report_S_A_FT_F = new Condesus.EMS.Business.RG.Entities.Report_S_A_FT_F(_Credential, _ProcessGroupProcess); }
                return _Report_S_A_FT_F;
            }
        }

        internal DataTable Report_A_F_by_I_COL(Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
            Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
            Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
            Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S,
            Int64 idIndicatorPM, Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {

           return new Condesus.EMS.Business.RG.Collections.Report_A_F_by_I_COL(_ProcessGroupProcess, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,
           idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,
           idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,
           idIndicatorPM, idIndicatorPM10, startDate, endDate).DTResults; 
            

        }

        internal Entities.FacilitiesByIndicators FacilitiesByIndicators ()
        {
            return new Condesus.EMS.Business.RG.Entities.FacilitiesByIndicators(_ProcessGroupProcess);
        }

        internal DataTable MultiObservatory(DataTable dtTVPProcessFilter, DateTime starDate, DateTime endDate)
        {
            return new Condesus.EMS.Business.RG.Collections.MultiObservatory(_Credential).Result(dtTVPProcessFilter, starDate, endDate);
        }
        #endregion

        #region Evolutionary
        internal List<Entities.Evolutionary_O> EvolutionaryItems(Int64 IdScope, Int64 idIndicatortCO2e, Int64 idIndicatorCO2,
            Int64 idIndicatorCH4, Int64 idIndicatortnN2O, Int64 idIndicatorPFC, Int64 idIndicatorHFC, Int64 idIndicatorSF6,
            Int64 idIndicatorHCT, Int64 idIndicatorHCNM, Int64 idIndicatorC2H6, Int64 idIndicatorC3H8, Int64 idIndicatorC4H10,
            Int64 idIndicatorCO, Int64 idIndicatorNOx, Int64 idIndicatorSOx, Int64 idIndicatorSO2, Int64 idIndicatorH2S,
            Int64 idIndicatorPM, Int64 idIndicatorPM10, DateTime startDate, DateTime endDate)
        {
            return new Condesus.EMS.Business.RG.Collections.Evolutionary(_ProcessGroupProcess, idIndicatortCO2e, idIndicatorCO2, idIndicatorCH4,
                idIndicatortnN2O, idIndicatorPFC, idIndicatorHFC, idIndicatorSF6, idIndicatorHCT, idIndicatorHCNM, idIndicatorC2H6,
                idIndicatorC3H8, idIndicatorC4H10, idIndicatorCO, idIndicatorNOx, idIndicatorSOx, idIndicatorSO2, idIndicatorH2S,
                idIndicatorPM, idIndicatorPM10, startDate, endDate).Items(IdScope);
        }
        #endregion

        #region Graphics
        internal Entities.Graphic_Pie Graphic_Pie
        {
            get
            {
                if (_Graphic_Pie == null)
                { _Graphic_Pie = new Condesus.EMS.Business.RG.Entities.Graphic_Pie(_Credential, _ProcessGroupProcess); }
                return _Graphic_Pie;
            }
        }
       
        internal Entities.Graphic_Bar Graphic_Bar
        {
            get
            {
                if (_Graphic_Bar == null)
                { _Graphic_Bar = new Condesus.EMS.Business.RG.Entities.Graphic_Bar(_Credential, _ProcessGroupProcess); }
                return _Graphic_Bar;
            }
        }

        #endregion
    }
}
