using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class FabricContentBaseType
    {
        private string descriptionField;
        private float ratioField;
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
        public float Ratio
        {
            get
            {
                return this.ratioField;
            }
            set
            {
                this.ratioField = value;
            }
        }
    }
}