using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace ArasInnovatorService.Common
{
    public static class ClassOperation
    {
        public static object GetConvert(string pi_assembly, string pi_namespace, string pi_className)
        {
            Type l_tempType = Assembly.Load(pi_assembly).GetType(string.Concat(pi_namespace, ".", pi_className));
            if (l_tempType == null)
            {
                return null;
            }
            object l_tempObject = (object)Activator.CreateInstance(l_tempType);
            return l_tempObject;
        }

        public static object[] GetConvert(string pi_assembly, string pi_namespace, string[] pi_classNameArray)
        {
            object[] l_temp = null;
            if (pi_classNameArray != null)
            {
                for (int i = 0; i < pi_classNameArray.Length; i++)
                {
                    l_temp[i] = GetConvert(pi_assembly, pi_namespace, pi_classNameArray[i]);
                }
            }
            return l_temp;
        }

        public static bool IsGenericList(this object o)
        {
            bool isGenericList = false;

            var oType = o.GetType();

            if (oType.IsGenericType && (oType.GetGenericTypeDefinition() == typeof(List<>)))
                isGenericList = true;

            return isGenericList;
        }

        public static bool CheckClassHaveProperty(string pi_assembly,string pi_nameSpace,string pi_className,string pi_propertyName)
        {
            bool l_returnFlag = false;
            object l_tempObject = GetConvert(pi_assembly, pi_nameSpace, pi_className);
            if(l_tempObject==null)
            {
                return l_returnFlag;
            }

            foreach (PropertyInfo l_propertyInfo in l_tempObject.GetType().GetProperties())
            {
                if(l_propertyInfo.Name.ToUpper() == pi_propertyName.Trim().ToUpper())
                {
                    l_returnFlag = true;
                }
            }

            return l_returnFlag;
        }

        public static bool CheckClassSkipOverProperty(string pi_assembly, string pi_nameSpace, string pi_className, string pi_propertyName)
        {
            bool l_returnFlag = false;
            object l_tempObject = GetConvert(pi_assembly, pi_nameSpace, pi_className);
            if (l_tempObject == null)
            {
                return l_returnFlag;
            }

            foreach (PropertyInfo l_propertyInfo in l_tempObject.GetType().GetProperties())
            {
                string[] l_propertyArray = (string[])l_propertyInfo.GetValue(null);
                for (int i = 0; i < l_propertyArray.Length; i++)
                {
                    if(l_propertyArray[i].ToUpper()==pi_propertyName.ToUpper())
                    {
                        l_returnFlag = true;
                        return l_returnFlag;
                    }
                }
            }

            return l_returnFlag;
        }
       
    }
}