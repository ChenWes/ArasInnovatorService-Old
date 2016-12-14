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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TrimPartManageService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TrimPartManageService.svc or TrimPartManageService.svc.cs at the Solution Explorer and start debugging.
    public class TrimPartManageService : ITrimPartManageService
    {
        HttpServerConnection m_Connection;
        Innovator m_Innovator;

        public void ArasServiceConfig(string pi_userName, string pi_pwd)
        {
            m_Connection = IomFactory.CreateHttpServerConnection(ConfigHelper.GetAPPConfigValue("InnovatorUrl"), ConfigHelper.GetAPPConfigValue("InnovatorDB"), pi_userName, Innovator.ScalcMD5(pi_pwd));
            m_Innovator = IomFactory.CreateInnovator(m_Connection);
        }

        public TrimPartClass GetTrimPartList(SelectionFilter pi_selectionFilter, int pi_pageIndex, int pi_pageSize)
        {
            TrimPartClass l_returnClass = new TrimPartClass();
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
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(SelectionFilter.CreateAndFilter(new SelectionFilter[] { SelectionFilter.CreateLeaf("CN_CLASS0", "EQ", "TRM"), pi_selectionFilter }), l_getItem);

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
                    l_getItem = ParseSelectionFilter.ParseSelection(SelectionFilter.CreateAndFilter(new SelectionFilter[] { SelectionFilter.CreateLeaf("CN_CLASS0", "EQ", "TRM") }), l_getItem);

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
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetTrimPartList, "2", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "GetTrimPartList Error" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "GetTrimPartList Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }
                if (l_returnItem.isEmpty())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetTrimPartList, "2", "");
                    l_returnClass.ErrorString = "GetTrimPartList Error: Return Is Empty";
                    l_returnClass.ErrorDetail = "GetTrimPartList Error: Return Is Empty, Maybe Is Not Matching Data, Please Edit Parameter And Try Later.";

                    return l_returnClass;
                }
                if (l_returnItem.getItemCount() == 0)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetTrimPartList, "2", "");
                    l_returnClass.ErrorString = "GetTrimPartList Error: Return Is Empty";
                    l_returnClass.ErrorDetail = "GetTrimPartList Error: Return Is Empty, Maybe Is Not Matching Data, Please Edit Parameter And Try Later.";

                    return l_returnClass;
                }

                #endregion

                #region To List
                if (l_returnItem != null)
                {
                    if (!l_returnItem.isError() && !l_returnItem.isEmpty())
                    {
                        TrimPartList l_tempTrimPartList = new TrimPartList();

                        TrimPartBaseType[] l_tempTrimPartBaseTypeArray = new TrimPartBaseType[l_returnItem.getItemCount()];
                        for (int i = 0; i < l_returnItem.getItemCount(); i++)
                        {
                            Item l_getTrimItem = l_returnItem.getItemByIndex(i);
                            TrimPartBaseType l_tempTrimPartBaseType = new TrimPartBaseType();

                            #region 4.trimpart

                            l_tempTrimPartBaseType.Description = l_getTrimItem.getProperty("description", "");

                            #region base info
                            l_tempTrimPartBaseType.Remarks = l_getTrimItem.getProperty("cn_remark", "");
                            #endregion

                            #region Header
                            //header
                            TrimPartHeaderType l_tempTrimPartHeaderType = new TrimPartHeaderType();
                            l_tempTrimPartHeaderType.Code = l_getTrimItem.getProperty("item_number", "");

                            TrimCategoryBaseType l_tempTrimCategoryBaseType = new TrimCategoryBaseType();
                            l_tempTrimCategoryBaseType.Code = l_getTrimItem.getProperty("cn_class1", "");
                            l_tempTrimCategoryBaseType.Description = l_getTrimItem.getProperty("description", "");
                            l_tempTrimPartHeaderType.Cateogry = l_tempTrimCategoryBaseType;

                            l_tempTrimPartHeaderType.SubCategory = l_getTrimItem.getProperty("cn_class2", "");
                            l_tempTrimPartHeaderType.Version = l_getTrimItem.getProperty("maj_rev", "");
                            l_tempTrimPartHeaderType.Status = l_getTrimItem.getProperty("state", "");

                            #region Header Set CustomerInfo
                            CustomerBaseType[] l_tempCustomerBaseTypeArray = new CustomerBaseType[1];
                            CustomerBaseType l_tempCustomerBaseType = new CustomerBaseType();

                            l_tempCustomerBaseType.CustomerCode = "";
                            l_tempCustomerBaseType.CustomerName = "";
                            l_tempCustomerBaseType.CustomerReferenceNum = "";
                            l_tempCustomerBaseType.BrandCode = "";
                            l_tempCustomerBaseType.BrandName = "";

                            //customer
                            string l_partCustomerID = l_getTrimItem.getProperty("cn_cust_code", "");
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
                            string l_partBrandID = l_getTrimItem.getProperty("cn_brand_code", "");
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
                            l_tempTrimPartHeaderType.CustomerInfo = l_tempCustomerBaseTypeArray;
                            #endregion

                            #endregion
                            l_tempTrimPartBaseType.TrimHeader = l_tempTrimPartHeaderType;

                            #region SupplierBaseType
                            SupplierBaseType[] l_tempSupplierBaseTypeArray = new SupplierBaseType[1];
                            SupplierBaseType l_tempSupplierBaseType = new SupplierBaseType();
                            string l_getPartSupplierID = l_getTrimItem.getProperty("cn_supplier_code", "");
                            if (!string.IsNullOrEmpty(l_getPartSupplierID))
                            {
                                Item l_getSupplierItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Supplier_ItemName"), l_getPartSupplierID);

                                l_tempSupplierBaseType.Code = l_getItem.getProperty("cn_supplier_code", "");
                                l_tempSupplierBaseType.ItemCode = l_getItem.getProperty("cn_supplier_item_code", "");
                                l_tempSupplierBaseType.SupplierName = l_getItem.getProperty("cn_class1", "");
                            }
                            l_tempSupplierBaseTypeArray[0] = l_tempSupplierBaseType;
                            #endregion
                            l_tempTrimPartBaseType.Supplier = l_tempSupplierBaseTypeArray;


                            //AGPO
                            l_tempTrimPartBaseType.AGPONum = new string[] { l_getTrimItem.getProperty("cn_ppo_agpo", "") };

                            //color
                            l_tempTrimPartBaseType.Color = null;

                            l_tempTrimPartBaseType.Material = "";


                            //year season                            
                            l_tempTrimPartBaseType.YearSeason = null;

                            //PartSize
                            Item l_getPartSizeItem = l_getTrimItem.getRelationships(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"));
                            if (!l_getPartSizeItem.isEmpty() && !l_getPartSizeItem.isError())
                            {
                                SizeBaseType[] l_tempSizeBaseTypeArray = new SizeBaseType[l_getPartSizeItem.getItemCount()];
                                for (int i_partSizeIndex = 0; i_partSizeIndex < l_getPartSizeItem.getItemCount(); i_partSizeIndex++)
                                {
                                    SizeBaseType l_tempSizeBaseType = new SizeBaseType();
                                    l_tempSizeBaseType.Dimension = l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size_desc", "");
                                    l_tempSizeBaseType.Measurement = 0; //l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size", "");//*
                                    l_tempSizeBaseTypeArray[i_partSizeIndex] = l_tempSizeBaseType;
                                }
                                l_tempTrimPartBaseType.Size = l_tempSizeBaseTypeArray;
                            }


                            #endregion

                            l_tempTrimPartBaseTypeArray[i] = l_tempTrimPartBaseType;
                        }
                        l_tempTrimPartList.TrimPart = l_tempTrimPartBaseTypeArray;
                        l_returnClass.TrimPartList = l_tempTrimPartList;
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

        public TrimPartClass GetTrimPartByID(string pi_trimPartID)
        {
            TrimPartClass l_returnClass = new TrimPartClass();
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
                if (!string.IsNullOrEmpty(pi_trimPartID))
                {
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(SelectionFilter.CreateAndFilter(new SelectionFilter[] { SelectionFilter.CreateLeaf("CN_CLASS0", "EQ", "TRM"), SelectionFilter.CreateLeaf("ITEM_NUMBER","EQ",pi_trimPartID) }), l_getItem);

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
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetTrimPartByID, "2", "");
                    l_returnClass.ErrorString = "GetTrimPartByID Error:pi_fabricPartID Is Null Or Empty.";
                    l_returnClass.ErrorDetail = "GetTrimPartByID Error:Please Setting pi_trimPartID And Try Later.";

                    return l_returnClass;
                }
                #endregion

                #region Check Return Item
                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetTrimPartByID, "2", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "GetTrimPartByID Error" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "GetTrimPartByID Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }
                if (l_returnItem.isEmpty())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetTrimPartByID, "2", "");
                    l_returnClass.ErrorString = "GetTrimPartList Error: Return Is Empty";
                    l_returnClass.ErrorDetail = "GetTrimPartList Error: Return Is Empty, Maybe Is Not Matching Data, Please Edit Parameter And Try Later.";

                    return l_returnClass;
                }
                if (l_returnItem.getItemCount() == 0)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetTrimPartByID, "2", "");
                    l_returnClass.ErrorString = "GetTrimPartByID Error: Return Is Empty";
                    l_returnClass.ErrorDetail = "GetTrimPartByID Error: Return Is Empty, Maybe Is Not Matching Data, Please Edit Parameter And Try Later.";

                    return l_returnClass;
                }

                #endregion

                #region To List
                if (l_returnItem != null)
                {
                    if (!l_returnItem.isError() && !l_returnItem.isEmpty())
                    {
                        TrimPartList l_tempTrimPartList = new TrimPartList();

                        TrimPartBaseType[] l_tempTrimPartBaseTypeArray = new TrimPartBaseType[l_returnItem.getItemCount()];
                        for (int i = 0; i < l_returnItem.getItemCount(); i++)
                        {
                            Item l_getTrimItem = l_returnItem.getItemByIndex(i);
                            TrimPartBaseType l_tempTrimPartBaseType = new TrimPartBaseType();

                            #region 4.trimpart

                            l_tempTrimPartBaseType.Description = l_getTrimItem.getProperty("description", "");

                            #region base info
                            l_tempTrimPartBaseType.Remarks = l_getTrimItem.getProperty("cn_remark", "");
                            #endregion

                            #region Header
                            //header
                            TrimPartHeaderType l_tempTrimPartHeaderType = new TrimPartHeaderType();
                            l_tempTrimPartHeaderType.Code = l_getTrimItem.getProperty("item_number", "");

                            TrimCategoryBaseType l_tempTrimCategoryBaseType = new TrimCategoryBaseType();
                            l_tempTrimCategoryBaseType.Code = l_getTrimItem.getProperty("cn_class1", "");
                            l_tempTrimCategoryBaseType.Description = l_getTrimItem.getProperty("description", "");
                            l_tempTrimPartHeaderType.Cateogry = l_tempTrimCategoryBaseType;

                            l_tempTrimPartHeaderType.SubCategory = l_getTrimItem.getProperty("cn_class2", "");
                            l_tempTrimPartHeaderType.Version = l_getTrimItem.getProperty("maj_rev", "");
                            l_tempTrimPartHeaderType.Status = l_getTrimItem.getProperty("state", "");

                            #region Header Set CustomerInfo
                            CustomerBaseType[] l_tempCustomerBaseTypeArray = new CustomerBaseType[1];
                            CustomerBaseType l_tempCustomerBaseType = new CustomerBaseType();

                            l_tempCustomerBaseType.CustomerCode = "";
                            l_tempCustomerBaseType.CustomerName = "";
                            l_tempCustomerBaseType.CustomerReferenceNum = "";
                            l_tempCustomerBaseType.BrandCode = "";
                            l_tempCustomerBaseType.BrandName = "";

                            //customer
                            string l_partCustomerID = l_getTrimItem.getProperty("cn_cust_code", "");
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
                            string l_partBrandID = l_getTrimItem.getProperty("cn_brand_code", "");
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
                            l_tempTrimPartHeaderType.CustomerInfo = l_tempCustomerBaseTypeArray;
                            #endregion

                            #endregion
                            l_tempTrimPartBaseType.TrimHeader = l_tempTrimPartHeaderType;

                            #region SupplierBaseType
                            SupplierBaseType[] l_tempSupplierBaseTypeArray = new SupplierBaseType[1];
                            SupplierBaseType l_tempSupplierBaseType = new SupplierBaseType();
                            string l_getPartSupplierID = l_getTrimItem.getProperty("cn_supplier_code", "");
                            if (!string.IsNullOrEmpty(l_getPartSupplierID))
                            {
                                Item l_getSupplierItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Supplier_ItemName"), l_getPartSupplierID);

                                l_tempSupplierBaseType.Code = l_getItem.getProperty("cn_supplier_code", "");
                                l_tempSupplierBaseType.ItemCode = l_getItem.getProperty("cn_supplier_item_code", "");
                                l_tempSupplierBaseType.SupplierName = l_getItem.getProperty("cn_class1", "");
                            }
                            l_tempSupplierBaseTypeArray[0] = l_tempSupplierBaseType;
                            #endregion
                            l_tempTrimPartBaseType.Supplier = l_tempSupplierBaseTypeArray;


                            //AGPO
                            l_tempTrimPartBaseType.AGPONum = new string[] { l_getTrimItem.getProperty("cn_ppo_agpo", "") };

                            //color
                            l_tempTrimPartBaseType.Color = null;

                            l_tempTrimPartBaseType.Material = "";


                            //year season                            
                            l_tempTrimPartBaseType.YearSeason = null;

                            //PartSize
                            Item l_getPartSizeItem = l_getTrimItem.getRelationships(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"));
                            if (!l_getPartSizeItem.isEmpty() && !l_getPartSizeItem.isError())
                            {
                                SizeBaseType[] l_tempSizeBaseTypeArray = new SizeBaseType[l_getPartSizeItem.getItemCount()];
                                for (int i_partSizeIndex = 0; i_partSizeIndex < l_getPartSizeItem.getItemCount(); i_partSizeIndex++)
                                {
                                    SizeBaseType l_tempSizeBaseType = new SizeBaseType();
                                    l_tempSizeBaseType.Dimension = l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size_desc", "");
                                    l_tempSizeBaseType.Measurement = 0; //l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size", "");//*
                                    l_tempSizeBaseTypeArray[i_partSizeIndex] = l_tempSizeBaseType;
                                }
                                l_tempTrimPartBaseType.Size = l_tempSizeBaseTypeArray;
                            }


                            #endregion

                            l_tempTrimPartBaseTypeArray[i] = l_tempTrimPartBaseType;
                        }
                        l_tempTrimPartList.TrimPart = l_tempTrimPartBaseTypeArray;
                        l_returnClass.TrimPartList = l_tempTrimPartList;
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
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "GetTrimPartByID Error:" + ex.ToString());

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetTrimPartByID, "2", "");
                l_returnClass.ErrorString = "GetTrimPartByID Error";
                l_returnClass.ErrorDetail = "GetTrimPartByID Error:" + ex.Message;

                return l_returnClass;

            }
        }

        public TrimPartClass GetTrimPartImage(string pi_trimPartID,eumImageType pi_getImageType)
        {
            TrimPartClass l_returnClass = new TrimPartClass();
            l_returnClass.SuccessFlag = true;

            if (string.IsNullOrEmpty(pi_trimPartID))
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetTrimPartImage, "0", "");
                l_returnClass.ErrorString = "GetTrimPartImage Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "GetTrimPartImage Error: Parameter Is Null , Please Setting TrimPart ID And Try Later.";

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
                        SelectionFilter.CreateLeaf("CN_CLASS0","EQ","TRM"), 
                        SelectionFilter.CreateLeaf("ITEM_NUMBER", "EQ", pi_trimPartID.Trim())
                    });

                Item l_searchItem = ParseSelectionFilter.ParseSelection(l_newSelectionFilter, l_getItem);
                l_returnItem = l_searchItem.apply();

                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetTrimPartImage, "1", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "GetTrimPartImage Error:" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "GetTrimPartImage Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }

                //parse garmentStyle image path
                if (l_returnItem.isEmpty())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetTrimPartImage, "1", "");
                    l_returnClass.ErrorString = "GetTrimPartImage Error:Get Item Is Empty .";
                    l_returnClass.ErrorDetail = "GetTrimPartImage Error:Aras Apply Item Return Is Empty , Please Check Selection Parameter .";

                    return l_returnClass;
                }

                string l_ImageFileItemValue = l_returnItem.getItemByIndex(0).getProperty("cn_thumbnail");
                if (string.IsNullOrEmpty(l_ImageFileItemValue))
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartImage, "1", "");
                    l_returnClass.ErrorString = "getTrimPartImage Error:TrimPart Item Image Is Empty .";
                    l_returnClass.ErrorDetail = "getTrimPartImage Error:TrimPart Image Is Empty , Please Upload Image , And Get Image Later .";

                    return l_returnClass;
                }
                if (l_ImageFileItemValue.IndexOf("vault:///?fileId=") == -1)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartImage, "1", "");
                    l_returnClass.ErrorString = "getTrimPartImage Error:Garment Style Item Image Is Not Vault .";
                    l_returnClass.ErrorDetail = "getTrimPartImage Error:Garment Record Image Is Empty , Please Upload Image , And Get Image Later .";

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
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "GetTrimPartImage Error:" + ex.ToString());

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetTrimPartImage, "2", "");
                l_returnClass.ErrorString = "GetTrimPartImage Error";
                l_returnClass.ErrorDetail = "GetTrimPartImage Error:" + ex.Message;

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
