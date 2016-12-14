using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class ArrayOfSelectionFilterBaseType
    {
        private SelectionFilterBaseType[] selectionFilterField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SelectionFilter", IsNullable = true)]
        public SelectionFilterBaseType[] SelectionFilter
        {
            get
            {
                return this.selectionFilterField;
            }
            set
            {
                this.selectionFilterField = value;
            }
        }
    }
}