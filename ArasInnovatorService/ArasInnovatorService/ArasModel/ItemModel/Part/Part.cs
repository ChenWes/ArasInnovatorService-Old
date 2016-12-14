using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ArasInnovatorService.Common;

namespace ArasInnovatorService.ArasModel
{
    public class Part
    {        
        //public string char ARAS:UNIQUENESS_HELPER {get;set;}
        public string KEYED_NAME { get; set; }
        public string ID { get; set; }
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
        public string LOCKED_BY_ID { get; set; }//Link User
        public string LOCKED_BY_NAME { get; set; }
        public string IS_CURRENT { get; set; }
        public string MINOR_REV { get; set; }
        public string IS_RELEASED { get; set; }
        public string NOT_LOCKABLE { get; set; }
        public string CSS { get; set; }
        public int GENERATION { get; set; }
        public string NEW_VERSION { get; set; }
        public string CONFIG_ID { get; set; }
        public string PERMISSION_ID { get; set; }//Link Permission
        public string PERMISSION_NAME { get; set; }
        public DateTime RELEASE_DATE { get; set; }
        public DateTime EFFECTIVE_DATE { get; set; }
        public string TEAM_ID { get; set; }
        public decimal COST { get; set; }
        public string COST_BASIS { get; set; }
        public string DESCRIPTION { get; set; }
        public string EXTERNAL_ID { get; set; }
        public string EXTERNAL_OWNER { get; set; }
        public string EXTERNAL_TYPE { get; set; }
        public string HAS_CHANGE_PENDING { get; set; }
        public string MAKE_BUY { get; set; }
        public string NAME { get; set; }
        public string CN_THUMBNAIL { get; set; }
        public string UNIT { get; set; }
        public decimal WEIGHT { get; set; }
        public string WEIGHT_BASIS { get; set; }
        public string ITEM_NUMBER { get; set; }
        public string CLASSIFICATION { get; set; }
        public string MAJOR_REV { get; set; }
        public string STATE { get; set; }
        public DateTime SUPERSEDED_DATE { get; set; }
        public string CN_CUST_CODE { get; set; }//Link Customer
        public string CN_CUST_NAME { get; set; }
        public string CN_BRAND_CODE { get; set; }//Link Brand
        public string CN_BRAND_NAME { get; set; }
        public string CN_CUST_ITEM_ID { get; set; }
        public string CN_CLASS0 { get; set; }
        public string CN_CLASS1 { get; set; }
        public string CN_CLASS2 { get; set; }
        public string CN_DYE_METHOD { get; set; }
        public string CN_FINISHING { get; set; }
        public string CN_FABRIC_WIDTH { get; set; }
        public int CN_FABRIC_WEIGHT { get; set; }
        public string CN_FABRIC_PATTERN { get; set; }
        public string CN_PRINTING { get; set; }
        public float CN_REP_V { get; set; }
        public float CN_REP_H { get; set; }
        public string CN_COMBO { get; set; }
        public float CN_FAB_SHK_LEN { get; set; }
        public float CN_FAB_SHK_WIDTH { get; set; }
        public float CN_GMT_SHK_LEN { get; set; }
        public float CN_GMT_SHK_WIDTH { get; set; }
        public string CN_FAB_TEST_GRADING { get; set; }
        public string CN_THREAD_TYPE { get; set; }
        public string CN_THREAD_COUNT { get; set; }
        public float CN_PRICE { get; set; }
        public string CN_SUPPLIER_CODE { get; set; }//Link Vendor
        public string CN_SUPPLIER_ITEM_CODE { get; set; }
        public string CN_SUPPLIER_NAME { get; set; }
        public string CN_PPO_AGPO { get; set; }
        public string CN_REMARKS { get; set; }
        public string CN_QUALITY_CODE { get; set; }
        public string CN_MATERIAL { get; set; }
        public string CN_DTM { get; set; }
        public string CN_LOGO { get; set; }
        public string CN_LOGO_DESCRIPTION { get; set; }
        public string CN_EMBROIDERY_SIZE_A_W { get; set; }
        public string CN_EMBROIDERY_SIZE_B_W { get; set; }
        public string CN_PLACEMENT { get; set; }
        public string CN_PRINTING_TYPE { get; set; }
        public string CN_PRINTING_METHOD { get; set; }
        public string CN_PRINTING_SIZE { get; set; }
        public string CN_HANDFEEL { get; set; }
        public string CN_AGOA { get; set; }
        public string CN_WASHING { get; set; }
        public string CN_YARN_COUNT { get; set; }
        public string CN_YARN_COUNT2 { get; set; }
        public int CN_CONSTRUCTION { get; set; }
        public int CN_CONSTRUCTION2 { get; set; }

        public List<PartDocument> PartDocumentList { get; set; }
        public List<PartCAD> PartCADList { get; set; }
        public List<PartColorCombo> PartColorComboList { get; set; }
        public List<PartSize> PartSizeList { get; set; }
        public List<PartContent> PartContentList { get; set; }
    }
    
}