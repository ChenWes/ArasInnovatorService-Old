using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.ServiceModel.Activation;
using Aras;
using Aras.IOM;
using ArasInnovatorService.ArasModel;
using ArasInnovatorService.Common;

namespace ArasInnovatorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GarmentStyleManagementWS" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GarmentStyleManagementWS.svc or GarmentStyleManagementWS.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GarmentStyleManagementWS : IGarmentStyleManagementWS
    {
        HttpServerConnection m_Connection;
        Innovator m_Innovator;

        public void ArasServiceConfig(string pi_userName, string pi_pwd)
        {
            m_Connection = IomFactory.CreateHttpServerConnection(ConfigHelper.GetAPPConfigValue("InnovatorUrl"), ConfigHelper.GetAPPConfigValue("InnovatorDB"), pi_userName, Innovator.ScalcMD5(pi_pwd));
            m_Innovator = IomFactory.CreateInnovator(m_Connection);
        }

        /// <summary>
        /// Get GarmentStyle List By Parameter
        /// 2015-08-18 add by WesChen
        /// </summary>
        /// <param name="pi_GarmentStyleClass"></param>
        /// <returns></returns>
        public GarmentStyleClass getGarmentStyleList(GarmentStyleClass pi_GarmentStyleClass)
        {
            GarmentStyleClass l_returnClass = new GarmentStyleClass();
            l_returnClass.SuccessFlag = true;
            if (pi_GarmentStyleClass==null)
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleList, "0", "");
                l_returnClass.ErrorString = "getGarmentStyleList Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "getGarmentStyleList Error: Parameter Is Null , Please Setting Parameter And Try Later." ;

                return l_returnClass;
            }

            try
            {
                
                //connection and new item
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyle_ItemName"), "get");
                
                l_getItem.setAttribute("pagesize", pi_GarmentStyleClass.DisplayPageSize.ToString());
                l_getItem.setAttribute("page", pi_GarmentStyleClass.DisplayPageIndex.ToString());

                Item l_returnItem=null;
                //add parameter to search item
                if (pi_GarmentStyleClass.SelectionFilter != null)
                {
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(pi_GarmentStyleClass.SelectionFilter, l_getItem);

                    //get GarmentStyle Colorway List
                    Item l_getGarmentStyleColorway = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleGarmentColorway_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleColorway);
                    //get GarmentStyle SizeRange List
                    Item l_getGarmentStyleSizeRange = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRange_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleSizeRange);

                    //----------------------------------------------------------------------------------------------
                    //get GarmentStyle BOM List
                    Item l_getGarmentStyleBOM = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleBOM_ItemName"), "get");
                    Item l_garmentStyleBOMlink = l_getGarmentStyleBOM.createRelatedItem(ConfigHelper.GetAPPConfigValue("GarmentBOM_ItemName"), "get");

                    Item l_getGarmentBOMPart = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentBOMPart_ItemName"), "get");
                    Item l_bomPartLink = l_getGarmentBOMPart.createRelatedItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                    Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    Item l_colorComboColorCodeLink = l_getPartColorCombo.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCombo_ItemName"), "get");

                    Item l_getColorComboColorCode = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName"), "get");
                    l_getColorComboColorCode.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCode_ItemName"), "get");

                    l_colorComboColorCodeLink.addRelationship(l_getColorComboColorCode);

                    l_bomPartLink.addRelationship(l_getPartColorCombo);

                    l_garmentStyleBOMlink.addRelationship(l_getGarmentBOMPart);
                    l_searchItem.addRelationship(l_getGarmentStyleBOM);
                    //----------------------------------------------------------------------------------------------

                    //get GarmentStyle Sketch List
                    Item l_getGarmentStyleSketch = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSketch_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleSketch);
                    //get GarmentStyle CAD List
                    Item l_getGarmentStyleCAD = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleCAD_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleCAD);
                    //get GarmentStyle Document List
                    Item l_getGarmentStyleDocument = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleDocument_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleDocument);
                    //get GarmentStyle YearSeason List
                    Item l_getGarmentStyleYearSeason = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleYearSeason_ItemName"), "get");
                    l_getGarmentStyleYearSeason.createRelatedItem(ConfigHelper.GetAPPConfigValue("YearSeason_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleYearSeason);

                    l_returnItem = l_searchItem.apply();
                }
                else
                {
                    //get GarmentStyle Colorway List
                    Item l_getGarmentStyleColorway = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleGarmentColorway_ItemName"), "get");
                    l_getItem.addRelationship(l_getGarmentStyleColorway);
                    //get GarmentStyle SizeRange List
                    Item l_getGarmentStyleSizeRange = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRange_ItemName"), "get");
                    l_getItem.addRelationship(l_getGarmentStyleSizeRange);
                    //----------------------------------------------------------------------------------------------
                    //get GarmentStyle BOM List
                    Item l_getGarmentStyleBOM = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleBOM_ItemName"), "get");
                    Item l_garmentStyleBOMlink = l_getGarmentStyleBOM.createRelatedItem(ConfigHelper.GetAPPConfigValue("GarmentBOM_ItemName"), "get");

                    Item l_getGarmentBOMPart = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentBOMPart_ItemName"), "get");
                    Item l_bomPartLink = l_getGarmentBOMPart.createRelatedItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                    Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    Item l_colorComboColorCodeLink = l_getPartColorCombo.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCombo_ItemName"), "get");

                    Item l_getColorComboColorCode = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName"), "get");
                    l_getColorComboColorCode.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCode_ItemName"), "get");

                    l_colorComboColorCodeLink.addRelationship(l_getColorComboColorCode);
                    //l_getPartColorCombo.addRelationship(l_getColorComboColorCode);

                    l_bomPartLink.addRelationship(l_getPartColorCombo);
                    //l_getGarmentBOMPart.addRelationship(l_getPartColorCombo);

                    l_garmentStyleBOMlink.addRelationship(l_getGarmentBOMPart);
                    //l_getGarmentStyleBOM.addRelationship(l_getGarmentBOMPart);
                    l_getItem.addRelationship(l_getGarmentStyleBOM);
                    //----------------------------------------------------------------------------------------------

                    //get GarmentStyle Sketch List
                    Item l_getGarmentStyleSketch = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSketch_ItemName"), "get");
                    l_getItem.addRelationship(l_getGarmentStyleSketch);
                    //get GarmentStyle CAD List
                    Item l_getGarmentStyleCAD = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleCAD_ItemName"), "get");
                    l_getItem.addRelationship(l_getGarmentStyleCAD);
                    //get GarmentStyle Document List
                    Item l_getGarmentStyleDocument = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleDocument_ItemName"), "get");
                    l_getItem.addRelationship(l_getGarmentStyleDocument);
                    //get GarmentStyle YearSeason List
                    Item l_getGarmentStyleYearSeason = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleYearSeason_ItemName"), "get");
                    l_getGarmentStyleYearSeason.createRelatedItem(ConfigHelper.GetAPPConfigValue("YearSeason_ItemName"), "get");
                    l_getItem.addRelationship(l_getGarmentStyleYearSeason);

                    l_returnItem = l_getItem.apply();
                }                

                if(l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleList, "1", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "getGarmentStyleList Error:" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "getGarmentStyleList Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }

                //item convert to class
                ItemConverHelper<Garment> l_itemConverHelper = new ItemConverHelper<Garment>();
                List<Garment> l_getList = l_itemConverHelper.ItemConver(l_returnItem);

                //get link
                ClassGetLink<Garment> l_GetLink = new ClassGetLink<Garment>();
                l_returnClass.GarmentStyleList = l_GetLink.GetLinkPropery(l_getList, m_Innovator);
                                

                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getGarmentStyleList Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleList, "2", "");
                l_returnClass.ErrorString = "getGarmentStyleList Error";
                l_returnClass.ErrorDetail = "getGarmentStyleList Error:" + ex.Message;

                return l_returnClass;
            }
        }

        /// <summary>
        /// Get GarmentStyle Item By Parmater
        /// 2015-08-18 add by WesChen
        /// </summary>
        /// <param name="pi_GarmentStyleClass"></param>
        /// <returns></returns>
        public GarmentStyleClass getGarmentStyleById(GarmentStyleClass pi_GarmentStyleClass)
        {
            GarmentStyleClass l_returnClass = new GarmentStyleClass();
            l_returnClass.SuccessFlag = true;
            if (pi_GarmentStyleClass == null)
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleById, "0", "");
                l_returnClass.ErrorString = "getGarmentStyleById Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "getGarmentStyleById Error: Parameter Is Null , Please Setting Parameter And Try Later.";

                return l_returnClass;
            }
            try
            {

                //connection and new item
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyle_ItemName"), "get");

                //l_getItem.setAttribute("pagesize", pi_GarmentStyleClass.DisplayPageSize.ToString());
                //l_getItem.setAttribute("page", pi_GarmentStyleClass.DisplayPageIndex.ToString());

                Item l_returnItem = null;
                //add parameter to search item
                if (!string.IsNullOrEmpty( pi_GarmentStyleClass.OperationID))
                {
                    pi_GarmentStyleClass.SelectionFilter = SelectionFilter.CreateLeaf("ITEM_NUMBER", "EQ", pi_GarmentStyleClass.OperationID.Trim());
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(pi_GarmentStyleClass.SelectionFilter, l_getItem);

                    //get GarmentStyle Colorway List
                    Item l_getGarmentStyleColorway = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleGarmentColorway_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleColorway);
                    //get GarmentStyle SizeRange List
                    Item l_getGarmentStyleSizeRange = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRange_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleSizeRange);
                    //----------------------------------------------------------------------------------------------
                    //get GarmentStyle BOM List
                    Item l_getGarmentStyleBOM = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleBOM_ItemName"), "get");
                    Item l_garmentStyleBOMlink = l_getGarmentStyleBOM.createRelatedItem(ConfigHelper.GetAPPConfigValue("GarmentBOM_ItemName"), "get");

                    Item l_getGarmentBOMPart = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentBOMPart_ItemName"), "get");
                    Item l_bomPartLink = l_getGarmentBOMPart.createRelatedItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                    Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    Item l_colorComboColorCodeLink = l_getPartColorCombo.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCombo_ItemName"), "get");

                    Item l_getColorComboColorCode = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName"), "get");
                    l_getColorComboColorCode.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCode_ItemName"), "get");

                    l_colorComboColorCodeLink.addRelationship(l_getColorComboColorCode);

                    l_bomPartLink.addRelationship(l_getPartColorCombo);

                    l_garmentStyleBOMlink.addRelationship(l_getGarmentBOMPart);
                    l_searchItem.addRelationship(l_getGarmentStyleBOM);
                    //----------------------------------------------------------------------------------------------
                    //get GarmentStyle Sketch List
                    Item l_getGarmentStyleSketch = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSketch_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleSketch);
                    //get GarmentStyle CAD List
                    Item l_getGarmentStyleCAD = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleCAD_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleCAD);
                    //get GarmentStyle Document List
                    Item l_getGarmentStyleDocument = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleDocument_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleDocument);
                    //get GarmentStyle YearSeason List
                    Item l_getGarmentStyleYearSeason = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleYearSeason_ItemName"), "get");
                    l_getGarmentStyleYearSeason.createRelatedItem(ConfigHelper.GetAPPConfigValue("YearSeason_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleYearSeason);

                    l_returnItem = l_searchItem.apply();
                }
                else
                {
                    //l_returnItem = l_getItem.apply();
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleById, "1", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "getGarmentStyleById Error:Operation ID Is Null";
                    l_returnClass.ErrorDetail = "getGarmentStyleById Error:Please Setting Operation ID And Try Later.";

                    return l_returnClass;
                }

                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleById, "1", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "getGarmentStyleById Error:" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "getGarmentStyleById Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }

                //item convert to class
                ItemConverHelper<Garment> l_itemConverHelper = new ItemConverHelper<Garment>();
                List<Garment> l_getList = l_itemConverHelper.ItemConver(l_returnItem);   
                l_returnClass.GarmentStyleList.Add(l_getList[0]);

                //get link
                ClassGetLink<Garment> l_GetLink = new ClassGetLink<Garment>();
                l_returnClass.GarmentStyleList = l_GetLink.GetLinkPropery(l_returnClass.GarmentStyleList, m_Innovator);

                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getGarmentStyleById Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleById, "2", "");
                l_returnClass.ErrorString = "getGarmentStyleById Error";
                l_returnClass.ErrorDetail = "getGarmentStyleById Error:" + ex.Message;

                return l_returnClass;
            }
        } 

        /// <summary>
        /// Add GarmentStyle 
        /// 2015-08-20 update by WesChen Test Successed
        /// </summary>
        /// <param name="pi_object"></param>
        /// <returns></returns>
        public GarmentStyleClass createGarmentStyle(GarmentStyleClass pi_GarmentStyleClass)
        {
            GarmentStyleClass l_returnClass = new GarmentStyleClass();
            l_returnClass.SuccessFlag = true;

            if (pi_GarmentStyleClass == null || pi_GarmentStyleClass.GarmentStyleList == null)
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.createGarmentStyle, "0", "");
                l_returnClass.ErrorString = "createGarmentStyle Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "createGarmentStyle Error: Parameter Is Null , Please Setting Parameter And Try Later.";

                return l_returnClass;
            }

            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));

                //set Link
                ClassSetLink<Garment> l_setLink = new ClassSetLink<Garment>();
                pi_GarmentStyleClass.GarmentStyleList = l_setLink.SetLinkPropery(pi_GarmentStyleClass.GarmentStyleList, m_Innovator);

                ItemConverHelper<Garment> l_itemConverHelper = new ItemConverHelper<Garment>();
                foreach (Garment l_garmentStyle in pi_GarmentStyleClass.GarmentStyleList)
                {                    
                    Item l_addItem = l_itemConverHelper.ItemConver(l_garmentStyle, m_Innovator, ConfigHelper.GetAPPConfigValue("GarmentStyle_ItemName"), "add");
                    if(!string.IsNullOrEmpty(l_garmentStyle.ID))
                    {
                        l_addItem.setAction("get");
                    }

                    Item l_returnItem = l_addItem.apply();

                    if (l_returnItem.isError())
                    {
                        l_returnClass.SuccessFlag = false;
                        l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.createGarmentStyle, "1", l_returnItem.getErrorCode());
                        l_returnClass.ErrorString = "createGarmentStyle Error:" + l_returnItem.getErrorString();
                        l_returnClass.ErrorDetail = "createGarmentStyle Error:" + l_returnItem.getErrorDetail();

                        return l_returnClass;
                    }
                }                                

                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "createGarmentStyle Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.createGarmentStyle, "2", "");
                l_returnClass.ErrorString = "createGarmentStyle Error";
                l_returnClass.ErrorDetail = "createGarmentStyle Error:" + ex.Message;

                return l_returnClass;
            }

        }

        /// <summary>
        /// update GarmentStyle
        /// 2015-08-20 add by WesChen Test Successed
        /// </summary>
        /// <param name="pi_GarmentStyleClass"></param>
        /// <returns></returns>
        public GarmentStyleClass updateGarmentStyle(GarmentStyleClass pi_GarmentStyleClass)
        {
            GarmentStyleClass l_returnClass = new GarmentStyleClass();
            l_returnClass.SuccessFlag = true;
            if (pi_GarmentStyleClass == null || pi_GarmentStyleClass.GarmentStyleList == null || pi_GarmentStyleClass.KeyColumnsList == null)
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.updateGarmentStyle, "0", "");
                l_returnClass.ErrorString = "updateGarmentStyle Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "updateGarmentStyle Error: Parameter Is Null , Please Setting Parameter And Try Later.";

                return l_returnClass;
            }

            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                ItemConverHelper<Garment> l_itemConverHelper = new ItemConverHelper<Garment>();

                //set Link
                ClassSetLink<Garment> l_setLink = new  ClassSetLink<Garment>();
                pi_GarmentStyleClass.GarmentStyleList = l_setLink.SetLinkPropery(pi_GarmentStyleClass.GarmentStyleList, m_Innovator);

                foreach (Garment l_garmentStyle in pi_GarmentStyleClass.GarmentStyleList)
                {
                    Item l_getItem = null;

                    if (!string.IsNullOrEmpty(l_garmentStyle.ID))
                    {
                        l_getItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("GarmentStyle_ItemName"), l_garmentStyle.ID.ToString());
                    }
                    else
                    {
                        //get item
                        Item l_searchItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyle_ItemName"), "get");

                        //add selection
                        List<SelectionFilter> l_searchKeyColumns = new List<SelectionFilter>();
                        ClassPro<Garment> l_getClassProValue = new ClassPro<Garment>();

                        //get in key columns value to search
                        for (int i = 0; i < pi_GarmentStyleClass.KeyColumnsList.Length; i++)
                        {
                            l_searchKeyColumns.Add(SelectionFilter.CreateLeaf(pi_GarmentStyleClass.KeyColumnsList[i].ToString().ToLower(), ConditionHelper.Condition_Mapping("EQ"), ConditionHelper.Condition_Addition(ConditionHelper.Condition_Mapping("EQ"), l_getClassProValue.getPropertiesValue(l_garmentStyle, pi_GarmentStyleClass.KeyColumnsList[i].ToString()))));
                        }

                        l_searchItem = ParseSelectionFilter.ParseSelection(SelectionFilter.CreateAndFilter(l_searchKeyColumns.ToArray()), l_searchItem);
                        l_getItem = l_searchItem.apply();
                    }

                    
                    //get error
                    if (l_getItem.isError())
                    {
                        l_returnClass.SuccessFlag = false;
                        l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.updateGarmentStyle, "1", l_getItem.getErrorCode());
                        l_returnClass.ErrorString = "updateGarmentStyle Error:" + l_getItem.getErrorString();
                        l_returnClass.ErrorDetail = "updateGarmentStyle Error:" + l_getItem.getErrorDetail();

                        return l_returnClass;
                    }

                    if(l_getItem.getItemCount()==0)
                    {
                        l_returnClass.SuccessFlag = false;
                        l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.updateGarmentStyle, "1", "");
                        l_returnClass.ErrorString = "updateGarmentStyle Error:Not Item To Operation";
                        l_returnClass.ErrorDetail = "updateGarmentStyle Error:Not Item To Operation";

                        return l_returnClass;
                    }
                    if (l_getItem.getItemCount() > 1)
                    {
                        for (int i = 0; i < l_getItem.getItemCount(); i++)
                        {
                            //for item
                            Item l_editDetailItem = l_getItem.getItemByIndex(i);
                            //set action
                            l_editDetailItem.setAction("edit");

                            //add valut to item
                            l_editDetailItem = l_itemConverHelper.ItemConver(l_garmentStyle, l_editDetailItem, m_Innovator, "edit");
                            Item l_returnItem = l_editDetailItem.apply();
                            ///get error
                            if (l_editDetailItem.isError())
                            {
                                l_returnClass.SuccessFlag = false;
                                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.updateGarmentStyle, "1", l_editDetailItem.getErrorCode());
                                l_returnClass.ErrorString = "updateGarmentStyle Error:" + l_editDetailItem.getErrorString();
                                l_returnClass.ErrorDetail = "updateGarmentStyle Error:" + l_editDetailItem.getErrorDetail();

                                return l_returnClass;
                            }
                        }
                    }
                    else
                    {
                        //edit action
                        l_getItem.setAction("edit");

                        //add value to item
                        Item l_editItem = l_itemConverHelper.ItemConver(l_garmentStyle, l_getItem, m_Innovator, "edit");
                        Item l_returnItem = l_editItem.apply();
                        //get error
                        if (l_returnItem.isError())
                        {
                            l_returnClass.SuccessFlag = false;
                            l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.updateGarmentStyle, "1", l_returnItem.getErrorCode());
                            l_returnClass.ErrorString = "updateGarmentStyle Error:" + l_returnItem.getErrorString();
                            l_returnClass.ErrorDetail = "updateGarmentStyle Error:" + l_returnItem.getErrorDetail();

                            return l_returnClass;
                        }
                    }
                }
                

                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "updateGarmentStyle Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.updateGarmentStyle, "2", "");
                l_returnClass.ErrorString = "updateGarmentStyle Error";
                l_returnClass.ErrorDetail = "updateGarmentStyle Error:" + ex.Message;

                return l_returnClass;
            }
        }

        /// <summary>
        /// Remove GarmentStyle 
        /// 2015-08-20 add by WesChen Test Successed
        /// </summary>
        /// <param name="pi_GarmentStyleClass"></param>
        /// <returns></returns>
        public GarmentStyleClass removeGarmentStyleById(GarmentStyleClass pi_GarmentStyleClass)
        {
            GarmentStyleClass l_returnClass = new GarmentStyleClass();
            l_returnClass.SuccessFlag = true;

            if (pi_GarmentStyleClass == null)
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.removeGarmentStyleById, "0", "");
                l_returnClass.ErrorString = "removeGarmentStyleById Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "removeGarmentStyleById Error: Parameter Is Null , Please Setting Parameter And Try Later.";

                return l_returnClass;
            }

            try
            {

                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyle_ItemName"), "get");
                Item l_deleteItem=null;

                if (!string.IsNullOrEmpty(pi_GarmentStyleClass.OperationID))
                {
                    pi_GarmentStyleClass.SelectionFilter = SelectionFilter.CreateLeaf("ITEM_NUMBER", "EQ", pi_GarmentStyleClass.OperationID.Trim());
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(pi_GarmentStyleClass.SelectionFilter, l_getItem);
                    l_deleteItem = l_searchItem.apply();
                }
                else
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleById, "1", "");
                    l_returnClass.ErrorString = "getGarmentStyleById Error:Operation ID Is Null";
                    l_returnClass.ErrorDetail = "getGarmentStyleById Error:Please Setting Operation ID And Try Later.";

                    return l_returnClass;
                }

                //Item l_searchItem = ParseSelectionFilter.ParseSelection(pi_GarmentStyleClass.SelectionFilter, l_getItem);
                //Item l_deleteItem = l_searchItem.apply();

                if (l_deleteItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.removeGarmentStyleById, "1", l_deleteItem.getErrorCode());
                    l_returnClass.ErrorString = "removeGarmentStyleById Error:" + l_deleteItem.getErrorString();
                    l_returnClass.ErrorDetail = "removeGarmentStyleById Error:" + l_deleteItem.getErrorDetail();

                    return l_returnClass;
                }

                if (l_deleteItem.getItemCount() == 0)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.removeGarmentStyleById, "1", "");
                    l_returnClass.ErrorString = "removeGarmentStyleById Error:Not Item To Operation";
                    l_returnClass.ErrorDetail = "removeGarmentStyleById Error:Not Item To Operation";

                    return l_returnClass;
                }

                if (l_deleteItem.getItemCount() > 1)
                {
                    for (int i = 0; i < l_deleteItem.getItemCount(); i++)
                    {
                        Item l_deleteDetailItem = l_deleteItem.getItemByIndex(i);
                        l_deleteDetailItem.setAction("delete");
                        Item l_returnItem = l_deleteDetailItem.apply();
                        if (l_returnItem.isError())
                        {
                            l_returnClass.SuccessFlag = false;
                            l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.removeGarmentStyleById, "1", l_returnItem.getErrorCode());
                            l_returnClass.ErrorString = "removeGarmentStyleById Error:" + l_returnItem.getErrorString();
                            l_returnClass.ErrorDetail = "removeGarmentStyleById Error:" + l_returnItem.getErrorDetail();

                            return l_returnClass;
                        }
                    }
                }
                else
                {
                    l_deleteItem.setAction("delete");
                    Item l_returnItem = l_deleteItem.apply();
                    if (l_returnItem.isError())
                    {
                        l_returnClass.SuccessFlag = false;
                        l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.removeGarmentStyleById, "1", l_returnItem.getErrorCode());
                        l_returnClass.ErrorString = "removeGarmentStyleById Error:" + l_returnItem.getErrorString();
                        l_returnClass.ErrorDetail = "removeGarmentStyleById Error:" + l_returnItem.getErrorDetail();

                        return l_returnClass;
                    }
                }

                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "removeGarmentStyleById Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.removeGarmentStyleById, "2", "");
                l_returnClass.ErrorString = "removeGarmentStyleById Error";
                l_returnClass.ErrorDetail = "removeGarmentStyleById Error:" + ex.Message;

                return l_returnClass;
            }
        }

        /// <summary>
        /// Get GarmentStyle Image 
        /// 2015-08-19 add by WesChen
        /// </summary>
        /// <param name="pi_selectionFilter"></param>
        /// <returns></returns>
        public GarmentStyleClass getGarmentStyleImage(GarmentStyleClass pi_GarmentStyleClass)
        {
            GarmentStyleClass l_returnClass = new GarmentStyleClass();
            l_returnClass.SuccessFlag = true;
            if (pi_GarmentStyleClass == null)
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleImage, "0", "");
                l_returnClass.ErrorString = "getGarmentStyleImage Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "getGarmentStyleImage Error: Parameter Is Null , Please Setting Parameter And Try Later.";

                return l_returnClass;
            }

            try
            {

                //search GarmentStyle 
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyle_ItemName"), "get");

                Item l_returnItem = null;
                if (pi_GarmentStyleClass.SelectionFilter != null)
                {
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(pi_GarmentStyleClass.SelectionFilter, l_getItem);
                    l_returnItem = l_searchItem.apply();
                }
                else
                {
                    l_returnItem = l_getItem.apply();
                }

                if (l_getItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleImage, "1", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "getGarmentStyleImage Error:" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "getGarmentStyleImage Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }

                //parse garmentStyle image path
                if(l_getItem.isEmpty())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleImage, "1", ""); ;
                    l_returnClass.ErrorString = "getGarmentStyleImage Error:Get Item Is Empty .";
                    l_returnClass.ErrorDetail = "getGarmentStyleImage Error:Aras Apply Item Return Is Empty , Please Check Selection Parameter .";

                    return l_returnClass;
                }

                string l_ImageFileItemValue = l_returnItem.getItemByIndex(0).getProperty("thumbnail");
                if (string.IsNullOrEmpty( l_ImageFileItemValue))
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleImage, "1", "");
                    l_returnClass.ErrorString = "getGarmentStyleImage Error:Garment Style Item Image Is Empty .";
                    l_returnClass.ErrorDetail = "getGarmentStyleImage Error:Garment Record Image Is Empty , Please Upload Image , And Get Image Later .";

                    return l_returnClass;
                }
                if (l_ImageFileItemValue.IndexOf("vault:///?fileId=") == -1)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleImage, "1", "");
                    l_returnClass.ErrorString = "getGarmentStyleImage Error:Garment Style Item Image Is Not Vault .";
                    l_returnClass.ErrorDetail = "getGarmentStyleImage Error:Garment Record Image Is Empty , Please Upload Image , And Get Image Later .";

                    return l_returnClass;
                }

                //get file item
                string l_fileItemID = Common.StringHelper.GetRightString(l_ImageFileItemValue,"vault:///?fileId="); 
                Item l_fileItem = m_Innovator.getItemById("File", l_fileItemID);

                if (pi_GarmentStyleClass.GetImageType == eumImageType.UseTempPath)
                {
                    //copy file to temp path
                    string l_tempImagePath = ItemDownFilePath.path + l_fileItem.getID() + "." + Common.StringHelper.GetRightString(l_fileItem.getFileName(), ".");
                    string l_tempImageUrl = Common.ConfigHelper.GetAPPConfigValue("tempFileUrl") + l_fileItem.getID() + "." + Common.StringHelper.GetRightString(l_fileItem.getFileName(), ".");
                    m_Innovator.getConnection().DownloadFile(l_fileItem, l_tempImagePath, true);
                    l_returnClass.GetReturnString = l_tempImageUrl; 
                }
                else if (pi_GarmentStyleClass.GetImageType == eumImageType.UsePrimaryPath)
                {
                    l_returnClass.GetReturnString = m_Innovator.getFileUrl(l_fileItemID, UrlType.SecurityToken);
                }

                //return temp path                

                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getGarmentStyleImage Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleImage, "2", "");
                l_returnClass.ErrorString = "getGarmentStyleImage Error";
                l_returnClass.ErrorDetail = "getGarmentStyleImage Error:" + ex.Message;

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
