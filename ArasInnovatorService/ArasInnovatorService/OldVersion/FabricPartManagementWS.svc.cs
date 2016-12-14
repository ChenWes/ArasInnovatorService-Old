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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FabricPartManagementWS" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FabricPartManagementWS.svc or FabricPartManagementWS.svc.cs at the Solution Explorer and start debugging.http://localhost:49504/FabricPartManagementWS.svc.cs
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FabricPartManagementWS : IFabricPartManagementWS
    {        

        HttpServerConnection m_Connection;
        Innovator m_Innovator;

        public void ArasServiceConfig(string pi_userName, string pi_pwd)
        {
            m_Connection = IomFactory.CreateHttpServerConnection(ConfigHelper.GetAPPConfigValue("InnovatorUrl"), ConfigHelper.GetAPPConfigValue("InnovatorDB"), pi_userName, Innovator.ScalcMD5(pi_pwd));
            m_Innovator = IomFactory.CreateInnovator(m_Connection);
        }
        
        /// <summary>
        /// Get FabricPart List by Parameter
        /// 2015-08-18 add by WesChen 
        /// </summary>
        /// <param name="pi_FabricPartClass"></param>
        /// <returns></returns>
        public FabricPartClass getFabricPartList(FabricPartClass pi_FabricPartClass)
        {
            FabricPartClass l_returnClass = new FabricPartClass();
            l_returnClass.SuccessFlag = true;
            if (pi_FabricPartClass == null)
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartList, "0", "");
                l_returnClass.ErrorString = "getFabricPartList Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "getFabricPartList Error: Parameter Is Null , Please Setting Parameter And Try Later.";

                return l_returnClass;
            }
            try
            {
                //connection and new item
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                //l_getItem.get("partsize");

                l_getItem.setAttribute("pagesize", pi_FabricPartClass.DisplayPageSize.ToString());
                l_getItem.setAttribute("page", pi_FabricPartClass.DisplayPageIndex.ToString());

                Item l_returnItem=null;
                //add parameter to search item                
                if(pi_FabricPartClass.SelectionFilter !=null)
                {
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(pi_FabricPartClass.SelectionFilter, l_getItem);

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
                    //get GarmentStyle BOM List
                    //Item l_getGarmentStyleBOM = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleBOM_ItemName"), "get");
                    //Item l_garmentStyleBOMlink = l_getGarmentStyleBOM.createRelatedItem(ConfigHelper.GetAPPConfigValue("GarmentBOM_ItemName"), "get");

                    //Item l_getGarmentBOMPart = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentBOMPart_ItemName"), "get");
                    //Item l_bomPartLink = l_getGarmentBOMPart.createRelatedItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                    Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    Item l_colorComboColorCodeLink = l_getPartColorCombo.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCombo_ItemName"), "get");

                    Item l_getColorComboColorCode = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName"), "get");
                    l_getColorComboColorCode.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCode_ItemName"), "get");

                    l_colorComboColorCodeLink.addRelationship(l_getColorComboColorCode);

                    //l_bomPartLink.addRelationship(l_getPartColorCombo);

                    //l_garmentStyleBOMlink.addRelationship(l_getGarmentBOMPart);
                    //l_searchItem.addRelationship(l_getGarmentStyleBOM);
                    l_searchItem.addRelationship(l_getPartColorCombo);
                    //----------------------------------------------------------------------------------------------

                    //get part Content List
                    Item l_getPartContent = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"), "get");
                    l_searchItem.addRelationship(l_getPartContent);

                    l_returnItem = l_searchItem.apply();
                }
                else
                {
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
                    Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    l_getItem.addRelationship(l_getPartColorCombo);
                    //get part Content List
                    Item l_getPartContent = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"), "get");
                    l_getItem.addRelationship(l_getPartContent);

                    l_returnItem = l_getItem.apply();
                }

                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartList, "1", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "getFabricPartList Error:" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "getFabricPartList Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }
                
                //item convert to class
                ItemConverHelper<Part> l_itemConverHelper = new ItemConverHelper<Part>();
                List<Part> l_getList = l_itemConverHelper.ItemConver(l_returnItem);

                //get link
                ClassGetLink<Part> l_GetLink = new ClassGetLink<Part>();
                l_returnClass.FabricPartList = l_GetLink.GetLinkPropery(l_getList, m_Innovator);

                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getFabricPartList Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartList, "2", "");
                l_returnClass.ErrorString = "getFabricPartList Error";
                l_returnClass.ErrorDetail = "getFabricPartList Error:" + ex.Message;

                return l_returnClass;
            }
        }

        /// <summary>
        /// Get FabricPart By id
        /// 2015-08-18 add by WesChen
        /// </summary>
        /// <param name="pi_FabricPartClass"></param>
        /// <returns></returns>
        public FabricPartClass getFabricPartById(FabricPartClass pi_FabricPartClass)
        {
            FabricPartClass l_returnClass = new FabricPartClass();
            l_returnClass.SuccessFlag = true;
            if (pi_FabricPartClass == null)
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartById, "0", "");
                l_returnClass.ErrorString = "getFabricPartById Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "getFabricPartById Error: Parameter Is Null , Please Setting Parameter And Try Later.";

                return l_returnClass;
            }
            try
            {
                //connection and new item
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                //l_getItem.setAttribute("pagesize", pi_FabricPartClass.DisplayPageSize.ToString());
                //l_getItem.setAttribute("page", pi_FabricPartClass.DisplayPageIndex.ToString());

                Item l_returnItem = null;
                //add parameter to search item
                if (!string.IsNullOrEmpty( pi_FabricPartClass.OperationID))
                {
                    pi_FabricPartClass.SelectionFilter = SelectionFilter.CreateLeaf("ITEM_NUMBER", "EQ", pi_FabricPartClass.OperationID.Trim());
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(pi_FabricPartClass.SelectionFilter, l_getItem);

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
                    Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    l_searchItem.addRelationship(l_getPartColorCombo);
                    //get part Content List
                    Item l_getPartContent = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"), "get");
                    l_searchItem.addRelationship(l_getPartContent);

                    l_returnItem = l_searchItem.apply();
                }
                else
                {
                    //l_returnItem = l_getItem.apply();
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartById, "1", "");
                    l_returnClass.ErrorString = "getFabricPartById Error:Operation ID Is Null" ;
                    l_returnClass.ErrorDetail = "getFabricPartById Error:Please Setting Operation ID And Try Later.";

                    return l_returnClass;
                }

                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartById, "1", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "getFabricPartById Error:" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "getFabricPartById Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }

                //item convert to class
                ItemConverHelper<Part> l_itemConverHelper = new ItemConverHelper<Part>();
                List<Part> l_getList = l_itemConverHelper.ItemConver(l_returnItem);
                l_returnClass.FabricPartList.Add(l_getList[0]);

                //get link
                ClassGetLink<Part> l_GetLink = new ClassGetLink<Part>();
                l_returnClass.FabricPartList = l_GetLink.GetLinkPropery(l_returnClass.FabricPartList, m_Innovator);

                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getFabricPartById Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartById, "2", "");
                l_returnClass.ErrorString = "getFabricPartById Error";
                l_returnClass.ErrorDetail = "getFabricPartById Error:" + ex.Message;

                return l_returnClass;
            }
        }

        /// <summary>
        /// Get FabricPart Image
        /// 2015-08-19 add by WesChen
        /// </summary>
        /// <param name="pi_FabricPartClass"></param>
        /// <returns></returns>
        public FabricPartClass getFabricPartImage(FabricPartClass pi_FabricPartClass)
        {
            FabricPartClass l_returnClass = new FabricPartClass();
            l_returnClass.SuccessFlag = true;
            if (pi_FabricPartClass == null)
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartImage, "0", "");
                l_returnClass.ErrorString = "getFabricPartImage Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "getFabricPartImage Error: Parameter Is Null , Please Setting Parameter And Try Later.";

                return l_returnClass;
            }

            try
            {

                //search GarmentStyle 
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                Item l_returnItem = null;
                //pi_FabricPartClass.SelectionFilter = Common.SelectionFilter.CreateLeaf("item_number", "EQ", "HL201506789");
                if (pi_FabricPartClass.SelectionFilter != null)
                {
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(pi_FabricPartClass.SelectionFilter, l_getItem);
                    l_returnItem = l_searchItem.apply();
                }
                else
                {
                    l_returnItem = l_getItem.apply();
                }

                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartImage, "1", l_getItem.getErrorCode());
                    l_returnClass.ErrorString = "getFabricPartImage Error:" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "getFabricPartImage Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }

                //parse garmentStyle image path
                if (l_returnItem.isEmpty())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartImage, "1", "");
                    l_returnClass.ErrorString = "getFabricPartImage Error:Get Item Is Empty .";
                    l_returnClass.ErrorDetail = "getFabricPartImage Error:Aras Apply Item Return Is Empty , Please Check Selection Parameter .";

                    return l_returnClass;
                }

                string l_ImageFileItemValue = l_returnItem.getItemByIndex(0).getProperty("cn_thumbnail");
                if (string.IsNullOrEmpty(l_ImageFileItemValue))
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartImage, "1", "");
                    l_returnClass.ErrorString = "getFabricPartImage Error:Garment Style Item Image Is Empty .";
                    l_returnClass.ErrorDetail = "getFabricPartImage Error:Garment Record Image Is Empty , Please Upload Image , And Get Image Later .";

                    return l_returnClass;
                }
                if (l_ImageFileItemValue.IndexOf("vault:///?fileId=") == -1)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartImage, "1", "");
                    l_returnClass.ErrorString = "getFabricPartImage Error:Garment Style Item Image Is Not Vault .";
                    l_returnClass.ErrorDetail = "getFabricPartImage Error:Garment Record Image Is Empty , Please Upload Image , And Get Image Later .";

                    return l_returnClass;
                }

                //get file item
                string l_fileItemID = Common.StringHelper.GetRightString(l_ImageFileItemValue, "vault:///?fileId=");
                Item l_fileItem = m_Innovator.getItemById("File", l_fileItemID);

                if (pi_FabricPartClass.GetImageType == eumImageType.UseTempPath)
                {
                    //copy file to temp path
                    string l_tempImagePath = ItemDownFilePath.path + l_fileItem.getID() + "." + Common.StringHelper.GetRightString(l_fileItem.getFileName(), ".");
                    string l_tempImageUrl = Common.ConfigHelper.GetAPPConfigValue("tempFileUrl") + l_fileItem.getID() + "." + Common.StringHelper.GetRightString(l_fileItem.getFileName(), ".");
                    m_Innovator.getConnection().DownloadFile(l_fileItem, l_tempImagePath, true);
                    l_returnClass.GetReturnString = l_tempImageUrl;
                }
                else if (pi_FabricPartClass.GetImageType == eumImageType.UsePrimaryPath)
                {
                    l_returnClass.GetReturnString = m_Innovator.getFileUrl(l_fileItemID, UrlType.SecurityToken);
                }

                //return temp path  

                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getFabricPartImage Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getFabricPartImage, "2", "");
                l_returnClass.ErrorString = "getFabricPartImage Error";
                l_returnClass.ErrorDetail = "getFabricPartImage Error:" + ex.Message;

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
