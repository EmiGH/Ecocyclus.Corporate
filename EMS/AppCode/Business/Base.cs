using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace Condesus.EMS.WebUI.Business
{
    public class Base : BasePage
    {
        protected String _CommandName = "";

        public Object Execute(Dictionary<String, Object> param)
        {
            Type _type = this.GetType();
            MethodInfo _method = _type.GetMethod(_CommandName);
            object[] _args = new object[] { param };
            try
            {
                return _method.Invoke(this, _args);
            }
            catch (Exception ex)
            {
                // TODO: Add logging functionality
                throw ex.InnerException;
            }
        }
        public Object Execute(Dictionary<String, Object> paramSource, Dictionary<String, Object> paramDestination)
        {
            Type _type = this.GetType();
            MethodInfo _method = _type.GetMethod(_CommandName);
            object[] _args = new object[] { paramSource, paramDestination };
            try
            {
                return _method.Invoke(this, _args);
            }
            catch (Exception ex)
            {
                // TODO: Add logging functionality
                throw;
            }
        }
        public Object HasLanguages()
        {
            Type _type = this.GetType();
            MethodInfo _method = _type.GetMethod(_CommandName);
            //Si lo encuentra quiere decir que tiene lenguaje
            if (_method != null)
                { return true; }
            
            //como no lo encontro, retorna false porque no tiene lenguaje.
            return false;
        }
        
        /// <summary>
        /// Esta funcion se encarga de redimensionar un array.
        /// </summary>
        /// <param name="orgArray">Array original</param>
        /// <param name="tamaño">Nuevo Tamaño</param>
        /// <returns>Un <c>Array</c></returns>
        public static Array RedimArray(Array originalArray, Int32 length)
        {
            //Obtiene el Type
            Type _type = originalArray.GetType().GetElementType();
            //Crea un nuevo array con la nueva dimension
            Array nArray = Array.CreateInstance(_type, length);
            //Hace la copia del viejo al nuevo.
            Array.Copy(originalArray, 0, nArray, 0, Math.Min(originalArray.Length, length));
            //Retorna el nuevo array.
            return nArray;
        } 

    }
}
