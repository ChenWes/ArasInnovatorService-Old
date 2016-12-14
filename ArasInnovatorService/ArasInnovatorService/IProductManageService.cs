using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using ArasInnovatorService.ArasModel.PublicModel;
using ArasInnovatorService.Common;

namespace ArasInnovatorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductManageService" in both code and config file together.
    [ServiceContract(Namespace = "http://esquel.com/ArasService/ProductManageService")]
    public interface IProductManageService
    {
        //重新定义方法
        //-----------------------------------------------------------------------------------------------

        [OperationContract]
        ProductClass GetProductList(SelectionFilter pi_selectionFilter, int pi_pageIndex, int pi_pageSize);

        [OperationContract]
        ProductClass GetProductByID(string pi_productID);

        [OperationContract]
        ProductClass GetProductImage(string pi_productID, eumImageType pi_getImageType);

        [OperationContract]
        ProductClass CreateProduct(Products pi_productList);

        [OperationContract]
        ProductClass UpdateProduct(Products pi_productList);

        [OperationContract]
        ProductClass RemoveProductByID(string pi_productID);

        //公共方法放入至抛开处理
        //-----------------------------------------------------------------------------------------------
        [OperationContract]
        SelectionFilter CreateAndFilter(SelectionFilter[] filters);

        [OperationContract]
        SelectionFilter CreateOrFilter(SelectionFilter[] filters);

        [OperationContract]
        SelectionFilter CreateLeaf(string attributeName, string searchOperator, string filterValue);
    }
}
