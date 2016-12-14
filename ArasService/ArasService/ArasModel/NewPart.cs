using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasService.ArasModel
{
    public class NewPart
    {        
        public string part_number { get; set; }
        public string description { get; set; }
        public decimal cost { get; set; }

        public List<FabricPart> tt { get; set; }
    }
}