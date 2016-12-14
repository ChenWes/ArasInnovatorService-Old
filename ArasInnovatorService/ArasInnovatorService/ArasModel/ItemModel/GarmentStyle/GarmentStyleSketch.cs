using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.ArasModel
{
    public class GarmentStyleSketch
    {
        public string ID { get; set; }
        public string CLASSIFICATION { get; set; }
        public string KEYED_NAME { get; set; }
        public DateTime CREATED_ON { get; set; }
        public string CREATED_BY_ID { get; set; }//Link User
        public string CREATED_BY_NAME { get; set; }
        public string OWNED_BY_ID { get; set; }
        public string MANAGED_BY_ID { get; set; }
        public DateTime MODIFIED_ON { get; set; }
        public string MODIFIED_BY_ID { get; set; }//Link User
        public string MODIFIED_BY_NAME { get; set; }
        public string CURRENT_STATE { get; set; }//Link Life Cycle State
        public string CURRENT_STATE_DESC { get; set; }
        public string STATE { get; set; }
        public string LOCKED_BY_ID { get; set; }//Link User
        public string LOCKED_BY_NAME { get; set; }
        public string IS_CURRENT { get; set; }
        public string MAJOR_REV { get; set; }
        public string MINOR_REV { get; set; }
        public string IS_RELEASED { get; set; }
        public string NOT_LOCKABLE { get; set; }
        public string CSS { get; set; }
        public int GENERATION { get; set; }
        public string NEW_VERSION { get; set; }
        public string CONFIG_ID { get; set; }
        public string PERMISSION_ID { get; set; }//Link Permission
        public string PERMISSION_NAME { get; set; }
        public string SOURCE_ID { get; set; }
        public string BEHAVIOR { get; set; }
        public int SORT_ORDER { get; set; }
        public string TEAM_ID { get; set; }
        public string RELATED_ID { get; set; }
    }
}