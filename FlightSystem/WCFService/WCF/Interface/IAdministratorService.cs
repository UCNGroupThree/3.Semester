using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface IAdministratorService { //TODO Nick - Login administration?

        [OperationContract]
        int AddAdministrator(Administrator administrator);

        [OperationContract]
        void UpdateAdministrator(Administrator administrator);

        [OperationContract]
        void DeleteAdministrator(Administrator administrator);

        [OperationContract]
        Administrator GetAdministrator(int id);

    }
}