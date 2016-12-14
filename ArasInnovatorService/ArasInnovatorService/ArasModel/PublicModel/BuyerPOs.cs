using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class BuyerPOs
    {
        private BuyerPO[] buyerPOField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("BuyerPO")]
        public BuyerPO[] BuyerPO
        {
            get
            {
                return this.buyerPOField;
            }
            set
            {
                this.buyerPOField = value;
            }
        }
    }
}