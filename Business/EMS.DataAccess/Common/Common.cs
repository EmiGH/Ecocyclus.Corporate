using System;
using System.Collections.Generic;
using System.Text;

namespace Condesus.EMS.DataAccess.Common
{
    public class Common
    {
        public static T CastNullValues<T>(object value, T defaultValue)
        {
            if (value == DBNull.Value) return defaultValue;
            return (T)Convert.ChangeType(value, typeof(T));
        }
        public static Object CastValueToNull<T>(object value, T defaultValue)
        {
            if (value.ToString() == "0") return DBNull.Value;
            return value;
        }
        public static Object CastDateTimeToNull<T>(object value, T defaultValue)
        {
            if (Convert.ToDateTime(value) == DateTime.MinValue) return DBNull.Value;
            return value;
        }
        public static Object CastValueToNull(Byte[] value)
        {
            if (value == null) return DBNull.Value;
            return value;
        }

    }
}
