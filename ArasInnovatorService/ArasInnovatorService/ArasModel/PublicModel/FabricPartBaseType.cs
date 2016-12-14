using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class FabricPartBaseType
    {
        private string[] pPONumField;
        private FabricPartHeaderType fabricHeaderField;
        private SupplierBaseType[] supplierField;
        private string descriptionField;
        private string idField;
        private string yarnCountField;
        private FabricContentBaseType[] contentField;
        private string constructionField;
        private string dyeMethodField;
        private string finishingField;
        private string widthField;
        private string patternField;
        private string printingField;
        private string repeatVerticalField;
        private string repeatHorizontalField;
        private FabricComboBaseType colorComboField;
        private FabricShrinkageBaseType fabricShrinkageField;
        private GarmentShrinkageBaseType garmentShrinkageField;
        private string testingGradeField;
        private PriceBaseType[] priceField;
        private YearSeasonBaseType[] yearSeasonField;
        private string remarksField;
        private FabricSizeBaseType[] sizeField;
        private URIDocumentsType layoutField;
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PPONum")]
        public string[] PPONum
        {
            get
            {
                return this.pPONumField;
            }
            set
            {
                this.pPONumField = value;
            }
        }
        /// <remarks/>
        public FabricPartHeaderType FabricHeader
        {
            get
            {
                return this.fabricHeaderField;
            }
            set
            {
                this.fabricHeaderField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Supplier")]
        public SupplierBaseType[] Supplier
        {
            get
            {
                return this.supplierField;
            }
            set
            {
                this.supplierField = value;
            }
        }
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
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("YarnCount", DataType = "integer")]
        public string YarnCount
        {
            get
            {
                return this.yarnCountField;
            }
            set
            {
                this.yarnCountField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Content")]
        public FabricContentBaseType[] Content
        {
            get
            {
                return this.contentField;
            }
            set
            {
                this.contentField = value;
            }
        }
        /// <remarks/>
        public string Construction
        {
            get
            {
                return this.constructionField;
            }
            set
            {
                this.constructionField = value;
            }
        }
        /// <remarks/>
        public string DyeMethod
        {
            get
            {
                return this.dyeMethodField;
            }
            set
            {
                this.dyeMethodField = value;
            }
        }
        /// <remarks/>
        public string Finishing
        {
            get
            {
                return this.finishingField;
            }
            set
            {
                this.finishingField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Width", DataType = "integer")]
        public string Width
        {
            get
            {
                return this.widthField;
            }
            set
            {
                this.widthField = value;
            }
        }
        /// <remarks/>
        public string Pattern
        {
            get
            {
                return this.patternField;
            }
            set
            {
                this.patternField = value;
            }
        }
        /// <remarks/>
        public string Printing
        {
            get
            {
                return this.printingField;
            }
            set
            {
                this.printingField = value;
            }
        }
        /// <remarks/>
        public string RepeatVertical
        {
            get
            {
                return this.repeatVerticalField;
            }
            set
            {
                this.repeatVerticalField = value;
            }
        }
        /// <remarks/>
        public string RepeatHorizontal
        {
            get
            {
                return this.repeatHorizontalField;
            }
            set
            {
                this.repeatHorizontalField = value;
            }
        }
        /// <remarks/>
        public FabricComboBaseType ColorCombo
        {
            get
            {
                return this.colorComboField;
            }
            set
            {
                this.colorComboField = value;
            }
        }
        /// <remarks/>
        public FabricShrinkageBaseType FabricShrinkage
        {
            get
            {
                return this.fabricShrinkageField;
            }
            set
            {
                this.fabricShrinkageField = value;
            }
        }
        /// <remarks/>
        public GarmentShrinkageBaseType GarmentShrinkage
        {
            get
            {
                return this.garmentShrinkageField;
            }
            set
            {
                this.garmentShrinkageField = value;
            }
        }
        /// <remarks/>
        public string TestingGrade
        {
            get
            {
                return this.testingGradeField;
            }
            set
            {
                this.testingGradeField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Price")]
        public PriceBaseType[] Price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("YearSeason")]
        public YearSeasonBaseType[] YearSeason
        {
            get
            {
                return this.yearSeasonField;
            }
            set
            {
                this.yearSeasonField = value;
            }
        }
        /// <remarks/>
        public string Remarks
        {
            get
            {
                return this.remarksField;
            }
            set
            {
                this.remarksField = value;
            }
        }
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Size")]
        public FabricSizeBaseType[] Size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }
        /// <remarks/>
        public URIDocumentsType Layout
        {
            get
            {
                return this.layoutField;
            }
            set
            {
                this.layoutField = value;
            }
        }
    }
}