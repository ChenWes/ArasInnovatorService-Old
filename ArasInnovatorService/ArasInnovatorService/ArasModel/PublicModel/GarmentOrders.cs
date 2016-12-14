using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class GarmentOrders
    {
        private GarmentOrder[] garmentOrderField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("GarmentOrder")]
        public GarmentOrder[] GarmentOrder
        {
            get
            {
                return this.garmentOrderField;
            }
            set
            {
                this.garmentOrderField = value;
            }
        }
    }
}