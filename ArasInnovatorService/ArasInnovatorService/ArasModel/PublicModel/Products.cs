using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class Products
    {
        private Product[] productField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Product")]
        public Product[] Product
        {
            get
            {
                return this.productField;
            }
            set
            {
                this.productField = value;
            }
        }
    }
}