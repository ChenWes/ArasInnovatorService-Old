using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class SeasonEnum
    {
        private string codeField;
        private string seasonNameField;
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
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SeasonName
        {
            get
            {
                return this.seasonNameField;
            }
            set
            {
                this.seasonNameField = value;
            }
        }
    }
}