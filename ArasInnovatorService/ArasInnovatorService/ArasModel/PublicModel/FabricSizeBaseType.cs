using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class FabricSizeBaseType
    {
        private decimal measurementField;
        private string dimensionField;
        /// <remarks/>
        public decimal Measurement
        {
            get
            {
                return this.measurementField;
            }
            set
            {
                this.measurementField = value;
            }
        }
        /// <remarks/>
        public string Dimension
        {
            get
            {
                return this.dimensionField;
            }
            set
            {
                this.dimensionField = value;
            }
        }
    }
}