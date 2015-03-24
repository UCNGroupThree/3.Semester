using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCFService.Model;

namespace WCFService.WCF.Interface
{
    [ServiceContract]
    interface IPostalService {

        [OperationContract]
        int AddPostal(Postal postal);

        [OperationContract]
        Postal UpdatePostal(Postal postal);

        [OperationContract]
        int DeletePostal(Postal postal);

        [OperationContract]
        Postal GetPostal(int postNumber);


    }
}
