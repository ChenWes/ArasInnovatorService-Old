using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class TrimBtnLogoBaseType
    {
        private string imageField;
        private string descriptionField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "anyURI")]
        public string Image
        {
            get
            {
                return this.imageField;
            }
            set
            {
                this.imageField = value;
            }
        }
        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
    }
}