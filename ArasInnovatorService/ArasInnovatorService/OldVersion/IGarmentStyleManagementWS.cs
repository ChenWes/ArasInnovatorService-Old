using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using ArasInnovatorService.ArasModel;
using ArasInnovatorService.ArasModel.PublicModel;
using ArasInnovatorService.Common;

namespace ArasInnovatorService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGarmentStyleManagementWS" in both code and config file together.
    [ServiceContract(Namespace = "http://esquel.com/ArasService/GarmentStyleManagementWS")]
    public interface IGarmentStyleManagementWS
    {
        [OperationContract]
        GarmentStyleClass getGarmentStyleList(GarmentStyleClass pi_GarmentStyleClass);

        [OperationContract]
        GarmentStyleClass getGarmentStyleById(GarmentStyleClass pi_GarmentStyleClass);

        [OperationContract]
        GarmentStyleClass createGarmentStyle(GarmentStyleClass pi_GarmentStyleClass);

        [OperationContract]
        GarmentStyleClass updateGarmentStyle(GarmentStyleClass pi_GarmentStyleClass);

        [OperationContract]
        GarmentStyleClass removeGarmentStyleById(GarmentStyleClass pi_GarmentStyleClass);

        [OperationContract]
        GarmentStyleClass getGarmentStyleImage(GarmentStyleClass pi_GarmentStyleClass);


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
