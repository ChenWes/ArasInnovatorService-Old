using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class Product
    {
        private ProductBaseType productInfoField;
        private GarmentBOMItemBaseType bOMField;
        /// <remarks/>
        public ProductBaseType ProductInfo
        {
            get
            {
                return this.productInfoField;
            }
            set
            {
                this.productInfoField = value;
            }
        }
        /// <remarks/>
        public GarmentBOMItemBaseType BOM
        {
            get
            {
                return this.bOMField;
            }
            set
            {
                this.bOMField = value;
            }
        }
    }
}