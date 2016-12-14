using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class ColorBaseType
    {
        private string nameField;
        private ColorBaseTypeDocumentURIs documentURIsField;
        private YearSeasonBaseType[] yearSeasonField;
        private string codeField;
        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        /// <remarks/>
        public ColorBaseTypeDocumentURIs DocumentURIs
        {
            get
            {
                return this.documentURIsField;
            }
            set
            {
                this.documentURIsField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("YearSeason")]
        public YearSeasonBaseType[] YearSeason
        {
            get
            {
                return this.yearSeasonField;
            }
            set
            {
                this.yearSeasonField = value;
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