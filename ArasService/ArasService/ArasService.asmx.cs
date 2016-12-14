using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using Aras;
using Aras.IOM;
using ArasService.Common;

namespace ArasService
{
    /// <summary>
    /// Summary description for ArasService
    /// Aras Innovator Web Service Interface To ESB
    /// 2015-08-06 add by WesChen
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ArasService : System.Web.Services.WebService
    {
        HttpServerConnection m_Connection;
        Innovator m_Innovator;
        
        public void ArasServiceConfig(string pi_userName,string pi_pwd)
        {
            m_Connection = IomFactory.CreateHttpServerConnection(ConfigHelper.GetAPPConfigValue("InnovatorUrl"), ConfigHelper.GetAPPConfigValue("InnovatorDB"), pi_userName, Innovator.ScalcMD5(pi_pwd));
            m_Innovator = IomFactory.CreateInnovator(m_Connection);
        }

        #region Test Method

        [WebMethod]
        public List<ArasModel.NewPart> GetPartList(int pi_pageindex)
        {
            //ArasServiceConfig("admin", "innovator");

            //List<ArasModel.NewPart> l_retrurnlist = new List<ArasModel.NewPart>();
            //try
            //{
            //    Item l_item = m_Innovator.newItem("my part", "get");
            //    l_item.setAttribute("pagesize", "20");
            //    l_item.setAttribute("page", "1");

            //    Item l_returnitem = l_item.apply();

            //    for (int i = 0; i < l_returnitem.getItemCount(); i++)
            //    {
            //        ArasModel.NewPart l_newpart = new ArasModel.NewPart()
            //        {
            //            part_number = l_returnitem.getItemByIndex(i).getProperty("part_number"),
            //            description = l_returnitem.getItemByIndex(i).getProperty("part_number"),
            //            cost = Convert.ToDecimal (l_returnitem.getItemByIndex(i).getProperty("cost"))
            //        };

            //        l_retrurnlist.Add(l_newpart);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogFileHelper.ExcuteEventLog(LogFilePath.path, ex.Source + "----" + ex.Message);
            //}
            //return l_retrurnlist;

            //-----------------------------------------------------------------------------------
            //ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
            //Item l_item = m_Innovator.newItem("my part", "get");
            //l_item.setAttribute("pagesize", "20");
            //l_item.setAttribute("page", "1");

            //Item l_returnitem = l_item.apply();
            //ItemConverHelper<ArasModel.NewPart> l_itemConverHelper = new ItemConverHelper<ArasModel.NewPart>();
            //List<ArasModel.NewPart> l_getList = l_itemConverHelper.ItemConver(l_returnitem);
            //return l_getList;
            //-----------------------------------------------------------------------------------
            ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
            Item l_item = m_Innovator.newItem("my part", "get");
            l_item.setAttribute("pagesize", "20");
            l_item.setAttribute("page", pi_pageindex.ToString());

            Item l_returnitem = l_item.apply();
            ItemConverHelper<ArasModel.NewPart> l_itemConverHelper = new ItemConverHelper<ArasModel.NewPart>();
            List<ArasModel.NewPart> l_getList = l_itemConverHelper.ItemConver(l_returnitem);
            return l_getList;
        }

        [WebMethod]
        public bool AddPart(ArasModel.NewPart pi_part)
        {
            bool l_returnflag = true;

            ArasServiceConfig("admin", "innovator");
            try
            {
                Item l_item = m_Innovator.newItem("part", "add");
                l_item.setProperty("part_number", pi_part.part_number);
                l_item.setProperty("description", pi_part.description);
                l_item.setProperty("cost", pi_part.cost.ToString());
                Item l_returnitem = l_item.apply();
            }
            catch (Exception ex)
            {
                l_returnflag = false;
                LogFileHelper.ExcuteEventLog(LogFilePath.path, ex.Source + "----" + ex.Message);
            }

            return l_returnflag;
        }

        [WebMethod]
        public bool EditPart(ArasModel.NewPart pi_part)
        {
            bool l_returnflag = true;
            ArasServiceConfig("admin", "innovator");
            try
            {
                Item l_item = m_Innovator.newItem("part", "get");
                l_item.setProperty("part_number", pi_part.part_number);


                Item l_getitem = l_item.apply();
                l_getitem.setProperty("description", pi_part.description);
                l_getitem.setProperty("cost", pi_part.cost.ToString());
                Item l_returnitem = l_getitem.apply();
            }
            catch (Exception ex)
            {
                l_returnflag = false;
                LogFileHelper.ExcuteEventLog(LogFilePath.path, ex.Source + "----" + ex.Message);
            }

            return l_returnflag;
        }

        //-----------------------------------------------------------------------------------------

        #endregion        


        #region Fabric

        [WebMethod]
        public void GetFabricLibrary(SelectionFilter pi_selectionFileter)
        {
            ArasServiceConfig("admin", "innovator");
            Item l_fabricItem = m_Innovator.newItem("Fabric", "get");
            //l_fabricItem = ParseSelectionFilter.ParseSelection(pi_selectionFileter, l_fabricItem);

            //string l_dd= l_fabricItem.ToString();
            
        }

        [WebMethod]
        public void GetFabricImage()
        {

        }

        #endregion

        #region Style

        [WebMethod]
        public void GetStyleLibrary()
        {

        }

        [WebMethod]
        public void GetStyleImage()
        {

        }

        #endregion

        #region Trim 

        [WebMethod]
        public void GetTrimLibrary()
        {

        }

        [WebMethod]
        public void GetTrimImage()
        {

        }

        #endregion

        #region Product

        [WebMethod]
        public void CreateProduct()
        {

        }

        [WebMethod]
        public void UpdateProduct()
        {

        }

        [WebMethod]
        public void GetProduct()
        {

        }

        #endregion

        [WebMethod]
        public void TestMethod()
        {
            
            //Sample 1
            SelectionFilter exampleFilter = SelectionFilter.CreateLeaf("A", "EQ", "1");
            GetFabricLibrary(exampleFilter);

            //Sample 2
            exampleFilter = SelectionFilter.CreateAndFilter(
                new SelectionFilter[]{
                    SelectionFilter.CreateLeaf("A", "EQ", "1"),
                    SelectionFilter.CreateLeaf("B", "NEQ", "2"),
                    SelectionFilter.CreateLeaf("C", "GT", "3")
                }
            );
            GetFabricLibrary(exampleFilter);

            //Sample 3
            exampleFilter = SelectionFilter.CreateOrFilter(
                new SelectionFilter[]{
                    SelectionFilter.CreateAndFilter(
                        new SelectionFilter[]{
                            SelectionFilter.CreateLeaf("A", "EQ", "1"),
                            SelectionFilter.CreateLeaf("B", "NEQ", "2"),
                            SelectionFilter.CreateLeaf("C", "GT", "3")
                        }
                    ),
                    SelectionFilter.CreateLeaf("D", "STE", "4")
                }
            );
            GetFabricLibrary(exampleFilter);

            //Sample 4
            exampleFilter = SelectionFilter.CreateOrFilter(
                new SelectionFilter[]{
                    SelectionFilter.CreateAndFilter(
                        new SelectionFilter[]{
                            SelectionFilter.CreateLeaf("A", "EQ", "1"),
                            SelectionFilter.CreateLeaf("B", "NEQ", "2"),
                            SelectionFilter.CreateLeaf("C", "GT", "3")
                        }
                    ),
                     SelectionFilter.CreateAndFilter(
                        new SelectionFilter[]{
                            SelectionFilter.CreateLeaf("D", "STE", "4"),
                            SelectionFilter.CreateLeaf("E", "GTE", "5"),
                        }
                    )
                }
            );
            GetFabricLibrary(exampleFilter);
        }
        
    }
}
