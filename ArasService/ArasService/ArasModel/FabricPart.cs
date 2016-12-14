using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasService.ArasModel
{
    public class FabricPart
    {

        public String item_number { get; set; }

        public String major_rev { get; set; }
        public String State { get; set; }

        public object cn_cust { get; set; }

        public object cn_cust_code { get; set; }
        public object cn_brand { get; set; }
        public object cn_brand_code { get; set; }

        public String cn_cust_item_id { get; set; }
        public String cn_class0 { get; set; }
        public String cn_class1 { get; set; }

        public String cn_class2 { get; set; }

        public String description { get; set; }

        public String cn_yarn_count { get; set; }

        public String cn_content { get; set; }

        public int cn_content_ratio { get; set; }


        public String cn_construction { get; set; }


        public String cn_dye_method { get; set; }


        public String cn_finishing { get; set; }


        public String cn_fabric_width { get; set; }


        public int cn_fabric_weight { get; set; }


        public String cn_fabric_pattern { get; set; }


        public bool cn_printing { get; set; }

        public float cn_rep_v { get; set; }


        public float cn_rep_h { get; set; }


        public String cn_combo { get; set; }


        public int cn_gmt_shk_len { get; set; }


        public int cn_fab_shk_width { get; set; }


        //public int cn_gmt_shk_len { get; set; }

        public int cn_gmt_shk_width { get; set; }

        public String cn_fab_test_grading { get; set; }

        public float cn_price { get; set; }
        //public String cn_dye_method { get; set; }

        public String cn_season { get; set; }

        public object cn_supplier { get; set; }
        public object cn_supplier_code { get; set; }
        public String cn_supplier_item_code { get; set; }
        public String cn_thumbnail { get; set; }

        public String cn_ppo_agpo { get; set; }
        public String cn_remarks { get; set; }
        public String cn_quality_code { get; set; }

        public String cn_material { get; set; }

        public bool cn_dtm { get; set; }

        public bool cn_logo { get; set; }

        public String cn_logo_description { get; set; }
        public String cn_embroidery_size_a_w { get; set; }

        public String cn_embroidery_size_b_w { get; set; }

        public String cn_placement { get; set; }

        public string imagePath { get; set; }
    }
}