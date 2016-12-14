using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class TrimPartList
    {
        private TrimPartBaseType[] trimPartField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TrimPart")]
        public TrimPartBaseType[] TrimPart
        {
            get
            {
                return this.trimPartField;
            }
            set
            {
                this.trimPartField = value;
            }
        }
    }
}