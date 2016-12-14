using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasService.Common
{
    /// <summary>
    /// Aras Service Error Class
    /// 2015-08-07 add by WesChen
    /// </summary>
    public class ErrorHelper
    {
        public string ErrorCode { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorString { get; set; }
        public string ErrorDetail { get; set; }        
    }
}