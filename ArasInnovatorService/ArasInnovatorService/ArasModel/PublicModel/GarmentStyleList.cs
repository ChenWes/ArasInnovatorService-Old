using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class GarmentStyleList
    {
        private ProductBaseType[] garmentStyleField;


        
        public ProductBaseType[] GarmentStyle
        {
            get
            {
                return this.garmentStyleField;
            }
            set
            {
                this.garmentStyleField = value;
            }
        }
    }
}