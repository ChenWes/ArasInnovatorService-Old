using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class ColorwayBaseType
    {
        private string pLUField;
        private ColorwayBaseTypeDocumentURIs documentURIsField;
        private string colorwayNameField;
        private string customerReferenceNameField;
        private string customerReferenceCodeField;
        private YearSeasonBaseType[] yearSeasonField;
        private string[] bodyPatternField;
        private string codeField;
        /// <remarks/>
        public string PLU
        {
            get
            {
                return this.pLUField;
            }
            set
            {
                this.pLUField = value;
            }
        }
        /// <remarks/>
        public ColorwayBaseTypeDocumentURIs DocumentURIs
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
        public string ColorwayName
        {
            get
            {
                return this.colorwayNameField;
            }
            set
            {
                this.colorwayNameField = value;
            }
        }
        /// <remarks/>
        public string CustomerReferenceName
        {
            get
            {
                return this.customerReferenceNameField;
            }
            set
            {
                this.customerReferenceNameField = value;
            }
        }
        /// <remarks/>
        public string CustomerReferenceCode
        {
            get
            {
                return this.customerReferenceCodeField;
            }
            set
            {
                this.customerReferenceCodeField = value;
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
        [System.Xml.Serialization.XmlElementAttribute("BodyPattern")]
        public string[] BodyPattern
        {
            get
            {
                return this.bodyPatternField;
            }
            set
            {
                this.bodyPatternField = value;
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