using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class TrimPartBaseType
    {
        private TrimPartHeaderType trimHeaderField;
        private string descriptionField;
        private ColorBaseType[] colorField;
        private SizeBaseType[] sizeField;
        private string materialField;
        private string dTMField;
        private string idField;
        private TrimBtnLogoBaseType logoField;
        private PriceBaseType[] priceField;
        private YearSeasonBaseType[] yearSeasonField;
        private SupplierBaseType[] supplierField;
        private string remarksField;
        private URIDocumentsType sketchField;
        private URIDocumentsType cADField;
        private string[] aGPONumField;
        /// <remarks/>
        public TrimPartHeaderType TrimHeader
        {
            get
            {
                return this.trimHeaderField;
            }
            set
            {
                this.trimHeaderField = value;
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
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Color")]
        public ColorBaseType[] Color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Size")]
        public SizeBaseType[] Size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }
        /// <remarks/>
        public string Material
        {
            get
            {
                return this.materialField;
            }
            set
            {
                this.materialField = value;
            }
        }
        /// <remarks/>
        public string DTM
        {
            get
            {
                return this.dTMField;
            }
            set
            {
                this.dTMField = value;
            }
        }
        /// <remarks/>
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        /// <remarks/>
        public TrimBtnLogoBaseType Logo
        {
            get
            {
                return this.logoField;
            }
            set
            {
                this.logoField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Price")]
        public PriceBaseType[] Price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
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
        [System.Xml.Serialization.XmlElementAttribute("Supplier")]
        public SupplierBaseType[] Supplier
        {
            get
            {
                return this.supplierField;
            }
            set
            {
                this.supplierField = value;
            }
        }
        /// <remarks/>
        public string Remarks
        {
            get
            {
                return this.remarksField;
            }
            set
            {
                this.remarksField = value;
            }
        }
        /// <remarks/>
        public URIDocumentsType Sketch
        {
            get
            {
                return this.sketchField;
            }
            set
            {
                this.sketchField = value;
            }
        }
        /// <remarks/>
        public URIDocumentsType CAD
        {
            get
            {
                return this.cADField;
            }
            set
            {
                this.cADField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AGPONum")]
        public string[] AGPONum
        {
            get
            {
                return this.aGPONumField;
            }
            set
            {
                this.aGPONumField = value;
            }
        }
    }
}