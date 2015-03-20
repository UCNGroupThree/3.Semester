using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {
    [ServiceContract]

    public interface IPlaneService { //TODO Kim
         
        [OperationContract]
        int AddPlane(Plane plane);

        [OperationContract]
        Plane UpdatePlane(Plane plane);

        [OperationContract]
        void DeletePlane(Plane plane);

        [OperationContract]
        Plane GetPlane(int id);
    }
}