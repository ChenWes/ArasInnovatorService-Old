using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArasInnovatorService.Common
{
    public static class StringHelper
    {
        /// <summary>
        /// get search string right string
        /// </summary>
        /// <param name="pi_tagetString"></param>
        /// <param name="pi_searchString"></param>
        /// <returns></returns>
        public static string GetRightString(string pi_tagetString,string pi_searchString)
        {
            string l_temp = "";

            try
            {
                int l_getStart = pi_tagetString.IndexOf(pi_searchString);

                if (l_getStart != -1)
                {
                    l_temp = pi_tagetString.Substring(l_getStart + pi_searchString.Length, pi_tagetString.Length - l_getStart - pi_searchString.Length);
                }
                return l_temp;

            }
            catch (Exception ex)
            {
                throw;
            }                        
        }
    }
}