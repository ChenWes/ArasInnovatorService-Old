using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using Aras;
using Aras.IOM;
using ArasService.Common;
using ArasService.ArasModel;

namespace ArasService
{
    /// <summary>
    /// Summary description for GarmentStyleManagementWS
    /// </summary>
    [WebService(Namespace = "http://esq.esquel.com/arasservice/garmentstyleservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class GarmentStyleManagementWS : System.Web.Services.WebService
    {
        HttpServerConnection m_Connection;
        Innovator m_Innovator;

        public void ArasServiceConfig(string pi_userName, string pi_pwd)
        {
            m_Connection = IomFactory.CreateHttpServerConnection(ConfigHelper.GetAPPConfigValue("InnovatorUrl"), ConfigHelper.GetAPPConfigValue("InnovatorDB"), pi_userName, Innovator.ScalcMD5(pi_pwd));
            m_Innovator = IomFactory.CreateInnovator(m_Connection);
        }

        [WebMethod]
        public List<GarmentType> getGarmentStyleList(SelectionFilter pi_selectionFilter)
        {
            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem("TrimPart", "get");
                l_getItem = ParseSelectionFilter.ParseSelection(pi_selectionFilter, l_getItem);
                ItemConverHelper<GarmentType> l_itemConverHelper = new ItemConverHelper<GarmentType>();
                List<GarmentType> l_getList = l_itemConverHelper.ItemConver(l_getItem);
                return l_getList;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getGarmentStyleList Error:" + ex.Message);
                return null;
            }
        }

        [WebMethod]
        public GarmentType getGarmentStyleById(SelectionFilter pi_selectionFilter)
        {
            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem("TrimPart", "get");
                l_getItem = ParseSelectionFilter.ParseSelection(pi_selectionFilter, l_getItem);
                ItemConverHelper<GarmentType> l_itemConverHelper = new ItemConverHelper<GarmentType>();
                List<GarmentType> l_getList = l_itemConverHelper.ItemConver(l_getItem);
                return l_getList[0];
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getGarmentStyleById Error:" + ex.Message);
                return null;
            }
        }

        [WebMethod]
        public bool createGarmentStyle(List<GarmentType> pi_object)
        {
            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem();

                ItemConverHelper<GarmentType> l_itemConverHelper = new ItemConverHelper<GarmentType>();
                l_getItem = l_itemConverHelper.ItemConver(pi_object, l_getItem, "TrimPart", "add");

                Item l_returnItem = l_getItem.apply();
                return true;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "createGarmentStyle Error:" + ex.Message);
                return false;              
            }

        }

        [WebMethod]
        public bool updateGarmentStyle(List<GarmentType> pi_object)
        {
            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem();

                ItemConverHelper<GarmentType> l_itemConverHelper = new ItemConverHelper<GarmentType>();
                l_getItem = l_itemConverHelper.ItemConver(pi_object, l_getItem, "TrimPart", "edit");

                Item l_returnItem = l_getItem.apply();
                return true;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "updateGarmentStyle Error:" + ex.Message);
                return false;
            }
        }

        [WebMethod]
        public bool removeGarmentStyleById(SelectionFilter pi_selectionFilter)
        {
            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem("TrimPart", "get");
                l_getItem = ParseSelectionFilter.ParseSelection(pi_selectionFilter, l_getItem);

                l_getItem.setAction("remove");
                Item l_returnItem = l_getItem.apply();

                return true;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "removeGarmentStyleById Error:" + ex.Message);
                return false;               
            }
        }
        
    }
}
