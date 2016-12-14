using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using ArasServiceObject.TrimPartManageService;

using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace ArasServiceObject.Controllers
{
    public class TrimPartController : Controller
    {
        //
        // GET: /TrimPart/
        public ActionResult Index()
        {
            TrimPartManageServiceClient l_serviceClient = new TrimPartManageServiceClient();
            TrimPartClass l_searchClass = new TrimPartClass();
            

            TrimPartClass l_getreturn = l_serviceClient.GetTrimPartList(null,1,20);
            JavaScriptSerializer l_jss = new JavaScriptSerializer();
            ViewBag.Data = l_jss.Serialize(l_getreturn.TrimPartList);

            ViewBag.xml = Serializer(typeof(TrimPartClass), l_getreturn);

            ViewBag.TotalPage = l_getreturn.DisplayPageIndex;
            ViewBag.CurrentPage = l_getreturn.DisplayPageIndex;

            return View(ViewBag); 
        }

        public ActionResult GetById()
        {
            TrimPartManageServiceClient l_serviceClient = new TrimPartManageServiceClient();

            TrimPartClass l_searchClass = new TrimPartClass();
            TrimPartClass l_getreturn = l_serviceClient.GetTrimPartByID("55108550");
            JavaScriptSerializer l_jss = new JavaScriptSerializer();
            ViewBag.Data = l_jss.Serialize(l_getreturn.TrimPartList);

            ViewBag.xml = Serializer(typeof(TrimPartClass), l_getreturn);

            ViewBag.TotalPage = l_getreturn.DisplayPageIndex;
            ViewBag.CurrentPage = l_getreturn.DisplayPageIndex;

            return View(ViewBag);
        }

        public ActionResult GetImage()
        {
            TrimPartManageServiceClient l_serviceClient = new TrimPartManageServiceClient();
                       

            TrimPartClass l_getreturn = l_serviceClient.GetTrimPartImage("55108550", eumImageType.UsePrimaryPath);
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