using System;
using System.Collections.Generic;
using System.Text;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.IA.Collections
{
    internal class ExceptionStates
    {
        #region Internal Properties
        private Credential _Credential;
        #endregion

        internal ExceptionStates(Credential credential)
        {
            _Credential = credential;
        }

            #region Read Functions
            internal Entities.ExceptionState Item(Int64 idExceptionState)
            {
                //Objeto de data layer para acceder a datos
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                Entities.ExceptionState _exceptionState = null;
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.ExceptionStates_LG_ReadById(idExceptionState, _Credential.CurrentLanguage.IdLanguage);
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_exceptionState == null)
                    {
                        _exceptionState = Factories.ExceptionStateFactory.CreateExceptionState(Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]),_Credential);
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            return _exceptionState;
                        }
                    }
                    else
                    {
                        return Factories.ExceptionStateFactory.CreateExceptionState(Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]),_Credential);
                    }
                }
                return _exceptionState;
            }
            internal Dictionary<Int64, Entities.ExceptionState> Items()
            {
                //Coleccion para devolver los países
                Dictionary<Int64, Entities.ExceptionState> _oItems = new Dictionary<Int64, Entities.ExceptionState>();

                //Objeto de data layer para acceder a datos
                DataAccess.IA.ImprovementActions _dbImprovementActions = new Condesus.EMS.DataAccess.IA.ImprovementActions();

                //Traigo los datos de la base
                IEnumerable<System.Data.Common.DbDataRecord> _record = _dbImprovementActions.ExceptionStates_ReadAll(_Credential.CurrentLanguage.IdLanguage);

                //Si esto esta en true, no debe insertar en la coleccion. (esto es cuando ya esta insertado el idioma elegido en la coleccion)
                Boolean _oInsert = true;

                //busca si hay mas de un id Country igual (distintos idiomas) y si hay mas, carga solo el del lenguage seleccionado
                foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                {
                    if (_oItems.ContainsKey(Convert.ToInt64(_dbRecord["IdExceptionState"])))
                    {
                        ///Si el que agregue es el default lo elimino porque el que voy a agregar es el del idioma en uso por el usuario
                        if (Convert.ToString(_dbRecord["IdLanguage"]).ToUpper() != _Credential.DefaultLanguage.IdLanguage.ToUpper())
                        {
                            _oItems.Remove(Convert.ToInt64(_dbRecord["IdExceptionState"]));
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
                            Entities.ExceptionState _oExceptionState = Factories.ExceptionStateFactory.CreateExceptionState(Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]),_Credential);
                            //Lo agrego a la coleccion
                            _oItems.Add(_oExceptionState.IdExceptionState, _oExceptionState);
                        }
                        _oInsert = true;
                    }
                    else
                    {   //Declara e instancia un pais 
                        //Declara e instancia un pais 
                        Entities.ExceptionState _oExceptionState = Factories.ExceptionStateFactory.CreateExceptionState(Convert.ToInt64(_dbRecord["idExceptionState"]), Convert.ToString(_dbRecord["IdLanguage"]), Convert.ToString(_dbRecord["Name"]),_Credential);
                        //Lo agrego a la coleccion
                        _oItems.Add(_oExceptionState.IdExceptionState, _oExceptionState);
                    }

                }
                return _oItems;
            }
            #endregion
    }
}
