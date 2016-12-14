using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using ArasInnovatorService.ArasModel.PublicModel;
using ArasInnovatorService.Common;

using Aras;
using Aras.IOM;

namespace ArasInnovatorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductManageService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ProductManageService.svc or ProductManageService.svc.cs at the Solution Explorer and start debugging.
    public class ProductManageService : IProductManageService
    {
        //----------------------------------------------------------------------------------------------------------------------------------------------------------
        HttpServerConnection m_Connection;
        Innovator m_Innovator;

        public void ArasServiceConfig(string pi_userName, string pi_pwd)
        {
            m_Connection = IomFactory.CreateHttpServerConnection(ConfigHelper.GetAPPConfigValue("InnovatorUrl"), ConfigHelper.GetAPPConfigValue("InnovatorDB"), pi_userName, Innovator.ScalcMD5(pi_pwd));
            m_Innovator = IomFactory.CreateInnovator(m_Connection);
        }

        /// <summary>
        /// Get Product List
        /// 2015-09-10 add by WesChen
        /// </summary>
        /// <param name="pi_selectionFilter">Selection Filter Parameter</param>
        /// <param name="pi_pageIndex">Return Data Page Index</param>
        /// <param name="pi_pageSize">Return Data Page Size</param>
        /// <returns></returns>
        public ProductClass GetProductList(SelectionFilter pi_selectionFilter, int pi_pageIndex, int pi_pageSize)
        {
            ProductClass l_returnClass = new ProductClass();
            l_returnClass.SuccessFlag = true;
            //pi_selectionFilter = SelectionFilter.CreateLeaf("ITEM_NUMBER", "EQ", "PYE-15DSFB001ET");
            try
            {
                #region connection and get new item
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyle_ItemName"), "get");
                #endregion

                #region page index and page size
                if (pi_pageIndex != null && pi_pageIndex > 0)
                {
                    l_getItem.setAttribute("page", pi_pageIndex.ToString());
                    l_returnClass.DisplayPageIndex = pi_pageIndex;
                }
                else
                {
                    pi_pageIndex = l_returnClass.DisplayPageIndex;
                    l_getItem.setAttribute("page", pi_pageIndex.ToString());                    
                }
                if (pi_pageSize != null && pi_pageSize > 0)
                {
                    l_getItem.setAttribute("pagesize", pi_pageSize.ToString());
                    l_returnClass.DisplayPageSize = pi_pageSize;
                }
                else
                {
                    pi_pageSize = l_returnClass.DisplayPageSize;
                    l_getItem.setAttribute("pagesize", pi_pageSize.ToString());                    
                }
                #endregion

                #region Parse Selection And Search
                Item l_returnItem = null;
                if (pi_selectionFilter != null)
                {
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(pi_selectionFilter, l_getItem);

                    #region Search AML

                    //get GarmentStyle Colorway List
                    Item l_getGarmentStyleColorway = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleGarmentColorway_ItemName"), "get");
                    l_getGarmentStyleColorway.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorWay_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleColorway);
                    //get GarmentStyle SizeRange List
                    Item l_getGarmentStyleSizeRange = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRange_ItemName"), "get");
                    Item l_getGarmentStyleSizeD1 = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRangeD1_ItemName"), "get");
                    Item l_getGarmentStyleSizeD2 = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRangeD2_ItemName"), "get");
                    l_getGarmentStyleSizeD1.addRelationship(l_getGarmentStyleSizeD2);
                    l_getGarmentStyleSizeRange.addRelationship(l_getGarmentStyleSizeD1);
                    l_searchItem.addRelationship(l_getGarmentStyleSizeRange);



                    //get Part Document List
                    Item l_getPartDocument = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartDocument_ItemName"), "get");                    
                    //get Part CAD List
                    Item l_getPartCAD = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartCAD_ItemName"), "get");                    
                    //get Part Size List
                    Item l_getPartSize = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"), "get");                    
                    //get part Content List
                    Item l_getPartContent = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"), "get");
                    
                    //----------------------------------------------------------------------------------------------
                    //get GarmentStyle BOM List
                    Item l_getGarmentStyleBOM = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleBOM_ItemName"), "get");
                    Item l_garmentStyleBOMlink = l_getGarmentStyleBOM.createRelatedItem(ConfigHelper.GetAPPConfigValue("GarmentBOM_ItemName"), "get");

                    Item l_getGarmentBOMPart = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentBOMPart_ItemName"), "get");
                    Item l_bomPartLink = l_getGarmentBOMPart.createRelatedItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                    Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    Item l_colorComboColorCodeLink = l_getPartColorCombo.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCombo_ItemName"), "get");

                    Item l_getColorComboColorCode = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName"), "get");
                    l_getColorComboColorCode.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCode_ItemName"), "get");

                    l_colorComboColorCodeLink.addRelationship(l_getColorComboColorCode);

                    l_bomPartLink.addRelationship(l_getPartColorCombo);
                    l_bomPartLink.addRelationship(l_getPartDocument);
                    l_bomPartLink.addRelationship(l_getPartCAD);
                    l_bomPartLink.addRelationship(l_getPartSize);
                    l_bomPartLink.addRelationship(l_getPartContent);

                    l_garmentStyleBOMlink.addRelationship(l_getGarmentBOMPart);
                    l_searchItem.addRelationship(l_getGarmentStyleBOM);
                    //----------------------------------------------------------------------------------------------

                    //get GarmentStyle Sketch List
                    Item l_getGarmentStyleSketch = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSketch_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleSketch);
                    //get GarmentStyle CAD List
                    Item l_getGarmentStyleCAD = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleCAD_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleCAD);
                    //get GarmentStyle Document List
                    Item l_getGarmentStyleDocument = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleDocument_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleDocument);
                    //get GarmentStyle YearSeason List
                    Item l_getGarmentStyleYearSeason = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleYearSeason_ItemName"), "get");
                    l_getGarmentStyleYearSeason.createRelatedItem(ConfigHelper.GetAPPConfigValue("YearSeason_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleYearSeason);
                    #endregion

                    l_returnItem = l_searchItem.apply();
                }
                else
                {
                    #region Search AML

                    //get GarmentStyle Colorway List
                    Item l_getGarmentStyleColorway = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleGarmentColorway_ItemName"), "get");                    
                    l_getGarmentStyleColorway.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorWay_ItemName"), "get");
                    l_getItem.addRelationship(l_getGarmentStyleColorway);
                    //get GarmentStyle SizeRange List
                    Item l_getGarmentStyleSizeRange = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRange_ItemName"), "get");
                    Item l_getGarmentStyleSizeD1 = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRangeD1_ItemName"), "get");
                    Item l_getGarmentStyleSizeD2 = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRangeD2_ItemName"), "get");
                    l_getGarmentStyleSizeD1.addRelationship(l_getGarmentStyleSizeD2);
                    l_getGarmentStyleSizeRange.addRelationship(l_getGarmentStyleSizeD1);
                    l_getItem.addRelationship(l_getGarmentStyleSizeRange);


                    //get Part Document List
                    Item l_getPartDocument = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartDocument_ItemName"), "get");
                    //get Part CAD List
                    Item l_getPartCAD = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartCAD_ItemName"), "get");
                    //get Part Size List
                    Item l_getPartSize = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"), "get");
                    //get part Content List
                    Item l_getPartContent = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"), "get");
                    //----------------------------------------------------------------------------------------------
                    //get GarmentStyle BOM List
                    Item l_getGarmentStyleBOM = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleBOM_ItemName"), "get");
                    Item l_garmentStyleBOMlink = l_getGarmentStyleBOM.createRelatedItem(ConfigHelper.GetAPPConfigValue("GarmentBOM_ItemName"), "get");

                    Item l_getGarmentBOMPart = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentBOMPart_ItemName"), "get");
                    Item l_bomPartLink = l_getGarmentBOMPart.createRelatedItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                    Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    Item l_colorComboColorCodeLink = l_getPartColorCombo.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCombo_ItemName"), "get");

                    Item l_getColorComboColorCode = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName"), "get");
                    l_getColorComboColorCode.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCode_ItemName"), "get");

                    l_colorComboColorCodeLink.addRelationship(l_getColorComboColorCode);

                    l_bomPartLink.addRelationship(l_getPartColorCombo);
                    l_bomPartLink.addRelationship(l_getPartDocument);
                    l_bomPartLink.addRelationship(l_getPartCAD);
                    l_bomPartLink.addRelationship(l_getPartSize);
                    l_bomPartLink.addRelationship(l_getPartContent);

                    l_garmentStyleBOMlink.addRelationship(l_getGarmentBOMPart);
                    l_getItem.addRelationship(l_getGarmentStyleBOM);
                    //----------------------------------------------------------------------------------------------

                    //get GarmentStyle Sketch List
                    Item l_getGarmentStyleSketch = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSketch_ItemName"), "get");
                    l_getItem.addRelationship(l_getGarmentStyleSketch);
                    //get GarmentStyle CAD List
                    Item l_getGarmentStyleCAD = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleCAD_ItemName"), "get");
                    l_getItem.addRelationship(l_getGarmentStyleCAD);
                    //get GarmentStyle Document List
                    Item l_getGarmentStyleDocument = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleDocument_ItemName"), "get");
                    l_getItem.addRelationship(l_getGarmentStyleDocument);
                    //get GarmentStyle YearSeason List
                    Item l_getGarmentStyleYearSeason = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleYearSeason_ItemName"), "get");
                    l_getGarmentStyleYearSeason.createRelatedItem(ConfigHelper.GetAPPConfigValue("YearSeason_ItemName"), "get");
                    l_getItem.addRelationship(l_getGarmentStyleYearSeason);
                    #endregion

                    l_returnItem = l_getItem.apply();
                }
                #endregion

                #region Check Return Item
                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetProductList, "2", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "GetProductList Error" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "GetProductList Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }
                if (l_returnItem.isEmpty())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetProductList, "2", "");
                    l_returnClass.ErrorString = "GetProductList Error: Return Is Empty";
                    l_returnClass.ErrorDetail = "GetProductList Error: Return Is Empty, Maybe Is Not Matching Data, Please Edit Parameter And Try Later.";

                    return l_returnClass;
                }
                if (l_returnItem.getItemCount() == 0)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetProductList, "2", "");
                    l_returnClass.ErrorString = "GetProductList Error: Return Is Empty";
                    l_returnClass.ErrorDetail = "GetProductList Error: Return Is Empty, Maybe Is Not Matching Data, Please Edit Parameter And Try Later.";

                    return l_returnClass;
                }

                #endregion

                #region To List

                if (l_returnItem.getItemCount() > 0)
                {
                    Product[] l_getProduct = new Product[l_returnItem.getItemCount()];

                    //get item and set value
                    for (int i = 0; i < l_returnItem.getItemCount(); i++)
                    {
                        Item l_getTempItem = l_returnItem.getItemByIndex(i);
                                               
                        #region product base

                        //new product info 
                        ProductBaseType l_productinfo = new ProductBaseType();
                        
                        l_productinfo.ProductType = l_getTempItem.getProperty("cn_class0", "");
                        l_productinfo.ProductCategory = l_getTempItem.getProperty("cn_class1", "");
                        l_productinfo.ProductSubCategory = l_getTempItem.getProperty("cn_class2", "");
                        
                        l_productinfo.MaterialStatus = "";//*                        
                        l_productinfo.Season = "";//*
                        l_productinfo.Year = "";//*

                        ProductClass l_tempProductImageClass = GetProductImage(l_getTempItem.getProperty("item_number", ""), eumImageType.UsePrimaryPath);
                        if (l_tempProductImageClass != null)
                        {
                            l_productinfo.Image = new URIDocumentsType() { DocumentURI = new string[] { l_tempProductImageClass.GetReturnString } };
                        }

                        #region productfeatures

                        ProductFeaturesType l_productfeatures = new ProductFeaturesType();
                        l_productfeatures.Gender = l_getTempItem.getProperty("cn_gender", "");
                        l_productfeatures.Collection = l_getTempItem.getProperty("cn_collection", "");
                        l_productfeatures.Series = l_getTempItem.getProperty("cn_series", "");
                        l_productfeatures.Making = l_getTempItem.getProperty("cn_making", "");
                        l_productfeatures.Fit = l_getTempItem.getProperty("cn_fit", "");
                        l_productfeatures.Line = l_getTempItem.getProperty("cn_line", "");
                        l_productfeatures.Collar = l_getTempItem.getProperty("cn_collar", "");
                        l_productfeatures.Placket = l_getTempItem.getProperty("cn_placket", "");
                        l_productfeatures.Sleeve = l_getTempItem.getProperty("cn_sleeve", "");
                        l_productfeatures.Cuff = l_getTempItem.getProperty("cn_cuff", "");
                        l_productfeatures.Pocket = l_getTempItem.getProperty("cn_pocket", "");
                        l_productfeatures.Washing = l_getTempItem.getProperty("cn_washing", "");
                        l_productfeatures.Embroidery = l_getTempItem.getProperty("cn_emb", "");
                        l_productfeatures.Printing = l_getTempItem.getProperty("cn_prt", "");
                        l_productfeatures.BodyPattern = l_getTempItem.getProperty("cn_body_pattern", "");
                        l_productfeatures.SAH = float.Parse(l_getTempItem.getProperty("cn_sah", "0"));
                        l_productfeatures.Remarks = l_getTempItem.getProperty("cn_remark", "");

                        #endregion
                        l_productinfo.ProductFeatures = l_productfeatures;

                        #region yearseason

                        Item l_getYearSeasonItem = l_getTempItem.getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleYearSeason_ItemName"));
                        if (l_getYearSeasonItem != null)
                        {
                            if (!l_getYearSeasonItem.isEmpty() && !l_getYearSeasonItem.isError())
                            {
                                YearSeasonBaseType[] l_yearSeasonArray = new YearSeasonBaseType[l_getYearSeasonItem.getItemCount()];
                                for (int j = 0; j < l_yearSeasonArray.Length; j++)
                                {
                                    Item l_getYearSeasonItem2 = l_getYearSeasonItem.getItemByIndex(j).getRelatedItem();
                                    if (!l_getYearSeasonItem2.isEmpty() && !l_getYearSeasonItem2.isError())
                                    {
                                        YearSeasonBaseType l_yearSeason = new YearSeasonBaseType();
                                        SeasonEnum l_seasonEnum = new SeasonEnum();

                                        l_seasonEnum.Code = l_getYearSeasonItem2.getProperty("cn_season", "");
                                        l_seasonEnum.SeasonName = l_getYearSeasonItem2.getProperty("cn_season", "");

                                        l_yearSeason.Season = l_seasonEnum;
                                        l_yearSeason.Year = l_getYearSeasonItem2.getProperty("cn_year", "");

                                        l_yearSeasonArray[j] = l_yearSeason;
                                    }
                                }
                                l_productfeatures.YearSeason = l_yearSeasonArray;
                            }

                        }
                        #endregion
                                                                       

                        #region finalsample
                        ProductBaseTypeFinalDevelopmentSample l_finalsample = new ProductBaseTypeFinalDevelopmentSample();
                        l_finalsample.Number = l_getTempItem.getProperty("cn_fds_no", "");
                        #endregion
                        l_productinfo.FinalDevelopmentSample = l_finalsample;                        

                        #region style
                        ProductBaseTypeStyle l_producttype = new ProductBaseTypeStyle();
                        l_producttype.Description = l_getTempItem.getProperty("cn_sleeve", "") + " " + l_getTempItem.getProperty("cn_collar", "") + " " + l_getTempItem.getProperty("cn_fit", "") + " " + l_getTempItem.getProperty("cn_body_pattern", "") + " " + l_getTempItem.getProperty("cn_collection", "") + "Shirt";//*
                        l_producttype.ID = l_getTempItem.getProperty("item_number","");//*
                        l_producttype.Version = l_getTempItem.getProperty("jajor_rev", "");//*
                        #endregion
                        l_productinfo.Style = l_producttype;

                        CustomerBaseType[] l_customerinfoarray = new CustomerBaseType[1];
                        CustomerBaseType l_customerinfo = new CustomerBaseType();
                        l_customerinfo.CustomerCode = "";
                        l_customerinfo.CustomerName = "";
                        l_customerinfo.BrandCode = "";
                        l_customerinfo.BrandName = "";

                        #region search customer
                        string l_customerID = l_getTempItem.getProperty("cn_cust_code");
                        if (!string.IsNullOrEmpty(l_customerID))
                        {
                            Item l_customerItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Customer_ItemName"), l_customerID);
                            if (!l_customerItem.isError() && !l_customerItem.isEmpty())
                            {
                                l_customerinfo.CustomerCode = l_customerItem.getProperty("keyed_name", "");
                                l_customerinfo.CustomerName = l_customerItem.getProperty("name", "");
                            }
                        }
                        
                        #endregion

                        #region search brand
                        string l_brandID = l_getTempItem.getProperty("cn_brand_code");
                        if (!string.IsNullOrEmpty(l_brandID))
                        {
                            Item l_brandItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("CustomerBrand_ItemName"), l_brandID);
                            {
                                if (!l_brandItem.isError() && !l_brandItem.isEmpty())
                                {
                                    l_customerinfo.BrandCode = l_brandItem.getProperty("keyed_name", "");
                                    l_customerinfo.BrandName = l_brandItem.getProperty("cn_brand", "");
                                }
                            }
                        }
                        #endregion

                        l_customerinfoarray[0] = l_customerinfo;
                        l_productinfo.CustomerInfo = l_customerinfoarray;

                        #endregion                                                

                        #region garmentBOM

                        GarmentBOMItemBaseType l_garmentbom = new GarmentBOMItemBaseType();

                        #region 1.colorway
                        Item l_getColorWayItem = l_getTempItem.getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleGarmentColorway_ItemName"));
                        
                        if (l_getColorWayItem != null)
                        {
                            if (!l_getColorWayItem.isError() && !l_getColorWayItem.isEmpty())
                            {

                                ColorwayBaseType[] l_colorWayArray = new ColorwayBaseType[l_getColorWayItem.getItemCount()];
                                for (int k = 0; k < l_colorWayArray.Length; k++)
                                {
                                    Item l_getColorWayItemRelated = l_getColorWayItem.getItemByIndex(k).getRelatedItem();
                                    if (l_getColorWayItemRelated != null)
                                    {
                                        if (!l_getColorWayItemRelated.isEmpty() && l_getColorWayItemRelated.isError())
                                        {
                                            ColorwayBaseType l_colorway = new ColorwayBaseType();
                                            l_colorway.BodyPattern = new string[] { l_getColorWayItemRelated.getProperty("cn_body_pattern", "") };
                                            l_colorway.Code = l_getColorWayItemRelated.getProperty("cn_cw_code", "");
                                            l_colorway.ColorwayName = l_getColorWayItemRelated.getProperty("cn_colorway", "");
                                            l_colorway.CustomerReferenceCode = l_getColorWayItemRelated.getProperty("cn_cust_cwcode", "");
                                            l_colorway.CustomerReferenceName = l_getColorWayItemRelated.getProperty("cn_cust_cw", "");
                                            l_colorway.DocumentURIs = null;
                                            l_colorway.PLU = l_getColorWayItemRelated.getProperty("cn_plu", "");

                                            l_colorWayArray[k] = l_colorway;
                                        }                                        
                                    }
                                }
                                l_garmentbom.Colorway = l_colorWayArray;
                            }
                        }
                        #endregion
                        

                        #region 2.productsize
                        Item l_getProductSizeItem = l_getTempItem.getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRange_ItemName"));
                        if (l_getProductSizeItem != null)
                        {
                            if (!l_getProductSizeItem.isEmpty() && !l_getProductSizeItem.isError())
                            {
                                GarmentBOMItemBaseTypeProductSizes[] l_productSizeArray = new GarmentBOMItemBaseTypeProductSizes[l_getProductSizeItem.getItemCount()];

                                for (int l = 0; l < l_getProductSizeItem.getItemCount(); l++)
                                {
                                    GarmentBOMItemBaseTypeProductSizes l_productSize = new GarmentBOMItemBaseTypeProductSizes();
                                    List<string> l_getsize = new List<string>();

                                    Item l_getProductSizeD1Item = l_getProductSizeItem.getItemByIndex(l).getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRangeD1_ItemName"));
                                    for (int l_size = 0; l_size < l_getProductSizeD1Item.getItemCount(); l_size++)
                                    {
                                        Item l_getProductSizeD2Item = l_getProductSizeD1Item.getItemByIndex(l_size).getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRangeD2_ItemName"));
                                        for (int l_size2 = 0; l_size2 < l_getProductSizeD2Item.getItemCount(); l_size2++)
                                        {
                                            l_getsize.Add(l_getProductSizeD2Item.getItemByIndex(l_size2).getProperty("cn_size2", ""));
                                        }
                                    }
                                    string[] l_getproductsizeArray = new string[l_getsize.Count];
                                    for (int i_arrayIndex = 0; i_arrayIndex < l_getsize.Count; i_arrayIndex++)
                                    {
                                        l_getproductsizeArray[i_arrayIndex] = l_getsize[i_arrayIndex];

                                    }
                                    l_productSize.ProductSize = l_getproductsizeArray;
                                    l_productSizeArray[l] = l_productSize;
                                }
                                l_garmentbom.ProductSizes = l_productSizeArray;
                            }
                        }
                                                                 
                        #endregion
                        

                        #region getPart,because fabric part and trim part with together, so get part to slit

                        List<FabricPartBaseType> l_getFabricPartList = new List<FabricPartBaseType>();
                        List<TrimPartBaseType> l_getTrimPartList = new List<TrimPartBaseType>();
                        
                        Item l_getGarmentStyleBOMItem = l_getTempItem.getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleBOM_ItemName"));
                        if (l_getGarmentStyleBOMItem != null)
                        {
                            if (!l_getGarmentStyleBOMItem.isError() && !l_getGarmentStyleBOMItem.isEmpty())
                            {
                                for (int i_bomIndex = 0; i_bomIndex < l_getGarmentStyleBOMItem.getItemCount(); i_bomIndex++)
                                {
                                    Item l_temp1 = l_getGarmentStyleBOMItem.getItemByIndex(i_bomIndex);
                                    if (l_temp1 != null)
                                    {
                                        #region temp1
                                        Item l_temp2 = l_getGarmentStyleBOMItem.getItemByIndex(i_bomIndex).getRelatedItem();
                                        if (l_temp2 != null)
                                        {
                                            #region temp2
                                            Item l_getGarmentBOMPartItem = l_getGarmentStyleBOMItem.getItemByIndex(i_bomIndex).getRelatedItem().getRelationships(ConfigHelper.GetAPPConfigValue("GarmentBOMPart_ItemName"));
                                            if (l_getGarmentBOMPartItem != null)
                                            {
                                                if (!l_getGarmentBOMPartItem.isEmpty() && !l_getGarmentBOMPartItem.isError())
                                                {
                                                    for (int i_bomPartIndex = 0; i_bomPartIndex < l_getGarmentBOMPartItem.getItemCount(); i_bomPartIndex++)
                                                    {
                                                        Item l_getPartItem = l_getGarmentBOMPartItem.getItemByIndex(i_bomPartIndex).getRelatedItem();
                                                        if (l_getPartItem != null)
                                                        {
                                                            if (!l_getPartItem.isEmpty() && !l_getPartItem.isError())
                                                            {
                                                                string l_getPartType = l_getPartItem.getProperty("cn_class0", "");
                                                                if (l_getPartType == "FAB")
                                                                {
                                                                    //l_getFabricPartList.Add(l_getPartItem.getItemByIndex(0));

                                                                    #region 3.fabricpart

                                                                    FabricPartBaseType l_tempFabricPartBaseType = new FabricPartBaseType();

                                                                    l_tempFabricPartBaseType.Description = l_getPartItem.getProperty("description", "");

                                                                    #region Content
                                                                    Item l_getPartContentItem = l_getPartItem.getRelationships(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"));
                                                                    if (l_getPartContentItem != null)
                                                                    {
                                                                        if (!l_getPartContentItem.isError() && !l_getPartContentItem.isEmpty())
                                                                        {
                                                                            FabricContentBaseType[] l_tempFabricContentBaseTypeArray = new FabricContentBaseType[l_getPartContentItem.getItemCount()];

                                                                            for (int i_getPartContentIndex = 0; i_getPartContentIndex < l_getPartContentItem.getItemCount(); i_getPartContentIndex++)
                                                                            {
                                                                                FabricContentBaseType l_tempFabricContentBaseType = new FabricContentBaseType();
                                                                                l_tempFabricContentBaseType.Description = l_getPartContentItem.getItemByIndex(i_getPartContentIndex).getProperty("cn_content", "");
                                                                                l_tempFabricContentBaseType.Ratio = float.Parse(l_getPartContentItem.getItemByIndex(i_getPartContentIndex).getProperty("cn_content_ratio", "0"));

                                                                                l_tempFabricContentBaseTypeArray[i_getPartContentIndex] = l_tempFabricContentBaseType;
                                                                            }
                                                                            l_tempFabricPartBaseType.Content = l_tempFabricContentBaseTypeArray;
                                                                        }
                                                                    }

                                                                    #endregion

                                                                    #region base info
                                                                    l_tempFabricPartBaseType.YarnCount = l_getPartItem.getProperty("cn_yarn_count", "");
                                                                    l_tempFabricPartBaseType.Construction = l_getPartItem.getProperty("cn_construction", "");
                                                                    l_tempFabricPartBaseType.DyeMethod = l_getPartItem.getProperty("cn_dye_method", "");
                                                                    l_tempFabricPartBaseType.Finishing = l_getPartItem.getProperty("cn_finishing", "");
                                                                    l_tempFabricPartBaseType.Width = l_getPartItem.getProperty("cn_fabric_width", "");
                                                                    l_tempFabricPartBaseType.Pattern = l_getPartItem.getProperty("cn_fabric_pattern", "");
                                                                    l_tempFabricPartBaseType.Printing = l_getPartItem.getProperty("cn_printing", "");
                                                                    l_tempFabricPartBaseType.RepeatVertical = l_getPartItem.getProperty("cn_rep_v", "");
                                                                    l_tempFabricPartBaseType.RepeatHorizontal = l_getPartItem.getProperty("cn_rep_h", "");
                                                                    l_tempFabricPartBaseType.TestingGrade = l_getPartItem.getProperty("cn_fab_test_grading", "");
                                                                    l_tempFabricPartBaseType.Remarks = l_getPartItem.getProperty("cn_remark", "");
                                                                    #endregion

                                                                    #region Header
                                                                    //header
                                                                    FabricPartHeaderType l_tempFabricPartHeaderType = new FabricPartHeaderType();
                                                                    l_tempFabricPartHeaderType.Code = l_getPartItem.getProperty("cn_ppo_agpo", "");
                                                                    l_tempFabricPartHeaderType.Cateogry = l_getPartItem.getProperty("cn_class1", "");
                                                                    l_tempFabricPartHeaderType.WovenFabrication = l_getPartItem.getProperty("cn_class2", "");
                                                                    l_tempFabricPartHeaderType.KnitFabrication = "";
                                                                    l_tempFabricPartHeaderType.Version = l_getPartItem.getProperty("maj_rev", "");
                                                                    l_tempFabricPartHeaderType.Status = l_getPartItem.getProperty("state", "");

                                                                    #region Header Set CustomerInfo
                                                                    CustomerBaseType[] l_tempCustomerBaseTypeArray = new CustomerBaseType[1];
                                                                    CustomerBaseType l_tempCustomerBaseType = new CustomerBaseType();

                                                                    l_tempCustomerBaseType.CustomerCode = "";
                                                                    l_tempCustomerBaseType.CustomerName = "";
                                                                    l_tempCustomerBaseType.CustomerReferenceNum = "";
                                                                    l_tempCustomerBaseType.BrandCode = "";
                                                                    l_tempCustomerBaseType.BrandName = "";

                                                                    //customer
                                                                    string l_partCustomerID = l_getPartItem.getProperty("cn_cust_code", "");
                                                                    if (!string.IsNullOrEmpty(l_partCustomerID))
                                                                    {
                                                                        Item l_getPartCustomerItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Customer_ItemName"), l_partCustomerID);
                                                                        if (!l_getPartCustomerItem.isError() && !l_getPartCustomerItem.isEmpty())
                                                                        {
                                                                            l_tempCustomerBaseType.CustomerCode = l_getPartCustomerItem.getProperty("keyed_name", "");
                                                                            l_tempCustomerBaseType.CustomerName = l_getPartCustomerItem.getProperty("name", "");
                                                                            l_tempCustomerBaseType.CustomerReferenceNum = l_getPartCustomerItem.getProperty("cn_cust_item_id", "");
                                                                        }
                                                                    }
                                                                    //brand
                                                                    string l_partBrandID = l_getPartItem.getProperty("cn_brand_code", "");
                                                                    if (!string.IsNullOrEmpty(l_partBrandID))
                                                                    {
                                                                        Item l_getPartBrandItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("CustomerBrand_ItemName"), l_partBrandID);
                                                                        if (!l_getPartBrandItem.isError() && !l_getPartBrandItem.isEmpty())
                                                                        {
                                                                            l_tempCustomerBaseType.BrandCode = l_getPartBrandItem.getProperty("keyed_name", "");
                                                                            l_tempCustomerBaseType.BrandName = l_getPartBrandItem.getProperty("name", "");
                                                                        }
                                                                    }

                                                                    l_tempCustomerBaseTypeArray[0] = l_tempCustomerBaseType;
                                                                    //header set customer info
                                                                    l_tempFabricPartHeaderType.CustomerInfo = l_tempCustomerBaseTypeArray;
                                                                    #endregion
                                                                    #endregion
                                                                    l_tempFabricPartBaseType.FabricHeader = l_tempFabricPartHeaderType;

                                                                    #region SupplierBaseType
                                                                    SupplierBaseType[] l_tempSupplierBaseTypeArray = new SupplierBaseType[1];
                                                                    SupplierBaseType l_tempSupplierBaseType = new SupplierBaseType();
                                                                    string l_getPartSupplierID = l_getPartItem.getProperty("cn_supplier_code", "");
                                                                    if (!string.IsNullOrEmpty(l_getPartSupplierID))
                                                                    {
                                                                        Item l_getSupplierItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Supplier_ItemName"), l_getPartSupplierID);

                                                                        l_tempSupplierBaseType.Code = l_getPartItem.getProperty("cn_supplier_code");
                                                                        l_tempSupplierBaseType.ItemCode = l_getPartItem.getProperty("cn_supplier_item_code");
                                                                        l_tempSupplierBaseType.SupplierName = l_getPartItem.getProperty("cn_class1");
                                                                    }
                                                                    l_tempSupplierBaseTypeArray[0] = l_tempSupplierBaseType;
                                                                    #endregion
                                                                    l_tempFabricPartBaseType.Supplier = l_tempSupplierBaseTypeArray;


                                                                    //color combo
                                                                    l_tempFabricPartBaseType.ColorCombo = null;

                                                                    //fabric shrinkage
                                                                    l_tempFabricPartBaseType.FabricShrinkage = null;

                                                                    //garment shrinkage
                                                                    l_tempFabricPartBaseType.GarmentShrinkage = null;

                                                                    //price
                                                                    PriceBaseType[] l_tempPriceBaseTypeArray = new PriceBaseType[1];
                                                                    PriceBaseType l_tempPriceBaseType = new PriceBaseType();
                                                                    l_tempPriceBaseType.Amount = Convert.ToDecimal(l_getPartItem.getProperty("cn_remark", "0"));
                                                                    l_tempPriceBaseType.Currency = "";//*
                                                                    l_tempPriceBaseTypeArray[0] = l_tempPriceBaseType;
                                                                    l_tempFabricPartBaseType.Price = l_tempPriceBaseTypeArray;

                                                                    //year season                            
                                                                    l_tempFabricPartBaseType.YearSeason = null;

                                                                    //PartSize
                                                                    Item l_getPartSizeItem = l_getPartItem.getRelationships(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"));
                                                                    if (l_getPartSizeItem != null)
                                                                    {
                                                                        if (!l_getPartSizeItem.isEmpty() && !l_getPartSizeItem.isError())
                                                                        {
                                                                            FabricSizeBaseType[] l_tempFabricSizeBaseTypeArray = new FabricSizeBaseType[l_getPartSizeItem.getItemCount()];
                                                                            for (int i_partSizeIndex = 0; i_partSizeIndex < l_getPartSizeItem.getItemCount(); i_partSizeIndex++)
                                                                            {
                                                                                FabricSizeBaseType l_tempFabricSizeBaseType = new FabricSizeBaseType();
                                                                                l_tempFabricSizeBaseType.Dimension = l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size_desc", "");
                                                                                l_tempFabricSizeBaseType.Measurement = 0; //l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size", "");//*
                                                                                l_tempFabricSizeBaseTypeArray[i_partSizeIndex] = l_tempFabricSizeBaseType;
                                                                            }
                                                                            l_tempFabricPartBaseType.Size = l_tempFabricSizeBaseTypeArray;
                                                                        }
                                                                    }

                                                                    //loyout                                           



                                                                    #endregion

                                                                    l_getFabricPartList.Add(l_tempFabricPartBaseType);

                                                                }
                                                                else if (l_getPartType == "TRM")
                                                                {
                                                                    //l_getTrimPartList.Add(l_getPartItem.getItemByIndex(0));

                                                                    #region 4.trimpart

                                                                    TrimPartBaseType l_tempTrimPartBaseType = new TrimPartBaseType();


                                                                    l_tempTrimPartBaseType.Description = l_getPartItem.getProperty("description", "");



                                                                    #region base info
                                                                    l_tempTrimPartBaseType.Remarks = l_getPartItem.getProperty("cn_remark", "");
                                                                    #endregion

                                                                    #region Header
                                                                    //header
                                                                    TrimPartHeaderType l_tempTrimPartHeaderType = new TrimPartHeaderType();
                                                                    l_tempTrimPartHeaderType.Code = l_getPartItem.getProperty("item_number", "");

                                                                    TrimCategoryBaseType l_tempTrimCategoryBaseType = new TrimCategoryBaseType();
                                                                    l_tempTrimCategoryBaseType.Code = l_getPartItem.getProperty("cn_class1", "");
                                                                    l_tempTrimCategoryBaseType.Description = l_getPartItem.getProperty("description", "");
                                                                    l_tempTrimPartHeaderType.Cateogry = l_tempTrimCategoryBaseType;

                                                                    l_tempTrimPartHeaderType.SubCategory = l_getPartItem.getProperty("cn_class2", "");
                                                                    l_tempTrimPartHeaderType.Version = l_getPartItem.getProperty("maj_rev", "");
                                                                    l_tempTrimPartHeaderType.Status = l_getPartItem.getProperty("state", "");

                                                                    #region Header Set CustomerInfo
                                                                    CustomerBaseType[] l_tempCustomerBaseTypeArray = new CustomerBaseType[1];
                                                                    CustomerBaseType l_tempCustomerBaseType = new CustomerBaseType();

                                                                    l_tempCustomerBaseType.CustomerCode = "";
                                                                    l_tempCustomerBaseType.CustomerName = "";
                                                                    l_tempCustomerBaseType.CustomerReferenceNum = "";
                                                                    l_tempCustomerBaseType.BrandCode = "";
                                                                    l_tempCustomerBaseType.BrandName = "";

                                                                    //customer
                                                                    string l_partCustomerID = l_getPartItem.getProperty("cn_cust_code", "");
                                                                    if (!string.IsNullOrEmpty(l_partCustomerID))
                                                                    {
                                                                        Item l_getPartCustomerItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Customer_ItemName"), l_partCustomerID);
                                                                        if (!l_getPartCustomerItem.isError() && !l_getPartCustomerItem.isEmpty())
                                                                        {
                                                                            l_tempCustomerBaseType.CustomerCode = l_getPartCustomerItem.getProperty("keyed_name", "");
                                                                            l_tempCustomerBaseType.CustomerName = l_getPartCustomerItem.getProperty("name", "");
                                                                            l_tempCustomerBaseType.CustomerReferenceNum = l_getPartCustomerItem.getProperty("cn_cust_item_id", "");
                                                                        }
                                                                    }
                                                                    //brand
                                                                    string l_partBrandID = l_getPartItem.getProperty("cn_brand_code", "");
                                                                    if (!string.IsNullOrEmpty(l_partBrandID))
                                                                    {
                                                                        Item l_getPartBrandItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("CustomerBrand_ItemName"), l_partBrandID);
                                                                        if (!l_getPartBrandItem.isError() && !l_getPartBrandItem.isEmpty())
                                                                        {
                                                                            l_tempCustomerBaseType.BrandCode = l_getPartBrandItem.getProperty("keyed_name", "");
                                                                            l_tempCustomerBaseType.BrandName = l_getPartBrandItem.getProperty("name", "");
                                                                        }
                                                                    }

                                                                    l_tempCustomerBaseTypeArray[0] = l_tempCustomerBaseType;
                                                                    //header set customer info
                                                                    l_tempTrimPartHeaderType.CustomerInfo = l_tempCustomerBaseTypeArray;
                                                                    #endregion

                                                                    #endregion
                                                                    l_tempTrimPartBaseType.TrimHeader = l_tempTrimPartHeaderType;

                                                                    #region SupplierBaseType
                                                                    SupplierBaseType[] l_tempSupplierBaseTypeArray = new SupplierBaseType[1];
                                                                    SupplierBaseType l_tempSupplierBaseType = new SupplierBaseType();
                                                                    string l_getPartSupplierID = l_getPartItem.getProperty("cn_supplier_code", "");
                                                                    if (!string.IsNullOrEmpty(l_getPartSupplierID))
                                                                    {
                                                                        Item l_getSupplierItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Supplier_ItemName"), l_getPartSupplierID);

                                                                        l_tempSupplierBaseType.Code = l_getPartItem.getProperty("cn_supplier_code", "");
                                                                        l_tempSupplierBaseType.ItemCode = l_getPartItem.getProperty("cn_supplier_item_code", "");
                                                                        l_tempSupplierBaseType.SupplierName = l_getPartItem.getProperty("cn_class1", "");
                                                                    }
                                                                    l_tempSupplierBaseTypeArray[0] = l_tempSupplierBaseType;
                                                                    #endregion
                                                                    l_tempTrimPartBaseType.Supplier = l_tempSupplierBaseTypeArray;


                                                                    //AGPO
                                                                    l_tempTrimPartBaseType.AGPONum = new string[] { l_getPartItem.getProperty("cn_ppo_agpo", "") };

                                                                    //color
                                                                    l_tempTrimPartBaseType.Color = null;

                                                                    l_tempTrimPartBaseType.Material = "";


                                                                    //year season                            
                                                                    l_tempTrimPartBaseType.YearSeason = null;

                                                                    //PartSize
                                                                    Item l_getPartSizeItem = l_getPartItem.getRelationships(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"));
                                                                    if (l_getPartSizeItem != null)
                                                                    {
                                                                        if (!l_getPartSizeItem.isEmpty() && !l_getPartSizeItem.isError())
                                                                        {
                                                                            SizeBaseType[] l_tempSizeBaseTypeArray = new SizeBaseType[l_getPartSizeItem.getItemCount()];
                                                                            for (int i_partSizeIndex = 0; i_partSizeIndex < l_getPartSizeItem.getItemCount(); i_partSizeIndex++)
                                                                            {
                                                                                SizeBaseType l_tempSizeBaseType = new SizeBaseType();
                                                                                l_tempSizeBaseType.Dimension = l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size_desc", "");
                                                                                l_tempSizeBaseType.Measurement = 0; //l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size", "");//*
                                                                                l_tempSizeBaseTypeArray[i_partSizeIndex] = l_tempSizeBaseType;
                                                                            }
                                                                            l_tempTrimPartBaseType.Size = l_tempSizeBaseTypeArray;
                                                                        }
                                                                    }


                                                                    #endregion

                                                                    l_getTrimPartList.Add(l_tempTrimPartBaseType);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }

                        #endregion

                        #region process fabirc
                        FabricBOMItemBaseType l_FabricBOMItemBaseType = new FabricBOMItemBaseType();
                        l_FabricBOMItemBaseType.ComboName = "";
                        l_FabricBOMItemBaseType.CutWay = "";
                        l_FabricBOMItemBaseType.FabricCode = "";

                        l_FabricBOMItemBaseType.FabricWidth = "";
                        l_FabricBOMItemBaseType.GarmentComponent = "";
                        l_FabricBOMItemBaseType.PieceFabricSize = "";
                        l_FabricBOMItemBaseType.PIeceFabricUsage = 0;
                        l_FabricBOMItemBaseType.YPD = 0;
                        //l_garmentbom.FabricBOM = l_fabricPartArray;

                        FabricBOMItemBaseTypeFabricParts l_FabricBOMItemBaseTypeFabricParts = new FabricBOMItemBaseTypeFabricParts();
                        if (l_getFabricPartList.Count > 0)
                        {
                            FabricPartBaseType[] l_FabricPartBaseTypeArray = new FabricPartBaseType[l_getFabricPartList.Count];
                            for (int i_fabricpartindex = 0; i_fabricpartindex < l_getFabricPartList.Count; i_fabricpartindex++)
                            {
                                l_FabricPartBaseTypeArray[i_fabricpartindex] = l_getFabricPartList[i_fabricpartindex];
                            }
                            l_FabricBOMItemBaseTypeFabricParts.FabricPart = l_FabricPartBaseTypeArray;
                        }
                        #endregion
                        l_FabricBOMItemBaseType.FabricParts = l_FabricBOMItemBaseTypeFabricParts;
                        l_garmentbom.FabricBOM = new FabricBOMItemBaseType[] { l_FabricBOMItemBaseType };

                        #region process trim
                        TrimBOMItemBaseType l_TrimBOMItemBaseType = new TrimBOMItemBaseType();
                        l_TrimBOMItemBaseType.TrimCode = l_getTempItem.getProperty("item_number", "");
                        l_TrimBOMItemBaseType.TrimColor = null;
                        l_TrimBOMItemBaseType.TrimSize = "";


                        TrimBOMItemBaseTypeTrimParts l_TrimBOMItemBaseTypeTrimParts = new TrimBOMItemBaseTypeTrimParts();
                        if(l_getTrimPartList.Count>0)
                        {
                            TrimPartBaseType[] l_TrimPartBaseTypeArray = new TrimPartBaseType[l_getTrimPartList.Count];
                            for (int i_trimpartindex = 0; i_trimpartindex < l_getTrimPartList.Count; i_trimpartindex++)
                            {
                                l_TrimPartBaseTypeArray[i_trimpartindex] = l_getTrimPartList[i_trimpartindex];
                            }
                            l_TrimBOMItemBaseTypeTrimParts.TrimPart = l_TrimPartBaseTypeArray;
                        }
                        #endregion
                        l_TrimBOMItemBaseType.TrimParts = l_TrimBOMItemBaseTypeTrimParts;
                        l_garmentbom.TrimBOM = new TrimBOMItemBaseType[] { l_TrimBOMItemBaseType };

                        #endregion

                        //new product
                        Product l_product = new Product();
                        l_product.ProductInfo = l_productinfo;
                        l_product.BOM = l_garmentbom;

                        //set product
                        l_getProduct[i] = l_product;
                    }

                    //new product is product array
                    Products l_products = new Products();
                    l_products.Product = l_getProduct;

                    //return class product is products
                    l_returnClass.ProductList = l_products;
                }

                #endregion

                #region login out
                m_Connection.Logout();
                #endregion

                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "GetProductList Error:" + ex.ToString());

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetProductList, "2", "");
                l_returnClass.ErrorString = "GetProductList Error";
                l_returnClass.ErrorDetail = "GetProductList Error:" + ex.Message;

                return l_returnClass;
            }

        }

        /// <summary>
        /// Get Product By ID
        /// 2015-09-10 add by WesChen
        /// </summary>
        /// <param name="pi_productID">Get Product ID(Code)</param>
        /// <returns></returns>
        public ProductClass GetProductByID(string pi_productID)
        {
            ProductClass l_returnClass = new ProductClass();
            l_returnClass.SuccessFlag = true;

            try
            {
                #region connection and get new item
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyle_ItemName"), "get");
                #endregion

                #region page index and page size
                l_getItem.setAttribute("page", "1");
                l_returnClass.DisplayPageIndex = 1;
                l_getItem.setAttribute("pagesize", "1");
                l_returnClass.DisplayPageSize = 1;
                #endregion

                #region Parse Selection And Search
                Item l_returnItem = null;
                if (!string.IsNullOrEmpty(pi_productID))
                {
                    SelectionFilter l_newSelectionFilter = SelectionFilter.CreateLeaf("ITEM_NUMBER", "EQ", pi_productID.Trim());
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(l_newSelectionFilter, l_getItem);

                    #region Search AML

                    //get GarmentStyle Colorway List
                    Item l_getGarmentStyleColorway = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleGarmentColorway_ItemName"), "get");
                    l_getGarmentStyleColorway.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorWay_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleColorway);
                    //get GarmentStyle SizeRange List
                    Item l_getGarmentStyleSizeRange = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRange_ItemName"), "get");
                    Item l_getGarmentStyleSizeD1 = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRangeD1_ItemName"), "get");
                    Item l_getGarmentStyleSizeD2 = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRangeD2_ItemName"), "get");
                    l_getGarmentStyleSizeD1.addRelationship(l_getGarmentStyleSizeD2);
                    l_getGarmentStyleSizeRange.addRelationship(l_getGarmentStyleSizeD1);
                    l_searchItem.addRelationship(l_getGarmentStyleSizeRange);


                    //get Part Document List
                    Item l_getPartDocument = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartDocument_ItemName"), "get");
                    //get Part CAD List
                    Item l_getPartCAD = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartCAD_ItemName"), "get");
                    //get Part Size List
                    Item l_getPartSize = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"), "get");
                    //get part Content List
                    Item l_getPartContent = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"), "get");
                    //----------------------------------------------------------------------------------------------
                    //get GarmentStyle BOM List
                    Item l_getGarmentStyleBOM = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleBOM_ItemName"), "get");
                    Item l_garmentStyleBOMlink = l_getGarmentStyleBOM.createRelatedItem(ConfigHelper.GetAPPConfigValue("GarmentBOM_ItemName"), "get");

                    Item l_getGarmentBOMPart = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentBOMPart_ItemName"), "get");
                    Item l_bomPartLink = l_getGarmentBOMPart.createRelatedItem(ConfigHelper.GetAPPConfigValue("Part_ItemName"), "get");

                    Item l_getPartColorCombo = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("PartColorCombo_ItemName"), "get");
                    Item l_colorComboColorCodeLink = l_getPartColorCombo.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCombo_ItemName"), "get");

                    Item l_getColorComboColorCode = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("ColorComboColorCode_ItemName"), "get");
                    l_getColorComboColorCode.createRelatedItem(ConfigHelper.GetAPPConfigValue("ColorCode_ItemName"), "get");

                    l_colorComboColorCodeLink.addRelationship(l_getColorComboColorCode);

                    l_bomPartLink.addRelationship(l_getPartColorCombo);
                    l_bomPartLink.addRelationship(l_getPartDocument);
                    l_bomPartLink.addRelationship(l_getPartCAD);
                    l_bomPartLink.addRelationship(l_getPartSize);
                    l_bomPartLink.addRelationship(l_getPartContent);

                    l_garmentStyleBOMlink.addRelationship(l_getGarmentBOMPart);
                    l_searchItem.addRelationship(l_getGarmentStyleBOM);
                    //----------------------------------------------------------------------------------------------

                    //get GarmentStyle Sketch List
                    Item l_getGarmentStyleSketch = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleSketch_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleSketch);
                    //get GarmentStyle CAD List
                    Item l_getGarmentStyleCAD = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleCAD_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleCAD);
                    //get GarmentStyle Document List
                    Item l_getGarmentStyleDocument = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleDocument_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleDocument);
                    //get GarmentStyle YearSeason List
                    Item l_getGarmentStyleYearSeason = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyleYearSeason_ItemName"), "get");
                    l_getGarmentStyleYearSeason.createRelatedItem(ConfigHelper.GetAPPConfigValue("YearSeason_ItemName"), "get");
                    l_searchItem.addRelationship(l_getGarmentStyleYearSeason);
                    #endregion

                    l_returnItem = l_searchItem.apply();
                }
                else
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetProductByID, "2", "");
                    l_returnClass.ErrorString = "GetProductByID Error:pi_productID Is Null Or Empty.";
                    l_returnClass.ErrorDetail = "GetProductByID Error:Please Setting pi_productID And Try Later.";

                    return l_returnClass;
                }
                #endregion

                #region Check Return Item
                if (l_returnItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleList, "2", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "GetProductList Error" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "GetProductList Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }
                if (l_returnItem.isEmpty())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleList, "2", "");
                    l_returnClass.ErrorString = "GetProductList Error: Return Is Empty";
                    l_returnClass.ErrorDetail = "GetProductList Error: Return Is Empty, Maybe Is Not Matching Data, Please Edit Parameter And Try Later.";

                    return l_returnClass;
                }
                if (l_returnItem.getItemCount() == 0)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.getGarmentStyleList, "2", "");
                    l_returnClass.ErrorString = "GetProductList Error: Return Is Empty";
                    l_returnClass.ErrorDetail = "GetProductList Error: Return Is Empty, Maybe Is Not Matching Data, Please Edit Parameter And Try Later.";

                    return l_returnClass;
                }

                #endregion

                #region To List

                if (l_returnItem.getItemCount() > 0)
                {
                    Product[] l_getProduct = new Product[l_returnItem.getItemCount()];

                    //get item and set value
                    for (int i = 0; i < l_returnItem.getItemCount(); i++)
                    {
                        Item l_getTempItem = l_returnItem.getItemByIndex(i);

                        #region product base

                        //new product info 
                        ProductBaseType l_productinfo = new ProductBaseType();

                        l_productinfo.ProductType = l_getTempItem.getProperty("cn_class0", "");
                        l_productinfo.ProductCategory = l_getTempItem.getProperty("cn_class1", "");
                        l_productinfo.ProductSubCategory = l_getTempItem.getProperty("cn_class2", "");

                        l_productinfo.MaterialStatus = "";//*                        
                        l_productinfo.Season = "";//*
                        l_productinfo.Year = "";//*

                        ProductClass l_tempProductImageClass = GetProductImage(l_getTempItem.getProperty("item_number", ""), eumImageType.UsePrimaryPath);
                        if (l_tempProductImageClass != null)
                        {
                            l_productinfo.Image = new URIDocumentsType() { DocumentURI = new string[] { l_tempProductImageClass.GetReturnString } };
                        }

                        #region productfeatures

                        ProductFeaturesType l_productfeatures = new ProductFeaturesType();
                        l_productfeatures.Gender = l_getTempItem.getProperty("cn_gender", "");
                        l_productfeatures.Collection = l_getTempItem.getProperty("cn_collection", "");
                        l_productfeatures.Series = l_getTempItem.getProperty("cn_series", "");
                        l_productfeatures.Making = l_getTempItem.getProperty("cn_making", "");
                        l_productfeatures.Fit = l_getTempItem.getProperty("cn_fit", "");
                        l_productfeatures.Line = l_getTempItem.getProperty("cn_line", "");
                        l_productfeatures.Collar = l_getTempItem.getProperty("cn_collar", "");
                        l_productfeatures.Placket = l_getTempItem.getProperty("cn_placket", "");
                        l_productfeatures.Sleeve = l_getTempItem.getProperty("cn_sleeve", "");
                        l_productfeatures.Cuff = l_getTempItem.getProperty("cn_cuff", "");
                        l_productfeatures.Pocket = l_getTempItem.getProperty("cn_pocket", "");
                        l_productfeatures.Washing = l_getTempItem.getProperty("cn_washing", "");
                        l_productfeatures.Embroidery = l_getTempItem.getProperty("cn_emb", "");
                        l_productfeatures.Printing = l_getTempItem.getProperty("cn_prt", "");
                        l_productfeatures.BodyPattern = l_getTempItem.getProperty("cn_body_pattern", "");
                        l_productfeatures.SAH = float.Parse(l_getTempItem.getProperty("cn_sah", "0"));
                        l_productfeatures.Remarks = l_getTempItem.getProperty("cn_remark", "");

                        #endregion
                        l_productinfo.ProductFeatures = l_productfeatures;

                        #region yearseason

                        Item l_getYearSeasonItem = l_getTempItem.getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleYearSeason_ItemName"));
                        if (l_getYearSeasonItem != null)
                        {
                            if (!l_getYearSeasonItem.isEmpty() && !l_getYearSeasonItem.isError())
                            {
                                YearSeasonBaseType[] l_yearSeasonArray = new YearSeasonBaseType[l_getYearSeasonItem.getItemCount()];
                                for (int j = 0; j < l_yearSeasonArray.Length; j++)
                                {
                                    Item l_getYearSeasonItem2 = l_getYearSeasonItem.getItemByIndex(j).getRelatedItem();
                                    if (!l_getYearSeasonItem2.isEmpty() && !l_getYearSeasonItem2.isError())
                                    {
                                        YearSeasonBaseType l_yearSeason = new YearSeasonBaseType();
                                        SeasonEnum l_seasonEnum = new SeasonEnum();

                                        l_seasonEnum.Code = l_getYearSeasonItem2.getProperty("cn_season", "");
                                        l_seasonEnum.SeasonName = l_getYearSeasonItem2.getProperty("cn_season", "");

                                        l_yearSeason.Season = l_seasonEnum;
                                        l_yearSeason.Year = l_getYearSeasonItem2.getProperty("cn_year", "");

                                        l_yearSeasonArray[j] = l_yearSeason;
                                    }
                                }
                                l_productfeatures.YearSeason = l_yearSeasonArray;
                            }

                        }
                        #endregion


                        #region finalsample
                        ProductBaseTypeFinalDevelopmentSample l_finalsample = new ProductBaseTypeFinalDevelopmentSample();
                        l_finalsample.Number = l_getTempItem.getProperty("cn_fds_no", "");
                        #endregion
                        l_productinfo.FinalDevelopmentSample = l_finalsample;

                        #region style
                        ProductBaseTypeStyle l_producttype = new ProductBaseTypeStyle();
                        l_producttype.Description = l_getTempItem.getProperty("cn_sleeve", "") + " " + l_getTempItem.getProperty("cn_collar", "") + " " + l_getTempItem.getProperty("cn_fit", "") + " " + l_getTempItem.getProperty("cn_body_pattern", "") + " " + l_getTempItem.getProperty("cn_collection", "") + "Shirt";//*
                        l_producttype.ID = l_getTempItem.getProperty("item_number", "");//*
                        l_producttype.Version = l_getTempItem.getProperty("jajor_rev", "");//*
                        #endregion
                        l_productinfo.Style = l_producttype;

                        CustomerBaseType[] l_customerinfoarray = new CustomerBaseType[1];
                        CustomerBaseType l_customerinfo = new CustomerBaseType();
                        l_customerinfo.CustomerCode = "";
                        l_customerinfo.CustomerName = "";
                        l_customerinfo.BrandCode = "";
                        l_customerinfo.BrandName = "";

                        #region search customer
                        string l_customerID = l_getTempItem.getProperty("cn_cust_code");
                        if (!string.IsNullOrEmpty(l_customerID))
                        {
                            Item l_customerItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Customer_ItemName"), l_customerID);
                            if (!l_customerItem.isError() && !l_customerItem.isEmpty())
                            {
                                l_customerinfo.CustomerCode = l_customerItem.getProperty("keyed_name", "");
                                l_customerinfo.CustomerName = l_customerItem.getProperty("name", "");
                            }
                        }

                        #endregion

                        #region search brand
                        string l_brandID = l_getTempItem.getProperty("cn_brand_code");
                        if (!string.IsNullOrEmpty(l_brandID))
                        {
                            Item l_brandItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("CustomerBrand_ItemName"), l_brandID);
                            {
                                if (!l_brandItem.isError() && !l_brandItem.isEmpty())
                                {
                                    l_customerinfo.BrandCode = l_brandItem.getProperty("keyed_name", "");
                                    l_customerinfo.BrandName = l_brandItem.getProperty("cn_brand", "");
                                }
                            }
                        }
                        #endregion

                        l_customerinfoarray[0] = l_customerinfo;
                        l_productinfo.CustomerInfo = l_customerinfoarray;

                        #endregion

                        #region garmentBOM

                        GarmentBOMItemBaseType l_garmentbom = new GarmentBOMItemBaseType();

                        #region 1.colorway
                        Item l_getColorWayItem = l_getTempItem.getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleGarmentColorway_ItemName"));
                        if (l_getColorWayItem != null)
                        {
                            if (!l_getColorWayItem.isError() && !l_getColorWayItem.isEmpty())
                            {
                                ColorwayBaseType[] l_colorWayArray = new ColorwayBaseType[l_getColorWayItem.getItemCount()];
                                for (int k = 0; k < l_colorWayArray.Length; k++)
                                {
                                    ColorwayBaseType l_colorway = new ColorwayBaseType();
                                    l_colorway.BodyPattern = new string[] { l_getColorWayItem.getItemByIndex(k).getProperty("cn_body_pattern", "") };
                                    l_colorway.Code = l_getColorWayItem.getItemByIndex(k).getProperty("cn_cw_code", "");
                                    l_colorway.ColorwayName = l_getColorWayItem.getItemByIndex(k).getProperty("cn_colorway", "");
                                    l_colorway.CustomerReferenceCode = l_getColorWayItem.getItemByIndex(k).getProperty("cn_cust_cwcode", "");
                                    l_colorway.CustomerReferenceName = l_getColorWayItem.getItemByIndex(k).getProperty("cn_cust_cw", "");
                                    l_colorway.DocumentURIs = null;
                                    l_colorway.PLU = l_getColorWayItem.getItemByIndex(k).getProperty("cn_plu", "");

                                    l_colorWayArray[k] = l_colorway;
                                }
                                l_garmentbom.Colorway = l_colorWayArray;
                            }
                        }
                        #endregion


                        #region 2.productsize
                        Item l_getProductSizeItem = l_getTempItem.getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRange_ItemName"));
                        if (l_getProductSizeItem != null)
                        {
                            if (!l_getProductSizeItem.isEmpty() && !l_getProductSizeItem.isError())
                            {
                                GarmentBOMItemBaseTypeProductSizes[] l_productSizeArray = new GarmentBOMItemBaseTypeProductSizes[l_getProductSizeItem.getItemCount()];

                                for (int l = 0; l < l_getProductSizeItem.getItemCount(); l++)
                                {
                                    GarmentBOMItemBaseTypeProductSizes l_productSize = new GarmentBOMItemBaseTypeProductSizes();
                                    List<string> l_getsize = new List<string>();

                                    Item l_getProductSizeD1Item = l_getProductSizeItem.getItemByIndex(l).getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRangeD1_ItemName"));
                                    for (int l_size = 0; l_size < l_getProductSizeD1Item.getItemCount(); l_size++)
                                    {
                                        Item l_getProductSizeD2Item = l_getProductSizeD1Item.getItemByIndex(l_size).getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleSizeRangeD2_ItemName"));
                                        for (int l_size2 = 0; l_size2 < l_getProductSizeD2Item.getItemCount(); l_size2++)
                                        {
                                            l_getsize.Add(l_getProductSizeD2Item.getItemByIndex(l_size2).getProperty("cn_size2", ""));
                                        }
                                    }
                                    string[] l_getproductsizeArray = new string[l_getsize.Count];
                                    for (int i_arrayIndex = 0; i_arrayIndex < l_getsize.Count; i_arrayIndex++)
                                    {
                                        l_getproductsizeArray[i_arrayIndex] = l_getsize[i_arrayIndex];

                                    }
                                    l_productSize.ProductSize = l_getproductsizeArray;
                                    l_productSizeArray[l] = l_productSize;
                                }
                                l_garmentbom.ProductSizes = l_productSizeArray;
                            }
                        }

                        #endregion


                        #region getPart,because fabric part and trim part with together, so get part to slit

                        List<FabricPartBaseType> l_getFabricPartList = new List<FabricPartBaseType>();
                        List<TrimPartBaseType> l_getTrimPartList = new List<TrimPartBaseType>();

                        Item l_getGarmentStyleBOMItem = l_getTempItem.getRelationships(ConfigHelper.GetAPPConfigValue("GarmentStyleBOM_ItemName"));
                        if (l_getGarmentStyleBOMItem != null)
                        {
                            if (!l_getGarmentStyleBOMItem.isError() && !l_getGarmentStyleBOMItem.isEmpty())
                            {
                                for (int i_bomIndex = 0; i_bomIndex < l_getGarmentStyleBOMItem.getItemCount(); i_bomIndex++)
                                {
                                    Item l_temp1 = l_getGarmentStyleBOMItem.getItemByIndex(i_bomIndex);
                                    if (l_temp1 != null)
                                    {
                                        #region temp1
                                        Item l_temp2 = l_getGarmentStyleBOMItem.getItemByIndex(i_bomIndex).getRelatedItem();
                                        if (l_temp2 != null)
                                        {
                                            #region temp2
                                            Item l_getGarmentBOMPartItem = l_getGarmentStyleBOMItem.getItemByIndex(i_bomIndex).getRelatedItem().getRelationships(ConfigHelper.GetAPPConfigValue("GarmentBOMPart_ItemName"));
                                            if (l_getGarmentBOMPartItem != null)
                                            {
                                                if (!l_getGarmentBOMPartItem.isEmpty() && !l_getGarmentBOMPartItem.isError())
                                                {
                                                    for (int i_bomPartIndex = 0; i_bomPartIndex < l_getGarmentBOMPartItem.getItemCount(); i_bomPartIndex++)
                                                    {
                                                        Item l_getPartItem = l_getGarmentBOMPartItem.getItemByIndex(i_bomPartIndex).getRelatedItem();
                                                        if (l_getPartItem != null)
                                                        {
                                                            if (!l_getPartItem.isEmpty() && !l_getPartItem.isError())
                                                            {
                                                                string l_getPartType = l_getPartItem.getProperty("cn_class0", "");
                                                                if (l_getPartType == "FAB")
                                                                {
                                                                    //l_getFabricPartList.Add(l_getPartItem.getItemByIndex(0));

                                                                    #region 3.fabricpart

                                                                    FabricPartBaseType l_tempFabricPartBaseType = new FabricPartBaseType();

                                                                    l_tempFabricPartBaseType.Description = l_getPartItem.getProperty("description", "");

                                                                    #region Content
                                                                    Item l_getPartContentItem = l_getPartItem.getRelationships(ConfigHelper.GetAPPConfigValue("PartContent_ItemName"));
                                                                    if (l_getPartContentItem != null)
                                                                    {
                                                                        if (!l_getPartContentItem.isError() && !l_getPartContentItem.isEmpty())
                                                                        {
                                                                            FabricContentBaseType[] l_tempFabricContentBaseTypeArray = new FabricContentBaseType[l_getPartContentItem.getItemCount()];

                                                                            for (int i_getPartContentIndex = 0; i_getPartContentIndex < l_getPartContentItem.getItemCount(); i_getPartContentIndex++)
                                                                            {
                                                                                FabricContentBaseType l_tempFabricContentBaseType = new FabricContentBaseType();
                                                                                l_tempFabricContentBaseType.Description = l_getPartContentItem.getItemByIndex(i_getPartContentIndex).getProperty("cn_content", "");
                                                                                l_tempFabricContentBaseType.Ratio = float.Parse(l_getPartContentItem.getItemByIndex(i_getPartContentIndex).getProperty("cn_content_ratio", "0"));

                                                                                l_tempFabricContentBaseTypeArray[i_getPartContentIndex] = l_tempFabricContentBaseType;
                                                                            }
                                                                            l_tempFabricPartBaseType.Content = l_tempFabricContentBaseTypeArray;
                                                                        }
                                                                    }

                                                                    #endregion

                                                                    #region base info
                                                                    l_tempFabricPartBaseType.YarnCount = l_getPartItem.getProperty("cn_yarn_count", "");
                                                                    l_tempFabricPartBaseType.Construction = l_getPartItem.getProperty("cn_construction", "");
                                                                    l_tempFabricPartBaseType.DyeMethod = l_getPartItem.getProperty("cn_dye_method", "");
                                                                    l_tempFabricPartBaseType.Finishing = l_getPartItem.getProperty("cn_finishing", "");
                                                                    l_tempFabricPartBaseType.Width = l_getPartItem.getProperty("cn_fabric_width", "");
                                                                    l_tempFabricPartBaseType.Pattern = l_getPartItem.getProperty("cn_fabric_pattern", "");
                                                                    l_tempFabricPartBaseType.Printing = l_getPartItem.getProperty("cn_printing", "");
                                                                    l_tempFabricPartBaseType.RepeatVertical = l_getPartItem.getProperty("cn_rep_v", "");
                                                                    l_tempFabricPartBaseType.RepeatHorizontal = l_getPartItem.getProperty("cn_rep_h", "");
                                                                    l_tempFabricPartBaseType.TestingGrade = l_getPartItem.getProperty("cn_fab_test_grading", "");
                                                                    l_tempFabricPartBaseType.Remarks = l_getPartItem.getProperty("cn_remark", "");
                                                                    #endregion

                                                                    #region Header
                                                                    //header
                                                                    FabricPartHeaderType l_tempFabricPartHeaderType = new FabricPartHeaderType();
                                                                    l_tempFabricPartHeaderType.Code = l_getPartItem.getProperty("cn_ppo_agpo", "");
                                                                    l_tempFabricPartHeaderType.Cateogry = l_getPartItem.getProperty("cn_class1", "");
                                                                    l_tempFabricPartHeaderType.WovenFabrication = l_getPartItem.getProperty("cn_class2", "");
                                                                    l_tempFabricPartHeaderType.KnitFabrication = "";
                                                                    l_tempFabricPartHeaderType.Version = l_getPartItem.getProperty("maj_rev", "");
                                                                    l_tempFabricPartHeaderType.Status = l_getPartItem.getProperty("state", "");

                                                                    #region Header Set CustomerInfo
                                                                    CustomerBaseType[] l_tempCustomerBaseTypeArray = new CustomerBaseType[1];
                                                                    CustomerBaseType l_tempCustomerBaseType = new CustomerBaseType();

                                                                    l_tempCustomerBaseType.CustomerCode = "";
                                                                    l_tempCustomerBaseType.CustomerName = "";
                                                                    l_tempCustomerBaseType.CustomerReferenceNum = "";
                                                                    l_tempCustomerBaseType.BrandCode = "";
                                                                    l_tempCustomerBaseType.BrandName = "";

                                                                    //customer
                                                                    string l_partCustomerID = l_getPartItem.getProperty("cn_cust_code", "");
                                                                    if (!string.IsNullOrEmpty(l_partCustomerID))
                                                                    {
                                                                        Item l_getPartCustomerItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Customer_ItemName"), l_partCustomerID);
                                                                        if (!l_getPartCustomerItem.isError() && !l_getPartCustomerItem.isEmpty())
                                                                        {
                                                                            l_tempCustomerBaseType.CustomerCode = l_getPartCustomerItem.getProperty("keyed_name", "");
                                                                            l_tempCustomerBaseType.CustomerName = l_getPartCustomerItem.getProperty("name", "");
                                                                            l_tempCustomerBaseType.CustomerReferenceNum = l_getPartCustomerItem.getProperty("cn_cust_item_id", "");
                                                                        }
                                                                    }
                                                                    //brand
                                                                    string l_partBrandID = l_getPartItem.getProperty("cn_brand_code", "");
                                                                    if (!string.IsNullOrEmpty(l_partBrandID))
                                                                    {
                                                                        Item l_getPartBrandItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("CustomerBrand_ItemName"), l_partBrandID);
                                                                        if (!l_getPartBrandItem.isError() && !l_getPartBrandItem.isEmpty())
                                                                        {
                                                                            l_tempCustomerBaseType.BrandCode = l_getPartBrandItem.getProperty("keyed_name", "");
                                                                            l_tempCustomerBaseType.BrandName = l_getPartBrandItem.getProperty("name", "");
                                                                        }
                                                                    }

                                                                    l_tempCustomerBaseTypeArray[0] = l_tempCustomerBaseType;
                                                                    //header set customer info
                                                                    l_tempFabricPartHeaderType.CustomerInfo = l_tempCustomerBaseTypeArray;
                                                                    #endregion
                                                                    #endregion
                                                                    l_tempFabricPartBaseType.FabricHeader = l_tempFabricPartHeaderType;

                                                                    #region SupplierBaseType
                                                                    SupplierBaseType[] l_tempSupplierBaseTypeArray = new SupplierBaseType[1];
                                                                    SupplierBaseType l_tempSupplierBaseType = new SupplierBaseType();
                                                                    string l_getPartSupplierID = l_getPartItem.getProperty("cn_supplier_code", "");
                                                                    if (!string.IsNullOrEmpty(l_getPartSupplierID))
                                                                    {
                                                                        Item l_getSupplierItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Supplier_ItemName"), l_getPartSupplierID);

                                                                        l_tempSupplierBaseType.Code = l_getPartItem.getProperty("cn_supplier_code");
                                                                        l_tempSupplierBaseType.ItemCode = l_getPartItem.getProperty("cn_supplier_item_code");
                                                                        l_tempSupplierBaseType.SupplierName = l_getPartItem.getProperty("cn_class1");
                                                                    }
                                                                    l_tempSupplierBaseTypeArray[0] = l_tempSupplierBaseType;
                                                                    #endregion
                                                                    l_tempFabricPartBaseType.Supplier = l_tempSupplierBaseTypeArray;


                                                                    //color combo
                                                                    l_tempFabricPartBaseType.ColorCombo = null;

                                                                    //fabric shrinkage
                                                                    l_tempFabricPartBaseType.FabricShrinkage = null;

                                                                    //garment shrinkage
                                                                    l_tempFabricPartBaseType.GarmentShrinkage = null;

                                                                    //price
                                                                    PriceBaseType[] l_tempPriceBaseTypeArray = new PriceBaseType[1];
                                                                    PriceBaseType l_tempPriceBaseType = new PriceBaseType();
                                                                    l_tempPriceBaseType.Amount = Convert.ToDecimal(l_getPartItem.getProperty("cn_remark", "0"));
                                                                    l_tempPriceBaseType.Currency = "";//*
                                                                    l_tempPriceBaseTypeArray[0] = l_tempPriceBaseType;
                                                                    l_tempFabricPartBaseType.Price = l_tempPriceBaseTypeArray;

                                                                    //year season                            
                                                                    l_tempFabricPartBaseType.YearSeason = null;

                                                                    //PartSize
                                                                    Item l_getPartSizeItem = l_getPartItem.getRelationships(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"));
                                                                    if (l_getPartSizeItem != null)
                                                                    {
                                                                        if (!l_getPartSizeItem.isEmpty() && !l_getPartSizeItem.isError())
                                                                        {
                                                                            FabricSizeBaseType[] l_tempFabricSizeBaseTypeArray = new FabricSizeBaseType[l_getPartSizeItem.getItemCount()];
                                                                            for (int i_partSizeIndex = 0; i_partSizeIndex < l_getPartSizeItem.getItemCount(); i_partSizeIndex++)
                                                                            {
                                                                                FabricSizeBaseType l_tempFabricSizeBaseType = new FabricSizeBaseType();
                                                                                l_tempFabricSizeBaseType.Dimension = l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size_desc", "");
                                                                                l_tempFabricSizeBaseType.Measurement = 0; //l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size", "");//*
                                                                                l_tempFabricSizeBaseTypeArray[i_partSizeIndex] = l_tempFabricSizeBaseType;
                                                                            }
                                                                            l_tempFabricPartBaseType.Size = l_tempFabricSizeBaseTypeArray;
                                                                        }
                                                                    }

                                                                    //loyout                                           



                                                                    #endregion

                                                                    l_getFabricPartList.Add(l_tempFabricPartBaseType);

                                                                }
                                                                else if (l_getPartType == "TRM")
                                                                {
                                                                    //l_getTrimPartList.Add(l_getPartItem.getItemByIndex(0));

                                                                    #region 4.trimpart

                                                                    TrimPartBaseType l_tempTrimPartBaseType = new TrimPartBaseType();


                                                                    l_tempTrimPartBaseType.Description = l_getPartItem.getProperty("description", "");



                                                                    #region base info
                                                                    l_tempTrimPartBaseType.Remarks = l_getPartItem.getProperty("cn_remark", "");
                                                                    #endregion

                                                                    #region Header
                                                                    //header
                                                                    TrimPartHeaderType l_tempTrimPartHeaderType = new TrimPartHeaderType();
                                                                    l_tempTrimPartHeaderType.Code = l_getPartItem.getProperty("item_number", "");

                                                                    TrimCategoryBaseType l_tempTrimCategoryBaseType = new TrimCategoryBaseType();
                                                                    l_tempTrimCategoryBaseType.Code = l_getPartItem.getProperty("cn_class1", "");
                                                                    l_tempTrimCategoryBaseType.Description = l_getPartItem.getProperty("description", "");
                                                                    l_tempTrimPartHeaderType.Cateogry = l_tempTrimCategoryBaseType;

                                                                    l_tempTrimPartHeaderType.SubCategory = l_getPartItem.getProperty("cn_class2", "");
                                                                    l_tempTrimPartHeaderType.Version = l_getPartItem.getProperty("maj_rev", "");
                                                                    l_tempTrimPartHeaderType.Status = l_getPartItem.getProperty("state", "");

                                                                    #region Header Set CustomerInfo
                                                                    CustomerBaseType[] l_tempCustomerBaseTypeArray = new CustomerBaseType[1];
                                                                    CustomerBaseType l_tempCustomerBaseType = new CustomerBaseType();

                                                                    l_tempCustomerBaseType.CustomerCode = "";
                                                                    l_tempCustomerBaseType.CustomerName = "";
                                                                    l_tempCustomerBaseType.CustomerReferenceNum = "";
                                                                    l_tempCustomerBaseType.BrandCode = "";
                                                                    l_tempCustomerBaseType.BrandName = "";

                                                                    //customer
                                                                    string l_partCustomerID = l_getPartItem.getProperty("cn_cust_code", "");
                                                                    if (!string.IsNullOrEmpty(l_partCustomerID))
                                                                    {
                                                                        Item l_getPartCustomerItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Customer_ItemName"), l_partCustomerID);
                                                                        if (!l_getPartCustomerItem.isError() && !l_getPartCustomerItem.isEmpty())
                                                                        {
                                                                            l_tempCustomerBaseType.CustomerCode = l_getPartCustomerItem.getProperty("keyed_name", "");
                                                                            l_tempCustomerBaseType.CustomerName = l_getPartCustomerItem.getProperty("name", "");
                                                                            l_tempCustomerBaseType.CustomerReferenceNum = l_getPartCustomerItem.getProperty("cn_cust_item_id", "");
                                                                        }
                                                                    }
                                                                    //brand
                                                                    string l_partBrandID = l_getPartItem.getProperty("cn_brand_code", "");
                                                                    if (!string.IsNullOrEmpty(l_partBrandID))
                                                                    {
                                                                        Item l_getPartBrandItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("CustomerBrand_ItemName"), l_partBrandID);
                                                                        if (!l_getPartBrandItem.isError() && !l_getPartBrandItem.isEmpty())
                                                                        {
                                                                            l_tempCustomerBaseType.BrandCode = l_getPartBrandItem.getProperty("keyed_name", "");
                                                                            l_tempCustomerBaseType.BrandName = l_getPartBrandItem.getProperty("name", "");
                                                                        }
                                                                    }

                                                                    l_tempCustomerBaseTypeArray[0] = l_tempCustomerBaseType;
                                                                    //header set customer info
                                                                    l_tempTrimPartHeaderType.CustomerInfo = l_tempCustomerBaseTypeArray;
                                                                    #endregion

                                                                    #endregion
                                                                    l_tempTrimPartBaseType.TrimHeader = l_tempTrimPartHeaderType;

                                                                    #region SupplierBaseType
                                                                    SupplierBaseType[] l_tempSupplierBaseTypeArray = new SupplierBaseType[1];
                                                                    SupplierBaseType l_tempSupplierBaseType = new SupplierBaseType();
                                                                    string l_getPartSupplierID = l_getPartItem.getProperty("cn_supplier_code", "");
                                                                    if (!string.IsNullOrEmpty(l_getPartSupplierID))
                                                                    {
                                                                        Item l_getSupplierItem = m_Innovator.getItemById(ConfigHelper.GetAPPConfigValue("Supplier_ItemName"), l_getPartSupplierID);

                                                                        l_tempSupplierBaseType.Code = l_getPartItem.getProperty("cn_supplier_code", "");
                                                                        l_tempSupplierBaseType.ItemCode = l_getPartItem.getProperty("cn_supplier_item_code", "");
                                                                        l_tempSupplierBaseType.SupplierName = l_getPartItem.getProperty("cn_class1", "");
                                                                    }
                                                                    l_tempSupplierBaseTypeArray[0] = l_tempSupplierBaseType;
                                                                    #endregion
                                                                    l_tempTrimPartBaseType.Supplier = l_tempSupplierBaseTypeArray;


                                                                    //AGPO
                                                                    l_tempTrimPartBaseType.AGPONum = new string[] { l_getPartItem.getProperty("cn_ppo_agpo", "") };

                                                                    //color
                                                                    l_tempTrimPartBaseType.Color = null;

                                                                    l_tempTrimPartBaseType.Material = "";


                                                                    //year season                            
                                                                    l_tempTrimPartBaseType.YearSeason = null;

                                                                    //PartSize
                                                                    Item l_getPartSizeItem = l_getPartItem.getRelationships(ConfigHelper.GetAPPConfigValue("PartSize_ItemName"));
                                                                    if (l_getPartSizeItem != null)
                                                                    {
                                                                        if (!l_getPartSizeItem.isEmpty() && !l_getPartSizeItem.isError())
                                                                        {
                                                                            SizeBaseType[] l_tempSizeBaseTypeArray = new SizeBaseType[l_getPartSizeItem.getItemCount()];
                                                                            for (int i_partSizeIndex = 0; i_partSizeIndex < l_getPartSizeItem.getItemCount(); i_partSizeIndex++)
                                                                            {
                                                                                SizeBaseType l_tempSizeBaseType = new SizeBaseType();
                                                                                l_tempSizeBaseType.Dimension = l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size_desc", "");
                                                                                l_tempSizeBaseType.Measurement = 0; //l_getPartSizeItem.getItemByIndex(i_partSizeIndex).getProperty("cn_size", "");//*
                                                                                l_tempSizeBaseTypeArray[i_partSizeIndex] = l_tempSizeBaseType;
                                                                            }
                                                                            l_tempTrimPartBaseType.Size = l_tempSizeBaseTypeArray;
                                                                        }
                                                                    }


                                                                    #endregion

                                                                    l_getTrimPartList.Add(l_tempTrimPartBaseType);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            #endregion
                                        }
                                        #endregion
                                    }
                                }
                            }
                        }

                        #endregion

                        #region process fabirc
                        FabricBOMItemBaseType l_FabricBOMItemBaseType = new FabricBOMItemBaseType();
                        l_FabricBOMItemBaseType.ComboName = "";
                        l_FabricBOMItemBaseType.CutWay = "";
                        l_FabricBOMItemBaseType.FabricCode = "";

                        l_FabricBOMItemBaseType.FabricWidth = "";
                        l_FabricBOMItemBaseType.GarmentComponent = "";
                        l_FabricBOMItemBaseType.PieceFabricSize = "";
                        l_FabricBOMItemBaseType.PIeceFabricUsage = 0;
                        l_FabricBOMItemBaseType.YPD = 0;
                        //l_garmentbom.FabricBOM = l_fabricPartArray;

                        FabricBOMItemBaseTypeFabricParts l_FabricBOMItemBaseTypeFabricParts = new FabricBOMItemBaseTypeFabricParts();
                        if (l_getFabricPartList.Count > 0)
                        {
                            FabricPartBaseType[] l_FabricPartBaseTypeArray = new FabricPartBaseType[l_getFabricPartList.Count];
                            for (int i_fabricpartindex = 0; i_fabricpartindex < l_getFabricPartList.Count; i_fabricpartindex++)
                            {
                                l_FabricPartBaseTypeArray[i_fabricpartindex] = l_getFabricPartList[i_fabricpartindex];
                            }
                            l_FabricBOMItemBaseTypeFabricParts.FabricPart = l_FabricPartBaseTypeArray;
                        }
                        #endregion
                        l_FabricBOMItemBaseType.FabricParts = l_FabricBOMItemBaseTypeFabricParts;
                        l_garmentbom.FabricBOM = new FabricBOMItemBaseType[] { l_FabricBOMItemBaseType };

                        #region process trim
                        TrimBOMItemBaseType l_TrimBOMItemBaseType = new TrimBOMItemBaseType();
                        l_TrimBOMItemBaseType.TrimCode = l_getTempItem.getProperty("item_number", "");
                        l_TrimBOMItemBaseType.TrimColor = null;
                        l_TrimBOMItemBaseType.TrimSize = "";


                        TrimBOMItemBaseTypeTrimParts l_TrimBOMItemBaseTypeTrimParts = new TrimBOMItemBaseTypeTrimParts();
                        if (l_getTrimPartList.Count > 0)
                        {
                            TrimPartBaseType[] l_TrimPartBaseTypeArray = new TrimPartBaseType[l_getTrimPartList.Count];
                            for (int i_trimpartindex = 0; i_trimpartindex < l_getTrimPartList.Count; i_trimpartindex++)
                            {
                                l_TrimPartBaseTypeArray[i_trimpartindex] = l_getTrimPartList[i_trimpartindex];
                            }
                            l_TrimBOMItemBaseTypeTrimParts.TrimPart = l_TrimPartBaseTypeArray;
                        }
                        #endregion
                        l_TrimBOMItemBaseType.TrimParts = l_TrimBOMItemBaseTypeTrimParts;
                        l_garmentbom.TrimBOM = new TrimBOMItemBaseType[] { l_TrimBOMItemBaseType };

                        #endregion

                        //new product
                        Product l_product = new Product();
                        l_product.ProductInfo = l_productinfo;
                        l_product.BOM = l_garmentbom;

                        //set product
                        l_getProduct[i] = l_product;
                    }

                    //new product is product array
                    Products l_products = new Products();
                    l_products.Product = l_getProduct;

                    //return class product is products
                    l_returnClass.ProductList = l_products;
                }

                #endregion

                #region login out
                m_Connection.Logout();
                #endregion

                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "GetProductByID Error:" + ex.ToString());

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetProductByID, "2", "");
                l_returnClass.ErrorString = "GetProductByID Error";
                l_returnClass.ErrorDetail = "GetProductByID Error:" + ex.Message;

                return l_returnClass;
            }
        }

        public ProductClass GetProductImage(string pi_productID,eumImageType pi_getImageType)
        {
            ProductClass l_returnClass = new ProductClass();
            l_returnClass.SuccessFlag = true;

            if (string.IsNullOrEmpty(pi_productID))
            {
                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetProductImage, "0", "");
                l_returnClass.ErrorString = "GetProductImage Error: Parameter Is Null";
                l_returnClass.ErrorDetail = "GetProductImage Error: Parameter Is Null , Please Setting Product ID And Try Later.";

                return l_returnClass;
            }

            if (pi_getImageType == null)
            {
                pi_getImageType = eumImageType.UsePrimaryPath;
            }

            try
            {
                //search GarmentStyle 
                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyle_ItemName"), "get");

                Item l_returnItem = null;                
                SelectionFilter l_newSelectionFilter = SelectionFilter.CreateLeaf("ITEM_NUMBER", "EQ", pi_productID.Trim());
                Item l_searchItem = ParseSelectionFilter.ParseSelection(l_newSelectionFilter, l_getItem);
                l_returnItem = l_searchItem.apply();
                

                if (l_getItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetProductImage, "1", l_returnItem.getErrorCode());
                    l_returnClass.ErrorString = "GetProductImage Error:" + l_returnItem.getErrorString();
                    l_returnClass.ErrorDetail = "GetProductImage Error:" + l_returnItem.getErrorDetail();

                    return l_returnClass;
                }

                //parse garmentStyle image path
                if (l_getItem.isEmpty())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetProductImage, "1", ""); ;
                    l_returnClass.ErrorString = "GetProductImage Error:Get Item Is Empty .";
                    l_returnClass.ErrorDetail = "GetProductImage Error:Aras Apply Item Return Is Empty , Please Check Selection Parameter .";

                    return l_returnClass;
                }

                string l_ImageFileItemValue = l_returnItem.getItemByIndex(0).getProperty("thumbnail");
                if (string.IsNullOrEmpty(l_ImageFileItemValue))
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetProductImage, "1", "");
                    l_returnClass.ErrorString = "GetProductImage Error:Garment Style Item Image Is Empty .";
                    l_returnClass.ErrorDetail = "GetProductImage Error:Garment Record Image Is Empty , Please Upload Image , And Get Image Later .";

                    return l_returnClass;
                }
                if (l_ImageFileItemValue.IndexOf("vault:///?fileId=") == -1)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetProductImage, "1", "");
                    l_returnClass.ErrorString = "GetProductImage Error:Garment Style Item Image Is Not Vault .";
                    l_returnClass.ErrorDetail = "GetProductImage Error:Garment Record Image Is Empty , Please Upload Image , And Get Image Later .";

                    return l_returnClass;
                }

                //get file item
                string l_fileItemID = Common.StringHelper.GetRightString(l_ImageFileItemValue, "vault:///?fileId=");
                Item l_fileItem = m_Innovator.getItemById("File", l_fileItemID);

                if (pi_getImageType == eumImageType.UseTempPath)
                {
                    //copy file to temp path
                    string l_tempImagePath = ItemDownFilePath.path + l_fileItem.getID() + "." + Common.StringHelper.GetRightString(l_fileItem.getFileName(), ".");
                    string l_tempImageUrl = Common.ConfigHelper.GetAPPConfigValue("tempFileUrl") + l_fileItem.getID() + "." + Common.StringHelper.GetRightString(l_fileItem.getFileName(), ".");
                    m_Innovator.getConnection().DownloadFile(l_fileItem, l_tempImagePath, true);
                    l_returnClass.GetReturnString = l_tempImageUrl;
                }
                else if (pi_getImageType == eumImageType.UsePrimaryPath)
                {
                    l_returnClass.GetReturnString = m_Innovator.getFileUrl(l_fileItemID, UrlType.SecurityToken);
                }

                //return temp path                


                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "GetProductImage Error:" + ex.Message);

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.GetProductImage, "2", "");
                l_returnClass.ErrorString = "GetProductImage Error";
                l_returnClass.ErrorDetail = "GetProductImage Error:" + ex.Message;

                return l_returnClass;
            }
        }

        public ProductClass CreateProduct(Products pi_productList)
        {
            return null;
        }

        public ProductClass UpdateProduct(Products pi_productList)
        {
            return null;
        }

        /// <summary>
        /// Remove Product By ID 
        /// 2015-09-10 add by WesChen
        /// </summary>
        /// <param name="pi_productID">Remove Product ID(Code)</param>
        /// <returns></returns>
        public ProductClass RemoveProductByID(string pi_productID)
        {
            ProductClass l_returnClass = new ProductClass();
            l_returnClass.SuccessFlag = true;

            try
            {

                ArasServiceConfig(ConfigHelper.GetAPPConfigValue("InnovatorUserName"), ConfigHelper.GetAPPConfigValue("InnovatorPassword"));
                Item l_getItem = m_Innovator.newItem(ConfigHelper.GetAPPConfigValue("GarmentStyle_ItemName"), "get");
                Item l_deleteItem = null;

                if (!string.IsNullOrEmpty(pi_productID))
                {
                    SelectionFilter l_newSelectionFilter = SelectionFilter.CreateLeaf("ITEM_NUMBER", "EQ", pi_productID.Trim());
                    Item l_searchItem = ParseSelectionFilter.ParseSelection(l_newSelectionFilter, l_getItem);
                    l_deleteItem = l_searchItem.apply();
                }
                else
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.RemoveProductByID, "1", "");
                    l_returnClass.ErrorString = "RemoveProductByID Error:Operation ID Is Null";
                    l_returnClass.ErrorDetail = "RemoveProductByID Error:Please Setting Operation ID And Try Later.";

                    return l_returnClass;
                }

                if (l_deleteItem.isError())
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.RemoveProductByID, "1", l_deleteItem.getErrorCode());
                    l_returnClass.ErrorString = "RemoveProductByID Error:" + l_deleteItem.getErrorString();
                    l_returnClass.ErrorDetail = "RemoveProductByID Error:" + l_deleteItem.getErrorDetail();

                    return l_returnClass;
                }

                if (l_deleteItem.getItemCount() == 0)
                {
                    l_returnClass.SuccessFlag = false;
                    l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.RemoveProductByID, "1", "");
                    l_returnClass.ErrorString = "RemoveProductByID Error:Not Item To Operation";
                    l_returnClass.ErrorDetail = "RemoveProductByID Error:Not Item To Operation";

                    return l_returnClass;
                }

                if (l_deleteItem.getItemCount() > 1)
                {
                    for (int i = 0; i < l_deleteItem.getItemCount(); i++)
                    {
                        Item l_deleteDetailItem = l_deleteItem.getItemByIndex(i);
                        l_deleteDetailItem.setAction("delete");
                        Item l_returnItem = l_deleteDetailItem.apply();
                        if (l_returnItem.isError())
                        {
                            l_returnClass.SuccessFlag = false;
                            l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.RemoveProductByID, "1", l_returnItem.getErrorCode());
                            l_returnClass.ErrorString = "RemoveProductByID Error:" + l_returnItem.getErrorString();
                            l_returnClass.ErrorDetail = "RemoveProductByID Error:" + l_returnItem.getErrorDetail();

                            return l_returnClass;
                        }
                    }
                }
                else
                {
                    l_deleteItem.setAction("delete");
                    Item l_returnItem = l_deleteItem.apply();
                    if (l_returnItem.isError())
                    {
                        l_returnClass.SuccessFlag = false;
                        l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.RemoveProductByID, "1", l_returnItem.getErrorCode());
                        l_returnClass.ErrorString = "RemoveProductByID Error:" + l_returnItem.getErrorString();
                        l_returnClass.ErrorDetail = "RemoveProductByID Error:" + l_returnItem.getErrorDetail();

                        return l_returnClass;
                    }
                }

                m_Connection.Logout();
                return l_returnClass;
            }
            catch (Exception ex)
            {
                LogFileHelper.ExcuteEventLog(LogFilePath.path, "RemoveProductByID Error:" + ex.ToString());

                l_returnClass.SuccessFlag = false;
                l_returnClass.ErrorCode = ErrorCodeSetting.GetErrorCode(Function.RemoveProductByID, "2", "");
                l_returnClass.ErrorString = "RemoveProductByID Error";
                l_returnClass.ErrorDetail = "RemoveProductByID Error:" + ex.Message;

                return l_returnClass;
            }
        }
        //----------------------------------------------------------------------------------------------------------------------------------------------------------

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