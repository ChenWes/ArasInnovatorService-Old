using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class BuyerPO
    {
        private string genericIDField;
        private Product productField;
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
    }
}