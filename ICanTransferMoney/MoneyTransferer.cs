using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ICanTransferMoney
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class MoneyTransferer : ICanTransferMoney
    {
        public bool TransferMoney(string idFrom, string idTo)
        {
            Console.Out.WriteLine("Not implemented yet");
            return false;
        }
    }
}
