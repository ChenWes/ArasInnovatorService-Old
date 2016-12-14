using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aras;
using Aras.IOM;

namespace ArasService.Common
{
    public static class ParseSelectionFilter
    {
        /// <summary>
        /// Parse Selection To Aras Innovator Property Condition
        /// 2015-08-07 add by WesChen
        /// </summary>
        /// <param name="pi_selectionFilter"></param>
        /// <returns></returns>
        public static string ParseSelection(SelectionFilter pi_selectionFilter)
        {
            try
            {
                if (pi_selectionFilter.FilterType == SelectionFilter.FilterTypeLeaf)
                {
                    return ("<" + pi_selectionFilter.AttributeName + " condition=\"" + ConditionHelper.Condition_Mapping(pi_selectionFilter.SearchOperator) + "\">" + ConditionHelper.Condition_Addition(ConditionHelper.Condition_Mapping(pi_selectionFilter.SearchOperator), pi_selectionFilter.FilterValue) + "</ " + pi_selectionFilter.AttributeName + ">");
                }
                else
                {

                    string sSeparator1 = "";
                    string sSeparator2 = "";

                    switch (pi_selectionFilter.FilterType)
                    {
                        case SelectionFilter.FilterTypeAnd:

                            sSeparator1 = "<or>";
                            break;
                        case SelectionFilter.FilterTypeOr:

                            sSeparator1 = "<and>";
                            break;
                    }
                    string[] l_sElement = new string[pi_selectionFilter.Filters.Length];
                    for (int i = 0; i < pi_selectionFilter.Filters.Length; i++)
                    {

                        l_sElement[i] = ParseSelection(pi_selectionFilter.Filters[i]);
                    }

                    switch (pi_selectionFilter.FilterType)
                    {
                        case SelectionFilter.FilterTypeAnd:

                            sSeparator2 = "</or>";
                            break;
                        case SelectionFilter.FilterTypeOr:

                            sSeparator2 = "</and>";
                            break;
                    }

                    return string.Join(sSeparator1, l_sElement) + sSeparator2;

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Parse Selection Error :" + ex.Message);
            }


        }

        /// <summary>
        /// Parse Selection To Aras Innovator Property Condition
        /// 2015-08-07 add by WesChen
        /// </summary>
        /// <param name="pi_selectionFilter"></param>
        /// <param name="pi_searchItem"></param>
        /// <returns></returns>
        public static Item ParseSelection(SelectionFilter pi_selectionFilter,Item pi_searchItem)
        {
            try
            {
                if (pi_selectionFilter.FilterType == SelectionFilter.FilterTypeLeaf)
                {                    
                    pi_searchItem.setPropertyCondition(pi_selectionFilter.AttributeName, ConditionHelper.Condition_Mapping(pi_selectionFilter.SearchOperator), ConditionHelper.Condition_Addition(ConditionHelper.Condition_Mapping(pi_selectionFilter.SearchOperator), pi_selectionFilter.FilterValue));
                    return pi_searchItem;
                }
                else
                {
                    switch (pi_selectionFilter.FilterType)
                    {
                        case SelectionFilter.FilterTypeAnd:
                            pi_searchItem.newAND();                           
                            break;
                        case SelectionFilter.FilterTypeOr:
                            pi_searchItem.newOR();
                            break;
                    }
                    string[] l_sElement = new string[pi_selectionFilter.Filters.Length];
                    for (int i = 0; i < pi_selectionFilter.Filters.Length; i++)
                    {
                        pi_searchItem = ParseSelection(pi_selectionFilter.Filters[i],pi_searchItem);
                    }

                    return pi_searchItem;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Parse Selection Error :" + ex.Message);
            }
        }
    }
}