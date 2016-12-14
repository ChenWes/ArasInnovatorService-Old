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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TrimPartManagementWS" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TrimPartManagementWS.svc or TrimPartManagementWS.svc.cs at the Solution Explorer and start debugging.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TrimPartManagementWS : ITrimPartManagementWS
    {
        HttpServerConnection m_Connection;
        Innovator m_Innovator;

        public void ArasServiceConfig(string pi_userName, string pi_pwd)
        {
            m_Connection = IomFactory.CreateHttpServerConnection(ConfigHelper.GetAPPConfigValue("InnovatorUrl"), ConfigHelper.GetAPPConfigValue("InnovatorDB"), pi_userName, Innovator.ScalcMD5(pi_pwd));
            m_Innovator = IomFactory.CreateInnovator(m_Connection);
        }

        /// <summary>
        /// get TrimPart List by Parameter
        /// 2015-08-18 add by WesChen
        /// </summary>
        /// <param name="pi_TrimPartClass"></param>
        /// <returns></returns>
        public TrimPartClass getTrimPartList(TrimPartClass pi_TrimPartClass)
        {
            TrimPartClass l_returnClass = new TrimPartClass();
            l_returnClass.SuccessFlag=true;
            if (pi_TrimPartClass == null)
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartList, "0", "");
                l_returnClass.ErrorString = "getTrimPartList Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "getTrimPartList Error: Parameter Is Null , Please Setting Parameter And Try Later.";

                return l_returnClass;
            }

            try
            {
                //connection and new item
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                l_getItem.setAttribute("pagesize", pi_TrimPartClass.DisplayPageSize.ToString());
                l_getItem.setAttribute("page", pi_TrimPartClass.DisplayPageIndex.ToString());
                
                Item l_returnItem =null;
                //add parameter to search item
                if (pi_TrimPartClass.SelectionFilter != null)
                {
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(pi_TrimPartClass.SelectionFilter, l_getItem);

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
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartList, "1", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "getTrimPartList Error:" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "getTrimPartList Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }

                ItemConverHelper<Part> l_itemConverHelper = new ItemConverHelper<Part>();
                List<Part> l_getList = l_itemConverHelper.ItemConver(l_returnItem);
                
                //get link
                ClassGetLink<Part> l_GetLink = new ClassGetLink<Part>();
                l_returnClass.TrimPartList = l_GetLink.GetLinkPropery(l_getList, m_Innovator);

                m_Connection.Logout();

                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getTrimPartList Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartList, "2", "");
                l_returnClass.ErrorString = "getTrimPartList Error";
                l_returnClass.ErrorDetail = "getTrimPartList Error:" + ex.Message;

                return l_returnClass;
            }

        }

        /// <summary>
        /// get TrimPart by id
        /// 2015-08-18 add by WesChen
        /// </summary>
        /// <param name="pi_TrimPartClass"></param>
        /// <returns></returns>
        public TrimPartClass getTrimPartById(TrimPartClass pi_TrimPartClass)
        {
            TrimPartClass l_returnClass = new TrimPartClass();
            l_returnClass.SuccessFlag = true;
            if (pi_TrimPartClass == null)
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartById, "0", "");
                l_returnClass.ErrorString = "getTrimPartById Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "getTrimPartById Error: Parameter Is Null , Please Setting Parameter And Try Later.";

                return l_returnClass;
            }

            try
            {
                //connection and new item
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                //l_getItem.setAttribute("pagesize", pi_TrimPartClass.DisplayPageSize.ToString());
                //l_getItem.setAttribute("page", pi_TrimPartClass.DisplayPageIndex.ToString());

                Item l_returnItem = null;
                //add parameter to search item
                if (!string.IsNullOrEmpty( pi_TrimPartClass.OperationID))
                {
                    pi_TrimPartClass.SelectionFilter = SelectionFilter.CreateLeaf("ITEM_NUMBER", "EQ", pi_TrimPartClass.OperationID.Trim());
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(pi_TrimPartClass.SelectionFilter, l_getItem);

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
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartById, "1", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "getTrimPartById Error:Operation ID Is Null";
                    l_returnClass.ErrorDetail = "getTrimPartById Error:Please Setting Operation ID And Try Later.";

                    return l_returnClass;
                }

                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartById, "1", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "getTrimPartById Error:" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "getTrimPartById Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }

                ItemConverHelper<Part> l_itemConverHelper = new ItemConverHelper<Part>();
                List<Part> l_getList = l_itemConverHelper.ItemConver(l_returnItem);
                l_returnClass.TrimPartList.Add(l_getList[0]);

                //get link
                ClassGetLink<Part> l_GetLink = new ClassGetLink<Part>();
                l_returnClass.TrimPartList = l_GetLink.GetLinkPropery(l_returnClass.TrimPartList, m_Innovator);

                m_Connection.Logout();

                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getTrimPartList Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartById, "2", "");
                l_returnClass.ErrorString = "getTrimPartById Error";
                l_returnClass.ErrorDetail = "getTrimPartById Error:" + ex.Message;

                return l_returnClass;
            }

        }

        /// <summary>
        /// Get Trim Part Image
        /// 2015-08-19 add by WesChen
        /// </summary>
        /// <param name="pi_TrimPartClass"></param>
        /// <returns></returns>
        public TrimPartClass getTrimPartImage(TrimPartClass pi_TrimPartClass)
        {
            TrimPartClass l_returnClass = new TrimPartClass();
            l_returnClass.SuccessFlag = true;

            if (pi_TrimPartClass == null)
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartImage, "0", "");
                l_returnClass.ErrorString = "getTrimPartImage Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "getTrimPartImage Error: Parameter Is Null , Please Setting Parameter And Try Later.";

                return l_returnClass;
            }

            try
            {
                //search GarmentStyle 
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                Item l_returnItem = null;
                if (pi_TrimPartClass.SelectionFilter != null)
                {
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(pi_TrimPartClass.SelectionFilter, l_getItem);
                    l_returnItem = l_searchItem.apply();
                }
                else
                {
                    l_returnItem = l_getItem.apply();
                }

                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartImage, "1", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "getTrimPartImage Error:" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "getTrimPartImage Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }

                //parse garmentStyle image path
                if (l_returnItem.isEmpty())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartImage, "1", ""); 
                    l_returnClass.ErrorString = "getTrimPartImage Error:Get Item Is Empty .";
                    l_returnClass.ErrorDetail = "getTrimPartImage Error:Aras Apply Item Return Is Empty , Please Check Selection Parameter .";

                    return l_returnClass;
                }

                string l_ImageFileItemValue = l_returnItem.getItemByIndex(0).getProperty("cn_thumbnail");
                if (string.IsNullOrEmpty(l_ImageFileItemValue))
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartImage, "1", ""); 
                    l_returnClass.ErrorString = "getTrimPartImage Error:Garment Style Item Image Is Empty .";
                    l_returnClass.ErrorDetail = "getTrimPartImage Error:Garment Record Image Is Empty , Please Upload Image , And Get Image Later .";

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

                if (pi_TrimPartClass.GetImageType == eumImageType.UseTempPath)
                {
                    //copy file to temp path
                    string l_tempImagePath = ItemDownFilePath.path + l_fileItem.getID() + "." + Common.StringHelper.GetRightString(l_fileItem.getFileName(), ".");
                    string l_tempImageUrl = Common.ConfigHelper.GetAPPConfigValue("tempFileUrl") + l_fileItem.getID() + "." + Common.StringHelper.GetRightString(l_fileItem.getFileName(), ".");
                    m_Innovator.getConnection().DownloadFile(l_fileItem, l_tempImagePath, true);
                    l_returnClass.GetReturnString = l_tempImageUrl;
                }
                else if (pi_TrimPartClass.GetImageType == eumImageType.UsePrimaryPath)
                {
                    l_returnClass.GetReturnString = m_Innovator.getFileUrl(l_fileItemID, UrlType.SecurityToken);
                }

                //return temp path  

                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getTrimPartImage Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getTrimPartImage, "2", ""); 
                l_returnClass.ErrorString = "getTrimPartImage Error";
                l_returnClass.ErrorDetail = "getTrimPartImage Error:" + ex.Message;

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
