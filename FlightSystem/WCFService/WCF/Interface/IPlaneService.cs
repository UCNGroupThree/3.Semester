using System.Collections.Generic;
using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {
    [ServiceContract]

    public interface IPlaneService {
         
        [OperationContract]
        int AddPlane(Plane plane);

        [OperationContract]
        Plane UpdatePlane(Plane plane);

        [OperationContract]
        void DeletePlane(Plane plane);

        [OperationContract]
        List<Plane> GetPlanesByName(string name);

        [OperationContract]
        Plane  GetPlaneByID(int id);

        [OperationContract]
        List<Plane> GetPlanesWithSeatNumber(int seats);

        [OperationContract]
        List<Plane> GetPlanesWithLessOrEqualSeatNumber(int seats);

        [OperationContract]
        List<Plane> GetPlanesWithMoreOrEqualSeatNumber(int seats);

        [OperationContract]
        List<Plane> GetAllPlanes();
    }
}