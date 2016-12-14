using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class TrimPartHeaderType
    {
        private string versionField;
        private string statusField;
        private TrimCategoryBaseType cateogryField;
        private string subCategoryField;
        private CustomerBaseType[] customerInfoField;
        private string codeField;
        /// <remarks/>
        public string Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
        /// <remarks/>
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
        /// <remarks/>
        public TrimCategoryBaseType Cateogry
        {
            get
            {
                return this.cateogryField;
            }
            set
            {
                this.cateogryField = value;
            }
        }
        /// <remarks/>
        public string SubCategory
        {
            get
            {
                return this.subCategoryField;
            }
            set
            {
                this.subCategoryField = value;
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
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
    }
}