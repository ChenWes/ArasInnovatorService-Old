using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aras;
using Aras.IOM;


namespace ArasInnovatorService.Common
{
    public static class ParseSelectionFilter
    {
        /// <summary>
        /// Parse Selection To Aras Innovator Property Condition
        /// 2015-08-07 add by WesChen
        /// 2015-08-17 update by WesChen Test Successed
        /// </summary>
        /// <param name="pi_selectionFilter"></param>
        /// <returns></returns>
        public static string ParseSelection(SelectionFilter pi_selectionFilter)
        {
            try
            {
                if (pi_selectionFilter.FilterType == SelectionFilter.FilterTypeLeaf)
                {
                    return ("<" + pi_selectionFilter.AttributeName.ToLower() + " condition=\"" + ConditionHelper.Condition_Mapping(pi_selectionFilter.SearchOperator) + "\">" + ConditionHelper.Condition_Addition(ConditionHelper.Condition_Mapping(pi_selectionFilter.SearchOperator), pi_selectionFilter.FilterValue) + "</" + pi_selectionFilter.AttributeName + ">");
                }
                else
                {
                    string sSeparator1 = "";
                    string sSeparator2 = "";

                    //get parameter type
                    switch (pi_selectionFilter.FilterType)
                    {
                        case SelectionFilter.FilterTypeAnd:
                            sSeparator1 = "<and>";
                            sSeparator2 = "</and>";
                            break;
                        case SelectionFilter.FilterTypeOr:
                            sSeparator1 = "<or>";
                            sSeparator2 = "</or>";
                            break;
                    }

                    //get filter
                    string[] l_sElement = new string[pi_selectionFilter.Filters.Length];
                    for (int i = 0; i < pi_selectionFilter.Filters.Length; i++)
                    {
                        l_sElement[i] = ParseSelection(pi_selectionFilter.Filters[i]);
                    }

                    return sSeparator1 + string.Join("", l_sElement) + sSeparator2;                
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
        /// 2015-08-18 update by Raymond Test Successed
        /// </summary>
        /// <param name="pi_selectionFilter"></param>
        /// <param name="pi_searchItem"></param>
        /// <returns></returns>
        public static Item ParseSelection(SelectionFilter pi_selectionFilter,Item pi_searchItem)
        {
            if (pi_searchItem == null)
            {
                return pi_searchItem;
            }

            try
            {

                if (pi_selectionFilter.FilterType == SelectionFilter.FilterTypeLeaf)
                {
                    Item leaf = pi_searchItem.newAND();
                    leaf.setPropertyCondition(pi_selectionFilter.AttributeName.ToLower(), ConditionHelper.Condition_Mapping(pi_selectionFilter.SearchOperator));
                    leaf.setProperty(pi_selectionFilter.AttributeName.ToLower(), ConditionHelper.Condition_Addition(pi_selectionFilter.SearchOperator, pi_selectionFilter.FilterValue));
                }
                else
                {
                    Item newRoot = null;
                    switch (pi_selectionFilter.FilterType)
                    {
                        case SelectionFilter.FilterTypeAnd:
                            newRoot = pi_searchItem.newAND();
                            break;
                        case SelectionFilter.FilterTypeOr:
                            newRoot = pi_searchItem.newOR();
                            break;
                    }


                    int nonLeafChildFilterCount = (
                        from f
                        in pi_selectionFilter.Filters
                        where f != null && f.FilterType != SelectionFilter.FilterTypeLeaf
                        select f).Count();

                    if (nonLeafChildFilterCount == 0)
                    {
                        foreach (SelectionFilter myFilterSrc in pi_selectionFilter.Filters)
                        {
                            newRoot.setPropertyCondition(myFilterSrc.AttributeName.ToLower(), ConditionHelper.Condition_Mapping(myFilterSrc.SearchOperator));
                            newRoot.setProperty(myFilterSrc.AttributeName.ToLower(), ConditionHelper.Condition_Addition(myFilterSrc.SearchOperator, myFilterSrc.FilterValue));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < pi_selectionFilter.Filters.Length; i++)
                        {
                            ParseSelection(pi_selectionFilter.Filters[i], newRoot);
                        }
                    }

                }

                return pi_searchItem;
            }
            catch (Exception ex)
            {
                throw new Exception("Parse Selection Error :" + ex.Message);
            }
        }
        
    }
}