using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class GarmentBOMItemBaseType
    {
        private ColorwayBaseType[] colorwayField;
        private GarmentBOMItemBaseTypeProductSizes[] productSizesField;
        private FabricBOMItemBaseType[] fabricBOMField;
        private TrimBOMItemBaseType[] trimBOMField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Colorway")]
        public ColorwayBaseType[] Colorway
        {
            get
            {
                return this.colorwayField;
            }
            set
            {
                this.colorwayField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ProductSizes")]
        public GarmentBOMItemBaseTypeProductSizes[] ProductSizes
        {
            get
            {
                return this.productSizesField;
            }
            set
            {
                this.productSizesField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FabricBOM")]
        public FabricBOMItemBaseType[] FabricBOM
        {
            get
            {
                return this.fabricBOMField;
            }
            set
            {
                this.fabricBOMField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TrimBOM")]
        public TrimBOMItemBaseType[] TrimBOM
        {
            get
            {
                return this.trimBOMField;
            }
            set
            {
                this.trimBOMField = value;
            }
        }
    }
}