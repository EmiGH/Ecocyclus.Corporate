using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.PF.Collections
{
    internal class TimeUnits
    {
     
        #region Internal Properties
            private Credential _Credential;
        #endregion

        internal TimeUnits(Credential credential)
            { _Credential = credential; }


        #region Read Functions
            internal Entities.TimeUnit Item(Int64 idTimeUnit)
            {
                   //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.TimeUnits _dbTimeUnits = _dbProcessesFramework.TimeUnits;

                Entities.TimeUnit _timeUnit = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.TimeUnits_ReadById(idTimeUnit, _Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_timeUnit == null)
                    {
                        _timeUnit = new Entities.TimeUnit(
                            Convert.ToInt64(_dbRecord["IdTimeUnit"]),
                            Convert.ToInt64(_dbRecord["Numerator"]),
                            Convert.ToInt64(_dbRecord["Exponent"]),
                            Convert.ToInt64(_dbRecord["Denominator"]),
                            Convert.ToBoolean(_dbRecord["IsPattern"]),
                            Convert.ToString(_dbRecord["Name"]),
                            Convert.ToString(_dbRecord["IdLanguage"]), _Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _timeUnit;
                        }
                    }
                    else
                    {
                        return new Entities.TimeUnit(
                            Convert.ToInt64(_dbRecord["IdTimeUnit"]),
                            Convert.ToInt64(_dbRecord["Numerator"]),
                            Convert.ToInt64(_dbRecord["Exponent"]),
                            Convert.ToInt64(_dbRecord["Denominator"]),
                            Convert.ToBoolean(_dbRecord["IsPattern"]),
                            Convert.ToString(_dbRecord["Name"]),
                            Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                    }
                }
                return _timeUnit;
            }
            internal Dictionary<Int64, Entities.TimeUnit> Items()
            {
                 //Coleccion para devolver los TimeUnit
                Dictionary<Int64, Entities.TimeUnit> _oItems = new Dictionary<Int64, Entities.TimeUnit>();

                //Objeto de data layer para acceder a datos
                DataAccess.PF.ProcessesFramework _dbProcessesFramework = new Condesus.EMS.DataAccess.PF.ProcessesFramework();
                //DataAccess.PF.Entities.TimeUnits _dbTimeUnits = _dbProcessesFramework.TimeUnits;

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbProcessesFramework.TimeUnits_ReadAll(_Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdTimeUnit"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdTimeUnit"]));
                        }
                        else
                        {
                            //No debe insertar en la coleccion ya que existe el idioma correcto.
                            _oInsert = false;
                        }

                        //Solo inserta si es necesario.
                        if (_oInsert)
                        {
                            //Declara e instancia  
                            Entities.TimeUnit _timeUnit = new Entities.TimeUnit(
                            Convert.ToInt64(_dbRecord["IdTimeUnit"]),
                            Convert.ToInt64(_dbRecord["Numerator"]),
                            Convert.ToInt64(_dbRecord["Exponent"]),
                            Convert.ToInt64(_dbRecord["Denominator"]),
                            Convert.ToBoolean(_dbRecord["IsPattern"]),
                            Convert.ToString(_dbRecord["Name"]),
                            Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                            //Lo agrego a la coleccion
                            _oItems.Add(_timeUnit.IdTimeUnit, _timeUnit);
                        }
                        _oInsert = true;
                    }
                    else
                    {   //Declara e instancia  
                        Entities.TimeUnit _timeUnit = new Entities.TimeUnit(
                            Convert.ToInt64(_dbRecord["IdTimeUnit"]),
                            Convert.ToInt64(_dbRecord["Numerator"]),
                            Convert.ToInt64(_dbRecord["Exponent"]),
                            Convert.ToInt64(_dbRecord["Denominator"]),
                            Convert.ToBoolean(_dbRecord["IsPattern"]),
                            Convert.ToString(_dbRecord["Name"]),
                            Convert.ToString(_dbRecord["IdLanguage"]),_Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_timeUnit.IdTimeUnit, _timeUnit);
                    }

                }
                return _oItems;
            }
        #endregion

    }
}
