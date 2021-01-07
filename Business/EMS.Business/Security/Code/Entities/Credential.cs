using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.Business.Security
{
    /// <summary>
    /// User's Credential Handler
    /// <para>Hold all security-related data for the logged-in user.</para>
    /// </summary>
    internal class Credential
    {
        #region Internal Properties
            //private Credential _Credential;
            //Usuario actual de la credential
            private String _Username;
            //Idioma elegido por el usuario
            private String _Language;
            //Idioma por defecto de la solucion
            private String _IdDefaultLanguage;
            //Proxy
            private Condesus.EMS.Business.DS.Entities.User _User;
            private Condesus.EMS.Business.DS.Entities.Language _CurrentLanguage;
            private Condesus.EMS.Business.DS.Entities.Language _DefaultLanguage;
            //Lista de organizaciones sobre las que puedo administrar datos
            private List<Int64> _ManagementRights;
        #endregion
        
        /// <summary>
        /// Constructor de <c>Credential</c>
        /// </summary>
        /// <param name="Username">El nombre de usuario de la persona</param>
        /// <param name="Password">La clave del usuario</param>
        /// <param name="CurrentLanguage">El lenguaje seleccionado por el usuario</param>
        internal Credential(String Username, String Password, String DefaultLanguage, String CurrentLanguage, String IPAddress)
        {
            //Chequeo la validez del usuario
            Condesus.EMS.Business.Security.Authority.Authenticate(Username, Condesus.EMS.Business.Security.Cryptography.Hash(Password), IPAddress);

            //Guardo el nombre de usuario
            _Username = Username;

            //Cargo el lenguaje actual seleccionado por el usuario en el login
            _Language = CurrentLanguage;

            //Guardo el lenguaje por defecto
            _IdDefaultLanguage = DefaultLanguage;
            
        }

        #region External Properties
          
            /// <summary>
            /// Opción de idioma actual seleccionada por el usuario
            /// </summary>
            internal Condesus.EMS.Business.DS.Entities.Language CurrentLanguage
            {
                 get 
                {
                    if (_DefaultLanguage == null)
                    {
                        Condesus.EMS.Business.DS.Collections.Languages _languages = new Condesus.EMS.Business.DS.Collections.Languages(this);
                        _DefaultLanguage = _languages.Item(_Language);
                    }
                    return _DefaultLanguage;
                }
            }
            /// <summary>
            /// Usuario actual 
            /// </summary>
            internal Condesus.EMS.Business.DS.Entities.User User
            {
                get 
                {
                    if (_User == null)
                    {
                        Condesus.EMS.Business.DS.Collections.Users oUsers = new Condesus.EMS.Business.DS.Collections.Users(this);
                        _User = oUsers.Item(_Username);
                    }
                    return _User;
                }
            }
            /// <summary>
            /// Lenguaje Default
            /// </summary>
            internal Condesus.EMS.Business.DS.Entities.Language DefaultLanguage
            {
                get
                {
                    if (_CurrentLanguage == null)
                    {
                        Condesus.EMS.Business.DS.Collections.Languages _languages = new Condesus.EMS.Business.DS.Collections.Languages(this);
                        _CurrentLanguage = _languages.Item(_IdDefaultLanguage);
                    }
                    return _CurrentLanguage;
                }
            }
        #endregion

        #region External Methods
            public void AllowOrganization(Int64 idOrganization)
            {
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //SACAR ESTO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                return;

                ////Si no tengo cargado los permisos los busco
                //if (_ManagementRights == null) { _ManagementRights = Condesus.EMS.Business.Security.Authority.ManagementRights(_User.Person.IdPerson); }

                ////Chequeo si la organization esta permitida
                //if (!_ManagementRights.Contains(idOrganization))
                //{ throw new UnauthorizedAccessException(Common.Resources.Errors.ManagementRightsViolation); }
            }
        #endregion

    }
}
