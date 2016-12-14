using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class YearSeasonBaseType
    {
        private string yearField;
        private SeasonEnum seasonField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string Year
        {
            get
            {
                return this.yearField;
            }
            set
            {
                this.yearField = value;
            }
        }
        /// <remarks/>
        public SeasonEnum Season
        {
            get
            {
                return this.seasonField;
            }
            set
            {
                this.seasonField = value;
            }
        }
    }
}