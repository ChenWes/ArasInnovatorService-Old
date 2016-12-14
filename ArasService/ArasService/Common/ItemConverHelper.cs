using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

using Aras;
using Aras.IOM;
using ArasService.ArasModel;

namespace ArasService.Common
{
    public class ItemConverHelper<T> where T:new()
    {
        /// <summary>
        /// Convert Item to Class Object As List
        /// 2015-08-07 add by WesChen
        /// </summary>
        /// <param name="pi_getItem"></param>
        /// <returns></returns>
        public List<T> ItemConver(Item pi_getItem)
        {
            List<T> l_modelList = new List<T>();
            int l_itemCount = pi_getItem.getItemCount();

            if (l_itemCount == 0)
            {
                return null;
            }

            for (int i = 0; i < l_itemCount; i++)
            {
                T l_model = new T();
                
                foreach (PropertyInfo l_propertyInfo in typeof(T).GetProperties())
                {
                    l_propertyInfo.SetValue(l_model, Convert.ChangeType(pi_getItem.getItemByIndex(i).getProperty(l_propertyInfo.Name), l_propertyInfo.PropertyType), null);                              
                }

                l_modelList.Add(l_model);
            }

            return l_modelList;
        }

        /// <summary>
        /// Convert Class object List To Item
        /// 2015-08-07 add by WesChen
        /// </summary>
        /// <param name="pi_object"></param>
        /// <param name="pi_item"></param>
        /// <param name="l_itemName"></param>
        /// <param name="pi_action"></param>
        /// <returns></returns>
        public Item ItemConver(List<T> pi_object,Item pi_item,string l_itemName, string pi_action)
        {
            foreach (T l_model in pi_object)
	        {
                Item l_newItem = pi_item.newItem(l_itemName, pi_action);

                foreach (PropertyInfo l_propertyInfo in typeof(T).GetProperties())
                {                    
                    l_newItem.setProperty(l_propertyInfo.Name, l_propertyInfo.GetValue(l_model, null).ToString());
                }            
            }

            return pi_item;
        }

    }
}