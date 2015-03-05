using System;
using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface {

    [ServiceContract]
    interface IUserService {

        [OperationContract]
        int AddUser(Administrator usr);

        [OperationContract]
        Administrator GetUser(string name);
    } 

}