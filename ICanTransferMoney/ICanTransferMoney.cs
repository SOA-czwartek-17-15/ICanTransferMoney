using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ICanTransferMoney
{
    [ServiceContract]
    public interface ICanTransferMoney
    {
        [OperationContract]
        bool TransferMoney(string idFrom, string idTo);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MoneyTransferer : ICanTransferMoney
    {
        public bool TransferMoney(string idFrom, string idTo)
        {
            Console.Out.WriteLine("Not implemented yet");
            return false;
        }
    }
}
