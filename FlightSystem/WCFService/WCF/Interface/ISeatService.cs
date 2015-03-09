using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface ISeatService {

        [OperationContract]
        int AddSeat(Seat seat);

        [OperationContract]
        void UpdateSeat(Seat seat);

        [OperationContract]
        void DeleteSeat(Seat seat);

        [OperationContract]
        Seat GetSeat(int id);
    }
}