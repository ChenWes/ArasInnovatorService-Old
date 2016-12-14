using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel.PublicModel
{
    public class FabricBOMItemBaseType
    {
        private string fabricCodeField;
        private string garmentComponentField;
        private string cutWayField;
        private string fabricWidthField;
        private string comboNameField;
        private float yPDField;
        private int pIeceFabricUsageField;
        private string pieceFabricSizeField;
        private FabricBOMItemBaseTypeFabricParts fabricPartsField;
        /// <remarks/>
        public string FabricCode
        {
            get
            {
                return this.fabricCodeField;
            }
            set
            {
                this.fabricCodeField = value;
            }
        }
        /// <remarks/>
        public string GarmentComponent
        {
            get
            {
                return this.garmentComponentField;
            }
            set
            {
                this.garmentComponentField = value;
            }
        }
        /// <remarks/>
        public string CutWay
        {
            get
            {
                return this.cutWayField;
            }
            set
            {
                this.cutWayField = value;
            }
        }
        /// <remarks/>
        public string FabricWidth
        {
            get
            {
                return this.fabricWidthField;
            }
            set
            {
                this.fabricWidthField = value;
            }
        }
        /// <remarks/>
        public string ComboName
        {
            get
            {
                return this.comboNameField;
            }
            set
            {
                this.comboNameField = value;
            }
        }
        /// <remarks/>
        public float YPD
        {
            get
            {
                return this.yPDField;
            }
            set
            {
                this.yPDField = value;
            }
        }
        /// <remarks/>
        public int PIeceFabricUsage
        {
            get
            {
                return this.pIeceFabricUsageField;
            }
            set
            {
                this.pIeceFabricUsageField = value;
            }
        }
        /// <remarks/>
        public string PieceFabricSize
        {
            get
            {
                return this.pieceFabricSizeField;
            }
            set
            {
                this.pieceFabricSizeField = value;
            }
        }
        /// <remarks/>
        public FabricBOMItemBaseTypeFabricParts FabricParts
        {
            get
            {
                return this.fabricPartsField;
            }
            set
            {
                this.fabricPartsField = value;
            }
        }
    }
}