using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.DS.Collections
{
    internal class ApplicabilityContactTypes
    {
        #region Internal Properties
            private Credential _Credential;
            private Entities.ApplicabilityContactType _ApplicabilityContactType;
        #endregion

        internal ApplicabilityContactTypes(Credential credential)
        {
            _Credential = credential;
        }

        #region Read Function
            internal Entities.ApplicabilityContactType Item(Int64 idAppli)
            {
                foreach (Entities.ApplicabilityContactType _oAppli in Items().Values)
                {
                    if (idAppli == _oAppli.IdApplicabilityContactType)
                    { 
                        _ApplicabilityContactType = _oAppli;
                        break; 
                    }            
                }
                return _ApplicabilityContactType;
            }

            internal Entities.ApplicabilityContactType Item(String name)
            {
                foreach (Entities.ApplicabilityContactType _oAppli in Items().Values)
                {
                    if (name == _oAppli.LanguageOption.Name)
                    {
                        _ApplicabilityContactType = _oAppli;
                        break;                 
                    }
                }
                return _ApplicabilityContactType;
            }
            internal Dictionary<Int64, Entities.ApplicabilityContactType> Items()
            { 
                //Coleccion para devolver los ContactType
                Dictionary<Int64, Entities.ApplicabilityContactType> _oItems = new Dictionary<Int64, Entities.ApplicabilityContactType>();

                //Objeto de data layer para acceder a datos
                DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
                //DataAccess.DS.ApplicabilityContactTypes _dbAppliContactTypes = _dbDirectoryServices.ApplicabilityContactTypes;

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbDirectoryServices.ApplicabilityContactTypes_ReadAll(_Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdApplicability"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdApplicability"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }

                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia un pais 
                            Entities.ApplicabilityContactType _oApplicabilityContactType = new Condesus.EMS.Business.DS.Entities.ApplicabilityContactType(Convert.ToInt64(_dbRecord["IdApplicability"]), Convert.ToString(_dbRecord["Name"]), _Credential);
                            //Lo agrego a la coleccion
                            _oItems.Add(_oApplicabilityContactType.IdApplicabilityContactType, _oApplicabilityContactType);
                        }
                        _oInsert = true;
                    }
                    else
                    {
                        Entities.ApplicabilityContactType _oApplicabilityContactType = new Condesus.EMS.Business.DS.Entities.ApplicabilityContactType(Convert.ToInt64(_dbRecord["IdApplicability"]), Convert.ToString(_dbRecord["Name"]),_Credential);
                        _oItems.Add(_oApplicabilityContactType.IdApplicabilityContactType, _oApplicabilityContactType);
                    }
                }
                return _oItems;
            }
        #endregion

    }
}
