using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using ArasServiceObject.ProductManageService;


using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Serialization;

namespace ArasServiceObject.Controllers
{
    public class GarmentStyleController : Controller
    {
        //
        // GET: /GarmentType/
        public ActionResult Index()
        {
            ProductManageServiceClient l_serviceClient = new ProductManageServiceClient();            
            

            //l_serviceClient.CreateLeaf("ITEM_NUMBER", "CT", "PYE");

            SelectionFilter l_filter= l_serviceClient.CreateAndFilter(new SelectionFilter[]{
                //l_serviceClient.CreateLeaf("cn_cust_code","EQ","1C0A8F68FA1A4DBD9AFFFCDABF188F52"),
                //l_serviceClient.CreateLeaf("cn_brand_code","EQ","EE70BCABD38B42309D8748BD73A3E1E1"),
                l_serviceClient.CreateLeaf("cn_class1","EQ","TOP")
                //l_serviceClient.CreateLeaf("cn_class2","EQ","MDS")
            });

            ProductClass l_getreturn = l_serviceClient.GetProductList(l_filter, 1, 2);
            //JavaScriptSerializer l_jss = new JavaScriptSerializer();
            //ViewBag.Data = l_jss.Serialize(l_getreturn.ProductList.Product);


            //ViewBag.XML = Serializer(typeof(ProductClass), l_getreturn);

            ViewBag.TotalPage = l_getreturn.DisplayPageIndex;
            ViewBag.CurrentPage = l_getreturn.DisplayPageIndex;

            return View(ViewBag);            
        }

        public ActionResult GetById()
        {
            ArasServiceObject.Hubs.ChatHub.Send("System", DateTime.Now.ToString() + " Run GetByID");

            //ProductManageServiceClient l_serviceClient = new ProductManageServiceClient();

            //ProductClass l_getreturn = l_serviceClient.GetProductByID("PYE-15DSFB001ET");
            //JavaScriptSerializer l_jss = new JavaScriptSerializer();
            //ViewBag.Data = l_jss.Serialize(l_getreturn.ProductList);

            //ViewBag.XML = Serializer(typeof(ProductClass), l_getreturn);

            //ViewBag.TotalPage = l_getreturn.DisplayPageIndex;
            //ViewBag.CurrentPage = l_getreturn.DisplayPageIndex;

            return View(ViewBag);  
        }

        public ActionResult GetImage()
        {
            ProductManageServiceClient l_serviceClient = new ProductManageServiceClient();
            ProductClass l_getreturn = l_serviceClient.GetProductImage("PYE-15DSFB001ET", eumImageType.UsePrimaryPath);
            //JavaScriptSerializer l_jss = new JavaScriptSerializer();

            ViewBag.imageUrl = l_getreturn.GetReturnString;            

            return View(ViewBag);  
        }

        public ActionResult Add()
        {
            //GarmentStyleManagementWSClient l_serviceClient = new GarmentStyleManagementWSClient();
            //GarmentStyleClass l_searchClass = new GarmentStyleClass();
            //GarmentStyleService.Garment[] l_addclass = new GarmentStyleService.Garment[] { 
            //    new GarmentStyleService.Garment() { 
            //        ITEM_NUMBER = "PYE-TEST-005", CN_CUST_STYLE = "PYE-STYLE-111", CN_FDS_NO = "S16G00111", CN_CUST_CODE="10177", CN_BRAND_CODE="PYE",
            //        GarmentStyleBOMList=new GarmentStyleBOM[]{
            //            new GarmentStyleBOM()
            //            {                            
            //                GarmentBOMList=new GarmentBOM[]{
            //                    new GarmentBOM(){  
            //                        CN_BOM_TYPE="FAB",
            //                        GarmentBOMPartList=new GarmentBOMPart[]{
            //                            new GarmentBOMPart(){                                            
            //                                PartList=new Part[]{
            //                                    new Part(){
            //                                        ID="FD385BF631114BC1A0FDFC6ABA7CC4DE"
            //                                    }
            //                                }
            //                            },
            //                            new GarmentBOMPart(){
            //                                PartList =new Part[]
            //                                {
            //                                    new Part()
            //                                    {
            //                                        ID="7F878E19903C4CCBB01B64819F5C7658"
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    } 
            //};
            //l_searchClass.GarmentStyleList = l_addclass;

            //GarmentStyleClass l_getreturn = l_serviceClient.createGarmentStyle(l_searchClass);
            //ViewBag.msg = l_getreturn.GetReturnString;

            return View(ViewBag);
        }

        public ActionResult Edit()
        {
            //GarmentStyleManagementWSClient l_serviceClient = new GarmentStyleManagementWSClient();
            //GarmentStyleClass l_searchClass = new GarmentStyleClass();
            //GarmentStyleService.Garment[] l_updateclass = new GarmentStyleService.Garment[] { 
            //    new GarmentStyleService.Garment() { 
            //        ITEM_NUMBER = "PYE-TEST-002", CN_CUST_STYLE = "PYE-STYLE-005", CN_FDS_NO = "S16G00005", CN_CUST_CODE="10177", CN_BRAND_CODE="PYE", CREATED_BY_ID="Wes Chen",
            //        GarmentStyleBOMList=new GarmentStyleBOM[]{
            //            new GarmentStyleBOM()
            //            {                            
            //                GarmentBOMList=new GarmentBOM[]{
            //                    new GarmentBOM(){                                    
            //                        GarmentBOMPartList=new GarmentBOMPart[]{
            //                            new GarmentBOMPart(){                                            
            //                                PartList=new Part[]{
            //                                    new Part(){
            //                                        ID="C21A611306374AAC850C15CE3ABF3610",
            //                                        DESCRIPTION="chenxuhua"
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    } 
            //};
            //l_searchClass.GarmentStyleList = l_updateclass;
            //l_searchClass.KeyColumnsList = new String[] { "ITEM_NUMBER" };

            //GarmentStyleClass l_getreturn = l_serviceClient.updateGarmentStyle(l_searchClass);
            //ViewBag.msg = l_getreturn.GetReturnString;

            return View(ViewBag);
        }

        public ActionResult Remove()
        {
            ProductManageServiceClient l_serviceClient = new ProductManageServiceClient();

            ProductClass l_getreturn = l_serviceClient.RemoveProductByID("PYE-TEST-001");

            ViewBag.msg = l_getreturn.GetReturnString;

            return View(ViewBag);
        }

        public ActionResult List()
        {
            return View();
        }

        public JsonResult GetDepartment(int limit, int offset)
        {
            var lstRes = new List<ArasServiceObject.Models.Dept>();
            for (var i = 0; i < 50; i++)
            {
                var oModel = new ArasServiceObject.Models.Dept();
                oModel.ID = Guid.NewGuid().ToString();
                oModel.Name = "销售部[" + i.ToString() + "]";
                oModel.Level = i.ToString();
                oModel.Desc = "销售部[" + i.ToString() + "]描述信息";
                lstRes.Add(oModel);
            }

            var total = lstRes.Count;
            var rows = lstRes.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
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