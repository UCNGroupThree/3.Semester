using System.Collections.Generic;
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

        [OperationContract]
        List<Plane> GetAllPlanes();

        [OperationContract]
        List<Plane> GetPlanesWithEqualSeatNumber(int seats);

        [OperationContract]
        List<Plane> GetPlaneswithLessThanOrEqualSeatNumber(int seats);

        [OperationContract]
        List<Plane> GetPlaneswithMoreOrEqualSeatNumber(int seats);
    }
}