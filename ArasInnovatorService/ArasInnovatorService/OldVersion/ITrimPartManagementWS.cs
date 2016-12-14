using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using ArasInnovatorService.ArasModel;
using ArasInnovatorService.Common;

namespace ArasInnovatorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITrimPartManagementWS" in both code and config file together.
    [ServiceContract(Namespace = "http://esquel.com/ArasService/TrimPartManagementWS")]
    public interface ITrimPartManagementWS
    {
        [OperationContract]
        TrimPartClass getTrimPartList(TrimPartClass pi_TrimPartClass);

        [OperationContract]
        TrimPartClass getTrimPartById(TrimPartClass pi_TrimPartClass);

        [OperationContract]
        TrimPartClass getTrimPartImage(TrimPartClass pi_TrimPartClass);

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
