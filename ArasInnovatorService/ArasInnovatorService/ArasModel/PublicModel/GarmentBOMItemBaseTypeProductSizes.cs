using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class GarmentBOMItemBaseTypeProductSizes
    {
        private string[] productSizeField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProductSize")]
        public string[] ProductSize
        {
            get
            {
                return this.productSizeField;
            }
            set
            {
                this.productSizeField = value;
            }
        }
    }
}