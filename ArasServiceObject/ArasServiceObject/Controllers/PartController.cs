using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace ArasServiceObject.Controllers
{
    public class PartController : Controller
    {
        //
        // GET: /Part/
        public ActionResult Index(int page = 1)
        {
            //TrimPartManagementWSClient l_get = new TrimPartManagementWSClient();
            //TrimPart[] l_trimPartList = l_get.TestMethod();

            //GarmentStyleManagementWSClient l_get = new GarmentStyleManagementWSClient();
            //GarmentStyleService.NewPart[] l_newpartlist = l_get.TestMethod();


            ////ArasServiceSoapClient l_serviceClient = new ArasServiceSoapClient();
            ////ArasService.NewPart[] l_getList = l_serviceClient.getpartlist(1);

            JavaScriptSerializer l_jss = new JavaScriptSerializer();
            ViewBag.Data = l_jss.Serialize(null);
            ViewBag.TotalPage = 10;
            ViewBag.CurrentPage = 1;

            return View(ViewBag);
        }

        public ActionResult Chat()
        {
            return View();
        }
	}
}