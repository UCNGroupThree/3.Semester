using System.Collections.Generic;
using System.ServiceModel;
using WCFService.Model;
using WCFService.WCF.Faults;

namespace WCFService.WCF.Interface {
    [ServiceContract]
    public interface IAdministratorService { //TODO Nick - Login administration?

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(AlreadyExistFault))]
        [FaultContract(typeof(PasswordFormatFault))]
        [FaultContract(typeof(DatabaseInsertFault))]
        int AddAdministrator(Administrator administrator);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(AlreadyExistFault))]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(DatabaseUpdateFault))]
        void UpdateAdministrator(Administrator administrator);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(NotFoundFault))]
        [FaultContract(typeof(PasswordFormatFault))]
        [FaultContract(typeof(DatabaseUpdateFault))]
        void UpdatePassword(Administrator administrator);

        [OperationContract]
        [FaultContract(typeof(NullPointerFault))]
        [FaultContract(typeof(DatabaseDeleteFault))]
        void DeleteAdministrator(Administrator administrator);

        [OperationContract]
        Administrator GetAdministrator(int id);

        [OperationContract]
        List<Administrator> GetAllAdministrators();

    }
}