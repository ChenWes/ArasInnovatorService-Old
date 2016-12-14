using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using ArasInnovatorService.ArasModel.PublicModel;
using ArasInnovatorService.Common;

using Aras;
using Aras.IOM;

namespace ArasInnovatorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FabricPartManageService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FabricPartManageService.svc or FabricPartManageService.svc.cs at the Solution Explorer and start debugging.
    public class FabricPartManageService : IFabricPartManageService
    {
        HttpServerConnection m_Connection;
        Innovator m_Innovator;

        public void ArasServiceConfig(string pi_userName, string pi_pwd)
        {
            m_Connection = IomFactory.CreateHttpServerConnection(ConfigHelper.GetAPPConfigValue("InnovatorUrl"), ConfigHelper.GetAPPConfigValue("InnovatorDB"), pi_userName, Innovator.ScalcMD5(pi_pwd));
            m_Innovator = IomFactory.CreateInnovator(m_Connection);
        }

        public FabricPartClass GetFabricPartList(SelectionFilter pi_selectionFilter, int pi_pageIndex, int pi_pageSize)
        {
            FabricPartClass l_returnClass = new FabricPartClass();
            l_returnClass.SuccessFlag = true;

            try
            {
                #region connection and get new item
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");
                #endregion

                #region page index and page size
                if (pi_pageIndex != null && pi_pageIndex > 0)
                {
                    l_getItem.setAttribute("page", pi_pageIndex.ToString());
                    l_returnClass.DisplayPageIndex = pi_pageIndex;
                }
                else
                {
                    pi_pageIndex = l_returnClass.DisplayPageIndex;
                    l_getItem.setAttribute("page", pi_pageIndex.ToString());                    
                }
                if (pi_pageSize != null && pi_pageSize > 0)
                {
                    l_getItem.setAttribute("pagesize", pi_pageSize.ToString());
                    l_returnClass.DisplayPageSize = pi_pageSize;
                }
                else
                {
                    pi_pageSize = l_returnClass.DisplayPageSize;
                    l_getItem.setAttribute("pagesize", pi_pageSize.ToString());                    
                }
                #endregion

                #region Parse Selection And Search
                Item l_returnItem = null;
                if (pi_selectionFilter != null)
                {
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(SelectionFilter.CreateAndFilter(new SelectionFilter[] { SelectionFilter.CreateLeaf("CN_CLASS0", "EQ", "FAB"), pi_selectionFilter }), l_getItem);

                    #region Search AML

                    //get Part Document List
                    Item l_getPartDocument = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartDocument_ItemName"), "get");
                    l_searchItem.addRelationship(l_getPartDocument);
                    //get Part CAD List
                    Item l_getPartCAD = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartCAD_ItemName"), "get");
                    l_searchItem.addRelationship(l_getPartCAD);
                    //get Part Size List
                    Item l_getPartSize = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"), "get");
                    l_searchItem.addRelationship(l_getPartSize);
                    //get Part Color Combo List
                    //Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    //l_searchItem.addRelationship(l_getPartColorCombo);
                    //----------------------------------------------------------------------------------------------                    

                    Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    Item l_colorComboColorCodeLink = l_getPartColorCombo.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCombo_ItemName"), "get");

                    Item l_getColorComboColorCode = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName"), "get");
                    l_getColorComboColorCode.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCode_ItemName"), "get");

                    l_colorComboColorCodeLink.addRelationship(l_getColorComboColorCode);
                    l_searchItem.addRelationship(l_getPartColorCombo);
                    //----------------------------------------------------------------------------------------------

                    //get part Content List
                    Item l_getPartContent = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"), "get");
                    l_searchItem.addRelationship(l_getPartContent);
                    #endregion

                    l_returnItem = l_searchItem.apply();
                }
                else
                {
                    #region Search AML
                    l_getItem = ParseSelectionFilter.ParseSelection(SelectionFilter.CreateAndFilter(new SelectionFilter[] { SelectionFilter.CreateLeaf("CN_CLASS0", "EQ", "FAB") }), l_getItem);

                    //get Part Document List
                    Item l_getPartDocument = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartDocument_ItemName"), "get");
                    l_getItem.addRelationship(l_getPartDocument);
                    //get Part CAD List
                    Item l_getPartCAD = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartCAD_ItemName"), "get");
                    l_getItem.addRelationship(l_getPartCAD);
                    //get Part Size List
                    Item l_getPartSize = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"), "get");
                    l_getItem.addRelationship(l_getPartSize);
                    //get Part Color Combo List
                    //Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    //l_searchItem.addRelationship(l_getPartColorCombo);
                    //----------------------------------------------------------------------------------------------                    

                    Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    Item l_colorComboColorCodeLink = l_getPartColorCombo.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCombo_ItemName"), "get");

                    Item l_getColorComboColorCode = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName"), "get");
                    l_getColorComboColorCode.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCode_ItemName"), "get");

                    l_colorComboColorCodeLink.addRelationship(l_getColorComboColorCode);
                    l_getItem.addRelationship(l_getPartColorCombo);
                    //----------------------------------------------------------------------------------------------

                    //get part Content List
                    Item l_getPartContent = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"), "get");
                    l_getItem.addRelationship(l_getPartContent);

                    
                    #endregion

                    l_returnItem = l_getItem.apply();
                }
                #endregion

                #region Check Return Item
                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartList, "2", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "GetFabricPartList Error" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "GetFabricPartList Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }
                if (l_returnItem.isEmpty())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartList, "2", "");
                    l_returnClass.ErrorString = "GetFabricPartList Error: Return Is Empty";
                    l_returnClass.ErrorDetail = "GetFabricPartList Error: Return Is Empty, Maybe Is Not Matching Data, Please Edit Parameter And Try Later.";

                    return l_returnClass;
                }
                if (l_returnItem.getItemCount() == 0)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartList, "2", "");
                    l_returnClass.ErrorString = "GetFabricPartList Error: Return Is Empty";
                    l_returnClass.ErrorDetail = "GetFabricPartList Error: Return Is Empty, Maybe Is Not Matching Data, Please Edit Parameter And Try Later.";

                    return l_returnClass;
                }

                #endregion

                #region To List
                if (l_returnItem != null)
                {
                    if (!l_returnItem.isError() && !l_returnItem.isEmpty())
                    {
                        FabricPartList l_tempFabricPartList = new FabricPartList();

                        FabricPartBaseType[] l_tempFabricPartBaseTypeArray = new FabricPartBaseType[l_returnItem.getItemCount()];
                        for (int i = 0; i < l_returnItem.getItemCount(); i++)
                        {
                            FabricPartBaseType l_tempFabricPartBaseType = new FabricPartBaseType();
                            Item l_getFabricItem = l_returnItem.getItemByIndex(i);
                            #region 3.fabricpart

                            l_tempFabricPartBaseType.Description = l_getFabricItem.getProperty("description", "");

                            #region Content
                            Item l_getPartContentItem = l_getFabricItem.getRelationships(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"));
                            if (l_getPartContentItem != null)
                            {
                                if (!l_getPartContentItem.isError() && !l_getPartContentItem.isEmpty())
                                {
                                    FabricContentBaseType[] l_tempFabricContentBaseTypeArray = new FabricContentBaseType[l_getPartContentItem.getItemCount()];

                                    for (int i_getPartContentIndex = 0; i_getPartContentIndex < l_getPartContentItem.getItemCount(); i_getPartContentIndex++)
                                    {
                                        FabricContentBaseType l_tempFabricContentBaseType = new FabricContentBaseType();
                                        l_tempFabricContentBaseType.Description = l_getPartContentItem.getItemByIndex(i_getPartContentIndex).getProperty("cn_content", "");
                                        l_tempFabricContentBaseType.Ratio = float.Parse(l_getPartContentItem.getItemByIndex(i_getPartContentIndex).getProperty("cn_content_ratio", "0"));

                                        l_tempFabricContentBaseTypeArray[i_getPartContentIndex] = l_tempFabricContentBaseType;
                                    }
                                    l_tempFabricPartBaseType.Content = l_tempFabricContentBaseTypeArray;
                                }
                            }

                            #endregion

                            #region base info
                            l_tempFabricPartBaseType.YarnCount = l_getFabricItem.getProperty("cn_yarn_count", "");
                            l_tempFabricPartBaseType.Construction = l_getFabricItem.getProperty("cn_construction", "");
                            l_tempFabricPartBaseType.DyeMethod = l_getFabricItem.getProperty("cn_dye_method", "");
                            l_tempFabricPartBaseType.Finishing = l_getFabricItem.getProperty("cn_finishing", "");
                            l_tempFabricPartBaseType.Width = l_getFabricItem.getProperty("cn_fabric_width", "");
                            l_tempFabricPartBaseType.Pattern = l_getFabricItem.getProperty("cn_fabric_pattern", "");
                            l_tempFabricPartBaseType.Printing = l_getFabricItem.getProperty("cn_printing", "");
                            l_tempFabricPartBaseType.RepeatVertical = l_getFabricItem.getProperty("cn_rep_v", "");
                            l_tempFabricPartBaseType.RepeatHorizontal = l_getFabricItem.getProperty("cn_rep_h", "");
                            l_tempFabricPartBaseType.TestingGrade = l_getFabricItem.getProperty("cn_fab_test_grading", "");
                            l_tempFabricPartBaseType.Remarks = l_getFabricItem.getProperty("cn_remark", "");
                            #endregion

                            #region Header
                            //header
                            FabricPartHeaderType l_tempFabricPartHeaderType = new FabricPartHeaderType();
                            l_tempFabricPartHeaderType.Code = l_getFabricItem.getProperty("cn_ppo_agpo", "");
                            l_tempFabricPartHeaderType.Cateogry = l_getFabricItem.getProperty("cn_class1", "");
                            l_tempFabricPartHeaderType.WovenFabrication = l_getFabricItem.getProperty("cn_class2", "");
                            l_tempFabricPartHeaderType.KnitFabrication = "";
                            l_tempFabricPartHeaderType.Version = l_getFabricItem.getProperty("maj_rev", "");
                            l_tempFabricPartHeaderType.Status = l_getFabricItem.getProperty("state", "");

                            #region Header Set CustomerInfo
                            CustomerBaseType[] l_tempCustomerBaseTypeArray = new CustomerBaseType[1];
                            CustomerBaseType l_tempCustomerBaseType = new CustomerBaseType();

                            l_tempCustomerBaseType.CustomerCode = "";
                            l_tempCustomerBaseType.CustomerName = "";
                            l_tempCustomerBaseType.CustomerReferenceNum = "";
                            l_tempCustomerBaseType.BrandCode = "";
                            l_tempCustomerBaseType.BrandName = "";

                            //customer
                            string l_partCustomerID = l_getFabricItem.getProperty("cn_cust_code", "");
                            if (!string.IsNullOrEmpty(l_partCustomerID))
                            {
                                Item l_getPartCustomerItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Customer_ItemName"), l_partCustomerID);
                                if (!l_getPartCustomerItem.isError() && !l_getPartCustomerItem.isEmpty())
                                {
                                    l_tempCustomerBaseType.CustomerCode = l_getPartCustomerItem.getProperty("keyed_name", "");
                                    l_tempCustomerBaseType.CustomerName = l_getPartCustomerItem.getProperty("name", "");
                                    l_tempCustomerBaseType.CustomerReferenceNum = l_getPartCustomerItem.getProperty("cn_cust_item_id", "");
                                }
                            }
                            //brand
                            string l_partBrandID = l_getFabricItem.getProperty("cn_brand_code", "");
                            if (!string.IsNullOrEmpty(l_partBrandID))
                            {
                                Item l_getPartBrandItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("CustomerBrand_ItemName"), l_partBrandID);
                                if (!l_getPartBrandItem.isError() && !l_getPartBrandItem.isEmpty())
                                {
                                    l_tempCustomerBaseType.BrandCode = l_getPartBrandItem.getProperty("keyed_name", "");
                                    l_tempCustomerBaseType.BrandName = l_getPartBrandItem.getProperty("name", "");
                                }
                            }

                            l_tempCustomerBaseTypeArray[0] = l_tempCustomerBaseType;
                            //header set customer info
                            l_tempFabricPartHeaderType.CustomerInfo = l_tempCustomerBaseTypeArray;
                            #endregion
                            #endregion
                            l_tempFabricPartBaseType.FabricHeader = l_tempFabricPartHeaderType;

                            #region SupplierBaseType
                            SupplierBaseType[] l_tempSupplierBaseTypeArray = new SupplierBaseType[1];
                            SupplierBaseType l_tempSupplierBaseType = new SupplierBaseType();
                            string l_getPartSupplierID = l_getFabricItem.getProperty("cn_supplier_code", "");
                            if (!string.IsNullOrEmpty(l_getPartSupplierID))
                            {
                                Item l_getSupplierItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Supplier_ItemName"), l_getPartSupplierID);

                                l_tempSupplierBaseType.Code = l_getItem.getProperty("cn_supplier_code");
                                l_tempSupplierBaseType.ItemCode = l_getItem.getProperty("cn_supplier_item_code");
                                l_tempSupplierBaseType.SupplierName = l_getItem.getProperty("cn_class1");
                            }
                            l_tempSupplierBaseTypeArray[0] = l_tempSupplierBaseType;
                            #endregion
                            l_tempFabricPartBaseType.Supplier = l_tempSupplierBaseTypeArray;


                            //color combo
                            l_tempFabricPartBaseType.ColorCombo = null;

                            //fabric shrinkage
                            l_tempFabricPartBaseType.FabricShrinkage = null;

                            //garment shrinkage
                            l_tempFabricPartBaseType.GarmentShrinkage = null;

                            //price
                            PriceBaseType[] l_tempPriceBaseTypeArray = new PriceBaseType[1];
                            PriceBaseType l_tempPriceBaseType = new PriceBaseType();
                            l_tempPriceBaseType.Amount = Convert.ToDecimal(l_getFabricItem.getProperty("cn_remark", "0"));
                            l_tempPriceBaseType.Currency = "";//*
                            l_tempPriceBaseTypeArray[0] = l_tempPriceBaseType;
                            l_tempFabricPartBaseType.Price = l_tempPriceBaseTypeArray;

                            //year season                            
                            l_tempFabricPartBaseType.YearSeason = null;

                            //PartSize
                            Item l_getPartSizeItem = l_getFabricItem.getRelationships(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"));
                            if (!l_getPartSizeItem.isEmpty() && !l_getPartSizeItem.isError())
                            {
                                FabricSizeBaseType[] l_tempFabricSizeBaseTypeArray = new FabricSizeBaseType[l_getPartSizeItem.getItemCount()];
                                for (int i_partSizeIndex = 0; i_partSizeIndex < l_getPartSizeItem.getItemCount(); i_partSizeIndex++)
                                {
                                    FabricSizeBaseType l_tempFabricSizeBaseType = new FabricSizeBaseType();
                                    l_tempFabricSizeBaseType.Dimension = l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size_desc", "");
                                    l_tempFabricSizeBaseType.Measurement = 0; //l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size", "");//*
                                    l_tempFabricSizeBaseTypeArray[i_partSizeIndex] = l_tempFabricSizeBaseType;
                                }
                                l_tempFabricPartBaseType.Size = l_tempFabricSizeBaseTypeArray;
                            }

                            //loyout                                           



                            #endregion

                            l_tempFabricPartBaseTypeArray[i] = l_tempFabricPartBaseType;
                        }
                        l_tempFabricPartList.FabricPart = l_tempFabricPartBaseTypeArray;
                        l_returnClass.FabricPartList = l_tempFabricPartList;
                    }
                }
                #endregion

                #region Logout
                m_Connection.Logout();
                #endregion

                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "GetFabricPartList Error:" + ex.ToString());

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartList, "2", "");
                l_returnClass.ErrorString = "GetFabricPartList Error";
                l_returnClass.ErrorDetail = "GetFabricPartList Error:" + ex.Message;

                return l_returnClass;

            }
        }

        public FabricPartClass GetFabricPartByID(string pi_fabricPartID)
        {
            FabricPartClass l_returnClass = new FabricPartClass();
            l_returnClass.SuccessFlag = true;

            try
            {
                #region connection and get new item
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");
                #endregion

                #region page index and page size
                l_getItem.setAttribute("page", "1");
                l_returnClass.DisplayPageIndex = 1;
                l_getItem.setAttribute("pagesize", "1");
                l_returnClass.DisplayPageSize = 1;
                #endregion

                #region Parse Selection And Search
                Item l_returnItem = null;
                if (!string.IsNullOrEmpty(pi_fabricPartID))
                {
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(SelectionFilter.CreateAndFilter(new SelectionFilter[] { SelectionFilter.CreateLeaf("CN_CLASS0", "EQ", "FAB"), SelectionFilter.CreateLeaf("ITEM_NUMBER","EQ",pi_fabricPartID) }), l_getItem);

                    #region Search AML

                    //get Part Document List
                    Item l_getPartDocument = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartDocument_ItemName"), "get");
                    l_searchItem.addRelationship(l_getPartDocument);
                    //get Part CAD List
                    Item l_getPartCAD = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartCAD_ItemName"), "get");
                    l_searchItem.addRelationship(l_getPartCAD);
                    //get Part Size List
                    Item l_getPartSize = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"), "get");
                    l_searchItem.addRelationship(l_getPartSize);
                    //get Part Color Combo List
                    //Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    //l_searchItem.addRelationship(l_getPartColorCombo);
                    //----------------------------------------------------------------------------------------------                    

                    Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    Item l_colorComboColorCodeLink = l_getPartColorCombo.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCombo_ItemName"), "get");

                    Item l_getColorComboColorCode = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName"), "get");
                    l_getColorComboColorCode.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCode_ItemName"), "get");

                    l_colorComboColorCodeLink.addRelationship(l_getColorComboColorCode);
                    l_searchItem.addRelationship(l_getPartColorCombo);
                    //----------------------------------------------------------------------------------------------

                    //get part Content List
                    Item l_getPartContent = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"), "get");
                    l_searchItem.addRelationship(l_getPartContent);
                    #endregion

                    l_returnItem = l_searchItem.apply();
                }
                else
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartByID, "2", "");
                    l_returnClass.ErrorString = "GetFabricPartByID Error:pi_fabricPartID Is Null Or Empty.";
                    l_returnClass.ErrorDetail = "GetFabricPartByID Error:Please Setting pi_fabricPartID And Try Later.";

                    return l_returnClass;
                }
                #endregion

                #region Check Return Item
                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartByID, "2", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "GetFabricPartByID Error" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "GetFabricPartByID Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }
                if (l_returnItem.isEmpty())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartByID, "2", "");
                    l_returnClass.ErrorString = "GetFabricPartByID Error: Return Is Empty";
                    l_returnClass.ErrorDetail = "GetFabricPartByID Error: Return Is Empty, Maybe Is Not Matching Data, Please Edit Parameter And Try Later.";

                    return l_returnClass;
                }
                if (l_returnItem.getItemCount() == 0)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartByID, "2", "");
                    l_returnClass.ErrorString = "GetFabricPartByID Error: Return Is Empty";
                    l_returnClass.ErrorDetail = "GetFabricPartByID Error: Return Is Empty, Maybe Is Not Matching Data, Please Edit Parameter And Try Later.";

                    return l_returnClass;
                }

                #endregion

                #region To List
                if (l_returnItem != null)
                {
                    if (!l_returnItem.isError() && !l_returnItem.isEmpty())
                    {
                        FabricPartList l_tempFabricPartList = new FabricPartList();

                        FabricPartBaseType[] l_tempFabricPartBaseTypeArray = new FabricPartBaseType[l_returnItem.getItemCount()];
                        for (int i = 0; i < l_returnItem.getItemCount(); i++)
                        {
                            FabricPartBaseType l_tempFabricPartBaseType = new FabricPartBaseType();
                            Item l_getFabricItem = l_returnItem.getItemByIndex(i);
                            #region 3.fabricpart

                            l_tempFabricPartBaseType.Description = l_getFabricItem.getProperty("description", "");

                            #region Content
                            Item l_getPartContentItem = l_getFabricItem.getRelationships(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"));
                            if (l_getPartContentItem != null)
                            {
                                if (!l_getPartContentItem.isError() && !l_getPartContentItem.isEmpty())
                                {
                                    FabricContentBaseType[] l_tempFabricContentBaseTypeArray = new FabricContentBaseType[l_getPartContentItem.getItemCount()];

                                    for (int i_getPartContentIndex = 0; i_getPartContentIndex < l_getPartContentItem.getItemCount(); i_getPartContentIndex++)
                                    {
                                        FabricContentBaseType l_tempFabricContentBaseType = new FabricContentBaseType();
                                        l_tempFabricContentBaseType.Description = l_getPartContentItem.getItemByIndex(i_getPartContentIndex).getProperty("cn_content", "");
                                        l_tempFabricContentBaseType.Ratio = float.Parse(l_getPartContentItem.getItemByIndex(i_getPartContentIndex).getProperty("cn_content_ratio", "0"));

                                        l_tempFabricContentBaseTypeArray[i_getPartContentIndex] = l_tempFabricContentBaseType;
                                    }
                                    l_tempFabricPartBaseType.Content = l_tempFabricContentBaseTypeArray;
                                }
                            }

                            #endregion

                            #region base info
                            l_tempFabricPartBaseType.YarnCount = l_getFabricItem.getProperty("cn_yarn_count", "");
                            l_tempFabricPartBaseType.Construction = l_getFabricItem.getProperty("cn_construction", "");
                            l_tempFabricPartBaseType.DyeMethod = l_getFabricItem.getProperty("cn_dye_method", "");
                            l_tempFabricPartBaseType.Finishing = l_getFabricItem.getProperty("cn_finishing", "");
                            l_tempFabricPartBaseType.Width = l_getFabricItem.getProperty("cn_fabric_width", "");
                            l_tempFabricPartBaseType.Pattern = l_getFabricItem.getProperty("cn_fabric_pattern", "");
                            l_tempFabricPartBaseType.Printing = l_getFabricItem.getProperty("cn_printing", "");
                            l_tempFabricPartBaseType.RepeatVertical = l_getFabricItem.getProperty("cn_rep_v", "");
                            l_tempFabricPartBaseType.RepeatHorizontal = l_getFabricItem.getProperty("cn_rep_h", "");
                            l_tempFabricPartBaseType.TestingGrade = l_getFabricItem.getProperty("cn_fab_test_grading", "");
                            l_tempFabricPartBaseType.Remarks = l_getFabricItem.getProperty("cn_remark", "");
                            #endregion

                            #region Header
                            //header
                            FabricPartHeaderType l_tempFabricPartHeaderType = new FabricPartHeaderType();
                            l_tempFabricPartHeaderType.Code = l_getFabricItem.getProperty("cn_ppo_agpo", "");
                            l_tempFabricPartHeaderType.Cateogry = l_getFabricItem.getProperty("cn_class1", "");
                            l_tempFabricPartHeaderType.WovenFabrication = l_getFabricItem.getProperty("cn_class2", "");
                            l_tempFabricPartHeaderType.KnitFabrication = "";
                            l_tempFabricPartHeaderType.Version = l_getFabricItem.getProperty("maj_rev", "");
                            l_tempFabricPartHeaderType.Status = l_getFabricItem.getProperty("state", "");

                            #region Header Set CustomerInfo
                            CustomerBaseType[] l_tempCustomerBaseTypeArray = new CustomerBaseType[1];
                            CustomerBaseType l_tempCustomerBaseType = new CustomerBaseType();

                            l_tempCustomerBaseType.CustomerCode = "";
                            l_tempCustomerBaseType.CustomerName = "";
                            l_tempCustomerBaseType.CustomerReferenceNum = "";
                            l_tempCustomerBaseType.BrandCode = "";
                            l_tempCustomerBaseType.BrandName = "";

                            //customer
                            string l_partCustomerID = l_getFabricItem.getProperty("cn_cust_code", "");
                            if (!string.IsNullOrEmpty(l_partCustomerID))
                            {
                                Item l_getPartCustomerItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Customer_ItemName"), l_partCustomerID);
                                if (!l_getPartCustomerItem.isError() && !l_getPartCustomerItem.isEmpty())
                                {
                                    l_tempCustomerBaseType.CustomerCode = l_getPartCustomerItem.getProperty("keyed_name", "");
                                    l_tempCustomerBaseType.CustomerName = l_getPartCustomerItem.getProperty("name", "");
                                    l_tempCustomerBaseType.CustomerReferenceNum = l_getPartCustomerItem.getProperty("cn_cust_item_id", "");
                                }
                            }
                            //brand
                            string l_partBrandID = l_getFabricItem.getProperty("cn_brand_code", "");
                            if (!string.IsNullOrEmpty(l_partBrandID))
                            {
                                Item l_getPartBrandItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("CustomerBrand_ItemName"), l_partBrandID);
                                if (!l_getPartBrandItem.isError() && !l_getPartBrandItem.isEmpty())
                                {
                                    l_tempCustomerBaseType.BrandCode = l_getPartBrandItem.getProperty("keyed_name", "");
                                    l_tempCustomerBaseType.BrandName = l_getPartBrandItem.getProperty("name", "");
                                }
                            }

                            l_tempCustomerBaseTypeArray[0] = l_tempCustomerBaseType;
                            //header set customer info
                            l_tempFabricPartHeaderType.CustomerInfo = l_tempCustomerBaseTypeArray;
                            #endregion
                            #endregion
                            l_tempFabricPartBaseType.FabricHeader = l_tempFabricPartHeaderType;

                            #region SupplierBaseType
                            SupplierBaseType[] l_tempSupplierBaseTypeArray = new SupplierBaseType[1];
                            SupplierBaseType l_tempSupplierBaseType = new SupplierBaseType();
                            string l_getPartSupplierID = l_getFabricItem.getProperty("cn_supplier_code", "");
                            if (!string.IsNullOrEmpty(l_getPartSupplierID))
                            {
                                Item l_getSupplierItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Supplier_ItemName"), l_getPartSupplierID);

                                l_tempSupplierBaseType.Code = l_getItem.getProperty("cn_supplier_code");
                                l_tempSupplierBaseType.ItemCode = l_getItem.getProperty("cn_supplier_item_code");
                                l_tempSupplierBaseType.SupplierName = l_getItem.getProperty("cn_class1");
                            }
                            l_tempSupplierBaseTypeArray[0] = l_tempSupplierBaseType;
                            #endregion
                            l_tempFabricPartBaseType.Supplier = l_tempSupplierBaseTypeArray;


                            //color combo
                            l_tempFabricPartBaseType.ColorCombo = null;

                            //fabric shrinkage
                            l_tempFabricPartBaseType.FabricShrinkage = null;

                            //garment shrinkage
                            l_tempFabricPartBaseType.GarmentShrinkage = null;

                            //price
                            PriceBaseType[] l_tempPriceBaseTypeArray = new PriceBaseType[1];
                            PriceBaseType l_tempPriceBaseType = new PriceBaseType();
                            l_tempPriceBaseType.Amount = Convert.ToDecimal(l_getFabricItem.getProperty("cn_remark", "0"));
                            l_tempPriceBaseType.Currency = "";//*
                            l_tempPriceBaseTypeArray[0] = l_tempPriceBaseType;
                            l_tempFabricPartBaseType.Price = l_tempPriceBaseTypeArray;

                            //year season                            
                            l_tempFabricPartBaseType.YearSeason = null;

                            //PartSize
                            Item l_getPartSizeItem = l_getFabricItem.getRelationships(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"));
                            if (!l_getPartSizeItem.isEmpty() && !l_getPartSizeItem.isError())
                            {
                                FabricSizeBaseType[] l_tempFabricSizeBaseTypeArray = new FabricSizeBaseType[l_getPartSizeItem.getItemCount()];
                                for (int i_partSizeIndex = 0; i_partSizeIndex < l_getPartSizeItem.getItemCount(); i_partSizeIndex++)
                                {
                                    FabricSizeBaseType l_tempFabricSizeBaseType = new FabricSizeBaseType();
                                    l_tempFabricSizeBaseType.Dimension = l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size_desc", "");
                                    l_tempFabricSizeBaseType.Measurement = 0; //l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size", "");//*
                                    l_tempFabricSizeBaseTypeArray[i_partSizeIndex] = l_tempFabricSizeBaseType;
                                }
                                l_tempFabricPartBaseType.Size = l_tempFabricSizeBaseTypeArray;
                            }

                            //loyout                                           



                            #endregion

                            l_tempFabricPartBaseTypeArray[i] = l_tempFabricPartBaseType;
                        }
                        l_tempFabricPartList.FabricPart = l_tempFabricPartBaseTypeArray;
                        l_returnClass.FabricPartList = l_tempFabricPartList;
                    }
                }
                #endregion

                #region Logout
                m_Connection.Logout();
                #endregion

                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "GetFabricPartByID Error:" + ex.ToString());

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartByID, "2", "");
                l_returnClass.ErrorString = "GetFabricPartByID Error";
                l_returnClass.ErrorDetail = "GetFabricPartByID Error:" + ex.Message;

                return l_returnClass;

            }
        }

        public FabricPartClass GetFabricPartImage(string pi_fabricPartID, eumImageType pi_getImageType)
        {
            FabricPartClass l_returnClass = new FabricPartClass();
            l_returnClass.SuccessFlag = true;

            if (string.IsNullOrEmpty(pi_fabricPartID))
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartImage, "0", "");
                l_returnClass.ErrorString = "GetFabricPartImage Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "GetFabricPartImage Error: Parameter Is Null , Please Setting FabricPart ID And Try Later.";

                return l_returnClass;
            }
            if (pi_getImageType == null)
            {
                pi_getImageType = eumImageType.UsePrimaryPath;
            }

            try
            {
                //search GarmentStyle 
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                Item l_returnItem = null;

                SelectionFilter l_newSelectionFilter = SelectionFilter.CreateAndFilter(
                    new SelectionFilter[]{
                            SelectionFilter.CreateLeaf("CN_CLASS0","EQ","FAB"), 
                            SelectionFilter.CreateLeaf("ITEM_NUMBER", "EQ", pi_fabricPartID.Trim())
                        });
                Item l_searchItem = ParseSelectionFilter.ParseSelection(l_newSelectionFilter, l_getItem);
                l_returnItem = l_searchItem.apply();

                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartImage, "1", l_getItem.getErrorCode());
                    l_returnClass.ErrorString = "GetFabricPartImage Error:" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "GetFabricPartImage Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }

                //parse garmentStyle image path
                if (l_returnItem.isEmpty())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartImage, "1", "");
                    l_returnClass.ErrorString = "GetFabricPartImage Error:Get Item Is Empty .";
                    l_returnClass.ErrorDetail = "GetFabricPartImage Error:Aras Apply Item Return Is Empty , Please Check Selection Parameter .";

                    return l_returnClass;
                }

                string l_ImageFileItemValue = l_returnItem.getItemByIndex(0).getProperty("cn_thumbnail");
                if (string.IsNullOrEmpty(l_ImageFileItemValue))
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartImage, "1", "");
                    l_returnClass.ErrorString = "getFabricPartImage Error:FabricPart Item Image Is Empty .";
                    l_returnClass.ErrorDetail = "getFabricPartImage Error:FabricPart Image Is Empty , Please Upload Image , And Get Image Later .";

                    return l_returnClass;
                }
                if (l_ImageFileItemValue.IndexOf("vault:///?fileId=") == -1)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartImage, "1", "");
                    l_returnClass.ErrorString = "GetFabricPartImage Error:FabricPart Item Image Is Not Vault .";
                    l_returnClass.ErrorDetail = "GetFabricPartImage Error:FabricPart Image Is Empty , Please Upload Image , And Get Image Later .";

                    return l_returnClass;
                }

                //get file item
                string l_fileItemID = Common.StringHelper.GetRightString(l_ImageFileItemValue, "vault:///?fileId=");
                Item l_fileItem = m_Innovator.getItemById("File", l_fileItemID);

                if (pi_getImageType == eumImageType.UseTempPath)
                {
                    //copy file to temp path
                    string l_tempImagePath = ItemDownFilePath.path + l_fileItem.getID() + "." + Common.StringHelper.GetRightString(l_fileItem.getFileName(), ".");
                    string l_tempImageUrl = Common.ConfigHelper.GetAPPConfigValue("tempFileUrl") + l_fileItem.getID() + "." + Common.StringHelper.GetRightString(l_fileItem.getFileName(), ".");
                    m_Innovator.getConnection().DownloadFile(l_fileItem, l_tempImagePath, true);
                    l_returnClass.GetReturnString = l_tempImageUrl;
                }
                else if (pi_getImageType == eumImageType.UsePrimaryPath)
                {
                    l_returnClass.GetReturnString = m_Innovator.getFileUrl(l_fileItemID, UrlType.SecurityToken);
                }

                //return temp path  

                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "GetFabricPartImage Error:" + ex.ToString());

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetFabricPartImage, "2", "");
                l_returnClass.ErrorString = "GetFabricPartImage Error";
                l_returnClass.ErrorDetail = "GetFabricPartImage Error:" + ex.Message;

                return l_returnClass;
            }
        }

        #region Selection Filter Functional

        public SelectionFilter CreateAndFilter(SelectionFilter[] filters)
        {
            SelectionFilter returnValue = new SelectionFilter();

            returnValue.FilterType = SelectionFilter.FilterTypeAnd;
            returnValue.Filters = filters;

            return returnValue;
        }

        public SelectionFilter CreateOrFilter(SelectionFilter[] filters)
        {
            SelectionFilter returnValue = new SelectionFilter();

            returnValue.FilterType = SelectionFilter.FilterTypeOr;
            returnValue.Filters = filters;

            return returnValue;
        }

        public SelectionFilter CreateLeaf(string attributeName, string searchOperator, string filterValue)
        {
            SelectionFilter returnValue = new SelectionFilter();

            returnValue.FilterType = SelectionFilter.FilterTypeLeaf;
            returnValue.AttributeName = attributeName;
            returnValue.SearchOperator = searchOperator;
            returnValue.FilterValue = filterValue;

            return returnValue;
        }

        #endregion
    }
}
