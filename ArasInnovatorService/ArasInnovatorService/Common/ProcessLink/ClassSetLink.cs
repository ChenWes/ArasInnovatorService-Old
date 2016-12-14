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
    public class ClassSetLink<T>
    {
        public List<T> SetLinkPropery<T>(List<T> l_tempTList, Innovator pi_innovator)
        {
            if (l_tempTList == null)
            {
                return null;
            }

            foreach (T l_tempT in l_tempTList)
            {
                foreach (PropertyInfo l_propertyInfo in typeof(T).GetProperties())
                {
                    #region Link
                    //Link*********************************************************************************************************************************
                    if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleBOM>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (GarmentStyleBOM l_tempObject in (List<GarmentStyleBOM>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<GarmentStyleBOM> l_tempClass = new ClassSetLink<GarmentStyleBOM>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<GarmentStyleBOM>)l_propertyInfo.GetValue(l_tempT, null),pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentBOM>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (GarmentBOM l_tempObject in (List<GarmentBOM>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<GarmentBOM> l_tempClass = new ClassSetLink<GarmentBOM>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<GarmentBOM>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentBOMPart>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (GarmentBOMPart l_tempObject in (List<GarmentBOMPart>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<GarmentBOMPart> l_tempClass = new ClassSetLink<GarmentBOMPart>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<GarmentBOMPart>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<Part>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (Part l_tempObject in (List<Part>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<Part> l_tempClass = new ClassSetLink<Part>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<Part>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartColorCombo>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (PartColorCombo l_tempObject in (List<PartColorCombo>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<PartColorCombo> l_tempClass = new ClassSetLink<PartColorCombo>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<PartColorCombo>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorCombo>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (ColorCombo l_tempObject in (List<ColorCombo>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<ColorCombo> l_tempClass = new ClassSetLink<ColorCombo>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<ColorCombo>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorComboColorCode>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (ColorComboColorCode l_tempObject in (List<ColorComboColorCode>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<ColorComboColorCode> l_tempClass = new ClassSetLink<ColorComboColorCode>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<ColorComboColorCode>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorCode>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (ColorCode l_tempObject in (List<ColorCode>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<ColorCode> l_tempClass = new ClassSetLink<ColorCode>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<ColorCode>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    #endregion

                    #region Part
                    //Part*********************************************************************************************************************************
                    else if (l_propertyInfo.PropertyType == typeof(List<PartDocument>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (PartDocument l_tempObject in (List<PartDocument>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<PartDocument> l_tempClass = new ClassSetLink<PartDocument>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<PartDocument>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartCAD>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (PartCAD l_tempObject in (List<PartCAD>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<PartCAD> l_tempClass = new ClassSetLink<PartCAD>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<PartCAD>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartSize>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (PartSize l_tempObject in (List<PartSize>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<PartSize> l_tempClass = new ClassSetLink<PartSize>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<PartSize>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }

                    else if (l_propertyInfo.PropertyType == typeof(List<PartContent>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (PartContent l_tempObject in (List<PartContent>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<PartContent> l_tempClass = new ClassSetLink<PartContent>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<PartContent>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    #endregion

                    #region GarmentStyle
                    //GarmentStyle*********************************************************************************************************************************
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleColorWay>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (GarmentStyleColorWay l_tempObject in (List<GarmentStyleColorWay>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<GarmentStyleColorWay> l_tempClass = new ClassSetLink<GarmentStyleColorWay>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<GarmentStyleColorWay>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleSizeRange>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (GarmentStyleSizeRange l_tempObject in (List<GarmentStyleSizeRange>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<GarmentStyleSizeRange> l_tempClass = new ClassSetLink<GarmentStyleSizeRange>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<GarmentStyleSizeRange>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }

                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleSketch>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (GarmentStyleSketch l_tempObject in (List<GarmentStyleSketch>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<GarmentStyleSketch> l_tempClass = new ClassSetLink<GarmentStyleSketch>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<GarmentStyleSketch>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleCAD>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (GarmentStyleCAD l_tempObject in (List<GarmentStyleCAD>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<GarmentStyleCAD> l_tempClass = new ClassSetLink<GarmentStyleCAD>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<GarmentStyleCAD>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleDocument>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (GarmentStyleDocument l_tempObject in (List<GarmentStyleDocument>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<GarmentStyleDocument> l_tempClass = new ClassSetLink<GarmentStyleDocument>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<GarmentStyleDocument>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleYearSeason>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            foreach (GarmentStyleYearSeason l_tempObject in (List<GarmentStyleYearSeason>)l_propertyInfo.GetValue(l_tempT, null))
                            {
                                ClassSetLink<GarmentStyleYearSeason> l_tempClass = new ClassSetLink<GarmentStyleYearSeason>();
                                l_propertyInfo.SetValue(l_tempT, l_tempClass.SetLinkPropery((List<GarmentStyleYearSeason>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                            }
                        }
                    }
                    #endregion

                    #region Common
                    switch (l_propertyInfo.Name.ToUpper())
                    {
                        case "CN_CUST_CODE":
                            if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                            {
                                Item l_searchItem = pi_innovator.newItem(ConfigHelper.GetAPPConfigValue("Customer_ItemName"), "get");
                                l_searchItem.setProperty("keyed_name".ToLower(), l_propertyInfo.GetValue(l_tempT, null).ToString());
                                Item l_getCustomer = l_searchItem.apply();
                                if (l_getCustomer.getItemCount() != 0)
                                {
                                    l_propertyInfo.SetValue(l_tempT, l_getCustomer.getItemByIndex(0).getProperty("id"), null);
                                }
                            }
                            break;
                        case "CN_BRAND_CODE":
                            if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                            {
                                Item l_searchItem = pi_innovator.newItem(ConfigHelper.GetAPPConfigValue("CustomerBrand_ItemName"), "get");
                                l_searchItem.setProperty("keyed_name".ToLower(), l_propertyInfo.GetValue(l_tempT, null).ToString());
                                Item l_getBrand = l_searchItem.apply();
                                if (l_getBrand.getItemCount() != 0)
                                {
                                    l_propertyInfo.SetValue(l_tempT, l_getBrand.getItemByIndex(0).getProperty("id"), null);
                                }
                            }
                            break;
                        case "CURRENT_STATE":
                            if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                            {
                                Item l_searchItem = pi_innovator.newItem(ConfigHelper.GetAPPConfigValue("State_ItemName"), "get");
                                l_searchItem.setProperty("keyed_name", l_propertyInfo.GetValue(l_tempT, null).ToString());
                                Item l_getUser = l_searchItem.apply();
                                if (l_getUser.getItemCount() != 0)
                                {
                                    l_propertyInfo.SetValue(l_tempT, l_getUser.getItemByIndex(0).getProperty("id"), null);
                                }
                            }
                            break;
                        case "CREATED_BY_ID":
                            if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                            {
                                Item l_searchItem = pi_innovator.newItem(ConfigHelper.GetAPPConfigValue("User_ItemName"), "get");
                                l_searchItem.setProperty("keyed_name".ToLower(), l_propertyInfo.GetValue(l_tempT, null).ToString());
                                Item l_getUser = l_searchItem.apply();
                                if (l_getUser.getItemCount() != 0)
                                {
                                    l_propertyInfo.SetValue(l_tempT, l_getUser.getItemByIndex(0).getProperty("id"), null);
                                }
                            }
                            break;
                        case "MODIFIED_BY_ID":
                            if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                            {
                                Item l_searchItem = pi_innovator.newItem(ConfigHelper.GetAPPConfigValue("User_ItemName"), "get");
                                l_searchItem.setProperty("keyed_name".ToLower(), l_propertyInfo.GetValue(l_tempT, null).ToString());
                                Item l_getUser = l_searchItem.apply();
                                if (l_getUser.getItemCount() != 0)
                                {
                                    l_propertyInfo.SetValue(l_tempT, l_getUser.getItemByIndex(0).getProperty("id"), null);
                                }
                            }
                            break;
                        case "LOCKED_BY_ID":
                            if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                            {
                                Item l_searchItem = pi_innovator.newItem(ConfigHelper.GetAPPConfigValue("User_ItemName"), "get");
                                l_searchItem.setProperty("keyed_name".ToLower(), l_propertyInfo.GetValue(l_tempT, null).ToString());
                                Item l_getUser = l_searchItem.apply();
                                if (l_getUser.getItemCount() != 0)
                                {
                                    l_propertyInfo.SetValue(l_tempT, l_getUser.getItemByIndex(0).getProperty("id"), null);
                                }
                            }
                            break;
                        case "PERMISSION_ID":
                            if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                            {
                                Item l_searchItem = pi_innovator.newItem(ConfigHelper.GetAPPConfigValue("Permission_ItemName"), "get");
                                l_searchItem.setProperty("keyed_name".ToLower(), l_propertyInfo.GetValue(l_tempT, null).ToString());
                                Item l_getBrand = l_searchItem.apply();
                                if (l_getBrand.getItemCount() != 0)
                                {
                                    l_propertyInfo.SetValue(l_tempT, l_getBrand.getItemByIndex(0).getProperty("id"), null);
                                }
                            }
                            break;
                        default:
                            break;
                    }

                    #endregion
                }

            }

            return l_tempTList;
        }
    }
}