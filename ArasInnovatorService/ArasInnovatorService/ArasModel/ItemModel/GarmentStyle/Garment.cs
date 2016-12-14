using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ArasInnovatorService.Common;

namespace ArasInnovatorService.ArasModel
{
    public class Garment
    {
        public string ID { get; set; }
        public string CLASSIFICATION { get; set; }
        public string KEYED_NAME { get; set; }
        public DateTime CREATED_ON { get; set; }
        public string CREATED_BY_ID { get; set; }//Link User
        public string CREATED_BY_NAME { get; set; }
        public string OWNED_BY_ID { get; set; }
        public string MANAGED_BY_ID { get; set; }
        public DateTime MODIFIED_ON { get; set; }
        public string MODIFIED_BY_ID { get; set; }//Link User
        public string MODIFIED_BY_NAME { get; set; }
        public string CURRENT_STATE { get; set; }//Link Life Cycle State
        public string CURRENT_STATE_DESC { get; set; }
        public string STATE { get; set; }
        public string LOCKED_BY_ID { get; set; }//Link User
        public string LOCKED_BY_NAME { get; set; }
        public string IS_CURRENT { get; set; }
        public string MAJOR_REV { get; set; }
        public string MINOR_REV { get; set; }
        public string IS_RELEASED { get; set; }
        public string NOT_LOCKABLE { get; set; }
        public string CSS { get; set; }
        public int GENERATION { get; set; }
        public string NEW_VERSION { get; set; }
        public string CONFIG_ID { get; set; }
        public string PERMISSION_ID { get; set; }//Link Permission
        public string PERMISSION_NAME { get; set; }
        public string TEAM_ID { get; set; }
        public string ITEM_NUMBER { get; set; }
        public string CN_CUST_STYLE { get; set; }
        public string CN_FDS_NO { get; set; }
        public string CN_CUST_CODE { get; set; }//Link Customer
        public string CN_CUST_NAME { get; set; }
        public string CN_BRAND_CODE { get; set; }//Link Brand
        public string CN_BRAND_NAME { get; set; }
        public string CN_CLASS0 { get; set; }
        public string CN_CLASS1 { get; set; }
        public string CN_CLASS2 { get; set; }
        public string CN_GENDER { get; set; }
        public string CN_COLLECTION { get; set; }
        public string CN_SERIES { get; set; }
        public string CN_MAKING { get; set; }
        public string CN_FIT { get; set; }
        public string CN_STYLING { get; set; }
        public string CN_COLLAR { get; set; }
        public string CN_PLACKET { get; set; }
        public string CN_SLEEVE { get; set; }
        public string CN_CUFF { get; set; }
        public string CN_WASHING { get; set; }
        public string CN_POCKET { get; set; }
        public string CN_EMB { get; set; }
        public string CN_PRT { get; set; }
        public string THUMBNAIL { get; set; }
        public float CN_SAH { get; set; }
        public string CN_REMARK { get; set; }
        public string DESCRIPTION { get; set; }
        //public string  ARAS:UNIQUENESS_HELPER {get;set;}
        public DateTime RELEASE_DATE { get; set; }
        public DateTime EFFECTIVE_DATE { get; set; }
        public DateTime SUPERSEDED_DATE { get; set; }


        public List<GarmentStyleColorWay> GarmentStyleColorWayList { get; set; }
        public List<GarmentStyleSizeRange> GarmentStyleSizeRangeList { get; set; }

        public List<GarmentStyleBOM> GarmentStyleBOMList { get; set; }

        public List<GarmentStyleSketch> GarmentStyleSketchList { get; set; }
        public List<GarmentStyleCAD> GarmentStyleCADList { get; set; }
        public List<GarmentStyleDocument> GarmentStyleDocumentList { get; set; }
        public List<GarmentStyleYearSeason> GarmentStyleYearSeason { get; set; }
    }
}