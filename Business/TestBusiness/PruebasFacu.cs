using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Condesus.EMS.Business.DS.Entities;
using Condesus.EMS.Business.PA.Entities;
using Condesus.EMS.Business.KC.Entities;
using Condesus.EMS.Business.GIS.Entities;
using Condesus.EMS.Business.DS.Collections;
using Condesus.EMS.Business.PA.Collections;
using Condesus.EMS.Business.KC.Collections;
using Condesus.EMS.Business.GIS.Collections;

namespace TestBusiness
{
    public class PruebasFacu
    {
        public Condesus.EMS.Business.EMS _EMS;

        internal PruebasFacu()
        {
            _EMS = new Condesus.EMS.Business.EMS("ems", "ems", "es-AR", "127.0.0.1");
        }

        public void Ejecutar()
        {
            //Zona de pruebas

            TestMethodologies();

            //String espero_aca = "";
        }

        private void TestConstantClassifitactions()
        {

            //Condesus.EMS.Business.PA.Entities.ConstantClassification _constantClassificationparent = _EMS.User.PerformanceAssessments.Configuration.ConstantClassification(3);

            //Condesus.EMS.Business.PA.Entities.ConstantClassification _constantClassification = _EMS.User.PerformanceAssessments.Configuration.ConstantClassificationAdd(_constantClassificationparent, "Test 4 Constant Clasif", "Testeamos el alta de constant Clasif 4");

            //_constantClassification.Modify(null, "update de Test 2", "Probamos el update de Test 2");

            //_EMS.User.PerformanceAssessments.Configuration.ConstantClassificationRemove(_constantClassification);

            //foreach (ConstantClassification _cc in _EMS.User.PerformanceAssessments.Configuration.ConstantClassifications().Values)
            //{
            //    MessageBox.Show(_cc.LanguageOption.Name);
            //}

            String espero_aca = "";
                    
        }

        private void TestConstant()
        {
            //Dictionary<Int64, ConstantClassification> _ConstantClasifications = new Dictionary<long, ConstantClassification>();            

            //Condesus.EMS.Business.PA.Entities.ConstantClassification _constantClassification = _EMS.User.PerformanceAssessments.Configuration.ConstantClassification(3);

            //_ConstantClasifications.Add(_constantClassification.IdConstantClassification, _constantClassification); 

            //Condesus.EMS.Business.PA.Entities.Constant _constant = _EMS.User.PerformanceAssessments.Configuration.ConstantAdd("+",4.67,1,"Test Constant 1", "Testeando Constante 1",_ConstantClasifications);

            //_constant.Modify("++", 4.80, 1, "Test Update Constant 1", "Test Update Constante 1", _ConstantClasifications);

            //_constant = _EMS.User.PerformanceAssessments.Configuration.ConstantAdd("--", 6.99, 1, "Test Constant 2", "Testeando Constante 2", _ConstantClasifications);

            //_constant = _EMS.User.PerformanceAssessments.Configuration.ConstantAdd("++", 2.37, 1, "Test Constant 3", "Testeando Constante 3", _ConstantClasifications);

            //_EMS.User.PerformanceAssessments.Configuration.ConstantRemove(_constant);

            //foreach (Constant _c in _EMS.User.PerformanceAssessments.Configuration.Constants().Values)
            //{
            //    MessageBox.Show(_c.LanguageOption.Name);
            //}

            //String espero_aca = "";


        }

        private void TestFacilityTypes()
        {

        #region Test ADD
        // Test ADD
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //Dictionary<Int64, FacilityType> _FacilityTypes = new Dictionary<long, FacilityType>();        

        //FacilityType _facilitytype = _EMS.User.GeographicInformationSystem.FacilityTypeAdd("Test Facility Type", "Probando alta de Facility Type");

        //_facilitytype = _EMS.User.GeographicInformationSystem.FacilityTypeAdd("Test Facility Type 2", "Probando alta de Facility Type 2");

        

        #region Test LG
        //// Test Languages Add
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //FacilityType _facilitytypeLgAdd = _EMS.User.GeographicInformationSystem.FacilityType(2);
            //Language _lg = _EMS.User.DirectoryServices.Configuration.Language("es-AR");
            //_facilitytypeLgAdd.LanguageCreate(_lg, "probamos facility Type lg AR", "probamos facility Type lg AR");
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //// Test Languages Add
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //FacilityType _facilitytypeLgUpd = _EMS.User.GeographicInformationSystem.FacilityType(2);
            //Language _lg = _EMS.User.DirectoryServices.Configuration.Language("es-AR");
            //_facilitytypeLgUpd.LanguageModify(_lg, "probamos facility Type lg AR modificado", "probamos facility Type lg AR modificado");
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //// Test Languages Add
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //FacilityType _facilitytypeLgDel = _EMS.User.GeographicInformationSystem.FacilityType(2);
            //Language _lg = _EMS.User.DirectoryServices.Configuration.Language("es-AR");
            //_facilitytypeLgDel.LanguageRemove(_lg);
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #endregion

        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #endregion

        #region Test UPDATE
        //// Test UPDATE
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //FacilityType _facilitytypeUpdate = _EMS.User.GeographicInformationSystem.FacilityType(2);
            //_facilitytypeUpdate.Modify("Test UPDATE Facility Type 2 1", "Modificando el FacilityType 2 1");

        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #endregion

        #region Test Collection
        //// Test Collection
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //foreach (FacilityType _facilityTypecol in _EMS.User.GeographicInformationSystem.FacilityTypes().Values)
            //{
            //    MessageBox.Show(_facilityTypecol.LanguageOption.Name.ToString());
            //}

        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #endregion

        #region Test Delete
        //// Test DELETE
        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //FacilityType _facilitytypeDelete = _EMS.User.GeographicInformationSystem.FacilityType(2);
            //_EMS.User.GeographicInformationSystem.Remove(_facilitytypeDelete);

        ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        #endregion

        MessageBox.Show("Revisar Base");

        }

        private void TestQualities()
        {

            #region Test ADD
            //// Test ADD
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //Dictionary<Int64, Quality> _Qualities = new Dictionary<long, Quality>();        

            //Quality _quality = _EMS.User.PerformanceAssessments.Configuration.QualityAdd("Test Quality 1", "Probando alta de Quality 1");

            //_quality = _EMS.User.PerformanceAssessments.Configuration.QualityAdd("Test Quality 2", "Probando alta de Quality 2");

            //_quality = _EMS.User.PerformanceAssessments.Configuration.QualityAdd("Test Quality 3", "Probando alta de Quality 3");

            #region Test LG
            //// Test Languages Add
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Quality _QualityLgAdd = _EMS.User.PerformanceAssessments.Configuration.Quality(1);
            //Language _lg = _EMS.User.DirectoryServices.Configuration.Language("es-AR");
            //_QualityLgAdd.LanguageCreate(_lg, "probamos facility Type lg AR", "probamos facility Type lg AR");
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //// Test Languages Add
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Quality _QualityLgUpd = _EMS.User.PerformanceAssessments.Configuration.Quality(1);
            //Language _lg = _EMS.User.DirectoryServices.Configuration.Language("es-AR");
            //_QualityLgUpd.LanguageModify(_lg, "probamos facility Type lg AR modificado", "probamos facility Type lg AR modificado");
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //// Test Languages Add
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Quality _QualityLgDel = _EMS.User.PerformanceAssessments.Configuration.Quality(1);
            //Language _lg = _EMS.User.DirectoryServices.Configuration.Language("es-AR");
            //_QualityLgDel.LanguageRemove(_lg);
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            #endregion

            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            #endregion

            #region Test UPDATE
            //// Test UPDATE
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //Quality _QualityUpdate = _EMS.User.PerformanceAssessments.Configuration.Quality(1);
            //_QualityUpdate.Modify("Test UPDATE Quality 1", "Modificando el Quality 1");

            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            #endregion

            #region Test Collection
            //// Test Collection
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //foreach (Quality _Qualitycol in _EMS.User.PerformanceAssessments.Configuration.Qualities().Values)
            //{
            //    MessageBox.Show(_Qualitycol.LanguageOption.Name.ToString());
            //}

            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            #endregion

            #region Test Delete
            //// Test DELETE
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //Quality _QualityDelete = _EMS.User.PerformanceAssessments.Configuration.Quality(1);
            //_EMS.User.PerformanceAssessments.Configuration.Remove(_QualityDelete);

            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            #endregion

            MessageBox.Show("Revisar Base");

        }

        private void TestMethodologies()
        {

            #region Test ADD
            // Test ADD
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //Dictionary<Int64, Methodology> _Methodologies = new Dictionary<long, Methodology>();

            //Resource _resource = null;

            //Methodology _Methodology = _EMS.User.PerformanceAssessments.Configuration.MethodologyAdd(_resource, "Test Name Methodology 1", "Test Type Methodology 1", "Probando alta de Methodology 1");

            //_Methodology = _EMS.User.PerformanceAssessments.Configuration.MethodologyAdd(_resource, "Test Name Methodology 1", "Test Type Methodology 1", "Probando alta de Methodology 1");

            //_Methodology = _EMS.User.PerformanceAssessments.Configuration.MethodologyAdd(_resource, "Test Name Methodology 1", "Test Type Methodology 1", "Probando alta de Methodology 1");

            #region Test LG
            //// Test Languages Add
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Methodology _MethodologyLgAdd = _EMS.User.PerformanceAssessments.Configuration.Methodology(1);
            //Language _lg = _EMS.User.DirectoryServices.Configuration.Language("es-AR");
            //_MethodologyLgAdd.LanguageCreate(_lg, "probamos methodology name lg AR", "probamos methodology Type lg AR", "probamos methodology desc Type lg AR");
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //// Test Languages Add
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Methodology _MethodologyLgUpd = _EMS.User.PerformanceAssessments.Configuration.Methodology(1);
            //Language _lg = _EMS.User.DirectoryServices.Configuration.Language("es-AR");
            //_MethodologyLgUpd.LanguageModify(_lg, "probamos modif methodology name lg AR", "probamos modif methodology Type lg AR", "probamos modif methodology desc Type lg AR");
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //// Test Languages Add
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //Methodology _MethodologyLgDel = _EMS.User.PerformanceAssessments.Configuration.Methodology(1);
            //Language _lg = _EMS.User.DirectoryServices.Configuration.Language("es-AR");
            //_MethodologyLgDel.LanguageRemove(_lg);
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            #endregion

            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            #endregion

            #region Test UPDATE
            //// Test UPDATE
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //Resource _resource=null;
            //Methodology _MethodologyUpdate = _EMS.User.PerformanceAssessments.Configuration.Methodology(1);

            //_MethodologyUpdate.Modify(_resource, "Test UPDATE methodology name 1", "Test UPDATE methodology Type 1", "UPDATE el methodology desc 1");

            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            #endregion

            #region Test Collection
            //// Test Collection
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //foreach (Methodology _Methodologycol in _EMS.User.PerformanceAssessments.Configuration.Methodologies().Values)
            //{
            //    MessageBox.Show(_Methodologycol.LanguageOption.MethodName.ToString());
            //}

            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            #endregion

            #region Test Delete
            //// Test DELETE
            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //Methodology _MethodologyDelete = _EMS.User.PerformanceAssessments.Configuration.Methodology(1);
            //_EMS.User.PerformanceAssessments.Configuration.Remove(_MethodologyDelete);

            ////++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            #endregion

            MessageBox.Show("Revisar Base");

        }


    }
}
