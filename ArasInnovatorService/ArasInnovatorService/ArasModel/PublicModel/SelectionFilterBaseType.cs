using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class SelectionFilterBaseType
    {
        private string attributeNameField;
        private string filterTypeField;
        private string filterValueField;
        private SelectionFilterBaseType[] filtersField;
        private string searchOperatorField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string AttributeName
        {
            get
            {
                return this.attributeNameField;
            }
            set
            {
                this.attributeNameField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string FilterType
        {
            get
            {
                return this.filterTypeField;
            }
            set
            {
                this.filterTypeField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string FilterValue
        {
            get
            {
                return this.filterValueField;
            }
            set
            {
                this.filterValueField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable = true)]
        [System.Xml.Serialization.XmlArrayItemAttribute("SelectionFilter")]
        public SelectionFilterBaseType[] Filters
        {
            get
            {
                return this.filtersField;
            }
            set
            {
                this.filtersField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string SearchOperator
        {
            get
            {
                return this.searchOperatorField;
            }
            set
            {
                this.searchOperatorField = value;
            }
        }
    }
}