using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Condesus.EMS.Business.Security;
using System.Transactions;

namespace Condesus.EMS.Business.CT.Entities
{
    public class ConfigurationCT
    {
        #region Internal Properties
            private Credential _Credential;
            private Dictionary<Int64, Entities.Category> _Categories;//puntero a categorias
        #endregion

            internal ConfigurationCT(Credential credential)
        {
            _Credential = credential;
        }

        #region Categories
            public Category Category(Int64 idCategory)
            {
                //Carga la coleccion de lenguages de es posicion
                return new Collections.Categories(_Credential).Item(idCategory);
            }
            public Dictionary<Int64, Category> Categories
            {
                get
                {
                    if (_Categories == null)
                    {
                        //Carga la coleccion de lenguages de es posicion
                        _Categories = new Collections.Categories(_Credential).Items();
                    }

                    return _Categories;
                }

            }
            #endregion
    }
}
