using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.Common
{
    public class ClassPro<T>
    {

        public string getAllProperties<T>(T t)
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }

            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return tStr;
            }

            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;

                object value = item.GetValue(t, null);

                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    tStr += string.Format("{0}:{1},", name, value);
                }
                else
                {
                    getAllProperties(value);
                }
            }
            return tStr;

        }

        /// <summary>
        /// Get Class Propertie Value
        /// 2015-08-20 add by WesChen
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="pi_PropertieName"></param>
        /// <returns></returns>
        public string getPropertiesValue<T>(T t,string pi_PropertieName)
        {
            string l_tStr = string.Empty;

            if (t == null)
            {
                return l_tStr;
            }

            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return l_tStr;
            }

            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);

                if (name.ToLower() == pi_PropertieName.ToLower())
                {
                    if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                    {
                        l_tStr = value.ToString();
                        break;
                    }
                    else
                    {
                        l_tStr = value.ToString();
                        break;
                    }
                }
            }

            return l_tStr;

        }
    }
}