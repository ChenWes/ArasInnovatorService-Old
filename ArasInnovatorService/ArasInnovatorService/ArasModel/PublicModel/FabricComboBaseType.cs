using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class FabricComboBaseType
    {
        private string comboNameField;
        private string codeField;
        /// <remarks/>
        public string ComboName
        {
            get
            {
                return this.comboNameField;
            }
            set
            {
                this.comboNameField = value;
            }
        }
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
    }
}