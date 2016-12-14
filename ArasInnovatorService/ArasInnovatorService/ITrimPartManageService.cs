using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using ArasInnovatorService.Common;
using ArasInnovatorService.ArasModel.PublicModel;

namespace ArasInnovatorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITrimPartManageService" in both code and config file together.
    [ServiceContract(Namespace = "http://esquel.com/ArasService/TrimPartManageService")]
    public interface ITrimPartManageService
    {
        [OperationContract]
        TrimPartClass GetTrimPartList(SelectionFilter pi_selectionFilter, int pi_pageIndex, int pi_pageSize);

        [OperationContract]
        TrimPartClass GetTrimPartByID(string pi_trimPartID);

        [OperationContract]
        TrimPartClass GetTrimPartImage(string pi_trimPartID, eumImageType pi_getImageType);

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
