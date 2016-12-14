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
    /// Summary description for TrimPartManagementWS
    /// </summary>
    [WebService(Namespace = "http://esq.esquel.com/arasservice/trimpartservice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TrimPartManagementWS : System.Web.Services.WebService
    {
        HttpServerConnection m_Connection;
        Innovator m_Innovator;

        public void ArasServiceConfig(string pi_userName, string pi_pwd)
        {
            m_Connection = IomFactory.CreateHttpServerConnection(ConfigHelper.GetAPPConfigValue("InnovatorUrl"), ConfigHelper.GetAPPConfigValue("InnovatorDB"), pi_userName, Innovator.ScalcMD5(pi_pwd));
            m_Innovator = IomFactory.CreateInnovator(m_Connection);
        }

        [WebMethod]
        public List<TrimPart> getTrimPartList(SelectionFilter pi_selectionFilter)
        {
            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem("TrimPart", "get");
                l_getItem = ParseSelectionFilter.ParseSelection(pi_selectionFilter, l_getItem);
                ItemConverHelper<TrimPart> l_itemConverHelper = new ItemConverHelper<TrimPart>();
                List<TrimPart> l_getList = l_itemConverHelper.ItemConver(l_getItem);
                return l_getList;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getTrimPartList Error:" + ex.Message);
                return null;                
            }

        }

        [WebMethod]
        public TrimPart getTrimPartById(SelectionFilter pi_selectionFilter)
        {
            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem("TrimPart", "get");
                l_getItem = ParseSelectionFilter.ParseSelection(pi_selectionFilter, l_getItem);
                ItemConverHelper<TrimPart> l_itemConverHelper = new ItemConverHelper<TrimPart>();
                List<TrimPart> l_getList = l_itemConverHelper.ItemConver(l_getItem);
                return l_getList[0];
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getTrimPartById Error:" + ex.Message);
                return null;                
            }

        }

        [WebMethod]
        public string getTrimPartImage(SelectionFilter pi_selectionFilter)
        {
            try
            {
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem("FabricPart", "get");
                l_getItem = ParseSelectionFilter.ParseSelection(pi_selectionFilter, l_getItem);
                ItemConverHelper<TrimPart> l_itemConverHelper = new ItemConverHelper<TrimPart>();
                List<TrimPart> l_getList = l_itemConverHelper.ItemConver(l_getItem);
                return l_getList[0].imagePath;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "getTrimPartImage Error:" + ex.Message);
                return null;
            }
        }
    }
}
