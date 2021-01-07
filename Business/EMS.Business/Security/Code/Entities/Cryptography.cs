using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Condesus.EMS.Business.Security
{
    internal class Cryptography
    {
        /// <summary>
        /// Metodo de hasheo de una cadena de caracteres alfanumericos con MD5
        /// </summary>
        /// <param name="Source">La cadena de caracteres</param>
        /// <returns>Un array de bytes con el hash</returns>
        internal static String Hash(String Source)
        {
            byte[] data = System.Text.UTF8Encoding.ASCII.GetBytes(Source);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashbyte = md5.ComputeHash(data, 0, Source.Length);
            return BitConverter.ToString(hashbyte);
            
        }
      
    }
}
