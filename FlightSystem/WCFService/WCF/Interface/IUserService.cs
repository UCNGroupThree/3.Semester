using System;
using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {

    [ServiceContract]
    interface IUserService { //TODO Nick - Lasse

        [OperationContract]
        int AddUser(User user);

        [OperationContract]
        User UpdateUser(User user);

        [OperationContract]
        void DeleteUser(User user);

        [OperationContract]
        User GetUser(int id);
    } 

}