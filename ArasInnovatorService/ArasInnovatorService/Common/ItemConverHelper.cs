using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

using Aras;
using Aras.IOM;
using ArasInnovatorService.ArasModel;

namespace ArasInnovatorService.Common
{
    public class ItemConverHelper<T> where T:new()
    {
        /// <summary>
        /// Convert Item to Class Object As List
        /// 2015-08-07 add by WesChen
        /// 2015-08-18 update by WesChen Test Successed
        /// 2015-09-03 update by WesChen Test Successed
        /// </summary>
        /// <param name="pi_getItem"></param>
        /// <returns></returns>
        public List<T> ItemConver(Item pi_getItem)
        {
            if (pi_getItem == null) { return null; }
            try
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
                        #region Link
                        //Link*********************************************************************************************************************************
                        if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleBOM>))
                        {
                            ItemConverHelper<GarmentStyleBOM> l_tempClass = new ItemConverHelper<GarmentStyleBOM>();
                            List<GarmentStyleBOM> l_tempList = new List<GarmentStyleBOM>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleBOM_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<GarmentBOM>))
                        {
                            ItemConverHelper<GarmentBOM> l_tempClass = new ItemConverHelper<GarmentBOM>();
                            List<GarmentBOM> l_tempList = new List<GarmentBOM>();                            
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelatedItem());
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<GarmentBOMPart>))
                        {
                            ItemConverHelper<GarmentBOMPart> l_tempClass = new ItemConverHelper<GarmentBOMPart>();
                            List<GarmentBOMPart> l_tempList = new List<GarmentBOMPart>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("GarmentBOMPart_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<Part>))
                        {
                            ItemConverHelper<Part> l_tempClass = new ItemConverHelper<Part>();
                            List<Part> l_tempList = new List<Part>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelatedItem());
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<PartColorCombo>))
                        {
                            ItemConverHelper<PartColorCombo> l_tempClass = new ItemConverHelper<PartColorCombo>();
                            List<PartColorCombo> l_tempList = new List<PartColorCombo>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<ColorCombo>))
                        {
                            ItemConverHelper<ColorCombo> l_tempClass = new ItemConverHelper<ColorCombo>();
                            List<ColorCombo> l_tempList = new List<ColorCombo>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelatedItem());
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<ColorComboColorCode>))
                        {
                            ItemConverHelper<ColorComboColorCode> l_tempClass = new ItemConverHelper<ColorComboColorCode>();
                            List<ColorComboColorCode> l_tempList = new List<ColorComboColorCode>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<ColorCode>))
                        {
                            ItemConverHelper<ColorCode> l_tempClass = new ItemConverHelper<ColorCode>();
                            List<ColorCode> l_tempList = new List<ColorCode>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelatedItem());
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        #endregion

                        #region Part
                        //Part*********************************************************************************************************************************
                        else if (l_propertyInfo.PropertyType == typeof(List<PartDocument>))
                        {
                            ItemConverHelper<PartDocument> l_tempClass = new ItemConverHelper<PartDocument>();
                            List<PartDocument> l_tempList = new List<PartDocument>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("PartDocument_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<PartCAD>))
                        {
                            ItemConverHelper<PartCAD> l_tempClass = new ItemConverHelper<PartCAD>();
                            List<PartCAD> l_tempList = new List<PartCAD>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("PartCAD_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<PartSize>))
                        {
                            ItemConverHelper<PartSize> l_tempClass = new ItemConverHelper<PartSize>();
                            List<PartSize> l_tempList = new List<PartSize>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("PartSize_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        
                        else if (l_propertyInfo.PropertyType == typeof(List<PartContent>))
                        {
                            ItemConverHelper<PartContent> l_tempClass = new ItemConverHelper<PartContent>();
                            List<PartContent> l_tempList = new List<PartContent>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("PartContent_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        #endregion

                        #region GarmentStyle
                        //GarmentStyle*********************************************************************************************************************************
                        else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleColorWay>))
                        {
                            ItemConverHelper<GarmentStyleColorWay> l_tempClass = new ItemConverHelper<GarmentStyleColorWay>();
                            List<GarmentStyleColorWay> l_tempList = new List<GarmentStyleColorWay>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleGarmentColorway_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleSizeRange>))
                        {
                            ItemConverHelper<GarmentStyleSizeRange> l_tempClass = new ItemConverHelper<GarmentStyleSizeRange>();
                            List<GarmentStyleSizeRange> l_tempList = new List<GarmentStyleSizeRange>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRange_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        
                        else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleSketch>))
                        {
                            ItemConverHelper<GarmentStyleSketch> l_tempClass = new ItemConverHelper<GarmentStyleSketch>();
                            List<GarmentStyleSketch> l_tempList = new List<GarmentStyleSketch>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleSketch_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleCAD>))
                        {
                            ItemConverHelper<GarmentStyleCAD> l_tempClass = new ItemConverHelper<GarmentStyleCAD>();
                            List<GarmentStyleCAD> l_tempList = new List<GarmentStyleCAD>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleCAD_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleDocument>))
                        {
                            ItemConverHelper<GarmentStyleDocument> l_tempClass = new ItemConverHelper<GarmentStyleDocument>();
                            List<GarmentStyleDocument> l_tempList = new List<GarmentStyleDocument>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleDocument_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleYearSeason>))
                        {
                            ItemConverHelper<GarmentStyleYearSeason> l_tempClass = new ItemConverHelper<GarmentStyleYearSeason>();
                            List<GarmentStyleYearSeason> l_tempList = new List<GarmentStyleYearSeason>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleYearSeason_ItemName")));
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        else if (l_propertyInfo.PropertyType == typeof(List<YearSeason>))
                        {
                            ItemConverHelper<YearSeason> l_tempClass = new ItemConverHelper<YearSeason>();
                            List<YearSeason> l_tempList = new List<YearSeason>();
                            l_tempList = l_tempClass.ItemConver(pi_getItem.getItemByIndex(i).getRelatedItem());
                            l_propertyInfo.SetValue(l_model, l_tempList, null);
                        }
                        #endregion

                        else
                        {
                            #region common property

                            //string/int/decimal/float/datetime   //ok
                            if (pi_getItem.getItemByIndex(i).getProperty(l_propertyInfo.Name.ToLower()) != null)
                            {
                                if (!string.IsNullOrEmpty(pi_getItem.getItemByIndex(i).getProperty(l_propertyInfo.Name.ToLower())) && pi_getItem.getItemByIndex(i).getProperty(l_propertyInfo.Name.ToLower()) != "0001/1/1 0:00:00" && pi_getItem.getItemByIndex(i).getProperty(l_propertyInfo.Name.ToLower()) != "1/1/0001 12:00:00 AM")
                                {
                                    l_propertyInfo.SetValue(l_model, Convert.ChangeType(pi_getItem.getItemByIndex(i).getProperty(l_propertyInfo.Name.ToLower()), l_propertyInfo.PropertyType), null);
                                }
                            }

                            #endregion
                        }
                    }

                    l_modelList.Add(l_model);
                }


                return l_modelList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }        

        /// <summary>
        /// Convert Class object List To Item for add method
        /// 2015-08-07 add by WesChen
        /// 2015-08-20 update by WesChen Test Successed
        /// 2015-09-08 update by WesChen Test Successed
        /// </summary>
        /// <param name="pi_object"></param>
        /// <param name="pi_innovator"></param>
        /// <param name="l_itemName"></param>
        /// <param name="pi_action"></param>
        /// <returns></returns>
        public Item ItemConver(T pi_object, Innovator pi_innovator, string l_itemName, string pi_action)
        {
            try
            {
                Item l_newItem = pi_innovator.newItem(l_itemName, pi_action);

                foreach (PropertyInfo l_propertyInfo in typeof(T).GetProperties())
                {
                    #region Link
                    //Link*********************************************************************************************************************************
                    if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleBOM>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleBOM l_garmentstyleBOM in (List<GarmentStyleBOM>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleBOM> l_tempClass = new ItemConverHelper<GarmentStyleBOM>();
                                Item l_tempItem = l_tempClass.ItemConver(l_garmentstyleBOM, pi_innovator, ConfigHelper.GetAPPConfigValue("GarmentStyleBOM_ItemName"), pi_action);
                                //If Have ID , NO Add
                                if (!string.IsNullOrEmpty(l_garmentstyleBOM.ID))
                                {
                                    //Set Action
                                    l_tempItem.setAction("get");
                                    l_tempItem.setAttribute("id",l_garmentstyleBOM.ID);
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_garmentstyleBOM);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if(!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentBOM>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentBOM l_garmentBOM in (List<GarmentBOM>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentBOM> l_tempClass = new ItemConverHelper<GarmentBOM>();
                                Item l_tempItem = l_tempClass.ItemConver(l_garmentBOM, pi_innovator, ConfigHelper.GetAPPConfigValue("GarmentBOM_ItemName"), pi_action);
                                if(!string.IsNullOrEmpty(l_garmentBOM.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.setAttribute("id",l_garmentBOM.ID);
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_garmentBOM);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                //if son item not add , then no add related item
                                if (l_tempItem.getAction().ToLower() == "add" || l_tempItem.getAction().ToLower() == "get")
                                {
                                    l_newItem.setRelatedItem(l_tempItem);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentBOMPart>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentBOMPart l_garmentBOMPart in (List<GarmentBOMPart>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentBOMPart> l_tempClass = new ItemConverHelper<GarmentBOMPart>();
                                Item l_tempItem = l_tempClass.ItemConver(l_garmentBOMPart, pi_innovator, ConfigHelper.GetAPPConfigValue("GarmentBOMPart_ItemName"), pi_action);
                                if(!string.IsNullOrEmpty(l_garmentBOMPart.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.setAttribute("id",l_garmentBOMPart.ID);
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_garmentBOMPart);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<Part>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (Part l_part in (List<Part>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<Part> l_tempClass = new ItemConverHelper<Part>();
                                Item l_tempItem = l_tempClass.ItemConver(l_part, pi_innovator, ConfigHelper.GetAPPConfigValue("Part_ItemName"), pi_action);

                                if(!string.IsNullOrEmpty(l_part.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.setAttribute("id", l_part.ID);
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_part);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                if (l_tempItem.getAction().ToLower() == "add" || l_tempItem.getAction().ToLower() == "get")
                                {
                                    l_newItem.setRelatedItem(l_tempItem);
                                }                                
                            }
                        }

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartColorCombo>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (PartColorCombo l_partColorCombo in (List<PartColorCombo>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<PartColorCombo> l_tempClass = new ItemConverHelper<PartColorCombo>();
                                Item l_tempItem = l_tempClass.ItemConver(l_partColorCombo, pi_innovator, ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), pi_action);
                                if(!string.IsNullOrEmpty(l_partColorCombo.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.setAttribute("id", l_partColorCombo.ID);
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_partColorCombo);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorCombo>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (ColorCombo l_colorCombo in (List<ColorCombo>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<ColorCombo> l_tempClass = new ItemConverHelper<ColorCombo>();
                                Item l_tempItem = l_tempClass.ItemConver(l_colorCombo, pi_innovator, ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), pi_action);
                                if (!string.IsNullOrEmpty(l_colorCombo.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.setAttribute("id", l_colorCombo.ID);
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_colorCombo);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                if (l_tempItem.getAction().ToLower() == "add" || l_tempItem.getAction().ToLower() == "get")
                                {
                                    l_newItem.setRelatedItem(l_tempItem);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorComboColorCode>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (ColorComboColorCode l_colorComboColorCode in (List<ColorComboColorCode>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<ColorComboColorCode> l_tempClass = new ItemConverHelper<ColorComboColorCode>();
                                Item l_tempItem = l_tempClass.ItemConver(l_colorComboColorCode, pi_innovator, ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName"), pi_action);
                                if (!string.IsNullOrEmpty(l_colorComboColorCode.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.setAttribute("id", l_colorComboColorCode.ID);
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_colorComboColorCode);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorCode>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (ColorCode l_colorCode in (List<ColorCode>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<ColorCode> l_tempClass = new ItemConverHelper<ColorCode>();
                                Item l_tempItem = l_tempClass.ItemConver(l_colorCode, pi_innovator, ConfigHelper.GetAPPConfigValue("ColorCode_ItemName"), pi_action);
                                if(!string.IsNullOrEmpty(l_colorCode.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.setAttribute("id", l_colorCode.ID);
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_colorCode);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                if (l_tempItem.getAction().ToLower() == "add" || l_tempItem.getAction().ToLower() == "get")
                                {
                                    l_newItem.setRelatedItem(l_tempItem);
                                }
                            }
                        }
                    }
                    #endregion

                    #region Part
                    //Part*********************************************************************************************************************************
                    else if (l_propertyInfo.PropertyType == typeof(List<PartDocument>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (PartDocument l_partDocument in (List<PartDocument>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<PartDocument> l_tempClass = new ItemConverHelper<PartDocument>();
                                Item l_tempItem = l_tempClass.ItemConver(l_partDocument, pi_innovator, ConfigHelper.GetAPPConfigValue("PartDocument_ItemName"), pi_action);
                                if(!string.IsNullOrEmpty(l_partDocument.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.removeAttribute("id");
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_partDocument);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartCAD>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (PartCAD l_partCAD in (List<PartCAD>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<PartCAD> l_tempClass = new ItemConverHelper<PartCAD>();
                                Item l_tempItem = l_tempClass.ItemConver(l_partCAD, pi_innovator, ConfigHelper.GetAPPConfigValue("PartCAD_ItemName"), pi_action);
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.removeAttribute("id");
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_partCAD);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartSize>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (PartSize l_partSize in (List<PartSize>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<PartSize> l_tempClass = new ItemConverHelper<PartSize>();
                                Item l_tempItem = l_tempClass.ItemConver(l_partSize, pi_innovator, ConfigHelper.GetAPPConfigValue("PartSize_ItemName"), pi_action);
                                if(!string.IsNullOrEmpty(l_partSize.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.removeAttribute("id");
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_partSize);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }
                    }

                    else if (l_propertyInfo.PropertyType == typeof(List<PartContent>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (PartContent l_partContent in (List<PartContent>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<PartContent> l_tempClass = new ItemConverHelper<PartContent>();
                                Item l_tempItem = l_tempClass.ItemConver(l_partContent, pi_innovator, ConfigHelper.GetAPPConfigValue("PartContent_ItemName"), pi_action);
                                if(!string.IsNullOrEmpty(l_partContent.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.removeAttribute("id");
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_partContent);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }
                    }

                    #endregion

                    #region GarmentStyle

                    //GarmentStyle*********************************************************************************************************************************
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleColorWay>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleColorWay l_garmentstyleColorWay in (List<GarmentStyleColorWay>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleColorWay> l_tempClass = new ItemConverHelper<GarmentStyleColorWay>();
                                Item l_tempItem = l_tempClass.ItemConver(l_garmentstyleColorWay, pi_innovator, ConfigHelper.GetAPPConfigValue("GarmentStyleGarmentColorway_ItemName"), pi_action);
                                if(!string.IsNullOrEmpty(l_garmentstyleColorWay.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.removeAttribute("id");
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_garmentstyleColorWay);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleSizeRange>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleSizeRange l_garmentstyleSizeRange in (List<GarmentStyleSizeRange>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleSizeRange> l_tempClass = new ItemConverHelper<GarmentStyleSizeRange>();
                                Item l_tempItem = l_tempClass.ItemConver(l_garmentstyleSizeRange, pi_innovator, ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRange_ItemName"), pi_action);
                                if (!string.IsNullOrEmpty(l_garmentstyleSizeRange.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.removeAttribute("id");
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_garmentstyleSizeRange);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }
                    }

                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleSketch>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleSketch l_garmentstyleSketch in (List<GarmentStyleSketch>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleSketch> l_tempClass = new ItemConverHelper<GarmentStyleSketch>();
                                Item l_tempItem = l_tempClass.ItemConver(l_garmentstyleSketch, pi_innovator, ConfigHelper.GetAPPConfigValue("GarmentStyleSketch_ItemName"), pi_action);
                                if(!string.IsNullOrEmpty(l_garmentstyleSketch.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.removeAttribute("id");
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_garmentstyleSketch);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleCAD>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleCAD l_garmentstyleCAD in (List<GarmentStyleCAD>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleCAD> l_tempClass = new ItemConverHelper<GarmentStyleCAD>();
                                Item l_tempItem = l_tempClass.ItemConver(l_garmentstyleCAD, pi_innovator, ConfigHelper.GetAPPConfigValue("GarmentStyleCAD_ItemName"), pi_action);
                                if(!string.IsNullOrEmpty(l_garmentstyleCAD.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.removeAttribute("id");
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_garmentstyleCAD);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleDocument>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleDocument l_garmentstyleDocument in (List<GarmentStyleDocument>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleDocument> l_tempClass = new ItemConverHelper<GarmentStyleDocument>();
                                Item l_tempItem = l_tempClass.ItemConver(l_garmentstyleDocument, pi_innovator, ConfigHelper.GetAPPConfigValue("GarmentStyleDocument_ItemName"), pi_action);
                                if(!string.IsNullOrEmpty(l_garmentstyleDocument.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.removeAttribute("id");
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_garmentstyleDocument);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleYearSeason>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleYearSeason l_garmentstyleDocument in (List<GarmentStyleYearSeason>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleYearSeason> l_tempClass = new ItemConverHelper<GarmentStyleYearSeason>();
                                Item l_tempItem = l_tempClass.ItemConver(l_garmentstyleDocument, pi_innovator, ConfigHelper.GetAPPConfigValue("GarmentStyleYearSeason_ItemName"), pi_action);
                                if(!string.IsNullOrEmpty(l_garmentstyleDocument.ID))
                                {
                                    l_tempItem.setAction("get");
                                    l_tempItem.removeAttribute("id");
                                    //Get Property List
                                    string[] l_getPropertyList = l_tempClass.GetItemPropertyList(l_garmentstyleDocument);
                                    for (int k = 0; k < l_getPropertyList.Length; k++)
                                    {
                                        if (!string.IsNullOrEmpty(l_getPropertyList[k]))
                                        {
                                            l_tempItem.removeProperty(l_getPropertyList[k]);
                                        }
                                    }
                                }
                                l_newItem.addRelationship(l_tempItem);
                            }
                        }
                    }
                    #endregion

                    else
                    {
                        #region Common
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {                            
                            if (!string.IsNullOrEmpty(l_propertyInfo.GetValue(pi_object, null).ToString()) && l_propertyInfo.GetValue(pi_object, null).ToString() != "0001/1/1 0:00:00" && l_propertyInfo.GetValue(pi_object, null).ToString() != "1/1/0001 12:00:00 AM")
                            {
                                l_newItem.setProperty(l_propertyInfo.Name.ToLower(), l_propertyInfo.GetValue(pi_object, null).ToString());
                            }
                        }
                        #endregion
                    }
                }

                return l_newItem;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Convert Class To Item for edit
        /// 2015-08-20 add by WesChen
        /// </summary>
        /// <param name="pi_object"></param>
        /// <param name="pi_operationItem"></param>
        /// <returns></returns>
        public Item ItemConver(T pi_object, Item pi_operationItem, Innovator pi_innovator, string pi_action)
        {
            try
            {
                foreach (PropertyInfo l_propertyInfo in typeof(T).GetProperties())
                {
                    #region Link
                    //Link*********************************************************************************************************************************
                    if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleBOM>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleBOM l_garmentstyleBOM in (List<GarmentStyleBOM>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleBOM> l_tempClass = new ItemConverHelper<GarmentStyleBOM>();
                                if (!string.IsNullOrEmpty(l_garmentstyleBOM.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("GarmentStyleBOM_ItemName"), l_garmentstyleBOM.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_garmentstyleBOM, l_getItem, pi_innovator, pi_action);
                                    Item l_return= l_getItem.apply();
                                    if(l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_garmentstyleBOM, null, pi_innovator, pi_action);
                                }
                                
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentBOM>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentBOM l_garmentBOM in (List<GarmentBOM>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentBOM> l_tempClass = new ItemConverHelper<GarmentBOM>();
                                if (!string.IsNullOrEmpty(l_garmentBOM.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("GarmentBOM_ItemName"), l_garmentBOM.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem= l_tempClass.ItemConver(l_garmentBOM, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_garmentBOM, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentBOMPart>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentBOMPart l_garmentBOMPart in (List<GarmentBOMPart>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentBOMPart> l_tempClass = new ItemConverHelper<GarmentBOMPart>();
                                if (!string.IsNullOrEmpty(l_garmentBOMPart.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("GarmentBOMPart_ItemName"), l_garmentBOMPart.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_garmentBOMPart, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_garmentBOMPart, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<Part>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (Part l_part in (List<Part>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<Part> l_tempClass = new ItemConverHelper<Part>();
                                if (!string.IsNullOrEmpty(l_part.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("Part_ItemName"), l_part.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_part, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_part, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartColorCombo>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (PartColorCombo l_partColorCombo in (List<PartColorCombo>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<PartColorCombo> l_tempClass = new ItemConverHelper<PartColorCombo>();
                                if (!string.IsNullOrEmpty(l_partColorCombo.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), l_partColorCombo.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_partColorCombo, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_partColorCombo, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorCombo>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (ColorCombo l_colorCombo in (List<ColorCombo>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<ColorCombo> l_tempClass = new ItemConverHelper<ColorCombo>();
                                if (!string.IsNullOrEmpty(l_colorCombo.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), l_colorCombo.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_colorCombo, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_colorCombo, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorComboColorCode>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (ColorComboColorCode l_colorComboColorCode in (List<ColorComboColorCode>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<ColorComboColorCode> l_tempClass = new ItemConverHelper<ColorComboColorCode>();
                                if (!string.IsNullOrEmpty(l_colorComboColorCode.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName"), l_colorComboColorCode.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_colorComboColorCode, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_colorComboColorCode, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorCode>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (ColorCode l_colorCode in (List<ColorCode>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<ColorCode> l_tempClass = new ItemConverHelper<ColorCode>();
                                if (!string.IsNullOrEmpty(l_colorCode.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("ColorCode_ItemName"), l_colorCode.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_colorCode, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_colorCode, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    #endregion

                    #region Part
                    //Part*********************************************************************************************************************************
                    else if (l_propertyInfo.PropertyType == typeof(List<PartDocument>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (PartDocument l_partDocument in (List<PartDocument>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<PartDocument> l_tempClass = new ItemConverHelper<PartDocument>();
                                if (!string.IsNullOrEmpty(l_partDocument.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("PartDocument_ItemName"), l_partDocument.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem= l_tempClass.ItemConver(l_partDocument, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_partDocument, null, pi_innovator, pi_action);
                                }
                            }
                        }

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartCAD>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (PartCAD l_partCAD in (List<PartCAD>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<PartCAD> l_tempClass = new ItemConverHelper<PartCAD>();
                                if (!string.IsNullOrEmpty(l_partCAD.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("PartCAD_ItemName"), l_partCAD.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_partCAD, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_partCAD, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartSize>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (PartSize l_partSize in (List<PartSize>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<PartSize> l_tempClass = new ItemConverHelper<PartSize>();
                                if (!string.IsNullOrEmpty(l_partSize.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"), l_partSize.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_partSize, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_partSize, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }

                    else if (l_propertyInfo.PropertyType == typeof(List<PartContent>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (PartContent l_partContent in (List<PartContent>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<PartContent> l_tempClass = new ItemConverHelper<PartContent>();
                                if (!string.IsNullOrEmpty(l_partContent.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"), l_partContent.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem= l_tempClass.ItemConver(l_partContent, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_partContent, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    #endregion

                    #region GarmentStyle
                    //GarmentStyle*********************************************************************************************************************************
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleColorWay>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleColorWay l_garmentstyleColorWay in (List<GarmentStyleColorWay>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleColorWay> l_tempClass = new ItemConverHelper<GarmentStyleColorWay>();
                                if (!string.IsNullOrEmpty(l_garmentstyleColorWay.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("GarmentStyleGarmentColorway_ItemName"), l_garmentstyleColorWay.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_garmentstyleColorWay, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_garmentstyleColorWay, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleSizeRange>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleSizeRange l_garmentstyleSizeRange in (List<GarmentStyleSizeRange>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleSizeRange> l_tempClass = new ItemConverHelper<GarmentStyleSizeRange>();
                                if (!string.IsNullOrEmpty(l_garmentstyleSizeRange.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRange_ItemName"), l_garmentstyleSizeRange.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_garmentstyleSizeRange, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_garmentstyleSizeRange, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }

                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleSketch>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleSketch l_garmentstyleSketch in (List<GarmentStyleSketch>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleSketch> l_tempClass = new ItemConverHelper<GarmentStyleSketch>();
                                if (!string.IsNullOrEmpty(l_garmentstyleSketch.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("GarmentStyleSketch_ItemName"), l_garmentstyleSketch.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_garmentstyleSketch, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_garmentstyleSketch, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleCAD>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleCAD l_garmentstyleCAD in (List<GarmentStyleCAD>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleCAD> l_tempClass = new ItemConverHelper<GarmentStyleCAD>();
                                if (!string.IsNullOrEmpty(l_garmentstyleCAD.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("GarmentStyleCAD_ItemName"), l_garmentstyleCAD.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_garmentstyleCAD, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_garmentstyleCAD, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleDocument>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleDocument l_garmentstyleDocument in (List<GarmentStyleDocument>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleDocument> l_tempClass = new ItemConverHelper<GarmentStyleDocument>();
                                if (!string.IsNullOrEmpty(l_garmentstyleDocument.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("GarmentStyleDocument_ItemName"), l_garmentstyleDocument.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_garmentstyleDocument, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_garmentstyleDocument, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleYearSeason>))
                    {
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            foreach (GarmentStyleYearSeason l_garmentstyleDocument in (List<GarmentStyleYearSeason>)l_propertyInfo.GetValue(pi_object, null))
                            {
                                ItemConverHelper<GarmentStyleYearSeason> l_tempClass = new ItemConverHelper<GarmentStyleYearSeason>();
                                if (!string.IsNullOrEmpty(l_garmentstyleDocument.ID))
                                {
                                    Item l_getItem = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("GarmentStyleYearSeason_ItemName"), l_garmentstyleDocument.ID);
                                    l_getItem.setAction(pi_action);
                                    l_getItem = l_tempClass.ItemConver(l_garmentstyleDocument, l_getItem, pi_innovator, pi_action);
                                    Item l_return = l_getItem.apply();
                                    if (l_return.isError())
                                    {
                                        throw new Exception(l_return.getErrorDetail());
                                    }
                                }
                                else
                                {
                                    l_tempClass.ItemConver(l_garmentstyleDocument, null, pi_innovator, pi_action);
                                }
                            }
                        }
                    }
                    #endregion
                    else
                    {
                        #region Common
                        if (l_propertyInfo.GetValue(pi_object, null) != null)
                        {
                            if (!string.IsNullOrEmpty(l_propertyInfo.GetValue(pi_object, null).ToString()) && l_propertyInfo.GetValue(pi_object, null).ToString() != "0001/1/1 0:00:00" && l_propertyInfo.GetValue(pi_object, null).ToString() != "1/1/0001 12:00:00 AM")
                            {
                                if (pi_operationItem != null)
                                {
                                    pi_operationItem.setProperty(l_propertyInfo.Name.ToLower(), l_propertyInfo.GetValue(pi_object, null).ToString());
                                }
                            }
                        }
                        #endregion
                    }
                }

                return pi_operationItem;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Object Propery List
        /// 2015-09-04 add by WesChen Test Successed
        /// </summary>
        /// <param name="pi_object"></param>
        /// <returns></returns>
        public string[] GetItemPropertyList(T pi_object)
        {
            if (pi_object == null) { return null; }
            try
            {
                int l_getProtyCount = typeof(T).GetProperties().Count();
                int l_runProtyIndex = 0;
                string[] l_property = new string[l_getProtyCount];

                foreach (PropertyInfo l_propertyInfo in typeof(T).GetProperties())
                {

                    #region Link
                    //Link*********************************************************************************************************************************
                    if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleBOM>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentBOM>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentBOMPart>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<Part>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartColorCombo>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorCombo>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorComboColorCode>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorCode>))
                    {

                    }
                    #endregion

                    #region Part
                    //Part*********************************************************************************************************************************
                    else if (l_propertyInfo.PropertyType == typeof(List<PartDocument>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartCAD>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartSize>))
                    {

                    }

                    else if (l_propertyInfo.PropertyType == typeof(List<PartContent>))
                    {

                    }
                    #endregion

                    #region GarmentStyle
                    //GarmentStyle*********************************************************************************************************************************
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleColorWay>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleSizeRange>))
                    {

                    }

                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleSketch>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleCAD>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleDocument>))
                    {

                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleYearSeason>))
                    {

                    }
                    #endregion

                    else
                    {
                        #region common property

                        l_property[l_runProtyIndex] = l_propertyInfo.Name.ToLower();
                        l_runProtyIndex++;

                        #endregion
                    }
                }





                return l_property;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }        
    }
}