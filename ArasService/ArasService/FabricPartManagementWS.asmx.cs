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
    /// Summary description for FabricPartManagementWS
    /// </summary>
    [WebService(Namespace = "http://esq.esquel.com/arasservice/fabricpartservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class FabricPartManagementWS : System.Web.Services.WebService
    {
        HttpServerConnection m_Connection;
        Innovator m_Innovator;

        public void ArasServiceConfig(string pi_userName, string pi_pwd)
        {
            m_Connection = IomFactory.CreateHttpServerConnection(ConfigHelper.GetAPPConfigValue("InnovatorUrl"), ConfigHelper.GetAPPConfigValue("InnovatorDB"), pi_userName, Innovator.ScalcMD5(pi_pwd));
            m_Innovator = IomFactory.CreateInnovator(m_Connection);
        }

        [WebMethod]
        public List<FabricPart> getFabricPartList(SelectionFilter pi_selectionFilter)
        {
            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem("FabricPart", "get");
                l_getItem = ParseSelectionFilter.ParseSelection(pi_selectionFilter, l_getItem);
                ItemConverHelper<FabricPart> l_itemConverHelper = new ItemConverHelper<FabricPart>();
                List<FabricPart> l_getList = l_itemConverHelper.ItemConver(l_getItem);
                return l_getList;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getFabricPartList Error:" + ex.Message);
                return null;                
            }
        }

        [WebMethod]
        public FabricPart getFabricPartById(SelectionFilter pi_selectionFilter)
        {
            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem("FabricPart", "get");
                l_getItem = ParseSelectionFilter.ParseSelection(pi_selectionFilter, l_getItem);
                ItemConverHelper<FabricPart> l_itemConverHelper = new ItemConverHelper<FabricPart>();
                List<FabricPart> l_getList = l_itemConverHelper.ItemConver(l_getItem);
                return l_getList[0];
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getFabricPartById Error:" + ex.Message);
                return null;
            }
        }
        
        [WebMethod]
        public string getFabricPartImage(SelectionFilter pi_selectionFilter)
        {
            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem("FabricPart", "get");
                l_getItem = ParseSelectionFilter.ParseSelection(pi_selectionFilter, l_getItem);
                ItemConverHelper<FabricPart> l_itemConverHelper = new ItemConverHelper<FabricPart>();
                List<FabricPart> l_getList = l_itemConverHelper.ItemConver(l_getItem);
                return l_getList[0].imagePath;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getFabricPartImage Error:" + ex.Message);
                return null;
            }
        }
    }
}
