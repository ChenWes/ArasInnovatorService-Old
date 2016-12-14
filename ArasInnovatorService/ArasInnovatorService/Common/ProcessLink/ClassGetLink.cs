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
    public class ClassGetLink<T>
    {
        public List<T> GetLinkPropery<T>(List<T> l_tempTList, Innovator pi_innovator)
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
                            ClassGetLink<GarmentStyleBOM> l_tempClass = new ClassGetLink<GarmentStyleBOM>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<GarmentStyleBOM>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);                            
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentBOM>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<GarmentBOM> l_tempClass = new ClassGetLink<GarmentBOM>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<GarmentBOM>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentBOMPart>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<GarmentBOMPart> l_tempClass = new ClassGetLink<GarmentBOMPart>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<GarmentBOMPart>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<Part>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<Part> l_tempClass = new ClassGetLink<Part>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<Part>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartColorCombo>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<PartColorCombo> l_tempClass = new ClassGetLink<PartColorCombo>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<PartColorCombo>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorCombo>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<ColorCombo> l_tempClass = new ClassGetLink<ColorCombo>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<ColorCombo>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorComboColorCode>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<ColorComboColorCode> l_tempClass = new ClassGetLink<ColorComboColorCode>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<ColorComboColorCode>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<ColorCode>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<ColorCode> l_tempClass = new ClassGetLink<ColorCode>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<ColorCode>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    #endregion

                    #region Part
                    //Part*********************************************************************************************************************************
                    else if (l_propertyInfo.PropertyType == typeof(List<PartDocument>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<PartDocument> l_tempClass = new ClassGetLink<PartDocument>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<PartDocument>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartCAD>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<PartCAD> l_tempClass = new ClassGetLink<PartCAD>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<PartCAD>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<PartSize>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<PartSize> l_tempClass = new ClassGetLink<PartSize>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<PartSize>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }

                    else if (l_propertyInfo.PropertyType == typeof(List<PartContent>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<PartContent> l_tempClass = new ClassGetLink<PartContent>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<PartContent>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    #endregion

                    #region GarmentStyle
                    //GarmentStyle*********************************************************************************************************************************
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleColorWay>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<GarmentStyleColorWay> l_tempClass = new ClassGetLink<GarmentStyleColorWay>();                            
                            l_propertyInfo.SetValue(l_tempT,l_tempClass.GetLinkPropery((List<GarmentStyleColorWay>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleSizeRange>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<GarmentStyleSizeRange> l_tempClass = new ClassGetLink<GarmentStyleSizeRange>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<GarmentStyleSizeRange>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }

                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleSketch>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<GarmentStyleSketch> l_tempClass = new ClassGetLink<GarmentStyleSketch>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<GarmentStyleSketch>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleCAD>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<GarmentStyleCAD> l_tempClass = new ClassGetLink<GarmentStyleCAD>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<GarmentStyleCAD>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleDocument>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<GarmentStyleDocument> l_tempClass = new ClassGetLink<GarmentStyleDocument>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<GarmentStyleDocument>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    else if (l_propertyInfo.PropertyType == typeof(List<GarmentStyleYearSeason>))
                    {
                        if (l_propertyInfo.GetValue(l_tempT, null) != null)
                        {
                            ClassGetLink<GarmentStyleYearSeason> l_tempClass = new ClassGetLink<GarmentStyleYearSeason>();
                            l_propertyInfo.SetValue(l_tempT, l_tempClass.GetLinkPropery((List<GarmentStyleYearSeason>)l_propertyInfo.GetValue(l_tempT, null), pi_innovator), null);
                        }
                    }
                    #endregion

                    #region Common

                    else
                    {
                        switch (l_propertyInfo.Name.ToUpper())
                        {
                            case "CN_CUST_CODE":
                                if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                                {
                                    Item l_getCustomer = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("Customer_ItemName"), l_propertyInfo.GetValue(l_tempT).ToString());
                                    if (l_getCustomer.getItemCount() != 0)
                                    {
                                        l_propertyInfo.SetValue(l_tempT, l_getCustomer.getItemByIndex(0).getProperty("keyed_name"), null);
                                        if (l_tempT.GetType().GetProperty("CN_CUST_NAME") != null)
                                        {
                                            l_tempT.GetType().GetProperty("CN_CUST_NAME").SetValue(l_tempT, l_getCustomer.getItemByIndex(0).getProperty("name"), null);
                                        }
                                    }
                                }
                                break;
                            case "CN_BRAND_CODE":
                                if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                                {
                                    Item l_getBrand = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("CustomerBrand_ItemName"), l_propertyInfo.GetValue(l_tempT).ToString());
                                    if (l_getBrand.getItemCount() != 0)
                                    {
                                        l_propertyInfo.SetValue(l_tempT, l_getBrand.getItemByIndex(0).getProperty("keyed_name"), null);
                                        if (l_tempT.GetType().GetProperty("CN_BRAND_NAME") != null)
                                        {
                                            l_tempT.GetType().GetProperty("CN_BRAND_NAME").SetValue(l_tempT, l_getBrand.getItemByIndex(0).getProperty("cn_brand"), null);
                                        }
                                    }
                                }
                                break;
                            case "CURRENT_STATE":
                                if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                                {
                                    Item l_getBrand = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("State_ItemName"), l_propertyInfo.GetValue(l_tempT).ToString());
                                    if (l_getBrand.getItemCount() != 0)
                                    {
                                        l_propertyInfo.SetValue(l_tempT, l_getBrand.getItemByIndex(0).getProperty("keyed_name"), null);
                                        if (l_tempT.GetType().GetProperty("CURRENT_STATE_DESC") != null)
                                        {
                                            l_tempT.GetType().GetProperty("CURRENT_STATE_DESC").SetValue(l_tempT, l_getBrand.getItemByIndex(0).getProperty("keyed_name"), null);
                                        }
                                    }
                                }
                                break;
                            case "CREATED_BY_ID":
                                if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                                {
                                    Item l_getUser = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("User_ItemName"), l_propertyInfo.GetValue(l_tempT).ToString());
                                    if (l_getUser.getItemCount() != 0)
                                    {
                                        l_propertyInfo.SetValue(l_tempT, l_getUser.getItemByIndex(0).getProperty("keyed_name"), null);
                                        if (l_tempT.GetType().GetProperty("CREATED_BY_NAME") != null)
                                        {
                                            l_tempT.GetType().GetProperty("CREATED_BY_NAME").SetValue(l_tempT, l_getUser.getItemByIndex(0).getProperty("keyed_name"), null);
                                        }
                                    }
                                }
                                break;
                            case "MODIFIED_BY_ID":
                                if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                                {
                                    Item l_getUser = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("User_ItemName"), l_propertyInfo.GetValue(l_tempT).ToString());
                                    if (l_getUser.getItemCount() != 0)
                                    {
                                        l_propertyInfo.SetValue(l_tempT, l_getUser.getItemByIndex(0).getProperty("keyed_name"), null);
                                        if (l_tempT.GetType().GetProperty("MODIFIED_BY_NAME") != null)
                                        {
                                            l_tempT.GetType().GetProperty("MODIFIED_BY_NAME").SetValue(l_tempT, l_getUser.getItemByIndex(0).getProperty("keyed_name"), null);
                                        }
                                    }
                                }
                                break;
                            case "LOCKED_BY_ID":
                                if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                                {
                                    Item l_getUser = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("User_ItemName"), l_propertyInfo.GetValue(l_tempT).ToString());
                                    if (l_getUser.getItemCount() != 0)
                                    {
                                        l_propertyInfo.SetValue(l_tempT, l_getUser.getItemByIndex(0).getProperty("keyed_name"), null);
                                        if (l_tempT.GetType().GetProperty("LOCKED_BY_NAME") != null)
                                        {
                                            l_tempT.GetType().GetProperty("LOCKED_BY_NAME").SetValue(l_tempT, l_getUser.getItemByIndex(0).getProperty("keyed_name"), null);
                                        }
                                    }
                                }
                                break;
                            case "PERMISSION_ID":
                                if (l_propertyInfo.GetValue(l_tempT) != null && !string.IsNullOrEmpty(l_propertyInfo.GetValue(l_tempT, null).ToString()))
                                {
                                    Item l_getBrand = pi_innovator.getItemById(ConfigHelper.GetAPPConfigValue("Permission_ItemName"), l_propertyInfo.GetValue(l_tempT).ToString());
                                    if (l_getBrand.getItemCount() != 0)
                                    {
                                        l_propertyInfo.SetValue(l_tempT, l_getBrand.getItemByIndex(0).getProperty("keyed_name"), null);
                                        if (l_tempT.GetType().GetProperty("PERMISSION_NAME") != null)
                                        {
                                            l_tempT.GetType().GetProperty("PERMISSION_NAME").SetValue(l_tempT, l_getBrand.getItemByIndex(0).getProperty("name"), null);
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                    }

                    #endregion
                }
            }

            return l_tempTList;
        }        
    }
}