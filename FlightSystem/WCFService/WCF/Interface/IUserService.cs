using System;
using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {

    [ServiceContract]
    interface IUserService {

        [OperationContract]
        int AddUser(User user);

        [OperationContract]
        void UpdateUser(User user);

        [OperationContract]
        void DeleteUser(User user);

        [OperationContract]
        User GetUser(int id);
    } 

}