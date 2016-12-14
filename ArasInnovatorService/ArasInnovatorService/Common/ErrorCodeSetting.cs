using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.Common
{
    public enum Function
    {
        getGarmentStyleList = 101,
        getGarmentStyleById = 102,
        createGarmentStyle = 103,
        updateGarmentStyle = 104,
        removeGarmentStyleById = 105,
        getGarmentStyleImage = 106,

        getFabricPartList = 201,
        getFabricPartById = 202,
        getFabricPartImage = 203,

        getTrimPartList = 301,
        getTrimPartById = 302,
        getTrimPartImage = 303,

        //Product
        GetProductList=1001,
        GetProductByID=1002,
        GetProductImage=1003,
        CreateProduct=1004,
        UpdateProduct=1005,
        RemoveProductByID=1006,

        //Fabric
        GetFabricPartList=2001,
        GetFabricPartByID=2001,
        GetFabricPartImage=2003,


        //Trim
        GetTrimPartList=3001,
        GetTrimPartByID=3002,
        GetTrimPartImage=3003
    }

    public static class ErrorCodeSetting
    {
        /// <summary>
        /// Sett Error Code
        /// </summary>
        /// <param name="pi_errorCode"></param>
        /// <param name="pi_errorNum"></param>
        /// <param name="pi_arasErrorCode"></param>
        /// <returns></returns>
        public static string GetErrorCode(Function pi_errorCode, string pi_errorNum, string pi_arasErrorCode)
        {
            if(string.IsNullOrEmpty(pi_arasErrorCode))
            {
                return pi_errorCode.ToString() + "-" + pi_errorNum;
            }
            else
            {
                return pi_errorCode.ToString() + "-" + pi_errorNum + "-" + pi_arasErrorCode;
            }
            
        }
    }
}