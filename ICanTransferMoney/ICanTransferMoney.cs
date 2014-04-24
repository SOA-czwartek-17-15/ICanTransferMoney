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
        bool transferMoney(Account from, Account to);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MoneyTransferer : ICanTransferMoney
    {
        public bool transferMoney(Account from, Account to)
        {
            Console.Out.WriteLine("Not implemented yet");
            return false;
        }
    }
}
