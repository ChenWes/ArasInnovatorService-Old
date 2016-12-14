using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class ProductBaseType
    {
        private ProductBaseTypeStyle styleField;
        private string yearField;
        private string seasonField;
        private CustomerBaseType[] customerInfoField;
        private string productTypeField;
        private string productCategoryField;
        private string productSubCategoryField;
        private ProductBaseTypeFinalDevelopmentSample finalDevelopmentSampleField;
        private ProductFeaturesType productFeaturesField;
        private string materialStatusField;
        private URIDocumentsType imageField;
        private URIDocumentsType paperPatternField;
        /// <remarks/>
        public ProductBaseTypeStyle Style
        {
            get
            {
                return this.styleField;
            }
            set
            {
                this.styleField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string Year
        {
            get
            {
                return this.yearField;
            }
            set
            {
                this.yearField = value;
            }
        }
        /// <remarks/>
        public string Season
        {
            get
            {
                return this.seasonField;
            }
            set
            {
                this.seasonField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CustomerInfo")]
        public CustomerBaseType[] CustomerInfo
        {
            get
            {
                return this.customerInfoField;
            }
            set
            {
                this.customerInfoField = value;
            }
        }
        /// <remarks/>
        public string ProductType
        {
            get
            {
                return this.productTypeField;
            }
            set
            {
                this.productTypeField = value;
            }
        }
        /// <remarks/>
        public string ProductCategory
        {
            get
            {
                return this.productCategoryField;
            }
            set
            {
                this.productCategoryField = value;
            }
        }
        /// <remarks/>
        public string ProductSubCategory
        {
            get
            {
                return this.productSubCategoryField;
            }
            set
            {
                this.productSubCategoryField = value;
            }
        }
        /// <remarks/>
        public ProductBaseTypeFinalDevelopmentSample FinalDevelopmentSample
        {
            get
            {
                return this.finalDevelopmentSampleField;
            }
            set
            {
                this.finalDevelopmentSampleField = value;
            }
        }
        /// <remarks/>
        public ProductFeaturesType ProductFeatures
        {
            get
            {
                return this.productFeaturesField;
            }
            set
            {
                this.productFeaturesField = value;
            }
        }
        /// <remarks/>
        public string MaterialStatus
        {
            get
            {
                return this.materialStatusField;
            }
            set
            {
                this.materialStatusField = value;
            }
        }
        /// <remarks/>
        public URIDocumentsType Image
        {
            get
            {
                return this.imageField;
            }
            set
            {
                this.imageField = value;
            }
        }
        /// <remarks/>
        public URIDocumentsType PaperPattern
        {
            get
            {
                return this.paperPatternField;
            }
            set
            {
                this.paperPatternField = value;
            }
        }
    }
}