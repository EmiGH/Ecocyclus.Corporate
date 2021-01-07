using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Condesus.EMS.Business.Security;

namespace Condesus.EMS.Business.Security
{
    internal class Authority
    {
        private Credential _Credential;
        /// <summary>
        /// Constructor de <c>Authority</c>
        /// </summary>
        internal Authority(Credential credential)
        {
            _Credential = credential;
        }

        /// <summary>
        /// Valida los datos de usuario y registra el acceso
        /// </summary>
        /// <param name="Username">El nombre de usuario de la persona</param>
        /// <param name="Password">La clave del usuario</param>
        /// <param name="IPAddress">La dirección IP desde la cual se está ingresando</param>
        internal static void Authenticate(String Username, String Password, String IPAddress)
        {
            Condesus.EMS.DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();
            if (!_dbSecuritySystems.Authenticate(Username, Password, IPAddress))
            {
                throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed);
            }
        }


        internal void Authorize(String className, Int64 idObject, Int64 idPerson, int permission)
        {
            Boolean _result = false;
            Condesus.EMS.DataAccess.Security.SecuritySystems _dbSecuritySystems = new Condesus.EMS.DataAccess.Security.SecuritySystems();
            IEnumerable<System.Data.Common.DbDataRecord> _record = _dbSecuritySystems.Authorize(className, idObject, idPerson, _Credential.CurrentLanguage.IdLanguage);
                    foreach (System.Data.Common.DbDataRecord _dbRecord in _record)
                    {
                        if (permission == Common.Permissions.View) 
                        { 
                            if ((Convert.ToInt16(_dbRecord["IdPermission"]) == Common.Permissions.View) || (Convert.ToInt16(_dbRecord["IdPermission"]) == Common.Permissions.Manage)) 
                            { _result = true; } 
                        }
                        if (permission == Common.Permissions.Manage) 
                        { 
                            if (Convert.ToInt16(_dbRecord["IdPermission"]) == Common.Permissions.Manage)
                            { _result = true; } 
                        }
                    }
                    if (!_result) { throw new UnauthorizedAccessException(Common.Resources.Errors.AuthenticationFailed); }
        }
        internal static void AuthorizePassword(String Username, String Password)
        {
            
            //Int16 _PasswordHistory;
            //Int16 _PasswordMaximunAge;
            //Int16 _PasswordMinimunAge;
            //Int16 _PasswordMinimunLength;
            //Boolean _MustMeetComplexCriteria;

            ////Traigo los datos de politica de la cuenta
            
            //Condesus.EMS.DataAccess.DS.DirectoryServices _dbDirectoryServices = new Condesus.EMS.DataAccess.DS.DirectoryServices();
            //DataTable oTable = _dbDirectoryServices.Users.ReadById(Username);
            //foreach (DataRow oDR in oTable.Rows)
            //{
            //    if (Convert.ToBoolean(oDR["CannotChangePassword"]))
            //    {
            //        //error                    
            //    }
            //}
            
            ////Leo los datos de politica de claves
            //Condesus.EMS.DataAccess.Security.Authority _dbAuthority = new Condesus.EMS.DataAccess.Security.Authority();
            //oTable = _dbAuthority.PasswordPolicy();
            //foreach (DataRow oDR in oTable.Rows)
            //{
            //    _PasswordHistory = Convert.ToInt16(oDR["History"]);
            //    _PasswordMaximunAge = Convert.ToInt16(oDR["MaximunAge"]);
            //    _PasswordMinimunAge = Convert.ToInt16(oDR["MinimunAge"]);
            //    _PasswordMinimunLength = Convert.ToInt16(oDR["MinimunLength"]);
            //    _MustMeetComplexCriteria = Convert.ToBoolean(oDR["MustMeetComplexCriteria"]);
            //}

            ////Si la politica de clave establece un numero especifico de repeticiones
            ////entonces debo traerme las anteriores para ver si repite o no
            //if (_PasswordHistory > 0)
            //{
            //    oTable = _dbDirectoryServices.Users.GetPasswordHistory(Username);
            //    foreach (DataRow oDR in oTable.Rows)
            //    {
            //        for (int i = 0; i < _PasswordHistory ; i++)
            //        {
            //            if (Password == Convert.ToString(oDR["Password"]))
            //            {
            //                //tiro error
            //            }
            //        }
            //    }
            //}

        }
    }
}
