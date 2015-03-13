using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface ITicketService { //TODO Senere

        [OperationContract]
        int AddTicket(Ticket ticket);

        [OperationContract]
        void UpdateTicket(Ticket ticket);

        [OperationContract]
        void DeleteTicket(Ticket ticket);

        [OperationContract]
        Ticket GetTicket(int id);
    }
}