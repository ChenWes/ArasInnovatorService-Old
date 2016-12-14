using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class ProductBaseTypeFinalDevelopmentSample
    {
        private string numberField;
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }
    }
}