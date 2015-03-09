using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface IPlaneService {
        [OperationContract]
        int AddPlane(Plane plane);

        [OperationContract]
        void UpdatePlane(Plane plane);

        [OperationContract]
        void DeletePlane(Plane plane);

        [OperationContract]
        Plane GetPlane(int id);
    }
}