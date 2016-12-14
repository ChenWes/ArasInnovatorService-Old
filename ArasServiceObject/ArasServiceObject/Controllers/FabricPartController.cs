using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using ArasServiceObject.FabricPartManageService;
using ArasInnovatorService.Common;

using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace ArasServiceObject.Controllers
{
    public class FabricPartController : Controller
    {
        //
        // GET: /FabricPart/
        public ActionResult Index()
        {
            FabricPartManageServiceClient l_serviceClient = new FabricPartManageServiceClient();
                        

            FabricPartClass l_getreturn = l_serviceClient.GetFabricPartList(null,1,20);
            JavaScriptSerializer l_jss = new JavaScriptSerializer();
            ViewBag.Data = l_jss.Serialize(l_getreturn.FabricPartList);

            ViewBag.xml = Serializer(typeof(FabricPartClass), l_getreturn);

            ViewBag.TotalPage = l_getreturn.DisplayPageIndex;
            ViewBag.CurrentPage = l_getreturn.DisplayPageIndex;

            return View(ViewBag); 
        }

        public ActionResult GetById()
        {
            FabricPartManageServiceClient l_serviceClient = new FabricPartManageServiceClient();

            FabricPartClass l_getreturn = l_serviceClient.GetFabricPartByID("DC100A50-197418K");
            JavaScriptSerializer l_jss = new JavaScriptSerializer();
            ViewBag.Data = l_jss.Serialize(l_getreturn.FabricPartList);

            ViewBag.xml = Serializer(typeof(FabricPartClass), l_getreturn);

            ViewBag.TotalPage = l_getreturn.DisplayPageIndex;
            ViewBag.CurrentPage = l_getreturn.DisplayPageIndex;

            return View(ViewBag);
        }

        public ActionResult GetImage()
        {
            FabricPartManageServiceClient l_serviceClient = new FabricPartManageServiceClient();                      

            FabricPartClass l_getreturn = l_serviceClient.GetFabricPartImage("DC100A50-197418K", eumImageType.UsePrimaryPath);
            JavaScriptSerializer l_jss = new JavaScriptSerializer();

            ViewBag.imageUrl = l_getreturn.GetReturnString;

            return View(ViewBag);
        }

        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }
	}
}