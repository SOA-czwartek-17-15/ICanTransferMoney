using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ICanTransferMoney
{
    [ServiceContract]
    interface IServiceRepository
    {
        [OperationContract]
        void RegisterService(string name, string address);
    }
}
