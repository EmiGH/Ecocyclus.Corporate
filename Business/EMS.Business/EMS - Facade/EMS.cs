using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Globalization;
using Condesus.EMS.Business.Security;
using ciloci.FormulaEngine;

namespace Condesus.EMS.Business
{
    /// <summary>
    /// Clase facade de toda la solución
    /// <para>Unico acceso a los objetos de bussines layer</para>
    /// </summary>
    public class EMS
    {
        #region Internal Properties
            //Idioma por defecto de la solucion
            private Condesus.EMS.Business.DS.Entities.Language _DefaultLanguage;
            private Credential _Credential;            
        #endregion

        #region Public Properties (Facades of System)
            public DS.Entities.User User
            {
                get { return _Credential.User; }
            }

            public EMSServices EMSServices
            {
                get { return new EMSServices(_Credential); }
            }
            #region GIS
            //TODO: Borrar este parche
            //Objeto gis
            public class GIS
            {
                #region private
                private String _ClassName;
                public String ClassName
                {
                    get { return _ClassName; }
                }
                
                private Int64 _IdObject;
                public Int64 IdObject
                {
                    get { return _IdObject; }
                }
                
                private String _GeographicalData;
                public String GeographicalData
                {
                    get { return _GeographicalData; }
                }
                
                private String _ShapeType;
                public String ShapeType
                {
                    get { return _ShapeType; }
                }
                
                private Int64 _ZoomLevel;
                public Int64 ZoomLevel
                {
                    get { return _ZoomLevel; }
                }
                #endregion

                internal GIS(String className,Int64 idObject,String geographicalData,String shapeType,Int64 zoomLevel) 
                {
                    _ClassName = className;
                    _IdObject = idObject;
                    _GeographicalData = geographicalData;
                    _ShapeType = shapeType;
                    _ZoomLevel = zoomLevel;
                }
            }
            public GIS GisReadByID(String className, Int64 idObject)
            {

                IEnumerable<System.Data.Common.DbDataRecord> _record = new DataAccess.GIS.Entities.GeographicalData().ReadByID(className, idObject);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    return new GIS(className, idObject, Convert.ToString(_dbRecord["GeographicalData"]), Convert.ToString(_dbRecord["ShapeType"]), Convert.ToInt64(_dbRecord["ZoomLevel"]));
                }
                return null;
            }
            public List<GIS> GisReadAll()
            {
                List<GIS> _Items = new List<GIS>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = new DataAccess.GIS.Entities.GeographicalData().ReadAll();
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    GIS _item = new GIS(Convert.ToString(_dbRecord["ClassName"]), Convert.ToInt64(_dbRecord["IdObject"]), Convert.ToString(_dbRecord["GeographicalData"]), Convert.ToString(_dbRecord["ShapeType"]), Convert.ToInt64(_dbRecord["ZoomLevel"]));
                    _Items.Add(_item);
                }
                return _Items;
            }
            public List<GIS> GisReadByClass(String className)
            {
                List<GIS> _Items = new List<GIS>();
                IEnumerable<System.Data.Common.DbDataRecord> _record = new DataAccess.GIS.Entities.GeographicalData().ReadByClass(className);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    GIS _item = new GIS(Convert.ToString(_dbRecord["ClassName"]), Convert.ToInt64(_dbRecord["IdObject"]), Convert.ToString(_dbRecord["GeographicalData"]), Convert.ToString(_dbRecord["ShapeType"]), Convert.ToInt64(_dbRecord["ZoomLevel"]));
                    _Items.Add(_item);
                }
                return _Items;
            }
            #endregion

        #endregion

            public EMS(String Username, String Password, String CurrentLanguage, String IPAddress)
        {
            //Especifico la cultura
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(CurrentLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(CurrentLanguage);

            //Cargo el idioma por defecto
            _DefaultLanguage = new Condesus.EMS.Business.DS.Collections.Languages(_Credential).DefaultLanguage();

            //genera una instancia de la credential
            _Credential = new Credential(Username, Password, _DefaultLanguage.IdLanguage, CurrentLanguage, IPAddress); 
                                  
         
        }
        
    }
   
}
