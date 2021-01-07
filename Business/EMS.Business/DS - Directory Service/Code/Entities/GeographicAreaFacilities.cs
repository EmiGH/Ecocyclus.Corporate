using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Entities
{
    //public class GeographicAreaFacilities : GeographicArea
    //{
    //    #region Internal Properties               
    //        private String _Mnemo;
    //        private KC.Entities.ResourceCatalog _ResourcePicture;
    //    #endregion

    //    #region External Properties
    //        public String Mnemo
    //        {
    //            get { return _Mnemo; }
    //        }

    //        #region Resources
    //        /// <summary>
    //        /// Retorna la coleccion de Imagenes que tiene asociado. (a travez de CatalogDoc)
    //        /// Key = IdResourceFile
    //        /// </summary>
    //        public Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> Pictures
    //        {
    //            get
    //            {
    //                Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc> _pictures = new Dictionary<Int64, Condesus.EMS.Business.KC.Entities.CatalogDoc>();
    //                //Si el proyecto no tiene ningun ResourcePicture Asociado...entrega vacio.
    //                if (this.ResourcePicture != null)
    //                {
    //                    foreach (Condesus.EMS.Business.KC.Entities.Catalog _catalog in this.ResourcePicture.Catalogues.Values)
    //                    {
    //                        if (_catalog.GetType().Name == "CatalogDoc")
    //                        {
    //                            //Lo castea a tipo Doc
    //                            Condesus.EMS.Business.KC.Entities.CatalogDoc _catalogDoc = (Condesus.EMS.Business.KC.Entities.CatalogDoc)_catalog;

    //                            //Solo nos quedamos con los que son tipo image
    //                            if (_catalogDoc.DocType.Contains("image"))
    //                            {
    //                                _pictures.Add(_catalogDoc.IdResourceFile, _catalogDoc);
    //                            }
    //                        }
    //                    }
    //                }
    //                return _pictures;
    //            }
    //        }

    //        public KC.Entities.ResourceCatalog ResourcePicture
    //        {
    //            get
    //            {
    //                return _ResourcePicture;
    //            }
    //        }
    //        #endregion
    //    #endregion

    //    internal GeographicAreaFacilities(Int64 idGeographicArea, String mnemo, Int64 idOrganization, Int64 idParentGeographicArea, String name, String description, KC.Entities.ResourceCatalog resourcePicture, Credential credential)
    //        : base(idGeographicArea, idOrganization, idParentGeographicArea, name, description, credential)
    //    {
    //        _Mnemo = mnemo;
    //        _ResourcePicture = resourcePicture;
    //    }
    //}
}

