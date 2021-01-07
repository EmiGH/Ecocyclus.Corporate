using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA.Collections
{
    internal class ExceptionTypes
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal ExceptionTypes(Credential credential)
        {
            _Credential = credential;
        }

            #region Read Functions
            internal Entities.ExceptionType Item(Int64 idExceptionType)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                Entities.ExceptionType _exceptionType = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.ExceptionTypes_ReadById(idExceptionType, _Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_exceptionType == null)
                    {
                        _exceptionType = new Entities.ExceptionType(Convert.ToInt64(_dbRecord["idExceptionType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _exceptionType;
                        }
                    }
                    else
                    {
                        return new Entities.ExceptionType(Convert.ToInt64(_dbRecord["idExceptionType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]),_Credential);
                    }
                }
                return _exceptionType;
            }
            internal Dictionary<Int64, Entities.ExceptionType> Items()
            {
               
                //Coleccion para devolver los países
                Dictionary<Int64, Entities.ExceptionType> _oItems = new Dictionary<Int64, Entities.ExceptionType>();

                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.ExceptionTypes_ReadAll(_Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdExceptionType"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdExceptionType"]));
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
                            Entities.ExceptionType _oExceptionType = new Entities.ExceptionType(Convert.ToInt64(_dbRecord["idExceptionType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                            //Lo agrego a la coleccion
                            _oItems.Add(_oExceptionType.IdExceptionType, _oExceptionType);
                        }
                        _oInsert = true;
                    }
                    else
                    {   //Declara e instancia un pais 
                        //Declara e instancia un pais 
                        Entities.ExceptionType _oExceptionType = new Entities.ExceptionType(Convert.ToInt64(_dbRecord["idExceptionType"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]), Convert.ToString(_dbRecord["Description"]), _Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_oExceptionType.IdExceptionType, _oExceptionType);
                    }

                }
                return _oItems;
            }
            #endregion
    }
}
