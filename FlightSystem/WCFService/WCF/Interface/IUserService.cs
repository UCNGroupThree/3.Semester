using System;
using System.Collections.Generic;
using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {

    [ServiceContract]
    interface IUserService { 

        [OperationContract]
        int AddUser(User user);

        [OperationContract]
        User UpdateUser(User user);

        [OperationContract]
        void DeleteUser(User user);

        [OperationContract]
        User GetUser(int id);

        [OperationContract]
        List<User> GetUserByName(string name);

        [OperationContract]
        List<User> GetAllUsers();
    } 

}