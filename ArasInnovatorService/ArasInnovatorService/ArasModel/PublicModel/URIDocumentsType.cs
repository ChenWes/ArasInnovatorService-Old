using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class URIDocumentsType
    {
        private string[] documentURIField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DocumentURI", DataType = "anyURI")]
        public string[] DocumentURI
        {
            get
            {
                return this.documentURIField;
            }
            set
            {
                this.documentURIField = value;
            }
        }
    }
}