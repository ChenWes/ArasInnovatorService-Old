using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class ProductFeaturesType
    {
        private string genderField;
        private string collectionField;
        private string seriesField;
        private string makingField;
        private string fitField;
        private string lineField;
        private string collarField;
        private string placketField;
        private string sleeveField;
        private string cuffField;
        private string pocketField;
        private string washingField;
        private string embroideryField;
        private string printingField;
        private string bodyPatternField;
        private float sAHField;
        private string remarksField;
        private YearSeasonBaseType[] yearSeasonField;
        /// <remarks/>
        public string Gender
        {
            get
            {
                return this.genderField;
            }
            set
            {
                this.genderField = value;
            }
        }
        /// <remarks/>
        public string Collection
        {
            get
            {
                return this.collectionField;
            }
            set
            {
                this.collectionField = value;
            }
        }
        /// <remarks/>
        public string Series
        {
            get
            {
                return this.seriesField;
            }
            set
            {
                this.seriesField = value;
            }
        }
        /// <remarks/>
        public string Making
        {
            get
            {
                return this.makingField;
            }
            set
            {
                this.makingField = value;
            }
        }
        /// <remarks/>
        public string Fit
        {
            get
            {
                return this.fitField;
            }
            set
            {
                this.fitField = value;
            }
        }
        /// <remarks/>
        public string Line
        {
            get
            {
                return this.lineField;
            }
            set
            {
                this.lineField = value;
            }
        }
        /// <remarks/>
        public string Collar
        {
            get
            {
                return this.collarField;
            }
            set
            {
                this.collarField = value;
            }
        }
        /// <remarks/>
        public string Placket
        {
            get
            {
                return this.placketField;
            }
            set
            {
                this.placketField = value;
            }
        }
        /// <remarks/>
        public string Sleeve
        {
            get
            {
                return this.sleeveField;
            }
            set
            {
                this.sleeveField = value;
            }
        }
        /// <remarks/>
        public string Cuff
        {
            get
            {
                return this.cuffField;
            }
            set
            {
                this.cuffField = value;
            }
        }
        /// <remarks/>
        public string Pocket
        {
            get
            {
                return this.pocketField;
            }
            set
            {
                this.pocketField = value;
            }
        }
        /// <remarks/>
        public string Washing
        {
            get
            {
                return this.washingField;
            }
            set
            {
                this.washingField = value;
            }
        }
        /// <remarks/>
        public string Embroidery
        {
            get
            {
                return this.embroideryField;
            }
            set
            {
                this.embroideryField = value;
            }
        }
        /// <remarks/>
        public string Printing
        {
            get
            {
                return this.printingField;
            }
            set
            {
                this.printingField = value;
            }
        }
        /// <remarks/>
        public string BodyPattern
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
        public float SAH
        {
            get
            {
                return this.sAHField;
            }
            set
            {
                this.sAHField = value;
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
    }
}