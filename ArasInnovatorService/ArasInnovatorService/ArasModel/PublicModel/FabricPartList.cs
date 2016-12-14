using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class FabricPartList
    {
        private FabricPartBaseType[] fabricPartField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FabricPart")]
        public FabricPartBaseType[] FabricPart
        {
            get
            {
                return this.fabricPartField;
            }
            set
            {
                this.fabricPartField = value;
            }
        }
    }
}