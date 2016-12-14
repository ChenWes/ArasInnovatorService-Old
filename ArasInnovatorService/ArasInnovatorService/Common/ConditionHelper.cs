using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.Common
{
    public static class ConditionHelper
    {
        /// <summary>
        /// Aras Condition String To PWD
        /// 2015-08-07 add by WesChen
        /// </summary>
        /// <param name="pi_strPWDCondition"></param>
        /// <returns></returns>
        public static string Condition_Mapping(string pi_strPWDCondition)
        {
            string l_strArasCondition = "";
            switch (pi_strPWDCondition.Trim().ToUpper())
            {
                case "EQ"://=
                    l_strArasCondition = "eq";
                    break;
                case "NEQ"://<>
                    l_strArasCondition = "ne";
                    break;

                case "GTE"://>=
                    l_strArasCondition = "ge";
                    break;
                case "LTE"://<=
                    l_strArasCondition = "le";
                    break;
                case "GT"://>
                    l_strArasCondition = "gt";
                    break;
                case "LT"://<
                    l_strArasCondition = "lt";
                    break;
                case "CT"://like '%Value%'
                    l_strArasCondition = "like";
                    break;
                case "NCT"://not like
                    l_strArasCondition = "not like";
                    break;

                case "SW"://like 'Value%'
                    l_strArasCondition = "like";
                    break;
                case"EW"://like '%Value'
                    l_strArasCondition = "like";
                    break;         

                //case "BET"://between and
                //    l_strArasCondition = "between";
                //    break;
                //case "NOTBET"://not between and
                //    l_strArasCondition = "not between";
                //    break;
                //case "IN"://in
                //    l_strArasCondition = "in";
                //    break;
                //case "IS"://is(null/not null)
                //    l_strArasCondition = "is";
                //    break;
                //case "INNULL"://is null
                //    l_strArasCondition = "is null";
                //    break;
                //case "ISNOTNULL"://is not null
                //    l_strArasCondition = "is not null";
                //    break;                
                default:
                    break;
            }

            return l_strArasCondition;
        }

        /// <summary>
        /// Aras Conditon Addition '%'
        /// 2015-08-07 add by WesChen
        /// </summary>
        /// <param name="pi_strCondition"></param>
        /// <param name="pi_strValue"></param>
        /// <returns></returns>
        public static string Condition_Addition(string pi_strCondition, string pi_strValue)
        {
            switch (pi_strCondition.ToUpper())
            {
                case"CT":
                    return "%" + pi_strValue + "%";
                    break;
                case"SW":
                    return  pi_strValue + "%";
                    break;
                case"EW":
                    return "%" + pi_strValue;
                    break;
                default:
                    return pi_strValue;
                    break;
            }
        }
    }
}