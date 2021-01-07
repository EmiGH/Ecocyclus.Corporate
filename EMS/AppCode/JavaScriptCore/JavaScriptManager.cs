using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Condesus.EMS.WebUI
{
    public class JavaScriptManager
    {
        private List<KeyValuePair<String, String>> _Attachs;
        private Page _Page;

        public JavaScriptManager()
        {
            _Attachs = new List<KeyValuePair<String, String>>();
            _Page = ((Page)HttpContext.Current.CurrentHandler);
        }

        /// <summary>
        /// Hace un attach del evento por el ScriptManager. Permite multiples attachs al mismo evento (Ej: onload)
        /// Importante: El orden de 'attacheo' no es necesariamente el orden en el que se disparan las funciones. 
        /// Si las funciones inyectadas son funcionalmente dependientes, crear funcion aparte y definir orden en esa funcion (Ej: onload)
        /// </summary>
        /// <param name="page">La pagina que va a regsitrar los eventos</param>
        /// <param name="clientEvent">El evento que dispara la funcion</param>
        /// <param name="function">La funcion que dispara el evento</param>
        public void AttachEvent(string clientEvent, string function)
        {
            //String _jsScript = "window.attachEvent('" + clientEvent + "', " + function + ");";
            _Attachs.Add(new KeyValuePair<String,String>(clientEvent, function));
        }

        public void AttachEvents(String strBrowserName)
        {
            System.Text.StringBuilder _sbBuffer = new System.Text.StringBuilder();


            if (strBrowserName == "IE")
            {
                //Primero me aseguro de Registrar el attacher por la funcion universal .onload (que se puede usar una sola vez y la uso para attachar lo que quiero n veces)
                _sbBuffer.Append("window.onload = AttachEvents;                                                                     \n");
                //_sbBuffer.Append("window.attachEvent('onload', AttachEvents);                                             \n");
                _sbBuffer.Append("function AttachEvents() {                                                                         \n");
                _sbBuffer.Append("  var isDOM = window.addEventListener;                                                            \n");
                _sbBuffer.Append("  if (isDOM) {                                                                                    \n");
                //agrego los attachs -> self.addEventListener('resize',HandlerFunc,true);
                foreach (var _attach in _Attachs)
                    { _sbBuffer.Append("      window.addEventListener('" + _attach.Key.ToLower() + "'," + _attach.Value + ",true);    \n"); }
                _sbBuffer.Append("  }                                                                                               \n");
                _sbBuffer.Append("  else                                                                                            \n");
                _sbBuffer.Append("  {                                                                                               \n");
                //agrego los attachs -> self.attachEvent('onresize',test);
                foreach (var _attach in _Attachs)
                    { _sbBuffer.Append("      window.attachEvent('on" + _attach.Key.ToLower() + "'," + _attach.Value + ");            \n"); }
                _sbBuffer.Append("  }                                                                                               \n");
                _sbBuffer.Append("}                                                                                                 \n");
            }
            else
            {
                _sbBuffer.Append("  //FireFox                                                                           \n");
                _sbBuffer.Append("  document.addEventListener('DOMContentLoaded', FW_OnLoad, false);                    \n");
                _sbBuffer.Append("  document.addEventListener('resize', SetFwBounds, false);                            \n");
            }
            //Limpio la Coleccion
            _Attachs.Clear();

            InjectJavascript("JSFW_ONLOAD_ATTACH_PAGE_EVENTS", _sbBuffer.ToString(), true);
        }

        /// <summary>
        /// La inyeccion del Script a la Pagina. Puede ser atravez de su SM o directamente a traves de la pagina.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <param name="jsScript"></param>
        /// <param name="scriptManager">True: Lo inyecta por el SM (debe existir uno instanciado en la pagina)</param>
        public void InjectJavascript(String key, String jsScript, Boolean scriptManager)
        {
            if (scriptManager)
                ScriptManager.RegisterClientScriptBlock(_Page, typeof(UpdatePanel), key, jsScript, true);
            else
                InjectJavascript(key, jsScript);
        }

        private void InjectJavascript(String key, String jsScript)
        {
            _Page.ClientScript.RegisterClientScriptBlock(_Page.GetType(), key, jsScript);
        }
    }
}
