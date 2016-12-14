using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class TrimBOMItemBaseType
    {
        private string trimCodeField;
        private string trimSizeField;
        private int trimUsageField;
        private TrimBOMItemBaseTypeTrimParts trimPartsField;
        private ColorBaseType trimColorField;
        /// <remarks/>
        public string TrimCode
        {
            get
            {
                return this.trimCodeField;
            }
            set
            {
                this.trimCodeField = value;
            }
        }
        /// <remarks/>
        public string TrimSize
        {
            get
            {
                return this.trimSizeField;
            }
            set
            {
                this.trimSizeField = value;
            }
        }
        /// <remarks/>
        public int TrimUsage
        {
            get
            {
                return this.trimUsageField;
            }
            set
            {
                this.trimUsageField = value;
            }
        }
        /// <remarks/>
        public TrimBOMItemBaseTypeTrimParts TrimParts
        {
            get
            {
                return this.trimPartsField;
            }
            set
            {
                this.trimPartsField = value;
            }
        }
        /// <remarks/>
        public ColorBaseType TrimColor
        {
            get
            {
                return this.trimColorField;
            }
            set
            {
                this.trimColorField = value;
            }
        }
    }
}