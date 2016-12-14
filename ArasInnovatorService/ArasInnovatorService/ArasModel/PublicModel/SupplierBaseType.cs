using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class SupplierBaseType
    {
        private string supplierNameField;
        private string itemCodeField;
        private string codeField;
        /// <remarks/>
        public string SupplierName
        {
            get
            {
                return this.supplierNameField;
            }
            set
            {
                this.supplierNameField = value;
            }
        }
        /// <remarks/>
        public string ItemCode
        {
            get
            {
                return this.itemCodeField;
            }
            set
            {
                this.itemCodeField = value;
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