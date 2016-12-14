using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class GarmentOrder
    {
        private string genericIDField;
        private Product productField;
        private BuyerPOs buyerPOsField;
        /// <remarks/>
        public string GenericID
        {
            get
            {
                return this.genericIDField;
            }
            set
            {
                this.genericIDField = value;
            }
        }
        /// <remarks/>
        public Product Product
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
        /// <remarks/>
        public BuyerPOs BuyerPOs
        {
            get
            {
                return this.buyerPOsField;
            }
            set
            {
                this.buyerPOsField = value;
            }
        }
    }
}