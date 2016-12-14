using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class CustomerBaseType
    {
        private string customerCodeField;
        private string customerNameField;
        private string customerReferenceNumField;
        private string brandNameField;
        private string brandCodeField;
        /// <remarks/>
        public string CustomerCode
        {
            get
            {
                return this.customerCodeField;
            }
            set
            {
                this.customerCodeField = value;
            }
        }
        /// <remarks/>
        public string CustomerName
        {
            get
            {
                return this.customerNameField;
            }
            set
            {
                this.customerNameField = value;
            }
        }
        /// <remarks/>
        public string CustomerReferenceNum
        {
            get
            {
                return this.customerReferenceNumField;
            }
            set
            {
                this.customerReferenceNumField = value;
            }
        }
        /// <remarks/>
        public string BrandName
        {
            get
            {
                return this.brandNameField;
            }
            set
            {
                this.brandNameField = value;
            }
        }
        /// <remarks/>
        public string BrandCode
        {
            get
            {
                return this.brandCodeField;
            }
            set
            {
                this.brandCodeField = value;
            }
        }
    }
}